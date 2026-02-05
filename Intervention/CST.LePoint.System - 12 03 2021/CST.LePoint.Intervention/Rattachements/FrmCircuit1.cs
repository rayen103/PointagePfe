using CST.LePoint.CtrlLibrary;
using CST.LePoint.Intervention.Metier;
using CST.LePoint.Intervention.Properties;
using CST.LePoint.Securite;
using CST.LePoint.Securite.Entites;
using CST.LePoint.Tiers.Metier;
using CST.LePoint.Vente.Metier;
using DevExpress.Utils;
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
    public partial class FrmCircuit : DevExpress.XtraEditors.XtraForm, IActionsSave, IActionsRechercher, IActionsAjout
    {
        List<LatLng> listGeo = new List<LatLng>();
        private bool bInit = true;
        private bool check = false;
        private bool bRepArt = false;
        private string nomColonneModifie = string.Empty;
        bool Continuer = false;
        private bool bRowValide = false;
        public bool VerDetail = false;
        private string _circuit = string.Empty;
        private int compteur = 0;
        private int k = 0;
        //map
        private int compteurClient = 0;
        string[] arrayClient = new string[20];
        double[] arrayStartlat = new double[20]; double[] arrayStartlong = new double[20];
        double[] arrayEndlat = new double[20]; double[] arrayEndlong = new double[20];
        private int CompteurMarker;
        private int CompteurRoute = 1;
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
            //proprietes.Add(new GvColumnPropriete("N° Convention"));
            proprietes.Add(new GvColumnPropriete("C. Client"));
            proprietes.Add(new GvColumnPropriete("Raison Sociale"));
            proprietes.Add(new GvColumnPropriete("Chiffre d'affaire n-1"));
            proprietes.Add(new GvColumnPropriete("Chiffre d'affaire n"));
            proprietes.Add(new GvColumnPropriete("Famille client"));
            //proprietes.Add(new GvColumnPropriete("C. Établissement", GvColumnPropriete.GvColumnEtat.Invisible));
            //proprietes.Add(new GvColumnPropriete("Établissement"));
            proprietes.Add(new GvColumnPropriete("Ordre", GvColumnPropriete.GvColumnType.Integer, GvColumnPropriete.GvColumnEtat.Enable));
            //proprietes.Add(new GvColumnPropriete("Nb. Passage", GvColumnPropriete.GvColumnType.Integer, GvColumnPropriete.GvColumnEtat.Invisible));
            proprietes.Add(new GvColumnPropriete("Region"));
            proprietes.Add(new GvColumnPropriete("Gouvernorat"));
            proprietes.Add(new GvColumnPropriete("GPS", GvColumnPropriete.GvColumnType.Boolean));
            proprietes.Add(new GvColumnPropriete("Eliminé", GvColumnPropriete.GvColumnType.Boolean, GvColumnPropriete.GvColumnEtat.Invisible));

            //proprietes.Add(new GvColumnPropriete("TTraj"));
            //proprietes.Add(new GvColumnPropriete("DTraj", GvColumnPropriete.GvColumnType.Decimal));
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
                    cmd.CommandText = "CircuitClient_Vue_Rechercher";
                    cmd.Parameters.AddWithValue("@CAMin", this.CAMin.Value <= 0 ? 0 : this.CAMin.Value);
                    cmd.Parameters.AddWithValue("@CAMax", this.CAMax.Value <= 0 ? 0 : this.CAMax.Value);
                    cmd.Parameters.AddWithValue("@CCircuit", string.IsNullOrEmpty(this._circuit) ? null : this._circuit );

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dtListe);
                }

                CtrlHelper.FillGridView(gridV, Titres(), dtListe);
                gridV.OptionsView.ShowFooter = true;
                gridV.Columns["Chiffre d'affaire n-1"].Summary.Clear();
                gridV.Columns["Chiffre d'affaire n"].Summary.Clear();
                gridV.Columns["Chiffre d'affaire n-1"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Chiffre d'affaire n-1", "{0:c3}");
                gridV.Columns["Chiffre d'affaire n"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Chiffre d'affaire n", "{0:c3}");
                //this.gridV.Columns["Region"].OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
                //this.gridV.Columns["Gouvernorat"].OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
                //this.gridV.Columns["Famille client"].OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
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
            if (this.CAMin.Value > this.CAMax.Value) {
                XtraMessageBox.Show("Veuillez vérifier la valeur de chiffre d'affaire maximum. ",
                                   Resources.NomApplication,
                                     MessageBoxButtons.OK,
                                     MessageBoxIcon.Information,
                                     MessageBoxDefaultButton.Button1);
                return;
            }
            CompteurRoute = 0;
            compteur = 0;
            k = 0;
            bInit = true;
            markers.Markers.Clear();
            routes.Routes.Clear();
            markerlist.Clear();
            this.RemplirGridVCircuit();
        }

        private GMapMarker Createmarker(string text, Decimal lat, Decimal lng, string CClient)
        {
            GMapMarker marker = new GMarkerGoogle(new PointLatLng(Convert.ToDouble(lat), Convert.ToDouble(lng)), GMarkerGoogleType.red);
            marker.ToolTipText = text;
            marker.ToolTip.Fill = Brushes.Black;
            marker.ToolTip.Foreground = Brushes.LightGray;
            marker.ToolTip.Stroke = Pens.Gray;
            marker.ToolTip.Format.Alignment = StringAlignment.Near;
            marker.ToolTip.Format.LineAlignment = StringAlignment.Center;
            marker.ToolTipMode = MarkerTooltipMode.Always;
            marker.Tag = CClient;
            return marker;
        }

        /*OLD VERSION private GMapMarker Createmarker(string text, Decimal lat, Decimal lng)
        {
            markerClient = new GMarkerGoogle(new PointLatLng(Convert.ToDouble(lat), Convert.ToDouble(lng)), GMarkerGoogleType.red);
            markerClient.ToolTipText = text;
            markerClient.ToolTip.Fill = Brushes.Black;
            markerClient.ToolTip.Foreground = Brushes.LightGray;
            markerClient.ToolTip.Stroke = Pens.Gray;
            markerClient.ToolTip.Format.Alignment = StringAlignment.Near;
            markerClient.ToolTip.Format.LineAlignment = StringAlignment.Center;
            markerClient.ToolTipMode = MarkerTooltipMode.Always;
            return markerClient;
        }*/

        public void LoadData()
        {
            CtrlHelper.InitValidationProvider(this.dxValidationProvider1, this);
            CtrlHelper.FillLookUpEdit(this.lkpResponsable, EquipeCollection.Charger());
            CtrlHelper.InitGridView(gridV, Titres());
            RemplirGridVCircuit();
            if (!(string.IsNullOrEmpty(this._circuit)))
            {
                this.txtCCircuit.Enabled = false;
                Circuit circuit = Circuit.ChargerEquipe(this._circuit);
                this.txtCCircuit.Text = circuit.Code;
                this.txtLibelle.Text = circuit.Libelle;
                this.lkpResponsable.EditValue = circuit.CEquipe;
                this.chkTablette.Checked = circuit.BTablette;
                //foreach (CircuitDetail detail in circuit.circuitDetailcoll)
                //{
                //    for (int i = 0; i < gridV.RowCount; i++)
                //    {
                //        if (this.gridV.GetRowCellValue(i, "C. Client").ToString().Equals(detail.CClient))
                //        {
                //            this.gridV.SetRowCellValue(i, "[X]", true);
                //            this.gridV.SetRowCellValue(i, "Ordre", detail.Ordre);
                //            //this.gridV.SetRowCellValue(i, "TTraj", detail.TTraj);
                //            //this.gridV.SetRowCellValue(i, "DTraj", detail.DTraj);

                //        }
                //    }
                //}
                this.gridV.Columns["[X]"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
                if(this.gridV.RowCount >0)
                    this.gridV.FocusedRowHandle = 0;
                try
                {
                    if (ChkGPS.Checked)
                    {
                        markers.Markers.Clear();
                        routes.Routes.Clear();
                        for (int i = 0; i < gridV.RowCount; i++)
                        {
                            if ((this.gridV.GetRowCellValue(i, "[X]").ToString().Equals("True")))
                            {
                                string client = this.gridV.GetRowCellDisplayText(i, "C. Client");
                                //string CEtablissement = this.gridV.GetRowCellDisplayText(i, "C. Établissement");
                                int ordre = int.Parse(this.gridV.GetRowCellDisplayText(i, "Ordre"));
                                Client cl = Client.Charger(client);
                                string tooltiptext = cl.RaisonSociale;
                                Decimal lat = cl.Latitude;
                                Decimal lng = cl.Longitude;
                                //if (cl.BEtablissement)
                                //{
                                //    Etablissement Et = Etablissement.Charger(CEtablissement);
                                //    tooltiptext = Et.Libelle;
                                //    lat = Et.Latitude;
                                //    lng = Et.Longitude;
                                //}
                                int k = i + 1;
                                if (!this.ChkOrdre.Checked)
                                {
                                    this.Createmarker("\n(" + ordre + ") " + tooltiptext + " ", lat, lng, client);
                                }
                                if (this.ChkOrdre.Checked)
                                {
                                    this.Createmarker("\n(" + ordre + ") ", lat, lng, client);
                                }

                                markers.Markers.Add(markerClient);
                                gmap.UpdateMarkerLocalPosition(markerClient);
                                
                                LatLng objGeo = new LatLng()
                                {
                                    lat = Convert.ToDouble(lat),
                                    lng = Convert.ToDouble(lng),
                                    Ordre = ordre
                                };

                                listGeo.Add(objGeo);
                            }
                        }


                        listGeo = listGeo.OrderBy(x => x.Ordre).ToList();
                        var xxv = listGeo;
                        while (listGeo.Count != 1)
                        {
                            arrayStartlat[compteur] = Convert.ToDouble(listGeo.ElementAt(0).lat);
                            arrayStartlong[compteur] = Convert.ToDouble(listGeo.ElementAt(0).lng);
                            arrayEndlat[compteur] = Convert.ToDouble(listGeo.ElementAt(1).lat);
                            arrayEndlong[compteur] = Convert.ToDouble(listGeo.ElementAt(1).lng);
                            arrayStartlat[compteur + 1] = Convert.ToDouble(listGeo.ElementAt(1).lat);
                            arrayStartlong[compteur + 1] = Convert.ToDouble(listGeo.ElementAt(1).lng);
                            PointLatLng start = new PointLatLng(Convert.ToDouble(listGeo.ElementAt(0).lat), Convert.ToDouble(listGeo.ElementAt(0).lng));
                            PointLatLng end = new PointLatLng(Convert.ToDouble(listGeo.ElementAt(1).lat), Convert.ToDouble(listGeo.ElementAt(1).lng));
                            GDirections gdirection;
                            //var xx = GMapProviders.GoogleMap.GetDirections(out gdirection, start, end, false, false, false, false, false);
                            //MapRoute route = GoogleMapProvider.Instance.GetRoute(start, end, false, false, 7);

                            DirectionsStatusCode status = GoogleMapProvider.Instance.GetDirections(out gdirection, start, end, false, false, false, false, false);
                            if (status == DirectionsStatusCode.OK)
                            {
                                //markers.Markers.Remove(markerClient);
                                //throw new Exception("Problème de géolocalisation!");

                                GMapRoute r = new GMapRoute(gdirection.Route, "My route");
                                r.Stroke.Width = 4;
                                r.Stroke.Color = Color.FromArgb(170, Color.DodgerBlue);
                                routes.Routes.Add(r);
                            }
                            listGeo.Remove(listGeo[0]);
                            compteur++;
                        }
                        bInit = false;
                        listGeo.Clear();
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

        }

        #region tools

        private void Calcultotaux(DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            double dureeTraj = 0;
            double DistanceT = 0;


            for (int i = 0; i < gridV.RowCount; i++)
            {

                if ((this.gridV.GetRowCellValue(i, "[X]").ToString().Equals("True")) && (i != e.RowHandle))
                {
                    dureeTraj = dureeTraj + double.Parse(this.gridV.GetRowCellValue(i, "TTraj").ToString());
                    DistanceT = DistanceT + double.Parse(this.gridV.GetRowCellValue(i, "DTraj").ToString());

                }
                if (e.Value.ToString().Equals("True") && (i == e.RowHandle))
                {
                    dureeTraj = dureeTraj + double.Parse(this.gridV.GetRowCellValue(i, "TTraj").ToString());
                    DistanceT = DistanceT + double.Parse(this.gridV.GetRowCellValue(i, "DTraj").ToString());

                }

            }

        }

        private uint CalculDuration(PointLatLng start, PointLatLng end)
        {
            GMapProvider.WebProxy = System.Net.WebRequest.GetSystemWebProxy();
            GMapProvider.WebProxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
            //MapRoute route = GMap.NET.MapProviders.GoogleMapProvider.Instance.GetRoute(start, end, false, false, 0);
            GDirections ss;
            var xx = GMapProviders.GoogleMap.GetDirections(out ss, start, end, false, false, false, false, false);

            GMapRoute r = new GMapRoute(ss.Route, "My route");

            //  var x = ss.Steps[0].;

            return ss.DurationValue;

        }

        private uint CalculDistance(PointLatLng start, PointLatLng end)
        {
            GMapProvider.WebProxy = System.Net.WebRequest.GetSystemWebProxy();
            GMapProvider.WebProxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
            //MapRoute route = GMap.NET.MapProviders.GoogleMapProvider.Instance.GetRoute(start, end, false, false, 0);
            GDirections ss;
            var xx = GMapProviders.GoogleMap.GetDirections(out ss, start, end, false, false, false, false, false);

            GMapRoute r = new GMapRoute(ss.Route, "My route");

            //var x = ss.Steps;

            return (ss.DistanceValue / 1000);

        }

        #endregion

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
                DialogResult dialogResult = XtraMessageBox.Show("Aucun Client sélectionné!, vous devez sélectionner au moins un client.",
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
                circuit.DateInsertion = DateTime.Now;
                circuit.DateModification = DateTime.Now;
                circuit.PCInsertion = Environment.UserName;
                circuit.PCModification = Environment.UserName;
                circuit.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                circuit.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                circuit.BTablette = this.chkTablette.Checked;
                if(lkpResponsable.EditValue != null || (string)lkpResponsable.EditValue != "")
                    circuit.CEquipe = lkpResponsable.EditValue.ToString();
                for (int i = 0; i < gridV.RowCount; i++)
                {
                    if (this.gridV.GetRowCellValue(i, "[X]").ToString().Equals("True"))
                    {
                        CircuitPointCollecte circuitDetail = new CircuitPointCollecte();
                        Client client = Client.Charger(gridV.GetRowCellValue(i, "C. Client").ToString());
                        circuitDetail.Latitude = client.Latitude;
                        circuitDetail.Longitude = client.Longitude;
                        //if (client.BEtablissement)
                        //{
                        //    Etablissement Et = Etablissement.Charger(gridV.GetRowCellValue(i, "C. Établissement").ToString());
                        //    circuitDetail.Latitude = Et.Latitude;
                        //    circuitDetail.Longitude = Et.Longitude;
                        //    circuitDetail.CEtablissement = gridV.GetRowCellValue(i, "C. Établissement").ToString();
                        //}
                        //circuitDetail.NConvention = gridV.GetRowCellValue(i, "N° Convention").ToString();
                        circuitDetail.CClient = gridV.GetRowCellValue(i, "C. Client").ToString();
                        circuitDetail.Ordre = int.Parse(gridV.GetRowCellValue(i, "Ordre").ToString());
                        //circuitDetail.TTraj = gridV.GetRowCellValue(i, "TTraj").ToString();
                        //circuitDetail.DTraj = decimal.Parse(gridV.GetRowCellValue(i, "DTraj").ToString());
                        circuit.circuitPointCollecteCollection.Add(circuitDetail);
                    }
                }

                circuit.Sauvegarder();

                if (enregistrerEtFermer)
                {
                    this.Close();
                }
                else
                {
                    XtraMessageBox.Show(" Enregistrement Avec Succes. ",
                                           Resources.NomApplication,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Information,
                                             MessageBoxDefaultButton.Button1);

                    Actualiser();
                }
            }
            catch (Exception)
            {
                XtraMessageBox.Show(" échec de l'enregistrement. ",
                                          Resources.NomApplication,
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information,
                                            MessageBoxDefaultButton.Button1);
            }
        }

        public void Actualiser()
        {
            this.CAMin.Value = 0;
            this.CAMax.Value = 0;
            CompteurRoute = 0;
            compteur = 0;
            k = 0;
            bInit = true;
            //this.CAMin.Value = 0;
            //this.CAMax.Value = 0;
            this.txtCCircuit.Text = string.Empty;
            this.txtLibelle.Text = string.Empty;
            LoadData();
            //loadMap();
            markers.Markers.Clear();
            routes.Routes.Clear();
            markerlist.Clear();
        }

        public void SelectionnerGridRow(bool bHaut)
        {
            if (bHaut)
                this.gridV.FocusedRowHandle = this.gridV.FocusedRowHandle - 1;
            else
                this.gridV.FocusedRowHandle = this.gridV.FocusedRowHandle + 1;
        }

        List<GMapMarker> markerlist = new List<GMapMarker>();
        private void gridV_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Caption != "[X]")
                return;
            int ordre = 1;

            if (!(bool)e.Value)
                this.gridV.SetRowCellValue(e.RowHandle, "Ordre", 0);
            else
            {
                for (int i = 0; i < gridV.RowCount; i++)
                {
                    if (this.gridV.GetRowCellValue(i, "[X]").ToString().Equals("True"))
                    {
                        if (int.Parse(this.gridV.GetRowCellValue(i, "Ordre").ToString()) >= ordre)
                            ordre = int.Parse(this.gridV.GetRowCellValue(i, "Ordre").ToString()) + 1;
                    }
                }
                this.gridV.SetRowCellValue(e.RowHandle, "Ordre", ordre);
            }

            if (ChkGPS.Checked)
            {
                try
                {
                    string cclient = this.gridV.GetRowCellDisplayText(e.RowHandle, "C. Client");

                    if ((bool)e.Value)
                    {
                        //string CEtablissement = this.gridV.GetFocusedRowCellDisplayText("C. Établissement");
                        Client client = Client.Charger(cclient);
                        string tooltiptext = client.RaisonSociale;
                        Decimal lat = client.Latitude;
                        Decimal lng = client.Longitude;
                        //if (client.BEtablissement)
                        //{
                        //    Etablissement Et = Etablissement.Charger(CEtablissement);
                        //    tooltiptext = Et.Libelle;
                        //    lat = Et.Latitude;
                        //    lng = Et.Longitude;
                        //}
                        //k++;
                        //CompteurMarker = k;
                        //ajouter un marker dans la map
                        //k = comp + 1;
                        GMapMarker marker = null;
                        if (!this.ChkOrdre.Checked)
                        {
                            marker = this.Createmarker("(" + ordre + ") " + tooltiptext + " ", lat, lng, cclient);
                        }
                        if (this.ChkOrdre.Checked)
                        {
                            marker = this.Createmarker("(" + ordre + ") ", lat, lng, cclient);
                        }
                        markers.Markers.Add(marker);
                        gmap.UpdateMarkerLocalPosition(marker);
                        markerlist.Add(marker);
                        //ajout de overlay à la map
                        //gmap.Overlays.Add(overlayOne);
                    }
                    else if (!(bool)e.Value)
                    {
                        List<GMapMarker> l = markerlist.FindAll(x => x.Tag.Equals(cclient));
                        foreach (GMapMarker m in l)
                        {
                            if(m.Tag.Equals(cclient)){
                                markers.Markers.Remove(m);
                                markerlist.Remove(m);
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
                    this.gridV.FocusedColumn = this.gridV.Columns["C. Client"];
                    return;
                }
            }
        }

        /* OLD VERSION private void gridV_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Caption != "[X]")
                return;
            if (ChkGPS.Checked == false)
            {
                if (e.Value.ToString().Equals("True"))
                {

                    int comp = 0;
                    for (int i = 0; i < gridV.RowCount; i++)
                    {
                        if (this.gridV.GetRowCellValue(i, "[X]").ToString().Equals("True") && (i != e.RowHandle))
                        {
                            comp = comp + 1;
                        }
                    }
                    var c = comp;
                    this.gridV.SetFocusedRowCellValue("Ordre", comp + 1);
                }

                else if (e.Value.ToString().Equals("False"))
                {
                    if (e.Column.Caption != "[X]")
                        return;
                    int comp = 0;
                    for (int i = 0; i < gridV.RowCount; i++)
                    {
                        if (this.gridV.GetRowCellValue(i, "[X]").ToString().Equals("True") && (i != e.RowHandle))
                        {
                            comp = comp + 1;
                        }
                    }
                    int ordretest = int.Parse(this.gridV.GetFocusedRowCellDisplayText("Ordre"));
                    if (ordretest != comp + 1)
                    {
                        XtraMessageBox.Show("Veuillez décocher dans l'ordre !",
                                            Resources.NomApplication,
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                        this.gridV.FocusedColumn = this.gridV.Columns["C. Client"];
                        return;
                    }
                    this.gridV.SetFocusedRowCellValue("Ordre", 0);
                }
            }
            else if (ChkGPS.Checked == true)
            {
                try
                {
                    if (e.Value.ToString().Equals("True"))
                    {
                        int comp = 0;
                        for (int i = 0; i < gridV.RowCount; i++)
                        {
                            if (this.gridV.GetRowCellValue(i, "[X]").ToString().Equals("True") && (i != e.RowHandle))
                            {
                                comp = comp + 1;
                            }
                        }
                        var c = comp;
                        this.gridV.SetFocusedRowCellValue("Ordre", comp + 1);

                        string cclient = this.gridV.GetFocusedRowCellDisplayText("C. Client");
                        //string CEtablissement = this.gridV.GetFocusedRowCellDisplayText("C. Établissement");

                        Client client = Client.Charger(cclient);
                        string tooltiptext = client.RaisonSociale;
                        Decimal lat = client.Latitude;
                        Decimal lng = client.Longitude;
                        //if (client.BEtablissement)
                        //{
                        //    Etablissement Et = Etablissement.Charger(CEtablissement);
                        //    tooltiptext = Et.Libelle;
                        //    lat = Et.Latitude;
                        //    lng = Et.Longitude;
                        //}
                        //k++;
                        CompteurMarker = k;
                        //ajouter un marker dans la map
                        k = comp + 1;
                        if (!this.ChkOrdre.Checked)
                        {
                            this.Createmarker("(" + k + ") " + tooltiptext + " ", lat, lng);
                        }
                        if (this.ChkOrdre.Checked)
                        {
                            this.Createmarker("(" + k + ") ", lat, lng);
                        }
                        markers.Markers.Add(markerClient);
                        gmap.UpdateMarkerLocalPosition(markerClient);
                        //ajout de overlay à la map
                        //gmap.Overlays.Add(overlayOne);

                        //Dessiner la route
                        if (bInit)
                        {
                            arrayStartlat[compteur] = Convert.ToDouble(lat);
                            arrayStartlong[compteur] = Convert.ToDouble(lng);
                            bInit = false;
                            //compteur = compteur + 1;
                        }
                        else
                        {

                            PointLatLng start = new PointLatLng(arrayStartlat[compteur], arrayStartlong[compteur]);
                            arrayEndlat[compteur] = Convert.ToDouble(lat);
                            arrayEndlong[compteur] = Convert.ToDouble(lng);

                            PointLatLng end = new PointLatLng(arrayEndlat[compteur], arrayEndlong[compteur]);

                            GDirections gdirection;
                            //var xx = GMapProviders.GoogleMap.GetDirections(out gdirection, start, end, false, false, false, false, false);
                            //MapRoute route = GoogleMapProvider.Instance.GetRoute(start, end, false, false, 7);

                            DirectionsStatusCode status = GoogleMapProvider.Instance.GetDirections(out gdirection, start, end, false, false, false, false, false);
                            if (status == DirectionsStatusCode.OK)
                            {
                                //if status != DirectionsStatusCode.OK
                                //markers.Markers.Remove(markerClient);
                                //throw new Exception("Problème de géolocalisation!");

                                GMapRoute r = new GMapRoute(gdirection.Route, "My route");
                                r.Stroke.Width = 4;
                                r.Stroke.Color = Color.FromArgb(170, Color.DodgerBlue);
                                routes.Routes.Add(r);

                                arrayStartlat[compteur + 1] = arrayEndlat[compteur];
                                arrayStartlong[compteur + 1] = arrayEndlong[compteur];
                                compteur = compteur + 1;
                                ////var j = arrayStartlat;
                                ////var jj = arrayStartlong;
                                ////var h = arrayEndlat;
                                ////var hh = arrayEndlong;
                                ////var b = compteur;

                                CompteurRoute++;

                                //calcul du trajet en km et en temps

                                double ttraj = double.Parse((CalculDuration(start, end) / 60).ToString());
                                decimal Dtraj = CalculDistance(start, end);
                                this.gridV.SetFocusedRowCellValue("TTraj", ttraj);
                                this.gridV.SetFocusedRowCellValue("DTraj", Dtraj);

                                Calcultotaux(e);
                            }
                        }

                    }
                    else if (e.Value.ToString().Equals("False"))
                    {
                        //int calcul = 0;
                        if (e.Column.Caption != "[X]")
                            return;

                        int comp = 0;
                        for (int i = 0; i < gridV.RowCount; i++)
                        {
                            if (this.gridV.GetRowCellValue(i, "[X]").ToString().Equals("True") && (i != e.RowHandle))
                            {
                                comp = comp + 1;
                            }
                        }
                        int ordretest = int.Parse(this.gridV.GetFocusedRowCellDisplayText("Ordre"));
                        if (ordretest != comp + 1)
                        {
                            XtraMessageBox.Show("Veuillez décocher dans l'ordre !",
                                                Resources.NomApplication,
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                            this.gridV.FocusedColumn = this.gridV.Columns["C. Client"];
                            return;
                        }
                        if (string.IsNullOrEmpty(_circuit))
                            compteur = comp;

                        this.gridV.SetFocusedRowCellValue("Ordre", 0);
                        this.gridV.SetFocusedRowCellValue("TTraj", 0);
                        this.gridV.SetFocusedRowCellValue("DTraj", 0);
                        string cclient = this.gridV.GetFocusedRowCellDisplayText("C. Client");
                        Client client = Client.Charger(cclient);

                        var x = comp;
                        if (comp == 0)
                        {
                            bInit = true;
                            //k--;
                            markers.Markers.RemoveAt(0);
                            arrayStartlat[compteur] = 0;
                            arrayStartlong[compteur] = 0;
                        }

                        else
                        {
                            PointLatLng start = new PointLatLng(arrayStartlat[compteur - 1], arrayStartlong[compteur - 1]);
                            PointLatLng end = new PointLatLng(arrayEndlat[compteur - 1], arrayEndlong[compteur - 1]);

                            //enlever un marquage lorssque check = false
                            try
                            {
                                if (string.IsNullOrEmpty(_circuit))
                                {
                                    routes.Routes.RemoveAt(comp - 1);
                                    markers.Markers.RemoveAt(comp);
                                }
                                else
                                {
                                    routes.Routes.RemoveAt(compteur - 1);
                                    markers.Markers.RemoveAt(compteur);
                                }
                            }
                            catch { }
                            //CompteurMarker--;

                            arrayStartlat[compteur] = 0;
                            arrayStartlong[compteur] = 0;

                            //decrementer pour revenir à la situation initiale
                            compteur--;

                            //vider l'element avant dernier
                            arrayEndlat[compteur] = 0;
                            arrayEndlong[compteur] = 0;
                            k--;
                            CompteurRoute--;
                        }

                    }
                }
                catch (Exception)
                {
                    XtraMessageBox.Show("Problème de géolocalisation ! Client sans Coordonnées Géographiques",
                                      Resources.NomApplication,
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    this.gridV.FocusedColumn = this.gridV.Columns["C. Client"];
                    return;
                }
            }
        }
        */

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

        private void ChkGPS_CheckedChanged(object sender, EventArgs e)
        {
            //Actualiser();
            CompteurRoute = 0;
            compteur = 0;
            k = 0;
            bInit = true;
            markers.Markers.Clear();
            routes.Routes.Clear();
            markerlist.Clear();
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

        private void lkpResponsable_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Delete)
                this.lkpResponsable.EditValue = null;
        }

        public void Ajouter()
        {
            FrmCircuitAjouter frm = new FrmCircuitAjouter(this.txtCCircuit.Text) { Text = Resources.Titre_FrmCircuit };
            ((FrmMDI)this.MdiParent).LoadForm(frm);
        }

        private void gridV_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (view.IsFilterRow(e.RowHandle))            
                return;

            if ((bool)view.GetRowCellValue(e.RowHandle, "Eliminé")) 
            {
                e.Appearance.BackColor = Color.LightCoral;
                e.Appearance.ForeColor = Color.White;
            }
            
        }

        private void map_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && !this.gmap.IsDragging)
            {
                markers.Clear();
                double lat = gmap.FromLocalToLatLng(e.X, e.Y).Lat;
                double lng = gmap.FromLocalToLatLng(e.X, e.Y).Lng;
                GMapMarker marker = new GMarkerGoogle(new PointLatLng(lat, lng), GMarkerGoogleType.red);
                marker.ToolTipText = "New marker";
                marker.ToolTip.Fill = Brushes.Black;
                marker.ToolTip.Foreground = Brushes.LightGray;
                marker.ToolTip.Stroke = Pens.Gray;
                marker.ToolTip.Format.Alignment = StringAlignment.Near;
                marker.ToolTip.Format.LineAlignment = StringAlignment.Center;
                marker.ToolTipMode = MarkerTooltipMode.Always;
                markers.Markers.Add(marker);
                gmap.UpdateMarkerLocalPosition(marker);
            }
        }
    
    }
        //        public void Ajouter()
        //{
        //    FrmCircuitAjouter frm = new FrmCircuitAjouter(this.txtCCircuit.Text) { Text = Resources.Titre_FrmCircuit };
        //    ((FrmMDI)this.MdiParent).LoadForm(frm);
        //}