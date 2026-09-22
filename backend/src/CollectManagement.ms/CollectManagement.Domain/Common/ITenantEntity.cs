using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Domain.Common;

/// <summary>
/// Marker interface for entities that are scoped to a specific tenant (Societe).
/// Entities implementing this interface will automatically have EF Core global query filters
/// applied to restrict data access to the authenticated user's société.
/// </summary>
public interface ITenantEntity
{
    SocieteId SocieteId { get; }
}
