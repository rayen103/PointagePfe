import { Injectable } from '@angular/core';

/** A raw geographic coordinate. */
export interface GeoPoint {
    latitude: number;
    longitude: number;
}

/** A node of the routing graph (departure, collection point or arrival). */
export interface RouteNode extends GeoPoint {
    id: string;
    label?: string;
}

export interface OptimizeRouteRequest {
    start: RouteNode;
    end: RouteNode;
    waypoints: RouteNode[];
    /** Optional polygon (>= 3 vertices) restricting the circuit. */
    polygon?: GeoPoint[];
    /** When true, edges leaving the polygon are heavily penalized and
     *  waypoints outside the polygon are excluded from the route. */
    restrictToPolygon?: boolean;
}

export interface RouteEvaluation {
    /** start → waypoints (in order) → end */
    orderedNodes: RouteNode[];
    totalDistanceKm: number;
    estimatedDurationMinutes: number;
}

export interface OptimizedRoute extends RouteEvaluation {
    orderedWaypointIds: string[];
    /** Waypoints left out because they sit outside the restricted polygon. */
    excludedWaypointIds: string[];
}

export interface PolygonStats {
    areaKm2: number;
    includedCount: number;
    excludedCount: number;
}

/** Average commercial speed used to estimate durations (urban collection routes). */
const AVERAGE_SPEED_KMH = 35;
const EARTH_RADIUS_KM = 6371;
/** Multiplier applied to edges that exit the restricted polygon. */
const OUTSIDE_POLYGON_PENALTY = 25;

/**
 * Route optimization for circuits.
 *
 * Points are modelled as graph nodes and distances as weighted edges.
 * Dijkstra (binary min-heap) computes the shortest path between every pair of
 * nodes — with polygon restriction enabled, direct edges that leave the zone
 * are penalized, so Dijkstra can route around them through other points.
 * The visiting order of the collection points is then built on top of those
 * shortest-path distances (nearest-neighbour construction + 2-opt refinement).
 */
@Injectable({ providedIn: 'root' })
export class DijkstraService {

    // ------------------------------------------------------------------ //
    //  Public API
    // ------------------------------------------------------------------ //

    /** Great-circle distance between two coordinates, in km. */
    haversineKm(a: GeoPoint, b: GeoPoint): number {
        const dLat = this.toRad(b.latitude - a.latitude);
        const dLng = this.toRad(b.longitude - a.longitude);
        const lat1 = this.toRad(a.latitude);
        const lat2 = this.toRad(b.latitude);

        const h =
            Math.sin(dLat / 2) ** 2 +
            Math.cos(lat1) * Math.cos(lat2) * Math.sin(dLng / 2) ** 2;

        return 2 * EARTH_RADIUS_KM * Math.asin(Math.sqrt(h));
    }

    /** Estimated travel time for a distance, in minutes. */
    estimateDurationMinutes(distanceKm: number): number {
        return Math.round((distanceKm / AVERAGE_SPEED_KMH) * 60);
    }

    /**
     * Compute the optimal visiting order of `waypoints` between `start` and `end`.
     */
    optimizeRoute(request: OptimizeRouteRequest): OptimizedRoute | null {
        const { start, end } = request;
        if (!this.isValidNode(start) || !this.isValidNode(end)) {
            return null;
        }

        const polygonActive =
            !!request.restrictToPolygon && (request.polygon?.length ?? 0) >= 3;

        const excluded: string[] = [];
        const waypoints = (request.waypoints ?? []).filter((w) => {
            if (!this.isValidNode(w)) {
                return false;
            }
            if (polygonActive && !this.isPointInPolygon(w, request.polygon!)) {
                excluded.push(w.id);
                return false;
            }
            return true;
        });

        const nodes: RouteNode[] = [start, ...waypoints, end];
        const distances = this.buildShortestPathMatrix(
            nodes,
            polygonActive ? request.polygon : undefined
        );

        // Visiting order over the Dijkstra shortest-path distances:
        // nearest-neighbour gives a good seed, 2-opt removes crossings.
        let order = this.nearestNeighbourOrder(nodes.length, distances);
        order = this.twoOptImprove(order, distances);

        const orderedNodes = order.map((i) => nodes[i]);
        const totalDistanceKm = this.pathDistance(order, distances);

        return {
            orderedNodes,
            orderedWaypointIds: orderedNodes
                .slice(1, orderedNodes.length - 1)
                .map((n) => n.id),
            excludedWaypointIds: excluded,
            totalDistanceKm,
            estimatedDurationMinutes: this.estimateDurationMinutes(totalDistanceKm),
        };
    }

