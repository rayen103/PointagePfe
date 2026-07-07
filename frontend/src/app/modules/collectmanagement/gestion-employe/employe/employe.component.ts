import {
    ChangeDetectionStrategy,
    ChangeDetectorRef,
    Component, OnDestroy,
    OnInit,
    ViewChild,
    ViewEncapsulation,
} from '@angular/core';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { debounceTime, distinctUntilChanged, finalize, map, Observable, Subject, switchMap, takeUntil } from 'rxjs';
import {
    ReactiveFormsModule,
    UntypedFormBuilder,
    UntypedFormControl,
    UntypedFormGroup,
    Validators,
} from '@angular/forms';
import { FuseConfirmationService } from '../../../../../@fuse/services/confirmation';
import { Employe } from '../../../../core/employes/employe.model';
import { EmployeService } from '../../../../core/employes/employe.service';
import { AsyncPipe, NgClass, NgForOf, NgTemplateOutlet } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatOptionModule } from '@angular/material/core';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatSelectModule } from '@angular/material/select';
import { fuseAnimations } from '../../../../../@fuse/animations';
import { RoleNavigation } from '../../../../core/role-utilisateur/role-utilisateur.model';
import { ActivatedRoute } from '@angular/router';
import { FuseNavigationAction } from '../../../../../@fuse/components/navigation';
import { MatAutocomplete, MatAutocompleteTrigger } from '@angular/material/autocomplete';
import { PagedSociete, Societe } from '../../../../core/Societe/societe.model';
import { SocieteService } from '../../../../core/Societe/societe.service';
import { TranslocoDirective } from '@ngneat/transloco';
import { Circuit } from '../../../../core/circuit/circuit.model';
import { CircuitService } from '../../../../core/circuit/circuit.service';
import { Bus } from '../../../../core/bus/bus.model';
import { BusService } from '../../../../core/bus/bus.service';
import { ShiftService } from '../../../../core/shift/shift.service';
import { Shift } from '../../../../core/shift/shift.model';
import { PointCollecteService } from '../../../../core/point-collecte/point-collecte.service';
import { PointCollecte } from '../../../../core/point-collecte/point-collecte.model';
import { GouvernoratService } from '../../../../core/gouvernorat/gouvernorat.service';
import { RegionService } from '../../../../core/region/region.service';
import { Gouvernorat } from '../../../../core/gouvernorat/gouvernorat.model';
import { Region } from '../../../../core/region/region.model';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { EmployeDialogComponent } from './employe-dialog.component';

@Component({
    selector: 'app-employe',
    standalone: true,
    imports: [
        AsyncPipe,
        MatButtonModule,
        MatFormFieldModule,
        MatIconModule,
        MatInputModule,
        MatOptionModule,
        MatProgressBarModule,
        MatSelectModule,
        NgTemplateOutlet,
        ReactiveFormsModule,
        NgClass,
        MatPaginatorModule,
        MatAutocomplete,
        MatAutocompleteTrigger,
        NgForOf,
        TranslocoDirective,
        MatDialogModule,
    ],
    templateUrl: './employe.component.html',
    styleUrl: './employe.component.scss',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    animations: fuseAnimations
})
export class EmployeComponent implements OnInit, OnDestroy {

    @ViewChild(MatPaginator) private _paginator: MatPaginator;

    employe$: Observable<Employe[]>;
    flashMessage: 'success' | 'error' | null = null;
    isLoading: boolean = false;
    employeslength: number;
    searchInputControl: UntypedFormControl = new UntypedFormControl();
    selectedEmploye: Employe | null = null;
    selectedEmployeForm: UntypedFormGroup;
    private _unsubscribeAll: Subject<any> = new Subject<any>();
    saveClicked = false
    roleNavigation: RoleNavigation;
    societe: Societe[] = [];
    circuits: Circuit[] = [];
    buses: Bus[] = [];
    shifts: Shift[] = [];
    pointsCollecte: PointCollecte[] = [];
    gouvernorats: Gouvernorat[] = [];
    regions: Region[] = [];
    filteredSocietes$: Observable<Societe[]>;
    isViewMode: boolean = false; // To distinguish between view and edit mode
    sortActive: string = 'matricule';
    sortDirection: 'asc' | 'desc' = 'asc';


    constructor(
        private _employeService: EmployeService,
        private _activatedRoute: ActivatedRoute,
        private _changeDetectorRef: ChangeDetectorRef,
        private _fuseConfirmationService: FuseConfirmationService,
        private _formBuilder: UntypedFormBuilder,
        private _societeService: SocieteService,
        private _circuitService: CircuitService,
        private _busService: BusService,
        private _shiftService: ShiftService,
        private _pointCollecteService: PointCollecteService,
        private _gouvernoratService: GouvernoratService,
        private _regionService: RegionService,
        private _dialog: MatDialog,
    ) {
    }

