using CollectManagement.Domain.Circuits;
using CollectManagement.Domain.Circuits.ValueObjects;

namespace CollectManagement.Application.Interfaces.Repositories.CircuitsPointsCollecte;

public interface ICircuitPointCollecteRepository : IRepositoryBase<CircuitPointCollecte>
{
    Task<IReadOnlyList<CircuitPointCollecte>> GetByCircuitAsync(
        CircuitId circuitId,
        CancellationToken ct);

    Task<CircuitPointCollecte> GetOneAsync(
        CircuitPointCollecteId id,
        CancellationToken ct);

    Task UpdateBulkAsync(CircuitPointCollecte entity, CancellationToken ct);
}
