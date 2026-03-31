import { ChangeDetectionStrategy, Component, ViewEncapsulation } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
    selector: 'app-rattachement-employe',
    standalone: true,
    imports: [RouterOutlet],
    template: '<router-outlet></router-outlet>',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RattachementEmployeComponent {}
