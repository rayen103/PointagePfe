namespace CollectManagement.Application.Features.RattachementArticles.Queries.GetOneRattachementArticle;

public class GetOneRattachementArticleDto
{
    public Ulid RattachementArticleId { get; set; }
    public Ulid RattachementId { get; set; }
    public string CodeArticle { get; set; } = string.Empty;
    public string? Libelle { get; set; }
    public decimal? Quantite { get; set; }
    public decimal? PrixRevient { get; set; }
    public decimal? TauxTVA { get; set; }
    public string? CodeUnite { get; set; }
    public string? CodeEntrepot { get; set; }
    public string? TypeRattachement { get; set; }
    public string? NumeroBonLivraison { get; set; }
    public DateTime? DateBonLivraison { get; set; }
    public bool IsActive { get; set; }
    public Ulid SocieteId { get; set; }
}
