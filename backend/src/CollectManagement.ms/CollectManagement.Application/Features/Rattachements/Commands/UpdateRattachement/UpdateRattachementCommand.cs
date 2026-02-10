namespace CollectManagement.Application.Features.Rattachements.Commands.UpdateRattachement;

public record UpdateRattachementCommand(
    Ulid RattachementId,
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
    bool IsActive
) : IRequest<UpdateRattachementResponse>;