    SortChange() {

        this.closeDetails();
        this.isLoading = true;
        this.getEmployes()
            .pipe(
                map(() => {
                    this.isLoading = false;

                    // Mark for check
                    this._changeDetectorRef.markForCheck();
                })
            )
            .subscribe();
    }

    getEmployes() {
        return this._employeService.GetEmploye(
            (this._paginator?.pageIndex | 0) + 1,
            this._paginator?.pageSize,
            this.sortActive,
            this.sortDirection,
            this.searchInputControl.value);
    }

    setSort(active: string, direction: 'asc' | 'desc'): void {
        this.sortActive = active;
        this.sortDirection = direction;
        this.SortChange();
    }

    hasActionPermission(action: FuseNavigationAction): boolean {
        return !this.roleNavigation || this.roleNavigation?.actions?.includes(action);
    }

    displaySociete = (societeId: string) => {
        const found = this.societe.find((s) => s.societeId === societeId);
        return found ? found.nom : '';
    };

    ngOnInit(): void {

        this.selectedEmployeForm = this._formBuilder.group({
            employeId: [''],
            matricule: ['', [Validators.required, Validators.maxLength(50)]],
            rfid: ['', [Validators.maxLength(50)]],
            nom: ['', [Validators.required, Validators.maxLength(100)]],
            prenom: ['', [Validators.required, Validators.maxLength(100)]],
            typeEmploye: ['EmployeSimple', [Validators.required]],
            codeCircuit: ['', [Validators.required, Validators.maxLength(50)]],
            codePointCollecte: ['', [Validators.required, Validators.maxLength(50)]],
            codeBus: ['', [Validators.maxLength(50)]],
            codeShift: ['', [Validators.maxLength(50)]],
            adresse: ['', [Validators.maxLength(255)]],
            codeGouvernorat: ['', [Validators.maxLength(50)]],
            codeRegion: ['', [Validators.maxLength(50)]],
            latitude: [null, [Validators.min(-90), Validators.max(90)]],
            longitude: [null, [Validators.min(-180), Validators.max(180)]],
            societeId: ['', [Validators.required]]
        });

        this.filteredSocietes$ = this.selectedEmployeForm.get('societeId')!.valueChanges.pipe(
            debounceTime(300),
            distinctUntilChanged(),
            switchMap((value) =>
                this._societeService
                    .GetSociete(1, 20, '', 'asc', value || '')
                    .pipe(map((res) => res.societes || []))
            )
        );

        this._activatedRoute.data
            .subscribe(async (data) => {

                if (!data?.navigation) {
                    return;
                }

                this.roleNavigation = data.navigation;
            });

        this.employe$ = this._employeService.employes$;

        this._employeService.employeLength$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((length) => {
                this.employeslength = length;

                // Mark for check
                this._changeDetectorRef.markForCheck();
            });

        this.searchInputControl.valueChanges
            .pipe(
                switchMap((query) => {
                    this.closeDetails();
                    this.isLoading = true;
                    return this.getEmployes();
                }),
                map(() => {
                    this.isLoading = false;
                })
            ).subscribe();

        this._societeService.GetSociete().subscribe((pagedSociete: PagedSociete) => {
            // Extract societes from the pagedSociete object
            this.societe = pagedSociete.societes || [];
            this._changeDetectorRef.markForCheck();
        });

        this._circuitService.GetCircuit()
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((pagedCircuit) => {
                this.circuits = pagedCircuit?.circuits ?? [];
                this._changeDetectorRef.markForCheck();
            });

        this._busService.GetBuses()
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((pagedBus) => {
                this.buses = pagedBus?.buses ?? [];
                this._changeDetectorRef.markForCheck();
            });

        this._shiftService.GetShifts()
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((pagedShift) => {
                this.shifts = pagedShift?.shifts ?? [];
                this._changeDetectorRef.markForCheck();
            });

        this._pointCollecteService.GetPointsCollecte()
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((pagedPoint) => {
                this.pointsCollecte = pagedPoint?.pointsCollecte ?? [];
                this._changeDetectorRef.markForCheck();
            });

        this._gouvernoratService.GetGouvernorats()
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((pagedGouv) => {
                this.gouvernorats = pagedGouv?.gouvernorats ?? [];
                this._changeDetectorRef.markForCheck();
            });

        this._regionService.GetRegions()
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((pagedRegion) => {
                this.regions = pagedRegion?.regions ?? [];
                this._changeDetectorRef.markForCheck();
            });


    }

