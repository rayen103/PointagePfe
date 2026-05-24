using System.ComponentModel.DataAnnotations;
using Carter;
using CollectManagement.Application.Common;
using CollectManagement.Application.Features.Circuits.Commands.CreateCircuit;
using CollectManagement.Application.Features.Circuits.Commands.DeleteCircuit;
using CollectManagement.Application.Features.Circuits.Commands.UpdateCircuit;
using CollectManagement.Application.Features.Circuits.Queries.GetOneCircuit;
using CollectManagement.Application.Features.Circuits.Queries.GetPagedListCircuit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CollectManagement.WebAPI.Authorization;

namespace CollectManagement.WebAPI.EndPoints;

public class CircuitEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var routeGroupBuilder = app.MapGroup("cm/circuit").RequireNavigationPermission("fichier.circuit");

        routeGroupBuilder.MapGet("list", CircuitList);
        routeGroupBuilder.MapPost("add", CreateCircuit);
        routeGroupBuilder.MapPatch("update", UpdateCircuit);
        routeGroupBuilder.MapPost("{id}/delete", DeleteCircuit);
        routeGroupBuilder.MapGet("{id}/one", OneCircuit);
    }

    public static async Task<IResult> CreateCircuit(
        [FromBody] [Required] CreateCircuitCommand createCircuitCommand,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var createResponse = await sender
            .Send(createCircuitCommand, cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<CreateCircuitResponse>(createResponse));
    }

    private static async Task<IResult> CircuitList(
        [FromQuery] string? search,
        [FromQuery] string? sort,
        [FromQuery] string? order,
        int page,
        int size,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var list = await sender
            .Send(new GetPagedListCircuitQuery(search, sort, order, page, size), cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<GetPagedListCircuitResponse>(list));
    }

    private static async Task<IResult> UpdateCircuit(
        [FromBody] [Required] UpdateCircuitCommand updateCircuitCommand,
        ISender sender,
        CancellationToken cancellationToken)
    {
        await sender
            .Send(updateCircuitCommand, cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<bool>(true));
    }

    private static async Task<IResult> DeleteCircuit(
        [FromRoute] [Required] Ulid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        await sender
            .Send(new DeleteCircuitCommand(id), cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<bool>(true));
    }

    public async static Task<IResult> OneCircuit(
        [Required] Ulid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var response = await sender
            .Send(new GetOneCircuitQuery(id), cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<GetOneCircuitDto>(response));
    }
}
