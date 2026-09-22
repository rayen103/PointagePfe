using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Application.Interfaces.Services;

/// <summary>
/// Provides the current tenant (Societe) context for the request.
/// Returns null for unauthenticated requests, which causes EF Core global query filters to be bypassed.
/// </summary>
public interface ITenantProvider
{
    SocieteId? SocieteId { get; }
}
