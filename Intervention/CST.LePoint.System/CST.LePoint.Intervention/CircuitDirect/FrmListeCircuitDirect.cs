using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CST.LePoint.Securite.Entites;
using CST.LePoint.CtrlLibrary;
using System.Data.SqlClient;
using System.Configuration;
using CST.LePoint.Intervention.Properties;
using DevExpress.XtraGrid;
using CST.LePoint.Intervention.Metier;
using GMap.NET.MapProviders;
using GMap.NET;
using CST.LePoint.Tiers.Metier;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms;
using System.Net;
using DevExpress.XtraGrid.Views.Grid;
using CST.LePoint.Intervention.Rattachements;

namespace CST.LePoint.Intervention.CircuitDirect
{
    public partial class FrmListeCircuitDirect : DevExpress.XtraEditors.XtraForm, IActionsListeSuppression
    {
        GMapOverlay routes = new GMapOverlay("routes");
        private GMapOverlay overlayOne = new GMapOverlay("OverlayOne");
        private GMap.NET.WindowsForms.GMapOverlay markers = new GMap.NET.WindowsForms.GMapOverlay("markers");
        List<LatLng> listGeo = new List<LatLng>();
        private GMap.NET.WindowsForms.GMapMarker markerClient;
        private string CircuiGlob = string.Empty;
        List<CircuitColor> lcolor = new List<CircuitColor>();

        public FrmListeCircuitDirect()
        {
            InitializeComponent();
        }

        #region Utilitaires

        private static GvColumnProprietes Titres()
        {
            GvColumnProprietes proprietes = new GvColumnProprietes();
            proprietes.Add(new GvColumnPropriete("[X]", GvColumnPropriete.GvColumnType.Boolean, GvColumnPropriete.GvColumnEtat.Enable));
            proprietes.Add(new GvColumnPropriete("Circuit"));
            proprietes.Add(new GvColumnPropriete("Libéllé"));
            proprietes.Add(new GvColumnPropriete("Equipe", GvColumnPropriete.GvColumnType.LookUpVide, EquipeCollection.Charger()));

            return proprietes;
        }

        private static GvColumnProprietes TitresDetail()
        {
            GvColumnProprietes proprietes = new GvColumnProprietes();

            //proprietes.Add(new GvColumnPropriete("N° Convention"));
            proprietes.Add(new GvColumnPropriete("Circuit", GvColumnPropriete.GvColumnEtat.Invisible));
            proprietes.Add(new GvColumnPropriete("C. Client"));
            proprietes.Add(new GvColumnPropriete("Raison Sociale"));
            proprietes.Add(new GvColumnPropriete("Region", GvColumnPropriete.GvColumnType.LookUpVide, CST.LePoint.Tiers.Referentiel.RegionCollection.Charger()));
            proprietes.Add(new GvColumnPropriete("Gouvernorat", GvColumnPropriete.GvColumnType.LookUpVide, CST.LePoint.Tiers.Referentiel.GouvernoratCollection.Charger()));
            proprietes.Add(new GvColumnPropriete("Ordre", GvColumnPropriete.GvColumnType.Integer));
            proprietes.Add(new GvColumnPropriete("GPS", GvColumnPropriete.GvColumnType.Boolean));
            proprietes.Add(new GvColumnPropriete("Latitude", GvColumnPropriete.GvColumnType.Decimal, GvColumnPropriete.GvColumnEtat.Invisible));
            proprietes.Add(new GvColumnPropriete("Longitude", GvColumnPropriete.GvColumnType.Decimal, GvColumnPropriete.GvColumnEtat.Invisible));

            //proprietes.Add(new GvColumnPropriete("Nb. Passage", GvColumnPropriete.GvColumnType.Integer, GvColumnPropriete.GvColumnEtat.Invisible));
            return proprietes;

        }

