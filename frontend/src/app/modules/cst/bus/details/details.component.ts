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
import { fuseAnimations } from '../../../../../@fuse/animations';
import { Bus } from '../../../../core/bus/bus.model';
import { catchError, EMPTY, Observable, of, Subject, switchMap, takeUntil, distinctUntilChanged, startWith, map, take, forkJoin } from 'rxjs';
import { BusService } from '../../../../core/bus/bus.service';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { UserService } from '../../../../core/user/user.service';
import { MapPickerComponent, MapRoutePoint } from '../../../../shared/components/map-picker/map-picker.component';
import { Circuit } from '../../../../core/circuit/circuit.model';
import { CircuitService } from '../../../../core/circuit/circuit.service';
import { CircuitPointCollecteService } from '../../../../core/circuit/circuit-point-collecte.service';
import { MapGeocodingService } from '../../../../core/common/map-geocoding.service';
import { Modem } from '../../../../core/modem/modem.model';
import { ModemService } from '../../../../core/modem/modem.service';
import { Chauffeur } from '../../../../core/chauffeur/chauffeur.model';
import { ChauffeurService } from '../../../../core/chauffeur/chauffeur.service';

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
    ],
  templateUrl: './details.component.html',
  styleUrl: './details.component.scss',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    animations: fuseAnimations,
})
export class DetailsComponent implements OnInit, OnDestroy, AfterViewInit {
    @ViewChild('busFormDirective') busFormDirective: FormGroupDirective;
    busForm: UntypedFormGroup;
    isNewBus: boolean = false;
    bus: Bus;
    flashMessage: 'success' | 'error' | null = null;
    isLoading: boolean = false;
    mapLatitude: number | null = null;
    mapLongitude: number | null = null;
    circuits: Circuit[] = [];
    modems: Modem[] = [];
    chauffeurs: Chauffeur[] = [];
    departurePoint: string = '';
    arrivalPoint: string = '';
    circuitRoutePoints: MapRoutePoint[] = [];
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    constructor(
        private _activatedRoute: ActivatedRoute,
        private _router: Router,
        private formBuilder: FormBuilder,
        private _busService: BusService,
        private _circuitService: CircuitService,
        private _circuitPointCollecteService: CircuitPointCollecteService,
        private _mapGeocodingService: MapGeocodingService,
        private _modemService: ModemService,
        private _chauffeurService: ChauffeurService,
        private _changeDetectorRef: ChangeDetectorRef,
        private _userService: UserService
    ) { }

    ngAfterViewInit(): void {
        // no-op: map picker initializes itself
    }

    ngOnInit(): void {

        this.busForm = this.formBuilder.group({
            busId: [null],
            numeroIMM: ['', Validators.required],
            modelBus: [''],
            marque: ['Mercedes-Benz'],
            annee: [2022],
            kilometrage: ['128 450 km'],
            imei: [''],
            capacite: [null, [Validators.required, Validators.min(1)]],
            codeCircuit: [''],
            codeChauffeur: [''],
            appSagem: [false],
            isActive: [true],
            latitude: [null],
            longitude: [null],
            societeId: ['', Validators.required],
        });

        this._userService.user$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((user) => {
                if (user?.societeId) {
                    this.busForm.patchValue({ societeId: user.societeId });
                }
            });

        this._circuitService
            .GetCircuit()
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((pagedCircuits) => {
                this.circuits = pagedCircuits?.circuits ?? [];
                this.refreshSelectedCircuitRoute();
                this._changeDetectorRef.markForCheck();
            });

        this._modemService
            .GetModems()
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((pagedModems) => {
                this.modems = (pagedModems?.modems ?? []).filter((m) => m.isActive);
                this._changeDetectorRef.markForCheck();
            });

        this._chauffeurService
            .GetChauffeurs()
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((pagedChauffeurs) => {
                this.chauffeurs = (pagedChauffeurs?.chauffeurs ?? []).filter((c) => c.isActive);
                this._changeDetectorRef.markForCheck();
            });

        this.busForm.get('codeCircuit')?.valueChanges
            .pipe(
                startWith(this.busForm.get('codeCircuit')?.value),
                distinctUntilChanged(),
                switchMap((codeCircuit: string | null) => this.onCircuitChanged(codeCircuit)),
                catchError(() => of([])),
                takeUntil(this._unsubscribeAll)
            )
            .subscribe((routePoints) => {
                this.circuitRoutePoints = routePoints;
                this._changeDetectorRef.markForCheck();
            });

        this._busService.bus$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((bus) => {
                this.bus = bus;
                this.isNewBus = !bus?.busId;

                if (bus.societeId) {
                    this.busForm.patchValue(bus);
                } else {
                    const { societeId, ...busWithoutSocieteId } = bus;
                    this.busForm.patchValue(busWithoutSocieteId);
                }

                this.mapLatitude = bus.latitude ?? null;
                this.mapLongitude = bus.longitude ?? null;

                this._changeDetectorRef.markForCheck();
            });

    }

