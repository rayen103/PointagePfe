namespace CST.LePoint.Intervention.Rattachements
{
    partial class FrmCircuit
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
            this.groupControlDetail = new DevExpress.XtraEditors.GroupControl();
            this.chkallcheck = new DevExpress.XtraEditors.CheckEdit();
            this.gridC = new DevExpress.XtraGrid.GridControl();
            this.gridV = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtCCircuit = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtDuree = new DevExpress.XtraEditors.SpinEdit();
            this.txtKm = new DevExpress.XtraEditors.SpinEdit();
            this.lkpPointFin = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.lkpPointDepart = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtLibelle = new DevExpress.XtraEditors.TextEdit();
            this.ChkGPS = new DevExpress.XtraEditors.CheckEdit();
            this.dxValidationProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.gmap = new GMap.NET.WindowsForms.GMapControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlDetail)).BeginInit();
            this.groupControlDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkallcheck.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCCircuit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDuree.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpPointFin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpPointDepart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLibelle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChkGPS.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupControlDetail
            // 
            this.groupControlDetail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupControlDetail.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControlDetail.Appearance.Options.UseFont = true;
            this.groupControlDetail.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControlDetail.AppearanceCaption.Options.UseFont = true;
            this.groupControlDetail.Controls.Add(this.chkallcheck);
            this.groupControlDetail.Controls.Add(this.gridC);
            this.groupControlDetail.Location = new System.Drawing.Point(12, 123);
            this.groupControlDetail.Name = "groupControlDetail";
            this.groupControlDetail.Size = new System.Drawing.Size(701, 475);
            this.groupControlDetail.TabIndex = 2;
            this.groupControlDetail.Text = "Points de Collecte";
            // 
            // chkallcheck
            // 
            this.chkallcheck.Location = new System.Drawing.Point(5, 24);
            this.chkallcheck.Name = "chkallcheck";
            this.chkallcheck.Properties.AutoWidth = true;
            this.chkallcheck.Properties.Caption = "Tout sélectionner";
            this.chkallcheck.Size = new System.Drawing.Size(105, 19);
            this.chkallcheck.TabIndex = 69;
            this.chkallcheck.CheckedChanged += new System.EventHandler(this.chkallcheck_CheckedChanged);
            // 
            // gridC
            // 
            this.gridC.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gridC.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridC.Location = new System.Drawing.Point(2, 49);
            this.gridC.MainView = this.gridV;
            this.gridC.Name = "gridC";
            this.gridC.Size = new System.Drawing.Size(699, 421);
            this.gridC.TabIndex = 1;
            this.gridC.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridV});
            // 
            // gridV
            // 
            this.gridV.GridControl = this.gridC;
            this.gridV.Name = "gridV";
            this.gridV.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridV_CellValueChanging);
            // 
            // txtCCircuit
            // 
            this.txtCCircuit.Location = new System.Drawing.Point(105, 30);
            this.txtCCircuit.Name = "txtCCircuit";
            this.txtCCircuit.Size = new System.Drawing.Size(140, 20);
            this.txtCCircuit.TabIndex = 0;
            this.txtCCircuit.Tag = "RQ";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(17, 33);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(37, 13);
            this.labelControl1.TabIndex = 28;
            this.labelControl1.Text = "Circuit :";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.labelControl6);
            this.groupControl2.Controls.Add(this.labelControl2);
            this.groupControl2.Controls.Add(this.txtDuree);
            this.groupControl2.Controls.Add(this.txtKm);
            this.groupControl2.Controls.Add(this.lkpPointFin);
            this.groupControl2.Controls.Add(this.labelControl5);
            this.groupControl2.Controls.Add(this.lkpPointDepart);
            this.groupControl2.Controls.Add(this.labelControl4);
            this.groupControl2.Controls.Add(this.labelControl3);
            this.groupControl2.Controls.Add(this.txtLibelle);
            this.groupControl2.Controls.Add(this.ChkGPS);
            this.groupControl2.Controls.Add(this.txtCCircuit);
            this.groupControl2.Controls.Add(this.labelControl1);
            this.groupControl2.Location = new System.Drawing.Point(12, 12);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(701, 105);
            this.groupControl2.TabIndex = 3;
            this.groupControl2.Text = "Circuit";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(466, 59);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(36, 13);
            this.labelControl6.TabIndex = 112;
            this.labelControl6.Text = "Durée :";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(466, 35);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(21, 13);
            this.labelControl2.TabIndex = 111;
            this.labelControl2.Text = "Km :";
            // 
            // txtDuree
            // 
            this.txtDuree.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtDuree.Location = new System.Drawing.Point(508, 56);
            this.txtDuree.Name = "txtDuree";
            this.txtDuree.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDuree.Properties.Mask.EditMask = "d";
            this.txtDuree.Properties.MaxValue = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.txtDuree.Size = new System.Drawing.Size(140, 20);
            this.txtDuree.TabIndex = 110;
            // 
            // txtKm
            // 
            this.txtKm.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtKm.Location = new System.Drawing.Point(508, 30);
            this.txtKm.Name = "txtKm";
            this.txtKm.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtKm.Properties.Mask.EditMask = "n";
            this.txtKm.Properties.MaxValue = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.txtKm.Size = new System.Drawing.Size(140, 20);
            this.txtKm.TabIndex = 109;
            // 
            // lkpPointFin
            // 
            this.lkpPointFin.EnterMoveNextControl = true;
            this.lkpPointFin.Location = new System.Drawing.Point(320, 56);
            this.lkpPointFin.Name = "lkpPointFin";
            this.lkpPointFin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpPointFin.Properties.NullText = "";
            this.lkpPointFin.Size = new System.Drawing.Size(140, 20);
            this.lkpPointFin.TabIndex = 107;
            this.lkpPointFin.Tag = "RQ";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.labelControl5.Location = new System.Drawing.Point(251, 61);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(63, 13);
            this.labelControl5.TabIndex = 108;
            this.labelControl5.Text = "Point de Fin :";
            // 
            // lkpPointDepart
            // 
            this.lkpPointDepart.EnterMoveNextControl = true;
            this.lkpPointDepart.Location = new System.Drawing.Point(105, 56);
            this.lkpPointDepart.Name = "lkpPointDepart";
            this.lkpPointDepart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpPointDepart.Properties.NullText = "";
            this.lkpPointDepart.Size = new System.Drawing.Size(140, 20);
            this.lkpPointDepart.TabIndex = 105;
            this.lkpPointDepart.Tag = "RQ";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.labelControl4.Location = new System.Drawing.Point(17, 59);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(82, 13);
            this.labelControl4.TabIndex = 106;
            this.labelControl4.Text = "Point de Depart :";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(251, 33);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(36, 13);
            this.labelControl3.TabIndex = 29;
            this.labelControl3.Text = "Libellé :";
            // 
            // txtLibelle
            // 
            this.txtLibelle.Location = new System.Drawing.Point(320, 30);
            this.txtLibelle.Name = "txtLibelle";
            this.txtLibelle.Size = new System.Drawing.Size(140, 20);
            this.txtLibelle.TabIndex = 1;
            this.txtLibelle.Tag = "RQ";
            // 
            // ChkGPS
            // 
            this.ChkGPS.EditValue = true;
            this.ChkGPS.Location = new System.Drawing.Point(643, 80);
            this.ChkGPS.Name = "ChkGPS";
            this.ChkGPS.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.ChkGPS.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.ChkGPS.Properties.Appearance.Options.UseFont = true;
            this.ChkGPS.Properties.Appearance.Options.UseForeColor = true;
            this.ChkGPS.Properties.AutoWidth = true;
            this.ChkGPS.Properties.Caption = "GPS";
            this.ChkGPS.Size = new System.Drawing.Size(45, 20);
            this.ChkGPS.TabIndex = 104;
            this.ChkGPS.CheckedChanged += new System.EventHandler(this.ChkGPS_CheckedChanged);
            // 
            // panelControl2
            // 
            this.panelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl2.Controls.Add(this.gmap);
            this.panelControl2.Location = new System.Drawing.Point(719, 12);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(482, 586);
            this.panelControl2.TabIndex = 103;
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
            this.gmap.Location = new System.Drawing.Point(7, 4);
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
            this.gmap.Size = new System.Drawing.Size(470, 577);
            this.gmap.TabIndex = 100;
            this.gmap.Zoom = 0D;
            this.gmap.OnMarkerEnter += new GMap.NET.WindowsForms.MarkerEnter(this.gmap_OnMarkerEnter);
            // 
            // FrmCircuit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(1213, 608);
            this.ClientSize = new System.Drawing.Size(1213, 608);
            this.Controls.Add(this.groupControlDetail);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.groupControl2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmCircuit";
            this.Text = "FrmCircuit";
            this.Load += new System.EventHandler(this.FrmCircuit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlDetail)).EndInit();
            this.groupControlDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkallcheck.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCCircuit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDuree.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpPointFin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpPointDepart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLibelle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChkGPS.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControlDetail;
        private DevExpress.XtraGrid.GridControl gridC;
        private DevExpress.XtraGrid.Views.Grid.GridView gridV;
        private DevExpress.XtraEditors.TextEdit txtCCircuit;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtLibelle;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProvider1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private GMap.NET.WindowsForms.GMapControl gmap;
        private DevExpress.XtraEditors.CheckEdit chkallcheck;
        private DevExpress.XtraEditors.CheckEdit ChkGPS;
        private DevExpress.XtraEditors.LookUpEdit lkpPointFin;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LookUpEdit lkpPointDepart;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SpinEdit txtDuree;
        private DevExpress.XtraEditors.SpinEdit txtKm;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}