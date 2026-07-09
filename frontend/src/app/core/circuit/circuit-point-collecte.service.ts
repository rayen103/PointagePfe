import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable, tap } from 'rxjs';
import { CircuitPointCollecte, CircuitPointCollecteList } from './circuit-point-collecte.model';
import { ApiService } from '../common/api.service';

@Injectable({
    providedIn: 'root'
})
export class CircuitPointCollecteService {
    private _circuitPoints: BehaviorSubject<CircuitPointCollecte[]> = new BehaviorSubject<CircuitPointCollecte[]>([]);

    constructor(private _apiService: ApiService) {}

    get circuitPoints$(): Observable<CircuitPointCollecte[]> {
        return this._circuitPoints.asObservable();
    }

    getByCircuit(circuitId: string): Observable<CircuitPointCollecte[]> {
        return this._apiService.Get<CircuitPointCollecteList>(`circuit-point-collecte/${circuitId}/list`)
            .pipe(
                tap((r) => {
                    this._circuitPoints.next(r.data?.items ?? []);
                }),
                map(r => r.data?.items ?? [])
            );
    }

    add(item: CircuitPointCollecte): Observable<CircuitPointCollecte> {
        return this._apiService.Post<CircuitPointCollecte>('circuit-point-collecte/add', item)
            .pipe(
                map(r => {
                    if (r.success) {
                        this._circuitPoints.next([...this._circuitPoints.value, r.data]);
                    }
                    return r.data;
                })
            );
    }

    update(item: CircuitPointCollecte): Observable<boolean> {
        return this._apiService.Patch<boolean>('circuit-point-collecte/update', item)
            .pipe(
                map(r => {
                    if (r.success) {
                        const idx = this._circuitPoints.value.findIndex(
                            p => p.circuitPointCollecteId === item.circuitPointCollecteId
                        );
                        if (idx > -1) {
                            const updated = [...this._circuitPoints.value];
                            updated[idx] = item;
                            this._circuitPoints.next(updated);
                        }
                    }
                    return r.success;
                })
            );
    }

    delete(id: string): Observable<boolean> {
        return this._apiService.Post<boolean>(`circuit-point-collecte/${id}/delete`, {})
            .pipe(
                map(r => {
                    if (r.success) {
                        this._circuitPoints.next(
                            this._circuitPoints.value.filter(p => p.circuitPointCollecteId !== id)
                        );
                    }
                    return r.success;
                })
            );
    }
}
