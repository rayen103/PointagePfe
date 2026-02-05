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
    public partial class FrmSocieteListe : XtraForm, IActionsListe, IActionsEdition, IActionsExport
    {
        public FrmSocieteListe()
        {
            InitializeComponent();
            Text = "Gestion des Sociétés";
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
            //if (gridV.FocusedRowHandle == GridControl.InvalidRowHandle)
            //    return;
            //var s = (Securite.Entites.Societe)gridV.GetRow(gridV.FocusedRowHandle);
            //FrmSociete frm = new FrmSociete(s);
            //frm.Text = "Société" + ": " + s.Id;
            //((CST.LePoint.FrmMDI)this.MdiParent).LoadForm(frm);
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

            var s = (Societe)gridV.GetRow(gridV.FocusedRowHandle);

            GestionContexteSecurite.ContexteActive.Set<Societe>().Remove(s);

            GestionContexteSecurite.ContexteActive.Enregistrer();
            gridV.DeleteRow(gridV.FocusedRowHandle);
            // Actualiser();
        }

        public void Actualiser()
        {
            grid.DataSource = GestionContexteSecurite.ContexteActive.Set<Societe>().ToList();
        }

        public void Ajouter()
        {
            var frm = new FrmSociete();
            frm.Text = "Société";
            ((FrmMDI)MdiParent).LoadForm(frm);
        }

        public void SelectionnerGridRow(bool bHaut)
        {
            if (bHaut)
                gridV.FocusedRowHandle = gridV.FocusedRowHandle - 1;
            else
                gridV.FocusedRowHandle = gridV.FocusedRowHandle + 1;
        }

        private void frmSocieteListe_Load(object sender, EventArgs e)
        {
        }

        private void frmSocieteListe_Activated(object sender, EventArgs e)
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