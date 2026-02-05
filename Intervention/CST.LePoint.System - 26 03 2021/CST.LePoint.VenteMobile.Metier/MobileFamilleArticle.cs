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
    public class MobileFamilleArticle
    {
        #region Proriétès

        [XmlAttribute("Code")]
        [Bindable(true)]
        public string Code { get; set; }

        [XmlAttribute("Libelle")]
        [Bindable(true)]
        public string Libelle { get; set; }

        #endregion
    }

    public class MobileFamilleArticleCollection : List<MobileFamilleArticle>
    {
        public static MobileFamilleArticleCollection Charger()
        {
            MobileFamilleArticleCollection collection = new MobileFamilleArticleCollection();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader reader;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Mobile_FamilleArticle_Charger";
                    cmd.Connection = connection;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        MobileFamilleArticle famille = new MobileFamilleArticle();
                        famille.Code = reader["Code"] == DBNull.Value ? "" : reader["Code"].ToString();
                        famille.Libelle = reader["Libelle"] == DBNull.Value ? "" : reader["Libelle"].ToString();
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
