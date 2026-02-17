using System.ComponentModel;
using CollectManagement.Application.Interfaces.Repositories.Rattachements;
using CollectManagement.Domain.Rattachements;
using CollectManagement.Domain.Rattachements.ValueObjects;
using CollectManagement.Infrastructure.Persistence.Context;

namespace CollectManagement.Infrastructure.Persistence.Repositories.RattachementRepositories;

public class RattachementRepository : RepositoryBase<Rattachement>, IRattachementRepository
{
    public RattachementRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<(IReadOnlyList<Rattachement>, int)> GetPagedListAsync(
        string? search,
        string? sort,
        string? order,
        int page,
        int size,
        CancellationToken cancellationToken)
    {
        var where = _dbSet.Where(w =>
            string.IsNullOrWhiteSpace(search) ||
            w.NumeroRattachement.Contains(search) ||
            (w.NumeroChantier != null && w.NumeroChantier.Contains(search)) ||
            (w.CodeClient != null && w.CodeClient.Contains(search)) ||
            (w.Responsable != null && w.Responsable.Contains(search)) ||
            (w.Reference != null && w.Reference.Contains(search))
        );

        var orderBy = where
            .OrderByDescending(o => o.DateRattachement);

        var prop = TypeDescriptor
            .GetProperties(typeof(Rattachement))
            .Find(sort ?? "DateRattachement", true);

        if (prop is not null && order == "asc")
            orderBy = where.OrderBy(o =>
                EF.Property<Rattachement>(o, prop.Name));

        if (prop is not null && order == "desc")
            orderBy = where.OrderByDescending(o =>
                EF.Property<Rattachement>(o, prop.Name));

        var countAsync = await where
            .CountAsync(cancellationToken)
            .ConfigureAwait(false);

        var readOnlyList = await orderBy
            .Skip((page - 1) * size)
            .Take(size)
            .Select(c => Rattachement.QueryCreate(
                c.RattachementId,
                c.NumeroRattachement,
                c.Exercice,
                c.DateRattachement,
                c.NumeroChantier,
                c.CodeClient,
                c.IsInternal,
                c.Cout,
                c.Type,
                c.Nature,
                c.Responsable,
                c.HeureDebut,
                c.HeureFin,
                c.Emplacement,
                c.Reference,
                c.Status,
                c.DateCloture,
                c.Remarque,
                c.IsActive,
                c.SocieteId
            ))
            .ToListAsync(cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return (readOnlyList, countAsync);
    }

    public async Task<Rattachement> GetOneAsync(
        RattachementId rattachementId,
        CancellationToken cancellationToken)
    {
        var rattachement = await _dbSet
            .Where(w => w.RattachementId == rattachementId)
            .Select(c => Rattachement.QueryCreate(
                c.RattachementId,
                c.NumeroRattachement,
                c.Exercice,
                c.DateRattachement,
                c.NumeroChantier,
                c.CodeClient,
                c.IsInternal,
                c.Cout,
                c.Type,
                c.Nature,
                c.Responsable,
                c.HeureDebut,
                c.HeureFin,
                c.Emplacement,
                c.Reference,
                c.Status,
                c.DateCloture,
                c.Remarque,
                c.IsActive,
                c.SocieteId
            ))
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        return rattachement!;
    }

    public async Task UpdateBulkAsync(Rattachement rattachement, CancellationToken cancellationToken)
    {
        await _dbSet
            .Where(w => w.RattachementId == rattachement.RattachementId)
            .ExecuteUpdateAsync(s => s
                    .SetProperty(p => p.NumeroRattachement, rattachement.NumeroRattachement)
                    .SetProperty(p => p.Exercice, rattachement.Exercice)
                    .SetProperty(p => p.DateRattachement, rattachement.DateRattachement)
                    .SetProperty(p => p.NumeroChantier, rattachement.NumeroChantier)
                    .SetProperty(p => p.CodeClient, rattachement.CodeClient)
                    .SetProperty(p => p.IsInternal, rattachement.IsInternal)
                    .SetProperty(p => p.Cout, rattachement.Cout)
                    .SetProperty(p => p.Type, rattachement.Type)
                    .SetProperty(p => p.Nature, rattachement.Nature)
                    .SetProperty(p => p.Responsable, rattachement.Responsable)
                    .SetProperty(p => p.HeureDebut, rattachement.HeureDebut)
                    .SetProperty(p => p.HeureFin, rattachement.HeureFin)
                    .SetProperty(p => p.Emplacement, rattachement.Emplacement)
                    .SetProperty(p => p.Reference, rattachement.Reference)
                    .SetProperty(p => p.Status, rattachement.Status)
                    .SetProperty(p => p.DateCloture, rattachement.DateCloture)
                    .SetProperty(p => p.Remarque, rattachement.Remarque)
                    .SetProperty(p => p.IsActive, rattachement.IsActive),
                cancellationToken)
            .ConfigureAwait(false);
    }
}
