import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, map, Observable, tap } from 'rxjs';
import { PagedSite, Site } from './site.model';
import { ApiResponse } from '../common/api-response';

@Injectable({providedIn: 'root'})
export class SiteService {
    private _httpClient = inject(HttpClient);
    private _sites: BehaviorSubject<Site[] | null> = new BehaviorSubject(null);

    /**
     * Getter for sites
     */
    get sites$(): Observable<Site[]> {
        return this._sites.asObservable();
    }

    /**
     * Get sites
     */
    GetSites(search: string = '', sort: string = 'LibelleSite', order: string = 'asc', page: number = 0, size: number = 10, societeId?: string): Observable<PagedSite> {
        return this._httpClient.get<ApiResponse<PagedSite>>('cm/site/list', {
            params: {
                search,
                sort,
                order,
                page: page + 1,
                size,
                societeId: societeId ?? ''
            }
        }).pipe(
            map(response => response.data),
            tap((response) => {
                this._sites.next(response.sites);
            })
        );
    }

    /**
     * Get all sites (without paging for selection)
     */
    GetAllSites(societeId?: string): Observable<Site[]> {
        return this.GetSites('', 'LibelleSite', 'asc', 0, 1000, societeId).pipe(
            map(response => response.sites)
        );
    }

    /**
     * Get site by id
     */
    GetSiteById(id: string): Observable<Site> {
        return this._httpClient.get<ApiResponse<Site>>(`cm/site/${id}/one`).pipe(
            map(response => response.data)
        );
    }

    /**
     * Create site
     */
    CreateSite(site: Site): Observable<Site> {
        return this._httpClient.post<ApiResponse<Site>>('cm/site/add', site).pipe(
            map(response => response.data)
        );
    }

    /**
     * Update site
     */
    UpdateSite(site: Site): Observable<boolean> {
        return this._httpClient.patch<ApiResponse<boolean>>('cm/site/update', site).pipe(
            map(response => response.data)
        );
    }

    /**
     * Delete site
     */
    DeleteSite(id: string): Observable<boolean> {
        return this._httpClient.post<ApiResponse<boolean>>(`cm/site/${id}/delete`, {}).pipe(
            map(response => response.data)
        );
    }
}