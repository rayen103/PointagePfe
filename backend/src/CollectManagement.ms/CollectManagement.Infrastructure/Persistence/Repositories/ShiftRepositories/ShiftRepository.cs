using System.ComponentModel;
using CollectManagement.Application.Interfaces.Repositories.Shifts;
using CollectManagement.Domain.Shifts;
using CollectManagement.Domain.Shifts.ValueObjects;
using CollectManagement.Infrastructure.Persistence.Context;

namespace CollectManagement.Infrastructure.Persistence.Repositories.ShiftRepositories;

public class ShiftRepository : RepositoryBase<Shift>, IShiftRepository
{
    public ShiftRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<(IReadOnlyList<Shift>, int)> GetPagedListAsync(
        string? search,
        string? sort,
        string? order,
        int page,
        int size,
        CancellationToken cancellationToken)
    {
        var where = _dbSet.Where(w =>
            string.IsNullOrWhiteSpace(search) ||
            w.CodeShift.Contains(search) ||
            (w.LibelleShift != null && w.LibelleShift.Contains(search))
        );

        var orderBy = where
            .OrderByDescending(o => o.CodeShift);

        var prop = TypeDescriptor
            .GetProperties(typeof(Shift))
            .Find(sort ?? "CodeShift", true);

        if (prop is not null && order == "asc")
            orderBy = where.OrderBy(o =>
                EF.Property<Shift>(o, prop.Name));

        if (prop is not null && order == "desc")
            orderBy = where.OrderByDescending(o =>
                EF.Property<Shift>(o, prop.Name));

        var countAsync = await where
            .CountAsync(cancellationToken)
            .ConfigureAwait(false);

        var readOnlyList = await orderBy
            .Skip((page - 1) * size)
            .Take(size)
            .Select(c => Shift.QueryCreate(
                c.ShiftId,
                c.CodeShift,
                c.LibelleShift,
                c.JourSemaine,
                c.HeureDebut,
                c.HeureFin,
                c.IsActive,
                c.SocieteId
            ))
            .ToListAsync(cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return (readOnlyList, countAsync);
    }

    public async Task<Shift> GetOneAsync(
        ShiftId shiftId,
        CancellationToken cancellationToken)
    {
        var shift = await _dbSet
            .Where(w => w.ShiftId == shiftId)
            .Select(c => Shift.QueryCreate(
                c.ShiftId,
                c.CodeShift,
                c.LibelleShift,
                c.JourSemaine,
                c.HeureDebut,
                c.HeureFin,
                c.IsActive,
                c.SocieteId
            ))
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        return shift!;
    }

    public async Task UpdateBulkAsync(Shift shift, CancellationToken cancellationToken)
    {
        await _dbSet
            .Where(w => w.ShiftId == shift.ShiftId)
            .ExecuteUpdateAsync(s => s
                    .SetProperty(p => p.CodeShift, shift.CodeShift)
                    .SetProperty(p => p.LibelleShift, shift.LibelleShift)
                    .SetProperty(p => p.JourSemaine, shift.JourSemaine)
                    .SetProperty(p => p.HeureDebut, shift.HeureDebut)
                    .SetProperty(p => p.HeureFin, shift.HeureFin)
                    .SetProperty(p => p.IsActive, shift.IsActive),
                cancellationToken)
            .ConfigureAwait(false);
    }
}
