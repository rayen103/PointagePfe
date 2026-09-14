import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { take } from 'rxjs';
import { TranslocoModule } from '@ngneat/transloco';
import { PdfExportService } from '../../../../core/common/pdf-export.service';
import { CsvExportService } from '../../../../core/common/csv-export.service';
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
import { Bus } from '../../../../core/bus/bus.model';
import { BusService } from '../../../../core/bus/bus.service';
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

    bus$: Observable<Bus[]>;

    flashMessage: 'success' | 'error' | null = null;
    isLoading: boolean = false;
    busesLength: number;
    searchInputControl: UntypedFormControl = new UntypedFormControl();
    private _unsubscribeAll: Subject<any> = new Subject<any>();
    roleNavigation: RoleNavigation;
    selectedBus: Bus | null = null;
    isViewMode: boolean = false;
    sortActive: string = 'numeroIMM';
    sortDirection: 'asc' | 'desc' = 'asc';

    constructor(
        private _csvExportService: CsvExportService,
        private _pdfExportService: PdfExportService,
        private _busService: BusService,
        private _changeDetectorRef: ChangeDetectorRef,
        private _fuseConfirmationService: FuseConfirmationService
    ) {}

    SortChange() {
        this.isLoading = true;
        this.getBuses()
            .pipe(
                map(() => {
                    this.isLoading = false;
                    this._changeDetectorRef.markForCheck();
                })
            )
            .subscribe();
    }

    getBuses() {
        return this._busService.GetBuses(
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
        this.bus$ = this._busService.buses$;

        this._busService.busesLength$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((length) => {
                this.busesLength = length;
                this._changeDetectorRef.markForCheck();
            });
        this.searchInputControl.valueChanges
            .pipe(
                switchMap((query) => {
                    this.isLoading = true;
                    return this.getBuses();
                }),
                map(() => {
                    this.isLoading = false;
                })
            )
            .subscribe();
    }

    toggleDetails(busId: string): void {
        if (this.selectedBus && this.selectedBus.busId === busId) {
            this.closeDetails();
            return;
        }

        this.bus$.pipe(
            map((buses) => {
                const index = buses.findIndex(item => item.busId === busId);
                return buses[index];
            })
        )
            .subscribe((bus) => {
                this.selectedBus = bus;
                this.isViewMode = true;
                this._changeDetectorRef.markForCheck();
            });
    }

    editBus(busId: string): void {
        if (this.selectedBus && this.selectedBus.busId === busId) {
            this.closeDetails();
            return;
        }

        this.bus$.pipe(
            map((buses) => {
                const index = buses.findIndex(item => item.busId === busId);
                return buses[index];
            })
        )
            .subscribe((bus) => {
                this.selectedBus = bus;
                this.isViewMode = false;
                this._changeDetectorRef.markForCheck();
            });
    }

    closeDetails(): void {
        this.selectedBus = null;
        this.isViewMode = false;
    }

    deleteSelectedBus(bus: Bus): void {
        if (!this.hasActionPermission(FuseNavigationAction.Delete)) {
            return;
        }
        const confirmation = this._fuseConfirmationService.open({
            title: 'Delete Bus',
            message:
                'Are you sure you want to remove this bus? This action cannot be undone!',
            actions: {
                confirm: {
                    label: 'Delete',
                },
            },
        });

        confirmation.afterClosed().subscribe((result) => {
            if (result === 'confirmed') {
                this._busService
                    .DeleteBus({ busId: bus.busId })
                    .subscribe(() => {
                        this._changeDetectorRef.markForCheck();
                    });
            }
        });
    }

    trackByFn(index: number, item: any): any {
        return item.busId || index;
    }

    ngOnDestroy(): void {
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }

    protected readonly FuseNavigationAction = FuseNavigationAction;

    exportData(): void {
        if (this._busService) {
            const obs$ = (this as any).buses$ || this._busService.buses$ || this._busService.bus$;
            if (obs$) {
                obs$.pipe(take(1)).subscribe((data: any) => {
                    const items = Array.isArray(data) ? data : (data?.items || data?.buses || data?.bus || []);
                    if (items && items.length > 0) {
                        const columns = [
            { header: 'Immatriculation', dataKey: 'numeroIMM' },
            { header: 'Modèle', dataKey: 'modelBus' },
            { header: 'Capacité', dataKey: 'capacite' },
            { header: 'Occupants', dataKey: 'currentOccupancy' },
            { header: 'Code Circuit', dataKey: 'codeCircuit' },
            { header: 'Statut Actif', dataKey: 'isActive' }
        ];
                        this._pdfExportService.exportToPdf('Rapport Flotte des Bus', columns, items, 'Bus_Export.pdf');
                    } else {
                        console.warn('No data available to export to PDF');
                    }
                });
            }
        }
    }
}
