namespace CST.LePoint.Intervention.Rattachements
{
    partial class FrmDeplacementClientCircuit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDeplacementClientCircuit));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridVClientS = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridVCircuitS = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.gridControl3 = new DevExpress.XtraGrid.GridControl();
            this.gridVClientD = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridControl4 = new DevExpress.XtraGrid.GridControl();
            this.gridVCircuitD = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.BtnDeplacer = new DevExpress.XtraEditors.SimpleButton();
            this.BtnAnnuler = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVClientS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVCircuitS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVClientD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVCircuitD)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.ForeColor = System.Drawing.Color.Black;
            this.groupControl1.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl1.Controls.Add(this.gridControl2);
            this.groupControl1.Controls.Add(this.gridControl1);
            this.groupControl1.Location = new System.Drawing.Point(12, 12);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(381, 721);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Circuit Source";
            // 
            // gridControl2
            // 
            this.gridControl2.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridControl2.Location = new System.Drawing.Point(5, 261);
            this.gridControl2.MainView = this.gridVClientS;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.Size = new System.Drawing.Size(371, 455);
            this.gridControl2.TabIndex = 2;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridVClientS});
            // 
            // gridVClientS
            // 
            this.gridVClientS.GridControl = this.gridControl2;
            this.gridVClientS.Name = "gridVClientS";
            this.gridVClientS.OptionsSelection.MultiSelect = true;
            this.gridVClientS.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridVClientS.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridVClientS_RowStyle);
            // 
            // gridControl1
            // 
            this.gridControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridControl1.Location = new System.Drawing.Point(5, 24);
            this.gridControl1.MainView = this.gridVCircuitS;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(371, 231);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridVCircuitS});
            // 
            // gridVCircuitS
            // 
            this.gridVCircuitS.GridControl = this.gridControl1;
            this.gridVCircuitS.Name = "gridVCircuitS";
            this.gridVCircuitS.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridVCircuitS_RowClick);
            this.gridVCircuitS.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridVCircuitS_RowStyle);
            // 
            // groupControl2
            // 
            this.groupControl2.AppearanceCaption.ForeColor = System.Drawing.Color.Black;
            this.groupControl2.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl2.Controls.Add(this.gridControl3);
            this.groupControl2.Controls.Add(this.gridControl4);
            this.groupControl2.Location = new System.Drawing.Point(499, 12);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(381, 716);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.Text = "Circuit Destinataire";
            // 
            // gridControl3
            // 
            this.gridControl3.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridControl3.Location = new System.Drawing.Point(5, 261);
            this.gridControl3.MainView = this.gridVClientD;
            this.gridControl3.Name = "gridControl3";
            this.gridControl3.Size = new System.Drawing.Size(371, 455);
            this.gridControl3.TabIndex = 6;
            this.gridControl3.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridVClientD});
            // 
            // gridVClientD
            // 
            this.gridVClientD.GridControl = this.gridControl3;
            this.gridVClientD.Name = "gridVClientD";
            this.gridVClientD.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridVClientD_RowStyle);
            // 
            // gridControl4
            // 
            this.gridControl4.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridControl4.Location = new System.Drawing.Point(5, 24);
            this.gridControl4.MainView = this.gridVCircuitD;
            this.gridControl4.Name = "gridControl4";
            this.gridControl4.Size = new System.Drawing.Size(371, 231);
            this.gridControl4.TabIndex = 4;
            this.gridControl4.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridVCircuitD});
            // 
            // gridVCircuitD
            // 
            this.gridVCircuitD.GridControl = this.gridControl4;
            this.gridVCircuitD.Name = "gridVCircuitD";
            this.gridVCircuitD.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridVCircuitD_RowClick);
            this.gridVCircuitD.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridVCircuitD_RowStyle);
            // 
            // BtnDeplacer
            // 
            this.BtnDeplacer.Image = ((System.Drawing.Image)(resources.GetObject("BtnDeplacer.Image")));
            this.BtnDeplacer.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.BtnDeplacer.Location = new System.Drawing.Point(399, 273);
            this.BtnDeplacer.Name = "BtnDeplacer";
            this.BtnDeplacer.Size = new System.Drawing.Size(94, 45);
            this.BtnDeplacer.TabIndex = 2;
            this.BtnDeplacer.Text = "Déplacer";
            this.BtnDeplacer.Click += new System.EventHandler(this.BtnDeplacer_Click);
            // 
            // BtnAnnuler
            // 
            this.BtnAnnuler.Image = ((System.Drawing.Image)(resources.GetObject("BtnAnnuler.Image")));
            this.BtnAnnuler.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.BtnAnnuler.Location = new System.Drawing.Point(399, 324);
            this.BtnAnnuler.Name = "BtnAnnuler";
            this.BtnAnnuler.Size = new System.Drawing.Size(94, 45);
            this.BtnAnnuler.TabIndex = 3;
            this.BtnAnnuler.Text = "Annuler";
            this.BtnAnnuler.Click += new System.EventHandler(this.BtnAnnuler_Click);
            // 
            // FrmDeplacementClientCircuit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(892, 745);
            this.ClientSize = new System.Drawing.Size(892, 745);
            this.Controls.Add(this.BtnAnnuler);
            this.Controls.Add(this.BtnDeplacer);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmDeplacementClientCircuit";
            this.Text = "FrmDeplacementClientCircuit";
            this.Load += new System.EventHandler(this.FrmDeplacementClientCircuit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVClientS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVCircuitS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVClientD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVCircuitD)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridVClientS;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridVCircuitS;
        private DevExpress.XtraEditors.SimpleButton BtnDeplacer;
        private DevExpress.XtraGrid.GridControl gridControl3;
        private DevExpress.XtraGrid.Views.Grid.GridView gridVClientD;
        private DevExpress.XtraGrid.GridControl gridControl4;
        private DevExpress.XtraGrid.Views.Grid.GridView gridVCircuitD;
        private DevExpress.XtraEditors.SimpleButton BtnAnnuler;
    }
}