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
import { Utilisateur } from '../../../../../core/utilisateurs/utilisateur.model';
import { UtilisateurService } from '../../../../../core/utilisateurs/utilisateur.service';
import { FuseConfirmationService } from '../../../../../../@fuse/services/confirmation';
import { RoleNavigation, RoleUtilisateur } from '../../../../../core/role-utilisateur/role-utilisateur.model';
import { FuseNavigationAction } from '../../../../../../@fuse/components/navigation';

@Component({
    selector: 'app-utilisateur-list',
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

    utilisateur$: Observable<Utilisateur[]>;
    roleUtilisateurs: RoleUtilisateur[] = [];

    isLoading: boolean = false;
    utilisateurslength: number;
    searchInputControl: UntypedFormControl = new UntypedFormControl();
    private _unsubscribeAll: Subject<any> = new Subject<any>();
    roleNavigation: RoleNavigation;
    sortActive: string = 'nomUtilisateur';
    sortDirection: 'asc' | 'desc' = 'asc';

    constructor(
        private _utilisateurService: UtilisateurService,
        private _changeDetectorRef: ChangeDetectorRef,
        private _fuseConfirmationService: FuseConfirmationService,
        private _activatedRoute: ActivatedRoute
    ) {}

    SortChange() {
        this.isLoading = true;
        this.getUtilisateurs()
            .pipe(
                map(() => {
                    this.isLoading = false;
                    this._changeDetectorRef.markForCheck();
                })
            )
            .subscribe();
    }

    getUtilisateurs() {
        return this._utilisateurService.GetUtilisateur(
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

    getRoleLabel(roleUtilisateurId: string): string {
        const role = this.roleUtilisateurs.find((r) => r.roleUtilisateurId === roleUtilisateurId);
        return role ? role.libelleRoleUtilisateur : '';
    }

    ngOnInit(): void {
        this.utilisateur$ = this._utilisateurService.utilisateurs$;

        this._activatedRoute.data
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((data) => {
                if (data?.navigation) {
                    this.roleNavigation = data.navigation;
                }
                if (data?.roles) {
                    this.roleUtilisateurs = data.roles;
                }
                this._changeDetectorRef.markForCheck();
            });

        this._utilisateurService.utilisateurLength$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((length) => {
                this.utilisateurslength = length;
                this._changeDetectorRef.markForCheck();
            });

        this.searchInputControl.valueChanges
            .pipe(
                takeUntil(this._unsubscribeAll),
                switchMap(() => {
                    this.isLoading = true;
                    return this.getUtilisateurs();
                }),
                map(() => {
                    this.isLoading = false;
                    this._changeDetectorRef.markForCheck();
                })
            )
            .subscribe();
    }

    deleteSelectedUtilisateur(utilisateur: Utilisateur): void {
        if (!this.hasActionPermission(FuseNavigationAction.Delete)) {
            return;
        }
        const confirmation = this._fuseConfirmationService.open({
            icon: { show: false },
            title: 'Supprimer cet utilisateur',
            message:
                'Êtes-vous sûr de vouloir supprimer cet utilisateur? Cette action ne peut pas être annulée!',
            actions: {
                confirm: { label: 'Supprimer' },
                cancel: { label: 'Annuler' },
            },
        });

        confirmation.afterClosed().subscribe((result) => {
            if (result === 'confirmed') {
                this._utilisateurService
                    .DeleteUtilisateur({ utilisateurId: utilisateur.utilisateurId })
                    .subscribe(() => {
                        this.SortChange();
                        this._changeDetectorRef.markForCheck();
                    });
            }
        });
    }

    trackByFn(index: number, item: any): any {
        return item.utilisateurId || index;
    }

    ngOnDestroy(): void {
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }

    protected readonly FuseNavigationAction = FuseNavigationAction;
}
