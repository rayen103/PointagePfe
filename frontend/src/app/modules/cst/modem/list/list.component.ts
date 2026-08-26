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
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { AsyncPipe, CommonModule } from '@angular/common';
import { ReactiveFormsModule, UntypedFormControl } from '@angular/forms';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { RouterLink } from '@angular/router';
import { fuseAnimations } from '../../../../../@fuse/animations';
import { map, Observable, Subject, switchMap, takeUntil } from 'rxjs';
import { Modem } from '../../../../core/modem/modem.model';
import { ModemService } from '../../../../core/modem/modem.service';
import { FuseConfirmationService } from '../../../../../@fuse/services/confirmation';
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
        ReactiveFormsModule,
        CommonModule,
        MatPaginatorModule,
        TranslocoModule,
        RouterLink,
    ],
    templateUrl: './list.component.html',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    animations: fuseAnimations,
})
export class ListComponent implements OnInit, OnDestroy {
    @ViewChild(MatPaginator) private _paginator: MatPaginator;

    modem$: Observable<Modem[]>;

    flashMessage: 'success' | 'error' | null = null;
    isLoading: boolean = false;
    modemsLength: number;
    searchInputControl: UntypedFormControl = new UntypedFormControl();
    private _unsubscribeAll: Subject<any> = new Subject<any>();
    roleNavigation: RoleNavigation;
    selectedModem: Modem | null = null;
    isViewMode: boolean = false;
    sortActive: string = 'imei';
    sortDirection: 'asc' | 'desc' = 'asc';

    constructor(
        private _pdfExportService: PdfExportService,

        private _csvExportService: CsvExportService,

        private _modemService: ModemService,
        private _changeDetectorRef: ChangeDetectorRef,
        private _fuseConfirmationService: FuseConfirmationService
    ) {}

    SortChange() {
        this.isLoading = true;
        this.getModems()
            .pipe(
                map(() => {
                    this.isLoading = false;
                    this._changeDetectorRef.markForCheck();
                })
            )
            .subscribe();
    }

    getModems() {
        return this._modemService.GetModems(
            (this._paginator?.pageIndex ?? 0) + 1,
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

    hasActionPermission(action: FuseNavigationAction): boolean {
        return !this.roleNavigation || this.roleNavigation?.actions?.includes(action);
    }

    ngOnInit(): void {
        this.modem$ = this._modemService.modems$;

        this._modemService.modemsLength$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((length) => {
                this.modemsLength = length;
                this._changeDetectorRef.markForCheck();
            });
        this.searchInputControl.valueChanges
            .pipe(
                switchMap((query) => {
                    this.isLoading = true;
                    return this.getModems();
                }),
                map(() => {
                    this.isLoading = false;
                })
            )
            .subscribe();
    }

    toggleDetails(modemId: string): void {
        if (this.selectedModem && this.selectedModem.modemId === modemId) {
            this.closeDetails();
            return;
        }

        this.modem$.pipe(
            map((modems) => {
                const index = modems.findIndex(item => item.modemId === modemId);
                return modems[index];
            })
        )
            .subscribe((modem) => {
                this.selectedModem = modem;
                this.isViewMode = true;
                this._changeDetectorRef.markForCheck();
            });
    }

    closeDetails(): void {
        this.selectedModem = null;
        this.isViewMode = false;
    }

    deleteSelectedModem(modem: Modem): void {
        if (!this.hasActionPermission(FuseNavigationAction.Delete)) {
            return;
        }
        const confirmation = this._fuseConfirmationService.open({
            title: 'Delete Modem',
            message:
                'Are you sure you want to remove this modem? This action cannot be undone!',
            actions: {
                confirm: {
                    label: 'Delete',
                },
            },
        });

        confirmation.afterClosed().subscribe((result) => {
            if (result === 'confirmed') {
                this._modemService
                    .DeleteModem(modem.modemId)
                    .subscribe(() => {
                        this._changeDetectorRef.markForCheck();
                    });
            }
        });
    }

    trackByFn(index: number, item: any): any {
        return item.modemId || index;
    }

    ngOnDestroy(): void {
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }

    protected readonly FuseNavigationAction = FuseNavigationAction;

    exportData(): void {
        if (this._modemService) {
            const obs$ = (this as any).modems$ || this._modemService.modems$ || this._modemService.modems$;
            if (obs$) {
                obs$.pipe(take(1)).subscribe((data: any) => {
                    const items = Array.isArray(data) ? data : (data?.items || data?.modems || data?.modems || []);
                    if (items && items.length > 0) {
                        const columns = [
            { header: 'N° IMEI', dataKey: 'numeroIMEI' },
            { header: 'Véhicule', dataKey: 'codeVehicule' },
            { header: 'Libellé', dataKey: 'libelle' },
            { header: 'Statut', dataKey: 'isActive' }
        ];
                        this._pdfExportService.exportToPdf('Rapport Modems', columns, items, 'Modems_Export.pdf');
                    } else {
                        console.warn('No data available to export to PDF');
                    }
                });
            }
        }
    }
}
