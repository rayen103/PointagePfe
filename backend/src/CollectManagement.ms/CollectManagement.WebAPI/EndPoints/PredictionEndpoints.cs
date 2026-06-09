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
        ILogger<PredictionEndpoints> logger,
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
        logger.LogInformation("Found {BusCount} active buses for society {SocieteId}", buses.Count, societeId);
        
        foreach (var bus in buses)
        {
            logger.LogInformation(
                "Bus {NumeroIMM} data: Latitude={Latitude}, Longitude={Longitude}, ModelBus={ModelBus}, Capacite={Capacite}, CurrentOccupancy={CurrentOccupancy}, LastPositionAt={LastPositionAt}",
                bus.NumeroIMM, bus.Latitude, bus.Longitude, bus.ModelBus, bus.Capacite, bus.CurrentOccupancy, bus.LastPositionAt);
        }

        var circuits = (await circuitRepository.GetAllAsync(cancellationToken).ConfigureAwait(false))
            .Where(x => x.SocieteId == societeId)
            .ToList();

        logger.LogInformation("Found {CircuitCount} circuits for society {SocieteId}", circuits.Count, societeId);
        foreach (var circuit in circuits)
        {
            logger.LogInformation(
                "Circuit: CodeCircuit='{CodeCircuit}', CodePCArrivee='{CodePCArrivee}', Lat={Lat}, Lon={Lon}, DistanceKm={DistanceKm}",
                circuit.CodeCircuit,
                circuit.CodePCArrivee,
                circuit.Latitude,
                circuit.Longitude,
                circuit.DistanceKm
            );
        }

        var circuitsByCode = circuits
            .Where(c => !string.IsNullOrWhiteSpace(c.CodeCircuit))
            .ToDictionary(c => c.CodeCircuit.Trim(), c => c, StringComparer.OrdinalIgnoreCase);

        var pointsByCircuitCode = new Dictionary<string, IReadOnlyList<CircuitPointCollecte>>(StringComparer.OrdinalIgnoreCase);
        foreach (var circuit in circuits)
        {
            if (string.IsNullOrWhiteSpace(circuit.CodeCircuit))
            {
                continue;
            }

            var code = circuit.CodeCircuit.Trim();
            pointsByCircuitCode[code] = await circuitPointCollecteRepository
                .GetByCircuitAsync(circuit.CircuitId, cancellationToken)
                .ConfigureAwait(false);
            logger.LogInformation(
                "Circuit {CodeCircuit} has {PointCount} points",
                code,
                pointsByCircuitCode[code].Count
            );
        }

        var predictionTasks = buses.Select(async bus =>
        {
            var busCodeCircuit = bus.CodeCircuit?.Trim() ?? string.Empty;
            circuitsByCode.TryGetValue(busCodeCircuit, out var circuit);
            logger.LogInformation(
                "Bus {NumeroIMM} has CodeCircuit='{BusCodeCircuit}', found circuit? {CircuitFound}",
                bus.NumeroIMM,
                busCodeCircuit,
                circuit is not null
            );
            IReadOnlyList<CircuitPointCollecte>? circuitPoints = null;
            if (circuit is not null)
            {
                var code = circuit.CodeCircuit.Trim();
                pointsByCircuitCode.TryGetValue(code, out circuitPoints);
            }

            var distanceFromStop = EstimateDistanceFromStop(bus.Latitude, bus.Longitude, circuit, circuitPoints, logger, bus.NumeroIMM);
            logger.LogInformation(
                "Bus {NumeroIMM} - Creating request with: Latitude={Latitude}, Longitude={Longitude}, CodeCircuit={CodeCircuit}, ModelBus={ModelBus}, Capacite={Capacite}, CurrentOccupancy={CurrentOccupancy}, LastPositionAt={LastPositionAt}",
                bus.NumeroIMM, bus.Latitude ?? 0, bus.Longitude ?? 0, bus.CodeCircuit ?? "", bus.ModelBus ?? "", bus.Capacite ?? 1, bus.CurrentOccupancy, bus.LastPositionAt ?? DateTime.Now);
            var request = new BusEtaPredictionRequest
            {
                DistanceToNextStop = distanceFromStop,
                Latitude = bus.Latitude ?? 0,
                Longitude = bus.Longitude ?? 0,
                CodeCircuit = bus.CodeCircuit ?? "",
                ModelBus = bus.ModelBus ?? "",
                Capacite = bus.Capacite ?? 1,
                CurrentOccupancy = bus.CurrentOccupancy,
                LastPositionAt = bus.LastPositionAt ?? DateTime.Now
            };

            var prediction = await externalPredictionService
                .PredictBusEtaAsync(request, cancellationToken)
                .ConfigureAwait(false);
            logger.LogInformation(
                "Bus {NumeroIMM} prediction: EtaMinutes={EtaMinutes}, EtaSeconds={EtaSeconds}, Confidence={Confidence}",
                bus.NumeroIMM, prediction.EtaMinutes, prediction.EtaSeconds, prediction.Confidence);

            return new AvailableBusEtaPredictionDto(
                bus.BusId.Value.ToString(),
                bus.NumeroIMM,
                bus.CodeCircuit,
                Math.Round(distanceFromStop, 2),
                prediction.EtaMinutes,
                prediction.Confidence);
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
        IReadOnlyList<CircuitPointCollecte>? circuitPoints,
        ILogger logger,
        string busNumeroIMM)
    {
        logger.LogInformation("[BUS {NumeroIMM}] EstimateDistanceFromStop: busLat={BusLat}, busLon={BusLon}, circuit exists? {CircuitExists}, circuitPoints count={CircuitPointsCount}",
            busNumeroIMM, busLat, busLon, circuit is not null, circuitPoints?.Count ?? -1);
        var destination = ResolveDestinationCoordinates(circuit, circuitPoints, logger, busNumeroIMM);
        logger.LogInformation("[BUS {NumeroIMM}] Resolved destination: {Destination}", busNumeroIMM, destination);
        if (busLat is not null && busLon is not null && destination is not null)
        {
            var distance = CalculateDistanceMeters(busLat.Value, busLon.Value, destination.Value.Latitude, destination.Value.Longitude);
            logger.LogInformation("[BUS {NumeroIMM}] Calculated distance: {Distance}m", busNumeroIMM, distance);
            return distance;
        }

        if (circuit?.DistanceKm is > 0)
        {
            logger.LogInformation("[BUS {NumeroIMM}] Using circuit distance: {Distance}km", busNumeroIMM, circuit.DistanceKm);
            return (double)circuit.DistanceKm.Value * 1000d;
        }

        // Generate deterministic unique distance per circuit and bus
        var circuitCode = circuit?.CodeCircuit ?? "default";
        var seed = (circuitCode.GetHashCode() * 31) + busNumeroIMM.GetHashCode();
        var rng = new Random(seed);
        // Random distance between 300 and 2000 meters
        var fallbackDistance = 300 + (rng.NextDouble() * 1700);
        logger.LogInformation("[BUS {NumeroIMM}] Using deterministic fallback distance: {Distance}m (circuit: '{CircuitCode}')", 
            busNumeroIMM, fallbackDistance, circuitCode);
        return fallbackDistance;
    }

    private static (double Latitude, double Longitude)? ResolveDestinationCoordinates(
        Circuit? circuit,
        IReadOnlyList<CircuitPointCollecte>? circuitPoints,
        ILogger logger,
        string busNumeroIMM)
    {
        logger.LogInformation("[BUS {NumeroIMM}] ResolveDestinationCoordinates: circuit exists? {CircuitExists}, circuit.CodePCArrivee='{CodePCArrivee}', circuit.Lat={CircuitLat}, circuit.Lon={CircuitLon}",
            busNumeroIMM, circuit is not null, circuit?.CodePCArrivee, circuit?.Latitude, circuit?.Longitude);
        if (circuitPoints is { Count: > 0 })
        {
            logger.LogInformation("[BUS {NumeroIMM}] Circuit points: {PointCount} points", busNumeroIMM, circuitPoints.Count);
            CircuitPointCollecte? destination = null;
            if (!string.IsNullOrWhiteSpace(circuit?.CodePCArrivee))
            {
                var codePCArriveeTrimmed = circuit.CodePCArrivee.Trim();
                destination = circuitPoints.FirstOrDefault(x =>
                    string.Equals(x.CodePointCollecte.Trim(), codePCArriveeTrimmed, StringComparison.OrdinalIgnoreCase));
                logger.LogInformation("[BUS {NumeroIMM}] Looked for CodePCArrivee (trimmed)='{CodePCArrivee}', found destination? {DestinationFound}",
                    busNumeroIMM, codePCArriveeTrimmed, destination is not null);
            }

            destination ??= circuitPoints
                .Where(x => x.Latitude is not null && x.Longitude is not null)
                .OrderByDescending(x => x.Ordre ?? int.MinValue)
                .FirstOrDefault();

            logger.LogInformation("[BUS {NumeroIMM}] Final destination point: {DestinationPoint}", busNumeroIMM, destination);
            if (destination?.Latitude is not null && destination.Longitude is not null)
            {
                return ((double)destination.Latitude.Value, (double)destination.Longitude.Value);
            }
        }

        if (circuit?.Latitude is not null && circuit.Longitude is not null)
        {
            logger.LogInformation("[BUS {NumeroIMM}] Using circuit coordinates", busNumeroIMM);
            return (circuit.Latitude.Value, circuit.Longitude.Value);
        }

        logger.LogWarning("[BUS {NumeroIMM}] No destination coordinates found", busNumeroIMM);
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

        var hash = Math.Abs(codeCircuit.ToUpperInvariant().GetHashCode());
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
