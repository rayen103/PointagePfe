import {
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
import { Modem } from '../../../../core/modem/modem.model';
import { catchError, EMPTY, Subject, takeUntil } from 'rxjs';
import { ModemService } from '../../../../core/modem/modem.service';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { UserService } from '../../../../core/user/user.service';
import { BusService } from '../../../../core/bus/bus.service';

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
        MatDividerModule,
        MatRippleModule,
        MatSlideToggleModule,
        TranslocoModule,
        RouterLink,
    ],
    templateUrl: './details.component.html',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    animations: fuseAnimations,
})
export class DetailsComponent implements OnInit, OnDestroy {
    @ViewChild('modemFormDirective') modemFormDirective: FormGroupDirective;
    modemForm: UntypedFormGroup;
    isNewModem: boolean = false;
    modem: Modem;
    flashMessage: 'success' | 'error' | null = null;
    isLoading: boolean = false;
    buses: any[] = [];
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    constructor(
        private _activatedRoute: ActivatedRoute,
        private _router: Router,
        private formBuilder: FormBuilder,
        private _modemService: ModemService,
        private _changeDetectorRef: ChangeDetectorRef,
        private _userService: UserService,
        private _busService: BusService
    ) { }

    ngOnInit(): void {
        this.modemForm = this.formBuilder.group({
            modemId: [null],
            imei: ['', Validators.required],
            modelModem: [''],
            numeroSim: [''],
            isActive: [true],
            societeId: ['', Validators.required],
            busId: [''],
        });

        this._busService.GetBuses(1, 1000, 'numeroIMM', 'asc', '').subscribe();
        this._busService.buses$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((buses) => {
                this.buses = buses || [];
                this._changeDetectorRef.markForCheck();
            });

        this._userService.user$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((user) => {
                if (user?.societeId) {
                    this.modemForm.patchValue({ societeId: user.societeId });
                }
            });

        this._modemService.modem$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((modem) => {
                this.modem = modem;
                this.isNewModem = !modem?.modemId;

                if (modem.societeId) {
                    this.modemForm.patchValue(modem);
                } else {
                    const { societeId, ...modemWithoutSocieteId } = modem;
                    this.modemForm.patchValue(modemWithoutSocieteId);
                }

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

    saveModem(): void {
        if (this.modemForm.invalid) {
            this.showFlashMessage('error');
            return;
        }
        const modem = this.modemForm.getRawValue() as Modem;

        if (!this.modem?.modemId) {
            this._modemService
                .AddModem(modem)
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

        this._modemService
            .UpdateModem(modem)
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

    ngOnDestroy(): void {
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }
}
