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
    public class MobilePolyflexArticle
    {
        #region Proriétès

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

    public class MobilePolyflexArticleCollection : List<MobilePolyflexArticle>
    {
        public static MobilePolyflexArticleCollection Charger()
        {
            MobilePolyflexArticleCollection collection = new MobilePolyflexArticleCollection();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString2"].ConnectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader reader;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Mobile_PolyflexArticle_Charger";
                    cmd.Connection = connection;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        MobilePolyflexArticle article = new MobilePolyflexArticle();
                        article.CArticle = reader["CArticle"] == DBNull.Value ? "" : reader["CArticle"].ToString();
                        article.LibArticle = reader["LibArticle"] == DBNull.Value ? "" : reader["LibArticle"].ToString();
                        article.Quantite = 0;
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
