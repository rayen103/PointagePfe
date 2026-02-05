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

namespace CST.LePoint.Intervention.Metier
{
    public class Location
    {

        #region Proprietes

       

        [XmlAttribute("Longitude")]
        [Bindable(true)]
        public decimal Longitude { get; set; }

        [XmlAttribute("Latitude")]
        [Bindable(true)]
        public decimal Latitude { get; set; }





        #endregion Proprietes

        public Location()
        {

        }



        public void Sauvegarder()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Transaction = transaction;
                    cmd.Connection = transaction.Connection;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "Location_Sauvegarder";
                
                    cmd.Parameters.AddWithValue("@LongitudeRep", Longitude);
                    cmd.Parameters.AddWithValue("@LatitudeRep", Latitude);
                

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    cmd.ExecuteNonQuery();





                    transaction.Commit();
                }

                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }


        const double PIx = Math.PI;
        const double RADIO = 6378.16;

        /// <summary>
        /// Convert degrees to Radians
        /// </summary>
        /// <param name="x">Degrees</param>
        /// <returns>The equivalent in radians</returns>
        public static double Radians(double x)
        {
            return x * PIx / 180;
        }

        /// <summary>
        /// Calculate the distance between two places.
        /// </summary>
        /// <param name="lon1"></param>
        /// <param name="lat1"></param>
        /// <param name="lon2"></param>
        /// <param name="lat2"></param>
        /// <returns></returns>
        public static double DistanceBetweenPlaces(double lon1, double lat1, double lon2, double lat2)
        {
            double R = 6371; // km
            double dLat = Radians(lat2 - lat1);
            double dLon = Radians(lon2 - lon1);
            lat1 = Radians(lat1);
            lat2 = Radians(lat2);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double d = R * c;

            return d;
        }


        //Console.WriteLine(DistanceAlgorithm.DistanceBetweenPlaces(36.578581, -118.291994, 36.23998, -116.83171));


        public void Supprimer()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Transaction = transaction;


                    cmd.Connection = transaction.Connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Location_Supprimer";
                    cmd.Parameters.AddWithValue("@Latitude", Latitude);
                    cmd.Parameters.AddWithValue("@Longitude", Longitude);
                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }





  

    }

    [Serializable]
    public class LocationCollection : List<Location>
    {
        public LocationCollection()
        {
        }


        //public static LocationCollection Charger()
        //{
        //    LocationCollection trameLocCollection = new LocationCollection();

        //    using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
        //    {
        //        cn.Open();
        //        SqlTransaction transaction = cn.BeginTransaction();
        //        try
        //        {
        //            SqlCommand cmd = new SqlCommand();
        //            cmd.Transaction = transaction;
        //            cmd.Connection = transaction.Connection;
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.CommandText = "Location_Charger";
        //            cmd.Parameters.AddWithValue("@CClient", DBNull.Value);

        //            using (SqlDataReader dr = cmd.ExecuteReader())
        //            {
        //                while (dr.Read())
        //                {
        //                    TrameLoc trameLoc = new TrameLoc();
        //                    if (dr["CClient"] != DBNull.Value)
        //                        client.CClient = dr["CClient"].ToString();
        //                    if (dr["CRegion"] != DBNull.Value)
        //                        client.CRegion = dr["CRegion"].ToString();
        //                    if (dr["AbreviationClient"] != DBNull.Value)
        //                        client.Abreviation = dr["AbreviationClient"].ToString();
        //                    if (dr["BActifClient"] != DBNull.Value)
        //                        client.BActif = bool.Parse(dr["BActifClient"].ToString());
        //                    if (dr["CClientFamille"] != DBNull.Value)
        //                        client.CClientFamille = dr["CClientFamille"].ToString();
        //                    if (dr["CGroupe"] != DBNull.Value)
        //                        client.CGroupe = dr["CGroupe"].ToString();
        //                    if (dr["CPays"] != DBNull.Value)
        //                        client.CPays = dr["CPays"].ToString();

        //                    trameLocCollection.Add(trameLoc);
        //                }
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            throw;
        //        }
        //    }

        //    return (trameLocCollection);
        //}
    }
}

