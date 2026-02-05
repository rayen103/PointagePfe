using System.Windows.Forms;
namespace CST.LePoint.Intervention.Tiers
{
    partial class FrmClientListe
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
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.CHKNUM = new DevExpress.XtraEditors.CheckEdit();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.txtDateDebut = new DevExpress.XtraEditors.DateEdit();
            this.txtDateFin = new DevExpress.XtraEditors.DateEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.radioTousMouvement = new System.Windows.Forms.RadioButton();
            this.radioMouvemente = new System.Windows.Forms.RadioButton();
            this.radioNonMouvemente = new System.Windows.Forms.RadioButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lookUpCircuit = new DevExpress.XtraEditors.LookUpEdit();
            this.ChkGPS = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl37 = new DevExpress.XtraEditors.LabelControl();
            this.lkpCTarif = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lkpCGouvernorat = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.lkpCRegion = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.lkpCPays = new DevExpress.XtraEditors.LookUpEdit();
            this.lkpCCommercial = new DevExpress.XtraEditors.LookUpEdit();
            this.lkpCFamille = new DevExpress.XtraEditors.LookUpEdit();
            this.txtRaisonSociale = new DevExpress.XtraEditors.TextEdit();
            this.txtCClient = new DevExpress.XtraEditors.TextEdit();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.gridClientListe = new DevExpress.XtraGrid.GridControl();
            this.gridV = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CHKNUM.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateDebut.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateDebut.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateFin.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateFin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpCircuit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChkGPS.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpCTarif.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpCGouvernorat.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpCRegion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpCPays.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpCCommercial.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpCFamille.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRaisonSociale.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCClient.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridClientListe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridV)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.groupControl1.Controls.Add(this.CHKNUM);
            this.groupControl1.Controls.Add(this.groupControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.lookUpCircuit);
            this.groupControl1.Controls.Add(this.ChkGPS);
            this.groupControl1.Controls.Add(this.labelControl37);
            this.groupControl1.Controls.Add(this.lkpCTarif);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.lkpCGouvernorat);
            this.groupControl1.Controls.Add(this.labelControl11);
            this.groupControl1.Controls.Add(this.labelControl12);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.lkpCRegion);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Controls.Add(this.lkpCPays);
            this.groupControl1.Controls.Add(this.lkpCCommercial);
            this.groupControl1.Controls.Add(this.lkpCFamille);
            this.groupControl1.Controls.Add(this.txtRaisonSociale);
            this.groupControl1.Controls.Add(this.txtCClient);
            this.groupControl1.Location = new System.Drawing.Point(2, 5);
            this.groupControl1.LookAndFeel.UseWindowsXPTheme = true;
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(955, 165);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Sélection";
            // 
            // CHKNUM
            // 
            this.CHKNUM.EditValue = null;
            this.CHKNUM.Location = new System.Drawing.Point(824, 136);
            this.CHKNUM.Name = "CHKNUM";
            this.CHKNUM.Properties.AllowGrayed = true;
            this.CHKNUM.Properties.Caption = "Numéro";
            this.CHKNUM.Size = new System.Drawing.Size(75, 19);
            this.CHKNUM.TabIndex = 79;
            // 
            // groupControl2
            // 
            this.groupControl2.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl2.AppearanceCaption.ForeColor = System.Drawing.Color.MidnightBlue;
            this.groupControl2.AppearanceCaption.Options.UseFont = true;
            this.groupControl2.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.groupControl2.Controls.Add(this.txtDateDebut);
            this.groupControl2.Controls.Add(this.txtDateFin);
            this.groupControl2.Controls.Add(this.labelControl9);
            this.groupControl2.Controls.Add(this.labelControl5);
            this.groupControl2.Controls.Add(this.radioTousMouvement);
            this.groupControl2.Controls.Add(this.radioMouvemente);
            this.groupControl2.Controls.Add(this.radioNonMouvemente);
            this.groupControl2.Location = new System.Drawing.Point(10, 102);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(796, 53);
            this.groupControl2.TabIndex = 6;
            this.groupControl2.Text = "Mouvements";
            // 
            // txtDateDebut
            // 
            this.txtDateDebut.EditValue = null;
            this.txtDateDebut.Location = new System.Drawing.Point(50, 24);
            this.txtDateDebut.Name = "txtDateDebut";
            this.txtDateDebut.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.txtDateDebut.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDateDebut.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDateDebut.Size = new System.Drawing.Size(152, 20);
            this.txtDateDebut.TabIndex = 3;
            this.txtDateDebut.Leave += new System.EventHandler(this.txtDateDebut_Leave);
            // 
            // txtDateFin
            // 
            this.txtDateFin.EditValue = null;
            this.txtDateFin.Location = new System.Drawing.Point(246, 24);
            this.txtDateFin.Name = "txtDateFin";
            this.txtDateFin.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.txtDateFin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDateFin.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDateFin.Size = new System.Drawing.Size(152, 20);
            this.txtDateFin.TabIndex = 4;
            this.txtDateFin.Leave += new System.EventHandler(this.txtDateFin_Leave);
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(220, 26);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(20, 13);
            this.labelControl9.TabIndex = 21;
            this.labelControl9.Text = "Au :";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(24, 24);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(20, 13);
            this.labelControl5.TabIndex = 20;
            this.labelControl5.Text = "Du :";
            // 
            // radioTousMouvement
            // 
            this.radioTousMouvement.AutoSize = true;
            this.radioTousMouvement.Location = new System.Drawing.Point(404, 27);
            this.radioTousMouvement.Name = "radioTousMouvement";
            this.radioTousMouvement.Size = new System.Drawing.Size(48, 17);
            this.radioTousMouvement.TabIndex = 0;
            this.radioTousMouvement.TabStop = true;
            this.radioTousMouvement.Text = "Tous";
            this.radioTousMouvement.UseVisualStyleBackColor = true;
            // 
            // radioMouvemente
            // 
            this.radioMouvemente.AutoSize = true;
            this.radioMouvemente.Location = new System.Drawing.Point(511, 27);
            this.radioMouvemente.Name = "radioMouvemente";
            this.radioMouvemente.Size = new System.Drawing.Size(92, 17);
            this.radioMouvemente.TabIndex = 1;
            this.radioMouvemente.TabStop = true;
            this.radioMouvemente.Text = "Mouvementés";
            this.radioMouvemente.UseVisualStyleBackColor = true;
            // 
            // radioNonMouvemente
            // 
            this.radioNonMouvemente.AutoSize = true;
            this.radioNonMouvemente.Location = new System.Drawing.Point(657, 27);
            this.radioNonMouvemente.Name = "radioNonMouvemente";
            this.radioNonMouvemente.Size = new System.Drawing.Size(114, 17);
            this.radioNonMouvemente.TabIndex = 2;
            this.radioNonMouvemente.TabStop = true;
            this.radioNonMouvemente.Text = "Non Mouvementés";
            this.radioNonMouvemente.UseVisualStyleBackColor = true;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(560, 62);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(40, 13);
            this.labelControl1.TabIndex = 74;
            this.labelControl1.Text = "Circuit : ";
            // 
            // lookUpCircuit
            // 
            this.lookUpCircuit.Location = new System.Drawing.Point(605, 61);
            this.lookUpCircuit.Name = "lookUpCircuit";
            this.lookUpCircuit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpCircuit.Size = new System.Drawing.Size(128, 20);
            this.lookUpCircuit.TabIndex = 73;
            this.lookUpCircuit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lookUpCircuit_KeyDown);
            // 
            // ChkGPS
            // 
            this.ChkGPS.EditValue = null;
            this.ChkGPS.Location = new System.Drawing.Point(824, 110);
            this.ChkGPS.Name = "ChkGPS";
            this.ChkGPS.Properties.AllowFocused = false;
            this.ChkGPS.Properties.AllowGrayed = true;
            this.ChkGPS.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.ChkGPS.Properties.Appearance.Options.UseForeColor = true;
            this.ChkGPS.Properties.AutoWidth = true;
            this.ChkGPS.Properties.Caption = "GPS";
            this.ChkGPS.Size = new System.Drawing.Size(41, 19);
            this.ChkGPS.TabIndex = 72;
            // 
            // labelControl37
            // 
            this.labelControl37.Location = new System.Drawing.Point(739, 64);
            this.labelControl37.Name = "labelControl37";
            this.labelControl37.Size = new System.Drawing.Size(26, 13);
            this.labelControl37.TabIndex = 71;
            this.labelControl37.Text = "Tarif:";
            // 
            // lkpCTarif
            // 
            this.lkpCTarif.EnterMoveNextControl = true;
            this.lkpCTarif.Location = new System.Drawing.Point(820, 61);
            this.lkpCTarif.Name = "lkpCTarif";
            this.lkpCTarif.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpCTarif.Size = new System.Drawing.Size(128, 20);
            this.lkpCTarif.TabIndex = 70;
            this.lkpCTarif.Tag = "RQ";
            this.lkpCTarif.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lkpCTarif_KeyDown);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(352, 63);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(68, 13);
            this.labelControl2.TabIndex = 34;
            this.labelControl2.Text = "Gouvernorat :";
            // 
            // lkpCGouvernorat
            // 
            this.lkpCGouvernorat.Location = new System.Drawing.Point(426, 60);
            this.lkpCGouvernorat.Name = "lkpCGouvernorat";
            this.lkpCGouvernorat.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpCGouvernorat.Size = new System.Drawing.Size(128, 20);
            this.lkpCGouvernorat.TabIndex = 33;
            this.lkpCGouvernorat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lkpCGouvernorat_KeyDown);
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(5, 62);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(40, 13);
            this.labelControl11.TabIndex = 31;
            this.labelControl11.Text = "Région :";
            // 
            // labelControl12
            // 
            this.labelControl12.Location = new System.Drawing.Point(5, 37);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(34, 13);
            this.labelControl12.TabIndex = 32;
            this.labelControl12.Text = "Client :";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(182, 63);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(30, 13);
            this.labelControl6.TabIndex = 28;
            this.labelControl6.Text = "Pays :";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(741, 37);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(73, 13);
            this.labelControl4.TabIndex = 26;
            this.labelControl4.Text = "Représentant :";
            // 
            // lkpCRegion
            // 
            this.lkpCRegion.Location = new System.Drawing.Point(51, 61);
            this.lkpCRegion.Name = "lkpCRegion";
            this.lkpCRegion.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpCRegion.Size = new System.Drawing.Size(128, 20);
            this.lkpCRegion.TabIndex = 2;
            this.lkpCRegion.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lkpCRegion_KeyDown);
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(560, 37);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(39, 13);
            this.labelControl7.TabIndex = 29;
            this.labelControl7.Text = "Famille :";
            // 
            // lkpCPays
            // 
            this.lkpCPays.Location = new System.Drawing.Point(218, 60);
            this.lkpCPays.Name = "lkpCPays";
            this.lkpCPays.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpCPays.Size = new System.Drawing.Size(128, 20);
            this.lkpCPays.TabIndex = 3;
            this.lkpCPays.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lkpCPays_KeyDown);
            // 
            // lkpCCommercial
            // 
            this.lkpCCommercial.Location = new System.Drawing.Point(820, 34);
            this.lkpCCommercial.Name = "lkpCCommercial";
            this.lkpCCommercial.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpCCommercial.Size = new System.Drawing.Size(128, 20);
            this.lkpCCommercial.TabIndex = 1;
            this.lkpCCommercial.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lkpCCommercial_KeyDown);
            // 
            // lkpCFamille
            // 
            this.lkpCFamille.Location = new System.Drawing.Point(605, 34);
            this.lkpCFamille.Name = "lkpCFamille";
            this.lkpCFamille.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpCFamille.Size = new System.Drawing.Size(128, 20);
            this.lkpCFamille.TabIndex = 0;
            this.lkpCFamille.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lkpCFamille_KeyDown);
            // 
            // txtRaisonSociale
            // 
            this.txtRaisonSociale.Enabled = false;
            this.txtRaisonSociale.Location = new System.Drawing.Point(182, 35);
            this.txtRaisonSociale.Name = "txtRaisonSociale";
            this.txtRaisonSociale.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.txtRaisonSociale.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.txtRaisonSociale.Properties.Appearance.Options.UseBackColor = true;
            this.txtRaisonSociale.Properties.Appearance.Options.UseForeColor = true;
            this.txtRaisonSociale.Size = new System.Drawing.Size(372, 20);
            this.txtRaisonSociale.TabIndex = 5;
            // 
            // txtCClient
            // 
            this.txtCClient.EnterMoveNextControl = true;
            this.txtCClient.Location = new System.Drawing.Point(51, 35);
            this.txtCClient.Name = "txtCClient";
            this.txtCClient.Size = new System.Drawing.Size(128, 20);
            this.txtCClient.TabIndex = 4;
            this.txtCClient.Tag = "Client";
            this.txtCClient.EditValueChanged += new System.EventHandler(this.txtCClient_EditValueChanged);
            this.txtCClient.Validating += new System.ComponentModel.CancelEventHandler(this.txtCClient_Validating);
            // 
            // groupControl3
            // 
            this.groupControl3.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl3.AppearanceCaption.ForeColor = System.Drawing.Color.MidnightBlue;
            this.groupControl3.AppearanceCaption.Options.UseFont = true;
            this.groupControl3.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl3.Controls.Add(this.gridClientListe);
            this.groupControl3.Location = new System.Drawing.Point(2, 176);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(930, 386);
            this.groupControl3.TabIndex = 1;
            this.groupControl3.Text = "Liste";
            // 
            // gridClientListe
            // 
            this.gridClientListe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridClientListe.Location = new System.Drawing.Point(2, 21);
            this.gridClientListe.MainView = this.gridV;
            this.gridClientListe.Name = "gridClientListe";
            this.gridClientListe.Size = new System.Drawing.Size(926, 363);
            this.gridClientListe.TabIndex = 0;
            this.gridClientListe.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridV});
            this.gridClientListe.DoubleClick += new System.EventHandler(this.gridClientListe_DoubleClick);
            // 
            // gridV
            // 
            this.gridV.GridControl = this.gridClientListe;
            this.gridV.Name = "gridV";
            // 
            // FrmClientListe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(969, 566);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.groupControl3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FrmClientListe";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.frmClientListe_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmClientListe_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CHKNUM.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateDebut.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateDebut.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateFin.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateFin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpCircuit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChkGPS.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpCTarif.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpCGouvernorat.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpCRegion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpCPays.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpCCommercial.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpCFamille.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRaisonSociale.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCClient.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridClientListe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion


        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LookUpEdit lkpCRegion;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LookUpEdit lkpCPays;
        private DevExpress.XtraEditors.LookUpEdit lkpCCommercial;
        private DevExpress.XtraEditors.LookUpEdit lkpCFamille;
        private DevExpress.XtraEditors.TextEdit txtRaisonSociale;
        private DevExpress.XtraEditors.TextEdit txtCClient;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.DateEdit txtDateDebut;
        private DevExpress.XtraEditors.DateEdit txtDateFin;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private System.Windows.Forms.RadioButton radioTousMouvement;
        private System.Windows.Forms.RadioButton radioMouvemente;
        public System.Windows.Forms.RadioButton radioNonMouvemente;
        private DevExpress.XtraGrid.GridControl gridClientListe;
        private DevExpress.XtraGrid.Views.Grid.GridView gridV;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LookUpEdit lkpCGouvernorat;
        private DevExpress.XtraEditors.LabelControl labelControl37;
        private DevExpress.XtraEditors.LookUpEdit lkpCTarif;
        private DevExpress.XtraEditors.CheckEdit ChkGPS;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LookUpEdit lookUpCircuit;
        private DevExpress.XtraEditors.CheckEdit CHKNUM;

    }
}