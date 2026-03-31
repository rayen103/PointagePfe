using CollectManagement.Domain.Common;
using CollectManagement.Domain.OrdresTravail.ValueObjects;

namespace CollectManagement.Domain.OrdresTravail;

public class OrdreTravailDetail : AuditableEntity
{
    public OrdreTravailDetailId OrdreTravailDetailId { get; private set; }

    public OrdreTravailId OrdreTravailId { get; private set; }

    public string CodeArticle { get; private set; }

    public string? CodeEntrepot { get; private set; }

    public string? CodeUnite { get; private set; }

    public string? LibelleArticle { get; private set; }

    public decimal? PrixUnitaireHT { get; private set; }

    public decimal? Quantite { get; private set; }

    public decimal? TauxTVA { get; private set; }

    public decimal? Montant { get; private set; }

    private OrdreTravailDetail(
        OrdreTravailDetailId ordreTravailDetailId,
        OrdreTravailId ordreTravailId,
        string codeArticle,
        string? codeEntrepot,
        string? codeUnite,
        string? libelleArticle,
        decimal? prixUnitaireHT,
        decimal? quantite,
        decimal? tauxTVA,
        decimal? montant)
    {
        OrdreTravailDetailId = ordreTravailDetailId;
        OrdreTravailId = ordreTravailId;
        CodeArticle = codeArticle;
        CodeEntrepot = codeEntrepot;
        CodeUnite = codeUnite;
        LibelleArticle = libelleArticle;
        PrixUnitaireHT = prixUnitaireHT;
        Quantite = quantite;
        TauxTVA = tauxTVA;
        Montant = montant;
    }

    public static OrdreTravailDetail Create(
        OrdreTravailDetailId ordreTravailDetailId,
        OrdreTravailId ordreTravailId,
        string codeArticle,
        string? codeEntrepot,
        string? codeUnite,
        string? libelleArticle,
        decimal? prixUnitaireHT,
        decimal? quantite,
        decimal? tauxTVA,
        decimal? montant)
    {
        return new OrdreTravailDetail(
            ordreTravailDetailId,
            ordreTravailId,
            codeArticle,
            codeEntrepot,
            codeUnite,
            libelleArticle,
            prixUnitaireHT,
            quantite,
            tauxTVA,
            montant);
    }

    public void Update(
        string codeArticle,
        string? codeEntrepot,
        string? codeUnite,
        string? libelleArticle,
        decimal? prixUnitaireHT,
        decimal? quantite,
        decimal? tauxTVA,
        decimal? montant)
    {
        CodeArticle = codeArticle;
        CodeEntrepot = codeEntrepot;
        CodeUnite = codeUnite;
        LibelleArticle = libelleArticle;
        PrixUnitaireHT = prixUnitaireHT;
        Quantite = quantite;
        TauxTVA = tauxTVA;
        Montant = montant;
    }

    public static OrdreTravailDetail QueryCreate(
        OrdreTravailDetailId ordreTravailDetailId,
        OrdreTravailId ordreTravailId,
        string codeArticle,
        string? codeEntrepot,
        string? codeUnite,
        string? libelleArticle,
        decimal? prixUnitaireHT,
        decimal? quantite,
        decimal? tauxTVA,
        decimal? montant)
    {
        return new OrdreTravailDetail(
            ordreTravailDetailId,
            ordreTravailId,
            codeArticle,
            codeEntrepot,
            codeUnite,
            libelleArticle,
            prixUnitaireHT,
            quantite,
            tauxTVA,
            montant);
    }

#pragma warning disable CS8618
    private OrdreTravailDetail() { }
#pragma warning restore CS8618
}
