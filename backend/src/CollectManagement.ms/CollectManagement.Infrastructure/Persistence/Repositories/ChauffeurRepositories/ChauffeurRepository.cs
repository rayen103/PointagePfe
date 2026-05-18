using CollectManagement.Application.Interfaces.Repositories.Chauffeurs;
using CollectManagement.Domain.Chauffeurs;
using CollectManagement.Infrastructure.Persistence.Context;

namespace CollectManagement.Infrastructure.Persistence.Repositories.ChauffeurRepositories;

public class ChauffeurRepository : RepositoryBase<Chauffeur>, IChauffeurRepository
{
    public ChauffeurRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
