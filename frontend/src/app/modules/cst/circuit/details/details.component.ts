import {
    AfterViewInit,
    ChangeDetectionStrategy, ChangeDetectorRef,
    Component,
    OnDestroy,
    OnInit,
    ViewChild,
    ViewEncapsulation,
} from '@angular/core';
import { TranslocoModule } from '@ngneat/transloco';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroupDirective, ReactiveFormsModule, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { CdkDragDrop, DragDropModule, moveItemInArray } from '@angular/cdk/drag-drop';
import { catchError, EMPTY, finalize, forkJoin, map, Observable, of, Subject, take, takeUntil } from 'rxjs';
import { fuseAnimations } from '../../../../../@fuse/animations';
import { Circuit } from '../../../../core/circuit/circuit.model';
import { CircuitService } from '../../../../core/circuit/circuit.service';
import { UserService } from '../../../../core/user/user.service';
import { MapPickerComponent, MapRoutePoint, PolygonMode } from '../../../../shared/components/map-picker/map-picker.component';
import { MapSkeletonComponent } from '../../../../shared/components/map-skeleton/map-skeleton.component';
import { MapGeocodingService } from '../../../../core/common/map-geocoding.service';
import { PointCollecteService } from '../../../../core/point-collecte/point-collecte.service';
import { PointCollecte } from '../../../../core/point-collecte/point-collecte.model';
import { FuseConfirmationService } from '../../../../../@fuse/services/confirmation';
import {
    DijkstraService,
    GeoPoint,
    OptimizedRoute,
    PolygonStats,
    RouteEvaluation,
    RouteNode,
} from '../../../../core/circuit/dijkstra.service';

@Component({
  selector: 'app-details',
  standalone: true,
    imports: [
        MatIconModule,
        ReactiveFormsModule,
        CommonModule,
        TranslocoModule,
        RouterLink,
        DragDropModule,
        MapPickerComponent,
        MapSkeletonComponent,
    ],
  templateUrl: './details.component.html',
  styleUrl: './details.component.scss',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    animations: fuseAnimations,
})
export class DetailsComponent implements OnInit, OnDestroy, AfterViewInit {
    @ViewChild('circuitFormDirective') circuitFormDirective: FormGroupDirective;
    circuitForm: UntypedFormGroup;
    isNewCircuit: boolean = false;
    circuit: Circuit;
    flashMessage: 'success' | 'error' | null = null;
    isLoading: boolean = false;
    isGeocodingAddresses: boolean = false;
    circuitRoutePoints: MapRoutePoint[] = [];
    departureAddressNotFound: boolean = false;
    arrivalAddressNotFound: boolean = false;
    allPoints: PointCollecte[] = [];
    /** Ordered selection — the index IS the visiting order. */
    selectedPointIds: string[] = [];
    orderedSelectedPoints: PointCollecte[] = [];
    pointSearchControl: UntypedFormControl = new UntypedFormControl('');
    filteredPoints: PointCollecte[] = [];

    // ---- Collapsible cards ----
    infoCollapsed: boolean = false;
    pointsCollapsed: boolean = true;

    // ---- Geographic zone (polygon) ----
    polygonPoints: GeoPoint[] = [];
    polygonMode: PolygonMode = 'none';
    restrictToPolygon: boolean = false;
    polygonStats: PolygonStats | null = null;

    // ---- Route optimization (Dijkstra) ----
    manualEvaluation: RouteEvaluation | null = null;
    optimizedResult: OptimizedRoute | null = null;
    optimizedApplied: boolean = false;

    private departureAddressPoint: MapRoutePoint | null = null;
    private arrivalAddressPoint: MapRoutePoint | null = null;
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    constructor(
        private _activatedRoute: ActivatedRoute,
        private _router: Router,
        private formBuilder: FormBuilder,
        private _circuitService: CircuitService,
        private _pointCollecteService: PointCollecteService,
        private _mapGeocodingService: MapGeocodingService,
        private _dijkstraService: DijkstraService,
        private _changeDetectorRef: ChangeDetectorRef,
        private _userService: UserService,
        private _fuseConfirmationService: FuseConfirmationService
    ) { }

    // ------------------------------------------------------------------ //
    //  Point selection + ordering
    // ------------------------------------------------------------------ //

    togglePointSelection(pointId: string): void {
        const idx = this.selectedPointIds.indexOf(pointId);
        if (idx === -1) {
            this.selectedPointIds = [...this.selectedPointIds, pointId];
        } else {
            this.selectedPointIds = this.selectedPointIds.filter(id => id !== pointId);
        }
        this.onSelectionOrderChanged();
    }

