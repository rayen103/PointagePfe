namespace CollectManagement.Application.Features.RattachementArticles.Commands.UpdateRattachementArticle;

public class UpdateRattachementArticleResponse
{
    public Ulid RattachementArticleId { get; set; }
    public Ulid RattachementId { get; set; }
    public string CodeArticle { get; set; } = string.Empty;
}
