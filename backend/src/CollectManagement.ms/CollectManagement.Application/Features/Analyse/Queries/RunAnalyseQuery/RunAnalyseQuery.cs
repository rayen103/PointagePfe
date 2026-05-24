using CollectManagement.Application.Features.Analyse.Contracts;
using CollectManagement.Domain.Analyse.Enums;

namespace CollectManagement.Application.Features.Analyse.Queries.RunAnalyseQuery;

public record RunAnalyseQuery(
    AnalyseReportType ReportType,
    AnalyseQueryRequest Request) : IRequest<AnalyseQueryResponse>;

