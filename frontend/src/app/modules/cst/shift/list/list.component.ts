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
import { Shift } from '../../../../core/shift/shift.model';
import { ShiftService } from '../../../../core/shift/shift.service';
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

    shift$: Observable<Shift[]>;

    flashMessage: 'success' | 'error' | null = null;
    isLoading: boolean = false;
    shiftsLength: number;
    searchInputControl: UntypedFormControl = new UntypedFormControl();
    private _unsubscribeAll: Subject<any> = new Subject<any>();
    roleNavigation: RoleNavigation;
    selectedShift: Shift | null = null;
    isViewMode: boolean = false;

    constructor(
        private _shiftService: ShiftService,
        private _changeDetectorRef: ChangeDetectorRef,
        private _fuseConfirmationService: FuseConfirmationService
    ) {}

    SortChange() {
        this.isLoading = true;
        this.getShifts()
            .pipe(
                map(() => {
                    this.isLoading = false;
                    this._changeDetectorRef.markForCheck();
                })
            )
            .subscribe();
    }

    getShifts() {
        return this._shiftService.GetShifts(
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
        this.shift$ = this._shiftService.shifts$;

        this._shiftService.shiftsLength$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((length) => {
                this.shiftsLength = length;
                this._changeDetectorRef.markForCheck();
            });
        this.searchInputControl.valueChanges
            .pipe(
                switchMap((query) => {
                    this.isLoading = true;
                    return this.getShifts();
                }),
                map(() => {
                    this.isLoading = false;
                })
            )
            .subscribe();
    }

    toggleDetails(shiftId: string): void {
        if (this.selectedShift && this.selectedShift.shiftId === shiftId) {
            this.closeDetails();
            return;
        }

        this.shift$.pipe(
            map((shifts) => {
                const index = shifts.findIndex(item => item.shiftId === shiftId);
                return shifts[index];
            })
        )
            .subscribe((shift) => {
                this.selectedShift = shift;
                this.isViewMode = true;
                this._changeDetectorRef.markForCheck();
            });
    }

    editShift(shiftId: string): void {
        if (this.selectedShift && this.selectedShift.shiftId === shiftId) {
            this.closeDetails();
            return;
        }

        this.shift$.pipe(
            map((shifts) => {
                const index = shifts.findIndex(item => item.shiftId === shiftId);
                return shifts[index];
            })
        )
            .subscribe((shift) => {
                this.selectedShift = shift;
                this.isViewMode = false;
                this._changeDetectorRef.markForCheck();
            });
    }

    closeDetails(): void {
        this.selectedShift = null;
        this.isViewMode = false;
    }

    deleteSelectedShift(shift: Shift): void {
        if (!this.hasActionPermission(FuseNavigationAction.Delete)) {
            return;
        }
        const confirmation = this._fuseConfirmationService.open({
            title: 'Delete Shift',
            message:
                'Are you sure you want to remove this shift? This action cannot be undone!',
            actions: {
                confirm: {
                    label: 'Delete',
                },
            },
        });

        confirmation.afterClosed().subscribe((result) => {
            if (result === 'confirmed') {
                this._shiftService
                    .DeleteShift({ shiftId: shift.shiftId })
                    .subscribe(() => {
                        this._changeDetectorRef.markForCheck();
                    });
            }
        });
    }

    trackByFn(index: number, item: any): any {
        return item.shiftId || index;
    }

    ngOnDestroy(): void {
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }

    protected readonly FuseNavigationAction = FuseNavigationAction;
}
