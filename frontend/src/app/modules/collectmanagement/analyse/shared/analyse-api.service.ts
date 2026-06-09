import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { ApiService } from 'app/core/common/api.service';
import {
    AvailableBusEtaPredictionResponse,
    AnalyseQueryRequest,
    AnalyseQueryResponse,
    AnalyseReportType,
    BusEtaPredictionRequest,
    BusEtaPredictionResponse,
    ReportLayout,
} from './analyse.model';

@Injectable({ providedIn: 'root' })
export class AnalyseApiService {
    constructor(private readonly _api: ApiService) {}

    runQuery(type: AnalyseReportType, request: AnalyseQueryRequest): Observable<AnalyseQueryResponse> {
        return this._api.Post2<AnalyseQueryResponse>(`analyse/${type}/query`, request).pipe(
            map((r) => r.data)
        );
    }

    getLayouts(type: AnalyseReportType): Observable<ReportLayout[]> {
        return this._api.Get<ReportLayout[]>(`analyse/${type}/layouts`).pipe(
            map((r) => r.data ?? [])
        );
    }

    upsertLayout(
        type: AnalyseReportType,
        payload: {
            reportLayoutId?: string | null;
            reportType?: number;
            name: string;
            configJson: string;
            isDefault: boolean;
        }
    ): Observable<ReportLayout> {
        return this._api.Post2<ReportLayout>(`analyse/${type}/layouts`, payload).pipe(
            map((r) => r.data)
        );
    }

    deleteLayout(type: AnalyseReportType, id: string): Observable<boolean> {
        return this._api.Post2<boolean>(`analyse/${type}/layouts/${id}/delete`, {}).pipe(
            map((r) => r.success)
        );
    }

    predictBusEta(request: BusEtaPredictionRequest): Observable<BusEtaPredictionResponse> {
        // Map frontend camelCase to Python snake_case
        const snakeCaseRequest = {
            DistanceFromStop: request.DistanceFromStop,
            log_distance: request.LogDistance,
            distance_over_300m: request.DistanceOver300m,
            hour: request.Hour,
            hour_sin: request.HourSin,
            hour_cos: request.HourCos,
            is_rush_hour: request.IsRushHour,
            day_of_week: request.DayOfWeek,
            Latitude: request.Latitude,
            Longitude: request.Longitude,
            code_circuit: request.CodeCircuit,
            model_bus: request.ModelBus,
            Capacite: request.Capacite,
            current_occupancy: request.CurrentOccupancy,
            last_position_at: request.LastPositionAt ? new Date(request.LastPositionAt).toISOString() : undefined,
        };

        return this._api.Post2<any>('prediction/bus-eta', snakeCaseRequest).pipe(
            map((r) => {
                // Map Python snake_case back to frontend camelCase
                return {
                    EtaMinutes: r.data.eta_minutes,
                    EtaSeconds: r.data.eta_seconds,
                    Confidence: r.data.confidence,
                    UsedFallbackStop: r.data.used_fallback_stop,
                };
            })
        );
    }

    predictAvailableBusEta(): Observable<AvailableBusEtaPredictionResponse> {
        return this._api.Get<AvailableBusEtaPredictionResponse>('prediction/bus-eta/available').pipe(
            map((r) => r.data)
        );
    }
}
