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
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { MatOptionModule } from '@angular/material/core';
import { CommonModule } from '@angular/common';
import { MatSelectModule } from '@angular/material/select';
import { FormBuilder, FormGroupDirective, ReactiveFormsModule, UntypedFormGroup, Validators } from '@angular/forms';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { fuseAnimations } from '../../../../../../@fuse/animations';
import { Subject, debounceTime, distinctUntilChanged, of, takeUntil, switchMap, finalize, catchError, EMPTY } from 'rxjs';
import { Employe } from '../../../../../core/employes/employe.model';
import { EmployeService } from '../../../../../core/employes/employe.service';
import { Circuit } from '../../../../../core/circuit/circuit.model';
import { Bus } from '../../../../../core/bus/bus.model';
import { Shift } from '../../../../../core/shift/shift.model';
import { PointCollecte } from '../../../../../core/point-collecte/point-collecte.model';
import { Gouvernorat } from '../../../../../core/gouvernorat/gouvernorat.model';
import { Region } from '../../../../../core/region/region.model';
import { PagedSociete, Societe } from '../../../../../core/Societe/societe.model';
import { SocieteService } from '../../../../../core/Societe/societe.service';
import { CircuitService } from '../../../../../core/circuit/circuit.service';
import { BusService } from '../../../../../core/bus/bus.service';
import { ShiftService } from '../../../../../core/shift/shift.service';
import { PointCollecteService } from '../../../../../core/point-collecte/point-collecte.service';
import { GouvernoratService } from '../../../../../core/gouvernorat/gouvernorat.service';
import { RegionService } from '../../../../../core/region/region.service';
import { FuseConfirmationService } from '../../../../../../@fuse/services/confirmation';

@Component({
    selector: 'app-details',
    standalone: true,
    imports: [
        CommonModule,
        ReactiveFormsModule,
        MatButtonModule,
        MatFormFieldModule,
        MatIconModule,
        MatInputModule,
        MatOptionModule,
        MatSelectModule,
        MatSlideToggleModule,
        MatAutocompleteModule,
        MatProgressBarModule,
        TranslocoModule,
        RouterLink,
    ],
    templateUrl: './details.component.html',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    animations: fuseAnimations,
})
export class DetailsComponent implements OnInit, OnDestroy {
    @ViewChild('employeFormDirective') employeFormDirective: FormGroupDirective;
    form: UntypedFormGroup;
    isNew: boolean = false;
    employe: Employe;
    societe: Societe[] = [];
    filteredSocietes$: any;
    circuits: Circuit[] = [];
    buses: Bus[] = [];
    shifts: Shift[] = [];
    pointsCollecte: PointCollecte[] = [];
    gouvernorats: Gouvernorat[] = [];
    regions: Region[] = [];
    saveClicked = false;
    flashMessage: 'success' | 'error' | null = null;
    isLoading: boolean = false;
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    constructor(
        private _activatedRoute: ActivatedRoute,
        private _router: Router,
        private _formBuilder: FormBuilder,
        private _employeService: EmployeService,
        private _societeService: SocieteService,
        private _circuitService: CircuitService,
        private _busService: BusService,
        private _shiftService: ShiftService,
        private _pointCollecteService: PointCollecteService,
        private _gouvernoratService: GouvernoratService,
        private _regionService: RegionService,
        private _changeDetectorRef: ChangeDetectorRef,
        private _fuseConfirmationService: FuseConfirmationService
    ) {}

    ngOnInit(): void {
        this.form = this._formBuilder.group({
            employeId: ['new'],
            matricule: ['', [Validators.required, Validators.maxLength(50)]],
            rfid: ['', [Validators.maxLength(50)]],
            nom: ['', [Validators.required, Validators.maxLength(100)]],
            prenom: ['', [Validators.required, Validators.maxLength(100)]],
            codeCircuit: ['', [Validators.maxLength(50)]],
            codePointCollecte: ['', [Validators.maxLength(50)]],
            codeBus: ['', [Validators.maxLength(50)]],
            codeShift: ['', [Validators.maxLength(50)]],
            adresse: ['', [Validators.maxLength(255)]],
            codeGouvernorat: ['', [Validators.maxLength(50)]],
            codeRegion: ['', [Validators.maxLength(50)]],
            latitude: [null, [Validators.min(-90), Validators.max(90)]],
            longitude: [null, [Validators.min(-180), Validators.max(180)]],
            societeId: ['', [Validators.required]],
            isActive: [true, [Validators.required]]
        });

        // Load referenced data collections
        this._societeService.GetSociete()
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((pagedSociete: PagedSociete) => {
                this.societe = pagedSociete.societes || [];
                this._changeDetectorRef.markForCheck();
            });

        this._circuitService.GetCircuit()
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((pagedCircuit) => {
                this.circuits = pagedCircuit?.circuits ?? [];
                this._changeDetectorRef.markForCheck();
            });

        this._busService.GetBuses()
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((pagedBus) => {
                this.buses = pagedBus?.buses ?? [];
                this._changeDetectorRef.markForCheck();
            });

        this._shiftService.GetShifts()
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((pagedShift) => {
                this.shifts = pagedShift?.shifts ?? [];
                this._changeDetectorRef.markForCheck();
            });

        this._pointCollecteService.GetPointsCollecte()
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((pagedPoint) => {
                this.pointsCollecte = pagedPoint?.pointsCollecte ?? [];
                this._changeDetectorRef.markForCheck();
            });

        this._gouvernoratService.GetGouvernorats()
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((pagedGouv) => {
                this.gouvernorats = pagedGouv?.gouvernorats ?? [];
                this._changeDetectorRef.markForCheck();
            });

        this._regionService.GetRegions()
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((pagedRegion) => {
                this.regions = pagedRegion?.regions ?? [];
                this._changeDetectorRef.markForCheck();
            });

        this.filteredSocietes$ = this.form.get('societeId')!.valueChanges.pipe(
            debounceTime(300),
            distinctUntilChanged(),
            takeUntil(this._unsubscribeAll),
            switchMap((value) =>
                this._societeService.GetSociete(1, 20, '', 'asc', value || '')
            ),
            switchMap((res: PagedSociete) => of(res.societes || []))
        );

        // Load the active employee from route data
        this._activatedRoute.data
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((data) => {
                this.employe = data.employe;
                this.isNew = !this.employe || this.employe.employeId === 'new';

                if (this.employe) {
                    this.form.patchValue(this.employe);
                } else {
                    this.form.reset({
                        employeId: 'new',
                        isActive: true
                    });
                }
                this._changeDetectorRef.markForCheck();
            });
    }

