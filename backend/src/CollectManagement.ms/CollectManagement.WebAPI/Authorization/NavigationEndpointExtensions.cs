using CollectManagement.Domain.Utilisateurs.Enums;

namespace CollectManagement.WebAPI.Authorization;

public static class NavigationEndpointExtensions
{
    public static RouteGroupBuilder RequireNavigationPermission(
        this RouteGroupBuilder routeGroupBuilder,
        string navigationId)
    {
        routeGroupBuilder
            .RequireAuthorization(NavigationPermissionRequirement.PolicyName)
            .WithMetadata(new NavigationPermissionMetadata(navigationId));

        return routeGroupBuilder;
    }

    public static NavigationAction ResolveAction(HttpRequest request)
    {
        var method = request.Method.ToUpperInvariant();
        var path = request.Path.Value?.ToLowerInvariant() ?? string.Empty;

        if (method is "GET" or "HEAD" or "OPTIONS")
        {
            if (path.Contains("print"))
                return NavigationAction.Print;
            if (path.Contains("export"))
                return NavigationAction.Export;
            if (path.Contains("search") || path.Contains("list"))
                return NavigationAction.Search;

            return NavigationAction.View;
        }

        if (method is "PATCH" or "PUT")
            return NavigationAction.Edit;

        if (method == "DELETE")
            return NavigationAction.Delete;

        if (path.Contains("delete"))
            return NavigationAction.Delete;
        if (path.Contains("update"))
            return NavigationAction.Edit;
        if (path.Contains("duplicate"))
            return NavigationAction.Duplicate;
        if (path.Contains("print"))
            return NavigationAction.Print;
        if (path.Contains("export"))
            return NavigationAction.Export;
        if (path.Contains("search"))
            return NavigationAction.Search;

        return NavigationAction.Add;
    }
}
