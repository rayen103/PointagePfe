using CST.LePoint.CtrlLibrary;
using CST.LePoint.CtrlLibrary.DevExpressEx;
using CST.LePoint.Intervention.Properties;
using CST.LePoint.Securite;
using CST.LePoint.Securite.Entites;
using CST.LePoint.Tools;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraPivotGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CST.LePoint.Intervention.Rattachements
{
    public partial class FrmAnalyseBITrace : DevExpress.XtraEditors.XtraForm, IActionsEdition, IActionsSuppression
    {
        public FrmAnalyseBITrace()
        {
            InitializeComponent();
        }

        public void Apercu()
        {
            DXReport.Apercu(pivotGridControl1, "Suivi Passage", new Margins(20, 20, 60, 30), false, true);
        }

        public void Actualiser()
        {
            var waitForm = new WaitDialogForm("Chargement en cours...",
                                  "Veuillez patienter !");
            try
            {

                CtrlHelper.FillComboBoxEdit(LkpListePG, PivotGridCollection.Charger(LkpListePG.Text, GestionSession.UtilisateurCourant.IdUtilisateur, this.Name));
                LkpListePG.Text = string.Empty;
                for (int i = 0; i < pivotGridControl1.Fields.Count; i++)
                {
                    pivotGridControl1.Fields[i].FilterValues.Clear();
                    pivotGridControl1.Fields[i].SummaryFilter.Clear();
                }

                this.view_AnalyseTraceTableAdapter.Fill(this.dataSetAnalyseTrace.View_AnalyseTrace, this.datedebut.DateTime.Date, this.datefin.DateTime.Date);

                waitForm.Close();
                waitForm.Dispose();
            }
            catch (Exception ex)
            {
                waitForm.Close();
                waitForm.Dispose();
                XtraMessageBox.Show(ex.Message,
                                        Resources.NomApplication,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                return;
            }
        }

        private void Sauvgarder()
        {
            string Nom = string.Empty;
            Nom = LkpListePG.Text;
            PivotGrid PivotGrid = new PivotGrid();
            string path = Directory.GetCurrentDirectory();
            if (Nom == string.Empty)
            {
                XtraMessageBox.Show("Veuillez saisir un Nom pour Pivot Grid !", "Champ obligatoire");
            }
            else
            {
                PivotGrid.NomPivotGrid = this.Name;
                PivotGrid.NomRapport = LkpListePG.Text;
                PivotGrid.Chemin = path + "\\" + Nom + ".xml";
                PivotGrid.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                PivotGrid.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                PivotGrid.PCInsertion = Environment.MachineName;
                PivotGrid.PCModification = Environment.MachineName;
                PivotGrid.Sauvgarder();
                pivotGridControl1.SaveLayoutToXml("" + Nom + ".xml", OptionsLayoutBase.FullLayout);
                CtrlHelper.FillComboBoxEdit(LkpListePG, PivotGridCollection.Charger(LkpListePG.Text, GestionSession.UtilisateurCourant.IdUtilisateur, this.Name));

            }
        }

        public void Supprimer()
        {
            string Nom = string.Empty;
            string path = Directory.GetCurrentDirectory();
            Nom = LkpListePG.Text;
            PivotGrid PG = new PivotGrid();



            if (LkpListePG.Text == "")
            {
                XtraMessageBox.Show("Veuillez Sélectionnez un Document à supprimer !", "Supprimer");
                return;
            }
            else
            {
                DialogResult dr = XtraMessageBox.Show("voulez vous supprimer cet élément ? : " + LkpListePG.Text,
                      "Supprimer", MessageBoxButtons.YesNo);
                switch (dr)
                {
                    case DialogResult.Yes:
                        File.Delete(path + "\\" + Nom + ".xml");
                        PG.Supprimmer(Nom);
                        Actualiser();
                        LkpListePG.Text = "";
                        break;
                    case DialogResult.No: break;
                        return;
                }
            }
        }

        private void FrmAnalyseBITrace_Load(object sender, EventArgs e)
        {
            var waitForm = new WaitDialogForm("Chargement en cours...",
                                   "Veuillez patienter !");
            try
            {
                pivotGridControl1.OptionsView.HideAllTotals();
                this.datedebut.DateTime = DateTime.Now;
                this.datefin.DateTime = DateTime.Now;
                CtrlHelper.FillComboBoxEdit(LkpListePG, PivotGridCollection.Charger(LkpListePG.Text, GestionSession.UtilisateurCourant.IdUtilisateur, this.Name));

                this.view_AnalyseTraceTableAdapter.Fill(this.dataSetAnalyseTrace.View_AnalyseTrace, this.datedebut.DateTime.Date, this.datefin.DateTime.Date);

                pivotGridControl1.ForceInitialize();
                pivotGridControl1.RestoreLayoutFromXml(SysHelper.XmlLayoutFileName(this, GestionSession.UtilisateurCourant.IdUtilisateur));
            }
            catch
            {
                return;
            }
            finally
            {
                waitForm.Close();
                waitForm.Dispose();
            }
        }

        private void FrmAnalyseBITrace_FormClosing(object sender, FormClosingEventArgs e)
        {
            pivotGridControl1.SaveLayoutToXml(SysHelper.XmlLayoutFileName(this, GestionSession.UtilisateurCourant.IdUtilisateur));
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            PivotGrid pv = PivotGrid.Charger(LkpListePG.Text, GestionSession.UtilisateurCourant.IdUtilisateur, this.Name);

            try
            {

                if (LkpListePG.Text.Contains("/"))
                {
                    XtraMessageBox.Show("Format non définit !");
                    LkpListePG.Text = string.Empty;
                    return;
                }



                if (LkpListePG.Text == string.Empty)
                {
                    XtraMessageBox.Show("Format non définit  !", "Champ obligatoire");
                }
                else
                {
                    if (pv != null && LkpListePG.Text == pv.NomRapport)
                    {

                        string Chemin = pv.Chemin;

                        DialogResult dr = XtraMessageBox.Show("Nom déjà trouvé !, voulez vous le Remplacer ?",
                       "Enregistrer", MessageBoxButtons.YesNo);
                        switch (dr)
                        {
                            case DialogResult.Yes:
                                Sauvgarder();
                                XtraMessageBox.Show("Enregistrement avec succès", "Enregistrer");
                                break;

                            case DialogResult.No: break;
                                return;
                        }
                    }
                    else
                    {
                        Sauvgarder();
                        XtraMessageBox.Show("Enregistrement avec succès", "Enregistrer");
                    }
                }
            }
            catch (Exception)
            {
                Sauvgarder();
                XtraMessageBox.Show("Enregistrement avec succès", "Enregistrer");
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            var waitForm = new WaitDialogForm("Chargement en cours...",
                       "Veuillez patienter !");
            try
            {
                PivotGrid pv = PivotGrid.ChargerChemin(LkpListePG.Text, GestionSession.UtilisateurCourant.IdUtilisateur, this.Name);
                pivotGridControl1.RestoreLayoutFromXml(pv.Chemin, OptionsLayoutBase.FullLayout);
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Format non définit !", "Grille de pivot");

            }
            finally
            {
                waitForm.Close();
                waitForm.Dispose();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                pivotGridControl1.OptionsView.ShowAllTotals();
            else
                pivotGridControl1.OptionsView.HideAllTotals();
        }
    }
}
