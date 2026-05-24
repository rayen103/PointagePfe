using CollectManagement.Domain.Common;
using CollectManagement.Domain.Sites;
using CollectManagement.Domain.Sites.ValueObjects;
using CollectManagement.Domain.Utilisateurs.ValueObjects;

namespace CollectManagement.Domain.Utilisateurs.Entities;

public class UtilisateurSite : AuditableEntity
{
    public UtilisateurId UtilisateurId { get; private set; }
    public Utilisateur Utilisateur { get; private set; }
    public SiteId SiteId { get; private set; }
    public Site Site { get; private set; }

    private UtilisateurSite(UtilisateurId utilisateurId, SiteId siteId)
    {
        UtilisateurId = utilisateurId;
        SiteId = siteId;
    }

    public static UtilisateurSite Create(UtilisateurId utilisateurId, SiteId siteId)
    {
        return new UtilisateurSite(utilisateurId, siteId);
    }
}