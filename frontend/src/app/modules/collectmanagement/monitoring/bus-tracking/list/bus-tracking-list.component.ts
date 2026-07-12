import {
    ChangeDetectionStrategy,
    Component,
    EventEmitter,
    Input,
    Output,
    ViewEncapsulation,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, UntypedFormControl } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { BusTrackingAdapterService, BusTrackingItem } from '../bus-tracking.adapter.service';

@Component({
    selector: 'app-bus-tracking-list',
    standalone: true,
    imports: [CommonModule, ReactiveFormsModule, MatIconModule, MatProgressBarModule],
    templateUrl: './bus-tracking-list.component.html',
    styleUrl: './bus-tracking-list.component.scss',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class BusTrackingListComponent {
    @Input() buses: BusTrackingItem[] = [];
    @Input() selectedBusId: string | null = null;
    @Input() isLoading: boolean = false;
    @Input() searchControl: UntypedFormControl;
    @Output() readonly selectBus = new EventEmitter<string>();

    activeFilter: 'all' | 'active' | 'inactive' = 'all';

    constructor(private readonly _adapter: BusTrackingAdapterService) {}

    get filteredBuses(): BusTrackingItem[] {
        if (this.activeFilter === 'active') {
            return this.buses.filter(b => b.isActive);
        } else if (this.activeFilter === 'inactive') {
            return this.buses.filter(b => !b.isActive);
        }
        return this.buses;
    }

    getCount(filter: 'active' | 'inactive'): number {
        if (filter === 'active') {
            return this.buses.filter(b => b.isActive).length;
        }
        return this.buses.filter(b => !b.isActive).length;
    }

    setFilter(filter: 'all' | 'active' | 'inactive'): void {
        this.activeFilter = filter;
    }

    getRatioPercent(bus: BusTrackingItem): number | null {
        const ratio = this._adapter.getOccupancyRatio(bus);
        return ratio === null ? null : Math.round(ratio * 100);
    }

    getOccupancyColorClass(bus: BusTrackingItem): string {
        const ratio = this._adapter.getOccupancyRatio(bus);
        if (ratio === null) return 'bg-gray-200 dark:bg-gray-700';
        if (ratio >= 0.9) return 'bg-red-500';
        if (ratio >= 0.7) return 'bg-orange-400';
        return 'bg-blue-400';
    }

    getCircuitDriverLabel(bus: BusTrackingItem): string {
        const circuit = bus.codeCircuit || 'Aucun circuit';
        const driver = bus.codeChauffeur || 'Aucun chauffeur';
        return `${circuit} · ${driver}`;
    }

    trackByBusId(index: number, item: BusTrackingItem): string {
        return item.busId ?? `${index}`;
    }
}

