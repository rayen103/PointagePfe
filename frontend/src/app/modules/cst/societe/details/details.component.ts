import {
    AfterViewInit,
    ChangeDetectionStrategy, ChangeDetectorRef,
    Component,
    OnDestroy,
    OnInit,
    ViewChild,
    ViewEncapsulation,
} from '@angular/core';
import { TranslocoModule } from '@ngneat/transloco';
import { ActivatedRoute, Router, RouterLink, RouterOutlet } from '@angular/router';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatOptionModule, MatRippleModule } from '@angular/material/core';
import { MatDividerModule } from '@angular/material/divider';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatExpansionModule } from '@angular/material/expansion';
import { AsyncPipe, CommonModule, NgTemplateOutlet } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatSelectModule } from '@angular/material/select';
import { FormBuilder, FormGroupDirective, ReactiveFormsModule, UntypedFormGroup, Validators } from '@angular/forms';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatTabsModule } from '@angular/material/tabs';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { fuseAnimations } from '../../../../../@fuse/animations';
import { MatPaginator } from '@angular/material/paginator';
import { Reseau, Site, Societe } from '../../../../core/Societe/societe.model';
import { catchError, map, Observable, of, ReplaySubject, Subject, takeUntil } from 'rxjs';
import { SocieteService } from '../../../../core/Societe/societe.service';
import { FuseConfirmationService } from '../../../../../@fuse/services/confirmation';
import { SecurefilePipe } from '../../../../core/pipes/securefile.pipe';

@Component({
  selector: 'app-details',
  standalone: true,
    imports: [
        MatButtonModule,
        MatFormFieldModule,
        MatIconModule,
        MatInputModule,
        MatProgressBarModule,
        MatSortModule,
        ReactiveFormsModule,
        MatOptionModule,
        MatSelectModule,
        MatCardModule,
        CommonModule,
        MatExpansionModule,
        MatDatepickerModule,
        MatDividerModule,
        MatRippleModule,
        MatSidenavModule,

        TranslocoModule,
        RouterLink,
        SecurefilePipe,
        MatTabsModule,
        MatCheckboxModule,
    ],
  templateUrl: './details.component.html',
  styleUrl: './details.component.scss',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    animations: fuseAnimations,
})
export class DetailsComponent implements OnInit, OnDestroy, AfterViewInit{
    @ViewChild(MatPaginator) private _paginator: MatPaginator;
    @ViewChild(MatSort) private _sort: MatSort;
    @ViewChild('societeFormDirective') societeFormDirective: FormGroupDirective;
    societeForm: UntypedFormGroup;
    isNewSociete: boolean = false;
    societe$: Observable<Societe>;
    societe: Societe;
    flashMessage: 'success' | 'error' | null = null;
    isLoading: boolean = false;
    private _unsubscribeAll: Subject<any> = new Subject<any>();
    societeId: string;
    showEmailError = false;
    logoSrc: string | ArrayBuffer | null = null;
    siteForm: UntypedFormGroup;
    reseauForm: UntypedFormGroup;
    sites: Site[] = [];
    reseaux: Reseau[] = [];
    selectedSiteId: string | null = null;
    selectedReseauId: string | null = null;

    constructor(
        private _activatedRoute: ActivatedRoute,
        private _router: Router,
        private formBuilder: FormBuilder,
        private _societeService: SocieteService,
        private _fuseConfirmationService: FuseConfirmationService,
        private _changeDetectorRef: ChangeDetectorRef
    ) { }

    ngOnInit(): void {

        this.societeForm = this.formBuilder.group({
            societeId: [null],
            logoPath:[null],
            logoData: [null],
            logoExtension: [null],
            nom: ['', Validators.required],
            initiales: [''],
            tva: [''],
            rc: [''],
            matriculeFiscal:['', Validators.required],
            rne: ['', Validators.required],
            capital: ['' ],
            dateOverture: ['', Validators.required],
            telephone1: ['' ],
            telephone2: ['' ],
            fax1: ['' ],
            fax2: ['' ],
            email: ['' ],
            adresse: ['' ],
            codePostal: ['' ],
            ville: ['' ],
            pays: ['' ],
            codeSociete: ['' ],
        });

        this.siteForm = this.formBuilder.group({
            siteId: [null],
            code: ['', Validators.required],
            site: ['', Validators.required],
            siege: [false],
            longitude: [null],
            latitude: [null],
            rayon: [null],
            timeMinute: [null],
            isActive: [true],
        });

        this.reseauForm = this.formBuilder.group({
            reseauId: [null],
            ipAddress: ['', Validators.required],
            port: [null, Validators.required],
            gmtPlus: [null],
            latitude: [null],
            longitude: [null],
            rayon: [null],
            timeToleranceMinute: [null],
            isActive: [true],
        });



        this._societeService.societe$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((societe) => {
                this.societe = societe;
                this.societeForm.patchValue(societe);
                if (societe?.societeId){
                    this.loadSites(societe.societeId);
                    this.loadReseaux(societe.societeId);
                } else {
                    this.sites = [];
                    this.reseaux = [];
                }

                this._changeDetectorRef.markForCheck();
            });

    }


