using CollectManagement.Domain.Reseaux;
using CollectManagement.Domain.Reseaux.ValueObjects;

namespace CollectManagement.Application.Interfaces.Repositories.Reseaux;

public interface IReseauRepository : IRepositoryBase<Reseau>
{
    Task<(IReadOnlyList<Reseau>, int)> GetPagedListAsync(
        string? search,
        string? sort,
        string? order,
        int page,
        int size,
        Ulid? societeId,
        CancellationToken cancellationToken);

    Task<Reseau> GetOneAsync(ReseauId reseauId, CancellationToken cancellationToken);
    Task UpdateBulkAsync(Reseau reseau, CancellationToken cancellationToken);
}
