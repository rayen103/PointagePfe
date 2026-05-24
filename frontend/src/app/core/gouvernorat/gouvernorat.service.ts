import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable, of, tap } from 'rxjs';
import { PagedGouvernorat, Gouvernorat } from './gouvernorat.model';
import { ApiService } from '../common/api.service';

@Injectable({
    providedIn: 'root'
})
export class GouvernoratService {
    private _gouvernorats: BehaviorSubject<Gouvernorat[] | null> = new BehaviorSubject([]);
    private _gouvernorat: BehaviorSubject<Gouvernorat | null> = new BehaviorSubject(null);
    private _gouvernoratsLength: BehaviorSubject<number | null> = new BehaviorSubject(0);

    constructor(private _apiservice: ApiService) { }

    get gouvernorats$(): Observable<Gouvernorat[]> {
        return this._gouvernorats.asObservable();
    }

    get gouvernorat$(): Observable<Gouvernorat> {
        return this._gouvernorat.asObservable();
    }

    get gouvernoratsLength$(): Observable<number> {
        return this._gouvernoratsLength.asObservable();
    }

    GetGouvernorats(
        page: number = 1,
        size: number = 1000,
        sort: string = 'codeGouvernorat',
        order: 'asc' | 'desc' | '' = 'asc',
        search: string = ''
    ): Observable<PagedGouvernorat> {
        return this._apiservice.Get<PagedGouvernorat>('gouvernorat/list', {
            params: { search: search || '', sort, order, page, size }
        })
            .pipe(
                tap((gouvernorats) => {
                    this._gouvernorats.next(gouvernorats.data?.gouvernorats);
                    this._gouvernoratsLength.next(gouvernorats.data?.totalCount);
                }),
                map(r => r.data)
            );
    }

    CreateNewGouvernorat(): Observable<Gouvernorat> {
        this._gouvernorat.next(null);

        const newGouvernorat: Gouvernorat = {
            gouvernoratId: null,
            codeGouvernorat: '',
            libelleGouvernorat: '',
            isActive: true,
            societeId: ''
        };
        this._gouvernorat.next(newGouvernorat);

        return of(newGouvernorat);
    }

    AddGouvernorat(gouvernorat: Gouvernorat): Observable<Gouvernorat> {
        return this._apiservice.Post<Gouvernorat>('gouvernorat/add', gouvernorat)
            .pipe(
                map((r) => {
                    if (!r.success) {
                        throw new Error(r.message || 'Failed to create gouvernorat');
                    }
                    gouvernorat.gouvernoratId = r.data.gouvernoratId;
                    this._gouvernorats.next([r.data, ...this._gouvernorats.value ?? []]);

                    return r.data;
                })
            );
    }

    UpdateGouvernorat(gouvernorat: Gouvernorat): Observable<boolean> {
        return this._apiservice.Patch<boolean>('gouvernorat/update', gouvernorat)
            .pipe(
                map((r) => {
                    if (!r.success) {
                        return false;
                    }

                    const index = this._gouvernorats.value
                        .findIndex(item => item.gouvernoratId === gouvernorat.gouvernoratId);

                    if (index !== -1) {
                        const updatedGouvernorats = [...this._gouvernorats.value];
                        updatedGouvernorats[index] = gouvernorat;
                        this._gouvernorats.next(updatedGouvernorats);
                    }

                    return true;
                })
            );
    }

    GetGouvernoratById(id: string): Observable<Gouvernorat> {
        return this._apiservice.Get<Gouvernorat>(`gouvernorat/${id}`)
            .pipe(
                tap((gouvernorat) => {
                    this._gouvernorat.next(gouvernorat.data);
                }),
                map(r => r.data)
            );
    }

    DeleteGouvernorat(id: string): Observable<boolean> {
        return this._apiservice.Delete<boolean>(`gouvernorat/delete/${id}`)
            .pipe(
                map((r) => {
                    if (!r.success) {
                        return false;
                    }

                    const updatedGouvernorats = this._gouvernorats.value
                        .filter(item => item.gouvernoratId !== id);
                    this._gouvernorats.next(updatedGouvernorats);

                    return true;
                })
            );
    }
}