    isPointSelected(pointId: string): boolean {
        return this.selectedPointIds.includes(pointId);
    }

    /** Drag & drop reorder of the visiting order. */
    dropSelectedPoint(event: CdkDragDrop<PointCollecte[]>): void {
        if (event.previousIndex === event.currentIndex) {
            return;
        }
        const ids = [...this.selectedPointIds];
        moveItemInArray(ids, event.previousIndex, event.currentIndex);
        this.selectedPointIds = ids;
        this.onSelectionOrderChanged();
    }

    removeSelectedPoint(pointId: string): void {
        this.selectedPointIds = this.selectedPointIds.filter(id => id !== pointId);
        this.onSelectionOrderChanged();
    }

    /** Any change of selection or order invalidates the last optimization. */
    private onSelectionOrderChanged(): void {
        this.optimizedResult = null;
        this.optimizedApplied = false;
        this.rebuildOrderedPoints();
        this.composeCircuitRoutePoints();
        this._changeDetectorRef.markForCheck();
    }

    private rebuildOrderedPoints(): void {
        this.orderedSelectedPoints = this.selectedPointIds
            .map((id) => this.allPoints.find((p) => p.pointCollecteId === id))
            .filter((p): p is PointCollecte => !!p);
        this.updatePolygonStats();
    }

    /** Return points that should be selectable in the current mode:
     *  - New circuit: only points with no circuit assigned.
     *  - Edit circuit: points with no circuit assigned + points assigned to this circuit.
     */
    private getAssignablePoints(): PointCollecte[] {
        const base = this.allPoints ?? [];
        if (!this.circuit?.circuitId || this.isNewCircuit) {
            return base.filter((p) => !p.circuitId);
        }
        return base.filter((p) => !p.circuitId || p.circuitId === this.circuit.circuitId);
    }

    /** Filter the collection points shown in the list by assignment mode + free-text term. */
    applyPointFilter(term: string | null | undefined): void {
        const assignable = this.getAssignablePoints();
        const q = (term ?? '').toString().trim().toLowerCase();
        this.filteredPoints = q
            ? assignable.filter((p) =>
                  [p.codePointCollecte, p.libellePointCollecte, p.codeRegion, p.codeGouvernorat]
                      .filter(Boolean)
                      .some((v) => v!.toString().toLowerCase().includes(q))
              )
            : assignable;
        this._changeDetectorRef.markForCheck();
    }

    /** Refresh selectable points and current selection after data changes. */
    private refreshPointSelection(): void {
        if (!this.circuit?.circuitId || this.isNewCircuit) {
            this.selectedPointIds = [];
        } else {
            this.selectedPointIds = this.allPoints
                .filter((p) => p.circuitId === this.circuit.circuitId)
                .map((p) => p.pointCollecteId);
        }
        this.applyPointFilter(this.pointSearchControl.value);
        this.rebuildOrderedPoints();
        this.composeCircuitRoutePoints();
    }

    get selectedPointCount(): number {
        return this.selectedPointIds.length;
    }

    toggleInfoCard(): void {
        this.infoCollapsed = !this.infoCollapsed;
        if (!this.infoCollapsed) {
            this.pointsCollapsed = true;
        }
    }

    togglePointsCard(): void {
        this.pointsCollapsed = !this.pointsCollapsed;
        if (!this.pointsCollapsed) {
            this.infoCollapsed = true;
        }
    }

    // ------------------------------------------------------------------ //
    //  Init
    // ------------------------------------------------------------------ //

