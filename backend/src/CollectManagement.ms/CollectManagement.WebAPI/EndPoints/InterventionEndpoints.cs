using System.ComponentModel.DataAnnotations;
using Carter;
using CollectManagement.Application.Common;
using CollectManagement.Application.Features.Interventions.Commands.CreateIntervention;
using CollectManagement.Application.Features.Interventions.Commands.DeleteIntervention;
using CollectManagement.Application.Features.Interventions.Commands.UpdateIntervention;
using CollectManagement.Application.Features.Interventions.Queries.GetOneIntervention;
using CollectManagement.Application.Features.Interventions.Queries.GetPagedListIntervention;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CollectManagement.WebAPI.EndPoints;

public class InterventionEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var routeGroupBuilder = app.MapGroup("cm/intervention").RequireAuthorization();

        routeGroupBuilder.MapGet("list", InterventionList);
        routeGroupBuilder.MapPost("add", CreateIntervention);
        routeGroupBuilder.MapPatch("update", UpdateIntervention);
        routeGroupBuilder.MapPost("{id}/delete", DeleteIntervention);
        routeGroupBuilder.MapGet("{id}/one", OneIntervention);
    }

    public static async Task<IResult> CreateIntervention(
        [FromBody] [Required] CreateInterventionCommand createInterventionCommand,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var createResponse = await sender
            .Send(createInterventionCommand, cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<CreateInterventionResponse>(createResponse));
    }

    private static async Task<IResult> InterventionList(
        [FromQuery] string? search,
        [FromQuery] string? sort,
        [FromQuery] string? order,
        int page,
        int size,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var list = await sender
            .Send(new GetPagedListInterventionQuery(search, sort, order, page, size), cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<GetPagedListInterventionResponse>(list));
    }

    private static async Task<IResult> UpdateIntervention(
        [FromBody] [Required] UpdateInterventionCommand updateInterventionCommand,
        ISender sender,
        CancellationToken cancellationToken)
    {
        await sender
            .Send(updateInterventionCommand, cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<bool>(true));
    }

    private static async Task<IResult> DeleteIntervention(
        [FromRoute] [Required] Ulid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        await sender
            .Send(new DeleteInterventionCommand(id.ToString()), cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<bool>(true));
    }

    private static async Task<IResult> OneIntervention(
        [FromRoute] [Required] Ulid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var intervention = await sender
            .Send(new GetOneInterventionQuery(id), cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<GetOneInterventionResponse>(intervention));
    }
}
