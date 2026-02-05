namespace CST.LePoint.Intervention.DroitsAcces
{
    partial class FrmParametre
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
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.txtindication = new System.Windows.Forms.RichTextBox();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.Paramér = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtdescription = new System.Windows.Forms.RichTextBox();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtnomprm = new DevExpress.XtraEditors.TextEdit();
            this.txtval = new DevExpress.XtraEditors.TextEdit();
            this.txtcode = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtnomprm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtval.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcode.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.gridControl1);
            this.groupControl1.Location = new System.Drawing.Point(12, 12);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(489, 406);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Paramétres Utilisés";
            // 
            // gridControl1
            // 
            this.gridControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(2, 21);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(485, 383);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridView1_RowClick);
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.txtindication);
            this.groupControl2.Controls.Add(this.labelControl3);
            this.groupControl2.Controls.Add(this.Paramér);
            this.groupControl2.Controls.Add(this.labelControl2);
            this.groupControl2.Controls.Add(this.txtdescription);
            this.groupControl2.Controls.Add(this.labelControl1);
            this.groupControl2.Controls.Add(this.txtnomprm);
            this.groupControl2.Controls.Add(this.txtval);
            this.groupControl2.Controls.Add(this.txtcode);
            this.groupControl2.Location = new System.Drawing.Point(520, 12);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(383, 406);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.Text = "Description du paramétre";
            // 
            // txtindication
            // 
            this.txtindication.BackColor = System.Drawing.Color.MistyRose;
            this.txtindication.Location = new System.Drawing.Point(59, 207);
            this.txtindication.Name = "txtindication";
            this.txtindication.Size = new System.Drawing.Size(250, 68);
            this.txtindication.TabIndex = 13;
            this.txtindication.Text = "";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(4, 188);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(57, 13);
            this.labelControl3.TabIndex = 12;
            this.labelControl3.Text = "Indication";
            // 
            // Paramér
            // 
            this.Paramér.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Paramér.Location = new System.Drawing.Point(4, 27);
            this.Paramér.Name = "Paramér";
            this.Paramér.Size = new System.Drawing.Size(61, 13);
            this.Paramér.TabIndex = 11;
            this.Paramér.Text = "Paramétre";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(4, 67);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(64, 13);
            this.labelControl2.TabIndex = 10;
            this.labelControl2.Text = "Description";
            // 
            // txtdescription
            // 
            this.txtdescription.BackColor = System.Drawing.Color.MistyRose;
            this.txtdescription.Location = new System.Drawing.Point(59, 86);
            this.txtdescription.Name = "txtdescription";
            this.txtdescription.Size = new System.Drawing.Size(250, 96);
            this.txtdescription.TabIndex = 9;
            this.txtdescription.Text = "";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(8, 337);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(93, 13);
            this.labelControl1.TabIndex = 8;
            this.labelControl1.Text = "Valeur Affectée :";
            // 
            // txtnomprm
            // 
            this.txtnomprm.Enabled = false;
            this.txtnomprm.Location = new System.Drawing.Point(66, 41);
            this.txtnomprm.Name = "txtnomprm";
            this.txtnomprm.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose;
            this.txtnomprm.Properties.Appearance.Options.UseBackColor = true;
            this.txtnomprm.Size = new System.Drawing.Size(243, 20);
            this.txtnomprm.TabIndex = 7;
            // 
            // txtval
            // 
            this.txtval.Location = new System.Drawing.Point(107, 334);
            this.txtval.Name = "txtval";
            this.txtval.Properties.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtval.Properties.Appearance.Options.UseBackColor = true;
            this.txtval.Size = new System.Drawing.Size(121, 20);
            this.txtval.TabIndex = 6;
            // 
            // txtcode
            // 
            this.txtcode.Enabled = false;
            this.txtcode.Location = new System.Drawing.Point(327, 24);
            this.txtcode.Name = "txtcode";
            this.txtcode.Properties.Appearance.BackColor = System.Drawing.Color.AntiqueWhite;
            this.txtcode.Properties.Appearance.Options.UseBackColor = true;
            this.txtcode.Size = new System.Drawing.Size(51, 20);
            this.txtcode.TabIndex = 0;
            this.txtcode.Visible = false;
            // 
            // FrmParametre
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 439);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmParametre";
            this.Text = "FrmParametre";
            this.Load += new System.EventHandler(this.FrmParametre_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtnomprm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtval.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcode.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private System.Windows.Forms.RichTextBox txtdescription;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtnomprm;
        private DevExpress.XtraEditors.TextEdit txtval;
        private DevExpress.XtraEditors.TextEdit txtcode;
        private System.Windows.Forms.RichTextBox txtindication;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl Paramér;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}