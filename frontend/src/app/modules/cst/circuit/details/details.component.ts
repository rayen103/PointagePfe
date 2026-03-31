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
import { Circuit } from '../../../../core/circuit/circuit.model';
import { CircuitPointCollecte } from '../../../../core/circuit/circuit-point-collecte.model';
import { CircuitPointCollecteService } from '../../../../core/circuit/circuit-point-collecte.service';
import { catchError, EMPTY, of, Subject, takeUntil } from 'rxjs';
import { CircuitService } from '../../../../core/circuit/circuit.service';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { UserService } from '../../../../core/user/user.service';
import { MapPickerComponent } from '../../../../shared/components/map-picker/map-picker.component';
import { MatTableModule } from '@angular/material/table';

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
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    constructor(
        private _activatedRoute: ActivatedRoute,
        private _router: Router,
        private formBuilder: FormBuilder,
        private _circuitService: CircuitService,
        private _circuitPointCollecteService: CircuitPointCollecteService,
        private _changeDetectorRef: ChangeDetectorRef,
        private _userService: UserService
    ) { }

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
            couleur: [''],
        });

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
                            this._changeDetectorRef.markForCheck();
                        });
                } else {
                    this.circuitPoints = [];
                }
            });

    }

    onBackdropClicked(): void {
        // Go back to the list
        this._router.navigate(['./'], { relativeTo: this._activatedRoute.parent });

        // Mark for check
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
        const circuit = this.circuitForm.getRawValue() as Circuit;
        console.log('Saving circuit:', circuit);

        if (!this.circuit?.circuitId) {
            this._circuitService
                .AddCircuit(circuit)
                .pipe(
                    catchError((error) => {
                        console.error('Error adding circuit:', error);
                        this.showFlashMessage('error');
                        return EMPTY;
                    })
                )
                .subscribe((response) => {
                    console.log('Circuit added successfully:', response);
                    this.showFlashMessage('success');
                    // Navigate back to list after successful creation
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

    }

    onLocationChange(location: { latitude: number; longitude: number }): void {
        this.circuitForm.patchValue({
            latitude: location.latitude,
            longitude: location.longitude,
        });
        this._changeDetectorRef.markForCheck();
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
                    this._changeDetectorRef.markForCheck();
                }
            });
    }

    removeWaypoint(id: string): void {
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
                    this._changeDetectorRef.markForCheck();
                }
            });
    }

    ngOnDestroy(): void {
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }

    ngAfterViewInit(): void {

    }
}
