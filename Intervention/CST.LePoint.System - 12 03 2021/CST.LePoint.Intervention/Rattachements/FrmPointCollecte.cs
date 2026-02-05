

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
using CST.LePoint.Tiers.Referentiel;

using System;
using CST.LePoint.CtrlLibrary;
using CST.LePoint.CtrlLibrary.DevExpressEx;
using CST.LePoint.Securite.DataAccess;
using CST.LePoint.Securite.Management;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET;
using GMap.NET.MapProviders;
using System.Net;

namespace CST.LePoint.Intervention.Rattachements
{
    public partial class FrmPointCollecte : DevExpress.XtraEditors.XtraForm, IActionsSave
    {
        private string _CodeClient = string.Empty;
        private string nomColonneModifie = string.Empty;
        double lat, lng;
        bool gps = false;

        GMapOverlay routes = new GMapOverlay("routes");
        private GMapOverlay overlayOne = new GMapOverlay("OverlayOne");
        private GMap.NET.WindowsForms.GMapOverlay markers = new GMap.NET.WindowsForms.GMapOverlay("markers");

        public FrmPointCollecte()
        {
            InitializeComponent();
        }

        public FrmPointCollecte(string cClient)
        {
            InitializeComponent();
            this._CodeClient = cClient;
        }




        public void ChargerEntite(string Code_pc)
        {
            CtrlHelper.EmptyControls(this);
            PointCollecte Pc = new PointCollecte();

            //IContexteSecurite cs = GestionContexteSecurite.ContexteActive;
            //bool userNameFound = cs.Set<Bien>().Any(u => u.CImmeuble.Trim().ToUpper() == textBien.Text.Trim().ToUpper());
            //if (!userNameFound)




            if ((Code_pc == null) || (Code_pc == ""))
                return;

            Pc = PointCollecte.Charger(Code_pc);
            textBien.Enabled = false;
            this.textBien.Text = Pc.Code_PC;
            textLibBien.Text = Pc.Lib_PC;
            lkpGouv.EditValue = Pc.Code_Gouv_PC;
            lkpRg.EditValue = Pc.Code_Region_PC;

            markers.Clear();
            if (Pc.Latt_PC != 0)
            {
                marker = Createmarker(Pc.Code_PC, Pc.Latt_PC, Pc.Long_PC);
                markers.Markers.Add(marker);
                gmap.UpdateMarkerLocalPosition(marker);
                gmap.Zoom = 16;
                gmap.Position = new PointLatLng(Convert.ToDouble(Pc.Latt_PC), Convert.ToDouble(Pc.Long_PC));
            }
        }


        private void frmPointCollecte_Load(object sender, EventArgs e)
        {
            CtrlHelper.ValidationProviderDeclare(dxValidationProvider1, this);
            //LoadData();
            //ChargerEntite(_CodeClient);
            CtrlHelper.FillLookUpEdit(this.lkpGouv, GouvernoratCollection.Charger());
            CtrlHelper.FillLookUpEdit(this.lkpRg, RegionCollection.Charger());
            loadMap();
            Actualiser();
            
           // ChargerEntite(_CodeClient);
           
        }

