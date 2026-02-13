import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable, of, tap } from 'rxjs';
import { PagedRattachement, Rattachement } from './rattachement.model';
import { ApiService } from '../common/api.service';

@Injectable({
  providedIn: 'root'
})
export class RattachementService {
    private _rattachements: BehaviorSubject<Rattachement[] | null> = new BehaviorSubject([]);
    private _rattachement: BehaviorSubject<Rattachement | null> = new BehaviorSubject(null);
    private _rattachementsLength: BehaviorSubject<number | null> = new BehaviorSubject(0);

    constructor(private _apiservice: ApiService) { }

    get rattachements$(): Observable<Rattachement[]> {
        return this._rattachements.asObservable();
    }

    get rattachement$(): Observable<Rattachement> {
        return this._rattachement.asObservable();
    }

    get rattachementsLength$(): Observable<number> {
        return this._rattachementsLength.asObservable();
    }

    GetRattachements(
        page: number = 1,
        size: number = 1000,
        sort: string = 'numeroRattachement',
        order: 'asc' | 'desc' | '' = 'asc',
        search: string = ''
    ): Observable<PagedRattachement> {
        return this._apiservice.Get<PagedRattachement>("rattachement/list", {
            params: { search: search || '', sort, order, page, size }
        })
            .pipe(
                tap((rattachements) => {
                    this._rattachements.next(rattachements.data?.rattachements);
                    this._rattachementsLength.next(rattachements.data?.totalCount);
                }),
                map(r => r.data)
            );
    }

    CreateNewRattachement(): Observable<Rattachement> {
        this._rattachement.next(null);

        const newRattachement: Rattachement = {
            rattachementId: null,
            numeroRattachement: "",
            exercice: "",
            dateRattachement: null,
            numeroChantier: "",
            codeClient: "",
            isInternal: false,
            cout: null,
            type: "",
            nature: "",
            responsable: "",
            heureDebut: "",
            heureFin: "",
            emplacement: "",
            reference: "",
            status: "",
            dateCloture: null,
            remarque: "",
            isActive: true,
            societeId: ""
        }
        this._rattachement.next(newRattachement);

        return of(newRattachement);
    }

    AddRattachement(rattachement: Rattachement): Observable<Rattachement> {
        return this._apiservice.Post<Rattachement>("rattachement/add", rattachement)
            .pipe(
                map((r) => {
                    if (!r.success) {
                        throw new Error(r.message || 'Failed to create rattachement');
                    }
                    rattachement.rattachementId = r.data.rattachementId;
                    this._rattachements.next([r.data, ...this._rattachements.value ?? []])

                    return r.data;
                })
            );
    }

    UpdateRattachement(rattachement: Rattachement): Observable<boolean> {
        return this._apiservice.Patch<boolean>("rattachement/update", rattachement)
            .pipe(
                map((r) => {
                    if (!r.success) {
                        return false;
                    }

                    const index = this._rattachements.value
                        .findIndex(item => item.rattachementId === rattachement.rattachementId);

                    if (index > -1) {
                        this._rattachements.value[index] = rattachement;
                    }

                    this._rattachement.next(rattachement);

                    return true;
                })
            );
    }

    GetRattachementById(id: string): Observable<Rattachement> {
        return this._apiservice.Get<Rattachement>(`rattachement/${id}/one`)
            .pipe(
                tap((r) => {
                    this._rattachement.next(r.data);
                }),
                map(r => r.data)
            );
    }

    DeleteRattachement(rattachement: { rattachementId: string }): Observable<boolean> {
        return this._apiservice.Post<boolean>(`rattachement/${rattachement.rattachementId}/delete`, rattachement)
            .pipe(
                tap((r) => {
                    if (r.success) {
                        const rattachements = this._rattachements.value.filter(item => item.rattachementId !== rattachement.rattachementId);
                        this._rattachements.next(rattachements);
                    }
                }),
                map(r => r.success)
            );
    }
}
