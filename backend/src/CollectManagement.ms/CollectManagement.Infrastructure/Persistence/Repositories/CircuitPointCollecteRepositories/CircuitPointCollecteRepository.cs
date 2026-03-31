using CollectManagement.Application.Interfaces.Repositories.CircuitsPointsCollecte;
using CollectManagement.Domain.Circuits;
using CollectManagement.Domain.Circuits.ValueObjects;
using CollectManagement.Infrastructure.Persistence.Context;

namespace CollectManagement.Infrastructure.Persistence.Repositories.CircuitPointCollecteRepositories;

public class CircuitPointCollecteRepository : RepositoryBase<CircuitPointCollecte>, ICircuitPointCollecteRepository
{
    public CircuitPointCollecteRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IReadOnlyList<CircuitPointCollecte>> GetByCircuitAsync(
        CircuitId circuitId,
        CancellationToken ct)
    {
        return await _dbSet
            .Where(w => w.CircuitId == circuitId)
            .OrderBy(o => o.Ordre)
            .Select(c => CircuitPointCollecte.QueryCreate(
                c.CircuitPointCollecteId,
                c.CircuitId,
                c.CodePointCollecte,
                c.LibellePointCollecte,
                c.Latitude,
                c.Longitude,
                c.Ordre
            ))
            .ToListAsync(ct)
            .ConfigureAwait(false);
    }

    public async Task<CircuitPointCollecte> GetOneAsync(
        CircuitPointCollecteId id,
        CancellationToken ct)
    {
        var entity = await _dbSet
            .Where(w => w.CircuitPointCollecteId == id)
            .Select(c => CircuitPointCollecte.QueryCreate(
                c.CircuitPointCollecteId,
                c.CircuitId,
                c.CodePointCollecte,
                c.LibellePointCollecte,
                c.Latitude,
                c.Longitude,
                c.Ordre
            ))
            .FirstOrDefaultAsync(ct)
            .ConfigureAwait(false);

        return entity!;
    }

    public async Task UpdateBulkAsync(CircuitPointCollecte entity, CancellationToken ct)
    {
        await _dbSet
            .Where(w => w.CircuitPointCollecteId == entity.CircuitPointCollecteId)
            .ExecuteUpdateAsync(s => s
                    .SetProperty(p => p.CodePointCollecte, entity.CodePointCollecte)
                    .SetProperty(p => p.LibellePointCollecte, entity.LibellePointCollecte)
                    .SetProperty(p => p.Latitude, entity.Latitude)
                    .SetProperty(p => p.Longitude, entity.Longitude)
                    .SetProperty(p => p.Ordre, entity.Ordre),
                ct)
            .ConfigureAwait(false);
    }
}
