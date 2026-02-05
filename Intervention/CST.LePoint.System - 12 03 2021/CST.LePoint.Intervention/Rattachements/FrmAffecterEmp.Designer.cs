namespace CST.LePoint.Intervention.Rattachements
{
    partial class FrmAffecterEmp
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
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition1 = new DevExpress.XtraGrid.StyleFormatCondition();
            this.groupControlDetail = new DevExpress.XtraEditors.GroupControl();
            this.gridC = new DevExpress.XtraGrid.GridControl();
            this.gridV = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnActualiser = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlDetail)).BeginInit();
            this.groupControlDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridV)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControlDetail
            // 
            this.groupControlDetail.AppearanceCaption.ForeColor = System.Drawing.Color.MidnightBlue;
            this.groupControlDetail.AppearanceCaption.Options.UseFont = true;
            this.groupControlDetail.AppearanceCaption.Options.UseForeColor = true;
            this.groupControlDetail.Controls.Add(this.gridC);
            this.groupControlDetail.Location = new System.Drawing.Point(6, 12);
            this.groupControlDetail.Name = "groupControlDetail";
            this.groupControlDetail.Size = new System.Drawing.Size(784, 374);
            this.groupControlDetail.TabIndex = 4;
            this.groupControlDetail.Text = "Liste";
            // 
            // gridC
            // 
            this.gridC.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridC.Location = new System.Drawing.Point(2, 21);
            this.gridC.MainView = this.gridV;
            this.gridC.Name = "gridC";
            this.gridC.Size = new System.Drawing.Size(780, 351);
            this.gridC.TabIndex = 0;
            this.gridC.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridV});
            this.gridC.DoubleClick += new System.EventHandler(this.gridC_DoubleClick);
            // 
            // gridV
            // 
            styleFormatCondition1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            styleFormatCondition1.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            styleFormatCondition1.Appearance.Options.UseBackColor = true;
            styleFormatCondition1.ApplyToRow = true;
            styleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Expression;
            styleFormatCondition1.Expression = "Not IsNullOrEmpty([OPR])";
            this.gridV.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
            styleFormatCondition1});
            this.gridV.GridControl = this.gridC;
            this.gridV.Name = "gridV";
            this.gridV.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridV.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gridV.OptionsSelection.EnableAppearanceHideSelection = false;
            this.gridV.OptionsView.ShowIndicator = false;
            this.gridV.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridV_RowClick);
            //this.gridV.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridV_CellValueChanged);
            // 
            // btnActualiser
            // 
            this.btnActualiser.Location = new System.Drawing.Point(517, 412);
            this.btnActualiser.Name = "btnActualiser";
            this.btnActualiser.Size = new System.Drawing.Size(144, 20);
            this.btnActualiser.TabIndex = 6;
            this.btnActualiser.Text = "Actualiser";
            this.btnActualiser.Click += new System.EventHandler(this.btnActualiser_Click_1);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(116, 412);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(144, 20);
            this.simpleButton1.TabIndex = 7;
            this.simpleButton1.Text = "Sauvegarder";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // FrmAffecterEmp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 462);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.btnActualiser);
            this.Controls.Add(this.groupControlDetail);
            this.KeyPreview = true;
            this.Name = "FrmAffecterEmp";
            this.Text = "FrmAffecterEmp";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmRechercherReg_FormClosed);
            this.Load += new System.EventHandler(this.FrmRechercherReg_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmRechercherReg_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlDetail)).EndInit();
            this.groupControlDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControlDetail;
        private DevExpress.XtraGrid.GridControl gridC;
        private DevExpress.XtraGrid.Views.Grid.GridView gridV;
        private DevExpress.XtraEditors.SimpleButton btnActualiser;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}