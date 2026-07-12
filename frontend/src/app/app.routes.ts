import { Route } from '@angular/router';
import { initialDataResolver } from 'app/app.resolvers';
import { AuthGuard } from 'app/core/auth/guards/auth.guard';
import { NoAuthGuard } from 'app/core/auth/guards/noAuth.guard';
import { LayoutComponent } from 'app/layout/layout.component';
import { navigationGuard } from './core/navigation/guards/navigation.guard';
import { GestionShellComponent } from './modules/cst/gestion-shell/gestion-shell.component';

// @formatter:off
/* eslint-disable max-len */
/* eslint-disable @typescript-eslint/explicit-function-return-type */
export const appRoutes: Route[] = [

    // La porte d'entrée publique : la landing page présente le produit.
    {path: '', pathMatch : 'full', redirectTo: 'home'},

    // Redirect signed-in user to the '/example'
    //
    // After the user signs in, the sign-in page will redirect the user to the 'signed-in-redirect'
    // path. Below is another redirection for that path to redirect the user to the desired
    // location. This is a small convenience to keep all main routes together here on this file.
    {path: 'signed-in-redirect', pathMatch : 'full', redirectTo: 'Accueil/page'},

    // Auth routes for guests
    {
        path: '',
        canActivate: [NoAuthGuard],
        canActivateChild: [NoAuthGuard],
        component: LayoutComponent,
        data: {
            layout: 'empty'
        },
        children: [
            {path: 'confirmation-required', loadChildren: () => import('app/modules/auth/confirmation-required/confirmation-required.routes')},
            {path: 'forgot-password', loadChildren: () => import('app/modules/auth/forgot-password/forgot-password.routes')},
            {path: 'reset-password', loadChildren: () => import('app/modules/auth/reset-password/reset-password.routes')},
            {path: 'sign-in', loadChildren: () => import('app/modules/auth/sign-in/sign-in.routes')},
            {path: 'sign-up', loadChildren: () => import('app/modules/auth/sign-up/sign-up.routes')},
            {path: 'verify-code', loadChildren: () => import('app/modules/auth/verify-code/verify-code.routes')}
        ]
    },

    // Auth routes for authenticated users
    {
        path: '',
        canActivate: [AuthGuard],
        canActivateChild: [AuthGuard],
        component: LayoutComponent,
        data: {
            layout: 'empty'
        },
        children: [
            {path: 'sign-out', loadChildren: () => import('app/modules/auth/sign-out/sign-out.routes')},
            {path: 'unlock-session', loadChildren: () => import('app/modules/auth/unlock-session/unlock-session.routes')}
        ]
    },

    // Landing routes
    {
        path: '',
        component: LayoutComponent,
        data: {
            layout: 'empty'
        },
        children: [
            {path: 'home', loadChildren: () => import('app/modules/landing/home/home.routes')},
        ]
    },

    // Admin routes
    {
        path: '',
        canActivate: [AuthGuard],
        canActivateChild: [AuthGuard],
        component: LayoutComponent,
        resolve: {
            initialData: initialDataResolver
        },
        children: [
            {path: 'example', loadChildren: () => import('app/modules/admin/example/example.routes')},
            {path: 'dashboard', pathMatch: 'full', redirectTo: 'Accueil/page'},
            {path: 'dashboards', pathMatch: 'full', redirectTo: 'Accueil/page'},
            {path: 'dashboards/dashboard', pathMatch: 'full', redirectTo: 'Accueil/page'},
            {path: 'dashboards/project', pathMatch: 'full', redirectTo: 'Accueil/page'},
            {path: 'dashboards/analytics', pathMatch: 'full', redirectTo: 'Accueil/page'},
            {path: 'dashboards/finance', pathMatch: 'full', redirectTo: 'Accueil/page'},
            {path: 'dashboards/crypto', pathMatch: 'full', redirectTo: 'Accueil/page'},
        ]
    },

    //Collect Management
    {
        path: '',
        canMatch: [AuthGuard],
        canActivateChild: [navigationGuard],
        component: LayoutComponent,
        resolve: {
            initialData: initialDataResolver,
        },
        children: [


            //Accueil
            {
                path:'Accueil', children:[
                    {path:'page',loadChildren:() => import('app/modules/collectmanagement/accueil/accueil.routes')},

                ]
            },
            {path: 'utilisateur', pathMatch: 'full', redirectTo: 'fichier/utilisateur'},
            {path: 'societe', pathMatch: 'full', redirectTo: 'fichier/societe'},
            {path: 'role-utilisateur', pathMatch: 'full', redirectTo: 'fichier/role-utilisateur'},
            {path: 'employe', pathMatch: 'full', redirectTo: 'fichier/employe'},
            {path: 'circuit', pathMatch: 'full', redirectTo: 'fichier/circuit'},
            {path: 'pointcollecte', pathMatch: 'full', redirectTo: 'fichier/pointcollecte'},
            {path: 'equipe', pathMatch: 'full', redirectTo: 'fichier/equipe'},
            {path: 'ordretravail', pathMatch: 'full', redirectTo: 'fichier/ordretravail'},
            {path: 'rattachement', pathMatch: 'full', redirectTo: 'fichier/rattachement'},
            {path: 'shift', pathMatch: 'full', redirectTo: 'fichier/shift'},
            {path: 'bus', pathMatch: 'full', redirectTo: 'fichier/bus'},
            {path: 'region', pathMatch: 'full', redirectTo: 'fichier/region'},
            {path: 'gouvernorat', pathMatch: 'full', redirectTo: 'fichier/gouvernorat'},
            {path: 'chauffeur', pathMatch: 'full', redirectTo: 'fichier/chauffeur'},
            {path: 'modem', pathMatch: 'full', redirectTo: 'fichier/modem'},
            {path: 'chantier', pathMatch: 'full', redirectTo: 'fichier/chantier'},
            {path: 'rattachement-employe', pathMatch: 'full', redirectTo: 'fichier/rattachement-employe'},
            {path: 'rattachement-article', pathMatch: 'full', redirectTo: 'fichier/rattachement-article'},
            //Fichier — wrapped in the Gestion shell so children show as a left sidebar (not a dropdown)
            {path:'fichier', component: GestionShellComponent, children:[
                    {path:'utilisateur',
                        data:{navigationId: 'fichier.utilisateur'},
                        loadChildren:() => import('app/modules/collectmanagement/gestion-utilisateur/utilisateur/utilisateur.routes')},
                    {path:'societe' ,
                        data:{navigationId: 'fichier.societe'},
                        loadChildren:() => import('./modules/cst/societe/societe.routes')},
                    {path:'role-utilisateur',
                        data:{navigationId:'fichier.role-utilisateur'} ,
                        loadChildren:() => import('app/modules/collectmanagement/gestion-utilisateur/role-utilisateur/role-utilisateur.routes')},
                    {path:'employe',
                        data:{navigationId:'fichier.employe'} ,
                        loadChildren:() => import('app/modules/collectmanagement/gestion-employe/employe/employe.routes')},
                    {path:'circuit',
                        data:{navigationId:'fichier.circuit'} ,
                        loadChildren:() => import('./modules/cst/circuit/circuit.routes')},
                    {path:'pointcollecte',
                        data:{navigationId:'fichier.pointcollecte'} ,
                        loadChildren:() => import('./modules/cst/pointcollecte/pointcollecte.routes')},
                    {path:'equipe',
                        data:{navigationId:'fichier.equipe'} ,
                        loadChildren:() => import('./modules/cst/equipe/equipe.routes')},
                    {path:'ordretravail',
                        data:{navigationId:'fichier.ordretravail'} ,
                        loadChildren:() => import('./modules/cst/ordretravail/ordretravail.routes')},
                    {path:'rattachement',
                        data:{navigationId:'fichier.rattachement'} ,
                        loadChildren:() => import('./modules/cst/rattachement/rattachement.routes')},
                    {path:'shift',
                        data:{navigationId:'fichier.shift'},
                        loadChildren:() => import('./modules/cst/shift/shift.routes')},
                    {path:'bus',
                        data:{navigationId:'fichier.bus'},
                        loadChildren:() => import('./modules/cst/bus/bus.routes')},
                    {path:'region',
                        data:{navigationId:'fichier.region'},
                        loadChildren:() => import('./modules/cst/region/region.routes')},
                    {path:'gouvernorat',
                        data:{navigationId:'fichier.gouvernorat'},
                        loadChildren:() => import('./modules/cst/gouvernorat/gouvernorat.routes')},
                    {path:'chauffeur',
                        data:{navigationId:'fichier.chauffeur'},
                        loadChildren:() => import('./modules/cst/chauffeur/chauffeur.routes')},
                    {path:'modem',
                        data:{navigationId:'fichier.modem'},
                        loadChildren:() => import('./modules/cst/modem/modem.routes')},
                    {path:'chantier',
                        data:{navigationId:'fichier.chantier'},
                        loadChildren:() => import('./modules/cst/chantier/chantier.routes')},
                    {path:'rattachement-employe',
                        data:{navigationId:'fichier.rattachement-employe'},
                        loadChildren:() => import('./modules/cst/rattachement-employe/rattachement-employe.routes')},
                    {path:'rattachement-article',
                        data:{navigationId:'fichier.rattachement-article'},
                        loadChildren:() => import('./modules/cst/rattachement-article/rattachement-article.routes')},
                ]},

            // Monitoring
            {
                path: 'monitoring',
                children: [
                    {
                        path: 'bus-tracking',
                        data: { navigationId: 'monitoring.bus-tracking' },
                        loadChildren: () =>
                            import(
                                'app/modules/collectmanagement/monitoring/bus-tracking/bus-tracking.routes'
                            ),
                    },
                ],
            },

            // Analyse / BI
            {
                path: 'analyse',
                data: { navigationId: 'analyse' },
                loadChildren: () => import('app/modules/collectmanagement/analyse/analyse.routes'),
            },


        ],

    }
];
