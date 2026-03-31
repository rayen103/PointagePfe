using System.ComponentModel;
using CollectManagement.Application.Interfaces.Repositories.Rattachements;
using CollectManagement.Domain.Rattachements;
using CollectManagement.Domain.Rattachements.ValueObjects;
using CollectManagement.Infrastructure.Persistence.Context;

namespace CollectManagement.Infrastructure.Persistence.Repositories.RattachementRepositories;

public class RattachementEmployeRepository : RepositoryBase<RattachementEmploye>, IRattachementEmployeRepository
{
    public RattachementEmployeRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<(IReadOnlyList<RattachementEmploye>, int)> GetPagedListAsync(
        string? search,
        string? sort,
        string? order,
        int page,
        int size,
        CancellationToken cancellationToken)
    {
        var where = _dbSet.Where(w =>
            string.IsNullOrWhiteSpace(search) ||
            w.Matricule.Contains(search) ||
            (w.NomPrenom != null && w.NomPrenom.Contains(search)) ||
            (w.TypeRattachement != null && w.TypeRattachement.Contains(search))
        );

        var orderBy = where.OrderByDescending(o => o.Matricule);

        var prop = TypeDescriptor
            .GetProperties(typeof(RattachementEmploye))
            .Find(sort ?? "Matricule", true);

        if (prop is not null && order == "asc")
            orderBy = where.OrderBy(o => EF.Property<RattachementEmploye>(o, prop.Name));

        if (prop is not null && order == "desc")
            orderBy = where.OrderByDescending(o => EF.Property<RattachementEmploye>(o, prop.Name));

        var countAsync = await where
            .CountAsync(cancellationToken)
            .ConfigureAwait(false);

        var readOnlyList = await orderBy
            .Skip((page - 1) * size)
            .Take(size)
            .Select(c => RattachementEmploye.QueryCreate(
                c.RattachementEmployeId,
                c.RattachementId,
                c.Matricule,
                c.NomPrenom,
                c.DateDebut,
                c.HeureDebut,
                c.DateFin,
                c.HeureFin,
                c.NombreHeure,
                c.Cout,
                c.CoutGlobal,
                c.TypeRattachement,
                c.IsActive,
                c.SocieteId
            ))
            .ToListAsync(cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return (readOnlyList, countAsync);
    }

    public async Task<RattachementEmploye> GetOneAsync(
        RattachementEmployeId rattachementEmployeId,
        CancellationToken cancellationToken)
    {
        var rattachementEmploye = await _dbSet
            .Where(w => w.RattachementEmployeId == rattachementEmployeId)
            .Select(c => RattachementEmploye.QueryCreate(
                c.RattachementEmployeId,
                c.RattachementId,
                c.Matricule,
                c.NomPrenom,
                c.DateDebut,
                c.HeureDebut,
                c.DateFin,
                c.HeureFin,
                c.NombreHeure,
                c.Cout,
                c.CoutGlobal,
                c.TypeRattachement,
                c.IsActive,
                c.SocieteId
            ))
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        return rattachementEmploye!;
    }

    public async Task UpdateBulkAsync(RattachementEmploye rattachementEmploye, CancellationToken cancellationToken)
    {
        await _dbSet
            .Where(w => w.RattachementEmployeId == rattachementEmploye.RattachementEmployeId)
            .ExecuteUpdateAsync(s => s
                    .SetProperty(p => p.RattachementId, rattachementEmploye.RattachementId)
                    .SetProperty(p => p.Matricule, rattachementEmploye.Matricule)
                    .SetProperty(p => p.NomPrenom, rattachementEmploye.NomPrenom)
                    .SetProperty(p => p.DateDebut, rattachementEmploye.DateDebut)
                    .SetProperty(p => p.HeureDebut, rattachementEmploye.HeureDebut)
                    .SetProperty(p => p.DateFin, rattachementEmploye.DateFin)
                    .SetProperty(p => p.HeureFin, rattachementEmploye.HeureFin)
                    .SetProperty(p => p.NombreHeure, rattachementEmploye.NombreHeure)
                    .SetProperty(p => p.Cout, rattachementEmploye.Cout)
                    .SetProperty(p => p.CoutGlobal, rattachementEmploye.CoutGlobal)
                    .SetProperty(p => p.TypeRattachement, rattachementEmploye.TypeRattachement)
                    .SetProperty(p => p.IsActive, rattachementEmploye.IsActive),
                cancellationToken)
            .ConfigureAwait(false);
    }
}
