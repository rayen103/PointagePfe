using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Carter;
using CollectManagement.Application.Common;
using CollectManagement.Application.Interfaces.Repositories.Regions;
using CollectManagement.Application.Shared;
using CollectManagement.Domain.Regions;
using CollectManagement.Domain.Regions.ValueObjects;
using CollectManagement.Domain.Societes.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using CollectManagement.WebAPI.Authorization;

namespace CollectManagement.WebAPI.EndPoints;

public class RegionEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var routeGroupBuilder = app.MapGroup("cm/region").RequireNavigationPermission("fichier.region");

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
        IRegionRepository repository,
        CancellationToken cancellationToken)
    {
        var records = await repository.GetAllAsync(cancellationToken).ConfigureAwait(false);
        IEnumerable<Region> query = records;

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(x =>
                x.CodeRegion.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                (x.LibelleRegion != null && x.LibelleRegion.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                (x.CodeGouvernorat != null && x.CodeGouvernorat.Contains(search, StringComparison.OrdinalIgnoreCase)));
        }

        var prop = TypeDescriptor.GetProperties(typeof(Region)).Find(sort ?? "CodeRegion", true);
        query = prop is not null && order == "desc"
            ? query.OrderByDescending(x => prop.GetValue(x))
            : query.OrderBy(x => prop is null ? x.CodeRegion : prop.GetValue(x));

        var totalCount = query.Count();
        var data = query
            .Skip((page - 1) * size)
            .Take(size)
            .Select(x => new RegionDto(
                x.RegionId.Value,
                x.CodeRegion,
                x.LibelleRegion,
                x.CodeGouvernorat,
                x.IsActive,
                x.SocieteId.Value))
            .ToList();

        return Results.Ok(new ApiResponse<object>(new { regions = data, totalCount }));
    }

    private static async Task<IResult> Create(
        [FromBody] [Required] UpsertRegionRequest request,
        IRegionRepository repository,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var region = Region.Create(
            new RegionId(Ulid.NewUlid()),
            request.CodeRegion,
            request.LibelleRegion,
            request.CodeGouvernorat,
            request.IsActive,
            new SocieteId(request.SocieteId));

        await repository.AddAsync(region, cancellationToken).ConfigureAwait(false);
        await unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return Results.Ok(new ApiResponse<RegionDto>(new RegionDto(
            region.RegionId.Value,
            region.CodeRegion,
            region.LibelleRegion,
            region.CodeGouvernorat,
            region.IsActive,
            region.SocieteId.Value)));
    }

    private static async Task<IResult> Update(
        [FromBody] [Required] UpdateRegionRequest request,
        IRegionRepository repository,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var regionId = new RegionId(request.RegionId);
        var region = await repository.GetAsync(x => x.RegionId == regionId, cancellationToken).ConfigureAwait(false);

        if (region is null)
            return Results.NotFound(new ApiResponse<string>("Region not found", false, StatusCodes.Status404NotFound));

        region.Update(request.CodeRegion, request.LibelleRegion, request.CodeGouvernorat, request.IsActive);
        repository.Update(region);
        await unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return Results.Ok(new ApiResponse<bool>(true));
    }

    private static async Task<IResult> Delete(
        [FromRoute] [Required] Ulid id,
        IRegionRepository repository,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var regionId = new RegionId(id);
        await repository.DeleteAsync(x => x.RegionId == regionId, cancellationToken).ConfigureAwait(false);
        await unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return Results.Ok(new ApiResponse<bool>(true));
    }

    private static async Task<IResult> One(
        [FromRoute] [Required] Ulid id,
        IRegionRepository repository,
        CancellationToken cancellationToken)
    {
        var regionId = new RegionId(id);
        var region = await repository.GetAsync(x => x.RegionId == regionId, cancellationToken).ConfigureAwait(false);

        if (region is null)
            return Results.NotFound(new ApiResponse<string>("Region not found", false, StatusCodes.Status404NotFound));

        return Results.Ok(new ApiResponse<RegionDto>(new RegionDto(
            region.RegionId.Value,
            region.CodeRegion,
            region.LibelleRegion,
            region.CodeGouvernorat,
            region.IsActive,
            region.SocieteId.Value)));
    }

    private record RegionDto(
        Ulid RegionId,
        string CodeRegion,
        string? LibelleRegion,
        string? CodeGouvernorat,
        bool IsActive,
        Ulid SocieteId);

    public record UpsertRegionRequest(
        string CodeRegion,
        string? LibelleRegion,
        string? CodeGouvernorat,
        bool IsActive,
        Ulid SocieteId);

    public record UpdateRegionRequest(
        Ulid RegionId,
        string CodeRegion,
        string? LibelleRegion,
        string? CodeGouvernorat,
        bool IsActive);
}
