using DevExpress.XtraEditors.Repository;
namespace CST.LePoint.Intervention.DroitsAcces
{
    partial class FrmUtilisateur
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.CB_Societe = new System.Windows.Forms.ComboBox();
            this.LB_Societe = new DevExpress.XtraEditors.LabelControl();
            this.lkpRole = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labRole = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl22 = new DevExpress.XtraEditors.LabelControl();
            this.txtUserName = new DevExpress.XtraEditors.TextEdit();
            this.txtNom = new DevExpress.XtraEditors.TextEdit();
            this.txtPrenom = new DevExpress.XtraEditors.TextEdit();
            this.txtConfirmPassword = new DevExpress.XtraEditors.TextEdit();
            this.txtPassword = new DevExpress.XtraEditors.TextEdit();
            this.dxValidationProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpRole.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrenom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConfirmPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.CB_Societe);
            this.panelControl1.Controls.Add(this.LB_Societe);
            this.panelControl1.Controls.Add(this.lkpRole);
            this.panelControl1.Controls.Add(this.labRole);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.labelControl8);
            this.panelControl1.Controls.Add(this.labelControl7);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.labelControl22);
            this.panelControl1.Controls.Add(this.txtUserName);
            this.panelControl1.Controls.Add(this.txtNom);
            this.panelControl1.Controls.Add(this.txtPrenom);
            this.panelControl1.Controls.Add(this.txtConfirmPassword);
            this.panelControl1.Controls.Add(this.txtPassword);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(450, 222);
            this.panelControl1.TabIndex = 2;
            // 
            // CB_Societe
            // 
            this.CB_Societe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_Societe.FormattingEnabled = true;
            this.CB_Societe.Items.AddRange(new object[] {
            " "});
            this.CB_Societe.Location = new System.Drawing.Point(91, 164);
            this.CB_Societe.Name = "CB_Societe";
            this.CB_Societe.Size = new System.Drawing.Size(202, 21);
            this.CB_Societe.TabIndex = 155;
            this.CB_Societe.SelectedValueChanged += new System.EventHandler(this.CB_Societe_SelectedValueChanged);
            // 
            // LB_Societe
            // 
            this.LB_Societe.Location = new System.Drawing.Point(43, 167);
            this.LB_Societe.Name = "LB_Societe";
            this.LB_Societe.Size = new System.Drawing.Size(42, 13);
            this.LB_Societe.TabIndex = 154;
            this.LB_Societe.Text = "Société :";
            // 
            // lkpRole
            // 
            this.lkpRole.EditValue = " ";
            this.lkpRole.Location = new System.Drawing.Point(91, 191);
            this.lkpRole.Name = "lkpRole";
            this.lkpRole.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpRole.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.lkpRole.Size = new System.Drawing.Size(202, 20);
            this.lkpRole.TabIndex = 5;
            this.lkpRole.Tag = "RQ";
            // 
            // labRole
            // 
            this.labRole.Location = new System.Drawing.Point(57, 194);
            this.labRole.Name = "labRole";
            this.labRole.Size = new System.Drawing.Size(28, 13);
            this.labRole.TabIndex = 152;
            this.labRole.Text = "Rôle :";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(17, 143);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(68, 13);
            this.labelControl1.TabIndex = 150;
            this.labelControl1.Text = "Confirmation :";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(14, 117);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(71, 13);
            this.labelControl8.TabIndex = 149;
            this.labelControl8.Text = "Mot de passe :";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(57, 65);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(28, 13);
            this.labelControl7.TabIndex = 147;
            this.labelControl7.Text = "Nom :";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(42, 91);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(43, 13);
            this.labelControl3.TabIndex = 148;
            this.labelControl3.Text = "Prénom :";
            // 
            // labelControl22
            // 
            this.labelControl22.Location = new System.Drawing.Point(53, 38);
            this.labelControl22.Name = "labelControl22";
            this.labelControl22.Size = new System.Drawing.Size(32, 13);
            this.labelControl22.TabIndex = 146;
            this.labelControl22.Text = "Login :";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(91, 34);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(202, 20);
            this.txtUserName.TabIndex = 0;
            // 
            // txtNom
            // 
            this.txtNom.Location = new System.Drawing.Point(91, 61);
            this.txtNom.Name = "txtNom";
            this.txtNom.Size = new System.Drawing.Size(202, 20);
            this.txtNom.TabIndex = 1;
            // 
            // txtPrenom
            // 
            this.txtPrenom.Location = new System.Drawing.Point(91, 87);
            this.txtPrenom.Name = "txtPrenom";
            this.txtPrenom.Size = new System.Drawing.Size(202, 20);
            this.txtPrenom.TabIndex = 2;
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.Location = new System.Drawing.Point(91, 139);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.Properties.PasswordChar = '•';
            this.txtConfirmPassword.Size = new System.Drawing.Size(202, 20);
            this.txtConfirmPassword.TabIndex = 4;
            this.txtConfirmPassword.Validating += new System.ComponentModel.CancelEventHandler(this.txtConfirmPassword_Validating);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(91, 113);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Properties.PasswordChar = '•';
            this.txtPassword.Size = new System.Drawing.Size(202, 20);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.EditValueChanged += new System.EventHandler(this.txtPassword_EditValueChanged);
            // 
            // FrmUtilisateur
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.ClientSize = new System.Drawing.Size(450, 222);
            this.ControlBox = false;
            this.Controls.Add(this.panelControl1);
            this.Name = "FrmUtilisateur";
            this.Text = "frmUtilisateur";
            this.Load += new System.EventHandler(this.frmUtilisateur_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpRole.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrenom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConfirmPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.ComboBox CB_Societe;
        private DevExpress.XtraEditors.LabelControl LB_Societe;
        private DevExpress.XtraEditors.ComboBoxEdit lkpRole;
        private DevExpress.XtraEditors.LabelControl labRole;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl22;
        private DevExpress.XtraEditors.TextEdit txtUserName;
        private DevExpress.XtraEditors.TextEdit txtNom;
        private DevExpress.XtraEditors.TextEdit txtPrenom;
        private DevExpress.XtraEditors.TextEdit txtConfirmPassword;
        private DevExpress.XtraEditors.TextEdit txtPassword;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProvider1;

    }
}