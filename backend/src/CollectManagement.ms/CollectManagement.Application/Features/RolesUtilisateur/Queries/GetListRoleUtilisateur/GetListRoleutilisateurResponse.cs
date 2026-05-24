namespace CollectManagement.Application.Features.RolesUtilisateur.Queries.GetListRoleUtilisateur;

public record GetListRoleutilisateurResponse(
    List<GetListRoleUtilisateurDto> RolesUtilisateur,
    int Length);
    
public record GetListRoleUtilisateurDto(
    Ulid RoleUtilisateurId,
    string LibelleRoleUtilisateur,
    Ulid? SocieteId,
    List<GetListRoleUtilisateurNavigation> Navigations);
public record GetListRoleUtilisateurNavigation(
    string NavigationId,
    List<int> Actions
);