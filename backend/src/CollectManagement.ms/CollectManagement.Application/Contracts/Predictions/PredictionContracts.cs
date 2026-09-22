using System.Text.Json.Serialization;

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
    [JsonPropertyName("distance_from_stop")]
    public double? DistanceFromStop { get; init; }

    [JsonPropertyName("log_distance")]
    public double? LogDistance { get; init; }

    [JsonPropertyName("distance_over_300m")]
    public int? DistanceOver300m { get; init; }

    [JsonPropertyName("hour")]
    public int? Hour { get; init; }

    [JsonPropertyName("hour_sin")]
    public double? HourSin { get; init; }

    [JsonPropertyName("hour_cos")]
    public double? HourCos { get; init; }

    [JsonPropertyName("is_rush_hour")]
    public int? IsRushHour { get; init; }

    [JsonPropertyName("day_of_week")]
    public int? DayOfWeek { get; init; }

    [JsonPropertyName("direction_ref")]
    public double? DirectionRef { get; init; }

    [JsonPropertyName("is_weekend")]
    public int? IsWeekend { get; init; }
    
    // New raw database fields
    [JsonPropertyName("distance_to_next_stop")]
    public double? DistanceToNextStop { get; init; }

    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }

    [JsonPropertyName("code_circuit")]
    public string? CodeCircuit { get; init; }

    [JsonPropertyName("model_bus")]
    public string? ModelBus { get; init; }

    [JsonPropertyName("capacite")]
    public double? Capacite { get; init; }

    [JsonPropertyName("current_occupancy")]
    public double? CurrentOccupancy { get; init; }

    [JsonPropertyName("last_position_at")]
    public DateTime? LastPositionAt { get; init; }
}

public sealed record BusEtaPredictionResponse(
    [property: JsonPropertyName("eta_minutes")] double EtaMinutes,
    [property: JsonPropertyName("eta_seconds")] int EtaSeconds,
    [property: JsonPropertyName("confidence")] double Confidence,
    [property: JsonPropertyName("used_fallback_stop")] bool UsedFallbackStop
);

public sealed record AvailableBusEtaPredictionDto(
    string BusId,
    string NumeroIMM,
    string? CodeCircuit,
    double DistanceFromStop,
    double EtaMinutes,
    double Confidence,
    string? StopName = null);

public sealed record AvailableBusEtaPredictionResponse(
    IReadOnlyList<AvailableBusEtaPredictionDto> Predictions);
