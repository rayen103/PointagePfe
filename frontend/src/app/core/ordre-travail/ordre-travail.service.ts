import { Injectable } from '@angular/core';
import { BehaviorSubject, forkJoin, map, Observable, of, switchMap, tap } from 'rxjs';
import { PagedOrdreTravail, OrdreTravail } from './ordre-travail.model';
import { ApiService } from '../common/api.service';

interface DurationPredictionResponse {
    predictedDurationHours: number;
    confidence: number;
    source: string;
    modelVersion: string;
}

@Injectable({
  providedIn: 'root'
})
export class OrdreTravailService {
    private _ordresTravail: BehaviorSubject<OrdreTravail[] | null> = new BehaviorSubject([]);
    private _ordreTravail: BehaviorSubject<OrdreTravail | null> = new BehaviorSubject(null);
    private _ordresTravailLength: BehaviorSubject<number | null> = new BehaviorSubject(0);

    constructor(private _apiservice: ApiService) { }

    get ordresTravail$(): Observable<OrdreTravail[]> {
        return this._ordresTravail.asObservable();
    }

    get ordreTravail$(): Observable<OrdreTravail> {
        return this._ordreTravail.asObservable();
    }

    get ordresTravailLength$(): Observable<number> {
        return this._ordresTravailLength.asObservable();
    }

    GetOrdresTravail(
        page: number = 1,
        size: number = 1000,
        sort: string = 'numeroOrdreTravail',
        order: 'asc' | 'desc' | '' = 'asc',
        search: string = ''
    ): Observable<PagedOrdreTravail> {
        return this._apiservice.Get<PagedOrdreTravail>("ordretravail/list", {
            params: { search: search || '', sort, order, page, size }
        })
            .pipe(
                map((response) => response.data),
                switchMap((pagedOrdresTravail) => {
                    const ordresTravail = pagedOrdresTravail?.ordresTravail ?? [];
                    if (!ordresTravail.length) {
                        this._ordresTravail.next([]);
                        this._ordresTravailLength.next(pagedOrdresTravail?.totalCount ?? 0);
                        return of(pagedOrdresTravail);
                    }

                    const predictions$ = ordresTravail.map((ordreTravail) =>
                        this._apiservice.Post2<DurationPredictionResponse>("prediction/duration", {
                            numeroChantier: ordreTravail.numeroChantier ?? null,
                            codeShift: null,
                            codeRattachement: null,
                            typeEmploye: null,
                            workOrderType: ordreTravail.etatOT ?? ordreTravail.libelle ?? null
                        }).pipe(map((response) => response?.data))
                    );

                    return forkJoin(predictions$).pipe(
                        map((predictions) => {
                            const enrichedOrdresTravail = ordresTravail.map((ordreTravail, index) => ({
                                ...ordreTravail,
                                predictedDurationHours: predictions[index]?.predictedDurationHours ?? null,
                                predictionConfidence: predictions[index]?.confidence ?? null,
                                predictionSource: predictions[index]?.source ?? 'fallback'
                            }));

                            this._ordresTravail.next(enrichedOrdresTravail);
                            this._ordresTravailLength.next(pagedOrdresTravail?.totalCount ?? enrichedOrdresTravail.length);

                            return {
                                ...pagedOrdresTravail,
                                ordresTravail: enrichedOrdresTravail
                            } as PagedOrdreTravail;
                        })
                    );
                }),
            );
    }

    CreateNewOrdreTravail(): Observable<OrdreTravail> {
        this._ordreTravail.next(null);

        const newOrdreTravail: OrdreTravail = {
            ordreTravailId: null,
            numeroOrdreTravail: "",
            numeroChantier: "",
            codeClient: "",
            numeroBonCommande: "",
            codeEquipe: "",
            etatOT: "",
            montant: null,
            dateCreation: null,
            numeroConvention: "",
            codeVehicule: "",
            libelle: "",
            isActive: true,
            societeId: ""
        }
        this._ordreTravail.next(newOrdreTravail);

        return of(newOrdreTravail);
    }

    AddOrdreTravail(ordreTravail: OrdreTravail): Observable<OrdreTravail> {
        return this._apiservice.Post<OrdreTravail>("ordretravail/add", ordreTravail)
            .pipe(
                map((r) => {
                    if (!r.success) {
                        throw new Error(r.message || 'Failed to create ordre travail');
                    }
                    ordreTravail.ordreTravailId = r.data.ordreTravailId;
                    this._ordresTravail.next([r.data, ...this._ordresTravail.value ?? []])

                    return r.data;
                })
            );
    }

    UpdateOrdreTravail(ordreTravail: OrdreTravail): Observable<boolean> {
        return this._apiservice.Patch<boolean>("ordretravail/update", ordreTravail)
            .pipe(
                map((r) => {
                    if (!r.success) {
                        return false;
                    }

                    const index = this._ordresTravail.value
                        .findIndex(item => item.ordreTravailId === ordreTravail.ordreTravailId);

                    if (index > -1) {
                        this._ordresTravail.value[index] = ordreTravail;
                    }

                    this._ordreTravail.next(ordreTravail);

                    return true;
                })
            );
    }

    GetOrdreTravailById(id: string): Observable<OrdreTravail> {
        return this._apiservice.Get<OrdreTravail>(`ordretravail/${id}/one`)
            .pipe(
                tap((r) => {
                    this._ordreTravail.next(r.data);
                }),
                map(r => r.data)
            );
    }

    DeleteOrdreTravail(ordreTravail: { ordreTravailId: string }): Observable<boolean> {
        return this._apiservice.Post<boolean>(`ordretravail/${ordreTravail.ordreTravailId}/delete`, ordreTravail)
            .pipe(
                tap((r) => {
                    if (r.success) {
                        const ordresTravail = this._ordresTravail.value.filter(item => item.ordreTravailId !== ordreTravail.ordreTravailId);
                        this._ordresTravail.next(ordresTravail);
                    }
                }),
                map(r => r.success)
            );
    }
}
