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
    public class Options : Item
    {
        #region Proriétès

        public string COptions { get; set; }
        public string LibOptions { get; set; }
        public DateTime DateInsertion { get; set; }
        public DateTime DateModification { get; set; }
        public int CreePar { get; set; }
        public int ModifiePar { get; set; }
        public string PCInsertion { get; set; }
        public string PCModification { get; set; }
        public string CTypeOptions { get; set; }
        public int Ordre { get; set; }

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
                cmd.CommandText = "Options_Sauvegarder";
                cmd.Parameters.AddWithValue("@COptions", this.COptions);
                cmd.Parameters.AddWithValue("@LibOptions", this.LibOptions);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                cmd.Parameters.AddWithValue("@CreePar", CreePar);
                cmd.Parameters.AddWithValue("@ModifiePar", ModifiePar);
                cmd.Parameters.AddWithValue("@PCInsertion", PCInsertion);
                cmd.Parameters.AddWithValue("@PCModification", PCModification);
                cmd.Parameters.AddWithValue("@CTypeOptions", this.CTypeOptions);
                cmd.Parameters.AddWithValue("@Ordre", this.Ordre);

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
                cmd.CommandText = "Options_Supprimer";
                cmd.Parameters.AddWithValue("@COptions", this.COptions);
                cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public static bool Supprimer(string COptions)
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
                    cmd.CommandText = "Options_Supprimer";
                    cmd.Parameters.AddWithValue("@COptions", COptions);
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
    public class OptionsCollection : ItemCollection
    {
        public OptionsCollection()
        {
        }

        public static OptionsCollection Charger()
        {
            OptionsCollection collection = new OptionsCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Options_Charger";
                    cmd.Parameters.Add(new SqlParameter("@COptions", DBNull.Value));

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Options Type = new Options();

                            Type.Code = dr["COptions"].ToString();
                            if (dr["LibOptions"] != DBNull.Value)
                                Type.Libelle = dr["LibOptions"].ToString();
                            if (dr["CTypeOptions"] != DBNull.Value)
                                Type.CTypeOptions = dr["CTypeOptions"].ToString();
                            if (dr["Ordre"] != DBNull.Value)
                                Type.Ordre = int.Parse(dr["Ordre"].ToString());
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
                cmd.CommandText = "Options_Charger";
                cmd.Parameters.AddWithValue("@COptions", DBNull.Value);

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
