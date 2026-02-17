import { ActivatedRouteSnapshot, Router, RouterStateSnapshot, Routes } from '@angular/router';
import { inject } from '@angular/core';
import { catchError, of } from 'rxjs';
import { RattachementComponent } from './rattachement.component';
import { ListComponent } from './list/list.component';
import { DetailsComponent } from './details/details.component';
import { RattachementService } from '../../../core/rattachement/rattachement.service';
import { UserService } from '../../../core/user/user.service';


const rattachementResolver = (
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
) => {
    const rattachementService = inject(RattachementService);
    const router = inject(Router);

    const id = route.paramMap.get('id')

    if (id === 'ajouter') {
        return rattachementService.CreateNewRattachement();
    }

    return rattachementService.GetRattachementById(id)
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
        component: RattachementComponent,
        children: [
            {
                path: '',
                component: ListComponent,
                resolve: {
                    rattachements: () => inject(RattachementService).GetRattachements(),
                    navigation: (route: ActivatedRouteSnapshot) => inject(UserService).getNavigation(route.data.navigationId),
                },
                title: 'Rattachements',
            },
            {
                path: ':id',
                component: DetailsComponent,
                resolve: {
                    rattachement: rattachementResolver,
                    navigation: (route: ActivatedRouteSnapshot) => inject(UserService).getNavigation(route.data.navigationId),

                },
                title: 'Rattachement',
            }
        ]
    }
] as Routes;
