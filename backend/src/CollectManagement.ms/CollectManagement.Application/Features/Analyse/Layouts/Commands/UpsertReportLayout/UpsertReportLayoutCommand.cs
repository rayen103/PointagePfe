using CollectManagement.Application.Features.Analyse.Contracts;
using CollectManagement.Domain.Analyse.Enums;

namespace CollectManagement.Application.Features.Analyse.Layouts.Commands.UpsertReportLayout;

public record UpsertReportLayoutCommand(
    Ulid? ReportLayoutId,
    AnalyseReportType ReportType,
    string Name,
    string ConfigJson,
    bool IsDefault) : IRequest<ReportLayoutDto>;

