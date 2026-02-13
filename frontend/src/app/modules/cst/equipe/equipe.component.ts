import { ChangeDetectionStrategy, Component, ViewEncapsulation } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-equipe',
  standalone: true,
  imports: [
      RouterOutlet
  ],
  templateUrl: './equipe.component.html',
  styleUrl: './equipe.component.scss',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class EquipeComponent {

}
