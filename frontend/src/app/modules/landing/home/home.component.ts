import { Component, ViewEncapsulation } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
    selector: 'landing-home',
    templateUrl: './home.component.html',
    encapsulation: ViewEncapsulation.None,
    standalone: true,
    imports: [RouterLink],
})
export class LandingHomeComponent {
    /** Un token présent = session probable : le CTA devient « Accéder au tableau de bord ». */
    get isAuthenticated(): boolean {
        return !!localStorage.getItem('accessToken');
    }

    readonly currentYear = new Date().getFullYear();
}
