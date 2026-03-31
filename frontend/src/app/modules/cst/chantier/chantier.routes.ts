import { ActivatedRouteSnapshot, Router, RouterStateSnapshot, Routes } from '@angular/router';
import { inject } from '@angular/core';
import { catchError, of } from 'rxjs';
import { ChantierComponent } from './chantier.component';
import { ListComponent } from './list/list.component';
import { DetailsComponent } from './details/details.component';
import { ChantierService } from '../../../core/chantier/chantier.service';
import { UserService } from '../../../core/user/user.service';

const chantierResolver = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
    const chantierService = inject(ChantierService);
    const router = inject(Router);
    const id = route.paramMap.get('id');
    if (id === 'ajouter') return chantierService.CreateNewChantier();
    return chantierService.GetChantierById(id).pipe(
        catchError(async (error) => { await router.navigateByUrl(state.url.split('/').slice(0, -1).join('/')); return of(error); })
    );
};

export default [
    {
        path: '',
        component: ChantierComponent,
        children: [
            {
                path: '',
                component: ListComponent,
                resolve: {
                    chantiers: () => inject(ChantierService).GetChantiers(),
                    navigation: (route: ActivatedRouteSnapshot) => inject(UserService).getNavigation(route.data.navigationId),
                },
                title: 'Chantiers',
            },
            {
                path: ':id',
                component: DetailsComponent,
                resolve: {
                    chantier: chantierResolver,
                    navigation: (route: ActivatedRouteSnapshot) => inject(UserService).getNavigation(route.data.navigationId),
                },
                title: 'Chantier',
            }
        ]
    }
] as Routes;
