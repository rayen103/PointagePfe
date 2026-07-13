import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
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
import { ActivatedRoute, RouterLink } from '@angular/router';
import { fuseAnimations } from '../../../../../@fuse/animations';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { combineLatest, map, Observable, Subject, switchMap, takeUntil, startWith, debounceTime, distinctUntilChanged, of } from 'rxjs';
import { Pointage } from '../../../../core/pointage/pointage.model';
import { PointageService } from '../../../../core/pointage/pointage.service';
import { Bus } from '../../../../core/bus/bus.model';
import { RoleNavigation } from '../../../../core/role-utilisateur/role-utilisateur.model';
import { FuseNavigationAction } from '../../../../../@fuse/components/navigation';
import { MatNativeDateModule } from '@angular/material/core';

@Component({
    selector: 'app-list',
    standalone: true,
    imports: [MatButtonModule, MatFormFieldModule, MatIconModule, MatInputModule, MatProgressBarModule,
        MatSortModule, ReactiveFormsModule, CommonModule, MatPaginatorModule, TranslocoModule, RouterLink,
        MatDatepickerModule, MatNativeDateModule],
    templateUrl: './list.component.html',
    styleUrl: './list.component.scss',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    animations: fuseAnimations,
})
export class ListComponent implements OnInit, OnDestroy {
    @ViewChild(MatPaginator) private _paginator: MatPaginator;
    @ViewChild(MatSort) private _sort: MatSort;

    pointages$: Observable<Pointage[]>;
    isLoading = false;
    pointagesLength: number;
    buses: Bus[] = [];
    roleNavigation: RoleNavigation;
    FuseNavigationAction = FuseNavigationAction;

    // Filters form controls
    searchInputControl = new UntypedFormControl('');
    busIdControl = new UntypedFormControl('');
    isSuccessControl = new UntypedFormControl('');
    startDateControl = new UntypedFormControl(null);
    endDateControl = new UntypedFormControl(null);

    private _unsubscribeAll = new Subject<any>();

    constructor(
        private _pointageService: PointageService,
        private _activatedRoute: ActivatedRoute,
        private _changeDetectorRef: ChangeDetectorRef
    ) {}

    SortChange() {
        this.isLoading = true;
        this._changeDetectorRef.markForCheck();

        let isSuccess: boolean | undefined = undefined;
        const isSuccessStr = this.isSuccessControl.value;
        if (isSuccessStr === 'true') isSuccess = true;
        if (isSuccessStr === 'false') isSuccess = false;

        const startDate = this.startDateControl.value;
        const endDate = this.endDateControl.value;
        let startStr: string | undefined = undefined;
        let endStr: string | undefined = undefined;

        if (startDate) {
            if (startDate instanceof Date) {
                startStr = startDate.toISOString();
            } else if (typeof startDate === 'object' && 'toISO' in startDate) {
                startStr = (startDate as any).toISO();
            } else {
                startStr = startDate.toString();
            }
        }
        if (endDate) {
            if (endDate instanceof Date) {
                endStr = endDate.toISOString();
            } else if (typeof endDate === 'object' && 'toISO' in endDate) {
                endStr = (endDate as any).toISO();
            } else {
                endStr = endDate.toString();
            }
        }

        this._pointageService.GetPointages(
            (this._paginator?.pageIndex ?? 0) + 1,
            this._paginator?.pageSize ?? 10,
            this._sort?.active ?? 'heurePointageUtc',
            (this._sort?.direction ?? 'desc') as 'asc' | 'desc' | '',
            this.searchInputControl.value || '',
            {
                busId: this.busIdControl.value || undefined,
                isSuccess,
                startDate: startStr,
                endDate: endStr
            }
        ).pipe(
            map(() => {
                this.isLoading = false;
                this._changeDetectorRef.markForCheck();
            })
        ).subscribe();
    }

    statTotal = 0;
    statValide = 0;
    statAnomalies = 0;
    statBuses = 0;
    successRate = 100;

    resetFilters() {
        this.searchInputControl.setValue('');
        this.busIdControl.setValue('');
        this.isSuccessControl.setValue('');
        this.startDateControl.setValue(null);
        this.endDateControl.setValue(null);
    }

    hasActionPermission(action: FuseNavigationAction): boolean {
        return !this.roleNavigation || this.roleNavigation?.actions?.includes(action);
    }

