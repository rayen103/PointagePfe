import { ActivatedRouteSnapshot, Router, RouterStateSnapshot, Routes } from '@angular/router';
import { inject } from '@angular/core';
import { catchError, of } from 'rxjs';
import { RegionComponent } from './region.component';
import { ListComponent } from './list/list.component';
import { DetailsComponent } from './details/details.component';
import { RegionService } from '../../../core/region/region.service';
import { UserService } from '../../../core/user/user.service';

const regionResolver = (
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
) => {
    const regionService = inject(RegionService);
    const router = inject(Router);

    const id = route.paramMap.get('id');

    if (id === 'ajouter') {
        return regionService.CreateNewRegion();
    }

    return regionService.GetRegionById(id)
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
        component: RegionComponent,
        children: [
            {
                path: '',
                component: ListComponent,
                resolve: {
                    regions: () => inject(RegionService).GetRegions(),
                    navigation: (route: ActivatedRouteSnapshot) => inject(UserService).getNavigation(route.data.navigationId),
                },
                data: {
                    navigationId: 'region'
                },
                title: 'Régions',
            },
            {
                path: ':id',
                component: DetailsComponent,
                resolve: {
                    region: regionResolver,
                    navigation: (route: ActivatedRouteSnapshot) => inject(UserService).getNavigation(route.data.navigationId),
                },
                data: {
                    navigationId: 'region'
                },
                title: 'Région',
            }
        ]
    }
] as Routes;
