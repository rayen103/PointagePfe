import { ActivatedRouteSnapshot, Routes } from '@angular/router';
import { inject } from '@angular/core';
import { BusTrackingComponent } from './bus-tracking.component';
import { BusService } from '../../../../core/bus/bus.service';
import { UserService } from '../../../../core/user/user.service';

export default [
    {
        path: '',
        component: BusTrackingComponent,
        resolve: {
            buses: () => inject(BusService).GetBuses(),
            navigation: (route: ActivatedRouteSnapshot) =>
                inject(UserService).getNavigation(route.data.navigationId),
        },
        title: 'Bus Tracking',
    },
] as Routes;

