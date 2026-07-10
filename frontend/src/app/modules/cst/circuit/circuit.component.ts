import { ChangeDetectionStrategy, Component, ViewEncapsulation } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-circuit',
  standalone: true,
  imports: [
      RouterOutlet
  ],
  templateUrl: './circuit.component.html',
  styleUrl: './circuit.component.scss',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CircuitComponent {

}
