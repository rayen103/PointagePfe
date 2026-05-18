using System.ComponentModel;
using CollectManagement.Application.Interfaces.Repositories.Bus;
using CollectManagement.Domain.Bus;
using CollectManagement.Domain.Bus.ValueObjects;
using CollectManagement.Infrastructure.Persistence.Context;

namespace CollectManagement.Infrastructure.Persistence.Repositories.BusRepositories;

public class BusRepository : RepositoryBase<Bus>, IBusRepository
{
    public BusRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<(IReadOnlyList<Bus>, int)> GetPagedListAsync(
        string? search,
        string? sort,
        string? order,
        int page,
        int size,
        CancellationToken cancellationToken)
    {
        var where = _dbSet.Where(w =>
            string.IsNullOrWhiteSpace(search) ||
            w.NumeroIMM.Contains(search) ||
            (w.ModelBus != null && w.ModelBus.Contains(search))
        );

        var orderBy = where
            .OrderByDescending(o => o.NumeroIMM);

        var prop = TypeDescriptor
            .GetProperties(typeof(Bus))
            .Find(sort ?? "NumeroIMM", true);

        if (prop is not null && order == "asc")
            orderBy = where.OrderBy(o =>
                EF.Property<Bus>(o, prop.Name));

        if (prop is not null && order == "desc")
            orderBy = where.OrderByDescending(o =>
                EF.Property<Bus>(o, prop.Name));

        var countAsync = await where
            .CountAsync(cancellationToken)
            .ConfigureAwait(false);

        var readOnlyList = await orderBy
            .Skip((page - 1) * size)
            .Take(size)
            .Select(c => Bus.QueryCreate(
                c.BusId,
                c.NumeroIMM,
                c.ModelBus,
                c.IMEI,
                c.Capacite,
                c.CodeCircuit,
                c.CodeChauffeur,
                c.AppSagem,
                c.IsActive,
                c.Latitude,
                c.Longitude,
                c.SocieteId
            ))
            .ToListAsync(cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return (readOnlyList, countAsync);
    }

    public async Task<Bus> GetOneAsync(
        BusId busId,
        CancellationToken cancellationToken)
    {
        var bus = await _dbSet
            .Where(w => w.BusId == busId)
            .Select(c => Bus.QueryCreate(
                c.BusId,
                c.NumeroIMM,
                c.ModelBus,
                c.IMEI,
                c.Capacite,
                c.CodeCircuit,
                c.CodeChauffeur,
                c.AppSagem,
                c.IsActive,
                c.Latitude,
                c.Longitude,
                c.SocieteId
            ))
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        return bus!;
    }

    public async Task UpdateBulkAsync(Bus bus, CancellationToken cancellationToken)
    {
        await _dbSet
            .Where(w => w.BusId == bus.BusId)
            .ExecuteUpdateAsync(s => s
                    .SetProperty(p => p.NumeroIMM, bus.NumeroIMM)
                    .SetProperty(p => p.ModelBus, bus.ModelBus)
                    .SetProperty(p => p.IMEI, bus.IMEI)
                    .SetProperty(p => p.Capacite, bus.Capacite)
                    .SetProperty(p => p.CodeCircuit, bus.CodeCircuit)
                    .SetProperty(p => p.CodeChauffeur, bus.CodeChauffeur)
                    .SetProperty(p => p.AppSagem, bus.AppSagem)
                    .SetProperty(p => p.IsActive, bus.IsActive)
                    .SetProperty(p => p.Latitude, bus.Latitude)
                    .SetProperty(p => p.Longitude, bus.Longitude),
                cancellationToken)
            .ConfigureAwait(false);
    }
}
