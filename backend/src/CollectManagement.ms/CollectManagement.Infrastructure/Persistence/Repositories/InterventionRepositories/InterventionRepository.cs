using System.ComponentModel;
using CollectManagement.Application.Interfaces.Repositories.Interventions;
using CollectManagement.Domain.Interventions;
using CollectManagement.Domain.Interventions.ValueObjects;
using CollectManagement.Infrastructure.Persistence.Context;

namespace CollectManagement.Infrastructure.Persistence.Repositories.InterventionRepositories;

public class InterventionRepository : RepositoryBase<Intervention>, IInterventionRepository
{
    public InterventionRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<(IReadOnlyList<Intervention>, int)> GetPagedListAsync(
        string? search,
        string? sort,
        string? order,
        int page,
        int size,
        CancellationToken cancellationToken)
    {
        var where = _dbSet.Where(w =>
            string.IsNullOrWhiteSpace(search) ||
            w.NumeroIntervention.Contains(search) ||
            (w.Description != null && w.Description.Contains(search)) ||
            (w.TypeIntervention != null && w.TypeIntervention.Contains(search)) ||
            (w.Statut != null && w.Statut.Contains(search))
        );

        var orderBy = where
            .OrderByDescending(o => o.DateIntervention);

        var prop = TypeDescriptor
            .GetProperties(typeof(Intervention))
            .Find(sort ?? "DateIntervention", true);

        if (prop is not null && order == "asc")
            orderBy = where.OrderBy(o =>
                EF.Property<Intervention>(o, prop.DisplayName));

        if (prop is not null && order == "desc")
            orderBy = where.OrderByDescending(o =>
                EF.Property<Intervention>(o, prop.DisplayName));

        var countAsync = await where
            .CountAsync(cancellationToken)
            .ConfigureAwait(false);

        var readOnlyList = await orderBy
            .Skip((page - 1) * size)
            .Take(size)
            .Select(c => Intervention.QueryCreate(
                c.InterventionId,
                c.NumeroIntervention,
                c.Description,
                c.DateIntervention,
                c.TypeIntervention,
                c.Statut,
                c.Cout
            ))
            .ToListAsync(cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return (readOnlyList, countAsync);
    }

    public Task<Intervention> GetOneAsync(InterventionId interventionId, CancellationToken cancellationToken)
    {
        return _dbSet.Where(w => w.InterventionId.Equals(interventionId))
            .FirstOrDefaultAsync(cancellationToken);
    }
}
