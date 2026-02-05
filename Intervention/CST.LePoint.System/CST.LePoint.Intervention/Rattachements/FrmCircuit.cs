using CST.LePoint.CtrlLibrary;
using CST.LePoint.Intervention.Metier;
using CST.LePoint.Intervention.Properties;
using CST.LePoint.Securite;
using CST.LePoint.Securite.Entites;
using CST.LePoint.Tiers.Metier;
using CST.LePoint.Vente.Metier;
using DevExpress.Utils;
using DevExpress.XtraEditors;
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
    public partial class FrmCircuit : DevExpress.XtraEditors.XtraForm, IActionsSave, IActionsRechercher
    {
        List<LatLng> listGeo = new List<LatLng>();
        private string nomColonneModifie = string.Empty;
        bool Continuer = false;
        private string _circuit = null;

        //map

        GMapOverlay routes = new GMapOverlay("routes");
        private GMapOverlay overlayOne = new GMapOverlay("OverlayOne");
        private GMap.NET.WindowsForms.GMapOverlay markers = new GMap.NET.WindowsForms.GMapOverlay("markers");
        private GMap.NET.WindowsForms.GMapMarker markerClient;
        private GMap.NET.WindowsForms.GMapMarker markerDepart;
        private GMap.NET.WindowsForms.GMapMarker markerArrive;

        public FrmCircuit()
        {
            InitializeComponent();
        }

        public FrmCircuit(string circuit)
        {
            InitializeComponent();
            _circuit = circuit;
            this.txtCCircuit.Properties.ReadOnly = true;
            //this.txtLibelle.Properties.ReadOnly = true;
            //this.txtKm.Properties.ReadOnly = true;
            //this.txtDuree.Properties.ReadOnly = true;
            //this.lkpPointFin.Properties.ReadOnly = true;
            //this.lkpPointDepart.Properties.ReadOnly = true;

        }

        private void FrmCircuit_Load(object sender, EventArgs e)
        {
            LoadData();
            loadMap();
        }

        private static GvColumnProprietes Titres()
        {
            GvColumnProprietes proprietes = new GvColumnProprietes();
            proprietes.Add(new GvColumnPropriete("[X]", GvColumnPropriete.GvColumnType.Boolean , GvColumnPropriete.GvColumnEtat.Enable));
            proprietes.Add(new GvColumnPropriete("C. Point"));
            proprietes.Add(new GvColumnPropriete("Libelle"));
            //proprietes.Add(new GvColumnPropriete("Ordre", GvColumnPropriete.GvColumnType.Integer, GvColumnPropriete.GvColumnEtat.Enable));
            proprietes.Add(new GvColumnPropriete("Region"));
            proprietes.Add(new GvColumnPropriete("Gouvernorat"));
            proprietes.Add(new GvColumnPropriete("GPS", GvColumnPropriete.GvColumnType.Boolean));
            return proprietes;
        }

        private void RemplirGridVCircuit()
        {
            DataTable dtListe = new DataTable();
            var waitForm = new WaitDialogForm("Chargement en cours...",
                                  "Veuillez patienter !");
            try
            {
                this.chkallcheck.Checked = false;
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandTimeout = 0;
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "PointCollecteCircuit_Vue_Rechercher";
                    cmd.Parameters.AddWithValue("@Code_Circuit", this._circuit);
                    cmd.Parameters.AddWithValue("@Code_Societe", GestionSession.SocieteCourante.CSociete);
                    cmd.Parameters.AddWithValue("@Code_Site", GestionSession.SocieteSite);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dtListe);
                }

                CtrlHelper.FillGridView(gridV, Titres(), dtListe);
                gridV.OptionsView.ShowFooter = true;
                gridV.BestFitColumns();
            }
            catch (Exception)
            {
                throw;
            }
            finally {
                waitForm.Close();
                waitForm.Dispose();
            }
        }

        public void Rechercher() {
            this.Actualiser();
        }

        private GMapMarker Createmarker(string text, Decimal lat, Decimal lng, string CClient , Color bg)
        {
            GMapMarker marker = new GMarkerGoogle(new PointLatLng(Convert.ToDouble(lat), Convert.ToDouble(lng)), GMarkerGoogleType.red);
            marker.ToolTipText = text;
            marker.ToolTip.Fill = new SolidBrush(bg);
            marker.ToolTip.Foreground = new SolidBrush(IdealTextColor(bg));
            marker.ToolTip.Stroke = new Pen(new SolidBrush(IdealTextColor(bg)));
            marker.ToolTip.Format.Alignment = StringAlignment.Near;
            marker.ToolTip.Format.LineAlignment = StringAlignment.Center;
            marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            marker.Tag = CClient;
            return marker;
        }

        public Color IdealTextColor(Color bg)
        {
            int nThreshold = 105;
            int bgDelta = Convert.ToInt32((bg.R * 0.299) + (bg.G * 0.587) +
                                          (bg.B * 0.114));

            Color foreColor = (255 - bgDelta < nThreshold) ? Color.Black : Color.White;
            return foreColor;
        }

        public void LoadData()
        {
            CtrlHelper.InitValidationProvider(this.dxValidationProvider1, this);
            CtrlHelper.FillLookUpEdit(this.lkpPointDepart, PointCollecteCollection.Charger());
            CtrlHelper.FillLookUpEdit(this.lkpPointFin, PointCollecteCollection.Charger());
            CtrlHelper.InitGridView(gridV, Titres());
            RemplirGridVCircuit();
            if (!(string.IsNullOrEmpty(this._circuit)))
            {
                Circuit circuit = Circuit.Charger(this._circuit);
                this.txtCCircuit.Text = circuit.Code_Circuit;
                this.txtLibelle.Text = circuit.Lib_Circuit;
                this.txtKm.Value = circuit.Km_Circuit;
                this.txtDuree.Value = circuit.Duree_Circuit;
                this.colorPick.EditValue = circuit.Couleur;
                this.lkpPointDepart.EditValue = circuit.PC_Depart_Circuit;
                this.lkpPointFin.EditValue = circuit.PC_Fin_Circuit;
                this.gridV.Columns["[X]"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
                if(this.gridV.RowCount >0)
                    this.gridV.FocusedRowHandle = 0;
                try
                {
                    if (ChkGPS.Checked)
                    {
                        markers.Markers.Clear();
                        routes.Routes.Clear();
                        //for (int i = 0; i < gridV.RowCount; i++)
                        //{
                        //    if (this.gridV.GetRowCellValue(i, "[X]").ToString().Equals("True"))
                        //    {
                        //        XtraMessageBox.Show("i= "+i,
                        //            Resources.NomApplication,
                        //              MessageBoxButtons.OK,
                        //              MessageBoxIcon.Information,
                        //              MessageBoxDefaultButton.Button1);
                        //    }
                        //}
                        foreach (CircuitPointCollecte circuitPointCollecte in circuit.circuitPointCollecteCollection)
                        {
                            Decimal lat = circuitPointCollecte.Latitude;
                            Decimal lng = circuitPointCollecte.Longitude;
                           // int color =  int.Parse(this.colorPick.EditValue.ToString());
                            GMapMarker marker = this.Createmarker(circuitPointCollecte.Lib_PC, lat, lng, circuitPointCollecte.Code_PC, Color.FromArgb(circuit.Couleur));
                            if (marker == null) continue;
                            markers.Markers.Add(marker);
                            gmap.UpdateMarkerLocalPosition(marker);
                            markerlist.Add(marker);
                            

                        }
                        this.gmap.ZoomAndCenterMarkers(markers.Id);
                    }
                }
                catch (Exception)
                {
                    markers.Markers.Remove(markerClient);

                    XtraMessageBox.Show("Problème de géolocalisation ! Client sans Coordonnées Géographiques",
                                         Resources.NomApplication,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    this.gridV.FocusedColumn = this.gridV.Columns["C. Point"];
                    listGeo.Clear();
                    return;
                }

            }

        }

        public void Enregistrer(bool enregistrerEtFermer)
        {
            int ordremax = 0;
            if (!this.dxValidationProvider1.Validate())
                return;
            if (gridV.RowCount == 0)
            {
                XtraMessageBox.Show("Veuillez entrer les détails. ",
                                   Resources.NomApplication,
                                     MessageBoxButtons.OK,
                                     MessageBoxIcon.Information,
                                     MessageBoxDefaultButton.Button1);
                return;
            }

            if ((string.IsNullOrEmpty(this.txtCCircuit.Text)))
            {
                XtraMessageBox.Show("Veuillez entrer le code Circuit. ",
                                   Resources.NomApplication,
                                     MessageBoxButtons.OK,
                                     MessageBoxIcon.Information,
                                     MessageBoxDefaultButton.Button1);
                return;
            }
            for (int i = 0; i < gridV.RowCount; i++)
            {
                if (this.gridV.GetRowCellValue(i, "[X]").ToString().Equals("True"))
                {
                    ordremax = ordremax + 1;
                }
            }
            if (ordremax == 0)
            {
                DialogResult dialogResult = XtraMessageBox.Show("Aucune Point de Collecte sélectionné !",
                                                       Resources.NomApplication,
                                                       MessageBoxButtons.OK,
                                                       MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                if (dialogResult == DialogResult.OK)
                    return;
            }
            try
            {
                Circuit circuit = new Circuit();

                circuit.Code = txtCCircuit.Text;
                circuit.Libelle = txtLibelle.Text;
                circuit.Code_Circuit = txtCCircuit.Text;
                circuit.Lib_Circuit = txtLibelle.Text;
                circuit.PC_Depart_Circuit = this.lkpPointDepart.EditValue.ToString();
                circuit.PC_Fin_Circuit = this.lkpPointFin.EditValue.ToString();
                circuit.Km_Circuit = this.txtKm.Value;
                circuit.Duree_Circuit = (int)this.txtDuree.Value;
                circuit.Couleur = colorPick.Color.ToArgb();
                circuit.DateInsertion = DateTime.Now;
                circuit.DateModification = DateTime.Now;
                circuit.PCInsertion = Environment.UserName;
                circuit.PCModification = Environment.UserName;
                circuit.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                circuit.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;

                for (int i = 0; i < gridV.RowCount; i++)
                {
                    if (this.gridV.GetRowCellValue(i, "[X]").ToString().Equals("True"))
                    {
                        CircuitPointCollecte circuitPointCollecte = new CircuitPointCollecte();
                        circuitPointCollecte.Code_PC = gridV.GetRowCellValue(i, "C. Point").ToString();
                        circuitPointCollecte.Lib_PC = gridV.GetRowCellValue(i, "Libelle").ToString();
                        circuitPointCollecte.Code_Circuit = this.txtCCircuit.Text;
                        circuitPointCollecte.LibCircuit = this.txtLibelle.Text;
                        circuit.circuitPointCollecteCollection.Add(circuitPointCollecte);
                    }
                }

                circuit.Sauvegarder();

                if (enregistrerEtFermer)
                {
                    this.Close();
                }
                else
                {
                    XtraMessageBox.Show(" Enregistrement Avec Succès. ",
                                           Resources.NomApplication,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Information,
                                             MessageBoxDefaultButton.Button1);

                    Actualiser();
                }
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Échec de l'enregistrement. ",
                                          Resources.NomApplication,
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information,
                                            MessageBoxDefaultButton.Button1);
            }
        }

        public void Actualiser()
        {
            markers.Markers.Clear();
            routes.Routes.Clear();
            markerlist.Clear();
            this.txtCCircuit.Text = string.Empty;
            this.txtLibelle.Text = string.Empty;
            this.txtKm.Value = 0;
            this.txtDuree.Value = 0;
            this.lkpPointDepart.EditValue = null;
            this.lkpPointFin.EditValue = null;
            this.colorPick.EditValue = null;
            LoadData();

        }

        List<GMapMarker> markerlist = new List<GMapMarker>();
        private void gridV_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Caption != "[X]")
                return;

            if (ChkGPS.Checked)
            {
                try
                {
                    string CPoint = this.gridV.GetRowCellDisplayText(e.RowHandle, "C. Point");

                    if ((bool)e.Value)
                    {
                        PointCollecte point = PointCollecte.Charger(CPoint);

                        Decimal lat = point.Latt_PC;
                        Decimal lng = point.Long_PC;

                        GMapMarker marker = null;

                        int color = colorPick.Color.ToArgb();
                        marker = this.Createmarker(point.Lib_PC, lat, lng, CPoint, Color.FromArgb(color));

                        markers.Markers.Add(marker);
                        gmap.UpdateMarkerLocalPosition(marker);
                        markerlist.Add(marker);
                        this.gmap.ZoomAndCenterMarkers(markers.Id);
                        //  gmap.
                        //ajout de overlay à la map
                        //gmap.Overlays.Add(overlayOne);
                    }
                    else if (!(bool)e.Value)
                    {
                        List<GMapMarker> l = markerlist.FindAll(x => x.Tag.Equals(CPoint));
                        foreach (GMapMarker m in l)
                        {
                            if (m.Tag.Equals(CPoint))
                            {
                                markers.Markers.Remove(m);
                                markerlist.Remove(m);
                                this.gmap.ZoomAndCenterMarkers(markers.Id);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    XtraMessageBox.Show("Problème de géolocalisation ! Client sans Coordonnées Géographiques",
                                      Resources.NomApplication,
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    this.gridV.FocusedColumn = this.gridV.Columns["C. Point"];
                    return;
                }
            }
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
            this.gmap.ZoomAndCenterMarkers(markers.Id);
            gmap.Refresh();
        }

        private void ChkGPS_CheckedChanged(object sender, EventArgs e)
        {
            markers.Markers.Clear();
            routes.Routes.Clear();
            markerlist.Clear();

            if (ChkGPS.Checked)
            {
                for (int i = 0; i < gridV.RowCount; i++)
                {
                    if (this.gridV.GetRowCellValue(i, "[X]").ToString().Equals("True"))
                    {
                        string CPoint = gridV.GetRowCellValue(i, "C. Point").ToString();

                        PointCollecte point = PointCollecte.Charger(CPoint);

                        Decimal lat = point.Latt_PC;
                        Decimal lng = point.Long_PC;

                        GMapMarker marker = null;
                        //int color = int.Parse(this.colorPick.EditValue.ToString());
                        marker = this.Createmarker(point.Lib_PC, lat, lng, CPoint, Color.FromArgb(colorPick.Color.ToArgb()));

                        markers.Markers.Add(marker);
                        //markers.Markers.Remove(marker);
                        gmap.UpdateMarkerLocalPosition(marker);
                        markerlist.Add(marker);
                    }
                }
            }
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

        private void chkallcheck_CheckedChanged(object sender, EventArgs e)
        {
            //this.gridV.Columns["[X]"].SortOrder = DevExpress.Data.ColumnSortOrder.None;
            this.gridV.BeginSort();
            CheckEdit ce = sender as CheckEdit;
            for (int i = 0; i < gridV.RowCount; i++)
            {
                this.gridV.SetRowCellValue(i, "[X]", ce.Checked);
                DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs evnt = new DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs(i, this.gridV.Columns["[X]"], ce.Checked);
                this.gridV_CellValueChanging(null, evnt);          
            }
            this.gridV.EndDataUpdate();
        }

        public void Ajouter()
        {
            FrmCircuitAjouter frm = new FrmCircuitAjouter(this.txtCCircuit.Text) { Text = Resources.Titre_FrmCircuit };
            ((FrmMDI)this.MdiParent).LoadForm(frm);
        }

        double z = 7;
        private void gmap_OnMapZoomChanged()
        {
            this.gmap.OnMapZoomChanged -= gmap_OnMapZoomChanged;
            double nz = this.gmap.Zoom;
            this.gmap.Zoom = z;
            Console.WriteLine(this.gmap.Zoom);
            Timer t = new Timer();
            t.Interval = 1000;
            t.Tick += (s, e) =>
            {
                if (this.gmap.Zoom == nz)
                {
                    t.Stop();
                    this.gmap.OnMapZoomChanged += gmap_OnMapZoomChanged;
                    z = this.gmap.Zoom;
                }
                if (this.gmap.Zoom > nz)
                    this.gmap.Zoom = this.gmap.Zoom + 0.01;
                if (this.gmap.Zoom < nz)
                    this.gmap.Zoom = this.gmap.Zoom - 0.01;
            };
            t.Start();
        }
    }
}