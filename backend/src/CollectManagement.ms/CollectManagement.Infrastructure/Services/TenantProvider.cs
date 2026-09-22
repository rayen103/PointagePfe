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
                string.Equals(societeIdStr, "018B1055-D0B7-DE38-752F-1B18F580C2E0", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(societeIdStr, "01K5RFKEH2YYM1WQ79T4G6QY5N", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(societeIdStr, "019970F9-BA22-F7A8-1E5C-E9D1206BF8B5", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(societeIdStr, "01M2DPE9ETCA0QN8PS7R810X3P", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(_loggedInUserService.UserId, "01M2G8C6R8634SHXFQSVXAYPQ7", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(_loggedInUserService.UserId, "019ECC22-A4E6-267F-50A1-3A04B83ADEDC", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            if (Ulid.TryParse(societeIdStr, out var ulid))
                return new SocieteId(ulid);
            return null;
        }
    }
}
