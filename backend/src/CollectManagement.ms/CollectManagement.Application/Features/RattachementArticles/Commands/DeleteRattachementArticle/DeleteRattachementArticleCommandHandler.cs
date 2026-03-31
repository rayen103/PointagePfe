using CollectManagement.Application.Interfaces.Repositories.Rattachements;
using CollectManagement.Domain.Rattachements.ValueObjects;

namespace CollectManagement.Application.Features.RattachementArticles.Commands.DeleteRattachementArticle;

public class DeleteRattachementArticleCommandHandler
    : IRequestHandler<DeleteRattachementArticleCommand, Unit>
{
    private readonly IRattachementArticleRepository _rattachementArticleRepository;

    public DeleteRattachementArticleCommandHandler(
        IRattachementArticleRepository rattachementArticleRepository)
    {
        _rattachementArticleRepository = rattachementArticleRepository;
    }

    public async Task<Unit> Handle(
        DeleteRattachementArticleCommand request,
        CancellationToken cancellationToken)
    {
        var rattachementArticleId = new RattachementArticleId(request.RattachementArticleId);

        await _rattachementArticleRepository
            .DeleteAsync(c => c.RattachementArticleId == rattachementArticleId, cancellationToken)
            .ConfigureAwait(false);

        return Unit.Value;
    }
}
