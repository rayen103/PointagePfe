using CollectManagement.Domain.Equipes;
using CollectManagement.Domain.Equipes.ValueObjects;

namespace CollectManagement.Application.Interfaces.Repositories.Equipes;

public interface IEquipeRepository : IRepositoryBase<Equipe>
{
    Task<(IReadOnlyList<Equipe>, int)> GetPagedListAsync(
        string? search,
        string? sort,
        string? order,
        int page,
        int size,
        CancellationToken cancellationToken
    );
    
    Task<Equipe> GetOneAsync(
        EquipeId equipeId,
        CancellationToken cancellationToken
    );
    
    Task UpdateBulkAsync(Equipe equipe, CancellationToken cancellationToken);
}
