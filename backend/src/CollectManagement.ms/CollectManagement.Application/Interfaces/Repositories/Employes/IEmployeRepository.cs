using CollectManagement.Domain.Employes;
using CollectManagement.Domain.Employes.ValueObjects;

namespace CollectManagement.Application.Interfaces.Repositories.Employes;

public interface IEmployeRepository : IRepositoryBase<Employe>
{
    Task<(IReadOnlyList<Employe>, int)> GetPagedListAsync(
        string? search,
        string? sort,
        string? order,
        int page,
        int size,
        CancellationToken cancellationToken
    );

    Task<Employe> GetOneAsync(
        EmployeId employeId,
        CancellationToken cancellationToken
    );
}
