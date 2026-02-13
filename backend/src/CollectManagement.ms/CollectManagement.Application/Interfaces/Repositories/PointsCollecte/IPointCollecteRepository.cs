using CollectManagement.Domain.PointsCollecte;
using CollectManagement.Domain.PointsCollecte.ValueObjects;

namespace CollectManagement.Application.Interfaces.Repositories.PointsCollecte;

public interface IPointCollecteRepository : IRepositoryBase<PointCollecte>
{
    Task<(IReadOnlyList<PointCollecte>, int)> GetPagedListAsync(
        string? search,
        string? sort,
        string? order,
        int page,
        int size,
        CancellationToken cancellationToken
    );
    
    Task<PointCollecte> GetOneAsync(
        PointCollecteId pointCollecteId,
        CancellationToken cancellationToken
    );
    
    Task UpdateBulkAsync(PointCollecte pointCollecte, CancellationToken cancellationToken);
}
