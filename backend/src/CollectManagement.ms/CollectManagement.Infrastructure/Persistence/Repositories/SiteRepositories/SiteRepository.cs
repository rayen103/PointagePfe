using System.ComponentModel;
using CollectManagement.Application.Interfaces.Repositories.Sites;
using CollectManagement.Domain.Sites;
using CollectManagement.Domain.Sites.ValueObjects;
using CollectManagement.Domain.Societes.ValueObjects;
using CollectManagement.Infrastructure.Persistence.Context;

namespace CollectManagement.Infrastructure.Persistence.Repositories.SiteRepositories;

public class SiteRepository : RepositoryBase<Site>, ISiteRepository
{
    public SiteRepository(ApplicationDbContext dbContext) : base(dbContext) { }

    public async Task<(IReadOnlyList<Site>, int)> GetPagedListAsync(
        string? search, string? sort, string? order, int page, int size, Ulid? societeId, CancellationToken cancellationToken)
    {
        SocieteId? filterSocieteId = societeId.HasValue ? new SocieteId(societeId.Value) : null;

        var where = _dbSet.Where(w =>
            (filterSocieteId == null || w.SocieteId == filterSocieteId) &&
            (string.IsNullOrWhiteSpace(search) ||
             w.Code.Contains(search) ||
             w.LibelleSite.Contains(search)));

        var orderBy = where.OrderByDescending(o => o.Code);

        var prop = TypeDescriptor.GetProperties(typeof(Site)).Find(sort ?? "Code", true);
        if (prop is not null && order == "asc")
            orderBy = where.OrderBy(o => EF.Property<Site>(o, prop.Name));
        if (prop is not null && order == "desc")
            orderBy = where.OrderByDescending(o => EF.Property<Site>(o, prop.Name));

        var totalCount = await where.CountAsync(cancellationToken).ConfigureAwait(false);
        var list = await orderBy
            .Skip((page - 1) * size).Take(size)
            .Select(c => Site.QueryCreate(c.SiteId, c.Code, c.LibelleSite, c.Siege, c.Longitude, c.Latitude, c.Rayon, c.TimeMinute, c.IsActive, c.SocieteId))
            .ToListAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        return (list, totalCount);
    }

    public async Task<Site> GetOneAsync(SiteId siteId, CancellationToken cancellationToken)
    {
        var site = await _dbSet.Where(w => w.SiteId == siteId)
            .Select(c => Site.QueryCreate(c.SiteId, c.Code, c.LibelleSite, c.Siege, c.Longitude, c.Latitude, c.Rayon, c.TimeMinute, c.IsActive, c.SocieteId))
            .FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);

        return site!;
    }

    public async Task UpdateBulkAsync(Site site, CancellationToken cancellationToken)
    {
        await _dbSet.Where(w => w.SiteId == site.SiteId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(p => p.Code, site.Code)
                .SetProperty(p => p.LibelleSite, site.LibelleSite)
                .SetProperty(p => p.Siege, site.Siege)
                .SetProperty(p => p.Longitude, site.Longitude)
                .SetProperty(p => p.Latitude, site.Latitude)
                .SetProperty(p => p.Rayon, site.Rayon)
                .SetProperty(p => p.TimeMinute, site.TimeMinute)
                .SetProperty(p => p.IsActive, site.IsActive),
                cancellationToken)
            .ConfigureAwait(false);
    }
}
