using CollectManagement.Domain.Rattachements;
using CollectManagement.Domain.Rattachements.ValueObjects;

namespace CollectManagement.Application.Interfaces.Repositories.Rattachements;

public interface IRattachementArticleRepository : IRepositoryBase<RattachementArticle>
{
    Task<(IReadOnlyList<RattachementArticle>, int)> GetPagedListAsync(
        string? search,
        string? sort,
        string? order,
        int page,
        int size,
        CancellationToken cancellationToken
    );

    Task<RattachementArticle> GetOneAsync(
        RattachementArticleId rattachementArticleId,
        CancellationToken cancellationToken
    );

    Task UpdateBulkAsync(RattachementArticle rattachementArticle, CancellationToken cancellationToken);
}
