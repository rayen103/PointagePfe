import { ActivatedRouteSnapshot, Router, RouterStateSnapshot, Routes } from '@angular/router';
import { inject } from '@angular/core';
import { catchError, of } from 'rxjs';
import { ModemComponent } from './modem.component';
import { ListComponent } from './list/list.component';
import { DetailsComponent } from './details/details.component';
import { ModemService } from '../../../core/modem/modem.service';
import { UserService } from '../../../core/user/user.service';

const modemResolver = (
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
) => {
    const modemService = inject(ModemService);
    const router = inject(Router);

    const id = route.paramMap.get('id');

    if (id === 'ajouter') {
        return modemService.CreateNewModem();
    }

    return modemService.GetModemById(id)
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
        component: ModemComponent,
        children: [
            {
                path: '',
                component: ListComponent,
                resolve: {
                    modems: () => inject(ModemService).GetModems(),
                    navigation: (route: ActivatedRouteSnapshot) => inject(UserService).getNavigation(route.data.navigationId),
                },
                data: {
                    navigationId: 'modem'
                },
                title: 'Modems',
            },
            {
                path: ':id',
                component: DetailsComponent,
                resolve: {
                    modem: modemResolver,
                    navigation: (route: ActivatedRouteSnapshot) => inject(UserService).getNavigation(route.data.navigationId),
                },
                data: {
                    navigationId: 'modem'
                },
                title: 'Modem',
            }
        ]
    }
] as Routes;
