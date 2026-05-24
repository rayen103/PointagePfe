import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable, of, tap } from 'rxjs';
import { PagedRegion, Region } from './region.model';
import { ApiService } from '../common/api.service';

@Injectable({
    providedIn: 'root'
})
export class RegionService {
    private _regions: BehaviorSubject<Region[] | null> = new BehaviorSubject([]);
    private _region: BehaviorSubject<Region | null> = new BehaviorSubject(null);
    private _regionsLength: BehaviorSubject<number | null> = new BehaviorSubject(0);

    constructor(private _apiservice: ApiService) { }

    get regions$(): Observable<Region[]> {
        return this._regions.asObservable();
    }

    get region$(): Observable<Region> {
        return this._region.asObservable();
    }

    get regionsLength$(): Observable<number> {
        return this._regionsLength.asObservable();
    }

    GetRegions(
        page: number = 1,
        size: number = 1000,
        sort: string = 'codeRegion',
        order: 'asc' | 'desc' | '' = 'asc',
        search: string = ''
    ): Observable<PagedRegion> {
        return this._apiservice.Get<PagedRegion>('region/list', {
            params: { search: search || '', sort, order, page, size }
        })
            .pipe(
                tap((regions) => {
                    this._regions.next(regions.data?.regions);
                    this._regionsLength.next(regions.data?.totalCount);
                }),
                map(r => r.data)
            );
    }

    CreateNewRegion(): Observable<Region> {
        this._region.next(null);

        const newRegion: Region = {
            regionId: null,
            codeRegion: '',
            libelleRegion: '',
            codeGouvernorat: '',
            isActive: true,
            societeId: ''
        };
        this._region.next(newRegion);

        return of(newRegion);
    }

    AddRegion(region: Region): Observable<Region> {
        return this._apiservice.Post<Region>('region/add', region)
            .pipe(
                map((r) => {
                    if (!r.success) {
                        throw new Error(r.message || 'Failed to create region');
                    }
                    region.regionId = r.data.regionId;
                    this._regions.next([r.data, ...this._regions.value ?? []]);

                    return r.data;
                })
            );
    }

    UpdateRegion(region: Region): Observable<boolean> {
        return this._apiservice.Patch<boolean>('region/update', region)
            .pipe(
                map((r) => {
                    if (!r.success) {
                        return false;
                    }

                    const index = this._regions.value
                        .findIndex(item => item.regionId === region.regionId);

                    if (index !== -1) {
                        const updatedRegions = [...this._regions.value];
                        updatedRegions[index] = region;
                        this._regions.next(updatedRegions);
                    }

                    return true;
                })
            );
    }

    GetRegionById(id: string): Observable<Region> {
        return this._apiservice.Get<Region>(`region/${id}`)
            .pipe(
                tap((region) => {
                    this._region.next(region.data);
                }),
                map(r => r.data)
            );
    }

    DeleteRegion(id: string): Observable<boolean> {
        return this._apiservice.Delete<boolean>(`region/delete/${id}`)
            .pipe(
                map((r) => {
                    if (!r.success) {
                        return false;
                    }

                    const updatedRegions = this._regions.value
                        .filter(item => item.regionId !== id);
                    this._regions.next(updatedRegions);

                    return true;
                })
            );
    }
}
