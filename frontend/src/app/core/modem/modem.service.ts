import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { ApiService } from '../common/api.service';
import { PagedModem } from './modem.model';

@Injectable({
    providedIn: 'root',
})
export class ModemService {
    constructor(private _apiService: ApiService) {}

    GetModems(
        page: number = 1,
        size: number = 1000,
        sort: string = 'imei',
        order: 'asc' | 'desc' | '' = 'asc',
        search: string = ''
    ): Observable<PagedModem> {
        return this._apiService
            .Get<PagedModem>('modem/list', {
                params: { search: search || '', sort, order, page, size },
            })
            .pipe(map((r) => r.data));
    }
}
