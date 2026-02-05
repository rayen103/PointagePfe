namespace CST.LePoint.Intervention.DroitsAcces
{
    partial class FrmSocieteListe
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

            this.grid = new CST.LePoint.CtrlLibrary.DevExpressEx.GridControlEx();
            this.gridV = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColIdSociete = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColNomSociete = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColDescSociete = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridV)).BeginInit();
            this.SuspendLayout();
            // 
            // grid
            // 
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Location = new System.Drawing.Point(0, 0);
            this.grid.MainView = this.gridV;
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(666, 532);
            this.grid.TabIndex = 0;
            this.grid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridV});
            this.grid.DoubleClick += new System.EventHandler(this.grid_DoubleClick);
            this.grid.MouseClick += new System.Windows.Forms.MouseEventHandler(this.grid_MouseClick);
            // 
            // gridV
            // 
            this.gridV.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColIdSociete,
            this.gridColNomSociete,
            this.gridColDescSociete});
            this.gridV.GridControl = this.grid;
            this.gridV.Name = "gridV";
            this.gridV.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridV.OptionsView.ShowGroupPanel = false;
            this.gridV.OptionsView.ShowIndicator = false;
            // 
            // gridColIdSociete
            // 
            this.gridColIdSociete.Caption = "Id";
            this.gridColIdSociete.FieldName = "Id";
            this.gridColIdSociete.Name = "gridColIdSociete";
            this.gridColIdSociete.OptionsColumn.AllowEdit = false;
            this.gridColIdSociete.Width = 100;
            // 
            // gridColNomSociete
            // 
            this.gridColNomSociete.Caption = "Nom";
            this.gridColNomSociete.FieldName = "Nom";
            this.gridColNomSociete.Name = "gridColNomSociete";
            this.gridColNomSociete.OptionsColumn.AllowEdit = false;
            this.gridColNomSociete.Visible = true;
            this.gridColNomSociete.VisibleIndex = 0;
            this.gridColNomSociete.Width = 200;
            // 
            // gridColDescSocietes
            // 
            this.gridColDescSociete.Caption = "Description";
            this.gridColDescSociete.FieldName = "Description";
            this.gridColDescSociete.Name = "gridColDescSocietes";
            this.gridColDescSociete.OptionsColumn.AllowEdit = false;
            this.gridColDescSociete.Visible = true;
            this.gridColDescSociete.VisibleIndex = 1;
            this.gridColDescSociete.Width = 464;
            // 
            // frmSocieteListe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 532);
            this.Controls.Add(this.grid);
            this.Name = "frmSocieteListe";
            this.Text = "frmSocieteListe";
            this.Activated += new System.EventHandler(this.frmSocieteListe_Activated);
            this.Load += new System.EventHandler(this.frmSocieteListe_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grid;
        private DevExpress.XtraGrid.Views.Grid.GridView gridV;
        private DevExpress.XtraGrid.Columns.GridColumn gridColIdSociete;
        private DevExpress.XtraGrid.Columns.GridColumn gridColNomSociete;
        //private DevExpress.XtraGrid.Columns.GridColumn gridColDescSociete;
        private DevExpress.XtraGrid.Columns.GridColumn gridColDescSociete;
    }
}