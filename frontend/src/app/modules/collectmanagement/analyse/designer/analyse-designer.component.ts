import {
    ChangeDetectionStrategy,
    ChangeDetectorRef,
    Component,
    Input,
    OnDestroy,
    OnInit,
    ViewEncapsulation,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, UntypedFormControl, UntypedFormGroup, UntypedFormBuilder } from '@angular/forms';
import { CdkDragDrop, DragDropModule, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatTableModule } from '@angular/material/table';
import { BehaviorSubject, finalize, Subject, takeUntil } from 'rxjs';
import { AnalyseApiService } from '../shared/analyse-api.service';
import {
    AnalyseColumn,
    AnalyseDesignerConfig,
    AnalyseQueryResponse,
    AnalyseReportType,
    ReportLayout,
} from '../shared/analyse.model';
import { fuseAnimations } from '../../../../../@fuse/animations';

export interface AnalyseFieldDef {
    key: string;
    label: string;
    dataType: string;
    isNumeric: boolean;
}

interface ColorScheme {
    primary: string;
    secondary: string;
    accent: string;
    gradient: string;
}

const colorSchemes: Record<AnalyseReportType, ColorScheme> = {
    bus: {
        primary: '#3b82f6',
        secondary: '#1d4ed8',
        accent: '#93c5fd',
        gradient: 'linear-gradient(135deg, #3b82f6 0%, #1d4ed8 100%)'
    },
    employe: {
        primary: '#10b981',
        secondary: '#059669',
        accent: '#6ee7b7',
        gradient: 'linear-gradient(135deg, #10b981 0%, #059669 100%)'
    },
    trace: {
        primary: '#f59e0b',
        secondary: '#d97706',
        accent: '#fcd34d',
        gradient: 'linear-gradient(135deg, #f59e0b 0%, #d97706 100%)'
    }
};

@Component({
    selector: 'app-analyse-designer',
    standalone: true,
    imports: [
        CommonModule,
        ReactiveFormsModule,
        DragDropModule,
        MatButtonModule,
        MatFormFieldModule,
        MatIconModule,
        MatInputModule,
        MatProgressBarModule,
        MatSelectModule,
        MatDatepickerModule,
        MatNativeDateModule,
        MatSlideToggleModule,
        MatTableModule,
    ],
    templateUrl: './analyse-designer.component.html',
    styleUrl: './analyse-designer.component.scss',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    animations: fuseAnimations,
})
export class AnalyseDesignerComponent implements OnInit, OnDestroy {
    @Input({ required: true }) reportType: AnalyseReportType;
    @Input({ required: true }) title: string;
    @Input({ required: true }) fields: AnalyseFieldDef[] = [];
    
    get colors(): ColorScheme {
        return colorSchemes[this.reportType];
    }

    readonly form: UntypedFormGroup;
    readonly searchFieldControl = new UntypedFormControl('');

    isLoading: boolean = false;
    layouts: ReportLayout[] = [];
    selectedLayoutId: string | null = null;

    availableFields: AnalyseFieldDef[] = [];
    selectedFields: AnalyseFieldDef[] = [];
    lignesFields: AnalyseFieldDef[] = [];
    colonnesFields: AnalyseFieldDef[] = [];
    valeursFields: AnalyseFieldDef[] = [];

    response: AnalyseQueryResponse | null = null;
    displayedColumns: string[] = [];
    rows: Record<string, any>[] = [];
    totals: Record<string, number> = {};

    private readonly _unsubscribeAll = new Subject<void>();
    private readonly _requestInFlight$ = new BehaviorSubject<boolean>(false);

    constructor(
        private readonly _fb: UntypedFormBuilder,
        private readonly _api: AnalyseApiService,
        private readonly _changeDetectorRef: ChangeDetectorRef
    ) {
        this.form = this._fb.group({
            dateFrom: [null],
            dateTo: [null],
            layoutName: [''],
            isDefault: [false],
        });
    }

    ngOnInit(): void {
        this.availableFields = [...this.fields];
        this.selectedFields = [];
        this.lignesFields = [];
        this.colonnesFields = [];
        this.valeursFields = [];

        this.searchFieldControl.valueChanges
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe(() => {
                this._changeDetectorRef.markForCheck();
            });

        this.loadLayouts();
    }

    loadLayouts(): void {
        this._api.getLayouts(this.reportType)
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((layouts) => {
                this.layouts = layouts ?? [];
                const defaultLayout = this.layouts.find((l) => l.isDefault);
                if (defaultLayout) {
                    this.selectLayout(defaultLayout.reportLayoutId, true);
                }
                this._changeDetectorRef.markForCheck();
            });
    }

    selectLayout(layoutId: string, autoRun: boolean = false): void {
        this.selectedLayoutId = layoutId;
        const layout = this.layouts.find((l) => l.reportLayoutId === layoutId);
        if (!layout) {
            return;
        }

        this.form.patchValue({
            layoutName: layout.name,
            isDefault: layout.isDefault,
        });

        let config: AnalyseDesignerConfig | null = null;
        try {
            config = JSON.parse(layout.configJson) as AnalyseDesignerConfig;
        } catch {
            config = null;
        }

        const fieldKeys = config?.fields ?? [];
        const selected = this.fields.filter((f) => fieldKeys.includes(f.key));
        const selectedSet = new Set(selected.map((f) => f.key));

        this.availableFields = this.fields.filter((f) => !selectedSet.has(f.key));

        this.lignesFields = [];
        this.colonnesFields = [];
        this.valeursFields = [];
        selected.forEach((f) => {
            if (f.isNumeric) {
                this.valeursFields.push(f);
            } else if (
                f.key.toLowerCase().includes('week') ||
                f.key.toLowerCase().includes('semaine') ||
                f.key.toLowerCase().includes('date')
            ) {
                this.colonnesFields.push(f);
            } else {
                this.lignesFields.push(f);
            }
        });
        this.selectedFields = [...this.lignesFields, ...this.colonnesFields, ...this.valeursFields];

        const dateFrom = config?.dateFrom ? new Date(config.dateFrom) : null;
        const dateTo = config?.dateTo ? new Date(config.dateTo) : null;
        this.form.patchValue({ dateFrom, dateTo });

        this._changeDetectorRef.markForCheck();

        if (autoRun) {
            this.run();
        }
    }

