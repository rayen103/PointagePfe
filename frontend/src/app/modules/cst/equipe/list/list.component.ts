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
import { Equipe } from '../../../../core/equipe/equipe.model';
import { EquipeService } from '../../../../core/equipe/equipe.service';
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
  styleUrl: './list.component.scss',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    animations: fuseAnimations,
})
export class ListComponent implements OnInit, OnDestroy {
    @ViewChild(MatPaginator) private _paginator: MatPaginator;

    equipe$: Observable<Equipe[]>;

    flashMessage: 'success' | 'error' | null = null;
    isLoading: boolean = false;
    equipesLength: number;
    searchInputControl: UntypedFormControl = new UntypedFormControl();
    private _unsubscribeAll: Subject<any> = new Subject<any>();
    roleNavigation: RoleNavigation;
    selectedEquipe: Equipe | null = null;
    isViewMode: boolean = false;
    sortActive: string = 'codeEquipe';
    sortDirection: 'asc' | 'desc' = 'asc';

    constructor(
        private _equipeService: EquipeService,
        private _changeDetectorRef: ChangeDetectorRef,
        private _fuseConfirmationService: FuseConfirmationService
    ) {}

    SortChange() {
        this.isLoading = true;
        this.getEquipes()
            .pipe(
                map(() => {
                    this.isLoading = false;

                    // Mark for check
                    this._changeDetectorRef.markForCheck();
                })
            )
            .subscribe();
    }

    getEquipes() {
        return this._equipeService.GetEquipes(
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
        this.equipe$ = this._equipeService.equipes$;

        this._equipeService.equipesLength$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((length) => {
                this.equipesLength = length;

                // Mark for check
                this._changeDetectorRef.markForCheck();
            });
        this.searchInputControl.valueChanges
            .pipe(
                switchMap((query) => {
                    this.isLoading = true;
                    return this.getEquipes();
                }),
                map(() => {
                    this.isLoading = false;
                })
            )
            .subscribe();
    }

    /**
     * Toggle equipe details for viewing (read-only mode)
     *
     * @param equipeId
     */
    toggleDetails(equipeId: string): void {
        if (this.selectedEquipe && this.selectedEquipe.equipeId === equipeId) {
            this.closeDetails();
            return;
        }

        this.equipe$.pipe(
            map((equipes) => {
                const index = equipes.findIndex(item => item.equipeId === equipeId);
                return equipes[index];
            })
        )
            .subscribe((equipe) => {
                this.selectedEquipe = equipe;
                this.isViewMode = true;

                this._changeDetectorRef.markForCheck();
            });
    }

    /**
     * Edit equipe - opens details in edit mode
     *
     * @param equipeId
     */
    editEquipe(equipeId: string): void {
        if (this.selectedEquipe && this.selectedEquipe.equipeId === equipeId) {
            this.closeDetails();
            return;
        }

        this.equipe$.pipe(
            map((equipes) => {
                const index = equipes.findIndex(item => item.equipeId === equipeId);
                return equipes[index];
            })
        )
            .subscribe((equipe) => {
                this.selectedEquipe = equipe;
                this.isViewMode = false;

                this._changeDetectorRef.markForCheck();
            });
    }

    /**
     * Close the details
     */
    closeDetails(): void {
        this.selectedEquipe = null;
        this.isViewMode = false;
    }

    /**
     * Delete the selected equipe
     */
    deleteSelectedEquipe(equipe: Equipe): void {
        if (!this.hasActionPermission(FuseNavigationAction.Delete)) {
            return;
        }
        const confirmation = this._fuseConfirmationService.open({
            title: 'Delete Equipe',
            message:
                'Are you sure you want to remove this equipe? This action cannot be undone!',
            actions: {
                confirm: {
                    label: 'Delete',
                },
            },
        });

        confirmation.afterClosed().subscribe((result) => {
            if (result === 'confirmed') {
                this._equipeService
                    .DeleteEquipe({ equipeId: equipe.equipeId })
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
        return item.equipeId || index;
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
