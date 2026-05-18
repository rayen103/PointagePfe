using System.ComponentModel.DataAnnotations;
using Carter;
using CollectManagement.Application.Common;
using CollectManagement.Application.Features.Bus.Commands.CreateBus;
using CollectManagement.Application.Features.Bus.Commands.DeleteBus;
using CollectManagement.Application.Features.Bus.Commands.UpdateBus;
using CollectManagement.Application.Interfaces.Repositories;
using CollectManagement.Application.Interfaces.Repositories.Bus;
using CollectManagement.Application.Shared;
using CollectManagement.Application.Features.Bus.Queries.GetOneBus;
using CollectManagement.Application.Features.Bus.Queries.GetPagedListBus;
using CollectManagement.Domain.Bus;
using CollectManagement.Domain.Bus.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CollectManagement.WebAPI.EndPoints;

public class BusEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var routeGroupBuilder = app.MapGroup("cm/bus").RequireAuthorization();

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
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var normalizedImei = request.IMEI.Trim();
        var bus = await busRepository
            .GetAsync(
                x => x.IMEI != null &&
                     x.IMEI.ToLower() == normalizedImei.ToLower(),
                cancellationToken)
            .ConfigureAwait(false);

        if (bus is null)
            return Results.NotFound(new ApiResponse<string>($"No bus mapped to IMEI '{request.IMEI}'.", false, StatusCodes.Status404NotFound));

        var occurredAtUtc = request.TimestampUtc?.ToUniversalTime() ?? DateTime.UtcNow;
        bus.UpdateRuntimeState(
            normalizedImei,
            request.Latitude,
            request.Longitude,
            request.Occupancy,
            occurredAtUtc);

        await busRepository
            .UpdateBulkAsync(bus, cancellationToken)
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

    public record UpdateBusRuntimePositionRequest(
        string IMEI,
        double? Latitude,
        double? Longitude,
        int? Occupancy,
        DateTime? TimestampUtc);

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
}
