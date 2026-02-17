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
import { Equipe } from '../../../../core/equipe/equipe.model';
import { catchError, EMPTY, of, Subject, takeUntil } from 'rxjs';
import { EquipeService } from '../../../../core/equipe/equipe.service';
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
        MatDatepickerModule,
        MatDividerModule,
        MatRippleModule,
        MatSlideToggleModule,
        TranslocoModule,
        RouterLink,
    ],
  templateUrl: './details.component.html',
  styleUrl: './details.component.scss',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    animations: fuseAnimations,
})
export class DetailsComponent implements OnInit, OnDestroy {
    @ViewChild('equipeFormDirective') equipeFormDirective: FormGroupDirective;
    equipeForm: UntypedFormGroup;
    isNewEquipe: boolean = false;
    equipe: Equipe;
    flashMessage: 'success' | 'error' | null = null;
    isLoading: boolean = false;
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    constructor(
        private _activatedRoute: ActivatedRoute,
        private _router: Router,
        private formBuilder: FormBuilder,
        private _equipeService: EquipeService,
        private _changeDetectorRef: ChangeDetectorRef,
        private _userService: UserService
    ) { }

    ngOnInit(): void {

        this.equipeForm = this.formBuilder.group({
            equipeId: [null],
            codeEquipe: ['', Validators.required],
            libelleEquipe: [''],
            codeClient: [''],
            codeEntrepot: [''],
            codeTarif: [''],
            codeFournisseur: [''],
            responsable: [''],
            isInternal: [false],
            codeVehicule: [''],
            isActive: [true],
            societeId: ['', Validators.required],
        });

        // Get current user's societeId
        this._userService.user$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((user) => {
                if (user?.societeId) {
                    this.equipeForm.patchValue({ societeId: user.societeId });
                }
            });

        this._equipeService.equipe$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((equipe) => {
                this.equipe = equipe;
                this.isNewEquipe = !equipe?.equipeId;
                
                // Don't overwrite societeId if it's already set from UserService
                // This prevents the empty societeId from new equipe data from overwriting the user's societeId
                if (equipe.societeId) {
                    // If equipe has a societeId (editing existing), use all equipe data
                    this.equipeForm.patchValue(equipe);
                } else {
                    // If equipe doesn't have societeId (new equipe), patch without societeId to preserve UserService value
                    const { societeId, ...equipeWithoutSocieteId } = equipe;
                    this.equipeForm.patchValue(equipeWithoutSocieteId);
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

    saveEquipe(): void {
        if (this.equipeForm.invalid) {
            this.showFlashMessage('error');
            return;
        }
        const equipe = this.equipeForm.getRawValue() as Equipe;

        if (!this.equipe?.equipeId) {
            this._equipeService
                .AddEquipe(equipe)
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

        this._equipeService
            .UpdateEquipe(equipe)
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
