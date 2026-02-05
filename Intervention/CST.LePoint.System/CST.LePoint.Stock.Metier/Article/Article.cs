using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace CST.LePoint.Stock.Metier
{
    [Serializable]
    public class Article
    {
        [XmlAttribute("CArticle")]
        [Bindable(true)]
        public string CArticle { get; set; }

        [XmlAttribute("LibArticle")]
        [Bindable(true)]
        public string LibArticle { get; set; }

        [XmlAttribute("StockReel")]
        [Bindable(true)]
        public decimal StockReel { get; set; }

        [XmlAttribute("CodeBarre")]
        [Bindable(true)]
        public string CodeBarre { get; set; }

        [XmlAttribute("CCategorie")]
        [Bindable(true)]
        public string CCategorie { get; set; }

        [XmlAttribute("CCodification")]
        [Bindable(true)]
        public string CCodification { get; set; }

        [XmlAttribute("CEmballageVente")]
        [Bindable(true)]
        public string CEmballageVente { get; set; }

        [XmlAttribute("CEmballageAchat")]
        [Bindable(true)]
        public string CEmballageAchat { get; set; }

        [XmlAttribute("CFamille")]
        [Bindable(true)]
        public string CFamille { get; set; }

        [XmlAttribute("CModele1")]
        [Bindable(true)]
        public string CModele1 { get; set; }

        [XmlAttribute("CModele2")]
        [Bindable(true)]
        public string CModele2 { get; set; }

        [XmlAttribute("CModele")]
        [Bindable(true)]
        public string CModele { get; set; }

        [XmlAttribute("CNature")]
        [Bindable(true)]
        public string CNature { get; set; }

        [XmlAttribute("CNGP")]
        [Bindable(true)]
        public string CNGP { get; set; }

        [XmlAttribute("CodeProduction")]
        [Bindable(true)]
        public string CodeProduction { get; set; }

        [XmlAttribute("CTaxeVente")]
        [Bindable(true)]
        public string CTaxeVente { get; set; }

        [XmlAttribute("CType")]
        [Bindable(true)]
        public string CType { get; set; }

        [XmlAttribute("CUniteVente")]
        [Bindable(true)]
        public string CUniteVente { get; set; }

        [XmlAttribute("TypeVente")]
        [Bindable(true)]
        public string TypeVente { get; set; }

        [XmlAttribute("BAchat")]
        [Bindable(true)]
        public bool BAchat { get; set; }

        [XmlAttribute("BQuantite")]
        [Bindable(true)]
        public bool BQuantite { get; set; }

        [XmlAttribute("BVente")]
        [Bindable(true)]
        public bool BVente { get; set; }

        [XmlAttribute("BApprovisionnement")]
        [Bindable(true)]
        public bool BApprovisionnement { get; set; }

        [XmlAttribute("BActif")]
        [Bindable(true)]
        public bool BActif { get; set; }

        [XmlAttribute("Fodec")]
        [Bindable(true)]
        public decimal Fodec { get; set; }

        [XmlAttribute("TPE")]
        [Bindable(true)]
        public decimal TPE { get; set; }

        [XmlAttribute("TaxeDroitConsommation")]
        [Bindable(true)]
        public decimal TaxeDroitConsommation { get; set; }

        [XmlAttribute("Poids")]
        [Bindable(true)]
        public decimal Poids { get; set; }

        [XmlAttribute("PrixRevientInitial")]
        [Bindable(true)]
        public decimal PrixRevientInitial { get; set; }

        [XmlAttribute("PrixPublic")]
        [Bindable(true)]
        public decimal PrixPublic { get; set; }

        [XmlAttribute("PrixRevient")]
        [Bindable(true)]
        public decimal PrixRevient { get; set; }

        [XmlAttribute("Indice")]
        [Bindable(true)]
        public int Indice { get; set; }

        [XmlAttribute("PrioriteRemise")]
        [Bindable(true)]
        public int PrioriteRemise { get; set; }

        [XmlAttribute("StockACommander")]
        [Bindable(true)]
        public decimal StockACommander { get; set; }

        [XmlAttribute("StockMax")]
        [Bindable(true)]
        public decimal StockMax { get; set; }

        [XmlAttribute("StockMin")]
        [Bindable(true)]
        public decimal StockMin { get; set; }

        [XmlAttribute("TauxNGP")]
        [Bindable(true)]
        public decimal TauxNGP { get; set; }

        [XmlAttribute("TauxProduction")]
        [Bindable(true)]
        public decimal TauxProduction { get; set; }

        [XmlAttribute("TauxRevient")]
        [Bindable(true)]
        public decimal TauxRevient { get; set; }

        [XmlAttribute("TotalQauntiteAchat")]
        [Bindable(true)]
        public decimal TotalQauntiteAchat { get; set; }

        [XmlAttribute("Volume")]
        [Bindable(true)]
        public decimal Volume { get; set; }

        [XmlAttribute("BBloquerPrixHT")]
        [Bindable(true)]
        public bool BBloquerPrixHT { get; set; }

        [XmlAttribute("BBloquerRemise")]
        [Bindable(true)]
        public bool BBloquerRemise { get; set; }

        [XmlAttribute("TypeAchat")]
        [Bindable(true)]
        public string TypeAchat { get; set; }

        [XmlAttribute("CTaxeAchat")]
        [Bindable(true)]
        public string CTaxeAchat { get; set; }

        [XmlAttribute("CUniteAchat")]
        [Bindable(true)]
        public string CUniteAchat { get; set; }

        [XmlAttribute("BPrixMargeFixe")]
        [Bindable(true)]
        public bool BPrixMargeFixe { get; set; }

        [XmlAttribute("BGestionNumeroSerie")]
        [Bindable(true)]
        public bool BGestionNumeroSerie { get; set; }

        [XmlAttribute("BGestionConsigne")]
        [Bindable(true)]
        public bool BGestionConsigne { get; set; }

        [XmlAttribute("PrixDevise")]
        [Bindable(true)]
        public decimal PrixDevise { get; set; }

        [XmlAttribute("DateInsertion")]
        [Bindable(true)]
        public DateTime DateInsertion { get; set; }

        [XmlAttribute("DateModification")]
        [Bindable(true)]
        public DateTime DateModification { get; set; }

        [XmlAttribute("CreePar")]
        [Bindable(true)]
        public int CreePar { get; set; }

        [XmlAttribute("ModifiePar")]
        [Bindable(true)]
        public int ModifiePar { get; set; }

        [XmlAttribute("PCInsertion")]
        [Bindable(true)]
        public string PCInsertion { get; set; }

        [XmlAttribute("PCModification")]
        [Bindable(true)]
        public string PCModification { get; set; }

        [XmlAttribute("BSpecial")]
        [Bindable(true)]
        public bool BSpecial { get; set; }

        [XmlAttribute("TauxCommission")]
        [Bindable(true)]
        public decimal TauxCommission { get; set; }

        [XmlAttribute("CNatureVente")]
        [Bindable(true)]
        public string CNatureVente { get; set; }

        [XmlAttribute("BGestionLot")]
        [Bindable(true)]
        public bool BGestionLot { get; set; }

        [XmlAttribute("Image_Article")]
        [Bindable(true)]
        public byte[] Image_Article { get; set; }

        [XmlAttribute("BTablette")]
        [Bindable(true)]
        public bool BTablette { get; set; }

        [XmlAttribute("bGestionLotEntree")]
        [Bindable(true)]
        public bool bGestionLotEntree { get; set; }

        [XmlAttribute("bGestionLotSortee")]
        [Bindable(true)]
        public bool bGestionLotSortee { get; set; }

        [XmlAttribute("bGestionLotMouvement")]
        [Bindable(true)]
        public int bGestionLotMouvement { get; set; }

        [XmlAttribute("LibelleMagasin")]
        [Bindable(true)]
        public string LibelleMagasin { get; set; }

        [XmlAttribute("CClient")]
        [Bindable(true)]
        public string CClient { get; set; }

        [XmlAttribute("NbNiveau")]
        [Bindable(true)]
        public int NbNiveau { get; set; }

        [XmlAttribute("NbPiece")]
        [Bindable(true)]
        public int NbPiece { get; set; }

        [XmlAttribute("ConsigneTempsMA")]
        [Bindable(true)]
        public int ConsigneTempsMA { get; set; }

        [XmlAttribute("ConsignePose")]
        [Bindable(true)]
        public int ConsignePose { get; set; }

        [XmlAttribute("NbBandes")]
        [Bindable(true)]
        public int NbBandes { get; set; }

        [XmlAttribute("NbFlans")]
        [Bindable(true)]
        public int NbFlans { get; set; }

        [XmlAttribute("ConsigneDechet")]
        [Bindable(true)]
        public int ConsigneDechet { get; set; }

        [XmlAttribute("CFournisseur")]
        [Bindable(true)]
        public string CFournisseur { get; set; }

        [XmlAttribute("CDimension")]
        [Bindable(true)]
        public string CDimension { get; set; }

        [XmlAttribute("CNomenclatureVernis")]
        [Bindable(true)]
        public string CNomenclatureVernis { get; set; }

        [XmlAttribute("CNomenclatureEncre")]
        [Bindable(true)]
        public string CNomenclatureEncre { get; set; }

        [XmlAttribute("CTypeFeuille")]
        [Bindable(true)]
        public string CTypeFeuille { get; set; }

        [XmlAttribute("BAlimentaire")]
        [Bindable(true)]
        public bool BAlimentaire { get; set; }

        [XmlAttribute("BIndustriel")]
        [Bindable(true)]
        public bool BIndustriel { get; set; }

        [XmlAttribute("BOuvertureFacile")]
        [Bindable(true)]
        public bool BOuvertureFacile { get; set; }

        [XmlAttribute("BRetreins")]
        [Bindable(true)]
        public bool BRetreins { get; set; }

        [XmlAttribute("BBPANI")]
        [Bindable(true)]
        public bool BBPANI { get; set; }

        [XmlAttribute("BVenteNegative")]
        [Bindable(true)]
        public bool BVenteNegative { get; set; }

        [XmlAttribute("BStandard")]
        [Bindable(true)]
        public bool BStandard { get; set; }

        [XmlAttribute("bGestionPrixderevien")]
        [Bindable(true)]
        public int bGestionPrixderevien { get; set; }

        [XmlAttribute("CDevise")]
        [Bindable(true)]
        public string CDevise { get; set; }

        [XmlAttribute("BBloquerLibelle")]
        [Bindable(true)]
        public bool BBloquerLibelle { get; set; }


        [XmlAttribute("Selectionne")]
        [Bindable(true)]
        public bool Selectionne { get; set; }

        [XmlAttribute("CGratuites")]
        [Bindable(true)]
        public string CGratuites { get; set; }

        [XmlAttribute("DateGratuitesDebut")]
        [Bindable(true)]
        public DateTime DateGratuitesDebut { get; set; }

        [XmlAttribute("DateGratuitesFin")]
        [Bindable(true)]
        public DateTime DateGratuitesFin { get; set; }
        public string CEtatArticle { get; set; }
        public int Ordre { get; set; }

        public ArticleEntrepotCollection ArticleEntrepots;
        public ArticlePrixCollection ArticlesPrix;
        public ArticleEquivalentCollection ArticleEquivalents;
        public ArticleFournisseurCollection ArticleFournisseurs;
        public ArticleComposantCollection ArticleComposants;
        public LotArticleCollection LotsArticle;

        public Article()
        {
            this.StockReel = 0m;
            this.ArticleEntrepots = new ArticleEntrepotCollection();
            this.ArticlesPrix = new ArticlePrixCollection();
            this.ArticleEquivalents = new ArticleEquivalentCollection();
            this.ArticleFournisseurs = new ArticleFournisseurCollection();
            this.ArticleComposants = new ArticleComposantCollection();
            this.LotsArticle = new LotArticleCollection();
        }

        public Article(string carticle)
        {
            this.CArticle = carticle;
            this.StockReel = 0m;
            this.ArticleEntrepots = new ArticleEntrepotCollection();
            this.ArticlesPrix = new ArticlePrixCollection();
            this.ArticleEquivalents = new ArticleEquivalentCollection();
            this.ArticleFournisseurs = new ArticleFournisseurCollection();
            this.ArticleComposants = new ArticleComposantCollection();
            this.LotsArticle = new LotArticleCollection();
        }

        public void Sauvegarder()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Transaction = transaction;
                    cmd.Connection = transaction.Connection;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "Article_Sauvegarder";
                    cmd.Parameters.AddWithValue("@CArticle", this.CArticle);
                    cmd.Parameters.AddWithValue("@LibArticle", this.LibArticle);
                    cmd.Parameters.AddWithValue("@LibelleMagasin", this.LibelleMagasin);
                    cmd.Parameters.AddWithValue("@CodeBarre", this.CodeBarre);
                    cmd.Parameters.AddWithValue("@CCategorie", this.CCategorie);
                    cmd.Parameters.AddWithValue("@CCodification", this.CCodification);
                    cmd.Parameters.AddWithValue("@CEmballageVente", this.CEmballageVente);
                    cmd.Parameters.AddWithValue("@CEmballageAchat", this.CEmballageAchat);
                    cmd.Parameters.AddWithValue("@CFamille", this.CFamille);
                    cmd.Parameters.AddWithValue("@CModele1", this.CModele1);
                    cmd.Parameters.AddWithValue("@CModele2", this.CModele2);
                    cmd.Parameters.AddWithValue("@CModele", this.CModele);
                    cmd.Parameters.AddWithValue("@CNature", this.CNature);
                    cmd.Parameters.AddWithValue("@CNGP", this.CNGP);
                    cmd.Parameters.AddWithValue("@CodeProduction", this.CodeProduction);
                    cmd.Parameters.AddWithValue("@CTaxeVente", this.CTaxeVente);
                    cmd.Parameters.AddWithValue("@CType", this.CType);
                    cmd.Parameters.AddWithValue("@CUniteVente", this.CUniteVente);
                    cmd.Parameters.AddWithValue("@TypeVente", this.TypeVente);
                    cmd.Parameters.AddWithValue("@BAchat", this.BAchat);
                    cmd.Parameters.AddWithValue("@BQuantite", this.BQuantite);
                    cmd.Parameters.AddWithValue("@BVente", this.BVente);
                    cmd.Parameters.AddWithValue("@BApprovisionnement", this.BApprovisionnement);
                    cmd.Parameters.AddWithValue("@Fodec", this.Fodec);
                    cmd.Parameters.AddWithValue("@TPE", this.TPE);
                    cmd.Parameters.AddWithValue("@TaxeDroitConsommation", this.TaxeDroitConsommation);
                    cmd.Parameters.AddWithValue("@Poids", this.Poids);
                    cmd.Parameters.AddWithValue("@PrixRevientInitial", this.PrixRevientInitial);
                    cmd.Parameters.AddWithValue("@PrixPublic", this.PrixPublic);
                    cmd.Parameters.AddWithValue("@PrixRevient", this.PrixRevient);
                    cmd.Parameters.AddWithValue("@Indice", this.Indice);
                    cmd.Parameters.AddWithValue("@PrioriteRemise", this.PrioriteRemise);
                    cmd.Parameters.AddWithValue("@StockACommander", this.StockACommander);
                    cmd.Parameters.AddWithValue("@StockMax", this.StockMax);
                    cmd.Parameters.AddWithValue("@StockMin", this.StockMin);
                    cmd.Parameters.AddWithValue("@TauxNGP", this.TauxNGP);
                    cmd.Parameters.AddWithValue("@TauxProduction", this.TauxProduction);
                    cmd.Parameters.AddWithValue("@TauxRevient", this.TauxRevient);
                    cmd.Parameters.AddWithValue("@Volume", this.Volume);
                    cmd.Parameters.AddWithValue("@BBloquerPrixHT", this.BBloquerPrixHT);
                    cmd.Parameters.AddWithValue("@BBloquerRemise", this.BBloquerRemise);
                    cmd.Parameters.AddWithValue("@TypeAchat", this.TypeAchat);
                    cmd.Parameters.AddWithValue("@CTaxeAchat", this.CTaxeAchat);
                    cmd.Parameters.AddWithValue("@CUniteAchat", this.CUniteAchat);
                    cmd.Parameters.AddWithValue("@BPrixMargeFixe", this.BPrixMargeFixe);
                    cmd.Parameters.AddWithValue("@BGestionNumeroSerie", this.BGestionNumeroSerie);
                    cmd.Parameters.AddWithValue("@BGestionConsigne", this.BGestionConsigne);
                    cmd.Parameters.AddWithValue("@PrixDevise", this.PrixDevise);
                    cmd.Parameters.AddWithValue("@BActif", this.BActif);
                    cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);
                    cmd.Parameters.AddWithValue("@CreePar", this.CreePar);
                    cmd.Parameters.AddWithValue("@PCModification", this.PCModification);
                    cmd.Parameters.AddWithValue("@ModifiePar", this.ModifiePar);
                    cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                    cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                    cmd.Parameters.AddWithValue("@BSpecial", this.BSpecial);
                    cmd.Parameters.AddWithValue("@TauxCommission", this.TauxCommission);
                    cmd.Parameters.AddWithValue("@CNatureVente", this.CNatureVente);
                    cmd.Parameters.AddWithValue("@BGestionLot", this.BGestionLot);
                    cmd.Parameters.AddWithValue("@Image_Article", this.Image_Article);
                    cmd.Parameters.AddWithValue("@BTablette", this.BTablette);
                    cmd.Parameters.AddWithValue("@bGestionLotEntree", this.bGestionLotEntree);
                    cmd.Parameters.AddWithValue("@bGestionLotSortee", this.bGestionLotSortee);
                    cmd.Parameters.AddWithValue("@bGestionLotMouvement", this.bGestionLotMouvement);
                    cmd.Parameters.AddWithValue("@bGestionPrixderevien", this.bGestionPrixderevien);
                    cmd.Parameters.AddWithValue("@CClient", this.CClient);
                    cmd.Parameters.AddWithValue("@NbNiveau", this.NbNiveau);
                    cmd.Parameters.AddWithValue("@NbPiece", this.NbPiece);
                    cmd.Parameters.AddWithValue("@ConsigneDechet", this.ConsigneDechet);
                    cmd.Parameters.AddWithValue("@ConsignePose", this.ConsignePose);
                    cmd.Parameters.AddWithValue("@NbBandes", this.NbBandes);
                    cmd.Parameters.AddWithValue("@NbFlans", this.NbFlans);
                    cmd.Parameters.AddWithValue("@ConsigneTempsMA", this.ConsigneTempsMA);
                    cmd.Parameters.AddWithValue("@CFournisseur", this.CFournisseur);
                    cmd.Parameters.AddWithValue("@CDimension", this.CDimension);
                    cmd.Parameters.AddWithValue("@CNomenclatureVernis", this.CNomenclatureVernis);
                    cmd.Parameters.AddWithValue("@CNomenclatureEncre", this.CNomenclatureEncre);
                    cmd.Parameters.AddWithValue("@CTypeFeuille", this.CTypeFeuille);

                    cmd.Parameters.AddWithValue("@BAlimentaire", this.BAlimentaire);
                    cmd.Parameters.AddWithValue("@BIndustriel", this.BIndustriel);
                    cmd.Parameters.AddWithValue("@BOuvertureFacile", this.BOuvertureFacile);
                    cmd.Parameters.AddWithValue("@BRetreins", this.BRetreins);
                    cmd.Parameters.AddWithValue("@BBPANI", this.BBPANI);
                    cmd.Parameters.AddWithValue("@BVenteNegative", this.BVenteNegative);
                    cmd.Parameters.AddWithValue("@BStandard", this.BStandard);
                    cmd.Parameters.AddWithValue("@CDevise", this.CDevise);
                    cmd.Parameters.AddWithValue("@BBloquerLibelle", this.BBloquerLibelle);
                    cmd.Parameters.AddWithValue("@CEtatArticle", this.CEtatArticle);
                    cmd.Parameters.AddWithValue("@Ordre", this.Ordre);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null || parametre.Value == "")
                            parametre.Value = DBNull.Value;

                    cmd.ExecuteNonQuery();

                    this.SupprimerArticlesEntrepotAnterieurs(transaction);
                    this.SupprimerArticlesEquivalentsAnterieurs(transaction);
                    this.SupprimerArticleComposantsAnterieurs(transaction);
                    this.SupprimerArticleFournisseursAnterieurs(transaction);
                    this.SupprimerArticlePrixAnterieurs(transaction);
                    //   if (this.BGestionLot)
                    //     this.SupprimerArticleLotsAnterieurs(transaction);

                    foreach (ArticleEntrepot articleEntrepot in ArticleEntrepots)
                    {
                        articleEntrepot.Sauvegarder(transaction);
                    }
                    foreach (ArticlePrix ArticlePrix in ArticlesPrix)
                    {
                        ArticlePrix.Sauvegarder(transaction);
                    }
                    foreach (ArticleFournisseur articleFournisseur in ArticleFournisseurs)
                    {
                        articleFournisseur.Sauvegarder(transaction);
                    }
                    foreach (ArticleComposant articleComposant in ArticleComposants)
                    {
                        articleComposant.Sauvegarder(transaction);
                    }
                    foreach (ArticleEquivalent articleEquivalent in ArticleEquivalents)
                    {
                        articleEquivalent.Sauvegarder(transaction);
                    }
                    //   if (this.BGestionLot)
                    // {
                    //   foreach (LotArticle lotArticle in LotsArticle)
                    // {
                    //   lotArticle.Sauvegarder(transaction);
                    //}
                    //}

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        private void SupprimerArticlesEntrepotAnterieurs(SqlTransaction transaction)
        {
            ArticleEntrepotCollection collection = ArticleEntrepotCollection.Charger(this.CArticle);
            foreach (ArticleEntrepot item in collection)
            {
                if (!this.ArticleEntrepots.Exists(p => p.CArticle == item.CArticle && p.CEntrepot == item.CEntrepot))
                    item.Supprimer(transaction);
            }
        }

        private void SupprimerArticlesEquivalentsAnterieurs(SqlTransaction transaction)
        {
            ArticleEquivalentCollection collection = ArticleEquivalentCollection.Charger(this.CArticle);
            foreach (ArticleEquivalent item in collection)
            {
                if (!this.ArticleEquivalents.Exists(p => p.CArticle == item.CArticle && p.CArticleEquivalent == item.CArticleEquivalent))
                    item.Supprimer(transaction);
            }
        }

        private void SupprimerArticleComposantsAnterieurs(SqlTransaction transaction)
        {
            ArticleComposantCollection collection = ArticleComposantCollection.Charger(this.CArticle);
            foreach (ArticleComposant item in collection)
            {
                if (!this.ArticleComposants.Exists(p => p.CArticle == item.CArticle && p.CComposant == item.CComposant))
                    item.Supprimer(transaction);
            }
        }

        private void SupprimerArticleFournisseursAnterieurs(SqlTransaction transaction)
        {
            ArticleFournisseurCollection collection = ArticleFournisseurCollection.Charger(this.CArticle);
            foreach (ArticleFournisseur item in collection)
            {
                if (!this.ArticleFournisseurs.Exists(p => p.CArticle == item.CArticle && p.CFournisseur == item.CFournisseur))
                    item.Supprimer(transaction);
            }
        }

        private void SupprimerArticlePrixAnterieurs(SqlTransaction transaction)
        {
            ArticlePrixCollection collection = ArticlePrixCollection.Charger(this.CArticle, true);
            foreach (ArticlePrix item in collection)
            {
                if (!this.ArticlesPrix.Exists(p => p.CArticle == item.CArticle && p.CTarif == item.CTarif))
                    item.Supprimer(transaction);
            }
        }

        private void SupprimerArticleLotsAnterieurs(SqlTransaction transaction)
        {
            LotArticleCollection collection = LotArticleCollection.Charger(this.CArticle);
            foreach (LotArticle item in collection)
            {
                if (!this.LotsArticle.Exists(p => p.CArticle == item.CArticle && p.CLot == item.CLot))
                    item.Supprimer(transaction);
            }
        }

        public void Supprimer()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Transaction = transaction;
                    cmd.Connection = transaction.Connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Article_Supprimer";
                    cmd.Parameters.AddWithValue("@CArticle", CArticle);
                    cmd.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public static void MAJPrixRevient(string cArticle, decimal PrixRecu, decimal QteRecu, decimal PrixRevient, decimal StkReel, int PMP, int ModifiePar)
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Transaction = transaction;
                    cmd.Connection = transaction.Connection;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "Article_MAJPrixRevient";
                    cmd.Parameters.AddWithValue("@cArticle", cArticle);
                    cmd.Parameters.AddWithValue("@PrixRecu", PrixRecu);
                    cmd.Parameters.AddWithValue("@QteRecu", QteRecu);
                    cmd.Parameters.AddWithValue("@PrixRevient", PrixRevient);
                    cmd.Parameters.AddWithValue("@StkReel", StkReel);
                    cmd.Parameters.AddWithValue("@PMP", PMP);
                    cmd.Parameters.AddWithValue("@ModifiePar", ModifiePar);
                    cmd.Parameters.AddWithValue("@PCModification", Environment.MachineName);


                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    cmd.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        /*   public static void MAJPrixRevient(SqlTransaction transaction , string cArticle, decimal PrixRecu, decimal QteRecu, decimal PrixRevient, decimal StkReel, int PMP, int ModifiePar)
           {
            
                   try
                   {
                       SqlCommand cmd = new SqlCommand();
                       cmd.Transaction = transaction;
                       cmd.Connection = transaction.Connection;
                       cmd.CommandType = CommandType.StoredProcedure;

                       cmd.CommandText = "Article_MAJPrixRevient";
                       cmd.Parameters.AddWithValue("@cArticle", cArticle);
                       cmd.Parameters.AddWithValue("@PrixRecu", PrixRecu);
                       cmd.Parameters.AddWithValue("@QteRecu", QteRecu);
                       cmd.Parameters.AddWithValue("@PrixRevient", PrixRevient);
                       cmd.Parameters.AddWithValue("@StkReel", StkReel);
                       cmd.Parameters.AddWithValue("@PMP", PMP);
                       cmd.Parameters.AddWithValue("@ModifiePar", ModifiePar);
                       cmd.Parameters.AddWithValue("@PCModification", Environment.MachineName);


                       foreach (SqlParameter parametre in cmd.Parameters)
                           if (parametre.Value == null)
                               parametre.Value = DBNull.Value;

                       cmd.ExecuteNonQuery();

                   
                   }
                   catch (Exception)
                   {
                       throw;
                   }
            
           }
           */
        public static void MAJPrixRevient(SqlTransaction transaction, string cArticle, decimal PrixRecu, decimal QteRecu, decimal PrixRevient, decimal StkReel, bool PMP, int ModifiePar)
        {

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "Article_MAJPrixRevient";
                cmd.Parameters.AddWithValue("@cArticle", cArticle);
                cmd.Parameters.AddWithValue("@PrixRecu", PrixRecu);
                cmd.Parameters.AddWithValue("@QteRecu", QteRecu);
                cmd.Parameters.AddWithValue("@PrixRevient", PrixRevient);
                cmd.Parameters.AddWithValue("@StkReel", StkReel);
                cmd.Parameters.AddWithValue("@PMP", PMP);
                cmd.Parameters.AddWithValue("@ModifiePar", ModifiePar);
                cmd.Parameters.AddWithValue("@PCModification", Environment.MachineName);


                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                cmd.ExecuteNonQuery();


            }
            catch (Exception)
            {

                throw;
            }

        }

        public static void MAJStockEnCommandeFnr(SqlTransaction transaction, string cArticle, decimal StockEnCommandeFnr, int ModifiePar)
        {

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "Article_MAJStockEnCommandeFnr";
                cmd.Parameters.AddWithValue("@cArticle", cArticle);
                cmd.Parameters.AddWithValue("@StockEnCommandeFnr", StockEnCommandeFnr);
                cmd.Parameters.AddWithValue("@ModifiePar", ModifiePar);
                cmd.Parameters.AddWithValue("@PCModification", Environment.MachineName);


                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                cmd.ExecuteNonQuery();


            }
            catch (Exception)
            {
                throw;
            }

        }

        public static Article Charger(string cArticle)
        {
            Article article = null;
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Article_Charger";
                cmd.Parameters.AddWithValue("@CArticle", cArticle);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    article = new Article();
                    article.CArticle = dr["CArticle"].ToString();
                    if (dr["LibArticle"] != DBNull.Value)
                        article.LibArticle = dr["LibArticle"].ToString();
                    if (dr["LibelleMagasin"] != DBNull.Value)
                        article.LibelleMagasin = dr["LibelleMagasin"].ToString();
                    if (dr["CodeBarre"] != DBNull.Value)
                        article.CodeBarre = dr["CodeBarre"].ToString();
                    if (dr["CCategorie"] != DBNull.Value)
                        article.CCategorie = dr["CCategorie"].ToString();
                    if (dr["CCodification"] != DBNull.Value)
                        article.CCodification = dr["CCodification"].ToString();
                    if (dr["CEmballageVente"] != DBNull.Value)
                        article.CEmballageVente = dr["CEmballageVente"].ToString();
                    if (dr["CEmballageAchat"] != DBNull.Value)
                        article.CEmballageAchat = dr["CEmballageAchat"].ToString();
                    if (dr["CFamille"] != DBNull.Value)
                        article.CFamille = dr["CFamille"].ToString();
                    if (dr["CModele1"] != DBNull.Value)
                        article.CModele1 = dr["CModele1"].ToString();
                    if (dr["CModele2"] != DBNull.Value)
                        article.CModele2 = dr["CModele2"].ToString();
                    if (dr["CModele"] != DBNull.Value)
                        article.CModele = dr["CModele"].ToString();
                    if (dr["CNature"] != DBNull.Value)
                        article.CNature = dr["CNature"].ToString();
                    if (dr["CNGP"] != DBNull.Value)
                        article.CNGP = dr["CNGP"].ToString();
                    if (dr["CodeProduction"] != DBNull.Value)
                        article.CodeProduction = dr["CodeProduction"].ToString();
                    if (dr["CTaxeVente"] != DBNull.Value)
                        article.CTaxeVente = dr["CTaxeVente"].ToString();
                    if (dr["CType"] != DBNull.Value)
                        article.CType = dr["CType"].ToString();
                    if (dr["CUniteVente"] != DBNull.Value)
                        article.CUniteVente = dr["CUniteVente"].ToString();
                    if (dr["TypeVente"] != DBNull.Value)
                        article.TypeVente = dr["TypeVente"].ToString();
                    if (dr["BAchat"] != DBNull.Value)
                        article.BAchat = bool.Parse(dr["BAchat"].ToString());
                    if (dr["BQuantite"] != DBNull.Value)
                        article.BQuantite = bool.Parse(dr["BQuantite"].ToString());
                    if (dr["BVente"] != DBNull.Value)
                        article.BVente = bool.Parse(dr["BVente"].ToString());
                    if (dr["BApprovisionnement"] != DBNull.Value)
                        article.BApprovisionnement = bool.Parse(dr["BApprovisionnement"].ToString());
                    if (dr["BActif"] != DBNull.Value)
                        article.BActif = bool.Parse(dr["BActif"].ToString());
                    if (dr["Fodec"] != DBNull.Value)
                        article.Fodec = decimal.Parse(dr["Fodec"].ToString());
                    if (dr["TPE"] != DBNull.Value)
                        article.TPE = decimal.Parse(dr["TPE"].ToString());
                    if (dr["TaxeDroitConsommation"] != DBNull.Value)
                        article.TaxeDroitConsommation = decimal.Parse(dr["TaxeDroitConsommation"].ToString());
                    if (dr["Poids"] != DBNull.Value)
                        article.Poids = decimal.Parse(dr["Poids"].ToString());
                    if (dr["PrixRevientInitial"] != DBNull.Value)
                        article.PrixRevientInitial = decimal.Parse(dr["PrixRevientInitial"].ToString());
                    if (dr["PrixPublic"] != DBNull.Value)
                        article.PrixPublic = decimal.Parse(dr["PrixPublic"].ToString());
                    if (dr["PrixRevient"] != DBNull.Value)
                        article.PrixRevient = decimal.Parse(dr["PrixRevient"].ToString());
                    if (dr["Indice"] != DBNull.Value)
                        article.Indice = int.Parse(dr["Indice"].ToString());
                    if (dr["PrioriteRemise"] != DBNull.Value)
                        article.PrioriteRemise = int.Parse(dr["PrioriteRemise"].ToString());
                    if (dr["StockACommander"] != DBNull.Value)
                        article.StockACommander = decimal.Parse(dr["StockACommander"].ToString());
                    if (dr["StockMax"] != DBNull.Value)
                        article.StockMax = decimal.Parse(dr["StockMax"].ToString());
                    if (dr["StockMin"] != DBNull.Value)
                        article.StockMin = decimal.Parse(dr["StockMin"].ToString());
                    if (dr["TauxNGP"] != DBNull.Value)
                        article.TauxNGP = decimal.Parse(dr["TauxNGP"].ToString());
                    if (dr["TauxProduction"] != DBNull.Value)
                        article.TauxProduction = decimal.Parse(dr["TauxProduction"].ToString());
                    if (dr["TauxRevient"] != DBNull.Value)
                        article.TauxRevient = decimal.Parse(dr["TauxRevient"].ToString());
                    if (dr["TotalQauntiteAchat"] != DBNull.Value)
                        article.TotalQauntiteAchat = decimal.Parse(dr["TotalQauntiteAchat"].ToString());
                    if (dr["Volume"] != DBNull.Value)
                        article.Volume = decimal.Parse(dr["Volume"].ToString());
                    if (dr["BBloquerPrixHT"] != DBNull.Value)
                        article.BBloquerPrixHT = bool.Parse(dr["BBloquerPrixHT"].ToString());
                    if (dr["BBloquerRemise"] != DBNull.Value)
                        article.BBloquerRemise = bool.Parse(dr["BBloquerRemise"].ToString());
                    if (dr["TypeAchat"] != DBNull.Value)
                        article.TypeAchat = dr["TypeAchat"].ToString();
                    if (dr["CTaxeAchat"] != DBNull.Value)
                        article.CTaxeAchat = dr["CTaxeAchat"].ToString();
                    if (dr["CUniteAchat"] != DBNull.Value)
                        article.CUniteAchat = dr["CUniteAchat"].ToString();
                    if (dr["BPrixMargeFixe"] != DBNull.Value)
                        article.BPrixMargeFixe = bool.Parse(dr["BPrixMargeFixe"].ToString());
                    if (dr["BGestionNumeroSerie"] != DBNull.Value)
                        article.BGestionNumeroSerie = bool.Parse(dr["BGestionNumeroSerie"].ToString());
                    if (dr["BGestionConsigne"] != DBNull.Value)
                        article.BGestionConsigne = bool.Parse(dr["BGestionConsigne"].ToString());
                    if (dr["PrixDevise"] != DBNull.Value)
                        article.PrixDevise = decimal.Parse(dr["PrixDevise"].ToString());
                    if (dr["StockReel"] != DBNull.Value)
                        article.StockReel = decimal.Parse(dr["StockReel"].ToString());
                    if (dr["BSpecial"] != DBNull.Value)
                        article.BSpecial = bool.Parse(dr["BSpecial"].ToString());
                    if (dr["CNatureVente"] != DBNull.Value)
                        article.CNatureVente = dr["CNatureVente"].ToString();
                    if (dr["TauxCommission"] != DBNull.Value)
                        article.TauxCommission = decimal.Parse(dr["TauxCommission"].ToString());
                    if (dr["BGestionLot"] != DBNull.Value)
                        article.BGestionLot = bool.Parse(dr["BGestionLot"].ToString());
                    if (dr["Image_Article"] != DBNull.Value)
                        article.Image_Article = (byte[])dr["Image_Article"];
                    if (dr["BTablette"] != DBNull.Value)
                        article.BTablette = bool.Parse(dr["BTablette"].ToString());
                    if (dr["bGestionLotMouvement"] != DBNull.Value)
                        article.bGestionLotMouvement = int.Parse(dr["bGestionLotMouvement"].ToString());
                    if (dr["bGestionLotEntree"] != DBNull.Value)
                        article.bGestionLotEntree = bool.Parse(dr["bGestionLotEntree"].ToString());
                    if (dr["bGestionLotSortee"] != DBNull.Value)
                        article.bGestionLotSortee = bool.Parse(dr["bGestionLotSortee"].ToString());
                    if (dr["CClient"] != DBNull.Value)
                        article.CClient = dr["CClient"].ToString();
                    if (dr["NbNiveau"] != DBNull.Value)
                        article.NbNiveau = int.Parse(dr["NbNiveau"].ToString());
                    if (dr["NbPiece"] != DBNull.Value)
                        article.NbPiece = int.Parse(dr["NbPiece"].ToString());
                    if (dr["ConsigneTempsMA"] != DBNull.Value)
                        article.ConsigneTempsMA = int.Parse(dr["ConsigneTempsMA"].ToString());
                    if (dr["ConsignePose"] != DBNull.Value)
                        article.ConsignePose = int.Parse(dr["ConsignePose"].ToString());
                    if (dr["NbBandes"] != DBNull.Value)
                        article.NbBandes = int.Parse(dr["NbBandes"].ToString());
                    if (dr["NbFlans"] != DBNull.Value)
                        article.NbFlans = int.Parse(dr["NbFlans"].ToString());
                    if (dr["ConsigneDechet"] != DBNull.Value)
                        article.ConsigneDechet = int.Parse(dr["ConsigneDechet"].ToString());
                    if (dr["CFournisseur"] != DBNull.Value)
                        article.CFournisseur = dr["CFournisseur"].ToString();
                    if (dr["CDimension"] != DBNull.Value)
                        article.CDimension = dr["CDimension"].ToString();
                    if (dr["CNomenclatureEncre"] != DBNull.Value)
                        article.CNomenclatureEncre = dr["CNomenclatureEncre"].ToString();
                    if (dr["CNomenclatureVernis"] != DBNull.Value)
                        article.CNomenclatureVernis = dr["CNomenclatureVernis"].ToString();
                    if (dr["CTypeFeuille"] != DBNull.Value)
                        article.CTypeFeuille = dr["CTypeFeuille"].ToString();

                    if (dr["BIndustriel"] != DBNull.Value)
                        article.BIndustriel = bool.Parse(dr["BIndustriel"].ToString());
                    if (dr["BAlimentaire"] != DBNull.Value)
                        article.BAlimentaire = bool.Parse(dr["BAlimentaire"].ToString());
                    if (dr["BOuvertureFacile"] != DBNull.Value)
                        article.BOuvertureFacile = bool.Parse(dr["BOuvertureFacile"].ToString());
                    if (dr["BRetreins"] != DBNull.Value)
                        article.BRetreins = bool.Parse(dr["BRetreins"].ToString());
                    if (dr["BBPANI"] != DBNull.Value)
                        article.BBPANI = bool.Parse(dr["BBPANI"].ToString());
                    if (dr["BVenteNegative"] != DBNull.Value)
                        article.BVenteNegative = bool.Parse(dr["BVenteNegative"].ToString());
                    if (dr["BStandard"] != DBNull.Value)
                        article.BStandard = bool.Parse(dr["BStandard"].ToString());
                    if (dr["bGestionPrixderevien"] != DBNull.Value)
                        article.bGestionPrixderevien = int.Parse(dr["bGestionPrixderevien"].ToString());
                    if (dr["CDevise"] != DBNull.Value)
                        article.CDevise = dr["CDevise"].ToString();
                    if (dr["BBloquerLibelle"] != DBNull.Value)
                        article.BBloquerLibelle = bool.Parse(dr["BBloquerLibelle"].ToString());
                    if (dr["CEtatArticle"] != DBNull.Value)
                        article.CEtatArticle = dr["CEtatArticle"].ToString();
                    if (dr["Ordre"] != DBNull.Value)
                        article.Ordre = int.Parse(dr["Ordre"].ToString());

                    article.ArticleEntrepots = ArticleEntrepotCollection.Charger(cArticle);
                    article.ArticlesPrix = ArticlePrixCollection.Charger(cArticle, true);
                    article.ArticleEquivalents = ArticleEquivalentCollection.Charger(cArticle);
                    article.ArticleComposants = ArticleComposantCollection.Charger(cArticle);
                    article.ArticleFournisseurs = ArticleFournisseurCollection.Charger(cArticle);
                    if (article.BGestionLot)
                        article.LotsArticle = LotArticleCollection.Charger(cArticle);
                }
            }
            return (article);
        }

        public static Article ChargerTous(string cArticle)
        {
            Article article = null;
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Article_ChargerTous";
                cmd.Parameters.AddWithValue("@CArticle", cArticle);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    article = new Article();
                    article.CArticle = dr["CArticle"].ToString();
                    if (dr["LibArticle"] != DBNull.Value)
                        article.LibArticle = dr["LibArticle"].ToString();
                    if (dr["LibelleMagasin"] != DBNull.Value)
                        article.LibelleMagasin = dr["LibelleMagasin"].ToString();
                    if (dr["CodeBarre"] != DBNull.Value)
                        article.CodeBarre = dr["CodeBarre"].ToString();
                    if (dr["CCategorie"] != DBNull.Value)
                        article.CCategorie = dr["CCategorie"].ToString();
                    if (dr["CCodification"] != DBNull.Value)
                        article.CCodification = dr["CCodification"].ToString();
                    if (dr["CEmballageVente"] != DBNull.Value)
                        article.CEmballageVente = dr["CEmballageVente"].ToString();
                    if (dr["CEmballageAchat"] != DBNull.Value)
                        article.CEmballageAchat = dr["CEmballageAchat"].ToString();
                    if (dr["CFamille"] != DBNull.Value)
                        article.CFamille = dr["CFamille"].ToString();
                    if (dr["CModele1"] != DBNull.Value)
                        article.CModele1 = dr["CModele1"].ToString();
                    if (dr["CModele2"] != DBNull.Value)
                        article.CModele2 = dr["CModele2"].ToString();
                    if (dr["CModele"] != DBNull.Value)
                        article.CModele = dr["CModele"].ToString();
                    if (dr["CNature"] != DBNull.Value)
                        article.CNature = dr["CNature"].ToString();
                    if (dr["CNGP"] != DBNull.Value)
                        article.CNGP = dr["CNGP"].ToString();
                    if (dr["CodeProduction"] != DBNull.Value)
                        article.CodeProduction = dr["CodeProduction"].ToString();
                    if (dr["CTaxeVente"] != DBNull.Value)
                        article.CTaxeVente = dr["CTaxeVente"].ToString();
                    if (dr["CType"] != DBNull.Value)
                        article.CType = dr["CType"].ToString();
                    if (dr["CUniteVente"] != DBNull.Value)
                        article.CUniteVente = dr["CUniteVente"].ToString();
                    if (dr["TypeVente"] != DBNull.Value)
                        article.TypeVente = dr["TypeVente"].ToString();
                    if (dr["BAchat"] != DBNull.Value)
                        article.BAchat = bool.Parse(dr["BAchat"].ToString());
                    if (dr["BQuantite"] != DBNull.Value)
                        article.BQuantite = bool.Parse(dr["BQuantite"].ToString());
                    if (dr["BVente"] != DBNull.Value)
                        article.BVente = bool.Parse(dr["BVente"].ToString());
                    if (dr["BApprovisionnement"] != DBNull.Value)
                        article.BApprovisionnement = bool.Parse(dr["BApprovisionnement"].ToString());
                    if (dr["BActif"] != DBNull.Value)
                        article.BActif = bool.Parse(dr["BActif"].ToString());
                    if (dr["Fodec"] != DBNull.Value)
                        article.Fodec = decimal.Parse(dr["Fodec"].ToString());
                    if (dr["TPE"] != DBNull.Value)
                        article.TPE = decimal.Parse(dr["TPE"].ToString());
                    if (dr["TaxeDroitConsommation"] != DBNull.Value)
                        article.TaxeDroitConsommation = decimal.Parse(dr["TaxeDroitConsommation"].ToString());
                    if (dr["Poids"] != DBNull.Value)
                        article.Poids = decimal.Parse(dr["Poids"].ToString());
                    if (dr["PrixRevientInitial"] != DBNull.Value)
                        article.PrixRevientInitial = decimal.Parse(dr["PrixRevientInitial"].ToString());
                    if (dr["PrixPublic"] != DBNull.Value)
                        article.PrixPublic = decimal.Parse(dr["PrixPublic"].ToString());
                    if (dr["PrixRevient"] != DBNull.Value)
                        article.PrixRevient = decimal.Parse(dr["PrixRevient"].ToString());
                    if (dr["Indice"] != DBNull.Value)
                        article.Indice = int.Parse(dr["Indice"].ToString());
                    if (dr["PrioriteRemise"] != DBNull.Value)
                        article.PrioriteRemise = int.Parse(dr["PrioriteRemise"].ToString());
                    if (dr["StockACommander"] != DBNull.Value)
                        article.StockACommander = decimal.Parse(dr["StockACommander"].ToString());
                    if (dr["StockMax"] != DBNull.Value)
                        article.StockMax = decimal.Parse(dr["StockMax"].ToString());
                    if (dr["StockMin"] != DBNull.Value)
                        article.StockMin = decimal.Parse(dr["StockMin"].ToString());
                    if (dr["TauxNGP"] != DBNull.Value)
                        article.TauxNGP = decimal.Parse(dr["TauxNGP"].ToString());
                    if (dr["TauxProduction"] != DBNull.Value)
                        article.TauxProduction = decimal.Parse(dr["TauxProduction"].ToString());
                    if (dr["TauxRevient"] != DBNull.Value)
                        article.TauxRevient = decimal.Parse(dr["TauxRevient"].ToString());
                    if (dr["TotalQauntiteAchat"] != DBNull.Value)
                        article.TotalQauntiteAchat = decimal.Parse(dr["TotalQauntiteAchat"].ToString());
                    if (dr["Volume"] != DBNull.Value)
                        article.Volume = decimal.Parse(dr["Volume"].ToString());
                    if (dr["BBloquerPrixHT"] != DBNull.Value)
                        article.BBloquerPrixHT = bool.Parse(dr["BBloquerPrixHT"].ToString());
                    if (dr["BBloquerRemise"] != DBNull.Value)
                        article.BBloquerRemise = bool.Parse(dr["BBloquerRemise"].ToString());
                    if (dr["TypeAchat"] != DBNull.Value)
                        article.TypeAchat = dr["TypeAchat"].ToString();
                    if (dr["CTaxeAchat"] != DBNull.Value)
                        article.CTaxeAchat = dr["CTaxeAchat"].ToString();
                    if (dr["CUniteAchat"] != DBNull.Value)
                        article.CUniteAchat = dr["CUniteAchat"].ToString();
                    if (dr["BPrixMargeFixe"] != DBNull.Value)
                        article.BPrixMargeFixe = bool.Parse(dr["BPrixMargeFixe"].ToString());
                    if (dr["BGestionNumeroSerie"] != DBNull.Value)
                        article.BGestionNumeroSerie = bool.Parse(dr["BGestionNumeroSerie"].ToString());
                    if (dr["BGestionConsigne"] != DBNull.Value)
                        article.BGestionConsigne = bool.Parse(dr["BGestionConsigne"].ToString());
                    if (dr["PrixDevise"] != DBNull.Value)
                        article.PrixDevise = decimal.Parse(dr["PrixDevise"].ToString());
                    if (dr["StockReel"] != DBNull.Value)
                        article.StockReel = decimal.Parse(dr["StockReel"].ToString());
                    if (dr["BSpecial"] != DBNull.Value)
                        article.BSpecial = bool.Parse(dr["BSpecial"].ToString());
                    if (dr["CNatureVente"] != DBNull.Value)
                        article.CNatureVente = dr["CNatureVente"].ToString();
                    if (dr["TauxCommission"] != DBNull.Value)
                        article.TauxCommission = decimal.Parse(dr["TauxCommission"].ToString());
                    if (dr["BGestionLot"] != DBNull.Value)
                        article.BGestionLot = bool.Parse(dr["BGestionLot"].ToString());
                    if (dr["Image_Article"] != DBNull.Value)
                        article.Image_Article = (byte[])dr["Image_Article"];
                    if (dr["BTablette"] != DBNull.Value)
                        article.BTablette = bool.Parse(dr["BTablette"].ToString());
                    if (dr["bGestionLotMouvement"] != DBNull.Value)
                        article.bGestionLotMouvement = int.Parse(dr["bGestionLotMouvement"].ToString());
                    if (dr["bGestionLotEntree"] != DBNull.Value)
                        article.bGestionLotEntree = bool.Parse(dr["bGestionLotEntree"].ToString());
                    if (dr["bGestionLotSortee"] != DBNull.Value)
                        article.bGestionLotSortee = bool.Parse(dr["bGestionLotSortee"].ToString());
                    if (dr["CClient"] != DBNull.Value)
                        article.CClient = dr["CClient"].ToString();
                    if (dr["NbNiveau"] != DBNull.Value)
                        article.NbNiveau = int.Parse(dr["NbNiveau"].ToString());
                    if (dr["NbPiece"] != DBNull.Value)
                        article.NbPiece = int.Parse(dr["NbPiece"].ToString());
                    if (dr["ConsigneTempsMA"] != DBNull.Value)
                        article.ConsigneTempsMA = int.Parse(dr["ConsigneTempsMA"].ToString());
                    if (dr["ConsignePose"] != DBNull.Value)
                        article.ConsignePose = int.Parse(dr["ConsignePose"].ToString());
                    if (dr["NbBandes"] != DBNull.Value)
                        article.NbBandes = int.Parse(dr["NbBandes"].ToString());
                    if (dr["NbFlans"] != DBNull.Value)
                        article.NbFlans = int.Parse(dr["NbFlans"].ToString());
                    if (dr["ConsigneDechet"] != DBNull.Value)
                        article.ConsigneDechet = int.Parse(dr["ConsigneDechet"].ToString());
                    if (dr["CFournisseur"] != DBNull.Value)
                        article.CFournisseur = dr["CFournisseur"].ToString();
                    if (dr["CDimension"] != DBNull.Value)
                        article.CDimension = dr["CDimension"].ToString();
                    if (dr["CNomenclatureEncre"] != DBNull.Value)
                        article.CNomenclatureEncre = dr["CNomenclatureEncre"].ToString();
                    if (dr["CNomenclatureVernis"] != DBNull.Value)
                        article.CNomenclatureVernis = dr["CNomenclatureVernis"].ToString();
                    if (dr["CTypeFeuille"] != DBNull.Value)
                        article.CTypeFeuille = dr["CTypeFeuille"].ToString();

                    if (dr["BIndustriel"] != DBNull.Value)
                        article.BIndustriel = bool.Parse(dr["BIndustriel"].ToString());
                    if (dr["BAlimentaire"] != DBNull.Value)
                        article.BAlimentaire = bool.Parse(dr["BAlimentaire"].ToString());
                    if (dr["BOuvertureFacile"] != DBNull.Value)
                        article.BOuvertureFacile = bool.Parse(dr["BOuvertureFacile"].ToString());
                    if (dr["BRetreins"] != DBNull.Value)
                        article.BRetreins = bool.Parse(dr["BRetreins"].ToString());
                    if (dr["BBPANI"] != DBNull.Value)
                        article.BBPANI = bool.Parse(dr["BBPANI"].ToString());
                    if (dr["BVenteNegative"] != DBNull.Value)
                        article.BVenteNegative = bool.Parse(dr["BVenteNegative"].ToString());
                    if (dr["BStandard"] != DBNull.Value)
                        article.BStandard = bool.Parse(dr["BStandard"].ToString());
                    if (dr["bGestionPrixderevien"] != DBNull.Value)
                        article.bGestionPrixderevien = int.Parse(dr["bGestionPrixderevien"].ToString());
                    if (dr["CDevise"] != DBNull.Value)
                        article.CDevise = dr["CDevise"].ToString();
                    if (dr["BBloquerLibelle"] != DBNull.Value)
                        article.BBloquerLibelle = bool.Parse(dr["BBloquerLibelle"].ToString());
                    if (dr["CEtatArticle"] != DBNull.Value)
                        article.CEtatArticle = dr["CEtatArticle"].ToString();
                    if (dr["Ordre"] != DBNull.Value)
                        article.Ordre = int.Parse(dr["Ordre"].ToString());

                    article.ArticleEntrepots = ArticleEntrepotCollection.Charger(cArticle);
                    article.ArticlesPrix = ArticlePrixCollection.Charger(cArticle, true);
                    article.ArticleEquivalents = ArticleEquivalentCollection.Charger(cArticle);
                    article.ArticleComposants = ArticleComposantCollection.Charger(cArticle);
                    article.ArticleFournisseurs = ArticleFournisseurCollection.Charger(cArticle);
                    if (article.BGestionLot)
                        article.LotsArticle = LotArticleCollection.Charger(cArticle);
                }
            }
            return (article);
        }

        public static Article ChargerGestionLot(string cArticle)
        {
            Article article = null;
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Article_ChargerGestionLot";
                cmd.Parameters.AddWithValue("@CArticle", cArticle);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    article = new Article();
                    article.CArticle = dr["CArticle"].ToString();
                    if (dr["LibArticle"] != DBNull.Value)
                        article.LibArticle = dr["LibArticle"].ToString();
                }
            }
            return (article);
        }

        public static void MiseAJourTVA(string AncienTVA, string NouveauTVA, int ModifiePar, int sens)
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Transaction = transaction;
                    cmd.Connection = transaction.Connection;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "Article_MiseAJourTaxe";
                    cmd.Parameters.AddWithValue("@CTaxeAnc", AncienTVA);
                    cmd.Parameters.AddWithValue("@CTaxeNouv", NouveauTVA);
                    cmd.Parameters.AddWithValue("@ModifiePar", ModifiePar);
                    cmd.Parameters.AddWithValue("@PCModification", Environment.MachineName);
                    cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Sens", sens);


                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    cmd.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public static string ChargerIndice(string codification)
        {
            string indice = null;
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Article_ChargerIndice";
                cmd.Parameters.AddWithValue("@Codification", codification);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (dr["Indice"] != DBNull.Value)
                        indice = dr["Indice"].ToString();
                }
            }
            return (indice);
        }

        public void ArticleGratuites_Modifier() {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Transaction = transaction;
                    cmd.Connection = transaction.Connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "ArticleGratuites_Modifier";
                    cmd.Parameters.AddWithValue("@CArticle", this.CArticle);
                    cmd.Parameters.AddWithValue("@CGratuites", this.CGratuites);
                    cmd.Parameters.AddWithValue("@DateGratuitesDebut", this.DateGratuitesDebut);
                    cmd.Parameters.AddWithValue("@DateGratuitesFin", this.DateGratuitesFin);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void Importation() 
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Transaction = transaction;
                    cmd.Connection = transaction.Connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Article_Importation";

                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                }

                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public static void UpdateOrdre(int Ordre, string CArticle)
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Transaction = transaction;
                    cmd.Connection = transaction.Connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Article_UpdateOrdre";
                    cmd.Parameters.AddWithValue("@Ordre", Ordre);
                    cmd.Parameters.AddWithValue("@CArticle", CArticle);

                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                }

                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }

    [Serializable]
    public class ArticleCollection : List<Article>
    {
        public static DataSet ChargerVue(string cArticle, string cCategorie, string cFamille, string cType, string cTarif, string cNature, string cNatureVente, string cModele, string cModele1, string cModele2)
        {
            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "Article_Rpt_Charger";
                cmd.Parameters.AddWithValue("@CArticle", cArticle);
                cmd.Parameters.AddWithValue("@CCategorie", cCategorie);
                cmd.Parameters.AddWithValue("@CFamille", cFamille);
                cmd.Parameters.AddWithValue("@CType", cType);
                cmd.Parameters.AddWithValue("@CNature", cNature);
                cmd.Parameters.AddWithValue("@CNatureVente", cNatureVente);
                cmd.Parameters.AddWithValue("@CTarif", cTarif);
                cmd.Parameters.AddWithValue("@CModele", cModele);
                cmd.Parameters.AddWithValue("@CModele1", cModele1);
                cmd.Parameters.AddWithValue("@CModele2", cModele2);

                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "Article_Rpt_Charger");
            }

            return (ds);
        }

        public static DataSet ChargerVue(string article, string cCategorie, string cFamille, string cType, string cNature, string cNatureVente, string cModele, string cModele1, string cModele2, DateTime dateDeb, DateTime dateFin, string cEntrepot, int mouvement)
        {
            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Article_Mvt_Rpt_Charger";
                cmd.Parameters.AddWithValue("@CArticle", article);
                cmd.Parameters.AddWithValue("@CCategorie", cCategorie);
                cmd.Parameters.AddWithValue("@CFamille", cFamille);
                cmd.Parameters.AddWithValue("@CType", cType);
                cmd.Parameters.AddWithValue("@CNature", cNature);
                cmd.Parameters.AddWithValue("@CNatureVente", cNatureVente);

                cmd.Parameters.AddWithValue("@CModele", cModele);
                cmd.Parameters.AddWithValue("@CModele1", cModele1);
                cmd.Parameters.AddWithValue("@CModele2", cModele2);
                cmd.Parameters.AddWithValue("@DateDeb", dateDeb);
                cmd.Parameters.AddWithValue("@DateFin", dateFin);
                cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                cmd.Parameters.AddWithValue("@Mouvement", mouvement);
                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "Article_Rpt_Charger");
            }

            return (ds);
        }

        public static DataSet ChargerVue(string article, string cCategorie, string cFamille, string cType, string cNature, string cNatureVente, string cModele, string cModele1, string cModele2, DateTime dateDeb, DateTime dateFin, int Etat, string cEntrepot)
        {
            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Article_MvtTyp_Rpt_Charger";
                cmd.Parameters.AddWithValue("@CArticle", article);
                cmd.Parameters.AddWithValue("@CCategorie", cCategorie);
                cmd.Parameters.AddWithValue("@CFamille", cFamille);
                cmd.Parameters.AddWithValue("@CType", cType);
                cmd.Parameters.AddWithValue("@CNature", cNature);
                cmd.Parameters.AddWithValue("@CNatureVente", cNatureVente);
                cmd.Parameters.AddWithValue("@CModele", cModele);
                cmd.Parameters.AddWithValue("@CModele1", cModele1);
                cmd.Parameters.AddWithValue("@CModele2", cModele2);
                cmd.Parameters.AddWithValue("@DateDeb", dateDeb);
                cmd.Parameters.AddWithValue("@DateFin", dateFin);
                cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "Article_MvtTyp_Rpt_Charger");
            }

            return (ds);
        }

        public static DataSet ChargerVue2(string article, string cCategorie, string cFamille, string cType, string cNature, string cNatureVente, string cModele, string cModele1, string cModele2, DateTime dateDeb, DateTime dateFin, string cEntrepot, int mouvement)
        {
            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ArticleMvtTyp_Vue_Rechercher";
                cmd.Parameters.AddWithValue("@CArticle", article);
                cmd.Parameters.AddWithValue("@CCategorie", cCategorie);
                cmd.Parameters.AddWithValue("@CFamille", cFamille);
                cmd.Parameters.AddWithValue("@CType", cType);
                cmd.Parameters.AddWithValue("@CNature", cNature);
                cmd.Parameters.AddWithValue("@CNatureVente", cNatureVente);

                cmd.Parameters.AddWithValue("@CModele", cModele);
                cmd.Parameters.AddWithValue("@CModele1", cModele1);
                cmd.Parameters.AddWithValue("@CModele2", cModele2);
                cmd.Parameters.AddWithValue("@DateDeb", dateDeb);
                cmd.Parameters.AddWithValue("@DateFin", dateFin);
                cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                cmd.Parameters.AddWithValue("@Mouvement", mouvement);
                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "Article_MvtTyp_Rpt_Charger");
            }

            return (ds);
        }

        public static DataSet ChargerVue1(string article, string cCategorie, string cFamille, string cType, string cNature, string cNatureVente, string cModele, string cModele1, string cModele2, DateTime dateDeb, DateTime dateFin, string cEntrepot, int mouvement)
        {
            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ArticleMvt_Vue_Rechercher";
                cmd.Parameters.AddWithValue("@CArticle", article);
                cmd.Parameters.AddWithValue("@CCategorie", cCategorie);
                cmd.Parameters.AddWithValue("@CFamille", cFamille);
                cmd.Parameters.AddWithValue("@CType", cType);
                cmd.Parameters.AddWithValue("@CNature", cNature);
                cmd.Parameters.AddWithValue("@CNatureVente", cNatureVente);
                cmd.Parameters.AddWithValue("@CModele", cModele);
                cmd.Parameters.AddWithValue("@CModele1", cModele1);
                cmd.Parameters.AddWithValue("@CModele2", cModele2);
                cmd.Parameters.AddWithValue("@DateDeb", dateDeb);
                cmd.Parameters.AddWithValue("@DateFin", dateFin);
                cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                cmd.Parameters.AddWithValue("@Mouvement", mouvement);
                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "ArticleMvt_Rpt_Charger");
            }

            return (ds);
        }

        public static ArticleCollection Charger()
        {
            ArticleCollection articlecollection = new ArticleCollection();
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Article_Charger";
                cmd.Parameters.AddWithValue("@CArticle", DBNull.Value);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Article article = new Article();
                    article.CArticle = dr["CArticle"].ToString();
                    article.LibArticle = dr["LibArticle"].ToString();
                    article.CodeBarre = dr["CodeBarre"].ToString();
                    article.CCategorie = dr["CCategorie"].ToString();
                    if (dr["CCodification"] != DBNull.Value)
                        article.CCodification = dr["CCodification"].ToString();
                    article.CEmballageVente = dr["CEmballageVente"].ToString();
                    article.CEmballageAchat = dr["CEmballageAchat"].ToString();
                    article.CFamille = dr["CFamille"].ToString();
                    article.CModele1 = dr["CModele1"].ToString();
                    article.CModele2 = dr["CModele2"].ToString();
                    article.CModele = dr["CModele"].ToString();
                    article.CNature = dr["CNature"].ToString();
                    article.CodeProduction = dr["CodeProduction"].ToString();
                    article.CTaxeVente = dr["CTaxeVente"].ToString();
                    article.CType = dr["CType"].ToString();
                    article.CUniteVente = dr["CUniteVente"].ToString();
                    article.TypeVente = dr["TypeVente"].ToString();
                    if (dr["BAchat"] != DBNull.Value)
                        article.BAchat = bool.Parse(dr["BAchat"].ToString());
                    if (dr["BQuantite"] != DBNull.Value)
                        article.BQuantite = bool.Parse(dr["BQuantite"].ToString());
                    if (dr["BVente"] != DBNull.Value)
                        article.BVente = bool.Parse(dr["BVente"].ToString());
                    if (dr["BApprovisionnement"] != DBNull.Value)
                        article.BApprovisionnement = bool.Parse(dr["BApprovisionnement"].ToString());
                    if (dr["Fodec"] != DBNull.Value)
                        article.Fodec = decimal.Parse(dr["Fodec"].ToString());
                    if (dr["Poids"] != DBNull.Value)
                        article.Poids = decimal.Parse(dr["Poids"].ToString());
                    if (dr["PrixRevientInitial"] != DBNull.Value)
                        article.PrixRevientInitial = decimal.Parse(dr["PrixRevientInitial"].ToString());
                    if (dr["PrixPublic"] != DBNull.Value)
                        article.PrixPublic = decimal.Parse(dr["PrixPublic"].ToString());
                    if (dr["PrixRevient"] != DBNull.Value)
                        article.PrixRevient = decimal.Parse(dr["PrixRevient"].ToString());
                    if (dr["Indice"] != DBNull.Value)
                        article.Indice = int.Parse(dr["Indice"].ToString());
                    if (dr["PrioriteRemise"] != DBNull.Value)
                        article.PrioriteRemise = int.Parse(dr["PrioriteRemise"].ToString());
                    if (dr["StockACommander"] != DBNull.Value)
                        article.StockACommander = decimal.Parse(dr["StockACommander"].ToString());
                    if (dr["StockMax"] != DBNull.Value)
                        article.StockMax = decimal.Parse(dr["StockMax"].ToString());
                    if (dr["StockMin"] != DBNull.Value)
                        article.StockMin = decimal.Parse(dr["StockMin"].ToString());
                    if (dr["TauxNGP"] != DBNull.Value)
                        article.TauxNGP = decimal.Parse(dr["TauxNGP"].ToString());
                    if (dr["TauxProduction"] != DBNull.Value)
                        article.TauxProduction = decimal.Parse(dr["TauxProduction"].ToString());
                    if (dr["TauxRevient"] != DBNull.Value)
                        article.TauxRevient = decimal.Parse(dr["TauxRevient"].ToString());
                    if (dr["TotalQauntiteAchat"] != DBNull.Value)
                        article.TotalQauntiteAchat = decimal.Parse(dr["TotalQauntiteAchat"].ToString());
                    if (dr["Volume"] != DBNull.Value)
                        article.Volume = decimal.Parse(dr["Volume"].ToString());
                    if (dr["BBloquerPrixHT"] != DBNull.Value)
                        article.BBloquerPrixHT = bool.Parse(dr["BBloquerPrixHT"].ToString());
                    if (dr["BBloquerRemise"] != DBNull.Value)
                        article.BBloquerRemise = bool.Parse(dr["BBloquerRemise"].ToString());
                    article.TypeAchat = dr["TypeAchat"].ToString();
                    article.CTaxeAchat = dr["CTaxeAchat"].ToString();
                    article.CUniteAchat = dr["CUniteAchat"].ToString();
                    if (dr["BPrixMargeFixe"] != DBNull.Value)
                        article.BPrixMargeFixe = bool.Parse(dr["BPrixMargeFixe"].ToString());
                    if (dr["BGestionNumeroSerie"] != DBNull.Value)
                        article.BGestionNumeroSerie = bool.Parse(dr["BGestionNumeroSerie"].ToString());
                    if (dr["BGestionConsigne"] != DBNull.Value)
                        article.BGestionConsigne = bool.Parse(dr["BGestionConsigne"].ToString());
                    if (dr["PrixDevise"] != DBNull.Value)
                        article.PrixDevise = decimal.Parse(dr["PrixDevise"].ToString());
                    if (dr["StockReel"] != DBNull.Value)
                        article.StockReel = decimal.Parse(dr["StockReel"].ToString());
                    if (dr["BSpecial"] != DBNull.Value)
                        article.BSpecial = bool.Parse(dr["BSpecial"].ToString());
                    if (dr["TauxCommission"] != DBNull.Value)
                        article.TauxCommission = decimal.Parse(dr["TauxCommission"].ToString());
                    if (dr["CNatureVente"] != DBNull.Value)
                        article.CNatureVente = dr["CNatureVente"].ToString();
                    if (dr["BGestionLot"] != DBNull.Value)
                        article.BGestionLot = bool.Parse(dr["BGestionLot"].ToString());

                    article.ArticleEntrepots = ArticleEntrepotCollection.Charger(article.CArticle);
                  //  article.ArticlesPrix = ArticlePrixCollection.Charger(article.CArticle, true);
                    articlecollection.Add(article);
                }
                dr.Close();
            }
            return (articlecollection);
        }
    }
}