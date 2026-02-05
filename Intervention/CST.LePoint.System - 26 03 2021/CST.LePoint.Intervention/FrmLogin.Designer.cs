namespace CST.LePoint.Intervention
{
    partial class FrmLogin1
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
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.txtLoginName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtLoginPwd = new DevExpress.XtraEditors.TextEdit();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnAnnuler = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.picturelogin = new DevExpress.XtraEditors.PictureEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLoginName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLoginPwd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picturelogin.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(178, 52);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(32, 13);
            this.labelControl8.TabIndex = 50;
            this.labelControl8.Text = "Login :";
            // 
            // txtLoginName
            // 
            this.txtLoginName.EnterMoveNextControl = true;
            this.txtLoginName.Location = new System.Drawing.Point(178, 71);
            this.txtLoginName.Name = "txtLoginName";
            this.txtLoginName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLoginName.Properties.MaxLength = 20;
            this.txtLoginName.Size = new System.Drawing.Size(188, 20);
            this.txtLoginName.TabIndex = 0;
            this.txtLoginName.Tag = "Client\\RQ";
            this.txtLoginName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLoginName_KeyPress);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(178, 97);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(71, 13);
            this.labelControl1.TabIndex = 52;
            this.labelControl1.Text = "Mot de passe :";
            // 
            // txtLoginPwd
            // 
            this.txtLoginPwd.EnterMoveNextControl = true;
            this.txtLoginPwd.Location = new System.Drawing.Point(178, 125);
            this.txtLoginPwd.Name = "txtLoginPwd";
            this.txtLoginPwd.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLoginPwd.Properties.MaxLength = 20;
            this.txtLoginPwd.Properties.PasswordChar = '*';
            this.txtLoginPwd.Size = new System.Drawing.Size(188, 20);
            this.txtLoginPwd.TabIndex = 1;
            this.txtLoginPwd.Tag = "Client\\RQ";
            this.txtLoginPwd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLoginPwd_KeyPress);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(178, 168);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnAnnuler
            // 
            this.btnAnnuler.Location = new System.Drawing.Point(275, 168);
            this.btnAnnuler.Name = "btnAnnuler";
            this.btnAnnuler.Size = new System.Drawing.Size(75, 23);
            this.btnAnnuler.TabIndex = 3;
            this.btnAnnuler.Text = "Annuler";
            this.btnAnnuler.Click += new System.EventHandler(this.btnAnnuler_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.picturelogin);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(514, 282);
            this.panelControl1.TabIndex = 53;
            // 
            // picturelogin
            // 
            this.picturelogin.Location = new System.Drawing.Point(12, 5);
            this.picturelogin.Name = "picturelogin";
            this.picturelogin.Size = new System.Drawing.Size(140, 149);
            this.picturelogin.TabIndex = 0;
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 282);
            this.Controls.Add(this.btnAnnuler);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtLoginPwd);
            this.Controls.Add(this.labelControl8);
            this.Controls.Add(this.txtLoginName);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmLogin";
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtLoginName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLoginPwd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picturelogin.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.TextEdit txtLoginName;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtLoginPwd;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnAnnuler;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PictureEdit picturelogin;
    }
}