using CST.LePoint.CtrlLibrary.DevExpressEx;
using CST.LePoint.Securite;
using CST.LePoint.Stock.Referentiel.Article;
using CST.LePoint.Stock.Referentiel.Commun;
using CST.LePoint.Tiers.Metier;
using CST.LePoint.Tiers.Referentiel;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using System;
using System.Drawing.Printing;
using System.Windows.Forms;
using CST.LePoint.Intervention.Metier;

namespace CST.LePoint.CtrlLibrary.Satellites
{
    public partial class FrmSatellites : DevExpress.XtraEditors.XtraForm
    {
        private RefSatellites _Satellites = new RefSatellites();
        private string _NomSatelilte = string.Empty;
        private bool bLoadTree = false;
        private bool isUpDated = false;
        private bool[] selectedNode = new Boolean[22];

        public FrmSatellites()
        {
            InitializeComponent();
        }

        private void Satellites_Load(object sender, EventArgs e)
        {
            InitTreeList();
            GvColumnProprietes proprietes = new GvColumnProprietes();
            proprietes.Add(new GvColumnPropriete("Code", GvColumnPropriete.GvColumnEtat.Enable));
            proprietes.Add(new GvColumnPropriete("Libellé", GvColumnPropriete.GvColumnEtat.Enable));
            CtrlHelper.InitGridView(this.gridV, proprietes, true);
        }

        private void InitTreeList()
        {
            /* Neoud : Commun */
            int TREE_ID_COMMUNS = 0;
            //_Satellites.Add(new RefSatellite("Communs", TREE_ID_COMMUNS));
            //_Satellites.Add(new RefSatellite("Banques", _Satellites.Count, TREE_ID_COMMUNS));
            //_Satellites.Add(new RefSatellite("Agences", _Satellites.Count, TREE_ID_COMMUNS));
            //_Satellites.Add(new RefSatellite("Modes Règlements", _Satellites.Count, TREE_ID_COMMUNS));

            /* Neoud : Tiers */
            int TREE_ID_TIERS = _Satellites.Count;
            //_Satellites.Add(new RefSatellite("Tiers", TREE_ID_TIERS));
            //_Satellites.Add(new RefSatellite("Civilités", _Satellites.Count, TREE_ID_TIERS));
            //_Satellites.Add(new RefSatellite("Natures Tiers", _Satellites.Count, TREE_ID_TIERS));
            //_Satellites.Add(new RefSatellite("Familles Clients", _Satellites.Count, TREE_ID_TIERS));
            //_Satellites.Add(new RefSatellite("Familles Fournisseurs", _Satellites.Count, TREE_ID_TIERS));
            //_Satellites.Add(new RefSatellite("Commercial", _Satellites.Count, TREE_ID_TIERS));
            //_Satellites.Add(new RefSatellite("Gratuites", _Satellites.Count, TREE_ID_TIERS));
            //_Satellites.Add(new RefSatellite("Type bon d'achat", _Satellites.Count, TREE_ID_TIERS));
            //_Satellites.Add(new RefSatellite("Type bon commande", _Satellites.Count, TREE_ID_TIERS));
            //_Satellites.Add(new RefSatellite("Type options", _Satellites.Count, TREE_ID_TIERS));
            //_Satellites.Add(new RefSatellite("Options", _Satellites.Count, TREE_ID_TIERS));
            //_Satellites.Add(new RefSatellite("Etat", _Satellites.Count, TREE_ID_TIERS));
            //_Satellites.Add(new RefSatellite("Etablissement", _Satellites.Count, TREE_ID_TIERS));
            //_Satellites.Add(new RefSatellite("Préparateurs", _Satellites.Count, TREE_ID_TIERS));
            //_Satellites.Add(new RefSatellite("Releveurs", _Satellites.Count, TREE_ID_TIERS));

            /* Neoud : Convention */
            int TREE_ID_CONVENTIONS = _Satellites.Count;
            //_Satellites.Add(new RefSatellite("Conventions", TREE_ID_CONVENTIONS));
            //_Satellites.Add(new RefSatellite("Types Convention", _Satellites.Count, TREE_ID_CONVENTIONS));
            //_Satellites.Add(new RefSatellite("Objectif", _Satellites.Count, TREE_ID_CONVENTIONS));
            //_Satellites.Add(new RefSatellite("Motif", _Satellites.Count, TREE_ID_CONVENTIONS));

            /* Neoud : Article */
            int TREE_ID_ARTICLE = _Satellites.Count;
            //_Satellites.Add(new RefSatellite("Articles", TREE_ID_ARTICLE));
            //_Satellites.Add(new RefSatellite("Catégories Articles", _Satellites.Count, TREE_ID_ARTICLE));
            //_Satellites.Add(new RefSatellite("Familles Articles", _Satellites.Count, TREE_ID_ARTICLE));
            //_Satellites.Add(new RefSatellite("Types Articles", _Satellites.Count, TREE_ID_ARTICLE));
            //_Satellites.Add(new RefSatellite("État Article", _Satellites.Count, TREE_ID_ARTICLE));
            //_Satellites.Add(new RefSatellite("Modèles", _Satellites.Count, TREE_ID_ARTICLE));
            //_Satellites.Add(new RefSatellite("Sous Modèles1", _Satellites.Count, _Satellites.Count));
            //_Satellites.Add(new RefSatellite("Sous Modèles2", _Satellites.Count, _Satellites.Count));
            //_Satellites.Add(new RefSatellite("Natures Articles", _Satellites.Count, TREE_ID_ARTICLE));
            //_Satellites.Add(new RefSatellite("Natures Vente", _Satellites.Count, TREE_ID_ARTICLE));
            //_Satellites.Add(new RefSatellite("Emballages", _Satellites.Count, TREE_ID_ARTICLE));
            //_Satellites.Add(new RefSatellite("Unités", _Satellites.Count, TREE_ID_ARTICLE));
            //_Satellites.Add(new RefSatellite("Tarifs", _Satellites.Count, TREE_ID_ARTICLE));
            //_Satellites.Add(new RefSatellite("Entrepôts", _Satellites.Count, TREE_ID_ARTICLE));

            /* Neoud : Divers */
            int TREE_ID_DIVERS = _Satellites.Count;
            _Satellites.Add(new RefSatellite("Divers", TREE_ID_DIVERS));
            //_Satellites.Add(new RefSatellite("Villes", _Satellites.Count, TREE_ID_DIVERS));
            _Satellites.Add(new RefSatellite("Régions", _Satellites.Count, TREE_ID_DIVERS));
           // _Satellites.Add(new RefSatellite("Pays", _Satellites.Count, TREE_ID_DIVERS));
           // _Satellites.Add(new RefSatellite("Voitures", _Satellites.Count, TREE_ID_DIVERS));            
            _Satellites.Add(new RefSatellite("Gouvernorat", _Satellites.Count, TREE_ID_DIVERS));
            _Satellites.Add(new RefSatellite("Chauffeurs", _Satellites.Count, TREE_ID_DIVERS));
            //_Satellites.Add(new RefSatellite("Jours fériés", _Satellites.Count, TREE_ID_DIVERS));
            _Satellites.Add(new RefSatellite("Modems", _Satellites.Count, TREE_ID_DIVERS));
            _Satellites.Add(new RefSatellite("Autobus", _Satellites.Count, TREE_ID_DIVERS));
            //_Satellites.Add(new RefSatellite("Circuits", _Satellites.Count, TREE_ID_DIVERS));

            this.treeLSatellites.OptionsBehavior.Editable = false;
            this.treeLSatellites.OptionsPrint.UsePrintStyles = true;
            this.treeLSatellites.DataSource = _Satellites;
            this.treeLSatellites.PopulateColumns();
            this.treeLSatellites.BestFitColumns();
            this.treeLSatellites.ExpandAll();
            this._NomSatelilte = string.Empty;

            bLoadTree = true;
        }

        private void warning(string msg, bool warning)
        {
            if (warning)
                XtraMessageBox.Show(msg,
                                           "Alerte",
                                           MessageBoxButtons.OK,
                                           MessageBoxIcon.Warning,
                                           MessageBoxDefaultButton.Button1);
            else
                XtraMessageBox.Show(msg,
                                             "Information",
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Information,
                                             MessageBoxDefaultButton.Button1);
        }

        private void setSelectedNode(int i)
        {
            selectedNode[i] = true;
            for (int j = 0; j < selectedNode.Length; j++)
            {
                if (j != i)
                {
                    selectedNode[j] = false;
                }
            }
        }

