import { ActivatedRouteSnapshot, Router, RouterStateSnapshot, Routes } from '@angular/router';
import { inject } from '@angular/core';
import { catchError, of } from 'rxjs';
import { EquipeComponent } from './equipe.component';
import { ListComponent } from './list/list.component';
import { DetailsComponent } from './details/details.component';
import { EquipeService } from '../../../core/equipe/equipe.service';
import { UserService } from '../../../core/user/user.service';


const equipeResolver = (
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
) => {
    const equipeService = inject(EquipeService);
    const router = inject(Router);

    const id = route.paramMap.get('id')

    if (id === 'ajouter') {
        return equipeService.CreateNewEquipe();
    }

    return equipeService.GetEquipeById(id)
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
        component: EquipeComponent,
        children: [
            {
                path: '',
                component: ListComponent,
                resolve: {
                    equipes: () => inject(EquipeService).GetEquipes(),
                    navigation: (route: ActivatedRouteSnapshot) => inject(UserService).getNavigation(route.data.navigationId),
                },
                title: 'Equipes',
            },
            {
                path: ':id',
                component: DetailsComponent,
                resolve: {
                    equipe: equipeResolver,
                    navigation: (route: ActivatedRouteSnapshot) => inject(UserService).getNavigation(route.data.navigationId),

                },
                title: 'Equipe',
            }
        ]
    }
] as Routes;
