using System.ComponentModel.DataAnnotations;
using Carter;
using CollectManagement.Application.Common;
using CollectManagement.Application.Contracts.Predictions;
using CollectManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CollectManagement.WebAPI.EndPoints;

public sealed class PredictionEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var routeGroupBuilder = app.MapGroup("cm/prediction").RequireAuthorization();

        routeGroupBuilder.MapPost("duration", PredictDuration);
        routeGroupBuilder.MapPost("duration/batch", PredictDurationBatch);
        routeGroupBuilder.MapPost("absence-risk", PredictAbsenceRisk);
        routeGroupBuilder.MapPost("absence-risk/batch", PredictAbsenceRiskBatch);
        routeGroupBuilder.MapGet("metadata", GetMetadata);
    }

    private static async Task<IResult> PredictDurationBatch(
        [FromBody] [Required] DurationBatchPredictionRequest request,
        IExternalPredictionService externalPredictionService,
        CancellationToken cancellationToken)
    {
        var prediction = await externalPredictionService
            .PredictDurationBatchAsync(request, cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<DurationBatchPredictionResponse>(prediction));
    }

    private static async Task<IResult> PredictDuration(
        [FromBody] [Required] DurationPredictionRequest request,
        IExternalPredictionService externalPredictionService,
        CancellationToken cancellationToken)
    {
        var prediction = await externalPredictionService
            .PredictDurationAsync(request, cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<DurationPredictionResponse>(prediction));
    }

    private static async Task<IResult> PredictAbsenceRisk(
        [FromBody] [Required] AbsenceRiskPredictionRequest request,
        IExternalPredictionService externalPredictionService,
        CancellationToken cancellationToken)
    {
        var prediction = await externalPredictionService
            .PredictAbsenceRiskAsync(request, cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<AbsenceRiskPredictionResponse>(prediction));
    }

    private static async Task<IResult> PredictAbsenceRiskBatch(
        [FromBody] [Required] AbsenceRiskBatchPredictionRequest request,
        IExternalPredictionService externalPredictionService,
        CancellationToken cancellationToken)
    {
        var prediction = await externalPredictionService
            .PredictAbsenceRiskBatchAsync(request, cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<AbsenceRiskBatchPredictionResponse>(prediction));
    }

    private static async Task<IResult> GetMetadata(
        IExternalPredictionService externalPredictionService,
        CancellationToken cancellationToken)
    {
        var metadata = await externalPredictionService
            .GetModelMetadataAsync(cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<PredictionModelMetadataResponse>(metadata));
    }
}
