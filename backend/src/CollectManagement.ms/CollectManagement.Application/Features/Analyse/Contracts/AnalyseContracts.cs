using CollectManagement.Domain.Analyse.Enums;

namespace CollectManagement.Application.Features.Analyse.Contracts;

public record AnalyseColumnDto(
    string Key,
    string Label,
    string DataType,
    bool IsNumeric);

public record AnalyseQueryRequest(
    DateTime? DateFrom,
    DateTime? DateTo,
    List<string> Fields);

public record AnalyseQueryResponse(
    List<AnalyseColumnDto> Columns,
    List<Dictionary<string, object?>> Rows,
    Dictionary<string, decimal> Totals);

public record ReportLayoutDto(
    Ulid ReportLayoutId,
    AnalyseReportType ReportType,
    string Name,
    string ConfigJson,
    bool IsDefault);

