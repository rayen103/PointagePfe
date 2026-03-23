import { ActivatedRouteSnapshot, Router, RouterStateSnapshot, Routes } from '@angular/router';
import { inject } from '@angular/core';
import { catchError, of } from 'rxjs';
import { ShiftComponent } from './shift.component';
import { ListComponent } from './list/list.component';
import { DetailsComponent } from './details/details.component';
import { ShiftService } from '../../../core/shift/shift.service';
import { UserService } from '../../../core/user/user.service';


const shiftResolver = (
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
) => {
    const shiftService = inject(ShiftService);
    const router = inject(Router);

    const id = route.paramMap.get('id');

    if (id === 'ajouter') {
        return shiftService.CreateNewShift();
    }

    return shiftService.GetShiftById(id)
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
        component: ShiftComponent,
        children: [
            {
                path: '',
                component: ListComponent,
                resolve: {
                    shifts: () => inject(ShiftService).GetShifts(),
                    navigation: (route: ActivatedRouteSnapshot) => inject(UserService).getNavigation(route.data.navigationId),
                },
                title: 'Shifts',
            },
            {
                path: ':id',
                component: DetailsComponent,
                resolve: {
                    shift: shiftResolver,
                    navigation: (route: ActivatedRouteSnapshot) => inject(UserService).getNavigation(route.data.navigationId),
                },
                title: 'Shift',
            }
        ]
    }
] as Routes;
