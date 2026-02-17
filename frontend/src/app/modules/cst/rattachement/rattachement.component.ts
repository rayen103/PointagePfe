import { ChangeDetectionStrategy, Component, ViewEncapsulation } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-rattachement',
  standalone: true,
  imports: [
      RouterOutlet
  ],
  templateUrl: './rattachement.component.html',
  styleUrl: './rattachement.component.scss',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RattachementComponent {

}
