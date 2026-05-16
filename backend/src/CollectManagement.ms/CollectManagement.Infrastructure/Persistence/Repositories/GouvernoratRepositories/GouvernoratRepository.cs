using CollectManagement.Application.Interfaces.Repositories.Gouvernorats;
using CollectManagement.Domain.Gouvernorats;
using CollectManagement.Infrastructure.Persistence.Context;

namespace CollectManagement.Infrastructure.Persistence.Repositories.GouvernoratRepositories;

public class GouvernoratRepository : RepositoryBase<Gouvernorat>, IGouvernoratRepository
{
    public GouvernoratRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
