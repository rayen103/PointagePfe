

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using CST.LePoint.Referentiel;
using CST.LePoint.Securite;
namespace CST.LePoint.Intervention.Metier
{
    public class Modem : Item
    {
        public string Model_Modem { get; set; }
        public string IMEI { get; set; }
        public string Num_SIM { get; set; }
        public string Code_Societe { get; set; }
        public string Code_Site { get; set; }

        public DateTime? DateInsertion { get; set; }
        public DateTime? DateModification { get; set; }
        public int CreePar { get; set; }
        public int ModifiePar { get; set; }
        public string PCInsertion { get; set; }
        public string PCModification { get; set; }



        public Modem()
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
                    cmd.CommandText = "Ref_Modem_Sauvegarder";
                    cmd.Parameters.AddWithValue("@IMEI", this.IMEI);
                    cmd.Parameters.AddWithValue("@Model_Modem", this.Model_Modem);
                    cmd.Parameters.AddWithValue("@Num_SIM", this.Num_SIM);
                    cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                    cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                    cmd.Parameters.AddWithValue("@CreePar", this.CreePar);
                    cmd.Parameters.AddWithValue("@ModifiePar", this.ModifiePar);
                    cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);
                    cmd.Parameters.AddWithValue("@PCModification", this.PCModification);
                    cmd.Parameters.AddWithValue("@Code_Societe", GestionSession.SocieteCourante.CSociete);
                    cmd.Parameters.AddWithValue("@Code_Site", GestionSession.SocieteSite);

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
                    cmd.CommandText = "Ref_Modem_Supprimer";
                    cmd.Parameters.AddWithValue("@IMEI", IMEI);
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

        public static Modem Charger(string IMEI)
        {
            Modem modem = null;
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Ref_Modem_Charger";
                cmd.Parameters.AddWithValue("@IMEI", IMEI);
                cmd.Parameters.AddWithValue("@Code_Societe", GestionSession.SocieteCourante.CSociete);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    modem = new Modem();
                    modem.IMEI = dr["IMEI"].ToString();
                    modem.Num_SIM = dr["Num_SIM"].ToString();
                    if (dr["Model_Modem"] != DBNull.Value)
                        modem.Model_Modem = dr["Model_Modem"].ToString();


                }
            }
            return (modem);
        }
    }
    public class ModemCollection : ItemCollection
    {

        public static ModemCollection Charger()
        {
            ModemCollection modemCollection = new ModemCollection();
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Ref_Modem_Charger";
                cmd.Parameters.AddWithValue("@IMEI", DBNull.Value);
                cmd.Parameters.AddWithValue("@Code_Societe", GestionSession.SocieteCourante.CSociete);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Modem modem = new Modem();
                    modem.IMEI = dr["IMEI"].ToString();
                    modem.Num_SIM = dr["Num_SIM"].ToString();
                    if (dr["Model_Modem"] != DBNull.Value)
                        modem.Model_Modem = dr["Model_Modem"].ToString();


                    modemCollection.Add(modem);
                }
                dr.Close();
            }
            return (modemCollection);
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
                cmd.CommandText = "Ref_Modem_Charger";
                cmd.Parameters.AddWithValue("@IMEI", DBNull.Value);
                cmd.Parameters.AddWithValue("@Code_Societe", GestionSession.SocieteCourante.CSociete);

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