    onBackdropClicked(): void {
        // Go back to the list
        this._router.navigate(['./'], { relativeTo: this._activatedRoute });

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

    saveSociete(): void {
        if (this.societeForm.invalid) {
            // Show an error message
            this.showFlashMessage('error');
            return;
        }
        const societe = this.societeForm.getRawValue() as Societe;


        if (!this.societe.societeId) {
            this._societeService
                .AddSociete(societe)
                .pipe(
                    catchError((error)=>{

                        this.showFlashMessage('error');
                        return of(error)
                    })
                )
                .subscribe(()=>{
                    this.showFlashMessage('success');
                });

            return;
        }

        this._societeService
            .UpdateSociete(societe)
            .subscribe((val)=>{
                if (val){
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


    ngAfterViewInit(): void {

    }
    toggleEmailError() {
        this.showEmailError = !this.showEmailError; // Toggle error display
    }
    private convertFileToBase64(file: File): Observable<string | ArrayBuffer> {
        const result = new ReplaySubject<string | ArrayBuffer>(1);
        const reader = new FileReader();
        reader.onload = (event) => result.next(event.target.result);
        reader.readAsDataURL(file);
        return result;
    }

    saveImage(event: any) {
        const file = event.target.files[0];

        this.convertFileToBase64(file)
            .pipe(
                map((result) => {
                    console.log(result);
                    this.logoSrc = result;
                    const base64Image = (result as string).replace(
                        /^data:image\/[a-z]+;base64,/,
                        ''
                    );
                    this.societeForm.get('logoData').patchValue(base64Image);
                    this.societeForm
                        .get('logoExtension')
                        .patchValue(file.type?.replace('image/', ''));

                    this._changeDetectorRef.markForCheck();
                })
            )
            .subscribe();
    }

    saveSite(): void {
        if (!this.societe?.societeId || this.siteForm.invalid) {
            this.showFlashMessage('error');
            return;
        }

        const payload: Site = {
            ...this.siteForm.getRawValue(),
            societeId: this.societe.societeId,
        };

        if (payload.siteId) {
            this._societeService.UpdateSite(payload).subscribe(() => {
                this.resetSiteForm();
                this.loadSites(this.societe.societeId);
                this.showFlashMessage('success');
            });
            return;
        }

        this._societeService.AddSite(payload).subscribe(() => {
            this.resetSiteForm();
            this.loadSites(this.societe.societeId);
            this.showFlashMessage('success');
        });
    }

    editSite(site: Site): void {
        this.selectedSiteId = site.siteId ?? null;
        this.siteForm.patchValue(site);
    }

    deleteSite(site: Site): void {
        if (!site.siteId || !this.societe?.societeId) {
            return;
        }
        this._societeService.DeleteSite(site.siteId).subscribe(() => {
            this.resetSiteForm();
            this.loadSites(this.societe.societeId);
        });
    }

    resetSiteForm(): void {
        this.selectedSiteId = null;
        this.siteForm.reset({
            siteId: null,
            code: '',
            site: '',
            siege: false,
            longitude: null,
            latitude: null,
            rayon: null,
            timeMinute: null,
            isActive: true,
        });
    }

    saveReseau(): void {
        if (!this.societe?.societeId || this.reseauForm.invalid) {
            this.showFlashMessage('error');
            return;
        }

        const payload: Reseau = {
            ...this.reseauForm.getRawValue(),
            societeId: this.societe.societeId,
        };

        if (payload.reseauId) {
            this._societeService.UpdateReseau(payload).subscribe(() => {
                this.resetReseauForm();
                this.loadReseaux(this.societe.societeId);
                this.showFlashMessage('success');
            });
            return;
        }

        this._societeService.AddReseau(payload).subscribe(() => {
            this.resetReseauForm();
            this.loadReseaux(this.societe.societeId);
            this.showFlashMessage('success');
        });
    }

    editReseau(reseau: Reseau): void {
        this.selectedReseauId = reseau.reseauId ?? null;
        this.reseauForm.patchValue(reseau);
    }

    deleteReseau(reseau: Reseau): void {
        if (!reseau.reseauId || !this.societe?.societeId) {
            return;
        }
        this._societeService.DeleteReseau(reseau.reseauId).subscribe(() => {
            this.resetReseauForm();
            this.loadReseaux(this.societe.societeId);
        });
    }

    resetReseauForm(): void {
        this.selectedReseauId = null;
        this.reseauForm.reset({
            reseauId: null,
            ipAddress: '',
            port: null,
            gmtPlus: null,
            latitude: null,
            longitude: null,
            rayon: null,
            timeToleranceMinute: null,
            isActive: true,
        });
    }

    private loadSites(societeId: string): void {
        this._societeService.GetSitesBySocieteId(societeId).subscribe((sites) => {
            this.sites = sites;
            this._changeDetectorRef.markForCheck();
        });
    }

    private loadReseaux(societeId: string): void {
        this._societeService.GetReseauxBySocieteId(societeId).subscribe((reseaux) => {
            this.reseaux = reseaux;
            this._changeDetectorRef.markForCheck();
        });
    }
}
