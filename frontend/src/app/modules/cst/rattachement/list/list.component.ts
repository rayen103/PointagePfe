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
import { Rattachement } from '../../../../core/rattachement/rattachement.model';
import { RattachementService } from '../../../../core/rattachement/rattachement.service';
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

    rattachement$: Observable<Rattachement[]>;

    flashMessage: 'success' | 'error' | null = null;
    isLoading: boolean = false;
    rattachementsLength: number;
    searchInputControl: UntypedFormControl = new UntypedFormControl();
    private _unsubscribeAll: Subject<any> = new Subject<any>();
    roleNavigation: RoleNavigation;
    selectedRattachement: Rattachement | null = null;
    isViewMode: boolean = false;

    constructor(
        private _rattachementService: RattachementService,
        private _changeDetectorRef: ChangeDetectorRef,
        private _fuseConfirmationService: FuseConfirmationService
    ) {}

    SortChange() {
        this.isLoading = true;
        this.getRattachements()
            .pipe(
                map(() => {
                    this.isLoading = false;

                    // Mark for check
                    this._changeDetectorRef.markForCheck();
                })
            )
            .subscribe();
    }

    getRattachements() {
        return this._rattachementService.GetRattachements(
            (this._paginator?.pageIndex ?? 0) + 1,
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
        this.rattachement$ = this._rattachementService.rattachements$;

        this._rattachementService.rattachementsLength$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((length) => {
                this.rattachementsLength = length;

                // Mark for check
                this._changeDetectorRef.markForCheck();
            });
        this.searchInputControl.valueChanges
            .pipe(
                switchMap((query) => {
                    this.isLoading = true;
                    return this.getRattachements();
                }),
                map(() => {
                    this.isLoading = false;
                })
            )
            .subscribe();
    }

    /**
     * Toggle rattachement details for viewing (read-only mode)
     *
     * @param rattachementId
     */
    toggleDetails(rattachementId: string): void {
        if (this.selectedRattachement && this.selectedRattachement.rattachementId === rattachementId) {
            this.closeDetails();
            return;
        }

        this.rattachement$.pipe(
            map((rattachements) => {
                const index = rattachements.findIndex(item => item.rattachementId === rattachementId);
                return rattachements[index];
            })
        )
            .subscribe((rattachement) => {
                this.selectedRattachement = rattachement;
                this.isViewMode = true;

                this._changeDetectorRef.markForCheck();
            });
    }

    /**
     * Edit rattachement - opens details in edit mode
     *
     * @param rattachementId
     */
    editRattachement(rattachementId: string): void {
        if (this.selectedRattachement && this.selectedRattachement.rattachementId === rattachementId) {
            this.closeDetails();
            return;
        }

        this.rattachement$.pipe(
            map((rattachements) => {
                const index = rattachements.findIndex(item => item.rattachementId === rattachementId);
                return rattachements[index];
            })
        )
            .subscribe((rattachement) => {
                this.selectedRattachement = rattachement;
                this.isViewMode = false;

                this._changeDetectorRef.markForCheck();
            });
    }

    /**
     * Close the details
     */
    closeDetails(): void {
        this.selectedRattachement = null;
        this.isViewMode = false;
    }

    /**
     * Delete the selected rattachement
     */
    deleteSelectedRattachement(rattachement: Rattachement): void {
        if (!this.hasActionPermission(FuseNavigationAction.Delete)) {
            return;
        }
        const confirmation = this._fuseConfirmationService.open({
            title: 'Delete Rattachement',
            message:
                'Are you sure you want to remove this rattachement? This action cannot be undone!',
            actions: {
                confirm: {
                    label: 'Delete',
                },
            },
        });

        confirmation.afterClosed().subscribe((result) => {
            if (result === 'confirmed') {
                this._rattachementService
                    .DeleteRattachement({ rattachementId: rattachement.rattachementId })
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
        return item.rattachementId || index;
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
