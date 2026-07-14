import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable, tap } from 'rxjs';
import { Pointage, PagedPointage } from './pointage.model';
import { ApiService } from '../common/api.service';
import { HttpParams } from '@angular/common/http';

@Injectable({ providedIn: 'root' })
export class PointageService {
    private _pointages = new BehaviorSubject<Pointage[] | null>([]);
    private _pointagesLength = new BehaviorSubject<number | null>(0);

    constructor(private _apiservice: ApiService) {}

    get pointages$(): Observable<Pointage[]> {
        return this._pointages.asObservable().pipe(map(list => list ?? []));
    }

    get pointagesLength$(): Observable<number> {
        return this._pointagesLength.asObservable().pipe(map(len => len ?? 0));
    }

    GetPointages(
        page = 1,
        size = 10,
        sort = 'heurePointageUtc',
        order: 'asc' | 'desc' | '' = 'desc',
        search = '',
        filters?: { busId?: string; isSuccess?: boolean; startDate?: string; endDate?: string }
    ): Observable<PagedPointage> {
        let params: any = {
            search: search || '',
            sort,
            order,
            page: page.toString(),
            size: size.toString()
        };

        if (filters?.busId) {
            params.busId = filters.busId;
        }
        if (filters?.isSuccess !== undefined && filters.isSuccess !== null) {
            params.isSuccess = filters.isSuccess.toString();
        }
        if (filters?.startDate) {
            params.startDate = filters.startDate;
        }
        if (filters?.endDate) {
            params.endDate = filters.endDate;
        }

        return this._apiservice.Get<PagedPointage>('pointage/list', { params })
            .pipe(
                tap(r => {
                    this._pointages.next(r.data?.pointages || []);
                    this._pointagesLength.next(r.data?.totalCount || 0);
                }),
                map(r => r.data)
            );
    }
}
