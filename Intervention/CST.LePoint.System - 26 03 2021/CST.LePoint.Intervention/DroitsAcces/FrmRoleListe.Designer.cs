using DevExpress.XtraEditors.Repository;
namespace CST.LePoint.Intervention.DroitsAcces
{
    partial class FrmRoleListe
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
            this.gridColIdRole = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColNomRole = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColDescRole = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new RepositoryItemMemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // grid
            // 
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Location = new System.Drawing.Point(0, 0);
            this.grid.MainView = this.gridV;
            this.grid.Name = "grid";
            this.grid.RepositoryItems.AddRange(new RepositoryItem[] {
            this.repositoryItemMemoEdit1});
            this.grid.Size = new System.Drawing.Size(734, 589);
            this.grid.TabIndex = 0;
            this.grid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridV});
            this.grid.DoubleClick += new System.EventHandler(this.grid_DoubleClick);
            this.grid.MouseClick += new System.Windows.Forms.MouseEventHandler(this.grid_MouseClick);
            // 
            // gridV
            // 
            this.gridV.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColIdRole,
            this.gridColNomRole,
            this.gridColDescRole});
            this.gridV.GridControl = this.grid;
            this.gridV.Name = "gridV";
            this.gridV.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridV.OptionsView.RowAutoHeight = true;
            this.gridV.OptionsView.ShowGroupPanel = false;
            this.gridV.OptionsView.ShowIndicator = false;
            // 
            // gridColIdRole
            // 
            this.gridColIdRole.Caption = "lblId";
            this.gridColIdRole.FieldName = "Id";
            this.gridColIdRole.Name = "gridColIdRole";
            this.gridColIdRole.OptionsColumn.AllowEdit = false;
            this.gridColIdRole.Width = 80;
            // 
            // gridColNomRole
            // 
            this.gridColNomRole.Caption = "lblNom";
            this.gridColNomRole.FieldName = "Nom";
            this.gridColNomRole.Name = "gridColNomRole";
            this.gridColNomRole.OptionsColumn.AllowEdit = false;
            this.gridColNomRole.Visible = true;
            this.gridColNomRole.VisibleIndex = 0;
            this.gridColNomRole.Width = 321;
            // 
            // gridColDescRole
            // 
            this.gridColDescRole.Caption = "lblDesc";
            this.gridColDescRole.ColumnEdit = this.repositoryItemMemoEdit1;
            this.gridColDescRole.FieldName = "Description";
            this.gridColDescRole.Name = "gridColDescRole";
            this.gridColDescRole.OptionsColumn.AllowEdit = false;
            this.gridColDescRole.Visible = true;
            this.gridColDescRole.VisibleIndex = 1;
            this.gridColDescRole.Width = 331;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            this.repositoryItemMemoEdit1.ReadOnly = true;
            // 
            // frmRolesListe
            // 
            this.ClientSize = new System.Drawing.Size(734, 589);
            this.Controls.Add(this.grid);
            this.Name = "frmRolesListe";
            this.Text = "frmRolesListe";
            this.Activated += new System.EventHandler(this.frmRolesListe_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grid;
        private DevExpress.XtraGrid.Views.Grid.GridView gridV;
        private DevExpress.XtraGrid.Columns.GridColumn gridColIdRole;
        private DevExpress.XtraGrid.Columns.GridColumn gridColNomRole;
        private DevExpress.XtraGrid.Columns.GridColumn gridColDescRole;
        private RepositoryItemMemoEdit repositoryItemMemoEdit1;

    }
}