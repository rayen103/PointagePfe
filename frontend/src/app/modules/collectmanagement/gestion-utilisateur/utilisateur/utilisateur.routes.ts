import { ActivatedRouteSnapshot, Router, RouterStateSnapshot, Routes } from '@angular/router';
import { inject } from '@angular/core';
import { catchError, of } from 'rxjs';
import { UtilisateurComponent } from './utilisateur.component';
import { ListComponent } from './list/list.component';
import { DetailsComponent } from './details/details.component';
import { UtilisateurService } from '../../../../core/utilisateurs/utilisateur.service';
import { RoleUtilisateurService } from '../../../../core/role-utilisateur/role-utilisateur.service';
import { UserService } from '../../../../core/user/user.service';

const utilisateurListResolver = () => inject(UtilisateurService).GetUtilisateur();

const utilisateurDetailsResolver = (
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
) => {
    const utilisateurService = inject(UtilisateurService);
    const router = inject(Router);

    const id = route.paramMap.get('id');

    if (id === 'ajouter') {
        return utilisateurService.CreateNewUtilisateur();
    }

    return utilisateurService.GetUtilisateurById(id).pipe(
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
        component: UtilisateurComponent,
        children: [
            {
                path: '',
                component: ListComponent,
                resolve: {
                    utilisateurs: utilisateurListResolver,
                    roles: () => inject(RoleUtilisateurService).GetAllRoleUtilisateur(),
                    navigation: (route: ActivatedRouteSnapshot) =>
                        inject(UserService).getNavigation(route.data.navigationId),
                },
                title: 'Utilisateurs',
            },
            {
                path: ':id',
                component: DetailsComponent,
                resolve: {
                    utilisateur: utilisateurDetailsResolver,
                    roles: () => inject(RoleUtilisateurService).GetAllRoleUtilisateur(),
                    navigation: (route: ActivatedRouteSnapshot) =>
                        inject(UserService).getNavigation(route.data.navigationId),
                },
                title: 'Utilisateur',
            },
        ],
    },
] as Routes;
