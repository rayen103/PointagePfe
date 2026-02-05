using CST.LePoint.Securite.Entites;
using CST.LePoint.Securite.Management;
using CST.LePoint.Intervention.Properties;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using System;
using System.Linq;
using System.Windows.Forms;

namespace CST.LePoint.Intervention.DroitsAcces
{
    public partial class dialogSelectRole : XtraForm
    {
        public Role Role;

        //private IEnumerable<Role> except;
        //public dialogSelectRole(IEnumerable<Role> except)
        public dialogSelectRole()
        {
            InitializeComponent();
            //this.except = except;
            Text = Resources.Titre_dialogSelectRole;
            InitialisationLabels();
        }

        private void InitialisationLabels()
        {
            gridColDescRole.Caption = Resources.lblDescription;
            gridColIdRole.Caption = Resources.lblIdentifiant;
            gridColNomRole.Caption = Resources.lblNom;
            btnAnnuler.Text = Resources.btnAnnuler;
            btnOK.Text = Resources.btnOk;
        }

        private void dialogSelectRole_Load(object sender, EventArgs e)
        {
            //var rows = GestionContexteSecurite.ContexteActive.Set<Role>().Except(except).ToList();
            var rows = GestionContexteSecurite.ContexteActive.Set<Role>().ToList();
            if (rows.Count == 0)
            {
                XtraMessageBox.Show(
                    caption: Resources.NomApplication,
                    text: "Il n'y a pas de roles disponibles!",//TODO TO Ressources!
                    owner: this,
                    icon: MessageBoxIcon.Warning,
                    buttons: MessageBoxButtons.OK
                );
                Dispose();
            }
            grid.DataSource = rows;
        }

        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void grid_DoubleClick(object sender, EventArgs e)
        {
            btnOK_Click(sender, e);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (gridV.FocusedRowHandle != GridControl.InvalidRowHandle)
            {
                Role = (Role)gridV.GetRow(gridV.FocusedRowHandle);
                DialogResult = DialogResult.OK;
            }
        }
    }
}