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
import { Gouvernorat } from '../../../../core/gouvernorat/gouvernorat.model';
import { GouvernoratService } from '../../../../core/gouvernorat/gouvernorat.service';
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

    gouvernorat$: Observable<Gouvernorat[]>;

    flashMessage: 'success' | 'error' | null = null;
    isLoading: boolean = false;
    gouvernoratsLength: number;
    searchInputControl: UntypedFormControl = new UntypedFormControl();
    private _unsubscribeAll: Subject<any> = new Subject<any>();
    roleNavigation: RoleNavigation;
    selectedGouvernorat: Gouvernorat | null = null;
    isViewMode: boolean = false;
    sortActive: string = 'codeGouvernorat';
    sortDirection: 'asc' | 'desc' = 'asc';

    constructor(
        private _pdfExportService: PdfExportService,

        private _csvExportService: CsvExportService,

        private _gouvernoratService: GouvernoratService,
        private _changeDetectorRef: ChangeDetectorRef,
        private _fuseConfirmationService: FuseConfirmationService
    ) {}

    SortChange() {
        this.isLoading = true;
        this.getGouvernorats()
            .pipe(
                map(() => {
                    this.isLoading = false;
                    this._changeDetectorRef.markForCheck();
                })
            )
            .subscribe();
    }

    getGouvernorats() {
        return this._gouvernoratService.GetGouvernorats(
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
        this.gouvernorat$ = this._gouvernoratService.gouvernorats$;

        this._gouvernoratService.gouvernoratsLength$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((length) => {
                this.gouvernoratsLength = length;
                this._changeDetectorRef.markForCheck();
            });
        this.searchInputControl.valueChanges
            .pipe(
                switchMap((query) => {
                    this.isLoading = true;
                    return this.getGouvernorats();
                }),
                map(() => {
                    this.isLoading = false;
                })
            )
            .subscribe();
    }

    toggleDetails(gouvernoratId: string): void {
        if (this.selectedGouvernorat && this.selectedGouvernorat.gouvernoratId === gouvernoratId) {
            this.closeDetails();
            return;
        }

        this.gouvernorat$.pipe(
            map((gouvernorats) => {
                const index = gouvernorats.findIndex(item => item.gouvernoratId === gouvernoratId);
                return gouvernorats[index];
            })
        )
            .subscribe((gouvernorat) => {
                this.selectedGouvernorat = gouvernorat;
                this.isViewMode = true;
                this._changeDetectorRef.markForCheck();
            });
    }

    closeDetails(): void {
        this.selectedGouvernorat = null;
        this.isViewMode = false;
    }

    deleteSelectedGouvernorat(gouvernorat: Gouvernorat): void {
        if (!this.hasActionPermission(FuseNavigationAction.Delete)) {
            return;
        }
        const confirmation = this._fuseConfirmationService.open({
            title: 'Delete Gouvernorat',
            message:
                'Are you sure you want to remove this gouvernorat? This action cannot be undone!',
            actions: {
                confirm: {
                    label: 'Delete',
                },
            },
        });

        confirmation.afterClosed().subscribe((result) => {
            if (result === 'confirmed') {
                this._gouvernoratService
                    .DeleteGouvernorat(gouvernorat.gouvernoratId)
                    .subscribe(() => {
                        this._changeDetectorRef.markForCheck();
                    });
            }
        });
    }

    trackByFn(index: number, item: any): any {
        return item.gouvernoratId || index;
    }

    ngOnDestroy(): void {
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }

    protected readonly FuseNavigationAction = FuseNavigationAction;

    exportData(): void {
        if (this._gouvernoratService) {
            const obs$ = (this as any).gouvernorats$ || this._gouvernoratService.gouvernorats$ || this._gouvernoratService.gouvernorats$;
            if (obs$) {
                obs$.pipe(take(1)).subscribe((data: any) => {
                    const items = Array.isArray(data) ? data : (data?.items || data?.gouvernorats || data?.gouvernorats || []);
                    if (items && items.length > 0) {
                        const columns = [
            { header: 'Code Gouvernorat', dataKey: 'codeGouvernorat' },
            { header: 'Libellé Gouvernorat', dataKey: 'libelleGouvernorat' }
        ];
                        this._pdfExportService.exportToPdf('Rapport Gouvernorats', columns, items, 'Gouvernorats_Export.pdf');
                    } else {
                        console.warn('No data available to export to PDF');
                    }
                });
            }
        }
    }
}
