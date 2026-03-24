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
import { Shift } from '../../../../core/shift/shift.model';
import { catchError, EMPTY, Subject, takeUntil } from 'rxjs';
import { ShiftService } from '../../../../core/shift/shift.service';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { UserService } from '../../../../core/user/user.service';
import { NgxMatTimepickerModule } from 'ngx-mat-timepicker';

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
        NgxMatTimepickerModule,
    ],
  templateUrl: './details.component.html',
  styleUrl: './details.component.scss',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    animations: fuseAnimations,
})
export class DetailsComponent implements OnInit, OnDestroy {
    @ViewChild('shiftFormDirective') shiftFormDirective: FormGroupDirective;
    shiftForm: UntypedFormGroup;
    isNewShift: boolean = false;
    shift: Shift;
    flashMessage: 'success' | 'error' | null = null;
    isLoading: boolean = false;
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    readonly joursSemaine: string[] = [
        'Lundi', 'Mardi', 'Mercredi', 'Jeudi', 'Vendredi', 'Samedi', 'Dimanche'
    ];

    constructor(
        private _activatedRoute: ActivatedRoute,
        private _router: Router,
        private formBuilder: FormBuilder,
        private _shiftService: ShiftService,
        private _changeDetectorRef: ChangeDetectorRef,
        private _userService: UserService
    ) { }

    ngOnInit(): void {

        this.shiftForm = this.formBuilder.group({
            shiftId: [null],
            codeShift: ['', Validators.required],
            libelleShift: [''],
            jourSemaine: [''],
            heureDebut: [''],
            heureFin: [''],
            isActive: [true],
            societeId: ['', Validators.required],
        });

        this._userService.user$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((user) => {
                if (user?.societeId) {
                    this.shiftForm.patchValue({ societeId: user.societeId });
                }
            });

        this._shiftService.shift$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((shift) => {
                this.shift = shift;
                this.isNewShift = !shift?.shiftId;

                if (shift.societeId) {
                    this.shiftForm.patchValue(shift);
                } else {
                    const { societeId, ...shiftWithoutSocieteId } = shift;
                    this.shiftForm.patchValue(shiftWithoutSocieteId);
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

    saveShift(): void {
        if (this.shiftForm.invalid) {
            this.showFlashMessage('error');
            return;
        }
        const shift = this.shiftForm.getRawValue() as Shift;

        if (!this.shift?.shiftId) {
            this._shiftService
                .AddShift(shift)
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

        this._shiftService
            .UpdateShift(shift)
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
