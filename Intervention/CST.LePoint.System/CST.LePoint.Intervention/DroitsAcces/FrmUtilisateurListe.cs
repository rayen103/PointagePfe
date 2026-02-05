using CST.LePoint.CtrlLibrary;
using CST.LePoint.CtrlLibrary.DevExpressEx;

//using CST.LePoint.Properties;
using CST.LePoint.Securite.Entites;
using CST.LePoint.Securite.Management;
using CST.LePoint.Intervention.Properties;
using CST.LePoint.Tools;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using CST.LePoint.Securite;

namespace CST.LePoint.Intervention.DroitsAcces
{
    public partial class FrmUtilisateurListe : XtraForm, IActionsListe, IActionsEdition, IActionsExport
    {
        public FrmUtilisateurListe()
        {
            InitializeComponent();
        }

        public void Imprimer()
        {
            DXReport.Imprimer(grid, Text);
        }

        public void Apercu()
        {
            DXReport.Apercu(grid, Text);
        }

        public void Exporter(string formatCible)
        {
            DXReport.Exporter(grid, formatCible, SysHelper.FileNameValide(Text), Text);
        }

        public void Modifier()
        {
            if (gridV.FocusedRowHandle == GridControl.InvalidRowHandle)
                return;

            string cUtilisateur = gridV.GetDataRow(gridV.FocusedRowHandle)["Login"].ToString();
            FrmUtilisateur frm = new FrmUtilisateur(cUtilisateur);

            frm.Text = Resources.Titre_frmUtilisateur + @": " + cUtilisateur;
            ((FrmMDI)MdiParent).LoadForm(frm);
        }

        public void Supprimer()
        {
            if (gridV.FocusedRowHandle == GridControl.InvalidRowHandle)
                return;

            DialogResult dialogResult = XtraMessageBox.Show("Êtes-vous sûr de vouloir supprimer cet enregistrement ? Cette suppression sera définitive.",
                                                          Resources.NomApplication,
                                                          MessageBoxButtons.YesNoCancel,
                                                          MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (dialogResult != System.Windows.Forms.DialogResult.Yes)
                return;

            string cUtilisateur = gridV.GetDataRow(gridV.FocusedRowHandle)["Login"].ToString();
            Utilisateur u = Utilisateur.Charger(cUtilisateur);
            u.Supprimer();

            GestionContexteSecurite.ContexteActive.Set<Utilisateur>().Remove(u);
            GestionContexteSecurite.ContexteActive.Enregistrer();

            gridV.DeleteRow(gridV.FocusedRowHandle);
        }

        private void RemplirGrid()
        {
            var dt = new DataTable();

            try
            {
                CtrlHelper.InitGridView(gridV, Titres(), true);

                using (
                    var cn =
                        new SqlConnection(
                            ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    var cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Utilisateur_Vue_Rechercher";
                    if (ConfigurationManager.AppSettings["MODE_APPLICATION"].ToString() == "ADMINISTRATION")
                    {
                        cmd.Parameters.AddWithValue("@CSociete", null);
                        cmd.Parameters.AddWithValue("@CSite", null);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@CSociete", GestionSession.SocieteCourante.CSociete);
                        cmd.Parameters.AddWithValue("@CSite", GestionSession.SocieteSite);
                    }

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    var adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }

                CtrlHelper.FillGridView(gridV, Titres(), dt);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Actualiser()
        {
            RemplirGrid();
        }

        public void Ajouter()
        {
            var frm = new FrmUtilisateur();
            frm.Text = Resources.Titre_frmUtilisateur;
            ((FrmMDI)MdiParent).LoadForm(frm);
        }

        public void SelectionnerGridRow(bool bHaut)
        {
            if (bHaut)
                gridV.FocusedRowHandle = gridV.FocusedRowHandle - 1;
            else
                gridV.FocusedRowHandle = gridV.FocusedRowHandle + 1;
        }

        private void InitLabels()
        {
            gridColUserId.Caption = Resources.lblIdentifiant;
            gridColUserName.Caption = "Login";
        }

        private void frmUtilisateurListe_Load(object sender, EventArgs e)
        {
            this.Text = "Gestion des Utilisateurs";
            InitLabels();
        }

        private static GvColumnProprietes Titres()
        {
            var proprietes = new GvColumnProprietes();
            proprietes.Add(new GvColumnPropriete("Login"));
            proprietes.Add(new GvColumnPropriete("Nom"));
            proprietes.Add(new GvColumnPropriete("Prénom"));
            proprietes.Add(new GvColumnPropriete("Société"));

            return proprietes;
        }

        private void frmUtilisateurListe_Activated(object sender, EventArgs e)
        {
            Actualiser();
        }

        private void grid_DoubleClick(object sender, EventArgs e)
        {
            if (Tag is FrmMDI.FlagSecurite &&
                ((FrmMDI.FlagSecurite)Tag).HasFlag(FrmMDI.FlagSecurite.ModifDisabled))
                return;
            Modifier();
        }

        private void grid_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var frm = (FrmMDI)MdiParent;
                ContextMenuStrip contextMenuStrip = frm.contextMenu;
                contextMenuStrip.Show(grid, e.X, e.Y);
            }
        }
    }
}