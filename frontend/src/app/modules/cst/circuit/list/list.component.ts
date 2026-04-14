import {
    ChangeDetectionStrategy,
    ChangeDetectorRef,
    Component,
    OnDestroy,
    OnInit,
    ViewChild,
    ViewEncapsulation,
} from '@angular/core';
import { TranslocoModule } from '@ngneat/transloco';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { AsyncPipe, CommonModule } from '@angular/common';
import { ReactiveFormsModule, UntypedFormControl } from '@angular/forms';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { RouterLink } from '@angular/router';
import { fuseAnimations } from '../../../../../@fuse/animations';
import { forkJoin, map, Observable, of, Subject, switchMap, takeUntil } from 'rxjs';
import { Circuit } from '../../../../core/circuit/circuit.model';
import { CircuitService } from '../../../../core/circuit/circuit.service';
import { FuseConfirmationService } from '../../../../../@fuse/services/confirmation';
import { RoleNavigation } from '../../../../core/role-utilisateur/role-utilisateur.model';
import { FuseNavigationAction } from '../../../../../@fuse/components/navigation';
import { MapViewerComponent, MapLocation } from '../../../../shared/components/map-viewer/map-viewer.component';
import { MapGeocodingService } from '../../../../core/common/map-geocoding.service';

@Component({
  selector: 'app-list',
  standalone: true,
    imports: [
        MatButtonModule,
        MatFormFieldModule,
        MatIconModule,
        MatInputModule,
        MatProgressBarModule,
        ReactiveFormsModule,
        CommonModule,
        MatPaginatorModule,
        TranslocoModule,
        RouterLink,
        MapViewerComponent,
    ],
  templateUrl: './list.component.html',
  styleUrl: './list.component.scss',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    animations: fuseAnimations,
})
export class ListComponent implements OnInit, OnDestroy {
    @ViewChild(MatPaginator) private _paginator: MatPaginator;

    circuit$: Observable<Circuit[]>;

    flashMessage: 'success' | 'error' | null = null;
    isLoading: boolean = false;
    circuitsLength: number;
    searchInputControl: UntypedFormControl = new UntypedFormControl();
    private _unsubscribeAll: Subject<any> = new Subject<any>();
    roleNavigation: RoleNavigation;
    selectedCircuit: Circuit | null = null;
    isViewMode: boolean = false;
    showMapView: boolean = false; // Toggle between list and map view
    mapLocations: MapLocation[] = []; // Locations for map display
    mapCircuitsWithLocations: number = 0;
    sortActive: string = 'codeCircuit';
    sortDirection: 'asc' | 'desc' = 'asc';

    constructor(
        private _circuitService: CircuitService,
        private _mapGeocodingService: MapGeocodingService,
        private _changeDetectorRef: ChangeDetectorRef,
        private _fuseConfirmationService: FuseConfirmationService
    ) {}

    SortChange() {
        this.isLoading = true;
        this.getCircuits()
            .pipe(
                map(() => {
                    this.isLoading = false;

                    // Mark for check
                    this._changeDetectorRef.markForCheck();
                })
            )
            .subscribe();
    }

    getCircuits() {
        return this._circuitService.GetCircuit(
            (this._paginator?.pageIndex | 0) + 1,
            this._paginator?.pageSize,
            this.sortActive,
            this.sortDirection,
            this.searchInputControl.value
        );
    }

    setSort(active: string, direction: 'asc' | 'desc'): void {
        this.sortActive = active;
        this.sortDirection = direction;
        this.SortChange();
    }

    hasActionPermission(action: FuseNavigationAction): boolean {
        return !this.roleNavigation || this.roleNavigation?.actions?.includes(action);
    }

    ngOnInit(): void {
        this.circuit$ = this._circuitService.circuits$;

        this._circuitService.circuitsLength$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((length) => {
                this.circuitsLength = length;

                // Mark for check
                this._changeDetectorRef.markForCheck();
            });
        
        // Subscribe to circuits to update map locations
        this.circuit$
            .pipe(
                switchMap((circuits) => this.buildMapLocations(circuits)),
                takeUntil(this._unsubscribeAll)
            )
            .subscribe((mapLocations) => {
                this.mapLocations = mapLocations;
                this.mapCircuitsWithLocations = new Set(
                    mapLocations.map((location) => location.circuitId ?? location.id)
                ).size;
                this._changeDetectorRef.markForCheck();
            });
        
        this.searchInputControl.valueChanges
            .pipe(
                switchMap((query) => {
                    this.isLoading = true;
                    return this.getCircuits();
                }),
                map(() => {
                    this.isLoading = false;
                })
            )
            .subscribe();
    }