    /**
     * Evaluate a fixed (manual) waypoint order so it can be compared with the
     * optimized one.
     */
    evaluateOrder(start: RouteNode, waypoints: RouteNode[], end: RouteNode): RouteEvaluation | null {
        if (!this.isValidNode(start) || !this.isValidNode(end)) {
            return null;
        }

        const orderedNodes = [start, ...waypoints.filter((w) => this.isValidNode(w)), end];
        let totalDistanceKm = 0;
        for (let i = 0; i < orderedNodes.length - 1; i++) {
            totalDistanceKm += this.haversineKm(orderedNodes[i], orderedNodes[i + 1]);
        }

        return {
            orderedNodes,
            totalDistanceKm,
            estimatedDurationMinutes: this.estimateDurationMinutes(totalDistanceKm),
        };
    }

    /** Ray-casting point-in-polygon test. */
    isPointInPolygon(point: GeoPoint, polygon: GeoPoint[]): boolean {
        if ((polygon?.length ?? 0) < 3) {
            return false;
        }

        let inside = false;
        for (let i = 0, j = polygon.length - 1; i < polygon.length; j = i++) {
            const xi = polygon[i].longitude, yi = polygon[i].latitude;
            const xj = polygon[j].longitude, yj = polygon[j].latitude;

            const intersects =
                yi > point.latitude !== yj > point.latitude &&
                point.longitude <
                    ((xj - xi) * (point.latitude - yi)) / (yj - yi) + xi;

            if (intersects) {
                inside = !inside;
            }
        }
        return inside;
    }

    /** Approximate polygon area in km² (shoelace on an equirectangular projection). */
    polygonAreaKm2(polygon: GeoPoint[]): number {
        if ((polygon?.length ?? 0) < 3) {
            return 0;
        }

        const refLat = this.toRad(polygon[0].latitude);
        const kmPerDegLat = (Math.PI * EARTH_RADIUS_KM) / 180;
        const kmPerDegLng = kmPerDegLat * Math.cos(refLat);

        let area = 0;
        for (let i = 0, j = polygon.length - 1; i < polygon.length; j = i++) {
            const xi = polygon[i].longitude * kmPerDegLng;
            const yi = polygon[i].latitude * kmPerDegLat;
            const xj = polygon[j].longitude * kmPerDegLng;
            const yj = polygon[j].latitude * kmPerDegLat;
            area += xj * yi - xi * yj;
        }
        return Math.abs(area / 2);
    }

    /** Included / excluded counts + area for a polygon over a set of points. */
    polygonStats(polygon: GeoPoint[], points: GeoPoint[]): PolygonStats {
        const usable = (polygon?.length ?? 0) >= 3;
        let included = 0;
        if (usable) {
            for (const p of points) {
                if (this.isPointInPolygon(p, polygon)) {
                    included++;
                }
            }
        }
        return {
            areaKm2: usable ? this.polygonAreaKm2(polygon) : 0,
            includedCount: included,
            excludedCount: usable ? points.length - included : 0,
        };
    }

    // ------------------------------------------------------------------ //
    //  Graph construction + Dijkstra
    // ------------------------------------------------------------------ //

    /**
     * All-pairs shortest-path distances: one Dijkstra run per node over the
     * complete weighted graph of the circuit points.
     */
    private buildShortestPathMatrix(nodes: RouteNode[], polygon?: GeoPoint[]): number[][] {
        const n = nodes.length;
        const adjacency: { to: number; weight: number }[][] =
            Array.from({ length: n }, () => []);

        for (let i = 0; i < n; i++) {
            for (let j = i + 1; j < n; j++) {
                let weight = this.haversineKm(nodes[i], nodes[j]);
                if (polygon && !this.segmentStaysInPolygon(nodes[i], nodes[j], polygon)) {
                    weight *= OUTSIDE_POLYGON_PENALTY;
                }
                adjacency[i].push({ to: j, weight });
                adjacency[j].push({ to: i, weight });
            }
        }

        return nodes.map((_, source) => this.dijkstra(adjacency, source));
    }

    /** Classic Dijkstra with a binary min-heap; returns distances from `source`. */
    private dijkstra(adjacency: { to: number; weight: number }[][], source: number): number[] {
        const n = adjacency.length;
        const dist = new Array<number>(n).fill(Number.POSITIVE_INFINITY);
        const visited = new Array<boolean>(n).fill(false);
        dist[source] = 0;

        const heap = new MinHeap();
        heap.push(source, 0);

        while (heap.size > 0) {
            const { index: u, priority } = heap.pop()!;
            if (visited[u] || priority > dist[u]) {
                continue;
            }
            visited[u] = true;

            for (const edge of adjacency[u]) {
                const candidate = dist[u] + edge.weight;
                if (candidate < dist[edge.to]) {
                    dist[edge.to] = candidate;
                    heap.push(edge.to, candidate);
                }
            }
        }

        return dist;
    }

