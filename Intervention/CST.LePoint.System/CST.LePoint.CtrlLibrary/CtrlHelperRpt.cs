using CrystalDecisions.CrystalReports.Engine;
using CST.LePoint.Securite;
using CST.LePoint.Securite.Entites;
using CST.LePoint.Stock.Referentiel.Article;
using CST.LePoint.Stock.Referentiel.Commun;
using CST.LePoint.Tiers.Metier;
using CST.LePoint.Tiers.Referentiel;
using CST.LePoint.Tools;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CST.LePoint.CtrlLibrary
{
    public partial class CtrlHelperRpt
    {
        #region Initialiser les Formulas

        public static void Initialiser_Entete_Pied_Rapport(ReportDocument report, string Title)
        {
            report.DataDefinition.FormulaFields["Titre"].Text = SysHelper.ToCrystalReportFormula(Title);
            report.DataDefinition.FormulaFields["F_Soc"].Text = SysHelper.ToCrystalReportFormula(GestionSession.SocieteCourante.Nom.ToString());
            if (GestionSession.UtilisateurCourant.Nom != null)
                report.DataDefinition.FormulaFields["Iduser"].Text = SysHelper.ToCrystalReportFormula(GestionSession.UtilisateurCourant.Nom.ToString() + " " + GestionSession.UtilisateurCourant.Prenom.ToString());
        }

        public static void Initialiser_Formula_Fournisseur(ReportDocument report, Fournisseur frns)
        {
            report.DataDefinition.FormulaFields["Code_Fnr"].Text = SysHelper.ToCrystalReportFormula(frns.CFournisseur);
            report.DataDefinition.FormulaFields["Raison_Sociale_Fnr"].Text = SysHelper.ToCrystalReportFormula(frns.RaisonSociale);
            report.DataDefinition.FormulaFields["TVA_Fnr"].Text = SysHelper.ToCrystalReportFormula(frns.MatriculeFiscal);
            if (frns.BTVAExonore == true)
                report.DataDefinition.FormulaFields["Exo_TVA_Fnr"].Text = SysHelper.ToCrystalReportFormula("OUI");
            else
                report.DataDefinition.FormulaFields["Exo_TVA_Fnr"].Text = SysHelper.ToCrystalReportFormula("NON");
            int i = 1;
            foreach (Adresse adresse in frns.Adresses)
            {
                report.DataDefinition.FormulaFields[String.Format("adresse{0}_Fnr", i)].Text = SysHelper.ToCrystalReportFormula(adresse.LibAdresse);
                i++;
            }
        }

        public static void Initialiser_Formula_Societe(ReportDocument report, Societe societe)
        {
            SysHelper.ToCrystalReportFormula(societe.Adresse);
            report.DataDefinition.FormulaFields["Adresse_Soc"].Text = SysHelper.ToCrystalReportFormula(societe.Adresse);
            report.DataDefinition.FormulaFields["Tel_Soc"].Text = SysHelper.ToCrystalReportFormula(societe.Telephone);
            report.DataDefinition.FormulaFields["RC_Soc"].Text = SysHelper.ToCrystalReportFormula(societe.RegistreCommerce);
            Banque banque = Banque.Charger(societe.CBanque);
            if (!string.IsNullOrWhiteSpace(societe.CBanque))
            {
                report.DataDefinition.FormulaFields["Banque_Soc"].Text = SysHelper.ToCrystalReportFormula(banque.Libelle);
                SocieteBanque societeBanque = SocieteBanque.Charger(societe.CSociete, societe.CBanque);
                report.DataDefinition.FormulaFields["CCB_Soc"].Text = SysHelper.ToCrystalReportFormula(societeBanque.CompteCourant);
                report.DataDefinition.FormulaFields["Agence_Soc"].Text = SysHelper.ToCrystalReportFormula(societe.Agence);
            }
            report.DataDefinition.FormulaFields["Fax_Soc"].Text = SysHelper.ToCrystalReportFormula(societe.Fax);
            report.DataDefinition.FormulaFields["CD_Soc"].Text = SysHelper.ToCrystalReportFormula(societe.CDouane);
            report.DataDefinition.FormulaFields["TVA_Soc"].Text = SysHelper.ToCrystalReportFormula(societe.CTVA);
        }

        public static void Initialiser_Formula_Referentiel(ReportDocument report, string Title)
        {
            Initialiser_Entete_Pied_Rapport(report, Title);
        }

        public static void Initialiser_Formula_Article(ReportDocument report, string Title, string cCategorie, string cFamille, string cType, string cNature, string cModele, string cModele1, string cModele2)
        {
            Initialiser_Entete_Pied_Rapport(report, Title);

            if (string.IsNullOrEmpty(cCategorie))
                report.DataDefinition.FormulaFields["Categorie_Art"].Text = SysHelper.ToCrystalReportFormula("Tous");
            else
                report.DataDefinition.FormulaFields["Categorie_Art"].Text = SysHelper.ToCrystalReportFormula(cCategorie);

            if (string.IsNullOrEmpty(cFamille))
                report.DataDefinition.FormulaFields["Famille_Art"].Text = SysHelper.ToCrystalReportFormula("Tous");
            else
                report.DataDefinition.FormulaFields["Famille_Art"].Text = SysHelper.ToCrystalReportFormula(cFamille);

            if (string.IsNullOrEmpty(cType))
                report.DataDefinition.FormulaFields["Type_Art"].Text = SysHelper.ToCrystalReportFormula("Tous");
            else
                report.DataDefinition.FormulaFields["Type_Art"].Text = SysHelper.ToCrystalReportFormula(cType);

            if (string.IsNullOrEmpty(cModele))

                report.DataDefinition.FormulaFields["Modele_Art"].Text = SysHelper.ToCrystalReportFormula("Tous");
            else
                report.DataDefinition.FormulaFields["Modele_Art"].Text = SysHelper.ToCrystalReportFormula(cModele);

            if (string.IsNullOrEmpty(cModele1))
                report.DataDefinition.FormulaFields["Sous_Modele1"].Text = SysHelper.ToCrystalReportFormula("Tous");
            else
                report.DataDefinition.FormulaFields["Sous_Modele1"].Text = SysHelper.ToCrystalReportFormula(cModele1);

            if (string.IsNullOrEmpty(cModele2))

                report.DataDefinition.FormulaFields["Sous_Modele2"].Text = SysHelper.ToCrystalReportFormula("Tous");
            else
                report.DataDefinition.FormulaFields["Sous_Modele2"].Text = SysHelper.ToCrystalReportFormula(cModele2);

            if (string.IsNullOrEmpty(cNature))
                report.DataDefinition.FormulaFields["Nature_Art"].Text = SysHelper.ToCrystalReportFormula("Tous");
            else
                report.DataDefinition.FormulaFields["Nature_Art"].Text = SysHelper.ToCrystalReportFormula(cNature);

        }

        public static void Initialiser_Formula_Article(ReportDocument report, string Title, string cCategorie, string cFamille, string cType, string cNature, string cModele, string cModele1, string cModele2, string cTarif)
        {
            Initialiser_Formula_Article(report, Title, cCategorie, cFamille, cType, cNature, cModele, cModele1, cModele2);

            if (string.IsNullOrEmpty(cTarif))
                report.DataDefinition.FormulaFields["Tarif"].Text = SysHelper.ToCrystalReportFormula("Tous");
            else
                report.DataDefinition.FormulaFields["Tarif"].Text = SysHelper.ToCrystalReportFormula(cTarif);
        }

        public static void Initialiser_Formula_Article(ReportDocument report, string Title, string cCategorie, string cFamille, string cType, string cNature, string cModele, string cModele1, string cModele2, DateTime dateDeb, DateTime dateFin)
        {
            Initialiser_Formula_Article(report, Title, cCategorie, cFamille, cType, cNature, cModele, cModele1, cModele2);
            report.DataDefinition.FormulaFields["Date_Debut"].Text = String.Format("'{0}'", dateDeb.ToShortDateString());
            report.DataDefinition.FormulaFields["Date_Fin"].Text = String.Format("'{0}'", dateFin.ToShortDateString());
        }

        public static void Initialiser_Formula_Article(ReportDocument report, string Title, string cCategorie, string cFamille, string cType, string cNature, string cModele, string cModele1, string cModele2, DateTime dateDeb, DateTime dateFin, string cEntrepot)
        {
            Initialiser_Formula_Article(report, Title, cCategorie, cFamille, cType, cNature, cModele, cModele1, cModele2, dateDeb, dateFin);
            if (string.IsNullOrEmpty(cEntrepot))
                report.DataDefinition.FormulaFields["Entrepot_Art"].Text = "'Tous'";
            else
                report.DataDefinition.FormulaFields["Entrepot_Art"].Text = SysHelper.ToCrystalReportFormula(cEntrepot);
        }

        public static void Initialiser_Formula_Mouvements(ReportDocument report, string Title, DateTime dateDeb, DateTime dateFin)
        {
            Initialiser_Entete_Pied_Rapport(report, Title);
            report.DataDefinition.FormulaFields["Date_Debut"].Text = String.Format("'{0}'", dateDeb.ToShortDateString());
            report.DataDefinition.FormulaFields["Date_Fin"].Text = String.Format("'{0}'", dateFin.ToShortDateString());
        }

        #endregion Initialiser les Formulas

        #region Conversion en Toute Lettre

        public static string Convertir_EnToutelettre(decimal MontantTTC)
        {
            string numeroLettre = "";
            //using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            //{
            //    cn.Open();
            //    SqlCommand cmd = new SqlCommand();
            //    cmd.Connection = cn;
            //    cmd.CommandType = CommandType.Text;
            //    cmd.CommandText = "SELECT dbo.Convert_to_lettres(" + MontantTTC + ")";
            //    SqlDataReader dr = cmd.ExecuteReader();
            //    try
            //    {
            //        while (dr.Read())
            //        {
            //            if (dr[""] != DBNull.Value)
            //                numeroLettre = dr[""].ToString();
            //        }
            //        dr.Close();
            //    }
            //    catch
            //    {
            //        numeroLettre = MontantTTC.ToString("### ### ### ### ### ##0.000");
            //    }
            //}
            try
            {
                NombreTouteLettre c = new NombreTouteLettre();
                c.setMontant(MontantTTC.ToString().Replace(" ", ""));
                numeroLettre = c.calculer_glob();
            }
            catch
            {
                numeroLettre = MontantTTC.ToString("### ### ### ### ### ##0.000");
            }
            return numeroLettre;
        }

        #endregion Conversion en Toute Lettre

        #region Conversion en Currency

        public static string Convertir_En_Currency(decimal Montant)
        {
            string MontantCurrency = string.Empty;
            MontantCurrency = String.Format("'{0}'", Montant.ToString("### ### ### ### ### ##0.000"));
            return MontantCurrency;
        }

        #endregion Conversion en Currency
    }
}