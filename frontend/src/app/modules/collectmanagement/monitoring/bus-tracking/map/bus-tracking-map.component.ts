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
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import {
    CircuitPointLocation,
    MapLocation,
    MapViewerComponent,
} from 'app/shared/components/map-viewer/map-viewer.component';

@Component({
    selector: 'app-bus-tracking-map',
    standalone: true,
    imports: [CommonModule, MatIconModule, MatSlideToggleModule, MatProgressSpinnerModule, MapViewerComponent],
    templateUrl: './bus-tracking-map.component.html',
    styleUrl: './bus-tracking-map.component.scss',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class BusTrackingMapComponent {
    @Input() locations: MapLocation[] = [];
    @Input() showAll: boolean = false;
    @Input() circuitPoints: CircuitPointLocation[] = [];
    @Input() optimizedRoute: { geometry: [number, number][]; distanceKm?: number; durationMinutes?: number } | null = null;
    @Input() selectedBusPosition: { latitude: number; longitude: number; heading?: number } | null = null;
    @Input() circuitArea: [number, number][] | null = null;
    @Input() routeLoading: boolean = false;
    @Output() readonly toggleShowAll = new EventEmitter<boolean>();

    get showCircuitOverlay(): boolean {
        return this.circuitPoints.length > 0 || !!this.optimizedRoute || !!this.selectedBusPosition;
    }
}
