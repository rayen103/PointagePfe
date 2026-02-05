using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace CST.Stock.Metier.MobileArticleFamille
{
    [Serializable]
    public class MobileArticleFamille
    {
        [XmlAttribute("CArticleFamille")]
        [Bindable(true)]
        public string CArticleFamille { get; set; }

        [XmlAttribute("LibArticleFamille")]
        [Bindable(true)]
        public string LibArticleFamille { get; set; }


        public static List<MobileArticleFamille> Charger()
        {
            List<MobileArticleFamille> list = new List<MobileArticleFamille>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;
                 cmd.Connection = connection;
                 cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Mobile_Famille_Charger";            
                cmd.Connection = connection;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    MobileArticleFamille famille = new MobileArticleFamille();
                    famille.CArticleFamille = reader["CArticleFamille"] == DBNull.Value ? "" : reader["CArticleFamille"].ToString();
                    famille.LibArticleFamille = reader["LibArticleFamille"]== DBNull.Value ? "" : reader["LibArticleFamille"].ToString();
                    list.Add(famille);
                }

                reader.Close();
                connection.Close();
                return list;
            }
        }


    }
}
