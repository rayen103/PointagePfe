
namespace CST.LePoint.Intervention.Rattachements
{
    partial class FrmPointCollecte
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
            this.gmap = new GMap.NET.WindowsForms.GMapControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lkpRg = new DevExpress.XtraEditors.LookUpEdit();
            this.lkpGouv = new DevExpress.XtraEditors.LookUpEdit();
            this.textLong = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.Adressetxt = new DevExpress.XtraEditors.LabelControl();
            this.textBien = new DevExpress.XtraEditors.TextEdit();
            this.of = new DevExpress.XtraEditors.LabelControl();
            this.textLibBien = new DevExpress.XtraEditors.TextEdit();
            this.textLat = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpRg.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpGouv.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textLong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBien.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textLibBien.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textLat.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gmap
            // 
            this.gmap.Bearing = 0F;
            this.gmap.CanDragMap = true;
            this.gmap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gmap.EmptyTileColor = System.Drawing.Color.Navy;
            this.gmap.GrayScaleMode = false;
            this.gmap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gmap.LevelsKeepInMemmory = 5;
            this.gmap.Location = new System.Drawing.Point(2, 2);
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
            this.gmap.Size = new System.Drawing.Size(661, 440);
            this.gmap.TabIndex = 5;
            this.gmap.Zoom = 0D;
            this.gmap.OnMarkerEnter += new GMap.NET.WindowsForms.MarkerEnter(this.gmap_OnMarkerEnter);
            this.gmap.MouseClick += new System.Windows.Forms.MouseEventHandler(this.map_MouseClick);
            // 
            // panelControl2
            // 
            this.panelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelControl2.Controls.Add(this.gmap);
            this.panelControl2.Location = new System.Drawing.Point(12, 152);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(665, 444);
            this.panelControl2.TabIndex = 104;
            // 
            // groupControl2
            // 
            this.groupControl2.AppearanceCaption.ForeColor = System.Drawing.Color.Black;
            this.groupControl2.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl2.Controls.Add(this.labelControl4);
            this.groupControl2.Controls.Add(this.textLat);
            this.groupControl2.Controls.Add(this.labelControl2);
            this.groupControl2.Controls.Add(this.lkpRg);
            this.groupControl2.Controls.Add(this.lkpGouv);
            this.groupControl2.Controls.Add(this.textLong);
            this.groupControl2.Controls.Add(this.labelControl1);
            this.groupControl2.Controls.Add(this.labelControl3);
            this.groupControl2.Controls.Add(this.Adressetxt);
            this.groupControl2.Controls.Add(this.textBien);
            this.groupControl2.Controls.Add(this.of);
            this.groupControl2.Controls.Add(this.textLibBien);
            this.groupControl2.Location = new System.Drawing.Point(12, 12);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(665, 134);
            this.groupControl2.TabIndex = 5;
            this.groupControl2.Text = "Point de Collecte";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(213, 71);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(40, 13);
            this.labelControl2.TabIndex = 34;
            this.labelControl2.Text = "Region :";
            // 
            // lkpRg
            // 
            this.lkpRg.EnterMoveNextControl = true;
            this.lkpRg.Location = new System.Drawing.Point(256, 68);
            this.lkpRg.Name = "lkpRg";
            this.lkpRg.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpRg.Size = new System.Drawing.Size(137, 20);
            this.lkpRg.TabIndex = 33;
            // 
            // lkpGouv
            // 
            this.lkpGouv.EnterMoveNextControl = true;
            this.lkpGouv.Location = new System.Drawing.Point(83, 68);
            this.lkpGouv.Name = "lkpGouv";
            this.lkpGouv.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpGouv.Size = new System.Drawing.Size(121, 20);
            this.lkpGouv.TabIndex = 32;
            // 
            // textLong
            // 
            this.textLong.Location = new System.Drawing.Point(83, 94);
            this.textLong.Name = "textLong";
            this.textLong.Size = new System.Drawing.Size(121, 20);
            this.textLong.TabIndex = 30;
           // this.textLong.EditValueChanged += new System.EventHandler(this.textLong_EditValueChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 97);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(72, 13);
            this.labelControl1.TabIndex = 31;
            this.labelControl1.Text = "Longitude       :";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(213, 38);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(39, 13);
            this.labelControl3.TabIndex = 29;
            this.labelControl3.Text = "Libellé  :";
            // 
            // Adressetxt
            // 
            this.Adressetxt.Location = new System.Drawing.Point(7, 71);
            this.Adressetxt.Name = "Adressetxt";
            this.Adressetxt.Size = new System.Drawing.Size(71, 13);
            this.Adressetxt.TabIndex = 4;
            this.Adressetxt.Text = "Gouvernorat  :";
            // 
            // textBien
            // 
            this.textBien.Location = new System.Drawing.Point(83, 35);
            this.textBien.Name = "textBien";
            this.textBien.Size = new System.Drawing.Size(121, 20);
            this.textBien.TabIndex = 0;
            this.textBien.EditValueChanged += new System.EventHandler(this.textBien_EditValueChanged);
            // 
            // of
            // 
            this.of.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.of.Location = new System.Drawing.Point(6, 38);
            this.of.Name = "of";
            this.of.Size = new System.Drawing.Size(71, 13);
            this.of.TabIndex = 1;
            this.of.Text = "Code              :";
            // 
            // textLibBien
            // 
            this.textLibBien.Location = new System.Drawing.Point(253, 35);
            this.textLibBien.Name = "textLibBien";
            this.textLibBien.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.textLibBien.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.textLibBien.Properties.Appearance.Options.UseBackColor = true;
            this.textLibBien.Properties.Appearance.Options.UseForeColor = true;
            this.textLibBien.Size = new System.Drawing.Size(233, 20);
            this.textLibBien.TabIndex = 2;
            // 
            // textLat
            // 
            this.textLat.EditValue = "";
            this.textLat.Location = new System.Drawing.Point(272, 94);
            this.textLat.Name = "textLat";
            this.textLat.Size = new System.Drawing.Size(121, 20);
            this.textLat.TabIndex = 35;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(213, 97);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(46, 13);
            this.labelControl4.TabIndex = 36;
            this.labelControl4.Text = "Latitude :";
            // 
            // FrmPointCollecte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1213, 608);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.panelControl2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmPointCollecte";
            this.Text = "FrmPointCollecte";
            this.Activated += new System.EventHandler(this.frmPointCollecte_Load);
            this.Load += new System.EventHandler(this.frmPointCollecte_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpRg.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpGouv.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textLong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBien.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textLibBien.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textLat.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProvider1;
        private GMap.NET.WindowsForms.GMapControl gmap;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl Adressetxt;
        private DevExpress.XtraEditors.TextEdit textBien;
        private DevExpress.XtraEditors.LabelControl of;
        private DevExpress.XtraEditors.TextEdit textLibBien;
        private DevExpress.XtraEditors.TextEdit textLong;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LookUpEdit lkpGouv;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LookUpEdit lkpRg;
        private DevExpress.XtraEditors.TextEdit textLat;
        private DevExpress.XtraEditors.LabelControl labelControl4;
    }
}