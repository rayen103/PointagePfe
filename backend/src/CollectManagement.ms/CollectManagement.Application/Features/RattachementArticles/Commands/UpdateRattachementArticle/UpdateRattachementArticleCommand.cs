namespace CollectManagement.Application.Features.RattachementArticles.Commands.UpdateRattachementArticle;

public record UpdateRattachementArticleCommand(
    Ulid RattachementArticleId,
    Ulid RattachementId,
    string CodeArticle,
    string? Libelle,
    decimal? Quantite,
    decimal? PrixRevient,
    decimal? TauxTVA,
    string? CodeUnite,
    string? CodeEntrepot,
    string? TypeRattachement,
    string? NumeroBonLivraison,
    DateTime? DateBonLivraison,
    bool IsActive
) : IRequest<UpdateRattachementArticleResponse>;
