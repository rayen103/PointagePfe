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
    public class MobileAuthUser
    {
        private string _utilisateur;
        public string commercial
        {
            get { return _utilisateur; }
            set { _utilisateur = value; }
        }
        private string _password;
        public string password
        {
            get { return _password; }
            set { _password = value; }
        }

        public MobileAuthUser()
        {
        }
        public bool CommercialConnexion(string cmr, string pass)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Mobile_user_connexion";
                cmd.Parameters.AddWithValue("@CUtilisateur", cmr);
                cmd.Parameters.AddWithValue("@MotDePasse", pass);
                cmd.Connection = connection;
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(reader.GetString(1));
                        if (!reader.GetString(1).Equals(""))
                        {
                            return true;
                        }
                    }
                }
                reader.Close();
                connection.Close();
            }
            return false;
        }
    }
}