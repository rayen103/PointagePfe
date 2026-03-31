using System.ComponentModel;
using CollectManagement.Application.Interfaces.Repositories.Rattachements;
using CollectManagement.Domain.Rattachements;
using CollectManagement.Domain.Rattachements.ValueObjects;
using CollectManagement.Infrastructure.Persistence.Context;

namespace CollectManagement.Infrastructure.Persistence.Repositories.RattachementRepositories;

public class RattachementArticleRepository : RepositoryBase<RattachementArticle>, IRattachementArticleRepository
{
    public RattachementArticleRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<(IReadOnlyList<RattachementArticle>, int)> GetPagedListAsync(
        string? search,
        string? sort,
        string? order,
        int page,
        int size,
        CancellationToken cancellationToken)
    {
        var where = _dbSet.Where(w =>
            string.IsNullOrWhiteSpace(search) ||
            w.CodeArticle.Contains(search) ||
            (w.Libelle != null && w.Libelle.Contains(search)) ||
            (w.TypeRattachement != null && w.TypeRattachement.Contains(search)) ||
            (w.NumeroBonLivraison != null && w.NumeroBonLivraison.Contains(search))
        );

        var orderBy = where.OrderByDescending(o => o.CodeArticle);

        var prop = TypeDescriptor
            .GetProperties(typeof(RattachementArticle))
            .Find(sort ?? "CodeArticle", true);

        if (prop is not null && order == "asc")
            orderBy = where.OrderBy(o => EF.Property<RattachementArticle>(o, prop.Name));

        if (prop is not null && order == "desc")
            orderBy = where.OrderByDescending(o => EF.Property<RattachementArticle>(o, prop.Name));

        var countAsync = await where
            .CountAsync(cancellationToken)
            .ConfigureAwait(false);

        var readOnlyList = await orderBy
            .Skip((page - 1) * size)
            .Take(size)
            .Select(c => RattachementArticle.QueryCreate(
                c.RattachementArticleId,
                c.RattachementId,
                c.CodeArticle,
                c.Libelle,
                c.Quantite,
                c.PrixRevient,
                c.TauxTVA,
                c.CodeUnite,
                c.CodeEntrepot,
                c.TypeRattachement,
                c.NumeroBonLivraison,
                c.DateBonLivraison,
                c.IsActive,
                c.SocieteId
            ))
            .ToListAsync(cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return (readOnlyList, countAsync);
    }

    public async Task<RattachementArticle> GetOneAsync(
        RattachementArticleId rattachementArticleId,
        CancellationToken cancellationToken)
    {
        var rattachementArticle = await _dbSet
            .Where(w => w.RattachementArticleId == rattachementArticleId)
            .Select(c => RattachementArticle.QueryCreate(
                c.RattachementArticleId,
                c.RattachementId,
                c.CodeArticle,
                c.Libelle,
                c.Quantite,
                c.PrixRevient,
                c.TauxTVA,
                c.CodeUnite,
                c.CodeEntrepot,
                c.TypeRattachement,
                c.NumeroBonLivraison,
                c.DateBonLivraison,
                c.IsActive,
                c.SocieteId
            ))
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        return rattachementArticle!;
    }

    public async Task UpdateBulkAsync(RattachementArticle rattachementArticle, CancellationToken cancellationToken)
    {
        await _dbSet
            .Where(w => w.RattachementArticleId == rattachementArticle.RattachementArticleId)
            .ExecuteUpdateAsync(s => s
                    .SetProperty(p => p.RattachementId, rattachementArticle.RattachementId)
                    .SetProperty(p => p.CodeArticle, rattachementArticle.CodeArticle)
                    .SetProperty(p => p.Libelle, rattachementArticle.Libelle)
                    .SetProperty(p => p.Quantite, rattachementArticle.Quantite)
                    .SetProperty(p => p.PrixRevient, rattachementArticle.PrixRevient)
                    .SetProperty(p => p.TauxTVA, rattachementArticle.TauxTVA)
                    .SetProperty(p => p.CodeUnite, rattachementArticle.CodeUnite)
                    .SetProperty(p => p.CodeEntrepot, rattachementArticle.CodeEntrepot)
                    .SetProperty(p => p.TypeRattachement, rattachementArticle.TypeRattachement)
                    .SetProperty(p => p.NumeroBonLivraison, rattachementArticle.NumeroBonLivraison)
                    .SetProperty(p => p.DateBonLivraison, rattachementArticle.DateBonLivraison)
                    .SetProperty(p => p.IsActive, rattachementArticle.IsActive),
                cancellationToken)
            .ConfigureAwait(false);
    }
}
