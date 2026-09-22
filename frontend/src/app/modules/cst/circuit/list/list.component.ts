import { PdfExportService } from '../../../../core/common/pdf-export.service';
import { CsvExportService } from '../../../../core/common/csv-export.service';
import {
    ChangeDetectionStrategy,
    ChangeDetectorRef,
    Component,
    OnDestroy,
    OnInit,
    ViewChild,
    ViewEncapsulation,
} from '@angular/core';
import { TranslocoModule } from '@ngneat/transloco';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, UntypedFormControl } from '@angular/forms';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatIconModule } from '@angular/material/icon';
import { RouterLink } from '@angular/router';
import { fuseAnimations } from '../../../../../@fuse/animations';
import {
    BehaviorSubject,
    combineLatest,
    debounceTime,
    distinctUntilChanged,
    map,
    Observable,
    of,
    Subject,
    switchMap,
    take,
    takeUntil,
} from 'rxjs';
import { Circuit } from '../../../../core/circuit/circuit.model';
import { CircuitService } from '../../../../core/circuit/circuit.service';
import { FuseConfirmationService } from '../../../../../@fuse/services/confirmation';
import { RoleNavigation } from '../../../../core/role-utilisateur/role-utilisateur.model';
import { FuseNavigationAction } from '../../../../../@fuse/components/navigation';
import { MapPickerComponent, MapRoutePoint } from '../../../../shared/components/map-picker/map-picker.component';
import { MapSkeletonComponent } from '../../../../shared/components/map-skeleton/map-skeleton.component';
import { CircuitPointCollecteService } from '../../../../core/circuit/circuit-point-collecte.service';
import { CircuitPointCollecte } from '../../../../core/circuit/circuit-point-collecte.model';
import { PointCollecteService } from '../../../../core/point-collecte/point-collecte.service';
import { PointCollecte } from '../../../../core/point-collecte/point-collecte.model';

type StatusFilter = 'all' | 'active' | 'inactive';

@Component({
    selector: 'app-list',
    standalone: true,
    imports: [
        MatIconModule,
        MatProgressBarModule,
        ReactiveFormsModule,
        CommonModule,
        MatPaginatorModule,
        TranslocoModule,
        RouterLink,
        MapPickerComponent,
        MapSkeletonComponent,
    ],
    templateUrl: './list.component.html',
    styleUrl: './list.component.scss',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    animations: fuseAnimations,
})
export class ListComponent implements OnInit, OnDestroy {
    @ViewChild(MatPaginator) private _paginator: MatPaginator;

    circuit$: Observable<Circuit[]>;
    filteredCircuits$: Observable<Circuit[]>;

    isLoading: boolean = false;
    circuitsLength: number;
    searchInputControl: UntypedFormControl = new UntypedFormControl();
    roleNavigation: RoleNavigation;

    statusFilter: StatusFilter = 'all';
    private readonly _statusFilter$ = new BehaviorSubject<StatusFilter>('all');

    selectedCircuit: Circuit | null = null;
    selectedCircuitPoints: CircuitPointCollecte[] = [];
    isLoadingPoints: boolean = false;
    /** Points count per circuit, learned as circuits get selected. */
    pointsCountByCircuit = new Map<string, number>();

    allPoints: PointCollecte[] = [];
    mapPoints: MapRoutePoint[] = [];
    sortActive: string = 'codeCircuit';
    sortDirection: 'asc' | 'desc' = 'asc';

    /** Placeholder rows for the loading skeleton. */
    readonly skeletonRows = Array.from({ length: 6 });

    private _unsubscribeAll: Subject<any> = new Subject<any>();

    constructor(
        private _pdfExportService: PdfExportService,

        private _csvExportService: CsvExportService,

        private _circuitService: CircuitService,
        private _circuitPointCollecteService: CircuitPointCollecteService,
        private _pointCollecteService: PointCollecteService,
        private _changeDetectorRef: ChangeDetectorRef,
        private _fuseConfirmationService: FuseConfirmationService
    ) {}

