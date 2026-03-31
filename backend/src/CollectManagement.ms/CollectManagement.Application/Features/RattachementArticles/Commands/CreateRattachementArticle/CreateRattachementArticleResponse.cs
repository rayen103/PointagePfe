namespace CollectManagement.Application.Features.RattachementArticles.Commands.CreateRattachementArticle;

public class CreateRattachementArticleResponse
{
    public Ulid RattachementArticleId { get; set; }
    public Ulid RattachementId { get; set; }
    public string CodeArticle { get; set; } = string.Empty;
    public Ulid SocieteId { get; set; }
}
