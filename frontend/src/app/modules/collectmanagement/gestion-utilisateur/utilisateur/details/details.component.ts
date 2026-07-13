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
import { MatOptionModule } from '@angular/material/core';
import { CommonModule } from '@angular/common';
import { MatSelectModule } from '@angular/material/select';
import { FormBuilder, FormGroupDirective, ReactiveFormsModule, UntypedFormGroup, Validators } from '@angular/forms';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { fuseAnimations } from '../../../../../../@fuse/animations';
import { Subject, debounceTime, distinctUntilChanged, of, takeUntil, switchMap, finalize, catchError, EMPTY } from 'rxjs';
import { Utilisateur } from '../../../../../core/utilisateurs/utilisateur.model';
import { UtilisateurService } from '../../../../../core/utilisateurs/utilisateur.service';
import { RoleUtilisateur } from '../../../../../core/role-utilisateur/role-utilisateur.model';
import { PagedSociete, Societe } from '../../../../../core/Societe/societe.model';
import { SocieteService } from '../../../../../core/Societe/societe.service';
import { Site } from '../../../../../core/site/site.model';
import { SiteService } from '../../../../../core/site/site.service';

@Component({
    selector: 'app-utilisateur-details',
    standalone: true,
    imports: [
        CommonModule,
        ReactiveFormsModule,
        MatButtonModule,
        MatFormFieldModule,
        MatIconModule,
        MatInputModule,
        MatOptionModule,
        MatSelectModule,
        MatAutocompleteModule,
        MatProgressBarModule,
        TranslocoModule,
        RouterLink,
    ],
    templateUrl: './details.component.html',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    animations: fuseAnimations,
})
export class DetailsComponent implements OnInit, OnDestroy {
    private readonly passwordComplexityRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z\d]).{8,}$/;

    @ViewChild('utilisateurFormDirective') utilisateurFormDirective: FormGroupDirective;
    form: UntypedFormGroup;
    isNew: boolean = false;
    utilisateur: Utilisateur;

    roleUtilisateurs: RoleUtilisateur[] = [];
    societe: Societe[] = [];
    filteredSocietes$: any;
    allSites: Site[] = [];
    selectedSiteIds: string[] = [];

    isChangingPassword = false;
    saveClicked = false;
    flashMessage: 'success' | 'error' | null = null;
    isLoading: boolean = false;
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    constructor(
        private _activatedRoute: ActivatedRoute,
        private _router: Router,
        private _formBuilder: FormBuilder,
        private _utilisateurService: UtilisateurService,
        private _societeService: SocieteService,
        private _siteService: SiteService,
        private _changeDetectorRef: ChangeDetectorRef
    ) {}

    ngOnInit(): void {
        this.form = this._formBuilder.group({
            utilisateurId: ['new'],
            nomUtilisateur: ['', [Validators.required, Validators.maxLength(20)]],
            nom: ['', [Validators.maxLength(50)]],
            prenom: ['', [Validators.maxLength(50)]],
            email: ['', [Validators.required, Validators.email, Validators.maxLength(100)]],
            password: ['', [Validators.pattern(this.passwordComplexityRegex)]],
            roleUtilisateurId: [null, [Validators.required]],
            isActive: [true, [Validators.required]],
            societeId: [''],
            siteIds: [[]],
        });

        // Sociétés (autocomplete)
        this._societeService.GetSociete()
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((pagedSociete: PagedSociete) => {
                this.societe = pagedSociete.societes || [];
                this._changeDetectorRef.markForCheck();
            });

        this.filteredSocietes$ = this.form.get('societeId')!.valueChanges.pipe(
            debounceTime(300),
            distinctUntilChanged(),
            takeUntil(this._unsubscribeAll),
            switchMap((value) => this._societeService.GetSociete(1, 20, '', 'asc', value || '')),
            switchMap((res: PagedSociete) => of(res.societes || []))
        );

        // Sites
        this._siteService.GetAllSites()
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((sites) => {
                this.allSites = sites;
                this._changeDetectorRef.markForCheck();
            });

        // Route data: roles + utilisateur
        this._activatedRoute.data
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((data) => {
                if (data?.roles) {
                    this.roleUtilisateurs = data.roles;
                }

                this.utilisateur = data.utilisateur;
                this.isNew = !this.utilisateur || this.utilisateur.utilisateurId === 'new';

                // Sur rafraîchissement direct d'une URL d'édition sans cache : retour à la liste.
                const routeId = this._activatedRoute.snapshot.paramMap.get('id');
                if (!this.isNew && !this.utilisateur && routeId !== 'ajouter') {
                    this.onBackdropClicked();
                    return;
                }

                if (this.utilisateur && !this.isNew) {
                    this.selectedSiteIds = this.utilisateur.siteIds || [];
                    this.form.patchValue({
                        utilisateurId: this.utilisateur.utilisateurId,
                        nomUtilisateur: this.utilisateur.nomUtilisateur,
                        nom: this.utilisateur.nom,
                        prenom: this.utilisateur.prenom,
                        email: this.utilisateur.email,
                        roleUtilisateurId: this.utilisateur.roleUtilisateurId,
                        isActive: this.utilisateur.isActive ?? true,
                        societeId: this.utilisateur.societeId || '',
                        siteIds: this.utilisateur.siteIds || [],
                    });
                    // En édition, le mot de passe n'est modifié que sur demande explicite.
                    this.form.get('password')?.clearValidators();
                    this.form.get('password')?.updateValueAndValidity();
                } else {
                    this.selectedSiteIds = [];
                    this.form.reset({ utilisateurId: 'new', isActive: true, siteIds: [], roleUtilisateurId: null });
                    // En création, le mot de passe est requis.
                    this.form.get('password')?.setValidators([Validators.required, Validators.pattern(this.passwordComplexityRegex)]);
                    this.form.get('password')?.updateValueAndValidity();
                }
                this._changeDetectorRef.markForCheck();
            });
    }

    displaySociete = (societeId: string) => {
        const found = this.societe.find((s) => s.societeId === societeId);
        return found ? found.nom : '';
    };

    toggleChangePassword(): void {
        this.isChangingPassword = !this.isChangingPassword;
        const control = this.form.get('password');
        if (this.isChangingPassword) {
            control?.setValidators([Validators.required, Validators.pattern(this.passwordComplexityRegex)]);
        } else {
            control?.setValue('');
            control?.clearValidators();
        }
        control?.updateValueAndValidity();
        this._changeDetectorRef.markForCheck();
    }

    toggleSite(siteId: string): void {
        const index = this.selectedSiteIds.indexOf(siteId);
        if (index > -1) {
            this.selectedSiteIds.splice(index, 1);
        } else {
            this.selectedSiteIds.push(siteId);
        }
        this.form.get('siteIds')?.setValue([...this.selectedSiteIds]);
        this._changeDetectorRef.markForCheck();
    }

    toggleAllSites(): void {
        if (this.selectedSiteIds.length === this.allSites.length) {
            this.selectedSiteIds = [];
        } else {
            this.selectedSiteIds = this.allSites.map((s) => s.siteId);
        }
        this.form.get('siteIds')?.setValue([...this.selectedSiteIds]);
        this._changeDetectorRef.markForCheck();
    }

    isSiteSelected(siteId: string): boolean {
        return this.selectedSiteIds.includes(siteId);
    }

    get allSitesSelected(): boolean {
        return this.allSites.length > 0 && this.selectedSiteIds.length === this.allSites.length;
    }

    showFlashMessage(type: 'success' | 'error'): void {
        this.flashMessage = type;
        this._changeDetectorRef.markForCheck();
        setTimeout(() => {
            this.flashMessage = null;
            this._changeDetectorRef.markForCheck();
        }, 8000);
    }

    onBackdropClicked(): void {
        this._router.navigate(['../'], { relativeTo: this._activatedRoute });
        this._changeDetectorRef.markForCheck();
    }

    save(): void {
        if (this.form.invalid) {
            this.showFlashMessage('error');
            this.form.markAllAsTouched();
            return;
        }

        this.saveClicked = true;
        this.isLoading = true;
        this._changeDetectorRef.markForCheck();

        const utilisateur = this.form.getRawValue();

        if (this.isNew) {
            this._utilisateurService.AddUtilisateur(utilisateur)
                .pipe(
                    catchError(() => {
                        this.showFlashMessage('error');
                        return EMPTY;
                    }),
                    finalize(() => {
                        this.saveClicked = false;
                        this.isLoading = false;
                        this._changeDetectorRef.markForCheck();
                    })
                )
                .subscribe(() => {
                    this.showFlashMessage('success');
                    setTimeout(() => this.onBackdropClicked(), 1200);
                });
        } else {
            this._utilisateurService.UpdateUtilisateur(utilisateur)
                .pipe(
                    catchError(() => {
                        this.showFlashMessage('error');
                        return EMPTY;
                    }),
                    finalize(() => {
                        this.saveClicked = false;
                        this.isLoading = false;
                        this._changeDetectorRef.markForCheck();
                    })
                )
                .subscribe((success) => {
                    if (success) {
                        this.showFlashMessage('success');
                        setTimeout(() => this.onBackdropClicked(), 1200);
                    } else {
                        this.showFlashMessage('error');
                    }
                });
        }
    }

    ngOnDestroy(): void {
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }
}
