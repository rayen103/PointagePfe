using System.ComponentModel.DataAnnotations;
using Carter;
using CollectManagement.Application.Common;
using CollectManagement.Application.Features.OrdresTravail.Commands.CreateOrdreTravail;
using CollectManagement.Application.Features.OrdresTravail.Commands.DeleteOrdreTravail;
using CollectManagement.Application.Features.OrdresTravail.Commands.UpdateOrdreTravail;
using CollectManagement.Application.Features.OrdresTravail.Queries.GetOneOrdreTravail;
using CollectManagement.Application.Features.OrdresTravail.Queries.GetPagedListOrdreTravail;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CollectManagement.WebAPI.EndPoints;

public class OrdreTravailEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var routeGroupBuilder = app.MapGroup("cm/ordretravail").RequireAuthorization();

        routeGroupBuilder.MapGet("list", OrdreTravailList);
        routeGroupBuilder.MapPost("add", CreateOrdreTravail);
        routeGroupBuilder.MapPatch("update", UpdateOrdreTravail);
        routeGroupBuilder.MapPost("{id}/delete", DeleteOrdreTravail);
        routeGroupBuilder.MapGet("{id}/one", OneOrdreTravail);
    }

    public static async Task<IResult> CreateOrdreTravail(
        [FromBody] [Required] CreateOrdreTravailCommand createOrdreTravailCommand,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var createResponse = await sender
            .Send(createOrdreTravailCommand, cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<CreateOrdreTravailResponse>(createResponse));
    }

    private static async Task<IResult> OrdreTravailList(
        [FromQuery] string? search,
        [FromQuery] string? sort,
        [FromQuery] string? order,
        int page,
        int size,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var list = await sender
            .Send(new GetPagedListOrdreTravailQuery(search, sort, order, page, size), cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<GetPagedListOrdreTravailResponse>(list));
    }

    private static async Task<IResult> UpdateOrdreTravail(
        [FromBody] [Required] UpdateOrdreTravailCommand updateOrdreTravailCommand,
        ISender sender,
        CancellationToken cancellationToken)
    {
        await sender
            .Send(updateOrdreTravailCommand, cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<bool>(true));
    }

    private static async Task<IResult> DeleteOrdreTravail(
        [FromQuery] [Required] Ulid ordreTravailId,
        ISender sender,
        CancellationToken cancellationToken)
    {
        await sender
            .Send(new DeleteOrdreTravailCommand(ordreTravailId), cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<bool>(true));
    }

    public async static Task<IResult> OneOrdreTravail(
        [Required] Ulid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var response = await sender
            .Send(new GetOneOrdreTravailQuery(id), cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<GetOneOrdreTravailDto>(response));
    }
}
