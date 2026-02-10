using CollectManagement.Domain.Interventions;
using CollectManagement.Domain.Interventions.ValueObjects;

namespace CollectManagement.Application.Interfaces.Repositories.Interventions;

public interface IInterventionRepository : IRepositoryBase<Intervention>
{
    Task<(IReadOnlyList<Intervention>, int)> GetPagedListAsync(
        string? search,
        string? sort,
        string? order,
        int page,
        int size,
        CancellationToken cancellationToken
    );

    Task<Intervention> GetOneAsync(
        InterventionId interventionId,
        CancellationToken cancellationToken
    );
}