    CreateEmploye(){

        if (!this.hasActionPermission(FuseNavigationAction.Add)){
            return;
        }

        const dialogRef = this._dialog.open(EmployeDialogComponent, {
            width: '800px',
            maxHeight: '80vh',
            data: {
                employe: null,
                societes: this.societe,
                circuits: this.circuits,
                buses: this.buses,
                shifts: this.shifts,
                pointsCollecte: this.pointsCollecte,
                gouvernorats: this.gouvernorats,
                regions: this.regions
            }
        });

        dialogRef.afterClosed().subscribe(result => {
            if (result) {
                this._employeService.AddEmploye(result)
                    .subscribe(() => {
                        this.SortChange();
                    });
            }
        });
    }

    /**
     * Toggle Employe details
     *
     * @param employeId
     */
    toggleDetails(employeId: string): void
    {
        // If the employe is already selected...
        if ( this.selectedEmploye && this.selectedEmploye.employeId === employeId )
        {
            // Close the details
            this.closeDetails();
            return;
        }

        // Get the employe by id
        this._employeService.GetEmployeById(employeId)
            .subscribe((employe) => {
                const dialogRef = this._dialog.open(EmployeDialogComponent, {
                width: '800px',
                maxHeight: '80vh',
                data: {
                    employe: employe,
                    societes: this.societe,
                    circuits: this.circuits,
                    buses: this.buses,
                    shifts: this.shifts,
                    pointsCollecte: this.pointsCollecte,
                    gouvernorats: this.gouvernorats,
                    regions: this.regions
                }
            });

                dialogRef.afterClosed().subscribe(result => {
                    if (result) {
                        this._employeService.UpdateEmploye(result)
                            .subscribe(() => {
                                this.SortChange();
                            });
                    }
                });
            });
    }

    /**
     * Edit Employe - opens dialog
     *
     * @param employeId
     */
    editEmploye(employeId: string): void
    {
        this.toggleDetails(employeId);
    }

    /**
     * Close the details
     */
    closeDetails(): void {
        this.selectedEmploye = null;
        this.isViewMode = false;
        // Mark for check
        this._changeDetectorRef.markForCheck();
    }

    /**
     * Handle view button click - prevents event propagation
     */
    onViewClick(event: Event, employeId: string): void {
        event.stopPropagation();
        this.toggleDetails(employeId);
    }

    /**
     * Handle edit button click - prevents event propagation
     */
    onEditClick(event: Event, employeId: string): void {
        event.stopPropagation();
        this.editEmploye(employeId);
    }

    /**
     * Handle delete button click - prevents event propagation
     */
    onDeleteClick(event: Event, employe: Employe): void {
        event.stopPropagation();
        this.deleteSelectedEmploye(employe);
    }

    /**
     * Update the selected employe using the form data
     */
    SaveSelectedEmploye(): void {
        if (!this.hasActionPermission(FuseNavigationAction.Edit) && !this.hasActionPermission(FuseNavigationAction.Add)) {
            return;
        }

        this.saveClicked = true;

        if (this.selectedEmployeForm.invalid) {
            this.selectedEmployeForm.markAllAsTouched();
            this._changeDetectorRef.markForCheck();
            setTimeout(() => {
                this.saveClicked = false;
                this._changeDetectorRef.markForCheck();
            }, 500);
            return;
        }

        if (!this.validateBusCircuitAssignment()) {
            this.saveClicked = false;
            this._changeDetectorRef.markForCheck();
            return;
        }

        // Get the employe object
        const employe = this.selectedEmployeForm.getRawValue();

        if (employe.employeId === "new" && this.hasActionPermission(FuseNavigationAction.Add)) {
            this._employeService.AddEmploye(employe)
                .pipe(
                    finalize(() => {
                        this.saveClicked = false;
                        this.closeDetails();
                        this._changeDetectorRef.markForCheck();
                    })
                )
                .subscribe(() => {
                    this.SortChange();
                });
        }

        if (employe.employeId !== "new" && this.hasActionPermission(FuseNavigationAction.Edit)) {
            // Update the employe on the server
            this._employeService.UpdateEmploye(employe)
                .pipe(
                    finalize(() => {
                        this.saveClicked = false;
                        this.closeDetails();
                        this._changeDetectorRef.markForCheck();
                    })
                )
                .subscribe(() => {
                    this.SortChange();
                });
        }
    }