    ngOnInit(): void {
        this.circuit$ = this._circuitService.circuits$;

        // Status filter applied client-side on the current page
        this.filteredCircuits$ = combineLatest([this.circuit$, this._statusFilter$]).pipe(
            map(([circuits, status]) => {
                if (!circuits) {
                    return [];
                }
                if (status === 'all') {
                    return circuits;
                }
                return circuits.filter((c) => (status === 'active' ? c.isActive : !c.isActive));
            })
        );

        this._circuitService.circuitsLength$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((length) => {
                this.circuitsLength = length;
                this._changeDetectorRef.markForCheck();
            });

        // Load collection points to resolve coordinates and route fallbacks
        this._pointCollecteService
            .GetPointsCollecte(1, 1000)
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((res) => {
                this.allPoints = res?.pointsCollecte ?? [];
                if (this.selectedCircuit) {
                    this.refreshSelectedCircuitRoute();
                } else {
                    this.circuit$.pipe(take(1)).subscribe((circuits) => {
                        this.mapPoints = this.buildOverviewPoints(circuits ?? []);
                        this._changeDetectorRef.markForCheck();
                    });
                }
            });

        // Overview map: every circuit with coordinates, as a clustered poi dot
        this.circuit$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((circuits) => {
                if (!this.selectedCircuit) {
                    this.mapPoints = this.buildOverviewPoints(circuits ?? []);
                }
                this._changeDetectorRef.markForCheck();
            });

        // Debounced server-side search
        this.searchInputControl.valueChanges
            .pipe(
                debounceTime(350),
                distinctUntilChanged(),
                switchMap(() => {
                    this.isLoading = true;
                    this._changeDetectorRef.markForCheck();
                    return this.getCircuits();
                }),
                takeUntil(this._unsubscribeAll)
            )
            .subscribe(() => {
                this.isLoading = false;
                this._changeDetectorRef.markForCheck();
            });
    }

    // ------------------------------------------------------------------ //
    //  Data loading / sorting / filtering
    // ------------------------------------------------------------------ //

    getCircuits() {
        return this._circuitService.GetCircuit(
            (this._paginator?.pageIndex | 0) + 1,
            this._paginator?.pageSize,
            this.sortActive,
            this.sortDirection,
            this.searchInputControl.value
        );
    }

    SortChange() {
        this.isLoading = true;
        this.getCircuits()
            .pipe(
                map(() => {
                    this.isLoading = false;
                    this._changeDetectorRef.markForCheck();
                })
            )
            .subscribe();
    }

    setSort(active: string, direction: 'asc' | 'desc'): void {
        this.sortActive = active;
        this.sortDirection = direction;
        this.SortChange();
    }

    setStatusFilter(status: StatusFilter): void {
        this.statusFilter = status;
        this._statusFilter$.next(status);
    }

    hasActionPermission(action: FuseNavigationAction): boolean {
        return !this.roleNavigation || this.roleNavigation?.actions?.includes(action);
    }

    // ------------------------------------------------------------------ //
    //  Selection → points + route on the map
    // ------------------------------------------------------------------ //

    private parseCoord(val: any): number | null {
        if (val == null) return null;
        if (typeof val === 'number') {
            return !isNaN(val) && val !== 0 ? val : null;
        }
        if (typeof val === 'string') {
            const cleaned = val.trim().replace(',', '.');
            const num = parseFloat(cleaned);
            return !isNaN(num) && num !== 0 ? num : null;
        }
        return null;
    }

    private isValidCoordinate(lat: any, lng: any): boolean {
        const nLat = this.parseCoord(lat);
        const nLng = this.parseCoord(lng);
        return nLat !== null && nLng !== null && nLat >= 25 && nLat <= 40 && nLng >= 5 && nLng <= 15;
    }

    private enrichCircuitPoints(circuit: Circuit, points: CircuitPointCollecte[]): void {
        if (!points?.length || !this.allPoints?.length) {
            return;
        }

        const circuitAssignedPoints = this.allPoints.filter(
            (ap) => circuit?.circuitId && ap.circuitId === circuit.circuitId
        );

        points.forEach((p, idx) => {
            if (this.isValidCoordinate(p.latitude, p.longitude)) {
                return;
            }

            const pCode = p.codePointCollecte?.trim().toLowerCase();
            const pLib = p.libellePointCollecte?.trim().toLowerCase();

            let match = this.allPoints.find((ap) => {
                const apCode = ap.codePointCollecte?.trim().toLowerCase();
                const apLib = ap.libellePointCollecte?.trim().toLowerCase();
                return (
                    (pCode && apCode && apCode === pCode) ||
                    (pLib && apLib && apLib === pLib) ||
                    (pCode && apLib && apLib === pCode) ||
                    (pLib && apCode && apCode === pLib)
                );
            });

            if (!match && circuitAssignedPoints.length > 0) {
                match = circuitAssignedPoints[idx] || circuitAssignedPoints.find((ap) => !points.some(pt => pt.codePointCollecte === ap.codePointCollecte));
            }

            if (match && this.isValidCoordinate(match.latitude, match.longitude)) {
                p.latitude = this.parseCoord(match.latitude)!;
                p.longitude = this.parseCoord(match.longitude)!;
                if (!p.libellePointCollecte && match.libellePointCollecte) {
                    p.libellePointCollecte = match.libellePointCollecte;
                }
            }
        });
    }

