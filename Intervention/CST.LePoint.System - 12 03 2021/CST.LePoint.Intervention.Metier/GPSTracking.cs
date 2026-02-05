using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST.LePoint.Intervention.Metier
{

    public enum PinType
    {
        PC,
        Bus,
        Site
    }

    public class GPSTracking
    {
        public string Titre { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public PinType pinType { get; set; }
    }

    public class GPSPointCollecte
    {
        public static List<GPSTracking> GetPC(string IMM)
        {
            List<GPSTracking> collection = new List<GPSTracking>();
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GPSPointCollecte_Get";
                cmd.Parameters.AddWithValue("@IMM", IMM);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    GPSTracking gpsTracking = new GPSTracking();
                    gpsTracking.Titre = dr["nom"].ToString() + " ( " + dr["nombre"].ToString() + " / " + dr["capacite"].ToString() + " )";

                    if (dr["Long_PC"] != DBNull.Value)
                        gpsTracking.Longitude = decimal.Parse(dr["Long_PC"].ToString());
                    if (dr["Latt_PC"] != DBNull.Value)
                        gpsTracking.Latitude = decimal.Parse(dr["Latt_PC"].ToString());
                    int type = (int)dr["pintype"];
                    if (type == 1)
                        gpsTracking.pinType = PinType.Bus;
                    else if(type == 2)
                        gpsTracking.pinType = PinType.Site;
                    else
                        gpsTracking.pinType = PinType.PC;
                    collection.Add(gpsTracking);
                }
                dr.Close();
                return (collection);
            }
        }

        public List<GPSTracking> GetBus(string IMM)
        {
            List<GPSTracking> collection = new List<GPSTracking>();
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GPSBus_Get";
                cmd.Parameters.AddWithValue("@IMM", IMM);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    GPSTracking gpsTracking = new GPSTracking();
                    gpsTracking.Titre = dr["Lib_PC"].ToString() + " (" + int.Parse(dr["nombre"].ToString()) + ")";

                    if (dr["Long_PC"] != DBNull.Value)
                        gpsTracking.Longitude = decimal.Parse(dr["Long_PC"].ToString());
                    if (dr["Latt_PC"] != DBNull.Value)
                        gpsTracking.Latitude = decimal.Parse(dr["Latt_PC"].ToString());

                    collection.Add(gpsTracking);
                }
                dr.Close();
                return (collection);
            }
        }
    }
}