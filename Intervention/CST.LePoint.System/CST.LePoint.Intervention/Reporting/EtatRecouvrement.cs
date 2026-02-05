using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using CST.LePoint.Intervention.Metier;
using CST.LePoint.CtrlLibrary.DevExpressEx;

namespace CST.LePoint.Intervention.Reporting
{
    public partial class EtatRecouvrement : DevExpress.XtraReports.UI.XtraReport
    {
        public EtatRecouvrement()
        {
            InitializeComponent();
        }

        public void DoReport(string CClient, string RaisonSociale)
        {
            RecouvrementCollection rec = RecouvrementCollection.Charger(CClient);
            RecouvrementClient recClient = RecouvrementClientCollection.Charger(CClient);
            this.txtCClient.Text = CClient;
            this.txtRaisonSociale.Text = RaisonSociale;
            this.txtAvance.Text = recClient.Avance.ToString("0.000");
            this.txtAvoir.Text = recClient.Avoir.ToString("0.000");
            if (rec.Count > 0)
            {
                this.NoDataFound.Visible = false;
                decimal totalMRecu = 0;
                decimal totalSolde = 0;
                decimal FtotalMRecu = 0;
                decimal FtotalSolde = 0;
                decimal BtotalMRecu = 0;
                decimal BtotalSolde = 0;
                decimal StotalMRecu = 0;
                decimal StotalSolde = 0;
                RecouvrementCollection Frec = new RecouvrementCollection();
                RecouvrementCollection Brec = new RecouvrementCollection();
                RecouvrementCollection Srec = new RecouvrementCollection();
                rec.ForEach(r =>
                {
                    if (r.TypeDocument == "F")
                    {
                        Frec.Add(r);
                        FtotalMRecu += r.MontantRecu;
                        FtotalSolde += r.Credit;
                    }
                    else if (r.TypeDocument == "BLNF")
                    {
                        Brec.Add(r);
                        BtotalMRecu += r.MontantRecu;
                        BtotalSolde += r.Credit;
                    }
                    else
                    {
                        Srec.Add(r);
                        StotalMRecu += r.MontantRecu;
                        StotalSolde += r.Credit;
                    }
                    totalMRecu += r.MontantRecu;
                    totalSolde += r.Credit;
                });
                if (Frec.Count > 0)
                {
                    this.Tab.Rows.Add(GetTitle("Facture"));
                    Frec.ForEach(r =>
                    {
                        int i = Frec.IndexOf(r);
                        XRTableRow row = new XRTableRow();
                        row.HeightF = 38;
                        row.WidthF = 747;
                        row.BackColor = i % 2 == 0 ? Color.Lavender : Color.White;
                        row.Cells.Add(GenCell(r.NDocument, DevExpress.XtraPrinting.TextAlignment.MiddleLeft, false, 124.5F));
                        row.Cells.Add(GenCell(r.DateDocument, DevExpress.XtraPrinting.TextAlignment.MiddleLeft, false, 124.5F));
                        row.Cells.Add(GenCell(r.JourCredit.ToString(), DevExpress.XtraPrinting.TextAlignment.MiddleCenter, false, 124.5F));
                        row.Cells.Add(GenCell(r.MontantTTC.ToString().ToString(), DevExpress.XtraPrinting.TextAlignment.MiddleRight, false, 124.5F));
                        row.Cells.Add(GenCell(r.MontantRecu.ToString(), DevExpress.XtraPrinting.TextAlignment.MiddleRight, false, 124.5F));
                        row.Cells.Add(GenCell(r.Credit.ToString(), DevExpress.XtraPrinting.TextAlignment.MiddleRight, false, 124.5F));
                        this.Tab.Rows.Add(row);
                    });
                    this.Tab.Rows.Add(GetTotal("", FtotalMRecu.ToString(), FtotalSolde.ToString()));
                }
                if (Brec.Count > 0)
                {
                    this.Tab.Rows.Add(GetTitle("Bon de livraison non facturé"));
                    Brec.ForEach(r =>
                    {
                        int i = Brec.IndexOf(r);
                        XRTableRow row = new XRTableRow();
                        row.HeightF = 38;
                        row.WidthF = 747;
                        row.BackColor = i % 2 == 0 ? Color.Lavender : Color.White;
                        row.Cells.Add(GenCell(r.NDocument, DevExpress.XtraPrinting.TextAlignment.MiddleLeft, false, 124.5F));
                        row.Cells.Add(GenCell(r.DateDocument, DevExpress.XtraPrinting.TextAlignment.MiddleLeft, false, 124.5F));
                        row.Cells.Add(GenCell(r.JourCredit.ToString(), DevExpress.XtraPrinting.TextAlignment.MiddleCenter, false, 124.5F));
                        row.Cells.Add(GenCell(r.MontantTTC.ToString().ToString(), DevExpress.XtraPrinting.TextAlignment.MiddleRight, false, 124.5F));
                        row.Cells.Add(GenCell(r.MontantRecu.ToString(), DevExpress.XtraPrinting.TextAlignment.MiddleRight, false, 124.5F));
                        row.Cells.Add(GenCell(r.Credit.ToString(), DevExpress.XtraPrinting.TextAlignment.MiddleRight, false, 124.5F));
                        this.Tab.Rows.Add(row);

                    });
                    this.Tab.Rows.Add(GetTotal("", BtotalMRecu.ToString(), BtotalSolde.ToString()));
                }
                if (Srec.Count > 0)
                {
                    this.Tab.Rows.Add(GetTitle("Spécifique"));
                    Srec.ForEach(r =>
                    {
                        int i = Srec.IndexOf(r);
                        XRTableRow row = new XRTableRow();
                        row.HeightF = 38;
                        row.WidthF = 747;
                        row.BackColor = i % 2 == 0 ? Color.Lavender : Color.White;
                        row.Cells.Add(GenCell(r.NDocument, DevExpress.XtraPrinting.TextAlignment.MiddleLeft, false, 124.5F));
                        row.Cells.Add(GenCell(r.DateDocument, DevExpress.XtraPrinting.TextAlignment.MiddleLeft, false, 124.5F));
                        row.Cells.Add(GenCell(r.JourCredit.ToString(), DevExpress.XtraPrinting.TextAlignment.MiddleCenter, false, 124.5F));
                        row.Cells.Add(GenCell(r.MontantTTC.ToString().ToString(), DevExpress.XtraPrinting.TextAlignment.MiddleRight, false, 124.5F));
                        row.Cells.Add(GenCell(r.MontantRecu.ToString(), DevExpress.XtraPrinting.TextAlignment.MiddleRight, false, 124.5F));
                        row.Cells.Add(GenCell(r.Credit.ToString(), DevExpress.XtraPrinting.TextAlignment.MiddleRight, false, 124.5F));
                        this.Tab.Rows.Add(row);

                    });
                    this.Tab.Rows.Add(GetTotal("", StotalMRecu.ToString(), StotalSolde.ToString()));
                }
                this.Tab.Rows.Add( GetTotal("Total", totalMRecu.ToString(), totalSolde.ToString()));
                this.txtSolde.Text = ((FtotalSolde + BtotalSolde) - (recClient.Avance + recClient.Avoir)).ToString();
                this.txtSoldeSpecifique.Text = StotalSolde.ToString("0.000");
            }

            FrmXRViewer report = new FrmXRViewer();
            report.Text = "État de recouvrement";
            report.DoReport(this);
        }

