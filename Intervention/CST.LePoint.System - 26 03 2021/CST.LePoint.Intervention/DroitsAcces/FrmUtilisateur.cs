using CST.LePoint.CtrlLibrary;
using CST.LePoint.Securite.DataAccess;
using CST.LePoint.Securite.Entites;
using CST.LePoint.Securite.Management;
using CST.LePoint.Intervention.Properties;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;
using CST.LePoint.Securite;
using System.Text.RegularExpressions;

namespace CST.LePoint.Intervention.DroitsAcces
{
    public partial class FrmUtilisateur : XtraForm, IActionsSave
    {
        /// <summary>
        ///     les roles non enregistrés de l'utilisateur, ceux qui vont être affichés dans le gridControl
        /// </summary>
        private readonly ObservableCollection<Role> roles = new ObservableCollection<Role>();

        private string _CUtilisateur = string.Empty;
        private bool bEditMotDePasse = false;
        public string _CRoleInitial;
        public string ErrorMessage;
        public FrmUtilisateur()
        {
            InitializeComponent();
        }

        private static GvColumnProprietes Titres()
        {
            GvColumnProprietes proprietes = new GvColumnProprietes();
            proprietes.Add(new GvColumnPropriete("[X]", GvColumnPropriete.GvColumnType.Boolean, GvColumnPropriete.GvColumnEtat.Enable));
            proprietes.Add(new GvColumnPropriete("C. Site"));
            proprietes.Add(new GvColumnPropriete("Nom"));
            return proprietes;
        }

        public FrmUtilisateur(string cUtilisateur, bool Modifier)
            : this()
        {
            _CUtilisateur = cUtilisateur;
            this.txtUserName.Properties.ReadOnly = true;
            this.lkpRole.Visible = false;
            this.labRole.Visible = false;
            this.labSite.Visible = false;
            this.chkallcheck.Visible = false;
            this.gridC.Visible = false;
        }
        public void Actualiser()
        {
            CtrlHelper.EmptyControls(this);
            bEditMotDePasse = false;
            LoadData();
            loadSociete();
            if (!string.IsNullOrEmpty(_CUtilisateur))
                ChargerEntite(_CUtilisateur);
            else
                Text = Resources.Titre_frmUtilisateur;
            string selectedval = this.lkpSociete.EditValue == null ? string.Empty : this.lkpSociete.EditValue.ToString();
            CtrlHelper.FillGridView(this.gridVSite, Titres(), SocieteSiteCollection.UtilisateurCharger(selectedval, this.txtUserName.Text));
        }

        private void LoadData()
        {
            ICollection<Role> collection = GestionContexteSecurite.ContexteActive.Set<Role>();

            CtrlHelper.InitGridView(gridVSite, Titres());
            gridVSite.OptionsView.ShowIndicator = false;

            lkpRole.Properties.DropDownRows = 12;
            lkpRole.Properties.AutoComplete = true;
            lkpRole.Properties.Items.BeginUpdate();
            bEditMotDePasse = false;

            try
            {
                lkpRole.Properties.Items.Clear();
                foreach (var item in collection)
                {
                    if (ConfigurationManager.AppSettings["MODE_APPLICATION"].ToString() != "ADMINISTRATION")
                    {
                        if (item.CSociete == GestionSession.SocieteCourante.CSociete)
                            lkpRole.Properties.Items.Add(item.Nom);
                    }
                }
            }
            finally
            {
                lkpRole.Properties.Items.EndUpdate();
            }
        }

        public FrmUtilisateur(string cUtilisateur)
            : this()
        {
            _CUtilisateur = cUtilisateur;
            this.txtUserName.Properties.ReadOnly = true;
        }

