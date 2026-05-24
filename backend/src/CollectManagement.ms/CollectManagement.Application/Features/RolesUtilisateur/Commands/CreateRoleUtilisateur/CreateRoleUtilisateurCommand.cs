namespace CollectManagement.Application.Features.RolesUtilisateur.Commands.CreateRoleUtilisateur;

public record CreateRoleUtilisateurCommand(
    string LibelleRoleUtilisateur,
    Ulid? SocieteId,
    List<CreateRoleUtilisateurNavigation> Navigations
): IRequest<CreateRoleUtilisateurResponse>;

public record CreateRoleUtilisateurNavigation(
    string NavigationId,
    List<int> Actions,
    List<CreateRoleUtilisateurSection> Sections
);
    
public record CreateRoleUtilisateurSection(
    string SectionId,
    List<int> Actions
);