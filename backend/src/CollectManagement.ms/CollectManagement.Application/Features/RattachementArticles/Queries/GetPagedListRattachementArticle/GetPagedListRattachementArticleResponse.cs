namespace CollectManagement.Application.Features.RattachementArticles.Queries.GetPagedListRattachementArticle;

public class GetPagedListRattachementArticleResponse
{
    public IReadOnlyList<GetPagedListRattachementArticleDto> RattachementArticles { get; set; } = [];
    public int TotalCount { get; set; }
}
