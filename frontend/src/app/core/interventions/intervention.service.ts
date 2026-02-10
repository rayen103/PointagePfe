import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, of, tap, map, catchError } from 'rxjs';
import { PagedIntervention, Intervention } from './intervention.model';
import { ApiService } from '../common/api.service';

@Injectable({
    providedIn: 'root'
})
export class InterventionService {

    private _interventions: BehaviorSubject<Intervention[] | null> = new BehaviorSubject([]);
    private _intervention: BehaviorSubject<Intervention | null> = new BehaviorSubject(null);
    private _interventionsLength: BehaviorSubject<number | null> = new BehaviorSubject(0);

    constructor(private _apiService: ApiService) {}

    get interventions$(): Observable<Intervention[]> {
        return this._interventions.asObservable();
    }

    get intervention$(): Observable<Intervention> {
        return this._intervention.asObservable();
    }

    get interventionLength$(): Observable<number> {
        return this._interventionsLength.asObservable();
    }

    GetIntervention(page: number = 1,
               size: number = 10,
               sort: string = '',
               order: 'asc' | 'desc' | '' = 'asc',
               search: string = '')
        : Observable<PagedIntervention>
    {
        return this._apiService.Get<PagedIntervention>("intervention/list",
            {
                params: { search: search || '', sort, order, page, size}
            })
            .pipe(
                tap((interventions) => {
                    this._interventions.next(interventions.data?.interventions ?? []);
                    this._interventionsLength.next(interventions.data?.total);
                }),
                map(r => r.data)
            );
    }

    CreateNewIntervention(): Observable<Intervention> {
        const newIntervention: Intervention = {
            interventionId: 'new',
            numeroIntervention: '',
            description: null,
            dateIntervention: new Date().toISOString(),
            typeIntervention: null,
            statut: null,
            cout: null,
        };
        this._interventions.next([newIntervention, ...this._interventions.value]);
        return of(newIntervention);
    }

    AddIntervention(intervention: Intervention): Observable<Intervention> {
        return this._apiService.Post<Intervention>("intervention/add", intervention).pipe(
            map((v) => {
                const newIntervention = v.data;
                this._interventions.next([newIntervention, ...this._interventions.value]);
                return newIntervention;
            }),
            catchError(error => {
                console.error('Error creating intervention', error);
                throw error;
            })
        );
    }

    UpdateIntervention(intervention: Intervention): Observable<boolean> {
        return this._apiService.Patch<boolean>("intervention/update", intervention).pipe(
            map((r) => {
                if (r.success) {
                    this._interventions.next(this._interventions.value.map(i =>
                        i.interventionId === intervention.interventionId ? intervention : i
                    ));
                }
                return r.success;
            })
        );
    }

    DeleteIntervention(intervention: { interventionId: string }): Observable<boolean> {
        return this._apiService.Post<Intervention>("intervention/" + intervention.interventionId + "/delete", null).pipe(
            map((v) => {
                this._interventions.next(this._interventions.value.filter(item => item.interventionId !== intervention.interventionId));
                return v.success;
            })
        );
    }

    GetInterventionById(id: string): Observable<Intervention> {
        const index = this._interventions.value?.findIndex(x => x.interventionId === id);
        if (index === -1) {
            return of(null);
        }
        return of(this._interventions.value[index])
    }
}
