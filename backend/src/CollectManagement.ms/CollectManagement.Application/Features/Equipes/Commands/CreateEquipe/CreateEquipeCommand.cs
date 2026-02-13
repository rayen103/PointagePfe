namespace CollectManagement.Application.Features.Equipes.Commands.CreateEquipe;

public record CreateEquipeCommand(
    string CodeEquipe,
    string? LibelleEquipe,
    string? CodeClient,
    string? CodeEntrepot,
    string? CodeTarif,
    string? CodeFournisseur,
    string? Responsable,
    bool IsInternal,
    string? CodeVehicule,
    bool IsActive,
    Ulid SocieteId
) : IRequest<CreateEquipeResponse>;
