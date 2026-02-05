using CST.LePoint.Securite.Entites;

namespace CST.LePoint.Securite
{
    public abstract class GestionSession
    {
        public static Utilisateur UtilisateurCourant;
        public static Societe SocieteCourante;
        public static bool SecuriteActive = false;
        public static string SocieteSite;
    }
}