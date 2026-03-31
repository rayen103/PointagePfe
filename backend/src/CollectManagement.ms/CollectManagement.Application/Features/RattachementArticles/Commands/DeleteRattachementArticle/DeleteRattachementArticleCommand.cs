namespace CollectManagement.Application.Features.RattachementArticles.Commands.DeleteRattachementArticle;

public record DeleteRattachementArticleCommand(Ulid RattachementArticleId) : IRequest<Unit>;
