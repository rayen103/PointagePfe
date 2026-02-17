using System.ComponentModel;
using CollectManagement.Application.Interfaces.Repositories.PointsCollecte;
using CollectManagement.Domain.PointsCollecte;
using CollectManagement.Domain.PointsCollecte.ValueObjects;
using CollectManagement.Infrastructure.Persistence.Context;

namespace CollectManagement.Infrastructure.Persistence.Repositories.PointCollecteRepositories;

public class PointCollecteRepository : RepositoryBase<PointCollecte>, IPointCollecteRepository
{
    public PointCollecteRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<(IReadOnlyList<PointCollecte>, int)> GetPagedListAsync(
        string? search,
        string? sort,
        string? order,
        int page,
        int size,
        CancellationToken cancellationToken)
    {
        var where = _dbSet.Where(w =>
            string.IsNullOrWhiteSpace(search) ||
            w.CodePointCollecte.Contains(search) ||
            w.LibellePointCollecte.Contains(search) ||
            (w.CodeGouvernorat != null && w.CodeGouvernorat.Contains(search)) ||
            (w.CodeRegion != null && w.CodeRegion.Contains(search))
        );

        var orderBy = where
            .OrderByDescending(o => o.CodePointCollecte);

        var prop = TypeDescriptor
            .GetProperties(typeof(PointCollecte))
            .Find(sort ?? "CodePointCollecte", true);

        if (prop is not null && order == "asc")
            orderBy = where.OrderBy(o =>
                EF.Property<PointCollecte>(o, prop.Name));

        if (prop is not null && order == "desc")
            orderBy = where.OrderByDescending(o =>
                EF.Property<PointCollecte>(o, prop.Name));

        var countAsync = await where
            .CountAsync(cancellationToken)
            .ConfigureAwait(false);

        var readOnlyList = await orderBy
            .Skip((page - 1) * size)
            .Take(size)
            .Select(c => PointCollecte.QueryCreate(
                c.PointCollecteId,
                c.CodePointCollecte,
                c.LibellePointCollecte,
                c.Latitude,
                c.Longitude,
                c.CodeGouvernorat,
                c.CodeRegion,
                c.IsActive,
                c.SocieteId
            ))
            .ToListAsync(cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return (readOnlyList, countAsync);
    }

    public async Task<PointCollecte> GetOneAsync(
        PointCollecteId pointCollecteId,
        CancellationToken cancellationToken)
    {
        var pointCollecte = await _dbSet
            .Where(w => w.PointCollecteId == pointCollecteId)
            .Select(c => PointCollecte.QueryCreate(
                c.PointCollecteId,
                c.CodePointCollecte,
                c.LibellePointCollecte,
                c.Latitude,
                c.Longitude,
                c.CodeGouvernorat,
                c.CodeRegion,
                c.IsActive,
                c.SocieteId
            ))
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        return pointCollecte!;
    }

    public async Task UpdateBulkAsync(PointCollecte pointCollecte, CancellationToken cancellationToken)
    {
        await _dbSet
            .Where(w => w.PointCollecteId == pointCollecte.PointCollecteId)
            .ExecuteUpdateAsync(s => s
                    .SetProperty(p => p.CodePointCollecte, pointCollecte.CodePointCollecte)
                    .SetProperty(p => p.LibellePointCollecte, pointCollecte.LibellePointCollecte)
                    .SetProperty(p => p.Latitude, pointCollecte.Latitude)
                    .SetProperty(p => p.Longitude, pointCollecte.Longitude)
                    .SetProperty(p => p.CodeGouvernorat, pointCollecte.CodeGouvernorat)
                    .SetProperty(p => p.CodeRegion, pointCollecte.CodeRegion)
                    .SetProperty(p => p.IsActive, pointCollecte.IsActive),
                cancellationToken)
            .ConfigureAwait(false);
    }
}
