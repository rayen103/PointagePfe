namespace CollectManagement.Application.Features.OrdresTravailDetails.Commands.UpdateOrdreTravailDetail;

public record UpdateOrdreTravailDetailCommand(
    Ulid OrdreTravailDetailId,
    string CodeArticle,
    string? CodeEntrepot,
    string? CodeUnite,
    string? LibelleArticle,
    decimal? PrixUnitaireHT,
    decimal? Quantite,
    decimal? TauxTVA,
    decimal? Montant
) : IRequest<UpdateOrdreTravailDetailResponse>;
