using System.ComponentModel.DataAnnotations;
using Carter;
using CollectManagement.Application.Common;
using CollectManagement.Application.Features.Bus.Commands.CreateBus;
using CollectManagement.Application.Features.Bus.Commands.DeleteBus;
using CollectManagement.Application.Features.Bus.Commands.UpdateBus;
using CollectManagement.Application.Interfaces.Repositories;
using CollectManagement.Application.Interfaces.Repositories.Bus;
using CollectManagement.Application.Interfaces.Repositories.Chauffeurs;
using CollectManagement.Application.Interfaces.Repositories.Circuits;
using CollectManagement.Application.Interfaces.Repositories.CircuitsPointsCollecte;
using CollectManagement.Application.Interfaces.Repositories.PointsCollecte;
using CollectManagement.Application.Shared;
using CollectManagement.Application.Features.Bus.Queries.GetOneBus;
using CollectManagement.Application.Features.Bus.Queries.GetPagedListBus;
using CollectManagement.Domain.Bus;
using CollectManagement.Domain.Bus.ValueObjects;
using CollectManagement.Domain.PointsCollecte;
using CollectManagement.Domain.PointsCollecte.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CollectManagement.WebAPI.Authorization;

namespace CollectManagement.WebAPI.EndPoints;

public class BusEndpoints : ICarterModule
{
    private const double GeofenceRadiusToleranceMeters = 250d;
    private const int EventTimestampToleranceMinutes = 15;

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var routeGroupBuilder = app.MapGroup("cm/bus").RequireNavigationPermission("fichier.bus");

