namespace CollectManagement.Application.Features.Rattachements.Commands.CreateRattachement;

public record CreateRattachementCommand(
    string NumeroRattachement,
    int? Exercice,
    DateTime DateRattachement,
    string? NumeroChantier,
    string? CodeClient,
    bool IsInternal,
    decimal? Cout,
    string? Type,
    string? Nature,
    string? Responsable,
    TimeSpan? HeureDebut,
    TimeSpan? HeureFin,
    string? Emplacement,
    string? Reference,
    string? Status,
    DateTime? DateCloture,
    string? Remarque,
    bool IsActive,
    Ulid SocieteId
) : IRequest<CreateRattachementResponse>;
