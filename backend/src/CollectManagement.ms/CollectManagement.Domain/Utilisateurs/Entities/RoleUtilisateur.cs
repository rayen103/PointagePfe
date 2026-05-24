using CollectManagement.Domain.Common;
using CollectManagement.Domain.Societes;
using CollectManagement.Domain.Societes.ValueObjects;
using CollectManagement.Domain.Utilisateurs.ValueObjects;

namespace CollectManagement.Domain.Utilisateurs.Entities;

public class RoleUtilisateur : AuditableEntity
{
    private readonly List<Navigation> _navigations = [];
    
    public RoleUtilisateurId RoleUtilisateurId { get; private set; }
    public string LibelleRoleUtilisateur { get; private set; }
    public SocieteId? SocieteId { get; private set; }
    public Societe? Societe { get; private set; }
    
    public IEnumerable<Navigation> Navigations => _navigations;

    private RoleUtilisateur(RoleUtilisateurId roleUtilisateurId, string libelleRoleUtilisateur, List<Navigation> navigations, SocieteId? societeId)
    {
        RoleUtilisateurId = roleUtilisateurId;
        LibelleRoleUtilisateur = libelleRoleUtilisateur;
        _navigations = navigations;
        SocieteId = societeId;
    }

    public static RoleUtilisateur Create(RoleUtilisateurId roleUtilisateurId, string libelleRoleUtilisateur, List<Navigation> navigations, SocieteId? societeId)
    {
        return new RoleUtilisateur(roleUtilisateurId, libelleRoleUtilisateur, navigations, societeId);
    }

    public void Update(string libelleRoleUtilisateur, List<Navigation> navigations, SocieteId? societeId)
    {
        LibelleRoleUtilisateur = libelleRoleUtilisateur;
        _navigations.Clear();
        _navigations.AddRange(navigations);
        SocieteId = societeId;
    }
    
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private RoleUtilisateur(){}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
}