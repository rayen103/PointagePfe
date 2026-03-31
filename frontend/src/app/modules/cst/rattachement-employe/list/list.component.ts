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
import { RattachementEmploye } from '../../../../core/rattachement-employe/rattachement-employe.model';
import { RattachementEmployeService } from '../../../../core/rattachement-employe/rattachement-employe.service';
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

    rattachementEmployes$: Observable<RattachementEmploye[]>;
    flashMessage: 'success' | 'error' | null = null;
    isLoading: boolean = false;
    rattachementEmployesLength: number;
    searchInputControl: UntypedFormControl = new UntypedFormControl();
    private _unsubscribeAll: Subject<any> = new Subject<any>();
    roleNavigation: RoleNavigation;

    constructor(
        private _rattachementEmployeService: RattachementEmployeService,
        private _changeDetectorRef: ChangeDetectorRef,
        private _fuseConfirmationService: FuseConfirmationService
    ) {}

    SortChange() {
        this.isLoading = true;
        this.getRattachementEmployes()
            .pipe(map(() => { this.isLoading = false; this._changeDetectorRef.markForCheck(); }))
            .subscribe();
    }

    getRattachementEmployes() {
        return this._rattachementEmployeService.GetRattachementEmployes(
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
        this.rattachementEmployes$ = this._rattachementEmployeService.rattachementEmployes$;

        this._rattachementEmployeService.rattachementEmployesLength$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe(length => {
                this.rattachementEmployesLength = length;
                this._changeDetectorRef.markForCheck();
            });

        this.searchInputControl.valueChanges
            .pipe(
                switchMap(() => { this.isLoading = true; return this.getRattachementEmployes(); }),
                map(() => { this.isLoading = false; })
            )
            .subscribe();
    }

    deleteRattachementEmploye(rattachementEmploye: RattachementEmploye): void {
        if (!this.hasActionPermission(FuseNavigationAction.Delete)) return;

        const confirmation = this._fuseConfirmationService.open({
            title: 'Delete Rattachement Employe',
            message: 'Are you sure you want to remove this record? This action cannot be undone!',
            actions: { confirm: { label: 'Delete' } },
        });

        confirmation.afterClosed().subscribe(result => {
            if (result === 'confirmed') {
                this._rattachementEmployeService
                    .DeleteRattachementEmploye({ rattachementEmployeId: rattachementEmploye.rattachementEmployeId })
                    .subscribe(() => this._changeDetectorRef.markForCheck());
            }
        });
    }

    trackByFn(index: number, item: any): any {
        return item.rattachementEmployeId || index;
    }

    ngOnDestroy(): void {
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }

    protected readonly FuseNavigationAction = FuseNavigationAction;
}
