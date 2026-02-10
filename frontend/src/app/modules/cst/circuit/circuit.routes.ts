import { ActivatedRouteSnapshot, Router, RouterStateSnapshot, Routes } from '@angular/router';
import { inject } from '@angular/core';
import { catchError, of } from 'rxjs';
import { CircuitComponent } from './circuit.component';
import { ListComponent } from './list/list.component';
import { DetailsComponent } from './details/details.component';
import { CircuitService } from '../../../core/circuit/circuit.service';
import { UserService } from '../../../core/user/user.service';


const circuitResolver = (
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
) => {
    const circuitService = inject(CircuitService);
    const router = inject(Router);

    const id = route.paramMap.get('id')

    if (id === 'ajouter') {
        return circuitService.CreateNewCircuit();
    }

    return circuitService.GetCircuitById(id)
        .pipe(

            catchError(async (error) => {

                const parentUrl = state.url.split('/').slice(0, -1).join('/');

                await router.navigateByUrl(parentUrl);

                return of(error);
            })
        );
};

/**
 * Can deactivate Circuit
 *
 * @param component
 * @param currentRoute
 * @param currentState
 * @param nextState
 */

const canDeactivateCircuit = (
    currentRoute: ActivatedRouteSnapshot,
    currentState: RouterStateSnapshot,
    nextState: RouterStateSnapshot
) => {
    // Get the next route
    let nextRoute: ActivatedRouteSnapshot = nextState.root;

    while (nextRoute.firstChild) {
        nextRoute = nextRoute.firstChild;
    }

    // If we are navigating to another circuit...
    if (!nextState.url.endsWith('/circuit/' + nextRoute.paramMap.get('id'))) {
        // Just navigate
        return true;
    }

};

export default [
    {
        path: '',
        component: CircuitComponent,
        children: [
            {
                path: '',
                component: ListComponent,
                resolve: {
                    circuits: () => inject(CircuitService).GetCircuit(),
                    navigation: (route: ActivatedRouteSnapshot) => inject(UserService).getNavigation(route.data.navigationId),
                },
                title: 'Circuits',
            },
            {
                path: ':id',
                component: DetailsComponent,
                resolve: {
                    circuit: circuitResolver,
                    navigation: (route: ActivatedRouteSnapshot) => inject(UserService).getNavigation(route.data.navigationId),

                },
                title: 'Circuit',
            }
        ]
    }
] as Routes;
