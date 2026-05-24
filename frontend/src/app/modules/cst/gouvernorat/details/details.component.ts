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
import { Gouvernorat } from '../../../../core/gouvernorat/gouvernorat.model';
import { catchError, EMPTY, Subject, takeUntil } from 'rxjs';
import { GouvernoratService } from '../../../../core/gouvernorat/gouvernorat.service';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { UserService } from '../../../../core/user/user.service';

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
    @ViewChild('gouvernoratFormDirective') gouvernoratFormDirective: FormGroupDirective;
    gouvernoratForm: UntypedFormGroup;
    isNewGouvernorat: boolean = false;
    gouvernorat: Gouvernorat;
    flashMessage: 'success' | 'error' | null = null;
    isLoading: boolean = false;
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    constructor(
        private _activatedRoute: ActivatedRoute,
        private _router: Router,
        private formBuilder: FormBuilder,
        private _gouvernoratService: GouvernoratService,
        private _changeDetectorRef: ChangeDetectorRef,
        private _userService: UserService
    ) { }

    ngOnInit(): void {
        this.gouvernoratForm = this.formBuilder.group({
            gouvernoratId: [null],
            codeGouvernorat: ['', Validators.required],
            libelleGouvernorat: [''],
            isActive: [true],
            societeId: ['', Validators.required],
        });

        this._userService.user$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((user) => {
                if (user?.societeId) {
                    this.gouvernoratForm.patchValue({ societeId: user.societeId });
                }
            });

        this._gouvernoratService.gouvernorat$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((gouvernorat) => {
                this.gouvernorat = gouvernorat;
                this.isNewGouvernorat = !gouvernorat?.gouvernoratId;

                if (gouvernorat.societeId) {
                    this.gouvernoratForm.patchValue(gouvernorat);
                } else {
                    const { societeId, ...gouvernoratWithoutSocieteId } = gouvernorat;
                    this.gouvernoratForm.patchValue(gouvernoratWithoutSocieteId);
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

    saveGouvernorat(): void {
        if (this.gouvernoratForm.invalid) {
            this.showFlashMessage('error');
            return;
        }
        const gouvernorat = this.gouvernoratForm.getRawValue() as Gouvernorat;

        if (!this.gouvernorat?.gouvernoratId) {
            this._gouvernoratService
                .AddGouvernorat(gouvernorat)
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

        this._gouvernoratService
            .UpdateGouvernorat(gouvernorat)
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
