import { ChangeDetectionStrategy, Component, ViewEncapsulation } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';

/**
 * Gestion shell — a persistent secondary sidebar (Design Canvas 1d) that wraps every
 * `/fichier/*` module. Clicking "Fichier" in the top nav lands here; the module list/detail
 * renders in the outlet while the children stay visible in the left sidebar (not a dropdown).
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
export class GestionShellComponent {
    readonly groups = [
        {
            label: 'Flotte & réseau',
            items: [
                { label: 'Bus', link: '/fichier/bus' },
                { label: 'Chauffeurs', link: '/fichier/chauffeur' },
                { label: 'Modems', link: '/fichier/modem' },
                { label: 'Circuits', link: '/fichier/circuit' },
                { label: 'Points de collecte', link: '/fichier/pointcollecte' },
                { label: 'Régions', link: '/fichier/region' },
            ],
        },
        {
            label: 'Organisation',
            items: [
                { label: 'Gouvernorats', link: '/fichier/gouvernorat' },
                { label: 'Shifts', link: '/fichier/shift' },
                { label: 'Équipes', link: '/fichier/equipe' },
                { label: 'Rattachements', link: '/fichier/rattachement' },
                { label: 'Sociétés', link: '/fichier/societe' },
                { label: 'Chantiers', link: '/fichier/chantier' },
                { label: 'Pointages', link: '/fichier/pointage' },
            ],
        },
        {
            label: 'Personnel & accès',
            items: [
                { label: 'Employés', link: '/fichier/employe' },
                { label: 'Utilisateurs', link: '/fichier/utilisateur' },
                { label: 'Rôles', link: '/fichier/role-utilisateur' },
            ],
        },
    ];
}
