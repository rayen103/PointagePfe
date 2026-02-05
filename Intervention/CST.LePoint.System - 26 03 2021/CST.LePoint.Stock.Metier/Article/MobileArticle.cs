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
using CST.LePoint.Stock.Metier;

namespace CST.Stock.Metier.Article
{
    [Serializable]
    public class MobileArticle
    {
        [XmlAttribute("codeArt")]
        [Bindable(true)]
        public string codeArt { get; set; }

        [XmlAttribute("libArt")]
        [Bindable(true)]
        public string libArt { get; set; }

        [XmlAttribute("CUnite")]
        [Bindable(true)]
        public string CUnite { get; set; }

        [XmlAttribute("StockReel")]
        [Bindable(true)]
        public decimal StockReel { get; set; }

        [XmlAttribute("qteOt")]
        [Bindable(true)]
        public decimal qteOt { get; set; }

        [XmlAttribute("qtePrep")]
        [Bindable(true)]
        public decimal qtePrep { get; set; }

        [XmlAttribute("qteRes")]
        [Bindable(true)]
        public decimal qteRes { get; set; }

        [XmlAttribute("Prix")]
        [Bindable(true)]
        public decimal Prix { get; set; }

        [XmlAttribute("ImageArt")]
        [Bindable(true)]
        public string ImageArt { get; set; }

        [XmlAttribute("codefamille")]
        [Bindable(true)]
        public string codefamille { get; set; }

        [XmlAttribute("Dividende")]
        [Bindable(true)]
        public decimal Dividende { get; set; }

        [XmlAttribute("Diviseur")]
        [Bindable(true)]
        public decimal Diviseur { get; set; }

        [XmlAttribute("CGratuites")]
        [Bindable(true)]
        public string CGratuites { get; set; }

        [XmlAttribute("DateGratuitesDebut")]
        [Bindable(true)]
        public DateTime? DateGratuitesDebut { get; set; }

        [XmlAttribute("DateGratuitesFin")]
        [Bindable(true)]
        public DateTime? DateGratuitesFin { get; set; }

        [XmlAttribute("TVA")]
        [Bindable(true)]
        public decimal TVA { get; set; }