        public void ChargerEntite(string cUtilisateur)
        {
            CtrlHelper.EmptyControls(this);
            Utilisateur user = new Utilisateur();

            IContexteSecurite cs = GestionContexteSecurite.ContexteActive;
            bool userNameFound = cs.Set<Utilisateur>().Any(u => u.Login.Trim().ToUpper() == txtUserName.Text.Trim().ToUpper());
            if (!userNameFound)
            {
                user = Utilisateur.Charger(cUtilisateur);
            }

            if (user == null)
                return;

            txtConfirmPassword.EditValue = user.MotDePasseCry;
            txtNom.EditValue = user.Nom;
            txtPassword.EditValue = user.MotDePasseCry;
            txtPrenom.EditValue = user.Prenom;
            txtUserName.EditValue = user.Login;
            Utilisateur utilisateur = cs.Set<Utilisateur>().FirstOrDefault(u => u.Login == cUtilisateur);
            if (utilisateur != null)
            {
                if (utilisateur.CSociete != null)
                {
                    this.lkpSociete.EditValue = utilisateur.CSociete;
                }
                lkpRole.SelectedItem = utilisateur.CRole;
            }

            //if(string.IsNullOrEmpty(_CRoleInitial))
            //{
            //    //Modification N.B.G. 11-02-2015
            //    //lkpRole.EditValue = user.CRole;
            //    try
            //    {
            //        Utilisateur usercontext = cs.Set<Utilisateur>().Where(u => u.Login.Trim().ToUpper() == user.Login.Trim().ToUpper()).First();
            //        lkpRole.EditValue = usercontext.CRole;
            //    }
            //    catch { }
            //    //Fin Modification N.B.G. 11-02-2015
            //}
        }

