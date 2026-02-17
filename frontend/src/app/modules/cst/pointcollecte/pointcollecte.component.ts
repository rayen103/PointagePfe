import { ChangeDetectionStrategy, Component, ViewEncapsulation } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-pointcollecte',
  standalone: true,
  imports: [
      RouterOutlet
  ],
  templateUrl: './pointcollecte.component.html',
  styleUrl: './pointcollecte.component.scss',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PointCollecteComponent {

}