    clearLayoutSelection(): void {
        this.selectedLayoutId = null;
        this.form.patchValue({ layoutName: '', isDefault: false });
        this._changeDetectorRef.markForCheck();
    }

    onDrop(event: CdkDragDrop<AnalyseFieldDef[]>): void {
        if (event.previousContainer === event.container) {
            moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
        } else {
            transferArrayItem(
                event.previousContainer.data,
                event.container.data,
                event.previousIndex,
                event.currentIndex
            );
        }

        this.syncSelectedFields();
        this._changeDetectorRef.markForCheck();
    }

    syncSelectedFields(): void {
        this.availableFields = [...this.availableFields].sort((a, b) => a.label.localeCompare(b.label));
        this.selectedFields = [...this.lignesFields, ...this.colonnesFields, ...this.valeursFields];
    }

    removeSelected(key: string, listType: 'lignes' | 'colonnes' | 'valeurs'): void {
        let list: AnalyseFieldDef[];
        if (listType === 'lignes') list = this.lignesFields;
        else if (listType === 'colonnes') list = this.colonnesFields;
        else list = this.valeursFields;

        const index = list.findIndex((f) => f.key === key);
        if (index === -1) {
            return;
        }

        const [removed] = list.splice(index, 1);
        this.availableFields.push(removed);
        this.syncSelectedFields();
        this._changeDetectorRef.markForCheck();
    }

    run(): void {
        if (this._requestInFlight$.value) {
            return;
        }

        this._requestInFlight$.next(true);
        this.isLoading = true;
        this._changeDetectorRef.markForCheck();

        const dateFrom: Date | null = this.form.get('dateFrom')?.value ?? null;
        const dateTo: Date | null = this.form.get('dateTo')?.value ?? null;

        const request = {
            dateFrom: dateFrom ? dateFrom.toISOString() : null,
            dateTo: dateTo ? dateTo.toISOString() : null,
            fields: this.selectedFields.map((f) => f.key),
        };

        this._api.runQuery(this.reportType, request)
            .pipe(
                finalize(() => {
                    this._requestInFlight$.next(false);
                    this.isLoading = false;
                    this._changeDetectorRef.markForCheck();
                }),
                takeUntil(this._unsubscribeAll)
            )
            .subscribe((res) => {
                this.response = res;
                this.applyResponse(res);
                this._changeDetectorRef.markForCheck();
            });
    }

    saveLayout(): void {
        const name = (this.form.get('layoutName')?.value ?? '').toString().trim();
        if (!name) {
            return;
        }

        const dateFrom: Date | null = this.form.get('dateFrom')?.value ?? null;
        const dateTo: Date | null = this.form.get('dateTo')?.value ?? null;
        const config: AnalyseDesignerConfig = {
            fields: this.selectedFields.map((f) => f.key),
            dateFrom: dateFrom ? dateFrom.toISOString() : null,
            dateTo: dateTo ? dateTo.toISOString() : null,
        };

        const payload = {
            reportLayoutId: this.selectedLayoutId,
            name,
            configJson: JSON.stringify(config),
            isDefault: this.form.get('isDefault')?.value === true,
        };

        this.isLoading = true;
        this._changeDetectorRef.markForCheck();

        this._api.upsertLayout(this.reportType, payload)
            .pipe(
                finalize(() => {
                    this.isLoading = false;
                    this._changeDetectorRef.markForCheck();
                }),
                takeUntil(this._unsubscribeAll)
            )
            .subscribe((saved) => {
                this.selectedLayoutId = saved.reportLayoutId;
                this.loadLayouts();
            });
    }

    deleteLayout(): void {
        if (!this.selectedLayoutId) {
            return;
        }

        const id = this.selectedLayoutId;
        this.isLoading = true;
        this._changeDetectorRef.markForCheck();

        this._api.deleteLayout(this.reportType, id)
            .pipe(
                finalize(() => {
                    this.isLoading = false;
                    this._changeDetectorRef.markForCheck();
                }),
                takeUntil(this._unsubscribeAll)
            )
            .subscribe(() => {
                this.clearLayoutSelection();
                this.loadLayouts();
            });
    }

    getFilteredAvailable(): AnalyseFieldDef[] {
        const q = (this.searchFieldControl.value ?? '').toString().trim().toLowerCase();
        if (!q) {
            return this.availableFields;
        }

        return this.availableFields.filter((f) => {
            const hay = `${f.label} ${f.key}`.toLowerCase();
            return hay.includes(q);
        });
    }

    getTotalLabel(key: string): string {
        const column = (this.response?.columns ?? []).find((c) => c.key === key);
        return column?.label ?? key;
    }

    private applyResponse(res: AnalyseQueryResponse): void {
        const columns = res?.columns ?? [];
        this.displayedColumns = columns.map((c) => c.key);
        this.rows = res?.rows ?? [];
        this.totals = res?.totals ?? {};
    }

    ngOnDestroy(): void {
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }
}

