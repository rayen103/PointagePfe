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
import { RouterLink } from '@angular/router';
import { fuseAnimations } from '../../../../../@fuse/animations';
import { map, Observable, Subject, switchMap, takeUntil } from 'rxjs';
import { Chantier } from '../../../../core/chantier/chantier.model';
import { ChantierService } from '../../../../core/chantier/chantier.service';
import { FuseConfirmationService } from '../../../../../@fuse/services/confirmation';
import { RoleNavigation } from '../../../../core/role-utilisateur/role-utilisateur.model';
import { FuseNavigationAction } from '../../../../../@fuse/components/navigation';

@Component({
    selector: 'app-list',
    standalone: true,
    imports: [MatButtonModule, MatFormFieldModule, MatIconModule, MatInputModule, MatProgressBarModule,
        MatSortModule, ReactiveFormsModule, CommonModule, MatPaginatorModule, TranslocoModule, RouterLink],
    templateUrl: './list.component.html',
    styleUrl: './list.component.scss',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    animations: fuseAnimations,
})
export class ListComponent implements OnInit, OnDestroy {
    @ViewChild(MatPaginator) private _paginator: MatPaginator;
    @ViewChild(MatSort) private _sort: MatSort;

    chantiers$: Observable<Chantier[]>;
    flashMessage: 'success' | 'error' | null = null;
    isLoading = false;
    chantiersLength: number;
    searchInputControl = new UntypedFormControl();
    private _unsubscribeAll = new Subject<any>();
    roleNavigation: RoleNavigation;
    FuseNavigationAction = FuseNavigationAction;

    constructor(
        private _chantierService: ChantierService,
        private _changeDetectorRef: ChangeDetectorRef,
        private _fuseConfirmationService: FuseConfirmationService
    ) {}

    SortChange() {
        this.isLoading = true;
        this.getChantiers().pipe(map(() => { this.isLoading = false; this._changeDetectorRef.markForCheck(); })).subscribe();
    }

    getChantiers() {
        return this._chantierService.GetChantiers(
            (this._paginator?.pageIndex ?? 0) + 1, this._paginator?.pageSize,
            this._sort?.active, this._sort?.direction, this.searchInputControl.value);
    }

    hasActionPermission(action: FuseNavigationAction): boolean {
        return !this.roleNavigation || this.roleNavigation?.actions?.includes(action);
    }

    ngOnInit(): void {
        this.chantiers$ = this._chantierService.chantiers$;
        this._chantierService.chantiersLength$.pipe(takeUntil(this._unsubscribeAll)).subscribe(l => { this.chantiersLength = l; this._changeDetectorRef.markForCheck(); });
        this.searchInputControl.valueChanges.pipe(takeUntil(this._unsubscribeAll), switchMap(q => this._chantierService.GetChantiers(1, this._paginator?.pageSize, this._sort?.active, this._sort?.direction, q))).subscribe(() => this._changeDetectorRef.markForCheck());
    }

    deleteChantier(chantier: Chantier): void {
        const dialog = this._fuseConfirmationService.open({ title: 'Delete Chantier', message: 'Are you sure?', actions: { confirm: { label: 'Delete' } } });
        dialog.afterClosed().subscribe(result => {
            if (result === 'confirmed') {
                this._chantierService.DeleteChantier({ chantierId: chantier.chantierId }).subscribe(() => this._changeDetectorRef.markForCheck());
            }
        });
    }

    ngOnDestroy(): void { this._unsubscribeAll.next(null); this._unsubscribeAll.complete(); }
}
