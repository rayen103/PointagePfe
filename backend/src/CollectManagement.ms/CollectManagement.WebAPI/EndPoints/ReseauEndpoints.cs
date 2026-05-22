using System.ComponentModel.DataAnnotations;
using Carter;
using CollectManagement.Application.Common;
using CollectManagement.Application.Features.Reseaux.Commands.CreateReseau;
using CollectManagement.Application.Features.Reseaux.Commands.DeleteReseau;
using CollectManagement.Application.Features.Reseaux.Commands.UpdateReseau;
using CollectManagement.Application.Features.Reseaux.Queries.GetOneReseau;
using CollectManagement.Application.Features.Reseaux.Queries.GetPagedListReseau;
using CollectManagement.WebAPI.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CollectManagement.WebAPI.EndPoints;

public class ReseauEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("cm/reseau").RequireNavigationPermission("fichier.societe");
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
            .Send(new GetPagedListReseauQuery(search, sort, order, page, size, societeId), cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<GetPagedListReseauResponse>(list));
    }

    private static async Task<IResult> Create(
        [FromBody][Required] CreateReseauCommand command,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var response = await sender.Send(command, cancellationToken).ConfigureAwait(false);
        return Results.Ok(new ApiResponse<CreateReseauResponse>(response));
    }

    private static async Task<IResult> Update(
        [FromBody][Required] UpdateReseauCommand command,
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
        await sender.Send(new DeleteReseauCommand(id), cancellationToken).ConfigureAwait(false);
        return Results.Ok(new ApiResponse<bool>(true));
    }

    private static async Task<IResult> One(
        [Required] Ulid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var response = await sender.Send(new GetOneReseauQuery(id), cancellationToken).ConfigureAwait(false);
        return Results.Ok(new ApiResponse<GetOneReseauDto>(response));
    }
}