    refreshSelectedCircuitRoute(): void {
        if (!this.selectedCircuit || !this.selectedCircuitPoints?.length) {
            return;
        }

        this.enrichCircuitPoints(this.selectedCircuit, this.selectedCircuitPoints);

        this.mapPoints = this.buildSelectedRoutePoints(this.selectedCircuit, this.selectedCircuitPoints);
        this._changeDetectorRef.markForCheck();
    }

    toggleDetails(circuitId: string): void {
        if (this.selectedCircuit && this.selectedCircuit.circuitId === circuitId) {
            this.closeDetails();
            return;
        }

        this.circuit$
            .pipe(
                take(1),
                map((circuits) => circuits.find((item) => item.circuitId === circuitId) ?? null),
                switchMap((circuit) => {
                    this.selectedCircuit = circuit;
                    this.isLoadingPoints = !!circuit?.circuitId;
                    this._changeDetectorRef.markForCheck();

                    if (circuit?.circuitId) {
                        return this._circuitPointCollecteService.getByCircuit(circuit.circuitId).pipe(
                            map((points) => ({ circuit, points }))
                        );
                    }
                    return of({ circuit, points: [] as CircuitPointCollecte[] });
                }),
                takeUntil(this._unsubscribeAll)
            )
            .subscribe(({ circuit, points }) => {
                if (!circuit || this.selectedCircuit?.circuitId !== circuit.circuitId) {
                    return;
                }

                let orderedPoints = [...points].sort((a, b) => (a.ordre ?? 0) - (b.ordre ?? 0));

                // Fallback for circuits without CircuitPointCollecte records yet
                if (orderedPoints.length === 0 && circuit) {
                    const fallbackPoints: CircuitPointCollecte[] = [];
                    const startPoint = this.allPoints.find((p) =>
                        p.codePointCollecte?.trim().toLowerCase() === circuit.codePCDepart?.trim().toLowerCase()
                    );
                    const endPoint = this.allPoints.find((p) =>
                        p.codePointCollecte?.trim().toLowerCase() === circuit.codePCArrivee?.trim().toLowerCase()
                    );
                    const assignedPoints = this.allPoints.filter((p) => p.circuitId === circuit.circuitId);

                    if (startPoint) {
                        fallbackPoints.push({
                            circuitPointCollecteId: null,
                            circuitId: circuit.circuitId,
                            codePointCollecte: startPoint.codePointCollecte,
                            libellePointCollecte: startPoint.libellePointCollecte || startPoint.codePointCollecte,
                            latitude: this.parseCoord(startPoint.latitude),
                            longitude: this.parseCoord(startPoint.longitude),
                            ordre: 0,
                        });
                    }

                    assignedPoints.forEach((p, idx) => {
                        if (p.codePointCollecte !== circuit.codePCDepart && p.codePointCollecte !== circuit.codePCArrivee) {
                            fallbackPoints.push({
                                circuitPointCollecteId: null,
                                circuitId: circuit.circuitId,
                                codePointCollecte: p.codePointCollecte,
                                libellePointCollecte: p.libellePointCollecte || p.codePointCollecte,
                                latitude: this.parseCoord(p.latitude),
                                longitude: this.parseCoord(p.longitude),
                                ordre: idx + 1,
                            });
                        }
                    });

                    if (endPoint) {
                        fallbackPoints.push({
                            circuitPointCollecteId: null,
                            circuitId: circuit.circuitId,
                            codePointCollecte: endPoint.codePointCollecte,
                            libellePointCollecte: endPoint.libellePointCollecte || endPoint.codePointCollecte,
                            latitude: this.parseCoord(endPoint.latitude),
                            longitude: this.parseCoord(endPoint.longitude),
                            ordre: fallbackPoints.length,
                        });
                    }

                    if (fallbackPoints.length > 0) {
                        orderedPoints = fallbackPoints;
                        // Silently backfill to CircuitPointCollecte for future requests
                        fallbackPoints.forEach((fp) => {
                            this._circuitPointCollecteService.add(fp).subscribe();
                        });
                    }
                }

                // Ensure all points have valid coordinates (enrich from allPoints if missing in CircuitPointCollecte)
                this.enrichCircuitPoints(circuit, orderedPoints);

                this.selectedCircuitPoints = orderedPoints;
                this.isLoadingPoints = false;
                this.pointsCountByCircuit.set(circuit.circuitId, orderedPoints.length);
                this.mapPoints = this.buildSelectedRoutePoints(circuit, orderedPoints);
                this._changeDetectorRef.markForCheck();
            });
    }

