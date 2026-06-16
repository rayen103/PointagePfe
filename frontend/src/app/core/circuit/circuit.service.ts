import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable, of, tap } from 'rxjs';
import { PagedCircuit, Circuit } from './circuit.model';
import { ApiService } from '../common/api.service';

@Injectable({
  providedIn: 'root'
})
export class CircuitService {
    private _circuits: BehaviorSubject<Circuit[] | null> = new BehaviorSubject([]);
    private _circuit: BehaviorSubject<Circuit | null> = new BehaviorSubject(null);
    private _circuitLength: BehaviorSubject<number | null> = new BehaviorSubject(0);

    constructor(private _apiservice: ApiService) { }

    get circuits$(): Observable<Circuit[]> {
        return this._circuits.asObservable();
    }

    get circuit$(): Observable<Circuit> {
        return this._circuit.asObservable();
    }

    get circuitsLength$(): Observable<number> {
        return this._circuitLength.asObservable();
    }

    GetCircuit(
        page: number = 1,
        size: number = 1000,
        sort: string = 'codeCircuit',
        order: 'asc' | 'desc' | '' = 'asc',
        search: string = ''
    ): Observable<PagedCircuit> {
        return this._apiservice.Get<PagedCircuit>("circuit/list", {
            params: { search: search || '', sort, order, page, size }
        })
            .pipe(
                tap((circuits) => {
                    this._circuits.next(circuits.data?.circuits);
                    this._circuitLength.next(circuits.data?.totalCount);
                }),
                map(r => r.data)
            );
    }

    CreateNewCircuit(): Observable<Circuit> {
        this._circuit.next(null);

        const newCircuit: Circuit = {
            circuitId: null,
            codeCircuit: "",
            libelleCircuit: "",
            description: "",
            isActive: true,
            societeId: "",
            circuitPointsCollecte: []
        }
        this._circuit.next(newCircuit);

        return of(newCircuit);
    }

    AddCircuit(circuit: Circuit): Observable<Circuit> {
        console.log('AddCircuit - Sending request:', circuit);
        return this._apiservice.Post<Circuit>("circuit/add", circuit)
            .pipe(
                map((r) => {
                    console.log('AddCircuit - Response received:', r);
                    if (!r.success) {
                        console.error('AddCircuit - Response indicates failure:', r.message, r);
                        throw new Error(r.message || 'Failed to create circuit');
                    }
                    circuit.circuitId = r.data.circuitId;
                    this._circuits.next([r.data, ...this._circuits.value ?? []])

                    return r.data;
                })
            );
    }

    UpdateCircuit(circuit: Circuit): Observable<boolean> {
        return this._apiservice.Patch<boolean>("circuit/update", circuit)
            .pipe(
                map((r) => {
                    if (!r.success) {
                        return false;
                    }

                    const index = this._circuits.value
                        .findIndex(item => item.circuitId === circuit.circuitId) ?? -1;

                    if (index > -1) {
                        this._circuits.value[index] = circuit;
                    }

                    this._circuit.next(circuit);

                    return true;
                })
            );
    }

    GetCircuitById(id: string): Observable<Circuit> {
        return this._apiservice.Get<Circuit>(`circuit/${id}/one`)
            .pipe(
                tap((r) => {
                    this._circuit.next(r.data);
                }),
                map(r => r.data)
            );
    }

    DeleteCircuit(circuit: { circuitId: string }): Observable<boolean> {
        return this._apiservice.Post<boolean>(`circuit/${circuit.circuitId}/delete`, circuit)
            .pipe(
                tap((r) => {
                    if (r.success) {
                        const circuits = this._circuits.value.filter(item => item.circuitId !== circuit.circuitId);
                        this._circuits.next(circuits);
                    }
                }),
                map(r => r.success)
            );
    }
}
