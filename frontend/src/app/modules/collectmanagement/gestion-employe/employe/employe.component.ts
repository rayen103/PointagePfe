import {
    ChangeDetectionStrategy,
    ChangeDetectorRef,
    Component, OnDestroy,
    OnInit,
    ViewChild,
    ViewEncapsulation,
} from '@angular/core';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
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
        MatSortModule,
        NgTemplateOutlet,
        ReactiveFormsModule,
        NgClass,
        MatPaginatorModule,
        MatAutocomplete,
        MatAutocompleteTrigger,
        NgForOf,
        TranslocoDirective,
    ],
    templateUrl: './employe.component.html',
    styleUrl: './employe.component.scss',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    animations: fuseAnimations
})
export class EmployeComponent implements OnInit, OnDestroy {

    @ViewChild(MatPaginator) private _paginator: MatPaginator;
    @ViewChild(MatSort) private _sort: MatSort;

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
    filteredSocietes$: Observable<Societe[]>;
    isViewMode: boolean = false; // To distinguish between view and edit mode


    constructor(
        private _employeService: EmployeService,
        private _activatedRoute: ActivatedRoute,
        private _changeDetectorRef: ChangeDetectorRef,
        private _fuseConfirmationService: FuseConfirmationService,
        private _formBuilder: UntypedFormBuilder,
        private _societeService: SocieteService,

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
            this._sort?.active,
            this._sort?.direction,
            this.searchInputControl.value);
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
            matricule: ['', [Validators.required]],
            rfid: [''],
            nom: ['', [Validators.required]],
            prenom: ['', [Validators.required]],
            typeEmploye: ['EmployeSimple', [Validators.required]],
            codeCircuit: [''],
            codePointCollecte: [''],
            codeBus: [''],
            codeShift: [''],
            adresse: [''],
            codeGouvernorat: [''],
            codeRegion: [''],
            latitude: [null],
            longitude: [null],
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


    }

    CreateEmploye() {

        if (!this.hasActionPermission(FuseNavigationAction.Add)) {
            return;
        }

        this._employeService.CreateNewEmploye().subscribe((newEmploye) => {
            this.selectedEmploye = newEmploye;
            this.isViewMode = false; // Edit mode for new employee
            this.selectedEmployeForm.patchValue(newEmploye);
            this.selectedEmployeForm.enable(); // Enable form for new employee
            this._changeDetectorRef.markForCheck();
        });
    }

    /**
     * Toggle Employe details (view mode - read-only)
     *
     * @param employeId
     */
    toggleDetails(employeId: string): void {
        // If the employe is already selected...
        if (this.selectedEmploye && this.selectedEmploye.employeId === employeId) {
            // Close the details
            this.closeDetails();
            return;
        }

        // Get the employe by id
        this._employeService.GetEmployeById(employeId)
            .subscribe((employe) => {

                // Set the selected employe
                this.selectedEmploye = employe;
                this.isViewMode = true; // View mode

                // Fill the form (for when switching to edit mode)
                this.selectedEmployeForm.patchValue(employe);

                // Mark for check
                this._changeDetectorRef.markForCheck();
            });
    }

    /**
     * Edit Employe - opens details in edit mode
     *
     * @param employeId
     */
    editEmploye(employeId: string): void {
        // If the employe is already selected in edit mode...
        if (this.selectedEmploye && this.selectedEmploye.employeId === employeId && !this.isViewMode) {
            // Close the details
            this.closeDetails();
            return;
        }

        // Get the employe by id
        this._employeService.GetEmployeById(employeId)
            .subscribe((employe) => {

                // Set the selected employe
                this.selectedEmploye = employe;
                this.isViewMode = false; // Edit mode

                // Fill the form
                this.selectedEmployeForm.patchValue(employe);
                
                // Enable all form controls in edit mode
                this.selectedEmployeForm.enable();

                // Mark for check
                this._changeDetectorRef.markForCheck();
            });
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
            this._changeDetectorRef.markForCheck();
            setTimeout(() => {
                this.saveClicked = false;
                this._changeDetectorRef.markForCheck();
            }, 500);
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
