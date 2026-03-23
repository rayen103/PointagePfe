import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable } from 'rxjs';
import { OrdreTravailDetail, OrdreTravailDetailList } from './ordre-travail-detail.model';
import { ApiService } from '../common/api.service';

@Injectable({
    providedIn: 'root'
})
export class OrdreTravailDetailService {
    private _details: BehaviorSubject<OrdreTravailDetail[]> = new BehaviorSubject<OrdreTravailDetail[]>([]);

    constructor(private _apiService: ApiService) {}

    get details$(): Observable<OrdreTravailDetail[]> {
        return this._details.asObservable();
    }

    getByOrdreTravail(ordreTravailId: string): Observable<OrdreTravailDetail[]> {
        return this._apiService.Get<OrdreTravailDetailList>(`ordre-travail-detail/${ordreTravailId}/list`)
            .pipe(
                map(r => {
                    const items = r.data?.items ?? [];
                    this._details.next(items);
                    return items;
                })
            );
    }

    add(item: Omit<OrdreTravailDetail, 'ordreTravailDetailId'>): Observable<OrdreTravailDetail> {
        return this._apiService.Post<OrdreTravailDetail>('ordre-travail-detail/add', item)
            .pipe(
                map(r => {
                    if (r.success) {
                        this._details.next([...this._details.value, r.data]);
                    }
                    return r.data;
                })
            );
    }

    update(item: OrdreTravailDetail): Observable<boolean> {
        return this._apiService.Patch<boolean>('ordre-travail-detail/update', item)
            .pipe(
                map(r => {
                    if (r.success) {
                        const idx = this._details.value.findIndex(
                            d => d.ordreTravailDetailId === item.ordreTravailDetailId
                        );
                        if (idx > -1) {
                            const updated = [...this._details.value];
                            updated[idx] = item;
                            this._details.next(updated);
                        }
                    }
                    return r.success;
                })
            );
    }

    delete(id: string): Observable<boolean> {
        return this._apiService.Post<boolean>(`ordre-travail-detail/${id}/delete`, {})
            .pipe(
                map(r => {
                    if (r.success) {
                        this._details.next(
                            this._details.value.filter(d => d.ordreTravailDetailId !== id)
                        );
                    }
                    return r.success;
                })
            );
    }
}
