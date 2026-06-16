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
import { Chauffeur } from '../../../../core/chauffeur/chauffeur.model';
import { Bus } from '../../../../core/bus/bus.model';
import { catchError, EMPTY, Subject, takeUntil } from 'rxjs';
import { ChauffeurService } from '../../../../core/chauffeur/chauffeur.service';
import { BusService } from '../../../../core/bus/bus.service';
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
    @ViewChild('chauffeurFormDirective') chauffeurFormDirective: FormGroupDirective;
    chauffeurForm: UntypedFormGroup;
    isNewChauffeur: boolean = false;
    chauffeur: Chauffeur;
    buses: Bus[] = [];
    flashMessage: 'success' | 'error' | null = null;
    isLoading: boolean = false;
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    constructor(
        private _activatedRoute: ActivatedRoute,
        private _router: Router,
        private formBuilder: FormBuilder,
        private _chauffeurService: ChauffeurService,
        private _busService: BusService,
        private _changeDetectorRef: ChangeDetectorRef,
        private _userService: UserService
    ) { }

    ngOnInit(): void {
        this.chauffeurForm = this.formBuilder.group({
            chauffeurId: [null],
            codeChauffeur: ['', Validators.required],
            nom: ['', Validators.required],
            prenom: [''],
            cin: [''],
            rfidChauffeur: [''],
            externe: [false],
            isActive: [true],
            societeId: ['', Validators.required],
            busId: [null],
        });

        // Load buses
        this._busService.GetBuses(1, 1000)
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe(pagedBus => {
                this.buses = pagedBus.buses || [];
            });

        this._userService.user$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((user) => {
                if (user?.societeId) {
                    this.chauffeurForm.patchValue({ societeId: user.societeId });
                }
            });

        this._chauffeurService.chauffeur$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((chauffeur) => {
                this.chauffeur = chauffeur;
                this.isNewChauffeur = !chauffeur?.chauffeurId;

                if (chauffeur.societeId) {
                    this.chauffeurForm.patchValue(chauffeur);
                } else {
                    const { societeId, ...chauffeurWithoutSocieteId } = chauffeur;
                    this.chauffeurForm.patchValue(chauffeurWithoutSocieteId);
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

    saveChauffeur(): void {
        if (this.chauffeurForm.invalid) {
            this.showFlashMessage('error');
            return;
        }
        const chauffeur = this.chauffeurForm.getRawValue() as Chauffeur;

        if (!this.chauffeur?.chauffeurId) {
            this._chauffeurService
                .AddChauffeur(chauffeur)
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

        this._chauffeurService
            .UpdateChauffeur(chauffeur)
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
