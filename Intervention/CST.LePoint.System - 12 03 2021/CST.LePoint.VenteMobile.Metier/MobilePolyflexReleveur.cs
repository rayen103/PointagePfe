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
    public class MobilePolyflexReleveur
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

    public class MobilePolyflexReleveurCollection : List<MobilePolyflexReleveur>
    {
        public static MobilePolyflexReleveurCollection Charger()
        {
            MobilePolyflexReleveurCollection collection = new MobilePolyflexReleveurCollection();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString2"].ConnectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader reader;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Mobile_PolyflexReleveur_Charger";
                    cmd.Connection = connection;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        MobilePolyflexReleveur releveur = new MobilePolyflexReleveur();
                        releveur.Code = reader["Code"] == DBNull.Value ? "" : reader["Code"].ToString();
                        releveur.Libelle = reader["Libelle"] == DBNull.Value ? "" : reader["Libelle"].ToString();
                        collection.Add(releveur);
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
