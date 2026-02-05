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
   public class MobileOrdreDetail
    {
       private string _Observations;
       public string Observations
         {
             get { return _Observations; }
             set { _Observations = value; }
        }
       private string _Remarque;
       public string Remarque
       {
           get { return _Remarque; }
           set { _Remarque = value; }
       }
  
  

        public MobileOrdreDetail()
        {

        }

        public static MobileOrdreDetail OrdreDetailCharger(String cclient)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Mobile_OT_Details";
                cmd.Parameters.AddWithValue("@CClient", cclient);             
                cmd.Connection = connection;
                reader = cmd.ExecuteReader();
                MobileOrdreDetail Orddetail = new MobileOrdreDetail();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Orddetail.Observations = reader["Observations"].ToString();
                        Orddetail.Remarque = reader["Remarque"].ToString();
                  

                    }
                }
                reader.Close();
                connection.Close();
                return Orddetail;
            }

        }
    
    }
}
