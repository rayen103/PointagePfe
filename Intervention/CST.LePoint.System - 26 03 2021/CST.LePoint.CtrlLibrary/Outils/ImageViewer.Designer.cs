namespace CST.LePoint.CtrlLibrary.Outils
{
    partial class ImageViewer
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
            this.Image = new DevExpress.XtraEditors.PictureEdit();
            ((System.ComponentModel.ISupportInitialize)(this.Image.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // Image
            // 
            this.Image.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Image.Location = new System.Drawing.Point(0, 0);
            this.Image.Name = "Image";
            this.Image.Properties.AllowFocused = false;
            this.Image.Properties.Appearance.BackColor = System.Drawing.Color.Black;
            this.Image.Properties.Appearance.ForeColor = System.Drawing.Color.White;
            this.Image.Properties.Appearance.Options.UseBackColor = true;
            this.Image.Properties.Appearance.Options.UseForeColor = true;
            this.Image.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.Image.Properties.NullText = "Pas d\'image";
            this.Image.Properties.ReadOnly = true;
            this.Image.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.Image.Size = new System.Drawing.Size(284, 262);
            this.Image.TabIndex = 0;
            // 
            // ImageViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.Image);
            this.Name = "ImageViewer";
            this.Text = "Visionneuse d\'images";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.Image.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PictureEdit Image;
    }
}