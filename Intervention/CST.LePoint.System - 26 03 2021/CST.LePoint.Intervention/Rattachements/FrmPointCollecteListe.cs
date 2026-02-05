
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CST.LePoint.Intervention.Metier;
using CST.LePoint.Securite;
using CST.LePoint.Securite.Entites;
using System.Data.SqlClient;
using System.Configuration;
using CST.LePoint.Achat;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using CST.LePoint.Intervention.Properties;
using System;
using CST.LePoint.CtrlLibrary;
using CST.LePoint.CtrlLibrary.DevExpressEx;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET;
using System.Net;
using GMap.NET.MapProviders;
using CST.LePoint.Intervention.Rattachements;
using CST.LePoint.Intervention;

namespace CST.LePoint.Intervention.Rattachements
{

    public partial class FrmPointCollecteListe : DevExpress.XtraEditors.XtraForm, IActionsEdition, IActionsSuppression, IActionsListe
    {
        private string _CodePC;
        private string nomColonneModifie = string.Empty;
        double lat, lng;

        GMapOverlay routes = new GMapOverlay("routes");
        private GMapOverlay overlayOne = new GMapOverlay("OverlayOne");
        private GMap.NET.WindowsForms.GMapOverlay markers = new GMap.NET.WindowsForms.GMapOverlay("markers");
       

        public FrmPointCollecteListe()
        {
            InitializeComponent();
        }

        public FrmPointCollecteListe(string cPc)
        {
            InitializeComponent();
            this._CodePC = cPc;
        }

        public void Rechercher()
        {
            RemplirGrid();
        }



        private static GvColumnProprietes Titres()
        {
            var proprietes = new GvColumnProprietes();
            proprietes.Add(new GvColumnPropriete("Code P.Collecte"));
            proprietes.Add(new GvColumnPropriete("Libelle Point Collecte"));
            proprietes.Add(new GvColumnPropriete("Gouv"));
            proprietes.Add(new GvColumnPropriete("Region"));
            proprietes.Add(new GvColumnPropriete("lat", GvColumnPropriete.GvColumnType.Decimal, GvColumnPropriete.GvColumnEtat.Invisible));
            proprietes.Add(new GvColumnPropriete("lng", GvColumnPropriete.GvColumnType.Decimal, GvColumnPropriete.GvColumnEtat.Invisible));


            return proprietes;
        }

