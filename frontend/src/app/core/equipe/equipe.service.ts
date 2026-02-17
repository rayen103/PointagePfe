import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable, of, tap } from 'rxjs';
import { PagedEquipe, Equipe } from './equipe.model';
import { ApiService } from '../common/api.service';

@Injectable({
  providedIn: 'root'
})
export class EquipeService {
    private _equipes: BehaviorSubject<Equipe[] | null> = new BehaviorSubject([]);
    private _equipe: BehaviorSubject<Equipe | null> = new BehaviorSubject(null);
    private _equipesLength: BehaviorSubject<number | null> = new BehaviorSubject(0);

    constructor(private _apiservice: ApiService) { }

    get equipes$(): Observable<Equipe[]> {
        return this._equipes.asObservable();
    }

    get equipe$(): Observable<Equipe> {
        return this._equipe.asObservable();
    }

    get equipesLength$(): Observable<number> {
        return this._equipesLength.asObservable();
    }

    GetEquipes(
        page: number = 1,
        size: number = 1000,
        sort: string = 'codeEquipe',
        order: 'asc' | 'desc' | '' = 'asc',
        search: string = ''
    ): Observable<PagedEquipe> {
        return this._apiservice.Get<PagedEquipe>("equipe/list", {
            params: { search: search || '', sort, order, page, size }
        })
            .pipe(
                tap((equipes) => {
                    this._equipes.next(equipes.data?.equipes);
                    this._equipesLength.next(equipes.data?.totalCount);
                }),
                map(r => r.data)
            );
    }

    CreateNewEquipe(): Observable<Equipe> {
        this._equipe.next(null);

        const newEquipe: Equipe = {
            equipeId: null,
            codeEquipe: "",
            libelleEquipe: "",
            codeClient: "",
            codeEntrepot: "",
            codeTarif: "",
            codeFournisseur: "",
            responsable: "",
            isInternal: false,
            codeVehicule: "",
            isActive: true,
            societeId: ""
        }
        this._equipe.next(newEquipe);

        return of(newEquipe);
    }

    AddEquipe(equipe: Equipe): Observable<Equipe> {
        return this._apiservice.Post<Equipe>("equipe/add", equipe)
            .pipe(
                map((r) => {
                    if (!r.success) {
                        throw new Error(r.message || 'Failed to create equipe');
                    }
                    equipe.equipeId = r.data.equipeId;
                    this._equipes.next([r.data, ...this._equipes.value ?? []])

                    return r.data;
                })
            );
    }

    UpdateEquipe(equipe: Equipe): Observable<boolean> {
        return this._apiservice.Patch<boolean>("equipe/update", equipe)
            .pipe(
                map((r) => {
                    if (!r.success) {
                        return false;
                    }

                    const index = this._equipes.value
                        .findIndex(item => item.equipeId === equipe.equipeId);

                    if (index > -1) {
                        this._equipes.value[index] = equipe;
                    }

                    this._equipe.next(equipe);

                    return true;
                })
            );
    }

    GetEquipeById(id: string): Observable<Equipe> {
        return this._apiservice.Get<Equipe>(`equipe/${id}/one`)
            .pipe(
                tap((r) => {
                    this._equipe.next(r.data);
                }),
                map(r => r.data)
            );
    }

    DeleteEquipe(equipe: { equipeId: string }): Observable<boolean> {
        return this._apiservice.Post<boolean>(`equipe/${equipe.equipeId}/delete`, equipe)
            .pipe(
                tap((r) => {
                    if (r.success) {
                        const equipes = this._equipes.value.filter(item => item.equipeId !== equipe.equipeId);
                        this._equipes.next(equipes);
                    }
                }),
                map(r => r.success)
            );
    }
}
