using CST.LePoint.CtrlLibrary.DevExpressEx;
using CST.LePoint.Securite.Entites;
using CST.LePoint.Securite.Management;
using CST.LePoint.Intervention.Properties;
using CST.LePoint.Tools;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;

using System;
using System.Linq;
using System.Windows.Forms;

namespace CST.LePoint.Intervention.DroitsAcces
{
    public partial class FrmRoleListe : XtraForm, IActionsListe, IActionsExport, IActionsEdition
    {
        public FrmRoleListe()
        {
            InitializeComponent();
            Text = "Gestion des Roles";
            initLabels();
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
            var r = (Role)gridV.GetRow(gridV.FocusedRowHandle);
            var frm = new FrmRole(r);
            frm.Text = Resources.Titre_frmRole + @": " + r.Nom;
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

            var u = (Role)gridV.GetRow(gridV.FocusedRowHandle);

            GestionContexteSecurite.ContexteActive.Set<Role>().Remove(u);
            GestionContexteSecurite.ContexteActive.Enregistrer();
            gridV.DeleteRow(gridV.FocusedRowHandle);
        }

        public void Actualiser()
        {
            if (GestionContexteSecurite.ContexteActive.Set<Role>() != null)
                grid.DataSource = GestionContexteSecurite.ContexteActive.Set<Role>().ToList();
        }

        public void Ajouter()
        {
            var frm = new FrmRole();
            frm.Text = Resources.Titre_frmRole;
            ((FrmMDI)MdiParent).LoadForm(frm);
        }

        public void SelectionnerGridRow(bool bHaut)
        {
            if (bHaut)
                gridV.FocusedRowHandle = gridV.FocusedRowHandle - 1;
            else
                gridV.FocusedRowHandle = gridV.FocusedRowHandle + 1;
        }

        private void initLabels()
        {
            gridColDescRole.Caption = Resources.lblDescription;
            gridColIdRole.Caption = Resources.lblIdentifiant;
            gridColNomRole.Caption = Resources.lblNom;
        }

        private void frmRolesListe_Activated(object sender, EventArgs e)
        {
            Actualiser();
        }

        private void grid_DoubleClick(object sender, EventArgs e)
        {
            if (Tag is FrmMDI.FlagSecurite &&
                ((FrmMDI.FlagSecurite)Tag).HasFlag(FrmMDI.FlagSecurite.ModifDisabled)) return;
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