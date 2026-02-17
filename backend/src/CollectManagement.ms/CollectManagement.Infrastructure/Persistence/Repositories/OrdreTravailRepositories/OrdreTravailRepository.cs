using System.ComponentModel;
using CollectManagement.Application.Interfaces.Repositories.OrdresTravail;
using CollectManagement.Domain.OrdresTravail;
using CollectManagement.Domain.OrdresTravail.ValueObjects;
using CollectManagement.Infrastructure.Persistence.Context;

namespace CollectManagement.Infrastructure.Persistence.Repositories.OrdreTravailRepositories;

public class OrdreTravailRepository : RepositoryBase<OrdreTravail>, IOrdreTravailRepository
{
    public OrdreTravailRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<(IReadOnlyList<OrdreTravail>, int)> GetPagedListAsync(
        string? search,
        string? sort,
        string? order,
        int page,
        int size,
        CancellationToken cancellationToken)
    {
        var where = _dbSet.Where(w =>
            string.IsNullOrWhiteSpace(search) ||
            w.NumeroOrdreTravail.Contains(search) ||
            (w.NumeroChantier != null && w.NumeroChantier.Contains(search)) ||
            (w.CodeClient != null && w.CodeClient.Contains(search)) ||
            (w.NumeroBonCommande != null && w.NumeroBonCommande.Contains(search)) ||
            (w.Libelle != null && w.Libelle.Contains(search))
        );

        var orderBy = where
            .OrderByDescending(o => o.DateCreation);

        var prop = TypeDescriptor
            .GetProperties(typeof(OrdreTravail))
            .Find(sort ?? "DateCreation", true);

        if (prop is not null && order == "asc")
            orderBy = where.OrderBy(o =>
                EF.Property<OrdreTravail>(o, prop.Name));

        if (prop is not null && order == "desc")
            orderBy = where.OrderByDescending(o =>
                EF.Property<OrdreTravail>(o, prop.Name));

        var countAsync = await where
            .CountAsync(cancellationToken)
            .ConfigureAwait(false);

        var readOnlyList = await orderBy
            .Skip((page - 1) * size)
            .Take(size)
            .Select(c => OrdreTravail.QueryCreate(
                c.OrdreTravailId,
                c.NumeroOrdreTravail,
                c.NumeroChantier,
                c.CodeClient,
                c.NumeroBonCommande,
                c.CodeEquipe,
                c.EtatOT,
                c.Montant,
                c.DateCreation,
                c.NumeroConvention,
                c.CodeVehicule,
                c.Libelle,
                c.IsActive,
                c.SocieteId
            ))
            .ToListAsync(cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return (readOnlyList, countAsync);
    }

    public async Task<OrdreTravail> GetOneAsync(
        OrdreTravailId ordreTravailId,
        CancellationToken cancellationToken)
    {
        var ordreTravail = await _dbSet
            .Where(w => w.OrdreTravailId == ordreTravailId)
            .Select(c => OrdreTravail.QueryCreate(
                c.OrdreTravailId,
                c.NumeroOrdreTravail,
                c.NumeroChantier,
                c.CodeClient,
                c.NumeroBonCommande,
                c.CodeEquipe,
                c.EtatOT,
                c.Montant,
                c.DateCreation,
                c.NumeroConvention,
                c.CodeVehicule,
                c.Libelle,
                c.IsActive,
                c.SocieteId
            ))
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        return ordreTravail!;
    }

    public async Task UpdateBulkAsync(OrdreTravail ordreTravail, CancellationToken cancellationToken)
    {
        await _dbSet
            .Where(w => w.OrdreTravailId == ordreTravail.OrdreTravailId)
            .ExecuteUpdateAsync(s => s
                    .SetProperty(p => p.NumeroOrdreTravail, ordreTravail.NumeroOrdreTravail)
                    .SetProperty(p => p.NumeroChantier, ordreTravail.NumeroChantier)
                    .SetProperty(p => p.CodeClient, ordreTravail.CodeClient)
                    .SetProperty(p => p.NumeroBonCommande, ordreTravail.NumeroBonCommande)
                    .SetProperty(p => p.CodeEquipe, ordreTravail.CodeEquipe)
                    .SetProperty(p => p.EtatOT, ordreTravail.EtatOT)
                    .SetProperty(p => p.Montant, ordreTravail.Montant)
                    .SetProperty(p => p.DateCreation, ordreTravail.DateCreation)
                    .SetProperty(p => p.NumeroConvention, ordreTravail.NumeroConvention)
                    .SetProperty(p => p.CodeVehicule, ordreTravail.CodeVehicule)
                    .SetProperty(p => p.Libelle, ordreTravail.Libelle)
                    .SetProperty(p => p.IsActive, ordreTravail.IsActive),
                cancellationToken)
            .ConfigureAwait(false);
    }
}
