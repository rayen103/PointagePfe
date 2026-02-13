using System.ComponentModel.DataAnnotations;
using Carter;
using CollectManagement.Application.Common;
using CollectManagement.Application.Features.PointsCollecte.Commands.CreatePointCollecte;
using CollectManagement.Application.Features.PointsCollecte.Commands.DeletePointCollecte;
using CollectManagement.Application.Features.PointsCollecte.Commands.UpdatePointCollecte;
using CollectManagement.Application.Features.PointsCollecte.Queries.GetOnePointCollecte;
using CollectManagement.Application.Features.PointsCollecte.Queries.GetPagedListPointCollecte;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CollectManagement.WebAPI.EndPoints;

public class PointCollecteEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var routeGroupBuilder = app.MapGroup("cm/pointcollecte").RequireAuthorization();

        routeGroupBuilder.MapGet("list", PointCollecteList);
        routeGroupBuilder.MapPost("add", CreatePointCollecte);
        routeGroupBuilder.MapPatch("update", UpdatePointCollecte);
        routeGroupBuilder.MapPost("{id}/delete", DeletePointCollecte);
        routeGroupBuilder.MapGet("{id}/one", OnePointCollecte);
    }

    public static async Task<IResult> CreatePointCollecte(
        [FromBody] [Required] CreatePointCollecteCommand createPointCollecteCommand,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var createResponse = await sender
            .Send(createPointCollecteCommand, cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<CreatePointCollecteResponse>(createResponse));
    }

    private static async Task<IResult> PointCollecteList(
        [FromQuery] string? search,
        [FromQuery] string? sort,
        [FromQuery] string? order,
        int page,
        int size,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var list = await sender
            .Send(new GetPagedListPointCollecteQuery(search, sort, order, page, size), cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<GetPagedListPointCollecteResponse>(list));
    }

    private static async Task<IResult> UpdatePointCollecte(
        [FromBody] [Required] UpdatePointCollecteCommand updatePointCollecteCommand,
        ISender sender,
        CancellationToken cancellationToken)
    {
        await sender
            .Send(updatePointCollecteCommand, cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<bool>(true));
    }

    private static async Task<IResult> DeletePointCollecte(
        [FromQuery] [Required] Ulid pointCollecteId,
        ISender sender,
        CancellationToken cancellationToken)
    {
        await sender
            .Send(new DeletePointCollecteCommand(pointCollecteId), cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<bool>(true));
    }

    public async static Task<IResult> OnePointCollecte(
        [Required] Ulid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var response = await sender
            .Send(new GetOnePointCollecteQuery(id), cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<GetOnePointCollecteDto>(response));
    }
}
