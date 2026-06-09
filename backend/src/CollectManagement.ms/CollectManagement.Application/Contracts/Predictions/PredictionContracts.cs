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

// Bus ETA Prediction Contracts
public sealed record BusEtaPredictionRequest
{
    // Legacy compatibility fields
    public double? DistanceFromStop { get; init; }
    public double? LogDistance { get; init; }
    public int? DistanceOver300m { get; init; }
    public int? Hour { get; init; }
    public double? HourSin { get; init; }
    public double? HourCos { get; init; }
    public int? IsRushHour { get; init; }
    public int? DayOfWeek { get; init; }
    public double? DirectionRef { get; init; }
    public int? IsWeekend { get; init; }
    
    // New raw database fields
    public double? Latitude { get; init; }
    public double? Longitude { get; init; }
    public string? CodeCircuit { get; init; }
    public string? ModelBus { get; init; }
    public double? Capacite { get; init; }
    public double? CurrentOccupancy { get; init; }
    public DateTime? LastPositionAt { get; init; }
}

public sealed record BusEtaPredictionResponse(
    double EtaMinutes,
    int EtaSeconds,
    double Confidence,
    bool UsedFallbackStop
);

public sealed record AvailableBusEtaPredictionDto(
    string BusId,
    string NumeroIMM,
    string? CodeCircuit,
    double DistanceFromStop,
    double EtaMinutes,
    double Confidence);

public sealed record AvailableBusEtaPredictionResponse(
    IReadOnlyList<AvailableBusEtaPredictionDto> Predictions);
