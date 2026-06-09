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
  // Legacy compatibility fields
  DistanceFromStop?: number;
  LogDistance?: number;
  DistanceOver300m?: number;
  Hour?: number;
  HourSin?: number;
  HourCos?: number;
  IsRushHour?: number;
  DayOfWeek?: number;
  DirectionRef?: number;
  IsWeekend?: number;

  // New raw database fields
  Latitude?: number;
  Longitude?: number;
  CodeCircuit?: string;
  ModelBus?: string;
  Capacite?: number;
  CurrentOccupancy?: number;
  LastPositionAt?: Date;
}

export interface BusEtaPredictionResponse {
  EtaMinutes: number;
  EtaSeconds: number;
  Confidence: number;
  UsedFallbackStop: boolean;
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