        private void RemplirGridV()
        {
            DataTable dtListe = new DataTable();

            try
            {
                this.gridV.RowCellStyle -= gridV_RowCellStyle;
                CtrlHelper.InitGridView(this.gridV, Titres());
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Circuit_Vue_Rechercher_Equipe";
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dtListe);
                }
                CtrlHelper.FillGridView(this.gridV, Titres(), dtListe);
                this.gridV.RowCellStyle += gridV_RowCellStyle;

            }
            catch (Exception)
            {
                throw;
            }
        }

        private GMapMarker Createmarker(string text, Decimal lat, Decimal lng, Color bg)
        {
            markerClient = new GMarkerGoogle(new PointLatLng(Convert.ToDouble(lat), Convert.ToDouble(lng)),GMarkerGoogleType.red);
            markerClient.ToolTipText = text;
            markerClient.ToolTip.Fill = new SolidBrush(bg);
            markerClient.ToolTip.Foreground =  new SolidBrush(IdealTextColor(bg));
            markerClient.ToolTip.Stroke = new Pen(new SolidBrush(IdealTextColor(bg)));
            markerClient.ToolTip.Format.Alignment = StringAlignment.Near;
            markerClient.ToolTip.Format.LineAlignment = StringAlignment.Center;
            markerClient.ToolTipMode = MarkerTooltipMode.Always;
            return markerClient;
        }

        private void RemplirGridVDetail()
        {
            DataTable dtListe = new DataTable();

            //loadMap();
            markers.Markers.Clear();
            routes.Routes.Clear();
            this.gridVDetail.RowCellStyle -= gridV_RowCellStyle;
            CtrlHelper.InitGridView(this.gridVDetail, TitresDetail());
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "CircuitDetailClient_Vue_Rechercher";
                cmd.Parameters.AddWithValue("@CCircuits", this.GetCircuitNames());

                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dtListe);
            }
            CtrlHelper.FillGridView(this.gridVDetail, TitresDetail(), dtListe);
            this.gridVDetail.RowCellStyle += gridV_RowCellStyle;
            try
            {
                if (ChkGPS.Checked)
                {
                    for (int i = 0; i < gridVDetail.RowCount; i++)
                    {
                        if ((bool)this.gridVDetail.GetRowCellValue(i, "GPS"))
                        {
                            string client = this.gridVDetail.GetRowCellDisplayText(i, "C. Client");
                            string ccircuit = this.gridVDetail.GetRowCellDisplayText(i, "Circuit");
                            string tooltiptext = this.gridVDetail.GetRowCellDisplayText(i, "Raison Sociale");
                            Decimal lat = (Decimal)this.gridVDetail.GetRowCellValue(i, "Latitude");
                            Decimal lng = (Decimal)this.gridVDetail.GetRowCellValue(i, "Longitude");

                            this.Createmarker(tooltiptext + " ", lat, lng, this.lcolor.Find(n => n.name == ccircuit).color);
                            markers.Markers.Add(markerClient);
                            gmap.UpdateMarkerLocalPosition(markerClient);
                        }
                    }
                }
            }
            catch (Exception)
            {
                markers.Markers.Remove(markerClient);
                XtraMessageBox.Show("Problème de géolocalisation ! Client sans Coordonnées Géographiques",
                                     Resources.NomApplication,
                                     MessageBoxButtons.OK,
                                     MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                this.gridV.FocusedColumn = this.gridV.Columns["Circuit"];
                listGeo.Clear();
                return;
            }
        }

        private void LoadData()
        {
            this.ChkGPS.Checked = true;
            CircuiGlob = string.Empty;
            markers.Markers.Clear();
            routes.Routes.Clear();
            this.lcolor.Clear();
            RemplirGridV();
            CtrlHelper.InitGridView(this.gridVDetail, TitresDetail());
        }

        #endregion Utilitaires

        public void Apercu()
        {
            return;
        }

        public void Actualiser()
        {
            LoadData();
        }

        public void Ajouter()
        {
            Ajouter(this.MdiParent);
        }

        public static void Ajouter(Form parent)
        {
            FrmAjoutCircuitDirect frm = new FrmAjoutCircuitDirect() { Text = Resources.Titre_FrmCircuit };
            ((FrmMDI)parent).LoadForm(frm);
        }

        public void SelectionnerGridRow(bool bHaut)
        {
            //throw new NotImplementedException();
        }

        public void Modifier()
        {
            if (gridV.FocusedRowHandle == GridControl.InvalidRowHandle)
                return;
            try
            {
                string circuit = this.gridV.GetFocusedRowCellDisplayText(this.gridV.Columns["Circuit"]);

                FrmCircuit frm = new FrmCircuit(circuit) { Text = String.Format(@"{0}: {1}", Resources.Titre_FrmCircuit, circuit) };
                ((FrmMDI)this.MdiParent).LoadForm(frm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Supprimer()
        {
            string ccirc = this.gridV.GetFocusedRowCellDisplayText("Circuit");
            if (this.gridV.RowCount == 0)
                return;

            DialogResult dialogResult = XtraMessageBox.Show("Voulez-vous Supprimer ce Circuit ?",
                                                Resources.NomApplication,
                                                MessageBoxButtons.YesNoCancel,
                                                MessageBoxIcon.Question,
                                                MessageBoxDefaultButton.Button1);
            if (dialogResult != DialogResult.Yes)
                return;

            Circuit circuitt = Circuit.Charger(ccirc);
            circuitt.Supprimer();

            XtraMessageBox.Show("Suppression avec Succés",
                                    Resources.NomApplication,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information,
                                    MessageBoxDefaultButton.Button1);
            Actualiser();
        }

        private void FrmCircuitListe_Load(object sender, EventArgs e)
        {
            LoadData();
            loadMap();
        }

        private void gridV_DoubleClick(object sender, EventArgs e)
        {
            Modifier();
        }

        private void loadMap()
        {
            gmap.Position = new PointLatLng(34, 9.5);
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
            gmap.Zoom = 7;
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

        private void gridV_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
          /*  GridView gv = sender as GridView;
            if (gv.Columns["Circuit"] == null)
                return;
            string name = gv.GetRowCellDisplayText(e.RowHandle, "Circuit");                
            if (this.lcolor.Exists(x => x.name == name))
            {
                CircuitColor cc = this.lcolor.Find(x => x.name == name);
                e.Appearance.BackColor = cc.color;
                e.Appearance.ForeColor = this.IdealTextColor(cc.color);
                e.HighPriority = true;
            }*/
        }

        public Color IdealTextColor(Color bg)
        {
            int nThreshold = 105;
            int bgDelta = Convert.ToInt32((bg.R * 0.299) + (bg.G * 0.587) +
                                          (bg.B * 0.114));

            Color foreColor = (255 - bgDelta < nThreshold) ? Color.Black : Color.White;
            return foreColor;
        }

        private void gridV_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Caption == "[X]")
            {
                Random random = new Random();
                string name = this.gridV.GetRowCellDisplayText(e.RowHandle, "Circuit");                
               if ((bool)(e.Value))
                {
                    if (this.lcolor.Exists(x => x.name == name))
                        return;
                   CircuitColor cc = new CircuitColor();
                    cc.name = name;
                     cc.color = Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
                    this.lcolor.Add(cc);
                }
                else
                {
                    this.lcolor.Remove(this.lcolor.Find(x => x.name == name));
                }
                //this.gridV.FocusedColumn = this.gridV.Columns["Circuit"];
                this.gridV.RefreshData();
                this.RemplirGridVDetail();
                this.gridVDetail.RefreshData();
            }
        }

        private string GetCircuitNames() 
        {
            string str = string.Empty;
            foreach (CircuitColor cc in this.lcolor)
            {
                if (str.Length == 0)
                    str = cc.name;
                else
                    str += "&" + cc.name;
            }
            return str;
        }

        private void ChkGPS_CheckedChanged(object sender, EventArgs e)
        {
            markers.Markers.Clear();
            routes.Routes.Clear();
            if (this.ChkGPS.Checked) 
            {
                this.gridV.RefreshData();
                this.RemplirGridVDetail();
                this.gridVDetail.RefreshData();
            }
        }

        private void gridV_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView gv = sender as GridView;
            if ((e.Column != gv.Columns["Circuit"])   && (e.Column != gv.Columns["[X]"]))
                return;
            string name = gv.GetRowCellDisplayText(e.RowHandle, "Circuit");
            if (this.lcolor.Exists(x => x.name == name))
            {
                CircuitColor cc = this.lcolor.Find(x => x.name == name);
                e.Appearance.BackColor = cc.color;
                e.Appearance.ForeColor = this.IdealTextColor(cc.color);
               
            }
        }

        private void gridVDetail_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {

            GridView gv = sender as GridView;
            if (e.Column != gv.Columns["C. Client"])    
                return;
            string name = gv.GetRowCellDisplayText(e.RowHandle, "Circuit");
            if (this.lcolor.Exists(x => x.name == name))
            {
                CircuitColor cc = this.lcolor.Find(x => x.name == name);
                e.Appearance.BackColor = cc.color;
                e.Appearance.ForeColor = this.IdealTextColor(cc.color);

            }

        }
    }

    class CircuitColor {
        public string name { get; set; }
        public Color color { get; set; }
    }
}