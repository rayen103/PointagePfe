import { ChangeDetectionStrategy, ChangeDetectorRef, Component, ViewEncapsulation } from '@angular/core';
import { CommonModule, DecimalPipe } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatSelectModule } from '@angular/material/select';
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
import { AnalyseApiService } from '../analyse/shared/analyse-api.service';
import { AvailableBusEtaPrediction, BusEtaPredictionResponse } from '../analyse/shared/analyse.model';
import { BusService } from '../../../core/bus/bus.service';
import { Bus, PagedBus } from '../../../core/bus/bus.model';

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

interface InsightCard {
    id: string;
    title: string;
    value: number;
    icon: string;
    color: string;
    progress: number;
    changeValue: number | null;
    changeLabel: string | null;
    isPositive: boolean;
}

interface OrderRow {
    id: string;
    title: string;
    detail: string;
    state: string;
    stateIcon: string;
    status: 'approved' | 'pending' | 'rejected' | 'system';
    statusLabel: string;
    date?: Date;
    link?: string;
}

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
    insights: InsightCard[];
    orders: OrderRow[];
    donut: DonutVm;
}

interface DonutVm {
    percent: number;
    dash: number;
    circ: number;
    segments: { label: string; value: number; color: string }[];
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
        MatCardModule,
        MatFormFieldModule,
        MatInputModule,
        MatSelectModule,
        ReactiveFormsModule,
        DecimalPipe,
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

    // ETA Prediction properties
    etaForm: FormGroup;
    etaResult: BusEtaPredictionResponse | null = null;
    availableEtaResults: AvailableBusEtaPrediction[] = [];
    isEtaLoading = false;
    isAvailableEtaLoading = false;
    buses$: Observable<Bus[]>;

    constructor(
        private _dashboardService: DashboardService,
        private _router: Router,
        private _changeDetectorRef: ChangeDetectorRef,
        private fb: FormBuilder,
        private analyseApiService: AnalyseApiService,
        private busService: BusService,
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

        // Initialize ETA form
        this.etaForm = this.fb.group({
            selectedBus: [null],
            Latitude: [null],
            Longitude: [null],
            CodeCircuit: [null],
            ModelBus: [null],
            Capacite: [null],
            CurrentOccupancy: [null],
            LastPositionAt: [null],
        });

        this.buses$ = this.busService.GetBuses().pipe(map((paged: PagedBus) => paged.buses));
        
        // Load available ETA predictions on init
        this.predictAvailableBusesEta();
    }

    refresh(): void {
        this._refresh$.next();
        this.predictAvailableBusesEta();
    }

    onBusSelect(bus: Bus | null): void {
        if (!bus) {
            this.etaForm.patchValue({
                Latitude: null,
                Longitude: null,
                CodeCircuit: null,
                ModelBus: null,
                Capacite: null,
                CurrentOccupancy: null,
                LastPositionAt: null,
            });
            return;
        }

        this.etaForm.patchValue({
            Latitude: bus.latitude,
            Longitude: bus.longitude,
            CodeCircuit: bus.codeCircuit,
            ModelBus: bus.modelBus,
            Capacite: bus.capacite,
            CurrentOccupancy: bus.currentOccupancy,
            LastPositionAt: bus.lastPositionAt,
        });
    }

    predictEta(): void {
        this.isEtaLoading = true;
        const rawValues = this.etaForm.getRawValue();
        this.analyseApiService.predictBusEta({
            Latitude: rawValues.Latitude,
            Longitude: rawValues.Longitude,
            CodeCircuit: rawValues.CodeCircuit,
            ModelBus: rawValues.ModelBus,
            Capacite: rawValues.Capacite,
            CurrentOccupancy: rawValues.CurrentOccupancy,
            LastPositionAt: rawValues.LastPositionAt,
        }).subscribe({
            next: (result) => {
                this.etaResult = result;
                this.isEtaLoading = false;
                this._changeDetectorRef.markForCheck();
            },
            error: () => {
                this.isEtaLoading = false;
                this._changeDetectorRef.markForCheck();
            },
        });
    }

    predictAvailableBusesEta(): void {
        this.isAvailableEtaLoading = true;
        this.analyseApiService.predictAvailableBusEta().subscribe({
            next: (result) => {
                this.availableEtaResults = result?.predictions ?? [];
                this.isAvailableEtaLoading = false;
                this._changeDetectorRef.markForCheck();
            },
            error: () => {
                this.availableEtaResults = [];
                this.isAvailableEtaLoading = false;
                this._changeDetectorRef.markForCheck();
            },
        });
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
            insights: this._buildInsights(data),
            orders: this._buildOrders(data),
            donut: this._buildDonut(data),
        };
    }

    private _buildDonut(data: DashboardData): DonutVm {
        const series = data.charts.doughnut?.series ?? [];
        const labels = data.charts.doughnut?.labels ?? [];
        const palette = ['#42abe0', '#d12b28', '#f2b33d', '#0e8a5f'];
        const total = series.reduce((a, b) => a + b, 0);
        const percent = total > 0 ? Math.round((series[0] / total) * 100) : 0;
        const r = 38;
        const circ = +(2 * Math.PI * r).toFixed(2);
        return {
            percent,
            circ,
            dash: +((percent / 100) * circ).toFixed(2),
            segments: labels.map((label, i) => ({
                label,
                value: series[i] ?? 0,
                color: palette[i % palette.length],
            })),
        };
    }

    private _buildInsights(data: DashboardData): InsightCard[] {
        const maxValue = Math.max(...data.kpis.map((k) => k.value), 1);
        return data.kpis.map((kpi) => ({
            id: kpi.id,
            title: kpi.title,
            value: kpi.value,
            icon: kpi.icon,
            color: kpi.color,
            progress: Math.min(100, Math.round((kpi.value / maxValue) * 100)),
            changeValue: kpi.change?.value ?? null,
            changeLabel: kpi.change?.label ?? null,
            isPositive: kpi.change?.isPositive ?? true,
        }));
    }

    private _buildOrders(data: DashboardData): OrderRow[] {
        const created: OrderRow[] = data.recentCreated.map((item) => ({
            id: item.id,
            title: item.title,
            detail: item.description ?? '—',
            state: 'Création',
            stateIcon: 'heroicons_outline:plus-circle',
            status: 'approved',
            statusLabel: 'Nouveau',
            date: item.date,
        }));
        const updated: OrderRow[] = data.recentUpdated.map((item) => ({
            id: item.id,
            title: item.title,
            detail: item.description ?? '—',
            state: 'Mise à jour',
            stateIcon: 'heroicons_outline:pencil-square',
            status: 'pending',
            statusLabel: 'Mis à jour',
            date: item.date,
        }));
        const system: OrderRow[] = data.systemActivity.map((item) => ({
            id: item.id,
            title: item.title,
            detail: item.description ?? '—',
            state: 'Système',
            stateIcon: 'heroicons_outline:cog-6-tooth',
            status: 'system',
            statusLabel: 'Système',
            date: item.date,
        }));
        return [...created, ...updated, ...system].slice(0, 6);
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
                height: '100%',
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
                height: '100%',
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
