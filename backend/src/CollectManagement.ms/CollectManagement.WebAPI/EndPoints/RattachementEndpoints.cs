using System.ComponentModel.DataAnnotations;
using Carter;
using CollectManagement.Application.Common;
using CollectManagement.Application.Features.Rattachements.Commands.CreateRattachement;
using CollectManagement.Application.Features.Rattachements.Commands.DeleteRattachement;
using CollectManagement.Application.Features.Rattachements.Commands.UpdateRattachement;
using CollectManagement.Application.Features.Rattachements.Queries.GetOneRattachement;
using CollectManagement.Application.Features.Rattachements.Queries.GetPagedListRattachement;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CollectManagement.WebAPI.Authorization;

namespace CollectManagement.WebAPI.EndPoints;

public class RattachementEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var routeGroupBuilder = app.MapGroup("cm/rattachement").RequireNavigationPermission("fichier.rattachement");

        routeGroupBuilder.MapGet("list", RattachementList);
        routeGroupBuilder.MapPost("add", CreateRattachement);
        routeGroupBuilder.MapPatch("update", UpdateRattachement);
        routeGroupBuilder.MapPost("{id}/delete", DeleteRattachement);
        routeGroupBuilder.MapGet("{id}/one", OneRattachement);
    }

    public static async Task<IResult> CreateRattachement(
        [FromBody] [Required] CreateRattachementCommand createRattachementCommand,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var createResponse = await sender
            .Send(createRattachementCommand, cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<CreateRattachementResponse>(createResponse));
    }

    private static async Task<IResult> RattachementList(
        [FromQuery] string? search,
        [FromQuery] string? sort,
        [FromQuery] string? order,
        int page,
        int size,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var list = await sender
            .Send(new GetPagedListRattachementQuery(search, sort, order, page, size), cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<GetPagedListRattachementResponse>(list));
    }

    private static async Task<IResult> UpdateRattachement(
        [FromBody] [Required] UpdateRattachementCommand updateRattachementCommand,
        ISender sender,
        CancellationToken cancellationToken)
    {
        await sender
            .Send(updateRattachementCommand, cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<bool>(true));
    }

    private static async Task<IResult> DeleteRattachement(
        [FromRoute] [Required] Ulid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        await sender
            .Send(new DeleteRattachementCommand(id), cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<bool>(true));
    }

    public async static Task<IResult> OneRattachement(
        [Required] Ulid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var response = await sender
            .Send(new GetOneRattachementQuery(id), cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<GetOneRattachementDto>(response));
    }
}
