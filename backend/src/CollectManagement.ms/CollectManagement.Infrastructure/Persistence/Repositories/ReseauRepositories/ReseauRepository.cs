using System.ComponentModel;
using CollectManagement.Application.Interfaces.Repositories.Reseaux;
using CollectManagement.Domain.Reseaux;
using CollectManagement.Domain.Reseaux.ValueObjects;
using CollectManagement.Domain.Societes.ValueObjects;
using CollectManagement.Infrastructure.Persistence.Context;

namespace CollectManagement.Infrastructure.Persistence.Repositories.ReseauRepositories;

public class ReseauRepository : RepositoryBase<Reseau>, IReseauRepository
{
    public ReseauRepository(ApplicationDbContext dbContext) : base(dbContext) { }

    public async Task<(IReadOnlyList<Reseau>, int)> GetPagedListAsync(
        string? search, string? sort, string? order, int page, int size, Ulid? societeId, CancellationToken cancellationToken)
    {
        SocieteId? filterSocieteId = societeId.HasValue ? new SocieteId(societeId.Value) : null;

        var where = _dbSet.Where(w =>
            (filterSocieteId == null || w.SocieteId == filterSocieteId) &&
            (string.IsNullOrWhiteSpace(search) ||
             w.IpAddress.Contains(search) ||
             w.Port.ToString().Contains(search)));

        var orderBy = where.OrderByDescending(o => o.IpAddress);

        var prop = TypeDescriptor.GetProperties(typeof(Reseau)).Find(sort ?? "IpAddress", true);
        if (prop is not null && order == "asc")
            orderBy = where.OrderBy(o => EF.Property<Reseau>(o, prop.Name));
        if (prop is not null && order == "desc")
            orderBy = where.OrderByDescending(o => EF.Property<Reseau>(o, prop.Name));

        var totalCount = await where.CountAsync(cancellationToken).ConfigureAwait(false);
        var list = await orderBy
            .Skip((page - 1) * size).Take(size)
            .Select(c => Reseau.QueryCreate(c.ReseauId, c.IpAddress, c.Port, c.GmtPlus, c.Latitude, c.Longitude, c.Rayon, c.TimeToleranceMinute, c.IsActive, c.SocieteId))
            .ToListAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        return (list, totalCount);
    }

    public async Task<Reseau> GetOneAsync(ReseauId reseauId, CancellationToken cancellationToken)
    {
        var reseau = await _dbSet.Where(w => w.ReseauId == reseauId)
            .Select(c => Reseau.QueryCreate(c.ReseauId, c.IpAddress, c.Port, c.GmtPlus, c.Latitude, c.Longitude, c.Rayon, c.TimeToleranceMinute, c.IsActive, c.SocieteId))
            .FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);

        return reseau!;
    }

    public async Task UpdateBulkAsync(Reseau reseau, CancellationToken cancellationToken)
    {
        await _dbSet.Where(w => w.ReseauId == reseau.ReseauId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(p => p.IpAddress, reseau.IpAddress)
                .SetProperty(p => p.Port, reseau.Port)
                .SetProperty(p => p.GmtPlus, reseau.GmtPlus)
                .SetProperty(p => p.Latitude, reseau.Latitude)
                .SetProperty(p => p.Longitude, reseau.Longitude)
                .SetProperty(p => p.Rayon, reseau.Rayon)
                .SetProperty(p => p.TimeToleranceMinute, reseau.TimeToleranceMinute)
                .SetProperty(p => p.IsActive, reseau.IsActive),
                cancellationToken)
            .ConfigureAwait(false);
    }
}
