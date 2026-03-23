using BusEntity = CollectManagement.Domain.Bus.Bus;
using CollectManagement.Domain.Bus.ValueObjects;

namespace CollectManagement.Application.Interfaces.Repositories.Bus;

public interface IBusRepository : IRepositoryBase<BusEntity>
{
    Task<(IReadOnlyList<BusEntity>, int)> GetPagedListAsync(
        string? search,
        string? sort,
        string? order,
        int page,
        int size,
        CancellationToken cancellationToken
    );

    Task<BusEntity> GetOneAsync(
        BusId busId,
        CancellationToken cancellationToken
    );

    Task UpdateBulkAsync(BusEntity bus, CancellationToken cancellationToken);
}
