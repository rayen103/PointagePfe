using System.ComponentModel.DataAnnotations;
using Carter;
using CollectManagement.Application.Common;
using CollectManagement.Application.Features.RattachementEmployes.Commands.CreateRattachementEmploye;
using CollectManagement.Application.Features.RattachementEmployes.Commands.DeleteRattachementEmploye;
using CollectManagement.Application.Features.RattachementEmployes.Commands.UpdateRattachementEmploye;
using CollectManagement.Application.Features.RattachementEmployes.Queries.GetOneRattachementEmploye;
using CollectManagement.Application.Features.RattachementEmployes.Queries.GetPagedListRattachementEmploye;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CollectManagement.WebAPI.EndPoints;

public class RattachementEmployeEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var routeGroupBuilder = app.MapGroup("cm/rattachement-employe").RequireAuthorization();

        routeGroupBuilder.MapGet("list", RattachementEmployeList);
        routeGroupBuilder.MapPost("add", CreateRattachementEmploye);
        routeGroupBuilder.MapPatch("update", UpdateRattachementEmploye);
        routeGroupBuilder.MapPost("{id}/delete", DeleteRattachementEmploye);
        routeGroupBuilder.MapGet("{id}/one", OneRattachementEmploye);
    }

    private static async Task<IResult> RattachementEmployeList(
        [FromQuery] string? search,
        [FromQuery] string? sort,
        [FromQuery] string? order,
        int page,
        int size,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var list = await sender
            .Send(new GetPagedListRattachementEmployeQuery(search, sort, order, page, size), cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<GetPagedListRattachementEmployeResponse>(list));
    }

    public static async Task<IResult> CreateRattachementEmploye(
        [FromBody] [Required] CreateRattachementEmployeCommand createRattachementEmployeCommand,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var createResponse = await sender
            .Send(createRattachementEmployeCommand, cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<CreateRattachementEmployeResponse>(createResponse));
    }

    private static async Task<IResult> UpdateRattachementEmploye(
        [FromBody] [Required] UpdateRattachementEmployeCommand updateRattachementEmployeCommand,
        ISender sender,
        CancellationToken cancellationToken)
    {
        await sender
            .Send(updateRattachementEmployeCommand, cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<bool>(true));
    }

    private static async Task<IResult> DeleteRattachementEmploye(
        [FromRoute] [Required] Ulid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        await sender
            .Send(new DeleteRattachementEmployeCommand(id), cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<bool>(true));
    }

    public static async Task<IResult> OneRattachementEmploye(
        [Required] Ulid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var response = await sender
            .Send(new GetOneRattachementEmployeQuery(id), cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<GetOneRattachementEmployeDto>(response));
    }
}
