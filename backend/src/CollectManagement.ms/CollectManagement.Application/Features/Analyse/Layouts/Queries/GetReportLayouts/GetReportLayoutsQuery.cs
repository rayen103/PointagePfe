using CollectManagement.Application.Features.Analyse.Contracts;
using CollectManagement.Domain.Analyse.Enums;

namespace CollectManagement.Application.Features.Analyse.Layouts.Queries.GetReportLayouts;

public record GetReportLayoutsQuery(AnalyseReportType ReportType)
    : IRequest<List<ReportLayoutDto>>;

