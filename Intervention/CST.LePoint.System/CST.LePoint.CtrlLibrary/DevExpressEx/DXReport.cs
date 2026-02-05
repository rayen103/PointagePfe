using CST.LePoint.CtrlLibrary.Properties;
using CST.LePoint.Securite;
using CST.LePoint.Tools;
using DevExpress.XtraGrid;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Control;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace CST.LePoint.CtrlLibrary.DevExpressEx
{
    public class DXReport
    {

        public static void Apercu(GridControl grid, string titrePage)
        {
            PrintReportDevExpress(grid, titrePage);
        }

        public static void Apercu(PivotGridControl pivotgrid, string titrePage, Margins marges, bool scaleFactor = false, bool landscape = false, PaperKind paperkind = PaperKind.A4)
        {
            PrintReportDevExpress(pivotgrid, titrePage, marges, landscape, scaleFactor, paperkind);
        }

        public static void Apercu(GridControl grid, string titrePage, bool Landscape, Margins marges, bool scaleFactor = false, PaperKind paperkind = PaperKind.Custom)
        {
            PrintReportDevExpress(grid, titrePage, marges, Landscape, scaleFactor, paperkind);
        }

        public static void Imprimer(GridControl grid, string titrePage)
        {
            PrintReportDevExpress(grid, titrePage, true);
        }

        public static void Exporter(GridControl gridV, string FormatCible, string NomFichier, string titrePage)
        {
            string path = SelectionnerFichier(string.Format(FormatCible + " Files(*.{0})|*.{0}", FormatCible.ToLower()), NomFichier + "." + FormatCible.ToLower());
            if (path != string.Empty)
            {
                PrintReportDevExpress(gridV, titrePage, false, path);
            }
        }

        public static String SelectionnerFichier(String filterType, String NomFichier)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter = filterType, FileName = NomFichier, Title = "Exporter sous" };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                return saveFileDialog.FileName;
            else
                return String.Empty;
        }

        private static void PrintReportDevExpress(GridControl grid, string titrePage, bool imprimer = false, string exporter = null)
        {
            DevExpress.XtraPrinting.PrintingSystem printingSystem = new DevExpress.XtraPrinting.PrintingSystem();

            PrintableComponentLink printLink = new PrintableComponentLink();

            printingSystem.Begin();

            printLink.Component = grid;

            //if (((GridView)(grid.FocusedView)).Columns.Count > 5)
            //    printLink.PaperKind = PaperKind.LetterRotated;

            PageHeaderFooter phf = printLink.PageHeaderFooter as PageHeaderFooter;
            var header = phf.Header;
            var footer = phf.Footer;
            header.Content.Clear();
            footer.Content.Clear();

            footer.Content.AddRange(new[] { DateTime.Now.ToString(), "", "Page [# de #]" });
            header.Content.AddRange(new[] { "", titrePage, "" });
            header.LineAlignment = BrickAlignment.Center;
            header.Font = new Font("Tahoma", 12, FontStyle.Bold);

            footer.Font = new Font("Tahoma", 8, FontStyle.Italic);
            
            printLink.CreateDocument(printingSystem);
            printLink.PrintingSystem.Document.AutoFitToPagesWidth = 1;
            printingSystem.End();

            //***************************limiter l'export file and mail au format Pdf******************************//
            printingSystem.SetCommandVisibility(PrintingSystemCommand.ExportHtm, CommandVisibility.None);
            printingSystem.SetCommandVisibility(PrintingSystemCommand.ExportMht, CommandVisibility.None);
            printingSystem.SetCommandVisibility(PrintingSystemCommand.ExportRtf, CommandVisibility.None);
            printingSystem.SetCommandVisibility(PrintingSystemCommand.ExportXls, CommandVisibility.None);
            printingSystem.SetCommandVisibility(PrintingSystemCommand.ExportXlsx, CommandVisibility.None);
            printingSystem.SetCommandVisibility(PrintingSystemCommand.ExportXps, CommandVisibility.None);
            printingSystem.SetCommandVisibility(PrintingSystemCommand.ExportTxt, CommandVisibility.None);
            printingSystem.SetCommandVisibility(PrintingSystemCommand.ExportCsv, CommandVisibility.None);
            printingSystem.SetCommandVisibility(PrintingSystemCommand.ExportGraphic, CommandVisibility.None);
            printingSystem.SetCommandVisibility(PrintingSystemCommand.SendCsv, CommandVisibility.None);
            printingSystem.SetCommandVisibility(PrintingSystemCommand.SendGraphic, CommandVisibility.None);
            printingSystem.SetCommandVisibility(PrintingSystemCommand.SendMht, CommandVisibility.None);
            printingSystem.SetCommandVisibility(PrintingSystemCommand.SendRtf, CommandVisibility.None);
            printingSystem.SetCommandVisibility(PrintingSystemCommand.SendTxt, CommandVisibility.None);
            printingSystem.SetCommandVisibility(PrintingSystemCommand.SendXls, CommandVisibility.None);
            printingSystem.SetCommandVisibility(PrintingSystemCommand.SendXlsx, CommandVisibility.None);
            printingSystem.SetCommandVisibility(PrintingSystemCommand.SendXps, CommandVisibility.None);
            //***************************limiter l'export file and mail au format Pdf******************************//

            if (exporter != null)
            {
                if (exporter.ToUpper().EndsWith("PDF"))
                    printingSystem.ExportToPdf(exporter);
                else if (exporter.ToUpper().EndsWith("XLS"))
                    printingSystem.ExportToPdf(exporter);
                else if (exporter.ToUpper().EndsWith("XLSX"))
                    printingSystem.ExportToPdf(exporter);
                else if (exporter.ToUpper().EndsWith("TXT"))
                    printingSystem.ExportToPdf(exporter);
            }
            else if (grid.IsPrintingAvailable)
            {
                if (imprimer)
                    printLink.PrintDlg();
                else
                {
                    printLink.ShowPreview();
                }
            }
        }

        private static void PrintReportDevExpress(PivotGridControl pivotgrid, string titrePage, Margins marges, bool landscape = false, bool scaleFactor = false, PaperKind paperkind = PaperKind.A4, bool imprimer = false, string exporter = null)
        {
            DevExpress.XtraPrinting.PrintingSystem printingSystem = new DevExpress.XtraPrinting.PrintingSystem();
            PrintableComponentLink printLink = new PrintableComponentLink();

            printingSystem.Begin();
            printLink.Component = pivotgrid;

            //if (((GridView)(grid.FocusedView)).Columns.Count > 5)
            //    printLink.PaperKind = PaperKind.LetterRotated;

            PageHeaderFooter phf = printLink.PageHeaderFooter as PageHeaderFooter;
            var header = phf.Header;
            var footer = phf.Footer;
            header.Content.Clear();
            footer.Content.Clear();

            string user = string.Empty;
            if (GestionSession.UtilisateurCourant.Nom != null)
                user = GestionSession.UtilisateurCourant.Nom.ToString() + " " + GestionSession.UtilisateurCourant.Prenom.ToString();

            footer.Content.AddRange(new[] { "Edité par : " + user + " Le " + DateTime.Now.ToString(), "",  Application.OpenForms[0].Text.ToUpper() +  " - Page [# de #]" });
            header.Content.AddRange(new[] { GestionSession.SocieteCourante.RaisonSociale.ToString(), titrePage, "" });
            header.LineAlignment = BrickAlignment.Center;
            header.Font = new Font("Tahoma", 16, FontStyle.Bold);
            footer.Font = new Font("Tahoma", 8, FontStyle.Italic);

            printLink.CreateDocument(printingSystem);
            printLink.PrintingSystem.PageSettings.Assign(marges, paperkind, landscape);
            if (scaleFactor)
                printLink.PrintingSystem.Document.ScaleFactor = 1;
            else
                printLink.PrintingSystem.Document.AutoFitToPagesWidth = 1;

            printingSystem.End();

            //***************************limiter l'export file and mail au format Pdf******************************//
            printingSystem.SetCommandVisibility(PrintingSystemCommand.ExportHtm, CommandVisibility.None);
            printingSystem.SetCommandVisibility(PrintingSystemCommand.ExportMht, CommandVisibility.None);
            printingSystem.SetCommandVisibility(PrintingSystemCommand.ExportRtf, CommandVisibility.None);
            //printingSystem.SetCommandVisibility(PrintingSystemCommand.ExportXls, CommandVisibility.None);
            //printingSystem.SetCommandVisibility(PrintingSystemCommand.ExportXlsx, CommandVisibility.None);
            printingSystem.SetCommandVisibility(PrintingSystemCommand.ExportXps, CommandVisibility.None);
            printingSystem.SetCommandVisibility(PrintingSystemCommand.ExportTxt, CommandVisibility.None);
            printingSystem.SetCommandVisibility(PrintingSystemCommand.ExportCsv, CommandVisibility.None);
            printingSystem.SetCommandVisibility(PrintingSystemCommand.ExportGraphic, CommandVisibility.None);
            printingSystem.SetCommandVisibility(PrintingSystemCommand.SendCsv, CommandVisibility.None);
            printingSystem.SetCommandVisibility(PrintingSystemCommand.SendGraphic, CommandVisibility.None);
            printingSystem.SetCommandVisibility(PrintingSystemCommand.SendMht, CommandVisibility.None);
            printingSystem.SetCommandVisibility(PrintingSystemCommand.SendRtf, CommandVisibility.None);
            printingSystem.SetCommandVisibility(PrintingSystemCommand.SendTxt, CommandVisibility.None);
            printingSystem.SetCommandVisibility(PrintingSystemCommand.SendXls, CommandVisibility.None);
            printingSystem.SetCommandVisibility(PrintingSystemCommand.SendXlsx, CommandVisibility.None);
            printingSystem.SetCommandVisibility(PrintingSystemCommand.SendXps, CommandVisibility.None);
            //***************************limiter l'export file and mail au format Pdf******************************//

            if (exporter != null)
            {
                if (exporter.ToUpper().EndsWith("PDF"))
                    printingSystem.ExportToPdf(exporter);
                else if (exporter.ToUpper().EndsWith("XLS"))
                    printingSystem.ExportToPdf(exporter);
                else if (exporter.ToUpper().EndsWith("XLSX"))
                    printingSystem.ExportToPdf(exporter);
                else if (exporter.ToUpper().EndsWith("TXT"))
                    printingSystem.ExportToPdf(exporter);
            }
            else if (pivotgrid.IsPrintingAvailable)
            {
                if (imprimer)
                    printLink.PrintDlg();
                else
                {
                    printLink.ShowPreview();
                }
            }
        }

        private static void PrintReportDevExpress(GridControl grid, string titrePage, Margins marges, bool landscape = false, bool scaleFactor = false, PaperKind paperkind = PaperKind.A4, bool imprimer = false, string exporter = null)
        {
            DevExpress.XtraPrinting.PrintingSystem printingSystem = new DevExpress.XtraPrinting.PrintingSystem();
            PrintableComponentLink printLink = new PrintableComponentLink();

            printingSystem.Begin();
            printLink.Component = grid;

            PageHeaderFooter phf = printLink.PageHeaderFooter as PageHeaderFooter;
            var header = phf.Header;
            var footer = phf.Footer;
            header.Content.Clear();
            footer.Content.Clear();
            string user = string.Empty;
            if (GestionSession.UtilisateurCourant.Nom != null)
                user = GestionSession.UtilisateurCourant.Nom.ToString() + " " + GestionSession.UtilisateurCourant.Prenom.ToString();

            footer.Content.AddRange(new[] { "Edité par : " + user + " Le " + DateTime.Now.ToString(), "", Application.OpenForms[0].Text.ToUpper() + " - Page [# de #]" });
            header.Content.AddRange(new[] { GestionSession.SocieteCourante.RaisonSociale.ToString(), titrePage, "" });
            header.LineAlignment = BrickAlignment.Center;
            header.Font = new Font("Tahoma", 12, FontStyle.Bold);
            footer.Font = new Font("Tahoma", 8, FontStyle.Italic);

            printLink.CreateDocument(printingSystem);
            printLink.PrintingSystem.PageSettings.Assign(marges, paperkind, landscape);
            if (scaleFactor)
                printLink.PrintingSystem.Document.ScaleFactor = 1;
            else
                printLink.PrintingSystem.Document.AutoFitToPagesWidth = 1;

            printingSystem.End();

            //***************************limiter l'export file and mail au format Pdf******************************//
            printingSystem.SetCommandVisibility(PrintingSystemCommand.ExportHtm, CommandVisibility.None);
            printingSystem.SetCommandVisibility(PrintingSystemCommand.ExportMht, CommandVisibility.None);
            printingSystem.SetCommandVisibility(PrintingSystemCommand.ExportRtf, CommandVisibility.None);
            printingSystem.SetCommandVisibility(PrintingSystemCommand.ExportXls, CommandVisibility.None);
            printingSystem.SetCommandVisibility(PrintingSystemCommand.ExportXlsx, CommandVisibility.None);
            printingSystem.SetCommandVisibility(PrintingSystemCommand.ExportXps, CommandVisibility.None);
            printingSystem.SetCommandVisibility(PrintingSystemCommand.ExportTxt, CommandVisibility.None);
            printingSystem.SetCommandVisibility(PrintingSystemCommand.ExportCsv, CommandVisibility.None);
            printingSystem.SetCommandVisibility(PrintingSystemCommand.ExportGraphic, CommandVisibility.None);
            printingSystem.SetCommandVisibility(PrintingSystemCommand.SendCsv, CommandVisibility.None);
            printingSystem.SetCommandVisibility(PrintingSystemCommand.SendGraphic, CommandVisibility.None);
            printingSystem.SetCommandVisibility(PrintingSystemCommand.SendMht, CommandVisibility.None);
            printingSystem.SetCommandVisibility(PrintingSystemCommand.SendRtf, CommandVisibility.None);
            printingSystem.SetCommandVisibility(PrintingSystemCommand.SendTxt, CommandVisibility.None);
            printingSystem.SetCommandVisibility(PrintingSystemCommand.SendXls, CommandVisibility.None);
            printingSystem.SetCommandVisibility(PrintingSystemCommand.SendXlsx, CommandVisibility.None);
            printingSystem.SetCommandVisibility(PrintingSystemCommand.SendXps, CommandVisibility.None);
            //***************************limiter l'export file and mail au format Pdf******************************//

            if (exporter != null)
            {
                if (exporter.ToUpper().EndsWith("PDF"))
                    printingSystem.ExportToPdf(exporter);
                else if (exporter.ToUpper().EndsWith("XLS"))
                    printingSystem.ExportToPdf(exporter);
                else if (exporter.ToUpper().EndsWith("XLSX"))
                    printingSystem.ExportToPdf(exporter);
                else if (exporter.ToUpper().EndsWith("TXT"))
                    printingSystem.ExportToPdf(exporter);
            }
            else if (grid.IsPrintingAvailable)
            {
                if (imprimer)
                    printLink.PrintDlg();
                else
                {
                    printLink.ShowPreview();
                }
            }
        }
    }
}