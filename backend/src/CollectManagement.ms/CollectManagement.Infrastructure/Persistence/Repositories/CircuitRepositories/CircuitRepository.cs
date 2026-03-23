using System.ComponentModel;
using CollectManagement.Application.Interfaces.Repositories.Circuits;
using CollectManagement.Domain.Circuits;
using CollectManagement.Domain.Circuits.ValueObjects;
using CollectManagement.Infrastructure.Persistence.Context;

namespace CollectManagement.Infrastructure.Persistence.Repositories.CircuitRepositories;

public class CircuitRepository : RepositoryBase<Circuit>, ICircuitRepository
{
    public CircuitRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<(IReadOnlyList<Circuit>, int)> GetPagedListAsync(
        string? search,
        string? sort,
        string? order,
        int page,
        int size,
        CancellationToken cancellationToken)
    {
        var where = _dbSet.Where(w =>
            string.IsNullOrWhiteSpace(search) ||
            w.CodeCircuit.Contains(search) ||
            (w.LibelleCircuit != null && w.LibelleCircuit.Contains(search)) ||
            (w.Description != null && w.Description.Contains(search))
        );

        var orderBy = where
            .OrderByDescending(o => o.CodeCircuit);

        var prop = TypeDescriptor
            .GetProperties(typeof(Circuit))
            .Find(sort ?? "CodeCircuit", true);

        if (prop is not null && order == "asc")
            orderBy = where.OrderBy(o =>
                EF.Property<Circuit>(o, prop.DisplayName));

        if (prop is not null && order == "desc")
            orderBy = where.OrderByDescending(o =>
                EF.Property<Circuit>(o, prop.DisplayName));

        var countAsync = await where
            .CountAsync(cancellationToken)
            .ConfigureAwait(false);

        var readOnlyList = await orderBy
            .Skip((page - 1) * size)
            .Take(size)
            .Select(c => Circuit.QueryCreate(
                c.CircuitId,
                c.CodeCircuit,
                c.LibelleCircuit,
                c.Description,
                c.IsActive,
                c.SocieteId,
                c.Latitude,
                c.Longitude,
                c.CodePCDepart,
                c.CodePCArrivee,
                c.DistanceKm,
                c.DureeMinutes,
                c.Couleur
            ))
            .ToListAsync(cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return (readOnlyList, countAsync);
    }

    public async Task<Circuit> GetOneAsync(
        CircuitId circuitId,
        CancellationToken cancellationToken)
    {
        var circuit = await _dbSet
            .Where(w => w.CircuitId == circuitId)
            .Select(c => Circuit.QueryCreate(
                c.CircuitId,
                c.CodeCircuit,
                c.LibelleCircuit,
                c.Description,
                c.IsActive,
                c.SocieteId,
                c.Latitude,
                c.Longitude,
                c.CodePCDepart,
                c.CodePCArrivee,
                c.DistanceKm,
                c.DureeMinutes,
                c.Couleur
            ))
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        return circuit!;
    }

    public async Task UpdateBulkAsync(Circuit circuit, CancellationToken cancellationToken)
    {
        await _dbSet
            .Where(w => w.CircuitId == circuit.CircuitId)
            .ExecuteUpdateAsync(s => s
                    .SetProperty(p => p.CodeCircuit, circuit.CodeCircuit)
                    .SetProperty(p => p.LibelleCircuit, circuit.LibelleCircuit)
                    .SetProperty(p => p.Description, circuit.Description)
                    .SetProperty(p => p.Latitude, circuit.Latitude)
                    .SetProperty(p => p.Longitude, circuit.Longitude)
                    .SetProperty(p => p.IsActive, circuit.IsActive)
                    .SetProperty(p => p.CodePCDepart, circuit.CodePCDepart)
                    .SetProperty(p => p.CodePCArrivee, circuit.CodePCArrivee)
                    .SetProperty(p => p.DistanceKm, circuit.DistanceKm)
                    .SetProperty(p => p.DureeMinutes, circuit.DureeMinutes)
                    .SetProperty(p => p.Couleur, circuit.Couleur),
                cancellationToken)
            .ConfigureAwait(false);
    }
}
