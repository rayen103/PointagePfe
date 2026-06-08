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

export interface BusEtaPredictionRequest {
    DistanceFromStop: number;
    log_distance: number;
    distance_over_300m: number;
    hour: number;
    hour_sin?: number | null;
    hour_cos?: number | null;
    is_rush_hour: number;
    day_of_week: number;
    DirectionRef: number;
    is_weekend: number;
}

export interface BusEtaPredictionResponse {
    eta_minutes: number;
    confidence: number;
}

export interface AvailableBusEtaPrediction {
    busId: string;
    numeroIMM: string;
    codeCircuit?: string | null;
    distanceFromStop: number;
    eta_minutes: number;
    confidence: number;
}

export interface AvailableBusEtaPredictionResponse {
    predictions: AvailableBusEtaPrediction[];
}
