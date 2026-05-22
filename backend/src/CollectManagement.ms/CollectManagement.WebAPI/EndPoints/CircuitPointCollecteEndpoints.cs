using System.ComponentModel.DataAnnotations;
using Carter;
using CollectManagement.Application.Common;
using CollectManagement.Application.Features.CircuitsPointsCollecte.Commands.CreateCircuitPointCollecte;
using CollectManagement.Application.Features.CircuitsPointsCollecte.Commands.DeleteCircuitPointCollecte;
using CollectManagement.Application.Features.CircuitsPointsCollecte.Commands.UpdateCircuitPointCollecte;
using CollectManagement.Application.Features.CircuitsPointsCollecte.Queries.GetByCircuit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CollectManagement.WebAPI.Authorization;

namespace CollectManagement.WebAPI.EndPoints;

public class CircuitPointCollecteEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var routeGroupBuilder = app.MapGroup("cm/circuit-point-collecte").RequireNavigationPermission("fichier.circuit");

        routeGroupBuilder.MapGet("{circuitId}/list", GetByCircuit);
        routeGroupBuilder.MapPost("add", CreateCircuitPointCollecte);
        routeGroupBuilder.MapPatch("update", UpdateCircuitPointCollecte);
        routeGroupBuilder.MapPost("{id}/delete", DeleteCircuitPointCollecte);
    }

    private static async Task<IResult> GetByCircuit(
        [Required] Ulid circuitId,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var response = await sender
            .Send(new GetByCircuitQuery(circuitId), cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<GetByCircuitResponse>(response));
    }

    private static async Task<IResult> CreateCircuitPointCollecte(
        [FromBody] [Required] CreateCircuitPointCollecteCommand command,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var createResponse = await sender
            .Send(command, cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<CreateCircuitPointCollecteResponse>(createResponse));
    }

    private static async Task<IResult> UpdateCircuitPointCollecte(
        [FromBody] [Required] UpdateCircuitPointCollecteCommand command,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var updateResponse = await sender
            .Send(command, cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<UpdateCircuitPointCollecteResponse>(updateResponse));
    }

    private static async Task<IResult> DeleteCircuitPointCollecte(
        [FromRoute] [Required] Ulid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        await sender
            .Send(new DeleteCircuitPointCollecteCommand(id), cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<bool>(true));
    }
}
