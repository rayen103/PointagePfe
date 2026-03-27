using System.ComponentModel;
using CollectManagement.Application.Interfaces.Repositories.Employes;
using CollectManagement.Domain.Employes;
using CollectManagement.Domain.Employes.Enums;
using CollectManagement.Domain.Employes.ValueObjects;
using CollectManagement.Infrastructure.Persistence.Context;

namespace CollectManagement.Infrastructure.Persistence.Repositories.EmployeRepositories;

public class EmployeRepository : RepositoryBase<Employe>, IEmployeRepository
{
    public EmployeRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<(IReadOnlyList<Employe>, int)> GetPagedListAsync(
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
            w.Nom.Contains(search) ||
            w.Prenom.Contains(search) ||
            (w.RFID != null && w.RFID.Contains(search)) ||
            (w.Adresse != null && w.Adresse.Contains(search))
        );

        var orderBy = where
            .OrderByDescending(o => o.Matricule);

        var prop = TypeDescriptor
            .GetProperties(typeof(Employe))
            .Find(sort ?? "Matricule", true);

        if (prop is not null && order == "asc")
            orderBy = where.OrderBy(o =>
                EF.Property<Employe>(o, prop.DisplayName));

        if (prop is not null && order == "desc")
            orderBy = where.OrderByDescending(o =>
                EF.Property<Employe>(o, prop.DisplayName));

        var countAsync = await where
            .CountAsync(cancellationToken)
            .ConfigureAwait(false);

        var readOnlyList = await orderBy
            .Skip((page - 1) * size)
            .Take(size)
            .Select(c => Employe.QueryCreate(
                c.EmployeId,
                c.Matricule,
                c.RFID,
                c.Nom,
                c.Prenom,
                c.TypeEmploye,
                c.CodeCircuit,
                c.CodePointCollecte,
                c.CodeBus,
                c.CodeShift,
                c.Adresse,
                c.CodeGouvernorat,
                c.CodeRegion,
                c.Latitude,
                c.Longitude,
                c.SocieteId
            ))
            .ToListAsync(cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return (readOnlyList, countAsync);
    }

    public Task<Employe> GetOneAsync(EmployeId employeId, CancellationToken cancellationToken)
    {
        return _dbSet.Where(w => w.EmployeId.Equals(employeId))
            .FirstOrDefaultAsync(cancellationToken);
    }
}
