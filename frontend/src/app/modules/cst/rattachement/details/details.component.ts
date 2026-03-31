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
import { Rattachement } from '../../../../core/rattachement/rattachement.model';
import { catchError, EMPTY, of, Subject, takeUntil } from 'rxjs';
import { RattachementService } from '../../../../core/rattachement/rattachement.service';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { UserService } from '../../../../core/user/user.service';
import { MatExpansionModule } from '@angular/material/expansion';
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
        MatExpansionModule,
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
    @ViewChild('rattachementFormDirective') rattachementFormDirective: FormGroupDirective;
    rattachementForm: UntypedFormGroup;
    isNewRattachement: boolean = false;
    rattachement: Rattachement;
    flashMessage: 'success' | 'error' | null = null;
    isLoading: boolean = false;
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    constructor(
        private _activatedRoute: ActivatedRoute,
        private _router: Router,
        private formBuilder: FormBuilder,
        private _rattachementService: RattachementService,
        private _changeDetectorRef: ChangeDetectorRef,
        private _userService: UserService
    ) { }

    ngOnInit(): void {

        this.rattachementForm = this.formBuilder.group({
            rattachementId: [null],
            numeroRattachement: ['', Validators.required],
            exercice: [''],
            dateRattachement: [null, Validators.required],
            numeroChantier: [''],
            codeClient: [''],
            isInternal: [false],
            cout: [null],
            type: [''],
            nature: [''],
            responsable: [''],
            heureDebut: [''],
            heureFin: [''],
            emplacement: [''],
            reference: [''],
            status: [''],
            dateCloture: [null],
            remarque: [''],
            isActive: [true],
            societeId: ['', Validators.required],
        });

        // Get current user's societeId
        this._userService.user$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((user) => {
                if (user?.societeId) {
                    this.rattachementForm.patchValue({ societeId: user.societeId });
                }
            });

        this._rattachementService.rattachement$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((rattachement) => {
                this.rattachement = rattachement;
                this.isNewRattachement = !rattachement?.rattachementId;

                // Don't overwrite societeId if it's already set from UserService
                // This prevents the empty societeId from new rattachement data from overwriting the user's societeId
                if (rattachement.societeId) {
                    // If rattachement has a societeId (editing existing), use all rattachement data
                    this.rattachementForm.patchValue(rattachement);
                } else {
                    // If rattachement doesn't have societeId (new rattachement), patch without societeId to preserve UserService value
                    const { societeId, ...rattachementWithoutSocieteId } = rattachement;
                    this.rattachementForm.patchValue(rattachementWithoutSocieteId);
                }

                this._changeDetectorRef.markForCheck();
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

    saveRattachement(): void {
        if (this.rattachementForm.invalid) {
            this.showFlashMessage('error');
            return;
        }
        const rattachement = this.rattachementForm.getRawValue() as Rattachement;

        if (!this.rattachement?.rattachementId) {
            this._rattachementService
                .AddRattachement(rattachement)
                .pipe(
                    catchError((error) => {
                        this.showFlashMessage('error');
                        return EMPTY;
                    })
                )
                .subscribe((response) => {
                    this.showFlashMessage('success');
                    // Navigate back to list after successful creation
                    setTimeout(() => {
                        this.onBackdropClicked();
                    }, 1500);
                });

            return;
        }

        this._rattachementService
            .UpdateRattachement(rattachement)
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
