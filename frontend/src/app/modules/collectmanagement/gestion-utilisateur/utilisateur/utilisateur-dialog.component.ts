import {
    ChangeDetectionStrategy,
    Component,
    Inject,
    OnDestroy,
    OnInit,
    ViewEncapsulation,
} from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import {
    ReactiveFormsModule,
    UntypedFormBuilder,
    UntypedFormGroup,
    Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatOptionModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { Subject, debounceTime, distinctUntilChanged, of, takeUntil, switchMap } from 'rxjs';
import { Utilisateur } from '../../../../core/utilisateurs/utilisateur.model';
import { RoleUtilisateur } from '../../../../core/role-utilisateur/role-utilisateur.model';
import { PagedSociete, Societe } from '../../../../core/Societe/societe.model';
import { Site } from '../../../../core/site/site.model';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { TranslocoDirective } from '@ngneat/transloco';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { CommonModule } from '@angular/common';
import { SocieteService } from '../../../../core/Societe/societe.service';

@Component({
    selector: 'app-utilisateur-dialog',
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
        MatSlideToggleModule,
        MatAutocompleteModule,
        TranslocoDirective,
        MatCheckboxModule,
    ],
    templateUrl: './utilisateur-dialog.component.html',
    styleUrls: ['./utilisateur-dialog.component.scss'],
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class UtilisateurDialogComponent implements OnInit, OnDestroy {
    private readonly passwordComplexityRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z\d]).{8,}$/;

    form: UntypedFormGroup;
    roleUtilisateurs: RoleUtilisateur[] = [];
    societe: Societe[] = [];
    filteredSocietes$: any;
    allSites: Site[] = [];
    selectedSiteIds: string[] = [];
    isChangingPassword = false;
    isNew: boolean = false;
    saveClicked = false;

    private _unsubscribeAll: Subject<any> = new Subject<any>();

    constructor(
        private _dialogRef: MatDialogRef<UtilisateurDialogComponent>,
        @Inject(MAT_DIALOG_DATA) public data: { utilisateur: Utilisateur | null; roleUtilisateurs: RoleUtilisateur[]; societes: Societe[]; sites: Site[] },
        private _formBuilder: UntypedFormBuilder,
        private _societeService: SocieteService
    ) {}

    ngOnInit(): void {
        this.roleUtilisateurs = this.data.roleUtilisateurs;
        this.societe = this.data.societes;
        this.allSites = this.data.sites;
        this.isNew = !this.data.utilisateur || this.data.utilisateur.utilisateurId === 'new';

        this.form = this._formBuilder.group({
            utilisateurId: [this.data.utilisateur?.utilisateurId || 'new'],
            nomUtilisateur: [this.data.utilisateur?.nomUtilisateur, [Validators.required]],
            nom: [this.data.utilisateur?.nom],
            prenom: [this.data.utilisateur?.prenom],
            email: [this.data.utilisateur?.email, [Validators.required]],
            password: ['', this.isNew ? [Validators.required, Validators.pattern(this.passwordComplexityRegex)] : [Validators.pattern(this.passwordComplexityRegex)]],
            roleUtilisateurId: [this.data.utilisateur?.roleUtilisateurId, [Validators.required]],
            isActive: [this.data.utilisateur?.isActive ?? true, [Validators.required]],
            societeId: [this.data.utilisateur?.societeId || ''],
            siteIds: [this.data.utilisateur?.siteIds || []]
        });

        this.selectedSiteIds = this.data.utilisateur?.siteIds || [];

        this.filteredSocietes$ = this.form.get('societeId')!.valueChanges.pipe(
            debounceTime(300),
            distinctUntilChanged(),
            takeUntil(this._unsubscribeAll),
            switchMap((value) =>
                this._societeService
                    .GetSociete(1, 20, '', 'asc', value || '')
            ),
            switchMap((res: PagedSociete) => of(res.societes || []))
        );
    }

    displaySociete = (societeId: string) => {
        const found = this.societe.find((s) => s.societeId === societeId);
        return found ? found.nom : '';
    };

    toggleSite(siteId: string): void {
        const index = this.selectedSiteIds.indexOf(siteId);
        if (index > -1) {
            this.selectedSiteIds.splice(index, 1);
        } else {
            this.selectedSiteIds.push(siteId);
        }
        this.form.get('siteIds').setValue(this.selectedSiteIds);
    }

    toggleAllSites(): void {
        if (this.selectedSiteIds.length === this.allSites.length) {
            this.selectedSiteIds = [];
        } else {
            this.selectedSiteIds = this.allSites.map(s => s.siteId);
        }
        this.form.get('siteIds').setValue(this.selectedSiteIds);
    }

    isSiteSelected(siteId: string): boolean {
        return this.selectedSiteIds.includes(siteId);
    }

    toggleChangePassword(): void {
        this.isChangingPassword = !this.isChangingPassword;
    }

    save(): void {
        this.saveClicked = true;

        if (this.form.invalid) {
            return;
        }

        this._dialogRef.close(this.form.getRawValue());
    }

    close(): void {
        this._dialogRef.close();
    }

    ngOnDestroy(): void {
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }
}
