using CST.LePoint.Securite;
using CST.LePoint.Securite.Entites;
using CST.LePoint.Intervention.Properties;
using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;
using CST.LePoint.CtrlLibrary;
using CST.LePoint.Tiers.Referentiel;
using DevExpress.XtraGrid.Views.Base;
using CST.LePoint.Tiers.Metier;
using DevExpress.XtraGrid.Views.Grid;

namespace CST.LePoint.Intervention.DroitsAcces
{
    public partial class FrmSociete : XtraForm, IActionsSave
    {
        //private bool addMode;

        public FrmSociete()
        {
            InitializeComponent();
        }
        
        private GvColumnProprietes Titres()
        {
            GvColumnProprietes proprietes = new GvColumnProprietes();
            proprietes.Add(new GvColumnPropriete("RIB", GvColumnPropriete.GvColumnType.String, GvColumnPropriete.GvColumnEtat.Enable));
            proprietes.Add(new GvColumnPropriete("Banque", GvColumnPropriete.GvColumnType.LookUpVide, GvColumnPropriete.GvColumnEtat.Enable, BanqueCollection.ChargerTout()));
            proprietes.Add(new GvColumnPropriete("Agence", GvColumnPropriete.GvColumnType.LookUpVide, GvColumnPropriete.GvColumnEtat.Enable, AgenceCollection.Charger()));
            proprietes.Add(new GvColumnPropriete("Compte comptable", GvColumnPropriete.GvColumnType.String, GvColumnPropriete.GvColumnEtat.Enable));
            proprietes.Add(new GvColumnPropriete("N° journal", GvColumnPropriete.GvColumnType.String, GvColumnPropriete.GvColumnEtat.Enable));
            proprietes.Add(new GvColumnPropriete("RIB par défaut", GvColumnPropriete.GvColumnType.Boolean, GvColumnPropriete.GvColumnEtat.Enable));
            return proprietes;
        }

