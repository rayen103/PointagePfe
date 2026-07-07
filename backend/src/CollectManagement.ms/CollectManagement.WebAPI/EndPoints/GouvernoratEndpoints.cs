using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Carter;
using CollectManagement.Application.Common;
using CollectManagement.Application.Interfaces.Repositories.Gouvernorats;
using CollectManagement.Application.Shared;
using CollectManagement.Domain.Gouvernorats;
using CollectManagement.Domain.Gouvernorats.ValueObjects;
using CollectManagement.Domain.Societes.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using CollectManagement.WebAPI.Authorization;

namespace CollectManagement.WebAPI.EndPoints;

public class GouvernoratEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var routeGroupBuilder = app.MapGroup("cm/gouvernorat").RequireNavigationPermission("fichier.gouvernorat");

        routeGroupBuilder.MapGet("list", List);
        routeGroupBuilder.MapPost("add", Create);
        routeGroupBuilder.MapPatch("update", Update);
        routeGroupBuilder.MapPost("{id}/delete", Delete);
        routeGroupBuilder.MapGet("{id}/one", One);
    }

    private static async Task<IResult> List(
        [FromQuery] string? search,
        [FromQuery] string? sort,
        [FromQuery] string? order,
        int page,
        int size,
        IGouvernoratRepository repository,
        CancellationToken cancellationToken)
    {
        var records = await repository.GetAllAsync(cancellationToken).ConfigureAwait(false);
        IEnumerable<Gouvernorat> query = records;

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(x =>
                x.CodeGouvernorat.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                (x.LibelleGouvernorat != null && x.LibelleGouvernorat.Contains(search, StringComparison.OrdinalIgnoreCase)));
        }

        // Deduplicate by CodeGouvernorat
        query = query
            .GroupBy(x => x.CodeGouvernorat)
            .Select(g => g.First());

        var prop = TypeDescriptor.GetProperties(typeof(Gouvernorat)).Find(sort ?? "CodeGouvernorat", true);
        query = prop is not null && order == "desc"
            ? query.OrderByDescending(x => prop.GetValue(x))
            : query.OrderBy(x => prop is null ? x.CodeGouvernorat : prop.GetValue(x));

        var totalCount = query.Count();
        var data = query
            .Skip((page - 1) * size)
            .Take(size)
            .Select(x => new GouvernoratDto(
                x.GouvernoratId.Value,
                x.CodeGouvernorat,
                x.LibelleGouvernorat,
                x.IsActive,
                x.SocieteId.Value))
            .ToList();

        return Results.Ok(new ApiResponse<object>(new { gouvernorats = data, totalCount }));
    }

    private static async Task<IResult> Create(
        [FromBody] [Required] UpsertGouvernoratRequest request,
        IGouvernoratRepository repository,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var gouvernorat = Gouvernorat.Create(
            new GouvernoratId(Ulid.NewUlid()),
            request.CodeGouvernorat,
            request.LibelleGouvernorat,
            request.IsActive,
            new SocieteId(request.SocieteId));

        await repository.AddAsync(gouvernorat, cancellationToken).ConfigureAwait(false);
        await unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return Results.Ok(new ApiResponse<GouvernoratDto>(new GouvernoratDto(
            gouvernorat.GouvernoratId.Value,
            gouvernorat.CodeGouvernorat,
            gouvernorat.LibelleGouvernorat,
            gouvernorat.IsActive,
            gouvernorat.SocieteId.Value)));
    }

    private static async Task<IResult> Update(
        [FromBody] [Required] UpdateGouvernoratRequest request,
        IGouvernoratRepository repository,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var gouvernoratId = new GouvernoratId(request.GouvernoratId);
        var gouvernorat = await repository.GetAsync(x => x.GouvernoratId == gouvernoratId, cancellationToken).ConfigureAwait(false);

        if (gouvernorat is null)
            return Results.NotFound(new ApiResponse<string>("Gouvernorat not found", false, StatusCodes.Status404NotFound));

        gouvernorat.Update(request.CodeGouvernorat, request.LibelleGouvernorat, request.IsActive);
        repository.Update(gouvernorat);
        await unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return Results.Ok(new ApiResponse<bool>(true));
    }

    private static async Task<IResult> Delete(
        [FromRoute] [Required] Ulid id,
        IGouvernoratRepository repository,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var gouvernoratId = new GouvernoratId(id);
        await repository.DeleteAsync(x => x.GouvernoratId == gouvernoratId, cancellationToken).ConfigureAwait(false);
        await unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return Results.Ok(new ApiResponse<bool>(true));
    }

    private static async Task<IResult> One(
        [FromRoute] [Required] Ulid id,
        IGouvernoratRepository repository,
        CancellationToken cancellationToken)
    {
        var gouvernoratId = new GouvernoratId(id);
        var gouvernorat = await repository.GetAsync(x => x.GouvernoratId == gouvernoratId, cancellationToken).ConfigureAwait(false);

        if (gouvernorat is null)
            return Results.NotFound(new ApiResponse<string>("Gouvernorat not found", false, StatusCodes.Status404NotFound));

        return Results.Ok(new ApiResponse<GouvernoratDto>(new GouvernoratDto(
            gouvernorat.GouvernoratId.Value,
            gouvernorat.CodeGouvernorat,
            gouvernorat.LibelleGouvernorat,
            gouvernorat.IsActive,
            gouvernorat.SocieteId.Value)));
    }

    private record GouvernoratDto(
        Ulid GouvernoratId,
        string CodeGouvernorat,
        string? LibelleGouvernorat,
        bool IsActive,
        Ulid SocieteId);

    public record UpsertGouvernoratRequest(
        string CodeGouvernorat,
        string? LibelleGouvernorat,
        Ulid SocieteId,
        bool IsActive = true);

    public record UpdateGouvernoratRequest(
        Ulid GouvernoratId,
        string CodeGouvernorat,
        string? LibelleGouvernorat,
        bool IsActive);
}
