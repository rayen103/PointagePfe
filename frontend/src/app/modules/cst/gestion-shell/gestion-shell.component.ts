import { ChangeDetectionStrategy, ChangeDetectorRef, Component, inject, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { UserService } from '../../../core/user/user.service';

export interface GestionItem {
    id: string;
    label: string;
    link: string;
}

export interface GestionGroup {
    label: string;
    items: GestionItem[];
}

/**
 * Gestion shell — a persistent secondary sidebar (Design Canvas 1d) that wraps every
 * `/fichier/*` module. Only entities permitted by the user's role are visible.
 */
@Component({
    selector: 'app-gestion-shell',
    standalone: true,
    imports: [RouterOutlet, RouterLink, RouterLinkActive],
    templateUrl: './gestion-shell.component.html',
    styleUrl: './gestion-shell.component.scss',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class GestionShellComponent implements OnInit, OnDestroy {
    private _userService = inject(UserService);
    private _router = inject(Router);
    private _cdr = inject(ChangeDetectorRef);
    private _destroy$ = new Subject<void>();

    readonly allGroups: GestionGroup[] = [
        {
            label: 'Flotte & réseau',
            items: [
                { id: 'fichier.bus', label: 'Bus', link: '/fichier/bus' },
                { id: 'fichier.chauffeur', label: 'Chauffeurs', link: '/fichier/chauffeur' },
                { id: 'fichier.modem', label: 'Modems', link: '/fichier/modem' },
                { id: 'fichier.circuit', label: 'Circuits', link: '/fichier/circuit' },
                { id: 'fichier.pointcollecte', label: 'Points de collecte', link: '/fichier/pointcollecte' },
                { id: 'fichier.region', label: 'Régions', link: '/fichier/region' },
            ],
        },
        {
            label: 'Organisation',
            items: [
                { id: 'fichier.gouvernorat', label: 'Gouvernorats', link: '/fichier/gouvernorat' },
                { id: 'fichier.shift', label: 'Shifts', link: '/fichier/shift' },
                { id: 'fichier.equipe', label: 'Équipes', link: '/fichier/equipe' },
                { id: 'fichier.rattachement', label: 'Rattachements', link: '/fichier/rattachement' },
                { id: 'fichier.societe', label: 'Sociétés', link: '/fichier/societe' },
                { id: 'fichier.chantier', label: 'Chantiers', link: '/fichier/chantier' },
                { id: 'fichier.pointage', label: 'Pointages', link: '/fichier/pointage' },
            ],
        },
        {
            label: 'Personnel & accès',
            items: [
                { id: 'fichier.employe', label: 'Employés', link: '/fichier/employe' },
                { id: 'fichier.utilisateur', label: 'Utilisateurs', link: '/fichier/utilisateur' },
                { id: 'fichier.role-utilisateur', label: 'Rôles', link: '/fichier/role-utilisateur' },
            ],
        },
    ];

    groups: GestionGroup[] = [];

    ngOnInit(): void {
        this._userService.user$
            .pipe(takeUntil(this._destroy$))
            .subscribe((user) => {
                const navigations = user?.navigations;
                const isSuperAdmin = !navigations || navigations.length === 0;
                const allowedSet = new Set(navigations?.map((n) => n.navigationId) ?? []);

                this.groups = this.allGroups
                    .map((group) => ({
                        label: group.label,
                        items: group.items.filter((item) => isSuperAdmin || allowedSet.has(item.id)),
                    }))
                    .filter((group) => group.items.length > 0);

                // If user lands directly on /fichier without a specific module, redirect to the first accessible module
                const currentUrl = this._router.url.split('?')[0].replace(/\/+$/, '');
                if (currentUrl === '/fichier') {
                    const firstAllowed = this.groups[0]?.items[0]?.link;
                    if (firstAllowed) {
                        this._router.navigateByUrl(firstAllowed);
                    }
                }

                this._cdr.markForCheck();
            });
    }

    ngOnDestroy(): void {
        this._destroy$.next();
        this._destroy$.complete();
    }
}
