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
    CircuitMapOverview,
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
    @Input() allCircuits: CircuitMapOverview[] = [];
    @Input() selectedCircuitId: string | null = null;
    @Output() readonly toggleShowAll = new EventEmitter<boolean>();
    @Output() readonly selectCircuit = new EventEmitter<string>();

    showAllCircuits: boolean = true;
    showAllPoints: boolean = true;

    get showCircuitOverlay(): boolean {
        return this.circuitPoints.length > 0 || !!this.optimizedRoute || !!this.selectedBusPosition || this.allCircuits.length > 0;
    }

    toggleCircuits(): void {
        this.showAllCircuits = !this.showAllCircuits;
    }

    togglePoints(): void {
        this.showAllPoints = !this.showAllPoints;
    }
}
