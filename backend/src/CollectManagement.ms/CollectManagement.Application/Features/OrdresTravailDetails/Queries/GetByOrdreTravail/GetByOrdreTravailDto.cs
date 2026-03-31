namespace CollectManagement.Application.Features.OrdresTravailDetails.Queries.GetByOrdreTravail;

public class GetByOrdreTravailDto
{
    public Ulid OrdreTravailDetailId { get; set; }
    public Ulid OrdreTravailId { get; set; }
    public string CodeArticle { get; set; } = string.Empty;
    public string? CodeEntrepot { get; set; }
    public string? CodeUnite { get; set; }
    public string? LibelleArticle { get; set; }
    public decimal? PrixUnitaireHT { get; set; }
    public decimal? Quantite { get; set; }
    public decimal? TauxTVA { get; set; }
    public decimal? Montant { get; set; }
}
