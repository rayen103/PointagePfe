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
    public class ConventionClientPlanificationMotif
    {
        #region Propriétès

        public Int64 IDCCPM { get; set; }

        public DateTime DatePlanification { get; set; }

        public DateTime? DateAncienne { get; set; }

        public string NConvention { get; set; }

        public string CMotif { get; set; }

        public DateTime? DateInsertion { get; set; }

        public DateTime? DateModification { get; set; }

        public int CreePar { get; set; }

        public int ModifiePar { get; set; }

        public string PCInsertion { get; set; }

        public string PCModification { get; set; }

        #endregion Propriétès

        public void Sauvegarder()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();

                try
                {
                    Sauvegarder(transaction);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void Sauvegarder(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ConventionClientPlanificationMotif_Sauvegarder";

                cmd.Parameters.AddWithValue("@IDCCPM", this.IDCCPM);
                cmd.Parameters.AddWithValue("@NConvention", this.NConvention);
                cmd.Parameters.AddWithValue("@DatePlanification", this.DatePlanification);
                cmd.Parameters.AddWithValue("@DateAncienne", this.DateAncienne);
                cmd.Parameters.AddWithValue("@CMotif", this.CMotif);
                cmd.Parameters.AddWithValue("@CreePar", GestionSession.UtilisateurCourant.IdUtilisateur);
                cmd.Parameters.AddWithValue("@PCInsertion", Environment.UserName);

                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ConventionClientPlanificationMotif Charger(Int64 IDCCPM)
        {
            ConventionClientPlanificationMotif motif = new ConventionClientPlanificationMotif();

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "ConventionClientPlanificationMotif_Charger";
                    cmd.Parameters.AddWithValue("@IDCCPM", IDCCPM);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            motif.IDCCPM = (Int64)dr["IDCCPM"];
                            if (dr["NConvention"] != DBNull.Value)
                                motif.NConvention = dr["NConvention"].ToString();
                            if (dr["DatePlanification"] != DBNull.Value)
                                motif.DatePlanification = (DateTime)dr["DatePlanification"];
                            if (dr["DateAncienne"] != DBNull.Value)
                                motif.DateAncienne = (DateTime)dr["DateAncienne"];
                            if (dr["CMotif"] != DBNull.Value)
                                motif.CMotif = dr["CMotif"].ToString();
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return (motif);
            }
        }
    }

    public class ConventionClientPlanificationMotifCollection : List<ConventionClientPlanificationMotif> {

        public static ConventionClientPlanificationMotifCollection Charger()
        {
            ConventionClientPlanificationMotifCollection collection = new ConventionClientPlanificationMotifCollection();
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "ConventionClientPlanificationMotif_Charger";
                    cmd.Parameters.AddWithValue("@IDCCPM", DBNull.Value);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ConventionClientPlanificationMotif motif = new ConventionClientPlanificationMotif();

                            motif.IDCCPM = (Int64)dr["IDCCPM"];
                            if (dr["NConvention"] != DBNull.Value)
                                motif.NConvention = dr["NConvention"].ToString();
                            if (dr["DatePlanification"] != DBNull.Value)
                                motif.DatePlanification = (DateTime)dr["DatePlanification"];
                            if (dr["DateAncienne"] != DBNull.Value)
                                motif.DateAncienne = (DateTime)dr["DateAncienne"];
                            if (dr["CMotif"] != DBNull.Value)
                                motif.CMotif = dr["CMotif"].ToString();

                            collection.Add(motif);
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return (collection);
            }
        }

        public static ConventionClientPlanificationMotifCollection Charger(string NConvention)
        {
            ConventionClientPlanificationMotifCollection collection = new ConventionClientPlanificationMotifCollection();
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "ConventionClientPlanificationMotif_ChargerParConvention";
                    cmd.Parameters.AddWithValue("@NConvention", NConvention);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ConventionClientPlanificationMotif motif = new ConventionClientPlanificationMotif();

                            motif.IDCCPM = (Int64)dr["IDCCPM"];
                            if (dr["NConvention"] != DBNull.Value)
                                motif.NConvention = dr["NConvention"].ToString();
                            if (dr["DatePlanification"] != DBNull.Value)
                                motif.DatePlanification = (DateTime)dr["DatePlanification"];
                            if (dr["DateAncienne"] != DBNull.Value)
                                motif.DateAncienne = (DateTime)dr["DateAncienne"];
                            if (dr["CMotif"] != DBNull.Value)
                                motif.CMotif = dr["CMotif"].ToString();

                            collection.Add(motif);
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return (collection);
            }
        }
    }
}
