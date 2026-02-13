namespace CollectManagement.Application.Features.OrdresTravail.Commands.CreateOrdreTravail;

public record CreateOrdreTravailCommand(
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
    bool IsActive,
    Ulid SocieteId
) : IRequest<CreateOrdreTravailResponse>;
