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

namespace CST.LePoint.VenteMobile.Metier
{
    public class MobileArticlee
    {
        #region Proprietes

        [XmlAttribute("codeArt")]
        [Bindable(true)]
        public string codeArt { get; set; }

        [XmlAttribute("libArt")]
        [Bindable(true)]
        public string libArt { get; set; }

        [XmlAttribute("qteOt")]
        [Bindable(true)]
        public string qteOt { get; set; }

        [XmlAttribute("qteRes")]
        [Bindable(true)]
        public string qteRes { get; set; }

        [XmlAttribute("qtePrep")]
        [Bindable(true)]
        public string qtePrep { get; set; }

        [XmlAttribute("b")]
        [Bindable(true)]
        public int b { get; set; }

        [XmlAttribute("CUnite")]
        [Bindable(true)]
        public string CUnite { get; set; }

        [XmlAttribute("ImageArt")]
        [Bindable(true)]
        public string ImageArt { get; set; }

        #endregion Proprietes

        public MobileArticlee()
        {

        }
    }

    [Serializable]
    public class MobileArticleeCollection : List<MobileArticlee>
    {
        public MobileArticleeCollection()
        {
        }
        
        public static MobileArticleeCollection articlesCharger(string code)
        {
            MobileArticleeCollection articlesMobile = new MobileArticleeCollection();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader reader;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Mobile_articles_charger";
                    cmd.Parameters.AddWithValue("@NOrdredeTravail", code);
                    cmd.Connection = connection;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        MobileArticlee article = new MobileArticlee();
                        article.codeArt = reader["CArticle"] == DBNull.Value ? "" : reader["CArticle"].ToString();
                        article.libArt = reader["LibArticle"] == DBNull.Value ? "" : reader["LibArticle"].ToString();
                        article.qteOt = reader["QuantiteOT"] == DBNull.Value ? "" : reader["QuantiteOT"].ToString();
                        article.qteRes = reader["QuantiteOTRes"] == DBNull.Value ? "" : reader["QuantiteOTRes"].ToString();
                        article.qtePrep = reader["QuantitePreparee"] == DBNull.Value ? "" : reader["QuantitePreparee"].ToString();
                        article.b = Convert.ToInt32(reader["BQuantite"]);
                        article.CUnite = reader["CUnite"] == DBNull.Value ? "" : reader["CUnite"].ToString();


                        if (reader["Image_Article"] != DBNull.Value) article.ImageArt = "";
                        else article.ImageArt = reader["Image_Article"].ToString();
                        articlesMobile.Add(article);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return (articlesMobile);
        }

        public static MobileArticleeCollection CrmarticlesCharger(string code)
        {
            MobileArticleeCollection articlesMobile = new MobileArticleeCollection();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader reader;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "MobileCRM_articles_charger";
                    cmd.Parameters.AddWithValue("@typeArticle", code);
                    cmd.Connection = connection;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        MobileArticlee article = new MobileArticlee();
                        article.codeArt = reader["COptions"] == DBNull.Value ? "" : reader["COptions"].ToString();
                        article.libArt = reader["LibOptions"] == DBNull.Value ? "" : reader["LibOptions"].ToString();
                        articlesMobile.Add(article);                       

                    }
                    return (articlesMobile);
                }
                catch (Exception)
                {
                    throw;
                }
             

            }

        }
    }
}