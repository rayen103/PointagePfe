using CollectManagement.Domain.Analyse.Enums;
using CollectManagement.Domain.Analyse.ValueObjects;
using CollectManagement.Domain.Common;
using CollectManagement.Domain.Societes;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Domain.Analyse;

public class ReportLayout : AuditableEntity
{
    public ReportLayoutId ReportLayoutId { get; private set; }
    public AnalyseReportType ReportType { get; private set; }
    public string Name { get; private set; }
    public string ConfigJson { get; private set; }
    public bool IsDefault { get; private set; }
    public SocieteId SocieteId { get; private set; }
    public Societe? Societe { get; private set; }

    private ReportLayout(
        ReportLayoutId reportLayoutId,
        AnalyseReportType reportType,
        string name,
        string configJson,
        bool isDefault,
        SocieteId societeId)
    {
        ReportLayoutId = reportLayoutId;
        ReportType = reportType;
        Name = name;
        ConfigJson = configJson;
        IsDefault = isDefault;
        SocieteId = societeId;
    }

    public static ReportLayout Create(
        ReportLayoutId reportLayoutId,
        AnalyseReportType reportType,
        string name,
        string configJson,
        bool isDefault,
        SocieteId societeId)
    {
        return new ReportLayout(
            reportLayoutId,
            reportType,
            name,
            configJson,
            isDefault,
            societeId);
    }

    public void Update(
        string name,
        string configJson,
        bool isDefault)
    {
        Name = name;
        ConfigJson = configJson;
        IsDefault = isDefault;
    }

#pragma warning disable CS8618
    private ReportLayout() { }
#pragma warning restore CS8618
}

