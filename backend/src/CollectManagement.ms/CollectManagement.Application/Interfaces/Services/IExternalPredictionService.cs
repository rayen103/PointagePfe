using CollectManagement.Application.Contracts.Predictions;

namespace CollectManagement.Application.Interfaces.Services;

public interface IExternalPredictionService
{
    Task<DurationPredictionResponse> PredictDurationAsync(
        DurationPredictionRequest request,
        CancellationToken cancellationToken = default);

    Task<AbsenceRiskPredictionResponse> PredictAbsenceRiskAsync(
        AbsenceRiskPredictionRequest request,
        CancellationToken cancellationToken = default);

    Task<PredictionModelMetadataResponse> GetModelMetadataAsync(
        CancellationToken cancellationToken = default);
}
