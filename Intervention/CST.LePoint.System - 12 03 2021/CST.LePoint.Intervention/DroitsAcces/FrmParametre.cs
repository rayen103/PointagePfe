using CST.LePoint.CtrlLibrary;
using CST.LePoint.Intervention.Properties;
using CST.LePoint.Securite;
using CST.LePoint.Securite.Entites;
using CST.LePoint.Vente.Metier;
using DevExpress.XtraEditors;
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

namespace CST.LePoint.Intervention.DroitsAcces
{
    public partial class FrmParametre :  DevExpress.XtraEditors.XtraForm, IActionsSave
    {
        string code = string.Empty;

        public FrmParametre()
        {         
            InitializeComponent();         
        }

        public GvColumnProprietes Titres()
        {
            GvColumnProprietes proprietes = new GvColumnProprietes();
            proprietes.Add(new GvColumnPropriete("CParametre", GvColumnPropriete.GvColumnEtat.Invisible));
            proprietes.Add(new GvColumnPropriete("Parametre"));
            proprietes.Add(new GvColumnPropriete("Description"));
            proprietes.Add(new GvColumnPropriete("Valeur"));

            return proprietes;
        }

        private void RemplirGridV()
        {
            DataTable dtListe = new DataTable();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "GC_VueRechercherParam";
                    cmd.Parameters.AddWithValue("@CApplication", "6");

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dtListe);

                }

                CtrlHelper.FillGridView(gridView1, Titres(), dtListe);

            }
            catch (Exception)
            {
                throw;
            }
        }

        private void FrmParametre_Load(object sender, EventArgs e)
        {
            CtrlHelper.InitGridView(gridView1, Titres());
            RemplirGridV();
        }

        private string Indication(string indication)
        {
            //mettre apres chaque /n dans le texte ecrit dans la table un retour à la ligne
            String[] tab1 = indication.Split(new string[] { "\\n" }, StringSplitOptions.None);
            StringBuilder sb1 = new StringBuilder();
            foreach (string str in tab1)
                sb1.AppendLine(str);
            return sb1.ToString();
        }

        public void AfficheDescription(string typeParametre, string valeur, string codeP, string P, string indication, string description)
        {
            ////string description = string.Empty;
            ////string Indication = string.Empty;
            ////int valeur = -1;

            //switch (typeParametre)
            //{
            //    case "Intervalle choisi":
                    txtdescription.Text = description;
                    txtindication.Text = Indication(indication);
                    txtnomprm.Text = P;
                    code = "1";
                    txtval.Text = valeur;
            //        break;
            //}

        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            txtcode.Text = this.gridView1.GetFocusedRowCellDisplayText("CParametre");
            Parametres parametre = Parametres.Charger(txtcode.Text);
            AfficheDescription(parametre.TypeParametre, parametre.Valeur, parametre.CParametre, parametre.Parametre, parametre.Indication, parametre.Description);      
        }

        public void Enregistrer(bool enregistrerEtFermer)
        {
            // ParametreCollection parametrecoll = ParametreCollection.Charger();

            try
            {
                if (verif())
                {
                    Parametres Para = Parametres.Charger(txtcode.Text);
                    if (Para != null)
                    {
                        Para.Valeur = txtval.Text;
                        Para.DateModification = DateTime.Now;
                        Para.ModifierPar = GestionSession.UtilisateurCourant.IdUtilisateur;
                        Para.PCModification = Environment.UserName;
                        Para.Modifier();

                        if (enregistrerEtFermer)
                            this.Close();
                        else
                        {
                            XtraMessageBox.Show("Modification Avec Succes.",
                                                   Resources.NomApplication,
                                                     MessageBoxButtons.OK,
                                                     MessageBoxIcon.Information,
                                                     MessageBoxDefaultButton.Button1);
                            RemplirGridV();
                            //  VenteHelper VENTEHEL = new VenteHelper(Para);
                        }
                    }
                    else
                    {
                        XtraMessageBox.Show("Echec de l'enregistrement!!.",
                                              Resources.NomApplication,
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Information,
                                                MessageBoxDefaultButton.Button1);
                    }
                }
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Echec de l'enregistrement.",
                                          Resources.NomApplication,
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information,
                                            MessageBoxDefaultButton.Button1);
            }
        }

        public void Actualiser()
        {
            code = string.Empty;
            txtval.Text = string.Empty;
            RemplirGridV();
        }
        
        public bool verif()
        {
            if (!string.IsNullOrEmpty(txtval.Text))
                if (code == "6")
                {
                   return true;
                    } return false;
             
        }
    }
}
