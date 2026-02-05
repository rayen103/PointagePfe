using System.Windows.Forms;
namespace CST.LePoint.Intervention.Tiers
{
    partial class FrmClient
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
             try { base.Dispose(disposing); }catch{ Application.Exit();}
        }


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmClient));
            this.PageClient = new DevExpress.XtraTab.XtraTabControl();
            this.tabInfo = new DevExpress.XtraTab.XtraTabPage();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl20 = new DevExpress.XtraEditors.LabelControl();
            this.lkpCPaysLivraison = new DevExpress.XtraEditors.LookUpEdit();
            this.txtLibAdresseLiv = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl17 = new DevExpress.XtraEditors.LabelControl();
            this.txtCPostalLiv = new DevExpress.XtraEditors.TextEdit();
            this.txtVilleAdresseLiv = new DevExpress.XtraEditors.TextEdit();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.txtLibAdresseFac = new DevExpress.XtraEditors.MemoEdit();
            this.chkAdresseLivraison = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtCPostalFac = new DevExpress.XtraEditors.TextEdit();
            this.txtVilleAdresseFac = new DevExpress.XtraEditors.TextEdit();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.lkpCPaysFacturation = new DevExpress.XtraEditors.LookUpEdit();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.txtMdp = new DevExpress.XtraEditors.TextEdit();
            this.labelControl45 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl44 = new DevExpress.XtraEditors.LabelControl();
            this.bttapercu = new DevExpress.XtraEditors.SimpleButton();
            this.bttparcourir = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl36 = new DevExpress.XtraEditors.LabelControl();
            this.txtemplacement = new DevExpress.XtraEditors.TextEdit();
            this.labelControl35 = new DevExpress.XtraEditors.LabelControl();
            this.txtRaisonSocialeFour = new DevExpress.XtraEditors.TextEdit();
            this.txtCFournisseur = new DevExpress.XtraEditors.TextEdit();
            this.labelControl22 = new DevExpress.XtraEditors.LabelControl();
            this.txtObservationClient = new DevExpress.XtraEditors.TextEdit();
            this.labelControl19 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl18 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.txtFax = new DevExpress.XtraEditors.TextEdit();
            this.txtNumCIN = new DevExpress.XtraEditors.TextEdit();
            this.txtNumeroTelephone1 = new DevExpress.XtraEditors.TextEdit();
            this.txtNumeroTelephone2 = new DevExpress.XtraEditors.TextEdit();
            this.txtEmail = new DevExpress.XtraEditors.TextEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.chkElimine = new DevExpress.XtraEditors.CheckEdit();
            this.chkClientPassager = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl21 = new DevExpress.XtraEditors.LabelControl();
            this.lkpGouvernorat = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.lkpCRegion = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.lkpRecouvreur = new DevExpress.XtraEditors.LookUpEdit();
            this.lkpCVendeur = new DevExpress.XtraEditors.LookUpEdit();
            this.LkpCFamille = new DevExpress.XtraEditors.LookUpEdit();
            this.txtAbreviationCode = new DevExpress.XtraEditors.TextEdit();
            this.txtCClient = new DevExpress.XtraEditors.TextEdit();
            this.txtRaisonSocial = new DevExpress.XtraEditors.TextEdit();
            this.chkBEtablissement = new DevExpress.XtraEditors.CheckEdit();
            this.tabVente = new DevExpress.XtraTab.XtraTabPage();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl6 = new DevExpress.XtraEditors.GroupControl();
            this.groupControl9 = new DevExpress.XtraEditors.GroupControl();
            this.rbContentieux = new System.Windows.Forms.RadioButton();
            this.rbNonContentieux = new System.Windows.Forms.RadioButton();
            this.txtRemiseExceptionnelle = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl24 = new DevExpress.XtraEditors.LabelControl();
            this.txtNbJourCreditFacture = new DevExpress.XtraEditors.SpinEdit();
            this.txtTauxRemise = new DevExpress.XtraEditors.SpinEdit();
            this.txtNbJourEcheancePaiment = new DevExpress.XtraEditors.SpinEdit();
            this.txtMontantCreditMax = new DevExpress.XtraEditors.SpinEdit();
            this.txtMontantCreditMin = new DevExpress.XtraEditors.SpinEdit();
            this.groupControl8 = new DevExpress.XtraEditors.GroupControl();
            this.radioVIP = new System.Windows.Forms.RadioButton();
            this.radioNonVIP = new System.Windows.Forms.RadioButton();
            this.groupControl7 = new DevExpress.XtraEditors.GroupControl();
            this.radioActif = new System.Windows.Forms.RadioButton();
            this.radioNonActif = new System.Windows.Forms.RadioButton();
            this.labelControl37 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl38 = new DevExpress.XtraEditors.LabelControl();
            this.lkpCTarif = new DevExpress.XtraEditors.LookUpEdit();
            this.lkpModePaiement = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl39 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl40 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl41 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl42 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl43 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl5 = new DevExpress.XtraEditors.GroupControl();
            this.groupControl16 = new DevExpress.XtraEditors.GroupControl();
            this.txtDateFinExoTPE = new DevExpress.XtraEditors.DateEdit();
            this.labelControl30 = new DevExpress.XtraEditors.LabelControl();
            this.radioTPEExonere = new System.Windows.Forms.RadioButton();
            this.radioTPENonExonere = new System.Windows.Forms.RadioButton();
            this.groupControl17 = new DevExpress.XtraEditors.GroupControl();
            this.txtDateFinExoTDC = new DevExpress.XtraEditors.DateEdit();
            this.labelControl32 = new DevExpress.XtraEditors.LabelControl();
            this.radioExoTDC = new System.Windows.Forms.RadioButton();
            this.radioNonExoTDC = new System.Windows.Forms.RadioButton();
            this.groupCAutorisation = new DevExpress.XtraEditors.GroupControl();
            this.labelControl28 = new DevExpress.XtraEditors.LabelControl();
            this.txtNAutorisation = new DevExpress.XtraEditors.TextEdit();
            this.txtDateDebSusp = new DevExpress.XtraEditors.DateEdit();
            this.labelControl25 = new DevExpress.XtraEditors.LabelControl();
            this.txtDateFinSusp = new DevExpress.XtraEditors.DateEdit();
            this.labelControl23 = new DevExpress.XtraEditors.LabelControl();
            this.chkBTransfertCompta = new DevExpress.XtraEditors.CheckEdit();
            this.groupControl15 = new DevExpress.XtraEditors.GroupControl();
            this.txtTauxRetenuSource = new DevExpress.XtraEditors.SpinEdit();
            this.txtTauxRetenuTVA = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl33 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl34 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl14 = new DevExpress.XtraEditors.GroupControl();
            this.radioTimbreExonore = new System.Windows.Forms.RadioButton();
            this.radioTimbreNonExonore = new System.Windows.Forms.RadioButton();
            this.groupControl13 = new DevExpress.XtraEditors.GroupControl();
            this.radioSuspension = new System.Windows.Forms.RadioButton();
            this.radioExport = new System.Windows.Forms.RadioButton();
            this.radioLocale = new System.Windows.Forms.RadioButton();
            this.groupControl12 = new DevExpress.XtraEditors.GroupControl();
            this.txtMontantExonoreTVA = new DevExpress.XtraEditors.SpinEdit();
            this.txtDateFinExoTVA = new DevExpress.XtraEditors.DateEdit();
            this.labelControl26 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl27 = new DevExpress.XtraEditors.LabelControl();
            this.radioTVAExonore = new System.Windows.Forms.RadioButton();
            this.radioTVANonExonore = new System.Windows.Forms.RadioButton();
            this.groupControl11 = new DevExpress.XtraEditors.GroupControl();
            this.txtDateFinExoFodec = new DevExpress.XtraEditors.DateEdit();
            this.labelControl31 = new DevExpress.XtraEditors.LabelControl();
            this.radioFodecExonore = new System.Windows.Forms.RadioButton();
            this.radioFodecNonExonore = new System.Windows.Forms.RadioButton();
            this.groupControl10 = new DevExpress.XtraEditors.GroupControl();
            this.txtnumetablissement = new DevExpress.XtraEditors.TextEdit();
            this.lkpcodecateg = new DevExpress.XtraEditors.LookUpEdit();
            this.lkpcodetva = new DevExpress.XtraEditors.LookUpEdit();
            this.lkpcle = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl29 = new DevExpress.XtraEditors.LabelControl();
            this.chkBAvanceForfaitaire = new DevExpress.XtraEditors.CheckEdit();
            this.radioNonMajore = new System.Windows.Forms.RadioButton();
            this.radioMajore = new System.Windows.Forms.RadioButton();
            this.txtCTVA = new DevExpress.XtraEditors.TextEdit();
            this.chkBInitialisationRemise = new DevExpress.XtraEditors.CheckEdit();
            this.panelControl15 = new DevExpress.XtraEditors.PanelControl();
            this.tabBanque = new DevExpress.XtraTab.XtraTabPage();
            this.panelControl14 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl18 = new DevExpress.XtraEditors.PanelControl();
            this.gridClientBanque = new DevExpress.XtraGrid.GridControl();
            this.gridVClientBanque = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tabContacts = new DevExpress.XtraTab.XtraTabPage();
            this.panelControl21 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl22 = new DevExpress.XtraEditors.PanelControl();
            this.gridClientContact = new DevExpress.XtraGrid.GridControl();
            this.gridVClientContact = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Etablissements = new DevExpress.XtraTab.XtraTabPage();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl20 = new DevExpress.XtraEditors.GroupControl();
            this.gridCEtab = new DevExpress.XtraGrid.GridControl();
            this.gridVEtab = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl19 = new DevExpress.XtraEditors.GroupControl();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.txtlongEtab = new DevExpress.XtraEditors.SpinEdit();
            this.txtlatEtab = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl51 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl52 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl49 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl50 = new DevExpress.XtraEditors.LabelControl();
            this.txtCodePostalEtab = new DevExpress.XtraEditors.TextEdit();
            this.txtVilleEtab = new DevExpress.XtraEditors.TextEdit();
            this.txtadresseEtab = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl48 = new DevExpress.XtraEditors.LabelControl();
            this.txtCodeEtab = new DevExpress.XtraEditors.TextEdit();
            this.labelControl53 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl54 = new DevExpress.XtraEditors.LabelControl();
            this.lkpRegionEtab = new DevExpress.XtraEditors.LookUpEdit();
            this.txtLibEtab = new DevExpress.XtraEditors.TextEdit();
            this.labelControl55 = new DevExpress.XtraEditors.LabelControl();
            this.dxValidationProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.PageClient)).BeginInit();
            this.PageClient.SuspendLayout();
            this.tabInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).BeginInit();
            this.groupControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpCPaysLivraison.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLibAdresseLiv.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCPostalLiv.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVilleAdresseLiv.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLibAdresseFac.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAdresseLivraison.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCPostalFac.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVilleAdresseFac.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpCPaysFacturation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMdp.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtemplacement.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRaisonSocialeFour.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCFournisseur.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtObservationClient.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumCIN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroTelephone1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroTelephone2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkElimine.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkClientPassager.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpGouvernorat.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpCRegion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpRecouvreur.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpCVendeur.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LkpCFamille.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAbreviationCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCClient.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRaisonSocial.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBEtablissement.Properties)).BeginInit();
            this.tabVente.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl6)).BeginInit();
            this.groupControl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl9)).BeginInit();
            this.groupControl9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemiseExceptionnelle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNbJourCreditFacture.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTauxRemise.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNbJourEcheancePaiment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMontantCreditMax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMontantCreditMin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl8)).BeginInit();
            this.groupControl8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl7)).BeginInit();
            this.groupControl7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpCTarif.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpModePaiement.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).BeginInit();
            this.groupControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl16)).BeginInit();
            this.groupControl16.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateFinExoTPE.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateFinExoTPE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl17)).BeginInit();
            this.groupControl17.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateFinExoTDC.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateFinExoTDC.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupCAutorisation)).BeginInit();
            this.groupCAutorisation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNAutorisation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateDebSusp.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateDebSusp.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateFinSusp.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateFinSusp.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBTransfertCompta.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl15)).BeginInit();
            this.groupControl15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTauxRetenuSource.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTauxRetenuTVA.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl14)).BeginInit();
            this.groupControl14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl13)).BeginInit();
            this.groupControl13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl12)).BeginInit();
            this.groupControl12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMontantExonoreTVA.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateFinExoTVA.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateFinExoTVA.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl11)).BeginInit();
            this.groupControl11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateFinExoFodec.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateFinExoFodec.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl10)).BeginInit();
            this.groupControl10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtnumetablissement.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpcodecateg.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpcodetva.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpcle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBAvanceForfaitaire.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCTVA.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBInitialisationRemise.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl15)).BeginInit();
            this.tabBanque.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl14)).BeginInit();
            this.panelControl14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl18)).BeginInit();
            this.panelControl18.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridClientBanque)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVClientBanque)).BeginInit();
            this.tabContacts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl21)).BeginInit();
            this.panelControl21.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl22)).BeginInit();
            this.panelControl22.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridClientContact)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVClientContact)).BeginInit();
            this.Etablissements.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl20)).BeginInit();
            this.groupControl20.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCEtab)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVEtab)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl19)).BeginInit();
            this.groupControl19.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtlongEtab.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtlatEtab.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodePostalEtab.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVilleEtab.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtadresseEtab.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodeEtab.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpRegionEtab.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLibEtab.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // PageClient
            // 
            this.PageClient.Location = new System.Drawing.Point(0, 0);
            this.PageClient.Name = "PageClient";
            this.PageClient.SelectedTabPage = this.tabInfo;
            this.PageClient.Size = new System.Drawing.Size(994, 686);
            this.PageClient.TabIndex = 0;
            this.PageClient.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabInfo,
            this.tabVente,
            this.tabBanque,
            this.tabContacts,
            this.Etablissements});
            this.PageClient.TabPageWidth = 170;
            // 
            // tabInfo
            // 
            this.tabInfo.Appearance.Header.Options.UseTextOptions = true;
            this.tabInfo.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tabInfo.Appearance.PageClient.BackColor = System.Drawing.Color.LightGray;
            this.tabInfo.Appearance.PageClient.Options.UseBackColor = true;
            this.tabInfo.Controls.Add(this.panelControl1);
            this.tabInfo.Name = "tabInfo";
            this.tabInfo.Size = new System.Drawing.Size(988, 658);
            this.tabInfo.TabPageWidth = 170;
            this.tabInfo.Text = "Généralités";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.groupControl4);
            this.panelControl1.Controls.Add(this.groupControl3);
            this.panelControl1.Controls.Add(this.groupControl2);
            this.panelControl1.Controls.Add(this.groupControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(988, 658);
            this.panelControl1.TabIndex = 0;
            // 
            // groupControl4
            // 
            this.groupControl4.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl4.AppearanceCaption.ForeColor = System.Drawing.Color.MidnightBlue;
            this.groupControl4.AppearanceCaption.Options.UseFont = true;
            this.groupControl4.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl4.Controls.Add(this.labelControl20);
            this.groupControl4.Controls.Add(this.lkpCPaysLivraison);
            this.groupControl4.Controls.Add(this.txtLibAdresseLiv);
            this.groupControl4.Controls.Add(this.labelControl11);
            this.groupControl4.Controls.Add(this.labelControl16);
            this.groupControl4.Controls.Add(this.labelControl17);
            this.groupControl4.Controls.Add(this.txtCPostalLiv);
            this.groupControl4.Controls.Add(this.txtVilleAdresseLiv);
            this.groupControl4.Location = new System.Drawing.Point(494, 153);
            this.groupControl4.Name = "groupControl4";
            this.groupControl4.Size = new System.Drawing.Size(488, 182);
            this.groupControl4.TabIndex = 2;
            this.groupControl4.Text = "Adresse Livraison";
            // 
            // labelControl20
            // 
            this.labelControl20.Location = new System.Drawing.Point(51, 150);
            this.labelControl20.Name = "labelControl20";
            this.labelControl20.Size = new System.Drawing.Size(30, 13);
            this.labelControl20.TabIndex = 64;
            this.labelControl20.Text = "Pays :";
            // 
            // lkpCPaysLivraison
            // 
            this.lkpCPaysLivraison.EnterMoveNextControl = true;
            this.lkpCPaysLivraison.Location = new System.Drawing.Point(84, 147);
            this.lkpCPaysLivraison.Name = "lkpCPaysLivraison";
            this.lkpCPaysLivraison.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpCPaysLivraison.Size = new System.Drawing.Size(140, 20);
            this.lkpCPaysLivraison.TabIndex = 3;
            this.lkpCPaysLivraison.Tag = "RQ";
            // 
            // txtLibAdresseLiv
            // 
            this.txtLibAdresseLiv.EnterMoveNextControl = true;
            this.txtLibAdresseLiv.Location = new System.Drawing.Point(84, 43);
            this.txtLibAdresseLiv.Name = "txtLibAdresseLiv";
            this.txtLibAdresseLiv.Properties.MaxLength = 100;
            this.txtLibAdresseLiv.Size = new System.Drawing.Size(327, 59);
            this.txtLibAdresseLiv.TabIndex = 0;
            this.txtLibAdresseLiv.UseOptimizedRendering = true;
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(56, 118);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(25, 13);
            this.labelControl11.TabIndex = 11;
            this.labelControl11.Text = "Ville :";
            // 
            // labelControl16
            // 
            this.labelControl16.Location = new System.Drawing.Point(230, 117);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(64, 13);
            this.labelControl16.TabIndex = 10;
            this.labelControl16.Text = "Code Postal :";
            // 
            // labelControl17
            // 
            this.labelControl17.Location = new System.Drawing.Point(35, 48);
            this.labelControl17.Name = "labelControl17";
            this.labelControl17.Size = new System.Drawing.Size(46, 13);
            this.labelControl17.TabIndex = 9;
            this.labelControl17.Text = "Adresse :";
            // 
            // txtCPostalLiv
            // 
            this.txtCPostalLiv.EnterMoveNextControl = true;
            this.txtCPostalLiv.Location = new System.Drawing.Point(297, 114);
            this.txtCPostalLiv.Name = "txtCPostalLiv";
            this.txtCPostalLiv.Properties.MaxLength = 20;
            this.txtCPostalLiv.Size = new System.Drawing.Size(114, 20);
            this.txtCPostalLiv.TabIndex = 2;
            this.txtCPostalLiv.Tag = "";
            // 
            // txtVilleAdresseLiv
            // 
            this.txtVilleAdresseLiv.EditValue = " ";
            this.txtVilleAdresseLiv.EnterMoveNextControl = true;
            this.txtVilleAdresseLiv.Location = new System.Drawing.Point(84, 115);
            this.txtVilleAdresseLiv.Name = "txtVilleAdresseLiv";
            this.txtVilleAdresseLiv.Properties.MaxLength = 20;
            this.txtVilleAdresseLiv.Size = new System.Drawing.Size(140, 20);
            this.txtVilleAdresseLiv.TabIndex = 1;
            this.txtVilleAdresseLiv.Tag = "";
            // 
            // groupControl3
            // 
            this.groupControl3.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl3.AppearanceCaption.ForeColor = System.Drawing.Color.MidnightBlue;
            this.groupControl3.AppearanceCaption.Options.UseFont = true;
            this.groupControl3.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl3.Controls.Add(this.txtLibAdresseFac);
            this.groupControl3.Controls.Add(this.chkAdresseLivraison);
            this.groupControl3.Controls.Add(this.labelControl10);
            this.groupControl3.Controls.Add(this.labelControl2);
            this.groupControl3.Controls.Add(this.labelControl1);
            this.groupControl3.Controls.Add(this.txtCPostalFac);
            this.groupControl3.Controls.Add(this.txtVilleAdresseFac);
            this.groupControl3.Controls.Add(this.labelControl12);
            this.groupControl3.Controls.Add(this.lkpCPaysFacturation);
            this.groupControl3.Location = new System.Drawing.Point(3, 152);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(485, 183);
            this.groupControl3.TabIndex = 1;
            this.groupControl3.Text = "Adresse Facturation";
            // 
            // txtLibAdresseFac
            // 
            this.txtLibAdresseFac.EnterMoveNextControl = true;
            this.txtLibAdresseFac.Location = new System.Drawing.Point(81, 43);
            this.txtLibAdresseFac.Name = "txtLibAdresseFac";
            this.txtLibAdresseFac.Properties.MaxLength = 100;
            this.txtLibAdresseFac.Size = new System.Drawing.Size(327, 59);
            this.txtLibAdresseFac.TabIndex = 0;
            this.txtLibAdresseFac.UseOptimizedRendering = true;
            // 
            // chkAdresseLivraison
            // 
            this.chkAdresseLivraison.EnterMoveNextControl = true;
            this.chkAdresseLivraison.Location = new System.Drawing.Point(227, 146);
            this.chkAdresseLivraison.Name = "chkAdresseLivraison";
            this.chkAdresseLivraison.Properties.Caption = "Adresse de Livraison";
            this.chkAdresseLivraison.Size = new System.Drawing.Size(121, 19);
            this.chkAdresseLivraison.TabIndex = 4;
            this.chkAdresseLivraison.Validating += new System.ComponentModel.CancelEventHandler(this.chkAdresseLivraison_Validating);
            this.chkAdresseLivraison.Validated += new System.EventHandler(this.chkAdresseLivraison_Validated);
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(53, 116);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(25, 13);
            this.labelControl10.TabIndex = 5;
            this.labelControl10.Text = "Ville :";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(227, 116);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(64, 13);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Code Postal :";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(32, 46);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(46, 13);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "Adresse :";
            // 
            // txtCPostalFac
            // 
            this.txtCPostalFac.EnterMoveNextControl = true;
            this.txtCPostalFac.Location = new System.Drawing.Point(294, 113);
            this.txtCPostalFac.Name = "txtCPostalFac";
            this.txtCPostalFac.Properties.MaxLength = 20;
            this.txtCPostalFac.Size = new System.Drawing.Size(114, 20);
            this.txtCPostalFac.TabIndex = 2;
            this.txtCPostalFac.Tag = "";
            // 
            // txtVilleAdresseFac
            // 
            this.txtVilleAdresseFac.EditValue = " ";
            this.txtVilleAdresseFac.EnterMoveNextControl = true;
            this.txtVilleAdresseFac.Location = new System.Drawing.Point(81, 113);
            this.txtVilleAdresseFac.Name = "txtVilleAdresseFac";
            this.txtVilleAdresseFac.Properties.MaxLength = 20;
            this.txtVilleAdresseFac.Size = new System.Drawing.Size(140, 20);
            this.txtVilleAdresseFac.TabIndex = 1;
            this.txtVilleAdresseFac.Tag = "";
            // 
            // labelControl12
            // 
            this.labelControl12.Location = new System.Drawing.Point(48, 148);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(30, 13);
            this.labelControl12.TabIndex = 62;
            this.labelControl12.Text = "Pays :";
            // 
            // lkpCPaysFacturation
            // 
            this.lkpCPaysFacturation.EnterMoveNextControl = true;
            this.lkpCPaysFacturation.Location = new System.Drawing.Point(81, 145);
            this.lkpCPaysFacturation.Name = "lkpCPaysFacturation";
            this.lkpCPaysFacturation.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpCPaysFacturation.Size = new System.Drawing.Size(140, 20);
            this.lkpCPaysFacturation.TabIndex = 3;
            this.lkpCPaysFacturation.Tag = "RQ";
            // 
            // groupControl2
            // 
            this.groupControl2.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl2.AppearanceCaption.ForeColor = System.Drawing.Color.MidnightBlue;
            this.groupControl2.AppearanceCaption.Options.UseFont = true;
            this.groupControl2.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl2.Controls.Add(this.txtMdp);
            this.groupControl2.Controls.Add(this.labelControl45);
            this.groupControl2.Controls.Add(this.simpleButton1);
            this.groupControl2.Controls.Add(this.labelControl44);
            this.groupControl2.Controls.Add(this.bttapercu);
            this.groupControl2.Controls.Add(this.bttparcourir);
            this.groupControl2.Controls.Add(this.labelControl36);
            this.groupControl2.Controls.Add(this.txtemplacement);
            this.groupControl2.Controls.Add(this.labelControl35);
            this.groupControl2.Controls.Add(this.txtRaisonSocialeFour);
            this.groupControl2.Controls.Add(this.txtCFournisseur);
            this.groupControl2.Controls.Add(this.labelControl22);
            this.groupControl2.Controls.Add(this.txtObservationClient);
            this.groupControl2.Controls.Add(this.labelControl19);
            this.groupControl2.Controls.Add(this.labelControl18);
            this.groupControl2.Controls.Add(this.labelControl15);
            this.groupControl2.Controls.Add(this.labelControl14);
            this.groupControl2.Controls.Add(this.labelControl13);
            this.groupControl2.Controls.Add(this.txtFax);
            this.groupControl2.Controls.Add(this.txtNumCIN);
            this.groupControl2.Controls.Add(this.txtNumeroTelephone1);
            this.groupControl2.Controls.Add(this.txtNumeroTelephone2);
            this.groupControl2.Controls.Add(this.txtEmail);
            this.groupControl2.Location = new System.Drawing.Point(1, 339);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(981, 292);
            this.groupControl2.TabIndex = 3;
            this.groupControl2.Text = "Info";
            // 
            // txtMdp
            // 
            this.txtMdp.Location = new System.Drawing.Point(152, 247);
            this.txtMdp.Name = "txtMdp";
            this.txtMdp.Properties.PasswordChar = '•';
            this.txtMdp.Size = new System.Drawing.Size(258, 20);
            this.txtMdp.TabIndex = 79;
            // 
            // labelControl45
            // 
            this.labelControl45.Location = new System.Drawing.Point(74, 252);
            this.labelControl45.Name = "labelControl45";
            this.labelControl45.Size = new System.Drawing.Size(71, 13);
            this.labelControl45.TabIndex = 78;
            this.labelControl45.Text = "Mot de passe :";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(868, 200);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(33, 23);
            this.simpleButton1.TabIndex = 77;
            this.simpleButton1.Text = "CL";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // labelControl44
            // 
            this.labelControl44.Location = new System.Drawing.Point(75, 224);
            this.labelControl44.Name = "labelControl44";
            this.labelControl44.Size = new System.Drawing.Size(38, 13);
            this.labelControl44.TabIndex = 76;
            this.labelControl44.Text = "Patente";
            // 
            // bttapercu
            // 
            this.bttapercu.Location = new System.Drawing.Point(809, 200);
            this.bttapercu.Name = "bttapercu";
            this.bttapercu.Size = new System.Drawing.Size(56, 23);
            this.bttapercu.TabIndex = 75;
            this.bttapercu.Text = "Aperçu";
            this.bttapercu.Click += new System.EventHandler(this.bttapercu_Click);
            // 
            // bttparcourir
            // 
            this.bttparcourir.Location = new System.Drawing.Point(755, 200);
            this.bttparcourir.Name = "bttparcourir";
            this.bttparcourir.Size = new System.Drawing.Size(52, 23);
            this.bttparcourir.TabIndex = 74;
            this.bttparcourir.Text = "Parcourir";
            this.bttparcourir.Click += new System.EventHandler(this.bttparcourir_Click);
            // 
            // labelControl36
            // 
            this.labelControl36.Location = new System.Drawing.Point(75, 205);
            this.labelControl36.Name = "labelControl36";
            this.labelControl36.Size = new System.Drawing.Size(70, 13);
            this.labelControl36.TabIndex = 73;
            this.labelControl36.Text = "Emplacement :";
            // 
            // txtemplacement
            // 
            this.txtemplacement.Enabled = false;
            this.txtemplacement.Location = new System.Drawing.Point(148, 201);
            this.txtemplacement.Name = "txtemplacement";
            this.txtemplacement.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtemplacement.Properties.Appearance.Options.UseFont = true;
            this.txtemplacement.Size = new System.Drawing.Size(601, 20);
            this.txtemplacement.TabIndex = 72;
            // 
            // labelControl35
            // 
            this.labelControl35.Location = new System.Drawing.Point(73, 169);
            this.labelControl35.Name = "labelControl35";
            this.labelControl35.Size = new System.Drawing.Size(76, 13);
            this.labelControl35.TabIndex = 71;
            this.labelControl35.Text = "Four. Associer :";
            // 
            // txtRaisonSocialeFour
            // 
            this.txtRaisonSocialeFour.Enabled = false;
            this.txtRaisonSocialeFour.Location = new System.Drawing.Point(282, 165);
            this.txtRaisonSocialeFour.Name = "txtRaisonSocialeFour";
            this.txtRaisonSocialeFour.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.txtRaisonSocialeFour.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.txtRaisonSocialeFour.Properties.Appearance.Options.UseBackColor = true;
            this.txtRaisonSocialeFour.Properties.Appearance.Options.UseForeColor = true;
            this.txtRaisonSocialeFour.Size = new System.Drawing.Size(608, 20);
            this.txtRaisonSocialeFour.TabIndex = 70;
            // 
            // txtCFournisseur
            // 
            this.txtCFournisseur.EnterMoveNextControl = true;
            this.txtCFournisseur.Location = new System.Drawing.Point(151, 165);
            this.txtCFournisseur.Name = "txtCFournisseur";
            this.txtCFournisseur.Size = new System.Drawing.Size(128, 20);
            this.txtCFournisseur.TabIndex = 69;
            this.txtCFournisseur.Tag = "Fournisseur";
            this.txtCFournisseur.Validating += new System.ComponentModel.CancelEventHandler(this.textEdit1_Validating);
            // 
            // labelControl22
            // 
            this.labelControl22.Location = new System.Drawing.Point(82, 133);
            this.labelControl22.Name = "labelControl22";
            this.labelControl22.Size = new System.Drawing.Size(66, 13);
            this.labelControl22.TabIndex = 68;
            this.labelControl22.Text = "Observation :";
            // 
            // txtObservationClient
            // 
            this.txtObservationClient.EnterMoveNextControl = true;
            this.txtObservationClient.Location = new System.Drawing.Point(151, 129);
            this.txtObservationClient.Name = "txtObservationClient";
            this.txtObservationClient.Properties.MaxLength = 500;
            this.txtObservationClient.Size = new System.Drawing.Size(741, 20);
            this.txtObservationClient.TabIndex = 5;
            // 
            // labelControl19
            // 
            this.labelControl19.Location = new System.Drawing.Point(103, 71);
            this.labelControl19.Name = "labelControl19";
            this.labelControl19.Size = new System.Drawing.Size(45, 13);
            this.labelControl19.TabIndex = 67;
            this.labelControl19.Text = "N° Tél 1 :";
            // 
            // labelControl18
            // 
            this.labelControl18.Location = new System.Drawing.Point(379, 99);
            this.labelControl18.Name = "labelControl18";
            this.labelControl18.Size = new System.Drawing.Size(31, 13);
            this.labelControl18.TabIndex = 66;
            this.labelControl18.Text = "Email :";
            // 
            // labelControl15
            // 
            this.labelControl15.Location = new System.Drawing.Point(365, 67);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(45, 13);
            this.labelControl15.TabIndex = 65;
            this.labelControl15.Text = "N° Tél 2 :";
            // 
            // labelControl14
            // 
            this.labelControl14.Location = new System.Drawing.Point(123, 102);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(25, 13);
            this.labelControl14.TabIndex = 64;
            this.labelControl14.Text = "CIN :";
            // 
            // labelControl13
            // 
            this.labelControl13.Location = new System.Drawing.Point(649, 68);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(40, 13);
            this.labelControl13.TabIndex = 63;
            this.labelControl13.Text = "N° Fax :";
            // 
            // txtFax
            // 
            this.txtFax.EnterMoveNextControl = true;
            this.txtFax.Location = new System.Drawing.Point(723, 65);
            this.txtFax.Name = "txtFax";
            this.txtFax.Properties.MaxLength = 20;
            this.txtFax.Size = new System.Drawing.Size(153, 20);
            this.txtFax.TabIndex = 2;
            // 
            // txtNumCIN
            // 
            this.txtNumCIN.EnterMoveNextControl = true;
            this.txtNumCIN.Location = new System.Drawing.Point(151, 98);
            this.txtNumCIN.Name = "txtNumCIN";
            this.txtNumCIN.Properties.Mask.EditMask = "[0-9]\\d*";
            this.txtNumCIN.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtNumCIN.Properties.MaxLength = 8;
            this.txtNumCIN.Size = new System.Drawing.Size(208, 20);
            this.txtNumCIN.TabIndex = 3;
            // 
            // txtNumeroTelephone1
            // 
            this.txtNumeroTelephone1.EnterMoveNextControl = true;
            this.txtNumeroTelephone1.Location = new System.Drawing.Point(151, 67);
            this.txtNumeroTelephone1.Name = "txtNumeroTelephone1";
            this.txtNumeroTelephone1.Properties.MaxLength = 20;
            this.txtNumeroTelephone1.Size = new System.Drawing.Size(208, 20);
            this.txtNumeroTelephone1.TabIndex = 0;
            // 
            // txtNumeroTelephone2
            // 
            this.txtNumeroTelephone2.EnterMoveNextControl = true;
            this.txtNumeroTelephone2.Location = new System.Drawing.Point(413, 64);
            this.txtNumeroTelephone2.Name = "txtNumeroTelephone2";
            this.txtNumeroTelephone2.Properties.MaxLength = 20;
            this.txtNumeroTelephone2.Size = new System.Drawing.Size(208, 20);
            this.txtNumeroTelephone2.TabIndex = 1;
            // 
            // txtEmail
            // 
            this.txtEmail.EditValue = "";
            this.txtEmail.EnterMoveNextControl = true;
            this.txtEmail.Location = new System.Drawing.Point(413, 95);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Properties.MaxLength = 50;
            this.txtEmail.Size = new System.Drawing.Size(208, 20);
            this.txtEmail.TabIndex = 4;
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl1.AppearanceCaption.ForeColor = System.Drawing.Color.MidnightBlue;
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl1.Controls.Add(this.chkElimine);
            this.groupControl1.Controls.Add(this.chkClientPassager);
            this.groupControl1.Controls.Add(this.labelControl21);
            this.groupControl1.Controls.Add(this.lkpGouvernorat);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Controls.Add(this.lkpCRegion);
            this.groupControl1.Controls.Add(this.labelControl8);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl9);
            this.groupControl1.Controls.Add(this.lkpRecouvreur);
            this.groupControl1.Controls.Add(this.lkpCVendeur);
            this.groupControl1.Controls.Add(this.LkpCFamille);
            this.groupControl1.Controls.Add(this.txtAbreviationCode);
            this.groupControl1.Controls.Add(this.txtCClient);
            this.groupControl1.Controls.Add(this.txtRaisonSocial);
            this.groupControl1.Controls.Add(this.chkBEtablissement);
            this.groupControl1.Location = new System.Drawing.Point(3, 7);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(979, 132);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Client";
            // 
            // chkElimine
            // 
            this.chkElimine.EnterMoveNextControl = true;
            this.chkElimine.Location = new System.Drawing.Point(803, 108);
            this.chkElimine.Name = "chkElimine";
            this.chkElimine.Properties.AutoWidth = true;
            this.chkElimine.Properties.Caption = "Eliminé";
            this.chkElimine.Size = new System.Drawing.Size(54, 19);
            this.chkElimine.TabIndex = 55;
            // 
            // chkClientPassager
            // 
            this.chkClientPassager.EnterMoveNextControl = true;
            this.chkClientPassager.Location = new System.Drawing.Point(803, 83);
            this.chkClientPassager.Name = "chkClientPassager";
            this.chkClientPassager.Properties.AutoWidth = true;
            this.chkClientPassager.Properties.Caption = "Client Passager";
            this.chkClientPassager.Size = new System.Drawing.Size(96, 19);
            this.chkClientPassager.TabIndex = 53;
            // 
            // labelControl21
            // 
            this.labelControl21.Location = new System.Drawing.Point(503, 105);
            this.labelControl21.Name = "labelControl21";
            this.labelControl21.Size = new System.Drawing.Size(68, 13);
            this.labelControl21.TabIndex = 52;
            this.labelControl21.Text = "Gouvernorat :";
            // 
            // lkpGouvernorat
            // 
            this.lkpGouvernorat.EnterMoveNextControl = true;
            this.lkpGouvernorat.Location = new System.Drawing.Point(574, 102);
            this.lkpGouvernorat.Name = "lkpGouvernorat";
            this.lkpGouvernorat.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpGouvernorat.Size = new System.Drawing.Size(136, 20);
            this.lkpGouvernorat.TabIndex = 51;
            this.lkpGouvernorat.Tag = "RQ";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(271, 105);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(40, 13);
            this.labelControl7.TabIndex = 50;
            this.labelControl7.Text = "Région :";
            // 
            // lkpCRegion
            // 
            this.lkpCRegion.EnterMoveNextControl = true;
            this.lkpCRegion.Location = new System.Drawing.Point(314, 101);
            this.lkpCRegion.Name = "lkpCRegion";
            this.lkpCRegion.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpCRegion.Size = new System.Drawing.Size(136, 20);
            this.lkpCRegion.TabIndex = 6;
            this.lkpCRegion.Tag = "RQ";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(71, 39);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(32, 13);
            this.labelControl8.TabIndex = 48;
            this.labelControl8.Text = "Code :";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(28, 68);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(75, 13);
            this.labelControl6.TabIndex = 46;
            this.labelControl6.Text = "Raison Sociale :";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(456, 39);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(62, 13);
            this.labelControl5.TabIndex = 45;
            this.labelControl5.Text = "Abréviation :";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(761, 35);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(39, 13);
            this.labelControl4.TabIndex = 44;
            this.labelControl4.Text = "Famille :";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(753, 63);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(47, 13);
            this.labelControl3.TabIndex = 43;
            this.labelControl3.Text = "Vendeur :";
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(15, 105);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(63, 13);
            this.labelControl9.TabIndex = 42;
            this.labelControl9.Text = "Recouvreur :";
            // 
            // lkpRecouvreur
            // 
            this.lkpRecouvreur.EnterMoveNextControl = true;
            this.lkpRecouvreur.Location = new System.Drawing.Point(81, 101);
            this.lkpRecouvreur.Name = "lkpRecouvreur";
            this.lkpRecouvreur.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpRecouvreur.Size = new System.Drawing.Size(136, 20);
            this.lkpRecouvreur.TabIndex = 5;
            this.lkpRecouvreur.Tag = "RQ";
            // 
            // lkpCVendeur
            // 
            this.lkpCVendeur.EnterMoveNextControl = true;
            this.lkpCVendeur.Location = new System.Drawing.Point(803, 59);
            this.lkpCVendeur.Name = "lkpCVendeur";
            this.lkpCVendeur.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpCVendeur.Size = new System.Drawing.Size(136, 20);
            this.lkpCVendeur.TabIndex = 4;
            this.lkpCVendeur.Tag = "RQ";
            // 
            // LkpCFamille
            // 
            this.LkpCFamille.EnterMoveNextControl = true;
            this.LkpCFamille.Location = new System.Drawing.Point(803, 31);
            this.LkpCFamille.Name = "LkpCFamille";
            this.LkpCFamille.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LkpCFamille.Size = new System.Drawing.Size(136, 20);
            this.LkpCFamille.TabIndex = 3;
            this.LkpCFamille.Tag = "RQ";
            // 
            // txtAbreviationCode
            // 
            this.txtAbreviationCode.EnterMoveNextControl = true;
            this.txtAbreviationCode.Location = new System.Drawing.Point(522, 36);
            this.txtAbreviationCode.Name = "txtAbreviationCode";
            this.txtAbreviationCode.Properties.MaxLength = 10;
            this.txtAbreviationCode.Size = new System.Drawing.Size(188, 20);
            this.txtAbreviationCode.TabIndex = 1;
            // 
            // txtCClient
            // 
            this.txtCClient.EnterMoveNextControl = true;
            this.txtCClient.Location = new System.Drawing.Point(106, 36);
            this.txtCClient.Name = "txtCClient";
            this.txtCClient.Properties.MaxLength = 20;
            this.txtCClient.Size = new System.Drawing.Size(188, 20);
            this.txtCClient.TabIndex = 0;
            this.txtCClient.Tag = "Client\\RQ";
            this.txtCClient.Validated += new System.EventHandler(this.txtCClient_Validated);
            // 
            // txtRaisonSocial
            // 
            this.txtRaisonSocial.EnterMoveNextControl = true;
            this.txtRaisonSocial.Location = new System.Drawing.Point(106, 64);
            this.txtRaisonSocial.Name = "txtRaisonSocial";
            this.txtRaisonSocial.Properties.MaxLength = 200;
            this.txtRaisonSocial.Size = new System.Drawing.Size(604, 20);
            this.txtRaisonSocial.TabIndex = 2;
            this.txtRaisonSocial.Tag = "RQ";
            // 
            // chkBEtablissement
            // 
            this.chkBEtablissement.EnterMoveNextControl = true;
            this.chkBEtablissement.Location = new System.Drawing.Point(936, 99);
            this.chkBEtablissement.Name = "chkBEtablissement";
            this.chkBEtablissement.Properties.AutoWidth = true;
            this.chkBEtablissement.Properties.Caption = "Gère par Etablissement";
            this.chkBEtablissement.Size = new System.Drawing.Size(133, 19);
            this.chkBEtablissement.TabIndex = 54;
            this.chkBEtablissement.Visible = false;
            // 
            // tabVente
            // 
            this.tabVente.Appearance.Header.Options.UseTextOptions = true;
            this.tabVente.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tabVente.Controls.Add(this.panelControl3);
            this.tabVente.Controls.Add(this.panelControl15);
            this.tabVente.Name = "tabVente";
            this.tabVente.Size = new System.Drawing.Size(988, 658);
            this.tabVente.TabPageWidth = 170;
            this.tabVente.Text = "Vente";
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.groupControl6);
            this.panelControl3.Controls.Add(this.groupControl5);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(0, 0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(988, 658);
            this.panelControl3.TabIndex = 4;
            // 
            // groupControl6
            // 
            this.groupControl6.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl6.AppearanceCaption.ForeColor = System.Drawing.Color.MidnightBlue;
            this.groupControl6.AppearanceCaption.Options.UseFont = true;
            this.groupControl6.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl6.Controls.Add(this.groupControl9);
            this.groupControl6.Controls.Add(this.txtRemiseExceptionnelle);
            this.groupControl6.Controls.Add(this.labelControl24);
            this.groupControl6.Controls.Add(this.txtNbJourCreditFacture);
            this.groupControl6.Controls.Add(this.txtTauxRemise);
            this.groupControl6.Controls.Add(this.txtNbJourEcheancePaiment);
            this.groupControl6.Controls.Add(this.txtMontantCreditMax);
            this.groupControl6.Controls.Add(this.txtMontantCreditMin);
            this.groupControl6.Controls.Add(this.groupControl8);
            this.groupControl6.Controls.Add(this.groupControl7);
            this.groupControl6.Controls.Add(this.labelControl37);
            this.groupControl6.Controls.Add(this.labelControl38);
            this.groupControl6.Controls.Add(this.lkpCTarif);
            this.groupControl6.Controls.Add(this.lkpModePaiement);
            this.groupControl6.Controls.Add(this.labelControl39);
            this.groupControl6.Controls.Add(this.labelControl40);
            this.groupControl6.Controls.Add(this.labelControl41);
            this.groupControl6.Controls.Add(this.labelControl42);
            this.groupControl6.Controls.Add(this.labelControl43);
            this.groupControl6.Location = new System.Drawing.Point(9, 476);
            this.groupControl6.Name = "groupControl6";
            this.groupControl6.Size = new System.Drawing.Size(974, 169);
            this.groupControl6.TabIndex = 1;
            this.groupControl6.Text = "Privilèges";
            // 
            // groupControl9
            // 
            this.groupControl9.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl9.AppearanceCaption.ForeColor = System.Drawing.Color.MidnightBlue;
            this.groupControl9.AppearanceCaption.Options.UseFont = true;
            this.groupControl9.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl9.Controls.Add(this.rbContentieux);
            this.groupControl9.Controls.Add(this.rbNonContentieux);
            this.groupControl9.Location = new System.Drawing.Point(723, 119);
            this.groupControl9.Name = "groupControl9";
            this.groupControl9.Size = new System.Drawing.Size(228, 41);
            this.groupControl9.TabIndex = 10;
            this.groupControl9.Text = "Douteux";
            // 
            // rbContentieux
            // 
            this.rbContentieux.AutoSize = true;
            this.rbContentieux.Location = new System.Drawing.Point(140, 21);
            this.rbContentieux.Name = "rbContentieux";
            this.rbContentieux.Size = new System.Drawing.Size(66, 17);
            this.rbContentieux.TabIndex = 1;
            this.rbContentieux.Text = "Douteux";
            this.rbContentieux.UseVisualStyleBackColor = true;
            // 
            // rbNonContentieux
            // 
            this.rbNonContentieux.AutoSize = true;
            this.rbNonContentieux.Checked = true;
            this.rbNonContentieux.Location = new System.Drawing.Point(6, 21);
            this.rbNonContentieux.Name = "rbNonContentieux";
            this.rbNonContentieux.Size = new System.Drawing.Size(88, 17);
            this.rbNonContentieux.TabIndex = 0;
            this.rbNonContentieux.TabStop = true;
            this.rbNonContentieux.Text = "Non Douteux";
            this.rbNonContentieux.UseVisualStyleBackColor = true;
            // 
            // txtRemiseExceptionnelle
            // 
            this.txtRemiseExceptionnelle.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtRemiseExceptionnelle.EnterMoveNextControl = true;
            this.txtRemiseExceptionnelle.Location = new System.Drawing.Point(677, 70);
            this.txtRemiseExceptionnelle.Name = "txtRemiseExceptionnelle";
            this.txtRemiseExceptionnelle.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtRemiseExceptionnelle.Properties.Mask.EditMask = "#,##0.00;";
            this.txtRemiseExceptionnelle.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtRemiseExceptionnelle.Size = new System.Drawing.Size(189, 20);
            this.txtRemiseExceptionnelle.TabIndex = 5;
            this.txtRemiseExceptionnelle.Visible = false;
            // 
            // labelControl24
            // 
            this.labelControl24.Location = new System.Drawing.Point(549, 73);
            this.labelControl24.Name = "labelControl24";
            this.labelControl24.Size = new System.Drawing.Size(126, 13);
            this.labelControl24.TabIndex = 71;
            this.labelControl24.Text = "Remise Exceptionelle(%) :";
            this.labelControl24.Visible = false;
            // 
            // txtNbJourCreditFacture
            // 
            this.txtNbJourCreditFacture.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtNbJourCreditFacture.EnterMoveNextControl = true;
            this.txtNbJourCreditFacture.Location = new System.Drawing.Point(213, 70);
            this.txtNbJourCreditFacture.Name = "txtNbJourCreditFacture";
            this.txtNbJourCreditFacture.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtNbJourCreditFacture.Properties.IsFloatValue = false;
            this.txtNbJourCreditFacture.Properties.Mask.EditMask = "n0";
            this.txtNbJourCreditFacture.Size = new System.Drawing.Size(189, 20);
            this.txtNbJourCreditFacture.TabIndex = 4;
            // 
            // txtTauxRemise
            // 
            this.txtTauxRemise.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtTauxRemise.EnterMoveNextControl = true;
            this.txtTauxRemise.Location = new System.Drawing.Point(677, 47);
            this.txtTauxRemise.Name = "txtTauxRemise";
            this.txtTauxRemise.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtTauxRemise.Properties.Mask.EditMask = "#,##0.00;";
            this.txtTauxRemise.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtTauxRemise.Size = new System.Drawing.Size(189, 20);
            this.txtTauxRemise.TabIndex = 3;
            // 
            // txtNbJourEcheancePaiment
            // 
            this.txtNbJourEcheancePaiment.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtNbJourEcheancePaiment.EnterMoveNextControl = true;
            this.txtNbJourEcheancePaiment.Location = new System.Drawing.Point(213, 47);
            this.txtNbJourEcheancePaiment.Name = "txtNbJourEcheancePaiment";
            this.txtNbJourEcheancePaiment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtNbJourEcheancePaiment.Properties.IsFloatValue = false;
            this.txtNbJourEcheancePaiment.Properties.Mask.EditMask = "n0";
            this.txtNbJourEcheancePaiment.Size = new System.Drawing.Size(189, 20);
            this.txtNbJourEcheancePaiment.TabIndex = 2;
            // 
            // txtMontantCreditMax
            // 
            this.txtMontantCreditMax.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtMontantCreditMax.EnterMoveNextControl = true;
            this.txtMontantCreditMax.Location = new System.Drawing.Point(677, 24);
            this.txtMontantCreditMax.Name = "txtMontantCreditMax";
            this.txtMontantCreditMax.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtMontantCreditMax.Properties.Mask.EditMask = "#,###0.000;";
            this.txtMontantCreditMax.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtMontantCreditMax.Size = new System.Drawing.Size(189, 20);
            this.txtMontantCreditMax.TabIndex = 1;
            // 
            // txtMontantCreditMin
            // 
            this.txtMontantCreditMin.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtMontantCreditMin.EnterMoveNextControl = true;
            this.txtMontantCreditMin.Location = new System.Drawing.Point(213, 24);
            this.txtMontantCreditMin.Name = "txtMontantCreditMin";
            this.txtMontantCreditMin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtMontantCreditMin.Properties.Mask.EditMask = "#,###0.000;";
            this.txtMontantCreditMin.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtMontantCreditMin.Size = new System.Drawing.Size(189, 20);
            this.txtMontantCreditMin.TabIndex = 0;
            // 
            // groupControl8
            // 
            this.groupControl8.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl8.AppearanceCaption.ForeColor = System.Drawing.Color.MidnightBlue;
            this.groupControl8.AppearanceCaption.Options.UseFont = true;
            this.groupControl8.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl8.Controls.Add(this.radioVIP);
            this.groupControl8.Controls.Add(this.radioNonVIP);
            this.groupControl8.Location = new System.Drawing.Point(374, 120);
            this.groupControl8.Name = "groupControl8";
            this.groupControl8.Size = new System.Drawing.Size(228, 41);
            this.groupControl8.TabIndex = 9;
            this.groupControl8.Text = "VIP";
            // 
            // radioVIP
            // 
            this.radioVIP.AutoSize = true;
            this.radioVIP.Location = new System.Drawing.Point(158, 21);
            this.radioVIP.Name = "radioVIP";
            this.radioVIP.Size = new System.Drawing.Size(41, 17);
            this.radioVIP.TabIndex = 1;
            this.radioVIP.Text = "VIP";
            this.radioVIP.UseVisualStyleBackColor = true;
            // 
            // radioNonVIP
            // 
            this.radioNonVIP.AutoSize = true;
            this.radioNonVIP.Checked = true;
            this.radioNonVIP.Location = new System.Drawing.Point(28, 21);
            this.radioNonVIP.Name = "radioNonVIP";
            this.radioNonVIP.Size = new System.Drawing.Size(63, 17);
            this.radioNonVIP.TabIndex = 0;
            this.radioNonVIP.TabStop = true;
            this.radioNonVIP.Text = "Non VIP";
            this.radioNonVIP.UseVisualStyleBackColor = true;
            // 
            // groupControl7
            // 
            this.groupControl7.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl7.AppearanceCaption.ForeColor = System.Drawing.Color.MidnightBlue;
            this.groupControl7.AppearanceCaption.Options.UseFont = true;
            this.groupControl7.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl7.Controls.Add(this.radioActif);
            this.groupControl7.Controls.Add(this.radioNonActif);
            this.groupControl7.Location = new System.Drawing.Point(21, 120);
            this.groupControl7.Name = "groupControl7";
            this.groupControl7.Size = new System.Drawing.Size(228, 41);
            this.groupControl7.TabIndex = 8;
            this.groupControl7.Text = "Actif";
            // 
            // radioActif
            // 
            this.radioActif.AutoSize = true;
            this.radioActif.Checked = true;
            this.radioActif.Location = new System.Drawing.Point(31, 21);
            this.radioActif.Name = "radioActif";
            this.radioActif.Size = new System.Drawing.Size(47, 17);
            this.radioActif.TabIndex = 0;
            this.radioActif.TabStop = true;
            this.radioActif.Text = "Actif";
            this.radioActif.UseVisualStyleBackColor = true;
            // 
            // radioNonActif
            // 
            this.radioNonActif.AutoSize = true;
            this.radioNonActif.Location = new System.Drawing.Point(128, 21);
            this.radioNonActif.Name = "radioNonActif";
            this.radioNonActif.Size = new System.Drawing.Size(69, 17);
            this.radioNonActif.TabIndex = 1;
            this.radioNonActif.Text = "Non Actif";
            this.radioNonActif.UseVisualStyleBackColor = true;
            // 
            // labelControl37
            // 
            this.labelControl37.Location = new System.Drawing.Point(137, 97);
            this.labelControl37.Name = "labelControl37";
            this.labelControl37.Size = new System.Drawing.Size(73, 13);
            this.labelControl37.TabIndex = 69;
            this.labelControl37.Text = "Tarif Appliqué :";
            // 
            // labelControl38
            // 
            this.labelControl38.Location = new System.Drawing.Point(579, 96);
            this.labelControl38.Name = "labelControl38";
            this.labelControl38.Size = new System.Drawing.Size(95, 13);
            this.labelControl38.TabIndex = 68;
            this.labelControl38.Text = "Mode de paiement :";
            // 
            // lkpCTarif
            // 
            this.lkpCTarif.EnterMoveNextControl = true;
            this.lkpCTarif.Location = new System.Drawing.Point(213, 93);
            this.lkpCTarif.Name = "lkpCTarif";
            this.lkpCTarif.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpCTarif.Size = new System.Drawing.Size(189, 20);
            this.lkpCTarif.TabIndex = 6;
            this.lkpCTarif.Tag = "RQ";
            // 
            // lkpModePaiement
            // 
            this.lkpModePaiement.EnterMoveNextControl = true;
            this.lkpModePaiement.Location = new System.Drawing.Point(677, 93);
            this.lkpModePaiement.Name = "lkpModePaiement";
            this.lkpModePaiement.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpModePaiement.Size = new System.Drawing.Size(189, 20);
            this.lkpModePaiement.TabIndex = 7;
            this.lkpModePaiement.Tag = "RQ";
            // 
            // labelControl39
            // 
            this.labelControl39.Location = new System.Drawing.Point(112, 27);
            this.labelControl39.Name = "labelControl39";
            this.labelControl39.Size = new System.Drawing.Size(98, 13);
            this.labelControl39.TabIndex = 0;
            this.labelControl39.Text = "Montant Crédit Min :";
            // 
            // labelControl40
            // 
            this.labelControl40.Location = new System.Drawing.Point(68, 50);
            this.labelControl40.Name = "labelControl40";
            this.labelControl40.Size = new System.Drawing.Size(142, 13);
            this.labelControl40.TabIndex = 66;
            this.labelControl40.Text = "Délai d\'échéance/Réglement :";
            // 
            // labelControl41
            // 
            this.labelControl41.Location = new System.Drawing.Point(82, 73);
            this.labelControl41.Name = "labelControl41";
            this.labelControl41.Size = new System.Drawing.Size(128, 13);
            this.labelControl41.TabIndex = 65;
            this.labelControl41.Text = "Délai d\'échéance/Facture :";
            // 
            // labelControl42
            // 
            this.labelControl42.Location = new System.Drawing.Point(572, 27);
            this.labelControl42.Name = "labelControl42";
            this.labelControl42.Size = new System.Drawing.Size(102, 13);
            this.labelControl42.TabIndex = 64;
            this.labelControl42.Text = "Montant Crédit Max :";
            // 
            // labelControl43
            // 
            this.labelControl43.Location = new System.Drawing.Point(614, 50);
            this.labelControl43.Name = "labelControl43";
            this.labelControl43.Size = new System.Drawing.Size(60, 13);
            this.labelControl43.TabIndex = 63;
            this.labelControl43.Text = "Remise(%) :";
            // 
            // groupControl5
            // 
            this.groupControl5.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl5.AppearanceCaption.ForeColor = System.Drawing.Color.MidnightBlue;
            this.groupControl5.AppearanceCaption.Options.UseFont = true;
            this.groupControl5.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl5.Controls.Add(this.groupControl16);
            this.groupControl5.Controls.Add(this.groupControl17);
            this.groupControl5.Controls.Add(this.groupCAutorisation);
            this.groupControl5.Controls.Add(this.chkBTransfertCompta);
            this.groupControl5.Controls.Add(this.groupControl15);
            this.groupControl5.Controls.Add(this.groupControl14);
            this.groupControl5.Controls.Add(this.groupControl13);
            this.groupControl5.Controls.Add(this.groupControl12);
            this.groupControl5.Controls.Add(this.groupControl11);
            this.groupControl5.Controls.Add(this.groupControl10);
            this.groupControl5.Controls.Add(this.chkBInitialisationRemise);
            this.groupControl5.Location = new System.Drawing.Point(9, 5);
            this.groupControl5.Name = "groupControl5";
            this.groupControl5.Size = new System.Drawing.Size(974, 465);
            this.groupControl5.TabIndex = 0;
            this.groupControl5.Text = "Taxe";
            // 
            // groupControl16
            // 
            this.groupControl16.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl16.AppearanceCaption.ForeColor = System.Drawing.Color.MidnightBlue;
            this.groupControl16.AppearanceCaption.Options.UseFont = true;
            this.groupControl16.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl16.Controls.Add(this.txtDateFinExoTPE);
            this.groupControl16.Controls.Add(this.labelControl30);
            this.groupControl16.Controls.Add(this.radioTPEExonere);
            this.groupControl16.Controls.Add(this.radioTPENonExonere);
            this.groupControl16.Location = new System.Drawing.Point(8, 128);
            this.groupControl16.Name = "groupControl16";
            this.groupControl16.Size = new System.Drawing.Size(961, 45);
            this.groupControl16.TabIndex = 2;
            this.groupControl16.Text = "Exonération TPE";
            // 
            // txtDateFinExoTPE
            // 
            this.txtDateFinExoTPE.EditValue = null;
            this.txtDateFinExoTPE.EnterMoveNextControl = true;
            this.txtDateFinExoTPE.Location = new System.Drawing.Point(606, 21);
            this.txtDateFinExoTPE.Name = "txtDateFinExoTPE";
            this.txtDateFinExoTPE.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDateFinExoTPE.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDateFinExoTPE.Size = new System.Drawing.Size(189, 20);
            this.txtDateFinExoTPE.TabIndex = 3;
            this.txtDateFinExoTPE.Tag = "";
            // 
            // labelControl30
            // 
            this.labelControl30.Location = new System.Drawing.Point(510, 24);
            this.labelControl30.Name = "labelControl30";
            this.labelControl30.Size = new System.Drawing.Size(93, 13);
            this.labelControl30.TabIndex = 2;
            this.labelControl30.Text = "Date Fin Exo. TPE :";
            // 
            // radioTPEExonere
            // 
            this.radioTPEExonere.AutoSize = true;
            this.radioTPEExonere.Location = new System.Drawing.Point(263, 24);
            this.radioTPEExonere.Name = "radioTPEExonere";
            this.radioTPEExonere.Size = new System.Drawing.Size(65, 17);
            this.radioTPEExonere.TabIndex = 1;
            this.radioTPEExonere.Text = "Exonéré";
            this.radioTPEExonere.UseVisualStyleBackColor = true;
            // 
            // radioTPENonExonere
            // 
            this.radioTPENonExonere.AutoSize = true;
            this.radioTPENonExonere.Checked = true;
            this.radioTPENonExonere.Location = new System.Drawing.Point(160, 24);
            this.radioTPENonExonere.Name = "radioTPENonExonere";
            this.radioTPENonExonere.Size = new System.Drawing.Size(87, 17);
            this.radioTPENonExonere.TabIndex = 0;
            this.radioTPENonExonere.TabStop = true;
            this.radioTPENonExonere.Text = "Non Exonéré";
            this.radioTPENonExonere.UseVisualStyleBackColor = true;
            this.radioTPENonExonere.CheckedChanged += new System.EventHandler(this.radioTPENonExonere_CheckedChanged);
            // 
            // groupControl17
            // 
            this.groupControl17.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl17.AppearanceCaption.ForeColor = System.Drawing.Color.MidnightBlue;
            this.groupControl17.AppearanceCaption.Options.UseFont = true;
            this.groupControl17.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl17.Controls.Add(this.txtDateFinExoTDC);
            this.groupControl17.Controls.Add(this.labelControl32);
            this.groupControl17.Controls.Add(this.radioExoTDC);
            this.groupControl17.Controls.Add(this.radioNonExoTDC);
            this.groupControl17.Location = new System.Drawing.Point(8, 177);
            this.groupControl17.Name = "groupControl17";
            this.groupControl17.Size = new System.Drawing.Size(961, 45);
            this.groupControl17.TabIndex = 3;
            this.groupControl17.Text = "Exonération Droits de consommation";
            // 
            // txtDateFinExoTDC
            // 
            this.txtDateFinExoTDC.EditValue = null;
            this.txtDateFinExoTDC.EnterMoveNextControl = true;
            this.txtDateFinExoTDC.Location = new System.Drawing.Point(606, 21);
            this.txtDateFinExoTDC.Name = "txtDateFinExoTDC";
            this.txtDateFinExoTDC.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDateFinExoTDC.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDateFinExoTDC.Size = new System.Drawing.Size(189, 20);
            this.txtDateFinExoTDC.TabIndex = 3;
            this.txtDateFinExoTDC.Tag = "";
            // 
            // labelControl32
            // 
            this.labelControl32.Location = new System.Drawing.Point(508, 24);
            this.labelControl32.Name = "labelControl32";
            this.labelControl32.Size = new System.Drawing.Size(95, 13);
            this.labelControl32.TabIndex = 2;
            this.labelControl32.Text = "Date Fin Exo. TDC :";
            // 
            // radioExoTDC
            // 
            this.radioExoTDC.AutoSize = true;
            this.radioExoTDC.Location = new System.Drawing.Point(263, 24);
            this.radioExoTDC.Name = "radioExoTDC";
            this.radioExoTDC.Size = new System.Drawing.Size(65, 17);
            this.radioExoTDC.TabIndex = 1;
            this.radioExoTDC.Text = "Exonéré";
            this.radioExoTDC.UseVisualStyleBackColor = true;
            // 
            // radioNonExoTDC
            // 
            this.radioNonExoTDC.AutoSize = true;
            this.radioNonExoTDC.Checked = true;
            this.radioNonExoTDC.Location = new System.Drawing.Point(160, 24);
            this.radioNonExoTDC.Name = "radioNonExoTDC";
            this.radioNonExoTDC.Size = new System.Drawing.Size(87, 17);
            this.radioNonExoTDC.TabIndex = 0;
            this.radioNonExoTDC.TabStop = true;
            this.radioNonExoTDC.Text = "Non Exonéré";
            this.radioNonExoTDC.UseVisualStyleBackColor = true;
            this.radioNonExoTDC.CheckedChanged += new System.EventHandler(this.radioNonExoTDC_CheckedChanged);
            // 
            // groupCAutorisation
            // 
            this.groupCAutorisation.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupCAutorisation.AppearanceCaption.ForeColor = System.Drawing.Color.MidnightBlue;
            this.groupCAutorisation.AppearanceCaption.Options.UseFont = true;
            this.groupCAutorisation.AppearanceCaption.Options.UseForeColor = true;
            this.groupCAutorisation.Controls.Add(this.labelControl28);
            this.groupCAutorisation.Controls.Add(this.txtNAutorisation);
            this.groupCAutorisation.Controls.Add(this.txtDateDebSusp);
            this.groupCAutorisation.Controls.Add(this.labelControl25);
            this.groupCAutorisation.Controls.Add(this.txtDateFinSusp);
            this.groupCAutorisation.Controls.Add(this.labelControl23);
            this.groupCAutorisation.Location = new System.Drawing.Point(403, 298);
            this.groupCAutorisation.Name = "groupCAutorisation";
            this.groupCAutorisation.Size = new System.Drawing.Size(566, 79);
            this.groupCAutorisation.TabIndex = 6;
            this.groupCAutorisation.Text = "Autorisation de Suspension";
            // 
            // labelControl28
            // 
            this.labelControl28.Location = new System.Drawing.Point(38, 28);
            this.labelControl28.Name = "labelControl28";
            this.labelControl28.Size = new System.Drawing.Size(80, 13);
            this.labelControl28.TabIndex = 34;
            this.labelControl28.Text = "N° Autorisation :";
            // 
            // txtNAutorisation
            // 
            this.txtNAutorisation.Enabled = false;
            this.txtNAutorisation.EnterMoveNextControl = true;
            this.txtNAutorisation.Location = new System.Drawing.Point(121, 25);
            this.txtNAutorisation.Name = "txtNAutorisation";
            this.txtNAutorisation.Properties.MaxLength = 20;
            this.txtNAutorisation.Size = new System.Drawing.Size(128, 20);
            this.txtNAutorisation.TabIndex = 0;
            this.txtNAutorisation.Tag = "";
            // 
            // txtDateDebSusp
            // 
            this.txtDateDebSusp.EditValue = null;
            this.txtDateDebSusp.Enabled = false;
            this.txtDateDebSusp.EnterMoveNextControl = true;
            this.txtDateDebSusp.Location = new System.Drawing.Point(121, 53);
            this.txtDateDebSusp.Name = "txtDateDebSusp";
            this.txtDateDebSusp.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDateDebSusp.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDateDebSusp.Size = new System.Drawing.Size(127, 20);
            this.txtDateDebSusp.TabIndex = 1;
            this.txtDateDebSusp.Tag = "";
            // 
            // labelControl25
            // 
            this.labelControl25.Location = new System.Drawing.Point(250, 56);
            this.labelControl25.Name = "labelControl25";
            this.labelControl25.Size = new System.Drawing.Size(23, 13);
            this.labelControl25.TabIndex = 2;
            this.labelControl25.Text = "Au : ";
            // 
            // txtDateFinSusp
            // 
            this.txtDateFinSusp.EditValue = null;
            this.txtDateFinSusp.Enabled = false;
            this.txtDateFinSusp.EnterMoveNextControl = true;
            this.txtDateFinSusp.Location = new System.Drawing.Point(274, 53);
            this.txtDateFinSusp.Name = "txtDateFinSusp";
            this.txtDateFinSusp.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDateFinSusp.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDateFinSusp.Size = new System.Drawing.Size(127, 20);
            this.txtDateFinSusp.TabIndex = 2;
            this.txtDateFinSusp.Tag = "";
            // 
            // labelControl23
            // 
            this.labelControl23.Location = new System.Drawing.Point(11, 56);
            this.labelControl23.Name = "labelControl23";
            this.labelControl23.Size = new System.Drawing.Size(107, 13);
            this.labelControl23.TabIndex = 40;
            this.labelControl23.Text = "Date Autorisation Du :";
            // 
            // chkBTransfertCompta
            // 
            this.chkBTransfertCompta.EnterMoveNextControl = true;
            this.chkBTransfertCompta.Location = new System.Drawing.Point(484, 383);
            this.chkBTransfertCompta.Name = "chkBTransfertCompta";
            this.chkBTransfertCompta.Properties.Caption = "Transférable vers la Comptabilité";
            this.chkBTransfertCompta.Size = new System.Drawing.Size(183, 19);
            this.chkBTransfertCompta.TabIndex = 8;
            // 
            // groupControl15
            // 
            this.groupControl15.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl15.AppearanceCaption.ForeColor = System.Drawing.Color.MidnightBlue;
            this.groupControl15.AppearanceCaption.Options.UseFont = true;
            this.groupControl15.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl15.Controls.Add(this.txtTauxRetenuSource);
            this.groupControl15.Controls.Add(this.txtTauxRetenuTVA);
            this.groupControl15.Controls.Add(this.labelControl33);
            this.groupControl15.Controls.Add(this.labelControl34);
            this.groupControl15.Location = new System.Drawing.Point(8, 406);
            this.groupControl15.Name = "groupControl15";
            this.groupControl15.Size = new System.Drawing.Size(961, 51);
            this.groupControl15.TabIndex = 10;
            this.groupControl15.Text = "Retenue";
            // 
            // txtTauxRetenuSource
            // 
            this.txtTauxRetenuSource.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtTauxRetenuSource.EnterMoveNextControl = true;
            this.txtTauxRetenuSource.Location = new System.Drawing.Point(152, 25);
            this.txtTauxRetenuSource.Name = "txtTauxRetenuSource";
            this.txtTauxRetenuSource.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtTauxRetenuSource.Properties.Mask.EditMask = "#,##0.00;";
            this.txtTauxRetenuSource.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtTauxRetenuSource.Size = new System.Drawing.Size(189, 20);
            this.txtTauxRetenuSource.TabIndex = 0;
            // 
            // txtTauxRetenuTVA
            // 
            this.txtTauxRetenuTVA.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtTauxRetenuTVA.EnterMoveNextControl = true;
            this.txtTauxRetenuTVA.Location = new System.Drawing.Point(606, 25);
            this.txtTauxRetenuTVA.Name = "txtTauxRetenuTVA";
            this.txtTauxRetenuTVA.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtTauxRetenuTVA.Properties.Mask.EditMask = "#,##0.00;";
            this.txtTauxRetenuTVA.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtTauxRetenuTVA.Size = new System.Drawing.Size(189, 20);
            this.txtTauxRetenuTVA.TabIndex = 1;
            // 
            // labelControl33
            // 
            this.labelControl33.Location = new System.Drawing.Point(486, 28);
            this.labelControl33.Name = "labelControl33";
            this.labelControl33.Size = new System.Drawing.Size(117, 13);
            this.labelControl33.TabIndex = 3;
            this.labelControl33.Text = "Taux Retenue à la TVA :";
            // 
            // labelControl34
            // 
            this.labelControl34.Location = new System.Drawing.Point(18, 28);
            this.labelControl34.Name = "labelControl34";
            this.labelControl34.Size = new System.Drawing.Size(131, 13);
            this.labelControl34.TabIndex = 1;
            this.labelControl34.Text = "Taux Retenue à la Source :";
            // 
            // groupControl14
            // 
            this.groupControl14.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl14.AppearanceCaption.ForeColor = System.Drawing.Color.MidnightBlue;
            this.groupControl14.AppearanceCaption.Options.UseFont = true;
            this.groupControl14.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl14.Controls.Add(this.radioTimbreExonore);
            this.groupControl14.Controls.Add(this.radioTimbreNonExonore);
            this.groupControl14.Location = new System.Drawing.Point(8, 358);
            this.groupControl14.Name = "groupControl14";
            this.groupControl14.Size = new System.Drawing.Size(393, 46);
            this.groupControl14.TabIndex = 7;
            this.groupControl14.Text = "Timbre Fiscal";
            // 
            // radioTimbreExonore
            // 
            this.radioTimbreExonore.AutoSize = true;
            this.radioTimbreExonore.Location = new System.Drawing.Point(263, 24);
            this.radioTimbreExonore.Name = "radioTimbreExonore";
            this.radioTimbreExonore.Size = new System.Drawing.Size(65, 17);
            this.radioTimbreExonore.TabIndex = 1;
            this.radioTimbreExonore.Text = "Exonéré";
            this.radioTimbreExonore.UseVisualStyleBackColor = true;
            // 
            // radioTimbreNonExonore
            // 
            this.radioTimbreNonExonore.AutoSize = true;
            this.radioTimbreNonExonore.Checked = true;
            this.radioTimbreNonExonore.Location = new System.Drawing.Point(160, 24);
            this.radioTimbreNonExonore.Name = "radioTimbreNonExonore";
            this.radioTimbreNonExonore.Size = new System.Drawing.Size(87, 17);
            this.radioTimbreNonExonore.TabIndex = 0;
            this.radioTimbreNonExonore.TabStop = true;
            this.radioTimbreNonExonore.Text = "Non Exonéré";
            this.radioTimbreNonExonore.UseVisualStyleBackColor = true;
            // 
            // groupControl13
            // 
            this.groupControl13.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl13.AppearanceCaption.ForeColor = System.Drawing.Color.MidnightBlue;
            this.groupControl13.AppearanceCaption.Options.UseFont = true;
            this.groupControl13.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl13.Controls.Add(this.radioSuspension);
            this.groupControl13.Controls.Add(this.radioExport);
            this.groupControl13.Controls.Add(this.radioLocale);
            this.groupControl13.Location = new System.Drawing.Point(8, 298);
            this.groupControl13.Name = "groupControl13";
            this.groupControl13.Size = new System.Drawing.Size(393, 55);
            this.groupControl13.TabIndex = 5;
            this.groupControl13.Text = "Nature Client";
            // 
            // radioSuspension
            // 
            this.radioSuspension.AutoSize = true;
            this.radioSuspension.Location = new System.Drawing.Point(263, 29);
            this.radioSuspension.Name = "radioSuspension";
            this.radioSuspension.Size = new System.Drawing.Size(78, 17);
            this.radioSuspension.TabIndex = 2;
            this.radioSuspension.Text = "suspension";
            this.radioSuspension.UseVisualStyleBackColor = true;
            this.radioSuspension.CheckedChanged += new System.EventHandler(this.radioSuspension_CheckedChanged);
            // 
            // radioExport
            // 
            this.radioExport.AutoSize = true;
            this.radioExport.Location = new System.Drawing.Point(160, 29);
            this.radioExport.Name = "radioExport";
            this.radioExport.Size = new System.Drawing.Size(57, 17);
            this.radioExport.TabIndex = 1;
            this.radioExport.Text = "Export";
            this.radioExport.UseVisualStyleBackColor = true;
            // 
            // radioLocale
            // 
            this.radioLocale.AutoSize = true;
            this.radioLocale.Checked = true;
            this.radioLocale.Location = new System.Drawing.Point(90, 29);
            this.radioLocale.Name = "radioLocale";
            this.radioLocale.Size = new System.Drawing.Size(49, 17);
            this.radioLocale.TabIndex = 0;
            this.radioLocale.TabStop = true;
            this.radioLocale.Text = "Local";
            this.radioLocale.UseVisualStyleBackColor = true;
            // 
            // groupControl12
            // 
            this.groupControl12.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl12.AppearanceCaption.ForeColor = System.Drawing.Color.MidnightBlue;
            this.groupControl12.AppearanceCaption.Options.UseFont = true;
            this.groupControl12.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl12.Controls.Add(this.txtMontantExonoreTVA);
            this.groupControl12.Controls.Add(this.txtDateFinExoTVA);
            this.groupControl12.Controls.Add(this.labelControl26);
            this.groupControl12.Controls.Add(this.labelControl27);
            this.groupControl12.Controls.Add(this.radioTVAExonore);
            this.groupControl12.Controls.Add(this.radioTVANonExonore);
            this.groupControl12.Location = new System.Drawing.Point(8, 225);
            this.groupControl12.Name = "groupControl12";
            this.groupControl12.Size = new System.Drawing.Size(961, 71);
            this.groupControl12.TabIndex = 4;
            this.groupControl12.Text = "Exonération TVA";
            // 
            // txtMontantExonoreTVA
            // 
            this.txtMontantExonoreTVA.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtMontantExonoreTVA.EnterMoveNextControl = true;
            this.txtMontantExonoreTVA.Location = new System.Drawing.Point(606, 47);
            this.txtMontantExonoreTVA.Name = "txtMontantExonoreTVA";
            this.txtMontantExonoreTVA.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtMontantExonoreTVA.Properties.EditFormat.FormatString = "c";
            this.txtMontantExonoreTVA.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtMontantExonoreTVA.Properties.Mask.EditMask = "#,###0.000;";
            this.txtMontantExonoreTVA.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtMontantExonoreTVA.Size = new System.Drawing.Size(189, 20);
            this.txtMontantExonoreTVA.TabIndex = 3;
            // 
            // txtDateFinExoTVA
            // 
            this.txtDateFinExoTVA.EditValue = null;
            this.txtDateFinExoTVA.EnterMoveNextControl = true;
            this.txtDateFinExoTVA.Location = new System.Drawing.Point(606, 24);
            this.txtDateFinExoTVA.Name = "txtDateFinExoTVA";
            this.txtDateFinExoTVA.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDateFinExoTVA.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDateFinExoTVA.Size = new System.Drawing.Size(189, 20);
            this.txtDateFinExoTVA.TabIndex = 2;
            this.txtDateFinExoTVA.Tag = "";
            // 
            // labelControl26
            // 
            this.labelControl26.Location = new System.Drawing.Point(509, 51);
            this.labelControl26.Name = "labelControl26";
            this.labelControl26.Size = new System.Drawing.Size(94, 13);
            this.labelControl26.TabIndex = 39;
            this.labelControl26.Text = "Montant Exo. TVA :";
            // 
            // labelControl27
            // 
            this.labelControl27.Location = new System.Drawing.Point(509, 27);
            this.labelControl27.Name = "labelControl27";
            this.labelControl27.Size = new System.Drawing.Size(94, 13);
            this.labelControl27.TabIndex = 38;
            this.labelControl27.Text = "Date Fin Exo. TVA :";
            // 
            // radioTVAExonore
            // 
            this.radioTVAExonore.AutoSize = true;
            this.radioTVAExonore.Location = new System.Drawing.Point(263, 37);
            this.radioTVAExonore.Name = "radioTVAExonore";
            this.radioTVAExonore.Size = new System.Drawing.Size(65, 17);
            this.radioTVAExonore.TabIndex = 1;
            this.radioTVAExonore.Text = "Exonéré";
            this.radioTVAExonore.UseVisualStyleBackColor = true;
            // 
            // radioTVANonExonore
            // 
            this.radioTVANonExonore.AutoSize = true;
            this.radioTVANonExonore.Checked = true;
            this.radioTVANonExonore.Location = new System.Drawing.Point(160, 37);
            this.radioTVANonExonore.Name = "radioTVANonExonore";
            this.radioTVANonExonore.Size = new System.Drawing.Size(87, 17);
            this.radioTVANonExonore.TabIndex = 0;
            this.radioTVANonExonore.TabStop = true;
            this.radioTVANonExonore.Text = "Non Exonéré";
            this.radioTVANonExonore.UseVisualStyleBackColor = true;
            this.radioTVANonExonore.Click += new System.EventHandler(this.radioTVANonExonore_Click);
            // 
            // groupControl11
            // 
            this.groupControl11.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl11.AppearanceCaption.ForeColor = System.Drawing.Color.MidnightBlue;
            this.groupControl11.AppearanceCaption.Options.UseFont = true;
            this.groupControl11.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl11.Controls.Add(this.txtDateFinExoFodec);
            this.groupControl11.Controls.Add(this.labelControl31);
            this.groupControl11.Controls.Add(this.radioFodecExonore);
            this.groupControl11.Controls.Add(this.radioFodecNonExonore);
            this.groupControl11.Location = new System.Drawing.Point(8, 79);
            this.groupControl11.Name = "groupControl11";
            this.groupControl11.Size = new System.Drawing.Size(961, 45);
            this.groupControl11.TabIndex = 1;
            this.groupControl11.Text = "Exonération Fodec";
            // 
            // txtDateFinExoFodec
            // 
            this.txtDateFinExoFodec.EditValue = null;
            this.txtDateFinExoFodec.EnterMoveNextControl = true;
            this.txtDateFinExoFodec.Location = new System.Drawing.Point(606, 21);
            this.txtDateFinExoFodec.Name = "txtDateFinExoFodec";
            this.txtDateFinExoFodec.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDateFinExoFodec.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDateFinExoFodec.Size = new System.Drawing.Size(189, 20);
            this.txtDateFinExoFodec.TabIndex = 3;
            this.txtDateFinExoFodec.Tag = "";
            // 
            // labelControl31
            // 
            this.labelControl31.Location = new System.Drawing.Point(499, 24);
            this.labelControl31.Name = "labelControl31";
            this.labelControl31.Size = new System.Drawing.Size(104, 13);
            this.labelControl31.TabIndex = 2;
            this.labelControl31.Text = "Date Fin Exo. Fodec :";
            // 
            // radioFodecExonore
            // 
            this.radioFodecExonore.AutoSize = true;
            this.radioFodecExonore.Location = new System.Drawing.Point(263, 24);
            this.radioFodecExonore.Name = "radioFodecExonore";
            this.radioFodecExonore.Size = new System.Drawing.Size(65, 17);
            this.radioFodecExonore.TabIndex = 1;
            this.radioFodecExonore.Text = "Exonéré";
            this.radioFodecExonore.UseVisualStyleBackColor = true;
            // 
            // radioFodecNonExonore
            // 
            this.radioFodecNonExonore.AutoSize = true;
            this.radioFodecNonExonore.Checked = true;
            this.radioFodecNonExonore.Location = new System.Drawing.Point(160, 24);
            this.radioFodecNonExonore.Name = "radioFodecNonExonore";
            this.radioFodecNonExonore.Size = new System.Drawing.Size(87, 17);
            this.radioFodecNonExonore.TabIndex = 0;
            this.radioFodecNonExonore.TabStop = true;
            this.radioFodecNonExonore.Text = "Non Exonéré";
            this.radioFodecNonExonore.UseVisualStyleBackColor = true;
            this.radioFodecNonExonore.Click += new System.EventHandler(this.radioFodecNonExonore_Click);
            // 
            // groupControl10
            // 
            this.groupControl10.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl10.AppearanceCaption.ForeColor = System.Drawing.Color.MidnightBlue;
            this.groupControl10.AppearanceCaption.Options.UseFont = true;
            this.groupControl10.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl10.Controls.Add(this.txtnumetablissement);
            this.groupControl10.Controls.Add(this.lkpcodecateg);
            this.groupControl10.Controls.Add(this.lkpcodetva);
            this.groupControl10.Controls.Add(this.lkpcle);
            this.groupControl10.Controls.Add(this.labelControl29);
            this.groupControl10.Controls.Add(this.chkBAvanceForfaitaire);
            this.groupControl10.Controls.Add(this.radioNonMajore);
            this.groupControl10.Controls.Add(this.radioMajore);
            this.groupControl10.Controls.Add(this.txtCTVA);
            this.groupControl10.Location = new System.Drawing.Point(8, 25);
            this.groupControl10.Name = "groupControl10";
            this.groupControl10.Size = new System.Drawing.Size(961, 51);
            this.groupControl10.TabIndex = 0;
            this.groupControl10.Text = "Identification Fiscale";
            // 
            // txtnumetablissement
            // 
            this.txtnumetablissement.EditValue = "000";
            this.txtnumetablissement.Enabled = false;
            this.txtnumetablissement.EnterMoveNextControl = true;
            this.txtnumetablissement.Location = new System.Drawing.Point(298, 26);
            this.txtnumetablissement.Name = "txtnumetablissement";
            this.txtnumetablissement.Properties.MaxLength = 3;
            this.txtnumetablissement.Size = new System.Drawing.Size(26, 20);
            this.txtnumetablissement.TabIndex = 36;
            this.txtnumetablissement.Tag = "";
            this.txtnumetablissement.Validated += new System.EventHandler(this.txtnumetablissement_Validated);
            // 
            // lkpcodecateg
            // 
            this.lkpcodecateg.EditValue = "";
            this.lkpcodecateg.EnterMoveNextControl = true;
            this.lkpcodecateg.Location = new System.Drawing.Point(249, 26);
            this.lkpcodecateg.Name = "lkpcodecateg";
            this.lkpcodecateg.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpcodecateg.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name75", "Name75")});
            this.lkpcodecateg.Size = new System.Drawing.Size(46, 20);
            this.lkpcodecateg.TabIndex = 35;
            this.lkpcodecateg.Tag = "";
            this.lkpcodecateg.EditValueChanged += new System.EventHandler(this.lkpcodecateg_EditValueChanged);
            // 
            // lkpcodetva
            // 
            this.lkpcodetva.EditValue = "";
            this.lkpcodetva.EnterMoveNextControl = true;
            this.lkpcodetva.Location = new System.Drawing.Point(200, 26);
            this.lkpcodetva.Name = "lkpcodetva";
            this.lkpcodetva.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpcodetva.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name75", "Name75")});
            this.lkpcodetva.Size = new System.Drawing.Size(46, 20);
            this.lkpcodetva.TabIndex = 34;
            this.lkpcodetva.Tag = "";
            // 
            // lkpcle
            // 
            this.lkpcle.EditValue = "";
            this.lkpcle.EnterMoveNextControl = true;
            this.lkpcle.Location = new System.Drawing.Point(152, 26);
            this.lkpcle.Name = "lkpcle";
            this.lkpcle.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpcle.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name75", "Name75")});
            this.lkpcle.Size = new System.Drawing.Size(46, 20);
            this.lkpcle.TabIndex = 33;
            this.lkpcle.Tag = "";
            // 
            // labelControl29
            // 
            this.labelControl29.Location = new System.Drawing.Point(9, 29);
            this.labelControl29.Name = "labelControl29";
            this.labelControl29.Size = new System.Drawing.Size(77, 13);
            this.labelControl29.TabIndex = 32;
            this.labelControl29.Text = "Matricule fiscal :";
            // 
            // chkBAvanceForfaitaire
            // 
            this.chkBAvanceForfaitaire.EnterMoveNextControl = true;
            this.chkBAvanceForfaitaire.Location = new System.Drawing.Point(690, 26);
            this.chkBAvanceForfaitaire.Name = "chkBAvanceForfaitaire";
            this.chkBAvanceForfaitaire.Properties.Caption = "Régime Forfaitaire";
            this.chkBAvanceForfaitaire.Size = new System.Drawing.Size(115, 19);
            this.chkBAvanceForfaitaire.TabIndex = 3;
            // 
            // radioNonMajore
            // 
            this.radioNonMajore.AutoSize = true;
            this.radioNonMajore.Checked = true;
            this.radioNonMajore.Location = new System.Drawing.Point(454, 29);
            this.radioNonMajore.Name = "radioNonMajore";
            this.radioNonMajore.Size = new System.Drawing.Size(80, 17);
            this.radioNonMajore.TabIndex = 1;
            this.radioNonMajore.TabStop = true;
            this.radioNonMajore.Text = "Non Majoré";
            this.radioNonMajore.UseVisualStyleBackColor = true;
            this.radioNonMajore.Visible = false;
            // 
            // radioMajore
            // 
            this.radioMajore.AutoSize = true;
            this.radioMajore.Location = new System.Drawing.Point(559, 29);
            this.radioMajore.Name = "radioMajore";
            this.radioMajore.Size = new System.Drawing.Size(58, 17);
            this.radioMajore.TabIndex = 2;
            this.radioMajore.Text = "Majoré";
            this.radioMajore.UseVisualStyleBackColor = true;
            this.radioMajore.Visible = false;
            // 
            // txtCTVA
            // 
            this.txtCTVA.EditValue = "";
            this.txtCTVA.EnterMoveNextControl = true;
            this.txtCTVA.Location = new System.Drawing.Point(89, 26);
            this.txtCTVA.Name = "txtCTVA";
            this.txtCTVA.Properties.MaxLength = 7;
            this.txtCTVA.Size = new System.Drawing.Size(58, 20);
            this.txtCTVA.TabIndex = 0;
            this.txtCTVA.Tag = "";
            this.txtCTVA.Validated += new System.EventHandler(this.txtCTVA_Validated);
            // 
            // chkBInitialisationRemise
            // 
            this.chkBInitialisationRemise.EnterMoveNextControl = true;
            this.chkBInitialisationRemise.Location = new System.Drawing.Point(677, 383);
            this.chkBInitialisationRemise.Name = "chkBInitialisationRemise";
            this.chkBInitialisationRemise.Properties.Caption = "Initialisation de remise après validation";
            this.chkBInitialisationRemise.Size = new System.Drawing.Size(208, 19);
            this.chkBInitialisationRemise.TabIndex = 9;
            // 
            // panelControl15
            // 
            this.panelControl15.Location = new System.Drawing.Point(783, 146);
            this.panelControl15.Name = "panelControl15";
            this.panelControl15.Size = new System.Drawing.Size(200, 100);
            this.panelControl15.TabIndex = 3;
            // 
            // tabBanque
            // 
            this.tabBanque.Appearance.Header.Options.UseTextOptions = true;
            this.tabBanque.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tabBanque.Appearance.PageClient.BackColor = System.Drawing.Color.Silver;
            this.tabBanque.Appearance.PageClient.Options.UseBackColor = true;
            this.tabBanque.Controls.Add(this.panelControl14);
            this.tabBanque.Name = "tabBanque";
            this.tabBanque.Size = new System.Drawing.Size(988, 658);
            this.tabBanque.TabPageWidth = 170;
            this.tabBanque.Text = "Banques";
            // 
            // panelControl14
            // 
            this.panelControl14.Controls.Add(this.panelControl18);
            this.panelControl14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl14.Location = new System.Drawing.Point(0, 0);
            this.panelControl14.Name = "panelControl14";
            this.panelControl14.Size = new System.Drawing.Size(988, 658);
            this.panelControl14.TabIndex = 0;
            // 
            // panelControl18
            // 
            this.panelControl18.Controls.Add(this.gridClientBanque);
            this.panelControl18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl18.Location = new System.Drawing.Point(2, 2);
            this.panelControl18.Name = "panelControl18";
            this.panelControl18.Size = new System.Drawing.Size(984, 654);
            this.panelControl18.TabIndex = 0;
            // 
            // gridClientBanque
            // 
            this.gridClientBanque.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridClientBanque.Location = new System.Drawing.Point(2, 2);
            this.gridClientBanque.MainView = this.gridVClientBanque;
            this.gridClientBanque.Name = "gridClientBanque";
            this.gridClientBanque.Size = new System.Drawing.Size(980, 650);
            this.gridClientBanque.TabIndex = 38;
            this.gridClientBanque.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridVClientBanque});
            // 
            // gridVClientBanque
            // 
            this.gridVClientBanque.GridControl = this.gridClientBanque;
            this.gridVClientBanque.Name = "gridVClientBanque";
            this.gridVClientBanque.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.gridVClientBanque_ValidateRow);
            this.gridVClientBanque.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridVClientBanque_KeyDown);
            // 
            // tabContacts
            // 
            this.tabContacts.Appearance.Header.Options.UseTextOptions = true;
            this.tabContacts.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tabContacts.Controls.Add(this.panelControl21);
            this.tabContacts.Name = "tabContacts";
            this.tabContacts.Size = new System.Drawing.Size(988, 658);
            this.tabContacts.TabPageWidth = 170;
            this.tabContacts.Text = "Contacts";
            // 
            // panelControl21
            // 
            this.panelControl21.Controls.Add(this.panelControl22);
            this.panelControl21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl21.Location = new System.Drawing.Point(0, 0);
            this.panelControl21.Name = "panelControl21";
            this.panelControl21.Size = new System.Drawing.Size(988, 658);
            this.panelControl21.TabIndex = 0;
            // 
            // panelControl22
            // 
            this.panelControl22.Controls.Add(this.gridClientContact);
            this.panelControl22.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl22.Location = new System.Drawing.Point(2, 2);
            this.panelControl22.Name = "panelControl22";
            this.panelControl22.Size = new System.Drawing.Size(984, 654);
            this.panelControl22.TabIndex = 0;
            // 
            // gridClientContact
            // 
            this.gridClientContact.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridClientContact.Location = new System.Drawing.Point(2, 2);
            this.gridClientContact.MainView = this.gridVClientContact;
            this.gridClientContact.Name = "gridClientContact";
            this.gridClientContact.Size = new System.Drawing.Size(980, 650);
            this.gridClientContact.TabIndex = 38;
            this.gridClientContact.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridVClientContact});
            // 
            // gridVClientContact
            // 
            this.gridVClientContact.GridControl = this.gridClientContact;
            this.gridVClientContact.Name = "gridVClientContact";
            this.gridVClientContact.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.gridVClientContact_ValidateRow);
            this.gridVClientContact.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridVClientContact_KeyDown);
            // 
            // Etablissements
            // 
            this.Etablissements.Controls.Add(this.panelControl2);
            this.Etablissements.Name = "Etablissements";
            this.Etablissements.PageVisible = false;
            this.Etablissements.Size = new System.Drawing.Size(988, 658);
            this.Etablissements.Text = "Etablissements";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.groupControl20);
            this.panelControl2.Controls.Add(this.groupControl19);
            this.panelControl2.Location = new System.Drawing.Point(3, 3);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(982, 652);
            this.panelControl2.TabIndex = 0;
            // 
            // groupControl20
            // 
            this.groupControl20.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl20.AppearanceCaption.ForeColor = System.Drawing.Color.MidnightBlue;
            this.groupControl20.AppearanceCaption.Options.UseFont = true;
            this.groupControl20.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl20.Controls.Add(this.gridCEtab);
            this.groupControl20.Location = new System.Drawing.Point(0, 138);
            this.groupControl20.Name = "groupControl20";
            this.groupControl20.Size = new System.Drawing.Size(982, 514);
            this.groupControl20.TabIndex = 5;
            this.groupControl20.Text = "Liste";
            // 
            // gridCEtab
            // 
            this.gridCEtab.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridCEtab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCEtab.Location = new System.Drawing.Point(2, 21);
            this.gridCEtab.MainView = this.gridVEtab;
            this.gridCEtab.Name = "gridCEtab";
            this.gridCEtab.Size = new System.Drawing.Size(978, 491);
            this.gridCEtab.TabIndex = 0;
            this.gridCEtab.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridVEtab});
            // 
            // gridVEtab
            // 
            this.gridVEtab.GridControl = this.gridCEtab;
            this.gridVEtab.Name = "gridVEtab";
            this.gridVEtab.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridVEtab_RowClick);
            // 
            // groupControl19
            // 
            this.groupControl19.Controls.Add(this.simpleButton2);
            this.groupControl19.Controls.Add(this.txtlongEtab);
            this.groupControl19.Controls.Add(this.txtlatEtab);
            this.groupControl19.Controls.Add(this.labelControl51);
            this.groupControl19.Controls.Add(this.labelControl52);
            this.groupControl19.Controls.Add(this.labelControl49);
            this.groupControl19.Controls.Add(this.labelControl50);
            this.groupControl19.Controls.Add(this.txtCodePostalEtab);
            this.groupControl19.Controls.Add(this.txtVilleEtab);
            this.groupControl19.Controls.Add(this.txtadresseEtab);
            this.groupControl19.Controls.Add(this.labelControl48);
            this.groupControl19.Controls.Add(this.txtCodeEtab);
            this.groupControl19.Controls.Add(this.labelControl53);
            this.groupControl19.Controls.Add(this.simpleButton3);
            this.groupControl19.Controls.Add(this.labelControl54);
            this.groupControl19.Controls.Add(this.lkpRegionEtab);
            this.groupControl19.Controls.Add(this.txtLibEtab);
            this.groupControl19.Controls.Add(this.labelControl55);
            this.groupControl19.Location = new System.Drawing.Point(0, 5);
            this.groupControl19.Name = "groupControl19";
            this.groupControl19.Size = new System.Drawing.Size(977, 127);
            this.groupControl19.TabIndex = 4;
            this.groupControl19.Text = "Saisie";
            // 
            // simpleButton2
            // 
            this.simpleButton2.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton2.Image")));
            this.simpleButton2.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleRight;
            this.simpleButton2.Location = new System.Drawing.Point(844, 80);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(90, 23);
            this.simpleButton2.TabIndex = 66;
            this.simpleButton2.Text = "Supprimer";
            this.simpleButton2.Visible = false;
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // txtlongEtab
            // 
            this.txtlongEtab.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtlongEtab.Location = new System.Drawing.Point(651, 92);
            this.txtlongEtab.Name = "txtlongEtab";
            this.txtlongEtab.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.txtlongEtab.Properties.Appearance.Options.UseBackColor = true;
            this.txtlongEtab.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtlongEtab.Size = new System.Drawing.Size(139, 20);
            this.txtlongEtab.TabIndex = 65;
            // 
            // txtlatEtab
            // 
            this.txtlatEtab.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtlatEtab.Location = new System.Drawing.Point(422, 91);
            this.txtlatEtab.Name = "txtlatEtab";
            this.txtlatEtab.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.txtlatEtab.Properties.Appearance.Options.UseBackColor = true;
            this.txtlatEtab.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtlatEtab.Size = new System.Drawing.Size(143, 20);
            this.txtlatEtab.TabIndex = 64;
            // 
            // labelControl51
            // 
            this.labelControl51.Location = new System.Drawing.Point(594, 95);
            this.labelControl51.Name = "labelControl51";
            this.labelControl51.Size = new System.Drawing.Size(54, 13);
            this.labelControl51.TabIndex = 63;
            this.labelControl51.Text = "Longitude :";
            // 
            // labelControl52
            // 
            this.labelControl52.Location = new System.Drawing.Point(382, 94);
            this.labelControl52.Name = "labelControl52";
            this.labelControl52.Size = new System.Drawing.Size(46, 13);
            this.labelControl52.TabIndex = 62;
            this.labelControl52.Text = "Latitude :";
            // 
            // labelControl49
            // 
            this.labelControl49.Location = new System.Drawing.Point(394, 64);
            this.labelControl49.Name = "labelControl49";
            this.labelControl49.Size = new System.Drawing.Size(25, 13);
            this.labelControl49.TabIndex = 61;
            this.labelControl49.Text = "Ville :";
            // 
            // labelControl50
            // 
            this.labelControl50.Location = new System.Drawing.Point(584, 65);
            this.labelControl50.Name = "labelControl50";
            this.labelControl50.Size = new System.Drawing.Size(64, 13);
            this.labelControl50.TabIndex = 60;
            this.labelControl50.Text = "Code Postal :";
            // 
            // txtCodePostalEtab
            // 
            this.txtCodePostalEtab.EnterMoveNextControl = true;
            this.txtCodePostalEtab.Location = new System.Drawing.Point(651, 62);
            this.txtCodePostalEtab.Name = "txtCodePostalEtab";
            this.txtCodePostalEtab.Properties.MaxLength = 20;
            this.txtCodePostalEtab.Size = new System.Drawing.Size(139, 20);
            this.txtCodePostalEtab.TabIndex = 59;
            this.txtCodePostalEtab.Tag = "";
            // 
            // txtVilleEtab
            // 
            this.txtVilleEtab.EditValue = " ";
            this.txtVilleEtab.EnterMoveNextControl = true;
            this.txtVilleEtab.Location = new System.Drawing.Point(422, 61);
            this.txtVilleEtab.Name = "txtVilleEtab";
            this.txtVilleEtab.Properties.MaxLength = 20;
            this.txtVilleEtab.Size = new System.Drawing.Size(143, 20);
            this.txtVilleEtab.TabIndex = 58;
            this.txtVilleEtab.Tag = "";
            // 
            // txtadresseEtab
            // 
            this.txtadresseEtab.EnterMoveNextControl = true;
            this.txtadresseEtab.Location = new System.Drawing.Point(62, 58);
            this.txtadresseEtab.Name = "txtadresseEtab";
            this.txtadresseEtab.Properties.MaxLength = 100;
            this.txtadresseEtab.Size = new System.Drawing.Size(301, 53);
            this.txtadresseEtab.TabIndex = 56;
            this.txtadresseEtab.UseOptimizedRendering = true;
            // 
            // labelControl48
            // 
            this.labelControl48.Location = new System.Drawing.Point(13, 61);
            this.labelControl48.Name = "labelControl48";
            this.labelControl48.Size = new System.Drawing.Size(46, 13);
            this.labelControl48.TabIndex = 57;
            this.labelControl48.Text = "Adresse :";
            // 
            // txtCodeEtab
            // 
            this.txtCodeEtab.Location = new System.Drawing.Point(62, 33);
            this.txtCodeEtab.Name = "txtCodeEtab";
            this.txtCodeEtab.Size = new System.Drawing.Size(105, 20);
            this.txtCodeEtab.TabIndex = 1;
            // 
            // labelControl53
            // 
            this.labelControl53.Location = new System.Drawing.Point(27, 36);
            this.labelControl53.Name = "labelControl53";
            this.labelControl53.Size = new System.Drawing.Size(32, 13);
            this.labelControl53.TabIndex = 55;
            this.labelControl53.Text = "Code :";
            // 
            // simpleButton3
            // 
            this.simpleButton3.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton3.Image")));
            this.simpleButton3.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleRight;
            this.simpleButton3.Location = new System.Drawing.Point(844, 51);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(90, 23);
            this.simpleButton3.TabIndex = 4;
            this.simpleButton3.Text = "Ajouter";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // labelControl54
            // 
            this.labelControl54.Location = new System.Drawing.Point(608, 36);
            this.labelControl54.Name = "labelControl54";
            this.labelControl54.Size = new System.Drawing.Size(40, 13);
            this.labelControl54.TabIndex = 52;
            this.labelControl54.Text = "Région :";
            // 
            // lkpRegionEtab
            // 
            this.lkpRegionEtab.EnterMoveNextControl = true;
            this.lkpRegionEtab.Location = new System.Drawing.Point(651, 33);
            this.lkpRegionEtab.Name = "lkpRegionEtab";
            this.lkpRegionEtab.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpRegionEtab.Size = new System.Drawing.Size(139, 20);
            this.lkpRegionEtab.TabIndex = 3;
            this.lkpRegionEtab.Tag = "";
            // 
            // txtLibEtab
            // 
            this.txtLibEtab.Location = new System.Drawing.Point(215, 33);
            this.txtLibEtab.Name = "txtLibEtab";
            this.txtLibEtab.Size = new System.Drawing.Size(350, 20);
            this.txtLibEtab.TabIndex = 2;
            // 
            // labelControl55
            // 
            this.labelControl55.Location = new System.Drawing.Point(173, 36);
            this.labelControl55.Name = "labelControl55";
            this.labelControl55.Size = new System.Drawing.Size(36, 13);
            this.labelControl55.TabIndex = 28;
            this.labelControl55.Text = "Libelle :";
            // 
            // FrmClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(995, 686);
            this.Controls.Add(this.PageClient);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FrmClient";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.FrmClientSaisie_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmClientSaisie_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.PageClient)).EndInit();
            this.PageClient.ResumeLayout(false);
            this.tabInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).EndInit();
            this.groupControl4.ResumeLayout(false);
            this.groupControl4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpCPaysLivraison.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLibAdresseLiv.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCPostalLiv.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVilleAdresseLiv.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            this.groupControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLibAdresseFac.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAdresseLivraison.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCPostalFac.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVilleAdresseFac.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpCPaysFacturation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMdp.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtemplacement.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRaisonSocialeFour.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCFournisseur.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtObservationClient.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumCIN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroTelephone1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroTelephone2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkElimine.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkClientPassager.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpGouvernorat.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpCRegion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpRecouvreur.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpCVendeur.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LkpCFamille.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAbreviationCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCClient.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRaisonSocial.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBEtablissement.Properties)).EndInit();
            this.tabVente.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl6)).EndInit();
            this.groupControl6.ResumeLayout(false);
            this.groupControl6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl9)).EndInit();
            this.groupControl9.ResumeLayout(false);
            this.groupControl9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemiseExceptionnelle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNbJourCreditFacture.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTauxRemise.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNbJourEcheancePaiment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMontantCreditMax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMontantCreditMin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl8)).EndInit();
            this.groupControl8.ResumeLayout(false);
            this.groupControl8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl7)).EndInit();
            this.groupControl7.ResumeLayout(false);
            this.groupControl7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpCTarif.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpModePaiement.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).EndInit();
            this.groupControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl16)).EndInit();
            this.groupControl16.ResumeLayout(false);
            this.groupControl16.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateFinExoTPE.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateFinExoTPE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl17)).EndInit();
            this.groupControl17.ResumeLayout(false);
            this.groupControl17.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateFinExoTDC.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateFinExoTDC.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupCAutorisation)).EndInit();
            this.groupCAutorisation.ResumeLayout(false);
            this.groupCAutorisation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNAutorisation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateDebSusp.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateDebSusp.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateFinSusp.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateFinSusp.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBTransfertCompta.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl15)).EndInit();
            this.groupControl15.ResumeLayout(false);
            this.groupControl15.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTauxRetenuSource.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTauxRetenuTVA.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl14)).EndInit();
            this.groupControl14.ResumeLayout(false);
            this.groupControl14.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl13)).EndInit();
            this.groupControl13.ResumeLayout(false);
            this.groupControl13.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl12)).EndInit();
            this.groupControl12.ResumeLayout(false);
            this.groupControl12.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMontantExonoreTVA.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateFinExoTVA.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateFinExoTVA.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl11)).EndInit();
            this.groupControl11.ResumeLayout(false);
            this.groupControl11.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateFinExoFodec.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateFinExoFodec.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl10)).EndInit();
            this.groupControl10.ResumeLayout(false);
            this.groupControl10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtnumetablissement.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpcodecateg.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpcodetva.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpcle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBAvanceForfaitaire.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCTVA.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBInitialisationRemise.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl15)).EndInit();
            this.tabBanque.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl14)).EndInit();
            this.panelControl14.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl18)).EndInit();
            this.panelControl18.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridClientBanque)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVClientBanque)).EndInit();
            this.tabContacts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl21)).EndInit();
            this.panelControl21.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl22)).EndInit();
            this.panelControl22.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridClientContact)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVClientContact)).EndInit();
            this.Etablissements.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl20)).EndInit();
            this.groupControl20.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridCEtab)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVEtab)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl19)).EndInit();
            this.groupControl19.ResumeLayout(false);
            this.groupControl19.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtlongEtab.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtlatEtab.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodePostalEtab.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVilleEtab.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtadresseEtab.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodeEtab.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpRegionEtab.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLibEtab.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl PageClient;
        private DevExpress.XtraTab.XtraTabPage tabVente;
        private DevExpress.XtraTab.XtraTabPage tabInfo;
        private DevExpress.XtraTab.XtraTabPage tabBanque;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.PanelControl panelControl15;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl14;
        private DevExpress.XtraEditors.PanelControl panelControl18;
        private DevExpress.XtraGrid.GridControl gridClientBanque;
        private DevExpress.XtraGrid.Views.Grid.GridView gridVClientBanque;
        private DevExpress.XtraTab.XtraTabPage tabContacts;
        private DevExpress.XtraEditors.PanelControl panelControl21;
        private DevExpress.XtraEditors.PanelControl panelControl22;
        private DevExpress.XtraGrid.GridControl gridClientContact;
        private DevExpress.XtraGrid.Views.Grid.GridView gridVClientContact;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LookUpEdit lkpRecouvreur;
        private DevExpress.XtraEditors.LookUpEdit lkpCVendeur;
        private DevExpress.XtraEditors.LookUpEdit LkpCFamille;
        private DevExpress.XtraEditors.TextEdit txtAbreviationCode;
        private DevExpress.XtraEditors.TextEdit txtCClient;
        private DevExpress.XtraEditors.TextEdit txtRaisonSocial;
        private DevExpress.XtraEditors.GroupControl groupControl4;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.LabelControl labelControl22;
        private DevExpress.XtraEditors.TextEdit txtObservationClient;
        private DevExpress.XtraEditors.LabelControl labelControl19;
        private DevExpress.XtraEditors.LabelControl labelControl18;
        private DevExpress.XtraEditors.LabelControl labelControl15;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        private DevExpress.XtraEditors.LabelControl labelControl13;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.LookUpEdit lkpCPaysFacturation;
        private DevExpress.XtraEditors.TextEdit txtFax;
        private DevExpress.XtraEditors.TextEdit txtNumCIN;
        private DevExpress.XtraEditors.TextEdit txtNumeroTelephone1;
        private DevExpress.XtraEditors.TextEdit txtNumeroTelephone2;
        private DevExpress.XtraEditors.TextEdit txtEmail;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.LabelControl labelControl16;
        private DevExpress.XtraEditors.LabelControl labelControl17;
        private DevExpress.XtraEditors.TextEdit txtCPostalLiv;
        private DevExpress.XtraEditors.TextEdit txtVilleAdresseLiv;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtCPostalFac;
        private DevExpress.XtraEditors.TextEdit txtVilleAdresseFac;
        private DevExpress.XtraEditors.GroupControl groupControl6;
        private DevExpress.XtraEditors.GroupControl groupControl8;
        private System.Windows.Forms.RadioButton radioVIP;
        private System.Windows.Forms.RadioButton radioNonVIP;
        private DevExpress.XtraEditors.GroupControl groupControl7;
        private System.Windows.Forms.RadioButton radioActif;
        private System.Windows.Forms.RadioButton radioNonActif;
        private DevExpress.XtraEditors.LabelControl labelControl37;
        private DevExpress.XtraEditors.LabelControl labelControl38;
        private DevExpress.XtraEditors.LookUpEdit lkpCTarif;
        private DevExpress.XtraEditors.LookUpEdit lkpModePaiement;
        private DevExpress.XtraEditors.LabelControl labelControl39;
        private DevExpress.XtraEditors.LabelControl labelControl40;
        private DevExpress.XtraEditors.LabelControl labelControl41;
        private DevExpress.XtraEditors.LabelControl labelControl42;
        private DevExpress.XtraEditors.LabelControl labelControl43;
        private DevExpress.XtraEditors.GroupControl groupControl5;
        private DevExpress.XtraEditors.CheckEdit chkBInitialisationRemise;
        private DevExpress.XtraEditors.GroupControl groupControl15;
        private DevExpress.XtraEditors.LabelControl labelControl33;
        private DevExpress.XtraEditors.LabelControl labelControl34;
        private DevExpress.XtraEditors.GroupControl groupControl14;
        private System.Windows.Forms.RadioButton radioTimbreExonore;
        private System.Windows.Forms.RadioButton radioTimbreNonExonore;
        private DevExpress.XtraEditors.GroupControl groupControl13;
        private System.Windows.Forms.RadioButton radioSuspension;
        private System.Windows.Forms.RadioButton radioExport;
        private System.Windows.Forms.RadioButton radioLocale;
        private DevExpress.XtraEditors.GroupControl groupControl12;
        private DevExpress.XtraEditors.DateEdit txtDateFinExoTVA;
        private DevExpress.XtraEditors.LabelControl labelControl26;
        private DevExpress.XtraEditors.LabelControl labelControl27;
        private System.Windows.Forms.RadioButton radioTVAExonore;
        private System.Windows.Forms.RadioButton radioTVANonExonore;
        private DevExpress.XtraEditors.GroupControl groupControl11;
        private DevExpress.XtraEditors.DateEdit txtDateFinExoFodec;
        private DevExpress.XtraEditors.LabelControl labelControl31;
        private System.Windows.Forms.RadioButton radioFodecExonore;
        private System.Windows.Forms.RadioButton radioFodecNonExonore;
        private DevExpress.XtraEditors.GroupControl groupControl10;
        private DevExpress.XtraEditors.LabelControl labelControl29;
        private DevExpress.XtraEditors.CheckEdit chkBAvanceForfaitaire;
        private System.Windows.Forms.RadioButton radioNonMajore;
        private System.Windows.Forms.RadioButton radioMajore;
        private DevExpress.XtraEditors.TextEdit txtCTVA;
        private DevExpress.XtraEditors.CheckEdit chkBTransfertCompta;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProvider1;
        private DevExpress.XtraEditors.SpinEdit txtMontantExonoreTVA;
        private DevExpress.XtraEditors.SpinEdit txtNbJourCreditFacture;
        private DevExpress.XtraEditors.SpinEdit txtTauxRemise;
        private DevExpress.XtraEditors.SpinEdit txtNbJourEcheancePaiment;
        private DevExpress.XtraEditors.SpinEdit txtMontantCreditMax;
        private DevExpress.XtraEditors.SpinEdit txtMontantCreditMin;
        private DevExpress.XtraEditors.SpinEdit txtTauxRetenuSource;
        private DevExpress.XtraEditors.SpinEdit txtTauxRetenuTVA;
        private DevExpress.XtraEditors.CheckEdit chkAdresseLivraison;
        private DevExpress.XtraEditors.MemoEdit txtLibAdresseFac;
        private DevExpress.XtraEditors.MemoEdit txtLibAdresseLiv;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LookUpEdit lkpCRegion;
        private DevExpress.XtraEditors.LabelControl labelControl20;
        private DevExpress.XtraEditors.LookUpEdit lkpCPaysLivraison;
        private DevExpress.XtraEditors.LabelControl labelControl21;
        private DevExpress.XtraEditors.LookUpEdit lkpGouvernorat;
        private DevExpress.XtraEditors.SpinEdit txtRemiseExceptionnelle;
        private DevExpress.XtraEditors.LabelControl labelControl24;
        private DevExpress.XtraEditors.CheckEdit chkClientPassager;
        private DevExpress.XtraEditors.GroupControl groupControl9;
        private RadioButton rbContentieux;
        private RadioButton rbNonContentieux;
        private DevExpress.XtraEditors.LabelControl labelControl28;
        private DevExpress.XtraEditors.LabelControl labelControl25;
        private DevExpress.XtraEditors.TextEdit txtNAutorisation;
        private DevExpress.XtraEditors.LabelControl labelControl23;
        private DevExpress.XtraEditors.DateEdit txtDateDebSusp;
        private DevExpress.XtraEditors.DateEdit txtDateFinSusp;
        private DevExpress.XtraEditors.GroupControl groupCAutorisation;
        private DevExpress.XtraEditors.GroupControl groupControl16;
        private DevExpress.XtraEditors.DateEdit txtDateFinExoTPE;
        private DevExpress.XtraEditors.LabelControl labelControl30;
        private RadioButton radioTPEExonere;
        private RadioButton radioTPENonExonere;
        private DevExpress.XtraEditors.GroupControl groupControl17;
        private DevExpress.XtraEditors.DateEdit txtDateFinExoTDC;
        private DevExpress.XtraEditors.LabelControl labelControl32;
        private RadioButton radioExoTDC;
        private RadioButton radioNonExoTDC;
        private DevExpress.XtraEditors.CheckEdit chkBEtablissement;
        private DevExpress.XtraEditors.LabelControl labelControl35;
        private DevExpress.XtraEditors.TextEdit txtRaisonSocialeFour;
        private DevExpress.XtraEditors.TextEdit txtCFournisseur;
        private DevExpress.XtraEditors.SimpleButton bttapercu;
        private DevExpress.XtraEditors.SimpleButton bttparcourir;
        private DevExpress.XtraEditors.LabelControl labelControl36;
        private DevExpress.XtraEditors.TextEdit txtemplacement;
        private DevExpress.XtraEditors.LabelControl labelControl44;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraTab.XtraTabPage Etablissements;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.GroupControl groupControl19;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SpinEdit txtlongEtab;
        private DevExpress.XtraEditors.SpinEdit txtlatEtab;
        private DevExpress.XtraEditors.LabelControl labelControl51;
        private DevExpress.XtraEditors.LabelControl labelControl52;
        private DevExpress.XtraEditors.LabelControl labelControl49;
        private DevExpress.XtraEditors.LabelControl labelControl50;
        private DevExpress.XtraEditors.TextEdit txtCodePostalEtab;
        private DevExpress.XtraEditors.TextEdit txtVilleEtab;
        private DevExpress.XtraEditors.MemoEdit txtadresseEtab;
        private DevExpress.XtraEditors.LabelControl labelControl48;
        private DevExpress.XtraEditors.TextEdit txtCodeEtab;
        private DevExpress.XtraEditors.LabelControl labelControl53;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.LabelControl labelControl54;
        private DevExpress.XtraEditors.LookUpEdit lkpRegionEtab;
        private DevExpress.XtraEditors.TextEdit txtLibEtab;
        private DevExpress.XtraEditors.LabelControl labelControl55;
        private DevExpress.XtraEditors.GroupControl groupControl20;
        private DevExpress.XtraGrid.GridControl gridCEtab;
        private DevExpress.XtraGrid.Views.Grid.GridView gridVEtab;
        private DevExpress.XtraEditors.LookUpEdit lkpcle;
        private DevExpress.XtraEditors.TextEdit txtnumetablissement;
        private DevExpress.XtraEditors.LookUpEdit lkpcodecateg;
        private DevExpress.XtraEditors.LookUpEdit lkpcodetva;
        private DevExpress.XtraEditors.LabelControl labelControl45;
        private DevExpress.XtraEditors.TextEdit txtMdp;
        private DevExpress.XtraEditors.CheckEdit chkElimine;
    }
}