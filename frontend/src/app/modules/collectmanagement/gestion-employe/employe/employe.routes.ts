import { EmployeComponent } from './employe.component';
import { employeResolver } from './employe.resolver';
import { ActivatedRouteSnapshot, Routes } from '@angular/router';
import { inject } from '@angular/core';
import { UserService } from '../../../../core/user/user.service';

export default [
    {
        path: '',
        component: EmployeComponent,
        resolve: {
            employes: employeResolver,
            navigation: (route: ActivatedRouteSnapshot) => inject(UserService).getNavigation(route.data.navigationId),
        },
        title: 'Employé'
    }
] as Routes;
