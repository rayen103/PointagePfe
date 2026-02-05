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
    public class MobilePolyflexEntrepot
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

    public class MobilePolyflexEntrepotCollection : List<MobilePolyflexEntrepot>
    {
        public static MobilePolyflexEntrepotCollection Charger()
        {
            MobilePolyflexEntrepotCollection collection = new MobilePolyflexEntrepotCollection();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString2"].ConnectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader reader;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Mobile_PolyflexEntrepot_Charger";
                    cmd.Connection = connection;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        MobilePolyflexEntrepot entrepot = new MobilePolyflexEntrepot();
                        entrepot.Code = reader["Code"] == DBNull.Value ? "" : reader["Code"].ToString();
                        entrepot.Libelle = reader["Libelle"] == DBNull.Value ? "" : reader["Libelle"].ToString();
                        collection.Add(entrepot);
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
