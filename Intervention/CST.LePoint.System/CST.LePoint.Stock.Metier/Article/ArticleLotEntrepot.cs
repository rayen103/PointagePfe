using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CST.LePoint.Stock.Metier
{
    [Serializable]
    public class ArticleLotEntrepot
    {
        #region Propriétés

        [XmlAttribute("CArticle")]
        [Bindable(true)]
        public string CArticle { get; set; }

        [XmlAttribute("CLot")]
        [Bindable(true)]
        public string CLot { get; set; }

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

        #endregion

        public ArticleLotEntrepot()
        {
        }

        public ArticleLotEntrepot(string cArticle, string cLot, string cEntrepot)
        {
            this.CArticle = cArticle;
            this.CLot = cLot;
            this.CEntrepot = cEntrepot;
        }

        public void Sauvegarder(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "ArticleLotEntrepot_Sauvegarder";
                cmd.Parameters.AddWithValue("@CArticle", CArticle);
                cmd.Parameters.AddWithValue("@CLot", CLot);
                cmd.Parameters.AddWithValue("@CEntrepot", CEntrepot);
                cmd.Parameters.AddWithValue("@EtageArticle", EtageArticle);
                cmd.Parameters.AddWithValue("@RangeArticle", RangeArticle);
                cmd.Parameters.AddWithValue("@StockACommander", StockACommander);
                cmd.Parameters.AddWithValue("@StockMax", StockMax);
                cmd.Parameters.AddWithValue("@StockMin", StockMin);
                cmd.Parameters.AddWithValue("@PCInsertion", PCInsertion);
                cmd.Parameters.AddWithValue("@CreePar", CreePar);
                cmd.Parameters.AddWithValue("@PCModification", PCModification);
                cmd.Parameters.AddWithValue("@ModifiePar", ModifiePar);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
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

                cmd.CommandText = "ArticleLotEntrepot_Supprimer";
                cmd.Parameters.AddWithValue("@CArticle", CArticle);
                cmd.Parameters.AddWithValue("@CLot", CLot);
                cmd.Parameters.AddWithValue("@CEntrepot", CEntrepot);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ArticleLotEntrepot Charger(string cArticle, string cLot ,string cEntrepot)
        {
            ArticleLotEntrepot articleLotEntrepot = null;
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ArticleLotEntrepot_Charger";
                cmd.Parameters.AddWithValue("@CArticle", cArticle);
                cmd.Parameters.AddWithValue("@CLot", cLot);
                cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    articleLotEntrepot = new ArticleLotEntrepot();
                    articleLotEntrepot.CArticle = dr["CArticle"].ToString();
                    articleLotEntrepot.CLot = dr["CLot"].ToString();
                    articleLotEntrepot.CEntrepot = dr["CEntrepot"].ToString();

                    if (dr["DateDerniereInventaire"] != DBNull.Value)
                        articleLotEntrepot.DateDerniereInventaire = DateTime.Parse(dr["DateDerniereInventaire"].ToString());
                    if (dr["EtageArticle"] != DBNull.Value)
                        articleLotEntrepot.EtageArticle = dr["EtageArticle"].ToString();
                    if (dr["RangeArticle"] != DBNull.Value)
                        articleLotEntrepot.RangeArticle = dr["RangeArticle"].ToString();
                    if (dr["StockACommander"] != DBNull.Value)
                        articleLotEntrepot.StockACommander = decimal.Parse(dr["StockACommander"].ToString());
                    if (dr["StockEnCommande"] != DBNull.Value)
                        articleLotEntrepot.StockEnCommande = decimal.Parse(dr["StockEnCommande"].ToString());
                    if (dr["StockEnCommandeDAP"] != DBNull.Value)
                        articleLotEntrepot.StockEnCommandeDAP = decimal.Parse(dr["StockEnCommandeDAP"].ToString());
                    if (dr["StockInitiale"] != DBNull.Value)
                        articleLotEntrepot.StockInitiale = decimal.Parse(dr["StockInitiale"].ToString());
                    if (dr["StockInventaire"] != DBNull.Value)
                        articleLotEntrepot.StockInventaire = decimal.Parse(dr["StockInventaire"].ToString());
                    if (dr["StockMax"] != DBNull.Value)
                        articleLotEntrepot.StockMax = decimal.Parse(dr["StockMax"].ToString());
                    if (dr["StockMin"] != DBNull.Value)
                        articleLotEntrepot.StockMin = decimal.Parse(dr["StockMin"].ToString());
                    if (dr["StockReel"] != DBNull.Value)
                        articleLotEntrepot.StockReel = decimal.Parse(dr["StockReel"].ToString());
                    if (dr["StockReserver"] != DBNull.Value)
                        articleLotEntrepot.StockReserver = decimal.Parse(dr["StockReserver"].ToString());
                }
                dr.Close();
            }
            return (articleLotEntrepot);
        }
    }

    [Serializable]
    public class ArticleLotEntrepotCollection : List<ArticleLotEntrepot>
    {
        public static ArticleLotEntrepotCollection Charger(string CArticle, string cLot)
        {
            ArticleLotEntrepotCollection articleLotEntrepotcollection = new ArticleLotEntrepotCollection();
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ArticleLotEntrepot_Charger";
                cmd.Parameters.AddWithValue("@CArticle", CArticle);
                cmd.Parameters.AddWithValue("@CLot", cLot);
                cmd.Parameters.AddWithValue("@CEntrepot", DBNull.Value);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ArticleLotEntrepot articleLotEntrepot = new ArticleLotEntrepot();
                    articleLotEntrepot.CArticle = dr["CArticle"].ToString();
                    articleLotEntrepot.CLot = dr["CLot"].ToString();
                    articleLotEntrepot.CEntrepot = dr["CEntrepot"].ToString();
                    if (dr["DateDerniereInventaire"] != DBNull.Value)
                        articleLotEntrepot.DateDerniereInventaire = DateTime.Parse(dr["DateDerniereInventaire"].ToString());
                    if (dr["EtageArticle"] != DBNull.Value)
                        articleLotEntrepot.EtageArticle = dr["EtageArticle"].ToString();
                    if (dr["RangeArticle"] != DBNull.Value)
                        articleLotEntrepot.RangeArticle = dr["RangeArticle"].ToString();
                    if (dr["StockACommander"] != DBNull.Value)
                        articleLotEntrepot.StockACommander = decimal.Parse(dr["StockACommander"].ToString());
                    if (dr["StockEnCommande"] != DBNull.Value)
                        articleLotEntrepot.StockEnCommande = decimal.Parse(dr["StockEnCommande"].ToString());
                    if (dr["StockEnCommandeDAP"] != DBNull.Value)
                        articleLotEntrepot.StockEnCommandeDAP = decimal.Parse(dr["StockEnCommandeDAP"].ToString());
                    if (dr["StockInitiale"] != DBNull.Value)
                        articleLotEntrepot.StockInitiale = decimal.Parse(dr["StockInitiale"].ToString());
                    if (dr["StockInventaire"] != DBNull.Value)
                        articleLotEntrepot.StockInventaire = decimal.Parse(dr["StockInventaire"].ToString());
                    if (dr["StockMax"] != DBNull.Value)
                        articleLotEntrepot.StockMax = decimal.Parse(dr["StockMax"].ToString());
                    if (dr["StockMin"] != DBNull.Value)
                        articleLotEntrepot.StockMin = decimal.Parse(dr["StockMin"].ToString());
                    if (dr["StockReel"] != DBNull.Value)
                        articleLotEntrepot.StockReel = decimal.Parse(dr["StockReel"].ToString());
                    if (dr["StockReserver"] != DBNull.Value)
                        articleLotEntrepot.StockReserver = decimal.Parse(dr["StockReserver"].ToString());

                    articleLotEntrepotcollection.Add(articleLotEntrepot);
                }
                dr.Close();
            }
            return (articleLotEntrepotcollection);
        }

        public static ArticleLotEntrepotCollection Charger()
        {
            ArticleLotEntrepotCollection articleLotEntrepotCollection = new ArticleLotEntrepotCollection();
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ArticleLotEntrepot_Charger";
                cmd.Parameters.AddWithValue("@CArticle", DBNull.Value);
                cmd.Parameters.AddWithValue("@CLot", DBNull.Value);
                cmd.Parameters.AddWithValue("@CEntrepot", DBNull.Value);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ArticleLotEntrepot articleLotEntrepot = new ArticleLotEntrepot();
                    articleLotEntrepot.CArticle = dr["CArticle"].ToString();
                    articleLotEntrepot.CLot = dr["CLot"].ToString();
                    articleLotEntrepot.CEntrepot = dr["CEntrepot"].ToString();

                    if (dr["DateDerniereInventaire"] != DBNull.Value)
                        articleLotEntrepot.DateDerniereInventaire = DateTime.Parse(dr["DateDerniereInventaire"].ToString());
                    if (dr["EtageArticle"] != DBNull.Value)
                        articleLotEntrepot.EtageArticle = dr["EtageArticle"].ToString();
                    if (dr["RangeArticle"] != DBNull.Value)
                        articleLotEntrepot.RangeArticle = dr["RangeArticle"].ToString();
                    if (dr["StockACommander"] != DBNull.Value)
                        articleLotEntrepot.StockACommander = decimal.Parse(dr["StockACommander"].ToString());
                    if (dr["StockEnCommande"] != DBNull.Value)
                        articleLotEntrepot.StockEnCommande = decimal.Parse(dr["StockEnCommande"].ToString());
                    if (dr["StockEnCommandeDAP"] != DBNull.Value)
                        articleLotEntrepot.StockEnCommandeDAP = decimal.Parse(dr["StockEnCommandeDAP"].ToString());
                    if (dr["StockInitiale"] != DBNull.Value)
                        articleLotEntrepot.StockInitiale = decimal.Parse(dr["StockInitiale"].ToString());
                    if (dr["StockInventaire"] != DBNull.Value)
                        articleLotEntrepot.StockInventaire = decimal.Parse(dr["StockInventaire"].ToString());
                    if (dr["StockMax"] != DBNull.Value)
                        articleLotEntrepot.StockMax = decimal.Parse(dr["StockMax"].ToString());
                    if (dr["StockMin"] != DBNull.Value)
                        articleLotEntrepot.StockMin = decimal.Parse(dr["StockMin"].ToString());
                    if (dr["StockReel"] != DBNull.Value)
                        articleLotEntrepot.StockReel = decimal.Parse(dr["StockReel"].ToString());
                    if (dr["StockReserver"] != DBNull.Value)
                        articleLotEntrepot.StockReserver = decimal.Parse(dr["StockReserver"].ToString());

                    articleLotEntrepotCollection.Add(articleLotEntrepot);
                }
                dr.Close();
            }
            return (articleLotEntrepotCollection);
        }
    }
}

