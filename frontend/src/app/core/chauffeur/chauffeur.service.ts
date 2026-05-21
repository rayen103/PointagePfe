import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { ApiService } from '../common/api.service';
import { PagedChauffeur } from './chauffeur.model';

@Injectable({
    providedIn: 'root',
})
export class ChauffeurService {
    constructor(private _apiService: ApiService) {}

    GetChauffeurs(
        page: number = 1,
        size: number = 1000,
        sort: string = 'codeChauffeur',
        order: 'asc' | 'desc' | '' = 'asc',
        search: string = ''
    ): Observable<PagedChauffeur> {
        return this._apiService
            .Get<PagedChauffeur>('chauffeur/list', {
                params: { search: search || '', sort, order, page, size },
            })
            .pipe(map((r) => r.data));
    }
}
