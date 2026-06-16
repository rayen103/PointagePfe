import {
    AfterViewInit,
    ChangeDetectionStrategy, ChangeDetectorRef,
    Component,
    inject,
    OnDestroy,
    OnInit,
    ViewChild,
    ViewEncapsulation,
} from '@angular/core';
import { TranslocoModule } from '@ngneat/transloco';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { MatOptionModule, MatRippleModule } from '@angular/material/core';
import { MatDividerModule } from '@angular/material/divider';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatSelectModule } from '@angular/material/select';
import { FormBuilder, FormGroupDirective, ReactiveFormsModule, UntypedFormGroup, Validators } from '@angular/forms';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { catchError, EMPTY, finalize, forkJoin, map, Observable, of, Subject, switchMap, take, takeUntil } from 'rxjs';
import { fuseAnimations } from '../../../../../@fuse/animations';
import { Circuit } from '../../../../core/circuit/circuit.model';
import { CircuitService } from '../../../../core/circuit/circuit.service';
import { CircuitPointCollecteService } from '../../../../core/circuit/circuit-point-collecte.service';
import { CircuitPointCollecte } from '../../../../core/circuit/circuit-point-collecte.model';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { UserService } from '../../../../core/user/user.service';
import { MapPickerComponent, MapRoutePoint } from '../../../../shared/components/map-picker/map-picker.component';
import { MatTableModule } from '@angular/material/table';
import { MapGeocodingService } from '../../../../core/common/map-geocoding.service';
import { PointCollecteService } from '../../../../core/point-collecte/point-collecte.service';
import { PointCollecte } from '../../../../core/point-collecte/point-collecte.model';
import { SelectionModel } from '@angular/cdk/collections';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { FuseConfirmationService } from '../../../../../@fuse/services/confirmation';
import { FuseNavigationAction } from '../../../../../@fuse/components/navigation';

