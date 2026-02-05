using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace CST.LePoint.VenteMobile.Metier
{
    public class Rattachment
    {
        #region Proprietes

        public string CClient { get; set; }
        public string RaisonSociale { get; set; }
        public string DateRattachement { get; set; }
        public string Remarque { get; set; }
        public string justifvente { get; set; }
        public string justifrecouvrement { get; set; }
        public string strategie { get; set; }

         #endregion Proprietes

        public Rattachment()
        {

        }
        
        public static Rattachment Charger(string nrattachement)
        {
               using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                Rattachment r = new Rattachment();

                connection.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader reader;
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "GP_Rattachement_Crm_Rattachement";
                    cmd.Parameters.AddWithValue("@NRattachement", nrattachement);
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            r.CClient = reader["CClient"] == DBNull.Value ? "" : reader["CClient"].ToString();
                            r.strategie = reader["StrategieConcurence"] == DBNull.Value ? "" : reader["StrategieConcurence"].ToString();
                            r.DateRattachement = reader["DateRattachement"] == DBNull.Value ? "" : DateTime.Parse(reader["DateRattachement"].ToString()).ToString("dd/MM/yyyy");
                            r.justifvente = reader["justif"] == DBNull.Value ? "" : reader["justif"].ToString();
                            r.justifrecouvrement = reader["justifrecouvrement"] == DBNull.Value ? "" : reader["justifrecouvrement"].ToString();
                            r.RaisonSociale = reader["RaisonSociale"] == DBNull.Value ? "" : reader["RaisonSociale"].ToString();
                            r.Remarque = reader["Remarque"] == DBNull.Value ? "" : reader["Remarque"].ToString();
                        }                    

                    }
                    return r;
                }
                catch (Exception)
                {
                    throw;
                }

          
        }

    }

    }
}
