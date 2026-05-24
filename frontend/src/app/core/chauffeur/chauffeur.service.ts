import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable, of, tap } from 'rxjs';
import { ApiService } from '../common/api.service';
import { Chauffeur, PagedChauffeur } from './chauffeur.model';

@Injectable({
    providedIn: 'root',
})
export class ChauffeurService {
    private _chauffeurs: BehaviorSubject<Chauffeur[] | null> = new BehaviorSubject([]);
    private _chauffeur: BehaviorSubject<Chauffeur | null> = new BehaviorSubject(null);
    private _chauffeursLength: BehaviorSubject<number | null> = new BehaviorSubject(0);

    constructor(private _apiservice: ApiService) { }

    get chauffeurs$(): Observable<Chauffeur[]> {
        return this._chauffeurs.asObservable();
    }

    get chauffeur$(): Observable<Chauffeur> {
        return this._chauffeur.asObservable();
    }

    get chauffeursLength$(): Observable<number> {
        return this._chauffeursLength.asObservable();
    }

    GetChauffeurs(
        page: number = 1,
        size: number = 1000,
        sort: string = 'codeChauffeur',
        order: 'asc' | 'desc' | '' = 'asc',
        search: string = ''
    ): Observable<PagedChauffeur> {
        return this._apiservice.Get<PagedChauffeur>('chauffeur/list', {
            params: { search: search || '', sort, order, page, size }
        })
            .pipe(
                tap((chauffeurs) => {
                    this._chauffeurs.next(chauffeurs.data?.chauffeurs);
                    this._chauffeursLength.next(chauffeurs.data?.totalCount);
                }),
                map(r => r.data)
            );
    }

    CreateNewChauffeur(): Observable<Chauffeur> {
        this._chauffeur.next(null);

        const newChauffeur: Chauffeur = {
            chauffeurId: null,
            codeChauffeur: '',
            nom: '',
            prenom: '',
            cin: '',
            rfidChauffeur: '',
            externe: false,
            isActive: true,
            societeId: ''
        };
        this._chauffeur.next(newChauffeur);

        return of(newChauffeur);
    }

    AddChauffeur(chauffeur: Chauffeur): Observable<Chauffeur> {
        return this._apiservice.Post<Chauffeur>('chauffeur/add', chauffeur)
            .pipe(
                map((r) => {
                    if (!r.success) {
                        throw new Error(r.message || 'Failed to create chauffeur');
                    }
                    chauffeur.chauffeurId = r.data.chauffeurId;
                    this._chauffeurs.next([r.data, ...this._chauffeurs.value ?? []]);

                    return r.data;
                })
            );
    }

    UpdateChauffeur(chauffeur: Chauffeur): Observable<boolean> {
        return this._apiservice.Patch<boolean>('chauffeur/update', chauffeur)
            .pipe(
                map((r) => {
                    if (!r.success) {
                        return false;
                    }

                    const index = this._chauffeurs.value
                        .findIndex(item => item.chauffeurId === chauffeur.chauffeurId);

                    if (index !== -1) {
                        const updatedChauffeurs = [...this._chauffeurs.value];
                        updatedChauffeurs[index] = chauffeur;
                        this._chauffeurs.next(updatedChauffeurs);
                    }

                    return true;
                })
            );
    }

    GetChauffeurById(id: string): Observable<Chauffeur> {
        return this._apiservice.Get<Chauffeur>(`chauffeur/${id}`)
            .pipe(
                tap((chauffeur) => {
                    this._chauffeur.next(chauffeur.data);
                }),
                map(r => r.data)
            );
    }

    DeleteChauffeur(id: string): Observable<boolean> {
        return this._apiservice.Delete<boolean>(`chauffeur/delete/${id}`)
            .pipe(
                map((r) => {
                    if (!r.success) {
                        return false;
                    }

                    const updatedChauffeurs = this._chauffeurs.value
                        .filter(item => item.chauffeurId !== id);
                    this._chauffeurs.next(updatedChauffeurs);

                    return true;
                })
            );
    }
}
