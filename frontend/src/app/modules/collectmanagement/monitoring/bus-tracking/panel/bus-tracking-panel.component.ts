import {
    ChangeDetectionStrategy,
    Component,
    EventEmitter,
    Input,
    Output,
    ViewEncapsulation,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { BusRuntimeEvent } from 'app/core/bus/bus.model';
import { BusTrackingAdapterService, BusTrackingItem } from '../bus-tracking.adapter.service';

@Component({
    selector: 'app-bus-tracking-panel',
    standalone: true,
    imports: [
        CommonModule,
        MatButtonModule,
        MatIconModule,
        MatProgressBarModule,
        MatProgressSpinnerModule,
    ],
    templateUrl: './bus-tracking-panel.component.html',
    styleUrl: './bus-tracking-panel.component.scss',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class BusTrackingPanelComponent {
    @Input() bus: BusTrackingItem | null = null;
    @Input() events: BusRuntimeEvent[] = [];
    @Input() eventsLoading: boolean = false;
    @Input() canEmptyBus: boolean = false;
    @Output() readonly emptyBus = new EventEmitter<void>();

    constructor(private readonly _adapter: BusTrackingAdapterService) {}

    getRatioPercent(): number | null {
        const ratio = this._adapter.getOccupancyRatio(this.bus);
        return ratio === null ? null : Math.round(ratio * 100);
    }
}

