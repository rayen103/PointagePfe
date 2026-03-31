using CollectManagement.Domain.OrdresTravail;
using CollectManagement.Domain.OrdresTravail.ValueObjects;

namespace CollectManagement.Application.Interfaces.Repositories.OrdresTravailDetails;

public interface IOrdreTravailDetailRepository : IRepositoryBase<OrdreTravailDetail>
{
    Task<IReadOnlyList<OrdreTravailDetail>> GetByOrdreTravailAsync(
        OrdreTravailId ordreTravailId,
        CancellationToken ct);

    Task<OrdreTravailDetail> GetOneAsync(
        OrdreTravailDetailId id,
        CancellationToken ct);

    Task UpdateBulkAsync(OrdreTravailDetail entity, CancellationToken ct);
}