        private void RemplirGrid()
        {
            var dt = new DataTable();

            PointCollecteCollection pc = new PointCollecteCollection();



            try
            {
                CtrlHelper.InitGridView(gridView1, Titres(), false);

                using (
                    var cn =
                        new SqlConnection(
                            ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    var cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "PointCollecte_Charger_Lib";
                    cmd.Parameters.AddWithValue("@Code_PC", _CodePC);
                    cmd.Parameters.AddWithValue("@Code_Societe", GestionSession.SocieteCourante.CSociete);
                    cmd.Parameters.AddWithValue("@Code_Site", GestionSession.SocieteSite);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    var adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }

                CtrlHelper.FillGridView(gridView1, Titres(), dt);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void frmPointCollecteListe_Load(object sender, EventArgs e)
        {
            //LoadData();
            CtrlHelper.InitGridView(this.gridView1, Titres(), false);
            RemplirGrid();
            loadMap();
        }


        public void Actualiser()
        {
            CtrlHelper.EmptyControls(this);
            markers.Clear();
            RemplirGrid();
            loadMap();
        }

        private void frmPointCollecteListe_Activated(object sender, EventArgs e)
        {
            Actualiser();
        }





        private void grid_DoubleClick(object sender, EventArgs e)
        {
            if (Tag is FrmPointCollecte.FlagSecurite &&
                ((FrmPointCollecte.FlagSecurite)Tag).HasFlag(FrmPointCollecte.FlagSecurite.ModifDisabled))
                return;
            Modifier();
        }

        public void Modifier()
        {

            if (gridView1.FocusedRowHandle == GridControl.InvalidRowHandle)
                return;
            if (gridView1.FocusedRowHandle >= 0)
            {
                string CPoint = this.gridView1.GetFocusedRowCellDisplayText("Code P.Collecte");
                //  string cUtilisateur = gridView1.GetDataRow(gridView1.FocusedRowHandle)["Login"].ToString();

                FrmPointCollecte frm = new FrmPointCollecte(CPoint);

                frm.Text = Resources.Titre_FrmPointCollecte + @": " + CPoint;
                ((FrmMDI)MdiParent).LoadForm(frm);
            }
        }

        public enum FlagSecurite
        {
            ModifDisabled
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (gridView1.FocusedRowHandle == GridControl.InvalidRowHandle)
                return;

            //string cUtilisateur = gridView1.GetDataRow(gridView1.FocusedRowHandle)["CBien"].ToString();

            try
            {
                string CPoint = this.gridView1.GetFocusedRowCellDisplayText(this.gridView1.Columns["Code Point Collecte"]);

                PointCollecte o = new PointCollecte();
                o = PointCollecte.Charger(CPoint);
                o.Supprimer();
                Actualiser();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        public void Imprimer()
        {
            DXReport.Imprimer(gridControl1, Text);
        }


        public void Apercu()
        {
            DXReport.Apercu(gridControl1, Text);
        }






        public void Ajouter()
        {
            var frm = new FrmPointCollecte();
            frm.Text = Resources.Titre_FrmPointCollecte;
            ((FrmMDI)MdiParent).LoadForm(frm);
        }




        public void Supprimer()
        {
            if (gridView1.FocusedRowHandle == GridControl.InvalidRowHandle)
                return;

            //string cUtilisateur = gridView1.GetDataRow(gridView1.FocusedRowHandle)["COffice"].ToString();
            string msgEchoue = "Impossible de supprimer cet Point?! ";
            string msgSucces = "Suppression effectuée avec succès!";
            string CPoint = this.gridView1.GetFocusedRowCellDisplayText(this.gridView1.Columns["Code Point Collecte"]);
            var dialogResult = XtraMessageBox.Show("Voulez-vous vraiment supprimer cette Point?",
                "",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (dialogResult != DialogResult.Yes)
                return;
            try
            {


                PointCollecte o = new PointCollecte();
                o = PointCollecte.Charger(CPoint);
                o.Supprimer();
                Actualiser();
                warning(msgSucces);
            }
            catch (Exception)
            {
                warning(msgEchoue);
            }


        }



        private void warning(string msg)
        {
            XtraMessageBox.Show(msg,
                                         "Alerte",
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Warning,
                                         MessageBoxDefaultButton.Button1);
        }

        public void SelectionnerGridRow(bool bHaut)
        {
            if (bHaut)
                gridView1.FocusedRowHandle = gridView1.FocusedRowHandle - 1;
            else
                gridView1.FocusedRowHandle = gridView1.FocusedRowHandle + 1;

            Decimal lat = (Decimal)this.gridView1.GetFocusedRowCellValue(this.gridView1.Columns["lat"]);
            Decimal lng = (Decimal)this.gridView1.GetFocusedRowCellValue(this.gridView1.Columns["lng"]);


            if (lat != 0)
            {
                gmap.Position = new PointLatLng(Convert.ToDouble(lat), Convert.ToDouble(lng));
                markers.Clear();
                GMapMarker marker = new GMarkerGoogle(new PointLatLng(Convert.ToDouble(lat), Convert.ToDouble(lng)), GMarkerGoogleType.red);
                marker = Createmarker(this.gridView1.GetFocusedRowCellDisplayText(this.gridView1.Columns["Libelle Point Collecte"]), lat, lng);
                markers.Markers.Add(marker);
                gmap.UpdateMarkerLocalPosition(marker);
            }
        }




        private void loadMap()
        {
            gmap.Position = new PointLatLng(36.80, 10.18);
            gmap.ShowCenter = false;
            GMapProvider.WebProxy = WebRequest.GetSystemWebProxy();
            GMapProvider.WebProxy.Credentials = CredentialCache.DefaultCredentials;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
            string myDir = "C:\\CST\\data\\GoogleMapcashe";
            System.IO.Directory.CreateDirectory(myDir);
            gmap.CacheLocation = myDir;
            gmap.MapProvider = GMapProviders.GoogleMap;
            gmap.MinZoom = 2;
            gmap.MaxZoom = 18;
            gmap.Zoom = 10;
            gmap.AutoScroll = false;
            gmap.CanDragMap = true;
            gmap.DragButton = MouseButtons.Left;
            //gmap.MouseWheelZoomEnabled = false;
            //gmap.OnMapDrag += delegate() 
            //{
            //    gmap.Cursor = Cursors.Hand;
            //};
            /*------ to be considered by aymen 28/08/2018 --------*/
            //gmap.MouseWheel += delegate(object sender, MouseEventArgs e)
            //{
            //    gmap.Zoom = e.Delta > 0 ? gmap.Zoom + 0.1 : gmap.Zoom - 0.1;
            //};
            gmap.Manager.Mode = AccessMode.ServerAndCache;
            gmap.Overlays.Add(routes);
            gmap.Overlays.Add(markers);
            gmap.Refresh();
        }


        private void gmap_OnMarkerEnter(GMapMarker item)
        {
            //item.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
            t.Interval = 1;
            t.Tick += delegate(object s, EventArgs e1)
            {
                markers.Markers.Remove(item);
                markers.Markers.Add(item);
                gmap.UpdateMarkerLocalPosition(item);
                t.Stop();
            };
            t.Start();
        }



        private GMapMarker Createmarker(string text, Decimal lat, Decimal lng)
        {
            GMapMarker marker = new GMarkerGoogle(new PointLatLng(Convert.ToDouble(lat), Convert.ToDouble(lng)), GMarkerGoogleType.red);
            marker.ToolTipText = text;
            marker.ToolTip.Fill = Brushes.Black;
            marker.ToolTip.Foreground = Brushes.LightGray;
            marker.ToolTip.Stroke = Pens.Gray;
            marker.ToolTip.Format.Alignment = StringAlignment.Near;
            marker.ToolTip.Format.LineAlignment = StringAlignment.Center;
            marker.ToolTipMode = MarkerTooltipMode.Always;
            return marker;
        }
        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {

            Decimal lat = (Decimal)this.gridView1.GetFocusedRowCellValue(this.gridView1.Columns["lat"]);
            Decimal lng = (Decimal)this.gridView1.GetFocusedRowCellValue(this.gridView1.Columns["lng"]);


            if (lat != 0)
            {
                gmap.Position=
                gmap.Position = new PointLatLng(Convert.ToDouble(lat), Convert.ToDouble(lng));
                markers.Clear();
                GMapMarker marker = new GMarkerGoogle(new PointLatLng(Convert.ToDouble(lat), Convert.ToDouble(lng)), GMarkerGoogleType.red);
                marker = Createmarker(this.gridView1.GetFocusedRowCellDisplayText(this.gridView1.Columns["Libelle Point Collecte"]), lat, lng);
                markers.Markers.Add(marker);
                //gmap.Zoom = 12;
                gmap.UpdateMarkerLocalPosition(marker);
            }
        }




    }



}


