import { ChangeDetectionStrategy, Component, ViewEncapsulation } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-bus',
  standalone: true,
  imports: [
      RouterOutlet
  ],
  templateUrl: './bus.component.html',
  styleUrl: './bus.component.scss',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class BusComponent {

}
