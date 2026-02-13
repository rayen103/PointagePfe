using CollectManagement.Domain.OrdresTravail;
using CollectManagement.Domain.OrdresTravail.ValueObjects;

namespace CollectManagement.Application.Interfaces.Repositories.OrdresTravail;

public interface IOrdreTravailRepository : IRepositoryBase<OrdreTravail>
{
    Task<(IReadOnlyList<OrdreTravail>, int)> GetPagedListAsync(
        string? search,
        string? sort,
        string? order,
        int page,
        int size,
        CancellationToken cancellationToken
    );
    
    Task<OrdreTravail> GetOneAsync(
        OrdreTravailId ordreTravailId,
        CancellationToken cancellationToken
    );
    
    Task UpdateBulkAsync(OrdreTravail ordreTravail, CancellationToken cancellationToken);
}
