import { ChangeDetectionStrategy, ChangeDetectorRef, Component, ViewEncapsulation } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { Router, RouterLink } from '@angular/router';
import {
    ApexAxisChartSeries,
    ApexChart,
    ApexDataLabels,
    ApexFill,
    ApexGrid,
    ApexLegend,
    ApexNonAxisChartSeries,
    ApexPlotOptions,
    ApexStroke,
    ApexTooltip,
    ApexXAxis,
    NgApexchartsModule,
} from 'ng-apexcharts';
import { BehaviorSubject, catchError, finalize, map, Observable, of, shareReplay, startWith, Subject, switchMap, tap } from 'rxjs';
import { DashboardAxisChart, DashboardData, DashboardPieChart, DashboardQuickAction } from './dashboard.models';
import { DashboardService } from './dashboard.service';

type AxisChartOptions = {
    series: ApexAxisChartSeries;
    chart: ApexChart;
    xaxis: ApexXAxis;
    dataLabels: ApexDataLabels;
    plotOptions?: ApexPlotOptions;
    grid: ApexGrid;
    colors: string[];
    legend: ApexLegend;
    tooltip: ApexTooltip;
    stroke?: ApexStroke;
    fill?: ApexFill;
};

type PieChartOptions = {
    series: ApexNonAxisChartSeries;
    chart: ApexChart;
    labels: string[];
    dataLabels: ApexDataLabels;
    legend: ApexLegend;
    colors: string[];
    tooltip: ApexTooltip;
    stroke?: ApexStroke;
};

interface DashboardViewModel {
    data: DashboardData;
    barChartOptions: AxisChartOptions;
    lineChartOptions: AxisChartOptions;
    pieChartOptions: PieChartOptions;
    doughnutChartOptions: PieChartOptions;
    hasBarData: boolean;
    hasLineData: boolean;
    hasPieData: boolean;
    hasDoughnutData: boolean;
}

