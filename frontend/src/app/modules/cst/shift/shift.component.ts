import { ChangeDetectionStrategy, Component, ViewEncapsulation } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-shift',
  standalone: true,
  imports: [
      RouterOutlet
  ],
  templateUrl: './shift.component.html',
  styleUrl: './shift.component.scss',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ShiftComponent {

}