    ngOnInit(): void {

        this.circuitForm = this.formBuilder.group({
            circuitId: [null],
            codeCircuit: ['', Validators.required],
            libelleCircuit: [''],
            description: [''],
            latitude: [null],
            longitude: [null],
            isActive: [true],
            societeId: ['', Validators.required],
            codePCDepart: ['', Validators.required],
            codePCArrivee: ['', Validators.required],
            distanceKm: [null],
            dureeMinutes: [null],
            couleur: ['#2196F3'],
        });

        // Load all collection points
        this._pointCollecteService.GetPointsCollecte().subscribe(res => {
            this.allPoints = res.pointsCollecte ?? [];
            this.refreshPointSelection();
            this.updateStartEndPointsOnMap();
            this.locateAddressesOnMap();
            this._changeDetectorRef.markForCheck();
        });

        // Filter the collection-point list as the user searches
        this.pointSearchControl.valueChanges
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((term) => this.applyPointFilter(term));

        // Update map when start/end points change
        this.circuitForm.get('codePCDepart').valueChanges
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe(() => this.updateStartEndPointsOnMap());

        this.circuitForm.get('codePCArrivee').valueChanges
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe(() => this.updateStartEndPointsOnMap());

        this.circuitForm.get('couleur').valueChanges
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe(() => this._changeDetectorRef.markForCheck());

        // Get current user's societeId
        this._userService.user$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((user) => {
                if (user?.societeId) {
                    this.circuitForm.patchValue({ societeId: user.societeId });
                }
            });

        this._circuitService.circuit$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((circuit) => {
                this.circuit = circuit;
                this.isNewCircuit = !circuit?.circuitId;

                // Don't overwrite societeId if it's already set from UserService
                if (circuit.societeId) {
                    this.circuitForm.patchValue(circuit);
                } else {
                    const { societeId, ...circuitWithoutSocieteId } = circuit;
                    this.circuitForm.patchValue(circuitWithoutSocieteId);
                }

                this.refreshPointSelection();
                this.locateAddressesOnMap();

                this._changeDetectorRef.markForCheck();
            });

    }

    updateStartEndPointsOnMap(): void {
        const startCode = this.circuitForm.get('codePCDepart').value;
        const endCode = this.circuitForm.get('codePCArrivee').value;

        const startPoint = this.allPoints.find(p => p.codePointCollecte === startCode);
        const endPoint = this.allPoints.find(p => p.codePointCollecte === endCode);

        this.departureAddressPoint = startPoint?.latitude != null ? {
            latitude: Number(startPoint.latitude),
            longitude: Number(startPoint.longitude),
            label: `Départ : ${startPoint.libellePointCollecte || startPoint.codePointCollecte}`,
            kind: 'departure',
        } : null;

        this.arrivalAddressPoint = endPoint?.latitude != null ? {
            latitude: Number(endPoint.latitude),
            longitude: Number(endPoint.longitude),
            label: `Arrivée : ${endPoint.libellePointCollecte || endPoint.codePointCollecte}`,
            kind: 'arrival',
        } : null;

        // A resolved point clears any stale "address not found" error (e.g. set
        // by an early geocoding attempt that ran before the points were loaded)
        if (this.departureAddressPoint) {
            this.departureAddressNotFound = false;
            this.setAddressNotFoundError('codePCDepart', false);
        }
        if (this.arrivalAddressPoint) {
            this.arrivalAddressNotFound = false;
            this.setAddressNotFoundError('codePCArrivee', false);
        }

        this.optimizedResult = null;
        this.optimizedApplied = false;
        this.composeCircuitRoutePoints();
        this._changeDetectorRef.markForCheck();
    }

    // ------------------------------------------------------------------ //
    //  Geographic zone (polygon)
    // ------------------------------------------------------------------ //

    startPolygonDraw(): void {
        this.polygonPoints = [];
        this.polygonMode = 'draw';
        this.updatePolygonStats();
        this._changeDetectorRef.markForCheck();
    }

    editPolygon(): void {
        this.polygonMode = this.polygonMode === 'edit' ? 'none' : 'edit';
        this._changeDetectorRef.markForCheck();
    }

    finishPolygon(): void {
        this.polygonMode = 'none';
        this._changeDetectorRef.markForCheck();
    }

    clearPolygon(): void {
        this.polygonPoints = [];
        this.polygonMode = 'none';
        this.restrictToPolygon = false;
        this.updatePolygonStats();
        this.composeCircuitRoutePoints();
        this._changeDetectorRef.markForCheck();
    }

    onPolygonChange(points: GeoPoint[]): void {
        this.polygonPoints = points;
        this.optimizedResult = null;
        this.optimizedApplied = false;
        this.updatePolygonStats();
        this.composeCircuitRoutePoints();
        this._changeDetectorRef.markForCheck();
    }

    onPolygonDrawFinished(points: GeoPoint[]): void {
        this.polygonPoints = points;
        this.polygonMode = 'edit';
        this.updatePolygonStats();
        this.composeCircuitRoutePoints();
        this._changeDetectorRef.markForCheck();
    }

