namespace CollectManagement.Application.Features.Utilisateurs.Commands.CreateUtilisateur;

public record CreateUtilisateurCommand(
    string NomUtilisateur,
    string Nom,
    string Prenom,
    string Email,
    string Password,
    Ulid? RoleUtilisateurId,
    bool IsActive,
    Ulid SocieteId,
    List<Ulid> SiteIds
):IRequest<CreateUtilisateurResponse>;