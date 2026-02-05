using CST.LePoint.CtrlLibrary;
using CST.LePoint.CtrlLibrary.DevExpressEx;
using CST.LePoint.CtrlLibrary.Satellites;
using CST.LePoint.Securite;
using CST.LePoint.Securite.Entites;
using CST.LePoint.Securite.GestionActions;
using CST.LePoint.Securite.Management;
using CST.LePoint.Intervention.DroitsAcces;
using CST.LePoint.Intervention.Properties;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraNavBar;
using DevExpress.XtraTab.ViewInfo;
using DevExpress.XtraTabbedMdi;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace CST.LePoint.Intervention
{
      public sealed partial class FrmMDI : XtraForm
    {
        # region - Propriétés -
        public bool _BModifyUser;
        public string _CRoleInitial;

        // Chaque utilisateur a son propre fichier de configuration de thème dans le dossier de l'exécutable son nom est "LookAndFeelSetting" suivi de l'identité :) B.G.N
        public string LookAndFeelSettingFileName = "LookAndFeelSettings_" + GestionSession.UtilisateurCourant.IdUtilisateur + ".dat";

        private const string SkinMask = "Thème : ";

        public static readonly CFApplication2 CfMenuApplication = CFApplication2.Deserialiser(Resources.CFMenus,
                                                                                 Resources.CFEvenements);

        public static readonly CFApplication2 CfNavBarApplication = CFApplication2.Deserialiser(Resources.CFNavBar,
                                                                               Resources.CFEvenements);

        // public static readonly CFApplication2 CfNavApplication = CFApplication2.Deserialiser(Resources.CFNavBar, Resources.CFEvenements);

        private readonly string _defaultSkinName = string.Empty;

        public object Obj;
        // La variable BFermer pour éviter un message d'erreur lors de la fermeture accélerer de plusieurs écrans B.G.N :)
        public static bool BFermer = false;

        #endregion

        # region - Evènements -

        [Flags]
        public enum FlagSecurite
        {
            ModifDisabled
        }

        public static void PostEditGridView(Control ctrl)
        {
            switch (ctrl.GetType().Name)
            {
                case "GridControl":
                    var myGrid = (GridControl)ctrl;
                    GridView gridView = (GridView)myGrid.DefaultView;
                    if (gridView.OptionsBehavior.Editable)
                        gridView.PostEditor();
                    break;

                //case "SpinEdit":
                //    var mySpinEdit = (SpinEdit)ctrl;
                //    mySpinEdit.EditValue = mySpinEdit.Text;
                //    break;

                //case "DateEdit":
                //    var myDateEdit = (DateEdit)ctrl;
                //    myDateEdit.EditValue = myDateEdit.Text;
                //    break;

                //case "ComboBoxEdit":
                //    var myComboBoxEdit = (ComboBoxEdit)ctrl;
                //    myComboBoxEdit.EditValue = myComboBoxEdit.Text;
                //    break;

                //case "LookUpEdit":
                //    var myLookUpEdit = (LookUpEdit)ctrl;
                //    //  myLookUpEdit.EditValue = myLookUpEdit.Text;
                //    break;
            }

            if (ctrl.Controls.Count > 0)
            {
                foreach (Control ctrlfils in ctrl.Controls)
                    PostEditGridView(ctrlfils);
            }
        }

        public FrmMDI()
        {
            InitializeComponent();

            LookAndFeelSettings.Load(LookAndFeelSettingFileName, out _defaultSkinName);

            if (GestionSession.UtilisateurCourant != null)
                bsiNomUtilisateur.Caption = string.Format("{0} {1}", GestionSession.UtilisateurCourant.Nom,
                                                          GestionSession.UtilisateurCourant.Prenom);
            else
                bsiNomUtilisateur.Caption = "";

            Text = Resources.NomApplication + " : " + GestionSession.SocieteCourante.RaisonSociale;
            navBarMDIForm.PaintStyleKind = NavBarViewKind.NavigationPane;
            navBarMDIForm.Dock = DockStyle.Left;

            ConfigurerToolBar(this);
            InitialisationSkins();
            ConfigurerMenu();
            ConfigurerNavBar();

            //Pour éliminer l'arrière plan blanc des boutons B.G.N :)
            //btnBas.Glyph = DevExpress.Utils.Controls.ImageHelper.MakeTransparent(ResourcesImages.down_16);
            //btnHaut.Glyph = DevExpress.Utils.Controls.ImageHelper.MakeTransparent(ResourcesImages.up_16);
            //btnFermer.Glyph = DevExpress.Utils.Controls.ImageHelper.MakeTransparent(ResourcesImages.cancel_16);
            //btnActualiser.Glyph = DevExpress.Utils.Controls.ImageHelper.MakeTransparent(ResourcesImages.refresh_16);
            //btnAjouter.Glyph = DevExpress.Utils.Controls.ImageHelper.MakeTransparent(ResourcesImages.add_16);
            //btnModifier.Glyph = DevExpress.Utils.Controls.ImageHelper.MakeTransparent(ResourcesImages.edit_16);
            //btnRechercher.Glyph = DevExpress.Utils.Controls.ImageHelper.MakeTransparent(ResourcesImages.research_16);
            //btnApercu.Glyph = DevExpress.Utils.Controls.ImageHelper.MakeTransparent(ResourcesImages.printer_16);
            //btnEnregistrer.Glyph = DevExpress.Utils.Controls.ImageHelper.MakeTransparent(ResourcesImages.save_16);
            //btnEnregistrerFermer.Glyph = DevExpress.Utils.Controls.ImageHelper.MakeTransparent(ResourcesImages.saveclose_16);
            //btnConfigurer.Glyph = DevExpress.Utils.Controls.ImageHelper.MakeTransparent(ResourcesImages.settings_16);
            //btnSupprimer.Glyph = DevExpress.Utils.Controls.ImageHelper.MakeTransparent(ResourcesImages.delete_16);
            //bsiExport.Glyph = DevExpress.Utils.Controls.ImageHelper.MakeTransparent(ResourcesImages.Menu_Upload_18);
            //btnExportPdf.Glyph = DevExpress.Utils.Controls.ImageHelper.MakeTransparent(ResourcesImages.Menu_ExportPdf_18);
            //btnExportXls.Glyph = DevExpress.Utils.Controls.ImageHelper.MakeTransparent(ResourcesImages.Menu_ExportXls_18);
            //btnExportXlsx.Glyph = DevExpress.Utils.Controls.ImageHelper.MakeTransparent(ResourcesImages.Menu_ExportXlsx_18);
            //btnExportTxt.Glyph = DevExpress.Utils.Controls.ImageHelper.MakeTransparent(ResourcesImages.Menu_ExportTxt_18);
        }

        public FrmMDI(bool bModifyUser, string cRoleInitial)
            : this()
        {
            // Surcharge pour donner l'accés à un utilisateur de modifier ses coordonnées :) B.G.N
            _BModifyUser = bModifyUser;
            _CRoleInitial = cRoleInitial;
        }

        #region Skins

        private void InitialisationSkins()
        {
            int i = 0;
            barManager.ForceInitialize();
            foreach (SkinContainer cnt in SkinManager.Default.Skins)
            {
                if (i < 60)
                {
                    var item = new BarButtonItem(barManager, SkinMask + cnt.SkinName);
                    brThemes.AddItem(item);
                    item.ItemClick += OnSkinClick;
                }
                else
                    break;

                i = i + 1;
            }

            brThemes.Caption = SkinMask + _defaultSkinName;
        }

        private void OnSkinClick(object sender, ItemClickEventArgs e)
        {
            string skinName = e.Item.Caption.Replace(SkinMask, string.Empty);
            barManager.GetController().PaintStyleName = "Skin";
            brThemes.Caption = e.Item.Caption;
            brThemes.Hint = brThemes.Caption;
            brThemes.ImageIndex = -1;
            UserLookAndFeel.Default.SetSkinStyle(skinName);
        }

        private void brThemes_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.Item.Name == "brThemes")
                return;
            barManager.GetController().PaintStyleName = e.Item.Description;
            InitialisationBarTheme(e.Item);
            barManager.GetController().ResetStyleDefaults();
            UserLookAndFeel.Default.SetDefaultStyle();
        }

        private void InitialisationBarTheme(BarItem item)
        {
            if (item == null) return;
            brThemes.ImageIndex = item.ImageIndex;
            brThemes.Caption = item.Caption;
            brThemes.Hint = item.Description;
        }

        #endregion

        public static void APropos(Form parent)
        {
            string text = Resources.NomApplication + Environment.NewLine +
                          "Copyright © " + DateTime.Now.Year;
            XtraMessageBox.Show(parent, text, Resources.NomApplication, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void Quitter(Form parent)
        {
            if (
                XtraMessageBox.Show(parent, "Êtes-vous sûr de vouloir quitter l'application ?", Resources.NomApplication,
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) ==
                DialogResult.Yes)
            {
                BFermer = true;
                Application.Exit();
            }
        }

        internal void ConfigurerNavBar()
        {
            // Lors de l'actualisation aprés la modification d'un rôle B.G.N :) 
            navBarMDIForm.Items.Clear();

            Action<FrmMDI, Form, string> ac = (parent, instance, titre) =>
            {
                for (int i = 0; i < parent.MdiManager.Pages.Count; i++)
                {
                    if (MdiManager.Pages[i].Text == titre)
                    {
                        MdiManager.SelectedPage = MdiManager.Pages[i];
                        instance.Dispose();
                        return;
                    }
                }
                instance.Text = titre;
                instance.Dock = DockStyle.Fill;
                instance.MdiParent = parent;
                instance.Show();
            };

            ((ISupportInitialize)(navBarMDIForm)).BeginInit();
            SuspendLayout();
            var rempBar = new RemplisseurNavBar<FrmMDI>(
                CfNavBarApplication, this, navBarMDIForm, ResourcesMenus.ResourceManager, ResourcesImages.ResourceManager,
                ac);

            if (GestionSession.SecuriteActive && GestionSession.UtilisateurCourant != null)
            {
                rempBar.PeutFaireAction =
                    idEvent => GestionUtilisateur.EstAutorise(
                        idEvent, Actions.Consulter, GestionSession.UtilisateurCourant);
            }

            rempBar.RemplirNavBar(navBarMDIForm);
            ((ISupportInitialize)(navBarMDIForm)).EndInit();
            ResumeLayout(true);
        }

        internal void ConfigurerMenu()
        {
            barMenu.ItemLinks.Clear();

            Action<FrmMDI, Form, string> ac = (parent, instance, titre) =>
            {
                for (int i = 0; i < parent.MdiManager.Pages.Count; i++)
                {
                    if (MdiManager.Pages[i].Text == titre)
                    {
                        MdiManager.SelectedPage = MdiManager.Pages[i];
                        instance.Dispose();
                        return;
                    }
                }
                instance.Text = titre;
                instance.Dock = DockStyle.Fill;
                instance.MdiParent = parent;
                instance.Show();
            };

            ((ISupportInitialize)(barManager)).BeginInit();
            SuspendLayout();
            var rempBar = new RemplisseurBar<FrmMDI>(
                CfMenuApplication, this, barManager, ResourcesMenus.ResourceManager, ResourcesImages.ResourceManager,
                ac);

            if (GestionSession.SecuriteActive && GestionSession.UtilisateurCourant != null)
            {
                rempBar.PeutFaireAction =
                    idEvent => GestionUtilisateur.EstAutorise(idEvent, Actions.Consulter, GestionSession.UtilisateurCourant);
            }

            rempBar.RemplirBar(barMenu);
            ((ISupportInitialize)(barManager)).EndInit();
            ResumeLayout(true);
        }

        internal void LoadForm(Form child)
        {
            var waitForm = new WaitDialogForm("Chargement en cours...",
                                               "Veuillez patienter !");
            for (int i = 0; i < MdiManager.Pages.Count; i++)
            {
                if ((MdiManager.Pages[i].Text == child.Text) && (child.Name == MdiManager.Pages[i].MdiChild.Name))
                {
                    MdiManager.SelectedPage = MdiManager.Pages[i];
                    waitForm.Close();
                    waitForm.Dispose();
                    return;
                }
            }
            child.MdiParent = this;
            child.Show();

            waitForm.Close();
            waitForm.Dispose();
        }

        private void btnActualiser_ItemClick(object sender, ItemClickEventArgs e)
        {
            TraiterActionFormulaire(btnActualiser);
        }

        private void btnAjouter_ItemClick(object sender, ItemClickEventArgs e)
        {
            TraiterActionFormulaire(btnAjouter);
        }

        private void btnApercu_ItemClick(object sender, ItemClickEventArgs e)
        {
            TraiterActionFormulaire(btnApercu);
        }

        private void btnBas_ItemClick(object sender, ItemClickEventArgs e)
        {
            TraiterActionFormulaire(btnBas);
        }

        private void btnChangerFormat_ItemClick(object sender, ItemClickEventArgs e)
        {
            TraiterActionFormulaire(btnChangerFormat);
        }

        private void btnCreerFormat_ItemClick(object sender, ItemClickEventArgs e)
        {
            TraiterActionFormulaire(btnCreerFormat);
        }

        private void btnDupliquer_ItemClick(object sender, ItemClickEventArgs e)
        {
            TraiterActionFormulaire(btnDupliquer);
        }

        private void btnEnregistrer_ItemClick(object sender, ItemClickEventArgs e)
        {
            PostEditGridView(this.ActiveMdiChild);
            TraiterActionFormulaire(btnEnregistrer);
        }

        private void btnEnregistrerFermer_ItemClick(object sender, ItemClickEventArgs e)
        {
            PostEditGridView(this.ActiveMdiChild);
            TraiterActionFormulaire(btnEnregistrerFermer);
        }

        private void btnExportPdf_ItemClick(object sender, ItemClickEventArgs e)
        {
            TraiterActionFormulaire(btnExportPdf);
        }

        private void btnExportTxt_ItemClick(object sender, ItemClickEventArgs e)
        {
            TraiterActionFormulaire(btnExportTxt);
        }

        private void btnExportXls_ItemClick(object sender, ItemClickEventArgs e)
        {
            TraiterActionFormulaire(btnExportXls);
        }

        private void btnExportXlsx_ItemClick(object sender, ItemClickEventArgs e)
        {
            TraiterActionFormulaire(btnExportXlsx);
        }

        private void btnFermer_ItemClick(object sender, ItemClickEventArgs e)
        {
            FermerOnglet(ActiveMdiChild);
        }

        private void btnHaut_ItemClick(object sender, ItemClickEventArgs e)
        {
            TraiterActionFormulaire(btnHaut);
        }

        private void btnImport_ItemClick(object sender, ItemClickEventArgs e)
        {
            TraiterActionFormulaire(btnImport);
        }

        private void btnImprimer_ItemClick(object sender, ItemClickEventArgs e)
        {
            TraiterActionFormulaire(btnImprimer);
        }

        private void btnModifier_ItemClick(object sender, ItemClickEventArgs e)
        {
            TraiterActionFormulaire(btnModifier);
        }

        private void btnRechercher_ItemClick(object sender, ItemClickEventArgs e)
        {
            TraiterActionFormulaire(btnRechercher);
        }

        private void btnSupprimer_ItemClick(object sender, ItemClickEventArgs e)
        {
            TraiterActionFormulaire(btnSupprimer);
        }

        private void cmbtnActualiser_Click(object sender, EventArgs e)
        {
            TraiterActionFormulaire(cmbtnActualiser);
        }

        private void cmbtnEnregistrer_Click(object sender, EventArgs e)
        {
            TraiterActionFormulaire(cmbtnEnregistrer);
        }

        private void cmbtnEnregistrerEtFermer_Click(object sender, EventArgs e)
        {
            TraiterActionFormulaire(cmbtnEnregistrerEtFermer);
        }

        private void cmbtnFermer_Click(object sender, EventArgs e)
        {
            FermerOnglet(ActiveMdiChild);
        }

        private void cmbtnModifier_Click(object sender, EventArgs e)
        {
            TraiterActionFormulaire(cmbtnModifier);
        }

        private void cmbtnSupprimer_Click(object sender, EventArgs e)
        {
            TraiterActionFormulaire(cmbtnSupprimer);
        }

        private void FermerOnglet(Form frmToClose)
        {
            try
            {
                if (MdiManager.Pages.Count == 0)
                {
                    if (
                        XtraMessageBox.Show("Êtes vous sûr de vouloir quitter l'application ?", Resources.NomApplication,
                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                            MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                 
                        BFermer = true;
                        Application.Exit();
                    }
                }
                else
                {
                    var activeForm = frmToClose as IActionsSave;
                    if (activeForm != null)
                    {
                        frmToClose.Close();
                    }
                    else
                        frmToClose.Close();
                }
            }
            catch (System.InvalidOperationException)
            {
                if (
                          XtraMessageBox.Show("Êtes vous sûr de vouloir quitter l'application ?", Resources.NomApplication,
                                              MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                              MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    Application.Exit();
                }
            }
        }

        private void ConfigurerToolBar(Form frm)
        {
            try
            {
                int compteur = 0;
                const int dockColMax = 10;

                barManager.ForceInitialize();

                cmbtnActualiser.Visible = false;

                cmbtnAjouter.Visible = false;
                cmbtnImprimer.Visible = false;
                cmbtnModifier.Visible = false;
                cmbtnSupprimer.Visible = false;
                cmbtnEnregistrer.Visible = false;
                cmbtnEnregistrerEtFermer.Visible = false;
                cmbtnFermer.Visible = false;

                btnActualiser.Visibility = BarItemVisibility.Never;
                btnConfigurer.Visibility = BarItemVisibility.Never;
                btnRechercher.Visibility = BarItemVisibility.Never;
                btnSupprimer.Visibility = BarItemVisibility.Never;

                barManager.Bars[brSkins.BarName].Visible = true;
                barManager.Bars[brSkins.BarName].DockCol = compteur;
                compteur++;

                barManager.Bars[brInitialisation.BarName].Visible = true;
                barManager.Bars[brInitialisation.BarName].DockCol = compteur;
                compteur++;

                foreach (Bar bar in barManager.Bars)
                {
                    if ((bar != barMenu) && (bar != barStatus) && (bar != brInitialisation) && (bar != brSkins))
                    {
                        bar.Visible = false;
                        bar.DockCol = dockColMax;
                    }
                }

                //barManager.Bars[brSkins.BarName].Visible = true;
                //barManager.Bars[brSkins.BarName].DockCol = compteur;
                //compteur++;

                //barManager.Bars[brInitialisation.BarName].Visible = true;
                //barManager.Bars[brInitialisation.BarName].DockCol = compteur;
                //compteur++;

                var frmActionsCommun = frm as IActionsCommun;
                if (frmActionsCommun != null)
                {
                    barManager.Bars[brGestion.BarName].Visible = true;
                    barManager.Bars[brGestion.BarName].DockCol = compteur;
                    compteur++;

                    btnFermer.Visibility = BarItemVisibility.Always;
                    btnActualiser.Visibility = BarItemVisibility.Always;
                    btnConfigurer.Visibility = BarItemVisibility.Always;

                    btnAjouter.Visibility = BarItemVisibility.Never;
                    btnModifier.Visibility = BarItemVisibility.Never;
                    btnBas.Visibility = BarItemVisibility.Never;
                    btnHaut.Visibility = BarItemVisibility.Never;
                    btnEnregistrer.Visibility = BarItemVisibility.Never;
                    btnEnregistrerFermer.Visibility = BarItemVisibility.Never;
                    btnSupprimer.Visibility = BarItemVisibility.Never;
                    btnRechercher.Visibility = BarItemVisibility.Never;
                    btnApercu.Visibility = BarItemVisibility.Never;

                    cmbtnActualiser.Visible = true;
                    cmbtnFermer.Visible = true;
                    cmbtnAjouter.Visible = false;
                    cmbtnImprimer.Visible = false;

                    cmbtnEnregistrer.Visible = false;
                    cmbtnEnregistrerEtFermer.Visible = false;
                    cmbtnModifier.Visible = false;
                    cmbtnFermer.Visible = false;
                    cmbtnSupprimer.Visible = false;
                }

                var frmActionsAjout = frm as IActionsAjout;
                if (frmActionsAjout != null)
                {
                    cmbtnAjouter.Visible = true;
                    btnAjouter.Visibility = BarItemVisibility.Always;
                }

                var frmActionsModification = frm as IActionsModification;
                if (frmActionsModification != null)
                {
                    cmbtnModifier.Visible = true;
                    btnModifier.Visibility = BarItemVisibility.Always;
                }

                var frmActionsSelectionnerGridRow = frm as IActionsSelectionnerGridRow;
                if (frmActionsSelectionnerGridRow != null)
                {
                    btnBas.Visibility = BarItemVisibility.Always;
                    btnHaut.Visibility = BarItemVisibility.Always;
                }

                var frmActionsSave = frm as IActionsSave;
                if (frmActionsSave != null)
                {
                    btnEnregistrer.Visibility = BarItemVisibility.Always;
                    btnEnregistrerFermer.Visibility = BarItemVisibility.Always;

                    cmbtnEnregistrer.Visible = true;
                    cmbtnEnregistrerEtFermer.Visible = true;
                    cmbtnFermer.Visible = true;
                }

                var frmActionsSuppression = frm as IActionsSuppression;
                if (frmActionsSuppression != null)
                {
                    cmbtnSupprimer.Visible = true;
                    btnSupprimer.Visibility = BarItemVisibility.Always;
                }

                var frmActionsDupliquer = frm as IActionsDupliquer;
                if (frmActionsDupliquer != null)
                {
                    barManager.Bars[brDupliquer.BarName].Visible = true;
                    barManager.Bars[brDupliquer.BarName].DockCol = compteur;
                    compteur++;
                }

                //var frmActionsSave = frm as IActionsSave;
                //if (frmActionsSave != null)
                //{
                //    barManager.Bars[brEnregistrer.BarName].Visible = true;
                //    barManager.Bars[brEnregistrer.BarName].DockCol = compteur;
                //    compteur++;

                //    cmbtnActualiser.Visible = true;
                //    btnActualiser.Visibility = BarItemVisibility.Always;
                //    cmbtnEnregistrer.Visible = true;
                //    cmbtnEnregistrerEtFermer.Visible = true;
                //    cmbtnFermer.Visible = true;
                //}

                //var frmActionsEdition = frm as IActionsEdition;
                //if (frmActionsEdition != null)
                //{
                //    barManager.Bars[brImpression.BarName].Visible = true;
                //    barManager.Bars[brImpression.BarName].DockCol = compteur;
                //    compteur++;
                //    cmbtnFermer.Visible = true;
                //}

                var frmActionsEdition = frm as IActionsEdition;
                if (frmActionsEdition != null)
                {
                    cmbtnImprimer.Visible = true;
                    btnApercu.Visibility = BarItemVisibility.Always;
                }

                var frmActionsExport = frm as IActionsExport;
                if (frmActionsExport != null)
                {
                    barManager.Bars[brExport.BarName].Visible = true;
                    barManager.Bars[brExport.BarName].DockCol = compteur;
                    compteur++;
                }

                var frmActionsImport = frm as IActionsImport;
                if (frmActionsImport != null)
                {
                    barManager.Bars[brImport.BarName].Visible = true;
                    barManager.Bars[brImport.BarName].DockCol = compteur;
                    compteur++;
                }

                var frmActionsesRechercher = frm as IActionsRechercher;
                if (frmActionsesRechercher != null)
                {
                    btnRechercher.Visibility = BarItemVisibility.Always;
                }
            }
            catch { Application.Exit(); }
        }

        private void ConfigurerToolbarSecurite(Form frm)
        {
            if (frm != null && GestionSession.SecuriteActive)
            {
                string formName;
                if (frm.GetType() == typeof(FrmAccueil))
                {
                    formName = typeof(FrmAccueil).FullName;
                }
                else
                {
                    formName = frm.GetType().FullName;
                }

                btnAjouter.Enabled = GestionUtilisateur.EstAutorise(formName, Actions.Ajouter,
                                                                    GestionSession.UtilisateurCourant);
                btnModifier.Enabled = GestionUtilisateur.EstAutorise(formName, Actions.Modifier,
                                                                     GestionSession.UtilisateurCourant);
                btnSupprimer.Enabled = GestionUtilisateur.EstAutorise(formName, Actions.Supprimer,
                                                                      GestionSession.UtilisateurCourant);
                btnApercu.Enabled = GestionUtilisateur.EstAutorise(formName, Actions.Apercu, GestionSession.UtilisateurCourant);
                btnImprimer.Enabled = GestionUtilisateur.EstAutorise(formName, Actions.Imprimer,
                                                                     GestionSession.UtilisateurCourant);

                bsiExport.Enabled = GestionUtilisateur.EstAutorise(formName, Actions.Exporter,
                                                                   GestionSession.UtilisateurCourant);

                btnDupliquer.Enabled = GestionUtilisateur.EstAutorise(formName, Actions.Consulter,
                                                                      GestionSession.UtilisateurCourant);

                btnRechercher.Enabled = GestionUtilisateur.EstAutorise(formName, Actions.Rechercher,
                                                                       GestionSession.UtilisateurCourant);

                bsiExport.Enabled = GestionUtilisateur.EstAutorise(formName, Actions.Exporter,
                                                                   GestionSession.UtilisateurCourant);

                cmbtnModifier.Enabled = btnModifier.Enabled;
                cmbtnSupprimer.Enabled = btnSupprimer.Enabled;
                cmbtnImprimer.Enabled = btnApercu.Enabled;
                cmbtnAjouter.Enabled = btnAjouter.Enabled;
                if (!btnModifier.Enabled)
                {
                    // Variable pour contrôler l'autorisation à la modification avec les double clicks B.G.N :)
                    frm.Tag = FlagSecurite.ModifDisabled;
                }

                if (!cmbtnModifier.Enabled)
                {
                    frm.Tag = FlagSecurite.ModifDisabled;
                }
            }
        }

        //private void FrmMDI_FormClosed(object sender, FormClosedEventArgs e)
        //{
        //    LookAndFeelSettings.Save("LookAndFeelSettings.dat");
        //    if (
        //        XtraMessageBox.Show("Êtes vous sûr de vouloir quitter l'application ?", Resources.NomApplication,
        //                   MessageBoxButtons.YesNo, MessageBoxIcon.Question,
        //                   MessageBoxDefaultButton.Button1) == DialogResult.No)
        //    {
        //        return;

        //    }
        //}

        private void FrmMDI_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void FrmMDI_Load(object sender, EventArgs e)
        {
            LoadForm(new FrmAccueil());
            if (_BModifyUser)
            {
                // Un utilisateur peut modifier ses coordonnées B.G.N :)
                FrmUtilisateur frm = new FrmUtilisateur(GestionSession.UtilisateurCourant.Login, true);
                frm._CRoleInitial = _CRoleInitial;
                frm.Text = Resources.Titre_frmUtilisateur + @": " + GestionSession.UtilisateurCourant.Login;
                LoadForm(frm);
            }
        }

        private void FrmMDI_MdiChildActivate(object sender, EventArgs e)
        {
            Application.DoEvents();
            Form frm = ActiveMdiChild;
            ConfigurerToolBar(frm);
            ConfigurerToolbarSecurite(frm);
        }

        private void MdiManager_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                BaseTabHitInfo hi = MdiManager.CalcHitInfo(e.Location);
                if (hi.HitTest == XtraTabHitTest.PageHeader)
                {
                    for (int i = 0; i < MdiManager.Pages.Count; i++)
                    {
                        XtraMdiTabPage page = MdiManager.Pages[i];
                        if (page == hi.Page)
                        {
                            FermerOnglet(page.MdiChild);
                            break;
                        }
                    }
                }
            }
        }

        private void MdiManager_PageRemoved(object sender, MdiTabPageEventArgs e)
        {
            //ActiverDesactiverBtnFermerDetails();
        }

        #endregion

        # region - Méthodes -

        #region Traiter Forms

        private void TraiterActionFormulaire(Object action)
        {
            if (ActiveMdiChild == null)
                return;

            #region switch par nom du controle

            if (action is BarButtonItem)
            {
                var barButtonItem = action as BarButtonItem;
                switch (barButtonItem.Name)
                {
                    case "btnEnregistrer":
                        {
                            var activeForm = ActiveMdiChild as IActionsSave;
                            if (activeForm != null) activeForm.Enregistrer(false);
                            break;
                        }
                    case "btnEnregistrerFermer":
                        {
                            var activeForm = ActiveMdiChild as IActionsSave;
                            if (activeForm != null) activeForm.Enregistrer(true);
                            break;
                        }

                    case "btnApercu":
                        {
                            var activeForm = ActiveMdiChild as IActionsEdition;
                            if (activeForm != null) activeForm.Apercu();
                            break;
                        }

                    case "btnSupprimer":
                        {
                            var activeForm = ActiveMdiChild as IActionsSuppression;
                            if (activeForm != null) activeForm.Supprimer();
                            break;
                        }

                    case "btnModifier":
                        {
                            var activeForm = ActiveMdiChild as IActionsModification;
                            if (activeForm != null) activeForm.Modifier();

                            break;
                        }
                    case "btnAjouter":
                        {
                            var activeForm = ActiveMdiChild as IActionsAjout;
                            if (activeForm != null) activeForm.Ajouter();

                            break;
                        }
                    case "btnHaut":
                        {
                            var activeForm = ActiveMdiChild as IActionsSelectionnerGridRow;
                            if (activeForm != null) activeForm.SelectionnerGridRow(true);

                            break;
                        }
                    case "btnBas":
                        {
                            var activeForm = ActiveMdiChild as IActionsSelectionnerGridRow;
                            if (activeForm != null) activeForm.SelectionnerGridRow(false);

                            break;
                        }

                    case "btnActualiser":
                        {
                            var frmActionsCommun = ActiveMdiChild as IActionsCommun;
                            frmActionsCommun.Actualiser();
                            break;
                        }
                    case "btnExportPdf":
                        {
                            var frmExport = ActiveMdiChild as IActionsExport;
                            if (frmExport != null)
                                frmExport.Exporter("PDF");

                            break;
                        }
                    case "btnExportXls":
                        {
                            var frmExport = ActiveMdiChild as IActionsExport;
                            if (frmExport != null)
                                frmExport.Exporter("XLS");
                            break;
                        }
                    case "btnExportXlsx":
                        {
                            var frmExport = ActiveMdiChild as IActionsExport;
                            if (frmExport != null)
                                frmExport.Exporter("XLSX");
                            break;
                        }
                    case "btnExportTxt":
                        {
                            var frmExport = ActiveMdiChild as IActionsExport;
                            if (frmExport != null)
                                frmExport.Exporter("TXT");
                            break;
                        }
                    case "btnImport":
                        {
                            var frmImport = ActiveMdiChild as IActionsImport;
                            if (frmImport != null)
                                frmImport.Importer();
                            break;
                        }

                    case "btnDupliquer":
                        {
                            var activeForm = ActiveMdiChild as IActionsDupliquer;
                            if (activeForm != null) activeForm.Dupliquer();
                            break;
                        }
                    case "btnRechercher":
                        {
                            var activeForm = ActiveMdiChild as IActionsRechercher;
                            if (activeForm != null) activeForm.Rechercher();
                            break;
                        }
                }
            }
            else if (action is ToolStripMenuItem)
            {
                // avec le click droit sur la grid :)
                var toolStripMenuItem = action as ToolStripMenuItem;
                switch (toolStripMenuItem.Name)
                {
                    case "cmbtnActualiser":
                        {
                            var frmActionsCommun = ActiveMdiChild as IActionsCommun;
                            frmActionsCommun.Actualiser();
                            break;
                        }
                    case "cmbtnSurPreImprimer":
                        {
                            var frmActionsCommun = ActiveMdiChild as IActionsEditionSpecifier;
                            if (frmActionsCommun != null)
                                frmActionsCommun.ApercuPreImprimer();
                            break;
                        }
                    case "cmbtnStandards":
                        {
                            var frmActionsCommun = ActiveMdiChild as IActionsEditionSpecifier;
                            if (frmActionsCommun != null)
                                frmActionsCommun.ApercuStandards();
                            break;
                        }
                    case "cmbtnRecap":
                        {
                            var frmActionsCommun = ActiveMdiChild as IActionsEditionListe;
                            if (frmActionsCommun != null)
                                frmActionsCommun.ApercuRecap();
                            break;
                        }
                    case "cmbtnEnDetail":
                        {
                            var frmActionsCommun = ActiveMdiChild as IActionsEditionListe;
                            if (frmActionsCommun != null)
                                frmActionsCommun.ApercuDetaille();
                            break;
                        }
                    case "cmbtnAjouter":
                        {
                            var frmActionsCommun = ActiveMdiChild as IActionsAjout;
                            frmActionsCommun.Ajouter();
                            break;
                        }
                    case "cmbtnModifier":
                        {
                            var activeForm = (IActionsModification)ActiveMdiChild;
                            activeForm.Modifier();

                            break;
                        }
                    case "cmbtnSupprimer":
                        {
                            var activeForm = ActiveMdiChild as IActionsSuppression;
                            activeForm.Supprimer();
                            break;
                        }
                    case "cmbtnEnregistrer":
                        {
                            var activeForm = ActiveMdiChild as IActionsSave;
                            activeForm.Enregistrer(false);
                            break;
                        }
                    case "cmbtnEnregistrerEtFermer":
                        {
                            var activeForm = ActiveMdiChild as IActionsSave;
                            activeForm.Enregistrer(true);
                            break;
                        }
                }
            }

            #endregion
        }

        #endregion

        private void FrmMDI_Resize(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.AutoScroll = true;
            }
        }

        #endregion

        private void btnConfigurer_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmSatellites frmSatellites = new FrmSatellites();
            frmSatellites.ShowDialog();
        }

        private void FrmMDI_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!BFermer)
            {
                LookAndFeelSettings.Save(LookAndFeelSettingFileName);
                if (
                    XtraMessageBox.Show("Êtes vous sûr de vouloir quitter l'application ?", Resources.NomApplication,
                               MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                               MessageBoxDefaultButton.Button1) == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void cmbtnAjouter_Click(object sender, EventArgs e)
        {
            TraiterActionFormulaire(cmbtnAjouter);
        }

        private void cmbtnSurPreImprimer_Click(object sender, EventArgs e)
        {
            TraiterActionFormulaire(cmbtnSurPreImprimer);
        }

        private void cmbtnStandards_Click(object sender, EventArgs e)
        {
            TraiterActionFormulaire(cmbtnStandards);
        }

        private void cmbtnRecap_Click(object sender, EventArgs e)
        {
            TraiterActionFormulaire(cmbtnRecap);
        }

        private void cmbtnEnDetail_Click(object sender, EventArgs e)
        {
            TraiterActionFormulaire(cmbtnEnDetail);
        }
    }
}