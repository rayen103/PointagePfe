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
    public class MobilebonAchat
    {
        #region Proprietes

        public string Code { get; set; }

        public string Libelle { get; set; }
    
       #endregion Proprietes

        public MobilebonAchat()
        {

        }
    }

    [Serializable]
    public class bonAchatMobileCollection : List<MobilebonAchat>
    {
        public bonAchatMobileCollection()
        {
        }

        public static bonAchatMobileCollection Charger()
        {
            bonAchatMobileCollection bachatMobile = new bonAchatMobileCollection();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader reader;
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "TypeBonAchat_Charger";
                    cmd.Parameters.AddWithValue("@CTBAchat", DBNull.Value);              
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        MobilebonAchat bachat = new MobilebonAchat();
                        bachat.Code = reader["CTBAchat"] == DBNull.Value ? "" : reader["CTBAchat"].ToString();
                        bachat.Libelle = reader["LibTBAchat"] == DBNull.Value ? "" : reader["LibTBAchat"].ToString();
                        bachatMobile.Add(bachat);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return (bachatMobile);
        }
    }
}