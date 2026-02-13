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

/**
 * Can deactivate PointCollecte
 *
 * @param component
 * @param currentRoute
 * @param currentState
 * @param nextState
 */

const canDeactivatePointCollecte = (
    currentRoute: ActivatedRouteSnapshot,
    currentState: RouterStateSnapshot,
    nextState: RouterStateSnapshot
) => {
    // Get the next route
    let nextRoute: ActivatedRouteSnapshot = nextState.root;

    while (nextRoute.firstChild) {
        nextRoute = nextRoute.firstChild;
    }

    // If we are navigating to another pointcollecte...
    if (!nextState.url.endsWith('/pointcollecte/' + nextRoute.paramMap.get('id'))) {
        // Just navigate
        return true;
    }

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
