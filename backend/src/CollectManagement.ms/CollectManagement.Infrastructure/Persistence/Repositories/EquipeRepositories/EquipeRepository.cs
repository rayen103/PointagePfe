using System.ComponentModel;
using CollectManagement.Application.Interfaces.Repositories.Equipes;
using CollectManagement.Domain.Equipes;
using CollectManagement.Domain.Equipes.ValueObjects;
using CollectManagement.Infrastructure.Persistence.Context;

namespace CollectManagement.Infrastructure.Persistence.Repositories.EquipeRepositories;

public class EquipeRepository : RepositoryBase<Equipe>, IEquipeRepository
{
    public EquipeRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<(IReadOnlyList<Equipe>, int)> GetPagedListAsync(
        string? search,
        string? sort,
        string? order,
        int page,
        int size,
        CancellationToken cancellationToken)
    {
        var where = _dbSet.Where(w =>
            string.IsNullOrWhiteSpace(search) ||
            w.CodeEquipe.Contains(search) ||
            (w.LibelleEquipe != null && w.LibelleEquipe.Contains(search)) ||
            (w.Responsable != null && w.Responsable.Contains(search)) ||
            (w.CodeClient != null && w.CodeClient.Contains(search))
        );

        var orderBy = where
            .OrderByDescending(o => o.CodeEquipe);

        var prop = TypeDescriptor
            .GetProperties(typeof(Equipe))
            .Find(sort ?? "CodeEquipe", true);

        if (prop is not null && order == "asc")
            orderBy = where.OrderBy(o =>
                EF.Property<Equipe>(o, prop.DisplayName));

        if (prop is not null && order == "desc")
            orderBy = where.OrderByDescending(o =>
                EF.Property<Equipe>(o, prop.DisplayName));

        var countAsync = await where
            .CountAsync(cancellationToken)
            .ConfigureAwait(false);

        var readOnlyList = await orderBy
            .Skip((page - 1) * size)
            .Take(size)
            .Select(c => Equipe.QueryCreate(
                c.EquipeId,
                c.CodeEquipe,
                c.LibelleEquipe,
                c.CodeClient,
                c.CodeEntrepot,
                c.CodeTarif,
                c.CodeFournisseur,
                c.Responsable,
                c.IsInternal,
                c.CodeVehicule,
                c.IsActive,
                c.SocieteId
            ))
            .ToListAsync(cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return (readOnlyList, countAsync);
    }

    public async Task<Equipe> GetOneAsync(
        EquipeId equipeId,
        CancellationToken cancellationToken)
    {
        var equipe = await _dbSet
            .Where(w => w.EquipeId == equipeId)
            .Select(c => Equipe.QueryCreate(
                c.EquipeId,
                c.CodeEquipe,
                c.LibelleEquipe,
                c.CodeClient,
                c.CodeEntrepot,
                c.CodeTarif,
                c.CodeFournisseur,
                c.Responsable,
                c.IsInternal,
                c.CodeVehicule,
                c.IsActive,
                c.SocieteId
            ))
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        return equipe!;
    }

    public async Task UpdateBulkAsync(Equipe equipe, CancellationToken cancellationToken)
    {
        await _dbSet
            .Where(w => w.EquipeId == equipe.EquipeId)
            .ExecuteUpdateAsync(s => s
                    .SetProperty(p => p.CodeEquipe, equipe.CodeEquipe)
                    .SetProperty(p => p.LibelleEquipe, equipe.LibelleEquipe)
                    .SetProperty(p => p.CodeClient, equipe.CodeClient)
                    .SetProperty(p => p.CodeEntrepot, equipe.CodeEntrepot)
                    .SetProperty(p => p.CodeTarif, equipe.CodeTarif)
                    .SetProperty(p => p.CodeFournisseur, equipe.CodeFournisseur)
                    .SetProperty(p => p.Responsable, equipe.Responsable)
                    .SetProperty(p => p.IsInternal, equipe.IsInternal)
                    .SetProperty(p => p.CodeVehicule, equipe.CodeVehicule)
                    .SetProperty(p => p.IsActive, equipe.IsActive),
                cancellationToken)
            .ConfigureAwait(false);
    }
}
