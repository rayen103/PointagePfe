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
    public class MobileEtat
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

    public class MobileEtatCollection : List<MobileEtat>
    {
        public static MobileEtatCollection Charger()
        {
            MobileEtatCollection collection = new MobileEtatCollection();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader reader;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Mobile_Etat_Charger";
                    cmd.Connection = connection;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        MobileEtat etat = new MobileEtat();
                        etat.Code = reader["CEtat"] == DBNull.Value ? "" : reader["CEtat"].ToString();
                        etat.Libelle = reader["LibEtat"] == DBNull.Value ? "" : reader["LibEtat"].ToString();
                        collection.Add(etat);
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
