using CollectManagement.Domain.Utilisateurs;
using CollectManagement.Domain.Utilisateurs.Entities;
using CollectManagement.Domain.Utilisateurs.ValueObjects;
using CollectManagement.Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CollectManagement.WebAPI.Authorization;

public sealed class NavigationPermissionHandler
    : AuthorizationHandler<NavigationPermissionRequirement>
{
    private readonly ApplicationDbContext _dbContext;

    public NavigationPermissionHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        NavigationPermissionRequirement requirement)
    {
        if (context.Resource is not HttpContext httpContext)
            return;

        var endpoint = httpContext.GetEndpoint();
        if (endpoint is null)
            return;

        if (endpoint.Metadata.GetMetadata<IAllowAnonymous>() is not null)
        {
            context.Succeed(requirement);
            return;
        }

        var metadata = endpoint.Metadata.GetMetadata<NavigationPermissionMetadata>();
        if (metadata is null)
        {
            context.Succeed(requirement);
            return;
        }

        var userId = context.User.FindFirst("sub")?.Value
                     ?? context.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (!Ulid.TryParse(userId, out var parsedUserId))
            return;

        var utilisateurId = new UtilisateurId(parsedUserId);
        var utilisateur = await _dbContext
            .Set<Utilisateur>()
            .AsNoTracking()
            .Include(u => u.RoleUtilisateur)
            .FirstOrDefaultAsync(u => u.UtilisateurId == utilisateurId, httpContext.RequestAborted)
            .ConfigureAwait(false);

        if (utilisateur is null)
            return;

        // Super-admin style users without role matrix keep access.
        if (utilisateur.RoleUtilisateurId is null)
        {
            context.Succeed(requirement);
            return;
        }

        var role = utilisateur.RoleUtilisateur;
        if (role is null)
            return;

        var navigation = role.Navigations.FirstOrDefault(n =>
            string.Equals(n.NavigationId, metadata.NavigationId, StringComparison.OrdinalIgnoreCase));

        if (navigation is null)
            return;

        var requiredAction = NavigationEndpointExtensions.ResolveAction(httpContext.Request);
        if (navigation.Actions.Contains(requiredAction))
            context.Succeed(requirement);
    }
}
