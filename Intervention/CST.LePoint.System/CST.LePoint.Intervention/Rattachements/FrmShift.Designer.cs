

namespace CST.LePoint.Intervention.Rattachements
{
    partial class FrmShift
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
            this.GridCPoste = new DevExpress.XtraGrid.GridControl();
            this.GridVPoste = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Détaille = new DevExpress.XtraEditors.GroupControl();
            ((System.ComponentModel.ISupportInitialize)(this.GridCPoste)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridVPoste)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Détaille)).BeginInit();
            this.Détaille.SuspendLayout();
            this.SuspendLayout();
            // 
            // GridCPoste
            // 
            this.GridCPoste.Cursor = System.Windows.Forms.Cursors.Default;
            this.GridCPoste.Location = new System.Drawing.Point(2, 21);
            this.GridCPoste.MainView = this.GridVPoste;
            this.GridCPoste.Name = "GridCPoste";
            this.GridCPoste.Size = new System.Drawing.Size(829, 383);
            this.GridCPoste.TabIndex = 2;
            this.GridCPoste.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GridVPoste});
            // 
            // GridVPoste
            // 
            this.GridVPoste.GridControl = this.GridCPoste;
            this.GridVPoste.Name = "GridVPoste";
            this.GridVPoste.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.GridVPoste_FocusedRowChanged);
            this.GridVPoste.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.GridVPoste_ValidateRow);
            this.GridVPoste.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GridVPoste_KeyDown);
            // 
            // Détaille
            // 
            this.Détaille.Controls.Add(this.GridCPoste);
            this.Détaille.Location = new System.Drawing.Point(1, 1);
            this.Détaille.Name = "Détaille";
            this.Détaille.Size = new System.Drawing.Size(833, 406);
            this.Détaille.TabIndex = 3;
            this.Détaille.Text = "Détaille Poste";
            // 
            // FrmShift
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(835, 409);
            this.Controls.Add(this.Détaille);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmShift";
            this.Text = "FrmPosteDeTravaille";
            this.Load += new System.EventHandler(this.FrmPosteDeTravaille_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GridCPoste)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridVPoste)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Détaille)).EndInit();
            this.Détaille.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl GridCPoste;
        private DevExpress.XtraGrid.Views.Grid.GridView GridVPoste;
        private DevExpress.XtraEditors.GroupControl Détaille;
    }
}