namespace CST.LePoint.Intervention.Rattachements
{
    partial class FrmEquipeListe
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
            this.gridVEquipe = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridEquipe = new DevExpress.XtraGrid.GridControl();
            this.txtCEquipe = new DevExpress.XtraEditors.TextEdit();
            this.groupControlDetail = new DevExpress.XtraEditors.GroupControl();
            this.bntRechercherBC = new DevExpress.XtraEditors.SimpleButton();
            this.lkpTarif = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.lkpVehicule = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.lkpEntrepot = new DevExpress.XtraEditors.LookUpEdit();
            this.lkpResponsable = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtLibelle = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVEquipe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridEquipe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCEquipe.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlDetail)).BeginInit();
            this.groupControlDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTarif.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpVehicule.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpEntrepot.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpResponsable.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLibelle.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridVEquipe
            // 
            this.gridVEquipe.GridControl = this.gridEquipe;
            this.gridVEquipe.Name = "gridVEquipe";
            this.gridVEquipe.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridVEquipe_RowClick);
            // 
            // gridEquipe
            // 
            this.gridEquipe.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridEquipe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridEquipe.Location = new System.Drawing.Point(2, 21);
            this.gridEquipe.MainView = this.gridVEquipe;
            this.gridEquipe.Name = "gridEquipe";
            this.gridEquipe.Size = new System.Drawing.Size(918, 362);
            this.gridEquipe.TabIndex = 1;
            this.gridEquipe.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridVEquipe});
            // 
            // txtCEquipe
            // 
            this.txtCEquipe.Location = new System.Drawing.Point(4, 42);
            this.txtCEquipe.Name = "txtCEquipe";
            this.txtCEquipe.Size = new System.Drawing.Size(118, 20);
            this.txtCEquipe.TabIndex = 0;
            // 
            // groupControlDetail
            // 
            this.groupControlDetail.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControlDetail.Appearance.Options.UseFont = true;
            this.groupControlDetail.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControlDetail.AppearanceCaption.ForeColor = System.Drawing.Color.MidnightBlue;
            this.groupControlDetail.AppearanceCaption.Options.UseFont = true;
            this.groupControlDetail.AppearanceCaption.Options.UseForeColor = true;
            this.groupControlDetail.Controls.Add(this.bntRechercherBC);
            this.groupControlDetail.Controls.Add(this.gridEquipe);
            this.groupControlDetail.Controls.Add(this.lkpTarif);
            this.groupControlDetail.Controls.Add(this.labelControl2);
            this.groupControlDetail.Location = new System.Drawing.Point(10, 12);
            this.groupControlDetail.Name = "groupControlDetail";
            this.groupControlDetail.Size = new System.Drawing.Size(922, 385);
            this.groupControlDetail.TabIndex = 0;
            this.groupControlDetail.Text = "Liste";
            // 
            // bntRechercherBC
            // 
            this.bntRechercherBC.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bntRechercherBC.Appearance.ForeColor = System.Drawing.Color.Black;
            this.bntRechercherBC.Appearance.Options.UseFont = true;
            this.bntRechercherBC.Appearance.Options.UseForeColor = true;
            this.bntRechercherBC.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.bntRechercherBC.Location = new System.Drawing.Point(71, 284);
            this.bntRechercherBC.Name = "bntRechercherBC";
            this.bntRechercherBC.Size = new System.Drawing.Size(130, 51);
            this.bntRechercherBC.TabIndex = 52;
            this.bntRechercherBC.Text = "Employe(s)";
            this.bntRechercherBC.Visible = false;
            this.bntRechercherBC.Click += new System.EventHandler(this.bntRechercherBC_Click);
            // 
            // lkpTarif
            // 
            this.lkpTarif.Location = new System.Drawing.Point(718, 109);
            this.lkpTarif.Name = "lkpTarif";
            this.lkpTarif.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpTarif.Properties.NullText = "";
            this.lkpTarif.Size = new System.Drawing.Size(170, 20);
            this.lkpTarif.TabIndex = 8;
            this.lkpTarif.Visible = false;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(683, 116);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(29, 13);
            this.labelControl2.TabIndex = 41;
            this.labelControl2.Text = "Tarif :";
            this.labelControl2.Visible = false;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(779, 26);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(68, 13);
            this.labelControl4.TabIndex = 31;
            this.labelControl4.Text = "Responsable :";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(8, 26);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(39, 13);
            this.labelControl1.TabIndex = 28;
            this.labelControl1.Text = "Equipe :";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.lkpVehicule);
            this.groupControl2.Controls.Add(this.labelControl7);
            this.groupControl2.Controls.Add(this.lkpEntrepot);
            this.groupControl2.Controls.Add(this.lkpResponsable);
            this.groupControl2.Controls.Add(this.labelControl3);
            this.groupControl2.Controls.Add(this.txtLibelle);
            this.groupControl2.Controls.Add(this.txtCEquipe);
            this.groupControl2.Controls.Add(this.labelControl4);
            this.groupControl2.Controls.Add(this.labelControl1);
            this.groupControl2.Location = new System.Drawing.Point(10, 403);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(920, 74);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.Text = "Saisie";
            // 
            // lkpVehicule
            // 
            this.lkpVehicule.Location = new System.Drawing.Point(628, 42);
            this.lkpVehicule.Name = "lkpVehicule";
            this.lkpVehicule.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpVehicule.Properties.NullText = "";
            this.lkpVehicule.Size = new System.Drawing.Size(144, 20);
            this.lkpVehicule.TabIndex = 51;
            this.lkpVehicule.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lkpVehicule_KeyDown);
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(631, 26);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(46, 13);
            this.labelControl7.TabIndex = 50;
            this.labelControl7.Text = "Véhicule :";
            // 
            // lkpEntrepot
            // 
            this.lkpEntrepot.Location = new System.Drawing.Point(485, 42);
            this.lkpEntrepot.Name = "lkpEntrepot";
            this.lkpEntrepot.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpEntrepot.Properties.NullText = "";
            this.lkpEntrepot.Size = new System.Drawing.Size(138, 20);
            this.lkpEntrepot.TabIndex = 48;
            this.lkpEntrepot.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lkpEntrepot_KeyDown_1);
            // 
            // lkpResponsable
            // 
            this.lkpResponsable.Enabled = false;
            this.lkpResponsable.Location = new System.Drawing.Point(777, 42);
            this.lkpResponsable.Name = "lkpResponsable";
            this.lkpResponsable.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpResponsable.Properties.NullText = "";
            this.lkpResponsable.Size = new System.Drawing.Size(134, 20);
            this.lkpResponsable.TabIndex = 47;
            this.lkpResponsable.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lkpResponsable_KeyDown);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(487, 26);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(49, 13);
            this.labelControl3.TabIndex = 45;
            this.labelControl3.Text = "Entrepôt :";
            // 
            // txtLibelle
            // 
            this.txtLibelle.Location = new System.Drawing.Point(126, 42);
            this.txtLibelle.Name = "txtLibelle";
            this.txtLibelle.Size = new System.Drawing.Size(354, 20);
            this.txtLibelle.TabIndex = 1;
            // 
            // FrmEquipeListe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 482);
            this.Controls.Add(this.groupControlDetail);
            this.Controls.Add(this.groupControl2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FrmEquipeListe";
            this.Text = "FrmEquipeListe";
            this.Load += new System.EventHandler(this.FrmEquipeListe_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmEquipeListe_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.gridVEquipe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridEquipe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCEquipe.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlDetail)).EndInit();
            this.groupControlDetail.ResumeLayout(false);
            this.groupControlDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTarif.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpVehicule.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpEntrepot.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpResponsable.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLibelle.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.Views.Grid.GridView gridVEquipe;
        private DevExpress.XtraGrid.GridControl gridEquipe;
        private DevExpress.XtraEditors.TextEdit txtCEquipe;
        private DevExpress.XtraEditors.GroupControl groupControlDetail;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtLibelle;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LookUpEdit lkpTarif;
        private DevExpress.XtraEditors.LookUpEdit lkpResponsable;
        private DevExpress.XtraEditors.LookUpEdit lkpEntrepot;
        private DevExpress.XtraEditors.LookUpEdit lkpVehicule;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.SimpleButton bntRechercherBC;
    }
}