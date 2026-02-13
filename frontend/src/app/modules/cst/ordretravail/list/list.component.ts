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
import { OrdreTravail } from '../../../../core/ordre-travail/ordre-travail.model';
import { OrdreTravailService } from '../../../../core/ordre-travail/ordre-travail.service';
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

    ordreTravail$: Observable<OrdreTravail[]>;

    flashMessage: 'success' | 'error' | null = null;
    isLoading: boolean = false;
    ordresTravailLength: number;
    searchInputControl: UntypedFormControl = new UntypedFormControl();
    private _unsubscribeAll: Subject<any> = new Subject<any>();
    roleNavigation: RoleNavigation;
    selectedOrdreTravail: OrdreTravail | null = null;
    isViewMode: boolean = false;

    constructor(
        private _ordreTravailService: OrdreTravailService,
        private _changeDetectorRef: ChangeDetectorRef,
        private _fuseConfirmationService: FuseConfirmationService
    ) {}

    SortChange() {
        this.isLoading = true;
        this.getOrdresTravail()
            .pipe(
                map(() => {
                    this.isLoading = false;

                    // Mark for check
                    this._changeDetectorRef.markForCheck();
                })
            )
            .subscribe();
    }

    getOrdresTravail() {
        return this._ordreTravailService.GetOrdresTravail(
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
        this.ordreTravail$ = this._ordreTravailService.ordresTravail$;

        this._ordreTravailService.ordresTravailLength$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((length) => {
                this.ordresTravailLength = length;

                // Mark for check
                this._changeDetectorRef.markForCheck();
            });
        this.searchInputControl.valueChanges
            .pipe(
                switchMap((query) => {
                    this.isLoading = true;
                    return this.getOrdresTravail();
                }),
                map(() => {
                    this.isLoading = false;
                })
            )
            .subscribe();
    }

    /**
     * Toggle ordre travail details for viewing (read-only mode)
     *
     * @param ordreTravailId
     */
    toggleDetails(ordreTravailId: string): void {
        if (this.selectedOrdreTravail && this.selectedOrdreTravail.ordreTravailId === ordreTravailId) {
            this.closeDetails();
            return;
        }

        this.ordreTravail$.pipe(
            map((ordresTravail) => {
                const index = ordresTravail.findIndex(item => item.ordreTravailId === ordreTravailId);
                return ordresTravail[index];
            })
        )
            .subscribe((ordreTravail) => {
                this.selectedOrdreTravail = ordreTravail;
                this.isViewMode = true;

                this._changeDetectorRef.markForCheck();
            });
    }

    /**
     * Edit ordre travail - opens details in edit mode
     *
     * @param ordreTravailId
     */
    editOrdreTravail(ordreTravailId: string): void {
        if (this.selectedOrdreTravail && this.selectedOrdreTravail.ordreTravailId === ordreTravailId) {
            this.closeDetails();
            return;
        }

        this.ordreTravail$.pipe(
            map((ordresTravail) => {
                const index = ordresTravail.findIndex(item => item.ordreTravailId === ordreTravailId);
                return ordresTravail[index];
            })
        )
            .subscribe((ordreTravail) => {
                this.selectedOrdreTravail = ordreTravail;
                this.isViewMode = false;

                this._changeDetectorRef.markForCheck();
            });
    }

    /**
     * Close the details
     */
    closeDetails(): void {
        this.selectedOrdreTravail = null;
        this.isViewMode = false;
    }

    /**
     * Delete the selected ordre travail
     */
    deleteSelectedOrdreTravail(ordreTravail: OrdreTravail): void {
        if (!this.hasActionPermission(FuseNavigationAction.Delete)) {
            return;
        }
        const confirmation = this._fuseConfirmationService.open({
            title: 'Delete Ordre de Travail',
            message:
                'Are you sure you want to remove this ordre de travail? This action cannot be undone!',
            actions: {
                confirm: {
                    label: 'Delete',
                },
            },
        });

        confirmation.afterClosed().subscribe((result) => {
            if (result === 'confirmed') {
                this._ordreTravailService
                    .DeleteOrdreTravail({ ordreTravailId: ordreTravail.ordreTravailId })
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
        return item.ordreTravailId || index;
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
