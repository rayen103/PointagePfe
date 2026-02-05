using CST.LePoint.Securite.DataAccess;
using CST.LePoint.Securite.Entites;
using System.Linq;

namespace CST.LePoint.Securite.Management
{
    public class GestionUtilisateur
    {
        //public static bool EstAutorise(string formName, Actions actions, Utilisateur utilisateur)
        //{
        //    bool bAutorise = false;
        //    if (actions == Actions.Rien)
        //        bAutorise = true;
        //    else
        //    {
        //        var cs = GestionContexteSecurite.ContexteActive;
        //        bAutorise = cs.Set<Utilisateur>().Any(user =>
        //            user.Login == utilisateur.Login &&
        //            user.Roles.Any(r =>
        //                r.Autorisations.Any(au =>
        //                    ((Actions)au.Actions & actions) == actions && au.NomForm == formName)));
        //    }

        //    return bAutorise;
        //}

        public static bool EstAutorise(string formName, Actions actions, Utilisateur utilisateur)
        {
            // Pour gérer les accées du NavBar B.G.N :) 
            bool bAutorise = false;
            if (formName.Remove(0, formName.LastIndexOf('.') + 1).Equals(Actions.Ajouter.ToString()))
            {
                formName = formName.Remove(formName.LastIndexOf('.'));
                actions = Actions.Ajouter;
            }
  
            if (actions == Actions.Rien)
                bAutorise = true;
            else
            {
                IContexteSecurite cs = GestionContexteSecurite.ContexteActive;
                if (utilisateur == null)
                    bAutorise = true;
                else
                {
                    Role role = (Role)GestionContexteSecurite.ContexteActive.Set<Role>().Where(r => r.Nom == utilisateur.CRole).FirstOrDefault();
                    GestionSession.UtilisateurCourant.Roles= new Tools.HashSetSerializable<Role>();
                    GestionSession.UtilisateurCourant.Roles.Add(role);

                    if (role != null)
                    {
                        //utilisateur.Roles.Add(role);
                        if(role.Autorisations.Count>0)
                        bAutorise = cs.Set<Utilisateur>().Any(user =>
                                                              user.Login == utilisateur.Login &&
                                                              user.Roles.Any(r => r.Autorisations.Any(au =>
                                                                                                      (au.Actions & actions) ==
                                                                                                      actions &&
                                                                                                      au.NomForm == formName)));
                        
                    }
                }
            }

            return bAutorise;
        }
    }
}