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
using CST.LePoint.Tiers.Referentiel;
using CST.LePoint.Securite;

namespace CST.LePoint.Intervention.Rattachements
{
    public partial class FrmCircuitListe : DevExpress.XtraEditors.XtraForm, IActionsListeSuppression, IActionsRechercher
    {
        GMapOverlay routes = new GMapOverlay("routes");
        private GMapOverlay overlayOne = new GMapOverlay("OverlayOne");
        private GMap.NET.WindowsForms.GMapOverlay markers = new GMap.NET.WindowsForms.GMapOverlay("markers");
        List<LatLng> listGeo = new List<LatLng>();
        private GMap.NET.WindowsForms.GMapMarker markerClient;
        private string CircuiGlob = string.Empty;
        private List<string> donemarkers = new List<string>();
        List<CircuitColor> lcolor = new List<CircuitColor>();

        public FrmCircuitListe()
        {
            InitializeComponent();
        }

        #region Utilitaires

        private static GvColumnProprietes Titres()
        {
            GvColumnProprietes proprietes = new GvColumnProprietes();
            proprietes.Add(new GvColumnPropriete("[X]", GvColumnPropriete.GvColumnType.Boolean, GvColumnPropriete.GvColumnEtat.Enable));
            proprietes.Add(new GvColumnPropriete("Circuit"));
            proprietes.Add(new GvColumnPropriete("Libelle"));

            return proprietes;
        }

