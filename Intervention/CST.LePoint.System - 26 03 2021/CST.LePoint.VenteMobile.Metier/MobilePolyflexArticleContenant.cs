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
    public class MobilePolyflexArticleContenant
    {
        #region Proriétès

        [XmlAttribute("CContenant")]
        [Bindable(true)]
        public string CContenant { get; set; }

        [XmlAttribute("CArticle")]
        [Bindable(true)]
        public string CArticle { get; set; }

        [XmlAttribute("LibArticle")]
        [Bindable(true)]
        public string LibArticle { get; set; }

        [XmlAttribute("Quantite")]
        [Bindable(true)]
        public decimal Quantite { get; set; }

        [XmlAttribute("Date")]
        [Bindable(true)]
        public string Date { get; set; }

        #endregion
    }

    public class MobilePolyflexArticleContenantCollection : List<MobilePolyflexArticleContenant>
    {
        public static MobilePolyflexArticleContenantCollection Charger()
        {
            MobilePolyflexArticleContenantCollection collection = new MobilePolyflexArticleContenantCollection();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString2"].ConnectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader reader;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Mobile_PolyflexArticleContenant_Charger";
                    cmd.Connection = connection;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        MobilePolyflexArticleContenant article = new MobilePolyflexArticleContenant();
                        article.CContenant = reader["CContenant"] == DBNull.Value ? "" : reader["CContenant"].ToString();
                        article.CArticle = reader["CArticle"] == DBNull.Value ? "" : reader["CArticle"].ToString();
                        article.LibArticle = reader["LibArticle"] == DBNull.Value ? "" : reader["LibArticle"].ToString();
                        article.Quantite = reader["Quantite"] == DBNull.Value ? 0 : (decimal)reader["Quantite"];
                        collection.Add(article);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return (collection);
        }
    }
    
}
