namespace CollectManagement.Application.Features.RattachementArticles.Queries.GetPagedListRattachementArticle;

public record GetPagedListRattachementArticleQuery(
    string? Search,
    string? Sort,
    string? Order,
    int Page,
    int Size
) : IRequest<GetPagedListRattachementArticleResponse>;
