import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable, of, tap } from 'rxjs';
import { PagedShift, Shift } from './shift.model';
import { ApiService } from '../common/api.service';

@Injectable({
  providedIn: 'root'
})
export class ShiftService {
    private _shifts: BehaviorSubject<Shift[] | null> = new BehaviorSubject([]);
    private _shift: BehaviorSubject<Shift | null> = new BehaviorSubject(null);
    private _shiftsLength: BehaviorSubject<number | null> = new BehaviorSubject(0);

    constructor(private _apiservice: ApiService) { }

    get shifts$(): Observable<Shift[]> {
        return this._shifts.asObservable();
    }

    get shift$(): Observable<Shift> {
        return this._shift.asObservable();
    }

    get shiftsLength$(): Observable<number> {
        return this._shiftsLength.asObservable();
    }

    GetShifts(
        page: number = 1,
        size: number = 1000,
        sort: string = 'codeShift',
        order: 'asc' | 'desc' | '' = 'asc',
        search: string = ''
    ): Observable<PagedShift> {
        return this._apiservice.Get<PagedShift>('shift/list', {
            params: { search: search || '', sort, order, page, size }
        })
            .pipe(
                tap((shifts) => {
                    this._shifts.next(shifts.data?.shifts);
                    this._shiftsLength.next(shifts.data?.totalCount);
                }),
                map(r => r.data)
            );
    }

    CreateNewShift(): Observable<Shift> {
        this._shift.next(null);

        const newShift: Shift = {
            shiftId: null,
            codeShift: '',
            libelleShift: '',
            jourSemaine: '',
            heureDebut: '',
            heureFin: '',
            isActive: true,
            societeId: ''
        };
        this._shift.next(newShift);

        return of(newShift);
    }

    AddShift(shift: Shift): Observable<Shift> {
        return this._apiservice.Post<Shift>('shift/add', shift)
            .pipe(
                map((r) => {
                    if (!r.success) {
                        throw new Error(r.message || 'Failed to create shift');
                    }
                    shift.shiftId = r.data.shiftId;
                    this._shifts.next([r.data, ...this._shifts.value ?? []]);

                    return r.data;
                })
            );
    }

    UpdateShift(shift: Shift): Observable<boolean> {
        return this._apiservice.Patch<boolean>('shift/update', shift)
            .pipe(
                map((r) => {
                    if (!r.success) {
                        return false;
                    }

                    const index = this._shifts.value
                        .findIndex(item => item.shiftId === shift.shiftId);

                    if (index > -1) {
                        this._shifts.value[index] = shift;
                    }

                    this._shift.next(shift);

                    return true;
                })
            );
    }

    GetShiftById(id: string): Observable<Shift> {
        return this._apiservice.Get<Shift>(`shift/${id}/one`)
            .pipe(
                tap((r) => {
                    this._shift.next(r.data);
                }),
                map(r => r.data)
            );
    }

    DeleteShift(shift: { shiftId: string }): Observable<boolean> {
        return this._apiservice.Post<boolean>(`shift/${shift.shiftId}/delete`, shift)
            .pipe(
                tap((r) => {
                    if (r.success) {
                        const shifts = this._shifts.value.filter(item => item.shiftId !== shift.shiftId);
                        this._shifts.next(shifts);
                    }
                }),
                map(r => r.success)
            );
    }
}
