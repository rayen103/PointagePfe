import { ChangeDetectionStrategy, Component, ViewEncapsulation } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AnalyseDesignerComponent, AnalyseFieldDef } from '../designer/analyse-designer.component';

@Component({
    selector: 'app-analyse-bi-employe',
    standalone: true,
    imports: [CommonModule, AnalyseDesignerComponent],
    templateUrl: './analyse-bi-employe.component.html',
    styleUrl: './analyse-bi-employe.component.scss',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AnalyseBiEmployeComponent {
    readonly fields: AnalyseFieldDef[] = [
        { key: 'matricule', label: 'Matricule', dataType: 'string', isNumeric: false },
        { key: 'nom', label: 'Nom', dataType: 'string', isNumeric: false },
        { key: 'prenom', label: 'Prénom', dataType: 'string', isNumeric: false },
        { key: 'typeEmploye', label: 'Type', dataType: 'string', isNumeric: false },
        { key: 'rfid', label: 'RFID', dataType: 'string', isNumeric: false },
        { key: 'codeCircuit', label: 'Circuit', dataType: 'string', isNumeric: false },
        { key: 'codePointCollecte', label: 'Point', dataType: 'string', isNumeric: false },
        { key: 'codeBus', label: 'Bus', dataType: 'string', isNumeric: false },
        { key: 'codeShift', label: 'Shift', dataType: 'string', isNumeric: false },
        { key: 'codeGouvernorat', label: 'Gouvernorat', dataType: 'string', isNumeric: false },
        { key: 'codeRegion', label: 'Région', dataType: 'string', isNumeric: false },
        { key: 'assignmentsCount', label: 'Affectations (période)', dataType: 'number', isNumeric: true },
        { key: 'totalHeures', label: 'Heures (période)', dataType: 'number', isNumeric: true },
        { key: 'totalCout', label: 'Coût (période)', dataType: 'number', isNumeric: true },
    ];
}

