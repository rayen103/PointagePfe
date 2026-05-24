import { ActivatedRouteSnapshot, Router, RouterStateSnapshot, Routes } from '@angular/router';
import { inject } from '@angular/core';
import { catchError, of } from 'rxjs';
import { GouvernoratComponent } from './gouvernorat.component';
import { ListComponent } from './list/list.component';
import { DetailsComponent } from './details/details.component';
import { GouvernoratService } from '../../../core/gouvernorat/gouvernorat.service';
import { UserService } from '../../../core/user/user.service';

const gouvernoratResolver = (
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
) => {
    const gouvernoratService = inject(GouvernoratService);
    const router = inject(Router);

    const id = route.paramMap.get('id');

    if (id === 'ajouter') {
        return gouvernoratService.CreateNewGouvernorat();
    }

    return gouvernoratService.GetGouvernoratById(id)
        .pipe(
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
        component: GouvernoratComponent,
        children: [
            {
                path: '',
                component: ListComponent,
                resolve: {
                    gouvernorats: () => inject(GouvernoratService).GetGouvernorats(),
                    navigation: (route: ActivatedRouteSnapshot) => inject(UserService).getNavigation(route.data.navigationId),
                },
                data: {
                    navigationId: 'gouvernorat'
                },
                title: 'Gouvernorats',
            },
            {
                path: ':id',
                component: DetailsComponent,
                resolve: {
                    gouvernorat: gouvernoratResolver,
                    navigation: (route: ActivatedRouteSnapshot) => inject(UserService).getNavigation(route.data.navigationId),
                },
                data: {
                    navigationId: 'gouvernorat'
                },
                title: 'Gouvernorat',
            }
        ]
    }
] as Routes;
