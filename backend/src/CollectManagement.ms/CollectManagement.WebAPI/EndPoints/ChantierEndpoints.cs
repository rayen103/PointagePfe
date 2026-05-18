using System.ComponentModel.DataAnnotations;
using Carter;
using CollectManagement.Application.Common;
using CollectManagement.Application.Features.Chantiers.Commands.CreateChantier;
using CollectManagement.Application.Features.Chantiers.Commands.DeleteChantier;
using CollectManagement.Application.Features.Chantiers.Commands.UpdateChantier;
using CollectManagement.Application.Features.Chantiers.Queries.GetOneChantier;
using CollectManagement.Application.Features.Chantiers.Queries.GetPagedListChantier;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CollectManagement.WebAPI.EndPoints;

public class ChantierEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("cm/chantier").RequireAuthorization();

        group.MapGet("list", ChantierList).AllowAnonymous();
        group.MapPost("add", CreateChantier);
        group.MapPatch("update", UpdateChantier);
        group.MapPost("{id}/delete", DeleteChantier);
        group.MapGet("{id}/one", OneChantier);
    }

    private static async Task<IResult> ChantierList(
        [FromQuery] string? search, [FromQuery] string? sort, [FromQuery] string? order,
        int page, int size, ISender sender, CancellationToken cancellationToken)
    {
        var list = await sender.Send(new GetPagedListChantierQuery(search, sort, order, page, size), cancellationToken).ConfigureAwait(false);
        return Results.Ok(new ApiResponse<GetPagedListChantierResponse>(list));
    }

    private static async Task<IResult> CreateChantier(
        [FromBody][Required] CreateChantierCommand cmd, ISender sender, CancellationToken cancellationToken)
    {
        var response = await sender.Send(cmd, cancellationToken).ConfigureAwait(false);
        return Results.Ok(new ApiResponse<CreateChantierResponse>(response));
    }

    private static async Task<IResult> UpdateChantier(
        [FromBody][Required] UpdateChantierCommand cmd, ISender sender, CancellationToken cancellationToken)
    {
        await sender.Send(cmd, cancellationToken).ConfigureAwait(false);
        return Results.Ok(new ApiResponse<bool>(true));
    }

    private static async Task<IResult> DeleteChantier(
        [FromRoute][Required] Ulid id, ISender sender, CancellationToken cancellationToken)
    {
        await sender.Send(new DeleteChantierCommand(id), cancellationToken).ConfigureAwait(false);
        return Results.Ok(new ApiResponse<bool>(true));
    }

    private static async Task<IResult> OneChantier(
        [Required] Ulid id, ISender sender, CancellationToken cancellationToken)
    {
        var response = await sender.Send(new GetOneChantierQuery(id), cancellationToken).ConfigureAwait(false);
        return Results.Ok(new ApiResponse<GetOneChantierDto>(response));
    }
}
