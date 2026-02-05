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
    public class MobileTypeArticle
    {
        #region Proriétès

        public string Code { get; set; }

        public string Libelle { get; set; }
        public string CFamille { get; set; }

        #endregion
    }

    public class MobileTypeArticleCollection : List<MobileTypeArticle>
    {
        public static MobileTypeArticleCollection Charger()
        {
            MobileTypeArticleCollection collection = new MobileTypeArticleCollection();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader reader;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Mobile_TypeArticle_Charger";
                    cmd.Connection = connection;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        MobileTypeArticle famille = new MobileTypeArticle();
                        famille.Code = reader["Code"] == DBNull.Value ? "" : reader["Code"].ToString();
                        famille.Libelle = reader["Libelle"] == DBNull.Value ? "" : reader["Libelle"].ToString();
                        famille.CFamille = reader["CFamille"] == DBNull.Value ? "" : reader["CFamille"].ToString();
                        collection.Add(famille);
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
