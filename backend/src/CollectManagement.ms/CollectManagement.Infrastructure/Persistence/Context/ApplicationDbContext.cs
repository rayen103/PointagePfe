using CollectManagement.Domain.Common;

namespace CollectManagement.Infrastructure.Persistence.Context;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options) : 
        base(options)
    {
        
    }

    public DbSet<CollectManagement.Domain.Bus.BusRuntimeEvent> BusRuntimeEvent { get; set; }
    public DbSet<CollectManagement.Domain.Analyse.ReportLayout> ReportLayout { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Get All configuration from assemply
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}