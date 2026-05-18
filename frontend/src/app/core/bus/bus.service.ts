import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable, of, tap } from 'rxjs';
import {
    PagedBus,
    Bus,
    BusLivePositionSnapshot,
    BusRuntimeEvent,
    BusRuntimeState,
    UpdateBusRuntimePositionPayload
} from './bus.model';
import { ApiService } from '../common/api.service';

@Injectable({
  providedIn: 'root'
})
export class BusService {
    private _buses: BehaviorSubject<Bus[] | null> = new BehaviorSubject([]);
    private _bus: BehaviorSubject<Bus | null> = new BehaviorSubject(null);
    private _busesLength: BehaviorSubject<number | null> = new BehaviorSubject(0);

    constructor(private _apiservice: ApiService) { }

    get buses$(): Observable<Bus[]> {
        return this._buses.asObservable();
    }

    get bus$(): Observable<Bus> {
        return this._bus.asObservable();
    }

    get busesLength$(): Observable<number> {
        return this._busesLength.asObservable();
    }

    GetBuses(
        page: number = 1,
        size: number = 1000,
        sort: string = 'numeroIMM',
        order: 'asc' | 'desc' | '' = 'asc',
        search: string = ''
    ): Observable<PagedBus> {
        return this._apiservice.Get<PagedBus>('bus/list', {
            params: { search: search || '', sort, order, page, size }
        })
            .pipe(
                tap((buses) => {
                    this._buses.next(buses.data?.buses);
                    this._busesLength.next(buses.data?.totalCount);
                }),
                map(r => r.data)
            );
    }

    CreateNewBus(): Observable<Bus> {
        this._bus.next(null);

        const newBus: Bus = {
            busId: null,
            numeroIMM: '',
            modelBus: '',
            imei: '',
            capacite: null,
            codeCircuit: '',
            codeChauffeur: '',
            appSagem: false,
            isActive: true,
            latitude: null,
            longitude: null,
            societeId: ''
        };
        this._bus.next(newBus);

        return of(newBus);
    }

    AddBus(bus: Bus): Observable<Bus> {
        return this._apiservice.Post<Bus>('bus/add', bus)
            .pipe(
                map((r) => {
                    if (!r.success) {
                        throw new Error(r.message || 'Failed to create bus');
                    }
                    bus.busId = r.data.busId;
                    this._buses.next([r.data, ...this._buses.value ?? []]);

                    return r.data;
                })
            );
    }

    UpdateBus(bus: Bus): Observable<boolean> {
        return this._apiservice.Patch<boolean>('bus/update', bus)
            .pipe(
                map((r) => {
                    if (!r.success) {
                        return false;
                    }

                    const index = this._buses.value
                        .findIndex(item => item.busId === bus.busId);

                    if (index > -1) {
                        this._buses.value[index] = bus;
                    }

                    this._bus.next(bus);

                    return true;
                })
            );
    }

    GetBusById(id: string): Observable<Bus> {
        return this._apiservice.Get<Bus>(`bus/${id}/one`)
            .pipe(
                tap((r) => {
                    this._bus.next(r.data);
                }),
                map(r => r.data)
            );
    }

    DeleteBus(bus: { busId: string }): Observable<boolean> {
        return this._apiservice.Post<boolean>(`bus/${bus.busId}/delete`, bus)
            .pipe(
                tap((r) => {
                    if (r.success) {
                        const buses = this._buses.value.filter(item => item.busId !== bus.busId);
                        this._buses.next(buses);
                    }
                }),
                map(r => r.success)
            );
    }

    UpdateRuntimePosition(payload: UpdateBusRuntimePositionPayload): Observable<BusRuntimeState> {
        return this._apiservice.Post<BusRuntimeState>('bus/runtime/position', payload)
            .pipe(map(r => r.data));
    }

    GetLivePositionsSnapshot(): Observable<BusLivePositionSnapshot> {
        return this._apiservice.Get<BusLivePositionSnapshot>('bus/runtime/positions/stream')
            .pipe(map(r => r.data));
    }

    EmptyBus(busId: string): Observable<BusRuntimeState> {
        return this._apiservice.Post<BusRuntimeState>(`bus/${busId}/vider`, {})
            .pipe(map(r => r.data));
    }

    GetBusRuntimeEvents(busId: string): Observable<BusRuntimeEvent[]> {
        return this._apiservice.Get<BusRuntimeEvent[]>(`bus/${busId}/events`)
            .pipe(map(r => r.data ?? []));
    }
}
