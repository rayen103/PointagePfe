using CrystalDecisions.CrystalReports.Engine;
using CST.LePoint.CtrlLibrary;
using CST.LePoint.CtrlLibrary.CrystalReport;
using CST.LePoint.CtrlLibrary.Search;
using CST.LePoint.Securite;
using CST.LePoint.Securite.Entites;
using CST.LePoint.Securite.Management;
using CST.LePoint.Tiers.Metier;
using CST.LePoint.Tiers.Referentiel;
using CST.LePoint.Tools;
using CST.LePoint.Vente.Metier;
using CST.LePoint.Intervention.Properties;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using CST.LePoint.Stock.Referentiel.Commun;
using CST.LePoint.Intervention.Metier;

namespace CST.LePoint.Intervention.Tiers
{
    public partial class FrmClientListe : DevExpress.XtraEditors.XtraForm, IActionsListe, IActionsEdition, IActionsListeSuppression, IActionsRechercher
    {
        public int YearContext = DateTime.Now.Year;
        public DateTime DateDebutExercice = DateTime.Parse("01/01/2014");
        public DateTime DateFinExercice = DateTime.Parse("31/12/2014");

        public FrmClientListe()
        {
            InitializeComponent();
        }

        #region Utilitaires

        private static GvColumnProprietes Titres()
        {
            GvColumnProprietes proprietes = new GvColumnProprietes();
            proprietes.Add(new GvColumnPropriete("Code"));
            proprietes.Add(new GvColumnPropriete("Raison Sociale"));
            proprietes.Add(new GvColumnPropriete("Adresse"));
            proprietes.Add(new GvColumnPropriete("Ville"));
            proprietes.Add(new GvColumnPropriete("Matricule"));
            proprietes.Add(new GvColumnPropriete("GPS", GvColumnPropriete.GvColumnType.Boolean));
            proprietes.Add(new GvColumnPropriete("Représentant"));
            return proprietes;
        }