    toggleRestrictToPolygon(): void {
        this.restrictToPolygon = !this.restrictToPolygon;
        this.optimizedResult = null;
        this.optimizedApplied = false;
        this.composeCircuitRoutePoints();
        this._changeDetectorRef.markForCheck();
    }

    get hasPolygon(): boolean {
        return this.polygonPoints.length >= 3;
    }

    isPointOutsideZone(point: PointCollecte): boolean {
        if (!this.hasPolygon || point.latitude == null || point.longitude == null) {
            return false;
        }
        return !this._dijkstraService.isPointInPolygon(
            { latitude: Number(point.latitude), longitude: Number(point.longitude) },
            this.polygonPoints
        );
    }

    private updatePolygonStats(): void {
        if (!this.hasPolygon) {
            this.polygonStats = null;
            return;
        }
        const locatedSelected = this.orderedSelectedPoints
            .filter((p) => p.latitude != null && p.longitude != null)
            .map((p) => ({ latitude: Number(p.latitude), longitude: Number(p.longitude) }));

        this.polygonStats = this._dijkstraService.polygonStats(this.polygonPoints, locatedSelected);
    }

    // ------------------------------------------------------------------ //
    //  Route optimization (Dijkstra)
    // ------------------------------------------------------------------ //

    get canOptimize(): boolean {
        return !!this.departureAddressPoint
            && !!this.arrivalAddressPoint
            && this.orderedSelectedPoints.some((p) => p.latitude != null && p.longitude != null);
    }

    optimizeRoute(): void {
        if (!this.canOptimize) {
            return;
        }

        const start: RouteNode = {
            id: '__start__',
            latitude: this.departureAddressPoint!.latitude,
            longitude: this.departureAddressPoint!.longitude,
        };
        const end: RouteNode = {
            id: '__end__',
            latitude: this.arrivalAddressPoint!.latitude,
            longitude: this.arrivalAddressPoint!.longitude,
        };
        const waypoints: RouteNode[] = this.orderedSelectedPoints
            .filter((p) => p.latitude != null && p.longitude != null)
            .map((p) => ({
                id: p.pointCollecteId,
                latitude: Number(p.latitude),
                longitude: Number(p.longitude),
                label: p.libellePointCollecte || p.codePointCollecte,
            }));

        this.manualEvaluation = this._dijkstraService.evaluateOrder(start, waypoints, end);
        this.optimizedResult = this._dijkstraService.optimizeRoute({
            start,
            end,
            waypoints,
            polygon: this.hasPolygon ? this.polygonPoints : undefined,
            restrictToPolygon: this.restrictToPolygon && this.hasPolygon,
        });
        this.optimizedApplied = false;
        this._changeDetectorRef.markForCheck();
    }

    get optimizationGainPct(): number | null {
        if (!this.manualEvaluation || !this.optimizedResult || this.manualEvaluation.totalDistanceKm === 0) {
            return null;
        }
        const gain = (1 - this.optimizedResult.totalDistanceKm / this.manualEvaluation.totalDistanceKm) * 100;
        return Math.round(gain * 10) / 10;
    }

    /** Apply the Dijkstra order: reorder the selection and fill distance/duration. */
    applyOptimizedOrder(): void {
        if (!this.optimizedResult) {
            return;
        }

        const optimizedIds = this.optimizedResult.orderedWaypointIds;
        // Points without coordinates (or excluded from the zone) keep their spot at the end
        const rest = this.selectedPointIds.filter((id) => !optimizedIds.includes(id));
        this.selectedPointIds = [...optimizedIds, ...rest];

        this.circuitForm.patchValue({
            distanceKm: Math.round(this.optimizedResult.totalDistanceKm * 10) / 10,
            dureeMinutes: this.optimizedResult.estimatedDurationMinutes,
        });

        this.rebuildOrderedPoints();
        this.composeCircuitRoutePoints();
        this.optimizedApplied = true;
        this._changeDetectorRef.markForCheck();
    }

    dismissOptimization(): void {
        this.optimizedResult = null;
        this.optimizedApplied = false;
        this._changeDetectorRef.markForCheck();
    }

    // ------------------------------------------------------------------ //
    //  Save / delete / navigation
    // ------------------------------------------------------------------ //

    onBackdropClicked(): void {
        this._router.navigate(['./'], { relativeTo: this._activatedRoute.parent });
        this._changeDetectorRef.markForCheck();
    }

