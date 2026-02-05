import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { EmployeService } from '../../../../core/employes/employe.service';

export const employeResolver = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
    const employeService = inject(EmployeService);
    return employeService.GetEmploye();
};