        private void RemplirGridVClientListe()
        {
            string cFamille = null;
            string cPays = null;
            string cVendeur = null;
            string cRegion = null;
            string cGouvernorat = null;
            string cTarif = null;
            string ccircuit = null;
            int Mouvement = 0;
            int num = 0;
            int gps = 0;
            DataTable dtListeClient = new DataTable();
            if (string.IsNullOrEmpty(this.txtDateDebut.Text) || string.IsNullOrEmpty(this.txtDateFin.Text))
                if (YearContext == DateTime.Now.Year)
                {
                    this.txtDateFin.EditValue = DateTime.Now;
                    this.txtDateDebut.EditValue = DateTime.Parse(String.Format("01/{0}/{1}", DateTime.Now.AddMonths(-DateTime.Now.Month + 1).Month, DateTime.Now.AddMonths(-1).Year));
                }
                else
                {
                    this.txtDateFin.EditValue = DateFinExercice;
                    this.txtDateDebut.EditValue = DateDebutExercice;
                }

            try
            {
                CtrlHelper.InitGridView(this.gridV, Titres());
                if (!string.IsNullOrEmpty(this.lkpCFamille.Text))
                    cFamille = this.lkpCFamille.EditValue.ToString();
                if (!string.IsNullOrEmpty(this.lkpCPays.Text))
                    cPays = this.lkpCPays.EditValue.ToString();
                if (!string.IsNullOrEmpty(this.lkpCCommercial.Text))
                    cVendeur = this.lkpCCommercial.EditValue.ToString();
                if (!string.IsNullOrEmpty(this.lkpCRegion.Text))
                    cRegion = this.lkpCRegion.EditValue.ToString();
                if (!string.IsNullOrEmpty(this.lkpCGouvernorat.Text))
                    cGouvernorat = this.lkpCGouvernorat.EditValue.ToString();
                if (!string.IsNullOrEmpty(this.lkpCTarif.Text))
                    cTarif = this.lkpCTarif.EditValue.ToString();
                if (!string.IsNullOrEmpty(this.lookUpCircuit.Text))
                    ccircuit = this.lookUpCircuit.EditValue.ToString();

                if (ChkGPS.CheckState == CheckState.Checked)
                    gps = 1;
                if (CHKNUM.CheckState == CheckState.Checked)
                    num = 1;
                if (ChkGPS.CheckState == CheckState.Unchecked)
                    gps = 2;
                if (CHKNUM.CheckState == CheckState.Unchecked)
                    num = 2;
               

                if (this.radioMouvemente.Checked == true)
                    Mouvement = 1;
                if (this.radioNonMouvemente.Checked == true)
                    Mouvement = 2;
             
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Client_Vue_Rechercher";
                    cmd.Parameters.AddWithValue("@CClient", this.txtCClient.Text);
                    cmd.Parameters.AddWithValue("@CClientFamille", cFamille);
                    cmd.Parameters.AddWithValue("@CPays", cPays);
                    cmd.Parameters.AddWithValue("@CVendeur", cVendeur);
                    cmd.Parameters.AddWithValue("@CRegion", cRegion);
                    cmd.Parameters.AddWithValue("@CTarif", cTarif);
                    cmd.Parameters.AddWithValue("@CGouvernorat", cGouvernorat);
                    cmd.Parameters.AddWithValue("@Mouvement", Mouvement);
                    cmd.Parameters.AddWithValue("@Numero",num);
                    cmd.Parameters.AddWithValue("@DateDebut", DateTime.Parse(txtDateDebut.Text));
                    cmd.Parameters.AddWithValue("@DateFin", DateTime.Parse(txtDateFin.Text));
                    cmd.Parameters.AddWithValue("@CCircuit", ccircuit);
                    cmd.Parameters.AddWithValue("@gps", gps);


                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dtListeClient);
                }
                CtrlHelper.FillGridView(this.gridV, Titres(), dtListeClient);
                gridV.Columns[0].Width = 60;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void LoadData()
        {
            CtrlHelper.FillLookUpEdit(this.lkpCFamille, ClientFamilleCollection.Charger());
            CtrlHelper.FillLookUpEdit(this.lkpCRegion, RegionCollection.Charger());
            CtrlHelper.FillLookUpEdit(this.lkpCPays, PaysCollection.Charger());
            CtrlHelper.FillLookUpEdit(this.lkpCCommercial, CommercialCollection.Charger());
            CtrlHelper.FillLookUpEdit(this.lkpCGouvernorat, GouvernoratCollection.Charger());
            CtrlHelper.FillLookUpEdit(this.lkpCTarif, TarifCollection.Charger());
            CtrlHelper.FillLookUpEdit(this.lookUpCircuit,CircuitCollection.Charger());
            this.ChkGPS.CheckState = CheckState.Indeterminate;
            this.CHKNUM.CheckState = CheckState.Indeterminate;
            if (YearContext == DateTime.Now.Year)
            {
                this.txtDateFin.EditValue = DateTime.Now;
                this.txtDateDebut.EditValue = DateTime.Parse(String.Format("01/{0}/{1}", DateTime.Now.AddMonths(-DateTime.Now.Month + 1).Month, DateTime.Now.AddMonths(-1).Year));
            }
            else
            {
                this.txtDateFin.EditValue = DateFinExercice;
                this.txtDateDebut.EditValue = DateDebutExercice;
            }
            this.radioTousMouvement.Checked = true;
        }

        #endregion Utilitaires

        #region Action

        public void Rechercher()
        {
            RemplirGridVClientListe();
        }

