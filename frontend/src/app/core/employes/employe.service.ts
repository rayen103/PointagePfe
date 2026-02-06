import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, of, tap, map, catchError } from 'rxjs';
import { PagedEmploye, Employe } from './employe.model';
import { ApiService } from '../common/api.service';
import { Societe } from '../Societe/societe.model';

@Injectable({
    providedIn: 'root'
})
export class EmployeService {

    private _employes: BehaviorSubject<Employe[] | null> = new BehaviorSubject([]);
    private _employe: BehaviorSubject<Employe | null> = new BehaviorSubject(null);
    private _employesLength: BehaviorSubject<number | null> = new BehaviorSubject(0);
    private _societes: BehaviorSubject<Societe[] | null> = new BehaviorSubject(null);

    constructor(private _apiservice: ApiService) {}

    get employes$(): Observable<Employe[]> {
        return this._employes.asObservable();
    }

    get employe$(): Observable<Employe> {
        return this._employe.asObservable();
    }

    get employeLength$(): Observable<number> {
        return this._employesLength.asObservable();
    }

    GetEmploye(page: number = 1,
               size: number = 10,
               sort: string = '',
               order: 'asc' | 'desc' | '' = 'asc',
               search: string = '')
        : Observable<PagedEmploye>
    {
        return this._apiservice.Get<PagedEmploye>("employe/list",
            {
                params: { search: search || '', sort, order, page, size}
            })
            .pipe(
                tap((employes) => {
                    this._employes.next(employes.data?.employes ?? []);
                    this._employesLength.next(employes.data?.total);
                }),
                map(r => r.data)
            );
    }

    GetSocietes(): Observable<Societe[]> {
        return this._apiservice.Get<Societe[]>('societe/list').pipe(
            map((response) => response.data ?? [])
        );
    }

    CreateNewEmploye(): Observable<Employe> {
        const newEmploye: Employe = {
            employeId: 'new',
            matricule: null,
            rfid: null,
            nom: null,
            prenom: null,
            codeCircuit: null,
            codePointCollecte: null,
            codeShift: null,
            adresse: null,
            codeGouvernorat: null,
            codeRegion: null,
            societeId: '',
        };
        this._employes.next([newEmploye, ...this._employes.value]);
        return of(newEmploye);
    }

    AddEmploye(employe: Employe): Observable<Employe> {
        return this._apiservice.Post<Employe>("employe/add", employe).pipe(
            map((v) => {
                const newEmploye = v.data;
                this._employes.next([newEmploye, ...this._employes.value]);
                return newEmploye;
            }),
            catchError(error => {
                console.error('Erreur lors de la création de l\'employé', error);
                throw error;
            })
        );
    }

    UpdateEmploye(employe: Employe): Observable<boolean> {
        return this._apiservice.Patch<boolean>("employe/update", employe).pipe(
            map((r) => {
                if (r.success) {
                    this._employes.next(this._employes.value.map(e =>
                        e.employeId === employe.employeId ? employe : e
                    ));
                }
                return r.success;
            })
        );
    }

    DeleteEmploye(employe: { employeId: string }): Observable<boolean> {
        return this._apiservice.Post<Employe>("employe/" + employe.employeId + "/delete", null).pipe(
            map((v) => {
                this._employes.next(this._employes.value.filter(item => item.employeId !== employe.employeId));
                return v.success;
            })
        );
    }

    GetEmployeById(Id: string): Observable<Employe> {
        const index = this._employes.value?.findIndex(x => x.employeId === Id);
        return of(this._employes.value[index])
    }

    get societes$(): Observable<Societe[] | null> {
        return this._societes.asObservable();
    }
}
