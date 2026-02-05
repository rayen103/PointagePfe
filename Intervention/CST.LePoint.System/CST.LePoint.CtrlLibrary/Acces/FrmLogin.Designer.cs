namespace CST.LePoint.CtrlLibrary.Acces
{
    partial class FrmLogin
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnModifier = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.btnAnnuler = new DevExpress.XtraEditors.SimpleButton();
            this.txtNomUtilisateur = new DevExpress.XtraEditors.TextEdit();
            this.txtMotDePasse = new DevExpress.XtraEditors.TextEdit();
            this.picturelogin = new DevExpress.XtraEditors.PictureEdit();
            this.lkpSociete = new DevExpress.XtraEditors.LookUpEdit();
            this.lkpSite = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNomUtilisateur.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMotDePasse.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picturelogin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpSociete.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpSite.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(291, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 69;
            this.label2.Text = "Site :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(158, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 68;
            this.label1.Text = "Société :";
            // 
            // btnModifier
            // 
            this.btnModifier.Location = new System.Drawing.Point(287, 208);
            this.btnModifier.Name = "btnModifier";
            this.btnModifier.Size = new System.Drawing.Size(82, 22);
            this.btnModifier.TabIndex = 4;
            this.btnModifier.Text = "Modifier";
            this.btnModifier.Click += new System.EventHandler(this.btnModifier_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(161, 75);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(71, 13);
            this.labelControl1.TabIndex = 67;
            this.labelControl1.Text = "Mot de passe :";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(161, 30);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(32, 13);
            this.labelControl8.TabIndex = 66;
            this.labelControl8.Text = "Login :";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(378, 208);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(82, 22);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnAnnuler
            // 
            this.btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAnnuler.Location = new System.Drawing.Point(196, 208);
            this.btnAnnuler.Name = "btnAnnuler";
            this.btnAnnuler.Size = new System.Drawing.Size(82, 22);
            this.btnAnnuler.TabIndex = 5;
            this.btnAnnuler.Text = "Annuler";
            this.btnAnnuler.Click += new System.EventHandler(this.btnAnnuler_Click);
            // 
            // txtNomUtilisateur
            // 
            this.txtNomUtilisateur.Location = new System.Drawing.Point(161, 49);
            this.txtNomUtilisateur.Name = "txtNomUtilisateur";
            this.txtNomUtilisateur.Size = new System.Drawing.Size(263, 20);
            this.txtNomUtilisateur.TabIndex = 0;
            // 
            // txtMotDePasse
            // 
            this.txtMotDePasse.Location = new System.Drawing.Point(161, 94);
            this.txtMotDePasse.Name = "txtMotDePasse";
            this.txtMotDePasse.Properties.PasswordChar = '•';
            this.txtMotDePasse.Size = new System.Drawing.Size(263, 20);
            this.txtMotDePasse.TabIndex = 1;
            // 
            // picturelogin
            // 
            this.picturelogin.EditValue = global::CST.LePoint.CtrlLibrary.Properties.Resources.loginacces1;
            this.picturelogin.Location = new System.Drawing.Point(12, 30);
            this.picturelogin.Name = "picturelogin";
            this.picturelogin.Properties.AllowScrollViaMouseDrag = false;
            this.picturelogin.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.picturelogin.Properties.Appearance.Options.UseBackColor = true;
            this.picturelogin.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.picturelogin.Properties.ReadOnly = true;
            this.picturelogin.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.picturelogin.Size = new System.Drawing.Size(128, 133);
            this.picturelogin.TabIndex = 63;
            // 
            // lkpSociete
            // 
            this.lkpSociete.Location = new System.Drawing.Point(161, 143);
            this.lkpSociete.Name = "lkpSociete";
            this.lkpSociete.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true)});
            this.lkpSociete.Size = new System.Drawing.Size(128, 20);
            this.lkpSociete.TabIndex = 70;
            this.lkpSociete.EditValueChanged += new System.EventHandler(this.lkpSociete_EditValueChanged);
            // 
            // lkpSite
            // 
            this.lkpSite.Location = new System.Drawing.Point(294, 143);
            this.lkpSite.Name = "lkpSite";
            this.lkpSite.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpSite.Size = new System.Drawing.Size(128, 20);
            this.lkpSite.TabIndex = 2;
            // 
            // FrmLogin
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnAnnuler;
            this.ClientSize = new System.Drawing.Size(472, 242);
            this.Controls.Add(this.lkpSite);
            this.Controls.Add(this.lkpSociete);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnModifier);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.labelControl8);
            this.Controls.Add(this.picturelogin);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnAnnuler);
            this.Controls.Add(this.txtNomUtilisateur);
            this.Controls.Add(this.txtMotDePasse);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmLogin_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmLogin_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.txtNomUtilisateur.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMotDePasse.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picturelogin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpSociete.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpSite.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public DevExpress.XtraEditors.SimpleButton btnModifier;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.PictureEdit picturelogin;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.SimpleButton btnAnnuler;
        private DevExpress.XtraEditors.TextEdit txtNomUtilisateur;
        private DevExpress.XtraEditors.TextEdit txtMotDePasse;
        private DevExpress.XtraEditors.LookUpEdit lkpSociete;
        private DevExpress.XtraEditors.LookUpEdit lkpSite;

    }
}