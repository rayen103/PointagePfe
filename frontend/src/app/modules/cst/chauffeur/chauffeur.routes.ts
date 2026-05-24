import { ActivatedRouteSnapshot, Router, RouterStateSnapshot, Routes } from '@angular/router';
import { inject } from '@angular/core';
import { catchError, of } from 'rxjs';
import { ChauffeurComponent } from './chauffeur.component';
import { ListComponent } from './list/list.component';
import { DetailsComponent } from './details/details.component';
import { ChauffeurService } from '../../../core/chauffeur/chauffeur.service';
import { UserService } from '../../../core/user/user.service';

const chauffeurResolver = (
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
) => {
    const chauffeurService = inject(ChauffeurService);
    const router = inject(Router);

    const id = route.paramMap.get('id');

    if (id === 'ajouter') {
        return chauffeurService.CreateNewChauffeur();
    }

    return chauffeurService.GetChauffeurById(id)
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
        component: ChauffeurComponent,
        children: [
            {
                path: '',
                component: ListComponent,
                resolve: {
                    chauffeurs: () => inject(ChauffeurService).GetChauffeurs(),
                    navigation: (route: ActivatedRouteSnapshot) => inject(UserService).getNavigation(route.data.navigationId),
                },
                data: {
                    navigationId: 'chauffeur'
                },
                title: 'Chauffeurs',
            },
            {
                path: ':id',
                component: DetailsComponent,
                resolve: {
                    chauffeur: chauffeurResolver,
                    navigation: (route: ActivatedRouteSnapshot) => inject(UserService).getNavigation(route.data.navigationId),
                },
                data: {
                    navigationId: 'chauffeur'
                },
                title: 'Chauffeur',
            }
        ]
    }
] as Routes;
