namespace CollectManagement.Application.Features.OrdresTravail.Commands.UpdateOrdreTravail;

public record UpdateOrdreTravailCommand(
    Ulid OrdreTravailId,
    string NumeroOrdreTravail,
    string? NumeroChantier,
    string? CodeClient,
    string? NumeroBonCommande,
    string? CodeEquipe,
    string? EtatOT,
    decimal? Montant,
    DateTime? DateCreation,
    string? NumeroConvention,
    string? CodeVehicule,
    string? Libelle,
    bool IsActive
) : IRequest<UpdateOrdreTravailResponse>;
