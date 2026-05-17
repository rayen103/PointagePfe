using CollectManagement.Application.Interfaces.Repositories.Regions;
using CollectManagement.Domain.Regions;
using CollectManagement.Infrastructure.Persistence.Context;

namespace CollectManagement.Infrastructure.Persistence.Repositories.RegionRepositories;

public class RegionRepository : RepositoryBase<Region>, IRegionRepository
{
    public RegionRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
