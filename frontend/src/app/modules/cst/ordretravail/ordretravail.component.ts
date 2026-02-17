import { ChangeDetectionStrategy, Component, ViewEncapsulation } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-ordretravail',
  standalone: true,
  imports: [
      RouterOutlet
  ],
  templateUrl: './ordretravail.component.html',
  styleUrl: './ordretravail.component.scss',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class OrdretravailComponent {

}
