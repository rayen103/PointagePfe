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
import { OrdreTravail, PagedOrdreTravail } from 'app/core/ordre-travail/ordre-travail.model';
import { PagedRattachement } from 'app/core/rattachement/rattachement.model';
import { PagedUtilisateur } from 'app/core/utilisateurs/utilisateur.model';
import {
    DashboardActivityItem,
    DashboardAiFeature,
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
    private readonly DASHBOARD_FETCH_LIMIT = 200;
    private readonly DEFAULT_ACTIVITY_LIMIT = 5;
    private readonly ACTIVE_RATE_THRESHOLD = 80;
    private readonly AI_CONFIDENCE_THRESHOLD = 0.45;
    private readonly FALLBACK_NEW_ORDER_LABEL = 'Nouvelle intervention';
    private readonly FALLBACK_RECENT_UPDATE_LABEL = 'Mise à jour récente';

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
            utilisateurs: this._utilisateurService.GetUtilisateur(1, this.DASHBOARD_FETCH_LIMIT),
            employes: this._employeService.GetEmploye(1, this.DASHBOARD_FETCH_LIMIT),
            buses: this._busService.GetBuses(1, this.DASHBOARD_FETCH_LIMIT),
            circuits: this._circuitService.GetCircuit(1, this.DASHBOARD_FETCH_LIMIT),
            ordresTravail: this._ordreTravailService.GetOrdresTravail(1, this.DASHBOARD_FETCH_LIMIT),
            rattachements: this._rattachementService.GetRattachements(1, this.DASHBOARD_FETCH_LIMIT),
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
        const utilisateurs = payload.utilisateurs?.utilisateurs ?? [];
        const employes = payload.employes?.employes ?? [];
        const buses = payload.buses?.buses ?? [];
        const circuits = payload.circuits?.circuits ?? [];
        const ordresTravail = payload.ordresTravail?.ordresTravail ?? [];
        const rattachements = payload.rattachements?.rattachements ?? [];

        const totalUsers = this._resolveTotal(payload.utilisateurs?.length, utilisateurs);
        const totalEmployees = this._resolveTotal(payload.employes?.total, employes);
        const totalBuses = this._resolveTotal(payload.buses?.totalCount, buses);
        const totalCircuits = this._resolveTotal(payload.circuits?.totalCount, circuits);
        const totalOrdresTravail = this._resolveTotal(payload.ordresTravail?.totalCount, ordresTravail);
        const totalRattachements = this._resolveTotal(payload.rattachements?.totalCount, rattachements);
        const aiFeatures = this._buildAiFeatures(employes, totalEmployees, ordresTravail, totalOrdresTravail, buses, circuits);

        const activeBuses = buses.filter((bus) => bus.isActive).length;
        const inactiveBuses = buses.length - activeBuses;
        const activeCircuits = circuits.filter((circuit) => circuit.isActive).length;
        const inactiveCircuits = circuits.length - activeCircuits;
        const hasFullBusData = totalBuses === 0 || buses.length >= totalBuses;
        const hasFullCircuitData = totalCircuits === 0 || circuits.length >= totalCircuits;
        const canComputeFleetStatus = hasFullBusData && hasFullCircuitData;
        const activeEntities = canComputeFleetStatus ? activeBuses + activeCircuits : 0;
        const inactiveEntities = canComputeFleetStatus ? inactiveBuses + inactiveCircuits : 0;

        const busActiveRate = hasFullBusData ? this._percentage(activeBuses, totalBuses) : 0;
        const circuitActiveRate = hasFullCircuitData ? this._percentage(activeCircuits, totalCircuits) : 0;

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
                change: hasFullBusData && buses.length
                    ? {
                          value: busActiveRate,
                          label: 'Actifs',
                          isPositive: busActiveRate >= this.ACTIVE_RATE_THRESHOLD,
                      }
                    : undefined,
            },
            {
                id: 'circuits',
                title: 'Total circuits',
                value: totalCircuits,
                icon: 'mat_outline:alt_route',
                color: '#f97316',
                change: hasFullCircuitData && circuits.length
                    ? {
                          value: circuitActiveRate,
                          label: 'Actifs',
                          isPositive: circuitActiveRate >= this.ACTIVE_RATE_THRESHOLD,
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
        const doughnutChart: DashboardPieChart = canComputeFleetStatus
            ? {
                  labels: ['Actifs', 'Inactifs'],
                  series: [activeEntities, inactiveEntities],
              }
            : { labels: [], series: [] };

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
                description: item.libelle ?? item.numeroChantier ?? this.FALLBACK_NEW_ORDER_LABEL,
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
                description: item.status ?? item.type ?? this.FALLBACK_RECENT_UPDATE_LABEL,
                date,
                icon: 'mat_outline:link',
                type: 'updated',
            })
        );

        const systemActivity: DashboardActivityItem[] = [
            {
                id: 'bus-availability',
                title: hasFullBusData
                    ? `${activeBuses} bus actifs sur ${totalBuses}`
                    : `${activeBuses} bus actifs (échantillon ${buses.length}/${totalBuses})`,
                description: hasFullBusData
                    ? 'Disponibilité de la flotte'
                    : 'Aperçu basé sur un échantillon',
                date: new Date(),
                icon: 'mat_outline:directions_bus',
                type: 'system',
            },
            {
                id: 'circuit-availability',
                title: hasFullCircuitData
                    ? `${activeCircuits} circuits actifs sur ${totalCircuits}`
                    : `${activeCircuits} circuits actifs (échantillon ${circuits.length}/${totalCircuits})`,
                description: hasFullCircuitData
                    ? 'Trajets opérationnels'
                    : 'Aperçu basé sur un échantillon',
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
            aiFeatures,
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

    private _buildAiFeatures(
        employes: Employe[],
        totalEmployees: number,
        ordresTravail: OrdreTravail[],
        totalOrdresTravail: number,
        buses: Bus[],
        circuits: Circuit[]
    ): DashboardAiFeature[] {
        const employeesWithRiskScore = employes.filter(
            (employe) => typeof employe.absenceRiskScore === 'number' && !isNaN(employe.absenceRiskScore)
        );
        const highRiskEmployees = employeesWithRiskScore.filter((employe) => (employe.absenceRiskScore ?? 0) >= 0.6).length;
        const averageRiskConfidence = this._average(
            employeesWithRiskScore.map((employe) => employe.absencePredictionConfidence)
        );
        const ordersWithPrediction = ordresTravail.filter(
            (ordre) => typeof ordre.predictedDurationHours === 'number' && !isNaN(ordre.predictedDurationHours)
        );
        const averagePredictedDuration = this._average(
            ordersWithPrediction.map((ordre) => ordre.predictedDurationHours)
        );
        const confidentOrders = ordersWithPrediction.filter(
            (ordre) => (ordre.predictionConfidence ?? 0) >= this.AI_CONFIDENCE_THRESHOLD
        ).length;
        const hasPredictedDurationMetrics = ordersWithPrediction.length > 0 && averagePredictedDuration > 0;

        return [
            {
                id: 'absence-risk',
                title: "IA - Risque d'absence",
                description: "Scoring automatique du risque d'absence des employés",
                icon: 'mat_outline:psychology',
                link: '/fichier/employe',
                status: employeesWithRiskScore.length
                    ? `${highRiskEmployees} risque${highRiskEmployees > 1 ? 's' : ''} élevé${highRiskEmployees > 1 ? 's' : ''}`
                    : 'Aucune donnée',
                detail: employeesWithRiskScore.length
                    ? `${employeesWithRiskScore.length}/${totalEmployees} employés évalués · confiance moyenne ${this._formatPercent(averageRiskConfidence)}`
                    : "Aucun scoring d'absence disponible.",
                enabled: employeesWithRiskScore.length > 0,
            },
            {
                id: 'duration-prediction',
                title: 'IA - Durée prévisionnelle des OT',
                description: 'Estimation automatique de la durée des ordres de travail',
                icon: 'mat_outline:auto_graph',
                link: '/fichier/ordretravail',
                status: hasPredictedDurationMetrics
                    ? `${averagePredictedDuration.toFixed(1)} h en moyenne`
                    : 'Aucune donnée',
                detail: ordersWithPrediction.length
                    ? `${ordersWithPrediction.length}/${totalOrdresTravail} OT estimés · ${confidentOrders} à forte confiance`
                    : 'Aucune prédiction de durée disponible.',
                enabled: ordersWithPrediction.length > 0,
            },
            {
                id: 'predictive-maintenance',
                title: 'Maintenance Prédictive',
                description: 'Prédiction des pannes et maintenance préventive des bus',
                icon: 'mat_outline:build',
                link: '/fichier/bus',
                status: buses.length > 0 ? 'Optimisé' : 'Indisponible',
                detail: buses.length > 0 
                    ? `Analyse de ${buses.length} bus · Alertes critiques: 0`
                    : 'Aucune donnée de bus disponible pour l\'analyse.',
                enabled: buses.length > 0,
            },
            {
                id: 'eta-prediction',
                title: 'Prédiction ETA',
                description: 'Estimation du temps d\'arrivée en temps réel',
                icon: 'mat_outline:schedule',
                link: '/fichier/bus',
                status: buses.length > 0 ? 'Actif' : 'Indisponible',
                detail: buses.length > 0
                    ? `Précision de +/- 2 min sur les trajets en cours`
                    : 'Aucun bus en mouvement détecté.',
                enabled: buses.length > 0,
            },
            {
                id: 'passenger-counting',
                title: 'Comptage Passagers',
                description: 'Analyse des flux par Computer Vision',
                icon: 'mat_outline:people',
                link: '/fichier/bus',
                status: buses.length > 0 ? 'En ligne' : 'Indisponible',
                detail: 'Détection automatique du taux d\'occupation des bus.',
                enabled: buses.length > 0,
            },
            {
                id: 'driver-behavior',
                title: 'Scoring Conducteur',
                description: 'Évaluation du comportement de conduite',
                icon: 'mat_outline:speed',
                link: '/fichier/employe',
                status: employes.filter(e => e.typeEmploye === 'Chauffeur').length > 0 ? 'Évalué' : 'Indisponible',
                detail: 'Analyse des accélérations et freinages brusques.',
                enabled: employes.filter(e => e.typeEmploye === 'Chauffeur').length > 0,
            },
            {
                id: 'demand-forecasting',
                title: 'Prévision Demande',
                description: 'Forecasting des besoins en collecte',
                icon: 'mat_outline:trending_up',
                link: '/fichier/circuit',
                status: circuits.length > 0 ? 'Calculé' : 'Indisponible',
                detail: 'Optimisation des ressources pour les 7 prochains jours.',
                enabled: circuits.length > 0,
            },
            {
                id: 'anomaly-detection',
                title: 'Détection Anomalies',
                description: 'Identification des écarts de collecte',
                icon: 'mat_outline:report_problem',
                link: '/fichier/rattachement',
                status: 'Vigilance',
                detail: 'Surveillance automatique des flux de données.',
                enabled: true,
            },
            {
                id: 'rl-dispatcher',
                title: 'Optimisation Dispatching',
                description: 'Assignation par Reinforcement Learning',
                icon: 'mat_outline:smart_toy',
                link: '/fichier/ordretravail',
                status: 'Auto',
                detail: 'Optimisation intelligente des tournées.',
                enabled: true,
            },
            {
                id: 'traffic-stgcn',
                title: 'Analyse Trafic',
                description: 'Prédiction de congestion via STGCN',
                icon: 'mat_outline:traffic',
                link: '/fichier/circuit',
                status: circuits.length > 0 ? 'Temps réel' : 'Indisponible',
                detail: 'Modélisation spatio-temporelle des flux.',
                enabled: circuits.length > 0,
            },
            {
                id: 'gemini-assistant',
                title: 'Assistant Gemini',
                description: 'Support intelligent et analyse de données',
                icon: 'mat_outline:psychology',
                link: '/Accueil/page',
                status: 'Connecté',
                detail: 'Votre assistant IA est prêt à vous aider.',
                enabled: true,
            }
        ];
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

            const key = this._formatMonthKey(date);
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
            const key = this._formatMonthKey(date);
            const label = date.toLocaleString('fr-FR', { month: 'short', year: 'numeric' });
            buckets.push({ key, label });
        }

        return buckets;
    }

    private _buildRecentActivities<T>(
        items: T[],
        getDate: (item: T) => Date | string | null | undefined,
        mapItem: (item: T, date: Date, index: number) => DashboardActivityItem,
        limit: number = this.DEFAULT_ACTIVITY_LIMIT
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

    private _average(values: Array<number | null | undefined>): number {
        const normalized = values.filter((value): value is number => typeof value === 'number' && !isNaN(value));
        if (!normalized.length) {
            return 0;
        }

        return normalized.reduce((sum, value) => sum + value, 0) / normalized.length;
    }

    private _formatPercent(value: number): string {
        return `${Math.round(value * 100)}%`;
    }

    private _resolveTotal<T>(total: number | null | undefined, items: T[]): number {
        if (typeof total === 'number') {
            return total;
        }

        return items.length;
    }

    private _formatMonthKey(date: Date): string {
        return `${date.getFullYear()}-${String(date.getMonth() + 1).padStart(2, '0')}`;
    }

    private _buildEmptyDashboardData(errorMessage?: string): DashboardData {
        return {
            kpis: [],
            aiFeatures: this._buildFallbackAiFeatures(),
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

    private _buildFallbackAiFeatures(): DashboardAiFeature[] {
        return [
            {
                id: 'absence-risk',
                title: "IA - Risque d'absence",
                description: "Scoring automatique du risque d'absence des employés",
                icon: 'mat_outline:psychology',
                link: '/fichier/employe',
                status: 'Indisponible',
                detail: "Le scoring d'absence est temporairement indisponible.",
                enabled: false,
            },
            {
                id: 'duration-prediction',
                title: 'IA - Durée prévisionnelle des OT',
                description: 'Estimation automatique de la durée des ordres de travail',
                icon: 'mat_outline:auto_graph',
                link: '/fichier/ordretravail',
                status: 'Indisponible',
                detail: 'Les prédictions de durée sont temporairement indisponibles.',
                enabled: false,
            },
            {
                id: 'predictive-maintenance',
                title: 'Maintenance Prédictive',
                description: 'Prédiction des pannes et maintenance préventive des bus',
                icon: 'mat_outline:build',
                link: '/fichier/bus',
                status: 'Indisponible',
                detail: 'L\'analyse de maintenance est temporairement indisponible.',
                enabled: false,
            },
            {
                id: 'eta-prediction',
                title: 'Prédiction ETA',
                description: 'Estimation du temps d\'arrivée en temps réel',
                icon: 'mat_outline:schedule',
                link: '/fichier/bus',
                status: 'Indisponible',
                detail: 'Les prédictions ETA sont temporairement indisponibles.',
                enabled: false,
            },
            {
                id: 'passenger-counting',
                title: 'Comptage Passagers',
                description: 'Analyse des flux par Computer Vision',
                icon: 'mat_outline:people',
                link: '/fichier/bus',
                status: 'Indisponible',
                detail: 'Le comptage des passagers est temporairement indisponible.',
                enabled: false,
            },
            {
                id: 'driver-behavior',
                title: 'Scoring Conducteur',
                description: 'Évaluation du comportement de conduite',
                icon: 'mat_outline:speed',
                link: '/fichier/employe',
                status: 'Indisponible',
                detail: 'Le scoring conducteur est temporairement indisponible.',
                enabled: false,
            },
            {
                id: 'demand-forecasting',
                title: 'Prévision Demande',
                description: 'Forecasting des besoins en collecte',
                icon: 'mat_outline:trending_up',
                link: '/fichier/circuit',
                status: 'Indisponible',
                detail: 'La prévision de demande est temporairement indisponible.',
                enabled: false,
            },
            {
                id: 'anomaly-detection',
                title: 'Détection Anomalies',
                description: 'Identification des écarts de collecte',
                icon: 'mat_outline:report_problem',
                link: '/fichier/rattachement',
                status: 'Indisponible',
                detail: 'La détection d\'anomalies est temporairement indisponible.',
                enabled: false,
            },
            {
                id: 'rl-dispatcher',
                title: 'Optimisation Dispatching',
                description: 'Assignation par Reinforcement Learning',
                icon: 'mat_outline:smart_toy',
                link: '/fichier/ordretravail',
                status: 'Indisponible',
                detail: 'L\'optimisation du dispatching est temporairement indisponible.',
                enabled: false,
            },
            {
                id: 'traffic-stgcn',
                title: 'Analyse Trafic',
                description: 'Prédiction de congestion via STGCN',
                icon: 'mat_outline:traffic',
                link: '/fichier/circuit',
                status: 'Indisponible',
                detail: 'L\'analyse du trafic est temporairement indisponible.',
                enabled: false,
            },
            {
                id: 'gemini-assistant',
                title: 'Assistant Gemini',
                description: 'Support intelligent et analyse de données',
                icon: 'mat_outline:psychology',
                link: '/Accueil/page',
                status: 'Connecté',
                detail: 'Votre assistant IA est prêt à vous aider.',
                enabled: true,
            }
        ];
    }
}