@Component({
    selector: 'app-accueil',
    standalone: true,
    imports: [
        CommonModule,
        RouterLink,
        MatButtonModule,
        MatIconModule,
        MatProgressBarModule,
        NgApexchartsModule,
    ],
    templateUrl: './accueil.component.html',
    styleUrl: './accueil.component.scss',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AccueilComponent {

    private readonly CHART_COLORS = {
        bar: ['#6366f1'],
        line: ['#0ea5e9', '#f97316'],
        pie: ['#22c55e', '#f97316', '#6366f1', '#14b8a6'],
        doughnut: ['#10b981', '#ef4444'],
    };

    readonly isLoading$ = new BehaviorSubject<boolean>(true);
    readonly quickActions: DashboardQuickAction[] = [
        {
            id: 'create-bus',
            title: 'Créer un bus',
            description: 'Ajouter un véhicule à la flotte',
            icon: 'mat_outline:directions_bus',
            link: '/fichier/bus/ajouter',
        },
        {
            id: 'create-circuit',
            title: 'Créer un circuit',
            description: 'Définir un nouveau trajet',
            icon: 'mat_outline:alt_route',
            link: '/fichier/circuit/ajouter',
        },
        {
            id: 'create-ordre',
            title: 'Nouvel ordre',
            description: 'Planifier une intervention',
            icon: 'mat_outline:assignment',
            link: '/fichier/ordretravail/ajouter',
        },
        {
            id: 'manage-users',
            title: 'Gérer les utilisateurs',
            description: 'Accès et rôles',
            icon: 'mat_outline:group',
            link: '/fichier/utilisateur',
        },
        {
            id: 'manage-employes',
            title: 'Gérer les employés',
            description: 'Effectifs et équipes',
            icon: 'mat_outline:badge',
            link: '/fichier/employe',
        },
        {
            id: 'manage-rattachements',
            title: 'Rattachements',
            description: 'Suivi des rattachements',
            icon: 'mat_outline:link',
            link: '/fichier/rattachement',
        },
    ];

    readonly viewModel$: Observable<DashboardViewModel>;

    private readonly _refresh$ = new Subject<void>();

    constructor(
        private _dashboardService: DashboardService,
        private _router: Router,
        private _changeDetectorRef: ChangeDetectorRef,
    ) {
        this.viewModel$ = this._refresh$.pipe(
            startWith(void 0),
            tap(() => this.isLoading$.next(true)),
            switchMap(() =>
                this._dashboardService.getDashboardData().pipe(
                    map((data) => this._buildViewModel(data)),
                    catchError(() => of(this._buildViewModel(this._buildFallbackData()))),
                    finalize(() => this.isLoading$.next(false))
                )
            ),
            shareReplay({ bufferSize: 1, refCount: true })
        );
    }

    refresh(): void {
        this._refresh$.next();
    }

    private _buildViewModel(data: DashboardData): DashboardViewModel {
        const barChartOptions = this._buildAxisChartOptions(data.charts.bar, 'bar', this.CHART_COLORS.bar);
        const lineChartOptions = this._buildAxisChartOptions(data.charts.line, 'line', this.CHART_COLORS.line);
        const pieChartOptions = this._buildPieChartOptions(data.charts.pie, 'pie', this.CHART_COLORS.pie);
        const doughnutChartOptions = this._buildPieChartOptions(
            data.charts.doughnut,
            'donut',
            this.CHART_COLORS.doughnut
        );

        return {
            data,
            barChartOptions,
            lineChartOptions,
            pieChartOptions,
            doughnutChartOptions,
            hasBarData: this._hasAxisData(data.charts.bar),
            hasLineData: this._hasAxisData(data.charts.line),
            hasPieData: this._hasPieData(data.charts.pie),
            hasDoughnutData: this._hasPieData(data.charts.doughnut),
        };
    }

    private _hasAxisData(chart: DashboardAxisChart): boolean {
        return chart.series.some((series) => series.data.some((value) => value > 0));
    }

    private _hasPieData(chart: DashboardPieChart): boolean {
        return chart.series.some((value) => value > 0);
    }

    private _buildAxisChartOptions(
        chart: DashboardAxisChart,
        type: 'bar' | 'line',
        colors: string[]
    ): AxisChartOptions {
        return {
            series: chart.series,
            chart: {
                type,
                height: 280,
                toolbar: { show: false },
                sparkline: { enabled: false },
            },
            xaxis: {
                categories: chart.labels,
                labels: {
                    style: {
                        colors: '#94a3b8',
                        fontSize: '12px',
                    },
                },
            },
            dataLabels: { enabled: false },
            plotOptions:
                type === 'bar'
                    ? {
                          bar: {
                              columnWidth: '48%',
                              borderRadius: 6,
                          },
                      }
                    : undefined,
            grid: {
                borderColor: 'rgba(148, 163, 184, 0.2)',
                strokeDashArray: 4,
            },
            colors,
            legend: {
                show: type === 'line',
                position: 'top',
                horizontalAlign: 'left',
                labels: { colors: '#94a3b8' },
            },
            tooltip: { theme: 'light' },
            stroke:
                type === 'line'
                    ? {
                          curve: 'smooth',
                          width: 3,
                      }
                    : undefined,
            fill:
                type === 'line'
                    ? {
                          type: 'solid',
                          opacity: 0.15,
                      }
                    : undefined,
        };
    }

    private _buildPieChartOptions(
        chart: DashboardPieChart,
        type: 'pie' | 'donut',
        colors: string[]
    ): PieChartOptions {
        return {
            series: chart.series,
            chart: {
                type,
                height: 280,
                toolbar: { show: false },
            },
            labels: chart.labels,
            dataLabels: { enabled: false },
            legend: {
                position: 'bottom',
                labels: { colors: '#94a3b8' },
            },
            colors,
            tooltip: { theme: 'light' },
            stroke: {
                width: 2,
            },
        };
    }

    private _buildFallbackData(): DashboardData {
        return {
            kpis: [],
            aiFeatures: [],
            charts: {
                bar: { labels: [], series: [] },
                line: { labels: [], series: [] },
                pie: { labels: [], series: [] },
                doughnut: { labels: [], series: [] },
            },
            recentCreated: [],
            recentUpdated: [],
            systemActivity: [],
            lastUpdated: new Date(),
            errorMessage: 'Impossible de charger les données du tableau de bord. Veuillez réessayer.',
            buses: [],
            employes: [],
            chauffeurs: [],
            circuits: [],
            utilisateurs: [],
            chantiers: [],
            equipes: [],
            pointsCollecte: [],
            ordresTravail: [],
            rattachements: [],
        };
    }
}
