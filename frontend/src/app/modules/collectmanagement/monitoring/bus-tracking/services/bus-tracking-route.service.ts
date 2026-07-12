import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { forkJoin, map, Observable, of } from 'rxjs';
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
  private readonly osrmBase = 'https://router.project-osrm.org/route/v1/driving';

  constructor(private http: HttpClient) {}

  calculateOptimizedRoute(
    startLat: number,
    startLon: number,
    collectionPoints: CircuitPointCollecte[],
    arrivalLat: number,
    arrivalLon: number
  ): Observable<OptimizedRouteResult> {
    const validPoints = collectionPoints
      .filter((p) => p.latitude != null && p.longitude != null)
      .sort((a, b) => (a.ordre ?? 0) - (b.ordre ?? 0));

    if (validPoints.length === 0) {
      return of(this.buildEmptyResult([], startLat, startLon, arrivalLat, arrivalLon));
    }

    const nodes = [
      { latitude: startLat, longitude: startLon },
      ...validPoints,
      { latitude: arrivalLat, longitude: arrivalLon },
    ];

    const segmentRequests: Observable<RouteSegment>[] = [];
    for (let i = 0; i < nodes.length - 1; i++) {
      const from = nodes[i];
      const to = nodes[i + 1];
      segmentRequests.push(
        this.getOSRMRoute(from.longitude, from.latitude, to.longitude, to.latitude).pipe(
          map((seg) => seg),
          catchError(() =>
            of(this.fallbackSegment(from.latitude, from.longitude, to.latitude, to.longitude))
          )
        )
      );
    }

    return forkJoin(segmentRequests).pipe(
      map((segs) => {
        const geometry: [number, number][] = [];
        let totalDistanceM = 0;
        let totalDurationS = 0;

        segs.forEach((seg) => {
          totalDistanceM += seg.distance;
          totalDurationS += seg.duration;
          if (geometry.length === 0) {
            geometry.push(...seg.geometry);
          } else {
            geometry.push(...seg.geometry.slice(1));
          }
        });

        return {
          orderedPoints: validPoints,
          totalDistanceKm: Math.round((totalDistanceM / 1000) * 100) / 100,
          estimatedDurationMinutes: Math.round((totalDurationS / 60) * 100) / 100,
          segments: segs,
          geometry,
        };
      }),
      catchError(() => of(this.buildEmptyResult(validPoints, startLat, startLon, arrivalLat, arrivalLon)))
    );
  }

  private getOSRMRoute(
    lng1: number,
    lat1: number,
    lng2: number,
    lat2: number
  ): Observable<RouteSegment> {
    const coordinates = `${lng1},${lat1};${lng2},${lat2}`;
    const params = new HttpParams()
      .set('overview', 'full')
      .set('geometries', 'geojson')
      .set('annotations', 'false');

    return this.http
      .get<any>(`${this.osrmBase}/${coordinates}`, { params })
      .pipe(
        map((res) => {
          const route = res?.routes?.[0];
          if (!route) {
            return this.fallbackSegment(lat1, lng1, lat2, lng2);
          }
          const coords = route.geometry?.coordinates ?? [];
          return {
            geometry: coords.map((c: number[]) => [c[1], c[0]] as [number, number]),
            distance: route.distance ?? 0,
            duration: route.duration ?? 0,
          };
        })
      );
  }

  private fallbackSegment(
    lat1: number,
    lon1: number,
    lat2: number,
    lon2: number
  ): RouteSegment {
    return {
      geometry: [
        [lat1, lon1],
        [lat2, lon2],
      ],
      distance: this.haversine(lat1, lon1, lat2, lon2) * 1000,
      duration: 0,
    };
  }

  private buildEmptyResult(
    validPoints: CircuitPointCollecte[],
    startLat: number,
    startLon: number,
    arrivalLat: number,
    arrivalLon: number
  ): OptimizedRouteResult {
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