    /**
     * Delete the selected employe using the form data
     */
    deleteSelectedEmploye(employe: Employe): void {
        if (!this.hasActionPermission(FuseNavigationAction.Delete)) {
            return;
        }

        // Open the confirmation dialog
        const confirmation = this._fuseConfirmationService.open({
            icon: {
                show: false,
            },
            title: 'Supprimer cet employé',
            message: 'Êtes-vous sûr de vouloir supprimer cet employé? Cette action ne peut pas être annulée!',
            actions: {
                confirm: {
                    label: 'Supprimer'
                },
                cancel: {
                    label: 'Annuler'
                }
            }
        });

        // Subscribe to the confirmation dialog closed action
        confirmation.afterClosed().subscribe((result) => {

            // If the confirm button pressed...
            if (result === 'confirmed') {

                // Delete the Employe on the server
                this._employeService.DeleteEmploye({ employeId: employe.employeId })
                    .subscribe(() => {

                        // Close the details
                        this.closeDetails();

                        // Mark for check
                        this._changeDetectorRef.markForCheck();
                    });
            }
        });
    }

    /**
     * Track by function for ngFor loops
     *
     * @param index
     * @param item
     */
    trackByFn(index: number, item: any): any {
        return item.employeId || index;
    }

    getFilteredCircuits(): Circuit[] {
        const societeId = this.selectedEmployeForm?.get('societeId')?.value;
        if (!societeId) {
            return this.circuits;
        }

        return this.circuits.filter((circuit) => circuit.societeId === societeId);
    }

    getFilteredBuses(): Bus[] {
        const societeId = this.selectedEmployeForm?.get('societeId')?.value;
        if (!societeId) {
            return this.buses;
        }

        return this.buses.filter((bus) => bus.societeId === societeId);
    }

    getFilteredShifts(): Shift[] {
        const societeId = this.selectedEmployeForm?.get('societeId')?.value;
        let filtered = this.shifts;
        
        if (societeId) {
            filtered = filtered.filter((shift) => shift.societeId === societeId);
        }

        // Return unique shifts by codeShift for the dropdown
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
        const societeId = this.selectedEmployeForm?.get('societeId')?.value;
        const codeCircuit = this.selectedEmployeForm?.get('codeCircuit')?.value;
        
        let filtered = this.pointsCollecte;
        
        if (societeId) {
            filtered = filtered.filter((point) => point.societeId === societeId);
        }

        // Optional: Filter points by circuit if needed, but the requirement is "one circuit + one point"
        // so we show all valid points for the society.

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

    private validateBusCircuitAssignment(): boolean {
        const selectedCodeBus = (this.selectedEmployeForm.get('codeBus')?.value ?? '').trim();
        const selectedCodeCircuit = (this.selectedEmployeForm.get('codeCircuit')?.value ?? '').trim();

        if (!selectedCodeBus || !selectedCodeCircuit) {
            return true;
        }

        const selectedBus = this.buses.find((bus) => bus.numeroIMM === selectedCodeBus);
        if (!selectedBus) {
            return true;
        }

        const busCircuit = (selectedBus.codeCircuit ?? '').trim();
        if (busCircuit !== selectedCodeCircuit) {
            this._fuseConfirmationService.open({
                title: 'Bus/Circuit mismatch',
                message: busCircuit
                    ? `The selected bus (${selectedCodeBus}) is assigned to circuit "${busCircuit}", not "${selectedCodeCircuit}".`
                    : `The selected bus (${selectedCodeBus}) is not assigned to any circuit.`,
                icon: {
                    show: false,
                },
                actions: {
                    confirm: {
                        label: 'OK',
                        color: 'primary'
                    }
                },
                dismissible: true
            });
            return false;
        }

        return true;
    }

    getRiskBadgeClass(level?: 'low' | 'medium' | 'high'): string {
        switch (level) {
            case 'high':
                return 'bg-red-100 text-red-700 dark:bg-red-500/20 dark:text-red-300';
            case 'medium':
                return 'bg-amber-100 text-amber-700 dark:bg-amber-500/20 dark:text-amber-300';
            default:
                return 'bg-green-100 text-green-700 dark:bg-green-500/20 dark:text-green-300';
        }
    }

    getRiskLabel(level?: 'low' | 'medium' | 'high'): string {
        switch (level) {
            case 'high':
                return 'High';
            case 'medium':
                return 'Medium';
            default:
                return 'Low';
        }
    }

    /**
     * On destroy
     */
    ngOnDestroy(): void {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }

    protected readonly FuseNavigationAction = FuseNavigationAction;
}
