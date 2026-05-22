using System.ComponentModel.DataAnnotations;
using Carter;
using CollectManagement.Application.Common;
using CollectManagement.Application.Features.Equipes.Commands.CreateEquipe;
using CollectManagement.Application.Features.Equipes.Commands.DeleteEquipe;
using CollectManagement.Application.Features.Equipes.Commands.UpdateEquipe;
using CollectManagement.Application.Features.Equipes.Queries.GetOneEquipe;
using CollectManagement.Application.Features.Equipes.Queries.GetPagedListEquipe;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CollectManagement.WebAPI.Authorization;

namespace CollectManagement.WebAPI.EndPoints;

public class EquipeEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var routeGroupBuilder = app.MapGroup("cm/equipe").RequireNavigationPermission("fichier.equipe");

        routeGroupBuilder.MapGet("list", EquipeList);
        routeGroupBuilder.MapPost("add", CreateEquipe);
        routeGroupBuilder.MapPatch("update", UpdateEquipe);
        routeGroupBuilder.MapPost("{id}/delete", DeleteEquipe);
        routeGroupBuilder.MapGet("{id}/one", OneEquipe);
    }

    public static async Task<IResult> CreateEquipe(
        [FromBody] [Required] CreateEquipeCommand createEquipeCommand,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var createResponse = await sender
            .Send(createEquipeCommand, cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<CreateEquipeResponse>(createResponse));
    }

    private static async Task<IResult> EquipeList(
        [FromQuery] string? search,
        [FromQuery] string? sort,
        [FromQuery] string? order,
        int page,
        int size,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var list = await sender
            .Send(new GetPagedListEquipeQuery(search, sort, order, page, size), cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<GetPagedListEquipeResponse>(list));
    }

    private static async Task<IResult> UpdateEquipe(
        [FromBody] [Required] UpdateEquipeCommand updateEquipeCommand,
        ISender sender,
        CancellationToken cancellationToken)
    {
        await sender
            .Send(updateEquipeCommand, cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<bool>(true));
    }

    private static async Task<IResult> DeleteEquipe(
        [FromRoute] [Required] Ulid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        await sender
            .Send(new DeleteEquipeCommand(id), cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<bool>(true));
    }

    public async static Task<IResult> OneEquipe(
        [Required] Ulid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var response = await sender
            .Send(new GetOneEquipeQuery(id), cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<GetOneEquipeDto>(response));
    }
}