    displaySociete = (societeId: string) => {
        const found = this.societe.find((s) => s.societeId === societeId);
        return found ? found.nom : '';
    };

    getFilteredCircuits(): Circuit[] {
        const societeId = this.form?.get('societeId')?.value;
        if (!societeId) {
            return this.circuits;
        }
        return this.circuits.filter((circuit) => circuit.societeId === societeId);
    }

    getFilteredBuses(): Bus[] {
        const societeId = this.form?.get('societeId')?.value;
        if (!societeId) {
            return this.buses;
        }
        return this.buses.filter((bus) => bus.societeId === societeId);
    }

    getFilteredShifts(): Shift[] {
        const societeId = this.form?.get('societeId')?.value;
        let filtered = this.shifts;

        if (societeId) {
            filtered = filtered.filter((shift) => shift.societeId === societeId);
        }

        const uniqueShifts: Shift[] = [];
        const codes = new Set<string>();

        filtered.forEach(shift => {
            if (!codes.has(shift.codeShift)) {
                codes.add(shift.codeShift);
                uniqueShifts.push(shift);
            }
        });

        return uniqueShifts;
    }

    getFilteredPointsCollecte(): PointCollecte[] {
        const societeId = this.form?.get('societeId')?.value;
        let filtered = this.pointsCollecte;

        if (societeId) {
            filtered = filtered.filter((point) => point.societeId === societeId);
        }

        return filtered;
    }

    hasExistingCircuitOption(codeCircuit: string | null | undefined): boolean {
        if (!codeCircuit) {
            return false;
        }
        return this.getFilteredCircuits().some((circuit) => circuit.codeCircuit === codeCircuit);
    }

    hasExistingBusOption(codeBus: string | null | undefined): boolean {
        if (!codeBus) {
            return false;
        }
        return this.getFilteredBuses().some((bus) => bus.numeroIMM === codeBus);
    }

    hasExistingShiftOption(codeShift: string | null | undefined): boolean {
        if (!codeShift) {
            return false;
        }
        return this.getFilteredShifts().some((shift) => shift.codeShift === codeShift);
    }

    hasExistingPointOption(codePoint: string | null | undefined): boolean {
        if (!codePoint) {
            return false;
        }
        return this.getFilteredPointsCollecte().some((point) => point.codePointCollecte === codePoint);
    }

    hasExistingGouvOption(codeGouv: string | null | undefined): boolean {
        if (!codeGouv) {
            return false;
        }
        return this.gouvernorats.some((gouv) => gouv.codeGouvernorat === codeGouv);
    }

    hasExistingRegionOption(codeRegion: string | null | undefined): boolean {
        if (!codeRegion) {
            return false;
        }
        return this.regions.some((reg) => reg.codeRegion === codeRegion);
    }

    showFlashMessage(type: 'success' | 'error'): void {
        this.flashMessage = type;
        this._changeDetectorRef.markForCheck();
        setTimeout(() => {
            this.flashMessage = null;
            this._changeDetectorRef.markForCheck();
        }, 8000);
    }

    onBackdropClicked(): void {
        this._router.navigate(['../'], { relativeTo: this._activatedRoute });
        this._changeDetectorRef.markForCheck();
    }

    save(): void {
        if (this.form.invalid) {
            this.showFlashMessage('error');
            this.form.markAllAsTouched();
            return;
        }

        if (!this.validateBusCircuitAssignment()) {
            return;
        }

        this.saveClicked = true;
        this.isLoading = true;
        this._changeDetectorRef.markForCheck();

        const employe = this.form.getRawValue();

        if (this.isNew) {
            this._employeService.AddEmploye(employe)
                .pipe(
                    catchError(() => {
                        this.showFlashMessage('error');
                        return EMPTY;
                    }),
                    finalize(() => {
                        this.saveClicked = false;
                        this.isLoading = false;
                        this._changeDetectorRef.markForCheck();
                    })
                )
                .subscribe(() => {
                    this.showFlashMessage('success');
                    setTimeout(() => {
                        this.onBackdropClicked();
                    }, 1500);
                });
        } else {
            this._employeService.UpdateEmploye(employe)
                .pipe(
                    catchError(() => {
                        this.showFlashMessage('error');
                        return EMPTY;
                    }),
                    finalize(() => {
                        this.saveClicked = false;
                        this.isLoading = false;
                        this._changeDetectorRef.markForCheck();
                    })
                )
                .subscribe((success) => {
                    if (success) {
                        this.showFlashMessage('success');
                        setTimeout(() => {
                            this.onBackdropClicked();
                        }, 1500);
                    } else {
                        this.showFlashMessage('error');
                    }
                });
        }
    }

    private validateBusCircuitAssignment(): boolean {
        return true;
    }

    ngOnDestroy(): void {
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }
}
