import { Injectable } from '@angular/core';
import { catchError, forkJoin, map, Observable, of } from 'rxjs';
import { BusService } from 'app/core/bus/bus.service';
import { CircuitService } from 'app/core/circuit/circuit.service';
import { EmployeService } from 'app/core/employes/employe.service';
import { OrdreTravailService } from 'app/core/ordre-travail/ordre-travail.service';
import { RattachementService } from 'app/core/rattachement/rattachement.service';
import { UtilisateurService } from 'app/core/utilisateurs/utilisateur.service';
import { PagedBus } from 'app/core/bus/bus.model';
import { PagedCircuit } from 'app/core/circuit/circuit.model';
import { Employe, PagedEmploye } from 'app/core/employes/employe.model';
import { PagedOrdreTravail } from 'app/core/ordre-travail/ordre-travail.model';
import { PagedRattachement } from 'app/core/rattachement/rattachement.model';
import { PagedUtilisateur } from 'app/core/utilisateurs/utilisateur.model';
import {
    DashboardActivityItem,
    DashboardAxisChart,
    DashboardAxisSeries,
    DashboardCharts,
    DashboardData,
    DashboardKpi,
    DashboardPieChart,
} from './dashboard.models';

@Injectable({
    providedIn: 'root',
})
export class DashboardService {
    constructor(
        private _utilisateurService: UtilisateurService,
        private _employeService: EmployeService,
        private _busService: BusService,
        private _circuitService: CircuitService,
        private _ordreTravailService: OrdreTravailService,
        private _rattachementService: RattachementService
    ) {}

    getDashboardData(): Observable<DashboardData> {
        return forkJoin({
            utilisateurs: this._utilisateurService.GetUtilisateur(1, 1),
            employes: this._employeService.GetEmploye(1, 1000),
            buses: this._busService.GetBuses(1, 1000),
            circuits: this._circuitService.GetCircuit(1, 1000),
            ordresTravail: this._ordreTravailService.GetOrdresTravail(1, 200),
            rattachements: this._rattachementService.GetRattachements(1, 200),
        }).pipe(
            map((payload) => this._buildDashboardData(payload)),
            catchError(() =>
                of(
                    this._buildEmptyDashboardData(
                        'Impossible de charger les données du tableau de bord. Veuillez réessayer.'
                    )
                )
            )
        );
    }

    private _buildDashboardData(payload: {
        utilisateurs: PagedUtilisateur;
        employes: PagedEmploye;
        buses: PagedBus;
        circuits: PagedCircuit;
        ordresTravail: PagedOrdreTravail;
        rattachements: PagedRattachement;
    }): DashboardData {
        const totalUsers = payload.utilisateurs?.length ?? 0;
        const totalEmployees = payload.employes?.total ?? 0;
        const totalBuses = payload.buses?.totalCount ?? 0;
        const totalCircuits = payload.circuits?.totalCount ?? 0;
        const totalOrdresTravail = payload.ordresTravail?.totalCount ?? 0;
        const totalRattachements = payload.rattachements?.totalCount ?? 0;

        const buses = payload.buses?.buses ?? [];
        const circuits = payload.circuits?.circuits ?? [];
        const employes = payload.employes?.employes ?? [];
        const ordresTravail = payload.ordresTravail?.ordresTravail ?? [];
        const rattachements = payload.rattachements?.rattachements ?? [];

        const activeBuses = buses.filter((bus) => bus.isActive).length;
        const inactiveBuses = buses.length - activeBuses;
        const activeCircuits = circuits.filter((circuit) => circuit.isActive).length;
        const inactiveCircuits = circuits.length - activeCircuits;
        const activeEntities = activeBuses + activeCircuits;
        const inactiveEntities = inactiveBuses + inactiveCircuits;

        const busActiveRate = this._percentage(activeBuses, buses.length);
        const circuitActiveRate = this._percentage(activeCircuits, circuits.length);

        const kpis: DashboardKpi[] = [
            {
                id: 'users',
                title: 'Total utilisateurs',
                value: totalUsers,
                icon: 'mat_outline:group',
                color: '#2563eb',
            },
            {
                id: 'employees',
                title: 'Total employés',
                value: totalEmployees,
                icon: 'mat_outline:badge',
                color: '#0f766e',
            },
            {
                id: 'buses',
                title: 'Total bus',
                value: totalBuses,
                icon: 'mat_outline:directions_bus',
                color: '#7c3aed',
                change: buses.length
                    ? {
                          value: busActiveRate,
                          label: 'Actifs',
                          isPositive: busActiveRate >= 80,
                      }
                    : undefined,
            },
            {
                id: 'circuits',
                title: 'Total circuits',
                value: totalCircuits,
                icon: 'mat_outline:alt_route',
                color: '#f97316',
                change: circuits.length
                    ? {
                          value: circuitActiveRate,
                          label: 'Actifs',
                          isPositive: circuitActiveRate >= 80,
                      }
                    : undefined,
            },
            {
                id: 'work-orders',
                title: 'Ordres de travail',
                value: totalOrdresTravail,
                icon: 'mat_outline:assignment',
                color: '#10b981',
            },
            {
                id: 'rattachements',
                title: 'Rattachements',
                value: totalRattachements,
                icon: 'mat_outline:link',
                color: '#e11d48',
            },
        ];

        const barChart: DashboardAxisChart = {
            labels: ['Utilisateurs', 'Employés', 'Bus', 'Circuits', 'Ordres', 'Rattachements'],
            series: [
                {
                    name: 'Total',
                    data: [
                        totalUsers,
                        totalEmployees,
                        totalBuses,
                        totalCircuits,
                        totalOrdresTravail,
                        totalRattachements,
                    ],
                },
            ],
        };

        const pieChart = this._buildEmployeDistribution(employes);
        const doughnutChart: DashboardPieChart = {
            labels: ['Actifs', 'Inactifs'],
            series: [activeEntities, inactiveEntities],
        };

        const monthBuckets = this._buildMonthBuckets(6);
        const lineChart: DashboardAxisChart = {
            labels: monthBuckets.map((bucket) => bucket.label),
            series: [
                this._buildMonthlySeries('Ordres', ordresTravail.map((item) => item.dateCreation), monthBuckets),
                this._buildMonthlySeries(
                    'Rattachements',
                    rattachements.map((item) => item.dateRattachement),
                    monthBuckets
                ),
            ],
        };

        const recentCreated = this._buildRecentActivities(
            ordresTravail,
            (item) => item.dateCreation,
            (item, date, index) => ({
                id: item.ordreTravailId ?? `ordre-${index}`,
                title: `Ordre ${item.numeroOrdreTravail}`,
                description: item.libelle || item.numeroChantier || 'Nouvelle intervention',
                date,
                icon: 'mat_outline:assignment',
                type: 'created',
            })
        );

        const recentUpdated = this._buildRecentActivities(
            rattachements,
            (item) => item.dateCloture ?? item.dateRattachement,
            (item, date, index) => ({
                id: item.rattachementId ?? `rattachement-${index}`,
                title: `Rattachement ${item.numeroRattachement}`,
                description: item.status || item.type || 'Mise à jour récente',
                date,
                icon: 'mat_outline:link',
                type: 'updated',
            })
        );

        const systemActivity: DashboardActivityItem[] = [
            {
                id: 'bus-availability',
                title: `${activeBuses} bus actifs sur ${totalBuses}`,
                description: 'Disponibilité de la flotte',
                date: new Date(),
                icon: 'mat_outline:directions_bus',
                type: 'system',
            },
            {
                id: 'circuit-availability',
                title: `${activeCircuits} circuits actifs sur ${totalCircuits}`,
                description: 'Trajets opérationnels',
                date: new Date(),
                icon: 'mat_outline:alt_route',
                type: 'system',
            },
            {
                id: 'user-coverage',
                title: `${totalUsers} utilisateurs enregistrés`,
                description: 'Base utilisateurs',
                date: new Date(),
                icon: 'mat_outline:group',
                type: 'system',
            },
        ];

        const charts: DashboardCharts = {
            bar: barChart,
            line: lineChart,
            pie: pieChart,
            doughnut: doughnutChart,
        };

        return {
            kpis,
            charts,
            recentCreated,
            recentUpdated,
            systemActivity,
            lastUpdated: new Date(),
        };
    }

