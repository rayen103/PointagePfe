using System.ComponentModel.DataAnnotations;
using Carter;
using CollectManagement.Application.Common;
using CollectManagement.Application.Features.Sites.Commands.CreateSite;
using CollectManagement.Application.Features.Sites.Commands.DeleteSite;
using CollectManagement.Application.Features.Sites.Commands.UpdateSite;
using CollectManagement.Application.Features.Sites.Queries.GetOneSite;
using CollectManagement.Application.Features.Sites.Queries.GetPagedListSite;
using CollectManagement.WebAPI.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CollectManagement.WebAPI.EndPoints;

public class SiteEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("cm/site").RequireNavigationPermission("fichier.societe");
        group.MapGet("list", List);
        group.MapPost("add", Create);
        group.MapPatch("update", Update);
        group.MapPost("{id}/delete", Delete);
        group.MapGet("{id}/one", One);
    }

    private static async Task<IResult> List(
        [FromQuery] string? search,
        [FromQuery] string? sort,
        [FromQuery] string? order,
        [FromQuery] Ulid? societeId,
        int page,
        int size,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var list = await sender
            .Send(new GetPagedListSiteQuery(search, sort, order, page, size, societeId), cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<GetPagedListSiteResponse>(list));
    }

    private static async Task<IResult> Create(
        [FromBody][Required] CreateSiteCommand command,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var response = await sender.Send(command, cancellationToken).ConfigureAwait(false);
        return Results.Ok(new ApiResponse<CreateSiteResponse>(response));
    }

    private static async Task<IResult> Update(
        [FromBody][Required] UpdateSiteCommand command,
        ISender sender,
        CancellationToken cancellationToken)
    {
        await sender.Send(command, cancellationToken).ConfigureAwait(false);
        return Results.Ok(new ApiResponse<bool>(true));
    }

    private static async Task<IResult> Delete(
        [FromRoute][Required] Ulid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        await sender.Send(new DeleteSiteCommand(id), cancellationToken).ConfigureAwait(false);
        return Results.Ok(new ApiResponse<bool>(true));
    }

    private static async Task<IResult> One(
        [Required] Ulid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var response = await sender.Send(new GetOneSiteQuery(id), cancellationToken).ConfigureAwait(false);
        return Results.Ok(new ApiResponse<GetOneSiteDto>(response));
    }
}
