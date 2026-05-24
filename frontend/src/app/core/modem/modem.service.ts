import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable, of, tap } from 'rxjs';
import { ApiService } from '../common/api.service';
import { Modem, PagedModem } from './modem.model';

@Injectable({
    providedIn: 'root',
})
export class ModemService {
    private _modems: BehaviorSubject<Modem[] | null> = new BehaviorSubject([]);
    private _modem: BehaviorSubject<Modem | null> = new BehaviorSubject(null);
    private _modemsLength: BehaviorSubject<number | null> = new BehaviorSubject(0);

    constructor(private _apiservice: ApiService) { }

    get modems$(): Observable<Modem[]> {
        return this._modems.asObservable();
    }

    get modem$(): Observable<Modem> {
        return this._modem.asObservable();
    }

    get modemsLength$(): Observable<number> {
        return this._modemsLength.asObservable();
    }

    GetModems(
        page: number = 1,
        size: number = 1000,
        sort: string = 'imei',
        order: 'asc' | 'desc' | '' = 'asc',
        search: string = ''
    ): Observable<PagedModem> {
        return this._apiservice.Get<PagedModem>('modem/list', {
            params: { search: search || '', sort, order, page, size }
        })
            .pipe(
                tap((modems) => {
                    this._modems.next(modems.data?.modems);
                    this._modemsLength.next(modems.data?.totalCount);
                }),
                map(r => r.data)
            );
    }

    CreateNewModem(): Observable<Modem> {
        this._modem.next(null);

        const newModem: Modem = {
            modemId: null,
            imei: '',
            modelModem: '',
            numeroSim: '',
            isActive: true,
            societeId: ''
        };
        this._modem.next(newModem);

        return of(newModem);
    }

    AddModem(modem: Modem): Observable<Modem> {
        return this._apiservice.Post<Modem>('modem/add', modem)
            .pipe(
                map((r) => {
                    if (!r.success) {
                        throw new Error(r.message || 'Failed to create modem');
                    }
                    modem.modemId = r.data.modemId;
                    this._modems.next([r.data, ...this._modems.value ?? []]);

                    return r.data;
                })
            );
    }

    UpdateModem(modem: Modem): Observable<boolean> {
        return this._apiservice.Patch<boolean>('modem/update', modem)
            .pipe(
                map((r) => {
                    if (!r.success) {
                        return false;
                    }

                    const index = this._modems.value
                        .findIndex(item => item.modemId === modem.modemId);

                    if (index !== -1) {
                        const updatedModems = [...this._modems.value];
                        updatedModems[index] = modem;
                        this._modems.next(updatedModems);
                    }

                    return true;
                })
            );
    }

    GetModemById(id: string): Observable<Modem> {
        return this._apiservice.Get<Modem>(`modem/${id}`)
            .pipe(
                tap((modem) => {
                    this._modem.next(modem.data);
                }),
                map(r => r.data)
            );
    }

    DeleteModem(id: string): Observable<boolean> {
        return this._apiservice.Delete<boolean>(`modem/delete/${id}`)
            .pipe(
                map((r) => {
                    if (!r.success) {
                        return false;
                    }

                    const updatedModems = this._modems.value
                        .filter(item => item.modemId !== id);
                    this._modems.next(updatedModems);

                    return true;
                })
            );
    }
}
