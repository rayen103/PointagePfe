import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, map, of } from 'rxjs';
import { environment } from '../../../environments/environment';

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
    constructor(private _httpClient: HttpClient) {}

    searchAddress(address: string): Observable<GeocodingResult | null> {
        const normalizedAddress = (address ?? '').trim();
        if (!normalizedAddress) {
            return of(null);
        }

        const params = new HttpParams()
            .set('q', normalizedAddress)
            .set('format', 'jsonv2')
            .set('limit', '1');

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

                    return {
                        latitude,
                        longitude,
                        displayName: first.display_name,
                    };
                }),
                catchError(() => of(null))
            );
    }
}
