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
import { CommonModule } from '@angular/common';
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
import { RattachementArticle } from '../../../../core/rattachement-article/rattachement-article.model';
import { RattachementArticleService } from '../../../../core/rattachement-article/rattachement-article.service';
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
        MatSortModule,
        ReactiveFormsModule,
        CommonModule,
        MatPaginatorModule,
        TranslocoModule,
        RouterLink,
    ],
    templateUrl: './list.component.html',
    styleUrl: './list.component.scss',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    animations: fuseAnimations,
})
export class ListComponent implements OnInit, OnDestroy {
    @ViewChild(MatPaginator) private _paginator: MatPaginator;
    @ViewChild(MatSort) private _sort: MatSort;

    rattachementArticles$: Observable<RattachementArticle[]>;
    flashMessage: 'success' | 'error' | null = null;
    isLoading: boolean = false;
    rattachementArticlesLength: number;
    searchInputControl: UntypedFormControl = new UntypedFormControl();
    private _unsubscribeAll: Subject<any> = new Subject<any>();
    roleNavigation: RoleNavigation;
    selectedRattachementArticle: RattachementArticle | null = null;
    isViewMode: boolean = false;
    sortActive: string = 'codeArticle';
    sortDirection: 'asc' | 'desc' = 'asc';

    constructor(
        private _pdfExportService: PdfExportService,

        private _csvExportService: CsvExportService,

        private _rattachementArticleService: RattachementArticleService,
        private _changeDetectorRef: ChangeDetectorRef,
        private _fuseConfirmationService: FuseConfirmationService
    ) {}

    SortChange() {
        this.isLoading = true;
        this.getRattachementArticles()
            .pipe(map(() => { this.isLoading = false; this._changeDetectorRef.markForCheck(); }))
            .subscribe();
    }

    getRattachementArticles() {
        return this._rattachementArticleService.GetRattachementArticles(
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
        this.rattachementArticles$ = this._rattachementArticleService.rattachementArticles$;

        this._rattachementArticleService.rattachementArticlesLength$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe(length => {
                this.rattachementArticlesLength = length;
                this._changeDetectorRef.markForCheck();
            });

        this.searchInputControl.valueChanges
            .pipe(
                switchMap(() => { this.isLoading = true; return this.getRattachementArticles(); }),
                map(() => { this.isLoading = false; })
            )
            .subscribe();
    }

    toggleDetails(rattachementArticleId: string): void {
        if (this.selectedRattachementArticle && this.selectedRattachementArticle.rattachementArticleId === rattachementArticleId) {
            this.closeDetails();
            return;
        }

        this.rattachementArticles$.pipe(
            map((rattachementArticles) => {
                const index = rattachementArticles.findIndex(item => item.rattachementArticleId === rattachementArticleId);
                return rattachementArticles[index];
            })
        )
            .subscribe((rattachementArticle) => {
                this.selectedRattachementArticle = rattachementArticle;
                this.isViewMode = true;
                this._changeDetectorRef.markForCheck();
            });
    }

    closeDetails(): void {
        this.selectedRattachementArticle = null;
        this.isViewMode = false;
    }

    deleteRattachementArticle(rattachementArticle: RattachementArticle): void {
        if (!this.hasActionPermission(FuseNavigationAction.Delete)) return;

        const confirmation = this._fuseConfirmationService.open({
            title: 'Delete Rattachement Article',
            message: 'Are you sure you want to remove this record? This action cannot be undone!',
            actions: { confirm: { label: 'Delete' } },
        });

        confirmation.afterClosed().subscribe(result => {
            if (result === 'confirmed') {
                this._rattachementArticleService
                    .DeleteRattachementArticle({ rattachementArticleId: rattachementArticle.rattachementArticleId })
                    .subscribe(() => this._changeDetectorRef.markForCheck());
            }
        });
    }

    trackByFn(index: number, item: any): any {
        return item.rattachementArticleId || index;
    }

    ngOnDestroy(): void {
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }

    protected readonly FuseNavigationAction = FuseNavigationAction;

    exportData(): void {
        if (this._rattachementArticleService) {
            const obs$ = (this as any).rattachementArticle$ || this._rattachementArticleService.rattachementArticle$ || this._rattachementArticleService.rattachementArticle$;
            if (obs$) {
                obs$.pipe(take(1)).subscribe((data: any) => {
                    const items = Array.isArray(data) ? data : (data?.items || data?.rattachementArticle || data?.rattachementArticle || []);
                    if (items && items.length > 0) {
                        const columns = [
            { header: 'Code Article', dataKey: 'codeArticle' },
            { header: 'Désignation', dataKey: 'designation' },
            { header: 'Quantité', dataKey: 'quantite' }
        ];
                        this._pdfExportService.exportToPdf('Rapport Rattachements Articles', columns, items, 'RattachementArticles_Export.pdf');
                    } else {
                        console.warn('No data available to export to PDF');
                    }
                });
            }
        }
    }
}
