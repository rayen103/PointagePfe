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
    public class MobileOption
    {
        #region Propriétés

        public string Code { get; set; }
        public string Libelle { get; set; }

        #endregion
    }

    public class MobileOptionCollection: List<MobileOption>
    {
        public static MobileOptionCollection Charger(string type)
        {
            MobileOptionCollection collection = new MobileOptionCollection();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader reader;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Mobile_Option_Charger";
                    cmd.Parameters.AddWithValue("@Type", type);
                    cmd.Connection = connection;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        MobileOption option = new MobileOption();
                        option.Code = reader["COptions"] == DBNull.Value ? "" : reader["COptions"].ToString();
                        option.Libelle = reader["LibOptions"] == DBNull.Value ? "" : reader["LibOptions"].ToString();
                        collection.Add(option);
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
