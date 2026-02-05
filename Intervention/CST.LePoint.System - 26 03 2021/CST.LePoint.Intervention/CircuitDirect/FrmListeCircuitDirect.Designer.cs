namespace CST.LePoint.Intervention.CircuitDirect
{
    partial class FrmListeCircuitDirect
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
            this.groupControlDetail = new DevExpress.XtraEditors.GroupControl();
            this.gridC = new DevExpress.XtraGrid.GridControl();
            this.gridV = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lkpTarif = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridVDetail = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lookUpEdit1 = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.gmap = new GMap.NET.WindowsForms.GMapControl();
            this.ChkGPS = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlDetail)).BeginInit();
            this.groupControlDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTarif.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChkGPS.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControlDetail
            // 
            this.groupControlDetail.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControlDetail.Appearance.Options.UseFont = true;
            this.groupControlDetail.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControlDetail.AppearanceCaption.ForeColor = System.Drawing.Color.MidnightBlue;
            this.groupControlDetail.AppearanceCaption.Options.UseFont = true;
            this.groupControlDetail.AppearanceCaption.Options.UseForeColor = true;
            this.groupControlDetail.Controls.Add(this.gridC);
            this.groupControlDetail.Controls.Add(this.lkpTarif);
            this.groupControlDetail.Controls.Add(this.labelControl2);
            this.groupControlDetail.Location = new System.Drawing.Point(14, 33);
            this.groupControlDetail.Name = "groupControlDetail";
            this.groupControlDetail.Size = new System.Drawing.Size(627, 289);
            this.groupControlDetail.TabIndex = 3;
            this.groupControlDetail.Text = "Circuits";
            // 
            // gridC
            // 
            this.gridC.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridC.Location = new System.Drawing.Point(2, 21);
            this.gridC.MainView = this.gridV;
            this.gridC.Name = "gridC";
            this.gridC.Size = new System.Drawing.Size(623, 266);
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
            this.gridV.DoubleClick += new System.EventHandler(this.gridV_DoubleClick);
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
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl1.Appearance.Options.UseFont = true;
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl1.AppearanceCaption.ForeColor = System.Drawing.Color.MidnightBlue;
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl1.Controls.Add(this.gridControl1);
            this.groupControl1.Controls.Add(this.lookUpEdit1);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Location = new System.Drawing.Point(16, 328);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(625, 349);
            this.groupControl1.TabIndex = 42;
            this.groupControl1.Text = "Liste Client";
            // 
            // gridControl1
            // 
            this.gridControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(2, 21);
            this.gridControl1.MainView = this.gridVDetail;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(621, 326);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridVDetail});
            // 
            // gridVDetail
            // 
            this.gridVDetail.GridControl = this.gridControl1;
            this.gridVDetail.Name = "gridVDetail";
            this.gridVDetail.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridVDetail_RowCellStyle);
            // 
            // lookUpEdit1
            // 
            this.lookUpEdit1.Location = new System.Drawing.Point(718, 109);
            this.lookUpEdit1.Name = "lookUpEdit1";
            this.lookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEdit1.Properties.NullText = "";
            this.lookUpEdit1.Size = new System.Drawing.Size(170, 20);
            this.lookUpEdit1.TabIndex = 8;
            this.lookUpEdit1.Visible = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(683, 116);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(29, 13);
            this.labelControl1.TabIndex = 41;
            this.labelControl1.Text = "Tarif :";
            this.labelControl1.Visible = false;
            // 
            // panelControl2
            // 
            this.panelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl2.Controls.Add(this.gmap);
            this.panelControl2.Location = new System.Drawing.Point(647, 12);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(570, 665);
            this.panelControl2.TabIndex = 104;
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
            this.gmap.Size = new System.Drawing.Size(560, 655);
            this.gmap.TabIndex = 100;
            this.gmap.Zoom = 0D;
            this.gmap.OnMarkerEnter += new GMap.NET.WindowsForms.MarkerEnter(this.gmap_OnMarkerEnter);
            // 
            // ChkGPS
            // 
            this.ChkGPS.EditValue = true;
            this.ChkGPS.Location = new System.Drawing.Point(590, 7);
            this.ChkGPS.Name = "ChkGPS";
            this.ChkGPS.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.ChkGPS.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.ChkGPS.Properties.Appearance.Options.UseFont = true;
            this.ChkGPS.Properties.Appearance.Options.UseForeColor = true;
            this.ChkGPS.Properties.Caption = "GPS";
            this.ChkGPS.Size = new System.Drawing.Size(49, 20);
            this.ChkGPS.TabIndex = 105;
            this.ChkGPS.CheckedChanged += new System.EventHandler(this.ChkGPS_CheckedChanged);
            // 
            // FrmCircuitListe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1229, 689);
            this.Controls.Add(this.groupControlDetail);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.ChkGPS);
            this.Controls.Add(this.panelControl2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmCircuitListe";
            this.Text = "FrmCircuitListe";
            this.Load += new System.EventHandler(this.FrmCircuitListe_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlDetail)).EndInit();
            this.groupControlDetail.ResumeLayout(false);
            this.groupControlDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTarif.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ChkGPS.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControlDetail;
        private DevExpress.XtraGrid.GridControl gridC;
        private DevExpress.XtraGrid.Views.Grid.GridView gridV;
        private DevExpress.XtraEditors.LookUpEdit lkpTarif;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridVDetail;
        private DevExpress.XtraEditors.LookUpEdit lookUpEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private GMap.NET.WindowsForms.GMapControl gmap;
        private DevExpress.XtraEditors.CheckEdit ChkGPS;
    }
}