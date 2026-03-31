import { ActivatedRouteSnapshot, Router, RouterStateSnapshot, Routes } from '@angular/router';
import { inject } from '@angular/core';
import { catchError, of } from 'rxjs';
import { BusComponent } from './bus.component';
import { ListComponent } from './list/list.component';
import { DetailsComponent } from './details/details.component';
import { BusService } from '../../../core/bus/bus.service';
import { UserService } from '../../../core/user/user.service';


const busResolver = (
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
) => {
    const busService = inject(BusService);
    const router = inject(Router);

    const id = route.paramMap.get('id');

    if (id === 'ajouter') {
        return busService.CreateNewBus();
    }

    return busService.GetBusById(id)
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
        component: BusComponent,
        children: [
            {
                path: '',
                component: ListComponent,
                resolve: {
                    buses: () => inject(BusService).GetBuses(),
                    navigation: (route: ActivatedRouteSnapshot) => inject(UserService).getNavigation(route.data.navigationId),
                },
                title: 'Buses',
            },
            {
                path: ':id',
                component: DetailsComponent,
                resolve: {
                    bus: busResolver,
                    navigation: (route: ActivatedRouteSnapshot) => inject(UserService).getNavigation(route.data.navigationId),
                },
                title: 'Bus',
            }
        ]
    }
] as Routes;
