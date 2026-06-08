import { ChangeDetectionStrategy, Component, ViewEncapsulation } from '@angular/core';
import { CommonModule, DecimalPipe } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { AnalyseDesignerComponent, AnalyseFieldDef } from '../designer/analyse-designer.component';
import { AnalyseApiService } from '../shared/analyse-api.service';
import { BusEtaPredictionResponse } from '../shared/analyse.model';

@Component({
    selector: 'app-analyse-bi-bus',
    standalone: true,
    imports: [
        CommonModule,
        ReactiveFormsModule,
        AnalyseDesignerComponent,
        MatCardModule,
        MatFormFieldModule,
        MatInputModule,
        MatButtonModule,
        MatIconModule,
        DecimalPipe,
    ],
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

    etaForm: FormGroup;
    etaResult: BusEtaPredictionResponse | null = null;
    isLoading = false;

    constructor(
        private fb: FormBuilder,
        private analyseApiService: AnalyseApiService
    ) {
        const now = new Date();
        this.etaForm = this.fb.group({
            DistanceFromStop: [500],
            log_distance: [6.2],
            distance_over_300m: [1],
            hour: [now.getHours()],
            hour_sin: [null],
            hour_cos: [null],
            is_rush_hour: [this.isRushHour(now.getHours()) ? 1 : 0],
            day_of_week: [now.getDay()],
            DirectionRef: [1],
            is_weekend: [now.getDay() === 0 || now.getDay() === 6 ? 1 : 0],
        });
    }

    private isRushHour(hour: number): boolean {
        return (hour >= 7 && hour <= 9) || (hour >= 17 && hour <= 19);
    }

    predictEta(): void {
        this.isLoading = true;
        this.analyseApiService.predictBusEta(this.etaForm.value).subscribe({
            next: (result) => {
                this.etaResult = result;
                this.isLoading = false;
            },
            error: () => {
                this.isLoading = false;
            },
        });
    }
}

