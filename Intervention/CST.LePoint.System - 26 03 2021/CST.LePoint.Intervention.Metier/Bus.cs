
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using CST.LePoint.Referentiel;
namespace CST.LePoint.Intervention.Metier
{
    public class Bus : Item
    {
        public string Num_IMM { get; set; }

        public string Model_Bus { get; set; }
        public string IMEI { get; set; }
        public int Capacite_Bus { get; set; }
        public string Code_Circuit { get; set; }
        public bool APP_Sagem { get; set; }

        public DateTime? DateInsertion { get; set; }
        public DateTime? DateModification { get; set; }
        public int CreePar { get; set; }
        public int ModifiePar { get; set; }
        public string PCInsertion { get; set; }
        public string PCModification { get; set; }


        public Bus()
        { }

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
                    cmd.CommandText = "Bus_Sauvegarder";
                    cmd.Parameters.AddWithValue("@Num_IMM", this.Num_IMM);
                    cmd.Parameters.AddWithValue("@Model_Bus", this.Model_Bus);
                    cmd.Parameters.AddWithValue("@IMEI", this.IMEI);
                    cmd.Parameters.AddWithValue("@Capacite_Bus", this.Capacite_Bus);
                    cmd.Parameters.AddWithValue("@Code_Circuit", this.Code_Circuit);
                    cmd.Parameters.AddWithValue("@APP_Sagem", this.APP_Sagem);
                    cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                    cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                    cmd.Parameters.AddWithValue("@CreePar", this.CreePar);
                    cmd.Parameters.AddWithValue("@ModifiePar", this.ModifiePar);
                    cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);
                    cmd.Parameters.AddWithValue("@PCModification", this.PCModification);

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
                    cmd.CommandText = "Bus_Supprimer";
                    cmd.Parameters.AddWithValue("@Num_IMM", Num_IMM);
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

        public static Bus Charger(string Num_IMM)
        {
            Bus bus = null;
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Bus_Charger";
                cmd.Parameters.AddWithValue("@Num_IMM", Num_IMM);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    bus = new Bus();
                    bus.Num_IMM = dr["Num_IMM"].ToString();
                    if (dr["Model_Bus"] != DBNull.Value)
                        bus.Model_Bus = dr["Model_Bus"].ToString();
                    if (dr["IMEI"] != DBNull.Value)
                        bus.IMEI = dr["IMEI"].ToString();
                    if (dr["Capacite_Bus"] != DBNull.Value)
                        bus.Capacite_Bus = int.Parse(dr["Capacite_Bus"].ToString());
                    if (dr["Code_Circuit"] != DBNull.Value)
                        bus.Code_Circuit = (dr["Code_Circuit"].ToString());
                    if (dr["APP_Sagem"] != DBNull.Value)
                        bus.APP_Sagem = bool.Parse(dr["APP_Sagem"].ToString());
                }
            }
            return (bus);
        }
    }
    public class BusCollection : ItemCollection
    {

        public static BusCollection Charger()
        {
            BusCollection BusCollection = new BusCollection();
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Bus_Charger";
                cmd.Parameters.AddWithValue("@Num_IMM", DBNull.Value);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Bus bus = new Bus();
                    bus.Num_IMM = dr["Num_IMM"].ToString();
                    bus.Code = dr["Num_IMM"].ToString();
                    bus.Model_Bus = dr["Model_Bus"].ToString();
                    bus.Libelle = dr["Model_Bus"].ToString();
                    bus.IMEI = dr["IMEI"].ToString();

                    BusCollection.Add(bus);
                }
                dr.Close();
            }
            return (BusCollection);
        }
        public static DataTable RemplirGrid()
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Bien_Charger_avec_surface";
                cmd.Parameters.AddWithValue("@Num_IMM", DBNull.Value);

                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            return (dt);
        }
    }
}
