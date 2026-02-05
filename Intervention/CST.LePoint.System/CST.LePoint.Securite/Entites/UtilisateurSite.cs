using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST.LePoint.Securite.Entites
{
    public class UtilisateurSite
    {
        #region Propriétés

        public string CUtilisateur { get; set; }
        public string CSociete { get; set; }
        public string CSite { get; set; }
        public DateTime DateInsertion { get; set; }
        public DateTime DateModification { get; set; }
        public int CreePar { get; set; }
        public int ModifiePar { get; set; }
        public string PCInsertion { get; set; }
        public string PCModification { get; set; }

        #endregion Propriétés

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
                catch
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
                cmd.CommandText = "UtilisateurSite_Sauvegarder";
                cmd.Parameters.AddWithValue("@CUtilisateur", CUtilisateur);
                cmd.Parameters.AddWithValue("@CSite", CSite);
                cmd.Parameters.AddWithValue("@CSociete", CSociete);
                cmd.Parameters.AddWithValue("@CreePar", CreePar);
                cmd.Parameters.AddWithValue("@PCInsertion", PCInsertion);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);

                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {
                throw;
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
                    Supprimer(transaction);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void Supprimer(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UtilisateurSite_Supprimer";
                cmd.Parameters.AddWithValue("@CUtilisateur", CUtilisateur);

                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }

        public static void Supprimer(SqlTransaction transaction, string CUtilisateur)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UtilisateurSite_Supprimer";
                cmd.Parameters.AddWithValue("@CUtilisateur", CUtilisateur);

                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }

        public static UtilisateurSite Charger(string CSociete, string CSite, string CUtilisateur)
        {
            UtilisateurSite utilisateurSite = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "UtilisateurSite_Charger";
                    cmd.Parameters.AddWithValue("@CSociete", CSociete);
                    cmd.Parameters.AddWithValue("@CSite", CSite);
                    cmd.Parameters.AddWithValue("@CUtilisateur", CUtilisateur);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            utilisateurSite = new UtilisateurSite();

                            utilisateurSite.CSociete = dr["CSociete"].ToString();
                            utilisateurSite.CSite = dr["CSite"].ToString();
                            utilisateurSite.CUtilisateur = dr["CUtilisateur"].ToString();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return utilisateurSite;
        }
    }

    public class UtilisateurSiteCollection
    { 
    
    }
}
