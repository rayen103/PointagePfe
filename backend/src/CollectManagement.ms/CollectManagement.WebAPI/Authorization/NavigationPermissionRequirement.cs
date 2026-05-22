using Microsoft.AspNetCore.Authorization;

namespace CollectManagement.WebAPI.Authorization;

public sealed class NavigationPermissionRequirement : IAuthorizationRequirement
{
    public const string PolicyName = "NavigationPermissionPolicy";
}
