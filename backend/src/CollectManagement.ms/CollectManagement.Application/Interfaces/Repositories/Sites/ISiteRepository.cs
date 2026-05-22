using CollectManagement.Domain.Sites;
using CollectManagement.Domain.Sites.ValueObjects;

namespace CollectManagement.Application.Interfaces.Repositories.Sites;

public interface ISiteRepository : IRepositoryBase<Site>
{
    Task<(IReadOnlyList<Site>, int)> GetPagedListAsync(
        string? search,
        string? sort,
        string? order,
        int page,
        int size,
        Ulid? societeId,
        CancellationToken cancellationToken);

    Task<Site> GetOneAsync(SiteId siteId, CancellationToken cancellationToken);
    Task UpdateBulkAsync(Site site, CancellationToken cancellationToken);
}
