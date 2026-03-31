namespace CollectManagement.Application.Features.RattachementArticles.Commands.CreateRattachementArticle;

public record CreateRattachementArticleCommand(
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
    bool IsActive,
    Ulid SocieteId
) : IRequest<CreateRattachementArticleResponse>;
