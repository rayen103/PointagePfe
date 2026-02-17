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
import { PointCollecte } from '../../../../core/point-collecte/point-collecte.model';
import { catchError, EMPTY, of, Subject, takeUntil } from 'rxjs';
import { PointCollecteService } from '../../../../core/point-collecte/point-collecte.service';
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
    @ViewChild('pointCollecteFormDirective') pointCollecteFormDirective: FormGroupDirective;
    pointCollecteForm: UntypedFormGroup;
    isNewPointCollecte: boolean = false;
    pointCollecte: PointCollecte;
    flashMessage: 'success' | 'error' | null = null;
    isLoading: boolean = false;
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    constructor(
        private _activatedRoute: ActivatedRoute,
        private _router: Router,
        private formBuilder: FormBuilder,
        private _pointCollecteService: PointCollecteService,
        private _changeDetectorRef: ChangeDetectorRef,
        private _userService: UserService
    ) { }

    ngOnInit(): void {

        this.pointCollecteForm = this.formBuilder.group({
            pointCollecteId: [null],
            codePointCollecte: ['', Validators.required],
            libellePointCollecte: [''],
            latitude: [null],
            longitude: [null],
            codeGouvernorat: [''],
            codeRegion: [''],
            isActive: [true],
            societeId: ['', Validators.required],
        });

        // Get current user's societeId
        this._userService.user$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((user) => {
                if (user?.societeId) {
                    this.pointCollecteForm.patchValue({ societeId: user.societeId });
                }
            });

        this._pointCollecteService.pointCollecte$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((pointCollecte) => {
                this.pointCollecte = pointCollecte;
                this.isNewPointCollecte = !pointCollecte?.pointCollecteId;
                
                // Don't overwrite societeId if it's already set from UserService
                // This prevents the empty societeId from new pointCollecte data from overwriting the user's societeId
                if (pointCollecte.societeId) {
                    // If pointCollecte has a societeId (editing existing), use all pointCollecte data
                    this.pointCollecteForm.patchValue(pointCollecte);
                } else {
                    // If pointCollecte doesn't have societeId (new pointCollecte), patch without societeId to preserve UserService value
                    const { societeId, ...pointCollecteWithoutSocieteId } = pointCollecte;
                    this.pointCollecteForm.patchValue(pointCollecteWithoutSocieteId);
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

    savePointCollecte(): void {
        if (this.pointCollecteForm.invalid) {
            this.showFlashMessage('error');
            return;
        }
        const pointCollecte = this.pointCollecteForm.getRawValue() as PointCollecte;

        if (!this.pointCollecte?.pointCollecteId) {
            this._pointCollecteService
                .AddPointCollecte(pointCollecte)
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

        this._pointCollecteService
            .UpdatePointCollecte(pointCollecte)
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