    onBackdropClicked(): void {
        this._router.navigate(['./'], { relativeTo: this._activatedRoute.parent });
        this._changeDetectorRef.markForCheck();
    }

    showFlashMessage(type: 'success' | 'error'): void {
        this.flashMessage = type;
        this._changeDetectorRef.markForCheck();
        setTimeout(() => {
            this.flashMessage = null;
            this._changeDetectorRef.markForCheck();
        }, 8000);
    }

    saveBus(): void {
        if (this.busForm.invalid) {
            this.showFlashMessage('error');
            return;
        }
        const rawValue = this.busForm.getRawValue();
        const bus = { ...rawValue } as any;
        delete bus.marque;
        delete bus.annee;
        delete bus.kilometrage;

        if (!this.bus?.busId) {
            this._busService
                .AddBus(bus)
                .pipe(
                    catchError((error) => {
                        this.showFlashMessage('error');
                        return EMPTY;
                    })
                )
                .subscribe((response) => {
                    this.showFlashMessage('success');
                    setTimeout(() => {
                        this.onBackdropClicked();
                    }, 1500);
                });

            return;
        }

        this._busService
            .UpdateBus(bus)
            .pipe(
                catchError((error) => {
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

    }

    onLocationChange(event: { latitude: number; longitude: number }): void {
        this.busForm.patchValue({ latitude: event.latitude, longitude: event.longitude });
        this.mapLatitude = event.latitude;
        this.mapLongitude = event.longitude;
    }

    hasExistingCircuit(codeCircuit: string | null | undefined): boolean {
        if (!codeCircuit) {
            return false;
        }

        return this.circuits.some((circuit) => circuit.codeCircuit === codeCircuit);
    }

    private onCircuitChanged(codeCircuit: string | null | undefined): Observable<MapRoutePoint[]> {
        if (!codeCircuit) {
            this.departurePoint = '';
            this.arrivalPoint = '';
            return of<MapRoutePoint[]>([]);
        }

        const selectedCircuit = this.circuits.find((circuit) => circuit.codeCircuit === codeCircuit);
        this.departurePoint = selectedCircuit?.codePCDepart ?? '';
        this.arrivalPoint = selectedCircuit?.codePCArrivee ?? '';

        if (!selectedCircuit?.circuitId) {
            return of<MapRoutePoint[]>([]);
        }

        return this._circuitPointCollecteService.getByCircuit(selectedCircuit.circuitId)
            .pipe(
                catchError(() => of([])),
                switchMap((points) => {
                    const orderedPoints = [...points]
                        .sort((a, b) => (a.ordre ?? 0) - (b.ordre ?? 0))
                        .filter((p) => p.latitude != null && p.longitude != null)
                        .map((point) => ({
                            latitude: point.latitude!,
                            longitude: point.longitude!,
                            label: point.libellePointCollecte || point.codePointCollecte,
                        }));

                    const departureAddress = (selectedCircuit.codePCDepart ?? '').trim();
                    const arrivalAddress = (selectedCircuit.codePCArrivee ?? '').trim();
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
                            const routePoints: MapRoutePoint[] = [];
                            if (departure) {
                                routePoints.push({
                                    latitude: departure.latitude,
                                    longitude: departure.longitude,
                                    label: `Departure: ${departureAddress}`,
                                });
                            }

                            routePoints.push(...orderedPoints);

                            if (arrival) {
                                routePoints.push({
                                    latitude: arrival.latitude,
                                    longitude: arrival.longitude,
                                    label: `Arrival: ${arrivalAddress}`,
                                });
                            }

                            if (routePoints.length > 0) {
                                return routePoints;
                            }

                            if (selectedCircuit.latitude != null && selectedCircuit.longitude != null) {
                                return [
                                    {
                                        latitude: selectedCircuit.latitude,
                                        longitude: selectedCircuit.longitude,
                                        label: selectedCircuit.codeCircuit,
                                    },
                                ];
                            }

                            return [];
                        })
                    );
                })
            );
    }

    private refreshSelectedCircuitRoute(): void {
        const codeCircuitControl = this.busForm.get('codeCircuit');
        if (!codeCircuitControl) {
            return;
        }

        this.onCircuitChanged(codeCircuitControl.value)
            .pipe(
                take(1),
                catchError(() => of([]))
            )
            .subscribe((routePoints) => {
                this.circuitRoutePoints = routePoints;
                this._changeDetectorRef.markForCheck();
            });
    }

    ngOnDestroy(): void {
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }
}
