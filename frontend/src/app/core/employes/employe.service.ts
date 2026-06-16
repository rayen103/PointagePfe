import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, of, tap, map, catchError, switchMap } from 'rxjs';
import { PagedEmploye, Employe } from './employe.model';
import { ApiService } from '../common/api.service';
import { Societe } from '../Societe/societe.model';

interface AbsenceRiskPredictionResponse {
    riskScore: number;
    riskLevel: 'low' | 'medium' | 'high';
    confidence: number;
    source: string;
    modelVersion: string;
}

interface AbsenceRiskBatchPredictionResponse {
    predictions: AbsenceRiskPredictionResponse[];
}

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
                map((response) => response.data),
                switchMap((pagedEmploye) => {
                    const employes = pagedEmploye?.employes ?? [];
                    if (!employes.length) {
                        this._employes.next([]);
                        this._employesLength.next(pagedEmploye?.total ?? 0);
                        return of(pagedEmploye);
                    }

                    return this._apiservice.Post2<AbsenceRiskBatchPredictionResponse>("prediction/absence-risk/batch", {
                        items: employes.map((employe) => ({
                            employeId: employe.employeId,
                            typeEmploye: employe.typeEmploye,
                            codeShift: employe.codeShift ?? null,
                            codeRattachement: employe.codeCircuit ?? null,
                            numeroChantier: null
                        }))
                    }).pipe(
                        map((response) => response?.data?.predictions ?? []),
                        map((predictions) => {
                            const enrichedEmployes = employes.map((employe, index) => ({
                                ...employe,
                                absenceRiskScore: predictions[index]?.riskScore ?? null,
                                absenceRiskLevel: predictions[index]?.riskLevel ?? 'low',
                                absencePredictionConfidence: predictions[index]?.confidence ?? null
                            }));

                            this._employes.next(enrichedEmployes);
                            this._employesLength.next(pagedEmploye?.total ?? enrichedEmployes.length);

                            return {
                                ...pagedEmploye,
                                employes: enrichedEmployes
                            } as PagedEmploye;
                        })
                    );
                }),
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
            typeEmploye: 'EmployeSimple',
            codeCircuit: null,
            codePointCollecte: null,
            codeBus: null,
            codeShift: null,
            adresse: null,
            codeGouvernorat: null,
            codeRegion: null,
            latitude: null,
            longitude: null,
            societeId: '',
            isActive: true,
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