        public void Enregistrer(bool enregistrerEtFermer)
        {
            bool societefound = false;

            if (ValidateChildren())
            {
                txtCSociete.EditValue = txtCSociete.Text.Trim();
                txtNom.EditValue = txtNom.Text.Trim();

                //IContexteSecurite cs = GestionContexteSecurite.ContexteActive;

                //bool NomSocieteTrouve = cs.Set<Societe>().Any(u => u.CSociete == txtCSociete.Text);

                //if (societe.CSociete != txtCSociete.Text && NomSocieteTrouve)
                //{
                //    txtNom.ErrorText = "le nom de la société existe déjà!!"; //TODO To Ressource
                //    return;
                //}
                //else
                //    txtNom.ErrorText = null;

                DialogResult dr = XtraMessageBox.Show(Resources.InfoMsg_MAJEnregistrement, Resources.NomApplication,
                                                        MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                switch (dr)
                {
                    case DialogResult.Cancel:
                        return;

                    case DialogResult.No:
                        Close();
                        return;

                    case DialogResult.Yes:
                        societefound = true;
                        break;
                }

                DevExpress.Utils.WaitDialogForm waitForm = new DevExpress.Utils.WaitDialogForm("Chargement en cours...",
                          "Veuillez patienter !");
                try
                {

                    System.Drawing.ImageConverter _imageConverter = new System.Drawing.ImageConverter();
                    Societe societe = new Societe();
                    societe.CSociete = txtCSociete.Text;
                    societe.Nom = txtNom.Text;
                    societe.Adresse = txtAdresse.Text;
                    societe.CodePostal = txtCPostal.Text;
                    societe.CTVA = txtCodeTVA.Text;
                    societe.Email = txtEmail.Text;
                    societe.Fax = txtFax.Text;
                    societe.Pays = txtPays.Text;
                    societe.RaisonSociale = txtInitiales.Text;
                    societe.RegistreCommerce = txtRegistreCommerce.Text;
                    societe.Telephone = txtNTel.Text;
                    societe.Ville = txtVille.Text;
                    societe.DateOuverture = DateTime.Parse(txtDateOuverture.Text);
                    societe.DateModification = DateTime.Now;
                    societe.BAssujetti = this.chkBAssujetti.Checked;
                    societe.PCModification = Environment.UserName;
                    societe.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                    societe.Logo = (byte[])_imageConverter.ConvertTo(PELogo.Image, typeof(byte[]));
                    societe.Ip = this.txtIPAdresse.Text;
                    societe.Port = (int)this.txtPort.Value;
                    societe.GMTPlus = (int)this.txtGMTPlus.Value;
                    societe.Latitude = this.txtLatitude.Value;
                    societe.Longitude = this.txtLongitude.Value;
                    societe.Rayon = (int)this.txtRayon.Value;
                    societe.Time = (int)this.txtTime.Value;

                    //for (int i = 0; i < this.gridView1.RowCount; i++)
                    //{
                    //    SocieteBanque banque = new SocieteBanque();
                    //    banque.CSociete = societe.CSociete;
                    //    banque.Agence = this.gridView1.GetRowCellValue(i, "Agence").ToString();
                    //    banque.CBanque = this.gridView1.GetRowCellValue(i, "Banque").ToString();
                    //    banque.RIB = this.gridView1.GetRowCellDisplayText(i, "RIB");
                    //    banque.BParDefautRib = false;
                    //    bool res = false;
                    //    if (bool.TryParse(this.gridView1.GetRowCellValue(i, "RIB par défaut").ToString(), out res))
                    //    {
                    //        banque.BParDefautRib = res;
                    //    }

                    //    banque.CompteComptable = this.gridView1.GetRowCellDisplayText(i, "Compte comptable");
                    //    banque.NumeroJournal = this.gridView1.GetRowCellDisplayText(i, "N° journal");
                    //    banque.DateInsertion = DateTime.Now;
                    //    banque.DateModification = DateTime.Now;
                    //    banque.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                    //    banque.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                    //    banque.PCInsertion = Environment.UserName;
                    //    banque.PCModification = Environment.UserName;
                    //    societe.SocieteBanques.Add(banque);
                    //}
                    for (int i = 0; i < this.gridVSite.RowCount; i++)
                    {
                        SocieteSite site = new SocieteSite();
                        site.bSiege = bool.Parse(gridVSite.GetRowCellValue(i, "Siege").ToString());
                        site.CSite = gridVSite.GetRowCellValue(i, "Code").ToString();
                        site.CSociete = societe.CSociete;
                        site.Site = gridVSite.GetRowCellValue(i, "Site").ToString();
                        site.Latitude = (decimal)gridVSite.GetRowCellValue(i, "Latitude");
                        site.Longitude = (decimal)gridVSite.GetRowCellValue(i, "Longitude");
                        site.Rayon = (int)gridVSite.GetRowCellValue(i, "Rayon");
                        site.DateInsertion = DateTime.Now;
                        site.DateModification = DateTime.Now;
                        site.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                        site.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                        site.PCInsertion = Environment.UserName;
                        site.PCModification = Environment.UserName;
                        societe.SocieteSites.Add(site);
                    }
                    societe.Sauvegarder();

                    //if (cs.Set<Societe>() != null)
                    //{
                    //    if (cs.Set<Societe>().Count > 0)
                    //        cs.Set<Societe>().Clear();
                    //    cs.Set<Societe>().Add(societe);
                    //}
                    //else
                    //{
                    //    cs.Charger();
                    //}

                    //cs.Enregistrer();

                    GestionSession.SocieteCourante = societe;

                    Text = "Société: " + societe.CSociete;
                    txtCSociete.EditValue = societe.CSociete;
                    ((FrmMDI)MdiParent).ConfigurerMenu();
                    if (waitForm != null)
                        waitForm.Close();
                    if (enregistrerEtFermer)
                        this.Close();
                    else
                    {
                        XtraMessageBox.Show("Société enregistré. ",
                                             Resources.NomApplication,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Information,
                                             MessageBoxDefaultButton.Button1);
                        Actualiser();
                    }
                }
                catch (Exception ex)
                {
                    if (waitForm != null)
                        waitForm.Close();
                    XtraMessageBox.Show("Échec d'enregistrement. \n\n" + ex.Message,
                          Resources.NomApplication,
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error);
                }
                finally
                {
                    if (waitForm != null)
                        waitForm.Dispose();
                }
            }
        }

        public void Actualiser()
        {
           // CtrlHelper.InitGridView(this.gridView1, Titres(), true);
            CtrlHelper.InitGridView(this.gridVSite, TitresSite(), true);
            Societe societe = Societe.Charger(GestionSession.SocieteCourante.CSociete);
            if (societe.SocieteSites.Count == 0)
            {
                this.gridVSite.AddNewRow();
                this.gridVSite.SetFocusedRowCellValue("Code", "001");
                this.gridVSite.SetFocusedRowCellValue("Site", societe.Nom);
                this.gridVSite.SetFocusedRowCellValue("Longitude", 0);
                this.gridVSite.SetFocusedRowCellValue("Latitude", 0);
                this.gridVSite.SetFocusedRowCellValue("Rayon", 500);
                this.gridVSite.UpdateCurrentRow();
            }

            //List<Societe> listeSocietes = GestionContexteSecurite.ContexteActive.Set<Societe>().ToList();
            //listeSocietes.Sort((s1, s2) => String.CompareOrdinal(s1.Nom, s2.Nom));
            //societe = listeSocietes[0];

            txtCSociete.EditValue = societe.CSociete;
            txtNom.EditValue = societe.Nom;
            txtAdresse.EditValue = societe.Adresse;
            txtCPostal.EditValue = societe.CodePostal;
            txtCodeTVA.EditValue = societe.CTVA;
            txtEmail.EditValue = societe.Email;
            txtFax.EditValue = societe.Fax;
            txtPays.EditValue = societe.Pays;
            txtInitiales.EditValue = societe.RaisonSociale;
            txtRegistreCommerce.EditValue = societe.RegistreCommerce;
            txtNTel.EditValue = societe.Telephone;
            txtVille.EditValue = societe.Ville;
            txtDateOuverture.EditValue = societe.DateOuverture;
            this.chkBAssujetti.Checked = societe.BAssujetti;
            this.PELogo.EditValue = societe.Logo;
            this.txtIPAdresse.Text = societe.Ip;
            this.txtPort.Value = societe.Port;
            this.txtGMTPlus.Value = societe.GMTPlus;
            this.txtLatitude.Value = societe.Latitude;
            this.txtLongitude.Value = societe.Longitude;
            this.txtRayon.Value = societe.Rayon;
            this.txtTime.Value = societe.Time;

            //foreach (SocieteBanque banque in societe.SocieteBanques)
            //{
            //    this.gridView1.AddNewRow();
            //    this.gridView1.SetFocusedRowCellValue("RIB", banque.RIB);
            //    this.gridView1.SetFocusedRowCellValue("Banque", banque.CBanque);
            //    this.gridView1.SetFocusedRowCellValue("Agence", banque.Agence);
            //    this.gridView1.SetFocusedRowCellValue("Compte comptable", banque.CompteComptable);
            //    this.gridView1.SetFocusedRowCellValue("N° journal", banque.NumeroJournal);
            //    this.gridView1.SetFocusedRowCellValue("RIB par défaut", banque.BParDefautRib);
            //    this.gridView1.UpdateCurrentRow();
            //}
            //gridView1.BestFitColumns();
            foreach (SocieteSite site in societe.SocieteSites)
            {
                this.gridVSite.AddNewRow();
                this.gridVSite.SetFocusedRowCellValue("Code", site.CSite);
                this.gridVSite.SetFocusedRowCellValue("Site", site.Site);
                this.gridVSite.SetFocusedRowCellValue("Siege", site.bSiege);
                this.gridVSite.SetFocusedRowCellValue("Longitude", site.Longitude);
                this.gridVSite.SetFocusedRowCellValue("Latitude", site.Latitude);
                this.gridVSite.SetFocusedRowCellValue("Rayon", site.Rayon);
                this.gridVSite.UpdateCurrentRow();
            }
            gridVSite.BestFitColumns();
            //GestionSession.SocieteCourante = societe;
        }

        private void FrmSociete_Load(object sender, EventArgs e)
        {            
            Actualiser();
        }

        //private void gridView1_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        //{
        //    ColumnView view = sender as ColumnView;
        //    string val = gridView1.GetRowCellValue(e.RowHandle, view.Columns["Banque"]).ToString();
        //    if (string.IsNullOrEmpty(val))
        //    {
        //        e.Valid = false;
        //        e.ErrorText = "Code banque est non renseigné !";
        //        view.SetColumnError(null, e.ErrorText);
        //    }
        //    else if (string.IsNullOrEmpty(gridView1.GetFocusedRowCellDisplayText("Agence")))
        //    {
        //        e.Valid = false;
        //        e.ErrorText = "Agence est non renseignée !";
        //        view.SetColumnError(null, e.ErrorText);
        //    }
        //    else if (string.IsNullOrEmpty(gridView1.GetFocusedRowCellDisplayText("RIB")))
        //    {
        //        e.Valid = false;
        //        e.ErrorText = "Clé R.I.B est non renseignée !";
        //        view.SetColumnError(null, e.ErrorText);
        //    }
        //    else if (!Client.VerificationRIB(gridView1.GetFocusedRowCellDisplayText("RIB")))
        //    {
        //        e.Valid = false;
        //        e.ErrorText = "R.I.B non Valide !";
        //        view.SetColumnError(null, e.ErrorText);
        //    }
        //}

        //private void gridView1_CellValueChanging(object sender, CellValueChangedEventArgs e)
        //{
        //    if (e.Column.Caption == "RIB par défaut")
        //    {
        //        GridView view = (GridView)sender;
        //        if (view.RowCount > 0)
        //        {
        //            for (int i = 0; i < view.RowCount; i++)
        //            {
        //                if (e.RowHandle != view.GetRowHandle(i))
        //                    view.SetRowCellValue(i, "RIB par défaut", false);
        //            }
                    
        //        }
        //    }

        //    if (e.Column.Caption != "RIB")
        //        return;
        //    if (e.Value.ToString().Length == 2)
        //        this.gridView1.SetFocusedRowCellValue("Banque", e.Value.ToString());
        //    if (e.Value.ToString().Length == 5)
        //        this.gridView1.SetFocusedRowCellValue("Agence", e.Value.ToString());
            
        //    if (e.Value.ToString().Length == 20)
        //    {
        //        this.gridView1.SetFocusedRowCellValue("Agence", e.Value.ToString().Substring(0, 5));
        //        this.gridView1.SetFocusedRowCellValue("Banque", e.Value.ToString().Substring(0, 2));
        //    }
        //}

        #region Site

        private GvColumnProprietes TitresSite()
        {
            GvColumnProprietes proprietes = new GvColumnProprietes();
            proprietes.Add(new GvColumnPropriete("Code", GvColumnPropriete.GvColumnType.String, GvColumnPropriete.GvColumnEtat.Enable));
            proprietes.Add(new GvColumnPropriete("Site", GvColumnPropriete.GvColumnEtat.Enable));
            proprietes.Add(new GvColumnPropriete("Siege", GvColumnPropriete.GvColumnType.Boolean, GvColumnPropriete.GvColumnEtat.Disable));
            proprietes.Add(new GvColumnPropriete("Longitude", GvColumnPropriete.GvColumnType.Decimal, GvColumnPropriete.GvColumnEtat.Enable));
            proprietes.Add(new GvColumnPropriete("Latitude", GvColumnPropriete.GvColumnType.Decimal, GvColumnPropriete.GvColumnEtat.Enable));
            proprietes.Add(new GvColumnPropriete("Rayon", GvColumnPropriete.GvColumnType.Integer, GvColumnPropriete.GvColumnEtat.Enable));
           
            return proprietes;
        }

        private void gridVSite_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            ColumnView view = sender as ColumnView;
            string val = gridVSite.GetRowCellValue(e.RowHandle, view.Columns["Code"]).ToString();
            if (string.IsNullOrEmpty(val))
            {
                e.Valid = false;
                e.ErrorText = "Code Site est non renseigné !";
                view.SetColumnError(null, e.ErrorText);
            }
            else if (string.IsNullOrEmpty(gridVSite.GetFocusedRowCellDisplayText("Site")))
            {
                e.Valid = false;
                e.ErrorText = "Site est non renseignée !";
                view.SetColumnError(null, e.ErrorText);
            }
            try
            {
                if (bool.Parse(gridVSite.GetFocusedRowCellValue("Siege").ToString()) != true)
                {
                    gridVSite.SetFocusedRowCellValue("Siege", false);
                }
            }
            catch (Exception) {
                gridVSite.SetFocusedRowCellValue("Siege", false);
            }
        }
       
        #endregion Site

        //private void gridView1_ShownEditor(object sender, EventArgs e)
        //{
        //    ColumnView view = (ColumnView)sender;
        //    if (view.FocusedColumn.FieldName == "Agence" && view.ActiveEditor is LookUpEdit)
        //    {
        //        LookUpEdit editor = (LookUpEdit)view.ActiveEditor;
        //        editor.Properties.DataSource = AgenceCollection.ChargerparBanque(Convert.ToString(view.GetFocusedRowCellValue("Banque")));
        //    }
        //}

        private void gridVSite_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ColumnView view = (ColumnView)sender;
            if (view.FocusedColumn.FieldName == "Code")
            {
                string code = view.GetFocusedRowCellDisplayText("Code");
                if (!string.IsNullOrEmpty(code))
                    e.Cancel = true;
            }
        }

    }
}