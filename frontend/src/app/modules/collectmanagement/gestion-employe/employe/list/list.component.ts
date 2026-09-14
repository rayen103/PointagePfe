import { CsvExportService } from '../../../../../core/common/csv-export.service';
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
import { RouterLink, ActivatedRoute } from '@angular/router';
import { fuseAnimations } from '../../../../../../@fuse/animations';
import { map, Observable, Subject, switchMap, takeUntil } from 'rxjs';
import { Employe } from '../../../../../core/employes/employe.model';
import { EmployeService } from '../../../../../core/employes/employe.service';
import { FuseConfirmationService } from '../../../../../../@fuse/services/confirmation';
import { RoleNavigation } from '../../../../../core/role-utilisateur/role-utilisateur.model';
import { FuseNavigationAction } from '../../../../../../@fuse/components/navigation';

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

    employe$: Observable<Employe[]>;

    flashMessage: 'success' | 'error' | null = null;
    isLoading: boolean = false;
    employeslength: number;
    searchInputControl: UntypedFormControl = new UntypedFormControl();
    private _unsubscribeAll: Subject<any> = new Subject<any>();
    roleNavigation: RoleNavigation;
    selectedEmploye: Employe | null = null;
    isViewMode: boolean = false;
    sortActive: string = 'matricule';
    sortDirection: 'asc' | 'desc' = 'asc';

    constructor(
        private _csvExportService: CsvExportService,

        private _employeService: EmployeService,
        private _changeDetectorRef: ChangeDetectorRef,
        private _fuseConfirmationService: FuseConfirmationService,
        private _activatedRoute: ActivatedRoute
    ) {}

    SortChange() {
        this.isLoading = true;
        this.getEmployes()
            .pipe(
                map(() => {
                    this.isLoading = false;
                    this._changeDetectorRef.markForCheck();
                })
            )
            .subscribe();
    }

    getEmployes() {
        return this._employeService.GetEmploye(
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
        this.employe$ = this._employeService.employes$;

        this._activatedRoute.data
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((data) => {
                if (data?.navigation) {
                    this.roleNavigation = data.navigation;
                }
            });

        this._employeService.employeLength$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((length) => {
                this.employeslength = length;
                this._changeDetectorRef.markForCheck();
            });

        this.searchInputControl.valueChanges
            .pipe(
                takeUntil(this._unsubscribeAll),
                switchMap((query) => {
                    this.isLoading = true;
                    return this.getEmployes();
                }),
                map(() => {
                    this.isLoading = false;
                    this._changeDetectorRef.markForCheck();
                })
            )
            .subscribe();
    }

    deleteSelectedEmploye(employe: Employe): void {
        if (!this.hasActionPermission(FuseNavigationAction.Delete)) {
            return;
        }
        const confirmation = this._fuseConfirmationService.open({
            title: 'Supprimer cet employé',
            message:
                'Êtes-vous sûr de vouloir supprimer cet employé? Cette action ne peut pas être annulée!',
            actions: {
                confirm: {
                    label: 'Supprimer',
                },
                cancel: {
                    label: 'Annuler',
                }
            },
        });

        confirmation.afterClosed().subscribe((result) => {
            if (result === 'confirmed') {
                this._employeService
                    .DeleteEmploye({ employeId: employe.employeId })
                    .subscribe(() => {
                        this.SortChange();
                        this._changeDetectorRef.markForCheck();
                    });
            }
        });
    }

    trackByFn(index: number, item: any): any {
        return item.employeId || index;
    }

    ngOnDestroy(): void {
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }

    protected readonly FuseNavigationAction = FuseNavigationAction;

    exportData(): void {
        if (this._employeService && this._employeService.employes$) {
            this._employeService.employes$.pipe(take(1)).subscribe((data: any) => {
                const items = Array.isArray(data) ? data : (data?.items || data?.employes || []);
                if (items && items.length > 0) {
                    this._csvExportService.exportToCsv('Employe_Export', items);
                } else {
                    console.warn('No data available to export');
                }
            });
        }
    }
}
