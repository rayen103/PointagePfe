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
                icon : 'mat_outline:business',
                link : '/fichier/societe',
                action:[FuseNavigationAction.View, FuseNavigationAction.Add, FuseNavigationAction.Edit, FuseNavigationAction.Delete, FuseNavigationAction.Preview, FuseNavigationAction.Print, FuseNavigationAction.Export, FuseNavigationAction.Search, FuseNavigationAction.Duplicate]
            },
            {
                id   : 'fichier.utilisateur',
                title: 'Utilisateur',
                type : 'basic',
                icon : 'mat_outline:group',
                link : '/fichier/utilisateur',
                action:[FuseNavigationAction.View, FuseNavigationAction.Add, FuseNavigationAction.Edit, FuseNavigationAction.Delete, FuseNavigationAction.Preview, FuseNavigationAction.Print, FuseNavigationAction.Export, FuseNavigationAction.Search, FuseNavigationAction.Duplicate]
            },
            {
                id   : 'fichier.employe',
                title: 'Employe',
                type : 'basic',
                icon : 'mat_outline:badge',
                link : '/fichier/employe',
                action:[FuseNavigationAction.View, FuseNavigationAction.Add, FuseNavigationAction.Edit, FuseNavigationAction.Delete, FuseNavigationAction.Preview, FuseNavigationAction.Print, FuseNavigationAction.Export, FuseNavigationAction.Search, FuseNavigationAction.Duplicate]
            },
            {
                id   : 'fichier.role-utilisateur',
                title: 'Role',
                type : 'basic',
                icon : 'mat_outline:manage_accounts',
                link : '/fichier/role-utilisateur',
                action:[FuseNavigationAction.View, FuseNavigationAction.Add, FuseNavigationAction.Edit, FuseNavigationAction.Delete, FuseNavigationAction.Preview, FuseNavigationAction.Print, FuseNavigationAction.Export, FuseNavigationAction.Search, FuseNavigationAction.Duplicate]
            },
            {
                id   : 'fichier.circuit',
                title: 'Circuit',
                type : 'basic',
                icon : 'mat_outline:alt_route',
                link : '/fichier/circuit',
                action:[FuseNavigationAction.View, FuseNavigationAction.Add, FuseNavigationAction.Edit, FuseNavigationAction.Delete, FuseNavigationAction.Preview, FuseNavigationAction.Print, FuseNavigationAction.Export, FuseNavigationAction.Search, FuseNavigationAction.Duplicate]
            },
            {
                id   : 'fichier.pointcollecte',
                title: 'Point de Collecte',
                type : 'basic',
                icon : 'mat_outline:location_on',
                link : '/fichier/pointcollecte',
                action:[FuseNavigationAction.View, FuseNavigationAction.Add, FuseNavigationAction.Edit, FuseNavigationAction.Delete, FuseNavigationAction.Preview, FuseNavigationAction.Print, FuseNavigationAction.Export, FuseNavigationAction.Search, FuseNavigationAction.Duplicate]
            },
            {
                id   : 'fichier.equipe',
                title: 'Equipe',
                type : 'basic',
                icon : 'mat_outline:groups',
                link : '/fichier/equipe',
                action:[FuseNavigationAction.View, FuseNavigationAction.Add, FuseNavigationAction.Edit, FuseNavigationAction.Delete, FuseNavigationAction.Preview, FuseNavigationAction.Print, FuseNavigationAction.Export, FuseNavigationAction.Search, FuseNavigationAction.Duplicate]
            },
            {
                id   : 'fichier.ordretravail',
                title: 'Ordre de Travail',
                type : 'basic',
                icon : 'mat_outline:assignment',
                link : '/fichier/ordretravail',
                action:[FuseNavigationAction.View, FuseNavigationAction.Add, FuseNavigationAction.Edit, FuseNavigationAction.Delete, FuseNavigationAction.Preview, FuseNavigationAction.Print, FuseNavigationAction.Export, FuseNavigationAction.Search, FuseNavigationAction.Duplicate]
            },
            {
                id   : 'fichier.rattachement',
                title: 'Rattachement',
                type : 'basic',
                icon : 'mat_outline:link',
                link : '/fichier/rattachement',
                action:[FuseNavigationAction.View, FuseNavigationAction.Add, FuseNavigationAction.Edit, FuseNavigationAction.Delete, FuseNavigationAction.Preview, FuseNavigationAction.Print, FuseNavigationAction.Export, FuseNavigationAction.Search, FuseNavigationAction.Duplicate]
            },
            {
                id   : 'fichier.shift',
                title: 'Shift',
                type : 'basic',
                icon : 'mat_outline:schedule',
                link : '/fichier/shift',
                action:[FuseNavigationAction.View, FuseNavigationAction.Add, FuseNavigationAction.Edit, FuseNavigationAction.Delete, FuseNavigationAction.Preview, FuseNavigationAction.Print, FuseNavigationAction.Export, FuseNavigationAction.Search, FuseNavigationAction.Duplicate]
            },
            {
                id   : 'fichier.bus',
                title: 'Bus',
                type : 'basic',
                icon : 'mat_outline:directions_bus',
                link : '/fichier/bus',
                action:[FuseNavigationAction.View, FuseNavigationAction.Add, FuseNavigationAction.Edit, FuseNavigationAction.Delete, FuseNavigationAction.Preview, FuseNavigationAction.Print, FuseNavigationAction.Export, FuseNavigationAction.Search, FuseNavigationAction.Duplicate]
            },
            {
                id   : 'fichier.region',
                title: 'Région',
                type : 'basic',
                icon : 'mat_outline:map',
                link : '/fichier/region',
                action:[FuseNavigationAction.View, FuseNavigationAction.Add, FuseNavigationAction.Edit, FuseNavigationAction.Delete, FuseNavigationAction.Preview, FuseNavigationAction.Print, FuseNavigationAction.Export, FuseNavigationAction.Search, FuseNavigationAction.Duplicate]
            },
            {
                id   : 'fichier.gouvernorat',
                title: 'Gouvernorat',
                type : 'basic',
                icon : 'mat_outline:location_city',
                link : '/fichier/gouvernorat',
                action:[FuseNavigationAction.View, FuseNavigationAction.Add, FuseNavigationAction.Edit, FuseNavigationAction.Delete, FuseNavigationAction.Preview, FuseNavigationAction.Print, FuseNavigationAction.Export, FuseNavigationAction.Search, FuseNavigationAction.Duplicate]
            },
            {
                id   : 'fichier.chauffeur',
                title: 'Chauffeur',
                type : 'basic',
                icon : 'mat_outline:person_pin',
                link : '/fichier/chauffeur',
                action:[FuseNavigationAction.View, FuseNavigationAction.Add, FuseNavigationAction.Edit, FuseNavigationAction.Delete, FuseNavigationAction.Preview, FuseNavigationAction.Print, FuseNavigationAction.Export, FuseNavigationAction.Search, FuseNavigationAction.Duplicate]
            },
            {
                id   : 'fichier.modem',
                title: 'Modem',
                type : 'basic',
                icon : 'mat_outline:router',
                link : '/fichier/modem',
                action:[FuseNavigationAction.View, FuseNavigationAction.Add, FuseNavigationAction.Edit, FuseNavigationAction.Delete, FuseNavigationAction.Preview, FuseNavigationAction.Print, FuseNavigationAction.Export, FuseNavigationAction.Search, FuseNavigationAction.Duplicate]
            },
            {
                id   : 'fichier.chantier',
                title: 'Chantier',
                type : 'basic',
                icon : 'mat_outline:construction',
                link : '/fichier/chantier',
                action:[FuseNavigationAction.View, FuseNavigationAction.Add, FuseNavigationAction.Edit, FuseNavigationAction.Delete, FuseNavigationAction.Preview, FuseNavigationAction.Print, FuseNavigationAction.Export, FuseNavigationAction.Search, FuseNavigationAction.Duplicate]
            },
        ]
    },

    {
        id: 'monitoring',
        title: 'Monitoring',
        type: 'group',
        icon: 'mat_outline:monitoring',
        children: [
            {
                id: 'monitoring.bus-tracking',
                title: 'Bus Tracking',
                type: 'basic',
                icon: 'mat_outline:location_searching',
                link: '/monitoring/bus-tracking',
                action: [
                    FuseNavigationAction.View,
                    FuseNavigationAction.Edit,
                    FuseNavigationAction.Search,
                    FuseNavigationAction.Export,
                    FuseNavigationAction.Preview,
                    FuseNavigationAction.Print,
                ],
            },
        ],
    },

    {
        id: 'analyse',
        title: 'Analyse BI',
        type: 'group',
        icon: 'mat_outline:analytics',
        children: [
            {
                id: 'analyse.bi-bus',
                title: 'Bus',
                type: 'basic',
                icon: 'mat_outline:directions_bus',
                link: '/analyse/bi-bus',
                action: [
                    FuseNavigationAction.View,
                    FuseNavigationAction.Search,
                    FuseNavigationAction.Export,
                    FuseNavigationAction.Preview,
                    FuseNavigationAction.Print,
                ],
            },
            {
                id: 'analyse.bi-employe',
                title: 'Employé',
                type: 'basic',
                icon: 'mat_outline:badge',
                link: '/analyse/bi-employe',
                action: [
                    FuseNavigationAction.View,
                    FuseNavigationAction.Search,
                    FuseNavigationAction.Export,
                    FuseNavigationAction.Preview,
                    FuseNavigationAction.Print,
                ],
            },
            {
                id: 'analyse.trace',
                title: 'Trace',
                type: 'basic',
                icon: 'mat_outline:history',
                link: '/analyse/trace',
                action: [
                    FuseNavigationAction.View,
                    FuseNavigationAction.Search,
                    FuseNavigationAction.Export,
                    FuseNavigationAction.Preview,
                    FuseNavigationAction.Print,
                ],
            },
        ],
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
        id   : 'monitoring',
        title: 'Monitoring',
        type : 'group',
        icon : 'mat_outline:monitoring',
        children: []
    },
    {
        id   : 'analyse',
        title: 'Anaalyse',
        type : 'group',
        icon : 'mat_outline:analytics',
        children: []
    }



];
