using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Carter;
using CollectManagement.Application.Common;
using CollectManagement.Application.Interfaces.Repositories.Bus;
using CollectManagement.Application.Interfaces.Repositories.Chauffeurs;
using CollectManagement.Application.Shared;
using CollectManagement.Domain.Bus.ValueObjects;
using CollectManagement.Domain.Chauffeurs;
using CollectManagement.Domain.Chauffeurs.ValueObjects;
using CollectManagement.Domain.Societes.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using CollectManagement.WebAPI.Authorization;

namespace CollectManagement.WebAPI.EndPoints;

public class ChauffeurEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var routeGroupBuilder = app.MapGroup("cm/chauffeur").RequireNavigationPermission("fichier.chauffeur");

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
        IChauffeurRepository repository,
        CancellationToken cancellationToken)
    {
        var records = await repository.GetAllAsync(cancellationToken).ConfigureAwait(false);
        IEnumerable<Chauffeur> query = records;

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(x =>
                x.CodeChauffeur.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                x.Nom.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                (x.Prenom != null && x.Prenom.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                (x.CIN != null && x.CIN.Contains(search, StringComparison.OrdinalIgnoreCase)));
        }

        var prop = TypeDescriptor.GetProperties(typeof(Chauffeur)).Find(sort ?? "CodeChauffeur", true);
        query = prop is not null && order == "desc"
            ? query.OrderByDescending(x => prop.GetValue(x))
            : query.OrderBy(x => prop is null ? x.CodeChauffeur : prop.GetValue(x));

        var totalCount = query.Count();
        var data = query
            .Skip((page - 1) * size)
            .Take(size)
            .Select(x => new ChauffeurDto(
                x.ChauffeurId.Value,
                x.CodeChauffeur,
                x.Nom,
                x.Prenom,
                x.CIN,
                x.RFIDChauffeur,
                x.Externe,
                x.IsActive,
                x.SocieteId.Value,
                x.BusId?.Value))
            .ToList();

        return Results.Ok(new ApiResponse<object>(new { chauffeurs = data, totalCount }));
    }

    private static async Task<IResult> Create(
        [FromBody] [Required] UpsertChauffeurRequest request,
        IChauffeurRepository repository,
        IBusRepository busRepository,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        BusId? busId = request.BusId.HasValue ? new BusId(request.BusId.Value) : null;
        var chauffeur = Chauffeur.Create(
            new ChauffeurId(Ulid.NewUlid()),
            request.CodeChauffeur,
            request.Nom,
            request.Prenom,
            request.CIN,
            request.RFIDChauffeur,
            request.Externe,
            request.IsActive,
            new SocieteId(request.SocieteId),
            busId);

        await repository.AddAsync(chauffeur, cancellationToken).ConfigureAwait(false);

        if (busId is not null)
        {
            var bus = await busRepository.GetAsync(x => x.BusId == busId, cancellationToken).ConfigureAwait(false);
            if (bus is not null)
            {
                bus.AssignChauffeur(request.CodeChauffeur);
                busRepository.Update(bus);

                var otherBuses = await busRepository
                    .GetManyAsync(x => x.CodeChauffeur != null && x.CodeChauffeur.ToLower() == request.CodeChauffeur.Trim().ToLower() && x.BusId != busId, cancellationToken)
                    .ConfigureAwait(false);

                foreach (var otherBus in otherBuses)
                {
                    otherBus.AssignChauffeur(null);
                    busRepository.Update(otherBus);
                }

                var otherChauffeurs = await repository
                    .GetManyAsync(x => x.BusId == busId && x.CodeChauffeur.ToLower() != request.CodeChauffeur.Trim().ToLower(), cancellationToken)
                    .ConfigureAwait(false);

                foreach (var otherChauffeur in otherChauffeurs)
                {
                    otherChauffeur.AssignBus(null);
                    repository.Update(otherChauffeur);
                }
            }
        }

        await unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return Results.Ok(new ApiResponse<ChauffeurDto>(new ChauffeurDto(
            chauffeur.ChauffeurId.Value,
            chauffeur.CodeChauffeur,
            chauffeur.Nom,
            chauffeur.Prenom,
            chauffeur.CIN,
            chauffeur.RFIDChauffeur,
            chauffeur.Externe,
            chauffeur.IsActive,
            chauffeur.SocieteId.Value,
            chauffeur.BusId?.Value)));
    }

    private static async Task<IResult> Update(
        [FromBody] [Required] UpdateChauffeurRequest request,
        IChauffeurRepository repository,
        IBusRepository busRepository,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var chauffeurId = new ChauffeurId(request.ChauffeurId);
        var chauffeur = await repository.GetAsync(x => x.ChauffeurId == chauffeurId, cancellationToken).ConfigureAwait(false);

        if (chauffeur is null)
            return Results.NotFound(new ApiResponse<string>("Chauffeur not found", false, StatusCodes.Status404NotFound));

        BusId? oldBusId = chauffeur.BusId;
        BusId? newBusId = request.BusId.HasValue ? new BusId(request.BusId.Value) : null;
        string oldCodeChauffeur = chauffeur.CodeChauffeur;
        
        chauffeur.Update(
            request.CodeChauffeur,
            request.Nom,
            request.Prenom,
            request.CIN,
            request.RFIDChauffeur,
            request.Externe,
            request.IsActive,
            newBusId);

        repository.Update(chauffeur);

        if (oldBusId != newBusId || !string.Equals(oldCodeChauffeur, request.CodeChauffeur, StringComparison.OrdinalIgnoreCase))
        {
            if (oldBusId is not null)
            {
                var oldBus = await busRepository.GetAsync(x => x.BusId == oldBusId && x.CodeChauffeur != null && x.CodeChauffeur.ToLower() == oldCodeChauffeur.Trim().ToLower(), cancellationToken).ConfigureAwait(false);
                if (oldBus is not null)
                {
                    oldBus.AssignChauffeur(null);
                    busRepository.Update(oldBus);
                }
            }

            if (newBusId is not null)
            {
                var newBus = await busRepository.GetAsync(x => x.BusId == newBusId, cancellationToken).ConfigureAwait(false);
                if (newBus is not null)
                {
                    newBus.AssignChauffeur(request.CodeChauffeur);
                    busRepository.Update(newBus);

                    var otherChauffeurs = await repository
                        .GetManyAsync(x => x.BusId == newBusId && x.ChauffeurId != chauffeurId, cancellationToken)
                        .ConfigureAwait(false);

                    foreach (var otherChauffeur in otherChauffeurs)
                    {
                        otherChauffeur.AssignBus(null);
                        repository.Update(otherChauffeur);
                    }

                    var otherBuses = await busRepository
                        .GetManyAsync(x => x.CodeChauffeur != null && x.CodeChauffeur.ToLower() == request.CodeChauffeur.Trim().ToLower() && x.BusId != newBusId, cancellationToken)
                        .ConfigureAwait(false);

                    foreach (var otherBus in otherBuses)
                    {
                        otherBus.AssignChauffeur(null);
                        busRepository.Update(otherBus);
                    }
                }
            }
        }

        await unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return Results.Ok(new ApiResponse<bool>(true));
    }

    private static async Task<IResult> Delete(
        [FromRoute] [Required] Ulid id,
        IChauffeurRepository repository,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var chauffeurId = new ChauffeurId(id);
        await repository.DeleteAsync(x => x.ChauffeurId == chauffeurId, cancellationToken).ConfigureAwait(false);
        await unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return Results.Ok(new ApiResponse<bool>(true));
    }

    private static async Task<IResult> One(
        [FromRoute] [Required] Ulid id,
        IChauffeurRepository repository,
        CancellationToken cancellationToken)
    {
        var chauffeurId = new ChauffeurId(id);
        var chauffeur = await repository.GetAsync(x => x.ChauffeurId == chauffeurId, cancellationToken).ConfigureAwait(false);

        if (chauffeur is null)
            return Results.NotFound(new ApiResponse<string>("Chauffeur not found", false, StatusCodes.Status404NotFound));

        return Results.Ok(new ApiResponse<ChauffeurDto>(new ChauffeurDto(
            chauffeur.ChauffeurId.Value,
            chauffeur.CodeChauffeur,
            chauffeur.Nom,
            chauffeur.Prenom,
            chauffeur.CIN,
            chauffeur.RFIDChauffeur,
            chauffeur.Externe,
            chauffeur.IsActive,
            chauffeur.SocieteId.Value,
            chauffeur.BusId?.Value)));
    }

    private record ChauffeurDto(
        Ulid ChauffeurId,
        string CodeChauffeur,
        string Nom,
        string? Prenom,
        string? CIN,
        string? RFIDChauffeur,
        bool Externe,
        bool IsActive,
        Ulid SocieteId,
        Ulid? BusId);

    public record UpsertChauffeurRequest(
        string CodeChauffeur,
        string Nom,
        string? Prenom,
        string? CIN,
        string? RFIDChauffeur,
        bool Externe,
        Ulid SocieteId,
        bool IsActive = true,
        Ulid? BusId = null);

    public record UpdateChauffeurRequest(
        Ulid ChauffeurId,
        string CodeChauffeur,
        string Nom,
        string? Prenom,
        string? CIN,
        string? RFIDChauffeur,
        bool Externe,
        bool IsActive,
        Ulid? BusId = null);
}
