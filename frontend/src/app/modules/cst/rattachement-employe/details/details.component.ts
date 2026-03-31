import {
    ChangeDetectionStrategy,
    ChangeDetectorRef,
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
import { RattachementEmploye } from '../../../../core/rattachement-employe/rattachement-employe.model';
import { catchError, EMPTY, Observable, Subject, takeUntil } from 'rxjs';
import { RattachementEmployeService } from '../../../../core/rattachement-employe/rattachement-employe.service';
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
    @ViewChild('rattachementEmployeFormDirective') rattachementEmployeFormDirective: FormGroupDirective;
    rattachementEmployeForm: UntypedFormGroup;
    isNewRattachementEmploye: boolean = false;
    rattachementEmploye: RattachementEmploye;
    flashMessage: 'success' | 'error' | null = null;
    isLoading: boolean = false;
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    constructor(
        private _activatedRoute: ActivatedRoute,
        private _router: Router,
        private formBuilder: FormBuilder,
        private _rattachementEmployeService: RattachementEmployeService,
        private _changeDetectorRef: ChangeDetectorRef,
        private _userService: UserService
    ) {}

    ngOnInit(): void {
        this.rattachementEmployeForm = this.formBuilder.group({
            rattachementEmployeId: [null],
            rattachementId: ['', Validators.required],
            matricule: ['', Validators.required],
            nomPrenom: [''],
            dateDebut: [null],
            heureDebut: [''],
            dateFin: [null],
            heureFin: [''],
            nombreHeure: [null],
            cout: [null],
            coutGlobal: [null],
            typeRattachement: [''],
            isActive: [true],
            societeId: [''],
        });

        this._activatedRoute.data
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe(({ rattachementEmploye }) => {
                this.rattachementEmploye = rattachementEmploye;
                this.isNewRattachementEmploye = !rattachementEmploye?.rattachementEmployeId;
                this.rattachementEmployeForm.patchValue(rattachementEmploye ?? {});
                this._changeDetectorRef.markForCheck();
            });
    }

    onBackdropClicked(): void {
        this._router.navigate(['../'], { relativeTo: this._activatedRoute });
    }

    saveRattachementEmploye(): void {
        if (this.rattachementEmployeForm.invalid) return;

        this.isLoading = true;
        const formValue = this.rattachementEmployeForm.getRawValue();

        const save$: Observable<any> = this.isNewRattachementEmploye
            ? this._rattachementEmployeService.AddRattachementEmploye(formValue)
            : this._rattachementEmployeService.UpdateRattachementEmploye(formValue);

        save$.pipe(
            catchError(() => {
                this.flashMessage = 'error';
                this.isLoading = false;
                this._changeDetectorRef.markForCheck();
                return EMPTY;
            })
        ).subscribe(() => {
            this.flashMessage = 'success';
            this.isLoading = false;
            this._changeDetectorRef.markForCheck();
            if (this.isNewRattachementEmploye) {
                this._router.navigate(['../'], { relativeTo: this._activatedRoute });
            }
        });
    }

    showFlashMessage(type: 'success' | 'error'): void {
        this.flashMessage = type;
        setTimeout(() => { this.flashMessage = null; this._changeDetectorRef.markForCheck(); }, 3000);
    }

    ngOnDestroy(): void {
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }
}
