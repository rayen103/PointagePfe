import { ActivatedRouteSnapshot, Router, RouterStateSnapshot, Routes } from '@angular/router';
import { inject } from '@angular/core';
import { catchError, of } from 'rxjs';
import { OrdretravailComponent } from './ordretravail.component';
import { ListComponent } from './list/list.component';
import { DetailsComponent } from './details/details.component';
import { OrdreTravailService } from '../../../core/ordre-travail/ordre-travail.service';
import { UserService } from '../../../core/user/user.service';


const ordreTravailResolver = (
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
) => {
    const ordreTravailService = inject(OrdreTravailService);
    const router = inject(Router);

    const id = route.paramMap.get('id')

    if (id === 'ajouter') {
        return ordreTravailService.CreateNewOrdreTravail();
    }

    return ordreTravailService.GetOrdreTravailById(id)
        .pipe(

            catchError(async (error) => {

                const parentUrl = state.url.split('/').slice(0, -1).join('/');

                await router.navigateByUrl(parentUrl);

                return of(error);
            })
        );
};

/**
 * Can deactivate OrdreTravail
 *
 * @param component
 * @param currentRoute
 * @param currentState
 * @param nextState
 */

const canDeactivateOrdreTravail = (
    currentRoute: ActivatedRouteSnapshot,
    currentState: RouterStateSnapshot,
    nextState: RouterStateSnapshot
) => {
    // Get the next route
    let nextRoute: ActivatedRouteSnapshot = nextState.root;

    while (nextRoute.firstChild) {
        nextRoute = nextRoute.firstChild;
    }

    // If we are navigating to another ordre travail...
    if (nextState.url.endsWith('/ordretravail/' + nextRoute.paramMap.get('id'))) {
        // Just navigate
        return true;
    }

};

export default [
    {
        path: '',
        component: OrdretravailComponent,
        children: [
            {
                path: '',
                component: ListComponent,
                resolve: {
                    ordresTravail: () => inject(OrdreTravailService).GetOrdresTravail(),
                    navigation: (route: ActivatedRouteSnapshot) => inject(UserService).getNavigation(route.data.navigationId),
                },
                title: 'Ordres de Travail',
            },
            {
                path: ':id',
                component: DetailsComponent,
                resolve: {
                    ordreTravail: ordreTravailResolver,
                    navigation: (route: ActivatedRouteSnapshot) => inject(UserService).getNavigation(route.data.navigationId),

                },
                title: 'Ordre de Travail',
            }
        ]
    }
] as Routes;
