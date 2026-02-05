using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace CST.LePoint.CtrlLibrary.CrystalReport
{
    public partial class FrmCRViewer : DevExpress.XtraEditors.XtraForm
    {
        public DevExpress.XtraEditors.XtraForm Form;
        public int facteurZoom = 118;

        public FrmCRViewer(string text)
        {
            InitializeComponent();
            this.Text = text;
        }

        public FrmCRViewer(string text, int zoom)
        {
            InitializeComponent();
            this.Text = text;
            this.facteurZoom = zoom;
        }

        public FrmCRViewer(string text, DevExpress.XtraEditors.XtraForm form)
        {
            InitializeComponent();
            this.Text = text;
            this.Form = form;
        }

        public ReportDocument Report = new ReportDocument();

        private void FrmCrystalReport_Load(object sender, EventArgs e)
        {
            try
            {
                InitialiserCRViewer();
                crViewer.ReportSource = Report;
                crViewer.Refresh();
                crViewer.Zoom(facteurZoom);
                crViewer.AllowedExportFormats = 1;
             }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void InitialiserCRViewer()
        {
            crViewer.ShowExportButton = true;
            crViewer.ShowCloseButton = true;
            crViewer.ShowGotoPageButton = true;
            crViewer.ShowGroupTreeButton = true;
            crViewer.ShowRefreshButton = true;
            crViewer.ShowTextSearchButton = true;
            crViewer.ShowZoomButton = true;
            crViewer.ShowPageNavigateButtons = true;
        }

        private void FrmCRViewer_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            if (Form != null)
                this.Form.Close();
        }

        private void crViewer_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.PageDown)
                crViewer.ShowNextPage();
            if (e.KeyCode == Keys.PageUp)
                crViewer.ShowPreviousPage();
            if (e.KeyCode == Keys.End)
                crViewer.ShowLastPage();
            if (e.KeyCode == Keys.Home)
                crViewer.ShowFirstPage();
            //if (e.KeyCode == Keys.P)
            //{
            //    //PrintDialog printDialog = new PrintDialog();
            //    //printDialog.PrinterSettings = default(PrinterSettings);
            //    //printDialog.ShowDialog(this);
            //    PrinterSettings printSet = new PrinterSettings();
            //    printSet.PrinterName = "HP LaserJet Professional P1102";
            //    PageSettings pageSet = new PageSettings();
            //    pageSet.PrinterSettings = printSet;
            //    //Report.PrintOptions.PrinterName = PrinterSettings.InstalledPrinters[2];
            //    crViewer.PrintReport();
            //    Report.PrintToPrinter(printSet, pageSet, false);
            //}
        }
    }
}