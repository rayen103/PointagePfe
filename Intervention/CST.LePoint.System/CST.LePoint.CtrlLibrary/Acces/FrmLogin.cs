using DevExpress.XtraEditors;

using CST.LePoint.Securite.Entites;
using CST.LePoint.Securite.Management;
using CST.LePoint.Tools;

using System;
using System.Linq;
using System.Windows.Forms;
using CST.LePoint.Securite.DataAccess;

namespace CST.LePoint.CtrlLibrary.Acces
{
    public partial class FrmLogin : XtraForm
    {
        public bool _BModifierFocused = false;
        public FrmLogin()
        {
            InitializeComponent();
        }

        public Utilisateur Utilisateur { get; set; }
        public string Site { get; set; }
        private void frmLogin_Load(object sender, EventArgs e)
        {
            //IContexteSecurite cs = GestionContexteSecurite.ContexteActive;
            //ddlSocietes.Properties.DataSource = cs.Set<Societe>().ToList();

            this.txtNomUtilisateur.Text = SystemInformation.UserName.ToUpper();
            // this.txtMotDePasse.Text = "12";
            loadSociete();

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            var selecteditem = "";
            if (this.lkpSociete.EditValue != null)
                selecteditem = this.lkpSociete.EditValue.ToString();
            IContexteSecurite cs = GestionContexteSecurite.ContexteActive;

            Utilisateur utilisateur = cs.Set<Utilisateur>().FirstOrDefault(u => (u.Login == null ? null : u.Login.ToUpper()) == txtNomUtilisateur.Text.ToUpper() &&
                                                                                (u.MotDePasseCry  == null ? null : u.MotDePasseCry.ToUpper() ) == SysHelper.CalculateSHA1(txtMotDePasse.Text) &&
                                                                                (u.CSociete == null ? null : u.CSociete) == selecteditem
                                                                           );


            // Utilisateur utilisateur1 = Utilisateur.Charger(txtNomUtilisateur.Text.ToUpper(), SysHelper.CalculateSHA1(txtMotDePasse.Text),CB_Societe.SelectedValue.ToString());
            
            if (utilisateur != null 
                && this.lkpSite.EditValue != null
                && UtilisateurSite.Charger(selecteditem, this.lkpSite.EditValue != null ? this.lkpSite.EditValue.ToString() : null, txtNomUtilisateur.Text.ToUpper())!=null)
            {
                Utilisateur = utilisateur;
                Site = this.lkpSite.EditValue.ToString();
                DialogResult = DialogResult.OK;
            }
            else
            {
                //if (CB_SITE.SelectedValue == null)
                //{
                //    XtraMessageBox.Show(
                //        caption: "Erreur",
                //        text: "Veuillez choisir un site",
                //        icon: MessageBoxIcon.Error,
                //        buttons: MessageBoxButtons.OK,
                //        owner: this);
                //    return;
                //}

                XtraMessageBox.Show(
                    caption: "Erreur",
                    text: "Le nom d'utilisateur ou le mot de passe que vous avez entré est incorrect",
                    icon: MessageBoxIcon.Error,
                    buttons: MessageBoxButtons.OK,
                    owner: this);
            }

        }

        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void FrmLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void btnModifier_Click(object sender, EventArgs e)
        {
            var selecteditem = "";
            if (this.lkpSociete.EditValue != null)
                selecteditem = this.lkpSociete.EditValue.ToString();
            IContexteSecurite cs = GestionContexteSecurite.ContexteActive;
            Utilisateur utilisateur = cs.Set<Utilisateur>().FirstOrDefault(u => (u.Login == null ? null : u.Login.ToUpper()) == txtNomUtilisateur.Text.ToUpper() &&
                                                                                (u.MotDePasseCry == null ? null : u.MotDePasseCry.ToUpper()) == SysHelper.CalculateSHA1(txtMotDePasse.Text) &&
                                                                                (u.CSociete == null ? null : u.CSociete) == selecteditem
                                                                          );

            if (utilisateur != null
                && this.lkpSite.EditValue != null
                && UtilisateurSite.Charger(selecteditem, this.lkpSite.EditValue != null ? this.lkpSite.EditValue.ToString() : null, txtNomUtilisateur.Text.ToUpper()) != null)
            {
                _BModifierFocused = true;
                Utilisateur = utilisateur;
                Site = this.lkpSite.EditValue.ToString();
                DialogResult = DialogResult.OK;
            }
            else
            {
                XtraMessageBox.Show(
                    caption: "Erreur",
                    text: "Le nom d'utilisateur ou le mot de passe que vous avez entré est incorrect",
                    icon: MessageBoxIcon.Error,
                    buttons: MessageBoxButtons.OK,
                    owner: this);
            }
        }

        private void loadSociete()
        {
            CtrlHelper.FillLookUpEdit(this.lkpSociete, Societe.ChargerItemCollection());
            CtrlHelper.FillLookUpEdit(this.lkpSite, SocieteSiteCollection.ChargerSociete(null));
            this.lkpSociete.ItemIndex = 0;
            this.lkpSociete.Properties.ReadOnly = true;

            //CB_Societe.DataSource = Societe.Charger_collection();
            //CB_Societe.DisplayMember = "Nom";
            //CB_Societe.ValueMember = "CSociete";
            //CB_Societe.SelectedIndex = 0;
        }

        private void CB_Societe_SelectedIndexChanged(object sender, EventArgs e)
        {
            CtrlHelper.FillLookUpEdit(this.lkpSite, SocieteSiteCollection.ChargerSociete(this.lkpSociete.EditValue != null ? this.lkpSociete.EditValue.ToString() : null));
            
            //if (CB_Societe.SelectedValue != null)
            //{
            //    CB_SITE.DataSource = SocieteSiteCollection.Charger(CB_Societe.SelectedValue.ToString(), null);
            //    CB_SITE.DisplayMember = "Site";
            //    CB_SITE.ValueMember = "CSite";
            //    CB_SITE.SelectedIndex = -1;
            //}
        }

        private void lkpSociete_EditValueChanged(object sender, EventArgs e)
        {
            CtrlHelper.FillLookUpEdit(this.lkpSite, SocieteSiteCollection.ChargerSociete(this.lkpSociete.EditValue != null ? this.lkpSociete.EditValue.ToString() : null));
        }
    }
}