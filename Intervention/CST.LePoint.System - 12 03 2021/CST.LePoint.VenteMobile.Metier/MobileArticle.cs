using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST.LePoint.VenteMobile.Metier
{
    [Serializable]
    public class MobileArticle
    {
        public string NOrdreMission { get; set; }
        public string CArticle { get; set; }
        public string Etat { get; set; }
        public string ColorEtat { get; set; }
        public string libArticle { get; set; }
        public string CGratuites { get; set; }
        public string CUnite { get; set; }
        public DateTime? DateGratuitesDebut { get; set; }
        public DateTime? DateGratuitesFin { get; set; }
        public int Dividende { get; set; }
        public int Diviseur { get; set; }
        public string ImageArt { get; set; }
        public decimal PrixHT { get; set; }
        public string CFamille { get; set; }
        public string CType { get; set; }
        public int Gratuite { get; set; }
        public decimal Montant { get; set; }
        public decimal Quantite { get; set; }
        public decimal StockReel { get; set; }
        public decimal Remise { get; set; }
        public int Index { get; set; }
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

                    article.CArticle = dr["CArticle"].ToString();
                    if (dr["LibArticle"] != DBNull.Value)
                        article.libArticle = dr["LibArticle"].ToString();
                    if (dr["DateGratuitesDebut"] != DBNull.Value && dr["DateGratuitesFin"] != DBNull.Value)
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

            public static MobileArticleCollection ChargerParFamilleType(string famille, string type, string CClient, string PanierCArticles, int page)
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
                        cmd.Parameters.AddWithValue("@PanierCArticles", PanierCArticles);
                        cmd.Parameters.AddWithValue("@page", page);
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            MobileArticle article = new MobileArticle();
                            article.CArticle = reader["codeArt"] == DBNull.Value ? "" : reader["codeArt"].ToString();
                            article.libArticle = reader["libArt"] == DBNull.Value ? "" : reader["libArt"].ToString();
                            article.CUnite = reader["CUnite"] == DBNull.Value ? "" : reader["CUnite"].ToString();
                            article.StockReel = reader["qteOt"] == DBNull.Value ? 0 : decimal.Parse(reader["qteOt"].ToString());

                            article.ImageArt = reader["ImageArt"] == DBNull.Value ? "" : "data:image/png;base64," + Convert.ToBase64String((byte[])reader["ImageArt"]);
                            article.CFamille = famille;
                            article.CType = type;
                            article.Dividende = 0;
                            article.Diviseur = 1;
                            if (reader["DateDG"] != DBNull.Value && reader["DateGF"] != DBNull.Value && reader["Dividende"] != DBNull.Value && reader["Diviseur"] != DBNull.Value)
                            {
                                DateTime DateDG = Convert.ToDateTime(reader["DateDG"]).Date;
                                DateTime DateGF = Convert.ToDateTime(reader["DateGF"]).Date;
                                if (time >= DateDG && time <= DateGF)
                                {
                                    article.Dividende = reader["Dividende"] == DBNull.Value ? 0 : (int)reader["Dividende"];
                                    article.Diviseur = reader["Diviseur"] == DBNull.Value ? 1 : (int)reader["Diviseur"];
                                }
                            }
                            article.TVA = (decimal)reader["TVA"];
                            article.PrixHT = (decimal)reader["PrixHT"];
                            article.Etat =  reader["Etat"] == DBNull.Value ? null : (string)reader["Etat"];
                            article.ColorEtat = reader["Color"] == DBNull.Value ? null : (string)reader["Color"];
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
