using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST.LePoint.VenteMobile.Metier
{
    public class MobileClient_Position
    {
        private string _cclient;
        
        public string cclient
        {
            get { return _cclient; }
            set { _cclient = value; }
        }
        
        private string _raisonsociale;
        
        public string raisonsociale
        {
            get { return _raisonsociale; }
            set { _raisonsociale = value; }
        }

        private string _numtel;

        public string numtel
        {
            get { return _numtel; }
            set { _numtel = value; }
        }
        
        private string _adresse;

        public string adresse
        {
            get { return _adresse; }
            set { _adresse = value; }
        }

        private string _ville;

        public string ville
        {
            get { return _ville; }
            set { _ville = value; }
        }
        
        private string _pays;
        
        public string pays
        {
            get { return _pays; }
            set { _pays = value; }
        }

        public MobileClient_Position()
        {

        }

        public MobileClient_Position PositionCharger(String ordre)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Mobile_Client_Position";
                cmd.Parameters.AddWithValue("@ordre", ordre);
                cmd.Connection = connection;
                reader = cmd.ExecuteReader();
                MobileClient_Position Client = new MobileClient_Position();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Client.cclient = reader["cclient"].ToString();
                        Client.raisonsociale = reader["raisonsociale"].ToString();
                        Client.numtel = reader["numtel"].ToString();
                        Client.adresse = reader["adresse"].ToString();
                        Client.ville = reader["ville"].ToString();
                        Client.pays = reader["pays"].ToString();

                    }
                }
                reader.Close();
                connection.Close();
                return Client;
            }

        }

    }
}
