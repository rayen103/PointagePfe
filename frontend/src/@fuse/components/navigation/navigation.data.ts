/* eslint-disable */
import { FuseNavigationAction, FuseNavigationItem } from '@fuse/components/navigation';

export const defaultNavigation: FuseNavigationItem[] = [

    {
        id   : 'fichier',
        title: 'Fichier',
        type : 'group',
        icon : 'mat_outline:assignment',
        children: [

            {
                id   : 'fichier.societe',
                title: 'Societe',
                type : 'basic',
                icon : 'mat_outline:group',
                link : '/fichier/societe',
                action:[FuseNavigationAction.Add, FuseNavigationAction.Edit, FuseNavigationAction.Delete]
            },
            {
                id   : 'fichier.utilisateur',
                title: 'Utilisateur',
                type : 'basic',
                icon : 'mat_outline:group',
                link : '/fichier/utilisateur',
                action:[FuseNavigationAction.Add, FuseNavigationAction.Edit, FuseNavigationAction.Delete]
            },
            {
                id   : 'fichier.role-utilisateur',
                title: 'Role',
                type : 'basic',
                icon : 'mat_outline:manage_accounts',
                link : '/fichier/role-utilisateur',
                action:[FuseNavigationAction.Add, FuseNavigationAction.Edit, FuseNavigationAction.Delete]
            },
        ]
    },
    {
        id   : 'dashboards',
        title: 'Dashboard',
        type : 'group',
        icon : 'mat_outline:assignment',
        children: [

            {
                id   : 'dashboards.dashboard',
                title: 'Dashboard',
                type : 'basic',
                icon : 'mat_outline:group',
                link : '/dashboards/dashboard',
                action:[FuseNavigationAction.Add, FuseNavigationAction.Edit, FuseNavigationAction.Delete]
            },
        ]
    },
    {
        id   : 'configurations',
        title: 'Configurations',
        type : 'group',
        icon : 'mat_outline:table_chart',
        children:[

            {
                id   : 'configurations.base',
                title: 'Base',
                type : 'basic',
                icon : 'mat_outline:developer_board',
                link : 'configurations/base',
                action:[FuseNavigationAction.Add, FuseNavigationAction.Edit, FuseNavigationAction.Delete]
            },
            {
                id   : 'configurations.formule',
                title: 'Formules',
                type : 'basic',
                icon : 'mat_outline:developer_board',
                link : 'configurations/formule',
                action:[FuseNavigationAction.Add, FuseNavigationAction.Edit, FuseNavigationAction.Delete]
            },

        ]
    },
    {
        id   : 'bonMelanges',
        title: 'Bon De Mélange',
        type : 'group',
        icon : 'mat_outline:table_chart',
        children:[

            {
                id   : 'bonMelanges.bonMelange',
                title: 'Bon De Mélange',
                type : 'basic',
                icon : 'mat_outline:developer_board',
                link : 'bonMelanges/bonMelange',
                action:[FuseNavigationAction.Add, FuseNavigationAction.Edit, FuseNavigationAction.Delete]
            },
            {
                id   : 'bonMelanges.gestionBonMelange',
                title: 'Gestion Bon De Mélange',
                type : 'basic',
                icon : 'mat_outline:developer_board',
                link : 'bonMelanges/gestionBonMelange',
                action:[FuseNavigationAction.Add, FuseNavigationAction.Edit, FuseNavigationAction.Delete]
            },

        ]
    },


];

export const compactNavigation: FuseNavigationItem[] = [
    {
        id   : 'homePage',
        tooltip: 'Home Page',
        title: 'H.P',
        type : 'aside',
        icon : 'heroicons_outline:home',
        children:[]
    },

    {
        id   : 'analysis',
        title: 'A',
        tooltip: 'Analysis',
        type : 'aside',
        icon : 'mat_outline:analytics',
        children: []
    },

    {
        id   : 'gestion-service',
        title: 'S.M',
        tooltip: 'Service management',
        type : 'aside',
        icon : 'heroicons_outline:user-group',
        children: []
    },

    {
        id   : 'donnees',
        title: 'S',
        tooltip: 'Satellite',
        type : 'aside',
        icon : 'heroicons_outline:cog-8-tooth',
        children: []
    },
    {
        id   : 'gestion-utilisateur',
        title: 'U.M',
        tooltip: 'User Management',
        type : 'aside',
        icon : 'heroicons_outline:user-plus',
        children: []
    },
];

export const futuristicNavigation: FuseNavigationItem[] = [

    {
        id   : 'homePage',
        title: 'Home Page',
        type : 'group',
        children:[]
    },
    {
        id   : 'gestionOperation',
        title: 'Analysis',
        type : 'group',
        children: []
    },
    {
        id   : 'traceabilitys',
        title: 'Traceability',
        type : 'group',
        children: []
    },
    {
        id   : 'gestion-service',
        title: 'Service management',
        type : 'group',
        children: []
    },

    {
        id   : 'donnees',
        title: 'Satellite',
        type : 'group',
        children: []
    },{
        id   : 'gestion-utilisateur',
        title: 'User Management',
        type : 'group',
        children: []
    },
];

export const horizontalNavigation: FuseNavigationItem[] = [

    {
        id   : 'fichier',
        title: 'Fichier',
        type : 'group',
        icon : 'mat_outline:assignment',
        children: []
    },
    {
        id   : 'dashboards',
        title: 'Dashboard',
        type : 'group',
        icon : 'mat_outline:assignment',
        children: []
    },
    {
        id   : 'bonMelanges',
        title: 'Bon De Mélange',
        type : 'group',
        icon : 'heroicons_outline:cog-8-tooth',
        children: []
    },
    {
        id   : 'configurations',
        title: 'Configurations',
        type : 'group',
        icon : 'heroicons_outline:cog-8-tooth',
        children: []
    },


];
