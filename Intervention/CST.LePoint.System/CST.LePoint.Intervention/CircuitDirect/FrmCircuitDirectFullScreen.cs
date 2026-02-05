using CST.LePoint.CtrlLibrary;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CST.LePoint.Intervention.CircuitDirect
{
    public partial class FrmCircuitDirectFullScreen : DevExpress.XtraEditors.XtraForm
    {
        private FrmAjoutCircuitDirect fac = new FrmAjoutCircuitDirect();

        public GridView getgridv()
        {
            return this.gridView1;
        }

        public GridControl getgridc()
        {
            return this.gridControl1;
        }

        public FrmCircuitDirectFullScreen()
        {
            InitializeComponent();
        }

        private void FrmCircuitDirectFullScreen_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Bounds = Screen.PrimaryScreen.Bounds;
            panel.Size = this.Size;
            panel.Dock = DockStyle.Fill;
            panel.Location = this.Location;
        }

        public FrmCircuitDirectFullScreen(GridView gridv, GridControl gridC)
        {
            InitializeComponent();
            CtrlHelper.InitGridView(gridView1, Titres());
            this.gridView1 = gridv;
            BindingSource bs = new BindingSource();
            bs.DataSource = gridC.DataSource;
            gridControl1.MainView.Assign(gridC.MainView, true);
            gridControl1.DataSource = bs;
        }

        private static GvColumnProprietes Titres()
        {
            GvColumnProprietes proprietes = new GvColumnProprietes();
            proprietes.Add(new GvColumnPropriete("[X]", GvColumnPropriete.GvColumnType.Boolean, GvColumnPropriete.GvColumnEtat.Enable));
            //proprietes.Add(new GvColumnPropriete("N° Convention"));
            proprietes.Add(new GvColumnPropriete("C. Client"));
            proprietes.Add(new GvColumnPropriete("Raison Sociale"));
            proprietes.Add(new GvColumnPropriete("Chiffre d'affaire n-1"));
            proprietes.Add(new GvColumnPropriete("Chiffre d'affaire n"));
            proprietes.Add(new GvColumnPropriete("Famille client"));
            //proprietes.Add(new GvColumnPropriete("C. Établissement", GvColumnPropriete.GvColumnEtat.Invisible));
            //proprietes.Add(new GvColumnPropriete("Établissement"));
            proprietes.Add(new GvColumnPropriete("Ordre", GvColumnPropriete.GvColumnType.Integer, GvColumnPropriete.GvColumnEtat.Enable));
            //proprietes.Add(new GvColumnPropriete("Nb. Passage", GvColumnPropriete.GvColumnType.Integer, GvColumnPropriete.GvColumnEtat.Invisible));
            proprietes.Add(new GvColumnPropriete("Region"));
            proprietes.Add(new GvColumnPropriete("Gouvernorat"));
            proprietes.Add(new GvColumnPropriete("Date dern. visite"));
            proprietes.Add(new GvColumnPropriete("GPS", GvColumnPropriete.GvColumnType.Boolean));

            //proprietes.Add(new GvColumnPropriete("TTraj"));
            //proprietes.Add(new GvColumnPropriete("DTraj", GvColumnPropriete.GvColumnType.Decimal));
            return proprietes;
        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Escape) || ((e.KeyCode == Keys.Alt) && (e.KeyCode == Keys.F4)))

                this.Close();
        }
    }
}