    resetForm(): void {
        const confirmation = this._fuseConfirmationService.open({
            title: 'Réinitialiser le formulaire',
            message: 'Les modifications non enregistrées seront perdues. Continuer ?',
            icon: { name: 'heroicons_outline:arrow-path', color: 'warn' },
            actions: { confirm: { label: 'Réinitialiser', color: 'warn' } },
        });

        confirmation.afterClosed().subscribe((result) => {
            if (result !== 'confirmed') {
                return;
            }
            this.circuitForm.patchValue(this.circuit ?? {});
            this.clearPolygon();
            this.refreshPointSelection();
            this.updateStartEndPointsOnMap();
            this._changeDetectorRef.markForCheck();
        });
    }

    deleteCircuit(): void {
        if (!this.circuit?.circuitId) {
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
                    .DeleteCircuit({ circuitId: this.circuit.circuitId })
                    .subscribe(() => {
                        this.onBackdropClicked();
                    });
            }
        });
    }

    showFlashMessage(type: 'success' | 'error'): void {
        this.flashMessage = type;
        this._changeDetectorRef.markForCheck();
        setTimeout(() => {
            this.flashMessage = null;
            this._changeDetectorRef.markForCheck();
        }, 8000);
    }

    saveCircuit(): void {
        if (this.circuitForm.invalid) {
            this.circuitForm.markAllAsTouched();
            this.showFlashMessage('error');
            return;
        }
        this.resolveAddressPoints(true)
            .pipe(take(1))
            .subscribe((isValid) => {
                if (!isValid) {
                    this.showFlashMessage('error');
                    return;
                }

                const circuit = this.circuitForm.getRawValue() as Circuit;
                circuit.pointCollecteIds = this.selectedPointIds;

                this.isLoading = true;

                if (!this.circuit?.circuitId) {
                    this._circuitService
                        .AddCircuit(circuit)
                        .pipe(
                            catchError(() => {
                                this.showFlashMessage('error');
                                return EMPTY;
                            }),
                            finalize(() => {
                                this.isLoading = false;
                                this._changeDetectorRef.markForCheck();
                            })
                        )
                        .subscribe(() => {
                            this.showFlashMessage('success');
                            setTimeout(() => {
                                this.onBackdropClicked();
                            }, 1200);
                        });

                    return;
                }

                this._circuitService
                    .UpdateCircuit(circuit)
                    .pipe(
                        catchError(() => {
                            this.showFlashMessage('error');
                            return EMPTY;
                        }),
                        finalize(() => {
                            this.isLoading = false;
                            this._changeDetectorRef.markForCheck();
                        })
                    )
                    .subscribe((val) => {
                        this.showFlashMessage(val ? 'success' : 'error');
                    });
            });

    }

    onLocationChange(location: { latitude: number; longitude: number }): void {
        this.circuitForm.patchValue({
            latitude: location.latitude,
            longitude: location.longitude,
        });
        this.composeCircuitRoutePoints();
        this._changeDetectorRef.markForCheck();
    }

    locateAddressesOnMap(): void {
        this.resolveAddressPoints(false)
            .pipe(take(1))
            .subscribe(() => {
                this._changeDetectorRef.markForCheck();
            });
    }

    // ------------------------------------------------------------------ //
    //  Departure / arrival resolution
    // ------------------------------------------------------------------ //

    private resolveAddressPoints(requireBoth: boolean): Observable<boolean> {
        // Departure/arrival are picked among the collection points; resolving
        // before they are loaded would geocode raw codes and flag false errors.
        if (!this.allPoints?.length) {
            return of(!requireBoth);
        }

        const departureCode = (this.circuitForm.get('codePCDepart')?.value ?? '').trim();
        const arrivalCode = (this.circuitForm.get('codePCArrivee')?.value ?? '').trim();

        if (requireBoth && (!departureCode || !arrivalCode)) {
            this.departureAddressNotFound = false;
            this.arrivalAddressNotFound = false;
            return of(false);
        }

        this.isGeocodingAddresses = true;

        return forkJoin({
            departure: this.resolveCodeOrAddress(departureCode),
            arrival: this.resolveCodeOrAddress(arrivalCode),
        }).pipe(
            map(({ departure, arrival }) => {
                this.departureAddressPoint = departure
                    ? { ...departure, label: `Départ : ${departureCode}`, kind: 'departure' as const }
                    : null;
                this.arrivalAddressPoint = arrival
                    ? { ...arrival, label: `Arrivée : ${arrivalCode}`, kind: 'arrival' as const }
                    : null;

                this.departureAddressNotFound = !!departureCode && !this.departureAddressPoint;
                this.arrivalAddressNotFound = !!arrivalCode && !this.arrivalAddressPoint;

                this.setAddressNotFoundError('codePCDepart', this.departureAddressNotFound);
                this.setAddressNotFoundError('codePCArrivee', this.arrivalAddressNotFound);

                this.composeCircuitRoutePoints();
                return requireBoth
                    ? !!this.departureAddressPoint && !!this.arrivalAddressPoint
                    : !this.departureAddressNotFound && !this.arrivalAddressNotFound;
            }),
            catchError(() => {
                this.departureAddressNotFound = !!departureCode;
                this.arrivalAddressNotFound = !!arrivalCode;
                this.setAddressNotFoundError('codePCDepart', this.departureAddressNotFound);
                this.setAddressNotFoundError('codePCArrivee', this.arrivalAddressNotFound);
                this.composeCircuitRoutePoints();
                return of(false);
            }),
            finalize(() => {
                this.isGeocodingAddresses = false;
                this._changeDetectorRef.markForCheck();
            })
        );
    }

    /** Resolve a departure/arrival value: a known point code first, geocoding as fallback. */
    private resolveCodeOrAddress(value: string): Observable<{ latitude: number; longitude: number } | null> {
        if (!value) {
            return of(null);
        }

        const known = this.allPoints.find(
            (p) => p.codePointCollecte === value && p.latitude != null && p.longitude != null
        );
        if (known) {
            return of({ latitude: Number(known.latitude), longitude: Number(known.longitude) });
        }

        return this._mapGeocodingService.searchAddress(value).pipe(
            map((r) => (r ? { latitude: r.latitude, longitude: r.longitude } : null))
        );
    }

    private setAddressNotFoundError(controlName: string, hasAddressNotFound: boolean): void {
        const control = this.circuitForm.get(controlName);
        if (!control) {
            return;
        }

        const errors = control.errors ?? {};
        if (hasAddressNotFound) {
            control.setErrors({ ...errors, addressNotFound: true });
            return;
        }

        if (!errors['addressNotFound']) {
            return;
        }

        const { addressNotFound, ...remainingErrors } = errors;
        control.setErrors(Object.keys(remainingErrors).length > 0 ? remainingErrors : null);
    }

    // ------------------------------------------------------------------ //
    //  Map route composition
    // ------------------------------------------------------------------ //

    private composeCircuitRoutePoints(): void {
        const stops: MapRoutePoint[] = this.orderedSelectedPoints
            .filter((p) => p.latitude != null && p.longitude != null)
            .map((p, index) => ({
                latitude: Number(p.latitude),
                longitude: Number(p.longitude),
                label: `${index + 1}. ${p.libellePointCollecte || p.codePointCollecte}`,
                kind: 'stop' as const,
                order: index + 1,
                outsideZone: this.restrictToPolygon && this.isPointOutsideZone(p),
            }));

        const routePoints: MapRoutePoint[] = [];
        if (this.departureAddressPoint) {
            routePoints.push(this.departureAddressPoint);
        }
        routePoints.push(...stops);
        if (this.arrivalAddressPoint) {
            routePoints.push(this.arrivalAddressPoint);
        }

        if (routePoints.length === 0) {
            const latitude = this.circuitForm?.get('latitude')?.value;
            const longitude = this.circuitForm?.get('longitude')?.value;
            if (latitude != null && longitude != null) {
                routePoints.push({
                    latitude,
                    longitude,
                    label: this.circuitForm.get('codeCircuit')?.value || 'Circuit',
                    kind: 'stop',
                    order: 1,
                });
            }
        }

        this.circuitRoutePoints = routePoints;

        // Live evaluation of the current (manual) order for the stats chips
        if (this.departureAddressPoint && this.arrivalAddressPoint && stops.length > 0) {
            this.manualEvaluation = this._dijkstraService.evaluateOrder(
                { id: '__start__', latitude: this.departureAddressPoint.latitude, longitude: this.departureAddressPoint.longitude },
                stops.map((s, i) => ({ id: String(i), latitude: s.latitude, longitude: s.longitude })),
                { id: '__end__', latitude: this.arrivalAddressPoint.latitude, longitude: this.arrivalAddressPoint.longitude }
            );
        } else {
            this.manualEvaluation = null;
        }
    }

    ngOnDestroy(): void {
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }

    ngAfterViewInit(): void {
        this.locateAddressesOnMap();
    }
}