        public void Apercu()
        {
            #region Recupération des données

            string sql = " SELECT C.CClient, C.RaisonSociale,  RG.LibRegion, ISNULL(C.NumeroTelephone1, '') +'  '+ case when C.NumeroTelephone2 is not null then C.NumeroTelephone2 else '' end as NumeroTelephone1,  ";
            sql = SysHelper.RetourChariot(sql) + " C.Fax, C.CTVA, C.NbJourEcheancePaiment, (select TOP 1 A.LibAdresse from Adresse a (NOLOCK) where C.CClient = A.NTiers AND BAdresseFacturation = 1) as LibAdresse,";
            sql = SysHelper.RetourChariot(sql) + " (select TOP 1 A.CPostal from Adresse a (NOLOCK) where C.CClient = A.NTiers AND BAdresseFacturation = 1) as CPostal, (select TOP 1 A.Ville from Adresse a (NOLOCK) where C.CClient = A.NTiers AND BAdresseFacturation = 1) as Ville, P.LibPays, N.LibNatureTiers   ";
            sql = SysHelper.RetourChariot(sql) + " FROM  Client C (NOLOCK)  ";
            //sql = SysHelper.RetourChariot(sql) + " LEFT OUTER JOIN Adresse A (NOLOCK) ON C.CClient = A.NTiers    ";
            sql = SysHelper.RetourChariot(sql) + " LEFT OUTER JOIN Ref_Region RG (NOLOCK) ON C.CRegion = RG.CRegion ";
            sql = SysHelper.RetourChariot(sql) + " LEFT OUTER JOIN Ref_NatureTiers N (NOLOCK) ON C.CNatureTiers = N.CNatureTiers  ";
            sql = SysHelper.RetourChariot(sql) + " LEFT OUTER JOIN ClientFamille F (NOLOCK) ON C.CClientFamille = F.CClientFamille  ";
            sql = SysHelper.RetourChariot(sql) + " LEFT OUTER JOIN Ref_Pays P (NOLOCK) ON C.CPays = P.CPays ";
            sql = SysHelper.RetourChariot(sql) + " INNER JOIN CircuitDetail ci (NOLOCK) ON ci.CClient=C.CClient";
            sql = SysHelper.RetourChariot(sql) + " WHERE 1 = 1 ";
            //sql = SysHelper.RetourChariot(sql) + " AND A.BAdresseFacturation = 1 ";
            if (!string.IsNullOrEmpty(this.txtCClient.Text))
                sql = SysHelper.RetourChariot(sql) + " AND C.CClient = '" + SysHelper.ToSqlString(this.txtCClient.Text) + "'";
            if (!string.IsNullOrEmpty(this.lkpCCommercial.Text))
                sql = SysHelper.RetourChariot(sql) + " AND C.CVendeur =" + SysHelper.ToInt(this.lkpCCommercial.EditValue);
            if (!string.IsNullOrEmpty(this.lkpCFamille.Text))
                sql = SysHelper.RetourChariot(sql) + " AND F.CClientFamille  = '" + SysHelper.ToSqlString(this.lkpCFamille.EditValue) + "'";
            if (!string.IsNullOrEmpty(this.lkpCPays.Text))
                sql = SysHelper.RetourChariot(sql) + " AND P.CPays  = '" + SysHelper.ToSqlString(this.lkpCPays.EditValue) + "'";
            if (!string.IsNullOrEmpty(this.lkpCRegion.Text))
                sql = SysHelper.RetourChariot(sql) + " AND C.CRegion  = '" + SysHelper.ToSqlString(this.lkpCRegion.EditValue) + "'";
            if (!string.IsNullOrEmpty(this.lkpCGouvernorat.Text))
                sql = SysHelper.RetourChariot(sql) + " AND C.CGouvernorat  = '" + SysHelper.ToSqlString(this.lkpCGouvernorat.EditValue) + "'";

            if (!string.IsNullOrEmpty(this.lkpCTarif.Text))
                sql = SysHelper.RetourChariot(sql) + " AND C.CTarif  = '" + SysHelper.ToSqlString(this.lkpCTarif.EditValue) + "'";
            if (ChkGPS.CheckState == CheckState.Checked)
                sql = SysHelper.RetourChariot(sql) + " AND ( C.Latitude != 0 and C.Longitude != 0 ) ";
            if (ChkGPS.CheckState == CheckState.Unchecked)
                sql = SysHelper.RetourChariot(sql) + " AND ( C.Latitude = 0 or C.Longitude = 0 ) ";
            if (CHKNUM.CheckState == CheckState.Checked)
                //  sql = SysHelper.RetourChariot(sql) + " AND  C.NumeroTelephone1 is not null and  C.NumeroTelephone1 != ''  ";
                sql = SysHelper.RetourChariot(sql) + " AND ( NumeroTelephone1 is not null  or NumeroTelephone2 is not null ) AND ( NumeroTelephone1  != ''  or NumeroTelephone2  != ''  )";
            if (CHKNUM.CheckState == CheckState.Unchecked)
                sql = SysHelper.RetourChariot(sql) + " AND ( NumeroTelephone1 is  null  And NumeroTelephone2 is  null ) ";
            if (!string.IsNullOrEmpty(this.lookUpCircuit.Text))
                sql = SysHelper.RetourChariot(sql) + " AND ci.CCircuit  = '" + SysHelper.ToSqlString(this.lookUpCircuit.EditValue) + "'";


            if (this.radioMouvemente.Checked == true)
            {
                sql = SysHelper.RetourChariot(sql) + " AND C.CClient IN (SELECT ISNULL(CClient,'') FROM BonSortie (NOLOCK)  ";
                sql = SysHelper.RetourChariot(sql) + " WHERE DateSortie BETWEEN " + SysHelper.ToSqlDatetime(txtDateDebut.Text) + " AND " + SysHelper.ToSqlDatetime(DateTime.Parse(txtDateFin.Text).AddDays(1).AddDays(-1));
                sql = SysHelper.RetourChariot(sql) + " UNION ALL";
                sql = SysHelper.RetourChariot(sql) + " SELECT ISNULL(CClient,'') FROM BonEntree (NOLOCK)  ";
                sql = SysHelper.RetourChariot(sql) + " WHERE DateEntree BETWEEN " + SysHelper.ToSqlDatetime(txtDateDebut.Text) + " AND " + SysHelper.ToSqlDatetime(DateTime.Parse(txtDateFin.Text).AddDays(1).AddDays(-1)) + ")";     
            }
            if (this.radioNonMouvemente.Checked == true)
            {
                sql = SysHelper.RetourChariot(sql) + " AND C.CClient NOT IN (SELECT ISNULL(CClient,'') FROM BonSortie (NOLOCK)  ";
                sql = SysHelper.RetourChariot(sql) + " WHERE DateSortie BETWEEN " + SysHelper.ToSqlDatetime(txtDateDebut.Text) + " AND " + SysHelper.ToSqlDatetime(DateTime.Parse(txtDateFin.Text).AddDays(1).AddDays(-1));
                sql = SysHelper.RetourChariot(sql) + " UNION ALL";
                sql = SysHelper.RetourChariot(sql) + " SELECT ISNULL(CClient,'') FROM BonEntree (NOLOCK)  ";
                sql = SysHelper.RetourChariot(sql) + " WHERE DateEntree BETWEEN " + SysHelper.ToSqlDatetime(txtDateDebut.Text) + " AND " + SysHelper.ToSqlDatetime(DateTime.Parse(txtDateFin.Text).AddDays(1).AddDays(-1)) + ")";
            }
           
          
          
           
            sql = SysHelper.RetourChariot(sql) + " ORDER BY CClient";

            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);
            }

