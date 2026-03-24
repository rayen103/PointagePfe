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
import { RattachementArticle } from '../../../../core/rattachement-article/rattachement-article.model';
import { catchError, EMPTY, Subject, takeUntil } from 'rxjs';
import { RattachementArticleService } from '../../../../core/rattachement-article/rattachement-article.service';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { UserService } from '../../../../core/user/user.service';
import { MatExpansionModule } from '@angular/material/expansion';

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
    ],
    templateUrl: './details.component.html',
    styleUrl: './details.component.scss',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    animations: fuseAnimations,
})
export class DetailsComponent implements OnInit, OnDestroy {
    @ViewChild('rattachementArticleFormDirective') rattachementArticleFormDirective: FormGroupDirective;
    rattachementArticleForm: UntypedFormGroup;
    isNewRattachementArticle: boolean = false;
    rattachementArticle: RattachementArticle;
    flashMessage: 'success' | 'error' | null = null;
    isLoading: boolean = false;
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    constructor(
        private _activatedRoute: ActivatedRoute,
        private _router: Router,
        private formBuilder: FormBuilder,
        private _rattachementArticleService: RattachementArticleService,
        private _changeDetectorRef: ChangeDetectorRef,
        private _userService: UserService
    ) {}

    ngOnInit(): void {
        this.rattachementArticleForm = this.formBuilder.group({
            rattachementArticleId: [null],
            rattachementId: ['', Validators.required],
            codeArticle: ['', Validators.required],
            libelle: [''],
            quantite: [null],
            prixRevient: [null],
            tauxTVA: [null],
            codeUnite: [''],
            codeEntrepot: [''],
            typeRattachement: [''],
            numeroBonLivraison: [''],
            dateBonLivraison: [null],
            isActive: [true],
            societeId: [''],
        });

        this._activatedRoute.data
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe(({ rattachementArticle }) => {
                this.rattachementArticle = rattachementArticle;
                this.isNewRattachementArticle = !rattachementArticle?.rattachementArticleId;
                this.rattachementArticleForm.patchValue(rattachementArticle ?? {});
                this._changeDetectorRef.markForCheck();
            });
    }

    onBackdropClicked(): void {
        this._router.navigate(['../'], { relativeTo: this._activatedRoute });
    }

    saveRattachementArticle(): void {
        if (this.rattachementArticleForm.invalid) return;

        this.isLoading = true;
        const formValue = this.rattachementArticleForm.getRawValue();

        const save$ = this.isNewRattachementArticle
            ? this._rattachementArticleService.AddRattachementArticle(formValue)
            : this._rattachementArticleService.UpdateRattachementArticle(formValue);

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
            if (this.isNewRattachementArticle) {
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
