using CST.LePoint.Securite;
using System;
using System.Windows.Forms;

namespace CST.LePoint.Intervention
{
    public partial class FrmLogin1 : DevExpress.XtraEditors.XtraForm
    {
        public FrmLogin1()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (txtLoginName.Text.Trim()  == string.Empty )
            //    {
            //        txtLoginName.Focus();
            //        return;
            //    }

            //    if (txtLoginPwd.Text.Trim() == string.Empty)
            //    {
            //        txtLoginPwd.Focus();
            //        return;
            //    }

            //    GestionSession.UtilisateurCourant = null;

            //    Utilisateur utilisateur = Utilisateur.Charger(txtLoginName.Text);
            //    if (utilisateur != null)
            //    {
            //        if (utilisateur.MotDePasse != txtLoginPwd.Text.Trim())
            //        {
            //            XtraMessageBox.Show("Veuillez vérifier vos paramètres d'accés.",
            //                                  Resources.NomApplication,
            //                                    MessageBoxButtons.OK,
            //                                    MessageBoxIcon.Information,
            //                                    MessageBoxDefaultButton.Button1);

            //            txtLoginPwd.Text = string.Empty;
            //            txtLoginPwd.Focus();
            //            return;
            //        }
            //        else
            //        {
            //            GestionSession.UtilisateurCourant = utilisateur;
            //            this.Close();
            //        }

            //    }
            //    else
            //    {
            //        XtraMessageBox.Show("Veuillez vérifier vos paramètres d'accés.",
            //                                 Resources.NomApplication,
            //                                   MessageBoxButtons.OK,
            //                                   MessageBoxIcon.Information,
            //                                   MessageBoxDefaultButton.Button1);
            //        txtLoginPwd.Text = string.Empty;
            //        txtLoginPwd.Focus();
            //        return;
            //    }

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString(), Resources.NomApplication);
            //}
        }

        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            GestionSession.UtilisateurCourant = null;
            this.Close();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            txtLoginName.Focus();
            //  picturelogin.Image = Properties.Resources.loginacces;
        }

        private void txtLoginPwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnOK_Click(sender, e);
            }
        }

        private void txtLoginName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnOK_Click(sender, e);
            }
        }
    }
}