using CollectManagement.Application.Interfaces.Repositories.Modems;
using CollectManagement.Domain.Modems;
using CollectManagement.Infrastructure.Persistence.Context;

namespace CollectManagement.Infrastructure.Persistence.Repositories.ModemRepositories;

public class ModemRepository : RepositoryBase<Modem>, IModemRepository
{
    public ModemRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
