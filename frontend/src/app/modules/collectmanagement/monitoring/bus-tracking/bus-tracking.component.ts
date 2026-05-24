import {
    ChangeDetectionStrategy,
    ChangeDetectorRef,
    Component,
    OnDestroy,
    OnInit,
    ViewEncapsulation,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { TranslocoModule } from '@ngneat/transloco';
import {
    UntypedFormControl,
    ReactiveFormsModule,
} from '@angular/forms';
import {
    BehaviorSubject,
    catchError,
    debounceTime,
    distinctUntilChanged,
    EMPTY,
    finalize,
    forkJoin,
    map,
    of,
    startWith,
    Subject,
    switchMap,
    takeUntil,
    timer,
} from 'rxjs';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { fuseAnimations } from '../../../../../@fuse/animations';
import { FuseConfirmationService } from '../../../../../@fuse/services/confirmation';
import { FuseNavigationAction } from '../../../../../@fuse/components/navigation';
import { RoleNavigation } from '../../../../core/role-utilisateur/role-utilisateur.model';
import { BusService } from '../../../../core/bus/bus.service';
import { BusRuntimeEvent } from '../../../../core/bus/bus.model';
import {
    BusTrackingAdapterService,
    BusTrackingItem,
} from './bus-tracking.adapter.service';
import { BusTrackingListComponent } from './list/bus-tracking-list.component';
import { BusTrackingMapComponent } from './map/bus-tracking-map.component';
import { BusTrackingPanelComponent } from './panel/bus-tracking-panel.component';

@Component({
    selector: 'app-bus-tracking',
    standalone: true,
    imports: [
        CommonModule,
        TranslocoModule,
        ReactiveFormsModule,
        MatButtonModule,
        MatFormFieldModule,
        MatIconModule,
        MatInputModule,
        MatProgressBarModule,
        MatSlideToggleModule,
        BusTrackingListComponent,
        BusTrackingPanelComponent,
        BusTrackingMapComponent,
    ],
    templateUrl: './bus-tracking.component.html',
    styleUrl: './bus-tracking.component.scss',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    animations: fuseAnimations,
})
export class BusTrackingComponent implements OnInit, OnDestroy {
    readonly searchInputControl = new UntypedFormControl('');
    readonly autoRefreshControl = new UntypedFormControl(true);
    readonly showAllOnMapControl = new UntypedFormControl(false);

    isLoading: boolean = false;
    roleNavigation: RoleNavigation;
    lastRefreshAtUtc: string | null = null;

    buses: BusTrackingItem[] = [];
    filteredBuses: BusTrackingItem[] = [];
    selectedBus: BusTrackingItem | null = null;
    selectedEvents: BusRuntimeEvent[] = [];
    selectedBusEventsLoading: boolean = false;

    private readonly _selectedBusId$ = new BehaviorSubject<string | null>(null);
    private readonly _unsubscribeAll = new Subject<void>();
    private readonly _pollIntervalMs = 10000;

    constructor(
        private readonly _activatedRoute: ActivatedRoute,
        private readonly _busService: BusService,
        private readonly _adapter: BusTrackingAdapterService,
        private readonly _changeDetectorRef: ChangeDetectorRef,
        private readonly _fuseConfirmationService: FuseConfirmationService
    ) {}

    hasActionPermission(action: FuseNavigationAction): boolean {
        return !this.roleNavigation || this.roleNavigation?.actions?.includes(action);
    }

    ngOnInit(): void {
        this._activatedRoute.data
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((data) => {
                if (data?.navigation) {
                    this.roleNavigation = data.navigation;
                }
            });

        this.searchInputControl.valueChanges
            .pipe(
                debounceTime(250),
                distinctUntilChanged(),
                takeUntil(this._unsubscribeAll)
            )
            .subscribe(() => {
                this.applyClientFilter();
                this._changeDetectorRef.markForCheck();
            });

        this._selectedBusId$
            .pipe(
                switchMap((busId) => {
                    if (!busId) {
                        this.selectedEvents = [];
                        this._changeDetectorRef.markForCheck();
                        return of([]);
                    }

                    this.selectedBusEventsLoading = true;
                    this._changeDetectorRef.markForCheck();

                    return this._busService.GetBusRuntimeEvents(busId).pipe(
                        catchError(() => of([])),
                        finalize(() => {
                            this.selectedBusEventsLoading = false;
                            this._changeDetectorRef.markForCheck();
                        })
                    );
                }),
                takeUntil(this._unsubscribeAll)
            )
            .subscribe((events) => {
                this.selectedEvents = [...(events ?? [])].sort(
                    (a, b) =>
                        new Date(b.occurredAtUtc).getTime() -
                        new Date(a.occurredAtUtc).getTime()
                );
                this._changeDetectorRef.markForCheck();
            });

        this.startPolling();
    }

    startPolling(): void {
        this.autoRefreshControl.valueChanges
            .pipe(
                startWith(this.autoRefreshControl.value),
                map((enabled) => enabled !== false),
                switchMap((enabled) =>
                    enabled ? timer(0, this._pollIntervalMs) : EMPTY
                ),
                switchMap(() => this.refreshOnce()),
                takeUntil(this._unsubscribeAll)
            )
            .subscribe();
    }

    manualRefresh(): void {
        this.refreshOnce().pipe(takeUntil(this._unsubscribeAll)).subscribe();
    }

    refreshOnce() {
        this.isLoading = true;
        this._changeDetectorRef.markForCheck();

        return forkJoin({
            paged: this._busService.GetBuses(1, 1000, 'numeroIMM', 'asc', ''),
            snapshot: this._busService.GetLivePositionsSnapshot(),
        }).pipe(
            map(({ paged, snapshot }) => {
                const items = this._adapter.buildTrackingItems(
                    paged?.buses ?? [],
                    snapshot ?? null
                );

                this.buses = items;
                this.applyClientFilter();
                this.lastRefreshAtUtc = snapshot?.generatedAtUtc ?? null;
                this.syncSelectedBus();
                this._changeDetectorRef.markForCheck();
            }),
            finalize(() => {
                this.isLoading = false;
                this._changeDetectorRef.markForCheck();
            }),
            catchError(() => {
                this.isLoading = false;
                this._changeDetectorRef.markForCheck();
                return of(null);
            })
        );
    }

    onSelectBus(busId: string): void {
        this._selectedBusId$.next(busId);
        this.syncSelectedBus();
        this._changeDetectorRef.markForCheck();
    }

    onToggleShowAllOnMap(showAll: boolean): void {
        this.showAllOnMapControl.setValue(showAll);
        this._changeDetectorRef.markForCheck();
    }

    getMapLocations() {
        return this._adapter.buildMapLocations(this.filteredBuses, {
            selectedBusId: this.selectedBus?.busId ?? this._selectedBusId$.value,
            showAll: this.showAllOnMapControl.value === true,
        });
    }

    emptySelectedBus(): void {
        if (!this.selectedBus) {
            return;
        }

        if (!this.hasActionPermission(FuseNavigationAction.Edit)) {
            return;
        }

        const confirmation = this._fuseConfirmationService.open({
            title: 'Vider le Bus',
            message:
                'Êtes-vous sûr de vouloir vider ce bus ? Cette action réinitialise le taux d’occupation.',
            icon: { show: false },
            actions: {
                confirm: { label: 'Vider', color: 'warn' },
                cancel: { label: 'Annuler' },
            },
            dismissible: true,
        });

        confirmation.afterClosed().subscribe((result) => {
            if (result !== 'confirmed') {
                return;
            }

            this.isLoading = true;
            this._changeDetectorRef.markForCheck();

            this._busService.EmptyBus(this.selectedBus!.busId)
                .pipe(
                    switchMap((runtimeState) => {
                        this.buses = this._adapter.mergeRuntimeState(this.buses, runtimeState);
                        this.applyClientFilter();
                        this.syncSelectedBus();
                        this._changeDetectorRef.markForCheck();

                        return this.refreshOnce().pipe(map(() => runtimeState));
                    }),
                    finalize(() => {
                        this.isLoading = false;
                        this._changeDetectorRef.markForCheck();
                    }),
                    catchError(() => {
                        this.isLoading = false;
                        this._changeDetectorRef.markForCheck();
                        return of(null);
                    })
                )
                .subscribe(() => {
                    const busId = this.selectedBus?.busId ?? null;
                    if (busId) {
                        this._selectedBusId$.next(busId);
                    }
                });
        });
    }

    private applyClientFilter(): void {
        const q = (this.searchInputControl.value ?? '').toString().trim().toLowerCase();
        if (!q) {
            this.filteredBuses = [...this.buses];
            return;
        }

        this.filteredBuses = (this.buses ?? []).filter((b) => {
            const haystack = `${b.numeroIMM ?? ''} ${b.imei ?? ''}`.toLowerCase();
            return haystack.includes(q);
        });
    }

    private syncSelectedBus(): void {
        const selectedId = this._selectedBusId$.value;
        if (!selectedId) {
            this.selectedBus = null;
            return;
        }

        this.selectedBus = this.buses.find((b) => b.busId === selectedId) ?? null;
    }

    ngOnDestroy(): void {
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }

    protected readonly FuseNavigationAction = FuseNavigationAction;
}
