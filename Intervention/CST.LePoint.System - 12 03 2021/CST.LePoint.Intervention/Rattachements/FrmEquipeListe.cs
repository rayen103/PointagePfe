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

namespace CST.LePoint.Intervention.Rattachements
{
    public partial class FrmEquipeListe : DevExpress.XtraEditors.XtraForm, IActionsSave
    {
        public bool bPopUp = true;

        public FrmEquipeListe()
        {
            InitializeComponent();
        }

        #region Utilitaire

        private void ViderEquipe()
        {
            this.txtCEquipe.Text = string.Empty;
            //this.txtCClient.Text = string.Empty;
            this.lkpEntrepot.EditValue = string.Empty;
            //this.txtCFournisseur.Text = string.Empty;
            this.lkpTarif.EditValue = string.Empty;
            this.lkpVehicule.EditValue = string.Empty;
            //this.txtDesignationClient.Text = string.Empty;
            //this.txtDesignationFournisseur.Text = string.Empty;
            this.txtLibelle.Text = string.Empty;
            this.lkpResponsable.EditValue = string.Empty;
        }

        private void RemplirGridVListe()
        {

            DataTable dtListeBL = new DataTable();

            try
            {


                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "GP_Rattachement_Vue_RechercherEquipe";
                    cmd.Parameters.AddWithValue("@CEquipe", txtCEquipe.Text);
                    //cmd.Parameters.AddWithValue("@MontantTTC", decimal.Parse(txtTTCBL.EditValue.ToString()));
                    //cmd.Parameters.AddWithValue("@NBonLivraison", txtNBonLivraison.Text);
                    //cmd.Parameters.AddWithValue("@DateLivraisonDebut", DateTime.Parse(txtDateDebut.Text));
                    //cmd.Parameters.AddWithValue("@DateLivraisonFin", DateTime.Parse(txtDateFin.Text).AddDays(1).AddSeconds(-1));
                    //cmd.Parameters.AddWithValue("@NonFacture", this.chkNonFacture.Checked);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dtListeBL);
                }

                CtrlHelper.FillGridView(gridVEquipe, TitresEquipe(), dtListeBL);
                //this.gridVEntete.Columns[4].Width = 400;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public GvColumnProprietes TitresEquipe()
        {
            GvColumnProprietes proprietes = new GvColumnProprietes();


            proprietes.Add(new GvColumnPropriete("Code"));
            proprietes.Add(new GvColumnPropriete("Libellé"));
           // proprietes.Add(new GvColumnPropriete("Client", GvColumnPropriete.GvColumnEtat.Invisible));
           // proprietes.Add(new GvColumnPropriete("Fournisseur", GvColumnPropriete.GvColumnEtat.Invisible));
            proprietes.Add(new GvColumnPropriete("Entrepôt", GvColumnPropriete.GvColumnEtat.Invisible));
            proprietes.Add(new GvColumnPropriete("Tarif", GvColumnPropriete.GvColumnEtat.Invisible));
            proprietes.Add(new GvColumnPropriete("Responsable"));

            proprietes.Add(new GvColumnPropriete("Véhicule"));




            return proprietes;
        }

        #endregion 

        #region evenement

        private void FrmEquipeListe_Load(object sender, EventArgs e)

        {
            CtrlHelper.InitGridView(gridVEquipe, TitresEquipe());
            //CtrlHelper.FillLookUpEdit(this.txtEntrepot, EntrepotCollection.Charger());
            CtrlHelper.FillLookUpEdit(this.lkpEntrepot, EntrepotCollection.Charger());
            CtrlHelper.FillLookUpEdit(this.lkpResponsable, EmployeCollection.ChargerResp());
            CtrlHelper.FillLookUpEdit(this.lkpVehicule, VehiculeCollection.Charger());
           // this.txtDesignationClient.Enabled = false;
            //this.txtDesignationFournisseur.Enabled = false;
           // this.labelControl4.Visible = false;
           // this.lkpResponsable.Visible = false;
           RemplirGridVListe();
        }

        private void txtClient_Validated(object sender, EventArgs e)
        {
           //// this.txtDesignationClient.Text = string.Empty;
           // //CtrlHelper.FillLookUpEdit(this.lkpchantier, ChantierCollection.Charger(""));
           // if (!string.IsNullOrWhiteSpace(this.txtCClient.Text))
           // {
           //     Client _Client = new Client();
           //     try
           //     {
           //         int x = int.Parse(this.txtCClient.Text);
           //         _Client = Client.ChargerVue(this.txtCClient.Text);
           //     }
           //     catch
           //     {
           //         _Client = Client.Charger(this.txtCClient.Text);
           //     }

           //     if (_Client != null)
           //     {

           //         this.txtCClient.EditValue = _Client.CClient;
           //        // txtDesignationClient.Text = _Client.RaisonSociale;
           //         //CtrlHelper.FillLookUpEdit(this.lkpchantier, ChantierCollection.Charger(_Client.CClient));

           //     }
           //     else
           //     {
           //         XtraMessageBox.Show(" Code Client invalide ",
           //                                     Resources.NomApplication,
           //                                     MessageBoxButtons.OK,
           //                                     MessageBoxIcon.Information,
           //                                     MessageBoxDefaultButton.Button1);

           //         this.txtCClient.EditValue = string.Empty;

           //         return;
           //     }
           // }
        }

        private void FrmEquipeListe_KeyDown(object sender, KeyEventArgs e)
        {
            Control item = this.ActiveControl;
            if (e.KeyCode == Keys.F3 || e.KeyCode == Keys.F4)
            {
                if (item.GetType().Name == "TextBoxMaskBox")
                {
                    TextEdit txtSelection = (TextEdit)item.Parent;
                    if ((txtSelection.Tag != null) && (txtSelection.IsEditorActive))
                    {
                        if (!string.IsNullOrEmpty(txtSelection.Tag.ToString()))
                        {
                            string source = txtSelection.Tag.ToString().Trim().ToUpper();
                            if (source.Contains("ARTICLE") || source.Contains("FOURNISSEUR") || source.Contains("CLIENT"))
                            {
                                bool bRechercheParCode = true;
                                if (e.KeyCode == Keys.F4) bRechercheParCode = false;
                                string selectedvalue = HelperRecherche.FindFieldValue(source, txtSelection.Text, bRechercheParCode);
                                //,this.lkpEnpMP.EditValue.ToString()

                                if (!string.IsNullOrEmpty(selectedvalue) && txtSelection.Text != selectedvalue)
                                    txtSelection.Text = selectedvalue;
                            }
                        }
                    }
                }
            }
        }










        #endregion

        #region Action
        public void Enregistrer(bool enregistrerEtFermer)
        {

            Equipe equipe = new Equipe();

            //if ((string.IsNullOrWhiteSpace(this.txtCClient.Text))&&(this.checkEdit1.Checked==false))
            //{
            //    XtraMessageBox.Show("Veuillez vérifier le code client",
            //                        Resources.NomApplication,
            //                        MessageBoxButtons.OK,
            //                        MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

            //    return;
           // }

            if (string.IsNullOrWhiteSpace(this.txtCEquipe.Text))
            {
                XtraMessageBox.Show("Veuillez entrer le code equipe",
                                    Resources.NomApplication,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                return;
            }

            equipe.CEquipe = this.txtCEquipe.Text;
            equipe.Libelle = this.txtLibelle.Text;

            //if (!string.IsNullOrEmpty(this.txtCClient.Text))
            //equipe.CClient = this.txtCClient.Text;
            if (!string.IsNullOrEmpty(this.lkpResponsable.Text))
                equipe.Responsable = this.lkpResponsable.EditValue.ToString();
            if (!string.IsNullOrEmpty(this.lkpVehicule.Text))
            equipe.CVehicule = this.lkpVehicule.EditValue.ToString();
            //if (!string.IsNullOrEmpty(this.txtCFournisseur.Text))
            //equipe.CFournisseur = this.txtCFournisseur.Text;
            //equipe.CTarif = this.lkpTarif.EditValue.ToString();
            if (!string.IsNullOrEmpty(this.lkpEntrepot.Text))
            equipe.CEntrepot = this.lkpEntrepot.EditValue.ToString();;
            //if (this.checkEdit1.Checked)
            //{


            //    equipe.BInterne = true;



            //}

            equipe.Sauvegarder();
            ViderEquipe();
            RemplirGridVListe();
            XtraMessageBox.Show("Enregistrée avec Succés",
                                     Resources.NomApplication,
                                     MessageBoxButtons.OK,
                                     MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            
        }

        public void Actualiser()
        {


            ViderEquipe();
            RemplirGridVListe();
        }

        #endregion

        private void txtCFournisseur_Validated(object sender, EventArgs e)
        {
            ////this.txtDesignationFournisseur.Text = string.Empty;
            ////CtrlHelper.FillLookUpEdit(this.lkpchantier, ChantierCollection.Charger(""));
            //if (!string.IsNullOrWhiteSpace(this.txtCFournisseur.Text))
            //{
            //    Fournisseur _Fournisseur = new Fournisseur();
            //    try
            //    {
            //        int x = int.Parse(this.txtCFournisseur.Text);
            //        _Fournisseur = Fournisseur.ChargerVue(this.txtCFournisseur.Text);
            //    }
            //    catch
            //    {
            //        _Fournisseur = Fournisseur.Charger(this.txtCFournisseur.Text);
            //    }

            //    if (_Fournisseur != null)
            //    {

            //        this.txtCFournisseur.EditValue = _Fournisseur.CFournisseur;
            //        //txtDesignationFournisseur.Text = _Fournisseur.RaisonSociale;
            //        //CtrlHelper.FillLookUpEdit(this.lkpchantier, ChantierCollection.Charger(_Client.CClient));

            //    }
            //    else
            //    {
            //        XtraMessageBox.Show(" Code Fournisseur invalide ",
            //                                    Resources.NomApplication,
            //                                    MessageBoxButtons.OK,
            //                                    MessageBoxIcon.Information,
            //                                    MessageBoxDefaultButton.Button1);

            //        this.txtCFournisseur.EditValue = string.Empty;

            //        return;
            //    }
            //}




        }

        public void Supprimer()
        {
            if (this.gridVEquipe.RowCount == 0)
                return;

            string cequipe = this.gridVEquipe.GetFocusedRowCellDisplayText("Code");

            DialogResult dialogResult = XtraMessageBox.Show("Voulez-vous Supprimer cet équipe ?",
                                                Resources.NomApplication,
                                                MessageBoxButtons.YesNoCancel,
                                                MessageBoxIcon.Question,
                                                MessageBoxDefaultButton.Button1);
            if (dialogResult != DialogResult.Yes)
                return;

            Equipe equipe = Equipe.Charger(cequipe);
            equipe.Supprimer();

            XtraMessageBox.Show("Suppression avec Succès",
                                    Resources.NomApplication,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information,
                                    MessageBoxDefaultButton.Button1);
            Actualiser();
        }

        private void gridVEquipe_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (this.gridVEquipe.RowCount == 0)
                return;
            
            Equipe equipe;
            String cEquipe = this.gridVEquipe.GetFocusedRowCellValue("Code").ToString();
            equipe = Equipe.Charger(cEquipe);
            
          //  this.txtCClient.EditValue = equipe.CClient;
            this.txtCEquipe.EditValue = equipe.CEquipe;
            

          //  this.txtCFournisseur.EditValue = equipe.CFournisseur;
            this.txtLibelle.EditValue = this.gridVEquipe.GetFocusedRowCellValue("Libellé").ToString();
            
            //if (equipe.BInterne.ToString().Equals("True"))

            //{
            //this.checkEdit1.Checked = true;
            //this.lkpResponsable.EditValue = this.gridVEquipe.GetFocusedRowCellValue("Responsable").ToString();
            //this.lkpEntrepot.EditValue = this.gridVEquipe.GetFocusedRowCellValue("Entrepôt");
            //}
            //else
            //    this.checkEdit1.Checked = false;    

            this.lkpEntrepot.EditValue = equipe.CEntrepot;
            //Employe emp = Employe.Charger(equipe.Responsable);
            this.lkpResponsable.EditValue = equipe.Responsable;
            this.lkpVehicule.EditValue = equipe.CVehicule;


            
        }

        private void lkpEntrepot_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Delete) || (e.KeyCode == Keys.Back))
            {
                if ((e.KeyCode == Keys.Delete) || (e.KeyCode == Keys.Back))
                {
                    this.lkpEntrepot.Text = string.Empty;
                }
            }
        }

