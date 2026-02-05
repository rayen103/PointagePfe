using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CST.LePoint.VenteMobile.Metier
{
    public class MobileClient
    {
        #region Proprietes

        [XmlAttribute("Code")]
        [Bindable(true)]
        public string Code { get; set; }

        [XmlAttribute("RaisonSociale")]
        [Bindable(true)]
        public string RaisonSociale { get; set; }


        [XmlAttribute("Long")]
        [Bindable(true)]
        public string Long { get; set; }


        [XmlAttribute("Lat")]
        [Bindable(true)]
        public string Lat { get; set; }

        [XmlAttribute("CGouvernorat")]
        [Bindable(true)]
        public string CGouvernorat { get; set; }

        [XmlAttribute("LibG")]
        [Bindable(true)]
        public string LibG { get; set; }

        [XmlAttribute("CCircuit")]
        [Bindable(true)]
        public string CCircuit { get; set; }

        [XmlAttribute("LibC")]
        [Bindable(true)]
        public string LibC { get; set; }

        #endregion Proprietes
        
        public MobileClient()
        {

        }
    }

    [Serializable]
    public class ClientMobileCollection : List<MobileClient>
    {
        public ClientMobileCollection()
        {
        }

        public static ClientMobileCollection Charger(string motCle, int page)
        {
            ClientMobileCollection clientMobile = new ClientMobileCollection();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    SqlDataReader reader;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Mobile_clients_Charger";
                    cmd.Parameters.AddWithValue("@motCle", "%" + motCle + "%");
                    cmd.Parameters.AddWithValue("@page", page);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        MobileClient client = new MobileClient();
                        client.Code = reader["Code"] == DBNull.Value ? "" : reader["Code"].ToString();
                        client.RaisonSociale = reader["RaisonSociale"] == DBNull.Value ? "" : reader["RaisonSociale"].ToString();
                        client.Long = reader["Longitude"] == DBNull.Value ? "" : reader["Longitude"].ToString();
                        client.Lat = reader["Latitude"] == DBNull.Value ? "" : reader["Latitude"].ToString();
                        client.CCircuit = reader["CCircuit"] == DBNull.Value ? "" : reader["CCircuit"].ToString();
                        client.LibC = reader["LibC"] == DBNull.Value ? "" : reader["LibC"].ToString();
                        client.CGouvernorat = reader["CGouvernorat"] == DBNull.Value ? "" : reader["CGouvernorat"].ToString();
                        client.LibG = reader["LibGouvernorat"] == DBNull.Value ? "" : reader["LibGouvernorat"].ToString();
                        clientMobile.Add(client);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return (clientMobile);
        }

        public static ClientMobileCollection ChargerRegionClients(string region)
        {
            ClientMobileCollection clientMobile = new ClientMobileCollection();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    SqlDataReader reader;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Mobile_Client_Par_Region";
                    cmd.Parameters.AddWithValue("@Region", region);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        MobileClient client = new MobileClient();
                        client.Code = reader["Code"] == DBNull.Value ? "" : reader["Code"].ToString();
                        client.RaisonSociale = reader["Libelle"] == DBNull.Value ? "" : reader["Libelle"].ToString();
                        clientMobile.Add(client);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return (clientMobile);
        }
    
    }
}