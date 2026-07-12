import { ActivatedRouteSnapshot, Router, RouterStateSnapshot, Routes } from '@angular/router';
import { inject } from '@angular/core';
import { catchError, of } from 'rxjs';
import { EmployeComponent } from './employe.component';
import { ListComponent } from './list/list.component';
import { DetailsComponent } from './details/details.component';
import { EmployeService } from '../../../../core/employes/employe.service';
import { UserService } from '../../../../core/user/user.service';

const employeResolver = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
    const employeService = inject(EmployeService);
    return employeService.GetEmploye();
};

const employeDetailsResolver = (
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
) => {
    const employeService = inject(EmployeService);
    const router = inject(Router);

    const id = route.paramMap.get('id');

    if (id === 'ajouter') {
        return employeService.CreateNewEmploye();
    }

    return employeService.GetEmployeById(id)
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
        component: EmployeComponent,
        children: [
            {
                path: '',
                component: ListComponent,
                resolve: {
                    employes: employeResolver,
                    navigation: (route: ActivatedRouteSnapshot) => inject(UserService).getNavigation(route.data.navigationId),
                },
                title: 'Employés'
            },
            {
                path: ':id',
                component: DetailsComponent,
                resolve: {
                    employe: employeDetailsResolver,
                    navigation: (route: ActivatedRouteSnapshot) => inject(UserService).getNavigation(route.data.navigationId),
                },
                title: 'Employé'
            }
        ]
    }
] as Routes;
