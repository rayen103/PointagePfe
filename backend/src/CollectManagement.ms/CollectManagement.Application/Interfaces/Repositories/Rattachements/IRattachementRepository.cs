using CollectManagement.Domain.Rattachements;
using CollectManagement.Domain.Rattachements.ValueObjects;

namespace CollectManagement.Application.Interfaces.Repositories.Rattachements;

public interface IRattachementRepository : IRepositoryBase<Rattachement>
{
    Task<(IReadOnlyList<Rattachement>, int)> GetPagedListAsync(
        string? search,
        string? sort,
        string? order,
        int page,
        int size,
        CancellationToken cancellationToken
    );
    
    Task<Rattachement> GetOneAsync(
        RattachementId rattachementId,
        CancellationToken cancellationToken
    );
    
    Task UpdateBulkAsync(Rattachement rattachement, CancellationToken cancellationToken);
}
