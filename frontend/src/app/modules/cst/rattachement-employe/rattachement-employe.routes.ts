import { ActivatedRouteSnapshot, Router, RouterStateSnapshot, Routes } from '@angular/router';
import { inject } from '@angular/core';
import { catchError, of } from 'rxjs';
import { RattachementEmployeComponent } from './rattachement-employe.component';
import { ListComponent } from './list/list.component';
import { DetailsComponent } from './details/details.component';
import { RattachementEmployeService } from '../../../core/rattachement-employe/rattachement-employe.service';
import { UserService } from '../../../core/user/user.service';

const rattachementEmployeResolver = (
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
) => {
    const service = inject(RattachementEmployeService);
    const router = inject(Router);
    const id = route.paramMap.get('id');

    if (id === 'ajouter') {
        return service.CreateNewRattachementEmploye();
    }

    return service.GetRattachementEmployeById(id).pipe(
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
        component: RattachementEmployeComponent,
        children: [
            {
                path: '',
                component: ListComponent,
                resolve: {
                    rattachementEmployes: () => inject(RattachementEmployeService).GetRattachementEmployes(),
                    navigation: (route: ActivatedRouteSnapshot) => inject(UserService).getNavigation(route.data.navigationId),
                },
                title: 'Rattachements Employes',
            },
            {
                path: ':id',
                component: DetailsComponent,
                resolve: {
                    rattachementEmploye: rattachementEmployeResolver,
                    navigation: (route: ActivatedRouteSnapshot) => inject(UserService).getNavigation(route.data.navigationId),
                },
                title: 'Rattachement Employe',
            }
        ]
    }
] as Routes;