        private void lkpTarif_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Delete) || (e.KeyCode == Keys.Back))
            {
                if ((e.KeyCode == Keys.Delete) || (e.KeyCode == Keys.Back))
                {
                    this.lkpTarif.EditValue = string.Empty;
                }
            }
        }

        private void txtCClient_TextChanged(object sender, EventArgs e)
        {
            ////this.txtDesignationClient.Text = string.Empty;
            ////CtrlHelper.FillLookUpEdit(this.lkpchantier, ChantierCollection.Charger(""));
            //if (!string.IsNullOrWhiteSpace(this.txtCClient.Text))
            //{
            //    Client _Client = new Client();
            //    try
            //    {
            //        int x = int.Parse(this.txtCClient.Text);
            //        _Client = Client.ChargerVue(this.txtCClient.Text);
            //    }
            //    catch
            //    {
            //        _Client = Client.Charger(this.txtCClient.Text);
            //    }

            //    if (_Client != null)
            //    {

            //        this.txtCClient.EditValue = _Client.CClient;
            //        //txtDesignationClient.Text = _Client.RaisonSociale;
            //        //CtrlHelper.FillLookUpEdit(this.lkpchantier, ChantierCollection.Charger(_Client.CClient));

            //    }
            //    else
            //    {
            //        XtraMessageBox.Show(" Code Client invalide ",
            //                                    Resources.NomApplication,
            //                                    MessageBoxButtons.OK,
            //                                    MessageBoxIcon.Information,
            //                                    MessageBoxDefaultButton.Button1);

            //        this.txtCClient.EditValue = string.Empty;

            //        return;
            //    }
            //}
        }

        private void txtCFournisseur_TextChanged(object sender, EventArgs e)
        {
         ////   this.txtDesignationFournisseur.Text = string.Empty;
         //   //CtrlHelper.FillLookUpEdit(this.lkpchantier, ChantierCollection.Charger(""));
         //   if (!string.IsNullOrWhiteSpace(this.txtCFournisseur.Text))
         //   {
         //       Fournisseur _Fournisseur = new Fournisseur();
         //       try
         //       {
         //           int x = int.Parse(this.txtCFournisseur.Text);
         //           _Fournisseur = Fournisseur.ChargerVue(this.txtCFournisseur.Text);
         //       }
         //       catch
         //       {
         //           _Fournisseur = Fournisseur.Charger(this.txtCFournisseur.Text);
         //       }

         //       if (_Fournisseur != null)
         //       {

         //           this.txtCFournisseur.EditValue = _Fournisseur.CFournisseur;
         //        //   txtDesignationFournisseur.Text = _Fournisseur.RaisonSociale;
         //           //CtrlHelper.FillLookUpEdit(this.lkpchantier, ChantierCollection.Charger(_Client.CClient));

         //       }
         //       else
         //       {
         //           XtraMessageBox.Show(" Code Fournisseur invalide ",
         //                                       Resources.NomApplication,
         //                                       MessageBoxButtons.OK,
         //                                       MessageBoxIcon.Information,
         //                                       MessageBoxDefaultButton.Button1);

         //           this.txtCFournisseur.EditValue = string.Empty;

         //           return;
         //       }
         //   }

        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {

            //if (this.checkEdit1.Checked)
            //{
            //    CtrlHelper.FillLookUpEdit(this.lkpResponsable, EmployeCollection.Charger());
            //    this.labelControl3.Visible = true;
            //    this.txtEntrepot.Visible = true;
                    
            //    this.labelControl4.Visible = true;
            //    this.lkpResponsable.Visible = true;
            //    this.txtCClient.Visible = false;
            //    this.txtCFournisseur.Visible = false;
            //    this.labelControl5.Visible = false;
            //    this.labelControl6.Visible = false;

            //}
            //else
            //{
            //    this.labelControl3.Visible = false;
            //    this.txtEntrepot.Visible = false;
            //    this.labelControl4.Visible = false;
            //    this.lkpResponsable.Visible = false;
            //    this.txtEntrepot.Text = string.Empty;
            //    this.txtCClient.Visible = true;
            //    this.txtCFournisseur.Visible = true;
            //    this.labelControl5.Visible = true;
            //    this.labelControl6.Visible = true;
            //    this.lkpResponsable.EditValue = string.Empty;
            //}
        }

        private void lkpEntrepot_KeyDown_1(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Delete) || (e.KeyCode == Keys.Back))
            {
                if ((e.KeyCode == Keys.Delete) || (e.KeyCode == Keys.Back))
                {
                    this.lkpEntrepot.EditValue = string.Empty;


                }
            }
        }

        private void lkpVehicule_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Delete) || (e.KeyCode == Keys.Back))
            {
                if ((e.KeyCode == Keys.Delete) || (e.KeyCode == Keys.Back))
                {
                    this.lkpVehicule.EditValue = string.Empty;


                }
            }
        }

        private void lkpResponsable_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Delete) || (e.KeyCode == Keys.Back))
            {
                if ((e.KeyCode == Keys.Delete) || (e.KeyCode == Keys.Back))
                {
                    this.lkpResponsable.EditValue = string.Empty;


                }
            }
        }

        private void bntRechercherBC_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(this.txtCEquipe.Text))
            {
                XtraMessageBox.Show("Veuillez entrer le Code Equipe",
                                    Resources.NomApplication,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                return;
            }
            
            if (!bPopUp)
                return;
            FrmAffecterEmp frm = new FrmAffecterEmp(this.txtCEquipe.Text,this);
            frm.BFrmAffecterEmp = true;
           // frm.Charger(this.txtCClient.Text);
            frm.Show();
            this.bPopUp = false;
        }

        public void Charger(string responsable)
        {

            this.lkpResponsable.EditValue = responsable;
            
           
            
        }
    }

}