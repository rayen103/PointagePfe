namespace CST.LePoint.Intervention.Rattachements
{
    partial class FrmAnalyseBITrace
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
            this.viewAnalyseTraceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSetAnalyseTrace = new CST.LePoint.Intervention.DataSetAnalyseTrace();
            this.Alle_RetourField = new DevExpress.XtraPivotGrid.PivotGridField();
            this.Code_Circuit_EmpField = new DevExpress.XtraPivotGrid.PivotGridField();
            this.Code_PC_EmpField = new DevExpress.XtraPivotGrid.PivotGridField();
            this.Code_Shift_EmpField = new DevExpress.XtraPivotGrid.PivotGridField();
            this.DHMS_CollecteField = new DevExpress.XtraPivotGrid.PivotGridField();
            this.DHMS_Sys_ModemField = new DevExpress.XtraPivotGrid.PivotGridField();
            this.EmbarqueField = new DevExpress.XtraPivotGrid.PivotGridField();
            this.Num_IMM_BusField = new DevExpress.XtraPivotGrid.PivotGridField();
            this.PC_CollecteField = new DevExpress.XtraPivotGrid.PivotGridField();
            this.Prenom_EmpField = new DevExpress.XtraPivotGrid.PivotGridField();
            this.RFID_EMPField = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField1 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField2 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.view_AnalyseTraceTableAdapter = new CST.LePoint.Intervention.DataSetAnalyseTraceTableAdapters.View_AnalyseTraceTableAdapter();
            this.SocieteField = new DevExpress.XtraPivotGrid.PivotGridField();
            this.SiteField = new DevExpress.XtraPivotGrid.PivotGridField();
            ((System.ComponentModel.ISupportInitialize)(this.LkpListePG.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datefin.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datefin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datedebut.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datedebut.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pivotGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewAnalyseTraceBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetAnalyseTrace)).BeginInit();
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
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
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
            this.Capacite_BusField.AreaIndex = 2;
            this.Capacite_BusField.Caption = "Capacite Bus";
            this.Capacite_BusField.FieldName = "Capacite_Bus";
            this.Capacite_BusField.Name = "Capacite_BusField";
            // 
            // Code_SocieteField
            // 
            this.Code_SocieteField.AllowedAreas = ((DevExpress.XtraPivotGrid.PivotGridAllowedAreas)(((DevExpress.XtraPivotGrid.PivotGridAllowedAreas.RowArea | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.ColumnArea) 
            | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.FilterArea)));
            this.Code_SocieteField.AreaIndex = 4;
            this.Code_SocieteField.Caption = "Code Societe";
            this.Code_SocieteField.FieldName = "Code_Societe";
            this.Code_SocieteField.Name = "Code_SocieteField";
            // 
            // JourSemaineField
            // 
            this.JourSemaineField.AllowedAreas = ((DevExpress.XtraPivotGrid.PivotGridAllowedAreas)(((DevExpress.XtraPivotGrid.PivotGridAllowedAreas.RowArea | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.ColumnArea) 
            | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.FilterArea)));
            this.JourSemaineField.AreaIndex = 3;
            this.JourSemaineField.Caption = "Jour Semaine";
            this.JourSemaineField.FieldName = "JourSemaine";
            this.JourSemaineField.Name = "JourSemaineField";
            // 
            // MoisLettreField
            // 
            this.MoisLettreField.AllowedAreas = ((DevExpress.XtraPivotGrid.PivotGridAllowedAreas)(((DevExpress.XtraPivotGrid.PivotGridAllowedAreas.RowArea | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.ColumnArea) 
            | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.FilterArea)));
            this.MoisLettreField.AreaIndex = 1;
            this.MoisLettreField.Caption = "Mois Lettre";
            this.MoisLettreField.FieldName = "MoisLettre";
            this.MoisLettreField.Name = "MoisLettreField";
            // 
            // fieldAnnee1
            // 
            this.fieldAnnee1.AllowedAreas = ((DevExpress.XtraPivotGrid.PivotGridAllowedAreas)(((DevExpress.XtraPivotGrid.PivotGridAllowedAreas.RowArea | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.ColumnArea) 
            | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.FilterArea)));
            this.fieldAnnee1.AreaIndex = 17;
            this.fieldAnnee1.Caption = "Année";
            this.fieldAnnee1.FieldName = "Annee";
            this.fieldAnnee1.Name = "fieldAnnee1";
            // 
            // fieldN
            // 
            this.fieldN.AllowedAreas = DevExpress.XtraPivotGrid.PivotGridAllowedAreas.DataArea;
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
            this.fieldIMEI.AreaIndex = 16;
            this.fieldIMEI.Caption = "IMEI";
            this.fieldIMEI.FieldName = "IMEI";
            this.fieldIMEI.Name = "fieldIMEI";
            // 
            // fieldCircuit_Bus
            // 
            this.fieldCircuit_Bus.AllowedAreas = ((DevExpress.XtraPivotGrid.PivotGridAllowedAreas)(((DevExpress.XtraPivotGrid.PivotGridAllowedAreas.RowArea | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.ColumnArea) 
            | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.FilterArea)));
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
            this.pivotGridControl1.DataSource = this.viewAnalyseTraceBindingSource;
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
            this.Code_Circuit_EmpField,
            this.Code_PC_EmpField,
            this.Code_Shift_EmpField,
            this.DHMS_CollecteField,
            this.DHMS_Sys_ModemField,
            this.EmbarqueField,
            this.Num_IMM_BusField,
            this.PC_CollecteField,
            this.Prenom_EmpField,
            this.RFID_EMPField,
            this.SocieteField,
            this.SiteField});
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
            // viewAnalyseTraceBindingSource
            // 
            this.viewAnalyseTraceBindingSource.DataMember = "View_AnalyseTrace";
            this.viewAnalyseTraceBindingSource.DataSource = this.dataSetAnalyseTrace;
            // 
            // dataSetAnalyseTrace
            // 
            this.dataSetAnalyseTrace.DataSetName = "DataSetAnalyseTrace";
            this.dataSetAnalyseTrace.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // Alle_RetourField
            // 
            this.Alle_RetourField.AllowedAreas = ((DevExpress.XtraPivotGrid.PivotGridAllowedAreas)(((DevExpress.XtraPivotGrid.PivotGridAllowedAreas.RowArea | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.ColumnArea) 
            | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.FilterArea)));
            this.Alle_RetourField.AreaIndex = 5;
            this.Alle_RetourField.Caption = "Alle/Retour";
            this.Alle_RetourField.FieldName = "AlleRetour";
            this.Alle_RetourField.Name = "Alle_RetourField";
            // 
            // Code_Circuit_EmpField
            // 
            this.Code_Circuit_EmpField.AllowedAreas = ((DevExpress.XtraPivotGrid.PivotGridAllowedAreas)(((DevExpress.XtraPivotGrid.PivotGridAllowedAreas.RowArea | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.ColumnArea) 
            | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.FilterArea)));
            this.Code_Circuit_EmpField.AreaIndex = 6;
            this.Code_Circuit_EmpField.Caption = "Circuit Employé";
            this.Code_Circuit_EmpField.FieldName = "CircuitEmp";
            this.Code_Circuit_EmpField.Name = "Code_Circuit_EmpField";
            // 
            // Code_PC_EmpField
            // 
            this.Code_PC_EmpField.AllowedAreas = ((DevExpress.XtraPivotGrid.PivotGridAllowedAreas)(((DevExpress.XtraPivotGrid.PivotGridAllowedAreas.RowArea | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.ColumnArea) 
            | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.FilterArea)));
            this.Code_PC_EmpField.AreaIndex = 7;
            this.Code_PC_EmpField.Caption = "PC Employé";
            this.Code_PC_EmpField.FieldName = "PCEmp";
            this.Code_PC_EmpField.Name = "Code_PC_EmpField";
            // 
            // Code_Shift_EmpField
            // 
            this.Code_Shift_EmpField.AllowedAreas = ((DevExpress.XtraPivotGrid.PivotGridAllowedAreas)(((DevExpress.XtraPivotGrid.PivotGridAllowedAreas.RowArea | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.ColumnArea) 
            | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.FilterArea)));
            this.Code_Shift_EmpField.AreaIndex = 8;
            this.Code_Shift_EmpField.Caption = "Shift Employé";
            this.Code_Shift_EmpField.FieldName = "Shift";
            this.Code_Shift_EmpField.Name = "Code_Shift_EmpField";
            // 
            // DHMS_CollecteField
            // 
            this.DHMS_CollecteField.AllowedAreas = ((DevExpress.XtraPivotGrid.PivotGridAllowedAreas)(((DevExpress.XtraPivotGrid.PivotGridAllowedAreas.RowArea | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.ColumnArea) 
            | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.FilterArea)));
            this.DHMS_CollecteField.AreaIndex = 9;
            this.DHMS_CollecteField.Caption = "Date Collecte";
            this.DHMS_CollecteField.FieldName = "DHMS_Collecte";
            this.DHMS_CollecteField.Name = "DHMS_CollecteField";
            // 
            // DHMS_Sys_ModemField
            // 
            this.DHMS_Sys_ModemField.AllowedAreas = ((DevExpress.XtraPivotGrid.PivotGridAllowedAreas)(((DevExpress.XtraPivotGrid.PivotGridAllowedAreas.RowArea | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.ColumnArea) 
            | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.FilterArea)));
            this.DHMS_Sys_ModemField.AreaIndex = 10;
            this.DHMS_Sys_ModemField.Caption = "Date Sys Modem";
            this.DHMS_Sys_ModemField.FieldName = "DHMS_Sys_Modem";
            this.DHMS_Sys_ModemField.Name = "DHMS_Sys_ModemField";
            // 
            // EmbarqueField
            // 
            this.EmbarqueField.AllowedAreas = ((DevExpress.XtraPivotGrid.PivotGridAllowedAreas)(((DevExpress.XtraPivotGrid.PivotGridAllowedAreas.RowArea | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.ColumnArea) 
            | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.FilterArea)));
            this.EmbarqueField.AreaIndex = 11;
            this.EmbarqueField.Caption = "Embarque";
            this.EmbarqueField.FieldName = "Embarque";
            this.EmbarqueField.Name = "EmbarqueField";
            // 
            // Num_IMM_BusField
            // 
            this.Num_IMM_BusField.AllowedAreas = ((DevExpress.XtraPivotGrid.PivotGridAllowedAreas)(((DevExpress.XtraPivotGrid.PivotGridAllowedAreas.RowArea | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.ColumnArea) 
            | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.FilterArea)));
            this.Num_IMM_BusField.AreaIndex = 12;
            this.Num_IMM_BusField.Caption = "Num IMM Bus";
            this.Num_IMM_BusField.FieldName = "Num_IMM_Bus";
            this.Num_IMM_BusField.Name = "Num_IMM_BusField";
            // 
            // PC_CollecteField
            // 
            this.PC_CollecteField.AllowedAreas = ((DevExpress.XtraPivotGrid.PivotGridAllowedAreas)(((DevExpress.XtraPivotGrid.PivotGridAllowedAreas.RowArea | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.ColumnArea) 
            | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.FilterArea)));
            this.PC_CollecteField.AreaIndex = 13;
            this.PC_CollecteField.Caption = "PC Collecte";
            this.PC_CollecteField.FieldName = "PCCollecte";
            this.PC_CollecteField.Name = "PC_CollecteField";
            // 
            // Prenom_EmpField
            // 
            this.Prenom_EmpField.AllowedAreas = ((DevExpress.XtraPivotGrid.PivotGridAllowedAreas)(((DevExpress.XtraPivotGrid.PivotGridAllowedAreas.RowArea | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.ColumnArea) 
            | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.FilterArea)));
            this.Prenom_EmpField.AreaIndex = 14;
            this.Prenom_EmpField.Caption = "Employé";
            this.Prenom_EmpField.FieldName = "Employer";
            this.Prenom_EmpField.Name = "Prenom_EmpField";
            // 
            // RFID_EMPField
            // 
            this.RFID_EMPField.AllowedAreas = ((DevExpress.XtraPivotGrid.PivotGridAllowedAreas)(((DevExpress.XtraPivotGrid.PivotGridAllowedAreas.RowArea | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.ColumnArea) 
            | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.FilterArea)));
            this.RFID_EMPField.AreaIndex = 15;
            this.RFID_EMPField.Caption = "RFID Employé";
            this.RFID_EMPField.FieldName = "RFID_EMP";
            this.RFID_EMPField.Name = "RFID_EMPField";
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
            // view_AnalyseTraceTableAdapter
            // 
            this.view_AnalyseTraceTableAdapter.ClearBeforeFill = true;
            // 
            // SocieteField
            // 
            this.SocieteField.AllowedAreas = ((DevExpress.XtraPivotGrid.PivotGridAllowedAreas)(((DevExpress.XtraPivotGrid.PivotGridAllowedAreas.RowArea | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.ColumnArea) 
            | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.FilterArea)));
            this.SocieteField.AreaIndex = 18;
            this.SocieteField.Caption = "Societé";
            this.SocieteField.FieldName = "Societe";
            this.SocieteField.Name = "SocieteField";
            // 
            // SiteField
            // 
            this.SiteField.AllowedAreas = ((DevExpress.XtraPivotGrid.PivotGridAllowedAreas)(((DevExpress.XtraPivotGrid.PivotGridAllowedAreas.RowArea | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.ColumnArea) 
            | DevExpress.XtraPivotGrid.PivotGridAllowedAreas.FilterArea)));
            this.SiteField.AreaIndex = 19;
            this.SiteField.Caption = "Site";
            this.SiteField.FieldName = "NomSite";
            this.SiteField.Name = "SiteField";
            // 
            // FrmAnalyseBITrace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1189, 591);
            this.Controls.Add(this.pivotGridControl1);
            this.Controls.Add(this.groupControl2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(1189, 591);
            this.Name = "FrmAnalyseBITrace";
            this.Text = "FrmAnalyseBITrace";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmAnalyseBITrace_FormClosing);
            this.Load += new System.EventHandler(this.FrmAnalyseBITrace_Load);
            ((System.ComponentModel.ISupportInitialize)(this.LkpListePG.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datefin.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datefin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datedebut.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datedebut.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pivotGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewAnalyseTraceBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetAnalyseTrace)).EndInit();
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
        private DevExpress.XtraPivotGrid.PivotGridField Code_Circuit_EmpField;
        private DevExpress.XtraPivotGrid.PivotGridField Code_PC_EmpField;
        private DevExpress.XtraPivotGrid.PivotGridField Code_Shift_EmpField;
        private DevExpress.XtraPivotGrid.PivotGridField DHMS_CollecteField;
        private DevExpress.XtraPivotGrid.PivotGridField DHMS_Sys_ModemField;
        private DevExpress.XtraPivotGrid.PivotGridField EmbarqueField;
        private DevExpress.XtraPivotGrid.PivotGridField Num_IMM_BusField;
        private DevExpress.XtraPivotGrid.PivotGridField PC_CollecteField;
        private DevExpress.XtraPivotGrid.PivotGridField Prenom_EmpField;
        private DevExpress.XtraPivotGrid.PivotGridField RFID_EMPField;
        private DataSetAnalyseTrace dataSetAnalyseTrace;
        private System.Windows.Forms.BindingSource viewAnalyseTraceBindingSource;
        private DataSetAnalyseTraceTableAdapters.View_AnalyseTraceTableAdapter view_AnalyseTraceTableAdapter;
        private System.Windows.Forms.CheckBox checkBox1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit datefin;
        private DevExpress.XtraEditors.DateEdit datedebut;
        private DevExpress.XtraPivotGrid.PivotGridField SocieteField;
        private DevExpress.XtraPivotGrid.PivotGridField SiteField;

    }
}