using CST.LePoint.CtrlLibrary;
using CST.LePoint.Intervention.Metier;
using CST.LePoint.Intervention.Properties;
using CST.LePoint.Securite;
using CST.LePoint.Securite.Entites;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CST.LePoint.Intervention.Rattachements
{
    public partial class FrmBusTracking : DevExpress.XtraEditors.XtraForm , IActionsCommun
    {
        private GMap.NET.WindowsForms.GMapOverlay markers = new GMap.NET.WindowsForms.GMapOverlay("markers");
        List<CircuitColor> lcolor = new List<CircuitColor>();
        Timer trackingtime = new Timer();

        public FrmBusTracking()
        {
            InitializeComponent();
        }

        public Color IdealTextColor(Color bg)
        {
            int nThreshold = 105;
            int bgDelta = Convert.ToInt32((bg.R * 0.299) + (bg.G * 0.587) +
                                          (bg.B * 0.114));

            Color foreColor = (255 - bgDelta < nThreshold) ? Color.Black : Color.White;
            return foreColor;
        }

        private static GvColumnProprietes Titres()
        {
            GvColumnProprietes proprietes = new GvColumnProprietes();
            proprietes.Add(new GvColumnPropriete("[X]", GvColumnPropriete.GvColumnType.Boolean, GvColumnPropriete.GvColumnEtat.Enable));
            proprietes.Add(new GvColumnPropriete("Num IMM"));
            proprietes.Add(new GvColumnPropriete("Model"));
            proprietes.Add(new GvColumnPropriete("Code_Circuit", GvColumnPropriete.GvColumnEtat.Invisible));
            proprietes.Add(new GvColumnPropriete("Circuit", GvColumnPropriete.GvColumnEtat.Disable));
            proprietes.Add(new GvColumnPropriete("Capacite", GvColumnPropriete.GvColumnEtat.Disable));
            return proprietes;
        }

        private static GvColumnProprietes TitresEmployer()
        {
            GvColumnProprietes proprietes = new GvColumnProprietes();
            proprietes.Add(new GvColumnPropriete("Employé"));
            proprietes.Add(new GvColumnPropriete("In/Out"));
            proprietes.Add(new GvColumnPropriete("Allée/Retour"));
            proprietes.Add(new GvColumnPropriete("Heure"));
            return proprietes;
        }

        private GMapMarker Createmarker(string text, Decimal lat, Decimal lng, string CBus, Color bg, PinType type)
        {
            var img = Properties.Resources.PC_32;
            if(PinType.Bus == type)
                 img = Properties.Resources.busPin_32;
            if (PinType.Site == type)
                img = Properties.Resources.Site_32;
            GMapMarker marker = new GMarkerGoogle(new PointLatLng(Convert.ToDouble(lat), Convert.ToDouble(lng)), img);
            //GMapToolTip tooltip = new GMapToolTip(marker);
            //tooltip.Font = new System.Drawing.Font("Tahoma", 9F);
            marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;

            marker.ToolTipText = text;
            if (PinType.Bus == type)
            {
                marker.ToolTip.Offset = new Point(10, 10);
                bg = Color.Silver;
                //marker.ToolTipMode = MarkerTooltipMode.Always;
            }
            marker.ToolTip.Fill = new SolidBrush(bg);
            marker.ToolTip.Foreground = new SolidBrush(IdealTextColor(bg));
            marker.ToolTip.TextPadding = new System.Drawing.Size(new Point(14, 14));
            marker.ToolTip.Stroke = new Pen(new SolidBrush(IdealTextColor(bg)));
            marker.ToolTip.Format.Alignment = StringAlignment.Center;
            marker.ToolTip.Format.LineAlignment = StringAlignment.Center;            
            
            //marker.ToolTip = tooltip;
            
            marker.Tag = CBus + (type == PinType.Bus ? "_$Bus$" : "");
            return marker;
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
                    cmd.CommandText = "Bus_Vue_Rechercher";
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

        private void gridV_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            //GridView gv = sender as GridView;
            //if ((e.Column != gv.Columns["Num IMM"]) && (e.Column != gv.Columns["[X]"]))
            //    return;
            //string name = gv.GetRowCellDisplayText(e.RowHandle, "Num IMM");
            //if (this.lcolor.Exists(x => x.name == name))
            //{
            //    CircuitColor cc = this.lcolor.Find(x => x.name == name);
            //    e.Appearance.BackColor = cc.color;
            //    e.Appearance.ForeColor = this.IdealTextColor(cc.color);
            //}
        }

        private void FrmBusTracking_Load(object sender, EventArgs e)
        {
            trackingtime.Interval = 10000;

            this.LoadData();
            this.loadMap();
        }

        private void LoadData()
        {
            if (trackingtime.Enabled)
                trackingtime.Stop();
            markers.Markers.Clear();
            this.lcolor.Clear();
            RemplirGridV();
            this.txtIMM.Text = "";
            this.txtModel.Text = "";
            this.txtCapacite.Text = "";  //khoubaib
            this.txtChauffeur.Text = "";
            this.txtdate.Text = DateTime.Now.ToString();
            CtrlHelper.InitGridView(this.gridVEmploye, TitresEmployer());
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

        int checkedRowIndex_gridVEquipes = -1;
        private void gridV_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Caption == "[X]")
            {
                Random random = new Random();
                markers.Markers.Clear();
                string name = this.gridV.GetRowCellDisplayText(e.RowHandle, "Num IMM");
                if ((bool)(e.Value))
                {
                    int rowHandle = this.gridV.GetRowHandle(checkedRowIndex_gridVEquipes);
                    this.gridV.SetRowCellValue(rowHandle, "[X]", false);
                    checkedRowIndex_gridVEquipes = this.gridV.GetDataSourceRowIndex(e.RowHandle);

                    this.tracking(name);
                    this.gmap.ZoomAndCenterMarkers(markers.Id);
                    this.fillemploye(name);
                    trackingtime.Tick += delegate
                    {
                        this.tracking(name);
                        this.fillemploye(name);
                    };
                    trackingtime.Start();
                    //if (this.lcolor.Exists(x => x.name == name))
                    //    return;
                    //CircuitColor cc = new CircuitColor();
                    //cc.name = name;
                    //cc.color = Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
                    //this.lcolor.Add(cc);
                }
                else
                {
                    this.txtIMM.Text = "";
                    this.txtModel.Text = "";
                    this.txtCapacite.Text = "";
                    this.txtChauffeur.Text = "";
                    //CtrlHelper.InitGridView(this.gridVEmploye, TitresEmployer());
                    //this.lcolor.Remove(this.lcolor.Find(x => x.name == name));
                    trackingtime.Stop();
                }
                this.gridV.RefreshData();
            }
        }

        private void tracking(string imm)
        {
            markers.Markers.Clear();
            List<GPSTracking> PCs = GPSPointCollecte.GetPC(imm);
            foreach (GPSTracking gps in PCs)
            {
                GMapMarker marker = Createmarker(gps.Titre, gps.Latitude, gps.Longitude, imm, Color.FromArgb(34,40,49), gps.pinType);
                markers.Markers.Add(marker);
                gmap.UpdateMarkerLocalPosition(marker);               
            }
        }

        private void FrmBusTracking_Leave(object sender, EventArgs e)
        {
            trackingtime.Stop();
        }

        private void gmap_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (item.Tag != null && item.Tag.ToString().EndsWith("_$Bus$"))
                {
                    string imm = item.Tag.ToString().Replace("_$Bus$", "");
                    this.fillemploye(imm);
                }
            }
        }

        private void fillemploye(string IMM)
        {
            this.txtIMM.Text = "";
            this.txtModel.Text = "";
            this.txtCapacite.Text = "";  //khoubaib
            this.txtChauffeur.Text = "";
            //CtrlHelper.InitGridView(this.gridVEmploye, TitresEmployer());
            DataView dv = this.gridVEmploye.DataSource as DataView;
            dv.Table.Clear();
            GPSBus bus = GPSBus.Charger(IMM);
            if (bus != null)
            {
                this.txtIMM.Text = IMM;
                this.txtModel.Text = bus.Model;
                this.txtCapacite.Text = bus.capacite ; //khoubaib
                this.txtdate.Text = DateTime.Now.ToString();
                this.txtChauffeur.Text = bus.Chauffeur;
                CtrlHelper.FillGridView(this.gridVEmploye, TitresEmployer(), bus.employes);
            }
        }

        public void Actualiser()
        {
          LoadData();
          loadMap();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            
            
            if (this.gridV.RowCount == 0)
                return;

            string Imm = this.gridV.GetFocusedRowCellDisplayText("Num IMM");

            DialogResult dialogResult = XtraMessageBox.Show("Voulez-vous reseter  la capactité de cette Bus ?",
                                                Resources.NomApplication,
                                                MessageBoxButtons.YesNoCancel,
                                                MessageBoxIcon.Question,
                                                MessageBoxDefaultButton.Button1);
            if (dialogResult != DialogResult.Yes)
                return;

            try
            {


                Bus bus = new Bus();
                bus.Reset(Imm);
                fillemploye(Imm);
                this.gridV.RefreshData();
                XtraMessageBox.Show("Reset avec Succès",
                                   Resources.NomApplication,
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Information,
                                   MessageBoxDefaultButton.Button1);
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Reset echoué",
                                    Resources.NomApplication,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning,
                                    MessageBoxDefaultButton.Button1);
            }        
        }
    }
}
