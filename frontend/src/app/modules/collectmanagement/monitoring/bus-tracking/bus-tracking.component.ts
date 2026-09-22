import {
    ChangeDetectionStrategy,
    ChangeDetectorRef,
    Component,
    OnDestroy,
    OnInit,
    ViewEncapsulation,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { TranslocoModule } from '@ngneat/transloco';
import {
    UntypedFormControl,
    ReactiveFormsModule,
} from '@angular/forms';
import {
    BehaviorSubject,
    catchError,
    debounceTime,
    distinctUntilChanged,
    EMPTY,
    finalize,
    forkJoin,
    map,
    Observable,
    of,
    startWith,
    Subject,
    switchMap,
    takeUntil,
    timer,
} from 'rxjs';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { fuseAnimations } from '../../../../../@fuse/animations';
import { FuseConfirmationService } from '../../../../../@fuse/services/confirmation';
import { FuseNavigationAction } from '../../../../../@fuse/components/navigation';
import { RoleNavigation } from '../../../../core/role-utilisateur/role-utilisateur.model';
import { BusService } from '../../../../core/bus/bus.service';
import { BusRuntimeEvent, BusPointage } from '../../../../core/bus/bus.model';
import { Circuit } from '../../../../core/circuit/circuit.model';
import { CircuitPointCollecte } from '../../../../core/circuit/circuit-point-collecte.model';
import { CircuitPointCollecteService } from '../../../../core/circuit/circuit-point-collecte.service';
import { CircuitService } from '../../../../core/circuit/circuit.service';
import { PointCollecte } from '../../../../core/point-collecte/point-collecte.model';
import { PointCollecteService } from '../../../../core/point-collecte/point-collecte.service';
import { CircuitMapOverview } from '../../../../shared/components/map-viewer/map-viewer.component';
import {
    BusTrackingAdapterService,
    BusTrackingItem,
} from './bus-tracking.adapter.service';
import {
    CircuitData,
    CircuitPointWithType,
} from './services/bus-tracking-circuit.service';
import {
    OptimizedRouteResult,
    BusTrackingRouteService,
} from './services/bus-tracking-route.service';
import { BusTrackingListComponent } from './list/bus-tracking-list.component';
import { BusTrackingMapComponent } from './map/bus-tracking-map.component';
import { BusTrackingPanelComponent } from './panel/bus-tracking-panel.component';

@Component({
    selector: 'app-bus-tracking',
    standalone: true,
    imports: [
        CommonModule,
        TranslocoModule,
        ReactiveFormsModule,
        MatButtonModule,
        MatFormFieldModule,
        MatIconModule,
        MatInputModule,
        MatProgressBarModule,
        MatSlideToggleModule,
        MatProgressSpinnerModule,
        BusTrackingListComponent,
        BusTrackingPanelComponent,
        BusTrackingMapComponent,
    ],
    templateUrl: './bus-tracking.component.html',
    styleUrl: './bus-tracking.component.scss',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    animations: fuseAnimations,
})
export class BusTrackingComponent implements OnInit, OnDestroy {
    readonly searchInputControl = new UntypedFormControl('');
    readonly autoRefreshControl = new UntypedFormControl(true);
    readonly showAllOnMapControl = new UntypedFormControl(false);

    isLoading: boolean = false;
    roleNavigation: RoleNavigation;
    lastRefreshAtUtc: string | null = null;

    buses: BusTrackingItem[] = [];
    filteredBuses: BusTrackingItem[] = [];
    allCircuits: Circuit[] = [];
    allPointsCollecte: PointCollecte[] = [];
    allCircuitsMapOverview: CircuitMapOverview[] = [];
    selectedBus: BusTrackingItem | null = null;
    selectedEvents: BusRuntimeEvent[] = [];
    selectedBusEventsLoading: boolean = false;
    selectedPointages: BusPointage[] = [];
    selectedBusPointagesLoading: boolean = false;

    circuitLoading: boolean = false;
    circuitData: CircuitData | null = null;
    circuitError: string | null = null;
    routeResult: OptimizedRouteResult | null = null;
    nextDestinationName: string | null = null;
    remainingDistanceKm: number | null = null;
    remainingEtaMinutes: number | null = null;
    routeProgressPercent: number | null = null;

    private readonly _selectedBusId$ = new BehaviorSubject<string | null>(null);
    private readonly _unsubscribeAll = new Subject<void>();
    private readonly _pollIntervalMs = 10000;
    private readonly _circuitsMap = new Map<string, Circuit>();
    private readonly _routeCache = new Map<string, OptimizedRouteResult>();

    constructor(
        private readonly _activatedRoute: ActivatedRoute,
        private readonly _busService: BusService,
        private readonly _adapter: BusTrackingAdapterService,
        private readonly _changeDetectorRef: ChangeDetectorRef,
        private readonly _fuseConfirmationService: FuseConfirmationService,
        private readonly _circuitService: CircuitService,
        private readonly _circuitPointService: CircuitPointCollecteService,
        private readonly _pointCollecteService: PointCollecteService,
        private readonly _routeService: BusTrackingRouteService,
    ) {}

    hasActionPermission(action: FuseNavigationAction): boolean {
        return !this.roleNavigation || this.roleNavigation?.actions?.includes(action);
    }

    ngOnInit(): void {
        this._activatedRoute.data
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((data) => {
                if (data?.navigation) {
                    this.roleNavigation = data.navigation;
                }
            });

        this.searchInputControl.valueChanges
            .pipe(
                debounceTime(250),
                distinctUntilChanged(),
                takeUntil(this._unsubscribeAll)
            )
            .subscribe(() => {
                this.applyClientFilter();
                this._changeDetectorRef.markForCheck();
            });

        this._selectedBusId$
            .pipe(
                switchMap((busId) => {
                    if (!busId || busId.startsWith('circuit_')) {
                        this.selectedEvents = [];
                        this._changeDetectorRef.markForCheck();
                        return of([]);
                    }

                    this.selectedBusEventsLoading = true;
                    this._changeDetectorRef.markForCheck();

                    return this._busService.GetBusRuntimeEvents(busId).pipe(
                        catchError(() => of([])),
                        finalize(() => {
                            this.selectedBusEventsLoading = false;
                            this._changeDetectorRef.markForCheck();
                        })
                    );
                }),
                takeUntil(this._unsubscribeAll)
            )
            .subscribe((events) => {
                this.selectedEvents = [...(events ?? [])].sort(
                    (a, b) =>
                        new Date(b.occurredAtUtc).getTime() -
                        new Date(a.occurredAtUtc).getTime()
                );
                this._changeDetectorRef.markForCheck();
            });

        // Pointages (badges RFID) du bus sélectionné.
        this._selectedBusId$
            .pipe(
                switchMap((busId) => {
                    if (!busId || busId.startsWith('circuit_')) {
                        this.selectedPointages = [];
                        this._changeDetectorRef.markForCheck();
                        return of([]);
                    }

                    this.selectedBusPointagesLoading = true;
                    this._changeDetectorRef.markForCheck();

                    return this._busService.GetBusPointages(busId).pipe(
                        catchError(() => of([])),
                        finalize(() => {
                            this.selectedBusPointagesLoading = false;
                            this._changeDetectorRef.markForCheck();
                        })
                    );
                }),
                takeUntil(this._unsubscribeAll)
            )
            .subscribe((pointages) => {
                this.selectedPointages = [...(pointages ?? [])].sort(
                    (a, b) =>
                        new Date(b.heurePointageUtc).getTime() -
                        new Date(a.heurePointageUtc).getTime()
                );
                this._changeDetectorRef.markForCheck();
            });

        this.startPolling();
    }

    startPolling(): void {
        this.autoRefreshControl.valueChanges
            .pipe(
                startWith(this.autoRefreshControl.value),
                map((enabled) => enabled !== false),
                switchMap((enabled) =>
                    enabled ? timer(0, this._pollIntervalMs) : EMPTY
                ),
                switchMap(() => this.refreshOnce()),
                takeUntil(this._unsubscribeAll)
            )
            .subscribe();
    }

    manualRefresh(): void {
        this.refreshOnce().pipe(takeUntil(this._unsubscribeAll)).subscribe();
    }

    refreshOnce() {
        this.isLoading = true;
        this._changeDetectorRef.markForCheck();

        return forkJoin({
            paged: this._busService.GetBuses(1, 1000, 'numeroIMM', 'asc', '').pipe(
                catchError(() => of({ buses: [], totalCount: 0 }))
            ),
            snapshot: this._busService.GetLivePositionsSnapshot().pipe(
                catchError(() => of(null))
            ),
            circuitsPaged: this._circuitService.GetCircuit(1, 1000, 'codeCircuit', 'asc', '').pipe(
                catchError(() => of({ circuits: [], totalCount: 0 }))
            ),
            pointsPaged: this._pointCollecteService.GetPointsCollecte(1, 1000).pipe(
                catchError(() => of({ pointsCollecte: [], totalCount: 0 }))
            ),
        }).pipe(
            map(({ paged, snapshot, circuitsPaged, pointsPaged }) => {
                const buses = paged?.buses ?? [];
                const circuits = circuitsPaged?.circuits ?? [];
                const allPoints = pointsPaged?.pointsCollecte ?? [];

                this.allCircuits = circuits;
                this.allPointsCollecte = allPoints;

                // Cache all circuits
                circuits.forEach((c) => {
                    if (c.codeCircuit) this._circuitsMap.set(c.codeCircuit.trim(), c);
                    if (c.circuitId) this._circuitsMap.set(c.circuitId.trim(), c);
                });

                const busItems = this._adapter.buildTrackingItems(buses, snapshot ?? null);

                // Find which circuit codes already have a bus assigned
                const assignedCircuitCodes = new Set(
                    busItems
                        .map((b) => (b.codeCircuit ?? '').trim().toLowerCase())
                        .filter(Boolean)
                );

                // Add virtual items for circuits without assigned buses
                const circuitItems: BusTrackingItem[] = [];
                for (const c of circuits) {
                    const code = (c.codeCircuit ?? '').trim().toLowerCase();
                    if (code && assignedCircuitCodes.has(code)) {
                        continue;
                    }

                    const depPoint = allPoints.find((p) => p.codePointCollecte === c.codePCDepart);
                    const arrPoint = allPoints.find((p) => p.codePointCollecte === c.codePCArrivee);
                    const lat = c.latitude ?? depPoint?.latitude ?? arrPoint?.latitude;
                    const lng = c.longitude ?? depPoint?.longitude ?? arrPoint?.longitude;

                    circuitItems.push({
                        busId: `circuit_${c.circuitId}`,
                        numeroIMM: c.libelleCircuit || `Circuit ${c.codeCircuit}`,
                        societeId: c.societeId,
                        isActive: false,
                        capacite: undefined,
                        latitude: lat,
                        longitude: lng,
                        currentOccupancy: undefined,
                        lastPositionAt: undefined,
                        lastOccupancyUpdateAt: undefined,
                        codeCircuit: c.codeCircuit,
                        codeChauffeur: undefined,
                    });
                }

                this.buses = [...busItems, ...circuitItems];
                this.applyClientFilter();
                this.lastRefreshAtUtc = snapshot?.generatedAtUtc ?? null;

                if (!this._selectedBusId$.value && this.buses.length > 0) {
                    this._selectedBusId$.next(this.buses[0].busId);
                }

                this.syncSelectedBusAndCircuit();
                this.buildAllCircuitsMapOverview(circuits, allPoints);
                this._changeDetectorRef.markForCheck();
            }),
            finalize(() => {
                this.isLoading = false;
                this._changeDetectorRef.markForCheck();
            }),
            catchError(() => {
                this.isLoading = false;
                this._changeDetectorRef.markForCheck();
                return of(null);
            })
        );
    }

    onSelectBus(busId: string): void {
        this._selectedBusId$.next(busId);
        this.syncSelectedBusAndCircuit();
        this._changeDetectorRef.markForCheck();
    }

    onSelectCircuitFromMap(circuitIdentifier: string): void {
        const matchingBus = this.buses.find(
            (b) =>
                (b.codeCircuit && b.codeCircuit.toLowerCase() === circuitIdentifier.toLowerCase()) ||
                b.busId === circuitIdentifier ||
                b.busId === `circuit_${circuitIdentifier}`
        );
        if (matchingBus) {
            this.onSelectBus(matchingBus.busId);
        }
    }

    onToggleShowAllOnMap(showAll: boolean): void {
        this.showAllOnMapControl.setValue(showAll);
        this._changeDetectorRef.markForCheck();
    }

    getMapLocations() {
        const realBuses = this.filteredBuses.filter((b) => !b.busId.startsWith('circuit_'));
        return this._adapter.buildMapLocations(realBuses, {
            selectedBusId: this.selectedBus?.busId ?? this._selectedBusId$.value,
            showAll: this.showAllOnMapControl.value === true,
        });
    }

    get CircuitPoints(): CircuitPointWithType[] {
        if (!this.circuitData) {
            return [];
        }

        // Regroupe les badges réussis par point de collecte de l'employé.
        const taggedByPoint = new Map<string, { nom: string; matricule?: string; heure: string; message?: string }[]>();
        for (const p of this.selectedPointages ?? []) {
            const code = (p.codePointCollecteEmploye ?? '').trim();
            if (!p.isSuccess || !code) {
                continue;
            }
            const key = code.toLowerCase();
            const list = taggedByPoint.get(key) ?? [];
            list.push({
                nom: p.nomEmploye || p.matricule || p.tag,
                matricule: p.matricule,
                heure: new Date(p.heurePointageUtc).toLocaleTimeString('fr-FR'),
                message: p.message,
            });
            taggedByPoint.set(key, list);
        }

        return this.circuitData.allPoints.map((p) => {
            const employees = taggedByPoint.get((p.codePointCollecte ?? '').trim().toLowerCase());
            return {
                ...p,
                pointCategory: p.pointCategory as 'departure' | 'collection' | 'arrival',
                name: p.libellePointCollecte || p.codePointCollecte,
                tagged: !!employees && employees.length > 0,
                taggedEmployees: employees ?? [],
            };
        });
    }

    get OptimizedRouteForMap() {
        if (!this.routeResult) {
            return null;
        }
        return {
            geometry: this.routeResult.geometry,
            distanceKm: this.routeResult.totalDistanceKm,
            durationMinutes: this.routeResult.estimatedDurationMinutes,
        };
    }

    get completedCollectionPoints(): number {
        if (!this.circuitData || !this.routeResult) {
            return 0;
        }
        const points = [this.circuitData.allPoints[0], ...this.circuitData.collectionPoints, this.circuitData.allPoints[this.circuitData.allPoints.length - 1]];
        if (!this.selectedBus || this.selectedBus.latitude == null || this.selectedBus.longitude == null) {
            return 0;
        }

        let closestIndex = 0;
        let closestDist = Number.POSITIVE_INFINITY;

        for (let i = 0; i < points.length; i++) {
            const p = points[i];
            if (p.latitude == null || p.longitude == null) continue;
            const d = this.haversine(this.selectedBus.latitude!, this.selectedBus.longitude!, p.latitude, p.longitude);
            if (d < closestDist) {
                closestDist = d;
                closestIndex = i;
            }
        }

        return Math.min(closestIndex, this.circuitData.collectionPoints.length);
    }

    get SelectedBusPosition() {
        if (!this.selectedBus || this.selectedBus.latitude == null || this.selectedBus.longitude == null) {
            return null;
        }
        return {
            latitude: this.selectedBus.latitude,
            longitude: this.selectedBus.longitude,
        };
    }

    emptySelectedBus(): void {
        if (!this.selectedBus) {
            return;
        }

        if (!this.hasActionPermission(FuseNavigationAction.Edit)) {
            return;
        }

        const confirmation = this._fuseConfirmationService.open({
            title: 'Vider le Bus',
            message:
                'Êtes-vous sûr de vouloir vider ce bus ? Cette action réinitialise le taux d’occupation.',
            icon: { show: false },
            actions: {
                confirm: { label: 'Vider', color: 'warn' },
                cancel: { label: 'Annuler' },
            },
            dismissible: true,
        });

        confirmation.afterClosed().subscribe((result) => {
            if (result !== 'confirmed') {
                return;
            }

            this.isLoading = true;
            this._changeDetectorRef.markForCheck();

            this._busService.EmptyBus(this.selectedBus!.busId)
                .pipe(
                    switchMap((runtimeState) => {
                        this.buses = this._adapter.mergeRuntimeState(this.buses, runtimeState);
                        this.applyClientFilter();
                        this.syncSelectedBusAndCircuit();
                        this._changeDetectorRef.markForCheck();

                        return this.refreshOnce().pipe(map(() => runtimeState));
                    }),
                    finalize(() => {
                        this.isLoading = false;
                        this._changeDetectorRef.markForCheck();
                    }),
                    catchError(() => {
                        this.isLoading = false;
                        this._changeDetectorRef.markForCheck();
                        return of(null);
                    })
                )
                .subscribe(() => {
                    const busId = this.selectedBus?.busId ?? null;
                    if (busId) {
                        this._selectedBusId$.next(busId);
                    }
                });
        });
    }

    private applyClientFilter(): void {
        const q = (this.searchInputControl.value ?? '').toString().trim().toLowerCase();
        if (!q) {
            this.filteredBuses = [...this.buses];
            return;
        }

        this.filteredBuses = (this.buses ?? []).filter((b) => {
            const haystack = `${b.numeroIMM ?? ''} ${b.imei ?? ''}`.toLowerCase();
            return haystack.includes(q);
        });
    }

    private syncSelectedBusAndCircuit(): void {
        const selectedId = this._selectedBusId$.value;
        if (!selectedId) {
            this.selectedBus = null;
            this.clearCircuitState();
            return;
        }

        const previousSelectedBus = this.selectedBus;
        this.selectedBus = this.buses.find((b) => b.busId === selectedId) ?? null;

        const codeCircuitChanged =
            previousSelectedBus?.codeCircuit !== this.selectedBus?.codeCircuit;
        const busChanged = this.selectedBus && this.selectedBus !== previousSelectedBus;

        if ((busChanged || !this.circuitData || codeCircuitChanged) && this.selectedBus?.codeCircuit) {
            this.loadCircuitDataForSelectedBus();
        } else if (!this.selectedBus) {
            this.clearCircuitState();
        } else if (
            this.selectedBus?.latitude != null &&
            this.selectedBus?.longitude != null &&
            this.circuitData
        ) {
            this.updateRouteProgress(this.selectedBus.latitude, this.selectedBus.longitude);
        }
    }

    private clearCircuitState(): void {
        this.circuitData = null;
        this.routeResult = null;
        this.circuitError = null;
        this.circuitLoading = false;
        this.nextDestinationName = null;
        this.remainingDistanceKm = null;
        this.remainingEtaMinutes = null;
        this.routeProgressPercent = null;
    }

    private loadCircuitDataForSelectedBus(): void {
        if (!this.selectedBus) {
            this.clearCircuitState();
            return;
        }

        const bus = this.selectedBus;
        const codeCircuit = bus.codeCircuit;

        if (!codeCircuit) {
            this.circuitData = null;
            this.routeResult = null;
            this.circuitError = null;
            this.nextDestinationName = null;
            this.remainingDistanceKm = null;
            this.remainingEtaMinutes = null;
            this.routeProgressPercent = null;
            return;
        }

        const cachedCircuit = this._circuitsMap.get(codeCircuit);
        if (cachedCircuit) {
            this.buildFromCircuit(codeCircuit, cachedCircuit);
            return;
        }

        this.circuitLoading = true;
        this.circuitError = null;
        this._changeDetectorRef.markForCheck();

        this._circuitService.GetCircuit(1, 1000, 'codeCircuit', 'asc', '')
            .pipe(
                map((response) => response.circuits ?? []),
                catchError(() => of([])),
                takeUntil(this._unsubscribeAll)
            )
            .subscribe((circuits) => {
                circuits.forEach((c) => this._circuitsMap.set(c.codeCircuit, c));

                const circuit = circuits.find((c) => c.codeCircuit === codeCircuit);
                if (circuit) {
                    this.buildFromCircuit(codeCircuit, circuit);
                } else {
                    this.circuitData = null;
                    this.routeResult = null;
                    this.circuitError = null;
                    this.nextDestinationName = null;
                    this.remainingDistanceKm = null;
                    this.remainingEtaMinutes = null;
                    this.routeProgressPercent = null;
                }

                this.circuitLoading = false;
                this._changeDetectorRef.markForCheck();
            });
    }

    private buildFromCircuit(codeCircuit: string, circuit: Circuit): void {
        this._circuitPointService
            .getByCircuit(circuit.circuitId)
            .pipe(
                map((points) => {
                    let effectivePoints = points ? [...points] : [];
                    if (effectivePoints.length === 0) {
                        effectivePoints = this.allPointsCollecte
                            .filter((p) => p.circuitId === circuit.circuitId)
                            .map((p, idx) => ({
                                circuitPointCollecteId: p.pointCollecteId,
                                circuitId: circuit.circuitId,
                                codePointCollecte: p.codePointCollecte,
                                libellePointCollecte: p.libellePointCollecte,
                                latitude: p.latitude,
                                longitude: p.longitude,
                                ordre: idx + 1,
                            }));
                    }

                    const depPoint = this.allPointsCollecte.find((p) => p.codePointCollecte === circuit.codePCDepart);
                    const arrPoint = this.allPointsCollecte.find((p) => p.codePointCollecte === circuit.codePCArrivee);

                    if (depPoint && !effectivePoints.some((p) => p.codePointCollecte === depPoint.codePointCollecte)) {
                        effectivePoints.unshift({
                            circuitPointCollecteId: depPoint.pointCollecteId,
                            circuitId: circuit.circuitId,
                            codePointCollecte: depPoint.codePointCollecte,
                            libellePointCollecte: depPoint.libellePointCollecte,
                            latitude: depPoint.latitude,
                            longitude: depPoint.longitude,
                            ordre: 0,
                        });
                    }

                    if (arrPoint && !effectivePoints.some((p) => p.codePointCollecte === arrPoint.codePointCollecte)) {
                        effectivePoints.push({
                            circuitPointCollecteId: arrPoint.pointCollecteId,
                            circuitId: circuit.circuitId,
                            codePointCollecte: arrPoint.codePointCollecte,
                            libellePointCollecte: arrPoint.libellePointCollecte,
                            latitude: arrPoint.latitude,
                            longitude: arrPoint.longitude,
                            ordre: 999,
                        });
                    }

                    const categorized = this.categorizePoints(effectivePoints, circuit.circuitId);
                    if (categorized.allPoints.length === 0) {
                        this.circuitData = categorized;
                        this.routeResult = null;
                        this.nextDestinationName = null;
                        this.remainingDistanceKm = null;
                        this.remainingEtaMinutes = null;
                        this.routeProgressPercent = null;
                        return;
                    }

                    this.circuitData = {
                        ...categorized,
                        circuitCode: circuit.codeCircuit,
                        circuitName: circuit.libelleCircuit ?? circuit.codeCircuit,
                        circuitColor: circuit.couleur ?? '#2563eb',
                    };

                    const originLat = circuit.latitude ?? categorized.departure?.latitude ?? 36.8065;
                    const originLon = circuit.longitude ?? categorized.departure?.longitude ?? 10.1815;

                    this._routeService
                        .calculateOptimizedRoute(
                            originLat,
                            originLon,
                            categorized.collectionPoints,
                            categorized.arrival?.latitude ?? originLat,
                            categorized.arrival?.longitude ?? originLon
                        )
                        .pipe(
                            catchError(() => of(null)),
                            takeUntil(this._unsubscribeAll)
                        )
                        .subscribe((result) => {
                            this.routeResult = result;
                            if (result) {
                                this._routeCache.set(circuit.circuitId, result);
                                const overviewItem = this.allCircuitsMapOverview.find(
                                    (o) => o.circuitId === circuit.circuitId
                                );
                                if (overviewItem) {
                                    overviewItem.geometry = result.geometry;
                                    overviewItem.distanceKm = result.totalDistanceKm;
                                    overviewItem.durationMinutes = result.estimatedDurationMinutes;
                                }
                            }

                            if (this.selectedBus && this.selectedBus.latitude != null && this.selectedBus.longitude != null) {
                                this.updateRouteProgress(this.selectedBus.latitude, this.selectedBus.longitude);
                            } else {
                                if (this.circuitData && this.circuitData.allPoints.length > 0) {
                                    const dep = this.circuitData.allPoints[0];
                                    if (dep) {
                                        this.nextDestinationName = dep.libellePointCollecte || dep.codePointCollecte;
                                    }
                                }
                                this.remainingDistanceKm = result?.totalDistanceKm ?? circuit.distanceKm ?? null;
                                this.remainingEtaMinutes = result?.estimatedDurationMinutes ?? circuit.dureeMinutes ?? null;
                                this.routeProgressPercent = 0;
                            }

                            this._changeDetectorRef.markForCheck();
                        });
                }),
                catchError(() => {
                    this.circuitLoading = false;
                    this._changeDetectorRef.markForCheck();
                    return of(null);
                }),
                takeUntil(this._unsubscribeAll)
            )
            .subscribe();
    }

    private buildAllCircuitsMapOverview(circuits: Circuit[], allPoints: PointCollecte[]): void {
        const overviewList: CircuitMapOverview[] = [];

        for (const c of circuits) {
            const depPoint = allPoints.find((p) => p.codePointCollecte === c.codePCDepart);
            const arrPoint = allPoints.find((p) => p.codePointCollecte === c.codePCArrivee);
            const circuitPts = allPoints.filter(
                (p) =>
                    p.circuitId === c.circuitId &&
                    p.codePointCollecte !== c.codePCDepart &&
                    p.codePointCollecte !== c.codePCArrivee
            );

            const orderedNodes: { latitude: number; longitude: number; name: string }[] = [];
            if (depPoint?.latitude != null && depPoint?.longitude != null) {
                orderedNodes.push({
                    latitude: depPoint.latitude,
                    longitude: depPoint.longitude,
                    name: depPoint.libellePointCollecte || depPoint.codePointCollecte,
                });
            } else if (c.latitude != null && c.longitude != null) {
                orderedNodes.push({
                    latitude: c.latitude,
                    longitude: c.longitude,
                    name: c.codePCDepart || 'Départ',
                });
            }

            for (const pt of circuitPts) {
                if (pt.latitude != null && pt.longitude != null) {
                    orderedNodes.push({
                        latitude: pt.latitude,
                        longitude: pt.longitude,
                        name: pt.libellePointCollecte || pt.codePointCollecte,
                    });
                }
            }

            if (arrPoint?.latitude != null && arrPoint?.longitude != null) {
                orderedNodes.push({
                    latitude: arrPoint.latitude,
                    longitude: arrPoint.longitude,
                    name: arrPoint.libellePointCollecte || arrPoint.codePointCollecte,
                });
            }

            const coords: [number, number][] = orderedNodes.map((n) => [n.latitude, n.longitude]);
            const cachedRoute = this._routeCache.get(c.circuitId);

            overviewList.push({
                circuitId: c.circuitId,
                codeCircuit: c.codeCircuit,
                name: c.libelleCircuit || c.codeCircuit,
                color: c.couleur || '#2563eb',
                coordinates: coords,
                geometry: cachedRoute?.geometry ?? coords,
                distanceKm: cachedRoute?.totalDistanceKm ?? c.distanceKm ?? undefined,
                durationMinutes: cachedRoute?.estimatedDurationMinutes ?? c.dureeMinutes ?? undefined,
            });

            if (!cachedRoute && orderedNodes.length >= 2) {
                const dep = orderedNodes[0];
                const arr = orderedNodes[orderedNodes.length - 1];
                const inter: CircuitPointCollecte[] = orderedNodes.slice(1, -1).map((pt, idx) => ({
                    circuitPointCollecteId: `inter_${idx}`,
                    circuitId: c.circuitId,
                    codePointCollecte: pt.name,
                    libellePointCollecte: pt.name,
                    latitude: pt.latitude,
                    longitude: pt.longitude,
                    ordre: idx + 1,
                }));

                this._routeService
                    .calculateOptimizedRoute(dep.latitude, dep.longitude, inter, arr.latitude, arr.longitude)
                    .pipe(
                        catchError(() => of(null)),
                        takeUntil(this._unsubscribeAll)
                    )
                    .subscribe((res) => {
                        if (res && res.geometry?.length > 1) {
                            this._routeCache.set(c.circuitId, res);
                            const item = this.allCircuitsMapOverview.find((item) => item.circuitId === c.circuitId);
                            if (item) {
                                item.geometry = res.geometry;
                                item.distanceKm = res.totalDistanceKm;
                                item.durationMinutes = res.estimatedDurationMinutes;
                                this._changeDetectorRef.markForCheck();
                            }
                        }
                    });
            }
        }

        this.allCircuitsMapOverview = overviewList;
    }

    private categorizePoints(
        points: CircuitPointCollecte[],
        circuitId: string
    ): CircuitData {
        const valid = points
            .filter((p) => p.latitude != null && p.longitude != null)
            .sort((a, b) => (a.ordre ?? 0) - (b.ordre ?? 0));

        if (valid.length === 0) {
            return {
                circuitId,
                circuitCode: circuitId,
                circuitName: circuitId,
                circuitColor: '#2563eb',
                departure: null,
                arrival: null,
                collectionPoints: [],
                allPoints: [],
                coordinates: [],
            };
        }

        const departure: CircuitPointWithType = { ...valid[0], pointCategory: 'departure' };
        const arrival: CircuitPointWithType = {
            ...valid[valid.length - 1],
            pointCategory: 'arrival',
        };
        const collectionPoints: CircuitPointWithType[] = valid
            .slice(1, -1)
            .map((p) => ({ ...p, pointCategory: 'collection' }));

        return {
            circuitId,
            circuitCode: circuitId,
            circuitName: circuitId,
            circuitColor: '#2563eb',
            departure,
            arrival,
            collectionPoints,
            allPoints: [departure, ...collectionPoints, arrival],
            coordinates: valid.map((p) => [p.latitude!, p.longitude!] as [number, number]),
        };
    }

    private updateRouteProgress(busLat: number, busLon: number): void {
        if (!this.routeResult || !this.circuitData || this.circuitData.allPoints.length === 0) {
            this.nextDestinationName = null;
            this.remainingDistanceKm = null;
            this.remainingEtaMinutes = null;
            this.routeProgressPercent = null;
            return;
        }

        const points = [this.circuitData.allPoints[0], ...this.circuitData.collectionPoints, this.circuitData.allPoints[this.circuitData.allPoints.length - 1]];
        let closestIndex = 0;
        let closestDist = Number.POSITIVE_INFINITY;

        for (let i = 0; i < points.length; i++) {
            const p = points[i];
            if (p.latitude == null || p.longitude == null) continue;
            const d = this.haversine(busLat, busLon, p.latitude, p.longitude);
            if (d < closestDist) {
                closestDist = d;
                closestIndex = i;
            }
        }

        const completedCount = Math.min(closestIndex, this.circuitData.collectionPoints.length);
        this.routeProgressPercent = this.circuitData.collectionPoints.length > 0
            ? Math.round((completedCount / this.circuitData.collectionPoints.length) * 100)
            : 0;

        if (closestIndex < points.length - 1) {
            this.nextDestinationName = points[closestIndex + 1].libellePointCollecte || points[closestIndex + 1].codePointCollecte;
        } else {
            this.nextDestinationName = points[points.length - 1].libellePointCollecte || points[points.length - 1].codePointCollecte;
        }

        if (this.routeResult.totalDistanceKm > 0 && closestIndex > 0) {
            const segmentFraction = closestIndex / Math.max(1, points.length - 1);
            this.remainingDistanceKm = Math.round(this.routeResult.totalDistanceKm * (1 - segmentFraction) * 100) / 100;
            this.remainingEtaMinutes = Math.round(this.routeResult.estimatedDurationMinutes * (1 - segmentFraction) * 100) / 100;
        } else {
            this.remainingDistanceKm = this.routeResult.totalDistanceKm;
            this.remainingEtaMinutes = this.routeResult.estimatedDurationMinutes;
        }
    }

    private haversine(lat1: number, lon1: number, lat2: number, lon2: number): number {
        const R = 6371;
        const toRad = (v: number) => (v * Math.PI) / 180;
        const dLat = toRad(lat2 - lat1);
        const dLon = toRad(lon2 - lon1);
        const a =
            Math.sin(dLat / 2) ** 2 +
            Math.cos(toRad(lat1)) * Math.cos(toRad(lat2)) * Math.sin(dLon / 2) ** 2;
        const c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
        return R * c;
    }

    ngOnDestroy(): void {
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
        this._circuitsMap.clear();
    }

    protected readonly FuseNavigationAction = FuseNavigationAction;
}