        routeGroupBuilder.MapGet("list", BusList);
        routeGroupBuilder.MapPost("add", CreateBus);
        routeGroupBuilder.MapPatch("update", UpdateBus);
        routeGroupBuilder.MapPost("{id}/delete", DeleteBus);
        routeGroupBuilder.MapGet("{id}/one", OneBus);
        routeGroupBuilder.MapPost("runtime/position", UpdateRuntimePosition);
        routeGroupBuilder.MapGet("runtime/positions/stream", StreamLivePositions);
        routeGroupBuilder.MapPost("{id}/vider", EmptyBus);
        routeGroupBuilder.MapGet("{id}/events", GetBusEvents);
    }

    public static async Task<IResult> CreateBus(
        [FromBody] [Required] CreateBusCommand createBusCommand,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var createResponse = await sender
            .Send(createBusCommand, cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<CreateBusResponse>(createResponse));
    }

    private static async Task<IResult> BusList(
        [FromQuery] string? search,
        [FromQuery] string? sort,
        [FromQuery] string? order,
        int page,
        int size,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var list = await sender
            .Send(new GetPagedListBusQuery(search, sort, order, page, size), cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<GetPagedListBusResponse>(list));
    }

    private static async Task<IResult> UpdateBus(
        [FromBody] [Required] UpdateBusCommand updateBusCommand,
        ISender sender,
        CancellationToken cancellationToken)
    {
        await sender
            .Send(updateBusCommand, cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<bool>(true));
    }

    private static async Task<IResult> DeleteBus(
        [FromRoute] [Required] Ulid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        await sender
            .Send(new DeleteBusCommand(id), cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<bool>(true));
    }

    public static async Task<IResult> OneBus(
        [Required] Ulid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var response = await sender
            .Send(new GetOneBusQuery(id), cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<GetOneBusDto>(response));
    }

    private static async Task<IResult> UpdateRuntimePosition(
        [FromBody] [Required] UpdateBusRuntimePositionRequest request,
        IBusRepository busRepository,
        IChauffeurRepository chauffeurRepository,
        ICircuitRepository circuitRepository,
        ICircuitPointCollecteRepository circuitPointCollecteRepository,
        IPointCollecteRepository pointCollecteRepository,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        if (request.TimestampUtc.HasValue)
        {
            var now = DateTime.UtcNow;
            var timestampUtc = request.TimestampUtc.Value.ToUniversalTime();
            var skew = (now - timestampUtc).Duration();

            if (skew > TimeSpan.FromMinutes(EventTimestampToleranceMinutes))
            {
                return Results.BadRequest(new ApiResponse<string>(
                    $"TimestampUtc exceeds tolerance of {EventTimestampToleranceMinutes} minutes.",
                    false,
                    StatusCodes.Status400BadRequest));
            }
        }

        var normalizedImei = request.IMEI.Trim();
        var bus = await busRepository
            .GetAsync(
                x => x.IMEI != null &&
                     x.IMEI.ToLower() == normalizedImei.ToLower(),
                cancellationToken)
            .ConfigureAwait(false);

        if (bus is null)
            return Results.NotFound(new ApiResponse<string>($"No bus mapped to IMEI '{request.IMEI}'.", false, StatusCodes.Status404NotFound));

        var rfidError = await ValidateRfidAsync(
                bus,
                request.RFIDChauffeur,
                chauffeurRepository,
                cancellationToken)
            .ConfigureAwait(false);

        if (rfidError is not null)
            return Results.BadRequest(new ApiResponse<string>(rfidError, false, StatusCodes.Status400BadRequest));

        var occurredAtUtc = request.TimestampUtc?.ToUniversalTime() ?? DateTime.UtcNow;
        bus.UpdateRuntimeState(
            normalizedImei,
            request.Latitude,
            request.Longitude,
            request.Occupancy,
            request.TimestampUtc,
            request.BatteryPercentage,
            request.BatteryVoltage,
            occurredAtUtc);

        await busRepository
            .UpdateBulkAsync(bus, cancellationToken)
            .ConfigureAwait(false);

        var outOfRadiusResult = await HandleOutOfRadiusScanAsync(
                bus,
                request.Latitude,
                request.Longitude,
                occurredAtUtc,
                circuitRepository,
                circuitPointCollecteRepository,
                pointCollecteRepository,
                cancellationToken)
            .ConfigureAwait(false);

        await AppendRuntimeEventAsync(
                unitOfWork.GetRepository<BusRuntimeEvent>(),
                bus.BusId,
                "PositionUpdated",
                "Bus runtime position and occupancy updated.",
                normalizedImei,
                bus.Latitude,
                bus.Longitude,
                bus.CurrentOccupancy,
                occurredAtUtc,
                cancellationToken)
            .ConfigureAwait(false);

        if (outOfRadiusResult.IsOutOfRadius)
        {
            await AppendRuntimeEventAsync(
                    unitOfWork.GetRepository<BusRuntimeEvent>(),
                    bus.BusId,
                    "OutOfRadiusScan",
                    outOfRadiusResult.Description,
                    normalizedImei,
                    bus.Latitude,
                    bus.Longitude,
                    bus.CurrentOccupancy,
                    occurredAtUtc,
                    cancellationToken)
                .ConfigureAwait(false);
        }

        if (outOfRadiusResult.AutoGeneratedPointCode is not null)
        {
            await AppendRuntimeEventAsync(
                    unitOfWork.GetRepository<BusRuntimeEvent>(),
                    bus.BusId,
                    "AutoPointCollecteGenerated",
                    $"Auto-generated point collecte '{outOfRadiusResult.AutoGeneratedPointCode}' due to out-of-radius scan.",
                    normalizedImei,
                    bus.Latitude,
                    bus.Longitude,
                    bus.CurrentOccupancy,
                    occurredAtUtc,
                    cancellationToken)
                .ConfigureAwait(false);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return Results.Ok(new ApiResponse<BusRuntimeStateDto>(new BusRuntimeStateDto(
            bus.BusId.Value,
            bus.NumeroIMM,
            bus.IMEI,
            bus.Latitude,
            bus.Longitude,
            bus.CurrentOccupancy,
            bus.LastPositionAt,
            bus.LastOccupancyUpdateAt
        )));
    }

    private static async Task<IResult> StreamLivePositions(
        IBusRepository busRepository,
        CancellationToken cancellationToken)
    {
        var buses = await busRepository
            .GetAllAsync(cancellationToken)
            .ConfigureAwait(false);

        var snapshot = new BusLivePositionSnapshotDto(
            DateTime.UtcNow,
            buses
                .Where(bus => bus.IsActive)
                .Select(bus => new BusLivePositionDto(
                    bus.BusId.Value,
                    bus.NumeroIMM,
                    bus.IMEI,
                    bus.Latitude,
                    bus.Longitude,
                    bus.CurrentOccupancy,
                    bus.LastPositionAt))
                .ToList());

        return Results.Ok(new ApiResponse<BusLivePositionSnapshotDto>(snapshot));
    }

    private static async Task<IResult> EmptyBus(
        [FromRoute] [Required] Ulid id,
        IBusRepository busRepository,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var busId = new BusId(id);
        var bus = await busRepository
            .GetAsync(x => x.BusId == busId, cancellationToken)
            .ConfigureAwait(false);

        if (bus is null)
            return Results.NotFound(new ApiResponse<string>("Bus not found.", false, StatusCodes.Status404NotFound));

        var occurredAtUtc = DateTime.UtcNow;
        bus.Empty(occurredAtUtc);

        await busRepository
            .UpdateBulkAsync(bus, cancellationToken)
            .ConfigureAwait(false);

        await AppendRuntimeEventAsync(
                unitOfWork.GetRepository<BusRuntimeEvent>(),
                bus.BusId,
                "BusEmptied",
                "Vider le Bus action executed.",
                bus.IMEI,
                bus.Latitude,
                bus.Longitude,
                bus.CurrentOccupancy,
                occurredAtUtc,
                cancellationToken)
            .ConfigureAwait(false);

        await unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return Results.Ok(new ApiResponse<BusRuntimeStateDto>(new BusRuntimeStateDto(
            bus.BusId.Value,
            bus.NumeroIMM,
            bus.IMEI,
            bus.Latitude,
            bus.Longitude,
            bus.CurrentOccupancy,
            bus.LastPositionAt,
            bus.LastOccupancyUpdateAt
        )));
    }

    private static async Task<IResult> GetBusEvents(
        [FromRoute] [Required] Ulid id,
        IRepositoryBase<BusRuntimeEvent> runtimeEventRepository,
        CancellationToken cancellationToken)
    {
        var busId = new BusId(id);
        var events = await runtimeEventRepository
            .GetManyAsync(x => x.BusId == busId, cancellationToken)
            .ConfigureAwait(false);

        var data = events
            .OrderByDescending(x => x.OccurredAtUtc)
            .Select(x => new BusRuntimeEventDto(
                x.BusRuntimeEventId.Value,
                x.BusId.Value,
                x.EventType,
                x.Description,
                x.IMEI,
                x.Latitude,
                x.Longitude,
                x.Occupancy,
                x.OccurredAtUtc))
            .ToList();

        return Results.Ok(new ApiResponse<IReadOnlyList<BusRuntimeEventDto>>(data));
    }

    private static async Task AppendRuntimeEventAsync(
        IRepositoryBase<BusRuntimeEvent> repository,
        BusId busId,
        string eventType,
        string description,
        string? imei,
        double? latitude,
        double? longitude,
        int? occupancy,
        DateTime occurredAtUtc,
        CancellationToken cancellationToken)
    {
        var runtimeEvent = BusRuntimeEvent.Create(
            new BusRuntimeEventId(Ulid.NewUlid()),
            busId,
            eventType,
            description,
            imei,
            latitude,
            longitude,
            occupancy,
            occurredAtUtc);

        await repository.AddAsync(runtimeEvent, cancellationToken).ConfigureAwait(false);
    }

    private static async Task<string?> ValidateRfidAsync(
        Domain.Bus.Bus bus,
        string? requestRfid,
        IChauffeurRepository chauffeurRepository,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(bus.CodeChauffeur))
            return null;

        var normalizedCodeChauffeur = bus.CodeChauffeur.Trim();
        var chauffeur = await chauffeurRepository
            .GetAsync(
                x => x.CodeChauffeur.ToLower() == normalizedCodeChauffeur.ToLower(),
                cancellationToken)
            .ConfigureAwait(false);

        if (chauffeur is null || !chauffeur.IsActive)
            return $"No active chauffeur found for code '{normalizedCodeChauffeur}'.";

        if (string.IsNullOrWhiteSpace(chauffeur.RFIDChauffeur))
            return null;

        if (string.IsNullOrWhiteSpace(requestRfid))
            return "RFIDChauffeur is required for this bus runtime scan.";

        var normalizedRfid = requestRfid.Trim();
        if (!string.Equals(chauffeur.RFIDChauffeur.Trim(), normalizedRfid, StringComparison.OrdinalIgnoreCase))
            return "RFIDChauffeur does not match the assigned chauffeur.";

        return null;
    }

    private static async Task<GeofenceDecision> HandleOutOfRadiusScanAsync(
        Domain.Bus.Bus bus,
        double? latitude,
        double? longitude,
        DateTime occurredAtUtc,
        ICircuitRepository circuitRepository,
        ICircuitPointCollecteRepository circuitPointCollecteRepository,
        IPointCollecteRepository pointCollecteRepository,
        CancellationToken cancellationToken)
    {
        if (latitude is null || longitude is null || string.IsNullOrWhiteSpace(bus.CodeCircuit))
            return GeofenceDecision.InRadius();

        var normalizedCodeCircuit = bus.CodeCircuit.Trim();
        var circuit = await circuitRepository
            .GetAsync(
                x => x.CodeCircuit.ToLower() == normalizedCodeCircuit.ToLower(),
                cancellationToken)
            .ConfigureAwait(false);

        if (circuit is null)
            return GeofenceDecision.InRadius();

        var circuitPoints = await circuitPointCollecteRepository
            .GetByCircuitAsync(circuit.CircuitId, cancellationToken)
            .ConfigureAwait(false);

        var pointsWithCoords = circuitPoints
            .Where(x => x.Latitude is not null && x.Longitude is not null)
            .ToList();

        if (pointsWithCoords.Count == 0)
            return GeofenceDecision.InRadius();

        var nearest = pointsWithCoords
            .Select(x => new
            {
                x.CodePointCollecte,
                DistanceMeters = CalculateDistanceMeters(
                    latitude.Value,
                    longitude.Value,
                    Convert.ToDouble(x.Latitude!.Value),
                    Convert.ToDouble(x.Longitude!.Value))
            })
            .OrderBy(x => x.DistanceMeters)
            .First();

        if (nearest.DistanceMeters <= GeofenceRadiusToleranceMeters)
            return GeofenceDecision.InRadius();

        var autoPointCode = await CreateAutoPointCollecteAsync(
                bus,
                latitude.Value,
                longitude.Value,
                occurredAtUtc,
                pointCollecteRepository,
                cancellationToken)
            .ConfigureAwait(false);

        var description =
            $"Scan is out of radius: {nearest.DistanceMeters:F2}m from nearest point '{nearest.CodePointCollecte}' (tolerance {GeofenceRadiusToleranceMeters:F0}m).";

        return GeofenceDecision.OutOfRadius(description, autoPointCode);
    }

    private static async Task<string> CreateAutoPointCollecteAsync(
        Domain.Bus.Bus bus,
        double latitude,
        double longitude,
        DateTime occurredAtUtc,
        IPointCollecteRepository pointCollecteRepository,
        CancellationToken cancellationToken)
    {
        var generatedCode = BuildAutoPointCollecteCode(occurredAtUtc);
        var label = $"Point Auto - {bus.NumeroIMM} ({occurredAtUtc:HH:mm:ss})";
        if (label.Length > 200)
            label = label[..200];

        var pointCollecte = PointCollecte.Create(
            new PointCollecteId(Ulid.NewUlid()),
            generatedCode,
            label,
            Convert.ToDecimal(latitude),
            Convert.ToDecimal(longitude),
            null,
            null,
            true,
            bus.SocieteId);

        await pointCollecteRepository.AddAsync(pointCollecte, cancellationToken).ConfigureAwait(false);
        return generatedCode;
    }

    private static string BuildAutoPointCollecteCode(DateTime occurredAtUtc)
    {
        var code = $"PC-AUTO-{occurredAtUtc:yyyyMMddHHmmss}";
        return code.Length <= 50 ? code : code[..50];
    }

    private static double CalculateDistanceMeters(
        double latitude1,
        double longitude1,
        double latitude2,
        double longitude2)
    {
        const double earthRadiusMeters = 6371000d;
        var lat1Rad = DegreesToRadians(latitude1);
        var lat2Rad = DegreesToRadians(latitude2);
        var deltaLat = DegreesToRadians(latitude2 - latitude1);
        var deltaLon = DegreesToRadians(longitude2 - longitude1);

        var a = Math.Sin(deltaLat / 2d) * Math.Sin(deltaLat / 2d) +
                Math.Cos(lat1Rad) * Math.Cos(lat2Rad) *
                Math.Sin(deltaLon / 2d) * Math.Sin(deltaLon / 2d);
        var c = 2d * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1d - a));
        return earthRadiusMeters * c;
    }

    private static double DegreesToRadians(double value) => value * (Math.PI / 180d);

    public record UpdateBusRuntimePositionRequest(
        string IMEI,
        double? Latitude,
        double? Longitude,
        int? Occupancy,
        DateTime? TimestampUtc,
        string? RFIDChauffeur,
        int? BatteryPercentage = null,
        double? BatteryVoltage = null);

    public record BusRuntimeStateDto(
        Ulid BusId,
        string NumeroIMM,
        string? IMEI,
        double? Latitude,
        double? Longitude,
        int CurrentOccupancy,
        DateTime? LastPositionAt,
        DateTime? LastOccupancyUpdateAt);

    public record BusLivePositionDto(
        Ulid BusId,
        string NumeroIMM,
        string? IMEI,
        double? Latitude,
        double? Longitude,
        int CurrentOccupancy,
        DateTime? LastPositionAt);

    public record BusLivePositionSnapshotDto(
        DateTime GeneratedAtUtc,
        IReadOnlyList<BusLivePositionDto> Buses);

    public record BusRuntimeEventDto(
        Ulid BusRuntimeEventId,
        Ulid BusId,
        string EventType,
        string Description,
        string? IMEI,
        double? Latitude,
        double? Longitude,
        int? Occupancy,
        DateTime OccurredAtUtc);

    private sealed record GeofenceDecision(bool IsOutOfRadius, string Description, string? AutoGeneratedPointCode)
    {
        public static GeofenceDecision InRadius() => new(false, string.Empty, null);
        public static GeofenceDecision OutOfRadius(string description, string autoGeneratedPointCode)
            => new(true, description, autoGeneratedPointCode);
    }
}
