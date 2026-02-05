using CST.LePoint.Referentiel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST.LePoint.Tiers.Referentiel
{
    public class EtatArticle:Item
    {
        #region Proriétès

        public string CEtatArticle { get; set; }
        public string LibEtatArticle { get; set; }
        public string Color { get; set; }
        public DateTime DateInsertion { get; set; }
        public DateTime DateModification { get; set; }
        public int CreePar { get; set; }
        public int ModifiePar { get; set; }
        public string PCInsertion { get; set; }
        public string PCModification { get; set; }

        #endregion

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
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
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
                cmd.CommandText = "EtatArticle_Sauvegarder";
                cmd.Parameters.AddWithValue("@CEtatArticle", this.CEtatArticle);
                cmd.Parameters.AddWithValue("@LibEtatArticle", this.LibEtatArticle);
                cmd.Parameters.AddWithValue("@Color", this.Color);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                cmd.Parameters.AddWithValue("@CreePar", CreePar);
                cmd.Parameters.AddWithValue("@ModifiePar", ModifiePar);
                cmd.Parameters.AddWithValue("@PCInsertion", Environment.UserName);
                cmd.Parameters.AddWithValue("@PCModification", Environment.UserName);

                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;
                cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw Ex;
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
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
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
                cmd.CommandText = "EtatArticle_Supprimer";
                cmd.Parameters.AddWithValue("@CEtatArticle", this.CEtatArticle);
                cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public static bool Supprimer(string CEtatArticle)
        {
            bool result = false;
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
                    cmd.CommandText = "EtatArticle_Supprimer";
                    cmd.Parameters.AddWithValue("@CEtatArticle", CEtatArticle);
                    cmd.ExecuteNonQuery();
                    result = true;
                    transaction.Commit();
                }
                catch (Exception Ex)
                {
                    transaction.Rollback();
                    throw Ex;
                }
            }
            return result;
        }
    }

    public class EtatArticleCollection : ItemCollection
    {
        public static EtatArticleCollection Charger()
        {
            EtatArticleCollection collection = new EtatArticleCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "EtatArticle_Charger";
                    cmd.Parameters.Add(new SqlParameter("@CEtatArticle", DBNull.Value));

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            EtatArticle Type = new EtatArticle();

                            Type.Code = dr["CEtatArticle"].ToString();
                            if (dr["LibEtatArticle"] != DBNull.Value)
                                Type.Libelle = dr["LibEtatArticle"].ToString();
                            if (dr["Color"] != DBNull.Value)
                                Type.Color = dr["Color"].ToString();
                            collection.Add(Type);
                        }
                        dr.Close();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return collection;
        }

        public static DataTable RemplirGrid()
        {
            DataTable dt = new DataTable();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "EtatArticle_Charger";
                cmd.Parameters.AddWithValue("@CEtatArticle", DBNull.Value);

                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            return (dt);
        }
    }
}