    /** Sample the segment; every sample must stay inside the polygon. */
    private segmentStaysInPolygon(a: GeoPoint, b: GeoPoint, polygon: GeoPoint[]): boolean {
        const SAMPLES = 8;
        for (let s = 0; s <= SAMPLES; s++) {
            const t = s / SAMPLES;
            const sample: GeoPoint = {
                latitude: a.latitude + (b.latitude - a.latitude) * t,
                longitude: a.longitude + (b.longitude - a.longitude) * t,
            };
            if (!this.isPointInPolygon(sample, polygon)) {
                return false;
            }
        }
        return true;
    }

    // ------------------------------------------------------------------ //
    //  Ordering heuristics (fixed start = 0, fixed end = n - 1)
    // ------------------------------------------------------------------ //

    private nearestNeighbourOrder(n: number, dist: number[][]): number[] {
        const endIndex = n - 1;
        const remaining = new Set<number>();
        for (let i = 1; i < endIndex; i++) {
            remaining.add(i);
        }

        const order = [0];
        let current = 0;
        while (remaining.size > 0) {
            let best = -1;
            let bestDistance = Number.POSITIVE_INFINITY;
            for (const candidate of remaining) {
                if (dist[current][candidate] < bestDistance) {
                    bestDistance = dist[current][candidate];
                    best = candidate;
                }
            }
            order.push(best);
            remaining.delete(best);
            current = best;
        }

        order.push(endIndex);
        return order;
    }

    /** 2-opt: reverse sub-sequences while it shortens the path (start/end pinned). */
    private twoOptImprove(order: number[], dist: number[][]): number[] {
        const improved = [...order];
        const n = improved.length;
        let changed = true;

        while (changed) {
            changed = false;
            for (let i = 1; i < n - 2; i++) {
                for (let k = i + 1; k < n - 1; k++) {
                    const before =
                        dist[improved[i - 1]][improved[i]] +
                        dist[improved[k]][improved[k + 1]];
                    const after =
                        dist[improved[i - 1]][improved[k]] +
                        dist[improved[i]][improved[k + 1]];

                    if (after + 1e-9 < before) {
                        // Reverse the segment [i, k]
                        for (let a = i, b = k; a < b; a++, b--) {
                            [improved[a], improved[b]] = [improved[b], improved[a]];
                        }
                        changed = true;
                    }
                }
            }
        }

        return improved;
    }

    private pathDistance(order: number[], dist: number[][]): number {
        let total = 0;
        for (let i = 0; i < order.length - 1; i++) {
            total += dist[order[i]][order[i + 1]];
        }
        return total;
    }

    private isValidNode(node: RouteNode | null | undefined): node is RouteNode {
        return (
            !!node &&
            node.latitude != null &&
            node.longitude != null &&
            !Number.isNaN(Number(node.latitude)) &&
            !Number.isNaN(Number(node.longitude))
        );
    }

    private toRad(deg: number): number {
        return (deg * Math.PI) / 180;
    }
}

/** Minimal binary min-heap keyed by priority (distance). */
class MinHeap {
    private items: { index: number; priority: number }[] = [];

    get size(): number {
        return this.items.length;
    }

    push(index: number, priority: number): void {
        this.items.push({ index, priority });
        let i = this.items.length - 1;
        while (i > 0) {
            const parent = (i - 1) >> 1;
            if (this.items[parent].priority <= this.items[i].priority) {
                break;
            }
            [this.items[parent], this.items[i]] = [this.items[i], this.items[parent]];
            i = parent;
        }
    }

    pop(): { index: number; priority: number } | undefined {
        if (this.items.length === 0) {
            return undefined;
        }
        const top = this.items[0];
        const last = this.items.pop()!;
        if (this.items.length > 0) {
            this.items[0] = last;
            let i = 0;
            for (;;) {
                const left = 2 * i + 1;
                const right = 2 * i + 2;
                let smallest = i;
                if (left < this.items.length && this.items[left].priority < this.items[smallest].priority) {
                    smallest = left;
                }
                if (right < this.items.length && this.items[right].priority < this.items[smallest].priority) {
                    smallest = right;
                }
                if (smallest === i) {
                    break;
                }
                [this.items[smallest], this.items[i]] = [this.items[i], this.items[smallest]];
                i = smallest;
            }
        }
        return top;
    }
}
