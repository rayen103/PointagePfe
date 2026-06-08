using System.Globalization;
using System.ComponentModel.DataAnnotations;
using Carter;
using CollectManagement.Application.Common;
using CollectManagement.Application.Contracts.Predictions;
using CollectManagement.Application.Interfaces.Repositories.Bus;
using CollectManagement.Application.Interfaces.Repositories.Circuits;
using CollectManagement.Application.Interfaces.Repositories.CircuitsPointsCollecte;
using CollectManagement.Application.Interfaces.Repositories.Utilisateurs;
using CollectManagement.Application.Interfaces.Services;
using CollectManagement.Domain.Circuits;
using CollectManagement.Domain.Societes.ValueObjects;
using CollectManagement.Domain.Utilisateurs.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using CollectManagement.WebAPI.Authorization;

namespace CollectManagement.WebAPI.EndPoints;

public sealed class PredictionEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var routeGroupBuilder = app.MapGroup("cm/prediction").RequireNavigationPermission("fichier.bus");

        routeGroupBuilder.MapPost("duration", PredictDuration);
        routeGroupBuilder.MapPost("duration/batch", PredictDurationBatch);
        routeGroupBuilder.MapPost("absence-risk", PredictAbsenceRisk);
        routeGroupBuilder.MapPost("absence-risk/batch", PredictAbsenceRiskBatch);
        routeGroupBuilder.MapGet("metadata", GetMetadata);
        routeGroupBuilder.MapPost("bus-eta", PredictBusEta);
        routeGroupBuilder.MapGet("bus-eta/available", PredictAvailableBusesEta);
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

    private static async Task<IResult> PredictBusEta(
        [FromBody] [Required] BusEtaPredictionRequest request,
        IExternalPredictionService externalPredictionService,
        CancellationToken cancellationToken)
    {
        var prediction = await externalPredictionService
            .PredictBusEtaAsync(request, cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<BusEtaPredictionResponse>(prediction));
    }

    private static async Task<IResult> PredictAvailableBusesEta(
        IExternalPredictionService externalPredictionService,
        IBusRepository busRepository,
        ICircuitRepository circuitRepository,
        ICircuitPointCollecteRepository circuitPointCollecteRepository,
        ILoggedInUserService loggedInUserService,
        IUtilisateurRepository utilisateurRepository,
        CancellationToken cancellationToken)
    {
        var societeId = await ResolveSocieteId(loggedInUserService, utilisateurRepository, cancellationToken).ConfigureAwait(false);
        var now = DateTime.Now;
        var hour = now.Hour;
        var dayOfWeek = (int)now.DayOfWeek;
        var isWeekend = dayOfWeek is 0 or 6 ? 1 : 0;
        var isRushHour = IsRushHour(hour) ? 1 : 0;

        var buses = (await busRepository.GetAllAsync(cancellationToken).ConfigureAwait(false))
            .Where(x => x.SocieteId == societeId && x.IsActive)
            .ToList();

        var circuits = (await circuitRepository.GetAllAsync(cancellationToken).ConfigureAwait(false))
            .Where(x => x.SocieteId == societeId)
            .ToList();

        var circuitsByCode = circuits
            .Where(c => !string.IsNullOrWhiteSpace(c.CodeCircuit))
            .ToDictionary(c => c.CodeCircuit, c => c, StringComparer.OrdinalIgnoreCase);

        var pointsByCircuitCode = new Dictionary<string, IReadOnlyList<CircuitPointCollecte>>(StringComparer.OrdinalIgnoreCase);
        foreach (var circuit in circuits)
        {
            pointsByCircuitCode[circuit.CodeCircuit] = await circuitPointCollecteRepository
                .GetByCircuitAsync(circuit.CircuitId, cancellationToken)
                .ConfigureAwait(false);
        }

        var predictionTasks = buses.Select(async bus =>
        {
            circuitsByCode.TryGetValue(bus.CodeCircuit ?? string.Empty, out var circuit);
            pointsByCircuitCode.TryGetValue(circuit?.CodeCircuit ?? string.Empty, out var circuitPoints);

            var distanceFromStop = EstimateDistanceFromStop(bus.Latitude, bus.Longitude, circuit, circuitPoints);
            var directionRef = DeriveDirectionRef(bus.CodeCircuit);

            var request = new BusEtaPredictionRequest(
                DistanceFromStop: distanceFromStop,
                log_distance: Math.Log(Math.Max(1, distanceFromStop)),
                distance_over_300m: distanceFromStop > 300 ? 1 : 0,
                hour: hour,
                hour_sin: null,
                hour_cos: null,
                is_rush_hour: isRushHour,
                day_of_week: dayOfWeek,
                DirectionRef: directionRef,
                is_weekend: isWeekend);

            var prediction = await externalPredictionService
                .PredictBusEtaAsync(request, cancellationToken)
                .ConfigureAwait(false);

            return new AvailableBusEtaPredictionDto(
                bus.BusId.Value.ToString(),
                bus.NumeroIMM,
                bus.CodeCircuit,
                Math.Round(distanceFromStop, 2),
                prediction.eta_minutes,
                prediction.confidence);
        });

        var predictions = await Task.WhenAll(predictionTasks).ConfigureAwait(false);
        var ordered = predictions
            .OrderBy(x => x.NumeroIMM, StringComparer.OrdinalIgnoreCase)
            .ToList();

        return Results.Ok(new ApiResponse<AvailableBusEtaPredictionResponse>(
            new AvailableBusEtaPredictionResponse(ordered)));
    }

    private static async Task<SocieteId> ResolveSocieteId(
        ILoggedInUserService loggedInUserService,
        IUtilisateurRepository utilisateurRepository,
        CancellationToken cancellationToken)
    {
        if (!Ulid.TryParse(loggedInUserService.UserId, out var userId))
        {
            return new SocieteId(Ulid.Empty);
        }

        var utilisateur = await utilisateurRepository
            .GetOneAsync(new UtilisateurId(userId), cancellationToken)
            .ConfigureAwait(false);

        return utilisateur?.SocieteId ?? new SocieteId(Ulid.Empty);
    }

    private static bool IsRushHour(int hour)
        => (hour >= 7 && hour <= 9) || (hour >= 17 && hour <= 19);

    private static double EstimateDistanceFromStop(
        double? busLat,
        double? busLon,
        Circuit? circuit,
        IReadOnlyList<CircuitPointCollecte>? circuitPoints)
    {
        var destination = ResolveDestinationCoordinates(circuit, circuitPoints);
        if (busLat is not null && busLon is not null && destination is not null)
        {
            return CalculateDistanceMeters(busLat.Value, busLon.Value, destination.Value.Latitude, destination.Value.Longitude);
        }

        if (circuit?.DistanceKm is > 0)
        {
            return (double)circuit.DistanceKm.Value * 1000d;
        }

        return 500d;
    }

    private static (double Latitude, double Longitude)? ResolveDestinationCoordinates(
        Circuit? circuit,
        IReadOnlyList<CircuitPointCollecte>? circuitPoints)
    {
        if (circuitPoints is { Count: > 0 })
        {
            CircuitPointCollecte? destination = null;
            if (!string.IsNullOrWhiteSpace(circuit?.CodePCArrivee))
            {
                destination = circuitPoints.FirstOrDefault(x =>
                    string.Equals(x.CodePointCollecte, circuit.CodePCArrivee, StringComparison.OrdinalIgnoreCase));
            }

            destination ??= circuitPoints
                .Where(x => x.Latitude is not null && x.Longitude is not null)
                .OrderByDescending(x => x.Ordre ?? int.MinValue)
                .FirstOrDefault();

            if (destination?.Latitude is not null && destination.Longitude is not null)
            {
                return ((double)destination.Latitude.Value, (double)destination.Longitude.Value);
            }
        }

        if (circuit?.Latitude is not null && circuit.Longitude is not null)
        {
            return (circuit.Latitude.Value, circuit.Longitude.Value);
        }

        return null;
    }

    private static double DeriveDirectionRef(string? codeCircuit)
    {
        if (string.IsNullOrWhiteSpace(codeCircuit))
        {
            return 1d;
        }

        if (double.TryParse(
                codeCircuit,
                NumberStyles.Float | NumberStyles.AllowThousands,
                CultureInfo.InvariantCulture,
                out var numericDirection))
        {
            return numericDirection;
        }

        var hash = Math.Abs(codeCircuit.GetHashCode(StringComparison.OrdinalIgnoreCase));
        return (hash % 360) + 1;
    }

    private static double CalculateDistanceMeters(
        double lat1,
        double lon1,
        double lat2,
        double lon2)
    {
        const double earthRadiusMeters = 6371000d;
        var dLat = DegreesToRadians(lat2 - lat1);
        var dLon = DegreesToRadians(lon2 - lon1);
        var a = Math.Pow(Math.Sin(dLat / 2), 2) +
                Math.Cos(DegreesToRadians(lat1)) *
                Math.Cos(DegreesToRadians(lat2)) *
                Math.Pow(Math.Sin(dLon / 2), 2);
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return earthRadiusMeters * c;
    }

    private static double DegreesToRadians(double value)
        => value * (Math.PI / 180d);
}
