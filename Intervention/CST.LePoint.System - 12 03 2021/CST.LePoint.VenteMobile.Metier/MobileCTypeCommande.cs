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
    public class MobileCTypeCommande
    {
        #region Proprietes

        public string Code { get; set; }

        public string Libelle { get; set; }

        #endregion Proprietes

    }

    [Serializable]
    public class MobileCTBCommandeCollection : List<MobileCTypeCommande>
    {
        public MobileCTBCommandeCollection()
        {
        }

        public static MobileCTBCommandeCollection Charger()
        {
            MobileCTBCommandeCollection modalites = new MobileCTBCommandeCollection();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    SqlDataReader reader;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "MobileTypeBonCommande_Charger";
                    cmd.Parameters.AddWithValue("@CTBC", "");
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        MobileCTypeCommande mobileTBC = new MobileCTypeCommande();
                        mobileTBC.Code = reader["CTBC"] == DBNull.Value ? "" : reader["CTBC"].ToString();
                        mobileTBC.Libelle = reader["LibTBCommande"] == DBNull.Value ? "" : reader["LibTBCommande"].ToString();

                        modalites.Add(mobileTBC);

                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return (modalites);
        }
    }
}