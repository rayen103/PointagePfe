import {
    ChangeDetectionStrategy,
    Component,
    Inject,
    OnDestroy,
    OnInit,
    ViewEncapsulation,
} from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import {
    ReactiveFormsModule,
    UntypedFormBuilder,
    UntypedFormGroup,
    Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatOptionModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { Subject, debounceTime, distinctUntilChanged, of, takeUntil, switchMap } from 'rxjs';
import { Employe } from '../../../../core/employes/employe.model';
import { Circuit } from '../../../../core/circuit/circuit.model';
import { Bus } from '../../../../core/bus/bus.model';
import { Shift } from '../../../../core/shift/shift.model';
import { PointCollecte } from '../../../../core/point-collecte/point-collecte.model';
import { Gouvernorat } from '../../../../core/gouvernorat/gouvernorat.model';
import { Region } from '../../../../core/region/region.model';
import { PagedSociete, Societe } from '../../../../core/Societe/societe.model';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { TranslocoDirective } from '@ngneat/transloco';
import { CommonModule } from '@angular/common';
import { SocieteService } from '../../../../core/Societe/societe.service';

@Component({
    selector: 'app-employe-dialog',
    standalone: true,
    imports: [
        CommonModule,
        ReactiveFormsModule,
        MatButtonModule,
        MatFormFieldModule,
        MatIconModule,
        MatInputModule,
        MatOptionModule,
        MatSelectModule,
        MatSlideToggleModule,
        MatAutocompleteModule,
        TranslocoDirective,
    ],
    templateUrl: './employe-dialog.component.html',
    styleUrls: ['./employe-dialog.component.scss'],
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class EmployeDialogComponent implements OnInit, OnDestroy {
    form: UntypedFormGroup;
    societe: Societe[] = [];
    filteredSocietes$: any;
    circuits: Circuit[] = [];
    buses: Bus[] = [];
    shifts: Shift[] = [];
    pointsCollecte: PointCollecte[] = [];
    gouvernorats: Gouvernorat[] = [];
    regions: Region[] = [];
    isNew: boolean = false;
    saveClicked = false;

    private _unsubscribeAll: Subject<any> = new Subject<any>();

    constructor(
        private _dialogRef: MatDialogRef<EmployeDialogComponent>,
        @Inject(MAT_DIALOG_DATA) public data: {
            employe: Employe | null;
            societes: Societe[];
            circuits: Circuit[];
            buses: Bus[];
            shifts: Shift[];
            pointsCollecte: PointCollecte[];
            gouvernorats: Gouvernorat[];
            regions: Region[];
        },
        private _formBuilder: UntypedFormBuilder,
        private _societeService: SocieteService
    ) { }

    ngOnInit(): void {
        this.societe = this.data.societes;
        this.circuits = this.data.circuits;
        this.buses = this.data.buses;
        this.shifts = this.data.shifts;
        this.pointsCollecte = this.data.pointsCollecte;
        this.gouvernorats = this.data.gouvernorats;
        this.regions = this.data.regions;
        this.isNew = !this.data.employe || this.data.employe.employeId === 'new';

        this.form = this._formBuilder.group({
            employeId: [this.data.employe?.employeId || 'new'],
            matricule: [this.data.employe?.matricule, [Validators.required, Validators.maxLength(50)]],
            rfid: [this.data.employe?.rfid, [Validators.maxLength(50)]],
            nom: [this.data.employe?.nom, [Validators.required, Validators.maxLength(100)]],
            prenom: [this.data.employe?.prenom, [Validators.required, Validators.maxLength(100)]],
            codeCircuit: [this.data.employe?.codeCircuit, [Validators.maxLength(50)]],
            codePointCollecte: [this.data.employe?.codePointCollecte, [Validators.maxLength(50)]],
            codeBus: [this.data.employe?.codeBus, [Validators.maxLength(50)]],
            codeShift: [this.data.employe?.codeShift, [Validators.maxLength(50)]],
            adresse: [this.data.employe?.adresse, [Validators.maxLength(255)]],
            codeGouvernorat: [this.data.employe?.codeGouvernorat, [Validators.maxLength(50)]],
            codeRegion: [this.data.employe?.codeRegion, [Validators.maxLength(50)]],
            latitude: [this.data.employe?.latitude, [Validators.min(-90), Validators.max(90)]],
            longitude: [this.data.employe?.longitude, [Validators.min(-180), Validators.max(180)]],
            societeId: [this.data.employe?.societeId, [Validators.required]],
            isActive: [this.data.employe?.isActive ?? true, [Validators.required]]
        });

        this.filteredSocietes$ = this.form.get('societeId')!.valueChanges.pipe(
            debounceTime(300),
            distinctUntilChanged(),
            takeUntil(this._unsubscribeAll),
            switchMap((value) =>
                this._societeService
                    .GetSociete(1, 20, '', 'asc', value || '')
            ),
            switchMap((res: PagedSociete) => of(res.societes || []))
        );
    }

    displaySociete = (societeId: string) => {
        const found = this.societe.find((s) => s.societeId === societeId);
        return found ? found.nom : '';
    };

    getFilteredCircuits(): Circuit[] {
        const societeId = this.form?.get('societeId')?.value;
        if (!societeId) {
            return this.circuits;
        }
        return this.circuits.filter((circuit) => circuit.societeId === societeId);
    }

    getFilteredBuses(): Bus[] {
        const societeId = this.form?.get('societeId')?.value;
        if (!societeId) {
            return this.buses;
        }
        return this.buses.filter((bus) => bus.societeId === societeId);
    }

    getFilteredShifts(): Shift[] {
        const societeId = this.form?.get('societeId')?.value;
        let filtered = this.shifts;

        if (societeId) {
            filtered = filtered.filter((shift) => shift.societeId === societeId);
        }

        const uniqueShifts: Shift[] = [];
        const codes = new Set<string>();

        filtered.forEach(shift => {
            if (!codes.has(shift.codeShift)) {
                codes.add(shift.codeShift);
                uniqueShifts.push(shift);
            }
        });

        return uniqueShifts;
    }

    getFilteredPointsCollecte(): PointCollecte[] {
        const societeId = this.form?.get('societeId')?.value;
        let filtered = this.pointsCollecte;

        if (societeId) {
            filtered = filtered.filter((point) => point.societeId === societeId);
        }

        return filtered;
    }

    hasExistingCircuitOption(codeCircuit: string | null | undefined): boolean {
        if (!codeCircuit) {
            return false;
        }
        return this.getFilteredCircuits().some((circuit) => circuit.codeCircuit === codeCircuit);
    }

    hasExistingBusOption(codeBus: string | null | undefined): boolean {
        if (!codeBus) {
            return false;
        }
        return this.getFilteredBuses().some((bus) => bus.numeroIMM === codeBus);
    }

    hasExistingShiftOption(codeShift: string | null | undefined): boolean {
        if (!codeShift) {
            return false;
        }
        return this.getFilteredShifts().some((shift) => shift.codeShift === codeShift);
    }

    hasExistingPointOption(codePoint: string | null | undefined): boolean {
        if (!codePoint) {
            return false;
        }
        return this.getFilteredPointsCollecte().some((point) => point.codePointCollecte === codePoint);
    }

    hasExistingGouvOption(codeGouv: string | null | undefined): boolean {
        if (!codeGouv) {
            return false;
        }
        return this.gouvernorats.some((gouv) => gouv.codeGouvernorat === codeGouv);
    }

    hasExistingRegionOption(codeRegion: string | null | undefined): boolean {
        if (!codeRegion) {
            return false;
        }
        return this.regions.some((reg) => reg.codeRegion === codeRegion);
    }

    save(): void {
        this.saveClicked = true;

        if (this.form.invalid) {
            return;
        }

        this._dialogRef.close(this.form.getRawValue());
    }

    close(): void {
        this._dialogRef.close();
    }

    ngOnDestroy(): void {
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }
}
