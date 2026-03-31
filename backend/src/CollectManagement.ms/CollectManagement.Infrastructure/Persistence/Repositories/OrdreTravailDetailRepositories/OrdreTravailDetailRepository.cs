using CollectManagement.Application.Interfaces.Repositories.OrdresTravailDetails;
using CollectManagement.Domain.OrdresTravail;
using CollectManagement.Domain.OrdresTravail.ValueObjects;
using CollectManagement.Infrastructure.Persistence.Context;

namespace CollectManagement.Infrastructure.Persistence.Repositories.OrdreTravailDetailRepositories;

public class OrdreTravailDetailRepository : RepositoryBase<OrdreTravailDetail>, IOrdreTravailDetailRepository
{
    public OrdreTravailDetailRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IReadOnlyList<OrdreTravailDetail>> GetByOrdreTravailAsync(
        OrdreTravailId ordreTravailId,
        CancellationToken ct)
    {
        return await _dbSet
            .Where(w => w.OrdreTravailId == ordreTravailId)
            .OrderBy(o => o.CodeArticle)
            .Select(c => OrdreTravailDetail.QueryCreate(
                c.OrdreTravailDetailId,
                c.OrdreTravailId,
                c.CodeArticle,
                c.CodeEntrepot,
                c.CodeUnite,
                c.LibelleArticle,
                c.PrixUnitaireHT,
                c.Quantite,
                c.TauxTVA,
                c.Montant
            ))
            .ToListAsync(ct)
            .ConfigureAwait(false);
    }

    public async Task<OrdreTravailDetail> GetOneAsync(
        OrdreTravailDetailId id,
        CancellationToken ct)
    {
        var entity = await _dbSet
            .Where(w => w.OrdreTravailDetailId == id)
            .Select(c => OrdreTravailDetail.QueryCreate(
                c.OrdreTravailDetailId,
                c.OrdreTravailId,
                c.CodeArticle,
                c.CodeEntrepot,
                c.CodeUnite,
                c.LibelleArticle,
                c.PrixUnitaireHT,
                c.Quantite,
                c.TauxTVA,
                c.Montant
            ))
            .FirstOrDefaultAsync(ct)
            .ConfigureAwait(false);

        return entity!;
    }

    public async Task UpdateBulkAsync(OrdreTravailDetail entity, CancellationToken ct)
    {
        await _dbSet
            .Where(w => w.OrdreTravailDetailId == entity.OrdreTravailDetailId)
            .ExecuteUpdateAsync(s => s
                    .SetProperty(p => p.CodeArticle, entity.CodeArticle)
                    .SetProperty(p => p.CodeEntrepot, entity.CodeEntrepot)
                    .SetProperty(p => p.CodeUnite, entity.CodeUnite)
                    .SetProperty(p => p.LibelleArticle, entity.LibelleArticle)
                    .SetProperty(p => p.PrixUnitaireHT, entity.PrixUnitaireHT)
                    .SetProperty(p => p.Quantite, entity.Quantite)
                    .SetProperty(p => p.TauxTVA, entity.TauxTVA)
                    .SetProperty(p => p.Montant, entity.Montant),
                ct)
            .ConfigureAwait(false);
    }
}
