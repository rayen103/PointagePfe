export type AnalyseReportType = 'bus' | 'employe' | 'trace';

export interface AnalyseColumn {
    key: string;
    label: string;
    dataType: string;
    isNumeric: boolean;
}

export interface AnalyseQueryRequest {
    dateFrom?: string | null;
    dateTo?: string | null;
    fields: string[];
}

export interface AnalyseQueryResponse {
    columns: AnalyseColumn[];
    rows: Record<string, any>[];
    totals: Record<string, number>;
}

export interface ReportLayout {
    reportLayoutId: string;
    reportType: number;
    name: string;
    configJson: string;
    isDefault: boolean;
}

export interface AnalyseDesignerConfig {
    fields: string[];
    dateFrom?: string | null;
    dateTo?: string | null;
}