        public MobileArticle()
        {

        }
        public static MobileArticle Charger(string cArticle)
        {
            MobileArticle article = null;

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Mobile_article_Charger";
                cmd.Parameters.AddWithValue("@CArticle", cArticle);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    article = new MobileArticle();

                    article.codeArt = dr["CArticle"].ToString();
                    if (dr["LibArticle"] != DBNull.Value)
                        article.libArt = dr["LibArticle"].ToString();
                    if(dr["DateGratuitesDebut"]!=DBNull.Value && dr["DateGratuitesFin"]!=DBNull.Value )
                    { 
                    DateTime DateDG = Convert.ToDateTime(dr["DateGratuitesDebut"]).Date;
                    DateTime DateGF = Convert.ToDateTime(dr["DateGratuitesFin"]).Date;
                    DateTime time = DateTime.Now.Date;
                    if (time >= DateDG && time <= DateGF)
                    {
                        if (dr["CGratuites"] != DBNull.Value)
                            article.CGratuites = dr["CGratuites"].ToString();
                     
                            if (dr["DateGratuitesFin"] != DBNull.Value)
                                article.DateGratuitesFin = Convert.ToDateTime(dr["DateGratuitesFin"].ToString());
                            if (dr["DateGratuitesDebut"] != DBNull.Value)
                                article.DateGratuitesDebut = Convert.ToDateTime(dr["DateGratuitesDebut"].ToString());
                    }
                    }
                }
                return (article);
            }
        }

        [Serializable]
        public class MobileArticleCollection : List<MobileArticle>
        {
            public MobileArticleCollection()
            {
            }

            public static MobileArticleCollection Charger(string famille, string CClient, string Ctarif, int page)
            {
                DateTime time = DateTime.Now.Date;
                MobileArticleCollection list = new MobileArticleCollection();
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    connection.Open();
                    try
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = connection;
                        SqlDataReader reader;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Mobile_familleArticles_charger";
                        cmd.Parameters.AddWithValue("@CFamille", famille);
                        cmd.Parameters.AddWithValue("@page", page);
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            MobileArticle article = new MobileArticle();
                            article.codeArt = reader["codeArt"] == DBNull.Value ? "" : reader["codeArt"].ToString();
                            article.libArt = reader["libArt"] == DBNull.Value ? "" : reader["libArt"].ToString();
                            article.CUnite = reader["CUnite"] == DBNull.Value ? "" : reader["CUnite"].ToString();
                            article.StockReel = reader["qteOt"] == DBNull.Value ? 0 : decimal.Parse(reader["qteOt"].ToString());
                            article.qtePrep = 0;
                            article.qteRes = 0;
                            article.ImageArt = reader["ImageArt"] == DBNull.Value ? "" : "data:image/png;base64," + Convert.ToBase64String((byte[])reader["ImageArt"]);
                            article.codefamille = famille;
                            article.Dividende = 0;
                            article.Diviseur = 1;
                            if (reader["DateDG"] != DBNull.Value && reader["DateGF"] != DBNull.Value && reader["Dividende"] != DBNull.Value && reader["Diviseur"] != DBNull.Value)
                            {
                                DateTime DateDG = Convert.ToDateTime(reader["DateDG"]).Date;
                                DateTime DateGF = Convert.ToDateTime(reader["DateGF"]).Date;
                                if (time >= DateDG && time <= DateGF)
                                {
                                    article.Dividende = reader["Dividende"] == DBNull.Value ? 0 : decimal.Parse(reader["Dividende"].ToString());
                                    article.Diviseur = reader["Diviseur"] == DBNull.Value ? 1 : decimal.Parse(reader["Diviseur"].ToString());
                                }
                            }
                            article.TVA = decimal.Parse(reader["TVA"].ToString());

                            ArticlePrix articlePrix = ArticlePrix.Charger(article.codeArt, Ctarif);
                            if (articlePrix != null)
                            {
                                article.Prix = articlePrix.PrixHT;
                            }
                            else
                                article.Prix = 0;
                            list.Add(article);
                        }
                        reader.Close();
                        connection.Close();

                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    return list;
                }
            }
            
            public static MobileArticleCollection ChargerParFamilleType(string famille, string type, string CClient, int page)
            {
                DateTime time = DateTime.Now.Date;
                MobileArticleCollection list = new MobileArticleCollection();
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    connection.Open();
                    try
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = connection;
                        SqlDataReader reader;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Mobile_ArticleFamilleType_Charger";
                        cmd.Parameters.AddWithValue("@CFamille", famille);
                        cmd.Parameters.AddWithValue("@CType", type);
                        cmd.Parameters.AddWithValue("@CClient", CClient);
                        cmd.Parameters.AddWithValue("@page", page);
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            MobileArticle article = new MobileArticle();
                            article.codeArt = reader["codeArt"] == DBNull.Value ? "" : reader["codeArt"].ToString();
                            article.libArt = reader["libArt"] == DBNull.Value ? "" : reader["libArt"].ToString();
                            article.CUnite = reader["CUnite"] == DBNull.Value ? "" : reader["CUnite"].ToString();
                            article.StockReel = reader["qteOt"] == DBNull.Value ? 0 : decimal.Parse(reader["qteOt"].ToString());
                            article.qtePrep = 0;
                            article.qteRes = 0;
                            article.ImageArt = reader["ImageArt"] == DBNull.Value ? "" : "data:image/png;base64," + Convert.ToBase64String((byte[])reader["ImageArt"]);
                            article.codefamille = famille;
                            article.Dividende = 0;
                            article.Diviseur = 1;
                            if (reader["DateDG"] != DBNull.Value && reader["DateGF"] != DBNull.Value && reader["Dividende"] != DBNull.Value && reader["Diviseur"] != DBNull.Value)
                            {
                                DateTime DateDG = Convert.ToDateTime(reader["DateDG"]).Date;
                                DateTime DateGF = Convert.ToDateTime(reader["DateGF"]).Date;
                                if (time >= DateDG && time <= DateGF)
                                {
                                    article.Dividende = reader["Dividende"] == DBNull.Value ? 0 : decimal.Parse(reader["Dividende"].ToString());
                                    article.Diviseur = reader["Diviseur"] == DBNull.Value ? 1 : decimal.Parse(reader["Diviseur"].ToString());
                                }
                            }
                            article.TVA = decimal.Parse(reader["TVA"].ToString());
                            article.Prix = (decimal)reader["PrixHT"];
                            list.Add(article);
                        }
                        reader.Close();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        connection.Close();                        
                    }
                    return list;
                }
            }
        }

    }
}