using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST.LePoint.Vente.Metier
{
    public class PrevisionProduction
    {
        public int Annee { get; set; }
        public int Mois { get; set; }
        public string Code_MP { get; set; }
        public decimal Tonnage { get; set; }
        public bool BAnnuelle { get; set; }

        public PrevisionProduction() { }

        public void Inserer()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    Inserer(transaction);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
        public void Inserer(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PrevisionProduction_Inserer";
                cmd.Parameters.AddWithValue("@Annee ", this.Annee);
                cmd.Parameters.AddWithValue("@Mois", this.Mois);
                cmd.Parameters.AddWithValue("@Code_MP", this.Code_MP);
                cmd.Parameters.AddWithValue("@Tonnage", this.Tonnage);
                cmd.Parameters.AddWithValue("@BAnnuelle", this.BAnnuelle);
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
        


    }
}
