
import { ChangeDetectionStrategy, Component, OnInit, ViewEncapsulation } from '@angular/core';
import { CommonModule, DecimalPipe } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatIconModule } from '@angular/material/icon';
import { AnalyseDesignerComponent, AnalyseFieldDef } from '../designer/analyse-designer.component';
import { AnalyseApiService } from '../shared/analyse-api.service';
import { AvailableBusEtaPrediction, BusEtaPredictionResponse } from '../shared/analyse.model';
import { BusService } from 'app/core/bus/bus.service';
import { Bus } from 'app/core/bus/bus.model';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

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
        MatSelectModule,
        DecimalPipe,
    ],
    templateUrl: './analyse-bi-bus.component.html',
    styleUrl: './analyse-bi-bus.component.scss',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AnalyseBiBusComponent implements OnInit {
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
    availableEtaResults: AvailableBusEtaPrediction[] = [];
    isLoading = false;
    isAvailableLoading = false;
    buses$: Observable<Bus[]>;

    constructor(
        private fb: FormBuilder,
        private analyseApiService: AnalyseApiService,
        private busService: BusService
    ) {
        this.etaForm = this.fb.group({
            selectedBus: [null],
            Latitude: [null],
            Longitude: [null],
            CodeCircuit: [null],
            ModelBus: [null],
            Capacite: [null],
            CurrentOccupancy: [null],
            LastPositionAt: [null],
        });

        this.buses$ = this.busService.GetBuses().pipe(map(paged => paged.buses));
    }

    ngOnInit(): void {
        this.predictAvailableBusesEta();
    }

    onBusSelect(bus: Bus | null): void {
        if (!bus) {
            this.etaForm.patchValue({
                Latitude: null,
                Longitude: null,
                CodeCircuit: null,
                ModelBus: null,
                Capacite: null,
                CurrentOccupancy: null,
                LastPositionAt: null,
            });
            return;
        }

        this.etaForm.patchValue({
            Latitude: bus.latitude,
            Longitude: bus.longitude,
            CodeCircuit: bus.codeCircuit,
            ModelBus: bus.modelBus,
            Capacite: bus.capacite,
            CurrentOccupancy: bus.currentOccupancy,
            LastPositionAt: bus.lastPositionAt,
        });
    }

    predictEta(): void {
        this.isLoading = true;
        const rawValues = this.etaForm.getRawValue();
        this.analyseApiService.predictBusEta({
            Latitude: rawValues.Latitude,
            Longitude: rawValues.Longitude,
            CodeCircuit: rawValues.CodeCircuit,
            ModelBus: rawValues.ModelBus,
            Capacite: rawValues.Capacite,
            CurrentOccupancy: rawValues.CurrentOccupancy,
            LastPositionAt: rawValues.LastPositionAt,
        }).subscribe({
            next: (result) => {
                this.etaResult = result;
                this.isLoading = false;
            },
            error: () => {
                this.isLoading = false;
            },
        });
    }

    predictAvailableBusesEta(): void {
        this.isAvailableLoading = true;
        this.analyseApiService.predictAvailableBusEta().subscribe({
            next: (result) => {
                this.availableEtaResults = result?.predictions ?? [];
                this.isAvailableLoading = false;
            },
            error: () => {
                this.availableEtaResults = [];
                this.isAvailableLoading = false;
            },
        });
    }
}

