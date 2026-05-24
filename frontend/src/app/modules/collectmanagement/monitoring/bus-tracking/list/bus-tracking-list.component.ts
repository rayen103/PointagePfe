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
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { BusTrackingAdapterService, BusTrackingItem } from '../bus-tracking.adapter.service';

@Component({
    selector: 'app-bus-tracking-list',
    standalone: true,
    imports: [CommonModule, MatIconModule, MatProgressBarModule],
    templateUrl: './bus-tracking-list.component.html',
    styleUrl: './bus-tracking-list.component.scss',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class BusTrackingListComponent {
    @Input() buses: BusTrackingItem[] = [];
    @Input() selectedBusId: string | null = null;
    @Input() isLoading: boolean = false;
    @Output() readonly selectBus = new EventEmitter<string>();

    constructor(private readonly _adapter: BusTrackingAdapterService) {}

    getRatioPercent(bus: BusTrackingItem): number | null {
        const ratio = this._adapter.getOccupancyRatio(bus);
        return ratio === null ? null : Math.round(ratio * 100);
    }

    trackByBusId(index: number, item: BusTrackingItem): string {
        return item.busId ?? `${index}`;
    }
}

