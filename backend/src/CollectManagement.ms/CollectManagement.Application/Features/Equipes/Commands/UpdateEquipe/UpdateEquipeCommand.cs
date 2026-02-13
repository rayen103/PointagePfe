namespace CollectManagement.Application.Features.Equipes.Commands.UpdateEquipe;

public record UpdateEquipeCommand(
    Ulid EquipeId,
    string CodeEquipe,
    string? LibelleEquipe,
    string? CodeClient,
    string? CodeEntrepot,
    string? CodeTarif,
    string? CodeFournisseur,
    string? Responsable,
    bool IsInternal,
    string? CodeVehicule,
    bool IsActive
) : IRequest<UpdateEquipeResponse>;
