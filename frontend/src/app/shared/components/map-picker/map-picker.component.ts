import {
    AfterViewInit,
    ChangeDetectionStrategy,
    Component,
    EventEmitter,
    Input,
    NgZone,
    OnChanges,
    OnDestroy,
    Output,
    SimpleChanges,
    ViewEncapsulation,
} from '@angular/core';
import * as L from 'leaflet';
import { LatLng, Map, Marker, Polyline } from 'leaflet';
import 'leaflet-routing-machine';
import { GeoPoint } from '../../../core/circuit/dijkstra.service';

export type MapRoutePointKind = 'departure' | 'arrival' | 'stop' | 'poi';
export type PolygonMode = 'none' | 'draw' | 'edit';

export interface MapRoutePoint {
    latitude: number;
    longitude: number;
    label?: string;
    /** Identifier emitted through `pointClick` when the marker is clicked. */
    id?: string;
    /** Departure / arrival / stop (route) or poi (plain dot, cluster-able). */
    kind?: MapRoutePointKind;
    /** 1-based visiting order shown inside stop markers. */
    order?: number;
    /** Point sits outside the restricted polygon — rendered with a warning style. */
    outsideZone?: boolean;
    /** Per-point color override (poi markers). */
    color?: string;
}

/** Above this count, poi markers are grouped into clusters. */
const CLUSTER_THRESHOLD = 25;
/** Screen distance (px) within which poi markers merge into one cluster. */
const CLUSTER_RADIUS_PX = 60;

const TUNISIA_BOUNDS = L.latLngBounds(
    [30.1, 7.5],
    [37.6, 11.8]
);

