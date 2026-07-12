import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, map, of, shareReplay } from 'rxjs';
import { environment } from '../../../environments/environment';
import * as L from 'leaflet';

const TUNISIA_BOUNDS = L.latLngBounds(
    [30.1, 7.5],
    [37.6, 11.8]
);

export interface GeocodingResult {
    latitude: number;
    longitude: number;
    displayName: string;
}

interface NominatimResponseItem {
    lat: string;
    lon: string;
    display_name: string;
}

@Injectable({
    providedIn: 'root',
})
export class MapGeocodingService {
    /** Cache of resolved addresses — avoids re-hitting Nominatim for the same query. */
    private readonly _addressCache = new Map<string, Observable<GeocodingResult | null>>();

    constructor(private _httpClient: HttpClient) {}

    searchAddress(address: string): Observable<GeocodingResult | null> {
        const normalizedAddress = (address ?? '').trim();
        if (!normalizedAddress) {
            return of(null);
        }

        const cacheKey = normalizedAddress.toLowerCase();
        const cached = this._addressCache.get(cacheKey);
        if (cached) {
            return cached;
        }

        const request$ = this.fetchAddress(normalizedAddress).pipe(
            shareReplay({ bufferSize: 1, refCount: false })
        );
        this._addressCache.set(cacheKey, request$);
        return request$;
    }

    private fetchAddress(normalizedAddress: string): Observable<GeocodingResult | null> {
        const params = new HttpParams()
            .set('q', normalizedAddress)
            .set('format', 'jsonv2')
            .set('limit', '1')
            .set('countrycodes', 'TN');

        return this._httpClient
            .get<NominatimResponseItem[]>(environment.mapGeocodingApi, {
                params,
            })
            .pipe(
                map((results) => {
                    const first = results?.[0];
                    if (!first) {
                        return null;
                    }

                    const latitude = Number(first.lat);
                    const longitude = Number(first.lon);
                    if (Number.isNaN(latitude) || Number.isNaN(longitude)) {
                        return null;
                    }

                    if (!this.isWithinTunisia(latitude, longitude)) {
                        return null;
                    }

                    return {
                        latitude,
                        longitude,
                        displayName: first.display_name,
                    };
                }),
                catchError(() => of(null))
            );
    }

    searchAddressesAutocomplete(address: string): Observable<GeocodingResult[]> {
        const normalizedAddress = (address ?? '').trim();
        if (!normalizedAddress) {
            return of([]);
        }

        const params = new HttpParams()
            .set('q', normalizedAddress)
            .set('format', 'jsonv2')
            .set('limit', '10')
            .set('countrycodes', 'TN');

        return this._httpClient
            .get<NominatimResponseItem[]>(environment.mapGeocodingApi, {
                params,
            })
            .pipe(
                map((results) => {
                    return (results ?? []).filter(result => {
                        const lat = Number(result.lat);
                        const lon = Number(result.lon);
                        return !Number.isNaN(lat) && !Number.isNaN(lon) && this.isWithinTunisia(lat, lon);
                    }).map(result => ({
                        latitude: Number(result.lat),
                        longitude: Number(result.lon),
                        displayName: result.display_name
                    }));
                }),
                catchError(() => of([]))
            );
    }

    isWithinTunisia(latitude: number, longitude: number): boolean {
        return TUNISIA_BOUNDS.contains([latitude, longitude]);
    }
}
