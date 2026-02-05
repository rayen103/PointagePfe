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
   public  class MobileHeur
    {
        private string _heure;
        public string heure
        {
            get { return _heure; }
            set { _heure = value; }
        }
        private string _duree;
        public string duree
        {
            get { return _duree; }
            set { _duree = value; }
        }

        public MobileHeur()
        {
        }
        public MobileHeur Charger(string id)
        {
            MobileHeur heur = new MobileHeur();
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    SqlDataReader reader = null;
                    SqlCommand cmd = new SqlCommand();
                    cmd.Transaction = transaction;
                    cmd.Connection = transaction.Connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Mobile_getheure_charger";
                    cmd.Parameters.AddWithValue("@ordre", id);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        heur.heure = reader["jr"].ToString() + ' ' + reader["dr"].ToString();
                        heur.duree = reader["duree"].ToString();
                    }
                    reader.Close();
                    transaction.Commit();
                    return heur;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return null;
                }
            }
        }
    }
}