using System.ComponentModel.DataAnnotations;
using Carter;
using CollectManagement.Application.Common;
using CollectManagement.Application.Features.Bus.Commands.CreateBus;
using CollectManagement.Application.Features.Bus.Commands.DeleteBus;
using CollectManagement.Application.Features.Bus.Commands.UpdateBus;
using CollectManagement.Application.Features.Bus.Queries.GetOneBus;
using CollectManagement.Application.Features.Bus.Queries.GetPagedListBus;
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
}
