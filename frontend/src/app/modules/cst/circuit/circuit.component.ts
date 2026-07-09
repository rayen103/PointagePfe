import { ChangeDetectionStrategy, Component, ViewEncapsulation } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { FuseNavigationAction } from '../../../../@fuse/components/navigation';
import { RoleNavigation } from '../../../core/role-utilisateur/role-utilisateur.model';

@Component({
  selector: 'app-circuit',
  standalone: true,
  imports: [
      RouterLink,
      RouterOutlet
  ],
  templateUrl: './circuit.component.html',
  styleUrl: './circuit.component.scss',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CircuitComponent {
    protected readonly FuseNavigationAction = FuseNavigationAction;
    roleNavigation: RoleNavigation;

    hasActionPermission(action: FuseNavigationAction): boolean {
        return !this.roleNavigation || this.roleNavigation?.actions?.includes(action);
    }
}
