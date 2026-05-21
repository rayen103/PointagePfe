using CollectManagement.Domain.Chantiers;
using CollectManagement.Domain.Chantiers.ValueObjects;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Application.Interfaces.Repositories.Chantiers;

public interface IChantierRepository : IRepositoryBase<Chantier>
{
    Task<(IReadOnlyList<Chantier>, int)> GetPagedListAsync(
        string? search,
        string? sort,
        string? order,
        int page,
        int size,
        CancellationToken cancellationToken);

    Task<Chantier> GetOneAsync(
        ChantierId chantierId,
        CancellationToken cancellationToken);

    Task<bool> ExistsActiveByNumeroAndSocieteAsync(
        string numeroChantier,
        SocieteId societeId,
        CancellationToken cancellationToken);

    Task UpdateBulkAsync(Chantier chantier, CancellationToken cancellationToken);
}
