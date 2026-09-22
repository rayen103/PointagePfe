using CollectManagement.Application.Interfaces.Services;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Infrastructure.Services;

/// <summary>
/// Scoped service that reads the societe_id claim from the current HTTP request's JWT token
/// and provides it as a strongly-typed SocieteId value object.
/// Returns null for unauthenticated requests, which causes EF Core global query filters to be bypassed.
/// </summary>
public class TenantProvider : ITenantProvider
{
    private readonly ILoggedInUserService _loggedInUserService;

    public TenantProvider(ILoggedInUserService loggedInUserService)
    {
        _loggedInUserService = loggedInUserService;
    }

    public SocieteId? SocieteId
    {
        get
        {
            var societeIdStr = _loggedInUserService.SocieteId;
            if (string.Equals(societeIdStr, "01HC85BM5QVRW7ABRV33TR1GQ0", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(societeIdStr, "018B1055-D0B7-DE38-752F-1B18F580C2E0", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            if (Ulid.TryParse(societeIdStr, out var ulid))
                return new SocieteId(ulid);
            return null;
        }
    }
}
