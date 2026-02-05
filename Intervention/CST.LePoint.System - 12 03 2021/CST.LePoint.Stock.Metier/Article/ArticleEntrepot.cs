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
    public class ArticleEntrepot
    {
        [XmlAttribute("CArticle")]
        [Bindable(true)]
        public string CArticle { get; set; }

        [XmlAttribute("CEntrepot")]
        [Bindable(true)]
        public string CEntrepot { get; set; }

        [XmlAttribute("DateDerniereInventaire")]
        [Bindable(true)]
        public DateTime? DateDerniereInventaire { get; set; }

        [XmlAttribute("EtageArticle")]
        [Bindable(true)]
        public string EtageArticle { get; set; }

        [XmlAttribute("RangeArticle")]
        [Bindable(true)]
        public string RangeArticle { get; set; }

        [XmlAttribute("StockACommander")]
        [Bindable(true)]
        public decimal StockACommander { get; set; }

        [XmlAttribute("StockEnCommande")]
        [Bindable(true)]
        public decimal StockEnCommande { get; set; }

        [XmlAttribute("StockEnCommandeDAP")]
        [Bindable(true)]
        public decimal StockEnCommandeDAP { get; set; }

        [XmlAttribute("StockInitial")]
        [Bindable(true)]
        public decimal StockInitiale { get; set; }

        [XmlAttribute("StockInventaire")]
        [Bindable(true)]
        public decimal StockInventaire { get; set; }

        [XmlAttribute("StockMax")]
        [Bindable(true)]
        public decimal StockMax { get; set; }

        [XmlAttribute("StockMin")]
        [Bindable(true)]
        public decimal StockMin { get; set; }

        [XmlAttribute("DateInsertion")]
        [Bindable(true)]
        public decimal StockReel { get; set; }

        [XmlAttribute("StockReserver")]
        [Bindable(true)]
        public decimal StockReserver { get; set; }

        [XmlAttribute("BActif")]
        [Bindable(true)]
        public bool BActif { get; set; }

        [XmlAttribute("DateInsertion")]
        [Bindable(true)]
        public DateTime? DateInsertion { get; set; }

        [XmlAttribute("DateModification")]
        [Bindable(true)]
        public DateTime? DateModification { get; set; }

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

        public ArticleEntrepot()
        {
        }

        public ArticleEntrepot(string carticle, string centrepot)
        {
            this.CArticle = carticle;
            this.CEntrepot = centrepot;
        }

        public void Sauvegarder(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "ArticleEntrepot_Sauvegarder";
                cmd.Parameters.AddWithValue("@CArticle", CArticle);
                cmd.Parameters.AddWithValue("@CEntrepot", CEntrepot);
                cmd.Parameters.AddWithValue("@EtageArticle", EtageArticle);
                cmd.Parameters.AddWithValue("@RangeArticle", RangeArticle);
                cmd.Parameters.AddWithValue("@StockACommander", StockACommander);
                cmd.Parameters.AddWithValue("@StockMax", StockMax);
                cmd.Parameters.AddWithValue("@StockMin", StockMin);
                cmd.Parameters.AddWithValue("@BActif", BActif);
                cmd.Parameters.AddWithValue("@PCInsertion", PCInsertion);
                cmd.Parameters.AddWithValue("@CreePar", CreePar);
                cmd.Parameters.AddWithValue("@PCModification", PCModification);
                cmd.Parameters.AddWithValue("@ModifiePar", ModifiePar);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null || parametre.Value == "")
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Sauvegarder()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                Sauvegarder(transaction);
            }
        }

        public void Supprimer(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "ArticleEntrepot_Supprimer";
                cmd.Parameters.AddWithValue("@CArticle", CArticle);
                cmd.Parameters.AddWithValue("@CEntrepot", CEntrepot);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ArticleEntrepot Charger(string carticle, string centrepot)
        {
            ArticleEntrepot articleentrepot = null;
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString2"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ArticleEntrepot_Charger";
                cmd.Parameters.AddWithValue("@CArticle", carticle);
                cmd.Parameters.AddWithValue("@CEntrepot", centrepot);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    articleentrepot = new ArticleEntrepot();
                    articleentrepot.CArticle = dr["CArticle"].ToString();
                    articleentrepot.CEntrepot = dr["CEntrepot"].ToString();

                    if (dr["DateDerniereInventaire"] != DBNull.Value)
                        articleentrepot.DateDerniereInventaire = DateTime.Parse(dr["DateDerniereInventaire"].ToString());
                    if (dr["EtageArticle"] != DBNull.Value)
                        articleentrepot.EtageArticle = dr["EtageArticle"].ToString();
                    if (dr["RangeArticle"] != DBNull.Value)
                        articleentrepot.RangeArticle = dr["RangeArticle"].ToString();
                    if (dr["StockACommander"] != DBNull.Value)
                        articleentrepot.StockACommander = decimal.Parse(dr["StockACommander"].ToString());
                    if (dr["StockEnCommande"] != DBNull.Value)
                        articleentrepot.StockEnCommande = decimal.Parse(dr["StockEnCommande"].ToString());
                    if (dr["StockEnCommandeDAP"] != DBNull.Value)
                        articleentrepot.StockEnCommandeDAP = decimal.Parse(dr["StockEnCommandeDAP"].ToString());
                    if (dr["StockInitiale"] != DBNull.Value)
                        articleentrepot.StockInitiale = decimal.Parse(dr["StockInitiale"].ToString());
                    if (dr["StockInventaire"] != DBNull.Value)
                        articleentrepot.StockInventaire = decimal.Parse(dr["StockInventaire"].ToString());
                    if (dr["StockMax"] != DBNull.Value)
                        articleentrepot.StockMax = decimal.Parse(dr["StockMax"].ToString());
                    if (dr["StockMin"] != DBNull.Value)
                        articleentrepot.StockMin = decimal.Parse(dr["StockMin"].ToString());
                    if (dr["StockReel"] != DBNull.Value)
                        articleentrepot.StockReel = decimal.Parse(dr["StockReel"].ToString());
                    if (dr["StockReserver"] != DBNull.Value)
                        articleentrepot.StockReserver = decimal.Parse(dr["StockReserver"].ToString());
                    if (dr["BActif"] != DBNull.Value)
                        articleentrepot.BActif = bool.Parse(dr["BActif"].ToString());
                }
                dr.Close();
            }
            return (articleentrepot);
        }
    }

    [Serializable]
    public class ArticleEntrepotCollection : List<ArticleEntrepot>
    {
        public static DataSet ChargerVue(string cCategorie, string cFamille, string cType, string cEntrepot, string cNature, string cModele, string cModele1, string cModele2)
        {
            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Article_Quantite_Rpt_Charger";
                cmd.Parameters.AddWithValue("@CCategorie", cCategorie);
                cmd.Parameters.AddWithValue("@CFamille", cFamille);
                cmd.Parameters.AddWithValue("@CType", cType);
                cmd.Parameters.AddWithValue("@CNature", cNature);
                cmd.Parameters.AddWithValue("@CModele", cModele);
                cmd.Parameters.AddWithValue("@CModele1", cModele1);
                cmd.Parameters.AddWithValue("@CModele2", cModele2);
                cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                cmd.Parameters.AddWithValue("@CArticle", DBNull.Value);

                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "ArticleEntrepotAlerte_Rpt_Charger");
            }
            return (ds);
        }

        public static DataSet ChargerVue(string cCategorie, string cFamille, string cType, string cEntrepot, string cNature, string cModele, string cModele1, string cModele2, string cArticle)
        {
            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Article_Quantite_Rpt_Charger";
                cmd.Parameters.AddWithValue("@CCategorie", cCategorie);
                cmd.Parameters.AddWithValue("@CFamille", cFamille);
                cmd.Parameters.AddWithValue("@CType", cType);
                cmd.Parameters.AddWithValue("@CNature", cNature);
                cmd.Parameters.AddWithValue("@CModele", cModele);
                cmd.Parameters.AddWithValue("@CModele1", cModele1);
                cmd.Parameters.AddWithValue("@CModele2", cModele2);
                cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                cmd.Parameters.AddWithValue("@CArticle", cArticle);

                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "ArticleEntrepotAlerte_Rpt_Charger");
            }
            return (ds);
        }

        public static DataSet ChargerVuePreInventaire(string cArticle, string cCategorie, string cFamille, string cType, string cEntrepot, string cModele, string cNature, string cS_Modele1, string cS_Modele2, int bQuantite, int order)
        {
            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PreInventaire_Rpt_Chercher";
                cmd.Parameters.AddWithValue("@CArticle", cArticle);
                cmd.Parameters.AddWithValue("@CCategorie", cCategorie);
                cmd.Parameters.AddWithValue("@CFamille", cFamille);
                cmd.Parameters.AddWithValue("@CType", cType);
                cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                cmd.Parameters.AddWithValue("@CModele", cModele);
                cmd.Parameters.AddWithValue("@CNature", cNature);
                cmd.Parameters.AddWithValue("@CSousModele1", cS_Modele1);
                cmd.Parameters.AddWithValue("@CSousModele2", cS_Modele2);
                cmd.Parameters.AddWithValue("@BQuantite", bQuantite);
                cmd.Parameters.AddWithValue("@Ordre", order);

                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "PreInventaire_Rpt_Charger");
            }
            return (ds);
        }

        public static DataSet ChargerVue(string cArticle, string cEntrepot, int bMouvemente, int bStockEnCommande, DateTime dateDeb, DateTime dateFin, string natureVente)
        {
            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Article_En_Souffrance_Rpt";
                cmd.Parameters.AddWithValue("@CArticle", cArticle);
                cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                cmd.Parameters.AddWithValue("@BMouvemente", bMouvemente);
                cmd.Parameters.AddWithValue("@BStockEnCommande", bStockEnCommande);
                cmd.Parameters.AddWithValue("@DateDeb", dateDeb);
                cmd.Parameters.AddWithValue("@DateFin", dateFin);
                cmd.Parameters.AddWithValue("@NatureVente", natureVente);
                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "Article_En_Souffrance_Rpt");
            }
            return (ds);
        }

        public static DataSet ChargerVue(string cArticle, string cCategorie, string cFamille, string cType, string cTarif, string cNature, string cModele, string cModele1, string cModele2, string cEntrepot)
        {
            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "Nomenclature_Article_Rpt_Charger";
                cmd.Parameters.AddWithValue("@CArticle", cArticle);
                cmd.Parameters.AddWithValue("@CCategorie", cCategorie);
                cmd.Parameters.AddWithValue("@CFamille", cFamille);
                cmd.Parameters.AddWithValue("@CType", cType);
                cmd.Parameters.AddWithValue("@CNature", cNature);
                cmd.Parameters.AddWithValue("@CTarif", cTarif);
                cmd.Parameters.AddWithValue("@CModele", cModele);
                cmd.Parameters.AddWithValue("@CModele1", cModele1);
                cmd.Parameters.AddWithValue("@CModele2", cModele2);
                cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);

                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "Nomenclature_Article_Rpt_Charger");
            }

            return (ds);
        }

        public static DataSet ChargerVue(string cCategorie, string cFamille, string cType, string cEntrepot, string cNature, string cModele, string cModele1, string cModele2, string cArticle, string cTarif, int bStock, int bGroup, int actif, int bEntrepot, DateTime dateDebut, DateTime dateFin, int bMvt)
        {
            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "StockEnValeurGrp_Vue_Rechercher";
                cmd.Parameters.AddWithValue("@CCategorie", cCategorie);
                cmd.Parameters.AddWithValue("@CFamille", cFamille);
                cmd.Parameters.AddWithValue("@CType", cType);
                cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                cmd.Parameters.AddWithValue("@CModele", cModele);
                cmd.Parameters.AddWithValue("@CModele1", cModele1);
                cmd.Parameters.AddWithValue("@CModele2", cModele2);
                cmd.Parameters.AddWithValue("@CNature", cNature);
                cmd.Parameters.AddWithValue("@CTarif", cTarif);
                cmd.Parameters.AddWithValue("@CArticle", cArticle);
                cmd.Parameters.AddWithValue("@BGroup", bGroup);
                cmd.Parameters.AddWithValue("@Actif", actif);
                cmd.Parameters.AddWithValue("@BStock", bStock);
                cmd.Parameters.AddWithValue("@BEntrepot", bEntrepot);
                cmd.Parameters.AddWithValue("@BMvt", bMvt);
                cmd.Parameters.AddWithValue("@DateDebut", dateDebut);
                cmd.Parameters.AddWithValue("@DateFin", dateFin);
                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "Stock_ValeurGrp_Categorie_Charger");
            }
            return (ds);
        }

        public static DataSet ChargerVue(string cCategorie, string cFamille, string cType, string cEntrepot, string cNature, string cModele, string cModele1, string cModele2, string cArticle, string cTarif, int bGroup, DateTime dateDeb, DateTime dateFin)
        {
            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "StockEnValeurParPeriode_Vue_Rechercher";
                cmd.Parameters.AddWithValue("@CCategorie", cCategorie);
                cmd.Parameters.AddWithValue("@CFamille", cFamille);
                cmd.Parameters.AddWithValue("@CType", cType);
                cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                cmd.Parameters.AddWithValue("@CModele", cModele);
                cmd.Parameters.AddWithValue("@CModele1", cModele1);
                cmd.Parameters.AddWithValue("@CModele2", cModele2);
                cmd.Parameters.AddWithValue("@CNature", cNature);
                cmd.Parameters.AddWithValue("@CTarif", cTarif);
                cmd.Parameters.AddWithValue("@CArticle", cArticle);
                cmd.Parameters.AddWithValue("@BGroup", bGroup);
                cmd.Parameters.AddWithValue("@DateDeb", dateDeb);
                cmd.Parameters.AddWithValue("@DateFin", dateFin);
                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "Stock_ValeurGrp_Categorie_Charger");
            }
            return (ds);
        }

        public static ArticleEntrepotCollection Charger(string CArticle)
        {
            ArticleEntrepotCollection articleentrepotcollection = new ArticleEntrepotCollection();
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ArticleEntrepot_Charger";
                cmd.Parameters.AddWithValue("@CArticle", CArticle);
                cmd.Parameters.AddWithValue("@CEntrepot", DBNull.Value);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ArticleEntrepot articleentrepot = new ArticleEntrepot();
                    articleentrepot.CArticle = dr["CArticle"].ToString();
                    articleentrepot.CEntrepot = dr["CEntrepot"].ToString();
                    if (dr["DateDerniereInventaire"] != DBNull.Value)
                        articleentrepot.DateDerniereInventaire = DateTime.Parse(dr["DateDerniereInventaire"].ToString());
                    if (dr["EtageArticle"] != DBNull.Value)
                        articleentrepot.EtageArticle = dr["EtageArticle"].ToString();
                    if (dr["RangeArticle"] != DBNull.Value)
                        articleentrepot.RangeArticle = dr["RangeArticle"].ToString();
                    if (dr["StockACommander"] != DBNull.Value)
                        articleentrepot.StockACommander = decimal.Parse(dr["StockACommander"].ToString());
                    if (dr["StockEnCommande"] != DBNull.Value)
                        articleentrepot.StockEnCommande = decimal.Parse(dr["StockEnCommande"].ToString());
                    if (dr["StockEnCommandeDAP"] != DBNull.Value)
                        articleentrepot.StockEnCommandeDAP = decimal.Parse(dr["StockEnCommandeDAP"].ToString());
                    if (dr["StockInitiale"] != DBNull.Value)
                        articleentrepot.StockInitiale = decimal.Parse(dr["StockInitiale"].ToString());
                    if (dr["StockInventaire"] != DBNull.Value)
                        articleentrepot.StockInventaire = decimal.Parse(dr["StockInventaire"].ToString());
                    if (dr["StockMax"] != DBNull.Value)
                        articleentrepot.StockMax = decimal.Parse(dr["StockMax"].ToString());
                    if (dr["StockMin"] != DBNull.Value)
                        articleentrepot.StockMin = decimal.Parse(dr["StockMin"].ToString());
                    if (dr["StockReel"] != DBNull.Value)
                        articleentrepot.StockReel = decimal.Parse(dr["StockReel"].ToString());
                    if (dr["StockReserver"] != DBNull.Value)
                        articleentrepot.StockReserver = decimal.Parse(dr["StockReserver"].ToString());
                    if (dr["BActif"] != DBNull.Value)
                        articleentrepot.BActif = bool.Parse(dr["BActif"].ToString());

                    articleentrepotcollection.Add(articleentrepot);
                }
                dr.Close();
            }
            return (articleentrepotcollection);
        }

        public static ArticleEntrepotCollection Charger()
        {
            ArticleEntrepotCollection articleentrepotcollection = new ArticleEntrepotCollection();
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ArticleEntrepot_Charger";
                cmd.Parameters.AddWithValue("@CArticle", DBNull.Value);
                cmd.Parameters.AddWithValue("@CEntrepot", DBNull.Value);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ArticleEntrepot articleentrepot = new ArticleEntrepot();
                    articleentrepot.CArticle = dr["CArticle"].ToString();
                    articleentrepot.CEntrepot = dr["CEntrepot"].ToString();

                    if (dr["DateDerniereInventaire"] != DBNull.Value)
                        articleentrepot.DateDerniereInventaire = DateTime.Parse(dr["DateDerniereInventaire"].ToString());
                    if (dr["EtageArticle"] != DBNull.Value)
                        articleentrepot.EtageArticle = dr["EtageArticle"].ToString();
                    if (dr["RangeArticle"] != DBNull.Value)
                        articleentrepot.RangeArticle = dr["RangeArticle"].ToString();
                    if (dr["StockACommander"] != DBNull.Value)
                        articleentrepot.StockACommander = decimal.Parse(dr["StockACommander"].ToString());
                    if (dr["StockEnCommande"] != DBNull.Value)
                        articleentrepot.StockEnCommande = decimal.Parse(dr["StockEnCommande"].ToString());
                    if (dr["StockEnCommandeDAP"] != DBNull.Value)
                        articleentrepot.StockEnCommandeDAP = decimal.Parse(dr["StockEnCommandeDAP"].ToString());
                    if (dr["StockInitiale"] != DBNull.Value)
                        articleentrepot.StockInitiale = decimal.Parse(dr["StockInitiale"].ToString());
                    if (dr["StockInventaire"] != DBNull.Value)
                        articleentrepot.StockInventaire = decimal.Parse(dr["StockInventaire"].ToString());
                    if (dr["StockMax"] != DBNull.Value)
                        articleentrepot.StockMax = decimal.Parse(dr["StockMax"].ToString());
                    if (dr["StockMin"] != DBNull.Value)
                        articleentrepot.StockMin = decimal.Parse(dr["StockMin"].ToString());
                    if (dr["StockReel"] != DBNull.Value)
                        articleentrepot.StockReel = decimal.Parse(dr["StockReel"].ToString());
                    if (dr["StockReserver"] != DBNull.Value)
                        articleentrepot.StockReserver = decimal.Parse(dr["StockReserver"].ToString());
                    if (dr["BActif"] != DBNull.Value)
                        articleentrepot.BActif = bool.Parse(dr["BActif"].ToString());

                    articleentrepotcollection.Add(articleentrepot);
                }
                dr.Close();
            }
            return (articleentrepotcollection);
        }
    }
}