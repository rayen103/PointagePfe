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
    public class PrevisionEmballage
    {
        public int Annee { get; set; }
        public int Mois { get; set; }
        public string Code_Emb { get; set; }
        public int QuantitePrevu { get; set; }
        public bool BAnnuelle { get; set; }

        public PrevisionEmballage() { }

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
                cmd.CommandText = "PrevisionEmballage_Inserer";
                cmd.Parameters.AddWithValue("@Annee ", this.Annee);
                cmd.Parameters.AddWithValue("@Mois", this.Mois);
                cmd.Parameters.AddWithValue("@Code_Emb", this.Code_Emb);
                cmd.Parameters.AddWithValue("@QuantitePrevu", this.QuantitePrevu);
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
