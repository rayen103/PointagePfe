import { Routes } from '@angular/router';

export default [
    {
        path: 'bi-bus',
        data: { navigationId: 'analyse.bi-bus' },
        loadComponent: () =>
            import('./bi-bus/analyse-bi-bus.component').then((m) => m.AnalyseBiBusComponent),
        title: 'Analyse BI Bus',
    },
    {
        path: 'bi-employe',
        data: { navigationId: 'analyse.bi-employe' },
        loadComponent: () =>
            import('./bi-employe/analyse-bi-employe.component').then((m) => m.AnalyseBiEmployeComponent),
        title: 'Analyse BI Employé',
    },
    {
        path: 'trace',
        data: { navigationId: 'analyse.trace' },
        loadComponent: () =>
            import('./trace/analyse-trace.component').then((m) => m.AnalyseTraceComponent),
        title: 'Analyse Trace',
    },
] as Routes;

