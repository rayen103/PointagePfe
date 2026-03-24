using CollectManagement.Domain.Rattachements;
using CollectManagement.Domain.Rattachements.ValueObjects;

namespace CollectManagement.Application.Interfaces.Repositories.Rattachements;

public interface IRattachementEmployeRepository : IRepositoryBase<RattachementEmploye>
{
    Task<(IReadOnlyList<RattachementEmploye>, int)> GetPagedListAsync(
        string? search,
        string? sort,
        string? order,
        int page,
        int size,
        CancellationToken cancellationToken
    );

    Task<RattachementEmploye> GetOneAsync(
        RattachementEmployeId rattachementEmployeId,
        CancellationToken cancellationToken
    );

    Task UpdateBulkAsync(RattachementEmploye rattachementEmploye, CancellationToken cancellationToken);
}
