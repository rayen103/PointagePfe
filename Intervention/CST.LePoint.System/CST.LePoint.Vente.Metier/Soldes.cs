using CST.LePoint.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST.LePoint.Vente.Metier
{
   public class Soldes
    {
       public static string SoldeAnterieur(DateTime dateDebut,  string nomChamps)
       {
           string sqlSoldeAnterieur = string.Empty;
           sqlSoldeAnterieur = SysHelper.RetourChariot(sqlSoldeAnterieur) + " , ISNULL((SELECT SUM(ISNULL(F.CreditFacture,0)) FROM Facture F (NOLOCK) WHERE F.CreditFacture <> 0  AND C.CClient = F.CClient AND ISNULL(F.BContentieux,0) = 0 AND F.DateFacture <" + SysHelper.ToSqlDatetime(dateDebut) + "),0)";
           sqlSoldeAnterieur = SysHelper.RetourChariot(sqlSoldeAnterieur) + " - ISNULL((SELECT SUM(ISNULL(R.ResteReglement,0)) FROM Reglement R (NOLOCK) WHERE ISNULL(R.BAnnulation,0) = 0 AND ISNULL(R.BContentieux,0) = 0 AND ISNULL(R.BGarantie,0) = 0 AND R.ResteReglement <> 0  AND C.CClient = R.CClient AND R.CEtatReglement <> '" + VenteHelper.EtatReglement.IMP.ToString() + "'  AND R.DateEmission <" + SysHelper.ToSqlDatetime(dateDebut) + ") ,0)";
           sqlSoldeAnterieur = SysHelper.RetourChariot(sqlSoldeAnterieur) + " + ISNULL((SELECT SUM(ISNULL(R.MontantImpaye,0)) FROM Reglement R (NOLOCK) WHERE ISNULL(R.BAnnulation,0) = 0 AND ISNULL(R.BContentieux,0) = 0 AND C.CClient = R.CClient AND R.CEtatReglement = '" + VenteHelper.EtatReglement.IMP.ToString() + "'  AND  R.DateAvis <" + SysHelper.ToSqlDatetime(dateDebut) + ") ,0)" + nomChamps;
           return sqlSoldeAnterieur;
       }
       public static string SoldeAnterieur(DateTime dateDebut, string nomChamps, bool Vendeur)
       {
           string sqlSoldeAnterieur = string.Empty;
           sqlSoldeAnterieur = SysHelper.RetourChariot(sqlSoldeAnterieur) + " ISNULL((select SUM( ISNULL(CreditFacture,0))from Facture F where CreditFacture <>0 and C.CClient=F.CClient AND F.DateFacture <" + SysHelper.ToSqlDatetime(dateDebut) + "),0)";
           sqlSoldeAnterieur = SysHelper.RetourChariot(sqlSoldeAnterieur) + "-ISNULL(( select sum(ISNULL(ResteReglement,0)) from Reglement R where ResteReglement<>0 and C.CClient=R.CClient AND CEtatReglement <>'IMP'  AND R.DateEmission <" + SysHelper.ToSqlDatetime(dateDebut) + ") ,0)";
           sqlSoldeAnterieur = SysHelper.RetourChariot(sqlSoldeAnterieur) + " +ISNULL(( select sum(ISNULL(MontantImpaye,0)) from Reglement R where C.CClient=R.CClient AND CEtatReglement ='IMP'  AND  R.DateEcheance <" + SysHelper.ToSqlDatetime(dateDebut) + ") ,0)" + nomChamps;
           return sqlSoldeAnterieur;
       }
       public static string SoldeFacture(DateTime dateDebut, DateTime dateFin, string nomChamps )
       {
           string sqlSoldeFacture = string.Empty;
           sqlSoldeFacture = SysHelper.RetourChariot(sqlSoldeFacture) + "SELECT SUM(ISNULL(F.CreditFacture,0))  FROM Facture F";
           sqlSoldeFacture = SysHelper.RetourChariot(sqlSoldeFacture) + "WHERE F.CreditFacture<>0 AND F.DateFacture >= " + SysHelper.ToSqlDatetime(dateDebut);
           sqlSoldeFacture = SysHelper.RetourChariot(sqlSoldeFacture) + "AND F.DateFacture <= " + SysHelper.ToSqlDatetime(dateFin);

           return sqlSoldeFacture;
       } 



    }
}
