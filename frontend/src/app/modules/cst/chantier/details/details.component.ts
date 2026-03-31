import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { TranslocoModule } from '@ngneat/transloco';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { MatOptionModule } from '@angular/material/core';
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
import { Chantier } from '../../../../core/chantier/chantier.model';
import { ChantierService } from '../../../../core/chantier/chantier.service';
import { catchError, EMPTY, Subject, takeUntil } from 'rxjs';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { UserService } from '../../../../core/user/user.service';

@Component({
    selector: 'app-details',
    standalone: true,
    imports: [MatButtonModule, MatFormFieldModule, MatIconModule, MatInputModule, MatProgressBarModule,
        ReactiveFormsModule, MatOptionModule, MatSelectModule, MatCardModule, CommonModule,
        MatDatepickerModule, MatDividerModule, MatSlideToggleModule, TranslocoModule, RouterLink],
    templateUrl: './details.component.html',
    styleUrl: './details.component.scss',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    animations: fuseAnimations,
})
export class DetailsComponent implements OnInit, OnDestroy {
    @ViewChild('chantierFormDirective') chantierFormDirective: FormGroupDirective;
    chantierForm: UntypedFormGroup;
    isNewChantier = false;
    chantier: Chantier;
    flashMessage: 'success' | 'error' | null = null;
    isLoading = false;
    private _unsubscribeAll = new Subject<any>();

    constructor(
        private _activatedRoute: ActivatedRoute,
        private _router: Router,
        private formBuilder: FormBuilder,
        private _chantierService: ChantierService,
        private _changeDetectorRef: ChangeDetectorRef,
        private _userService: UserService
    ) {}

    ngOnInit(): void {
        this.chantierForm = this.formBuilder.group({
            chantierId: [null], numeroChantier: ['', Validators.required], libelleChantier: [''],
            codeClient: [''], adresse: [''], montantHT: [null], montantTTC: [null],
            nature: [''], responsable: [''], dateDebut: [null], dateFin: [null],
            status: [''], isActive: [true], societeId: ['', Validators.required],
        });

        this._userService.user$.pipe(takeUntil(this._unsubscribeAll)).subscribe(user => {
            if (user?.societeId) this.chantierForm.patchValue({ societeId: user.societeId });
        });

        this._chantierService.chantier$.pipe(takeUntil(this._unsubscribeAll)).subscribe(chantier => {
            this.chantier = chantier;
            this.isNewChantier = !chantier?.chantierId;
            if (chantier?.societeId) { this.chantierForm.patchValue(chantier); }
            else { const { societeId, ...rest } = chantier || {} as any; this.chantierForm.patchValue(rest); }
            this._changeDetectorRef.markForCheck();
        });
    }

    onBackdropClicked(): void { this._router.navigate(['./'], { relativeTo: this._activatedRoute.parent }); }

    showFlashMessage(type: 'success' | 'error'): void {
        this.flashMessage = type; this._changeDetectorRef.markForCheck();
        setTimeout(() => { this.flashMessage = null; this._changeDetectorRef.markForCheck(); }, 8000);
    }

    saveChantier(): void {
        if (this.chantierForm.invalid) { this.showFlashMessage('error'); return; }
        const chantier = this.chantierForm.getRawValue() as Chantier;
        if (!this.chantier?.chantierId) {
            this._chantierService.AddChantier(chantier).pipe(catchError(() => { this.showFlashMessage('error'); return EMPTY; }))
                .subscribe(() => { this.showFlashMessage('success'); setTimeout(() => this.onBackdropClicked(), 1500); });
            return;
        }
        this._chantierService.UpdateChantier(chantier).pipe(catchError(() => { this.showFlashMessage('error'); return EMPTY; }))
            .subscribe(val => val ? this.showFlashMessage('success') : this.showFlashMessage('error'));
    }

    ngOnDestroy(): void { this._unsubscribeAll.next(null); this._unsubscribeAll.complete(); }
}
