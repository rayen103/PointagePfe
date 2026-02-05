using CST.LePoint.Referentiel;
using CST.LePoint.Stock.Referentiel.Article;
using CST.LePoint.Stock.Referentiel.Commun;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CST.LePoint.CtrlLibrary.Search
{
    public partial class FrmRecherche : DevExpress.XtraEditors.XtraForm
    {
        public bool BRechercheParCode;

        public string Critere { get; set; }

        public string SourceTag { get; set; }

        public int Position { get; set; }

        public string CEntrepot { get; set; }

        public string CNatureVente { get; set; }

        public bool BActif { get; set; }

        public bool BGestionLot { get; set; }

        public DataTable dataTable { get; set; }

        public FrmRecherche()
        {
            InitializeComponent();
        }

        public GvColumnProprietes ProprietesGridView()
        {
            GvColumnProprietes proprites = new GvColumnProprietes();

            proprites.Add(new GvColumnPropriete("Code"));
            proprites.Add(new GvColumnPropriete("Libellé"));

            return proprites;
        }

        private void frmRecherche_Load(object sender, EventArgs e)
        {
            ItemCollection collection = null;
            radioButtonParCode.Checked = BRechercheParCode;
            radioButtonParLibelle.Checked = !BRechercheParCode;

            this.txtCritereSelection.Text = this.Critere;
            this.tabRecherche.SelectedTabPage = this.tabPageSelection;

            if (radioButtonPositionDebut.Checked == true)
                this.Position = 1;

            else if (radioButtonPositionFin.Checked == true)
                this.Position = 2;

            else
                this.Position = 3;

           
            switch (SourceTag)
            {
                case "VEHICULE":
                case "CLIENT":
                case "FOURNISSEUR":
                    collection = this.RemplirResultatRecherche();
                    if (collection.Count > 0)
                        tabRecherche.SelectedTabPage = tabPageResultat;
                    break;

                case "ARTICLE_SAISIE":
                    groupControl2.Visible = false;
                    collection = this.RemplirResultatRecherche();
                    if (collection.Count > 0)
                        tabRecherche.SelectedTabPage = tabPageResultat;
                    break;

                case "LOT":
                    this.ClientSize = new System.Drawing.Size(1000, 344);
                    this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
                    this.Location = new System.Drawing.Point(170, 200);
                    groupControl2.Visible = false;
                    groupControl3.Visible = false;
                    collection = this.RemplirResultatRecherche();
                    if (collection.Count > 0)
                        tabRecherche.SelectedTabPage = tabPageResultat;
                    break;

                case "ARTICLE":

                    panelControl.Visible = true;

                    CtrlHelper.FillLookUpEdit(lkpCEntrepot, EntrepotCollection.Charger());
                    lkpCEntrepot.EditValue = CEntrepot;

                    CtrlHelper.FillLookUpEdit(lkpCNatureVente, ArticleNatureVenteCollection.Charger());
                    lkpCNatureVente.EditValue = CNatureVente;

                    CtrlHelper.FillLookUpEdit(lkpCTarif, TarifCollection.Charger());
                    //lkpCTarif.ItemIndex = 0;

                    chkBActif.Checked = BActif;
                    chkBGestionLot.Checked = BGestionLot;

                    collection = this.RemplirResultatRecherche();
                    if (collection.Count > 0)
                        tabRecherche.SelectedTabPage = tabPageResultat;

                    break;

                default:
                    break;
            }
        }

        private void gridControl_DoubleClick(object sender, EventArgs e)
        {
            if ((SourceTag == "LOT") || (SourceTag == "ARTICLE_SAISIE"))
                HelperRecherche.rowSelected = resultat.GetFocusedDataRow();
            else
                HelperRecherche.FindValue = resultat.GetFocusedRowCellDisplayText(resultat.Columns[0]);
            this.Close();
        }

        private void tabRecherche_Click(object sender, EventArgs e)
        {
            if (this.tabRecherche.SelectedTabPage == this.tabPageResultat)
            {
                BRechercheParCode = radioButtonParCode.Checked;

                if (radioButtonPositionDebut.Checked == true)
                    this.Position = 1;

                else if (radioButtonPositionFin.Checked == true)
                    this.Position = 2;

                else
                    this.Position = 3;

                this.RemplirResultatRecherche();
            }
        }

        private ItemCollection RemplirResultatRecherche()
        {
            ItemCollection collection = new ItemCollection();

            switch (SourceTag)
            {
                case "CLIENT":
                    RechercherListeClient();
                    this.tabRecherche.SelectedTabPage = this.tabPageResultat;
                    break;

                case "FOURNISSEUR":
                    RechercherListeFournisseur();
                    this.tabRecherche.SelectedTabPage = this.tabPageResultat;
                    break;

                case "LOT":
                    RechercherListeDataTable();
                    this.tabRecherche.SelectedTabPage = this.tabPageResultat;
                    break;

                case "ARTICLE_SAISIE":
                    RechercherListeDataTable();
                    this.tabRecherche.SelectedTabPage = this.tabPageResultat;
                    break;

                case "ARTICLE":
                    RechercherListeArticles();
                    this.tabRecherche.SelectedTabPage = this.tabPageResultat;
                    break;

                default:
                    break;
            }

            return collection;
        }

        private void RechercherListeDataTable()
        {
            CtrlHelper.FillGridViewWithDataTable(this.resultat, this.dataTable);
            if (!string.IsNullOrEmpty(Critere))
            {
                if (SourceTag == "ARTICLE_SAISIE")
                {
                    if (BRechercheParCode)
                        this.resultat.ActiveFilterString = "Contains([CArticle], '" + Critere + "')";
                    else
                        this.resultat.ActiveFilterString = "Contains([LibArticle], '" + Critere + "')";
                }
                else
                {
                    this.resultat.ActiveFilterString = "Contains([CLot], '" + Critere + "')";
                }
            }
        }

        private void RechercherListeArticles()
        {
            string requete = string.Empty;
            string cEntrepot = string.Empty;
            string cNatureVente = string.Empty;
            string cTarif = string.Empty;

            int bAchat = (chkBAchat.Checked ? 1 : 0);
            int bVente = (chkBVente.Checked ? 1 : 0);
            int bActif = (chkBActif.Checked ? 1 : 0);
            int bLot = (chkBGestionLot.Checked ? 1 : 0);

            Item ItemEntrepot = new Item();
            ItemEntrepot = (Item)this.lkpCEntrepot.GetSelectedDataRow();
            if (ItemEntrepot != null)
                cEntrepot = ItemEntrepot.Code;

            Item ItemNatureVente = new Item();
            ItemNatureVente = (Item)this.lkpCNatureVente.GetSelectedDataRow();
            if (ItemNatureVente != null)
                cNatureVente = ItemNatureVente.Code;

            Item ItemTarif = new Item();
            ItemTarif = (Item)this.lkpCTarif.GetSelectedDataRow();
            if (ItemTarif != null)
                cTarif = ItemTarif.Code;

            requete = "SELECT Distinct A.CArticle, A.LibArticle FROM Article A (NOLOCK)";
            requete = requete + " INNER JOIN ArticleEntrepot AE (NOLOCK) ON  A.CArticle = AE.CArticle ";
            requete = requete + " INNER JOIN ArticlePrix AP (NOLOCK) ON AE.CArticle = AP.CArticle";
            requete = requete + " WHERE 1 = 1 ";
            if (!string.IsNullOrEmpty(this.txtCritereSelection.Text))
            {
                if (this.BRechercheParCode)
                {
                    if (this.Position == 1)
                        requete = String.Format("{0} AND A.CArticle LIKE '{1}%'", requete, this.txtCritereSelection.Text.Replace("'", "''"));
                    if (this.Position == 2)
                        requete = String.Format("{0} AND A.CArticle LIKE '%{1}'", requete, this.txtCritereSelection.Text.Replace("'", "''"));
                    if (this.Position == 3)
                        requete = String.Format("{0} AND A.CArticle LIKE '%{1}%'", requete, this.txtCritereSelection.Text.Replace("'", "''"));
                }
                else
                {
                    if (this.Position == 1)
                        requete = String.Format("{0} AND A.LibArticle LIKE '{1}%'", requete, this.txtCritereSelection.Text.Replace("'", "''"));
                    if (this.Position == 2)
                        requete = String.Format("{0} AND A.LibArticle LIKE '%{1}'", requete, this.txtCritereSelection.Text.Replace("'", "''"));
                    if (this.Position == 3)
                        requete = String.Format("{0} AND A.LibArticle LIKE '%{1}%'", requete, this.txtCritereSelection.Text.Replace("'", "''"));
                }
            }

            if (!string.IsNullOrEmpty(cEntrepot))
                requete = String.Format("{0} AND AE.CEntrepot ='{1}'", requete, cEntrepot);
            if (!string.IsNullOrEmpty(cNatureVente))
                requete = String.Format("{0} AND A.CNatureVente ='{1}'", requete, cNatureVente);
            if (!string.IsNullOrEmpty(cTarif))
                requete = String.Format("{0} AND  AP.CTarif='{1}'", requete, cTarif);
            
            if(chkBAchat.Checked)
                requete = String.Format("{0} AND A.BAchat = {1}", requete, int.Parse(bAchat.ToString()));
            if (chkBVente.Checked)
                requete = String.Format("{0} AND A.BVente = {1}", requete, int.Parse(bVente.ToString()));
            if ((chkBActif.Checked) && (!string.IsNullOrEmpty(cEntrepot)))
                requete = String.Format("{0} AND ISNULL(AE.BActif,0) = {1}", requete, int.Parse(bActif.ToString()));
            if ((chkBActif.Checked) && (string.IsNullOrEmpty(cEntrepot)))
                requete = String.Format("{0} AND ISNULL(A.BActif,0) = {1}", requete, int.Parse(bActif.ToString()));
            if (chkBGestionLot.Checked)
                requete = String.Format("{0} AND ISNULL(A.BGestionLot,0) = {1}", requete, int.Parse(bLot.ToString()));
            ItemCollection collection = new ItemCollection();

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = requete;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Item item = new Item() { Code = dr["CArticle"].ToString(), Libelle = dr["LibArticle"].ToString() };
                        collection.Add(item);
                    }
                }
            }

            CtrlHelper.FillGridViewWithCollection(resultat, ProprietesGridView(), collection);
        }

        private void RechercherListeClient()
        {
            string requete = string.Empty;

            requete = "SELECT CClient,RaisonSociale FROM  Client (NOLOCK)";
            requete = requete + " WHERE 1 = 1 ";
            if (!string.IsNullOrEmpty(txtCritereSelection.Text))
            {
                if (this.BRechercheParCode)
                {

                    if (this.Position == 1)
                        requete = String.Format("{0} AND CClient LIKE '{1}%'", requete, this.txtCritereSelection.Text.Replace("'", "''"));
                    if (this.Position == 2)
                        requete = String.Format("{0} AND CClient LIKE '%{1}'", requete, this.txtCritereSelection.Text.Replace("'", "''"));
                    if (this.Position == 3)
                        requete = String.Format("{0} AND CClient LIKE '%{1}%'", requete, this.txtCritereSelection.Text.Replace("'", "''"));

                }
                else
                {

                    if (this.Position == 1)
                        requete = String.Format("{0} AND RaisonSociale LIKE '{1}%'", requete, this.txtCritereSelection.Text.Replace("'", "''"));
                    if (this.Position == 2)
                        requete = String.Format("{0} AND RaisonSociale LIKE '%{1}'", requete, this.txtCritereSelection.Text.Replace("'", "''"));
                    if (this.Position == 3)
                        requete = String.Format("{0} AND RaisonSociale LIKE '%{1}%'", requete, this.txtCritereSelection.Text.Replace("'", "''"));

                }
            }

            ItemCollection collection = new ItemCollection();

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = requete;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Item item = new Item() { Code = dr["CClient"].ToString(), Libelle = dr["RaisonSociale"].ToString() };
                        collection.Add(item);
                    }
                }
            }

            GvColumnProprietes gridprop = ProprietesGridView();
            CtrlHelper.FillGridViewWithCollection(resultat, gridprop, collection);
        }

        private void RechercherListeFournisseur()
        {
            string requete = string.Empty;

            requete = "SELECT CFournisseur,RaisonSociale FROM  Fournisseur (NOLOCK)";
            requete = requete + " WHERE 1 = 1 ";
            if (!string.IsNullOrEmpty(txtCritereSelection.Text))
            {

                if (this.BRechercheParCode)
                {
                        if (this.Position == 1)
                            requete = String.Format("{0} AND CFournisseur LIKE '{1}%'", requete, this.txtCritereSelection.Text.Replace("'", "''"));
                        if (this.Position == 2)
                            requete = String.Format("{0} AND CFournisseur LIKE '%{1}'", requete, this.txtCritereSelection.Text.Replace("'", "''"));
                        if (this.Position == 3)
                            requete = String.Format("{0} AND CFournisseur LIKE '%{1}%'", requete, this.txtCritereSelection.Text.Replace("'", "''"));
                }
                else
                {
                        if (this.Position == 1)
                            requete = String.Format("{0} AND RaisonSociale LIKE '{1}%'", requete, this.txtCritereSelection.Text.Replace("'", "''"));
                        if (this.Position == 2)
                            requete = String.Format("{0} AND RaisonSociale LIKE '%{1}'", requete, this.txtCritereSelection.Text.Replace("'", "''"));
                        if (this.Position == 3)
                            requete = String.Format("{0} AND RaisonSociale LIKE '%{1}%'", requete, this.txtCritereSelection.Text.Replace("'", "''"));
                }
            }

            ItemCollection collection = new ItemCollection();

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = requete;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Item item = new Item() { Code = dr["CFournisseur"].ToString(), Libelle = dr["RaisonSociale"].ToString() };
                        collection.Add(item);
                    }
                }
            }

            GvColumnProprietes gridprop = ProprietesGridView();
            CtrlHelper.FillGridViewWithCollection(resultat, gridprop, collection);
        }

        private void txtCritereSelection_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BRechercheParCode = radioButtonParCode.Checked;

                if (radioButtonPositionDebut.Checked == true)
                    this.Position = 1;

                else if (radioButtonPositionFin.Checked == true)
                    this.Position = 2;

                else
                    this.Position = 3;

                this.RemplirResultatRecherche();
                tabRecherche.SelectedTabPage = tabPageResultat;
            }
        }

        private void FrmRecherche_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        private void lkpCEntrepot_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back || e.KeyData == Keys.Delete)
            {
                this.lkpCEntrepot.EditValue = string.Empty;
                e.Handled = true;
            }
        }

        private void lkpCTarif_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back || e.KeyData == Keys.Delete)
            {
                this.lkpCTarif.EditValue = string.Empty;
                e.Handled = true;
            }
        }

        private void resultat_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.resultat.RowCount == 0 )
                return;
            if (e.KeyCode == Keys.Enter)
            {
                if ((SourceTag == "LOT") || (SourceTag == "ARTICLE_SAISIE"))
                    HelperRecherche.rowSelected = resultat.GetFocusedDataRow();
                else
                    HelperRecherche.FindValue = resultat.GetFocusedRowCellDisplayText(resultat.Columns[0]);
                this.Close();
            }
        }

        private void lkpCNatureVente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back || e.KeyData == Keys.Delete)
            {
                this.lkpCNatureVente.EditValue = string.Empty;
                e.Handled = true;
            }
        }
    }
}