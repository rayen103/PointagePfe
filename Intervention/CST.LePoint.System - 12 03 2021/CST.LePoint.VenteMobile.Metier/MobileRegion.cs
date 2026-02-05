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
    public class MobileRegion
    {

        #region Proprietes

        public string Code { get; set; }
        public string Libelle { get; set; }
        public string CGouvernorat { get; set; }
    
       #endregion Proprietes

        public MobileRegion()
        {

        }

    }

    [Serializable]
    public class RegionMobileCollection : List<MobileRegion>
    {
        public RegionMobileCollection()
        {
        }

        public static RegionMobileCollection Charger()
        {
            RegionMobileCollection regionMobile = new RegionMobileCollection();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader reader;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = connection;
                    cmd.CommandText = "Mobile_regions_Charger";
                    cmd.Parameters.AddWithValue("@CEquipe", DBNull.Value);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                      MobileRegion region = new MobileRegion();
                      region.Code = reader["CRegion"] == DBNull.Value ? "" : reader["CRegion"].ToString();
                      region.Libelle = reader["LibRegion"] == DBNull.Value ? "" : reader["LibRegion"].ToString();
                      region.CGouvernorat = reader["CGouvernorat"] == DBNull.Value ? "" : reader["CGouvernorat"].ToString();
                      regionMobile.Add(region);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return (regionMobile);
        }

        public static RegionMobileCollection Charger(string CEquipe)
        {
            RegionMobileCollection regionMobile = new RegionMobileCollection();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader reader;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = connection;
                    cmd.CommandText = "Mobile_regions_Charger";
                    cmd.Parameters.AddWithValue("@CEquipe", string.IsNullOrEmpty(CEquipe) ? null : CEquipe);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        MobileRegion region = new MobileRegion();
                        region.Code = reader["CRegion"] == DBNull.Value ? "" : reader["CRegion"].ToString();
                        region.Libelle = reader["LibRegion"] == DBNull.Value ? "" : reader["LibRegion"].ToString();
                        region.CGouvernorat = reader["CGouvernorat"] == DBNull.Value ? "" : reader["CGouvernorat"].ToString();
                        regionMobile.Add(region);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return (regionMobile);
        }

    }
}