using System.Data;
namespace CST.LePoint.CtrlLibrary.Search
{
    public class HelperRecherche
    {
        public static DataRow rowSelected = null;

        public static string FindValue = string.Empty;

        public static string FindFieldValue(string sourceTag, string critere, bool bRechercheParCode)
        {
            FindValue = string.Empty;
            sourceTag = sourceTag.Trim().ToUpper();

            FrmRecherche frmRecherche = new FrmRecherche();
            frmRecherche.SourceTag = sourceTag.Replace("\\RQ", string.Empty);
            frmRecherche.Critere = critere;
            frmRecherche.BRechercheParCode = bRechercheParCode;

            frmRecherche.ShowDialog();

            return FindValue;
        }

        public static string FindFieldValue(string sourceTag, string critere, bool bRechercheParCode, bool bLot = false)
        {
            FindValue = string.Empty;
            sourceTag = sourceTag.Trim().ToUpper();

            FrmRecherche frmRecherche = new FrmRecherche();
            frmRecherche.SourceTag = sourceTag.Replace("\\RQ", string.Empty);
            frmRecherche.Critere = critere;
            frmRecherche.BRechercheParCode = bRechercheParCode;
            frmRecherche.BGestionLot = bLot;

            frmRecherche.ShowDialog();

            return FindValue;
        }

        public static string FindFieldValue(string sourceTag, string critere, bool bRechercheParCode, string cEntrepot, bool bActif = true)
        {
            FindValue = string.Empty;
            sourceTag = sourceTag.Trim().ToUpper();

            FrmRecherche frmRecherche = new FrmRecherche();
            frmRecherche.SourceTag = sourceTag.Replace("\\RQ", string.Empty);
            frmRecherche.Critere = critere;
            frmRecherche.CEntrepot = cEntrepot;
            frmRecherche.BActif = bActif;
            frmRecherche.BRechercheParCode = bRechercheParCode;

            frmRecherche.ShowDialog();

            return FindValue;
        }

        public static string FindFieldValue(string sourceTag, string critere, bool bRechercheParCode, string cEntrepot, string cNatureVente, bool bActif = true)
        {
            FindValue = string.Empty;
            sourceTag = sourceTag.Trim().ToUpper();

            FrmRecherche frmRecherche = new FrmRecherche();
            frmRecherche.SourceTag = sourceTag.Replace("\\RQ", string.Empty);
            frmRecherche.Critere = critere;
            frmRecherche.CEntrepot = cEntrepot;
            frmRecherche.BActif = bActif;
            frmRecherche.CNatureVente = cNatureVente;
            frmRecherche.BRechercheParCode = bRechercheParCode;

            frmRecherche.ShowDialog();

            return FindValue;
        }

        public static DataRow FindFieldValueByRow(string sourceTag, string critere, bool bRechercheParCode, DataTable dt)
        {
            rowSelected = null;
            sourceTag = sourceTag.Trim().ToUpper();

            FrmRecherche frmRecherche = new FrmRecherche();
            frmRecherche.SourceTag = sourceTag.Replace("\\RQ", string.Empty);
            frmRecherche.Critere = critere;
            frmRecherche.BRechercheParCode = bRechercheParCode;
            frmRecherche.dataTable = dt;
            frmRecherche.ShowDialog();
            return rowSelected;
        }

    }
}