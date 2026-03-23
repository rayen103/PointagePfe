namespace CollectManagement.Application.Features.Chantiers.Commands.UpdateChantier;

public record UpdateChantierCommand(
    Ulid ChantierId,
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
    bool IsActive
) : IRequest<UpdateChantierResponse>;
