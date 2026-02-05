using CST.LePoint.CtrlLibrary;
using CST.LePoint.Intervention.Metier;
using CST.LePoint.Intervention.Properties;
using CST.LePoint.Securite;
using CST.LePoint.Securite.Entites;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CST.LePoint.Intervention.Rattachements
{
    public partial class FrmDeplacementClientCircuit : DevExpress.XtraEditors.XtraForm, IActionsSave
    {
        public FrmDeplacementClientCircuit()
        {
            InitializeComponent();
        }

        #region Titres

        private GvColumnProprietes TitresCircuitS()
        {
            GvColumnProprietes proprietes = new GvColumnProprietes();
            proprietes.Add(new GvColumnPropriete("Circuit"));
            proprietes.Add(new GvColumnPropriete("Libellé"));
            proprietes.Add(new GvColumnPropriete("Équipe", GvColumnPropriete.GvColumnType.LookUpVide, EquipeCollection.Charger()));
            proprietes.Add(new GvColumnPropriete("État"));

            return proprietes;
        }

        private GvColumnProprietes TitresClientS()
        {
            GvColumnProprietes proprietes = new GvColumnProprietes();
            proprietes.Add(new GvColumnPropriete("C. Client"));
            proprietes.Add(new GvColumnPropriete("Raison Sociale"));
            proprietes.Add(new GvColumnPropriete("Statut", GvColumnPropriete.GvColumnEtat.Invisible));

            return proprietes;
        }

        private GvColumnProprietes TitresCircuitD()
        {
            GvColumnProprietes proprietes = new GvColumnProprietes();
            proprietes.Add(new GvColumnPropriete("Circuit"));
            proprietes.Add(new GvColumnPropriete("Libellé"));
            proprietes.Add(new GvColumnPropriete("Équipe", GvColumnPropriete.GvColumnType.LookUpVide, EquipeCollection.Charger()));
            proprietes.Add(new GvColumnPropriete("État"));

            return proprietes;
        }

        private GvColumnProprietes TitresClientD()
        {
            GvColumnProprietes proprietes = new GvColumnProprietes();
            proprietes.Add(new GvColumnPropriete("C. Client"));
            proprietes.Add(new GvColumnPropriete("Raison Sociale"));
            proprietes.Add(new GvColumnPropriete("Statut", GvColumnPropriete.GvColumnEtat.Invisible));

            return proprietes;
        }

        #endregion

        #region Remplir Grid

        private void RemplirGridVCircuitS()
        {
            DataTable dtListe = new DataTable();
            try
            {
                CtrlHelper.InitGridView(this.gridVCircuitS, TitresCircuitS());
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Circuit_Charger_Deplacement_Source";
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dtListe);
                }
                CtrlHelper.FillGridView(this.gridVCircuitS, TitresCircuitS(), dtListe);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void RemplirGridVCircuitD()
        {
            DataTable dtListe = new DataTable();
            try
            {
                CtrlHelper.InitGridView(this.gridVCircuitD, TitresCircuitD());
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Circuit_Charger_Deplacement_Destinataire";
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dtListe);
                }
                CtrlHelper.FillGridView(this.gridVCircuitD, TitresCircuitD(), dtListe);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void RemplirGridVClientS(string circuit)
        {
            DataTable dtListe = new DataTable();

            try
            {
                CtrlHelper.InitGridView(gridVClientS, TitresClientS());
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Client_Charger_Deplacement_Source";
                    cmd.Parameters.AddWithValue("@CCircuit", circuit);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dtListe);
                }
                CtrlHelper.FillGridView(gridVClientS, TitresClientS(), dtListe);
                this.gridVClientS.OptionsSelection.MultiSelect = true;
                this.gridVClientS.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;

            }
            catch (Exception)
            {
                throw;
            }
        }

        private void RemplirGridVClientD(string circuit)
        {
            DataTable dtListe = new DataTable();

            try
            {
                CtrlHelper.InitGridView(gridVClientD, TitresClientD());
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Client_Charger_Deplacement_Destinataire";
                    cmd.Parameters.AddWithValue("@CCircuit", circuit);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dtListe);
                }

                CtrlHelper.FillGridView(gridVClientD, TitresClientD(), dtListe);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Management

        private void LoadData(){
            this.RemplirGridVCircuitS();
            this.RemplirGridVCircuitD();
            CtrlHelper.InitGridView(gridVClientS, TitresClientS());
            CtrlHelper.InitGridView(gridVClientD, TitresClientD());
        }

        private void FrmDeplacementClientCircuit_Load(object sender, EventArgs e)
        {
            this.LoadData();
        }

        #endregion
        
        #region event

        int CCColorS = -1;
        private void gridVCircuitS_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            string circuitS = this.gridVCircuitS.GetFocusedRowCellDisplayText("Circuit");
            string circuitD = this.gridVCircuitD.GetFocusedRowCellDisplayText("Circuit");
            if (circuitD == circuitS) {
                this.gridVCircuitS.FocusedRowHandle = CCColorS;
                return; 
            }
            CCColorS = this.gridVCircuitS.FocusedRowHandle;
            CCColorD = this.gridVCircuitD.FocusedRowHandle;
            this.gridVCircuitS.RefreshData();
            this.gridVCircuitD.RefreshData();
            this.RemplirGridVClientS(circuitS);
            this.RemplirGridVClientD(circuitD);
        }
        
        int CCColorD = -1;
        private void gridVCircuitD_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            string circuitS = this.gridVCircuitS.GetFocusedRowCellDisplayText("Circuit");
            string circuitD = this.gridVCircuitD.GetFocusedRowCellDisplayText("Circuit");
            if (circuitD == circuitS)
            {
                this.gridVCircuitD.FocusedRowHandle = CCColorD;
                return;
            }
            CCColorS = this.gridVCircuitS.FocusedRowHandle;
            CCColorD = this.gridVCircuitD.FocusedRowHandle;
            this.gridVCircuitS.RefreshData();
            this.gridVCircuitD.RefreshData();
            this.RemplirGridVClientS(circuitS);
            this.RemplirGridVClientD(circuitD);
        }

        private void gridVCircuitS_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView view = sender as GridView;
            //string CC = e.RowHandle > -1 ? view.GetRowCellDisplayText(e.RowHandle, "Circuit") : "";
            if (e.RowHandle == CCColorS)
            {
                e.Appearance.BackColor = Color.CornflowerBlue;
                e.Appearance.ForeColor = Color.Black;
                e.HighPriority = true;
            }
        }

        private void gridVCircuitD_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView view = sender as GridView;
            //string CC = e.RowHandle > -1 ? view.GetRowCellDisplayText(e.RowHandle, "Circuit") : "";
            if (e.RowHandle == CCColorD)
            {
                e.Appearance.BackColor = Color.PaleTurquoise;
                e.Appearance.ForeColor = Color.Black;
                e.HighPriority = true;
            }
        }

        private void gridVClientD_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView view = sender as GridView;
            string CC = e.RowHandle > -1 ? view.GetRowCellDisplayText(e.RowHandle, "Statut") : "";
            if (CC == "New")
            {
                e.Appearance.BackColor = Color.CadetBlue;
                e.Appearance.ForeColor = Color.Black;
                e.HighPriority = true;
            }
        }

        private void gridVClientS_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView view = sender as GridView;
            string CC = e.RowHandle > -1 ? view.GetRowCellDisplayText(e.RowHandle, "Statut") : "";
            if (CC == "Annuler")
            {
                e.Appearance.BackColor = Color.Pink;
                e.Appearance.ForeColor = Color.Black;
                e.HighPriority = true;
            }
        }

        #endregion

        #region Action

        private void BtnDeplacer_Click(object sender, EventArgs e)
        {
            if (this.gridVClientS.RowCount == this.gridVClientS.SelectedRowsCount)
            {
                DialogResult dialogResult = XtraMessageBox.Show("Le circuit doit comporter au moins un client!",
                                                       Resources.NomApplication,
                                                       MessageBoxButtons.OK,
                                                       MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            foreach (int i in this.gridVClientS.GetSelectedRows())
            {
                DataTable dt = gridControl3.DataSource as DataTable;
                DataRow newRow = dt.NewRow();
                newRow["C. Client"] = this.gridVClientS.GetRowCellDisplayText(i, "C. Client");
                newRow["Raison Sociale"] = this.gridVClientS.GetRowCellDisplayText(i, "Raison Sociale");
                newRow["Statut"] = "New";
                dt.Rows.InsertAt(newRow, 0);
            }
            this.gridVClientS.DeleteSelectedRows();
            this.gridVClientS.RefreshData();
        }

        private void BtnAnnuler_Click(object sender, EventArgs e)
        {
            foreach (int i in this.gridVClientD.GetSelectedRows())
            {
                string status = this.gridVClientD.GetRowCellDisplayText(i, "Statut");
                if (status != "New") continue;
                DataTable dt = gridControl2.DataSource as DataTable;
                DataRow newRow = dt.NewRow();
                newRow["C. Client"] = this.gridVClientD.GetRowCellDisplayText(i, "C. Client");
                newRow["Raison Sociale"] = this.gridVClientD.GetRowCellDisplayText(i, "Raison Sociale");
                newRow["Statut"] = "Annuler";
                dt.Rows.InsertAt(newRow, 0);
                this.gridVClientD.DeleteRow(i);
            }
            this.gridVClientD.RefreshData();
        }

        public void Enregistrer(bool enregistrerEtFermer)
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                var waitForm = new DevExpress.Utils.WaitDialogForm("Chargement en cours...",
                          "Veuillez patienter!");
                try
                {
                    string ccircuit = this.gridVCircuitD.GetFocusedRowCellDisplayText("Circuit");
                    string ccircuitS = this.gridVCircuitS.GetFocusedRowCellDisplayText("Circuit");
                    string CEquipe = this.gridVCircuitD.GetFocusedRowCellValue("Équipe").ToString(); ;
                    string Etat = this.gridVCircuitD.GetFocusedRowCellDisplayText("État");
                    bool deplaced = false;
                    for (int i = 0; i < this.gridVClientD.RowCount; i++)
                    {
                        string statut = this.gridVClientD.GetRowCellDisplayText(i, "Statut");
                        string cclient = this.gridVClientD.GetRowCellDisplayText(i, "C. Client");
                        string raisonsocile = this.gridVClientD.GetRowCellDisplayText(i, "Raison Sociale");
                        if (statut == "New")
                        {
                            deplaced = true;
                            ConventionClient.AnnulerplannificationClient_Circuit(transaction, ccircuitS, cclient);

                            CircuitPointCollecte dcircuitDetail = new CircuitPointCollecte();
                            //dcircuitDetail.CClient = cclient;
                            //dcircuitDetail.CCircuit = ccircuitS;
                            dcircuitDetail.Supprimer(transaction);

                            CircuitPointCollecte circuitDetail = new CircuitPointCollecte();
                            //circuitDetail.CCircuit = ccircuit;
                            //circuitDetail.CClient = cclient;
                            circuitDetail.Latitude = 0;
                            circuitDetail.Longitude = 0;
                            circuitDetail.Sauvegarder(transaction);

                            if (Etat == "PLANIFIÉ")
                            {
                                ConventionClient convention = new ConventionClient();
                                convention.CClient = cclient;
                                convention.CCircuit = ccircuit;
                                convention.CEquipe = CEquipe;
                                convention.RaisonSociale = raisonsocile;
                                convention.DateConvention = DateTime.Now.Date;
                                convention.BPlanificationAuto = true;
                                //convention.Gdate = this.MergeList();
                                convention.DaterepriseFacturation = DateTime.Now.Date;
                                convention.BNFacturation = false;
                                convention.Exercice = DateTime.Now.Year.ToString();
                                convention.CTypeVisite = null;
                                convention.PeriodicitePlanif = 1;
                                convention.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                                convention.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                                convention.PCInsertion = Environment.UserName;
                                convention.PCModification = Environment.UserName;
                                convention.Inserer(transaction);

                                foreach (GeneratedDate gd in ConventionClient.ChargerPlanification_Circuit_D(ccircuit))
                                    convention.InsererDatePlanif(transaction, gd.Dates, gd.Duree);
                            }
                        }
                    }
                    if (!deplaced)
                    {
                        waitForm.Close();
                        transaction.Rollback();
                        XtraMessageBox.Show("Pas de déplacement. ",
                                       Resources.NomApplication,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Information,
                                         MessageBoxDefaultButton.Button1);
                        return;
                    }
                    transaction.Commit();
                    waitForm.Close();
                }
                catch (Exception)
                {
                    waitForm.Close();
                    transaction.Rollback();
                    XtraMessageBox.Show("Échec de l'enregistrement. ",
                                  Resources.NomApplication,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information,
                                    MessageBoxDefaultButton.Button1);
                    return;
                }
                finally
                {
                    waitForm.Dispose();
                }
            }

            if (enregistrerEtFermer)
            {
                this.Close();
            }
            else
            {
                XtraMessageBox.Show("Enregistrement Avec Succès. ",
                                       Resources.NomApplication,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Information,
                                         MessageBoxDefaultButton.Button1);
                this.Actualiser();
            }
        }

        public void Actualiser()
        {
            CCColorD = -1;
            CCColorS = -1;
            LoadData();
        }

        #endregion Action

    }
}