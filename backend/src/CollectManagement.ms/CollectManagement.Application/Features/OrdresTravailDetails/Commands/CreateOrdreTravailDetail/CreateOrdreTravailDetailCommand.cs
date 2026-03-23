namespace CollectManagement.Application.Features.OrdresTravailDetails.Commands.CreateOrdreTravailDetail;

public record CreateOrdreTravailDetailCommand(
    Ulid OrdreTravailId,
    string CodeArticle,
    string? CodeEntrepot,
    string? CodeUnite,
    string? LibelleArticle,
    decimal? PrixUnitaireHT,
    decimal? Quantite,
    decimal? TauxTVA,
    decimal? Montant
) : IRequest<CreateOrdreTravailDetailResponse>;
