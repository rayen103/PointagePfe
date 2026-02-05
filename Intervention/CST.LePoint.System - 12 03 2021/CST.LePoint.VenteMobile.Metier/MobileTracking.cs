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
    public class MobileTracking
    {
      
            private string _total;

            public string total
            {
                get { return _total; }
                set { _total = value; }
            }
            private string _lat;

            public string lat
            {
                get { return _lat; }
                set { _lat = value; }
            }
            private string _long;

            public string lng
            {
                get { return _long; }
                set { _long = value; }
            }
            private string _Cequipe;

            public string Cequipe
            {
                get { return _Cequipe; }
                set { _Cequipe = value; }
            }
            private string _type_log;

            public string type_log
            {
                get { return _type_log; }
                set { _type_log = value; }
            }

            public void sauvegarder(MobileTracking tracking)
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlTransaction transaction = cn.BeginTransaction();
                    try
                    {
                        string count;
                        int number = 0;
                        SqlDataReader reader = null;
                        SqlCommand cmd = new SqlCommand();
                        cmd.Transaction = transaction;
                        cmd.Connection = transaction.Connection;
                        cmd.CommandText = "Mobile_tracking_count";
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            count = reader["count"].ToString() == null ? "" : reader["count"].ToString();
                            number = Int32.Parse(count);

                        }
                        if (string.IsNullOrEmpty(tracking.total) && number > int.Parse(tracking.total))
                        {
                            tracking_supprimer(transaction);
                            tracking_inserer(transaction, tracking);
                        }
                        else
                            transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();

                    }

                }
            }
             public void tracking_supprimer(SqlTransaction transaction)  {
                     try
                  {
                      SqlCommand cmd = new SqlCommand();
                      cmd.Transaction = transaction;
                      cmd.Connection = transaction.Connection;
                      cmd.CommandType = CommandType.StoredProcedure;
                      cmd.CommandText = "Mobile_tracking_supprimer";
                      cmd.ExecuteNonQuery();

                  }
                  catch (Exception)
                  {
                      throw;     
                  }

              }
        
 
        public void tracking_inserer(SqlTransaction transaction,MobileTracking tracking)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Mobile_tracking_inserer";
                cmd.Parameters.AddWithValue("@DateTrame",DateTime.Now);
                cmd.Parameters.AddWithValue("@HeureTrame", DateTime.Now);
                   cmd.Parameters.AddWithValue("@Cequipe", tracking.Cequipe);
                   cmd.Parameters.AddWithValue("@Longitudeequipe", tracking.lng);
                  cmd.Parameters.AddWithValue("@Latitudeequipe", tracking.lat);
          

            }
            catch (Exception)
            {
                throw;
            }
        }
            }
    }