        private static GvColumnProprietes TitresDetail()
        {
            GvColumnProprietes proprietes = new GvColumnProprietes();
            proprietes.Add(new GvColumnPropriete("Circuit", GvColumnPropriete.GvColumnEtat.Invisible));
            proprietes.Add(new GvColumnPropriete("C. Point"));
            proprietes.Add(new GvColumnPropriete("Libelle"));
            proprietes.Add(new GvColumnPropriete("Region", GvColumnPropriete.GvColumnType.LookUpVide, CST.LePoint.Tiers.Referentiel.RegionCollection.Charger()));
            proprietes.Add(new GvColumnPropriete("Gouvernorat", GvColumnPropriete.GvColumnType.LookUpVide, CST.LePoint.Tiers.Referentiel.GouvernoratCollection.Charger()));
            proprietes.Add(new GvColumnPropriete("GPS", GvColumnPropriete.GvColumnType.Boolean));
            proprietes.Add(new GvColumnPropriete("Latitude", GvColumnPropriete.GvColumnType.Decimal, GvColumnPropriete.GvColumnEtat.Invisible));
            proprietes.Add(new GvColumnPropriete("Longitude", GvColumnPropriete.GvColumnType.Decimal, GvColumnPropriete.GvColumnEtat.Invisible));

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
                    cmd.CommandText = "Circuit_Vue_Rechercher";
                    cmd.Parameters.AddWithValue("@Code_Societe", GestionSession.SocieteCourante.CSociete);
                    cmd.Parameters.AddWithValue("@Code_Site", GestionSession.SocieteSite);
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

        private GMapMarker Createmarker(string text, Decimal lat, Decimal lng, Color bg, string CPoint, string circuit)
        {
            GMapMarker marker = markers.Markers.Where(m => (m.Tag != null ? m.Tag.ToString() : "") == CPoint).SingleOrDefault();            
            if (marker != null)
            {
                for (int i = 0; i < gridVDetail.RowCount; i++)
                {
                       string point = this.gridVDetail.GetRowCellDisplayText(i, "C. Point");
                       string ccircuit = this.gridVDetail.GetRowCellDisplayText(i, "Circuit");

                       if (CPoint == point && !donemarkers.Contains(point))
                       {
                           marker.ToolTip.Fill = new SolidBrush(Color.LightGray);
                           marker.ToolTip.Foreground = new SolidBrush(IdealTextColor(Color.LightGray));
                           marker.ToolTip.Stroke = new Pen(new SolidBrush(Color.Aquamarine));
                           marker.ToolTip.Format.Alignment = StringAlignment.Near;
                           marker.ToolTip.Format.LineAlignment = StringAlignment.Center;
                           marker.ToolTipText = marker.ToolTipText + " [ " + ccircuit + " ]";
                       }
                           
                                                                                                                                 
                }
                donemarkers.Add(CPoint);
               
                

                //if (!marker.ToolTipText.Contains("[")) 
                //{                    
                //    marker.ToolTipText = marker.ToolTipText + " [ " + circuit + " ]";
                //}
                //else
                    //marker.ToolTipText = marker.ToolTipText.Insert(marker.ToolTipText.Length - 2, ", " + circuit);
                    markers.Markers.Remove(marker);
                    markerClient = marker;
            }
            else
            {
                markerClient = new GMarkerGoogle(new PointLatLng(Convert.ToDouble(lat), Convert.ToDouble(lng)), GMarkerGoogleType.red);
                markerClient.ToolTipText = text;
                markerClient.ToolTip.Fill = new SolidBrush(bg);
                markerClient.ToolTip.Foreground = new SolidBrush(IdealTextColor(bg));
                markerClient.ToolTip.Stroke = new Pen(new SolidBrush(IdealTextColor(bg)));
                markerClient.ToolTip.Format.Alignment = StringAlignment.Near;
                markerClient.ToolTip.Format.LineAlignment = StringAlignment.Center;
                markerClient.ToolTipMode = MarkerTooltipMode.Always;
                markerClient.Tag = CPoint;

            }
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
                cmd.CommandText = "Circuit_PointCollecte_Vue_Rechercher";
                cmd.Parameters.AddWithValue("@CCircuits", this.GetCircuitNames());
                cmd.Parameters.AddWithValue("@Code_Societe", GestionSession.SocieteCourante.CSociete);
                cmd.Parameters.AddWithValue("@Code_Site", GestionSession.SocieteSite);

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

                for (int i = 0; i < gridVDetail.RowCount; i++)
                {
                    if ((bool)this.gridVDetail.GetRowCellValue(i, "GPS"))
                    {
                        string point = this.gridVDetail.GetRowCellDisplayText(i, "C. Point");
                        string ccircuit = this.gridVDetail.GetRowCellDisplayText(i, "Circuit");
                        string tooltiptext = this.gridVDetail.GetRowCellDisplayText(i, "Libelle");
                        Decimal lat = (Decimal)this.gridVDetail.GetRowCellValue(i, "Latitude");
                        Decimal lng = (Decimal)this.gridVDetail.GetRowCellValue(i, "Longitude");
                        Circuit c= Circuit.Charger(ccircuit);

                        this.Createmarker(tooltiptext + " ", lat, lng, Color.FromArgb(c.Couleur), point, ccircuit);
                        //this.Createmarker(tooltiptext + " ", lat, lng, this.lcolor.Find(n => n.name == ccircuit).color);
                        markers.Markers.Add(markerClient);
                        gmap.UpdateMarkerLocalPosition(markerClient);
                    }
                }
                donemarkers.Clear();

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
            CircuiGlob = string.Empty;
            markers.Markers.Clear();
            routes.Routes.Clear();
            this.lcolor.Clear();
            RemplirGridV();

            CtrlHelper.InitGridView(this.gridVDetail, TitresDetail());
        }

        #endregion Utilitaires

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
            FrmCircuit frm = new FrmCircuit() { Text = Resources.Titre_FrmCircuit };
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

                FrmCircuit frm = new FrmCircuit(circuit) { Text = String.Format(@"{0}: {1}", "Circuit", circuit) };
                ((FrmMDI)this.MdiParent).LoadForm(frm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Supprimer()
        {
            
            if (this.gridV.RowCount == 0)
                return;

            string ccirc = this.gridV.GetFocusedRowCellDisplayText("Circuit");

            DialogResult dialogResult = XtraMessageBox.Show("Voulez-vous Supprimer ce Circuit ?",
                                                Resources.NomApplication,
                                                MessageBoxButtons.YesNoCancel,
                                                MessageBoxIcon.Question,
                                                MessageBoxDefaultButton.Button1);
            if (dialogResult != DialogResult.Yes)
                return;

            try
            {


                Circuit circuitt = new Circuit();
                circuitt.Code_Circuit = ccirc;
                circuitt.Supprimer();
                Actualiser();
                XtraMessageBox.Show("Suppression avec Succès",
                                   Resources.NomApplication,
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Information,
                                   MessageBoxDefaultButton.Button1);
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Suppression echoué",
                                    Resources.NomApplication,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning,
                                    MessageBoxDefaultButton.Button1);
            }

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
            gmap.MouseWheelZoomType = MouseWheelZoomType.MousePositionWithoutCenter;
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
                    Circuit cr = Circuit.Charger(this.gridV.GetFocusedRowCellValue("Circuit").ToString());
                    cc.name = name;
                    cc.color = Color.FromArgb(cr.Couleur) ;
                    //cc.color = Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
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
                this.gmap.ZoomAndCenterMarkers(markers.Id);
                
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
            if (e.Column != gv.Columns["C. Point"])    
                return;
            string name = gv.GetRowCellDisplayText(e.RowHandle, "Circuit");
            if (this.lcolor.Exists(x => x.name == name))
            {
                CircuitColor cc = this.lcolor.Find(x => x.name == name);
                e.Appearance.BackColor = cc.color;
                e.Appearance.ForeColor = this.IdealTextColor(cc.color);

            }

        }

        private void LookUpEdit_KeyDown(object sender, KeyEventArgs e) 
        {
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Delete)
                if(sender is LookUpEdit)
                    ( sender as LookUpEdit ).EditValue = string.Empty;
        }

        public void Rechercher()
        {
            markers.Markers.Clear();
            routes.Routes.Clear();
            this.lcolor.Clear();
            RemplirGridV();
            CtrlHelper.InitGridView(this.gridVDetail, TitresDetail());
        }

        private void gmap_OnMapZoomChanged()
        {
            Console.WriteLine(this.gmap.Zoom);
        }
    }
}