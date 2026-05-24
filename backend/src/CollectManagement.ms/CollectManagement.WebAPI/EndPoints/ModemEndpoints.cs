using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Carter;
using CollectManagement.Application.Common;
using CollectManagement.Application.Interfaces.Repositories.Modems;
using CollectManagement.Application.Shared;
using CollectManagement.Domain.Modems;
using CollectManagement.Domain.Modems.ValueObjects;
using CollectManagement.Domain.Societes.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using CollectManagement.WebAPI.Authorization;

namespace CollectManagement.WebAPI.EndPoints;

public class ModemEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var routeGroupBuilder = app.MapGroup("cm/modem").RequireNavigationPermission("fichier.modem");

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
        IModemRepository repository,
        CancellationToken cancellationToken)
    {
        var records = await repository.GetAllAsync(cancellationToken).ConfigureAwait(false);
        IEnumerable<Modem> query = records;

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(x =>
                x.IMEI.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                (x.ModelModem != null && x.ModelModem.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                (x.NumeroSim != null && x.NumeroSim.Contains(search, StringComparison.OrdinalIgnoreCase)));
        }

        var prop = TypeDescriptor.GetProperties(typeof(Modem)).Find(sort ?? "IMEI", true);
        query = prop is not null && order == "desc"
            ? query.OrderByDescending(x => prop.GetValue(x))
            : query.OrderBy(x => prop is null ? x.IMEI : prop.GetValue(x));

        var totalCount = query.Count();
        var data = query
            .Skip((page - 1) * size)
            .Take(size)
            .Select(x => new ModemDto(
                x.ModemId.Value,
                x.IMEI,
                x.ModelModem,
                x.NumeroSim,
                x.IsActive,
                x.SocieteId.Value))
            .ToList();

        return Results.Ok(new ApiResponse<object>(new { modems = data, totalCount }));
    }

    private static async Task<IResult> Create(
        [FromBody] [Required] UpsertModemRequest request,
        IModemRepository repository,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var modem = Modem.Create(
            new ModemId(Ulid.NewUlid()),
            request.IMEI,
            request.ModelModem,
            request.NumeroSim,
            request.IsActive,
            new SocieteId(request.SocieteId));

        await repository.AddAsync(modem, cancellationToken).ConfigureAwait(false);
        await unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return Results.Ok(new ApiResponse<ModemDto>(new ModemDto(
            modem.ModemId.Value,
            modem.IMEI,
            modem.ModelModem,
            modem.NumeroSim,
            modem.IsActive,
            modem.SocieteId.Value)));
    }

    private static async Task<IResult> Update(
        [FromBody] [Required] UpdateModemRequest request,
        IModemRepository repository,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var modemId = new ModemId(request.ModemId);
        var modem = await repository.GetAsync(x => x.ModemId == modemId, cancellationToken).ConfigureAwait(false);

        if (modem is null)
            return Results.NotFound(new ApiResponse<string>("Modem not found", false, StatusCodes.Status404NotFound));

        modem.Update(request.IMEI, request.ModelModem, request.NumeroSim, request.IsActive);
        repository.Update(modem);
        await unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return Results.Ok(new ApiResponse<bool>(true));
    }

    private static async Task<IResult> Delete(
        [FromRoute] [Required] Ulid id,
        IModemRepository repository,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var modemId = new ModemId(id);
        await repository.DeleteAsync(x => x.ModemId == modemId, cancellationToken).ConfigureAwait(false);
        await unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return Results.Ok(new ApiResponse<bool>(true));
    }

    private static async Task<IResult> One(
        [FromRoute] [Required] Ulid id,
        IModemRepository repository,
        CancellationToken cancellationToken)
    {
        var modemId = new ModemId(id);
        var modem = await repository.GetAsync(x => x.ModemId == modemId, cancellationToken).ConfigureAwait(false);

        if (modem is null)
            return Results.NotFound(new ApiResponse<string>("Modem not found", false, StatusCodes.Status404NotFound));

        return Results.Ok(new ApiResponse<ModemDto>(new ModemDto(
            modem.ModemId.Value,
            modem.IMEI,
            modem.ModelModem,
            modem.NumeroSim,
            modem.IsActive,
            modem.SocieteId.Value)));
    }

    private record ModemDto(
        Ulid ModemId,
        string IMEI,
        string? ModelModem,
        string? NumeroSim,
        bool IsActive,
        Ulid SocieteId);

    public record UpsertModemRequest(
        string IMEI,
        string? ModelModem,
        string? NumeroSim,
        bool IsActive,
        Ulid SocieteId);

    public record UpdateModemRequest(
        Ulid ModemId,
        string IMEI,
        string? ModelModem,
        string? NumeroSim,
        bool IsActive);
}
