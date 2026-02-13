using CollectManagement.Domain.Circuits;
using CollectManagement.Domain.Circuits.ValueObjects;

namespace CollectManagement.Application.Interfaces.Repositories.Circuits;

public interface ICircuitRepository : IRepositoryBase<Circuit>
{
    Task<(IReadOnlyList<Circuit>, int)> GetPagedListAsync(
        string? search,
        string? sort,
        string? order,
        int page,
        int size,
        CancellationToken cancellationToken
    );
    
    Task<Circuit> GetOneAsync(
        CircuitId circuitId,
        CancellationToken cancellationToken
    );
    
    Task UpdateBulkAsync(Circuit circuit, CancellationToken cancellationToken);
}
