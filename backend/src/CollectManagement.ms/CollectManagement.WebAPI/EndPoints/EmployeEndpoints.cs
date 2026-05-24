using System.ComponentModel.DataAnnotations;
using Carter;
using CollectManagement.Application.Common;
using CollectManagement.Application.Features.Employes.Commands.CreateEmploye;
using CollectManagement.Application.Features.Employes.Commands.DeleteEmploye;
using CollectManagement.Application.Features.Employes.Commands.UpdateEmploye;
using CollectManagement.Application.Features.Employes.Queries.GetOneEmploye;
using CollectManagement.Application.Features.Employes.Queries.GetPagedListEmploye;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CollectManagement.WebAPI.Authorization;

namespace CollectManagement.WebAPI.EndPoints;

public class EmployeEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var routeGroupBuilder = app.MapGroup("cm/employe").RequireNavigationPermission("fichier.employe");

        routeGroupBuilder.MapGet("list", EmployeList);
        routeGroupBuilder.MapPost("add", CreateEmploye);
        routeGroupBuilder.MapPatch("update", UpdateEmploye);
        routeGroupBuilder.MapPost("{id}/delete", DeleteEmploye);
        routeGroupBuilder.MapGet("{id}/one", OneEmploye);
    }

    public static async Task<IResult> CreateEmploye(
        [FromBody] [Required] CreateEmployeCommand createEmployeCommand,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var createResponse = await sender
            .Send(createEmployeCommand, cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<CreateEmployeResponse>(createResponse));
    }

    private static async Task<IResult> EmployeList(
        [FromQuery] string? search,
        [FromQuery] string? sort,
        [FromQuery] string? order,
        int page,
        int size,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var list = await sender
            .Send(new GetPagedListEmployeQuery(search, sort, order, page, size), cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<GetPagedListEmployeResponse>(list));
    }

    private static async Task<IResult> UpdateEmploye(
        [FromBody] [Required] UpdateEmployeCommand updateEmployeCommand,
        ISender sender,
        CancellationToken cancellationToken)
    {
        await sender
            .Send(updateEmployeCommand, cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<bool>(true));
    }

    private static async Task<IResult> DeleteEmploye(
        [FromRoute] [Required] Ulid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        await sender
            .Send(new DeleteEmployeCommand(id.ToString()), cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<bool>(true));
    }

    private static async Task<IResult> OneEmploye(
        [FromRoute] [Required] Ulid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var employe = await sender
            .Send(new GetOneEmployeQuery(id), cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<GetOneEmployeResponse>(employe));
    }
}
