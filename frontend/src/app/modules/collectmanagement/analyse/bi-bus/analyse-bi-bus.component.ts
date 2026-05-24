import { ChangeDetectionStrategy, Component, ViewEncapsulation } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AnalyseDesignerComponent, AnalyseFieldDef } from '../designer/analyse-designer.component';

@Component({
    selector: 'app-analyse-bi-bus',
    standalone: true,
    imports: [CommonModule, AnalyseDesignerComponent],
    templateUrl: './analyse-bi-bus.component.html',
    styleUrl: './analyse-bi-bus.component.scss',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AnalyseBiBusComponent {
    readonly fields: AnalyseFieldDef[] = [
        { key: 'numeroIMM', label: 'Bus', dataType: 'string', isNumeric: false },
        { key: 'modelBus', label: 'Modèle', dataType: 'string', isNumeric: false },
        { key: 'imei', label: 'IMEI', dataType: 'string', isNumeric: false },
        { key: 'capacite', label: 'Capacité', dataType: 'number', isNumeric: true },
        { key: 'currentOccupancy', label: 'Occupation', dataType: 'number', isNumeric: true },
        { key: 'occupancyRatio', label: 'Taux', dataType: 'number', isNumeric: true },
        { key: 'codeCircuit', label: 'Circuit', dataType: 'string', isNumeric: false },
        { key: 'isActive', label: 'Actif', dataType: 'boolean', isNumeric: false },
        { key: 'latitude', label: 'Latitude', dataType: 'number', isNumeric: true },
        { key: 'longitude', label: 'Longitude', dataType: 'number', isNumeric: true },
        { key: 'lastPositionAt', label: 'Dernière position', dataType: 'datetime', isNumeric: false },
        { key: 'eventsCount', label: 'Events (période)', dataType: 'number', isNumeric: true },
        { key: 'lastEventAtUtc', label: 'Dernier event', dataType: 'datetime', isNumeric: false },
        { key: 'maxOccupancyInRange', label: 'Max occupation (période)', dataType: 'number', isNumeric: true },
    ];
}