    private _buildEmployeDistribution(employes: Employe[]): DashboardPieChart {
        if (!employes.length) {
            return { labels: [], series: [] };
        }

        const counts = employes.reduce<Record<string, number>>((acc, employe) => {
            const key = employe.typeEmploye ?? 'Autres';
            acc[key] = (acc[key] ?? 0) + 1;
            return acc;
        }, {});

        const labels = Object.keys(counts);
        const series = labels.map((label) => counts[label]);

        return {
            labels,
            series,
        };
    }

    private _buildMonthlySeries(
        label: string,
        dates: Array<Date | string | null | undefined>,
        buckets: Array<{ key: string; label: string }>
    ): DashboardAxisSeries {
        const bucketMap = new Map<string, number>();
        buckets.forEach((bucket) => bucketMap.set(bucket.key, 0));

        dates.forEach((value) => {
            const date = this._normalizeDate(value);
            if (!date) {
                return;
            }

            const key = `${date.getFullYear()}-${String(date.getMonth() + 1).padStart(2, '0')}`;
            if (bucketMap.has(key)) {
                bucketMap.set(key, (bucketMap.get(key) ?? 0) + 1);
            }
        });

        return {
            name: label,
            data: buckets.map((bucket) => bucketMap.get(bucket.key) ?? 0),
        };
    }

    private _buildMonthBuckets(monthCount: number): Array<{ key: string; label: string }> {
        const buckets: Array<{ key: string; label: string }> = [];
        const now = new Date();

        for (let offset = monthCount - 1; offset >= 0; offset -= 1) {
            const date = new Date(now.getFullYear(), now.getMonth() - offset, 1);
            const key = `${date.getFullYear()}-${String(date.getMonth() + 1).padStart(2, '0')}`;
            const label = date.toLocaleString('fr-FR', { month: 'short', year: 'numeric' });
            buckets.push({ key, label });
        }

        return buckets;
    }

    private _buildRecentActivities<T>(
        items: T[],
        getDate: (item: T) => Date | string | null | undefined,
        mapItem: (item: T, date: Date, index: number) => DashboardActivityItem,
        limit: number = 5
    ): DashboardActivityItem[] {
        return items
            .map((item, index) => ({
                item,
                index,
                date: this._normalizeDate(getDate(item)),
            }))
            .filter((entry) => entry.date)
            .sort((a, b) => (b.date?.getTime() ?? 0) - (a.date?.getTime() ?? 0))
            .slice(0, limit)
            .map((entry) => mapItem(entry.item, entry.date as Date, entry.index));
    }

    private _normalizeDate(value: Date | string | null | undefined): Date | null {
        if (!value) {
            return null;
        }

        if (value instanceof Date) {
            return isNaN(value.getTime()) ? null : value;
        }

        const date = new Date(value);
        return isNaN(date.getTime()) ? null : date;
    }

    private _percentage(part: number, total: number): number {
        if (!total) {
            return 0;
        }

        return Math.round((part / total) * 100);
    }

    private _buildEmptyDashboardData(errorMessage?: string): DashboardData {
        return {
            kpis: [],
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
            errorMessage,
        };
    }
}
