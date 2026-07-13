import { ActivatedRouteSnapshot, Routes } from '@angular/router';
import { inject } from '@angular/core';
import { PointageComponent } from './pointage.component';
import { ListComponent } from './list/list.component';
import { PointageService } from '../../../core/pointage/pointage.service';
import { UserService } from '../../../core/user/user.service';
import { BusService } from '../../../core/bus/bus.service';

export default [
    {
        path: '',
        component: PointageComponent,
        children: [
            {
                path: '',
                component: ListComponent,
                resolve: {
                    pointages: () => inject(PointageService).GetPointages(),
                    buses: () => inject(BusService).GetBuses(),
                    navigation: (route: ActivatedRouteSnapshot) => inject(UserService).getNavigation(route.data.navigationId),
                },
                title: 'Pointages',
            }
        ]
    }
] as Routes;
