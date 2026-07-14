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
import { BusRuntimeEvent, BusPointage } from 'app/core/bus/bus.model';
import { BusTrackingAdapterService, BusTrackingItem } from '../bus-tracking.adapter.service';

export interface CircuitPanelInfo {
    circuitId: string;
    circuitCode: string;
    circuitName: string;
    completedPoints: number;
    totalCollectionPoints: number;
    progressPercent: number | null;
    nextDestinationName: string | null;
    remainingDistanceKm: number | null;
    remainingEtaMinutes: number | null;
}

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
    @Input() pointages: BusPointage[] = [];
    @Input() pointagesLoading: boolean = false;
    @Input() canEmptyBus: boolean = false;
    @Input() circuitInfo: CircuitPanelInfo | null = null;
    @Output() readonly emptyBus = new EventEmitter<void>();

    constructor(private readonly _adapter: BusTrackingAdapterService) {}

    getRatioPercent(): number | null {
        const ratio = this._adapter.getOccupancyRatio(this.bus);
        return ratio === null ? null : Math.round(ratio * 100);
    }

    getDashArray(): string {
        const ratio = this._adapter.getOccupancyRatio(this.bus);
        if (ratio === null) {
            return '0 251.3';
        }
        const val = ratio * 251.3;
        return `${val} 251.3`;
    }

    getEventDotColor(ev: BusRuntimeEvent): string {
        const type = (ev.eventType || '').toLowerCase();
        if (type.includes('badge') || type.includes('rfid')) return '#42ABE0';
        if (type.includes('départ') || type.includes('start') || type.includes('fin')) return '#0E8A5F';
        if (type.includes('ralentissement') || type.includes('incident') || type.includes('alerte')) return '#F2B33D';
        return '#121242';
    }

    getEventRingColor(ev: BusRuntimeEvent): string {
        const type = (ev.eventType || '').toLowerCase();
        if (type.includes('badge') || type.includes('rfid')) return '#EAF6FC';
        if (type.includes('départ') || type.includes('start') || type.includes('fin')) return '#E3F5EE';
        if (type.includes('ralentissement') || type.includes('incident') || type.includes('alerte')) return '#FFF6E3';
        return '#EDF1F6';
    }

    getPointageLabel(p: BusPointage): string {
        if (p.nomEmploye && p.nomEmploye.trim()) return p.nomEmploye.trim();
        if (p.matricule && p.matricule.trim()) return p.matricule.trim();
        return `Badge ${p.tag}`;
    }

    getStatusLabel(): string {
        return this.bus?.isActive ? 'En route' : 'Arrêt';
    }

    getStatusColor(): string {
        return this.bus?.isActive ? 'text-green-600' : 'text-gray-500';
    }
}
