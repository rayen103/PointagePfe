namespace CSTAdmin
{
    partial class FrmLanding
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtServerName = new DevExpress.XtraEditors.TextEdit();
            this.txtPassword = new DevExpress.XtraEditors.TextEdit();
            this.txtDataBase = new DevExpress.XtraEditors.TextEdit();
            this.txtLog = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.Start = new DevExpress.XtraEditors.SimpleButton();
            this.btnImport = new DevExpress.XtraEditors.SimpleButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtDomainUser = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.btnAuthorize = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtUser = new DevExpress.XtraEditors.TextEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtServerName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataBase.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLog.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDomainUser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUser.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.labelControl1.Location = new System.Drawing.Point(55, 49);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(65, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Server name:";
            // 
            // labelControl2
            // 
            this.labelControl2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.labelControl2.Location = new System.Drawing.Point(55, 100);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(50, 13);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Password:";
            // 
            // labelControl3
            // 
            this.labelControl3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.labelControl3.Location = new System.Drawing.Point(55, 126);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(50, 13);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "DataBase:";
            // 
            // txtServerName
            // 
            this.txtServerName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtServerName.Location = new System.Drawing.Point(151, 46);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Properties.Appearance.BackColor = System.Drawing.Color.DimGray;
            this.txtServerName.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtServerName.Properties.Appearance.Options.UseBackColor = true;
            this.txtServerName.Properties.Appearance.Options.UseForeColor = true;
            this.txtServerName.Size = new System.Drawing.Size(165, 20);
            this.txtServerName.TabIndex = 1;
            this.txtServerName.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.txt_EditValueChanging);
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtPassword.Location = new System.Drawing.Point(151, 97);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Properties.Appearance.BackColor = System.Drawing.Color.DimGray;
            this.txtPassword.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtPassword.Properties.Appearance.Options.UseBackColor = true;
            this.txtPassword.Properties.Appearance.Options.UseForeColor = true;
            this.txtPassword.Properties.PasswordChar = '•';
            this.txtPassword.Size = new System.Drawing.Size(165, 20);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.txt_EditValueChanging);
            // 
            // txtDataBase
            // 
            this.txtDataBase.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtDataBase.Location = new System.Drawing.Point(151, 123);
            this.txtDataBase.Name = "txtDataBase";
            this.txtDataBase.Properties.Appearance.BackColor = System.Drawing.Color.DimGray;
            this.txtDataBase.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtDataBase.Properties.Appearance.Options.UseBackColor = true;
            this.txtDataBase.Properties.Appearance.Options.UseForeColor = true;
            this.txtDataBase.Size = new System.Drawing.Size(165, 20);
            this.txtDataBase.TabIndex = 4;
            this.txtDataBase.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.txt_EditValueChanging);
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.Location = new System.Drawing.Point(55, 218);
            this.txtLog.Name = "txtLog";
            this.txtLog.Properties.Appearance.BackColor = System.Drawing.Color.DimGray;
            this.txtLog.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtLog.Properties.Appearance.Options.UseBackColor = true;
            this.txtLog.Properties.Appearance.Options.UseForeColor = true;
            this.txtLog.Properties.ReadOnly = true;
            this.txtLog.Size = new System.Drawing.Size(567, 218);
            this.txtLog.TabIndex = 10;
            this.txtLog.UseOptimizedRendering = true;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.labelControl4.Location = new System.Drawing.Point(55, 199);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(38, 13);
            this.labelControl4.TabIndex = 7;
            this.labelControl4.Text = "Output:";
            // 
            // Start
            // 
            this.Start.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Start.Enabled = false;
            this.Start.Location = new System.Drawing.Point(241, 150);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(75, 23);
            this.Start.TabIndex = 5;
            this.Start.Text = "Do it";
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // btnImport
            // 
            this.btnImport.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnImport.Location = new System.Drawing.Point(547, 104);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 8;
            this.btnImport.Text = "Import";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel1.Location = new System.Drawing.Point(341, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1, 150);
            this.panel1.TabIndex = 10;
            // 
            // txtDomainUser
            // 
            this.txtDomainUser.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtDomainUser.Location = new System.Drawing.Point(457, 46);
            this.txtDomainUser.Name = "txtDomainUser";
            this.txtDomainUser.Properties.Appearance.BackColor = System.Drawing.Color.DimGray;
            this.txtDomainUser.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtDomainUser.Properties.Appearance.Options.UseBackColor = true;
            this.txtDomainUser.Properties.Appearance.Options.UseForeColor = true;
            this.txtDomainUser.Size = new System.Drawing.Size(165, 20);
            this.txtDomainUser.TabIndex = 6;
            this.txtDomainUser.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.txtDomainUser_EditValueChanging);
            // 
            // labelControl5
            // 
            this.labelControl5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelControl5.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.labelControl5.Location = new System.Drawing.Point(367, 49);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(65, 13);
            this.labelControl5.TabIndex = 12;
            this.labelControl5.Text = "Domain/User:";
            // 
            // btnAuthorize
            // 
            this.btnAuthorize.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnAuthorize.Enabled = false;
            this.btnAuthorize.Location = new System.Drawing.Point(547, 75);
            this.btnAuthorize.Name = "btnAuthorize";
            this.btnAuthorize.Size = new System.Drawing.Size(75, 23);
            this.btnAuthorize.TabIndex = 7;
            this.btnAuthorize.Text = "Authorize";
            this.btnAuthorize.Click += new System.EventHandler(this.btnAuthorize_Click);
            // 
            // labelControl6
            // 
            this.labelControl6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl6.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.labelControl6.Location = new System.Drawing.Point(647, 443);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(27, 13);
            this.labelControl6.TabIndex = 14;
            this.labelControl6.Text = "By AJ";
            // 
            // txtUser
            // 
            this.txtUser.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtUser.Location = new System.Drawing.Point(151, 72);
            this.txtUser.Name = "txtUser";
            this.txtUser.Properties.Appearance.BackColor = System.Drawing.Color.DimGray;
            this.txtUser.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtUser.Properties.Appearance.Options.UseBackColor = true;
            this.txtUser.Properties.Appearance.Options.UseForeColor = true;
            this.txtUser.Size = new System.Drawing.Size(165, 20);
            this.txtUser.TabIndex = 2;
            this.txtUser.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.txt_EditValueChanging);
            // 
            // labelControl7
            // 
            this.labelControl7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelControl7.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.labelControl7.Location = new System.Drawing.Point(55, 75);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(26, 13);
            this.labelControl7.TabIndex = 15;
            this.labelControl7.Text = "User:";
            // 
            // FrmLanding
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 460);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.btnAuthorize);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.txtDomainUser);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.txtDataBase);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtServerName);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.MinimumSize = new System.Drawing.Size(694, 436);
            this.Name = "FrmLanding";
            this.ShowIcon = false;
            this.Text = "Admin By AJ";
            ((System.ComponentModel.ISupportInitialize)(this.txtServerName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataBase.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLog.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDomainUser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUser.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtServerName;
        private DevExpress.XtraEditors.TextEdit txtPassword;
        private DevExpress.XtraEditors.TextEdit txtDataBase;
        private DevExpress.XtraEditors.MemoEdit txtLog;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SimpleButton Start;
        private DevExpress.XtraEditors.SimpleButton btnImport;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.TextEdit txtDomainUser;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.SimpleButton btnAuthorize;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit txtUser;
        private DevExpress.XtraEditors.LabelControl labelControl7;
    }
}