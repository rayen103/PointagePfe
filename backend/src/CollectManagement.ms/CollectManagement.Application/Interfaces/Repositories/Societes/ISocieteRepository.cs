using CollectManagement.Domain.Societes;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Application.Interfaces.Repositories.Societes;

public interface ISocieteRepository:IRepositoryBase<Societe>
{
    Task<(IReadOnlyList<Societe>, int)> GetPagedListAsync(
        string? search,
        string? sort,
        string? order,
        int page,
        int size,
        CancellationToken cancellationToken
    );
    
    Task<Societe> GetOneAsync(
        SocieteId societeId,
        CancellationToken cancellationToken
    );

    
    Task UpdateBulkAsync(Societe societe, CancellationToken cancellationToken);
}