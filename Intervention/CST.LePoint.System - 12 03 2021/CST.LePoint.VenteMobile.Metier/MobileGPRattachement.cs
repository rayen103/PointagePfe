using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST.LePoint.VenteMobile.Metier
{

    public class MobileGPRattachement
    {
        #region Propriétés

        public string NRattachement { get; set; }
        public string TypeRattachement { get; set; }
        public string CClient { get; set; }
        public string RaisonSociale { get; set; }
        public string NBonCommande { get; set; }
        public DateTime DateRetour { get; set; }
        public string Observation { get; set; }
        public string SignatureClient { get; set; }
        public string NConvention { get; set; }
        public DateTime Dateplanification { get; set; }
        public string CEtat { get; set; }
        public string Equipe { get; set; }
        public string CEquipe { get; set; }
        public string NOrdredeTravail { get; set; }
        public string JustificationVente { get; set; }
        public string JustificationRecouvrement { get; set; }
        public string StrategieConcurence { get; set; }
        public DateTime DateInsertion { get; set; }
        public DateTime DateModification { get; set; }
        public int CreePar { get; set; }
        public int ModifiePar { get; set; }
        public string PCInsertion { get; set; }
        public string PCModification { get; set; }

        #endregion

        public static MobileGPRattachement GP_OT_Charger(string NOrdredeTravail)
        {
            MobileGPRattachement rattachement = new MobileGPRattachement();
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "Mobile_GP_OrdredeTravail_Charger";
                    cmd.Parameters.AddWithValue("@NOrdredeTravail", NOrdredeTravail);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            rattachement.NOrdredeTravail = NOrdredeTravail;
                            rattachement.CEquipe = dr["CEquipe"].ToString();
                            rattachement.Equipe = dr["Equipe"].ToString();
                            rattachement.CClient = dr["CClient"].ToString();
                            if (dr["RaisonSociale"] != DBNull.Value)
                                rattachement.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["NBonCommande"] != DBNull.Value)
                                rattachement.NBonCommande = dr["NBonCommande"].ToString();
                            if (dr["NConvention"] != DBNull.Value)
                                rattachement.NConvention = dr["NConvention"].ToString();
                        }

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return rattachement;
        }

        public string Sauvgarder()
        {
            string Status = "";
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.Transaction = transaction;
                        cmd.Connection = transaction.Connection;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Mobile_GP_Rattachement_Sauvgarder";
                        cmd.Parameters.AddWithValue("@CClient", CClient);
                        cmd.Parameters.AddWithValue("@RaisonSociale ", RaisonSociale);
                        cmd.Parameters.AddWithValue("@NBonCommande", NBonCommande);
                        cmd.Parameters.AddWithValue("@Equipe", Equipe);
                        cmd.Parameters.AddWithValue("@CEquipe", CEquipe);
                        cmd.Parameters.AddWithValue("@NOrdredeTravail", NOrdredeTravail);
                        cmd.Parameters.AddWithValue("@Observation", Observation);
                        cmd.Parameters.AddWithValue("@TypeRattachement", TypeRattachement);
                        cmd.Parameters.AddWithValue("@CEtat", CEtat);
                        foreach (SqlParameter parametre in cmd.Parameters)
                            if (parametre.Value == null)
                                parametre.Value = DBNull.Value;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                this.NRattachement = dr["NRattachement"].ToString();
                                Status = dr["Status"].ToString();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Status = ex.Message;
                        throw;
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Status = ex.Message;
                    throw;
                }
            }
            return Status;
        }

        public string Sauvgarder(SqlTransaction transaction)
        {
            string Status = "";
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Mobile_GP_Rattachement_Sauvgarder";
                cmd.Parameters.AddWithValue("@CClient", CClient);
                cmd.Parameters.AddWithValue("@RaisonSociale ", RaisonSociale);
                cmd.Parameters.AddWithValue("@NBonCommande", NBonCommande);
                cmd.Parameters.AddWithValue("@Equipe", CEquipe);
                cmd.Parameters.AddWithValue("@CEquipe", CEquipe);
                cmd.Parameters.AddWithValue("@NOrdredeTravail", NOrdredeTravail);
                cmd.Parameters.AddWithValue("@Observation", Observation);
                cmd.Parameters.AddWithValue("@TypeRattachement", TypeRattachement);
                cmd.Parameters.AddWithValue("@CEtat", CEtat);
                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        this.NRattachement = dr["NRattachement"].ToString();
                        Status = dr["Status"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Status = ex.Message;
                throw;
            }
            return Status;
        }
    }
}
