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
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { RouterLink } from '@angular/router';
import { fuseAnimations } from '../../../../../@fuse/animations';
import { map, Observable, Subject, switchMap, takeUntil } from 'rxjs';
import { PointCollecte } from '../../../../core/point-collecte/point-collecte.model';
import { PointCollecteService } from '../../../../core/point-collecte/point-collecte.service';
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

    pointCollecte$: Observable<PointCollecte[]>;

    flashMessage: 'success' | 'error' | null = null;
    isLoading: boolean = false;
    pointsCollecteLength: number;
    searchInputControl: UntypedFormControl = new UntypedFormControl();
    private _unsubscribeAll: Subject<any> = new Subject<any>();
    roleNavigation: RoleNavigation;
    selectedPointCollecte: PointCollecte | null = null;
    isViewMode: boolean = false;

    constructor(
        private _pointCollecteService: PointCollecteService,
        private _changeDetectorRef: ChangeDetectorRef,
        private _fuseConfirmationService: FuseConfirmationService
    ) {}

    SortChange() {
        this.isLoading = true;
        this.getPointsCollecte()
            .pipe(
                map(() => {
                    this.isLoading = false;

                    // Mark for check
                    this._changeDetectorRef.markForCheck();
                })
            )
            .subscribe();
    }

    getPointsCollecte() {
        return this._pointCollecteService.GetPointCollecte(
            (this._paginator?.pageIndex | 0) + 1,
            this._paginator?.pageSize,
            this._sort?.active,
            this._sort?.direction,
            this.searchInputControl.value
        );
    }

    hasActionPermission(action: FuseNavigationAction): boolean {
        return !this.roleNavigation || this.roleNavigation?.actions?.includes(action);
    }

    ngOnInit(): void {
        this.pointCollecte$ = this._pointCollecteService.pointsCollecte$;

        this._pointCollecteService.pointsCollecteLength$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((length) => {
                this.pointsCollecteLength = length;

                // Mark for check
                this._changeDetectorRef.markForCheck();
            });
        this.searchInputControl.valueChanges
            .pipe(
                switchMap((query) => {
                    this.isLoading = true;
                    return this.getPointsCollecte();
                }),
                map(() => {
                    this.isLoading = false;
                })
            )
            .subscribe();
    }

    /**
     * Toggle point collecte details for viewing (read-only mode)
     *
     * @param pointCollecteId
     */
    toggleDetails(pointCollecteId: string): void {
        if (this.selectedPointCollecte && this.selectedPointCollecte.pointCollecteId === pointCollecteId) {
            this.closeDetails();
            return;
        }

        this.pointCollecte$.pipe(
            map((pointsCollecte) => {
                const index = pointsCollecte.findIndex(item => item.pointCollecteId === pointCollecteId);
                return pointsCollecte[index];
            })
        )
            .subscribe((pointCollecte) => {
                this.selectedPointCollecte = pointCollecte;
                this.isViewMode = true;

                this._changeDetectorRef.markForCheck();
            });
    }

    /**
     * Edit point collecte - opens details in edit mode
     *
     * @param pointCollecteId
     */
    editPointCollecte(pointCollecteId: string): void {
        if (this.selectedPointCollecte && this.selectedPointCollecte.pointCollecteId === pointCollecteId) {
            this.closeDetails();
            return;
        }

        this.pointCollecte$.pipe(
            map((pointsCollecte) => {
                const index = pointsCollecte.findIndex(item => item.pointCollecteId === pointCollecteId);
                return pointsCollecte[index];
            })
        )
            .subscribe((pointCollecte) => {
                this.selectedPointCollecte = pointCollecte;
                this.isViewMode = false;

                this._changeDetectorRef.markForCheck();
            });
    }

    /**
     * Close the details
     */
    closeDetails(): void {
        this.selectedPointCollecte = null;
        this.isViewMode = false;
    }

    /**
     * Delete the selected point collecte
     */
    deleteSelectedPointCollecte(pointCollecte: PointCollecte): void {
        if (!this.hasActionPermission(FuseNavigationAction.Delete)) {
            return;
        }
        const confirmation = this._fuseConfirmationService.open({
            title: 'Delete Point Collecte',
            message:
                'Are you sure you want to remove this point collecte? This action cannot be undone!',
            actions: {
                confirm: {
                    label: 'Delete',
                },
            },
        });

        confirmation.afterClosed().subscribe((result) => {
            if (result === 'confirmed') {
                this._pointCollecteService
                    .DeletePointCollecte({ pointCollecteId: pointCollecte.pointCollecteId })
                    .subscribe(() => {
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
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }

    protected readonly FuseNavigationAction = FuseNavigationAction;
}
