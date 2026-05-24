import {
    ChangeDetectionStrategy,
    Component,
    EventEmitter,
    Input,
    Output,
    ViewEncapsulation,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import {
    MapLocation,
    MapViewerComponent,
} from 'app/shared/components/map-viewer/map-viewer.component';

@Component({
    selector: 'app-bus-tracking-map',
    standalone: true,
    imports: [CommonModule, MatIconModule, MatSlideToggleModule, MapViewerComponent],
    templateUrl: './bus-tracking-map.component.html',
    styleUrl: './bus-tracking-map.component.scss',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class BusTrackingMapComponent {
    @Input() locations: MapLocation[] = [];
    @Input() showAll: boolean = false;
    @Output() readonly toggleShowAll = new EventEmitter<boolean>();
}