        private XRTableRow GetTitle(string title)
        {
            XRTableRow row = new XRTableRow();
            row.HeightF = 38;
            row.WidthF = 747;
            row.BackColor = Color.AntiqueWhite;
            XRTableCell cell = new XRTableCell();
            cell.HeightF = 38;
            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            cell.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 10, 10, 10, 100F);
            cell.Text = title;
            cell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            row.Cells.Add(cell);
            return row;
        }

        private XRTableCell GenCell(string text, DevExpress.XtraPrinting.TextAlignment TextAlignement, bool usebold, float width)
        {
            XRTableCell cell = new XRTableCell();
            cell.TextAlignment = TextAlignement;
            cell.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 10, 10, 10, 100F);
            cell.Text = text;
            cell.WidthF = width;
            cell.HeightF = 38;
            if (usebold)
                cell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            return cell;
        }

        private XRTableRow GetTotal(string title, string MRecu, string Solde)
        {
            XRTableRow row = new XRTableRow();
            row.HeightF = 38;
            row.WidthF = 747;
            row.BorderColor = System.Drawing.Color.LightGray;
            row.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            row.BackColor = Color.White;
            row.Cells.Add(GenCell(title, DevExpress.XtraPrinting.TextAlignment.MiddleLeft, true, 498));
            row.Cells.Add(GenCell(MRecu, DevExpress.XtraPrinting.TextAlignment.MiddleRight, true, 124.5F));
            row.Cells.Add(GenCell(Solde, DevExpress.XtraPrinting.TextAlignment.MiddleRight, true, 124.5F));
            return row;
        }

    }
}
