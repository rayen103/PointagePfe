using System.ComponentModel;
using CollectManagement.Application.Interfaces.Repositories.Chantiers;
using CollectManagement.Domain.Chantiers;
using CollectManagement.Domain.Chantiers.ValueObjects;
using CollectManagement.Infrastructure.Persistence.Context;

namespace CollectManagement.Infrastructure.Persistence.Repositories.ChantierRepositories;

public class ChantierRepository : RepositoryBase<Chantier>, IChantierRepository
{
    public ChantierRepository(ApplicationDbContext dbContext) : base(dbContext) { }

    public async Task<(IReadOnlyList<Chantier>, int)> GetPagedListAsync(
        string? search, string? sort, string? order, int page, int size, CancellationToken cancellationToken)
    {
        var where = _dbSet.Where(w =>
            string.IsNullOrWhiteSpace(search) ||
            w.NumeroChantier.Contains(search) ||
            (w.LibelleChantier != null && w.LibelleChantier.Contains(search)));

        var orderBy = where.OrderByDescending(o => o.NumeroChantier);

        var prop = TypeDescriptor.GetProperties(typeof(Chantier)).Find(sort ?? "NumeroChantier", true);
        if (prop is not null && order == "asc")
            orderBy = where.OrderBy(o => EF.Property<Chantier>(o, prop.Name));
        if (prop is not null && order == "desc")
            orderBy = where.OrderByDescending(o => EF.Property<Chantier>(o, prop.Name));

        var totalCount = await where.CountAsync(cancellationToken).ConfigureAwait(false);
        var list = await orderBy
            .Skip((page - 1) * size).Take(size)
            .Select(c => Chantier.QueryCreate(c.ChantierId, c.NumeroChantier, c.LibelleChantier, c.CodeClient,
                c.Adresse, c.MontantHT, c.MontantTTC, c.Nature, c.Responsable, c.DateDebut, c.DateFin,
                c.Status, c.IsActive, c.SocieteId))
            .ToListAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        return (list, totalCount);
    }

    public async Task<Chantier> GetOneAsync(ChantierId chantierId, CancellationToken cancellationToken)
    {
        var chantier = await _dbSet
            .Where(w => w.ChantierId == chantierId)
            .Select(c => Chantier.QueryCreate(c.ChantierId, c.NumeroChantier, c.LibelleChantier, c.CodeClient,
                c.Adresse, c.MontantHT, c.MontantTTC, c.Nature, c.Responsable, c.DateDebut, c.DateFin,
                c.Status, c.IsActive, c.SocieteId))
            .FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
        return chantier!;
    }

    public async Task UpdateBulkAsync(Chantier chantier, CancellationToken cancellationToken)
    {
        await _dbSet.Where(w => w.ChantierId == chantier.ChantierId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(p => p.NumeroChantier, chantier.NumeroChantier)
                .SetProperty(p => p.LibelleChantier, chantier.LibelleChantier)
                .SetProperty(p => p.CodeClient, chantier.CodeClient)
                .SetProperty(p => p.Adresse, chantier.Adresse)
                .SetProperty(p => p.MontantHT, chantier.MontantHT)
                .SetProperty(p => p.MontantTTC, chantier.MontantTTC)
                .SetProperty(p => p.Nature, chantier.Nature)
                .SetProperty(p => p.Responsable, chantier.Responsable)
                .SetProperty(p => p.DateDebut, chantier.DateDebut)
                .SetProperty(p => p.DateFin, chantier.DateFin)
                .SetProperty(p => p.Status, chantier.Status)
                .SetProperty(p => p.IsActive, chantier.IsActive),
            cancellationToken).ConfigureAwait(false);
    }
}