            if ((ds.Tables.Count > 0) && (ds.Tables[0].Rows.Count == 0))
            {
                DialogResult dialogResult = XtraMessageBox.Show(String.Format("Aucun enregistrement trouvé !"),
                                                  Resources.NomApplication,
                                                  MessageBoxButtons.OK,
                                                  MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            #endregion Recupération des données

            #region Génération du rapport
            
            ReportDocument report = new ReportDocument();
            string reportPath = Application.StartupPath + "\\Reporting\\RptClient.rpt";
            report.Load(reportPath, CrystalDecisions.Shared.OpenReportMethod.OpenReportByDefault);
            CtrlHelperRpt.Initialiser_Entete_Pied_Rapport(report, "LISTE DES CLIENTS");

            report.DataDefinition.FormulaFields["Famille_Fnr"].Text = "'Tous'";
            report.DataDefinition.FormulaFields["Vendeur"].Text = "'Tous'";
            report.DataDefinition.FormulaFields["Pays"].Text = "'Tous'";
            report.DataDefinition.FormulaFields["Region"].Text = "'Tous'";
            report.DataDefinition.FormulaFields["Gouvernorat"].Text = "'Tous'";
          if  (ChkGPS.CheckState == CheckState.Indeterminate)
            report.DataDefinition.FormulaFields["GPS"].Text = "'Tous'";
          if (ChkGPS.CheckState == CheckState.Checked)
              report.DataDefinition.FormulaFields["GPS"].Text = "'Oui'";
          if (ChkGPS.CheckState == CheckState.Unchecked)
              report.DataDefinition.FormulaFields["GPS"].Text = "'Non'";

            if (!string.IsNullOrEmpty(this.lkpCFamille.Text))
                report.DataDefinition.FormulaFields["Famille_Fnr"].Text = String.Format("'{0}'", this.lkpCFamille.Text);
            if (!string.IsNullOrEmpty(this.lkpCCommercial.Text))
                report.DataDefinition.FormulaFields["Vendeur"].Text = String.Format("'{0}'", this.lkpCCommercial.Text);
            if (!string.IsNullOrEmpty(this.lkpCPays.Text))
                report.DataDefinition.FormulaFields["Pays"].Text = String.Format("'{0}'", this.lkpCPays.Text);
   
            if (!string.IsNullOrEmpty(this.lkpCRegion.Text))
                report.DataDefinition.FormulaFields["Region"].Text = String.Format("'{0}'", this.lkpCRegion.Text);
            if (!string.IsNullOrEmpty(this.lkpCGouvernorat.Text))
                report.DataDefinition.FormulaFields["Gouvernorat"].Text = String.Format("'{0}'", this.lkpCGouvernorat.Text);
            if(radioMouvemente.Checked)
                report.DataDefinition.FormulaFields["Mouvement"].Text = String.Format("'MOUVEMENTÉS DU : {0}     AU : {1}'", txtDateDebut.Text, txtDateFin.Text);
            if (radioNonMouvemente.Checked)
                report.DataDefinition.FormulaFields["Mouvement"].Text = String.Format("'NON MOUVEMENTÉS DU : {0}     AU : {1}'", txtDateDebut.Text, txtDateFin.Text);

            report.SetDataSource(ds.Tables[0]);
            FrmCRViewer frm = new FrmCRViewer(Resources.NomApplication + " : " + GestionSession.SocieteCourante.RaisonSociale);
            frm.Report = report;
            frm.Show();

            #endregion Génération du rapport
        }

        public void Actualiser()
        {
            CtrlHelper.EmptyControls(this);
            LoadData();
            RemplirGridVClientListe();
        }

        public void Ajouter()
        {
            Ajouter(this.MdiParent);
        }

        public static void Ajouter(Form parent)
        {
            FrmClient frm = new FrmClient() { Text = Resources.Titre_FrmClient };
            ((FrmMDI)parent).LoadForm(frm);
        }

        public void Modifier()
        {
            if (gridV.FocusedRowHandle == GridControl.InvalidRowHandle)
                return;

            if (this.Tag is CST.LePoint.Intervention.FrmMDI.FlagSecurite &&
            ((CST.LePoint.Intervention.FrmMDI.FlagSecurite)this.Tag).HasFlag(CST.LePoint.Intervention.FrmMDI.FlagSecurite.ModifDisabled))
                return;

            try
            {
                string cClient = this.gridV.GetFocusedRowCellDisplayText(this.gridV.Columns["Code"]);

                FrmClient frm = new FrmClient(cClient) { Text = String.Format(@"{0}: {1}", Resources.Titre_FrmClient, cClient) };
                ((FrmMDI)this.MdiParent).LoadForm(frm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SelectionnerGridRow(bool bHaut)
        {
            if (bHaut)
                this.gridV.FocusedRowHandle = this.gridV.FocusedRowHandle - 1;
            else
                this.gridV.FocusedRowHandle = this.gridV.FocusedRowHandle + 1;
        }

        public void Supprimer()
        {
            string msgEchoue = "Impossible de supprimer ce client! ";
            string msgSucces = "Suppression effectuée avec succès!";

            var dialogResult = XtraMessageBox.Show("Voulez-vous vraiment supprimer ce client?",
                "",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (dialogResult != DialogResult.Yes)
                return;
            try
            {
                Client client = new Client();
                client.CClient = this.gridV.GetFocusedRowCellDisplayText(gridV.Columns[0]);
                client.Supprimer();
                XtraMessageBox.Show(msgSucces, Properties.Resources.NomApplication,
                         MessageBoxButtons.OK,
                         MessageBoxIcon.Information,
                         MessageBoxDefaultButton.Button1);
                this.RemplirGridVClientListe();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(msgEchoue + ex.Message, Properties.Resources.NomApplication,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Information,
                                         MessageBoxDefaultButton.Button1);
            }
        }

        #endregion Action

        #region Evenements

        private void frmClientListe_Load(object sender, EventArgs e)
        {
            LoadData();
            CtrlHelper.InitGridView(this.gridV, Titres());
            RemplirGridVClientListe();
        }

        private void frmClientListe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3 || e.KeyCode == Keys.F4)
            {
                Control item = this.ActiveControl;

                if (item.GetType().Name == "TextBoxMaskBox")
                {
                    TextEdit txtSelection = (TextEdit)item.Parent;

                    if ((txtSelection.Tag != null) && (txtSelection.IsEditorActive) &&
                   (!string.IsNullOrEmpty(txtSelection.Tag.ToString())))
                    {
                        string source = txtSelection.Tag.ToString().Trim().ToUpper();

                        if ((source == "ARTICLE") || (source == "FOURNISSEUR") || (source == "CLIENT"))
                        {
                            bool bRechercheParCode = true;
                            if (e.KeyCode == Keys.F4) bRechercheParCode = false;
                            string selectedvalue = HelperRecherche.FindFieldValue(source, txtSelection.Text, bRechercheParCode);

                            if (!string.IsNullOrEmpty(selectedvalue) && txtSelection.Text != selectedvalue)
                                txtSelection.Text = selectedvalue;
                        }
                    }
                }
            }
        }

        private void txtDateDebut_Leave(object sender, EventArgs e)
        {
            if (txtDateDebut.Text == string.Empty)
                txtDateDebut.EditValue = DateTime.Parse(txtDateFin.Text).AddMonths(-1);

            if (DateTime.Parse(txtDateDebut.Text) > DateTime.Parse(txtDateFin.Text))
                txtDateFin.EditValue = DateTime.Parse(txtDateDebut.Text).AddMonths(1);
        }

        private void txtDateFin_Leave(object sender, EventArgs e)
        {
            if (txtDateFin.Text == string.Empty)
                txtDateFin.EditValue = DateTime.Parse(txtDateDebut.Text).AddMonths(1);

            if (DateTime.Parse(txtDateDebut.Text) > DateTime.Parse(txtDateFin.Text))
                txtDateDebut.EditValue = DateTime.Parse(txtDateFin.Text).AddMonths(-1);
        }

        private void txtCClient_Validating(object sender, CancelEventArgs e)
        {
            txtRaisonSociale.Text = String.Empty;
            if (!string.IsNullOrEmpty(this.txtCClient.Text))
            {
                Client client = new Client();
                try
                {
                    int x = int.Parse(this.txtCClient.Text);
                    client = Client.ChargerVue(this.txtCClient.Text);
                }
                catch
                {
                    client = Client.Charger(this.txtCClient.Text);
                }

                if (client != null)
                {
                    this.txtCClient.Text = client.CClient;
                    txtRaisonSociale.EditValue = client.RaisonSociale;
                    RemplirGridVClientListe();
                }
                else
                {
                    this.txtCClient.Text = string.Empty;
                    txtRaisonSociale.Text = String.Empty;
                    this.txtCClient.Focus();
                    RemplirGridVClientListe();
                }
            }
        }

        private void lkpCFamille_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back || e.KeyData == Keys.Delete)
            {
                this.lkpCFamille.EditValue = string.Empty;
                e.Handled = true;
            }
        }

        private void lkpCRegion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back || e.KeyData == Keys.Delete)
            {
                this.lkpCRegion.EditValue = string.Empty;
                e.Handled = true;
            }
        }

        private void lkpCPays_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back || e.KeyData == Keys.Delete)
            {
                this.lkpCPays.EditValue = null;
                e.Handled = true;
            }
        }

        private void lkpCCommercial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back || e.KeyData == Keys.Delete)
            {
                this.lkpCCommercial.EditValue = null;
                e.Handled = true;
            }
        }

        private void gridClientListe_DoubleClick(object sender, EventArgs e)
        {
            if (GestionUtilisateur.EstAutorise(VenteHelper.NOMAPPLICATION+"Tiers." + this.Name, Actions.Modifier, GestionSession.UtilisateurCourant))
                Modifier();
        }

        private void lkpCGouvernorat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back || e.KeyData == Keys.Delete)
            {
                this.lkpCGouvernorat.EditValue = null;
                e.Handled = true;
            }
        }

        #endregion Evenements

        private void lkpCTarif_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back || e.KeyData == Keys.Delete)
            {
                this.lkpCTarif.EditValue = null;
                e.Handled = true;
            }
        }

        private void lookUpCircuit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back || e.KeyData == Keys.Delete)
            {
                this.lookUpCircuit.EditValue = null;
                e.Handled = true;
            }
        }

        private void txtCClient_EditValueChanged(object sender, EventArgs e)
        {
            if (txtCClient.Text == string.Empty)
            { this.RemplirGridVClientListe(); }

        }
    }
}