    getInitials(name: string): string {
        if (!name) return '—';
        const parts = name.trim().split(/\s+/);
        if (parts.length >= 2) {
            return (parts[0][0] + parts[1][0]).toUpperCase();
        }
        return parts[0].slice(0, 2).toUpperCase();
    }

    getAvatarColor(name: string): string {
        if (!name) return '#cbd5e1';
        const colors = [
            '#3b82f6', // blue
            '#10b981', // emerald
            '#8b5cf6', // violet
            '#f59e0b', // amber
            '#ec4899', // pink
            '#06b6d4', // cyan
            '#84cc16', // lime
            '#f43f5e', // rose
        ];
        let hash = 0;
        for (let i = 0; i < name.length; i++) {
            hash = name.charCodeAt(i) + ((hash << 5) - hash);
        }
        const index = Math.abs(hash) % colors.length;
        return colors[index];
    }

    ngOnInit(): void {
        this.pointages$ = this._pointageService.pointages$;

        // Resolve data
        this._activatedRoute.data.pipe(takeUntil(this._unsubscribeAll)).subscribe(data => {
            this.buses = data.buses?.buses || [];
            this.roleNavigation = data.navigation;
            this._changeDetectorRef.markForCheck();
        });

        this._pointageService.pointagesLength$.pipe(takeUntil(this._unsubscribeAll)).subscribe(l => {
            this.pointagesLength = l;
            this._changeDetectorRef.markForCheck();
        });

        this.pointages$.pipe(takeUntil(this._unsubscribeAll)).subscribe(pointages => {
            if (pointages) {
                this.statTotal = this.pointagesLength || pointages.length;
                const pageTotal = pointages.length;
                const pageValide = pointages.filter(p => p.isSuccess).length;
                this.statValide = pageValide;
                this.statAnomalies = pointages.filter(p => !p.isSuccess).length;
                this.successRate = pageTotal > 0 ? Math.round((pageValide / pageTotal) * 100) : 100;
                this.statBuses = new Set(pointages.map(p => p.busNumeroIMM).filter(Boolean)).size;
                this._changeDetectorRef.markForCheck();
            }
        });

        combineLatest([
            this.searchInputControl.valueChanges.pipe(startWith(''), debounceTime(300), distinctUntilChanged()),
            this.busIdControl.valueChanges.pipe(startWith(''), distinctUntilChanged()),
            this.isSuccessControl.valueChanges.pipe(startWith(''), distinctUntilChanged()),
            this.startDateControl.valueChanges.pipe(startWith(null), distinctUntilChanged()),
            this.endDateControl.valueChanges.pipe(startWith(null), distinctUntilChanged())
        ]).pipe(
            takeUntil(this._unsubscribeAll),
            switchMap(([search, busId, isSuccessStr, startDate, endDate]) => {
                // If one date is selected but not both, and they were not cleared, wait
                if ((startDate && !endDate) || (!startDate && endDate)) {
                    return of(null);
                }

                this.isLoading = true;
                this._changeDetectorRef.markForCheck();

                let isSuccess: boolean | undefined = undefined;
                if (isSuccessStr === 'true') isSuccess = true;
                if (isSuccessStr === 'false') isSuccess = false;

                let startStr: string | undefined = undefined;
                let endStr: string | undefined = undefined;

                if (startDate) {
                    if (startDate instanceof Date) {
                        startStr = startDate.toISOString();
                    } else if (typeof startDate === 'object' && 'toISO' in startDate) {
                        startStr = (startDate as any).toISO();
                    } else {
                        startStr = startDate.toString();
                    }
                }
                if (endDate) {
                    if (endDate instanceof Date) {
                        endStr = endDate.toISOString();
                    } else if (typeof endDate === 'object' && 'toISO' in endDate) {
                        endStr = (endDate as any).toISO();
                    } else {
                        endStr = endDate.toString();
                    }
                }

                return this._pointageService.GetPointages(
                    1,
                    this._paginator?.pageSize ?? 10,
                    this._sort?.active ?? 'heurePointageUtc',
                    (this._sort?.direction ?? 'desc') as 'asc' | 'desc' | '',
                    search || '',
                    {
                        busId: busId || undefined,
                        isSuccess,
                        startDate: startStr,
                        endDate: endStr
                    }
                ).pipe(
                    map(() => {
                        this.isLoading = false;
                        this._changeDetectorRef.markForCheck();
                    })
                );
            })
        ).subscribe();
    }

    ngOnDestroy(): void {
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }
}
