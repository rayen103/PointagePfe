using CST.LePoint.Securite;
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
                cmd.Parameters.AddWithValue("@Code_Societe", GestionSession.SocieteCourante.CSociete);
                cmd.Parameters.AddWithValue("@Code_Site", GestionSession.SocieteSite);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    GPSTracking gpsTracking = new GPSTracking();
                    //gpsTracking.Titre = dr["nom"].ToString() + " ( " + dr["nombre"].ToString() + " / " + dr["capacite"].ToString() + " )";
                    gpsTracking.Titre = dr["nom"].ToString() + " ( " + dr["nombre"].ToString() + " )";

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
                cmd.Parameters.AddWithValue("@Code_Societe", GestionSession.SocieteCourante.CSociete);
                cmd.Parameters.AddWithValue("@Code_Site", GestionSession.SocieteSite);
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

    public class GPSBus
    {
        public string IMM { get; set; }
        public string Model { get; set; }
        public string Chauffeur { get; set; }
        public string Nombre { get; set; }
        public string capacite { get; set; } //khoubaib
        public DataTable employes { get; set; }

        public GPSBus()
        {
            employes = new DataTable();
        }

        public static GPSBus Charger(string IMM)
        {
            GPSBus bus = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "GPSBus_Charger";
                    cmd.Parameters.AddWithValue("@IMM", IMM);
                    cmd.Parameters.AddWithValue("@Code_Societe", GestionSession.SocieteCourante.CSociete);
                    cmd.Parameters.AddWithValue("@Code_Site", GestionSession.SocieteSite);
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        bus = new GPSBus();
                        bus.Model = dr["Model"].ToString();
                        bus.Chauffeur = dr["Chauffeur"].ToString();
                        bus.Nombre = dr["nombre"].ToString();
                        bus.employes = GPSEmployeBusCollection.Charger(IMM);
                        bus.capacite = dr["nombre"].ToString() + " / " + dr["capacite"].ToString();// int.Parse(dr["capacite"].ToString()); //khoubaib
                    }
                    dr.Close();
                }
            }
            catch (Exception)
            {
                
                throw;
            }

            return bus;
        }

    }

    public class GPSEmployeBus
    {
        public string NomPrenom { get; set; }
        public string Embarquer { get; set; }
        public string AlleeRetour { get; set; }
        public string Heure { get; set; }
    }

    public class GPSEmployeBusCollection : List<GPSEmployeBus>
    {
        public static DataTable Charger(string IMM)
        {
            GPSEmployeBusCollection collection = new GPSEmployeBusCollection();
            DataTable dtListe = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "GPSEmployeBus_Charger";
                    cmd.Parameters.AddWithValue("@IMM", IMM);
                    cmd.Parameters.AddWithValue("@Code_Societe", GestionSession.SocieteCourante.CSociete);
                    cmd.Parameters.AddWithValue("@Code_Site", GestionSession.SocieteSite);
                    //SqlDataReader dr = cmd.ExecuteReader();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dtListe);
                    //while (dr.Read())
                    //{
                    //    GPSEmployeBus employer = new GPSEmployeBus();
                    //    employer.NomPrenom = dr["NomPrenom"].ToString();
                    //    employer.Embarquer = dr["Embarquer"].ToString();
                    //    employer.AlleeRetour = dr["AlleeRetour"].ToString();

                    //    collection.Add(employer);
                    //    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    //    adapter.Fill(dtListe);
                    //}
                    //dr.Close();
                }
            }
            catch (Exception)
            {
                
                throw;
            }

            return dtListe;
        }
    }
}