import { PdfExportService } from '../../../../core/common/pdf-export.service';
import { CsvExportService } from '../../../../core/common/csv-export.service';
import { take } from 'rxjs';
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
import { MatOption, MatSelect } from '@angular/material/select';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { AsyncPipe, CommonModule, DatePipe, NgTemplateOutlet } from '@angular/common';
import { ReactiveFormsModule, UntypedFormControl } from '@angular/forms';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { RouterLink } from '@angular/router';
import { fuseAnimations } from '../../../../../@fuse/animations';
import { map, Observable, Subject, switchMap, takeUntil } from 'rxjs';
import { Societe } from '../../../../core/Societe/societe.model';
import { SocieteService } from '../../../../core/Societe/societe.service';
import { FuseConfirmationService } from '../../../../../@fuse/services/confirmation';
import { SecurefilePipe } from '../../../../core/pipes/securefile.pipe';
import { RoleNavigation } from '../../../../core/role-utilisateur/role-utilisateur.model';
import { FuseNavigationAction } from '../../../../../@fuse/components/navigation';

@Component({
  selector: 'app-list',
  standalone: true,
    imports: [
        MatButtonModule,
        MatFormFieldModule,
        MatIconModule,
        MatInputModule,
        MatProgressBarModule,
        MatSortModule,
        ReactiveFormsModule,
        CommonModule,
        MatPaginatorModule,
        TranslocoModule,
        RouterLink,
        SecurefilePipe,
    ],
  templateUrl: './list.component.html',
  styleUrl: './list.component.scss',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    animations: fuseAnimations,
})
export class ListComponent implements OnInit, OnDestroy{
    @ViewChild(MatPaginator) private _paginator: MatPaginator;
    @ViewChild(MatSort) private _sort: MatSort;

    societe$: Observable<Societe[]>;

    flashMessage: 'success' | 'error' | null = null;
    isLoading: boolean = false;
    societeslength: number;
    searchInputControl: UntypedFormControl = new UntypedFormControl();
    private _unsubscribeAll: Subject<any> = new Subject<any>();
    roleNavigation: RoleNavigation;
    selectedSociete: Societe | null = null;
    isViewMode: boolean = false; // Pour distinguer le mode visualisation du mode édition
    sortActive: string = 'nom';
    sortDirection: 'asc' | 'desc' = 'asc';

    constructor(
        private _pdfExportService: PdfExportService,

        private _csvExportService: CsvExportService,

        private _societeService: SocieteService,
        private _changeDetectorRef: ChangeDetectorRef,
        private _fuseConfirmationService: FuseConfirmationService
    ) {}

    SortChange() {
        this.isLoading = true;
        this.getSocietes()
            .pipe(
                map(() => {
                    this.isLoading = false;

                    // Mark for check
                    this._changeDetectorRef.markForCheck();
                })
            )
            .subscribe();
    }

    getSocietes() {
        return this._societeService.GetSociete(
            (this._paginator?.pageIndex | 0) + 1,
            this._paginator?.pageSize,
            this.sortActive,
            this.sortDirection,
            this.searchInputControl.value
        );
    }

    setSort(active: string, direction: 'asc' | 'desc'): void {
        this.sortActive = active;
        this.sortDirection = direction;
        this.SortChange();
    }

    hasActionPermission(action: FuseNavigationAction): boolean{
        return !this.roleNavigation || this.roleNavigation?.actions?.includes(action);
    }


    ngOnInit(): void {
        this.societe$ = this._societeService.societes$;

        this._societeService.societesLength$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((length) => {
                this.societeslength = length;

                // Mark for check
                this._changeDetectorRef.markForCheck();
            });
        this.searchInputControl.valueChanges
            .pipe(
                switchMap((query) => {
                    this.isLoading = true;
                    return this.getSocietes();
                }),
                map(() => {
                    this.isLoading = false;
                })
            )
            .subscribe();
    }

    /**
     * Toggle societe details for viewing (read-only mode)
     *
     * @param societeId
     */
    toggleDetails(societeId: string): void {
        //if the societe is already selected ...
        if (this.selectedSociete && this.selectedSociete.societeId === societeId) {
            // close the details
            this.closeDetails();
            return;
        }

        //Get the Societe by id
        this.societe$.pipe(
            map((Societes) => {
                const index = Societes.findIndex(item => item.societeId === societeId);
                return Societes[index];
            })
        )
            .subscribe((Societe) => {
                //set the selected societe
                this.selectedSociete = Societe;
                this.isViewMode = true; // Mode visualisation

                //Mark for check
                this._changeDetectorRef.markForCheck();
            });
    }

    /**
     * Edit societe - opens details in edit mode
     *
     * @param societeId
     */
    editSociete(societeId: string): void {
        //if the societe is already selected ...
        if (this.selectedSociete && this.selectedSociete.societeId === societeId) {
            // close the details
            this.closeDetails();
            return;
        }

        //Get the Societe by id
        this.societe$.pipe(
            map((Societes) => {
                const index = Societes.findIndex(item => item.societeId === societeId);
                return Societes[index];
            })
        )
            .subscribe((Societe) => {
                //set the selected societe
                this.selectedSociete = Societe;
                this.isViewMode = false; // Mode édition

                //Mark for check
                this._changeDetectorRef.markForCheck();
            });
    }

    /**
     * Close the details
     */
    closeDetails(): void {
        this.selectedSociete = null;
        this.isViewMode = false;
    }

    /**
     * Delete the selected product using the form data
     */
    deleteSelectedSociete(societe: Societe): void {
        if (!this.hasActionPermission(FuseNavigationAction.Delete)){
            return;
        }
        // Open the confirmation dialog
        const confirmation = this._fuseConfirmationService.open({
            title: 'Delete Societe',
            message:
                'Are you sure you want to remove this position? This action cannot be undone!',
            actions: {
                confirm: {
                    label: 'Delete',
                },
            },
        });

        // Subscribe to the confirmation dialog closed action
        confirmation.afterClosed().subscribe((result) => {
            // If the confirm button pressed...
            if (result === 'confirmed') {
                // Delete the Fonction on the server
                this._societeService
                    .DeleteSociete({ societeId: societe.societeId })
                    .subscribe(() => {
                        // Mark for check
                        this._changeDetectorRef.markForCheck();
                    });
            }
        });
    }

    /**
     * Track by function for ngFor loops
     *
     * @param index
     * @param item
     */
    trackByFn(index: number, item: any): any {
        return item.id || index;
    }

    /**
     * On destroy
     */
    ngOnDestroy(): void {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }

    protected readonly FuseNavigationAction = FuseNavigationAction;

    exportData(): void {
        if (this._societeService) {
            const obs$ = (this as any).societes$ || this._societeService.societes$ || this._societeService.societes$;
            if (obs$) {
                obs$.pipe(take(1)).subscribe((data: any) => {
                    const items = Array.isArray(data) ? data : (data?.items || data?.societes || data?.societes || []);
                    if (items && items.length > 0) {
                        const columns = [
            { header: 'Code Société', dataKey: 'codeSociete' },
            { header: 'Raison Sociale', dataKey: 'raisonSociale' },
            { header: 'Adresse', dataKey: 'adresse' },
            { header: 'Téléphone', dataKey: 'telephone' }
        ];
                        this._pdfExportService.exportToPdf('Rapport Sociétés', columns, items, 'Societes_Export.pdf');
                    } else {
                        console.warn('No data available to export to PDF');
                    }
                });
            }
        }
    }
}
