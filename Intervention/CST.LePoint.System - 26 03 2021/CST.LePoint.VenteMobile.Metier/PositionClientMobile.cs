using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;

namespace CST.LePoint.VenteMobile.Metier
{
    public class PositionClientMobile
    {
        private string _CClient;
        
        public string CClient
        {
            get { return _CClient; }
            set { _CClient = value; }
        }
        
        private string _lat;
        
        public string lat
        {
            get { return _lat; }
            set { _lat = value; }
        }
        
        private string _lng;
        
        public string lng
        {
            get { return _lng; }
            set { _lng = value; }
        }
        
        public PositionClientMobile()
        {
        }
        
        public string verifierPositionClient(PositionClientMobile clientp1)
        {
            PositionClientMobile clientp = clientp1;
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                string msg = "";
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    //SqlDataReader reader = null;
                    //SqlCommand cmd1 = new SqlCommand();
                    //cmd1.Transaction = transaction;
                    //cmd1.Connection = transaction.Connection;
                    //cmd1.CommandType = CommandType.StoredProcedure;
                    //if (clientp.update == false)
                    //{
                    //    cmd1.CommandText = "Mobile_Client_avoir_position";
                    //    cmd1.Parameters.AddWithValue("@CClient", clientp.CClient);
                    //    cmd1.Parameters.AddWithValue("@Code", clientp.CEtablissement);
                    //    reader = cmd1.ExecuteReader();
                    //    if (reader.HasRows)
                    //    {
                    //        return "exist";
                    //    }
                    //    else
                    //        return "null";
                    //}
                    //else if (clientp.update == true)
                    //{
                    msg = UpdateClient(transaction, clientp);
                    //}
                    transaction.Commit();
                    return msg;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return "error";

                }
            }
        }

        public string UpdateClient(SqlTransaction transaction, PositionClientMobile clientp)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Mobile_Client_GPS";
                cmd.Parameters.AddWithValue("@Longitude", clientp.lng);
                cmd.Parameters.AddWithValue("@latitude", clientp.lat);
                cmd.Parameters.AddWithValue("@CClient", clientp.CClient);
                cmd.ExecuteNonQuery();
                return "success";
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}