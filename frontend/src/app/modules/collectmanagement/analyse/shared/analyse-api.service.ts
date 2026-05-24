import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { ApiService } from 'app/core/common/api.service';
import { AnalyseQueryRequest, AnalyseQueryResponse, AnalyseReportType, ReportLayout } from './analyse.model';

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
}

