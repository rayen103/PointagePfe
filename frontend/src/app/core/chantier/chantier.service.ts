import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable, of, tap } from 'rxjs';
import { Chantier, PagedChantier } from './chantier.model';
import { ApiService } from '../common/api.service';

@Injectable({ providedIn: 'root' })
export class ChantierService {
    private _chantiers = new BehaviorSubject<Chantier[] | null>([]);
    private _chantier = new BehaviorSubject<Chantier | null>(null);
    private _chantiersLength = new BehaviorSubject<number | null>(0);

    constructor(private _apiservice: ApiService) {}

    get chantiers$(): Observable<Chantier[]> { return this._chantiers.asObservable(); }
    get chantier$(): Observable<Chantier> { return this._chantier.asObservable(); }
    get chantiersLength$(): Observable<number> { return this._chantiersLength.asObservable(); }

    GetChantiers(page = 1, size = 1000, sort = 'numeroChantier', order: 'asc' | 'desc' | '' = 'asc', search = ''): Observable<PagedChantier> {
        return this._apiservice.Get<PagedChantier>('chantier/list', { params: { search: search || '', sort, order, page, size } })
            .pipe(
                tap(r => { this._chantiers.next(r.data?.chantiers); this._chantiersLength.next(r.data?.totalCount); }),
                map(r => r.data)
            );
    }

    CreateNewChantier(): Observable<Chantier> {
        const newChantier: Chantier = { chantierId: null, numeroChantier: '', isActive: true, societeId: '' };
        this._chantier.next(newChantier);
        return of(newChantier);
    }

    AddChantier(chantier: Chantier): Observable<Chantier> {
        return this._apiservice.Post<Chantier>('chantier/add', chantier)
            .pipe(map(r => {
                if (!r.success) throw new Error(r.message || 'Failed');
                chantier.chantierId = r.data.chantierId;
                this._chantiers.next([r.data, ...(this._chantiers.value ?? [])]);
                return r.data;
            }));
    }

    UpdateChantier(chantier: Chantier): Observable<boolean> {
        return this._apiservice.Patch<boolean>('chantier/update', chantier)
            .pipe(map(r => {
                if (!r.success) return false;
                const idx = this._chantiers.value?.findIndex(i => i.chantierId === chantier.chantierId);
                if (idx != null && idx > -1) this._chantiers.value[idx] = chantier;
                this._chantier.next(chantier);
                return true;
            }));
    }

    GetChantierById(id: string): Observable<Chantier> {
        return this._apiservice.Get<Chantier>(`chantier/${id}/one`)
            .pipe(tap(r => this._chantier.next(r.data)), map(r => r.data));
    }

    DeleteChantier(chantier: { chantierId: string }): Observable<boolean> {
        return this._apiservice.Post<boolean>(`chantier/${chantier.chantierId}/delete`, chantier)
            .pipe(tap(r => { if (r.success) this._chantiers.next(this._chantiers.value?.filter(i => i.chantierId !== chantier.chantierId)); }), map(r => r.success));
    }
}
