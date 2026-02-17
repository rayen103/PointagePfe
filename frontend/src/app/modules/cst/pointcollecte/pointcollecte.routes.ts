import { ActivatedRouteSnapshot, Router, RouterStateSnapshot, Routes } from '@angular/router';
import { inject } from '@angular/core';
import { catchError, of } from 'rxjs';
import { PointCollecteComponent } from './pointcollecte.component';
import { ListComponent } from './list/list.component';
import { DetailsComponent } from './details/details.component';
import { PointCollecteService } from '../../../core/point-collecte/point-collecte.service';
import { UserService } from '../../../core/user/user.service';


const pointCollecteResolver = (
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
) => {
    const pointCollecteService = inject(PointCollecteService);
    const router = inject(Router);

    const id = route.paramMap.get('id')

    if (id === 'ajouter') {
        return pointCollecteService.CreateNewPointCollecte();
    }

    return pointCollecteService.GetPointCollecteById(id)
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
        component: PointCollecteComponent,
        children: [
            {
                path: '',
                component: ListComponent,
                resolve: {
                    pointsCollecte: () => inject(PointCollecteService).GetPointsCollecte(),
                    navigation: (route: ActivatedRouteSnapshot) => inject(UserService).getNavigation(route.data.navigationId),
                },
                title: 'Points de Collecte',
            },
            {
                path: ':id',
                component: DetailsComponent,
                resolve: {
                    pointCollecte: pointCollecteResolver,
                    navigation: (route: ActivatedRouteSnapshot) => inject(UserService).getNavigation(route.data.navigationId),

                },
                title: 'Point de Collecte',
            }
        ]
    }
] as Routes;
