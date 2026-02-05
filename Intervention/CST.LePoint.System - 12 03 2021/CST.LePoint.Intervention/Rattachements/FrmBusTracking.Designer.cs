namespace CST.LePoint.Intervention.Rattachements
{
    partial class FrmBusTracking
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
            this.MapPanel = new DevExpress.XtraEditors.PanelControl();
            this.gmap = new GMap.NET.WindowsForms.GMapControl();
            this.groupControlDetail = new DevExpress.XtraEditors.GroupControl();
            this.gridC = new DevExpress.XtraGrid.GridControl();
            this.gridV = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lkpTarif = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.MapPanel)).BeginInit();
            this.MapPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlDetail)).BeginInit();
            this.groupControlDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTarif.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // MapPanel
            // 
            this.MapPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MapPanel.Controls.Add(this.gmap);
            this.MapPanel.Location = new System.Drawing.Point(354, 12);
            this.MapPanel.Name = "MapPanel";
            this.MapPanel.Size = new System.Drawing.Size(641, 709);
            this.MapPanel.TabIndex = 105;
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
            this.gmap.Location = new System.Drawing.Point(5, 5);
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
            this.gmap.Size = new System.Drawing.Size(631, 699);
            this.gmap.TabIndex = 100;
            this.gmap.Zoom = 0D;
            this.gmap.OnMarkerClick += new GMap.NET.WindowsForms.MarkerClick(this.gmap_OnMarkerClick);
            this.gmap.OnMarkerEnter += new GMap.NET.WindowsForms.MarkerEnter(this.gmap_OnMarkerEnter);
            // 
            // groupControlDetail
            // 
            this.groupControlDetail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupControlDetail.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 9F);
            this.groupControlDetail.AppearanceCaption.ForeColor = System.Drawing.Color.Black;
            this.groupControlDetail.AppearanceCaption.Options.UseFont = true;
            this.groupControlDetail.AppearanceCaption.Options.UseForeColor = true;
            this.groupControlDetail.Controls.Add(this.gridC);
            this.groupControlDetail.Controls.Add(this.lkpTarif);
            this.groupControlDetail.Controls.Add(this.labelControl2);
            this.groupControlDetail.Location = new System.Drawing.Point(12, 12);
            this.groupControlDetail.Name = "groupControlDetail";
            this.groupControlDetail.Size = new System.Drawing.Size(336, 315);
            this.groupControlDetail.TabIndex = 106;
            this.groupControlDetail.Text = "Bus";
            // 
            // gridC
            // 
            this.gridC.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridC.Location = new System.Drawing.Point(2, 22);
            this.gridC.MainView = this.gridV;
            this.gridC.Name = "gridC";
            this.gridC.Size = new System.Drawing.Size(332, 291);
            this.gridC.TabIndex = 1;
            this.gridC.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridV});
            // 
            // gridV
            // 
            this.gridV.GridControl = this.gridC;
            this.gridV.Name = "gridV";
            this.gridV.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridV_RowCellStyle);
            this.gridV.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridV_CellValueChanging);
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
            this.labelControl2.Location = new System.Drawing.Point(683, 117);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(29, 13);
            this.labelControl2.TabIndex = 41;
            this.labelControl2.Text = "Tarif :";
            this.labelControl2.Visible = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(14, 350);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(51, 13);
            this.labelControl1.TabIndex = 107;
            this.labelControl1.Text = "Num IMM :";
            // 
            // FrmBusTracking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1007, 733);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.groupControlDetail);
            this.Controls.Add(this.MapPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmBusTracking";
            this.Text = "FrmBusTracking";
            this.Load += new System.EventHandler(this.FrmBusTracking_Load);
            this.Leave += new System.EventHandler(this.FrmBusTracking_Leave);
            ((System.ComponentModel.ISupportInitialize)(this.MapPanel)).EndInit();
            this.MapPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlDetail)).EndInit();
            this.groupControlDetail.ResumeLayout(false);
            this.groupControlDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTarif.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl MapPanel;
        private GMap.NET.WindowsForms.GMapControl gmap;
        private DevExpress.XtraEditors.GroupControl groupControlDetail;
        private DevExpress.XtraGrid.GridControl gridC;
        private DevExpress.XtraGrid.Views.Grid.GridView gridV;
        private DevExpress.XtraEditors.LookUpEdit lkpTarif;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}