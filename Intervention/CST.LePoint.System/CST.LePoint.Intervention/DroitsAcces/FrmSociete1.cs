using CST.LePoint.Securite;
using CST.LePoint.Securite.Entites;
using CST.LePoint.Intervention.Properties;
using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;

namespace CST.LePoint.Intervention.DroitsAcces
{
    public partial class FrmSociete : XtraForm, IActionsSave
    {
        //private bool addMode;

        public FrmSociete()
        {
            InitializeComponent();
        }

        public void Enregistrer(bool enregistrerEtFermer)
        {
            if (ValidateChildren())
            {
                txtCSociete.EditValue = txtCSociete.Text.Trim();
                txtNom.EditValue = txtNom.Text.Trim();

                //IContexteSecurite cs = GestionContexteSecurite.ContexteActive;

                //bool NomSocieteTrouve = cs.Set<Societe>().Any(u => u.CSociete == txtCSociete.Text);

                //if (societe.CSociete != txtCSociete.Text && NomSocieteTrouve)
                //{
                //    txtNom.ErrorText = "le nom de la société existe déjà!!"; //TODO To Ressource
                //    return;
                //}
                //else
                //    txtNom.ErrorText = null;

                DialogResult dr = XtraMessageBox.Show(Resources.InfoMsg_MAJEnregistrement, Resources.NomApplication,
                                                        MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                switch (dr)
                {
                    case DialogResult.Cancel:
                        return;

                    case DialogResult.No:
                        Close();
                        return;

                    case DialogResult.Yes:
                        break;
                }

                Societe societe = new Societe();
                societe.CSociete = txtCSociete.Text;
                societe.Nom = txtNom.Text;
                societe.Adresse = txtAdresse.Text;
                societe.CodePostal = txtCPostal.Text;
                societe.CTVA = txtCodeTVA.Text;
                societe.Email = txtEmail.Text;
                societe.Fax = txtFax.Text;
                societe.Pays = txtPays.Text;
                societe.RaisonSociale = txtInitiales.Text;
                societe.RegistreCommerce = txtRegistreCommerce.Text;
                societe.Telephone = txtNTel.Text;
                societe.Ville = txtVille.Text;
                societe.DateOuverture = DateTime.Parse(txtDateOuverture.Text);
                societe.DateModification = DateTime.Now;
                societe.BAssujetti = this.chkBAssujetti.Checked;
                societe.Sauvegarder();

                //if (cs.Set<Societe>() != null)
                //{
                //    if (cs.Set<Societe>().Count > 0)
                //        cs.Set<Societe>().Clear();
                //    cs.Set<Societe>().Add(societe);
                //}
                //else
                //{
                //    cs.Charger();
                //}

                //cs.Enregistrer();

                GestionSession.SocieteCourante = societe;

                Text = "Société: " + societe.CSociete;
                txtCSociete.EditValue = societe.CSociete;
                ((FrmMDI)MdiParent).ConfigurerMenu();

                if (enregistrerEtFermer)
                    Close();
            }
        }

        public void Actualiser()
        {
            Societe societe = Societe.Charger();

            //List<Societe> listeSocietes = GestionContexteSecurite.ContexteActive.Set<Societe>().ToList();
            //listeSocietes.Sort((s1, s2) => String.CompareOrdinal(s1.Nom, s2.Nom));
            //societe = listeSocietes[0];

            txtCSociete.EditValue = societe.CSociete;
            txtNom.EditValue = societe.Nom;
            txtAdresse.EditValue = societe.Adresse;
            txtCPostal.EditValue = societe.CodePostal;
            txtCodeTVA.EditValue = societe.CTVA;
            txtEmail.EditValue = societe.Email;
            txtFax.EditValue = societe.Fax;
            txtPays.EditValue = societe.Pays;
            txtInitiales.EditValue = societe.RaisonSociale;
            txtRegistreCommerce.EditValue = societe.RegistreCommerce;
            txtNTel.EditValue = societe.Telephone;
            txtVille.EditValue = societe.Ville;
            txtDateOuverture.EditValue = societe.DateOuverture;
            this.chkBAssujetti.Checked = societe.BAssujetti;
            GestionSession.SocieteCourante = societe;
        }

        private void FrmSociete_Load(object sender, EventArgs e)
        {
            Actualiser();
        }
    }
}