        private void ChargerEntite(string nonEntite)
        {
            switch (nonEntite.Trim())
            {
                case "Banques":
                    {
                        GvColumnProprietes proprietes = new GvColumnProprietes();
                        proprietes.Add(new GvColumnPropriete("Code", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Libellé", GvColumnPropriete.GvColumnEtat.Enable));
                        //proprietes.Add(new GvColumnPropriete("IBAN", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Code Bic", GvColumnPropriete.GvColumnEtat.Enable));
                        //proprietes.Add(new GvColumnPropriete("Initiale", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Pays", GvColumnPropriete.GvColumnType.LookUpVide, GvColumnPropriete.GvColumnEtat.Enable, PaysCollection.Charger()));
                        proprietes.Add(new GvColumnPropriete("Adresse", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Code Postal", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Ville ", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Supprimer", GvColumnPropriete.GvColumnType.Boolean, GvColumnPropriete.GvColumnEtat.Invisible));
                        CtrlHelper.InitGridView(this.gridV, proprietes, true);
                        CtrlHelper.FillGridView(this.gridV, proprietes, BanqueCollection.RemplirGrid());

                        break;
                    }
                case "Agences":
                    {
                        GvColumnProprietes proprietes = new GvColumnProprietes();
                        proprietes.Add(new GvColumnPropriete("Banque", GvColumnPropriete.GvColumnType.LookUpVide, GvColumnPropriete.GvColumnEtat.Enable, BanqueCollection.Charger()));
                        proprietes.Add(new GvColumnPropriete("Code", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Libellé", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Adresse", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Tel", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("fax", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Email", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Responsable", GvColumnPropriete.GvColumnEtat.Enable));
                        // proprietes.Add(new GvColumnPropriete("Supprimer", GvColumnPropriete.GvColumnType.Boolean, GvColumnPropriete.GvColumnEtat.Enable));
                        CtrlHelper.InitGridView(this.gridV, proprietes, true);
                        CtrlHelper.FillGridView(this.gridV, proprietes, AgenceCollection.RemplirGrid());

                        break;
                    }
                case "Modes Règlements":
                    {
                        GvColumnProprietes proprietes = new GvColumnProprietes(); 
                        proprietes.Add(new GvColumnPropriete("Code", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Libellé", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Echéance", GvColumnPropriete.GvColumnType.Boolean, GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Mobile", GvColumnPropriete.GvColumnType.Boolean, GvColumnPropriete.GvColumnEtat.Enable));
                        CtrlHelper.InitGridView(this.gridV, proprietes, true);
                        CtrlHelper.FillGridView(this.gridV, proprietes, ModeReglementCollection.RemplirGrid());
                        break;
                    }

                case "Civilités":
                    {
                        GvColumnProprietes proprietes = new GvColumnProprietes(); proprietes.Add(new GvColumnPropriete("Code", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Libellé", GvColumnPropriete.GvColumnEtat.Enable));
                        CtrlHelper.InitGridView(this.gridV, proprietes, true);
                        CtrlHelper.FillGridView(this.gridV, proprietes, CiviliteCollection.RemplirGrid());

                        break;
                    }
                case "Natures Tiers":
                    {
                        GvColumnProprietes proprietes = new GvColumnProprietes(); proprietes.Add(new GvColumnPropriete("Code", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Libellé", GvColumnPropriete.GvColumnEtat.Enable));
                        CtrlHelper.InitGridView(this.gridV, proprietes, true);
                        CtrlHelper.FillGridView(this.gridV, proprietes, NatureTiersCollection.RemplirGrid());
                        break;
                    }

                case "Familles Clients":
                    {
                        GvColumnProprietes proprietes = new GvColumnProprietes(); proprietes.Add(new GvColumnPropriete("Code", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Libellé", GvColumnPropriete.GvColumnEtat.Enable));
                        CtrlHelper.InitGridView(this.gridV, proprietes, true);
                        CtrlHelper.FillGridView(this.gridV, proprietes, ClientFamilleCollection.RemplirGrid());
                        break;
                    }

                case "Commercial":
                    {
                        GvColumnProprietes proprietes = new GvColumnProprietes(); proprietes.Add(new GvColumnPropriete("Code", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Nom", GvColumnPropriete.GvColumnType.String, GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Prénom", GvColumnPropriete.GvColumnType.String, GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("CUtilisateur", GvColumnPropriete.GvColumnType.String, GvColumnPropriete.GvColumnEtat.Invisible));
                        proprietes.Add(new GvColumnPropriete("Tèlèphone", GvColumnPropriete.GvColumnType.String, GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Portable", GvColumnPropriete.GvColumnType.String, GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Email", GvColumnPropriete.GvColumnType.String, GvColumnPropriete.GvColumnEtat.Enable));
                        CtrlHelper.InitGridView(this.gridV, proprietes, true);
                        CtrlHelper.FillGridView(this.gridV, proprietes, CommercialCollection.RemplirGrid());

                        break;
                    }
                case "Gratuites":
                    {
                        GvColumnProprietes proprietes = new GvColumnProprietes(); 
                        proprietes.Add(new GvColumnPropriete("Code", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Libellé", GvColumnPropriete.GvColumnType.String, GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Dividende", GvColumnPropriete.GvColumnType.Integer, GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Diviseur", GvColumnPropriete.GvColumnType.Integer, GvColumnPropriete.GvColumnEtat.Enable));
                        CtrlHelper.InitGridView(this.gridV, proprietes, true);
                        CtrlHelper.FillGridView(this.gridV, proprietes, GratuitesCollection.RemplirGrid());

                        break;
                    }
                case "Type bon d'achat":
                    {
                        GvColumnProprietes proprietes = new GvColumnProprietes();
                        proprietes.Add(new GvColumnPropriete("Code", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Libellé", GvColumnPropriete.GvColumnType.String, GvColumnPropriete.GvColumnEtat.Enable));
                        CtrlHelper.InitGridView(this.gridV, proprietes, true);
                        CtrlHelper.FillGridView(this.gridV, proprietes, TypeBonAchatCollection.RemplirGrid());

                        break;
                    }
                case "Type bon commande":
                    {
                        GvColumnProprietes proprietes = new GvColumnProprietes();
                        proprietes.Add(new GvColumnPropriete("Code", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Libellé", GvColumnPropriete.GvColumnType.String, GvColumnPropriete.GvColumnEtat.Enable));
                        CtrlHelper.InitGridView(this.gridV, proprietes, true);
                        CtrlHelper.FillGridView(this.gridV, proprietes, TypeBonCommandeCollection.RemplirGrid());

                        break;
                    }
                case "Type options":
                    {
                        GvColumnProprietes proprietes = new GvColumnProprietes();
                        proprietes.Add(new GvColumnPropriete("Code", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Libellé", GvColumnPropriete.GvColumnType.String, GvColumnPropriete.GvColumnEtat.Enable));

                        CtrlHelper.InitGridView(this.gridV, proprietes, true);
                        CtrlHelper.FillGridView(this.gridV, proprietes, TypeOptionsCollection.RemplirGrid());

                        break;
                    }
                case "Options":
                    {
                        GvColumnProprietes proprietes = new GvColumnProprietes();
                        proprietes.Add(new GvColumnPropriete("Code", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Libellé", GvColumnPropriete.GvColumnType.String, GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Type option", GvColumnPropriete.GvColumnType.LookUpVide, GvColumnPropriete.GvColumnEtat.Enable, TypeOptionsCollection.Charger()));
                        proprietes.Add(new GvColumnPropriete("Ordre", GvColumnPropriete.GvColumnType.Integer, GvColumnPropriete.GvColumnEtat.Enable));

                        CtrlHelper.InitGridView(this.gridV, proprietes, true);
                        this.gridV.OptionsCustomization.AllowFilter = true;
                        CtrlHelper.FillGridView(this.gridV, proprietes, OptionsCollection.RemplirGrid());

                        break;
                    }
                case "Etat":
                    {
                        GvColumnProprietes proprietes = new GvColumnProprietes();
                        proprietes.Add(new GvColumnPropriete("Code", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Libellé", GvColumnPropriete.GvColumnType.String, GvColumnPropriete.GvColumnEtat.Enable));

                        CtrlHelper.InitGridView(this.gridV, proprietes, true);
                        CtrlHelper.FillGridView(this.gridV, proprietes, EtatCollection.RemplirGrid());

                        break;
                    }
                case "Préparateurs":
                    {
                        GvColumnProprietes proprietes = new GvColumnProprietes(); proprietes.Add(new GvColumnPropriete("Code", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Nom", GvColumnPropriete.GvColumnType.String, GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Prénom", GvColumnPropriete.GvColumnType.String, GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("CUtilisateur", GvColumnPropriete.GvColumnType.String, GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Tèlèphone", GvColumnPropriete.GvColumnType.String, GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Portable", GvColumnPropriete.GvColumnType.String, GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Email", GvColumnPropriete.GvColumnType.String, GvColumnPropriete.GvColumnEtat.Enable));
                        CtrlHelper.InitGridView(this.gridV, proprietes, true);
                        CtrlHelper.FillGridView(this.gridV, proprietes, PreparateurCollection.RemplirGrid());

                        break;
                    }
                case "Releveurs":
                    {
                        GvColumnProprietes proprietes = new GvColumnProprietes();
                        proprietes.Add(new GvColumnPropriete("Code", GvColumnPropriete.GvColumnType.String, GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Nom", GvColumnPropriete.GvColumnType.String, GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Prénom", GvColumnPropriete.GvColumnType.String, GvColumnPropriete.GvColumnEtat.Enable));
                        CtrlHelper.InitGridView(this.gridV, proprietes, true);
                        CtrlHelper.FillGridView(this.gridV, proprietes, ReleveurCollection.RemplirGrid());

                        break;
                    }
                case "Familles Fournisseurs":
                    {
                        GvColumnProprietes proprietes = new GvColumnProprietes(); proprietes.Add(new GvColumnPropriete("Code", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Libellé", GvColumnPropriete.GvColumnEtat.Enable));
                        CtrlHelper.InitGridView(this.gridV, proprietes, true);
                        CtrlHelper.FillGridView(this.gridV, proprietes, FournisseurFamilleCollection.RemplirGrid(null));
                        break;
                    }
                case "Familles Articles":
                    {
                        GvColumnProprietes proprietes = new GvColumnProprietes(); 
                        proprietes.Add(new GvColumnPropriete("Code", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Libellé", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Active", GvColumnPropriete.GvColumnType.Boolean, GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Ordre", GvColumnPropriete.GvColumnType.Integer, GvColumnPropriete.GvColumnEtat.Enable));

                        CtrlHelper.InitGridView(this.gridV, proprietes, true);
                        CtrlHelper.FillGridView(this.gridV, proprietes, ArticleFamilleCollection.RemplirGrid());
                        break;
                    }
                case "Catégories Articles":
                    {
                        GvColumnProprietes proprietes = new GvColumnProprietes(); 
                        proprietes.Add(new GvColumnPropriete("Code", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Libellé", GvColumnPropriete.GvColumnEtat.Enable));
                        CtrlHelper.InitGridView(this.gridV, proprietes, true);
                        CtrlHelper.FillGridView(this.gridV, proprietes, ArticleCategorieCollection.RemplirGrid());
                        break;
                    }
                case "Types Articles":
                    {
                        GvColumnProprietes proprietes = new GvColumnProprietes(); 
                        proprietes.Add(new GvColumnPropriete("Code", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Libellé", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Active", GvColumnPropriete.GvColumnType.Boolean, GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Ordre", GvColumnPropriete.GvColumnType.Integer, GvColumnPropriete.GvColumnEtat.Enable));

                        CtrlHelper.InitGridView(this.gridV, proprietes, true);
                        CtrlHelper.FillGridView(this.gridV, proprietes, ArticleTypeCollection.RemplirGrid());
                        break;
                    }
                case "État Article":
                    {
                        GvColumnProprietes proprietes = new GvColumnProprietes();
                        proprietes.Add(new GvColumnPropriete("Code", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Libellé", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Color", GvColumnPropriete.GvColumnType.Color, GvColumnPropriete.GvColumnEtat.Enable));
                        CtrlHelper.InitGridView(this.gridV, proprietes, true);
                        foreach (EtatArticle etat in EtatArticleCollection.Charger())
                        {
                            gridV.AddNewRow();
                            gridV.SetFocusedRowCellValue("Code", etat.Code);
                            gridV.SetFocusedRowCellValue("Libellé", etat.Libelle);
                            System.Drawing.Color color = string.IsNullOrEmpty(etat.Color) ? System.Drawing.Color.Black : System.Drawing.ColorTranslator.FromHtml(etat.Color);
                            gridV.SetFocusedRowCellValue("Color", color);
                            gridV.UpdateCurrentRow();
                        }

                        break;
                    }

                case "Modèles":
                    {
                        GvColumnProprietes proprietes = new GvColumnProprietes(); 
                        proprietes.Add(new GvColumnPropriete("Code", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Libellé", GvColumnPropriete.GvColumnEtat.Enable));
                        CtrlHelper.InitGridView(this.gridV, proprietes, true);
                        CtrlHelper.FillGridView(this.gridV, proprietes, ArticleModeleCollection.RemplirGrid());
                        break;
                    }
                case "Sous Modèles1":
                    {
                        GvColumnProprietes proprietes = new GvColumnProprietes(); 
                        proprietes.Add(new GvColumnPropriete("Code", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Libellé", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Modèle", GvColumnPropriete.GvColumnType.LookUp, GvColumnPropriete.GvColumnEtat.Enable, ArticleModeleCollection.Charger()));
                        CtrlHelper.InitGridView(this.gridV, proprietes, true);
                        CtrlHelper.FillGridView(this.gridV, proprietes, ArticleSousModele1Collection.RemplirGrid());
                        break;
                    }
                case "Sous Modèles2":
                    {
                        GvColumnProprietes proprietes = new GvColumnProprietes(); 
                        proprietes.Add(new GvColumnPropriete("Code", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Libellé", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Modèle", GvColumnPropriete.GvColumnType.LookUp, GvColumnPropriete.GvColumnEtat.Enable, ArticleModeleCollection.Charger()));
                        proprietes.Add(new GvColumnPropriete("Sous Modèle 1", GvColumnPropriete.GvColumnType.LookUp, GvColumnPropriete.GvColumnEtat.Enable, ArticleSousModele1Collection.Charger()));
                        CtrlHelper.InitGridView(this.gridV, proprietes, true);
                        CtrlHelper.FillGridView(this.gridV, proprietes, ArticleSousModele2Collection.RemplirGrid());
                        break;
                    }

                case "Natures Articles":
                    {
                        GvColumnProprietes proprietes = new GvColumnProprietes(); 
                        proprietes.Add(new GvColumnPropriete("Code", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Libellé", GvColumnPropriete.GvColumnEtat.Enable));

                        CtrlHelper.InitGridView(this.gridV, proprietes, true);

                        CtrlHelper.FillGridView(this.gridV, proprietes, ArticleNatureCollection.RemplirGrid());
                        break;
                    }

                case "Natures Vente":
                    {
                        GvColumnProprietes proprietes = new GvColumnProprietes(); 
                        proprietes.Add(new GvColumnPropriete("Code", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Libellé", GvColumnPropriete.GvColumnEtat.Enable));

                        CtrlHelper.InitGridView(this.gridV, proprietes, true);

                        CtrlHelper.FillGridView(this.gridV, proprietes, ArticleNatureVenteCollection.RemplirGrid());
                        break;
                    }

                case "Emballages":
                    {
                        GvColumnProprietes proprietes = new GvColumnProprietes(); proprietes.Add(new GvColumnPropriete("Code", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Libellé", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Quantité", GvColumnPropriete.GvColumnType.Integer, GvColumnPropriete.GvColumnEtat.Enable));
                        CtrlHelper.InitGridView(this.gridV, proprietes, true);
                        CtrlHelper.FillGridView(this.gridV, proprietes, EmballageCollection.RemplirGrid());
                        break;
                    }
                case "Unités":
                    {
                        GvColumnProprietes proprietes = new GvColumnProprietes(); 
                        proprietes.Add(new GvColumnPropriete("Code", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Libellé", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("NbreDecimal", GvColumnPropriete.GvColumnType.Decimal, GvColumnPropriete.GvColumnEtat.Enable));

                        CtrlHelper.InitGridView(this.gridV, proprietes, true);
                        CtrlHelper.FillGridView(this.gridV, proprietes, UniteCollection.RemplirGrid());
                        break;
                    }

                case "Tarifs":
                    {
                        GvColumnProprietes proprietes = new GvColumnProprietes(); 
                        proprietes.Add(new GvColumnPropriete("Code", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Libellé", GvColumnPropriete.GvColumnEtat.Enable));
                        CtrlHelper.InitGridView(this.gridV, proprietes, true);
                        CtrlHelper.FillGridView(this.gridV, proprietes, TarifCollection.RemplirGrid());
                        break;
                    }
                case "Entrepôts":
                    {
                        GvColumnProprietes proprietes = new GvColumnProprietes(); proprietes.Add(new GvColumnPropriete("Code", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Libellé", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Adresse", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Principale", GvColumnPropriete.GvColumnType.Boolean, GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Livrable", GvColumnPropriete.GvColumnType.Boolean, GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Fixe", GvColumnPropriete.GvColumnType.String, GvColumnPropriete.GvColumnEtat.Enable));
                        CtrlHelper.InitGridView(this.gridV, proprietes, true);
                        CtrlHelper.FillGridView(this.gridV, proprietes, EntrepotCollection.RemplirGrid());
                        break;
                    }
                case "Régions":
                    {
                        GvColumnProprietes proprietes = new GvColumnProprietes(); 
                        proprietes.Add(new GvColumnPropriete("Code", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Libellé", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Active", GvColumnPropriete.GvColumnType.Boolean, GvColumnPropriete.GvColumnEtat.Enable));
                        CtrlHelper.InitGridView(this.gridV, proprietes, true);
                        CtrlHelper.FillGridView(this.gridV, proprietes, RegionCollection.RemplirGrid());
                        break;
                    }
                case "Gouvernorat":
                    {
                        GvColumnProprietes proprietes = new GvColumnProprietes(); proprietes.Add(new GvColumnPropriete("Code", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Libellé", GvColumnPropriete.GvColumnEtat.Enable));
                        CtrlHelper.InitGridView(this.gridV, proprietes, true);
                        CtrlHelper.FillGridView(this.gridV, proprietes, GouvernoratCollection.RemplirGrid());
                        break;
                    }
                case "Pays":
                    {
                        GvColumnProprietes proprietes = new GvColumnProprietes(); proprietes.Add(new GvColumnPropriete("Code", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Libellé", GvColumnPropriete.GvColumnEtat.Enable));
                        CtrlHelper.InitGridView(this.gridV, proprietes, true);
                        CtrlHelper.FillGridView(this.gridV, proprietes, PaysCollection.RemplirGrid());
                        break;
                    }
                case "Voitures":
                    {
                        GvColumnProprietes proprietes = new GvColumnProprietes(); proprietes.Add(new GvColumnPropriete("Code", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Libellé", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("N° de Série", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Charge Maximale par Kg", GvColumnPropriete.GvColumnType.DecimalPositif, GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Disponible", GvColumnPropriete.GvColumnType.Boolean, GvColumnPropriete.GvColumnEtat.Invisible));
                        proprietes.Add(new GvColumnPropriete("Active", GvColumnPropriete.GvColumnType.Boolean, GvColumnPropriete.GvColumnEtat.Enable));

                        CtrlHelper.InitGridView(this.gridV, proprietes, true);
                        CtrlHelper.FillGridView(this.gridV, proprietes, VehiculeCollection.RemplirGrid());
                        break;
                    }
                case "Chauffeurs":
                    {
                        GvColumnProprietes proprietes = new GvColumnProprietes(); proprietes.Add(new GvColumnPropriete("Code", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("C.I.N", GvColumnPropriete.GvColumnType.String, GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("RFID_Chauf",GvColumnPropriete.GvColumnType.String, GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Externe", GvColumnPropriete.GvColumnType.Boolean, GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Nom", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Prénom", GvColumnPropriete.GvColumnEtat.Enable));
                        CtrlHelper.InitGridView(this.gridV, proprietes, true);
                        CtrlHelper.FillGridView(this.gridV, proprietes, ChauffeurCollection.RemplirGrid());
                        break;
                    }
                case "Villes":
                    {
                        GvColumnProprietes proprietes = new GvColumnProprietes(); proprietes.Add(new GvColumnPropriete("Code", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Libellé", GvColumnPropriete.GvColumnEtat.Enable));
                        CtrlHelper.InitGridView(this.gridV, proprietes, true);
                        CtrlHelper.FillGridView(this.gridV, proprietes, VilleCollection.RemplirGrid());
                        break;
                    }
                case "Types Convention":
                    {
                        GvColumnProprietes proprietes = new GvColumnProprietes(); proprietes.Add(new GvColumnPropriete("Code", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Libellé", GvColumnPropriete.GvColumnEtat.Enable));
                        CtrlHelper.InitGridView(this.gridV, proprietes, true);
                        CtrlHelper.FillGridView(this.gridV, proprietes, TypeConventionCollection.RemplirGrid());
                        break;
                    }
                case "Objectif":
                    {
                        GvColumnProprietes proprietes = new GvColumnProprietes(); proprietes.Add(new GvColumnPropriete("Code", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Libellé", GvColumnPropriete.GvColumnEtat.Enable));
                        CtrlHelper.InitGridView(this.gridV, proprietes, true);
                        CtrlHelper.FillGridView(this.gridV, proprietes, TypeVisiteCollection.RemplirGrid());
                        break;
                    }
                case "Motif":
                    {
                        GvColumnProprietes proprietes = new GvColumnProprietes(); 
                        proprietes.Add(new GvColumnPropriete("Code", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Libellé", GvColumnPropriete.GvColumnEtat.Enable));
                        CtrlHelper.InitGridView(this.gridV, proprietes, true);
                        CtrlHelper.FillGridView(this.gridV, proprietes, MotifCollection.RemplirGrid());
                        break;
                    }
                case "Etablissement":
                    {
                        GvColumnProprietes proprietes = new GvColumnProprietes(); proprietes.Add(new GvColumnPropriete("Code", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Libellé", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Client", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Region", GvColumnPropriete.GvColumnType.LookUpVide, GvColumnPropriete.GvColumnEtat.Enable, RegionCollection.Charger()));
                        CtrlHelper.InitGridView(this.gridV, proprietes, true);
                        CtrlHelper.FillGridView(this.gridV, proprietes, EtablissementCollection.RemplirGrid());

                        break;
                    }
                case "Jours fériés":
                    {
                        GvColumnProprietes proprietes = new GvColumnProprietes(); 
                        proprietes.Add(new GvColumnPropriete("Code", GvColumnPropriete.GvColumnEtat.Invisible));
                        proprietes.Add(new GvColumnPropriete("Date", GvColumnPropriete.GvColumnType.Date, GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Libellé", GvColumnPropriete.GvColumnEtat.Enable));
                        CtrlHelper.InitGridView(this.gridV, proprietes, true);
                        CtrlHelper.FillGridView(this.gridV, proprietes, JoursFeriesCollection.RemplirGrid());

                        break;
                    }

                case "Modems":
                    {
                        GvColumnProprietes proprietes = new GvColumnProprietes();
                        proprietes.Add(new GvColumnPropriete("IMEI", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Modele",  GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Numero SIM", GvColumnPropriete.GvColumnEtat.Enable));
                        CtrlHelper.InitGridView(this.gridV, proprietes, true);
                        CtrlHelper.FillGridView(this.gridV, proprietes, ModemCollection.RemplirGrid());

                        break;
                    }

                case "Autobus":
                    {
                        GvColumnProprietes proprietes = new GvColumnProprietes();
                        proprietes.Add(new GvColumnPropriete("Immatriculation", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Model Bus", GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("IMEI", GvColumnPropriete.GvColumnType.LookUpVide, GvColumnPropriete.GvColumnEtat.Enable, ModemCollection.Charger()));
                        proprietes.Add(new GvColumnPropriete("Capacite Bus", GvColumnPropriete.GvColumnType.Integer, GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Code Circuit", GvColumnPropriete.GvColumnType.LookUpVide, GvColumnPropriete.GvColumnEtat.Enable, RefCircuitCollection.Charger()));
                        proprietes.Add(new GvColumnPropriete("APP Sagem", GvColumnPropriete.GvColumnType.Boolean, GvColumnPropriete.GvColumnEtat.Enable));
                        proprietes.Add(new GvColumnPropriete("Chauffeur", GvColumnPropriete.GvColumnType.LookUpVide, GvColumnPropriete.GvColumnEtat.Enable, ChauffeurCollection.Charger()));

                        CtrlHelper.InitGridView(this.gridV, proprietes, true);
                        CtrlHelper.FillGridView(this.gridV, proprietes, BusCollection.RemplirGrid());

                        break;
                    }
                default:
                    break;
            }
        }

        private void Sauvgarder()
        {
            string msgEchoue = "Opération de modification a échoué";
            string msgSucces = "Modifications effectuées avec succès";

            var dialogResult = XtraMessageBox.Show("Voulez-vous Enregistrer vos modifications ?",
                "",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (dialogResult != DialogResult.Yes)
                return;
            if (this._NomSatelilte != "Modems" && this._NomSatelilte != "Autobus" && this._NomSatelilte != "Chauffeurs" && this._NomSatelilte != "Commercial" && this._NomSatelilte != "Préparateurs" && this._NomSatelilte != "Releveurs")
            {
              
                if (string.IsNullOrEmpty(gridV.GetFocusedRowCellDisplayText("Libellé")))
                {
                    msgEchoue = "Opération d'enregistrement a échoué!! Veuillez saisir un libellé";
                    warning(msgEchoue, true);
                    return;
                }
            }
            try
            {
                switch (this._NomSatelilte.Trim())
                {
                    case "Banques":
                        {
                            string cPays = null;
                            Banque banque = new Banque();
                            banque.Code = gridV.GetFocusedRowCellDisplayText("Code").ToUpper();
                            banque.Libelle = gridV.GetFocusedRowCellDisplayText("Libellé").ToUpper();
                            banque.CPostal = gridV.GetFocusedRowCellDisplayText("Code Postal");
                            banque.Ville = gridV.GetFocusedRowCellDisplayText("Ville ");
                            banque.LibAdresse = gridV.GetFocusedRowCellDisplayText("Adresse");
                            banque.CodeBic = gridV.GetFocusedRowCellDisplayText("Code Bic");
                            banque.IBAN = gridV.GetFocusedRowCellDisplayText("IBAN");
                            banque.Initiale = gridV.GetFocusedRowCellDisplayText("Initiale");
                            if (!string.IsNullOrEmpty(gridV.GetFocusedRowCellDisplayText("Pays")))
                                cPays = gridV.GetFocusedRowCellValue("Pays").ToString();
                            banque.CPays = cPays;

                            banque.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            banque.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            banque.PCInsertion = Environment.MachineName;
                            banque.PCModification = Environment.MachineName;
                            if (string.IsNullOrWhiteSpace(banque.Initiale))
                                throw (new Exception("Opération d'enregistrement a échoué!!"));
                            banque.Sauvegarder();

                            break;
                        }
                    case "Agences":
                        {
                            Agence agence = new Agence();
                            agence.Code = gridV.GetFocusedRowCellDisplayText("Code").ToUpper();
                            agence.Libelle = gridV.GetFocusedRowCellDisplayText("Libellé").ToUpper();
                            agence.CBanque = gridV.GetFocusedRowCellValue("Banque").ToString();
                            agence.Adresses = gridV.GetFocusedRowCellDisplayText("Adresse");
                            agence.Tel = gridV.GetFocusedRowCellDisplayText("Tel");
                            agence.Fax = gridV.GetFocusedRowCellDisplayText("fax");
                            agence.Email = gridV.GetFocusedRowCellDisplayText("Email");
                            agence.CAgenceBanque = agence.CBanque + agence.Code;
                            agence.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            agence.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            agence.PCInsertion = Environment.MachineName;
                            agence.PCModification = Environment.MachineName;
                            agence.Responsable = gridV.GetFocusedRowCellDisplayText("Responsable");
                            agence.Sauvegarder();

                            break;
                        }

                    case "Modes Règlements":
                        {
                            ModeReglement reglement = new ModeReglement();
                            reglement.Code = gridV.GetFocusedRowCellDisplayText("Code");
                            reglement.Libelle = gridV.GetFocusedRowCellDisplayText("Libellé");

                            reglement.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            reglement.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            reglement.PCInsertion = Environment.MachineName;
                            reglement.PCModification = Environment.MachineName;

                            reglement.BEcheance = gridV.GetFocusedRowCellValue("Echéance").ToString().Equals("True");
                            reglement.BMobile = gridV.GetFocusedRowCellValue("Mobile").ToString().Equals("True");
                            reglement.Sauvegarder();
                            break;
                        }

                    case "Civilités":
                        {
                            Civilite civilite = new Civilite();
                            civilite.Code = gridV.GetFocusedRowCellDisplayText("Code");
                            civilite.Libelle = gridV.GetFocusedRowCellValue("Libellé").ToString();

                            civilite.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            civilite.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            civilite.PCInsertion = Environment.MachineName;
                            civilite.PCModification = Environment.MachineName;

                            civilite.Sauvegarder();
                            break;
                        }
                    case "Natures Tiers":
                        {
                            NatureTiers natureTiers = new NatureTiers();
                            try
                            {
                                natureTiers.CNatureTiers = int.Parse(gridV.GetFocusedRowCellDisplayText("Code"));
                            }
                            catch (Exception)
                            {
                                msgEchoue = "Opération de modification a échoué: le code ne peut pas contenir des lettres!!! Veuillez saisir un autre code ";
                                warning(msgEchoue, true);
                            }

                            natureTiers.Libelle = gridV.GetFocusedRowCellDisplayText("Libellé"); ;

                            natureTiers.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            natureTiers.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            natureTiers.PCInsertion = Environment.MachineName;
                            natureTiers.PCModification = Environment.MachineName;

                            natureTiers.Sauvegarder();
                            break;
                        }

                    case "Familles Clients":
                        {
                            ClientFamille clientFamille = new ClientFamille();
                            clientFamille.Code = gridV.GetFocusedRowCellDisplayText("Code");
                            clientFamille.Libelle = gridV.GetFocusedRowCellDisplayText("Libellé");

                            clientFamille.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            clientFamille.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            clientFamille.PCInsertion = Environment.MachineName;
                            clientFamille.PCModification = Environment.MachineName;

                            clientFamille.Sauvegarder();
                            break;
                        }

                    case "Commercial":
                        {
                            Commercial commercial = new Commercial();

                            try
                            { commercial.CCommercial = int.Parse(gridV.GetFocusedRowCellDisplayText("Code")); }
                            catch
                            {
                                msgEchoue = "Opération d'enregistrement a échoué!! Le code doit être un entier";
                                gridV.DeleteRow(gridV.FocusedRowHandle);
                                warning(msgEchoue, true);
                                return;
                            }
                            // commercial.Libelle = gridV.GetFocusedRowCellDisplayText("Libellé");
                            commercial.CUtilisateur = gridV.GetFocusedRowCellDisplayText("CUtilisateur");
                            commercial.Email = gridV.GetFocusedRowCellDisplayText("Email");
                            if (string.IsNullOrEmpty(gridV.GetFocusedRowCellDisplayText("Nom")))
                            {
                                msgEchoue = "Opération d'enregistrement a échoué!! Veuillez saisir le nom";
                                warning(msgEchoue, true);
                                return;
                            }
                            else
                                commercial.Nom = gridV.GetFocusedRowCellDisplayText("Nom");
                            commercial.Portable = gridV.GetFocusedRowCellDisplayText("Portable");
                            commercial.Prenom = gridV.GetFocusedRowCellDisplayText("Prénom");
                            commercial.Telephone = gridV.GetFocusedRowCellDisplayText("Tèlèphone");
                            //  int i = commercial.Libelle.IndexOf(" ");

                            commercial.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            commercial.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            commercial.PCInsertion = Environment.MachineName;
                            commercial.PCModification = Environment.MachineName;

                            commercial.Sauvegarder();

                            break;
                        }
                    case "Gratuites":
                        {
                            Gratuites gratuites = new Gratuites();
                            int D1=0, D2=0;
                            gratuites.CGratuites = gridV.GetFocusedRowCellDisplayText("Code");
                            gratuites.LibGratuites = gridV.GetFocusedRowCellDisplayText("Libellé");
                            int.TryParse(gridV.GetFocusedRowCellDisplayText("Dividende"), out D1);
                            gratuites.Dividende = D1;
                            int.TryParse(gridV.GetFocusedRowCellDisplayText("Diviseur"), out D2);
                            gratuites.Diviseur = D2;
                            gratuites.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            gratuites.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            gratuites.PCInsertion = Environment.MachineName;
                            gratuites.PCModification = Environment.MachineName;

                            gratuites.Sauvegarder();
                            break;
                        }
                    case "Type bon d'achat":
                        {
                            TypeBonAchat tbachat = new TypeBonAchat();
                            tbachat.CTBAchat = gridV.GetFocusedRowCellDisplayText("Code");
                            tbachat.LibTBAchat = gridV.GetFocusedRowCellDisplayText("Libellé");
                            tbachat.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            tbachat.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            tbachat.PCInsertion = Environment.MachineName;
                            tbachat.PCModification = Environment.MachineName;

                            tbachat.Sauvegarder();
                            break;
                        }
                    case "Type bon commande":
                        {
                            TypeBonCommande tbc = new TypeBonCommande();
                            tbc.CTBC = gridV.GetFocusedRowCellDisplayText("Code");
                            tbc.LibTBCommande = gridV.GetFocusedRowCellDisplayText("Libellé");
                            tbc.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            tbc.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            tbc.PCInsertion = Environment.MachineName;
                            tbc.PCModification = Environment.MachineName;

                            tbc.Sauvegarder();
                            break;
                        }
                    case "Type options":
                        {
                            TypeOptions toptions = new TypeOptions();
                            toptions.CTypeOptions = gridV.GetFocusedRowCellDisplayText("Code");
                            toptions.LibTypeOptions = gridV.GetFocusedRowCellDisplayText("Libellé");
                            toptions.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            toptions.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            toptions.PCInsertion = Environment.MachineName;
                            toptions.PCModification = Environment.MachineName;

                            toptions.Sauvegarder();
                            break;
                        }
                    case "Options":
                        {
                            Options options = new Options();
                            options.COptions = gridV.GetFocusedRowCellDisplayText("Code");
                            options.LibOptions = gridV.GetFocusedRowCellDisplayText("Libellé");
                            options.CTypeOptions = gridV.GetFocusedRowCellValue("Type option").ToString();
                            int ordre =0;
                            if(int.TryParse( gridV.GetFocusedRowCellValue("Ordre") != null ? gridV.GetFocusedRowCellValue("Ordre").ToString(): "0", out ordre))
                                options.Ordre = ordre;
                            options.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            options.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            options.PCInsertion = Environment.MachineName;
                            options.PCModification = Environment.MachineName;

                            options.Sauvegarder();
                            break;
                        }
                    case "Etat":
                        {
                            Etat etat = new Etat();
                            etat.CEtat = gridV.GetFocusedRowCellDisplayText("Code");
                            etat.LibEtat = gridV.GetFocusedRowCellDisplayText("Libellé");
                            etat.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            etat.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            etat.PCInsertion = Environment.MachineName;
                            etat.PCModification = Environment.MachineName;

                            etat.Sauvegarder();
                            break;
                        }
                    case "Préparateurs":
                        {
                            Preparateur Preparateur = new Preparateur();

                            try
                            { Preparateur.CPreparateur = int.Parse(gridV.GetFocusedRowCellDisplayText("Code")); }
                            catch
                            {
                                msgEchoue = "Opération d'enregistrement a échoué!! Le code doit être un entier";
                                gridV.DeleteRow(gridV.FocusedRowHandle);
                                warning(msgEchoue, true);
                                return;
                            }
                            // commercial.Libelle = gridV.GetFocusedRowCellDisplayText("Libellé");
                            Preparateur.CUtilisateur = gridV.GetFocusedRowCellDisplayText("CUtilisateur");
                            Preparateur.Email = gridV.GetFocusedRowCellDisplayText("Email");
                            if (string.IsNullOrEmpty(gridV.GetFocusedRowCellDisplayText("Nom")))
                            {
                                msgEchoue = "Opération d'enregistrement a échoué!! Veuillez saisir le nom";
                                warning(msgEchoue, true);
                                return;
                            }
                            else
                                Preparateur.Nom = gridV.GetFocusedRowCellDisplayText("Nom");
                            Preparateur.Portable = gridV.GetFocusedRowCellDisplayText("Portable");
                            Preparateur.Prenom = gridV.GetFocusedRowCellDisplayText("Prénom");
                            Preparateur.Telephone = gridV.GetFocusedRowCellDisplayText("Tèlèphone");
                            //  int i = commercial.Libelle.IndexOf(" ");

                            Preparateur.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            Preparateur.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            Preparateur.PCInsertion = Environment.MachineName;
                            Preparateur.PCModification = Environment.MachineName;

                            Preparateur.Sauvegarder();

                            break;
                        }
                    case "Releveurs":
                        {
                            Releveur Releveur = new Releveur();

                            if (string.IsNullOrEmpty(gridV.GetFocusedRowCellDisplayText("Code")))
                            {
                                msgEchoue = "Opération d'enregistrement a échoué!! Veuillez saisir le code";
                                warning(msgEchoue, true);
                                return;
                            }
                            else
                                Releveur.CReleveur = gridV.GetFocusedRowCellDisplayText("Code");
                            if (string.IsNullOrEmpty(gridV.GetFocusedRowCellDisplayText("Nom")))
                            {
                                msgEchoue = "Opération d'enregistrement a échoué!! Veuillez saisir le nom";
                                warning(msgEchoue, true);
                                return;
                            }
                            else
                                Releveur.Nom = gridV.GetFocusedRowCellDisplayText("Nom");
                            Releveur.Prenom = gridV.GetFocusedRowCellDisplayText("Prénom");

                            Releveur.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            Releveur.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            Releveur.PCInsertion = Environment.MachineName;
                            Releveur.PCModification = Environment.MachineName;

                            Releveur.Sauvegarder();

                            break;
                        }
                    case "Familles Fournisseurs":
                        {
                            FournisseurFamille fournisseurFamille = new FournisseurFamille();
                            fournisseurFamille.Code = gridV.GetFocusedRowCellDisplayText("Code");
                            fournisseurFamille.Libelle = gridV.GetFocusedRowCellDisplayText("Libellé");

                            fournisseurFamille.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            fournisseurFamille.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            fournisseurFamille.PCInsertion = Environment.MachineName;
                            fournisseurFamille.PCModification = Environment.MachineName;

                            fournisseurFamille.Sauvegarder();
                            break;
                        }
                    case "Familles Articles":
                        {
                            ArticleFamille articleFamille = new ArticleFamille();
                            articleFamille.Code = gridV.GetFocusedRowCellDisplayText("Code");
                            articleFamille.Libelle = gridV.GetFocusedRowCellDisplayText("Libellé");
                            articleFamille.BActive = gridV.GetFocusedRowCellValue("Active").ToString().Equals("True");

                            int ordre = 0;
                            if (int.TryParse(gridV.GetFocusedRowCellValue("Ordre") != null ? gridV.GetFocusedRowCellValue("Ordre").ToString() : "0", out ordre))
                                articleFamille.Ordre = ordre;

                            articleFamille.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            articleFamille.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            articleFamille.PCInsertion = Environment.MachineName;
                            articleFamille.PCModification = Environment.MachineName;

                            articleFamille.Sauvegarder();
                            break;
                        }
                    case "Catégories Articles":
                        {
                            ArticleCategorie articleCategorie = new ArticleCategorie();
                            articleCategorie.Code = gridV.GetFocusedRowCellDisplayText("Code");
                            articleCategorie.Libelle = gridV.GetFocusedRowCellDisplayText("Libellé");

                            articleCategorie.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            articleCategorie.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            articleCategorie.PCInsertion = Environment.MachineName;
                            articleCategorie.PCModification = Environment.MachineName;

                            articleCategorie.Sauvegarder();
                            break;
                        }
                    case "Types Articles":
                        {
                            ArticleType articleType = new ArticleType();
                            articleType.Code = gridV.GetFocusedRowCellDisplayText("Code");
                            articleType.Libelle = gridV.GetFocusedRowCellDisplayText("Libellé");
                            articleType.BActive = (bool)gridV.GetFocusedRowCellValue("Active");//.ToString().Equals("True");
                            int ordre = 0;
                            if (int.TryParse(gridV.GetFocusedRowCellValue("Ordre") != null ? gridV.GetFocusedRowCellValue("Ordre").ToString() : "0", out ordre))
                                articleType.Ordre = ordre;

                            articleType.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            articleType.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            articleType.PCInsertion = Environment.MachineName;
                            articleType.PCModification = Environment.MachineName;

                            articleType.Sauvegarder();
                            break;
                        }
                    case "État Article":
                        {
                            EtatArticle etat = new EtatArticle();
                            etat.CEtatArticle = gridV.GetFocusedRowCellDisplayText("Code");
                            etat.LibEtatArticle = gridV.GetFocusedRowCellDisplayText("Libellé");
                            var color = gridV.GetFocusedRowCellValue("Color");
                            if (color == null || color is DBNull)
                                color = System.Drawing.Color.Black;
                            etat.Color = String.Format("#{0:X2}{1:X2}{2:X2}", ((System.Drawing.Color)color).R, ((System.Drawing.Color)color).G, ((System.Drawing.Color)color).B);
                            etat.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            etat.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            etat.PCInsertion = Environment.MachineName;
                            etat.PCModification = Environment.MachineName;

                            etat.Sauvegarder();
                            break;
                        }

                    case "Modèles":
                        {
                            ArticleModele articleModele = new ArticleModele();
                            articleModele.Code = gridV.GetFocusedRowCellDisplayText("Code");
                            articleModele.Libelle = gridV.GetFocusedRowCellDisplayText("Libellé");

                            articleModele.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            articleModele.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            articleModele.PCInsertion = Environment.MachineName;
                            articleModele.PCModification = Environment.MachineName;

                            articleModele.Sauvegarder();
                            break;
                        }
                    case "Sous Modèles1":
                        {
                            ArticleSousModele1 articleSousModele1 = new ArticleSousModele1();
                            articleSousModele1.Code = gridV.GetFocusedRowCellDisplayText("Code");
                            articleSousModele1.Libelle = gridV.GetFocusedRowCellDisplayText("Libellé");
                            articleSousModele1.CModeleArticle = gridV.GetFocusedRowCellValue("Modèle").ToString();

                            articleSousModele1.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            articleSousModele1.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            articleSousModele1.PCInsertion = Environment.MachineName;
                            articleSousModele1.PCModification = Environment.MachineName;

                            articleSousModele1.Sauvegarder();
                            break;
                        }
                    case "Sous Modèles2":
                        {
                            ArticleSousModele2 articleSousModele2 = new ArticleSousModele2();
                            articleSousModele2.Code = gridV.GetFocusedRowCellDisplayText("Code");
                            articleSousModele2.Libelle = gridV.GetFocusedRowCellDisplayText("Libellé");
                            articleSousModele2.CModeleArticle = gridV.GetFocusedRowCellValue("Modèle").ToString();
                            articleSousModele2.CSousModele1Article = gridV.GetFocusedRowCellValue("Sous Modèle 1").ToString();

                            articleSousModele2.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            articleSousModele2.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            articleSousModele2.PCInsertion = Environment.MachineName;
                            articleSousModele2.PCModification = Environment.MachineName;

                            articleSousModele2.Sauvegarder();
                            break;
                        }

                    case "Natures Articles":
                        {
                            ArticleNature articleNature = new ArticleNature();
                            articleNature.Code = gridV.GetFocusedRowCellDisplayText("Code");
                            articleNature.Libelle = gridV.GetFocusedRowCellDisplayText("Libellé");

                            articleNature.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            articleNature.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            articleNature.PCInsertion = Environment.MachineName;
                            articleNature.PCModification = Environment.MachineName;

                            articleNature.Sauvegarder();

                            break;
                        }

                    case "Natures Vente":
                        {
                            ArticleNatureVente articleNatureVente = new ArticleNatureVente();
                            articleNatureVente.Code = gridV.GetFocusedRowCellDisplayText("Code");
                            articleNatureVente.Libelle = gridV.GetFocusedRowCellDisplayText("Libellé");

                            articleNatureVente.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            articleNatureVente.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            articleNatureVente.PCInsertion = Environment.MachineName;
                            articleNatureVente.PCModification = Environment.MachineName;

                            articleNatureVente.Sauvegarder();

                            break;
                        }

                    case "Emballages":
                        {
                            Emballage emballage = new Emballage();
                            emballage.Code = gridV.GetFocusedRowCellDisplayText("Code");
                            emballage.Libelle = gridV.GetFocusedRowCellDisplayText("Libellé");
                            emballage.Quantite = int.Parse(gridV.GetFocusedRowCellDisplayText("Quantité"));

                            emballage.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            emballage.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            emballage.PCInsertion = Environment.MachineName;
                            emballage.PCModification = Environment.MachineName;

                            emballage.Sauvegarder();
                            break;
                        }
                    case "Unités":
                        {
                            Unite unite = new Unite();
                            unite.Code = gridV.GetFocusedRowCellDisplayText("Code");
                            unite.Libelle = gridV.GetFocusedRowCellDisplayText("Libellé");
                            if (!string.IsNullOrEmpty(gridV.GetFocusedRowCellDisplayText("NbreDecimal")))
                                unite.NombreDecimaleUnite = decimal.Parse(gridV.GetFocusedRowCellDisplayText("NbreDecimal"));

                            unite.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            unite.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            unite.PCInsertion = Environment.MachineName;
                            unite.PCModification = Environment.MachineName;

                            unite.Sauvegarder();
                            break;
                        }

                    case "Tarifs":
                        {
                            Tarif tarif = new Tarif();
                            tarif.Code = gridV.GetFocusedRowCellDisplayText("Code");
                            tarif.Libelle = gridV.GetFocusedRowCellDisplayText("Libellé");

                            tarif.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            tarif.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            tarif.PCInsertion = Environment.MachineName;
                            tarif.PCModification = Environment.MachineName;

                            tarif.Sauvegarder();
                            break;
                        }
                    case "Entrepôts":
                        {
                            Entrepot entrepot = new Entrepot();
                            entrepot.Code = gridV.GetFocusedRowCellDisplayText("Code");
                            entrepot.Libelle = gridV.GetFocusedRowCellDisplayText("Libellé");
                            entrepot.AdresseEntrepot = gridV.GetFocusedRowCellDisplayText("Adresse");
                            entrepot.TypeEntrepot = gridV.GetFocusedRowCellDisplayText("Fixe");

                            string flag = gridV.GetFocusedRowCellValue("Principale").ToString();
                            if (flag.Equals("True"))
                                entrepot.BParDefault = true;
                            else
                                entrepot.BParDefault = false;

                            string flag2 = gridV.GetFocusedRowCellValue("Livrable").ToString();
                            if (flag2.Equals("True"))
                                entrepot.BLivrable = true;
                            else
                                entrepot.BLivrable = false;
                            
                            entrepot.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            entrepot.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            entrepot.PCInsertion = Environment.MachineName;
                            entrepot.PCModification = Environment.MachineName;

                            entrepot.Sauvegarder();
                            break;
                        }
                    case "Régions":
                        {
                            Region region = new Region();
                            region.Code = gridV.GetFocusedRowCellDisplayText("Code");
                            region.Libelle = gridV.GetFocusedRowCellDisplayText("Libellé");
                            region.BActive = (bool)gridV.GetFocusedRowCellValue("Active");
                            region.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            region.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            region.PCInsertion = Environment.MachineName;
                            region.PCModification = Environment.MachineName;

                            region.Sauvegarder();
                            break;
                        }
                    case "Gouvernorat":
                        {
                            Gouvernorat gouvernorat = new Gouvernorat();
                            gouvernorat.Code = gridV.GetFocusedRowCellDisplayText("Code");
                            gouvernorat.Libelle = gridV.GetFocusedRowCellDisplayText("Libellé");

                            gouvernorat.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            gouvernorat.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            gouvernorat.PCInsertion = Environment.MachineName;
                            gouvernorat.PCModification = Environment.MachineName;

                            gouvernorat.Sauvegarder();
                            break;
                        }
                    case "Pays":
                        {
                            Pays pays = new Pays();
                            pays.Code = gridV.GetFocusedRowCellDisplayText("Code");
                            pays.Libelle = gridV.GetFocusedRowCellDisplayText("Libellé");

                            pays.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            pays.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            pays.PCInsertion = Environment.MachineName;
                            pays.PCModification = Environment.MachineName;

                            pays.Sauvegarder();
                            break;
                        }
                    case "Voitures":
                        {
                            Vehicule vehicule = new Vehicule();
                            vehicule.Code = gridV.GetFocusedRowCellDisplayText("Code");
                            vehicule.Libelle = gridV.GetFocusedRowCellDisplayText("Libellé");
                            vehicule.NumeroSerie = gridV.GetFocusedRowCellDisplayText("N° de Série");
                            vehicule.BActif = gridV.GetFocusedRowCellValue("Active").ToString().Equals("True");
                            if(!string.IsNullOrEmpty( gridV.GetFocusedRowCellDisplayText("Charge Maximale par Kg")))
                                vehicule.ChargeMax = decimal.Parse(gridV.GetFocusedRowCellDisplayText("Charge Maximale par Kg"));
                            vehicule.BDisponible = true;

                            vehicule.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            vehicule.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            vehicule.PCInsertion = Environment.MachineName;
                            vehicule.PCModification = Environment.MachineName;

                            vehicule.Sauvegarder();
                            break;
                        }
                    case "Chauffeurs":
                        {
                            Chauffeur chauffeur = new Chauffeur();
                            chauffeur.Code = gridV.GetFocusedRowCellDisplayText("Code");
                            if (string.IsNullOrEmpty(gridV.GetFocusedRowCellDisplayText("Nom")))
                            {
                                msgEchoue = "Opération d'enregistrement a échoué!! Veuillez saisir le nom";
                                warning(msgEchoue, false);
                                return;
                            }
                            else
                                chauffeur.Nom = gridV.GetFocusedRowCellDisplayText("Nom");
                            chauffeur.Prenom = gridV.GetFocusedRowCellDisplayText("Prénom");
                            chauffeur.CIN = gridV.GetFocusedRowCellDisplayText("C.I.N"); ;

                            chauffeur.RFID_Chauf = gridV.GetFocusedRowCellDisplayText("RFID_Chauf");
                            chauffeur.BExterne = (gridV.GetFocusedRowCellValue("Externe").ToString()).Equals("True");

                            chauffeur.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            chauffeur.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            chauffeur.PCInsertion = Environment.MachineName;
                            chauffeur.PCModification = Environment.MachineName;

                            chauffeur.Sauvegarder();
                            break;
                        }
                    case "Villes":
                        {
                            Ville ville = new Ville();
                            ville.Code = gridV.GetFocusedRowCellDisplayText("Code");
                            ville.Libelle = gridV.GetFocusedRowCellDisplayText("Libellé");

                            ville.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            ville.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            ville.PCInsertion = Environment.MachineName;
                            ville.PCModification = Environment.MachineName;

                            ville.Sauvegarder();
                            break;
                        }

                    case "Types Convention":
                        {
                            TypeConvention typeConvention = new TypeConvention();
                            typeConvention.Code = gridV.GetFocusedRowCellDisplayText("Code");
                            typeConvention.Libelle = gridV.GetFocusedRowCellDisplayText("Libellé");

                            typeConvention.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            typeConvention.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            typeConvention.PCInsertion = Environment.MachineName;
                            typeConvention.PCModification = Environment.MachineName;

                            typeConvention.Sauvegarder();
                            break;
                        }
                    case "Objectif":
                        {
                            TypeVisite typeVisite = new TypeVisite();
                            typeVisite.CTypeVisite = gridV.GetFocusedRowCellDisplayText("Code");
                            typeVisite.Libelle = gridV.GetFocusedRowCellDisplayText("Libellé");
                            typeVisite.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            typeVisite.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            typeVisite.PCInsertion = Environment.MachineName;
                            typeVisite.PCModification = Environment.MachineName;
                            typeVisite.Sauvegarder();
                            break;
                        }
                    case "Motif":
                        {
                            Motif motif = new Motif();
                            motif.CMotif = gridV.GetFocusedRowCellDisplayText("Code");
                            motif.Libelle = gridV.GetFocusedRowCellDisplayText("Libellé");
                            motif.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            motif.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            motif.PCInsertion = Environment.MachineName;
                            motif.PCModification = Environment.MachineName;
                            motif.Sauvegarder();
                            break;
                        }
                    case "Etablissement":
                        {
                            Etablissement etablissement = new Etablissement();
                            etablissement.Code = gridV.GetFocusedRowCellDisplayText("Code");
                            etablissement.Libelle = gridV.GetFocusedRowCellDisplayText("Libellé");
                            etablissement.CClient = gridV.GetFocusedRowCellDisplayText("Client");
                            if (!string.IsNullOrEmpty(gridV.GetFocusedRowCellDisplayText("Region")))
                                etablissement.CRegion = gridV.GetFocusedRowCellValue("Region").ToString();
                            etablissement.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            etablissement.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            etablissement.PCInsertion = Environment.MachineName;
                            etablissement.PCModification = Environment.MachineName;
                            etablissement.Sauvegarder();
                            break;
                        }
                    case "Jours fériés":
                        {
                            JoursFeries jf = new JoursFeries();
                            int id = -1; DateTime date = DateTime.Now.Date;
                            int.TryParse(gridV.GetFocusedRowCellDisplayText("Code"), out id);
                            jf.IDJFerie = id;
                            jf.LibJFerie = gridV.GetFocusedRowCellDisplayText("Libellé");
                            DateTime.TryParse(gridV.GetFocusedRowCellDisplayText("Date"), out date);
                            jf.DateJFerie = date;
                            jf.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            jf.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            jf.PCInsertion = Environment.MachineName;
                            jf.PCModification = Environment.MachineName;
                            jf.Sauvegarder();
                            break;
                        }

                    case "Modems":
                        {
                            Modem m = new Modem();
                            m.IMEI = gridV.GetFocusedRowCellDisplayText("IMEI");
                            m.Model_Modem = gridV.GetFocusedRowCellDisplayText("Modele");
                            m.Num_SIM = gridV.GetFocusedRowCellDisplayText("Numero SIM");
                            m.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            m.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            m.PCInsertion = Environment.MachineName;
                            m.PCModification = Environment.MachineName;
                            m.Sauvegarder();
                            break;
                        }

                    case "Autobus":
                        {
                            Bus bus = new Bus();
                            bus.Num_IMM = gridV.GetFocusedRowCellDisplayText("Immatriculation");
                            bus.Model_Bus = gridV.GetFocusedRowCellDisplayText("Model Bus");
                            bus.IMEI = gridV.GetFocusedRowCellDisplayText("IMEI");
                            bus.APP_Sagem = gridV.GetFocusedRowCellValue("APP Sagem").ToString().Equals("True");
                            
                            if (int.Parse(gridV.GetFocusedRowCellDisplayText("Capacite Bus")) > 0)
                            {
                                bus.Capacite_Bus = int.Parse(gridV.GetFocusedRowCellDisplayText("Capacite Bus"));
                            }
                            else
                            {
                                warning("La capacité de bus doit être supérieur à 0", true);
                                return;
                            }
                            bus.Code_Circuit = gridV.GetFocusedRowCellValue("Code Circuit").ToString();
                            bus.CChauffeur = gridV.GetFocusedRowCellValue("Chauffeur").ToString(); 
                            
                            bus.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            bus.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                            bus.PCInsertion = Environment.MachineName;
                            bus.PCModification = Environment.MachineName;

                            bus.Sauvegarder();
                            break;
                        }                      
                    default:
                        break;
                }

                this.ChargerEntite(this._NomSatelilte);

                warning(msgSucces, false);
            }
            catch (Exception)
            {
                warning(msgEchoue, true);
            }
        }

        private void Supprimer()
        {
            string msgEchoue = "Impossible de supprimer cet élément! ";
            string msgSucces = "Suppression effectuée avec succès!";

            var dialogResult = XtraMessageBox.Show("Voulez-vous Vraiment supprimer cet Elément?",
                "",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (dialogResult != DialogResult.Yes)
                return;

            try
            {
                switch (this._NomSatelilte.Trim())
                {
                    case "Banques":
                        {
                            Banque banque = new Banque();
                            banque.Code = this.gridV.GetFocusedRowCellDisplayText(gridV.Columns[0]);
                            banque.Supprimer();
                            break;
                        }
                    case "Agences":
                        {
                            Agence agence = new Agence();
                            agence.CAgenceBanque = this.gridV.GetFocusedRowCellValue(gridV.Columns["Banque"]).ToString() + this.gridV.GetFocusedRowCellDisplayText("Code");
                            agence.Supprimer();
                            break;
                        }
                    case "Modes Règlements":
                        {
                            ModeReglement reglement = new ModeReglement();
                            reglement.Code = this.gridV.GetFocusedRowCellDisplayText(gridV.Columns[0]);
                            reglement.Supprimer();

                            break;
                        }

                    case "Civilités":
                        {
                            Civilite civilite = new Civilite();
                            civilite.Code = this.gridV.GetFocusedRowCellDisplayText(gridV.Columns[0]);
                            civilite.Supprimer();
                            break;
                        }
                    case "Natures Tiers":
                        {
                            NatureTiers natureTiers = new NatureTiers();
                            natureTiers.CNatureTiers = int.Parse(this.gridV.GetFocusedRowCellDisplayText(gridV.Columns[0]));
                            natureTiers.Supprimer();
                            break;
                        }

                    case "Familles Clients":
                        {
                            ClientFamille clientFamille = new ClientFamille();
                            clientFamille.Code = this.gridV.GetFocusedRowCellDisplayText(gridV.Columns[0]);
                            clientFamille.Supprimer();
                            break;
                        }

                    case "Commercial":
                        {
                            Commercial commercial = new Commercial();
                            commercial.CCommercial = int.Parse(this.gridV.GetFocusedRowCellDisplayText(gridV.Columns[0]));
                            commercial.Supprimer();

                            break;
                        }
                    case "Gratuites":
                        {
                            Gratuites gratuites = new Gratuites();
                            gratuites.CGratuites = this.gridV.GetFocusedRowCellDisplayText(gridV.Columns[0]);
                            gratuites.Supprimer();

                            break;
                        }
                    case "Type bon d'achat":
                        {
                            TypeBonAchat tbachat = new TypeBonAchat();
                            tbachat.CTBAchat = this.gridV.GetFocusedRowCellDisplayText(gridV.Columns[0]);
                            tbachat.Supprimer();

                            break;
                        }
                    case "Type bon commande":
                        {
                            TypeBonCommande tbc = new TypeBonCommande();
                            tbc.CTBC = this.gridV.GetFocusedRowCellDisplayText(gridV.Columns[0]);
                            tbc.Supprimer();

                            break;
                        }
                    case "Type options":
                        {
                            TypeOptions toptions = new TypeOptions();
                            toptions.CTypeOptions = this.gridV.GetFocusedRowCellDisplayText(gridV.Columns[0]);
                            toptions.Supprimer();

                            break;
                        }
                    case "Options":
                        {
                            Options options = new Options();
                            options.COptions = this.gridV.GetFocusedRowCellDisplayText(gridV.Columns[0]);
                            options.Supprimer();

                            break;
                        }
                    case "Etat":
                        {
                            Etat etat = new Etat();
                            etat.CEtat = this.gridV.GetFocusedRowCellDisplayText(gridV.Columns[0]);
                            etat.Supprimer();

                            break;
                        }
                    case "Préparateurs":
                        {
                            Preparateur Preparateur = new Preparateur();
                            Preparateur.CPreparateur = int.Parse(this.gridV.GetFocusedRowCellDisplayText(gridV.Columns[0]));
                            Preparateur.Supprimer();

                            break;
                        }
                    case "Releveurs":
                        {
                            Releveur Releveur = new Releveur();
                            Releveur.CReleveur = this.gridV.GetFocusedRowCellDisplayText(gridV.Columns[0]);
                            Releveur.Supprimer();

                            break;
                        }
                    case "Familles Fournisseurs":
                        {
                            FournisseurFamille fournisseurFamille = new FournisseurFamille();
                            fournisseurFamille.Code = this.gridV.GetFocusedRowCellDisplayText(gridV.Columns[0]);
                            fournisseurFamille.Supprimer();
                            break;
                        }
                    case "Familles Articles":
                        {
                            ArticleFamille articleFamille = new ArticleFamille();
                            articleFamille.Code = this.gridV.GetFocusedRowCellDisplayText(gridV.Columns[0]);
                            articleFamille.Supprimer();
                            break;
                        }
                    case "Catégories Articles":
                        {
                            ArticleCategorie articleCategorie = new ArticleCategorie();
                            articleCategorie.Code = gridV.GetFocusedRowCellDisplayText("Code");
                            articleCategorie.Libelle = gridV.GetFocusedRowCellDisplayText("Libellé");
                            articleCategorie.Supprimer();
                            break;
                        }
                    case "Types Articles":
                        {
                            ArticleType articleType = new ArticleType();
                            articleType.Code = this.gridV.GetFocusedRowCellDisplayText(gridV.Columns[0]);
                            articleType.Supprimer();
                            break;
                        }
                    case "État Article":
                        {
                            EtatArticle etat = new EtatArticle();
                            etat.CEtatArticle = this.gridV.GetFocusedRowCellDisplayText("Code");
                            etat.Supprimer();
                            break;
                        }

                    case "Modèles":
                        {
                            ArticleModele articleModele = new ArticleModele();
                            articleModele.Code = this.gridV.GetFocusedRowCellDisplayText(gridV.Columns[0]);
                            articleModele.Supprimer();
                            break;
                        }
                    case "Sous Modèles1":
                        {
                            ArticleSousModele1 articleSousModele1 = new ArticleSousModele1();
                            articleSousModele1.Code = this.gridV.GetFocusedRowCellDisplayText(gridV.Columns[0]);
                            articleSousModele1.Supprimer();
                            break;
                        }
                    case "Sous Modèles2":
                        {
                            ArticleSousModele2 articleSousModele2 = new ArticleSousModele2();
                            articleSousModele2.Code = this.gridV.GetFocusedRowCellDisplayText(gridV.Columns[0]);
                            articleSousModele2.Supprimer();
                            break;
                        }

                    case "Natures Articles":
                        {
                            ArticleNature articleNature = new ArticleNature();
                            articleNature.Code = this.gridV.GetFocusedRowCellDisplayText(gridV.Columns[0]);
                            articleNature.Supprimer();

                            break;
                        }

                    case "Natures Vente":
                        {
                            ArticleNatureVente articleNatureVente = new ArticleNatureVente();
                            articleNatureVente.Code = this.gridV.GetFocusedRowCellDisplayText(gridV.Columns[0]);
                            articleNatureVente.Supprimer();

                            break;
                        }

                    case "Emballages":
                        {
                            Emballage emballage = new Emballage();
                            emballage.Code = this.gridV.GetFocusedRowCellDisplayText(gridV.Columns[0]);
                            emballage.Supprimer();
                            break;
                        }
                    case "Unités":
                        {
                            Unite unite = new Unite();
                            unite.Code = this.gridV.GetFocusedRowCellDisplayText(gridV.Columns[0]);
                            unite.Supprimer();
                            break;
                        }

                    case "Tarifs":
                        {
                            Tarif tarif = new Tarif();
                            tarif.Code = this.gridV.GetFocusedRowCellDisplayText(gridV.Columns[0]);
                            tarif.Supprimer();
                            break;
                        }
                    case "Entrepôts":
                        {
                            Entrepot entrepot = new Entrepot();
                            entrepot.Code = this.gridV.GetFocusedRowCellDisplayText(gridV.Columns[0]);
                            entrepot.Supprimer();
                            break;
                        }
                    case "Régions":
                        {
                            Region region = new Region();
                            region.Code = this.gridV.GetFocusedRowCellDisplayText(gridV.Columns[0]);
                            region.Supprimer();
                            break;
                        }
                    case "Gouvernorat":
                        {
                            Gouvernorat gouvernorat = new Gouvernorat();
                            gouvernorat.Code = this.gridV.GetFocusedRowCellDisplayText(gridV.Columns[0]);
                            gouvernorat.Supprimer();
                            break;
                        }
                    case "Pays":
                        {
                            Pays.Supprimer(this.gridV.GetFocusedRowCellDisplayText(gridV.Columns[0]));
                            break;
                        }
                    case "Voitures":
                        {
                            Vehicule vehicule = new Vehicule();
                            vehicule.Code = this.gridV.GetFocusedRowCellDisplayText(gridV.Columns[0]);
                            vehicule.Supprimer();
                            break;
                        }
                    case "Chauffeurs":
                        {
                            Chauffeur chauffeur = new Chauffeur();
                            chauffeur.Code = this.gridV.GetFocusedRowCellDisplayText(gridV.Columns[0]);
                            chauffeur.Supprimer();
                            break;
                        }
                    case "Villes":
                        {
                            Ville.Supprimer(this.gridV.GetFocusedRowCellDisplayText(gridV.Columns[0]));
                            break;
                          
                        }
                        
                    case "Types Convention":
                        {
                            TypeConvention.Supprimer(this.gridV.GetFocusedRowCellDisplayText(gridV.Columns[0]));
                            break;

                        }
                    case "Objectif":
                        {
                            TypeVisite.Supprimer(this.gridV.GetFocusedRowCellDisplayText(gridV.Columns[0]));
                            break;

                        }
                    case "Motif":
                        {
                            Motif.Supprimer(this.gridV.GetFocusedRowCellDisplayText(gridV.Columns[0]));
                            break;
                        }
                    case "Etablissement":
                        {
                            Etablissement etablissement = new Etablissement();
                            etablissement.Code = this.gridV.GetFocusedRowCellDisplayText(gridV.Columns[0]);
                            etablissement.Supprimer();

                            break;
                        }
                    case "Jours fériés":
                        {
                            JoursFeries jf = new JoursFeries();
                            jf.IDJFerie = int.Parse(this.gridV.GetFocusedRowCellDisplayText(gridV.Columns[0]));
                            jf.Supprimer();

                            break;
                        }

                    case "Modems":
                        {
                            Modem m = new Modem();
                            m.IMEI = this.gridV.GetFocusedRowCellDisplayText(gridV.Columns[0]);
                            m.Supprimer();

                            break;
                        }


                    case "Autobus":
                        {
                            Bus m = new Bus();
                            m.Num_IMM = this.gridV.GetFocusedRowCellDisplayText(gridV.Columns[0]);
                            m.Supprimer();

                            break;
                        }
                    default:
                        break;
                }

                this.ChargerEntite(this._NomSatelilte);

                warning(msgSucces, false);
            }
            catch (Exception)
            {
                warning(msgEchoue, true);
            }
        }

        private void treeLSatellites_AfterFocusNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            if (!bLoadTree)
                return;

            if (treeLSatellites.Nodes.Count == 0)
                return;

            foreach (RefSatellite sat in _Satellites)
            {
                if (e.Node.Id == sat.ID)
                {
                    _NomSatelilte = sat.Name;
                    groupCParametrages.Text = _NomSatelilte;
                    ChargerEntite(_NomSatelilte);
                    break;
                }
            }
        }

        private void gridV_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Delete) || (e.KeyCode == Keys.Back))
            {
                if (this.gridV.SelectedRowsCount == 0)
                    return;

                Supprimer();
            }

            if (((e.KeyCode == Keys.Enter) && (isUpDated)) || ((e.KeyCode == Keys.Tab) && (isUpDated) && (gridV.FocusedColumn == gridV.Columns[gridV.Columns.Count - 1])))
            {
                gridV.CloseEditor();

                Sauvgarder();
                isUpDated = false;
            }

        }

        private void gridV_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            if (gridV.FocusedColumn.Name == "Supprimer")
                this.Supprimer();
            isUpDated = true;
        }

        private void gridV_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            //if (gridV.GetFocusedRowCellDisplayText("Supprimer").Equals("Checked"))
            //    this.Supprimer();
            //if (gridV.FocusedColumn.Name == "Supprimer")
            //    this.Supprimer();
        }

        private void FrmSatellites_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void FrmSatellites_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();

            if (e.Control && e.KeyCode == Keys.P)
                DXReport.Apercu(this.gridC, this.groupCParametrages.Text, false, new Margins(20, 20, 60, 30));
        }

    }
}
