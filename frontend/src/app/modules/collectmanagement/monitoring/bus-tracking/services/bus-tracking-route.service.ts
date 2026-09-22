import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { CircuitPointCollecte } from '../../../../../core/circuit/circuit-point-collecte.model';

export interface RouteSegment {
  geometry: [number, number][];
  distance: number;
  duration: number;
}

export interface OptimizedRouteResult {
  orderedPoints: CircuitPointCollecte[];
  totalDistanceKm: number;
  estimatedDurationMinutes: number;
  segments: RouteSegment[];
  geometry: [number, number][];
}

@Injectable({ providedIn: 'root' })
export class BusTrackingRouteService {
  constructor(private http: HttpClient) {}

  calculateOptimizedRoute(
    startLat: number,
    startLon: number,
    collectionPoints: CircuitPointCollecte[],
    arrivalLat: number,
    arrivalLon: number
  ): Observable<OptimizedRouteResult> {
    const validPoints = (collectionPoints ?? [])
      .filter((p) => p.latitude != null && p.longitude != null)
      .sort((a, b) => (a.ordre ?? 0) - (b.ordre ?? 0));

    const nodes: { latitude: number; longitude: number }[] = [
      { latitude: startLat, longitude: startLon },
      ...validPoints.map((p) => ({ latitude: p.latitude!, longitude: p.longitude! })),
      { latitude: arrivalLat, longitude: arrivalLon },
    ].filter((n) => n.latitude != null && n.longitude != null);

    if (nodes.length < 2) {
      return of(this.buildEmptyResult(validPoints));
    }

    const coordinatesParam = nodes
      .map((p) => `${p.longitude.toFixed(6)},${p.latitude.toFixed(6)}`)
      .join(';');

    const urlOsmDe = `https://routing.openstreetmap.de/routed-car/route/v1/driving/${coordinatesParam}?overview=full&geometries=geojson`;
    const urlOsrmOrg = `https://router.project-osrm.org/route/v1/driving/${coordinatesParam}?overview=full&geometries=geojson`;

    return this.http.get<any>(urlOsmDe).pipe(
      catchError(() => this.http.get<any>(urlOsrmOrg)),
      map((res) => {
        const route = res?.routes?.[0];
        if (!route || !route.geometry?.coordinates?.length) {
          return this.buildFallbackResult(validPoints, nodes);
        }

        const coords = route.geometry.coordinates.map(
          (c: [number, number]) => [c[1], c[0]] as [number, number]
        );
        const distKm = Math.round(((route.distance ?? 0) / 1000) * 100) / 100;
        const durMin = Math.max(1, Math.round(((route.duration ?? 0) / 60) * 10) / 10);

        return {
          orderedPoints: validPoints,
          totalDistanceKm: distKm,
          estimatedDurationMinutes: durMin,
          segments: [],
          geometry: coords,
        };
      }),
      catchError(() => of(this.buildFallbackResult(validPoints, nodes)))
    );
  }

  private buildFallbackResult(
    validPoints: CircuitPointCollecte[],
    nodes: { latitude: number; longitude: number }[]
  ): OptimizedRouteResult {
    const geometry: [number, number][] = [];

    for (let i = 0; i < nodes.length; i++) {
      const curr = nodes[i];
      geometry.push([curr.latitude, curr.longitude]);

      if (i < nodes.length - 1) {
        const next = nodes[i + 1];
        const isCurrSouth = curr.latitude < 36.785;
        const isNextSouth = next.latitude < 36.785;
        const isCurrNorthEast = curr.latitude >= 36.805 && curr.longitude >= 10.25;
        const isNextNorthEast = next.latitude >= 36.805 && next.longitude >= 10.25;

        const crossesLake =
          (isCurrSouth && isNextNorthEast) || (isCurrNorthEast && isNextSouth);

        if (crossesLake) {
          if (isCurrSouth) {
            geometry.push([36.7845, 10.2780]); // Radès approach
            geometry.push([36.8055, 10.2875]); // Pont Radès - La Goulette bridge
            geometry.push([36.8180, 10.3060]); // La Goulette approach
          } else {
            geometry.push([36.8180, 10.3060]);
            geometry.push([36.8055, 10.2875]);
            geometry.push([36.7845, 10.2780]);
          }
        }
      }
    }

    let totalDistM = 0;
    for (let i = 0; i < geometry.length - 1; i++) {
      totalDistM += this.haversine(
        geometry[i][0],
        geometry[i][1],
        geometry[i + 1][0],
        geometry[i + 1][1]
      ) * 1000;
    }

    const distKm = Math.round((totalDistM / 1000) * 100) / 100;
    // Average urban speed ~ 35 km/h
    const durMin = Math.max(1, Math.round(((distKm / 35) * 60) * 10) / 10);

    return {
      orderedPoints: validPoints,
      totalDistanceKm: distKm,
      estimatedDurationMinutes: durMin,
      segments: [],
      geometry,
    };
  }

  private buildEmptyResult(validPoints: CircuitPointCollecte[]): OptimizedRouteResult {
    return {
      orderedPoints: validPoints,
      totalDistanceKm: 0,
      estimatedDurationMinutes: 0,
      segments: [],
      geometry: [],
    };
  }

  private haversine(lat1: number, lon1: number, lat2: number, lon2: number): number {
    const R = 6371;
    const dLat = this.toRad(lat2 - lat1);
    const dLon = this.toRad(lon2 - lon1);
    const a =
      Math.sin(dLat / 2) ** 2 +
      Math.cos(this.toRad(lat1)) * Math.cos(this.toRad(lat2)) * Math.sin(dLon / 2) ** 2;
    const c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
    return R * c;
  }

  private toRad(value: number): number {
    return (value * Math.PI) / 180;
  }
}
