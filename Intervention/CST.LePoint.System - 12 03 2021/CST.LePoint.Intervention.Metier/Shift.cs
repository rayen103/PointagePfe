

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
    public class Shift : Item
    {
        public string Lib_Shift { get; set; }
        public string Code_Shift { get; set; }
        public string Jour_Semaine { get; set; }
        public TimeSpan Heure_Debut { get; set; }
        public TimeSpan Heure_Fin { get; set; }
        public string Code_Societe { get; set; }
        public string Code_Site { get; set; }

        public DateTime? DateInsertion { get; set; }
        public DateTime? DateModification { get; set; }
        public int CreePar { get; set; }
        public int ModifiePar { get; set; }
        public string PCInsertion { get; set; }
        public string PCModification { get; set; }


        public Shift()
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
                    cmd.CommandText = "Shift_Sauvegarder";
                    cmd.Parameters.AddWithValue("@Code_Shift", this.Code_Shift);
                    cmd.Parameters.AddWithValue("@Lib_Shift", this.Lib_Shift);
                    cmd.Parameters.AddWithValue("@Jour_Semaine", this.Jour_Semaine);
                    cmd.Parameters.AddWithValue("@Heure_Debut", this.Heure_Debut);
                    cmd.Parameters.AddWithValue("@Heure_Fin", this.Heure_Fin);
                    cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                    cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                    cmd.Parameters.AddWithValue("@CreePar", this.CreePar);
                    cmd.Parameters.AddWithValue("@ModifiePar", this.ModifiePar);
                    cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);
                    cmd.Parameters.AddWithValue("@PCModification", this.PCModification);
                    cmd.Parameters.AddWithValue("@Code_Societe", GestionSession.SocieteCourante.CSociete);
                    cmd.Parameters.AddWithValue("@Code_Site", GestionSession.SocieteSite); ;

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
                    cmd.CommandText = "Shift_Supprimer";
                    cmd.Parameters.AddWithValue("@Code_Shift", Code_Shift);
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

        public static Shift Charger(string Code_Shift)
        {
            Shift shift = null;
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Shift_Charger";
                cmd.Parameters.AddWithValue("@Code_Shift", Code_Shift);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    shift = new Shift();
                    shift.Code_Shift = dr["Code_Shift"].ToString();
                    shift.Jour_Semaine = dr["Jour_Semaine"].ToString();
                    if (dr["Lib_Shift"] != DBNull.Value)
                        shift.Lib_Shift = dr["Lib_Shift"].ToString();
                    if (dr["Heure_Debut"] != DBNull.Value)
                        shift.Heure_Debut = (TimeSpan)dr["Heure_Debut"];
                    if (dr["Heure_Fin"] != DBNull.Value)
                        shift.Heure_Fin = (TimeSpan)dr["Heure_Fin"];                    
           
                }
            }
            return (shift);
        }
    }
    public class ShiftCollection : ItemCollection
    {

        public static ShiftCollection Charger()
        {
            ShiftCollection ShiftCollection = new ShiftCollection();
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Shift_Charger";
                cmd.Parameters.AddWithValue("@Code_Shift", DBNull.Value);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Shift shift = new Shift();
                    shift.Code_Shift = dr["Code_Shift"].ToString();
                    shift.Code = dr["Code_Shift"].ToString();
                    shift.Lib_Shift = dr["Lib_Shift"].ToString();
                    shift.Libelle = dr["Lib_Shift"].ToString();
                    shift.Jour_Semaine = dr["Jour_Semaine"].ToString();
                    if (dr["Heure_Debut"] != DBNull.Value)
                        shift.Heure_Debut = ((DateTime)dr["Heure_Debut"]).TimeOfDay;
                    if (dr["Heure_Fin"] != DBNull.Value)
                        shift.Heure_Fin = ((DateTime)dr["Heure_Fin"]).TimeOfDay;   

                    ShiftCollection.Add(shift);
                }
                dr.Close();
            }
            return (ShiftCollection);
        }


public static ShiftCollection Charger_Group()
        {
            ShiftCollection ShiftCollection = new ShiftCollection();
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Shift_Charger_GroupBy";
                cmd.Parameters.AddWithValue("@Code_Shift", DBNull.Value);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Shift shift = new Shift();
                    shift.Code_Shift = dr["Code_Shift"].ToString();
                    shift.Code = dr["Code_Shift"].ToString();
                    shift.Lib_Shift = dr["Lib_Shift"].ToString();
                    shift.Libelle = dr["Lib_Shift"].ToString();
                    

                    ShiftCollection.Add(shift);
                }
                dr.Close();
            }
            return (ShiftCollection);
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
                cmd.CommandText = "Shift_Charger";
                cmd.Parameters.AddWithValue("@Code_Shift", DBNull.Value);

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
