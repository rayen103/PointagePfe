using System;
using CrystalDecisions.CrystalReports.Engine;

namespace CST.LePoint.CtrlLibrary.CrystalReport
{
    [Serializable]
    public class CRDocument : ReportDocument
    {
        public CRDocument()
        {
        }

        public bool ViewReport(string text)
        {
            bool bView = false;
            try
            {
                if (this.Database == null)
                    throw new Exception("Aucune source decimal données as été définit");
                if (this.Database.Tables.Count == 0)
                    throw new Exception("Aucune source decimal données as été définit");
                if (string.IsNullOrEmpty(this.FileName))
                    throw new Exception("Le rapport est non définit !");

                FrmCRViewer frm = new FrmCRViewer(text);
                frm.Report = this;
                frm.Show();

                bView = true;
            }
            catch (System.Exception ex)
            {
                bView = false;
                throw ex;
            }

            return bView;
        }
    }
}