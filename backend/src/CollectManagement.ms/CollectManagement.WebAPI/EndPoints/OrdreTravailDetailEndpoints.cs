using System.ComponentModel.DataAnnotations;
using Carter;
using CollectManagement.Application.Common;
using CollectManagement.Application.Features.OrdresTravailDetails.Commands.CreateOrdreTravailDetail;
using CollectManagement.Application.Features.OrdresTravailDetails.Commands.DeleteOrdreTravailDetail;
using CollectManagement.Application.Features.OrdresTravailDetails.Commands.UpdateOrdreTravailDetail;
using CollectManagement.Application.Features.OrdresTravailDetails.Queries.GetByOrdreTravail;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CollectManagement.WebAPI.EndPoints;

public class OrdreTravailDetailEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var routeGroupBuilder = app.MapGroup("cm/ordre-travail-detail").RequireAuthorization();

        routeGroupBuilder.MapGet("{ordreTravailId}/list", GetByOrdreTravail);
        routeGroupBuilder.MapPost("add", CreateOrdreTravailDetail);
        routeGroupBuilder.MapPatch("update", UpdateOrdreTravailDetail);
        routeGroupBuilder.MapPost("{id}/delete", DeleteOrdreTravailDetail);
    }

    private static async Task<IResult> GetByOrdreTravail(
        [Required] Ulid ordreTravailId,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var response = await sender
            .Send(new GetByOrdreTravailQuery(ordreTravailId), cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<GetByOrdreTravailResponse>(response));
    }

    private static async Task<IResult> CreateOrdreTravailDetail(
        [FromBody] [Required] CreateOrdreTravailDetailCommand command,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var createResponse = await sender
            .Send(command, cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<CreateOrdreTravailDetailResponse>(createResponse));
    }

    private static async Task<IResult> UpdateOrdreTravailDetail(
        [FromBody] [Required] UpdateOrdreTravailDetailCommand command,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var updateResponse = await sender
            .Send(command, cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<UpdateOrdreTravailDetailResponse>(updateResponse));
    }

    private static async Task<IResult> DeleteOrdreTravailDetail(
        [FromQuery] [Required] Ulid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        await sender
            .Send(new DeleteOrdreTravailDetailCommand(id), cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<bool>(true));
    }
}
