using System.ComponentModel.DataAnnotations;
using Carter;
using CollectManagement.Application.Common;
using CollectManagement.Application.Features.RattachementArticles.Commands.CreateRattachementArticle;
using CollectManagement.Application.Features.RattachementArticles.Commands.DeleteRattachementArticle;
using CollectManagement.Application.Features.RattachementArticles.Commands.UpdateRattachementArticle;
using CollectManagement.Application.Features.RattachementArticles.Queries.GetOneRattachementArticle;
using CollectManagement.Application.Features.RattachementArticles.Queries.GetPagedListRattachementArticle;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CollectManagement.WebAPI.Authorization;

namespace CollectManagement.WebAPI.EndPoints;

public class RattachementArticleEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var routeGroupBuilder = app.MapGroup("cm/rattachement-article").RequireNavigationPermission("fichier.rattachement");

        routeGroupBuilder.MapGet("list", RattachementArticleList);
        routeGroupBuilder.MapPost("add", CreateRattachementArticle);
        routeGroupBuilder.MapPatch("update", UpdateRattachementArticle);
        routeGroupBuilder.MapPost("{id}/delete", DeleteRattachementArticle);
        routeGroupBuilder.MapGet("{id}/one", OneRattachementArticle);
    }

    private static async Task<IResult> RattachementArticleList(
        [FromQuery] string? search,
        [FromQuery] string? sort,
        [FromQuery] string? order,
        int page,
        int size,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var list = await sender
            .Send(new GetPagedListRattachementArticleQuery(search, sort, order, page, size), cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<GetPagedListRattachementArticleResponse>(list));
    }

    public static async Task<IResult> CreateRattachementArticle(
        [FromBody] [Required] CreateRattachementArticleCommand createRattachementArticleCommand,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var createResponse = await sender
            .Send(createRattachementArticleCommand, cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<CreateRattachementArticleResponse>(createResponse));
    }

    private static async Task<IResult> UpdateRattachementArticle(
        [FromBody] [Required] UpdateRattachementArticleCommand updateRattachementArticleCommand,
        ISender sender,
        CancellationToken cancellationToken)
    {
        await sender
            .Send(updateRattachementArticleCommand, cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<bool>(true));
    }

    private static async Task<IResult> DeleteRattachementArticle(
        [FromRoute] [Required] Ulid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        await sender
            .Send(new DeleteRattachementArticleCommand(id), cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<bool>(true));
    }

    public static async Task<IResult> OneRattachementArticle(
        [Required] Ulid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var response = await sender
            .Send(new GetOneRattachementArticleQuery(id), cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<GetOneRattachementArticleDto>(response));
    }
}
