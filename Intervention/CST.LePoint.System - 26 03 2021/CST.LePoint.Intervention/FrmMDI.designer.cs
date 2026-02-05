using System.Windows.Forms;
namespace CST.LePoint.Intervention
{
    sealed partial class FrmMDI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            try { base.Dispose(disposing); }
            catch { Application.Exit(); }
        }


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMDI));
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.brInitialisation = new DevExpress.XtraBars.Bar();
            this.btnFermer = new DevExpress.XtraBars.BarButtonItem();
            this.btnActualiser = new DevExpress.XtraBars.BarButtonItem();
            this.btnVide = new DevExpress.XtraBars.BarButtonItem();
            this.barMenu = new DevExpress.XtraBars.Bar();
            this.barStatus = new DevExpress.XtraBars.Bar();
            this.bsiNomUtilisateur = new DevExpress.XtraBars.BarStaticItem();
            this.brGestion = new DevExpress.XtraBars.Bar();
            this.btnAjouter = new DevExpress.XtraBars.BarButtonItem();
            this.btnModifier = new DevExpress.XtraBars.BarButtonItem();
            this.btnHaut = new DevExpress.XtraBars.BarButtonItem();
            this.btnBas = new DevExpress.XtraBars.BarButtonItem();
            this.btnEnregistrer = new DevExpress.XtraBars.BarButtonItem();
            this.btnEnregistrerFermer = new DevExpress.XtraBars.BarButtonItem();
            this.btnSupprimer = new DevExpress.XtraBars.BarButtonItem();
            this.btnRechercher = new DevExpress.XtraBars.BarButtonItem();
            this.btnApercu = new DevExpress.XtraBars.BarButtonItem();
            this.btnConfigurer = new DevExpress.XtraBars.BarButtonItem();
            this.brImport = new DevExpress.XtraBars.Bar();
            this.btnImport = new DevExpress.XtraBars.BarButtonItem();
            this.brExport = new DevExpress.XtraBars.Bar();
            this.bsiExport = new DevExpress.XtraBars.BarSubItem();
            this.btnExportPdf = new DevExpress.XtraBars.BarButtonItem();
            this.btnExportXls = new DevExpress.XtraBars.BarButtonItem();
            this.btnExportXlsx = new DevExpress.XtraBars.BarButtonItem();
            this.btnExportTxt = new DevExpress.XtraBars.BarButtonItem();
            this.brDupliquer = new DevExpress.XtraBars.Bar();
            this.btnDupliquer = new DevExpress.XtraBars.BarButtonItem();
            this.brSkins = new DevExpress.XtraBars.Bar();
            this.brThemes = new DevExpress.XtraBars.BarSubItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem7 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem8 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem9 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem10 = new DevExpress.XtraBars.BarButtonItem();
            this.btnMenuUtilisateur = new DevExpress.XtraBars.BarButtonItem();
            this.btnMenuPays = new DevExpress.XtraBars.BarButtonItem();
            this.btnMenuEtat = new DevExpress.XtraBars.BarButtonItem();
            this.btnMenuDevise = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem19 = new DevExpress.XtraBars.BarButtonItem();
            this.btnFermerOnglet = new DevExpress.XtraBars.BarButtonItem();
            this.mnbtnFermer = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem7 = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.btnChangerFormat = new DevExpress.XtraBars.BarButtonItem();
            this.btnCreerFormat = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.MdiManager = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(this.components);
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmbtnActualiser = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbtnAjouter = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbtnModifier = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbtnSupprimer = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbtnEnregistrer = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbtnEnregistrerEtFermer = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbtnFermer = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbtnImprimer = new System.Windows.Forms.ToolStripMenuItem();
            this.lstbtnDocument = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbtnSurPreImprimer = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbtnStandards = new System.Windows.Forms.ToolStripMenuItem();
            this.lstbtnListe = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbtnRecap = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbtnEnDetail = new System.Windows.Forms.ToolStripMenuItem();
            this.barSubItem4 = new DevExpress.XtraBars.BarSubItem();
            this.barSubItem5 = new DevExpress.XtraBars.BarSubItem();
            this.navBarMDIForm = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarItem1 = new DevExpress.XtraNavBar.NavBarItem();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.brEnregistrer = new DevExpress.XtraBars.Bar();
            this.brSuppression = new DevExpress.XtraBars.Bar();
            this.brImpression = new DevExpress.XtraBars.Bar();
            this.btnImprimer = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MdiManager)).BeginInit();
            this.contextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarMDIForm)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager
            // 
            this.barManager.AllowCustomization = false;
            this.barManager.AllowMoveBarOnToolbar = false;
            this.barManager.AllowQuickCustomization = false;
            this.barManager.AllowShowToolbarsPopup = false;
            this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.brInitialisation,
            this.barMenu,
            this.barStatus,
            this.brGestion,
            this.brImport,
            this.brExport,
            this.brDupliquer,
            this.brSkins});
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItem5,
            this.barButtonItem6,
            this.barButtonItem7,
            this.barButtonItem8,
            this.barButtonItem9,
            this.barButtonItem10,
            this.btnMenuUtilisateur,
            this.btnMenuPays,
            this.btnMenuEtat,
            this.btnMenuDevise,
            this.btnActualiser,
            this.btnAjouter,
            this.btnModifier,
            this.btnSupprimer,
            this.btnHaut,
            this.btnBas,
            this.btnEnregistrer,
            this.btnEnregistrerFermer,
            this.btnApercu,
            this.btnImport,
            this.barButtonItem19,
            this.btnFermerOnglet,
            this.btnFermer,
            this.bsiExport,
            this.btnExportPdf,
            this.btnExportXls,
            this.btnExportXlsx,
            this.btnExportTxt,
            this.mnbtnFermer,
            this.barButtonItem1,
            this.barSubItem7,
            this.barButtonItem2,
            this.btnVide,
            this.barButtonItem4,
            this.btnChangerFormat,
            this.btnCreerFormat,
            this.btnDupliquer,
            this.btnRechercher,
            this.bsiNomUtilisateur,
            this.brThemes,
            this.btnConfigurer,
            this.barButtonItem3});
            this.barManager.MainMenu = this.barMenu;
            this.barManager.MaxItemId = 64;
            this.barManager.StatusBar = this.barStatus;
            // 
            // brInitialisation
            // 
            this.brInitialisation.BarName = "Actualiser";
            this.brInitialisation.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Top;
            this.brInitialisation.DockCol = 1;
            this.brInitialisation.DockRow = 1;
            this.brInitialisation.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.brInitialisation.FloatLocation = new System.Drawing.Point(252, 189);
            this.brInitialisation.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnFermer),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnActualiser, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnVide)});
            this.brInitialisation.OptionsBar.AllowQuickCustomization = false;
            this.brInitialisation.Text = "Actualiser";
            // 
            // btnFermer
            // 
            this.btnFermer.Caption = "Fermer";
            this.btnFermer.Glyph = ((System.Drawing.Image)(resources.GetObject("btnFermer.Glyph")));
            this.btnFermer.Id = 34;
            this.btnFermer.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4));
            this.btnFermer.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btnFermer.LargeGlyph")));
            this.btnFermer.Name = "btnFermer";
            this.btnFermer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnFermer_ItemClick);
            // 
            // btnActualiser
            // 
            this.btnActualiser.Caption = "Actualiser";
            this.btnActualiser.Glyph = ((System.Drawing.Image)(resources.GetObject("btnActualiser.Glyph")));
            this.btnActualiser.Id = 17;
            this.btnActualiser.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5);
            this.btnActualiser.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btnActualiser.LargeGlyph")));
            this.btnActualiser.Name = "btnActualiser";
            this.btnActualiser.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnActualiser_ItemClick);
            // 
            // btnVide
            // 
            this.btnVide.Id = 50;
            this.btnVide.Name = "btnVide";
            // 
            // barMenu
            // 
            this.barMenu.BarAppearance.Disabled.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.barMenu.BarAppearance.Disabled.Options.UseFont = true;
            this.barMenu.BarAppearance.Hovered.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.barMenu.BarAppearance.Hovered.Options.UseFont = true;
            this.barMenu.BarAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.barMenu.BarAppearance.Normal.Options.UseFont = true;
            this.barMenu.BarAppearance.Pressed.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.barMenu.BarAppearance.Pressed.Options.UseFont = true;
            this.barMenu.BarName = "Main menu";
            this.barMenu.DockCol = 0;
            this.barMenu.DockRow = 0;
            this.barMenu.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barMenu.OptionsBar.MultiLine = true;
            this.barMenu.OptionsBar.UseWholeRow = true;
            this.barMenu.Text = "Main menu";
            // 
            // barStatus
            // 
            this.barStatus.BarName = "Status bar";
            this.barStatus.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.barStatus.DockCol = 0;
            this.barStatus.DockRow = 0;
            this.barStatus.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.barStatus.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bsiNomUtilisateur)});
            this.barStatus.OptionsBar.AllowQuickCustomization = false;
            this.barStatus.OptionsBar.DrawDragBorder = false;
            this.barStatus.OptionsBar.UseWholeRow = true;
            this.barStatus.Text = "Status bar";
            // 
            // bsiNomUtilisateur
            // 
            this.bsiNomUtilisateur.Caption = "barStaticItem1";
            this.bsiNomUtilisateur.Id = 57;
            this.bsiNomUtilisateur.Name = "bsiNomUtilisateur";
            this.bsiNomUtilisateur.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // brGestion
            // 
            this.brGestion.BarName = "Gestion";
            this.brGestion.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Top;
            this.brGestion.DockCol = 2;
            this.brGestion.DockRow = 1;
            this.brGestion.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.brGestion.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnAjouter),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnModifier),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnHaut, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnBas),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnEnregistrer, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnEnregistrerFermer),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSupprimer, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnRechercher, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnApercu, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnConfigurer, true)});
            this.brGestion.OptionsBar.AllowQuickCustomization = false;
            this.brGestion.Text = "Gestion";
            // 
            // btnAjouter
            // 
            this.btnAjouter.Caption = "Ajouter";
            this.btnAjouter.Glyph = ((System.Drawing.Image)(resources.GetObject("btnAjouter.Glyph")));
            this.btnAjouter.Id = 18;
            this.btnAjouter.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N));
            this.btnAjouter.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btnAjouter.LargeGlyph")));
            this.btnAjouter.Name = "btnAjouter";
            this.btnAjouter.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAjouter_ItemClick);
            // 
            // btnModifier
            // 
            this.btnModifier.Caption = "Modifier";
            this.btnModifier.Glyph = ((System.Drawing.Image)(resources.GetObject("btnModifier.Glyph")));
            this.btnModifier.Id = 19;
            this.btnModifier.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btnModifier.LargeGlyph")));
            this.btnModifier.Name = "btnModifier";
            this.btnModifier.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnModifier_ItemClick);
            // 
            // btnHaut
            // 
            this.btnHaut.Caption = "Haut";
            this.btnHaut.Glyph = ((System.Drawing.Image)(resources.GetObject("btnHaut.Glyph")));
            this.btnHaut.Id = 22;
            this.btnHaut.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btnHaut.LargeGlyph")));
            this.btnHaut.Name = "btnHaut";
            this.btnHaut.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnHaut_ItemClick);
            // 
            // btnBas
            // 
            this.btnBas.Caption = "Bas";
            this.btnBas.Glyph = ((System.Drawing.Image)(resources.GetObject("btnBas.Glyph")));
            this.btnBas.Id = 23;
            this.btnBas.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btnBas.LargeGlyph")));
            this.btnBas.Name = "btnBas";
            this.btnBas.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBas_ItemClick);
            // 
            // btnEnregistrer
            // 
            this.btnEnregistrer.Caption = "Enregistrer";
            this.btnEnregistrer.Glyph = ((System.Drawing.Image)(resources.GetObject("btnEnregistrer.Glyph")));
            this.btnEnregistrer.Id = 24;
            this.btnEnregistrer.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S));
            this.btnEnregistrer.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btnEnregistrer.LargeGlyph")));
            this.btnEnregistrer.Name = "btnEnregistrer";
            this.btnEnregistrer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEnregistrer_ItemClick);
            // 
            // btnEnregistrerFermer
            // 
            this.btnEnregistrerFermer.Caption = "Enregistrer et Fermer";
            this.btnEnregistrerFermer.Glyph = ((System.Drawing.Image)(resources.GetObject("btnEnregistrerFermer.Glyph")));
            this.btnEnregistrerFermer.Id = 25;
            this.btnEnregistrerFermer.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btnEnregistrerFermer.LargeGlyph")));
            this.btnEnregistrerFermer.Name = "btnEnregistrerFermer";
            this.btnEnregistrerFermer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEnregistrerFermer_ItemClick);
            // 
            // btnSupprimer
            // 
            this.btnSupprimer.Caption = "Supprimer";
            this.btnSupprimer.Glyph = ((System.Drawing.Image)(resources.GetObject("btnSupprimer.Glyph")));
            this.btnSupprimer.Id = 20;
            this.btnSupprimer.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.C));
            this.btnSupprimer.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btnSupprimer.LargeGlyph")));
            this.btnSupprimer.Name = "btnSupprimer";
            this.btnSupprimer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSupprimer_ItemClick);
            // 
            // btnRechercher
            // 
            this.btnRechercher.Caption = "Rechercher";
            this.btnRechercher.Glyph = ((System.Drawing.Image)(resources.GetObject("btnRechercher.Glyph")));
            this.btnRechercher.Id = 56;
            this.btnRechercher.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R));
            this.btnRechercher.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btnRechercher.LargeGlyph")));
            this.btnRechercher.Name = "btnRechercher";
            this.btnRechercher.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnRechercher_ItemClick);
            // 
            // btnApercu
            // 
            this.btnApercu.Caption = "Aperçu";
            this.btnApercu.Glyph = ((System.Drawing.Image)(resources.GetObject("btnApercu.Glyph")));
            this.btnApercu.Id = 26;
            this.btnApercu.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P));
            this.btnApercu.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btnApercu.LargeGlyph")));
            this.btnApercu.Name = "btnApercu";
            this.btnApercu.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnApercu_ItemClick);
            // 
            // btnConfigurer
            // 
            this.btnConfigurer.Caption = "Satellites";
            this.btnConfigurer.Glyph = ((System.Drawing.Image)(resources.GetObject("btnConfigurer.Glyph")));
            this.btnConfigurer.Id = 62;
            this.btnConfigurer.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btnConfigurer.LargeGlyph")));
            this.btnConfigurer.Name = "btnConfigurer";
            this.btnConfigurer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnConfigurer_ItemClick);
            // 
            // brImport
            // 
            this.brImport.BarName = "brImport";
            this.brImport.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Top;
            this.brImport.DockCol = 3;
            this.brImport.DockRow = 1;
            this.brImport.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.brImport.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnImport, DevExpress.XtraBars.BarItemPaintStyle.Standard)});
            this.brImport.OptionsBar.AllowQuickCustomization = false;
            this.brImport.Text = "Import";
            // 
            // btnImport
            // 
            this.btnImport.Caption = "Importer";
            this.btnImport.Glyph = ((System.Drawing.Image)(resources.GetObject("btnImport.Glyph")));
            this.btnImport.Id = 28;
            this.btnImport.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btnImport.LargeGlyph")));
            this.btnImport.Name = "btnImport";
            this.btnImport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnImport_ItemClick);
            // 
            // brExport
            // 
            this.brExport.BarName = "brExport";
            this.brExport.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Top;
            this.brExport.DockCol = 4;
            this.brExport.DockRow = 1;
            this.brExport.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.brExport.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bsiExport, DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu)});
            this.brExport.OptionsBar.AllowQuickCustomization = false;
            this.brExport.Text = "brExport";
            // 
            // bsiExport
            // 
            this.bsiExport.Caption = "Exporter";
            this.bsiExport.Glyph = ((System.Drawing.Image)(resources.GetObject("bsiExport.Glyph")));
            this.bsiExport.Id = 35;
            this.bsiExport.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("bsiExport.LargeGlyph")));
            this.bsiExport.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnExportPdf),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnExportXls),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnExportXlsx),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnExportTxt)});
            this.bsiExport.Name = "bsiExport";
            // 
            // btnExportPdf
            // 
            this.btnExportPdf.Caption = "Exporter en PDF";
            this.btnExportPdf.Glyph = ((System.Drawing.Image)(resources.GetObject("btnExportPdf.Glyph")));
            this.btnExportPdf.Id = 36;
            this.btnExportPdf.Name = "btnExportPdf";
            this.btnExportPdf.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnExportPdf_ItemClick);
            // 
            // btnExportXls
            // 
            this.btnExportXls.Caption = "Exporter en XLS";
            this.btnExportXls.Glyph = ((System.Drawing.Image)(resources.GetObject("btnExportXls.Glyph")));
            this.btnExportXls.Id = 37;
            this.btnExportXls.Name = "btnExportXls";
            this.btnExportXls.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnExportXls_ItemClick);
            // 
            // btnExportXlsx
            // 
            this.btnExportXlsx.Caption = "Exporter en XLSX";
            this.btnExportXlsx.Glyph = ((System.Drawing.Image)(resources.GetObject("btnExportXlsx.Glyph")));
            this.btnExportXlsx.Id = 38;
            this.btnExportXlsx.Name = "btnExportXlsx";
            this.btnExportXlsx.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnExportXlsx_ItemClick);
            // 
            // btnExportTxt
            // 
            this.btnExportTxt.Caption = "Exporter en TXT";
            this.btnExportTxt.Glyph = ((System.Drawing.Image)(resources.GetObject("btnExportTxt.Glyph")));
            this.btnExportTxt.Id = 39;
            this.btnExportTxt.Name = "btnExportTxt";
            this.btnExportTxt.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnExportTxt_ItemClick);
            // 
            // brDupliquer
            // 
            this.brDupliquer.BarName = "Custom 12";
            this.brDupliquer.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Top;
            this.brDupliquer.DockCol = 5;
            this.brDupliquer.DockRow = 1;
            this.brDupliquer.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.brDupliquer.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnDupliquer)});
            this.brDupliquer.OptionsBar.AllowQuickCustomization = false;
            this.brDupliquer.Text = "Dupliquer";
            // 
            // btnDupliquer
            // 
            this.btnDupliquer.Caption = "Dupliquer";
            this.btnDupliquer.Glyph = ((System.Drawing.Image)(resources.GetObject("btnDupliquer.Glyph")));
            this.btnDupliquer.Id = 55;
            this.btnDupliquer.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btnDupliquer.LargeGlyph")));
            this.btnDupliquer.Name = "btnDupliquer";
            this.btnDupliquer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDupliquer_ItemClick);
            // 
            // brSkins
            // 
            this.brSkins.BarName = "Skins";
            this.brSkins.DockCol = 0;
            this.brSkins.DockRow = 1;
            this.brSkins.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.brSkins.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.brThemes)});
            this.brSkins.Text = "Skins";
            // 
            // brThemes
            // 
            this.brThemes.Caption = "Thèmes";
            this.brThemes.Id = 61;
            this.brThemes.Name = "brThemes";
            this.brThemes.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.brThemes.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.brThemes_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1019, 51);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 681);
            this.barDockControlBottom.Size = new System.Drawing.Size(1019, 25);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 51);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 630);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1019, 51);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 630);
            // 
            // barButtonItem5
            // 
            this.barButtonItem5.Caption = "Configuration";
            this.barButtonItem5.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem5.Glyph")));
            this.barButtonItem5.Id = 7;
            this.barButtonItem5.Name = "barButtonItem5";
            // 
            // barButtonItem6
            // 
            this.barButtonItem6.Caption = "Importation Commande";
            this.barButtonItem6.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem6.Glyph")));
            this.barButtonItem6.Id = 8;
            this.barButtonItem6.Name = "barButtonItem6";
            // 
            // barButtonItem7
            // 
            this.barButtonItem7.Caption = "Modification Format";
            this.barButtonItem7.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem7.Glyph")));
            this.barButtonItem7.Id = 9;
            this.barButtonItem7.Name = "barButtonItem7";
            // 
            // barButtonItem8
            // 
            this.barButtonItem8.Caption = "Impression Commande";
            this.barButtonItem8.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem8.Glyph")));
            this.barButtonItem8.Id = 10;
            this.barButtonItem8.Name = "barButtonItem8";
            // 
            // barButtonItem9
            // 
            this.barButtonItem9.Caption = "Chèque";
            this.barButtonItem9.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem9.Glyph")));
            this.barButtonItem9.Id = 11;
            this.barButtonItem9.Name = "barButtonItem9";
            // 
            // barButtonItem10
            // 
            this.barButtonItem10.Caption = "Accusé/Rib/Intercalaire";
            this.barButtonItem10.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem10.Glyph")));
            this.barButtonItem10.Id = 12;
            this.barButtonItem10.Name = "barButtonItem10";
            // 
            // btnMenuUtilisateur
            // 
            this.btnMenuUtilisateur.Caption = "Utilisateur";
            this.btnMenuUtilisateur.Glyph = ((System.Drawing.Image)(resources.GetObject("btnMenuUtilisateur.Glyph")));
            this.btnMenuUtilisateur.Id = 13;
            this.btnMenuUtilisateur.Name = "btnMenuUtilisateur";
            // 
            // btnMenuPays
            // 
            this.btnMenuPays.Caption = "Pays";
            this.btnMenuPays.Glyph = ((System.Drawing.Image)(resources.GetObject("btnMenuPays.Glyph")));
            this.btnMenuPays.Id = 14;
            this.btnMenuPays.Name = "btnMenuPays";
            // 
            // btnMenuEtat
            // 
            this.btnMenuEtat.Caption = "Etat";
            this.btnMenuEtat.Glyph = ((System.Drawing.Image)(resources.GetObject("btnMenuEtat.Glyph")));
            this.btnMenuEtat.Id = 15;
            this.btnMenuEtat.Name = "btnMenuEtat";
            // 
            // btnMenuDevise
            // 
            this.btnMenuDevise.Caption = "Devise";
            this.btnMenuDevise.Glyph = ((System.Drawing.Image)(resources.GetObject("btnMenuDevise.Glyph")));
            this.btnMenuDevise.Id = 16;
            this.btnMenuDevise.Name = "btnMenuDevise";
            // 
            // barButtonItem19
            // 
            this.barButtonItem19.Caption = "Configurer";
            this.barButtonItem19.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem19.Glyph")));
            this.barButtonItem19.Id = 30;
            this.barButtonItem19.Name = "barButtonItem19";
            // 
            // btnFermerOnglet
            // 
            this.btnFermerOnglet.Caption = "Fermer";
            this.btnFermerOnglet.Glyph = ((System.Drawing.Image)(resources.GetObject("btnFermerOnglet.Glyph")));
            this.btnFermerOnglet.Id = 32;
            this.btnFermerOnglet.Name = "btnFermerOnglet";
            // 
            // mnbtnFermer
            // 
            this.mnbtnFermer.Caption = "Fermer";
            this.mnbtnFermer.Glyph = ((System.Drawing.Image)(resources.GetObject("mnbtnFermer.Glyph")));
            this.mnbtnFermer.Id = 43;
            this.mnbtnFermer.Name = "mnbtnFermer";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Quitter";
            this.barButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem1.Glyph")));
            this.barButtonItem1.Id = 45;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // barSubItem7
            // 
            this.barSubItem7.Caption = "?";
            this.barSubItem7.Id = 47;
            this.barSubItem7.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem2)});
            this.barSubItem7.Name = "barSubItem7";
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "A Propos de Yasmine";
            this.barButtonItem2.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem2.Glyph")));
            this.barButtonItem2.Id = 48;
            this.barButtonItem2.Name = "barButtonItem2";
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Id = 52;
            this.barButtonItem4.Name = "barButtonItem4";
            // 
            // btnChangerFormat
            // 
            this.btnChangerFormat.Caption = "Charger Format";
            this.btnChangerFormat.Glyph = global::CST.LePoint.Intervention.Properties.Resources.Menu_ModifierFormat_18;
            this.btnChangerFormat.Id = 53;
            this.btnChangerFormat.Name = "btnChangerFormat";
            this.btnChangerFormat.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnChangerFormat_ItemClick);
            // 
            // btnCreerFormat
            // 
            this.btnCreerFormat.Caption = "Créer Format";
            this.btnCreerFormat.Glyph = global::CST.LePoint.Intervention.Properties.Resources.Menu_CreerFormat_18;
            this.btnCreerFormat.Id = 54;
            this.btnCreerFormat.Name = "btnCreerFormat";
            this.btnCreerFormat.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCreerFormat_ItemClick);
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "Rechercher";
            this.barButtonItem3.Id = 63;
            this.barButtonItem3.Name = "barButtonItem3";
            // 
            // MdiManager
            // 
            this.MdiManager.CloseTabOnMiddleClick = DevExpress.XtraTabbedMdi.CloseTabOnMiddleClick.Never;
            this.MdiManager.MdiParent = this;
            this.MdiManager.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MdiManager_MouseDown);
            this.MdiManager.PageRemoved += new DevExpress.XtraTabbedMdi.MdiTabPageEventHandler(this.MdiManager_PageRemoved);
            // 
            // contextMenu
            // 
            this.contextMenu.AllowDrop = true;
            this.contextMenu.ImeMode = System.Windows.Forms.ImeMode.On;
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmbtnActualiser,
            this.cmbtnAjouter,
            this.cmbtnModifier,
            this.cmbtnSupprimer,
            this.cmbtnEnregistrer,
            this.cmbtnEnregistrerEtFermer,
            this.cmbtnFermer,
            this.cmbtnImprimer});
            this.contextMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.contextMenu.Name = "contextMenuStrip1";
            this.contextMenu.ShowCheckMargin = true;
            this.contextMenu.Size = new System.Drawing.Size(206, 180);
            // 
            // cmbtnActualiser
            // 
            this.cmbtnActualiser.Image = ((System.Drawing.Image)(resources.GetObject("cmbtnActualiser.Image")));
            this.cmbtnActualiser.Name = "cmbtnActualiser";
            this.cmbtnActualiser.Size = new System.Drawing.Size(205, 22);
            this.cmbtnActualiser.Text = "Actualiser";
            this.cmbtnActualiser.Click += new System.EventHandler(this.cmbtnActualiser_Click);
            // 
            // cmbtnAjouter
            // 
            this.cmbtnAjouter.Image = global::CST.LePoint.Intervention.Properties.Resources.Menu_Add;
            this.cmbtnAjouter.Name = "cmbtnAjouter";
            this.cmbtnAjouter.Size = new System.Drawing.Size(205, 22);
            this.cmbtnAjouter.Text = "Ajouter";
            this.cmbtnAjouter.Click += new System.EventHandler(this.cmbtnAjouter_Click);
            // 
            // cmbtnModifier
            // 
            this.cmbtnModifier.Image = ((System.Drawing.Image)(resources.GetObject("cmbtnModifier.Image")));
            this.cmbtnModifier.Name = "cmbtnModifier";
            this.cmbtnModifier.Size = new System.Drawing.Size(205, 22);
            this.cmbtnModifier.Text = "Modifier";
            this.cmbtnModifier.Click += new System.EventHandler(this.cmbtnModifier_Click);
            // 
            // cmbtnSupprimer
            // 
            this.cmbtnSupprimer.Image = ((System.Drawing.Image)(resources.GetObject("cmbtnSupprimer.Image")));
            this.cmbtnSupprimer.Name = "cmbtnSupprimer";
            this.cmbtnSupprimer.Size = new System.Drawing.Size(205, 22);
            this.cmbtnSupprimer.Text = "Supprimer";
            this.cmbtnSupprimer.Click += new System.EventHandler(this.cmbtnSupprimer_Click);
            // 
            // cmbtnEnregistrer
            // 
            this.cmbtnEnregistrer.Image = ((System.Drawing.Image)(resources.GetObject("cmbtnEnregistrer.Image")));
            this.cmbtnEnregistrer.Name = "cmbtnEnregistrer";
            this.cmbtnEnregistrer.Size = new System.Drawing.Size(205, 22);
            this.cmbtnEnregistrer.Text = "Enregistrer";
            this.cmbtnEnregistrer.Click += new System.EventHandler(this.cmbtnEnregistrer_Click);
            // 
            // cmbtnEnregistrerEtFermer
            // 
            this.cmbtnEnregistrerEtFermer.Image = ((System.Drawing.Image)(resources.GetObject("cmbtnEnregistrerEtFermer.Image")));
            this.cmbtnEnregistrerEtFermer.Name = "cmbtnEnregistrerEtFermer";
            this.cmbtnEnregistrerEtFermer.Size = new System.Drawing.Size(205, 22);
            this.cmbtnEnregistrerEtFermer.Text = "Enregistrer et Fermer";
            this.cmbtnEnregistrerEtFermer.Click += new System.EventHandler(this.cmbtnEnregistrerEtFermer_Click);
            // 
            // cmbtnFermer
            // 
            this.cmbtnFermer.Image = ((System.Drawing.Image)(resources.GetObject("cmbtnFermer.Image")));
            this.cmbtnFermer.Name = "cmbtnFermer";
            this.cmbtnFermer.Size = new System.Drawing.Size(205, 22);
            this.cmbtnFermer.Text = "Fermer";
            this.cmbtnFermer.Click += new System.EventHandler(this.cmbtnFermer_Click);
            // 
            // cmbtnImprimer
            // 
            this.cmbtnImprimer.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lstbtnDocument,
            this.lstbtnListe});
            this.cmbtnImprimer.Image = global::CST.LePoint.Intervention.Properties.Resources.Menu_Print;
            this.cmbtnImprimer.Name = "cmbtnImprimer";
            this.cmbtnImprimer.Size = new System.Drawing.Size(205, 22);
            this.cmbtnImprimer.Text = "Imprimer";
            // 
            // lstbtnDocument
            // 
            this.lstbtnDocument.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmbtnSurPreImprimer,
            this.cmbtnStandards});
            this.lstbtnDocument.Name = "lstbtnDocument";
            this.lstbtnDocument.Size = new System.Drawing.Size(130, 22);
            this.lstbtnDocument.Text = "Document";
            // 
            // cmbtnSurPreImprimer
            // 
            this.cmbtnSurPreImprimer.Name = "cmbtnSurPreImprimer";
            this.cmbtnSurPreImprimer.Size = new System.Drawing.Size(161, 22);
            this.cmbtnSurPreImprimer.Text = "Sur Pré-Imprimé";
            this.cmbtnSurPreImprimer.Click += new System.EventHandler(this.cmbtnSurPreImprimer_Click);
            // 
            // cmbtnStandards
            // 
            this.cmbtnStandards.Name = "cmbtnStandards";
            this.cmbtnStandards.Size = new System.Drawing.Size(161, 22);
            this.cmbtnStandards.Text = "Standards";
            this.cmbtnStandards.Click += new System.EventHandler(this.cmbtnStandards_Click);
            // 
            // lstbtnListe
            // 
            this.lstbtnListe.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmbtnRecap,
            this.cmbtnEnDetail});
            this.lstbtnListe.Name = "lstbtnListe";
            this.lstbtnListe.Size = new System.Drawing.Size(130, 22);
            this.lstbtnListe.Text = "Liste";
            // 
            // cmbtnRecap
            // 
            this.cmbtnRecap.Name = "cmbtnRecap";
            this.cmbtnRecap.Size = new System.Drawing.Size(120, 22);
            this.cmbtnRecap.Text = "Récap";
            this.cmbtnRecap.Click += new System.EventHandler(this.cmbtnRecap_Click);
            // 
            // cmbtnEnDetail
            // 
            this.cmbtnEnDetail.Name = "cmbtnEnDetail";
            this.cmbtnEnDetail.Size = new System.Drawing.Size(120, 22);
            this.cmbtnEnDetail.Text = "En Détail";
            this.cmbtnEnDetail.Click += new System.EventHandler(this.cmbtnEnDetail_Click);
            // 
            // barSubItem4
            // 
            this.barSubItem4.Id = -1;
            this.barSubItem4.Name = "barSubItem4";
            // 
            // barSubItem5
            // 
            this.barSubItem5.Id = -1;
            this.barSubItem5.Name = "barSubItem5";
            // 
            // navBarMDIForm
            // 
            this.navBarMDIForm.ActiveGroup = null;
            this.navBarMDIForm.Dock = System.Windows.Forms.DockStyle.Left;
            this.navBarMDIForm.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.navBarItem1});
            this.navBarMDIForm.Location = new System.Drawing.Point(0, 51);
            this.navBarMDIForm.Name = "navBarMDIForm";
            this.navBarMDIForm.OptionsNavPane.ExpandedWidth = 163;
            this.navBarMDIForm.Size = new System.Drawing.Size(163, 630);
            this.navBarMDIForm.StoreDefaultPaintStyleName = true;
            this.navBarMDIForm.TabIndex = 4;
            this.navBarMDIForm.Text = "navBarControl1";
            // 
            // navBarItem1
            // 
            this.navBarItem1.Caption = "navBarItem1";
            this.navBarItem1.Name = "navBarItem1";
            // 
            // splitterControl1
            // 
            this.splitterControl1.Location = new System.Drawing.Point(163, 51);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(5, 630);
            this.splitterControl1.TabIndex = 9;
            this.splitterControl1.TabStop = false;
            // 
            // brEnregistrer
            // 
            this.brEnregistrer.BarName = "Enregistrement";
            this.brEnregistrer.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Top;
            this.brEnregistrer.DockCol = 4;
            this.brEnregistrer.DockRow = 1;
            this.brEnregistrer.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.brEnregistrer.OptionsBar.AllowQuickCustomization = false;
            this.brEnregistrer.Text = "Enregistrement";
            // 
            // brSuppression
            // 
            this.brSuppression.BarName = "Suppression";
            this.brSuppression.DockCol = 5;
            this.brSuppression.DockRow = 1;
            this.brSuppression.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.brSuppression.OptionsBar.AllowRename = true;
            this.brSuppression.Text = "Suppression";
            // 
            // brImpression
            // 
            this.brImpression.BarName = "Impression";
            this.brImpression.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Top;
            this.brImpression.DockCol = 6;
            this.brImpression.DockRow = 1;
            this.brImpression.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.brImpression.OptionsBar.AllowQuickCustomization = false;
            this.brImpression.Text = "Impression";
            // 
            // btnImprimer
            // 
            this.btnImprimer.Caption = "Imprimer";
            this.btnImprimer.Glyph = ((System.Drawing.Image)(resources.GetObject("btnImprimer.Glyph")));
            this.btnImprimer.Id = 27;
            this.btnImprimer.Name = "btnImprimer";
            this.btnImprimer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnImprimer_ItemClick);
            // 
            // FrmMDI
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1019, 706);
            this.Controls.Add(this.splitterControl1);
            this.Controls.Add(this.navBarMDIForm);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(608, 411);
            this.Name = "FrmMDI";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMDI_FormClosing);
            this.Load += new System.EventHandler(this.FrmMDI_Load);
            this.MdiChildActivate += new System.EventHandler(this.FrmMDI_MdiChildActivate);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmMDI_KeyPress);
            this.Resize += new System.EventHandler(this.FrmMDI_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MdiManager)).EndInit();
            this.contextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarMDIForm)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.Bar brInitialisation;
        private DevExpress.XtraBars.Bar barMenu;
        private DevExpress.XtraBars.Bar barStatus;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barButtonItem5;
        private DevExpress.XtraBars.BarButtonItem barButtonItem6;
        private DevExpress.XtraBars.BarButtonItem barButtonItem7;
        private DevExpress.XtraBars.BarButtonItem barButtonItem8;
        private DevExpress.XtraBars.BarButtonItem barButtonItem9;
        private DevExpress.XtraBars.BarButtonItem barButtonItem10;
        private DevExpress.XtraBars.BarButtonItem btnMenuUtilisateur;
        private DevExpress.XtraBars.BarButtonItem btnMenuPays;
        private DevExpress.XtraBars.BarButtonItem btnMenuEtat;
        private DevExpress.XtraBars.BarButtonItem btnMenuDevise;
        private DevExpress.XtraBars.BarButtonItem btnActualiser;
        private DevExpress.XtraBars.Bar brGestion;
        private DevExpress.XtraBars.Bar brImport;
        private DevExpress.XtraBars.BarButtonItem barButtonItem19;

        private DevExpress.XtraBars.BarButtonItem btnEnregistrer;
        private DevExpress.XtraBars.BarButtonItem btnEnregistrerFermer;
        private DevExpress.XtraBars.BarButtonItem btnImport;
        private DevExpress.XtraBars.BarButtonItem btnApercu;
        private DevExpress.XtraBars.BarButtonItem btnHaut;
        private DevExpress.XtraBars.BarButtonItem btnBas;
        private DevExpress.XtraBars.BarButtonItem btnAjouter;
        private DevExpress.XtraBars.BarButtonItem btnModifier;

        //private DevExpress.XtraBars.BarButtonItem btnMenuBanque;
        //private DevExpress.XtraBars.BarButtonItem btnMenuClient;
        //private DevExpress.XtraBars.BarButtonItem btnMenuAgence;
        //private DevExpress.XtraBars.BarButtonItem btnMenuCommande;

        private DevExpress.XtraBars.BarButtonItem btnExportPdf;
        private DevExpress.XtraBars.BarButtonItem btnExportXls;
        private DevExpress.XtraBars.BarButtonItem btnExportXlsx;
        private DevExpress.XtraBars.BarButtonItem btnExportTxt;
        private DevExpress.XtraBars.BarButtonItem btnFermerOnglet;
        private DevExpress.XtraBars.BarButtonItem btnVide;
        private DevExpress.XtraBars.BarButtonItem btnFermer;
        private DevExpress.XtraBars.BarSubItem bsiExport;
        private DevExpress.XtraBars.BarButtonItem mnbtnFermer;

        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;


        public System.Windows.Forms.ContextMenuStrip contextMenu;
        public System.Windows.Forms.ToolStripMenuItem cmbtnActualiser;
        public System.Windows.Forms.ToolStripMenuItem cmbtnModifier;
        public System.Windows.Forms.ToolStripMenuItem cmbtnSupprimer;
        public System.Windows.Forms.ToolStripMenuItem cmbtnEnregistrer;
        public System.Windows.Forms.ToolStripMenuItem cmbtnEnregistrerEtFermer;
        public System.Windows.Forms.ToolStripMenuItem cmbtnFermer;


        public DevExpress.XtraNavBar.NavBarControl navBarMDIForm;
        private DevExpress.XtraBars.BarSubItem barSubItem4;
        private DevExpress.XtraBars.BarSubItem barSubItem5;
        private DevExpress.XtraBars.BarSubItem barSubItem7;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;

        public DevExpress.XtraTabbedMdi.XtraTabbedMdiManager MdiManager;
        private DevExpress.XtraNavBar.NavBarItem navBarItem1;
        private DevExpress.XtraBars.Bar brExport;
        private DevExpress.XtraBars.BarButtonItem btnChangerFormat;
        private DevExpress.XtraBars.BarButtonItem btnCreerFormat;
        public DevExpress.XtraBars.BarButtonItem btnSupprimer;
        private DevExpress.XtraBars.Bar brDupliquer;
        private DevExpress.XtraBars.BarButtonItem btnDupliquer;
        public DevExpress.XtraBars.BarButtonItem btnRechercher;
        private DevExpress.XtraBars.BarStaticItem bsiNomUtilisateur;


        private DevExpress.XtraBars.Bar brSkins;
        private DevExpress.XtraBars.BarSubItem brThemes;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private DevExpress.XtraBars.BarButtonItem btnConfigurer;
        private DevExpress.XtraBars.Bar brEnregistrer;
        private DevExpress.XtraBars.Bar brSuppression;
        private DevExpress.XtraBars.Bar brImpression;
        private DevExpress.XtraBars.BarButtonItem btnImprimer;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private ToolStripMenuItem cmbtnAjouter;
        private ToolStripMenuItem cmbtnImprimer;
        private ToolStripMenuItem lstbtnDocument;
        private ToolStripMenuItem lstbtnListe;
        private ToolStripMenuItem cmbtnRecap;
        private ToolStripMenuItem cmbtnEnDetail;
        private ToolStripMenuItem cmbtnSurPreImprimer;
        private ToolStripMenuItem cmbtnStandards;


    }
}
