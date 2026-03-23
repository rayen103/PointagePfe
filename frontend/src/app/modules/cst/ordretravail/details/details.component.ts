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
import { OrdreTravail } from '../../../../core/ordre-travail/ordre-travail.model';
import { OrdreTravailDetail } from '../../../../core/ordre-travail/ordre-travail-detail.model';
import { OrdreTravailDetailService } from '../../../../core/ordre-travail/ordre-travail-detail.service';
import { catchError, EMPTY, Subject, takeUntil } from 'rxjs';
import { OrdreTravailService } from '../../../../core/ordre-travail/ordre-travail.service';
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
    @ViewChild('ordreTravailFormDirective') ordreTravailFormDirective: FormGroupDirective;
    ordreTravailForm: UntypedFormGroup;
    newDetailForm: UntypedFormGroup;
    isNewOrdreTravail: boolean = false;
    ordreTravail: OrdreTravail;
    ordreTravailDetails: OrdreTravailDetail[] = [];
    flashMessage: 'success' | 'error' | null = null;
    isLoading: boolean = false;
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    constructor(
        private _activatedRoute: ActivatedRoute,
        private _router: Router,
        private formBuilder: FormBuilder,
        private _ordreTravailService: OrdreTravailService,
        private _ordreTravailDetailService: OrdreTravailDetailService,
        private _changeDetectorRef: ChangeDetectorRef,
        private _userService: UserService
    ) { }

    ngOnInit(): void {

        this.ordreTravailForm = this.formBuilder.group({
            ordreTravailId: [null],
            numeroOrdreTravail: ['', Validators.required],
            numeroChantier: [''],
            codeClient: [''],
            numeroBonCommande: [''],
            codeEquipe: [''],
            etatOT: [''],
            montant: [null],
            dateCreation: [null],
            numeroConvention: [''],
            codeVehicule: [''],
            libelle: [''],
            isActive: [true],
            societeId: ['', Validators.required],
        });

        this.newDetailForm = this.formBuilder.group({
            codeArticle: ['', Validators.required],
            libelleArticle: [''],
            codeEntrepot: [''],
            codeUnite: [''],
            quantite: [null],
            prixUnitaireHT: [null],
            tauxTVA: [null],
            montant: [null],
        });

        // Get current user's societeId
        this._userService.user$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((user) => {
                if (user?.societeId) {
                    this.ordreTravailForm.patchValue({ societeId: user.societeId });
                }
            });

        this._ordreTravailService.ordreTravail$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((ordreTravail) => {
                this.ordreTravail = ordreTravail;
                this.isNewOrdreTravail = !ordreTravail?.ordreTravailId;
                
                // Don't overwrite societeId if it's already set from UserService
                // This prevents the empty societeId from new ordreTravail data from overwriting the user's societeId
                if (ordreTravail.societeId) {
                    // If ordreTravail has a societeId (editing existing), use all ordreTravail data
                    this.ordreTravailForm.patchValue(ordreTravail);
                } else {
                    // If ordreTravail doesn't have societeId (new ordreTravail), patch without societeId to preserve UserService value
                    const { societeId, ...ordreTravailWithoutSocieteId } = ordreTravail;
                    this.ordreTravailForm.patchValue(ordreTravailWithoutSocieteId);
                }

                this._changeDetectorRef.markForCheck();
            });

        // Load line items for existing records
        this._ordreTravailService.ordreTravail$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((ordreTravail) => {
                if (ordreTravail?.ordreTravailId) {
                    this._ordreTravailDetailService.getByOrdreTravail(ordreTravail.ordreTravailId)
                        .pipe(takeUntil(this._unsubscribeAll))
                        .subscribe((details) => {
                            this.ordreTravailDetails = details ?? [];
                            this._changeDetectorRef.markForCheck();
                        });
                } else {
                    this.ordreTravailDetails = [];
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

    saveOrdreTravail(): void {
        if (this.ordreTravailForm.invalid) {
            this.showFlashMessage('error');
            return;
        }
        const ordreTravail = this.ordreTravailForm.getRawValue() as OrdreTravail;

        if (!this.ordreTravail?.ordreTravailId) {
            this._ordreTravailService
                .AddOrdreTravail(ordreTravail)
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

        this._ordreTravailService
            .UpdateOrdreTravail(ordreTravail)
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

    addDetail(): void {
        if (this.newDetailForm.invalid || !this.ordreTravail?.ordreTravailId) {
            return;
        }
        const detail = {
            ordreTravailId: this.ordreTravail.ordreTravailId,
            ...this.newDetailForm.getRawValue(),
        };
        this._ordreTravailDetailService.add(detail)
            .pipe(
                catchError(() => {
                    this.showFlashMessage('error');
                    return EMPTY;
                })
            )
            .subscribe((created) => {
                if (created) {
                    this.ordreTravailDetails = [...this.ordreTravailDetails, created];
                    this.newDetailForm.reset();
                    this._changeDetectorRef.markForCheck();
                }
            });
    }

    removeDetail(id: string): void {
        this._ordreTravailDetailService.delete(id)
            .pipe(
                catchError(() => {
                    this.showFlashMessage('error');
                    return EMPTY;
                })
            )
            .subscribe((success) => {
                if (success) {
                    this.ordreTravailDetails = this.ordreTravailDetails.filter(
                        d => d.ordreTravailDetailId !== id
                    );
                    this._changeDetectorRef.markForCheck();
                }
            });
    }

    ngOnDestroy(): void {
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }
}