@Component({
  selector: 'app-details',
  standalone: true,
    imports: [
        MatButtonModule,
        MatFormFieldModule,
        MatIconModule,
        MatInputModule,
        MatProgressBarModule,
        ReactiveFormsModule,
        MatOptionModule,
        MatSelectModule,
        MatCardModule,
        CommonModule,
        MatDatepickerModule,
        MatDividerModule,
        MatRippleModule,
        MatSlideToggleModule,
        TranslocoModule,
        RouterLink,
        MapPickerComponent,
        MatTableModule,
        MatCheckboxModule,
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
    newPointForm: UntypedFormGroup;
    isNewCircuit: boolean = false;
    circuit: Circuit;
    circuitPoints: CircuitPointCollecte[] = [];
    flashMessage: 'success' | 'error' | null = null;
    isLoading: boolean = false;
    isGeocodingAddresses: boolean = false;
    circuitRoutePoints: MapRoutePoint[] = [];
    departureAddressNotFound: boolean = false;
    arrivalAddressNotFound: boolean = false;
    allPoints: PointCollecte[] = [];
    selection = new SelectionModel<PointCollecte>(true, []);
    displayedColumns: string[] = ['select', 'ordre', 'code', 'label', 'coords'];
    
    private departureAddressPoint: MapRoutePoint | null = null;
    private arrivalAddressPoint: MapRoutePoint | null = null;
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    constructor(
        private _activatedRoute: ActivatedRoute,
        private _router: Router,
        private formBuilder: FormBuilder,
        private _circuitService: CircuitService,
        private _circuitPointCollecteService: CircuitPointCollecteService,
        private _pointCollecteService: PointCollecteService,
        private _mapGeocodingService: MapGeocodingService,
        private _changeDetectorRef: ChangeDetectorRef,
        private _userService: UserService,
        private _fuseConfirmationService: FuseConfirmationService
    ) { }

    hasActionPermission(action: FuseNavigationAction): boolean {
        return true;
    }

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
            codePCDepart: [''],
            codePCArrivee: [''],
            distanceKm: [null],
            dureeMinutes: [null],
            couleur: ['#2196F3'],
        });

        // Load all collection points
        this._pointCollecteService.GetPointsCollecte().subscribe(res => {
            this.allPoints = res.pointsCollecte;
            this._changeDetectorRef.markForCheck();
        });

        this.circuitForm.get('codePCDepart')?.addValidators([Validators.required]);
        this.circuitForm.get('codePCArrivee')?.addValidators([Validators.required]);
        
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

        this.newPointForm = this.formBuilder.group({
            codePointCollecte: ['', Validators.required],
            libellePointCollecte: [''],
            ordre: [null],
            latitude: [null],
            longitude: [null],
        });

        // Get current user's societeId
        this._userService.user$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((user) => {
                console.log('UserService user data:', user);
                if (user?.societeId) {
                    console.log('Setting societeId from user:', user.societeId);
                    this.circuitForm.patchValue({ societeId: user.societeId });
                } else {
                    console.warn('User does not have societeId!', user);
                }
            });

        this._circuitService.circuit$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((circuit) => {
                console.log('CircuitService circuit data:', circuit);
                this.circuit = circuit;
                this.isNewCircuit = !circuit?.circuitId;
                
                // Don't overwrite societeId if it's already set from UserService
                // This prevents the empty societeId from new circuit data from overwriting the user's societeId
                if (circuit.societeId) {
                    console.log('Circuit has societeId, patching all data');
                    // If circuit has a societeId (editing existing), use all circuit data
                    this.circuitForm.patchValue(circuit);
                } else {
                    console.log('Circuit has no societeId, preserving form societeId');
                    // If circuit doesn't have societeId (new circuit), patch without societeId to preserve UserService value
                    const { societeId, ...circuitWithoutSocieteId } = circuit;
                    this.circuitForm.patchValue(circuitWithoutSocieteId);
                }
                
                console.log('Form societeId after patch:', this.circuitForm.get('societeId').value);
                this.locateAddressesOnMap();

                this._changeDetectorRef.markForCheck();
            });

        // Load waypoints for existing circuits
        this._circuitService.circuit$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((circuit) => {
                if (circuit?.circuitId) {
                    this._circuitPointCollecteService.getByCircuit(circuit.circuitId)
                        .pipe(takeUntil(this._unsubscribeAll))
                        .subscribe((points) => {
                            this.circuitPoints = points ?? [];
                            this.composeCircuitRoutePoints();
                            this._changeDetectorRef.markForCheck();
                        });
                } else {
                    this.circuitPoints = [];
                    this.composeCircuitRoutePoints();
                }
            });

    }

    updateStartEndPointsOnMap(): void {
        const startCode = this.circuitForm.get('codePCDepart').value;
        const endCode = this.circuitForm.get('codePCArrivee').value;
        
        const startPoint = this.allPoints.find(p => p.codePointCollecte === startCode);
        const endPoint = this.allPoints.find(p => p.codePointCollecte === endCode);
        
        this.departureAddressPoint = startPoint ? {
            latitude: Number(startPoint.latitude),
            longitude: Number(startPoint.longitude),
            label: `Departure: ${startPoint.libellePointCollecte || startPoint.codePointCollecte}`
        } : null;
        
        this.arrivalAddressPoint = endPoint ? {
            latitude: Number(endPoint.latitude),
            longitude: Number(endPoint.longitude),
            label: `Arrival: ${endPoint.libellePointCollecte || endPoint.codePointCollecte}`
        } : null;
        
        this.composeCircuitRoutePoints();
        this._changeDetectorRef.markForCheck();
    }

    /** Whether the number of selected elements matches the total number of rows. */
    isAllSelected(): boolean {
        const numSelected = this.selection.selected.length;
        const numRows = this.allPoints.length;
        return numSelected === numRows;
    }

    /** Selects all rows if they are not all selected; otherwise clear selection. */
    masterToggle(): void {
        this.isAllSelected() ?
            this.selection.clear() :
            this.allPoints.forEach(row => this.selection.select(row));
    }

    onBackdropClicked(): void {
        // Go back to the list
        this._router.navigate(['./'], { relativeTo: this._activatedRoute.parent });

        // Mark for check
        this._changeDetectorRef.markForCheck();
    }

    deleteCircuit(): void {
        if (!this.hasActionPermission || !this.circuit?.circuitId) {
            return;
        }

        const confirmation = this._fuseConfirmationService.open({
            title: 'Delete Circuit',
            message:
                'Are you sure you want to remove this circuit? This action cannot be undone!',
            actions: {
                confirm: {
                    label: 'Delete',
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
            console.log('Form is invalid:', this.circuitForm.errors);
            console.log('Form values:', this.circuitForm.value);
            console.log('Form controls status:', {
                codeCircuit: this.circuitForm.get('codeCircuit')?.errors,
                societeId: this.circuitForm.get('societeId')?.errors,
            });
            // Show an error message
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
                console.log('Saving circuit:', circuit);

                if (!this.circuit?.circuitId) {
                    this._circuitService
                        .AddCircuit(circuit)
                        .pipe(
                            switchMap((createdCircuit) => {
                                console.log('Circuit added successfully:', createdCircuit);
                                if (this.circuitPoints.length > 0) {
                                    // Save all temporary points
                                    const pointObservables = this.circuitPoints.map((point, index) => {
                                        const newPoint: CircuitPointCollecte = {
                                            ...point,
                                            circuitId: createdCircuit.circuitId,
                                            ordre: index + 1
                                        };
                                        return this._circuitPointCollecteService.add(newPoint);
                                    });
                                    return forkJoin(pointObservables).pipe(
                                        map(() => createdCircuit)
                                    );
                                }
                                return of(createdCircuit);
                            }),
                            catchError((error) => {
                                console.error('Error adding circuit or points:', error);
                                this.showFlashMessage('error');
                                return EMPTY;
                            })
                        )
                        .subscribe(() => {
                            this.showFlashMessage('success');
                            setTimeout(() => {
                                this.onBackdropClicked();
                            }, 1500);
                        });

                    return;
                }

                this._circuitService
                    .UpdateCircuit(circuit)
                    .pipe(
                        catchError((error) => {
                            console.error('Error updating circuit:', error);
                            this.showFlashMessage('error');
                            return EMPTY;
                        })
                    )
                    .subscribe((val) => {
                        if (val) {
                            this.showFlashMessage('success');
                            return;
                        }

                        this.showFlashMessage('error');
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

    /**
     * Add selected points to circuit
     */
    addSelectedPoints(): void {
        if (!this.selection.hasValue()) return;

        const selectedPoints = this.selection.selected;
        const circuitId = this.circuit.circuitId;

        if (circuitId) {
            // Existing circuit: add via API
            const observables = selectedPoints.map((point, index) => {
                const newPoint: CircuitPointCollecte = {
                    circuitPointCollecteId: null,
                    circuitId: circuitId,
                    codePointCollecte: point.codePointCollecte,
                    libellePointCollecte: point.libellePointCollecte,
                    ordre: this.circuitPoints.length + index + 1,
                    latitude: point.latitude,
                    longitude: point.longitude
                };
                return this._circuitPointCollecteService.add(newPoint);
            });

            forkJoin(observables).subscribe(() => {
                this.selection.clear();
                // Reload circuit points
                this._circuitPointCollecteService.getByCircuit(circuitId).subscribe(points => {
                    this.circuitPoints = points ?? [];
                    this.composeCircuitRoutePoints();
                    this._changeDetectorRef.markForCheck();
                });
            });
        } else {
            // New circuit: add temporarily
            selectedPoints.forEach((point, index) => {
                const newPoint: CircuitPointCollecte = {
                    circuitPointCollecteId: null,
                    circuitId: null,
                    codePointCollecte: point.codePointCollecte,
                    libellePointCollecte: point.libellePointCollecte,
                    ordre: this.circuitPoints.length + index + 1,
                    latitude: point.latitude,
                    longitude: point.longitude
                };
                this.circuitPoints.push(newPoint);
            });
            this.selection.clear();
            this.composeCircuitRoutePoints();
            this._changeDetectorRef.markForCheck();
        }
    }

    addWaypoint(): void {
        if (this.newPointForm.invalid || !this.circuit?.circuitId) {
            return;
        }
        const point: CircuitPointCollecte = {
            circuitPointCollecteId: null,
            circuitId: this.circuit.circuitId,
            ...this.newPointForm.getRawValue(),
        };
        this._circuitPointCollecteService.add(point)
            .pipe(
                catchError(() => {
                    this.showFlashMessage('error');
                    return EMPTY;
                })
            )
            .subscribe((created) => {
                if (created) {
                    this.circuitPoints = [...this.circuitPoints, created];
                    this.newPointForm.reset();
                    this.composeCircuitRoutePoints();
                    this._changeDetectorRef.markForCheck();
                }
            });
    }

    removeWaypoint(point: CircuitPointCollecte): void {
        const id = point.circuitPointCollecteId;
        if (id) {
            this._circuitPointCollecteService.delete(id)
                .pipe(
                    catchError(() => {
                        this.showFlashMessage('error');
                        return EMPTY;
                    })
                )
                .subscribe((success) => {
                    if (success) {
                        this.circuitPoints = this.circuitPoints.filter(p => p.circuitPointCollecteId !== id);
                        this.composeCircuitRoutePoints();
                        this._changeDetectorRef.markForCheck();
                    }
                });
        } else {
            this.circuitPoints = this.circuitPoints.filter(p => p !== point);
            this.composeCircuitRoutePoints();
            this._changeDetectorRef.markForCheck();
        }
    }

    private resolveAddressPoints(requireBoth: boolean): Observable<boolean> {
        const departureAddress = (this.circuitForm.get('codePCDepart')?.value ?? '').trim();
        const arrivalAddress = (this.circuitForm.get('codePCArrivee')?.value ?? '').trim();

        if (requireBoth && (!departureAddress || !arrivalAddress)) {
            this.departureAddressNotFound = false;
            this.arrivalAddressNotFound = false;
            return of(false);
        }

        this.isGeocodingAddresses = true;

        const departure$ = departureAddress
            ? this._mapGeocodingService.searchAddress(departureAddress)
            : of(null);
        const arrival$ = arrivalAddress
            ? this._mapGeocodingService.searchAddress(arrivalAddress)
            : of(null);

        return forkJoin({
            departure: departure$,
            arrival: arrival$,
        }).pipe(
            map(({ departure, arrival }) => {
                this.departureAddressPoint = departure
                    ? {
                        latitude: departure.latitude,
                        longitude: departure.longitude,
                        label: `Departure: ${departureAddress}`,
                    }
                    : null;
                this.arrivalAddressPoint = arrival
                    ? {
                        latitude: arrival.latitude,
                        longitude: arrival.longitude,
                        label: `Arrival: ${arrivalAddress}`,
                    }
                    : null;

                this.departureAddressNotFound = !!departureAddress && !this.departureAddressPoint;
                this.arrivalAddressNotFound = !!arrivalAddress && !this.arrivalAddressPoint;

                this.setAddressNotFoundError('codePCDepart', this.departureAddressNotFound);
                this.setAddressNotFoundError('codePCArrivee', this.arrivalAddressNotFound);

                this.composeCircuitRoutePoints();
                return requireBoth
                    ? !!this.departureAddressPoint && !!this.arrivalAddressPoint
                    : !this.departureAddressNotFound && !this.arrivalAddressNotFound;
            }),
            catchError(() => {
                this.departureAddressNotFound = !!departureAddress;
                this.arrivalAddressNotFound = !!arrivalAddress;
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

    private composeCircuitRoutePoints(): void {
        const waypointPoints: MapRoutePoint[] = [...(this.circuitPoints ?? [])]
            .sort((a, b) => (a.ordre ?? 0) - (b.ordre ?? 0))
            .filter((point) => point.latitude != null && point.longitude != null)
            .map((point) => ({
                latitude: point.latitude!,
                longitude: point.longitude!,
                label: point.libellePointCollecte || point.codePointCollecte,
            }));

        const routePoints: MapRoutePoint[] = [];
        if (this.departureAddressPoint) {
            routePoints.push(this.departureAddressPoint);
        }

        routePoints.push(...waypointPoints);

        if (this.arrivalAddressPoint) {
            routePoints.push(this.arrivalAddressPoint);
        }

        if (routePoints.length === 0) {
            const latitude = this.circuitForm.get('latitude')?.value;
            const longitude = this.circuitForm.get('longitude')?.value;
            if (latitude != null && longitude != null) {
                routePoints.push({
                    latitude,
                    longitude,
                    label: this.circuitForm.get('codeCircuit')?.value || 'Circuit',
                });
            }
        }

        this.circuitRoutePoints = routePoints;
    }

    ngOnDestroy(): void {
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }

    ngAfterViewInit(): void {
        this.locateAddressesOnMap();
    }
}
