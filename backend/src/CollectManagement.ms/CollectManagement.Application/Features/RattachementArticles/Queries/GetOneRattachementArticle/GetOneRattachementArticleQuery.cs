namespace CollectManagement.Application.Features.RattachementArticles.Queries.GetOneRattachementArticle;

public record GetOneRattachementArticleQuery(Ulid RattachementArticleId)
    : IRequest<GetOneRattachementArticleDto>;