@Component({
    selector: 'app-map-picker',
    standalone: true,
    templateUrl: './map-picker.component.html',
    styleUrl: './map-picker.component.scss',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MapPickerComponent implements AfterViewInit, OnChanges, OnDestroy {
    private static nextMapId = 0;
    @Input() latitude: number | null = 36.8065; // Default to Tunis, Tunisia
    @Input() longitude: number | null = 10.1815;
    @Input() zoom: number = 7; // Show Tunisia
    @Input() height: string = '400px';
    @Input() routePoints: MapRoutePoint[] = [];
    @Input() color: string = '#2563eb';
    /** When false, map clicks no longer place the base marker. */
    @Input() enableMainMarker: boolean = true;
    /** When false, only markers are shown (no OSRM route between them). */
    @Input() drawRoute: boolean = true;
    /** Polygon (geographic zone) vertices. */
    @Input() polygon: GeoPoint[] = [];
    /** 'draw' appends vertices on click, 'edit' makes vertices draggable. */
    @Input() polygonMode: PolygonMode = 'none';
    @Output() locationChange = new EventEmitter<{ latitude: number; longitude: number }>();
    @Output() polygonChange = new EventEmitter<GeoPoint[]>();
    /** Emitted when the user closes the polygon (double-click while drawing). */
    @Output() polygonDrawFinished = new EventEmitter<GeoPoint[]>();
    /** Emitted with the point id when a marker is clicked. */
    @Output() pointClick = new EventEmitter<string>();

    readonly mapElementId: string = `map-picker-${MapPickerComponent.nextMapId++}`;
    private map: Map | null = null;
    private marker: Marker | null = null;
    private routeMarkers: Marker[] = [];
    private routeControl: L.Routing.Control | null = null;
    private routePolyline: Polyline | null = null;
    private routeUpdateTimer: ReturnType<typeof setTimeout> | null = null;
    private poiPoints: MapRoutePoint[] = [];
    private poiMarkers: Marker[] = [];

    private workingPolygon: GeoPoint[] = [];
    private polygonLayer: L.Polygon | null = null;
    private polygonVertexMarkers: Marker[] = [];
    private resizeObserver: ResizeObserver | null = null;

    constructor(private _ngZone: NgZone) {}

    ngAfterViewInit(): void {
        this.initMap();
    }

    ngOnChanges(changes: SimpleChanges): void {
        if (!this.map) {
            return;
        }

        if (changes['latitude'] || changes['longitude']) {
            this.syncMainMarkerPosition();
        }

        if (changes['routePoints'] || changes['color']) {
            this.scheduleRouteUpdate();
        }

        if (changes['polygon'] || changes['polygonMode'] || changes['color']) {
            this.workingPolygon = [...(this.polygon ?? [])];
            this.renderPolygon();
        }
    }

    private initMap(): void {
        // Create the map centered on Tunisia
        const center: LatLng = L.latLng(
            this.latitude ?? 36.8065,
            this.longitude ?? 10.1815
        );

        // Leaflet events run outside Angular to avoid change detection storms
        this._ngZone.runOutsideAngular(() => {
            this.map = L.map(this.mapElementId, {
                center: center,
                zoom: this.zoom,
                maxBounds: TUNISIA_BOUNDS,
                maxBoundsViscosity: 1.0,
                doubleClickZoom: false,
                preferCanvas: true,
            });

            // Add OpenStreetMap tile layer
            L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
                attribution: '© OpenStreetMap contributors',
                maxZoom: 19,
            }).addTo(this.map);

            // Add a marker if coordinates are provided
            if (this.enableMainMarker && this.latitude != null && this.longitude != null) {
                this.marker = L.marker(center, { icon: this.createMainMarkerIcon(), draggable: true })
                    .addTo(this.map)
                    .bindPopup('Position de référence');

                this.setupMarkerDragHandler(this.marker);
            }

            this.map.on('click', (event: L.LeafletMouseEvent) => {
                this.onMapClick(event);
            });

            this.map.on('dblclick', () => {
                if (this.polygonMode === 'draw' && this.workingPolygon.length >= 3) {
                    this._ngZone.run(() => {
                        this.polygonDrawFinished.emit([...this.workingPolygon]);
                    });
                }
            });

            // Re-cluster poi markers when the zoom level changes
            this.map.on('zoomend', () => {
                this.renderPoiMarkers();
            });

            // Keep the map sized correctly when its container is lazy-rendered/resized
            const container = this.map.getContainer();
            this.resizeObserver = new ResizeObserver(() => {
                this.map?.invalidateSize();
            });
            this.resizeObserver.observe(container);

            this.workingPolygon = [...(this.polygon ?? [])];
            this.renderPolygon();
            this.updateRouteOverlay();
        });
    }

    // ------------------------------------------------------------------ //
    //  Click handling (base marker or polygon vertex)
    // ------------------------------------------------------------------ //

    private onMapClick(event: L.LeafletMouseEvent): void {
        const { lat, lng } = event.latlng;

        // Only allow interactions within Tunisia
        if (!TUNISIA_BOUNDS.contains([lat, lng])) {
            return;
        }

        if (this.polygonMode === 'draw') {
            this.workingPolygon = [...this.workingPolygon, { latitude: lat, longitude: lng }];
            this.renderPolygon();
            this._ngZone.run(() => this.polygonChange.emit([...this.workingPolygon]));
            return;
        }

        if (this.polygonMode === 'edit' || !this.enableMainMarker) {
            return;
        }

        if (this.marker) {
            this.marker.setLatLng(event.latlng);
        } else {
            this.marker = L.marker(event.latlng, { icon: this.createMainMarkerIcon(), draggable: true })
                .addTo(this.map!)
                .bindPopup('Position de référence');

            this.setupMarkerDragHandler(this.marker);
        }

        this._ngZone.run(() => {
            this.locationChange.emit({ latitude: lat, longitude: lng });
        });
    }

    private syncMainMarkerPosition(): void {
        if (!this.map || !this.enableMainMarker || this.latitude == null || this.longitude == null) {
            return;
        }

        const mainPosition = L.latLng(this.latitude, this.longitude);
        if (this.marker) {
            this.marker.setLatLng(mainPosition);
        } else {
            this.marker = L.marker(mainPosition, { icon: this.createMainMarkerIcon(), draggable: true })
                .addTo(this.map)
                .bindPopup('Position de référence');
            this.setupMarkerDragHandler(this.marker);
        }
    }

    // ------------------------------------------------------------------ //
    //  Markers (CSS divIcons — no external image requests)
    // ------------------------------------------------------------------ //

    private createMainMarkerIcon(): L.DivIcon {
        return L.divIcon({
            className: 'mp-marker-wrap',
            html: `<span class="mp-marker mp-marker--base"></span>`,
            iconSize: [22, 22],
            iconAnchor: [11, 11],
            popupAnchor: [0, -12],
        });
    }

    private createRouteMarkerIcon(point: MapRoutePoint, index: number, total: number): L.DivIcon {
        const kind: MapRoutePointKind =
            point.kind ?? (index === 0 ? 'departure' : index === total - 1 ? 'arrival' : 'stop');

        let inner: string;
        if (kind === 'poi') {
            inner = `<span class="mp-poi" style="--mp-color:${point.color ?? this.color}"></span>`;
        } else if (kind === 'departure') {
            inner = `<span class="mp-pin mp-pin--start${point.outsideZone ? ' is-out' : ''}">
                        <svg viewBox="0 0 24 24" width="12" height="12" fill="#fff"><path d="M8 5v14l11-7z"/></svg>
                     </span>`;
        } else if (kind === 'arrival') {
            inner = `<span class="mp-pin mp-pin--end${point.outsideZone ? ' is-out' : ''}">
                        <svg viewBox="0 0 24 24" width="12" height="12" fill="#fff"><path d="M14.4 6L14 4H5v17h2v-7h5.6l.4 2h7V6z"/></svg>
                     </span>`;
        } else {
            const orderLabel = point.order != null ? String(point.order) : String(index);
            inner = `<span class="mp-pin mp-pin--stop${point.outsideZone ? ' is-out' : ''}" style="--mp-color:${this.color}">${orderLabel}</span>`;
        }

        return L.divIcon({
            className: 'mp-marker-wrap',
            html: inner,
            iconSize: [28, 28],
            iconAnchor: [14, 14],
            popupAnchor: [0, -16],
        });
    }

    // ------------------------------------------------------------------ //
    //  Route overlay
    // ------------------------------------------------------------------ //

    /** Coalesce rapid input changes into a single route redraw (OSRM request). */
    private scheduleRouteUpdate(): void {
        if (this.routeUpdateTimer) {
            clearTimeout(this.routeUpdateTimer);
        }
        this.routeUpdateTimer = setTimeout(() => {
            this._ngZone.runOutsideAngular(() => this.updateRouteOverlay());
        }, 250);
    }

    private updateRouteOverlay(): void {
        if (!this.map) {
            return;
        }

        this.routeMarkers.forEach((marker) => marker.remove());
        this.routeMarkers = [];

        if (this.routePolyline) {
            this.routePolyline.remove();
            this.routePolyline = null;
        }
        if (this.routeControl) {
            this.routeControl.remove();
            this.routeControl = null;
        }

        const validPoints = (this.routePoints ?? [])
            .filter((point) => point.latitude != null && point.longitude != null);

        const validRoutePoints = validPoints.filter((point) => point.kind !== 'poi');
        this.poiPoints = validPoints.filter((point) => point.kind === 'poi');
        this.renderPoiMarkers();

        if (validPoints.length === 0) {
            return;
        }

        const latLngs = validRoutePoints.map((point) => L.latLng(point.latitude, point.longitude));

        if (this.drawRoute && latLngs.length > 1) {
            this.routeControl = L.Routing.control({
                waypoints: latLngs,
                show: false,
                addWaypoints: false,
                fitSelectedRoutes: false,
                routeWhileDragging: false,
                createMarker: () => null,
                lineOptions: {
                    styles: [
                        { color: '#ffffff', weight: 7, opacity: 0.85 },
                        { color: this.color, weight: 4, opacity: 0.95 },
                    ],
                    extendToWaypoints: true,
                    missingRouteTolerance: 0,
                },
            } as any)
                .on('routingerror', () => {
                    this.routePolyline = L.polyline(latLngs, {
                        color: this.color,
                        weight: 4,
                        opacity: 0.8,
                        dashArray: '8 6',
                    }).addTo(this.map!);
                })
                .addTo(this.map);
        }

        validRoutePoints.forEach((point, index) => {
            const routeIcon = this.createRouteMarkerIcon(point, index, validRoutePoints.length);
            const markerLabel = point.label ?? (index === 0
                ? 'Départ'
                : index === validRoutePoints.length - 1
                    ? 'Arrivée'
                    : `Point ${index + 1}`);

            const routeMarker = L.marker([point.latitude, point.longitude], { icon: routeIcon })
                .addTo(this.map!)
                .bindPopup(markerLabel);

            if (point.id) {
                routeMarker.on('click', () => {
                    this._ngZone.run(() => this.pointClick.emit(point.id!));
                });
            }

            this.routeMarkers.push(routeMarker);
        });

        this.fitToContent([
            ...latLngs,
            ...this.poiPoints.map((p) => L.latLng(p.latitude, p.longitude)),
        ]);
    }

    /**
     * Render poi markers, grouped into count-clusters when there are many of
     * them (recomputed on zoom, based on on-screen pixel distance).
     */
    private renderPoiMarkers(): void {
        if (!this.map) {
            return;
        }

        this.poiMarkers.forEach((marker) => marker.remove());
        this.poiMarkers = [];

        const points = this.poiPoints;
        if (points.length === 0) {
            return;
        }

        const clusters: { x: number; y: number; points: MapRoutePoint[] }[] = [];
        if (points.length > CLUSTER_THRESHOLD) {
            const zoom = this.map.getZoom();
            for (const point of points) {
                const projected = this.map.project(L.latLng(point.latitude, point.longitude), zoom);
                const hit = clusters.find(
                    (c) => Math.hypot(c.x - projected.x, c.y - projected.y) < CLUSTER_RADIUS_PX
                );
                if (hit) {
                    hit.points.push(point);
                } else {
                    clusters.push({ x: projected.x, y: projected.y, points: [point] });
                }
            }
        } else {
            points.forEach((point) =>
                clusters.push({ x: 0, y: 0, points: [point] })
            );
        }

        for (const cluster of clusters) {
            if (cluster.points.length === 1) {
                const point = cluster.points[0];
                const marker = L.marker([point.latitude, point.longitude], {
                    icon: this.createRouteMarkerIcon(point, 1, 3),
                })
                    .addTo(this.map)
                    .bindPopup(point.label ?? '');

                if (point.id) {
                    marker.on('click', () => {
                        this._ngZone.run(() => this.pointClick.emit(point.id!));
                    });
                }
                this.poiMarkers.push(marker);
                continue;
            }

            // Cluster marker: centroid + count; click zooms onto its points
            const centroidLat = cluster.points.reduce((s, p) => s + p.latitude, 0) / cluster.points.length;
            const centroidLng = cluster.points.reduce((s, p) => s + p.longitude, 0) / cluster.points.length;
            const clusterMarker = L.marker([centroidLat, centroidLng], {
                icon: L.divIcon({
                    className: 'mp-marker-wrap',
                    html: `<span class="mp-cluster">${cluster.points.length}</span>`,
                    iconSize: [36, 36],
                    iconAnchor: [18, 18],
                }),
            }).addTo(this.map);

            const clusterBounds = L.latLngBounds(
                cluster.points.map((p) => [p.latitude, p.longitude] as [number, number])
            );
            clusterMarker.on('click', () => {
                this.map?.fitBounds(clusterBounds, { padding: [60, 60], maxZoom: 15 });
            });

            this.poiMarkers.push(clusterMarker);
        }
    }

    private fitToContent(routeLatLngs: LatLng[]): void {
        if (!this.map) {
            return;
        }

        const boundsPoints = [...routeLatLngs];
        if (this.marker) {
            boundsPoints.push(this.marker.getLatLng());
        }
        this.workingPolygon.forEach((v) => boundsPoints.push(L.latLng(v.latitude, v.longitude)));

        if (boundsPoints.length > 0) {
            this.map.fitBounds(L.latLngBounds(boundsPoints), { padding: [40, 40], maxZoom: 14 });
        }
    }

    // ------------------------------------------------------------------ //
    //  Polygon (geographic zone)
    // ------------------------------------------------------------------ //

    private renderPolygon(): void {
        if (!this.map) {
            return;
        }

        if (this.polygonLayer) {
            this.polygonLayer.remove();
            this.polygonLayer = null;
        }
        this.polygonVertexMarkers.forEach((m) => m.remove());
        this.polygonVertexMarkers = [];

        const vertices = this.workingPolygon;
        if (vertices.length === 0) {
            return;
        }

        const latLngs = vertices.map((v) => L.latLng(v.latitude, v.longitude));

        this.polygonLayer = L.polygon(latLngs, {
            color: this.color,
            weight: 2,
            dashArray: this.polygonMode === 'draw' ? '6 6' : undefined,
            fillColor: this.color,
            fillOpacity: 0.08,
            interactive: false,
        }).addTo(this.map);

        // Vertex handles (visible while drawing or editing)
        if (this.polygonMode === 'draw' || this.polygonMode === 'edit') {
            vertices.forEach((vertex, index) => {
                const handle = L.marker([vertex.latitude, vertex.longitude], {
                    icon: L.divIcon({
                        className: 'mp-marker-wrap',
                        html: `<span class="mp-vertex" style="--mp-color:${this.color}"></span>`,
                        iconSize: [14, 14],
                        iconAnchor: [7, 7],
                    }),
                    draggable: this.polygonMode === 'edit',
                }).addTo(this.map!);

                if (this.polygonMode === 'edit') {
                    handle.on('drag', (event: L.LeafletEvent) => {
                        const position = (event.target as Marker).getLatLng();
                        this.workingPolygon[index] = {
                            latitude: position.lat,
                            longitude: position.lng,
                        };
                        this.polygonLayer?.setLatLngs(
                            this.workingPolygon.map((v) => L.latLng(v.latitude, v.longitude))
                        );
                    });
                    handle.on('dragend', () => {
                        this._ngZone.run(() => this.polygonChange.emit([...this.workingPolygon]));
                    });
                    // Right-click removes a vertex (a triangle minimum is kept)
                    handle.on('contextmenu', () => {
                        if (this.workingPolygon.length <= 3) {
                            return;
                        }
                        this.workingPolygon = this.workingPolygon.filter((_, i) => i !== index);
                        this.renderPolygon();
                        this._ngZone.run(() => this.polygonChange.emit([...this.workingPolygon]));
                    });
                }

                this.polygonVertexMarkers.push(handle);
            });
        }
    }

    private setupMarkerDragHandler(marker: Marker): void {
        // Prevent dragging outside Tunisia
        marker.on('drag', (event) => {
            const position = event.target.getLatLng();
            if (!TUNISIA_BOUNDS.contains(position)) {
                // Constrain to Tunisia bounds
                const lat = Math.max(TUNISIA_BOUNDS.getSouth(), Math.min(TUNISIA_BOUNDS.getNorth(), position.lat));
                const lng = Math.max(TUNISIA_BOUNDS.getWest(), Math.min(TUNISIA_BOUNDS.getEast(), position.lng));
                event.target.setLatLng([lat, lng]);
            }
        });

        marker.on('dragend', (event) => {
            const position = event.target.getLatLng();
            this._ngZone.run(() => {
                this.locationChange.emit({
                    latitude: position.lat,
                    longitude: position.lng,
                });
            });
        });
    }

    ngOnDestroy(): void {
        if (this.routeUpdateTimer) {
            clearTimeout(this.routeUpdateTimer);
            this.routeUpdateTimer = null;
        }
        this.resizeObserver?.disconnect();
        this.resizeObserver = null;

        this.routeMarkers.forEach((marker) => marker.remove());
        this.routeMarkers = [];
        this.poiMarkers.forEach((marker) => marker.remove());
        this.poiMarkers = [];
        this.polygonVertexMarkers.forEach((marker) => marker.remove());
        this.polygonVertexMarkers = [];

        if (this.polygonLayer) {
            this.polygonLayer.remove();
            this.polygonLayer = null;
        }
        if (this.routePolyline) {
            this.routePolyline.remove();
            this.routePolyline = null;
        }
        if (this.routeControl) {
            this.routeControl.remove();
            this.routeControl = null;
        }

        if (this.map) {
            this.map.remove();
            this.map = null;
        }
    }
}
