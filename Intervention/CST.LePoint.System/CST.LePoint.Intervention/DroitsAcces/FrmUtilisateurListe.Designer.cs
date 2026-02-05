namespace CST.LePoint.Intervention.DroitsAcces
{
    partial class FrmUtilisateurListe
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
            this.gridColUserId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColUserName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColNom = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColPrenom = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColSociete = new DevExpress.XtraGrid.Columns.GridColumn();
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
            this.gridColUserId,
            this.gridColUserName,
            this.gridColNom,
            this.gridColPrenom,
            this.gridColSociete});
            this.gridV.GridControl = this.grid;
            this.gridV.Name = "gridV";
            this.gridV.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridV.OptionsView.ShowGroupPanel = false;
            this.gridV.OptionsView.ShowIndicator = false;
            // 
            // gridColUserId
            // 
            this.gridColUserId.Caption = "lblUserId";
            this.gridColUserId.FieldName = "Id";
            this.gridColUserId.Name = "gridColUserId";
            this.gridColUserId.OptionsColumn.AllowEdit = false;
            this.gridColUserId.Width = 100;
            // 
            // gridColUserName
            // 
            this.gridColUserName.Caption = "Login";
            this.gridColUserName.FieldName = "Login";
            this.gridColUserName.Name = "gridColUserName";
            this.gridColUserName.OptionsColumn.AllowEdit = false;
            this.gridColUserName.Visible = true;
            this.gridColUserName.VisibleIndex = 0;
            this.gridColUserName.Width = 200;
            // 
            // gridColNom
            // 
            this.gridColNom.Caption = "Nom";
            this.gridColNom.FieldName = "Nom";
            this.gridColNom.Name = "gridColNom";
            this.gridColNom.OptionsColumn.AllowEdit = false;
            this.gridColNom.Visible = true;
            this.gridColNom.VisibleIndex = 1;
            this.gridColNom.Width = 200;
            // 
            // gridColPrenom
            // 
            this.gridColPrenom.Caption = "Prénom";
            this.gridColPrenom.FieldName = "Prenom";
            this.gridColPrenom.Name = "gridColPrenom";
            this.gridColPrenom.OptionsColumn.AllowEdit = false;
            this.gridColPrenom.Visible = true;
            this.gridColPrenom.VisibleIndex = 2;
            this.gridColPrenom.Width = 200;
            // 
            // gridColSociete
            // 
            this.gridColSociete.Caption = "Société";
            this.gridColSociete.FieldName = "Societe";
            this.gridColSociete.Name = "gridColSociete";
            this.gridColSociete.OptionsColumn.AllowEdit = false;
            this.gridColSociete.Visible = true;
            this.gridColSociete.VisibleIndex = 3;
            this.gridColSociete.Width = 200;
            // 
            // frmUtilisateurListe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 532);
            this.Controls.Add(this.grid);
            this.Name = "frmUtilisateurListe";
            this.Text = "frmGestionUtilisateur";
            this.Activated += new System.EventHandler(this.frmUtilisateurListe_Activated);
            this.Load += new System.EventHandler(this.frmUtilisateurListe_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grid;
        private DevExpress.XtraGrid.Views.Grid.GridView gridV;
        private DevExpress.XtraGrid.Columns.GridColumn gridColUserId;
        private DevExpress.XtraGrid.Columns.GridColumn gridColUserName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColSociete;
        private DevExpress.XtraGrid.Columns.GridColumn gridColNom;
        private DevExpress.XtraGrid.Columns.GridColumn gridColPrenom;
    }
}