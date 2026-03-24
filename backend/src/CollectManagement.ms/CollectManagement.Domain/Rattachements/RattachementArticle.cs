using CollectManagement.Domain.Common;
using CollectManagement.Domain.Rattachements.ValueObjects;
using CollectManagement.Domain.Societes;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Domain.Rattachements;

public class RattachementArticle : AuditableEntity
{
    public RattachementArticleId RattachementArticleId { get; private set; }

    public RattachementId RattachementId { get; private set; }

    public string CodeArticle { get; private set; }

    public string? Libelle { get; private set; }

    public decimal? Quantite { get; private set; }

    public decimal? PrixRevient { get; private set; }

    public decimal? TauxTVA { get; private set; }

    public string? CodeUnite { get; private set; }

    public string? CodeEntrepot { get; private set; }

    public string? TypeRattachement { get; private set; }

    public string? NumeroBonLivraison { get; private set; }

    public DateTime? DateBonLivraison { get; private set; }

    public bool IsActive { get; private set; } = true;

    public SocieteId SocieteId { get; private set; }

    public Societe? Societe { get; private set; }

    public Rattachement? Rattachement { get; private set; }

    private RattachementArticle(
        RattachementArticleId rattachementArticleId,
        RattachementId rattachementId,
        string codeArticle,
        string? libelle,
        decimal? quantite,
        decimal? prixRevient,
        decimal? tauxTVA,
        string? codeUnite,
        string? codeEntrepot,
        string? typeRattachement,
        string? numeroBonLivraison,
        DateTime? dateBonLivraison,
        bool isActive,
        SocieteId societeId)
    {
        RattachementArticleId = rattachementArticleId;
        RattachementId = rattachementId;
        CodeArticle = codeArticle;
        Libelle = libelle;
        Quantite = quantite;
        PrixRevient = prixRevient;
        TauxTVA = tauxTVA;
        CodeUnite = codeUnite;
        CodeEntrepot = codeEntrepot;
        TypeRattachement = typeRattachement;
        NumeroBonLivraison = numeroBonLivraison;
        DateBonLivraison = dateBonLivraison;
        IsActive = isActive;
        SocieteId = societeId;
    }

    public static RattachementArticle Create(
        RattachementArticleId rattachementArticleId,
        RattachementId rattachementId,
        string codeArticle,
        string? libelle,
        decimal? quantite,
        decimal? prixRevient,
        decimal? tauxTVA,
        string? codeUnite,
        string? codeEntrepot,
        string? typeRattachement,
        string? numeroBonLivraison,
        DateTime? dateBonLivraison,
        bool isActive,
        SocieteId societeId)
    {
        return new RattachementArticle(
            rattachementArticleId,
            rattachementId,
            codeArticle,
            libelle,
            quantite,
            prixRevient,
            tauxTVA,
            codeUnite,
            codeEntrepot,
            typeRattachement,
            numeroBonLivraison,
            dateBonLivraison,
            isActive,
            societeId);
    }

    public void Update(
        RattachementId rattachementId,
        string codeArticle,
        string? libelle,
        decimal? quantite,
        decimal? prixRevient,
        decimal? tauxTVA,
        string? codeUnite,
        string? codeEntrepot,
        string? typeRattachement,
        string? numeroBonLivraison,
        DateTime? dateBonLivraison,
        bool isActive)
    {
        RattachementId = rattachementId;
        CodeArticle = codeArticle;
        Libelle = libelle;
        Quantite = quantite;
        PrixRevient = prixRevient;
        TauxTVA = tauxTVA;
        CodeUnite = codeUnite;
        CodeEntrepot = codeEntrepot;
        TypeRattachement = typeRattachement;
        NumeroBonLivraison = numeroBonLivraison;
        DateBonLivraison = dateBonLivraison;
        IsActive = isActive;
    }

    public static RattachementArticle QueryCreate(
        RattachementArticleId rattachementArticleId,
        RattachementId rattachementId,
        string codeArticle,
        string? libelle,
        decimal? quantite,
        decimal? prixRevient,
        decimal? tauxTVA,
        string? codeUnite,
        string? codeEntrepot,
        string? typeRattachement,
        string? numeroBonLivraison,
        DateTime? dateBonLivraison,
        bool isActive,
        SocieteId societeId)
    {
        return new RattachementArticle(
            rattachementArticleId,
            rattachementId,
            codeArticle,
            libelle,
            quantite,
            prixRevient,
            tauxTVA,
            codeUnite,
            codeEntrepot,
            typeRattachement,
            numeroBonLivraison,
            dateBonLivraison,
            isActive,
            societeId);
    }

#pragma warning disable CS8618
    private RattachementArticle() { }
#pragma warning restore CS8618
}
