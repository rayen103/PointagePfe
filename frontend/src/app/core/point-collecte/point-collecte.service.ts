import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable, of, tap } from 'rxjs';
import { PagedPointCollecte, PointCollecte } from './point-collecte.model';
import { ApiService } from '../common/api.service';

@Injectable({
  providedIn: 'root'
})
export class PointCollecteService {
    private _pointsCollecte: BehaviorSubject<PointCollecte[] | null> = new BehaviorSubject([]);
    private _pointCollecte: BehaviorSubject<PointCollecte | null> = new BehaviorSubject(null);
    private _pointsCollecteLength: BehaviorSubject<number | null> = new BehaviorSubject(0);

    constructor(private _apiservice: ApiService) { }

    get pointsCollecte$(): Observable<PointCollecte[]> {
        return this._pointsCollecte.asObservable();
    }

    get pointCollecte$(): Observable<PointCollecte> {
        return this._pointCollecte.asObservable();
    }

    get pointsCollecteLength$(): Observable<number> {
        return this._pointsCollecteLength.asObservable();
    }

    GetPointsCollecte(
        page: number = 1,
        size: number = 1000,
        sort: string = 'codePointCollecte',
        order: 'asc' | 'desc' | '' = 'asc',
        search: string = ''
    ): Observable<PagedPointCollecte> {
        return this._apiservice.Get<PagedPointCollecte>("pointcollecte/list", {
            params: { search: search || '', sort, order, page, size }
        })
            .pipe(
                tap((pointsCollecte) => {
                    this._pointsCollecte.next(pointsCollecte.data?.pointsCollecte);
                    this._pointsCollecteLength.next(pointsCollecte.data?.totalCount);
                }),
                map(r => r.data)
            );
    }

    CreateNewPointCollecte(): Observable<PointCollecte> {
        this._pointCollecte.next(null);

        const newPointCollecte: PointCollecte = {
            pointCollecteId: null,
            codePointCollecte: "",
            libellePointCollecte: "",
            latitude: null,
            longitude: null,
            codeGouvernorat: "",
            codeRegion: "",
            isActive: true,
            societeId: ""
        }
        this._pointCollecte.next(newPointCollecte);

        return of(newPointCollecte);
    }

    AddPointCollecte(pointCollecte: PointCollecte): Observable<PointCollecte> {
        return this._apiservice.Post<PointCollecte>("pointcollecte/add", pointCollecte)
            .pipe(
                map((r) => {
                    if (!r.success) {
                        throw new Error(r.message || 'Failed to create point collecte');
                    }
                    pointCollecte.pointCollecteId = r.data.pointCollecteId;
                    this._pointsCollecte.next([r.data, ...this._pointsCollecte.value ?? []])

                    return r.data;
                })
            );
    }

    UpdatePointCollecte(pointCollecte: PointCollecte): Observable<boolean> {
        return this._apiservice.Patch<boolean>("pointcollecte/update", pointCollecte)
            .pipe(
                map((r) => {
                    if (!r.success) {
                        return false;
                    }

                    const index = this._pointsCollecte.value
                        .findIndex(item => item.pointCollecteId === pointCollecte.pointCollecteId);

                    if (index > -1) {
                        this._pointsCollecte.value[index] = pointCollecte;
                    }

                    this._pointCollecte.next(pointCollecte);

                    return true;
                })
            );
    }

    GetPointCollecteById(id: string): Observable<PointCollecte> {
        return this._apiservice.Get<PointCollecte>(`pointcollecte/${id}/one`)
            .pipe(
                tap((r) => {
                    this._pointCollecte.next(r.data);
                }),
                map(r => r.data)
            );
    }

    DeletePointCollecte(pointCollecte: { pointCollecteId: string }): Observable<boolean> {
        return this._apiservice.Post<boolean>(`pointcollecte/${pointCollecte.pointCollecteId}/delete`, pointCollecte)
            .pipe(
                tap((r) => {
                    if (r.success) {
                        const pointsCollecte = this._pointsCollecte.value.filter(item => item.pointCollecteId !== pointCollecte.pointCollecteId);
                        this._pointsCollecte.next(pointsCollecte);
                    }
                }),
                map(r => r.success)
            );
    }
}
