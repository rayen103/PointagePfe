import { ChangeDetectionStrategy, Component, ViewEncapsulation } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AnalyseDesignerComponent, AnalyseFieldDef } from '../designer/analyse-designer.component';

@Component({
    selector: 'app-analyse-trace',
    standalone: true,
    imports: [CommonModule, AnalyseDesignerComponent],
    templateUrl: './analyse-trace.component.html',
    styleUrl: './analyse-trace.component.scss',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AnalyseTraceComponent {
    readonly fields: AnalyseFieldDef[] = [
        { key: 'numeroRattachement', label: 'N° Rattachement', dataType: 'string', isNumeric: false },
        { key: 'dateRattachement', label: 'Date', dataType: 'date', isNumeric: false },
        { key: 'numeroChantier', label: 'Chantier', dataType: 'string', isNumeric: false },
        { key: 'codeClient', label: 'Client', dataType: 'string', isNumeric: false },
        { key: 'isInternal', label: 'Interne', dataType: 'boolean', isNumeric: false },
        { key: 'cout', label: 'Coût', dataType: 'number', isNumeric: true },
        { key: 'type', label: 'Type', dataType: 'string', isNumeric: false },
        { key: 'nature', label: 'Nature', dataType: 'string', isNumeric: false },
        { key: 'responsable', label: 'Responsable', dataType: 'string', isNumeric: false },
        { key: 'status', label: 'Statut', dataType: 'string', isNumeric: false },
        { key: 'dateCloture', label: 'Clôture', dataType: 'date', isNumeric: false },
        { key: 'emplacement', label: 'Emplacement', dataType: 'string', isNumeric: false },
        { key: 'reference', label: 'Référence', dataType: 'string', isNumeric: false },
        { key: 'isActive', label: 'Actif', dataType: 'boolean', isNumeric: false },
    ];
}

