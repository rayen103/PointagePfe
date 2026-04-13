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
import { catchError, EMPTY, Subject, takeUntil } from 'rxjs';
import { BusService } from '../../../../core/bus/bus.service';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { UserService } from '../../../../core/user/user.service';
import { MapPickerComponent } from '../../../../shared/components/map-picker/map-picker.component';
import { Circuit } from '../../../../core/circuit/circuit.model';
import { CircuitService } from '../../../../core/circuit/circuit.service';

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
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    constructor(
        private _activatedRoute: ActivatedRoute,
        private _router: Router,
        private formBuilder: FormBuilder,
        private _busService: BusService,
        private _circuitService: CircuitService,
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
            imei: [''],
            capacite: [null],
            codeCircuit: [''],
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

                this._circuitService
                    .GetCircuit()
                    .pipe(takeUntil(this._unsubscribeAll))
                    .subscribe((pagedCircuits) => {
                        const allCircuits = pagedCircuits?.circuits ?? [];
                        this.circuits = user?.societeId
                            ? allCircuits.filter((c) => c.societeId === user.societeId)
                            : allCircuits;
                        this._changeDetectorRef.markForCheck();
                    });
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
        const bus = this.busForm.getRawValue() as Bus;

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

    ngOnDestroy(): void {
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }
}