        public void Enregistrer(bool enregistrerEtFermer)
        {
            if (ValidateChildren())
            {
                this.textBien.EditValue = textBien.Text.Trim();
                this.textLibBien.EditValue = textLibBien.Text.Trim();


                if (string.IsNullOrWhiteSpace(textBien.Text))
                {
                    MessageBox.Show("Enter Bien code !!!");
                    textBien.Select();
                    return;
                }
                else if (string.IsNullOrWhiteSpace(textLibBien.Text))
                {
                    MessageBox.Show("Enter Libelle Bien !!!");
                    textLibBien.Select();
                    return;
                }
                PointCollecte pc = new PointCollecte();
                pc = PointCollecte.Charger(this.textBien.Text);
                if (pc == null)
                {
                    pc = new PointCollecte();


                }
                else
                {
                    DialogResult dr = XtraMessageBox.Show(Resources.InfoMsg_MAJEnregistrement, Resources.NomApplication,
                                                   MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                    if (dr != DialogResult.Yes)
                        return;
                }
                //Bien Bien = new Bien();
                pc.Code_PC = this.textBien.Text;
                pc.Lib_PC = textLibBien.Text;
                //pc.Code_Gouv_PC = textAdresseBien.Text;
                pc.Code_Gouv_PC = lkpGouv.EditValue != null ? lkpGouv.EditValue.ToString() : null;
                pc.Code_Region_PC = lkpRg.EditValue != null ? lkpRg.EditValue.ToString() : null ;
                pc.Latt_PC = Convert.ToDecimal(lat);
                pc.Long_PC = Convert.ToDecimal(lng);
                pc.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                pc.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                pc.PCInsertion = Environment.UserName;
                pc.PCModification = Environment.UserName;

                pc.Sauvegarder();
                textBien.Text = "";
                textLibBien.Text = "";
                lkpRg.EditValue = "";
                lkpGouv.EditValue = "";


                Actualiser();

                if (enregistrerEtFermer)
                    Close();
            }
        }



        public void Actualiser()
        {
            CtrlHelper.EmptyControls(this);
            markers.Clear();
            if (!string.IsNullOrEmpty(_CodeClient))

                ChargerEntite(_CodeClient);
            else
                CtrlHelper.EmptyControls(this);
        }



        private void frmShiftListe_Activated(object sender, EventArgs e)
        {
            Actualiser();
        }





        public enum FlagSecurite
        {
            ModifDisabled
        }




        private void warning(string msg)
        {
            XtraMessageBox.Show(msg,
                                         "Alerte",
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Warning,
                                         MessageBoxDefaultButton.Button1);
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
            gmap.Zoom = 6;
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
            this.textLat.Text = lat.ToString("####.######");
            this.textLong.Text = lng.ToString("####.######");
            return marker;
        }


        GMapMarker marker;
        private void map_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && !this.gmap.IsDragging)
            {
                
                markers.Clear();
                lat = gmap.FromLocalToLatLng(e.X, e.Y).Lat;
                lng = gmap.FromLocalToLatLng(e.X, e.Y).Lng;
                marker = new GMarkerGoogle(new PointLatLng(lat, lng), GMarkerGoogleType.red);
                //marker.ToolTipText = textBien.Text;
                marker.ToolTipText = ((textBien.Text == null) || (textBien.Text == "")) ? "New Marker" : textBien.Text;
                marker.ToolTip.Fill = Brushes.Black;
                marker.ToolTip.Foreground = Brushes.LightGray;
                marker.ToolTip.Stroke = Pens.Gray;
                marker.ToolTip.Format.Alignment = StringAlignment.Near;
                marker.ToolTip.Format.LineAlignment = StringAlignment.Center;
                marker.ToolTipMode = MarkerTooltipMode.Always;
                markers.Markers.Add(marker);
                gmap.UpdateMarkerLocalPosition(marker);
                //this.textLong.Text = ((lat.ToString("####.######")) + " , " + lng.ToString("####.######"));
                this.textLat.Text = lat.ToString("####.######");
                this.textLong.Text = lng.ToString("####.######");
            }
        }

        private void textBien_EditValueChanged(object sender, EventArgs e)
        {
            if (_CodeClient == null || _CodeClient == "" && marker != null)
            {
                markers.Clear();
                marker.ToolTipText = ((textBien.Text == null) || (textBien.Text == "")) ? "New Marker" : textBien.Text;
                markers.Markers.Add(marker);

            }
        }


        private void textLong_EditValueChanged(object sender, EventArgs e)
        {
            markers.Clear();
            if (this.textLong.Text != null)
            {
                Decimal x = Decimal.Parse(this.textLat.Text.ToString()) ;
                Decimal y = Decimal.Parse(this.textLong.Text.ToString());
                Createmarker(this.textBien.Text, x, y);
                markers.Markers.Add(marker);
            }
        }


    }



}