    closeDetails(): void {
        this.selectedCircuit = null;
        this.selectedCircuitPoints = [];
        this.isLoadingPoints = false;

        this.circuit$
            .pipe(take(1), map((circuits) => this.buildOverviewPoints(circuits ?? [])))
            .subscribe((points) => {
                this.mapPoints = points;
                this._changeDetectorRef.markForCheck();
            });
    }

    onMapPointClick(circuitId: string): void {
        if (circuitId && circuitId !== this.selectedCircuit?.circuitId) {
            this.toggleDetails(circuitId);
        }
    }

    private buildOverviewPoints(circuits: Circuit[]): MapRoutePoint[] {
        return circuits
            .map((c): MapRoutePoint | null => {
                let lat = this.parseCoord(c.latitude);
                let lng = this.parseCoord(c.longitude);

                // Fallback to departure point coordinates if circuit coordinates are missing
                if ((lat === null || lng === null) && c.codePCDepart && this.allPoints?.length) {
                    const startPoint = this.allPoints.find((p) =>
                        p.codePointCollecte?.trim().toLowerCase() === c.codePCDepart?.trim().toLowerCase()
                    );
                    if (startPoint && this.isValidCoordinate(startPoint.latitude, startPoint.longitude)) {
                        lat = this.parseCoord(startPoint.latitude);
                        lng = this.parseCoord(startPoint.longitude);
                    }
                }

                if (lat === null || lng === null) {
                    return null;
                }

                return {
                    id: c.circuitId,
                    kind: 'poi' as const,
                    latitude: lat,
                    longitude: lng,
                    label: `${c.codeCircuit}${c.libelleCircuit ? ' — ' + c.libelleCircuit : ''}`,
                    color: c.couleur || '#2563eb',
                };
            })
            .filter((p): p is MapRoutePoint => p != null);
    }

    private buildSelectedRoutePoints(circuit: Circuit, points: CircuitPointCollecte[]): MapRoutePoint[] {
        const located = points
            .map((p) => ({
                ...p,
                parsedLat: this.parseCoord(p.latitude),
                parsedLng: this.parseCoord(p.longitude),
            }))
            .filter((p) => p.parsedLat !== null && p.parsedLng !== null);

        if (located.length === 0) {
            let lat = this.parseCoord(circuit.latitude);
            let lng = this.parseCoord(circuit.longitude);
            if ((lat === null || lng === null) && circuit.codePCDepart && this.allPoints?.length) {
                const sp = this.allPoints.find((p) =>
                    p.codePointCollecte?.trim().toLowerCase() === circuit.codePCDepart?.trim().toLowerCase()
                );
                if (sp && this.isValidCoordinate(sp.latitude, sp.longitude)) {
                    lat = this.parseCoord(sp.latitude);
                    lng = this.parseCoord(sp.longitude);
                }
            }
            if (lat !== null && lng !== null) {
                return [{
                    latitude: lat,
                    longitude: lng,
                    label: circuit.libelleCircuit || circuit.codeCircuit,
                    kind: 'departure',
                    order: 0,
                }];
            }
            return [];
        }

        // No `id` here: clicking a route waypoint should not re-trigger a circuit selection
        return located.map((p, index) => ({
            latitude: p.parsedLat!,
            longitude: p.parsedLng!,
            label: p.libellePointCollecte || p.codePointCollecte,
            kind: index === 0 ? 'departure' as const
                : index === located.length - 1 ? 'arrival' as const
                : 'stop' as const,
            order: index,
        }));
    }

    // ------------------------------------------------------------------ //
    //  Quick actions
    // ------------------------------------------------------------------ //

