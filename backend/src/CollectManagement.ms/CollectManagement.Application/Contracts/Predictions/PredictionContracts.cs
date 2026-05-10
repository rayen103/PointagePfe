namespace CollectManagement.Application.Contracts.Predictions;

public sealed record DurationPredictionRequest(
    string? NumeroChantier,
    string? CodeShift,
    string? CodeRattachement,
    string? TypeEmploye,
    string? WorkOrderType);

public sealed record DurationPredictionResponse(
    double PredictedDurationHours,
    double Confidence,
    string Source,
    string ModelVersion);

public sealed record DurationBatchPredictionRequest(
    IReadOnlyList<DurationPredictionRequest> Items);

public sealed record DurationBatchPredictionResponse(
    IReadOnlyList<DurationPredictionResponse> Predictions);

public sealed record AbsenceRiskPredictionRequest(
    string? EmployeId,
    string? TypeEmploye,
    string? CodeShift,
    string? CodeRattachement,
    string? NumeroChantier);

public sealed record AbsenceRiskPredictionResponse(
    double RiskScore,
    string RiskLevel,
    double Confidence,
    string Source,
    string ModelVersion);

public sealed record AbsenceRiskBatchPredictionRequest(
    IReadOnlyList<AbsenceRiskPredictionRequest> Items);

public sealed record AbsenceRiskBatchPredictionResponse(
    IReadOnlyList<AbsenceRiskPredictionResponse> Predictions);

public sealed record PredictionModelMetadataResponse(
    string ModelVersion,
    string DurationDatasetSource,
    string AbsenceDatasetSource,
    DateTime TrainedAtUtc,
    int DurationSampleCount,
    int AbsenceSampleCount,
    double DurationMaeEstimate,
    double AbsenceAucEstimate,
    bool UsesExternalDataset,
    bool HybridTrainingReady,
    IReadOnlyList<string> GovernanceChecks);
