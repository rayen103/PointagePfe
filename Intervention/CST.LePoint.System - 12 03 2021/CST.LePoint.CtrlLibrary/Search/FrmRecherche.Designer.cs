namespace CST.LePoint.CtrlLibrary.Search
{
    partial class FrmRecherche
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.tabRecherche = new DevExpress.XtraTab.XtraTabControl();
            this.tabPageSelection = new DevExpress.XtraTab.XtraTabPage();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl = new DevExpress.XtraEditors.PanelControl();
            this.chkBActif = new DevExpress.XtraEditors.CheckEdit();
            this.lkpCNatureVente = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lkpCTarif = new DevExpress.XtraEditors.LookUpEdit();
            this.lkpCEntrepot = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl24 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl23 = new DevExpress.XtraEditors.LabelControl();
            this.chkBVente = new DevExpress.XtraEditors.CheckEdit();
            this.chkBAchat = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtCritereSelection = new DevExpress.XtraEditors.TextEdit();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.radioButtonParCode = new System.Windows.Forms.RadioButton();
            this.radioButtonParLibelle = new System.Windows.Forms.RadioButton();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.radioButtonPositionDebut = new System.Windows.Forms.RadioButton();
            this.radioButtonPositionFin = new System.Windows.Forms.RadioButton();
            this.radioButtonPositionMilieu = new System.Windows.Forms.RadioButton();
            this.tabPageResultat = new DevExpress.XtraTab.XtraTabPage();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.resultat = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.chkBGestionLot = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.tabRecherche)).BeginInit();
            this.tabRecherche.SuspendLayout();
            this.tabPageSelection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).BeginInit();
            this.panelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkBActif.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpCNatureVente.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpCTarif.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpCEntrepot.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBVente.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBAchat.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCritereSelection.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            this.tabPageResultat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBGestionLot.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tabRecherche
            // 
            this.tabRecherche.Appearance.BackColor = System.Drawing.Color.Gainsboro;
            this.tabRecherche.Appearance.Options.UseBackColor = true;
            this.tabRecherche.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabRecherche.Location = new System.Drawing.Point(0, 0);
            this.tabRecherche.Name = "tabRecherche";
            this.tabRecherche.SelectedTabPage = this.tabPageSelection;
            this.tabRecherche.Size = new System.Drawing.Size(523, 344);
            this.tabRecherche.TabIndex = 0;
            this.tabRecherche.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPageSelection,
            this.tabPageResultat});
            this.tabRecherche.Click += new System.EventHandler(this.tabRecherche_Click);
            // 
            // tabPageSelection
            // 
            this.tabPageSelection.Appearance.PageClient.BackColor = System.Drawing.Color.Maroon;
            this.tabPageSelection.Appearance.PageClient.Options.UseBackColor = true;
            this.tabPageSelection.Controls.Add(this.panelControl1);
            this.tabPageSelection.Name = "tabPageSelection";
            this.tabPageSelection.Size = new System.Drawing.Size(517, 316);
            this.tabPageSelection.Text = "Sélection";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.panelControl);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.txtCritereSelection);
            this.panelControl1.Controls.Add(this.groupControl3);
            this.panelControl1.Controls.Add(this.groupControl2);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(517, 316);
            this.panelControl1.TabIndex = 0;
            // 
            // panelControl
            // 
            this.panelControl.Controls.Add(this.chkBGestionLot);
            this.panelControl.Controls.Add(this.chkBActif);
            this.panelControl.Controls.Add(this.lkpCNatureVente);
            this.panelControl.Controls.Add(this.labelControl2);
            this.panelControl.Controls.Add(this.lkpCTarif);
            this.panelControl.Controls.Add(this.lkpCEntrepot);
            this.panelControl.Controls.Add(this.labelControl24);
            this.panelControl.Controls.Add(this.labelControl23);
            this.panelControl.Controls.Add(this.chkBVente);
            this.panelControl.Controls.Add(this.chkBAchat);
            this.panelControl.Location = new System.Drawing.Point(11, 108);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(499, 115);
            this.panelControl.TabIndex = 11;
            this.panelControl.Visible = false;
            // 
            // chkBActif
            // 
            this.chkBActif.EditValue = true;
            this.chkBActif.Location = new System.Drawing.Point(298, 10);
            this.chkBActif.Name = "chkBActif";
            this.chkBActif.Properties.Caption = "Afficher Que Articles Actifs";
            this.chkBActif.Size = new System.Drawing.Size(152, 19);
            this.chkBActif.TabIndex = 27;
            // 
            // lkpCNatureVente
            // 
            this.lkpCNatureVente.Location = new System.Drawing.Point(103, 67);
            this.lkpCNatureVente.Name = "lkpCNatureVente";
            this.lkpCNatureVente.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpCNatureVente.Size = new System.Drawing.Size(150, 20);
            this.lkpCNatureVente.TabIndex = 25;
            this.lkpCNatureVente.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lkpCNatureVente_KeyDown);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl2.Location = new System.Drawing.Point(14, 70);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(86, 13);
            this.labelControl2.TabIndex = 26;
            this.labelControl2.Text = "Nature de Vente :";
            // 
            // lkpCTarif
            // 
            this.lkpCTarif.Location = new System.Drawing.Point(103, 38);
            this.lkpCTarif.Name = "lkpCTarif";
            this.lkpCTarif.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpCTarif.Size = new System.Drawing.Size(150, 20);
            this.lkpCTarif.TabIndex = 1;
            this.lkpCTarif.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lkpCTarif_KeyDown);
            // 
            // lkpCEntrepot
            // 
            this.lkpCEntrepot.Location = new System.Drawing.Point(103, 9);
            this.lkpCEntrepot.Name = "lkpCEntrepot";
            this.lkpCEntrepot.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpCEntrepot.Size = new System.Drawing.Size(150, 20);
            this.lkpCEntrepot.TabIndex = 0;
            this.lkpCEntrepot.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lkpCEntrepot_KeyDown);
            // 
            // labelControl24
            // 
            this.labelControl24.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl24.Location = new System.Drawing.Point(68, 41);
            this.labelControl24.Name = "labelControl24";
            this.labelControl24.Size = new System.Drawing.Size(32, 13);
            this.labelControl24.TabIndex = 24;
            this.labelControl24.Text = "Tarif : ";
            // 
            // labelControl23
            // 
            this.labelControl23.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl23.Location = new System.Drawing.Point(51, 12);
            this.labelControl23.Name = "labelControl23";
            this.labelControl23.Size = new System.Drawing.Size(49, 13);
            this.labelControl23.TabIndex = 23;
            this.labelControl23.Text = "Entrepôt :";
            // 
            // chkBVente
            // 
            this.chkBVente.Location = new System.Drawing.Point(101, 94);
            this.chkBVente.Name = "chkBVente";
            this.chkBVente.Properties.Caption = "Vente";
            this.chkBVente.Size = new System.Drawing.Size(63, 19);
            this.chkBVente.TabIndex = 2;
            // 
            // chkBAchat
            // 
            this.chkBAchat.Location = new System.Drawing.Point(195, 94);
            this.chkBAchat.Name = "chkBAchat";
            this.chkBAchat.Properties.Caption = "Achat";
            this.chkBAchat.Size = new System.Drawing.Size(58, 19);
            this.chkBAchat.TabIndex = 3;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.labelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.labelControl1.Location = new System.Drawing.Point(11, 253);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(110, 13);
            this.labelControl1.TabIndex = 10;
            this.labelControl1.Text = "Critère de sélection";
            // 
            // txtCritereSelection
            // 
            this.txtCritereSelection.Location = new System.Drawing.Point(11, 272);
            this.txtCritereSelection.Name = "txtCritereSelection";
            this.txtCritereSelection.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtCritereSelection.Properties.Appearance.Options.UseBackColor = true;
            this.txtCritereSelection.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.txtCritereSelection.Size = new System.Drawing.Size(307, 22);
            this.txtCritereSelection.TabIndex = 3;
            this.txtCritereSelection.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCritereSelection_KeyDown);
            // 
            // groupControl3
            // 
            this.groupControl3.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl3.AppearanceCaption.ForeColor = System.Drawing.Color.Navy;
            this.groupControl3.AppearanceCaption.Options.UseFont = true;
            this.groupControl3.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl3.Controls.Add(this.radioButtonParCode);
            this.groupControl3.Controls.Add(this.radioButtonParLibelle);
            this.groupControl3.Location = new System.Drawing.Point(11, 13);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(195, 89);
            this.groupControl3.TabIndex = 0;
            this.groupControl3.Text = "Tri";
            // 
            // radioButtonParCode
            // 
            this.radioButtonParCode.AutoSize = true;
            this.radioButtonParCode.Checked = true;
            this.radioButtonParCode.Location = new System.Drawing.Point(37, 24);
            this.radioButtonParCode.Name = "radioButtonParCode";
            this.radioButtonParCode.Size = new System.Drawing.Size(69, 17);
            this.radioButtonParCode.TabIndex = 0;
            this.radioButtonParCode.TabStop = true;
            this.radioButtonParCode.Text = "Par Code";
            this.radioButtonParCode.UseVisualStyleBackColor = true;
            // 
            // radioButtonParLibelle
            // 
            this.radioButtonParLibelle.AutoSize = true;
            this.radioButtonParLibelle.Location = new System.Drawing.Point(37, 58);
            this.radioButtonParLibelle.Name = "radioButtonParLibelle";
            this.radioButtonParLibelle.Size = new System.Drawing.Size(73, 17);
            this.radioButtonParLibelle.TabIndex = 1;
            this.radioButtonParLibelle.Text = "Par Libellé";
            this.radioButtonParLibelle.UseVisualStyleBackColor = true;
            // 
            // groupControl2
            // 
            this.groupControl2.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl2.AppearanceCaption.ForeColor = System.Drawing.Color.Navy;
            this.groupControl2.AppearanceCaption.Options.UseFont = true;
            this.groupControl2.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl2.AutoSize = true;
            this.groupControl2.Controls.Add(this.radioButtonPositionDebut);
            this.groupControl2.Controls.Add(this.radioButtonPositionFin);
            this.groupControl2.Controls.Add(this.radioButtonPositionMilieu);
            this.groupControl2.Location = new System.Drawing.Point(212, 13);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(299, 89);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.Text = "Position";
            // 
            // radioButtonPositionDebut
            // 
            this.radioButtonPositionDebut.AutoSize = true;
            this.radioButtonPositionDebut.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonPositionDebut.Location = new System.Drawing.Point(27, 43);
            this.radioButtonPositionDebut.Name = "radioButtonPositionDebut";
            this.radioButtonPositionDebut.Size = new System.Drawing.Size(56, 17);
            this.radioButtonPositionDebut.TabIndex = 0;
            this.radioButtonPositionDebut.Text = "A###";
            this.radioButtonPositionDebut.UseVisualStyleBackColor = true;
            // 
            // radioButtonPositionFin
            // 
            this.radioButtonPositionFin.AutoSize = true;
            this.radioButtonPositionFin.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonPositionFin.Location = new System.Drawing.Point(171, 43);
            this.radioButtonPositionFin.Name = "radioButtonPositionFin";
            this.radioButtonPositionFin.Size = new System.Drawing.Size(56, 17);
            this.radioButtonPositionFin.TabIndex = 2;
            this.radioButtonPositionFin.Text = "###A";
            this.radioButtonPositionFin.UseVisualStyleBackColor = true;
            // 
            // radioButtonPositionMilieu
            // 
            this.radioButtonPositionMilieu.AutoSize = true;
            this.radioButtonPositionMilieu.Checked = true;
            this.radioButtonPositionMilieu.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonPositionMilieu.Location = new System.Drawing.Point(99, 43);
            this.radioButtonPositionMilieu.Name = "radioButtonPositionMilieu";
            this.radioButtonPositionMilieu.Size = new System.Drawing.Size(49, 17);
            this.radioButtonPositionMilieu.TabIndex = 1;
            this.radioButtonPositionMilieu.TabStop = true;
            this.radioButtonPositionMilieu.Text = "###";
            this.radioButtonPositionMilieu.UseVisualStyleBackColor = true;
            // 
            // tabPageResultat
            // 
            this.tabPageResultat.Controls.Add(this.gridControl);
            this.tabPageResultat.Name = "tabPageResultat";
            this.tabPageResultat.Size = new System.Drawing.Size(517, 316);
            this.tabPageResultat.Text = "Résultat";
            // 
            // gridControl
            // 
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.RelationName = "Level1";
            this.gridControl.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gridControl.Location = new System.Drawing.Point(0, 0);
            this.gridControl.MainView = this.resultat;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(517, 316);
            this.gridControl.TabIndex = 0;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.resultat});
            this.gridControl.DoubleClick += new System.EventHandler(this.gridControl_DoubleClick);
            // 
            // resultat
            // 
            this.resultat.GridControl = this.gridControl;
            this.resultat.GroupPanelText = " ";
            this.resultat.Name = "resultat";
            this.resultat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.resultat_KeyDown);
            // 
            // chkBGestionLot
            // 
            this.chkBGestionLot.Location = new System.Drawing.Point(298, 39);
            this.chkBGestionLot.Name = "chkBGestionLot";
            this.chkBGestionLot.Properties.Caption = "Afficher Que Articles Gérés Par Lot";
            this.chkBGestionLot.Size = new System.Drawing.Size(196, 19);
            this.chkBGestionLot.TabIndex = 28;
            // 
            // FrmRecherche
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 344);
            this.Controls.Add(this.tabRecherche);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Name = "FrmRecherche";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmRecherche_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmRecherche_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.tabRecherche)).EndInit();
            this.tabRecherche.ResumeLayout(false);
            this.tabPageSelection.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).EndInit();
            this.panelControl.ResumeLayout(false);
            this.panelControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkBActif.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpCNatureVente.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpCTarif.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpCEntrepot.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBVente.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBAchat.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCritereSelection.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            this.groupControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            this.tabPageResultat.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBGestionLot.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl tabRecherche;
        private DevExpress.XtraTab.XtraTabPage tabPageSelection;
        private DevExpress.XtraTab.XtraTabPage tabPageResultat;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView resultat;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtCritereSelection;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private System.Windows.Forms.RadioButton radioButtonParCode;
        private System.Windows.Forms.RadioButton radioButtonParLibelle;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private System.Windows.Forms.RadioButton radioButtonPositionDebut;
        private System.Windows.Forms.RadioButton radioButtonPositionFin;
        private System.Windows.Forms.RadioButton radioButtonPositionMilieu;
        private DevExpress.XtraEditors.PanelControl panelControl;
        private DevExpress.XtraEditors.LookUpEdit lkpCTarif;
        private DevExpress.XtraEditors.LookUpEdit lkpCEntrepot;
        private DevExpress.XtraEditors.LabelControl labelControl24;
        private DevExpress.XtraEditors.LabelControl labelControl23;
        private DevExpress.XtraEditors.CheckEdit chkBVente;
        private DevExpress.XtraEditors.CheckEdit chkBAchat;
        private DevExpress.XtraEditors.CheckEdit chkBActif;
        private DevExpress.XtraEditors.LookUpEdit lkpCNatureVente;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.CheckEdit chkBGestionLot;
    }
}