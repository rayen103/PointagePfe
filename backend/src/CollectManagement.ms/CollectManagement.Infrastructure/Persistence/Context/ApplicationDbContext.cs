using System.Linq.Expressions;
using CollectManagement.Application.Interfaces.Services;
using CollectManagement.Domain.Common;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Infrastructure.Persistence.Context;

public class ApplicationDbContext: DbContext
{
    private readonly ITenantProvider _tenantProvider;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        ITenantProvider tenantProvider) : 
        base(options)
    {
        _tenantProvider = tenantProvider;
    }

    public DbSet<CollectManagement.Domain.Bus.BusRuntimeEvent> BusRuntimeEvent { get; set; }
    public DbSet<CollectManagement.Domain.Analyse.ReportLayout> ReportLayout { get; set; }
    public DbSet<CollectManagement.Domain.Regions.Region> Region { get; set; }
    public DbSet<CollectManagement.Domain.Modems.Modem> Modem { get; set; }
    public DbSet<CollectManagement.Domain.Chauffeurs.Chauffeur> Chauffeur { get; set; }
    public DbSet<CollectManagement.Domain.Gouvernorats.Gouvernorat> Gouvernorat { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Get All configuration from assemply
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
        
        // Apply global query filters for all entities implementing ITenantEntity.
        // This ensures every query is automatically scoped to the current user's société.
        // When _tenantProvider.SocieteId is null (unauthenticated/anonymous requests),
        // the filter is bypassed so login/registration can access all data.
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(ITenantEntity).IsAssignableFrom(entityType.ClrType))
            {
                modelBuilder.Entity(entityType.ClrType)
                    .HasQueryFilter(BuildTenantFilter(entityType.ClrType));
            }
        }
    }
    
    /// <summary>
    /// Builds a lambda expression for the given entity type:
    ///   entity => _tenantProvider.SocieteId == null || entity.SocieteId == _tenantProvider.SocieteId
    /// 
    /// EF Core evaluates _tenantProvider.SocieteId on each query execution (not at model build time),
    /// because the expression captures the field reference, not its current value.
    /// </summary>
    private LambdaExpression BuildTenantFilter(Type entityType)
    {
        // Parameter: e (the entity)
        var parameter = Expression.Parameter(entityType, "e");
        
        // Access: this._tenantProvider.SocieteId
        var tenantProviderField = Expression.Field(Expression.Constant(this), nameof(_tenantProvider));
        var currentTenantId = Expression.Property(tenantProviderField, nameof(ITenantProvider.SocieteId));
        
        // Condition 1: _tenantProvider.SocieteId == null (bypass for unauthenticated requests)
        var nullCheck = Expression.Equal(
            currentTenantId, 
            Expression.Constant(null, typeof(SocieteId)));
        
        // Access: e.SocieteId
        var entitySocieteId = Expression.Property(parameter, nameof(ITenantEntity.SocieteId));
        
        // Condition 2: e.SocieteId == _tenantProvider.SocieteId
        var tenantMatch = Expression.Equal(entitySocieteId, 
            Expression.Convert(currentTenantId, typeof(SocieteId)));
        
        // Combined: null check OR tenant match
        var body = Expression.OrElse(nullCheck, tenantMatch);
        
        return Expression.Lambda(body, parameter);
    }
}