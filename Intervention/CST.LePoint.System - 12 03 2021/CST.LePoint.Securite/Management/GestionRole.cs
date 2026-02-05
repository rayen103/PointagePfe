using CST.LePoint.Securite.DataAccess;
using CST.LePoint.Securite.Entites;
using System.Linq;

namespace CST.LePoint.Securite.Management
{
    public class GestionRole
    {
        public static bool EstAffecte(string formName, Actions actions, Role role)
        {
            bool bAutorise;
            if (actions == Actions.Rien)
                bAutorise = true;
            else
            {
                IContexteSecurite cs = GestionContexteSecurite.ContexteActive;
                bAutorise = cs.Set<Role>().Any(r =>
                                               r.Id == role.Id &&
                                               r.Autorisations.Any(au =>
                                                                   (au.Actions & actions) == actions &&
                                                                   au.NomForm == formName));
            }

            return bAutorise;
        }

        public static bool EstAffecte(string formName, Actions actions, Role role, bool bInitialiser)
        {
            bool bAutorise;
            if (actions == Actions.Rien)
                bAutorise = true;
            else
            {
                IContexteSecurite cs = GestionContexteSecurite.ContexteActive;
                bAutorise = cs.Set<Role>().Any(r =>
                                               r.Id == role.Id &&
                                               r.Autorisations.Any(au =>
                                                                   (au.Actions & actions) == actions &&
                                                                   au.NomForm == formName));
            }

            return bAutorise;
        }
    }
}