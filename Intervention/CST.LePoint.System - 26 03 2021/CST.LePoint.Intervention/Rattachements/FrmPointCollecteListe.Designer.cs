namespace CST.LePoint.Intervention.Rattachements
{
    partial class FrmPointCollecteListe
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
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Code_Office = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Lib_Office = new DevExpress.XtraGrid.Columns.GridColumn();
            this.AdresseBien = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.gmap = new GMap.NET.WindowsForms.GMapControl();
            this.groupControlDetail = new DevExpress.XtraEditors.GroupControl();
            this.lkpTarif = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlDetail)).BeginInit();
            this.groupControlDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTarif.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.Code_Office,
            this.Lib_Office,
            this.AdresseBien});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridView1_RowClick);
            this.gridView1.DoubleClick += new System.EventHandler(this.grid_DoubleClick);
            // 
            // Code_Office
            // 
            this.Code_Office.Caption = "gridColumn1";
            this.Code_Office.Name = "Code_Office";
            this.Code_Office.Visible = true;
            this.Code_Office.VisibleIndex = 0;
            // 
            // Lib_Office
            // 
            this.Lib_Office.Caption = "gridColumn1";
            this.Lib_Office.Name = "Lib_Office";
            this.Lib_Office.Visible = true;
            this.Lib_Office.VisibleIndex = 1;
            // 
            // AdresseBien
            // 
            this.AdresseBien.Caption = "gridColumn1";
            this.AdresseBien.Name = "AdresseBien";
            this.AdresseBien.Visible = true;
            this.AdresseBien.VisibleIndex = 2;
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridControl1.Location = new System.Drawing.Point(5, 24);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(447, 401);
            this.gridControl1.TabIndex = 9;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // panelControl2
            // 
            this.panelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl2.Controls.Add(this.gmap);
            this.panelControl2.Location = new System.Drawing.Point(466, 12);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(863, 737);
            this.panelControl2.TabIndex = 105;
            // 
            // gmap
            // 
            this.gmap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gmap.Bearing = 0F;
            this.gmap.CanDragMap = true;
            this.gmap.EmptyTileColor = System.Drawing.Color.Navy;
            this.gmap.GrayScaleMode = false;
            this.gmap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gmap.LevelsKeepInMemmory = 5;
            this.gmap.Location = new System.Drawing.Point(6, 6);
            this.gmap.Margin = new System.Windows.Forms.Padding(4);
            this.gmap.MarkersEnabled = true;
            this.gmap.MaxZoom = 2;
            this.gmap.MinZoom = 2;
            this.gmap.MouseWheelZoomEnabled = true;
            this.gmap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gmap.Name = "gmap";
            this.gmap.NegativeMode = false;
            this.gmap.PolygonsEnabled = true;
            this.gmap.RetryLoadTile = 0;
            this.gmap.RoutesEnabled = true;
            this.gmap.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gmap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gmap.ShowTileGridLines = false;
            this.gmap.Size = new System.Drawing.Size(851, 725);
            this.gmap.TabIndex = 5;
            this.gmap.Zoom = 0D;
            this.gmap.OnMarkerEnter += new GMap.NET.WindowsForms.MarkerEnter(this.gmap_OnMarkerEnter);
            // 
            // groupControlDetail
            // 
            this.groupControlDetail.AppearanceCaption.ForeColor = System.Drawing.Color.Black;
            this.groupControlDetail.AppearanceCaption.Options.UseForeColor = true;
            this.groupControlDetail.Controls.Add(this.lkpTarif);
            this.groupControlDetail.Controls.Add(this.labelControl2);
            this.groupControlDetail.Controls.Add(this.gridControl1);
            this.groupControlDetail.Location = new System.Drawing.Point(12, 12);
            this.groupControlDetail.Name = "groupControlDetail";
            this.groupControlDetail.Size = new System.Drawing.Size(457, 430);
            this.groupControlDetail.TabIndex = 6;
            this.groupControlDetail.Text = "Points de Collecte";
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
            // FrmPointCollecteListe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1328, 749);
            this.Controls.Add(this.groupControlDetail);
            this.Controls.Add(this.panelControl2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmPointCollecteListe";
            this.Text = "FrmBien";
            this.Activated += new System.EventHandler(this.frmPointCollecteListe_Activated);
            this.Load += new System.EventHandler(this.frmPointCollecteListe_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlDetail)).EndInit();
            this.groupControlDetail.ResumeLayout(false);
            this.groupControlDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTarif.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn Code_Office;
        private DevExpress.XtraGrid.Columns.GridColumn Lib_Office;
        private DevExpress.XtraGrid.Columns.GridColumn AdresseBien;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private GMap.NET.WindowsForms.GMapControl gmap;
        private DevExpress.XtraEditors.GroupControl groupControlDetail;
        private DevExpress.XtraEditors.LookUpEdit lkpTarif;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}