        public void Enregistrer(bool enregistrerEtFermer)
        {
            if (!ValidateChildren()) return;

            if (this.lkpSociete.EditValue == null)
            {
                XtraMessageBox.Show("Aucune société n'a été sélectionnée !", Resources.NomApplication,
                                                 MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                this.lkpSociete.Focus();
                return;
            }
            // Modification N.B.G. 12-02-2015
            if ((string.IsNullOrEmpty(this.txtUserName.Text)) || string.IsNullOrWhiteSpace(this.txtUserName.Text))
            {
                XtraMessageBox.Show("Le Login est vide !", Resources.NomApplication,
                                                 MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                this.txtUserName.Focus();
                return;
            }
           
            // Fin Modification N.B.G. 12-02-2015

            if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                XtraMessageBox.Show("Mot de passe est vide !", Resources.NomApplication,
                                                 MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                this.txtPassword.Focus();
                return;
            }

            if (txtPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
            {
                XtraMessageBox.Show("Mot de passe est incorrect !", Resources.NomApplication,
                                                  MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                txtConfirmPassword.Text = string.Empty;
                return;
            }
            if ((string.IsNullOrEmpty(this.lkpRole.Text)) || string.IsNullOrWhiteSpace(this.lkpRole.Text))
            {
                XtraMessageBox.Show("Le Rôle est vide !", Resources.NomApplication,
                                                 MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                this.lkpRole.Focus();
                return;
            }
            List<UtilisateurSite> listeUtilisateurSite = null;
            for (int i = 0; i < this.gridVSite.RowCount; i++)
            {
                if (this.gridVSite.GetRowCellValue(i, "[X]").ToString().Equals("True"))
                {
                    if (listeUtilisateurSite == null)
                        listeUtilisateurSite = new List<UtilisateurSite>();
                    UtilisateurSite utilisateursite = new UtilisateurSite();
                    utilisateursite.CUtilisateur = this.txtUserName.Text;
                    utilisateursite.CSite = this.gridVSite.GetRowCellValue(i, "C. Site").ToString();
                    utilisateursite.CSociete = this.lkpSociete.EditValue.ToString();
                    listeUtilisateurSite.Add(utilisateursite);
                }
            }
            if (listeUtilisateurSite == null)
            {
                XtraMessageBox.Show("Aucun Site Sélectionné !", Resources.NomApplication,
                                                 MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            txtUserName.EditValue = txtUserName.Text.Trim();
            string selval = string.IsNullOrEmpty(this.lkpSociete.EditValue.ToString()) ? GestionSession.SocieteCourante.CSociete : this.lkpSociete.EditValue.ToString();
            Utilisateur user = new Utilisateur();
            IContexteSecurite cs = GestionContexteSecurite.ContexteActive;
            bool userNameFound = cs.Set<Utilisateur>().Any(u => u.Login.Trim().ToUpper() == txtUserName.Text.Trim().ToUpper() && u.CSociete == selval);
            if (userNameFound)
            {
                user = cs.Set<Utilisateur>().Where(u => u.Login.Trim().ToUpper() == txtUserName.Text.Trim().ToUpper()).First();
                cs.Set<Utilisateur>().Remove(user);

            }
            bool userfound = true;
            user = Utilisateur.Charger(txtUserName.Text, selval);
            if (user == null)
            {
                user = new Utilisateur();

                userfound = false;
            }
            else
            {
                DialogResult dr = XtraMessageBox.Show(Resources.InfoMsg_MAJEnregistrement, Resources.NomApplication,
                                                      MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (dr != DialogResult.Yes)
                    return;
            }
            user.Login = txtUserName.Text;
            user.Nom = txtNom.Text;
            user.Prenom = txtPrenom.Text;
            user.CSociete = this.lkpSociete.EditValue.ToString();
            user.utilisateurSite = listeUtilisateurSite;
            if (txtConfirmPassword.Text.Trim().Length != 40)
                user.MotDePasse = txtConfirmPassword.Text;

            if (!bEditMotDePasse)
                user.MotDePasseCry = string.Empty;

            user.CRole = lkpRole.Text;
            user.Sauvegarder();

            Role roleUtilisateur = new Role();
            ICollection<Role> roles = GestionContexteSecurite.ContexteActive.Set<Role>().ToList();
            foreach (var r in roles)
            {
                if (r.Nom == lkpRole.Text)
                {
                    roleUtilisateur = r;
                    break;
                }
            }
            user.Roles.Clear();
            roles.ToList().ForEach(r => user.Roles.Add(roleUtilisateur));
            cs.Set<Utilisateur>().Add(user);
            cs.Enregistrer();

            ((FrmMDI)MdiParent).ConfigurerMenu();

            if (enregistrerEtFermer) Close();
            else
            {
                if (userfound)
                {
                    _CUtilisateur = user.Login;
                    this.Text = Resources.Titre_frmUtilisateur + ": " + user.Login;
                    XtraMessageBox.Show("L'utilisateur " + _CUtilisateur + " a été modifié avec succès");
                }
                else
                {
                    XtraMessageBox.Show("L'utilisateur " + txtUserName.Text + " a été ajouté avec succès");
                    txtConfirmPassword.EditValue = "";
                    txtNom.EditValue = "";
                    txtPassword.EditValue = "";
                    txtPrenom.EditValue = "";
                    txtUserName.EditValue = "";
                    lkpRole.SelectedIndex = -1;
                    bEditMotDePasse = false;

                }
            }
        }

        //private void btnAjouter_Click(object sender, EventArgs e)
        //{
        //    var selectRoleDialog = new dialogSelectRole();
        //    if (selectRoleDialog.ShowDialog(this) == DialogResult.OK)
        //    {
        //        if (!roles.Contains(selectRoleDialog.Role))
        //            roles.Add(selectRoleDialog.Role);

        //        //grid.RefreshDataSource();
        //    }
        //}

        //private void btnEnlever_Click(object sender, EventArgs e)
        //{
        //    if (gridV.FocusedRowHandle != GridControl.InvalidRowHandle)
        //    {
        //        var r = (Role)gridV.GetRow(gridV.FocusedRowHandle);
        //        roles.Remove(r);

        //        grid.RefreshDataSource();
        //    }
        //}

        private void frmUtilisateur_Load(object sender, EventArgs e)
        {
            CtrlHelper.ValidationProviderDeclare(dxValidationProvider1, this);
            if (ConfigurationManager.AppSettings["MODE_APPLICATION"].ToString() == "ADMINISTRATION")
                this.lkpSociete.Enabled = true;
            //LoadData();
            //loadSociete();
            //ChargerEntite(_CUtilisateur);

            Actualiser();
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtConfirmPassword.Text != txtPassword.Text)
            {
                txtConfirmPassword.ErrorText = "Vérifier votre mot de passe";
                e.Cancel = true;
            }
            else
            {
                txtConfirmPassword.ErrorText = null;
            }
        }

        private void txtPassword_EditValueChanged(object sender, EventArgs e)
        {
            bEditMotDePasse = true;
        }

        private void loadSociete()
        {
            CtrlHelper.FillLookUpEdit(this.lkpSociete, Societe.ChargerItemCollection());

            //CB_Societe.DataSource = Societe.Charger_collection();
            //CB_Societe.DisplayMember = "Nom";
            //CB_Societe.ValueMember = "CSociete";
            //CB_Societe.SelectedIndex = -1;
            if (ConfigurationManager.AppSettings["MODE_APPLICATION"].ToString() != "ADMINISTRATION")
                this.lkpSociete.EditValue = GestionSession.SocieteCourante.CSociete;
        }

        private void CB_Societe_SelectedValueChanged(object sender, EventArgs e)
        {
            if (ConfigurationManager.AppSettings["MODE_APPLICATION"].ToString() == "ADMINISTRATION")
            {
                ICollection<Role> collection = GestionContexteSecurite.ContexteActive.Set<Role>();

                lkpRole.Properties.DropDownRows = 12;
                lkpRole.Properties.AutoComplete = true;
                lkpRole.Properties.Items.BeginUpdate();

                try
                {
                    lkpRole.Properties.Items.Clear();
                    foreach (var item in collection)
                    {
                        string selectedval = this.lkpSociete.EditValue == null ? string.Empty : this.lkpSociete.EditValue.ToString();

                        CtrlHelper.FillGridView(this.gridVSite, Titres(), SocieteSiteCollection.UtilisateurCharger( selectedval, this.txtUserName.Text));

                        if (item.CSociete == selectedval)
                            lkpRole.Properties.Items.Add(item.Nom);
                    }
                }
                finally
                {
                    lkpRole.Properties.Items.EndUpdate();
                }
            }
        }

        private bool ValidatePassword(string password, out string ErrorMessage)
        {
            var input = password;
            ErrorMessage = string.Empty;

            //if (string.IsNullOrWhiteSpace(input))
            //{
            //    throw new Exception("Mot de passe est vide !");
            //}

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{8}");
            //var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            //if (!hasLowerChar.IsMatch(input))
            //{
            //    ErrorMessage = "Password should contain At least one lower case letter";
            //    return false;
            //}
             if (!hasUpperChar.IsMatch(input))
            {
                ErrorMessage = "Le mot de passe doit contenir au moins une lettre majuscule";
                return false;
            }
            if (!hasMiniMaxChars.IsMatch(input))
            {
                ErrorMessage = "Le mot de passe ne doit pas être inférieur à 8 caractères";
                return false;

            }
            else if (!hasNumber.IsMatch(input))
            {
                ErrorMessage = "Le mot de passe doit contenir au moins une valeur numérique [0->9]";
                return false;
            }

            else if (!hasSymbols.IsMatch(input))
            {
                ErrorMessage = "Le mot de passe doit contenir au moins un caractère spécial	[!@#$%^&*()_+=\"[{]};:<>|./?,-]";
                return false;
            }
            else
            {
                ErrorMessage = "";
                return true;
            }
        }      

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            return;
            if (!String.IsNullOrEmpty(txtPassword.Text))
            {
                if (!ValidatePassword(this.txtPassword.Text, out ErrorMessage))
                {
                    e.Cancel = true;
                    this.txtError.Text = ErrorMessage;
                }
            }
            this.txtError.Text = ErrorMessage;
        }

        private void chkallcheck_CheckedChanged(object sender, EventArgs e)
        {
            this.gridVSite.BeginSort();
            CheckEdit ce = sender as CheckEdit;
            for (int i = 0; i < gridVSite.RowCount; i++)
            {
                this.gridVSite.SetRowCellValue(i, "[X]", ce.Checked);
            }
            this.gridVSite.EndDataUpdate();
        }

        private void lkpSociete_EditValueChanged(object sender, EventArgs e)
        {
            if (ConfigurationManager.AppSettings["MODE_APPLICATION"].ToString() == "ADMINISTRATION")
            {
                ICollection<Role> collection = GestionContexteSecurite.ContexteActive.Set<Role>();

                lkpRole.Properties.DropDownRows = 12;
                lkpRole.Properties.AutoComplete = true;
                lkpRole.Properties.Items.BeginUpdate();

                try
                {
                    lkpRole.Properties.Items.Clear();
                    foreach (var item in collection)
                    {
                        string selectedval = this.lkpSociete.EditValue == null ? string.Empty : this.lkpSociete.EditValue.ToString();

                        CtrlHelper.FillGridView(this.gridVSite, Titres(), SocieteSiteCollection.UtilisateurCharger(selectedval, this.txtUserName.Text));

                        if (item.CSociete == selectedval)
                            lkpRole.Properties.Items.Add(item.Nom);
                    }
                }
                finally
                {
                    lkpRole.Properties.Items.EndUpdate();
                }
            }
        }
    }
}