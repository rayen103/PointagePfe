import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable, of, tap } from 'rxjs';
import { PagedRattachementArticle, RattachementArticle } from './rattachement-article.model';
import { ApiService } from '../common/api.service';

@Injectable({ providedIn: 'root' })
export class RattachementArticleService {
    private _rattachementArticles = new BehaviorSubject<RattachementArticle[] | null>([]);
    private _rattachementArticle = new BehaviorSubject<RattachementArticle | null>(null);
    private _rattachementArticlesLength = new BehaviorSubject<number | null>(0);

    constructor(private _apiservice: ApiService) {}

    get rattachementArticles$(): Observable<RattachementArticle[]> {
        return this._rattachementArticles.asObservable();
    }

    get rattachementArticle$(): Observable<RattachementArticle> {
        return this._rattachementArticle.asObservable();
    }

    get rattachementArticlesLength$(): Observable<number> {
        return this._rattachementArticlesLength.asObservable();
    }

    GetRattachementArticles(
        page: number = 1,
        size: number = 1000,
        sort: string = 'codeArticle',
        order: 'asc' | 'desc' | '' = 'asc',
        search: string = ''
    ): Observable<PagedRattachementArticle> {
        return this._apiservice.Get<PagedRattachementArticle>('rattachement-article/list', {
            params: { search: search || '', sort, order, page, size }
        }).pipe(
            tap(r => {
                this._rattachementArticles.next(r.data?.rattachementArticles);
                this._rattachementArticlesLength.next(r.data?.totalCount);
            }),
            map(r => r.data)
        );
    }

    CreateNewRattachementArticle(): Observable<RattachementArticle> {
        const newItem: RattachementArticle = {
            rattachementArticleId: null,
            rattachementId: '',
            codeArticle: '',
            libelle: '',
            quantite: null,
            prixRevient: null,
            tauxTVA: null,
            codeUnite: '',
            codeEntrepot: '',
            typeRattachement: '',
            numeroBonLivraison: '',
            dateBonLivraison: null,
            isActive: true,
            societeId: ''
        };
        this._rattachementArticle.next(newItem);
        return of(newItem);
    }

    AddRattachementArticle(rattachementArticle: RattachementArticle): Observable<RattachementArticle> {
        return this._apiservice.Post<RattachementArticle>('rattachement-article/add', rattachementArticle)
            .pipe(
                map(r => {
                    if (!r.success) throw new Error(r.message || 'Failed to create rattachement article');
                    rattachementArticle.rattachementArticleId = r.data.rattachementArticleId;
                    this._rattachementArticles.next([r.data, ...(this._rattachementArticles.value ?? [])]);
                    return r.data;
                })
            );
    }

    UpdateRattachementArticle(rattachementArticle: RattachementArticle): Observable<boolean> {
        return this._apiservice.Patch<boolean>('rattachement-article/update', rattachementArticle)
            .pipe(
                map(r => {
                    if (!r.success) return false;
                    const index = this._rattachementArticles.value
                        ?.findIndex(item => item.rattachementArticleId === rattachementArticle.rattachementArticleId);
                    if (index != null && index > -1) {
                        this._rattachementArticles.value[index] = rattachementArticle;
                    }
                    this._rattachementArticle.next(rattachementArticle);
                    return true;
                })
            );
    }

    GetRattachementArticleById(id: string): Observable<RattachementArticle> {
        return this._apiservice.Get<RattachementArticle>(`rattachement-article/${id}/one`)
            .pipe(
                tap(r => this._rattachementArticle.next(r.data)),
                map(r => r.data)
            );
    }

    DeleteRattachementArticle(rattachementArticle: { rattachementArticleId: string }): Observable<boolean> {
        return this._apiservice.Post<boolean>(
            `rattachement-article/${rattachementArticle.rattachementArticleId}/delete`,
            rattachementArticle
        ).pipe(
            tap(r => {
                if (r.success) {
                    this._rattachementArticles.next(
                        this._rattachementArticles.value?.filter(
                            item => item.rattachementArticleId !== rattachementArticle.rattachementArticleId
                        )
                    );
                }
            }),
            map(r => r.success)
        );
    }
}