    duplicateCircuit(circuit: Circuit): void {
        if (!this.hasActionPermission(FuseNavigationAction.Add)) {
            return;
        }

        const confirmation = this._fuseConfirmationService.open({
            title: 'Dupliquer le circuit',
            message: `Créer une copie de « ${circuit.codeCircuit} » ? Les points de collecte ne sont pas copiés (un point n'appartient qu'à un seul circuit).`,
            icon: { name: 'heroicons_outline:document-duplicate', color: 'info' },
            actions: { confirm: { label: 'Dupliquer', color: 'primary' } },
        });

        confirmation.afterClosed().subscribe((result) => {
            if (result !== 'confirmed') {
                return;
            }

            const copy: Circuit = {
                ...circuit,
                circuitId: null,
                codeCircuit: `${circuit.codeCircuit}-COPIE`,
                libelleCircuit: circuit.libelleCircuit ? `${circuit.libelleCircuit} (copie)` : '',
                pointCollecteIds: [],
            };

            this._circuitService.AddCircuit(copy).subscribe({
                next: () => this._changeDetectorRef.markForCheck(),
                error: () => {
                    this._fuseConfirmationService.open({
                        title: 'Duplication impossible',
                        message: 'La copie n\'a pas pu être créée (le code existe peut-être déjà).',
                        icon: { name: 'heroicons_outline:exclamation-triangle', color: 'warn' },
                        actions: { confirm: { label: 'OK' }, cancel: { show: false } },
                    });
                },
            });
        });
    }

    deleteSelectedCircuit(circuit: Circuit): void {
        if (!this.hasActionPermission(FuseNavigationAction.Delete)) {
            return;
        }
        const confirmation = this._fuseConfirmationService.open({
            title: 'Supprimer le circuit',
            message: 'Voulez-vous vraiment supprimer ce circuit ? Cette action est irréversible.',
            actions: {
                confirm: {
                    label: 'Supprimer',
                },
            },
        });

        confirmation.afterClosed().subscribe((result) => {
            if (result === 'confirmed') {
                this._circuitService
                    .DeleteCircuit({ circuitId: circuit.circuitId })
                    .subscribe(() => {
                        if (this.selectedCircuit?.circuitId === circuit.circuitId) {
                            this.closeDetails();
                        }
                        this._changeDetectorRef.markForCheck();
                    });
            }
        });
    }

    /** Export the currently displayed circuits as CSV. */
    exportCircuits(circuits: Circuit[]): void {
        const header = ['Code', 'Libellé', 'Description', 'Statut', 'Distance (km)', 'Durée (min)', 'Départ', 'Arrivée'];
        const rows = circuits.map((c) => [
            c.codeCircuit,
            c.libelleCircuit ?? '',
            (c.description ?? '').replace(/[\r\n;]+/g, ' '),
            c.isActive ? 'Actif' : 'Inactif',
            c.distanceKm ?? '',
            c.dureeMinutes ?? '',
            c.codePCDepart ?? '',
            c.codePCArrivee ?? '',
        ]);

        const csv = [header, ...rows].map((r) => r.join(';')).join('\n');
        const blob = new Blob(['﻿' + csv], { type: 'text/csv;charset=utf-8' });
        const url = URL.createObjectURL(blob);
        const link = document.createElement('a');
        link.href = url;
        link.download = `circuits-${new Date().toISOString().slice(0, 10)}.csv`;
        link.click();
        URL.revokeObjectURL(url);
    }

    pointsCountOf(circuit: Circuit): number | null {
        return this.pointsCountByCircuit.get(circuit.circuitId) ?? null;
    }

    trackByCircuit(index: number, item: Circuit): string {
        return item.circuitId ?? String(index);
    }

    ngOnDestroy(): void {
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }

    protected readonly FuseNavigationAction = FuseNavigationAction;

    exportData(): void {
        if (this._circuitService) {
            const obs$ = (this as any).circuits$ || this._circuitService.circuits$ || this._circuitService.circuit$;
            if (obs$) {
                obs$.pipe(take(1)).subscribe((data: any) => {
                    const items = Array.isArray(data) ? data : (data?.items || data?.circuits || data?.circuit || []);
                    if (items && items.length > 0) {
                        const columns = [
            { header: 'Code Circuit', dataKey: 'codeCircuit' },
            { header: 'Libellé', dataKey: 'libelle' },
            { header: 'Distance (km)', dataKey: 'distance' },
            { header: 'Statut', dataKey: 'isActive' }
        ];
                        this._pdfExportService.exportToPdf('Rapport Circuits de Collecte', columns, items, 'Circuits_Export.pdf');
                    } else {
                        console.warn('No data available to export to PDF');
                    }
                });
            }
        }
    }
}
