using CST.LePoint.Securite.DataAccess;
using CST.LePoint.Securite.Entites;
using System.Collections.Generic;
using System.Linq;

namespace CST.LePoint.Securite.Management
{
    public class GestionAutorisation
    {
        public static void AttribuerAutorisation(Role role, Autorisation autorisation)
        {
            //if (autorisation.Id != Guid.Empty)
            //    throw new ArgumentException("l'autorisation ne doit pas être fournit de la base");

            IContexteSecurite cs = GestionContexteSecurite.ContexteActive;

            role = cs.Set<Role>().First(r => r.Id == role.Id);
            if (autorisation.Actions == Actions.Rien)
            {
                var aut = role.Autorisations.FirstOrDefault(au => au.NomForm == autorisation.NomForm);
                if (aut != null)
                {
                    if (!AutorisationEstAttribuerAPlusieursRole(aut))
                    {
                        cs.Set<Autorisation>().Remove(aut);
                    }
                    role.Autorisations.Remove(aut);
                }
            }
            else
            {
                if (!role.Autorisations.Any(
                        au => au.Actions == autorisation.Actions && au.NomForm == autorisation.NomForm))
                {
                    var autDansCs =
                        cs.Set<Autorisation>().FirstOrDefault(
                            au => au.NomForm == autorisation.NomForm && au.Actions == autorisation.Actions);
                    if (autDansCs == null)
                    {
                        var autRoleAvecNomForm =
                            role.Autorisations.FirstOrDefault(au => au.NomForm == autorisation.NomForm);
                        if (autRoleAvecNomForm == null)
                        {
                            cs.Set<Autorisation>().Add(autorisation);
                            role.Autorisations.Add(autorisation);
                        }
                        else
                        {
                            if (AutorisationEstAttribuerAPlusieursRole(autRoleAvecNomForm))
                            {
                                cs.Set<Autorisation>().Add(autorisation);
                                role.Autorisations.Remove(autRoleAvecNomForm);
                                role.Autorisations.Add(autorisation);
                            }
                            else
                            {
                                autRoleAvecNomForm.Actions = autorisation.Actions;
                            }
                        }
                    }
                    else role.Autorisations.Add(autDansCs);
                }
            }
        }

        private static bool AutorisationEstAttribuerAPlusieursRole(Autorisation autorisation)
        {
            return (GetRoles(autorisation).Count() > 1);
        }

        private static IEnumerable<Role> GetRoles(Autorisation aut)
        {
            IContexteSecurite cs = GestionContexteSecurite.ContexteActive;
            return cs.Set<Role>().Where(r => r.Autorisations.Any(a => a.NomForm == aut.NomForm && a.Actions == aut.Actions));
        }
    }
}