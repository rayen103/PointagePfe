import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable, of, tap } from 'rxjs';
import { PagedRattachementEmploye, RattachementEmploye } from './rattachement-employe.model';
import { ApiService } from '../common/api.service';

@Injectable({ providedIn: 'root' })
export class RattachementEmployeService {
    private _rattachementEmployes = new BehaviorSubject<RattachementEmploye[] | null>([]);
    private _rattachementEmploye = new BehaviorSubject<RattachementEmploye | null>(null);
    private _rattachementEmployesLength = new BehaviorSubject<number | null>(0);

    constructor(private _apiservice: ApiService) {}

    get rattachementEmployes$(): Observable<RattachementEmploye[]> {
        return this._rattachementEmployes.asObservable();
    }

    get rattachementEmploye$(): Observable<RattachementEmploye> {
        return this._rattachementEmploye.asObservable();
    }

    get rattachementEmployesLength$(): Observable<number> {
        return this._rattachementEmployesLength.asObservable();
    }

    GetRattachementEmployes(
        page: number = 1,
        size: number = 1000,
        sort: string = 'matricule',
        order: 'asc' | 'desc' | '' = 'asc',
        search: string = ''
    ): Observable<PagedRattachementEmploye> {
        return this._apiservice.Get<PagedRattachementEmploye>('rattachement-employe/list', {
            params: { search: search || '', sort, order, page, size }
        }).pipe(
            tap(r => {
                this._rattachementEmployes.next(r.data?.rattachementEmployes);
                this._rattachementEmployesLength.next(r.data?.totalCount);
            }),
            map(r => r.data)
        );
    }

    CreateNewRattachementEmploye(): Observable<RattachementEmploye> {
        const newItem: RattachementEmploye = {
            rattachementEmployeId: null,
            rattachementId: '',
            matricule: '',
            nomPrenom: '',
            dateDebut: null,
            heureDebut: '',
            dateFin: null,
            heureFin: '',
            nombreHeure: null,
            cout: null,
            coutGlobal: null,
            typeRattachement: '',
            isActive: true,
            societeId: ''
        };
        this._rattachementEmploye.next(newItem);
        return of(newItem);
    }

    AddRattachementEmploye(rattachementEmploye: RattachementEmploye): Observable<RattachementEmploye> {
        return this._apiservice.Post<RattachementEmploye>('rattachement-employe/add', rattachementEmploye)
            .pipe(
                map(r => {
                    if (!r.success) throw new Error(r.message || 'Failed to create rattachement employe');
                    rattachementEmploye.rattachementEmployeId = r.data.rattachementEmployeId;
                    this._rattachementEmployes.next([r.data, ...(this._rattachementEmployes.value ?? [])]);
                    return r.data;
                })
            );
    }

    UpdateRattachementEmploye(rattachementEmploye: RattachementEmploye): Observable<boolean> {
        return this._apiservice.Patch<boolean>('rattachement-employe/update', rattachementEmploye)
            .pipe(
                map(r => {
                    if (!r.success) return false;
                    const index = this._rattachementEmployes.value
                        ?.findIndex(item => item.rattachementEmployeId === rattachementEmploye.rattachementEmployeId);
                    if (index != null && index > -1) {
                        this._rattachementEmployes.value[index] = rattachementEmploye;
                    }
                    this._rattachementEmploye.next(rattachementEmploye);
                    return true;
                })
            );
    }

    GetRattachementEmployeById(id: string): Observable<RattachementEmploye> {
        return this._apiservice.Get<RattachementEmploye>(`rattachement-employe/${id}/one`)
            .pipe(
                tap(r => this._rattachementEmploye.next(r.data)),
                map(r => r.data)
            );
    }

    DeleteRattachementEmploye(rattachementEmploye: { rattachementEmployeId: string }): Observable<boolean> {
        return this._apiservice.Post<boolean>(
            `rattachement-employe/${rattachementEmploye.rattachementEmployeId}/delete`,
            rattachementEmploye
        ).pipe(
            tap(r => {
                if (r.success) {
                    this._rattachementEmployes.next(
                        this._rattachementEmployes.value?.filter(
                            item => item.rattachementEmployeId !== rattachementEmploye.rattachementEmployeId
                        )
                    );
                }
            }),
            map(r => r.success)
        );
    }
}
