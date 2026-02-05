using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CST.LePoint.CtrlLibrary;
using System.Data.SqlClient;
using System.Configuration;
using CST.LePoint.Securite.Entites;
using CST.LePoint.Tiers.Metier;
using CST.LePoint.Intervention.Properties;
using CST.LePoint.CtrlLibrary.Search;
using CST.LePoint.Intervention.Metier;
using CST.LePoint.Stock.Referentiel.Commun;
using CST.LePoint.Tiers.Referentiel;
using CST.LePoint.Securite;
namespace CST.LePoint.Intervention.Rattachements
{
    public partial class FrmEmployeListe : DevExpress.XtraEditors.XtraForm, IActionsSave
    {
        public FrmEmployeListe()
        {
            InitializeComponent();
        }

        #region Utilitaire
        private void RemplirGridVListe()
        {

            DataTable dtListe = new DataTable();

            try
            {

                CtrlHelper.InitGridView(gridView1, TitresEmploye());

                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Employe_Charger";
                    cmd.Parameters.AddWithValue("@RFID_Emp", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Code_Societe", GestionSession.SocieteCourante.CSociete);
                    cmd.Parameters.AddWithValue("@Code_Site", GestionSession.SocieteSite);
                   
                    //cmd.Parameters.AddWithValue("@DateLivraisonFin", DateTime.Parse(txtDateFin.Text).AddDays(1).AddSeconds(-1));
                    
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dtListe);
                }

                CtrlHelper.FillGridView(gridView1, TitresEmploye(), dtListe);
                //this.gridVEntete.Columns[4].Width = 400;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public GvColumnProprietes TitresEmploye()
        {
            GvColumnProprietes proprietes = new GvColumnProprietes();

            proprietes.Add(new GvColumnPropriete("Matricule"));
            proprietes.Add(new GvColumnPropriete("RFID"));
            proprietes.Add(new GvColumnPropriete("Nom"));
            proprietes.Add(new GvColumnPropriete("Prénom"));
            proprietes.Add(new GvColumnPropriete("Circuit", GvColumnPropriete.GvColumnType.LookUpVide, GvColumnPropriete.GvColumnEtat.Disable, CircuitCollection.Charger()));
            proprietes.Add(new GvColumnPropriete("Point Collecte", GvColumnPropriete.GvColumnType.LookUpVide, GvColumnPropriete.GvColumnEtat.Disable, PointCollecteCollection.Charger()));
            proprietes.Add(new GvColumnPropriete("Shift", GvColumnPropriete.GvColumnType.LookUpVide, GvColumnPropriete.GvColumnEtat.Disable, ShiftCollection.Charger()));
            proprietes.Add(new GvColumnPropriete("Adresse"));
            proprietes.Add(new GvColumnPropriete("Gouvernourat", GvColumnPropriete.GvColumnType.LookUpVide, GvColumnPropriete.GvColumnEtat.Disable, GouvernoratCollection.Charger()));
            proprietes.Add(new GvColumnPropriete("Region", GvColumnPropriete.GvColumnType.LookUpVide, GvColumnPropriete.GvColumnEtat.Disable, RegionCollection.Charger()));
            return proprietes;
        }

        #endregion 

        private void FrmEmployeListe_Load(object sender, EventArgs e)
        {
            CtrlHelper.FillLookUpEdit(this.lkpCircuit, CircuitCollection.Charger());
            CtrlHelper.FillLookUpEdit(this.lkpPC, PointCollecteCollection.Charger());
            CtrlHelper.FillLookUpEdit(this.lkpGouv, GouvernoratCollection.Charger());
            CtrlHelper.FillLookUpEdit(this.lkpRg, RegionCollection.Charger());
            CtrlHelper.FillLookUpEdit(this.lkpShift, ShiftCollection.Charger_Group());

            RemplirGridVListe();

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            if (this.gridView1.RowCount == 0)
                return;
            this.txtMatricule.Text = this.gridView1.GetFocusedRowCellDisplayText("Matricule");
            this.txtRFID.Text = this.gridView1.GetFocusedRowCellDisplayText("RFID");
            this.txtNom.Text = this.gridView1.GetFocusedRowCellDisplayText("Nom");
            this.txtPrenom.Text = this.gridView1.GetFocusedRowCellDisplayText("Prénom");
            this.lkpCircuit.EditValue = this.gridView1.GetFocusedRowCellValue("Circuit");
            this.lkpPC.EditValue = this.gridView1.GetFocusedRowCellValue("Point Collecte");
            this.lkpShift.EditValue = this.gridView1.GetFocusedRowCellValue("Shift");
            this.txtAdresse.Text = this.gridView1.GetFocusedRowCellDisplayText("Adresse");
            this.lkpGouv.EditValue = this.gridView1.GetFocusedRowCellValue("Gouvernourat");
            this.lkpRg.EditValue = this.gridView1.GetFocusedRowCellValue("Region");
            //this.lkpRg.
        }

        public void Enregistrer(bool enregistrerEtFermer)
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    Employe employe = new Employe();
                    employe.Code = this.txtRFID.Text;
                    employe.Libelle = this.txtMatricule.Text;

                    employe.RFID_Emp = this.txtRFID.Text;
                    employe.Matricule_Emp = this.txtMatricule.Text;                   
                    employe.Nom_Emp = this.txtNom.Text;
                    employe.Prenom_Emp = this.txtPrenom.Text;
                    employe.Code_Circuit_Emp = this.lkpCircuit.EditValue.ToString();
                    employe.Code_PC_Emp = this.lkpPC.EditValue.ToString();
                    employe.Code_Shift = this.lkpShift.EditValue.ToString();
                    employe.Adresse = this.txtAdresse.Text;
                    employe.Code_Gouv_Emp = this.lkpGouv.EditValue.ToString();
                    employe.Code_Region_Emp = this.lkpRg.EditValue.ToString();
                    employe.Sauvegarder(transaction);
                    transaction.Commit();
                    RemplirGridVListe();
                    Actualiser();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public void Actualiser()
        {
            this.txtMatricule.Text = string.Empty;
            this.txtRFID.Text = string.Empty;
            this.txtNom.Text = string.Empty;
            this.txtPrenom.Text = string.Empty;
            this.lkpCircuit.EditValue = string.Empty;
            this.lkpPC.EditValue = string.Empty;
            this.lkpShift.EditValue = string.Empty;
            this.txtAdresse.Text = string.Empty;
            this.lkpGouv.EditValue = string.Empty;
            this.lkpRg.EditValue = string.Empty;
            RemplirGridVListe();
        }

        private void checkEdit2_CheckedChanged(object sender, EventArgs e)
        {

        }

        public void Supprimer()
        {
            string rfid = this.gridView1.GetFocusedRowCellDisplayText("RFID");
            if (this.gridView1.RowCount == 0)
                return;

            DialogResult dialogResult = XtraMessageBox.Show("Voulez-vous Supprimer cet employe ?",
                                                Resources.NomApplication,
                                                MessageBoxButtons.YesNoCancel,
                                                MessageBoxIcon.Question,
                                                MessageBoxDefaultButton.Button1);
            if (dialogResult != DialogResult.Yes)
                return;
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    Employe employe = Employe.Charger(rfid);
                    
                    employe.Supprimer(transaction);
                    transaction.Commit();
                    XtraMessageBox.Show("Suppression avec Succés",
                                            Resources.NomApplication,
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information,
                                            MessageBoxDefaultButton.Button1);
                    Actualiser();
                }
                catch
                {
                    XtraMessageBox.Show("Suppression échoué",
                                            Resources.NomApplication,
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Error,
                                            MessageBoxDefaultButton.Button1);
                }
                finally
                {
                    cn.Close();
                }
            }
        }

        private void lkpEquipe_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Delete) || (e.KeyCode == Keys.Back))
            {
                if ((e.KeyCode == Keys.Delete) || (e.KeyCode == Keys.Back))
                {
                   // this.lkpEquipe.EditValue = string.Empty;
                }
            }
        }

        private void labelControl8_Click(object sender, EventArgs e)
        {

        }

        private void lkpCircuit_EditValueChanged(object sender, EventArgs e)
        {
            CtrlHelper.FillLookUpEdit(this.lkpPC, CircuitPointCollecteCollection.ChargerPC(this.lkpCircuit.EditValue.ToString()));            
        }

        
    }
}