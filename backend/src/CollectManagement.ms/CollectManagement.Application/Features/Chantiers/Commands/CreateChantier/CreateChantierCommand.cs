namespace CollectManagement.Application.Features.Chantiers.Commands.CreateChantier;

public record CreateChantierCommand(
    string NumeroChantier,
    string? LibelleChantier,
    string? CodeClient,
    string? Adresse,
    decimal? MontantHT,
    decimal? MontantTTC,
    string? Nature,
    string? Responsable,
    DateTime? DateDebut,
    DateTime? DateFin,
    string? Status,
    bool IsActive,
    Ulid SocieteId
) : IRequest<CreateChantierResponse>;
