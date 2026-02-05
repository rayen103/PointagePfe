using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Printing;

namespace CST.LePoint.Intervention.Metier
{
    public class AFCSPrinter
    {
        /*Page printing entrustment*/
        public delegate void DoPrintDelegate(Graphics g, ref bool HasMorePage);

        PrintDocument iSPriner = null;
        bool m_bUseDefaultPaperSetting = false;

        DoPrintDelegate DoPrint = null;



        public AFCSPrinter()
        {
            iSPriner = new PrintDocument();
            iSPriner.PrintPage += new PrintPageEventHandler
                (this.OnPrintPage);

        }

        public void Dispose()
        {
            if (iSPriner != null) iSPriner.Dispose();
            iSPriner = null;

        }

        /*Set printer name*/
        public string PrinterName
        {
            get { return iSPriner.PrinterSettings.PrinterName; }
            set { iSPriner.PrinterSettings.PrinterName = value; }
        }

        /*Set to print the document name*/
        public string DocumentName
        {
            get { return iSPriner.DocumentName; }
            set { iSPriner.DocumentName = value; }
        }

        /*Whether to use default settings*/
        public bool UseDefaultPaper
        {
            get { return m_bUseDefaultPaperSetting; }
            set
            {
                m_bUseDefaultPaperSetting = value;
                if (!m_bUseDefaultPaperSetting)
                {
                    //If not applicable default is to create a custom paper, note, must use this version of the constructor is the custom paper
                    PaperSize ps = new PaperSize("Custom Size 1", 827, 1169);
                    //The default settings for the new custom paper paper
                    iSPriner.DefaultPageSettings.PaperSize = ps;
                }
            }
        }

        /*The definition of the paper width in millimetres mm*/
        public float PaperWidth
        {
            get { return iSPriner.DefaultPageSettings.PaperSize.Width / 100f * 25.4f; }
            set
            {
                //Note, only a custom paper can modify the property, otherwise it will lead to abnormal
                if (iSPriner.DefaultPageSettings.PaperSize.Kind == PaperKind.Custom)
                    iSPriner.DefaultPageSettings.PaperSize.Width = (int)(value / 25.4 * 100);
            }
        }

        /*The definition of the paper height in millimetres mm*/
        public float PaperHeight
        {
            get { return (int)iSPriner.PrinterSettings.DefaultPageSettings.PaperSize.Height / 100f * 25.4f; }
            set
            {
                //Note, only a custom paper can modify the property, otherwise it will lead to abnormal
                if (iSPriner.DefaultPageSettings.PaperSize.Kind == PaperKind.Custom)
                    iSPriner.DefaultPageSettings.PaperSize.Height = (int)(value / 25.4 * 100);
            }
        }


        /*Page print*/
        private void OnPrintPage(object sender, PrintPageEventArgs ev)
        {

            //Invoking the delegate drawing print content
            if (DoPrint != null)
            {
                bool bHadMore = false;
                DoPrint(ev.Graphics, ref bHadMore);
                ev.HasMorePages = bHadMore;

            }

        }


        /* Start printing*/
        public void Print(DoPrintDelegate doPrint)
        {

            DoPrint = doPrint;
            this.iSPriner.Print();
        }
    }
}