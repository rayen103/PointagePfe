using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Carter;
using CollectManagement.Application.Common;
using CollectManagement.Application.Interfaces.Repositories.Chauffeurs;
using CollectManagement.Application.Shared;
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
                x.SocieteId.Value))
            .ToList();

        return Results.Ok(new ApiResponse<object>(new { chauffeurs = data, totalCount }));
    }

    private static async Task<IResult> Create(
        [FromBody] [Required] UpsertChauffeurRequest request,
        IChauffeurRepository repository,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var chauffeur = Chauffeur.Create(
            new ChauffeurId(Ulid.NewUlid()),
            request.CodeChauffeur,
            request.Nom,
            request.Prenom,
            request.CIN,
            request.RFIDChauffeur,
            request.Externe,
            request.IsActive,
            new SocieteId(request.SocieteId));

        await repository.AddAsync(chauffeur, cancellationToken).ConfigureAwait(false);
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
            chauffeur.SocieteId.Value)));
    }

    private static async Task<IResult> Update(
        [FromBody] [Required] UpdateChauffeurRequest request,
        IChauffeurRepository repository,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var chauffeurId = new ChauffeurId(request.ChauffeurId);
        var chauffeur = await repository.GetAsync(x => x.ChauffeurId == chauffeurId, cancellationToken).ConfigureAwait(false);

        if (chauffeur is null)
            return Results.NotFound(new ApiResponse<string>("Chauffeur not found", false, StatusCodes.Status404NotFound));

        chauffeur.Update(
            request.CodeChauffeur,
            request.Nom,
            request.Prenom,
            request.CIN,
            request.RFIDChauffeur,
            request.Externe,
            request.IsActive);

        repository.Update(chauffeur);
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
            chauffeur.SocieteId.Value)));
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
        Ulid SocieteId);

    public record UpsertChauffeurRequest(
        string CodeChauffeur,
        string Nom,
        string? Prenom,
        string? CIN,
        string? RFIDChauffeur,
        bool Externe,
        bool IsActive,
        Ulid SocieteId);

    public record UpdateChauffeurRequest(
        Ulid ChauffeurId,
        string CodeChauffeur,
        string Nom,
        string? Prenom,
        string? CIN,
        string? RFIDChauffeur,
        bool Externe,
        bool IsActive);
}
