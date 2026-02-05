namespace CST.LePoint.Intervention.DroitsAcces
{
    partial class FrmRole
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
            this.gridV = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grid = new CST.LePoint.CtrlLibrary.DevExpressEx.GridControlEx();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.txtNomRole = new DevExpress.XtraEditors.TextEdit();
            this.txtIdRole = new DevExpress.XtraEditors.TextEdit();
            this.txtDescRole = new DevExpress.XtraEditors.MemoEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblIdRole = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblDescRole = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblNomRole = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblListeAutorisation = new DevExpress.XtraEditors.LabelControl();
            this.chbtnSelectAll = new DevExpress.XtraEditors.CheckButton();
            this.CB_Societe = new System.Windows.Forms.ComboBox();
            this.LB_Societe = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.gridV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNomRole.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIdRole.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescRole.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblIdRole)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDescRole)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNomRole)).BeginInit();
            this.SuspendLayout();
            // 
            // gridV
            // 
            this.gridV.GridControl = this.grid;
            this.gridV.Name = "gridV";
            this.gridV.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridV.OptionsView.ShowGroupPanel = false;
            this.gridV.OptionsView.ShowIndicator = false;
            this.gridV.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gridV_CustomRowCellEdit);
            this.gridV.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gridV_ShowingEditor);
            this.gridV.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(this.gridV_CustomUnboundColumnData);
            this.gridV.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.gridV_CustomColumnDisplayText);
            // 
            // grid
            // 
            this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grid.Location = new System.Drawing.Point(12, 225);
            this.grid.MainView = this.gridV;
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(798, 323);
            this.grid.TabIndex = 6;
            this.grid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridV});
            this.grid.DoubleClick += new System.EventHandler(this.grid_DoubleClick);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.txtNomRole);
            this.layoutControl1.Controls.Add(this.txtIdRole);
            this.layoutControl1.Controls.Add(this.txtDescRole);
            this.layoutControl1.Location = new System.Drawing.Point(12, 12);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(326, 137);
            this.layoutControl1.TabIndex = 5;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // txtNomRole
            // 
            this.txtNomRole.Location = new System.Drawing.Point(69, 36);
            this.txtNomRole.Name = "txtNomRole";
            this.txtNomRole.Size = new System.Drawing.Size(245, 20);
            this.txtNomRole.StyleController = this.layoutControl1;
            this.txtNomRole.TabIndex = 7;
            // 
            // txtIdRole
            // 
            this.txtIdRole.Location = new System.Drawing.Point(69, 12);
            this.txtIdRole.Name = "txtIdRole";
            this.txtIdRole.Size = new System.Drawing.Size(245, 20);
            this.txtIdRole.StyleController = this.layoutControl1;
            this.txtIdRole.TabIndex = 4;
            // 
            // txtDescRole
            // 
            this.txtDescRole.Location = new System.Drawing.Point(69, 60);
            this.txtDescRole.Name = "txtDescRole";
            this.txtDescRole.Size = new System.Drawing.Size(245, 65);
            this.txtDescRole.StyleController = this.layoutControl1;
            this.txtDescRole.TabIndex = 5;
            this.txtDescRole.UseOptimizedRendering = true;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblIdRole,
            this.lblDescRole,
            this.lblNomRole});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(326, 137);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lblIdRole
            // 
            this.lblIdRole.Control = this.txtIdRole;
            this.lblIdRole.CustomizationFormText = "lblIdRole";
            this.lblIdRole.Location = new System.Drawing.Point(0, 0);
            this.lblIdRole.Name = "lblIdRole";
            this.lblIdRole.Size = new System.Drawing.Size(306, 24);
            this.lblIdRole.Text = "lblIdRole";
            this.lblIdRole.TextSize = new System.Drawing.Size(54, 13);
            // 
            // lblDescRole
            // 
            this.lblDescRole.Control = this.txtDescRole;
            this.lblDescRole.CustomizationFormText = "lblDescRole";
            this.lblDescRole.Location = new System.Drawing.Point(0, 48);
            this.lblDescRole.Name = "lblPassword";
            this.lblDescRole.Size = new System.Drawing.Size(306, 69);
            this.lblDescRole.Text = "lblDescRole";
            this.lblDescRole.TextSize = new System.Drawing.Size(54, 13);
            // 
            // lblNomRole
            // 
            this.lblNomRole.Control = this.txtNomRole;
            this.lblNomRole.CustomizationFormText = "lblNomRole";
            this.lblNomRole.Location = new System.Drawing.Point(0, 24);
            this.lblNomRole.Name = "lblUserName";
            this.lblNomRole.Size = new System.Drawing.Size(306, 24);
            this.lblNomRole.Text = "lblNomRole";
            this.lblNomRole.TextSize = new System.Drawing.Size(54, 13);
            // 
            // lblListeAutorisation
            // 
            this.lblListeAutorisation.Location = new System.Drawing.Point(12, 206);
            this.lblListeAutorisation.Name = "lblListeAutorisation";
            this.lblListeAutorisation.Size = new System.Drawing.Size(90, 13);
            this.lblListeAutorisation.TabIndex = 9;
            this.lblListeAutorisation.Text = "lblListeAutorisation";
            // 
            // chbtnSelectAll
            // 
            this.chbtnSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chbtnSelectAll.Location = new System.Drawing.Point(715, 196);
            this.chbtnSelectAll.Name = "chbtnSelectAll";
            this.chbtnSelectAll.Size = new System.Drawing.Size(95, 23);
            this.chbtnSelectAll.TabIndex = 10;
            this.chbtnSelectAll.Text = "Sélectionner Tout";
            this.chbtnSelectAll.CheckedChanged += new System.EventHandler(this.chbtnSelectAll_CheckedChanged);
            // 
            // CB_Societe
            // 
            this.CB_Societe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_Societe.FormattingEnabled = true;
            this.CB_Societe.Items.AddRange(new object[] {
            " "});
            this.CB_Societe.Location = new System.Drawing.Point(81, 155);
            this.CB_Societe.Name = "CB_Societe";
            this.CB_Societe.Size = new System.Drawing.Size(245, 21);
            this.CB_Societe.TabIndex = 159;
            // 
            // LB_Societe
            // 
            this.LB_Societe.Location = new System.Drawing.Point(33, 158);
            this.LB_Societe.Name = "LB_Societe";
            this.LB_Societe.Size = new System.Drawing.Size(42, 13);
            this.LB_Societe.TabIndex = 158;
            this.LB_Societe.Text = "Société :";
            // 
            // FrmRole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.ClientSize = new System.Drawing.Size(822, 577);
            this.ControlBox = false;
            this.Controls.Add(this.CB_Societe);
            this.Controls.Add(this.LB_Societe);
            this.Controls.Add(this.chbtnSelectAll);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.lblListeAutorisation);
            this.Name = "FrmRole";
            this.Text = "frmRole";
            ((System.ComponentModel.ISupportInitialize)(this.gridV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtNomRole.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIdRole.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescRole.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblIdRole)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDescRole)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNomRole)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.Views.Grid.GridView gridV;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.TextEdit txtNomRole;
        private DevExpress.XtraEditors.TextEdit txtIdRole;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem lblIdRole;
        private DevExpress.XtraLayout.LayoutControlItem lblDescRole;
        private DevExpress.XtraLayout.LayoutControlItem lblNomRole;
        private DevExpress.XtraEditors.LabelControl lblListeAutorisation;
        private DevExpress.XtraEditors.MemoEdit txtDescRole;
        private DevExpress.XtraEditors.CheckButton chbtnSelectAll;
        private CtrlLibrary.DevExpressEx.GridControlEx grid;
        private System.Windows.Forms.ComboBox CB_Societe;
        private DevExpress.XtraEditors.LabelControl LB_Societe;
    }
}
