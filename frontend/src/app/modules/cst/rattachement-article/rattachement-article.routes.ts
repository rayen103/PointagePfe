import { ActivatedRouteSnapshot, Router, RouterStateSnapshot, Routes } from '@angular/router';
import { inject } from '@angular/core';
import { catchError, of } from 'rxjs';
import { RattachementArticleComponent } from './rattachement-article.component';
import { ListComponent } from './list/list.component';
import { DetailsComponent } from './details/details.component';
import { RattachementArticleService } from '../../../core/rattachement-article/rattachement-article.service';
import { UserService } from '../../../core/user/user.service';

const rattachementArticleResolver = (
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
) => {
    const service = inject(RattachementArticleService);
    const router = inject(Router);
    const id = route.paramMap.get('id');

    if (id === 'ajouter') {
        return service.CreateNewRattachementArticle();
    }

    return service.GetRattachementArticleById(id).pipe(
        catchError(async (error) => {
            const parentUrl = state.url.split('/').slice(0, -1).join('/');
            await router.navigateByUrl(parentUrl);
            return of(error);
        })
    );
};

export default [
    {
        path: '',
        component: RattachementArticleComponent,
        children: [
            {
                path: '',
                component: ListComponent,
                resolve: {
                    rattachementArticles: () => inject(RattachementArticleService).GetRattachementArticles(),
                    navigation: (route: ActivatedRouteSnapshot) => inject(UserService).getNavigation(route.data.navigationId),
                },
                title: 'Rattachements Articles',
            },
            {
                path: ':id',
                component: DetailsComponent,
                resolve: {
                    rattachementArticle: rattachementArticleResolver,
                    navigation: (route: ActivatedRouteSnapshot) => inject(UserService).getNavigation(route.data.navigationId),
                },
                title: 'Rattachement Article',
            }
        ]
    }
] as Routes;