    /**
     * Update map locations from circuits
     */
    private buildMapLocations(circuits: Circuit[]): Observable<MapLocation[]> {
        if (!circuits?.length) {
            return of([]);
        }

        const perCircuitLocations$ = circuits.map((circuit) => {
            const baseLocations: MapLocation[] = [];

            if (circuit.latitude != null && circuit.longitude != null) {
                baseLocations.push({
                    id: `${circuit.circuitId}-base`,
                    circuitId: circuit.circuitId,
                    pointType: 'base',
                    name: `${circuit.codeCircuit}${circuit.libelleCircuit ? ` - ${circuit.libelleCircuit}` : ''}`,
                    latitude: circuit.latitude,
                    longitude: circuit.longitude,
                    isActive: circuit.isActive,
                    description: circuit.description,
                });
            }

            const departureAddress = (circuit.codePCDepart ?? '').trim();
            const arrivalAddress = (circuit.codePCArrivee ?? '').trim();

            const departure$ = departureAddress
                ? this._mapGeocodingService.searchAddress(departureAddress)
                : of(null);
            const arrival$ = arrivalAddress
                ? this._mapGeocodingService.searchAddress(arrivalAddress)
                : of(null);

            return forkJoin({
                departure: departure$,
                arrival: arrival$,
            }).pipe(
                map(({ departure, arrival }) => {
                    if (departure) {
                        baseLocations.push({
                            id: `${circuit.circuitId}-depart`,
                            circuitId: circuit.circuitId,
                            pointType: 'departure',
                            name: `${circuit.codeCircuit} - Departure`,
                            latitude: departure.latitude,
                            longitude: departure.longitude,
                            isActive: circuit.isActive,
                            description: departureAddress,
                        });
                    }

                    if (arrival) {
                        baseLocations.push({
                            id: `${circuit.circuitId}-arrivee`,
                            circuitId: circuit.circuitId,
                            pointType: 'arrival',
                            name: `${circuit.codeCircuit} - Arrival`,
                            latitude: arrival.latitude,
                            longitude: arrival.longitude,
                            isActive: circuit.isActive,
                            description: arrivalAddress,
                        });
                    }

                    return baseLocations;
                })
            );
        });

        return forkJoin(perCircuitLocations$).pipe(
            map((locationsPerCircuit) => locationsPerCircuit.flat())
        );
    }

    /**
     * Toggle between list and map view
     */
    toggleMapView(): void {
        this.showMapView = !this.showMapView;
        this._changeDetectorRef.markForCheck();
    }

    /**
     * Toggle circuit details for viewing (read-only mode)
     *
     * @param circuitId
     */
    toggleDetails(circuitId: string): void {
        if (this.selectedCircuit && this.selectedCircuit.circuitId === circuitId) {
            this.closeDetails();
            return;
        }

        this.circuit$.pipe(
            map((circuits) => {
                const index = circuits.findIndex(item => item.circuitId === circuitId);
                return circuits[index];
            })
        )
            .subscribe((circuit) => {
                this.selectedCircuit = circuit;
                this.isViewMode = true;

                this._changeDetectorRef.markForCheck();
            });
    }

    /**
     * Edit circuit - opens details in edit mode
     *
     * @param circuitId
     */
    editCircuit(circuitId: string): void {
        if (this.selectedCircuit && this.selectedCircuit.circuitId === circuitId) {
            this.closeDetails();
            return;
        }

        this.circuit$.pipe(
            map((circuits) => {
                const index = circuits.findIndex(item => item.circuitId === circuitId);
                return circuits[index];
            })
        )
            .subscribe((circuit) => {
                this.selectedCircuit = circuit;
                this.isViewMode = false;

                this._changeDetectorRef.markForCheck();
            });
    }

    /**
     * Close the details
     */
    closeDetails(): void {
        this.selectedCircuit = null;
        this.isViewMode = false;
    }

    /**
     * Delete the selected circuit
     */
    deleteSelectedCircuit(circuit: Circuit): void {
        if (!this.hasActionPermission(FuseNavigationAction.Delete)) {
            return;
        }
        const confirmation = this._fuseConfirmationService.open({
            title: 'Delete Circuit',
            message:
                'Are you sure you want to remove this circuit? This action cannot be undone!',
            actions: {
                confirm: {
                    label: 'Delete',
                },
            },
        });

        confirmation.afterClosed().subscribe((result) => {
            if (result === 'confirmed') {
                this._circuitService
                    .DeleteCircuit({ circuitId: circuit.circuitId })
                    .subscribe(() => {
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
        return item.id || index;
    }

    /**
     * On destroy
     */
    ngOnDestroy(): void {
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }

    protected readonly FuseNavigationAction = FuseNavigationAction;
}
