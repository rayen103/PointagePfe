namespace CST.LePoint.Intervention.Rattachements
{
    partial class FrmAnalyseBIBus
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
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.LkpListePG = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.datefin = new DevExpress.XtraEditors.DateEdit();
            this.datedebut = new DevExpress.XtraEditors.DateEdit();
            this.Capacite_BusField = new DevExpress.XtraPivotGrid.PivotGridField();
            this.Code_SocieteField = new DevExpress.XtraPivotGrid.PivotGridField();
            this.JourSemaineField = new DevExpress.XtraPivotGrid.PivotGridField();
            this.MoisLettreField = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldAnnee1 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldN = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldIMEI = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldCircuit_Bus = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridControl1 = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.viewAnalyseBusBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSetAnalyseBus = new CST.LePoint.Intervention.DataSetAnalyseBus();
            this.Alle_RetourField = new DevExpress.XtraPivotGrid.PivotGridField();
            this.NB_PassageField = new DevExpress.XtraPivotGrid.PivotGridField();
            this.DHMS_CollecteField = new DevExpress.XtraPivotGrid.PivotGridField();
            this.DHMS_Sys_ModemField = new DevExpress.XtraPivotGrid.PivotGridField();
            this.Num_IMM_BusField = new DevExpress.XtraPivotGrid.PivotGridField();
            this.PC_CollecteField = new DevExpress.XtraPivotGrid.PivotGridField();
            this.SocieteField = new DevExpress.XtraPivotGrid.PivotGridField();
            this.SiteField = new DevExpress.XtraPivotGrid.PivotGridField();
            this.ChauffeurField = new DevExpress.XtraPivotGrid.PivotGridField();
            this.Model_BusField = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField1 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField2 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.view_AnalyseBusTableAdapter = new CST.LePoint.Intervention.DataSetAnalyseBusTableAdapters.View_AnalyseBusTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.LkpListePG.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datefin.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datefin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datedebut.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datedebut.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pivotGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewAnalyseBusBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetAnalyseBus)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(14, 29);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(46, 13);
            this.labelControl1.TabIndex = 14;
            this.labelControl1.Text = "Rapport :";
            // 
            // LkpListePG
            // 
            this.LkpListePG.Location = new System.Drawing.Point(63, 26);
            this.LkpListePG.Name = "LkpListePG";
            this.LkpListePG.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LkpListePG.Size = new System.Drawing.Size(357, 20);
            this.LkpListePG.TabIndex = 0;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(516, 25);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 2;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(426, 25);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupControl2
            // 
            this.groupControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl2.AppearanceCaption.ForeColor = System.Drawing.Color.MidnightBlue;
            this.groupControl2.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl2.Controls.Add(this.checkBox1);
            this.groupControl2.Controls.Add(this.labelControl3);
            this.groupControl2.Controls.Add(this.labelControl2);
            this.groupControl2.Controls.Add(this.datefin);
            this.groupControl2.Controls.Add(this.datedebut);
            this.groupControl2.Controls.Add(this.labelControl1);
            this.groupControl2.Controls.Add(this.LkpListePG);
            this.groupControl2.Controls.Add(this.btnLoad);
            this.groupControl2.Controls.Add(this.btnSave);
            this.groupControl2.Location = new System.Drawing.Point(9, 12);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(1171, 54);
            this.groupControl2.TabIndex = 3;
            this.groupControl2.Text = "Générateur de rapports";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(597, 28);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(82, 17);
            this.checkBox1.TabIndex = 29;
            this.checkBox1.Text = "Avec Totals";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(892, 29);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(20, 13);
            this.labelControl3.TabIndex = 28;
            this.labelControl3.Text = "Au :";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(758, 29);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(20, 13);
            this.labelControl2.TabIndex = 27;
            this.labelControl2.Text = "Du :";
            // 
            // datefin
            // 
            this.datefin.EditValue = null;
            this.datefin.Location = new System.Drawing.Point(918, 26);
            this.datefin.Name = "datefin";
            this.datefin.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.datefin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datefin.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datefin.Size = new System.Drawing.Size(100, 20);
            this.datefin.TabIndex = 26;
            // 
            // datedebut
            // 
            this.datedebut.EditValue = null;
            this.datedebut.Location = new System.Drawing.Point(784, 26);
            this.datedebut.Name = "datedebut";
            this.datedebut.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.datedebut.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datedebut.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datedebut.Size = new System.Drawing.Size(100, 20);
            this.datedebut.TabIndex = 25;
            // 
            // Capacite_BusField
            // 
            this.Capacite_BusField.AllowedAreas = ((DevExpress.XtraPivotGrid.PivotGridAllowedAreas)(((DevExpress.XtraPivotGrid.PivotGridAllowedAreas.RowArea | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.ColumnArea) 
            | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.FilterArea)));
            this.Capacite_BusField.Appearance.Cell.Options.UseTextOptions = true;
            this.Capacite_BusField.Appearance.Cell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Capacite_BusField.Appearance.CellGrandTotal.Options.UseTextOptions = true;
            this.Capacite_BusField.Appearance.CellGrandTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Capacite_BusField.Appearance.CellTotal.Options.UseTextOptions = true;
            this.Capacite_BusField.Appearance.CellTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Capacite_BusField.Appearance.Header.Options.UseTextOptions = true;
            this.Capacite_BusField.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Capacite_BusField.Appearance.Value.Options.UseTextOptions = true;
            this.Capacite_BusField.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Capacite_BusField.Appearance.ValueGrandTotal.Options.UseTextOptions = true;
            this.Capacite_BusField.Appearance.ValueGrandTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Capacite_BusField.Appearance.ValueTotal.Options.UseTextOptions = true;
            this.Capacite_BusField.Appearance.ValueTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Capacite_BusField.AreaIndex = 2;
            this.Capacite_BusField.Caption = "Capacite Bus";
            this.Capacite_BusField.FieldName = "Capacite_Bus";
            this.Capacite_BusField.Name = "Capacite_BusField";
            // 
            // Code_SocieteField
            // 
            this.Code_SocieteField.AllowedAreas = ((DevExpress.XtraPivotGrid.PivotGridAllowedAreas)(((DevExpress.XtraPivotGrid.PivotGridAllowedAreas.RowArea | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.ColumnArea) 
            | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.FilterArea)));
            this.Code_SocieteField.Appearance.Cell.Options.UseTextOptions = true;
            this.Code_SocieteField.Appearance.Cell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Code_SocieteField.Appearance.CellGrandTotal.Options.UseTextOptions = true;
            this.Code_SocieteField.Appearance.CellGrandTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Code_SocieteField.Appearance.CellTotal.Options.UseTextOptions = true;
            this.Code_SocieteField.Appearance.CellTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Code_SocieteField.Appearance.Header.Options.UseTextOptions = true;
            this.Code_SocieteField.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Code_SocieteField.Appearance.Value.Options.UseTextOptions = true;
            this.Code_SocieteField.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Code_SocieteField.Appearance.ValueGrandTotal.Options.UseTextOptions = true;
            this.Code_SocieteField.Appearance.ValueGrandTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Code_SocieteField.Appearance.ValueTotal.Options.UseTextOptions = true;
            this.Code_SocieteField.Appearance.ValueTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Code_SocieteField.AreaIndex = 4;
            this.Code_SocieteField.Caption = "Code Societe";
            this.Code_SocieteField.FieldName = "Code_Societe";
            this.Code_SocieteField.Name = "Code_SocieteField";
            // 
            // JourSemaineField
            // 
            this.JourSemaineField.AllowedAreas = ((DevExpress.XtraPivotGrid.PivotGridAllowedAreas)(((DevExpress.XtraPivotGrid.PivotGridAllowedAreas.RowArea | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.ColumnArea) 
            | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.FilterArea)));
            this.JourSemaineField.Appearance.Cell.Options.UseTextOptions = true;
            this.JourSemaineField.Appearance.Cell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.JourSemaineField.Appearance.CellGrandTotal.Options.UseTextOptions = true;
            this.JourSemaineField.Appearance.CellGrandTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.JourSemaineField.Appearance.CellTotal.Options.UseTextOptions = true;
            this.JourSemaineField.Appearance.CellTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.JourSemaineField.Appearance.Header.Options.UseTextOptions = true;
            this.JourSemaineField.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.JourSemaineField.Appearance.Value.Options.UseTextOptions = true;
            this.JourSemaineField.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.JourSemaineField.Appearance.ValueGrandTotal.Options.UseTextOptions = true;
            this.JourSemaineField.Appearance.ValueGrandTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.JourSemaineField.Appearance.ValueTotal.Options.UseTextOptions = true;
            this.JourSemaineField.Appearance.ValueTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.JourSemaineField.AreaIndex = 3;
            this.JourSemaineField.Caption = "Jour Semaine";
            this.JourSemaineField.FieldName = "JourSemaine";
            this.JourSemaineField.Name = "JourSemaineField";
            // 
            // MoisLettreField
            // 
            this.MoisLettreField.AllowedAreas = ((DevExpress.XtraPivotGrid.PivotGridAllowedAreas)(((DevExpress.XtraPivotGrid.PivotGridAllowedAreas.RowArea | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.ColumnArea) 
            | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.FilterArea)));
            this.MoisLettreField.Appearance.Cell.Options.UseTextOptions = true;
            this.MoisLettreField.Appearance.Cell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.MoisLettreField.Appearance.CellGrandTotal.Options.UseTextOptions = true;
            this.MoisLettreField.Appearance.CellGrandTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.MoisLettreField.Appearance.CellTotal.Options.UseTextOptions = true;
            this.MoisLettreField.Appearance.CellTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.MoisLettreField.Appearance.Header.Options.UseTextOptions = true;
            this.MoisLettreField.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.MoisLettreField.Appearance.Value.Options.UseTextOptions = true;
            this.MoisLettreField.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.MoisLettreField.Appearance.ValueGrandTotal.Options.UseTextOptions = true;
            this.MoisLettreField.Appearance.ValueGrandTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.MoisLettreField.Appearance.ValueTotal.Options.UseTextOptions = true;
            this.MoisLettreField.Appearance.ValueTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.MoisLettreField.AreaIndex = 1;
            this.MoisLettreField.Caption = "Mois Lettre";
            this.MoisLettreField.FieldName = "MoisLettre";
            this.MoisLettreField.Name = "MoisLettreField";
            // 
            // fieldAnnee1
            // 
            this.fieldAnnee1.AllowedAreas = ((DevExpress.XtraPivotGrid.PivotGridAllowedAreas)(((DevExpress.XtraPivotGrid.PivotGridAllowedAreas.RowArea | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.ColumnArea) 
            | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.FilterArea)));
            this.fieldAnnee1.Appearance.Cell.Options.UseTextOptions = true;
            this.fieldAnnee1.Appearance.Cell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldAnnee1.Appearance.CellGrandTotal.Options.UseTextOptions = true;
            this.fieldAnnee1.Appearance.CellGrandTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldAnnee1.Appearance.CellTotal.Options.UseTextOptions = true;
            this.fieldAnnee1.Appearance.CellTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldAnnee1.Appearance.Header.Options.UseTextOptions = true;
            this.fieldAnnee1.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldAnnee1.Appearance.Value.Options.UseTextOptions = true;
            this.fieldAnnee1.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldAnnee1.Appearance.ValueGrandTotal.Options.UseTextOptions = true;
            this.fieldAnnee1.Appearance.ValueGrandTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldAnnee1.Appearance.ValueTotal.Options.UseTextOptions = true;
            this.fieldAnnee1.Appearance.ValueTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldAnnee1.AreaIndex = 11;
            this.fieldAnnee1.Caption = "Année";
            this.fieldAnnee1.FieldName = "Annee";
            this.fieldAnnee1.Name = "fieldAnnee1";
            // 
            // fieldN
            // 
            this.fieldN.AllowedAreas = ((DevExpress.XtraPivotGrid.PivotGridAllowedAreas)((DevExpress.XtraPivotGrid.PivotGridAllowedAreas.FilterArea | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.DataArea)));
            this.fieldN.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.fieldN.AreaIndex = 0;
            this.fieldN.Caption = "N°";
            this.fieldN.FieldName = "N";
            this.fieldN.Name = "fieldN";
            // 
            // fieldIMEI
            // 
            this.fieldIMEI.AllowedAreas = ((DevExpress.XtraPivotGrid.PivotGridAllowedAreas)(((DevExpress.XtraPivotGrid.PivotGridAllowedAreas.RowArea | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.ColumnArea) 
            | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.FilterArea)));
            this.fieldIMEI.Appearance.Cell.Options.UseTextOptions = true;
            this.fieldIMEI.Appearance.Cell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldIMEI.Appearance.CellGrandTotal.Options.UseTextOptions = true;
            this.fieldIMEI.Appearance.CellGrandTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldIMEI.Appearance.CellTotal.Options.UseTextOptions = true;
            this.fieldIMEI.Appearance.CellTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldIMEI.Appearance.Header.Options.UseTextOptions = true;
            this.fieldIMEI.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldIMEI.Appearance.Value.Options.UseTextOptions = true;
            this.fieldIMEI.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldIMEI.Appearance.ValueGrandTotal.Options.UseTextOptions = true;
            this.fieldIMEI.Appearance.ValueGrandTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldIMEI.Appearance.ValueTotal.Options.UseTextOptions = true;
            this.fieldIMEI.Appearance.ValueTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldIMEI.AreaIndex = 10;
            this.fieldIMEI.Caption = "IMEI";
            this.fieldIMEI.FieldName = "IMEI";
            this.fieldIMEI.Name = "fieldIMEI";
            // 
            // fieldCircuit_Bus
            // 
            this.fieldCircuit_Bus.AllowedAreas = ((DevExpress.XtraPivotGrid.PivotGridAllowedAreas)(((DevExpress.XtraPivotGrid.PivotGridAllowedAreas.RowArea | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.ColumnArea) 
            | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.FilterArea)));
            this.fieldCircuit_Bus.Appearance.Cell.Options.UseTextOptions = true;
            this.fieldCircuit_Bus.Appearance.Cell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldCircuit_Bus.Appearance.CellGrandTotal.Options.UseTextOptions = true;
            this.fieldCircuit_Bus.Appearance.CellGrandTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldCircuit_Bus.Appearance.CellTotal.Options.UseTextOptions = true;
            this.fieldCircuit_Bus.Appearance.CellTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldCircuit_Bus.Appearance.Header.Options.UseTextOptions = true;
            this.fieldCircuit_Bus.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldCircuit_Bus.Appearance.Value.Options.UseTextOptions = true;
            this.fieldCircuit_Bus.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldCircuit_Bus.Appearance.ValueGrandTotal.Options.UseTextOptions = true;
            this.fieldCircuit_Bus.Appearance.ValueGrandTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldCircuit_Bus.Appearance.ValueTotal.Options.UseTextOptions = true;
            this.fieldCircuit_Bus.Appearance.ValueTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldCircuit_Bus.AreaIndex = 0;
            this.fieldCircuit_Bus.Caption = "Circuit Bus";
            this.fieldCircuit_Bus.FieldName = "CircuitBus";
            this.fieldCircuit_Bus.Name = "fieldCircuit_Bus";
            // 
            // pivotGridControl1
            // 
            this.pivotGridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pivotGridControl1.Appearance.CustomTotalCell.BackColor = System.Drawing.Color.LightBlue;
            this.pivotGridControl1.Appearance.CustomTotalCell.Options.UseBackColor = true;
            this.pivotGridControl1.Appearance.FieldValueTotal.BackColor = System.Drawing.Color.LightBlue;
            this.pivotGridControl1.Appearance.FieldValueTotal.Options.UseBackColor = true;
            this.pivotGridControl1.Appearance.GrandTotalCell.BackColor = System.Drawing.Color.LightBlue;
            this.pivotGridControl1.Appearance.GrandTotalCell.Options.UseBackColor = true;
            this.pivotGridControl1.Appearance.TotalCell.BackColor = System.Drawing.Color.LightBlue;
            this.pivotGridControl1.Appearance.TotalCell.Options.UseBackColor = true;
            this.pivotGridControl1.DataSource = this.viewAnalyseBusBindingSource;
            this.pivotGridControl1.Fields.AddRange(new DevExpress.XtraPivotGrid.PivotGridField[] {
            this.fieldCircuit_Bus,
            this.fieldIMEI,
            this.fieldN,
            this.fieldAnnee1,
            this.MoisLettreField,
            this.JourSemaineField,
            this.Code_SocieteField,
            this.Capacite_BusField,
            this.Alle_RetourField,
            this.NB_PassageField,
            this.DHMS_CollecteField,
            this.DHMS_Sys_ModemField,
            this.Num_IMM_BusField,
            this.PC_CollecteField,
            this.SocieteField,
            this.SiteField,
            this.ChauffeurField,
            this.Model_BusField});
            this.pivotGridControl1.Location = new System.Drawing.Point(9, 72);
            this.pivotGridControl1.Name = "pivotGridControl1";
            this.pivotGridControl1.OptionsPrint.PrintColumnHeaders = DevExpress.Utils.DefaultBoolean.True;
            this.pivotGridControl1.OptionsPrint.PrintDataHeaders = DevExpress.Utils.DefaultBoolean.False;
            this.pivotGridControl1.OptionsPrint.PrintFilterHeaders = DevExpress.Utils.DefaultBoolean.False;
            this.pivotGridControl1.OptionsPrint.PrintHeadersOnEveryPage = true;
            this.pivotGridControl1.OptionsPrint.PrintHorzLines = DevExpress.Utils.DefaultBoolean.True;
            this.pivotGridControl1.OptionsPrint.PrintRowHeaders = DevExpress.Utils.DefaultBoolean.True;
            this.pivotGridControl1.OptionsPrint.PrintVertLines = DevExpress.Utils.DefaultBoolean.True;
            this.pivotGridControl1.Size = new System.Drawing.Size(1171, 507);
            this.pivotGridControl1.TabIndex = 4;
            // 
            // viewAnalyseBusBindingSource
            // 
            this.viewAnalyseBusBindingSource.DataMember = "View_AnalyseBus";
            this.viewAnalyseBusBindingSource.DataSource = this.dataSetAnalyseBus;
            // 
            // dataSetAnalyseBus
            // 
            this.dataSetAnalyseBus.DataSetName = "DataSetAnalyseBus";
            this.dataSetAnalyseBus.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // Alle_RetourField
            // 
            this.Alle_RetourField.AllowedAreas = ((DevExpress.XtraPivotGrid.PivotGridAllowedAreas)(((DevExpress.XtraPivotGrid.PivotGridAllowedAreas.RowArea | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.ColumnArea) 
            | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.FilterArea)));
            this.Alle_RetourField.Appearance.Cell.Options.UseTextOptions = true;
            this.Alle_RetourField.Appearance.Cell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Alle_RetourField.Appearance.CellGrandTotal.Options.UseTextOptions = true;
            this.Alle_RetourField.Appearance.CellGrandTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Alle_RetourField.Appearance.CellTotal.Options.UseTextOptions = true;
            this.Alle_RetourField.Appearance.CellTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Alle_RetourField.Appearance.Header.Options.UseTextOptions = true;
            this.Alle_RetourField.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Alle_RetourField.Appearance.Value.Options.UseTextOptions = true;
            this.Alle_RetourField.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Alle_RetourField.Appearance.ValueGrandTotal.Options.UseTextOptions = true;
            this.Alle_RetourField.Appearance.ValueGrandTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Alle_RetourField.Appearance.ValueTotal.Options.UseTextOptions = true;
            this.Alle_RetourField.Appearance.ValueTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Alle_RetourField.AreaIndex = 5;
            this.Alle_RetourField.Caption = "Alle/Retour";
            this.Alle_RetourField.FieldName = "AlleRetour";
            this.Alle_RetourField.Name = "Alle_RetourField";
            // 
            // NB_PassageField
            // 
            this.NB_PassageField.Appearance.Cell.Options.UseTextOptions = true;
            this.NB_PassageField.Appearance.Cell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.NB_PassageField.Appearance.CellGrandTotal.Options.UseTextOptions = true;
            this.NB_PassageField.Appearance.CellGrandTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.NB_PassageField.Appearance.CellTotal.Options.UseTextOptions = true;
            this.NB_PassageField.Appearance.CellTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.NB_PassageField.Appearance.Header.Options.UseTextOptions = true;
            this.NB_PassageField.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.NB_PassageField.Appearance.Value.Options.UseTextOptions = true;
            this.NB_PassageField.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.NB_PassageField.Appearance.ValueGrandTotal.Options.UseTextOptions = true;
            this.NB_PassageField.Appearance.ValueGrandTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.NB_PassageField.Appearance.ValueTotal.Options.UseTextOptions = true;
            this.NB_PassageField.Appearance.ValueTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.NB_PassageField.AreaIndex = 16;
            this.NB_PassageField.Caption = "Nombre Passage";
            this.NB_PassageField.FieldName = "NB_Passage";
            this.NB_PassageField.Name = "NB_PassageField";
            // 
            // DHMS_CollecteField
            // 
            this.DHMS_CollecteField.AllowedAreas = ((DevExpress.XtraPivotGrid.PivotGridAllowedAreas)(((DevExpress.XtraPivotGrid.PivotGridAllowedAreas.RowArea | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.ColumnArea) 
            | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.FilterArea)));
            this.DHMS_CollecteField.Appearance.Cell.Options.UseTextOptions = true;
            this.DHMS_CollecteField.Appearance.Cell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.DHMS_CollecteField.Appearance.CellGrandTotal.Options.UseTextOptions = true;
            this.DHMS_CollecteField.Appearance.CellGrandTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.DHMS_CollecteField.Appearance.CellTotal.Options.UseTextOptions = true;
            this.DHMS_CollecteField.Appearance.CellTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.DHMS_CollecteField.Appearance.Header.Options.UseTextOptions = true;
            this.DHMS_CollecteField.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.DHMS_CollecteField.Appearance.Value.Options.UseTextOptions = true;
            this.DHMS_CollecteField.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.DHMS_CollecteField.Appearance.ValueGrandTotal.Options.UseTextOptions = true;
            this.DHMS_CollecteField.Appearance.ValueGrandTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.DHMS_CollecteField.Appearance.ValueTotal.Options.UseTextOptions = true;
            this.DHMS_CollecteField.Appearance.ValueTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.DHMS_CollecteField.AreaIndex = 6;
            this.DHMS_CollecteField.Caption = "Date Collecte";
            this.DHMS_CollecteField.FieldName = "DHMS_Collecte";
            this.DHMS_CollecteField.Name = "DHMS_CollecteField";
            // 
            // DHMS_Sys_ModemField
            // 
            this.DHMS_Sys_ModemField.AllowedAreas = ((DevExpress.XtraPivotGrid.PivotGridAllowedAreas)(((DevExpress.XtraPivotGrid.PivotGridAllowedAreas.RowArea | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.ColumnArea) 
            | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.FilterArea)));
            this.DHMS_Sys_ModemField.Appearance.Cell.Options.UseTextOptions = true;
            this.DHMS_Sys_ModemField.Appearance.Cell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.DHMS_Sys_ModemField.Appearance.CellGrandTotal.Options.UseTextOptions = true;
            this.DHMS_Sys_ModemField.Appearance.CellGrandTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.DHMS_Sys_ModemField.Appearance.CellTotal.Options.UseTextOptions = true;
            this.DHMS_Sys_ModemField.Appearance.CellTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.DHMS_Sys_ModemField.Appearance.Header.Options.UseTextOptions = true;
            this.DHMS_Sys_ModemField.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.DHMS_Sys_ModemField.Appearance.Value.Options.UseTextOptions = true;
            this.DHMS_Sys_ModemField.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.DHMS_Sys_ModemField.Appearance.ValueGrandTotal.Options.UseTextOptions = true;
            this.DHMS_Sys_ModemField.Appearance.ValueGrandTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.DHMS_Sys_ModemField.Appearance.ValueTotal.Options.UseTextOptions = true;
            this.DHMS_Sys_ModemField.Appearance.ValueTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.DHMS_Sys_ModemField.AreaIndex = 7;
            this.DHMS_Sys_ModemField.Caption = "Date Sys Modem";
            this.DHMS_Sys_ModemField.FieldName = "DHMS_Sys_Modem";
            this.DHMS_Sys_ModemField.Name = "DHMS_Sys_ModemField";
            // 
            // Num_IMM_BusField
            // 
            this.Num_IMM_BusField.AllowedAreas = ((DevExpress.XtraPivotGrid.PivotGridAllowedAreas)(((DevExpress.XtraPivotGrid.PivotGridAllowedAreas.RowArea | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.ColumnArea) 
            | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.FilterArea)));
            this.Num_IMM_BusField.Appearance.Cell.Options.UseTextOptions = true;
            this.Num_IMM_BusField.Appearance.Cell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Num_IMM_BusField.Appearance.CellGrandTotal.Options.UseTextOptions = true;
            this.Num_IMM_BusField.Appearance.CellGrandTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Num_IMM_BusField.Appearance.CellTotal.Options.UseTextOptions = true;
            this.Num_IMM_BusField.Appearance.CellTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Num_IMM_BusField.Appearance.Header.Options.UseTextOptions = true;
            this.Num_IMM_BusField.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Num_IMM_BusField.Appearance.Value.Options.UseTextOptions = true;
            this.Num_IMM_BusField.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Num_IMM_BusField.Appearance.ValueGrandTotal.Options.UseTextOptions = true;
            this.Num_IMM_BusField.Appearance.ValueGrandTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Num_IMM_BusField.Appearance.ValueTotal.Options.UseTextOptions = true;
            this.Num_IMM_BusField.Appearance.ValueTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Num_IMM_BusField.AreaIndex = 8;
            this.Num_IMM_BusField.Caption = "Num IMM Bus";
            this.Num_IMM_BusField.FieldName = "Num_IMM_Bus";
            this.Num_IMM_BusField.Name = "Num_IMM_BusField";
            // 
            // PC_CollecteField
            // 
            this.PC_CollecteField.AllowedAreas = ((DevExpress.XtraPivotGrid.PivotGridAllowedAreas)(((DevExpress.XtraPivotGrid.PivotGridAllowedAreas.RowArea | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.ColumnArea) 
            | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.FilterArea)));
            this.PC_CollecteField.Appearance.Cell.Options.UseTextOptions = true;
            this.PC_CollecteField.Appearance.Cell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.PC_CollecteField.Appearance.CellGrandTotal.Options.UseTextOptions = true;
            this.PC_CollecteField.Appearance.CellGrandTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.PC_CollecteField.Appearance.CellTotal.Options.UseTextOptions = true;
            this.PC_CollecteField.Appearance.CellTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.PC_CollecteField.Appearance.Header.Options.UseTextOptions = true;
            this.PC_CollecteField.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.PC_CollecteField.Appearance.Value.Options.UseTextOptions = true;
            this.PC_CollecteField.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.PC_CollecteField.Appearance.ValueGrandTotal.Options.UseTextOptions = true;
            this.PC_CollecteField.Appearance.ValueGrandTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.PC_CollecteField.Appearance.ValueTotal.Options.UseTextOptions = true;
            this.PC_CollecteField.Appearance.ValueTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.PC_CollecteField.AreaIndex = 9;
            this.PC_CollecteField.Caption = "PC Collecte";
            this.PC_CollecteField.FieldName = "PCCollecte";
            this.PC_CollecteField.Name = "PC_CollecteField";
            // 
            // SocieteField
            // 
            this.SocieteField.AllowedAreas = ((DevExpress.XtraPivotGrid.PivotGridAllowedAreas)(((DevExpress.XtraPivotGrid.PivotGridAllowedAreas.RowArea | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.ColumnArea) 
            | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.FilterArea)));
            this.SocieteField.Appearance.Cell.Options.UseTextOptions = true;
            this.SocieteField.Appearance.Cell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.SocieteField.Appearance.CellGrandTotal.Options.UseTextOptions = true;
            this.SocieteField.Appearance.CellGrandTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.SocieteField.Appearance.CellTotal.Options.UseTextOptions = true;
            this.SocieteField.Appearance.CellTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.SocieteField.Appearance.Header.Options.UseTextOptions = true;
            this.SocieteField.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.SocieteField.Appearance.Value.Options.UseTextOptions = true;
            this.SocieteField.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.SocieteField.Appearance.ValueGrandTotal.Options.UseTextOptions = true;
            this.SocieteField.Appearance.ValueGrandTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.SocieteField.Appearance.ValueTotal.Options.UseTextOptions = true;
            this.SocieteField.Appearance.ValueTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.SocieteField.AreaIndex = 12;
            this.SocieteField.Caption = "Societé";
            this.SocieteField.FieldName = "Societe";
            this.SocieteField.Name = "SocieteField";
            // 
            // SiteField
            // 
            this.SiteField.AllowedAreas = ((DevExpress.XtraPivotGrid.PivotGridAllowedAreas)(((DevExpress.XtraPivotGrid.PivotGridAllowedAreas.RowArea | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.ColumnArea) 
            | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.FilterArea)));
            this.SiteField.Appearance.Cell.Options.UseTextOptions = true;
            this.SiteField.Appearance.Cell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.SiteField.Appearance.CellGrandTotal.Options.UseTextOptions = true;
            this.SiteField.Appearance.CellGrandTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.SiteField.Appearance.CellTotal.Options.UseTextOptions = true;
            this.SiteField.Appearance.CellTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.SiteField.Appearance.Header.Options.UseTextOptions = true;
            this.SiteField.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.SiteField.Appearance.Value.Options.UseTextOptions = true;
            this.SiteField.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.SiteField.Appearance.ValueGrandTotal.Options.UseTextOptions = true;
            this.SiteField.Appearance.ValueGrandTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.SiteField.Appearance.ValueTotal.Options.UseTextOptions = true;
            this.SiteField.Appearance.ValueTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.SiteField.AreaIndex = 13;
            this.SiteField.Caption = "Site";
            this.SiteField.FieldName = "NomSite";
            this.SiteField.Name = "SiteField";
            // 
            // ChauffeurField
            // 
            this.ChauffeurField.Appearance.Cell.Options.UseTextOptions = true;
            this.ChauffeurField.Appearance.Cell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ChauffeurField.Appearance.CellGrandTotal.Options.UseTextOptions = true;
            this.ChauffeurField.Appearance.CellGrandTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ChauffeurField.Appearance.CellTotal.Options.UseTextOptions = true;
            this.ChauffeurField.Appearance.CellTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ChauffeurField.Appearance.Header.Options.UseTextOptions = true;
            this.ChauffeurField.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ChauffeurField.Appearance.Value.Options.UseTextOptions = true;
            this.ChauffeurField.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ChauffeurField.Appearance.ValueGrandTotal.Options.UseTextOptions = true;
            this.ChauffeurField.Appearance.ValueGrandTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ChauffeurField.Appearance.ValueTotal.Options.UseTextOptions = true;
            this.ChauffeurField.Appearance.ValueTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ChauffeurField.AreaIndex = 14;
            this.ChauffeurField.Caption = "Chauffeur";
            this.ChauffeurField.FieldName = "Chauffeur";
            this.ChauffeurField.Name = "ChauffeurField";
            // 
            // Model_BusField
            // 
            this.Model_BusField.Appearance.Cell.Options.UseTextOptions = true;
            this.Model_BusField.Appearance.Cell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Model_BusField.Appearance.CellGrandTotal.Options.UseTextOptions = true;
            this.Model_BusField.Appearance.CellGrandTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Model_BusField.Appearance.CellTotal.Options.UseTextOptions = true;
            this.Model_BusField.Appearance.CellTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Model_BusField.Appearance.Header.Options.UseTextOptions = true;
            this.Model_BusField.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Model_BusField.Appearance.Value.Options.UseTextOptions = true;
            this.Model_BusField.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Model_BusField.Appearance.ValueGrandTotal.Options.UseTextOptions = true;
            this.Model_BusField.Appearance.ValueGrandTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Model_BusField.Appearance.ValueTotal.Options.UseTextOptions = true;
            this.Model_BusField.Appearance.ValueTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Model_BusField.AreaIndex = 15;
            this.Model_BusField.Caption = "Model Bus";
            this.Model_BusField.FieldName = "Model_Bus";
            this.Model_BusField.Name = "Model_BusField";
            // 
            // pivotGridField1
            // 
            this.pivotGridField1.AreaIndex = 4;
            this.pivotGridField1.Caption = "Jour Semaine";
            this.pivotGridField1.FieldName = "JourSemaine";
            this.pivotGridField1.Name = "pivotGridField1";
            // 
            // pivotGridField2
            // 
            this.pivotGridField2.AreaIndex = 4;
            this.pivotGridField2.Caption = "Jour Semaine";
            this.pivotGridField2.FieldName = "JourSemaine";
            this.pivotGridField2.Name = "pivotGridField2";
            // 
            // view_AnalyseBusTableAdapter
            // 
            this.view_AnalyseBusTableAdapter.ClearBeforeFill = true;
            // 
            // FrmAnalyseBIBus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1189, 591);
            this.Controls.Add(this.pivotGridControl1);
            this.Controls.Add(this.groupControl2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(1189, 591);
            this.Name = "FrmAnalyseBIBus";
            this.Text = "FrmAnalyseBIBus";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmAnalyseBIBus_FormClosing);
            this.Load += new System.EventHandler(this.FrmAnalyseBIBus_Load);
            ((System.ComponentModel.ISupportInitialize)(this.LkpListePG.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datefin.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datefin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datedebut.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datedebut.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pivotGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewAnalyseBusBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetAnalyseBus)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit LkpListePG;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnSave;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraPivotGrid.PivotGridField Capacite_BusField;
        private DevExpress.XtraPivotGrid.PivotGridField Code_SocieteField;
        private DevExpress.XtraPivotGrid.PivotGridField JourSemaineField;
        private DevExpress.XtraPivotGrid.PivotGridField MoisLettreField;
        private DevExpress.XtraPivotGrid.PivotGridField fieldAnnee1;
        private DevExpress.XtraPivotGrid.PivotGridField fieldN;
        private DevExpress.XtraPivotGrid.PivotGridField fieldIMEI;
        private DevExpress.XtraPivotGrid.PivotGridField fieldCircuit_Bus;
        private DevExpress.XtraPivotGrid.PivotGridControl pivotGridControl1;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField1;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField2;
        private DevExpress.XtraPivotGrid.PivotGridField Alle_RetourField;
        private DevExpress.XtraPivotGrid.PivotGridField NB_PassageField;
        private DevExpress.XtraPivotGrid.PivotGridField DHMS_CollecteField;
        private DevExpress.XtraPivotGrid.PivotGridField DHMS_Sys_ModemField;
        private DevExpress.XtraPivotGrid.PivotGridField Num_IMM_BusField;
        private DevExpress.XtraPivotGrid.PivotGridField PC_CollecteField;
        private System.Windows.Forms.CheckBox checkBox1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit datefin;
        private DevExpress.XtraEditors.DateEdit datedebut;
        private DevExpress.XtraPivotGrid.PivotGridField SocieteField;
        private DevExpress.XtraPivotGrid.PivotGridField SiteField;
        private DevExpress.XtraPivotGrid.PivotGridField ChauffeurField;
        private DevExpress.XtraPivotGrid.PivotGridField Model_BusField;
        private DataSetAnalyseBus dataSetAnalyseBus;
        private System.Windows.Forms.BindingSource viewAnalyseBusBindingSource;
        private DataSetAnalyseBusTableAdapters.View_AnalyseBusTableAdapter view_AnalyseBusTableAdapter;

    }
}