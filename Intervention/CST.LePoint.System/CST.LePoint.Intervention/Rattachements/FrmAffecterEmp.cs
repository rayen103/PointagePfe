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
using CST.LePoint.Intervention.Properties;
using System.Data.SqlClient;
using System.Configuration;
using CST.LePoint.Tiers.Referentiel;
using CST.LePoint.Tiers.Metier;
using CST.LePoint.Intervention.Metier;

namespace CST.LePoint.Intervention.Rattachements
{
    public partial class FrmAffecterEmp : DevExpress.XtraEditors.XtraForm
    {
        //public int YearContext = DateTime.Now.Year;
        //public string NRegRecuperer;
        //private string CodeClient = string.Empty;
        //private string CodeclientReg = string.Empty;
        ////private string CodefournisseurBL = string.Empty;
        public string TitreColGrid = string.Empty;
        public string TitreGroupeControle = string.Empty;
        //private FrmChantier frmChantier = new FrmChantier();
        private FrmEquipeListe frmEquipeListe = new FrmEquipeListe();
        public bool bPopUp = true;
        //public DateTime DateDebutExercice = DateTime.Parse("01/01/2016");
        //public DateTime DateFinExercice = DateTime.Parse("31/12/2014");
        //public bool BFrmBonChargement = false;
        private string CEquipe = string.Empty;
        public bool BFrmAffecterEmp = false;

        private string RespRecuperer = string.Empty;
       // private FrmEquipeListe frmEquipeListe;
        //public bool BFrmBC_OT = false;
        //public string nReg;
       
        
        public FrmAffecterEmp()
        {
            InitializeComponent();
        }
       

     #region Utilitaire

       

        public GvColumnProprietes Titres()
        {
        
            GvColumnProprietes proprietes = new GvColumnProprietes();
            proprietes.Add(new GvColumnPropriete("[X]", GvColumnPropriete.GvColumnEtat.Enable));
            proprietes.Add(new GvColumnPropriete("Matricule"));
            proprietes.Add(new GvColumnPropriete("Nom"));
            proprietes.Add(new GvColumnPropriete("[Responsable]", GvColumnPropriete.GvColumnEtat.Enable));

          
      
            return proprietes;
        }

        private void RemplirGridV()
        {
           
            DataTable dtListeBC = new DataTable();

            try
            {
                CtrlHelper.InitGridView(gridV, Titres());

                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "GP_Rattachement_Vue_RechercherEmp";
                    cmd.Parameters.AddWithValue("@Matricule", DBNull.Value);
                    //cmd.Parameters.AddWithValue("@CclientBL", this.CodeclientReg);
                   
                   
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dtListeBC);
                }

                CtrlHelper.FillGridView(gridV, Titres(), dtListeBC);
                this.gridV.OptionsSelection.EnableAppearanceFocusedRow = false;
            }
            catch (Exception)
            {
                throw;
            }
        }



        public FrmAffecterEmp(string equipe, FrmEquipeListe frmEquipeListe)
        {
            // TODO: Complete member initialization
           this.frmEquipeListe = frmEquipeListe;
            InitializeComponent();
            this.TitreColGrid = "Matricule";
            this.TitreGroupeControle = "Liste Employés";
            this.CEquipe = equipe;
        }

      

      public void LoadData()
      {
         
      }
      public void Charger(string cFournisseur)
      {
      }
      private void ViderClient()
      {
         

          //this.txtDateDebut.Text = string.Empty;
          //this.txtDateFin.Text = string.Empty;
          // this.txtTel.Text = string.Empty;
      }

        #endregion

      private void FrmRechercherReg_Load(object sender, EventArgs e)
      {
          //LoadData();
          RemplirGridV();
      }

      private void btnActualiser_Click(object sender, EventArgs e)
      {
          ViderClient();
          RemplirGridV();
      }

      private void FrmRechercherReg_FormClosed(object sender, FormClosedEventArgs e)
      {
          if (this.BFrmAffecterEmp && this.frmEquipeListe.Visible)
              this.frmEquipeListe.bPopUp = true;
          
      }

      private void FrmRechercherReg_KeyDown(object sender, KeyEventArgs e)
      {
          if (e.KeyCode == Keys.Escape)
              this.Close();
      }

      private void gridV_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
      {
          ////this.txtCClient.EditValue = this.gridV.GetFocusedRowCellValue("Code Client");
          //this.txtRaisonSociale.EditValue = this.gridV.GetFocusedRowCellValue("Raison_Sociale");
         
      }

      private void gridC_DoubleClick(object sender, EventArgs e)
      {
              //this.NRegRecuperer = this.gridV.GetFocusedRowCellDisplayText(this.TitreColGrid);
              ////this.frmBC_OT.ChargerEntiteReglement(NRegRecuperer);
              //this.frmBC_OT.bPopUp = true;
        

          //this.Close();
      }

      private void btnActualiser_Click_1(object sender, EventArgs e)
      {
          RemplirGridV();
      }

      private void simpleButton1_Click(object sender, EventArgs e)
      {

          //for (int i = 0; i < gridV.RowCount; i++)
          //{
          //    if (this.gridV.GetRowCellValue(i, "[X]").ToString() == "True")
          //    {
          //       string matricule= this.gridV.GetRowCellDisplayText(i, "Matricule");
          //       string equipe = this.CEquipe;
                 
                  
          //       Employe emp= Employe.Charger(matricule);

          //       if (!string.IsNullOrWhiteSpace(emp.CEquipe))
          //       {
          //           DialogResult dialogResult = XtraMessageBox.Show("Cet Employé appartient déjà à une Equipe ! Voulez-vous vraiment changer cet Equipe ?",
          //                                      Resources.NomApplication,
          //                                      MessageBoxButtons.YesNoCancel,
          //                                      MessageBoxIcon.Question,
          //                                      MessageBoxDefaultButton.Button1);
          //           if (dialogResult != DialogResult.Yes)
          //               return;
          //       }
          //     // Employe.ModifierCodeEquipe(equipe, matricule);
                 
          //    }
          //}

          //for (int i = 0; i < gridV.RowCount; i++)
          //{
          //    if (this.gridV.GetRowCellValue(i, "[Responsable]").ToString() == "True")
          //    {
          //        string matricule = this.gridV.GetRowCellDisplayText(i, "Matricule");


          //       // Employe.ModifierBitResponsable("True", matricule);
          //        this.RespRecuperer = this.gridV.GetRowCellDisplayText(i,this.TitreColGrid);
          //        this.frmEquipeListe.Charger(RespRecuperer);

          //    }
          //}


         // this.Close();
      }

     
    
    
    
    }
}