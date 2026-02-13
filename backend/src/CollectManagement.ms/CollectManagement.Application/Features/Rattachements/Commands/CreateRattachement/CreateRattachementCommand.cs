namespace CollectManagement.Application.Features.Rattachements.Commands.CreateRattachement;

public record CreateRattachementCommand(
    string NumeroRattachement,
    string? Exercice,
    DateTime DateRattachement,
    string? NumeroChantier,
    string? CodeClient,
    bool IsInternal,
    decimal? Cout,
    string? Type,
    string? Nature,
    string? Responsable,
    string? HeureDebut,
    string? HeureFin,
    string? Emplacement,
    string? Reference,
    string? Status,
    DateTime? DateCloture,
    string? Remarque,
    bool IsActive,
    Ulid SocieteId
) : IRequest<CreateRattachementResponse>;
