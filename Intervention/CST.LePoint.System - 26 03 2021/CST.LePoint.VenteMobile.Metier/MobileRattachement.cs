using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CST.LePoint.VenteMobile.Metier
{
    public class MobileRattachement
    {
        #region Propriétés

        public string CClient { get; set; }
        public string CSousTraitant { get; set; }
        public string RaisonSociale { get; set; }
        public string NBonCommande { get; set; }
        public string Cetablissement { get; set; }
        public string Observations { get; set; }
        public string DateRattachement { get; set; }
        public string NRattachement { get; set; }
        public string TypeRattachement { get; set; }
        public DateTime? DateRetour { get; set; }
        public string Remarque { get; set; }
        public string PCInsertion { get; set; }
        public int CreePar { get; set; }
        public string SignatureClient { get; set; }
        public string NConvention { get; set; }
        public DateTime Dateplanification { get; set; }
        public string CEtat { get; set; }
        public string CEquipe { get; set; }
        public string NOrdredeTravail { get; set; }
        public string JustificationVente { get; set; }
        public string JustificationRecouvrement { get; set; }
        public string StrategieConcurence { get; set; }

        public MobileRattachementArticleCollection RattachementArticles = new MobileRattachementArticleCollection();

        #endregion Propriétés

        public MobileRattachement()
        {

        }

        public string Sauvegarder(string id, bool isJustif, string TypeRat,string CJustif)
        {
            string NRattachement = "";
            int indice = 0;
            string format = "yyyy";
            var CurrentYear = DateTime.Now.ToString(format);
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    SqlDataReader reader = null;
                    SqlCommand cmd = new SqlCommand();
                    cmd.Transaction = transaction;
                    cmd.Connection = transaction.Connection;
                    cmd.CommandText = "Mobile_rattachement_Charger";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Exercice", CurrentYear);
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            NRattachement = reader["NRattachement"].ToString();
                            indice = Int32.Parse(reader["DernierIndice"].ToString());
                        }
                        reader.Close();
                        MobileRattachement ordre = new MobileRattachement();
                        ordre = GP_OrdreTravail_avoir(transaction, id);
                        string responsable = AvoirResponsbelTravail(transaction, ordre.CSousTraitant);
                        bool inserer = Gp_Rattement_inserer(transaction, ordre.CClient, ordre.RaisonSociale, ordre.NBonCommande, ordre.CSousTraitant, ordre.Cetablissement, NRattachement, indice, responsable, id, "", TypeRat, "EC"/* Etat encours */);
                        //throw new Exception("hhh");
                        if (inserer != false)
                        {
                            if (TypeRat.Equals("CRM"))
                            {
                                GP_OrdredeTravailCrm_update(transaction, id);
                                if (isJustif)
                                {
                                    ReclamationOrdre(transaction, id, true);
                                    Gp_Rattement_update(transaction, NRattachement, null, null, ordre.CSousTraitant, "0", CJustif, "AN");
                                    ChangerEtat(transaction, "MVENTE", "AN", id);
                                    //MobileRattachementOptions ro = new MobileRattachementOptions();
                                    //ro.Sauvegarder(transaction, NRattachement, null, CJustif, 0, responsable);
                                }
                            }
                            else
                            {
                                GP_OrdredeTravail_update(transaction, id);
                                if (isJustif)
                                {
                                    ReclamationOrdre(transaction, id, false);
                                    Gp_Rattement_update(transaction, NRattachement, null, null, ordre.CSousTraitant, "0", CJustif, "AN");
                                }
                            }
                            //GP_OrdredeTravailDetail_avoir(transaction, id, nouveau_bon_commande, ordre.CClient);
                        }
                    }
                    transaction.Commit();

                    return (NRattachement);
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return "error";
                }
            }
        }

        public string SauvegarderBCL( string responsable, string CClient, string RaisonSociale)
        {
            string nouveau_bon_commande = "";
            int indice = 0;
            string format = "yyyy";
            var CurrentYear = DateTime.Now.ToString(format);
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    SqlDataReader reader = null;
                    SqlCommand cmd = new SqlCommand();
                    cmd.Transaction = transaction;
                    cmd.Connection = transaction.Connection;
                    cmd.CommandText = "Mobile_rattachement_Charger";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Exercice", CurrentYear);
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            nouveau_bon_commande = reader["NRattachement"].ToString();
                            indice = Int32.Parse(reader["DernierIndice"].ToString());
                        }
                        reader.Close();
                    }
                    transaction.Commit();

                    return (nouveau_bon_commande);
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return "error";
                }
            }
        }

        public static MobileRattachement GP_OrdreTravail_avoir(SqlTransaction transaction, string Ordre)
        {
            MobileRattachement ordre = new MobileRattachement();
            try
            {
                SqlDataReader reader = null;
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Mobile_OrdredeTravail_Charger";
                cmd.Parameters.AddWithValue("@NOrdredeTravail", Ordre);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ordre.CClient = reader["CClient"] == DBNull.Value ? "" : reader["CClient"].ToString();
                    ordre.CSousTraitant = reader["CSousTraitant"] == DBNull.Value ? "" : reader["CSousTraitant"].ToString();
                    ordre.RaisonSociale = reader["RaisonSociale"] == DBNull.Value ? "" : reader["RaisonSociale"].ToString();
                    ordre.NBonCommande = reader["NBonCommande"] == DBNull.Value ? "" : reader["NBonCommande"].ToString();
                    ordre.Cetablissement = reader["Cetablissement"] == DBNull.Value ? "" : reader["Cetablissement"].ToString();
                    ordre.NConvention = reader["NConvention"] == DBNull.Value ? "" : reader["NConvention"].ToString();
                    ordre.Dateplanification = DateTime.Parse(  reader["Dateplanification"].ToString()).Date;
                }
                reader.Close();

                return ordre;
            }
            catch (Exception )
            {
                throw;
            }


        }

        public bool ReclamationOrdre(SqlTransaction transaction, string ordre, bool isCRM)
        {
            bool msg;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Mobile_OT_reclamation_Inserer";
                cmd.Parameters.AddWithValue("@NOrdredeTravail", ordre);
                cmd.Parameters.AddWithValue("@Etat", "Manquer");
                cmd.Parameters.AddWithValue("@iscrm", isCRM);
                cmd.ExecuteNonQuery();
                msg = true;
            }
            catch (Exception)
            {
                msg = false;
                throw;
            }
            return (msg);
        }
        
        public bool Gp_Rattement_inserer(SqlTransaction transaction, string CClient, string RaisonSociale, string NBonCommande, string CSousTraitant, string Cetablissement, string nouveau_bon_commande, int indice, string responsable, string id, string observation, string TypeRat, string cetat)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Mobile_GP_Rattachement_inserer";
                cmd.Parameters.AddWithValue("@NRattachement", nouveau_bon_commande);
                cmd.Parameters.AddWithValue("@Indice", indice);
                cmd.Parameters.AddWithValue("@Responsable", responsable);
                cmd.Parameters.AddWithValue("@CClient", CClient);
                cmd.Parameters.AddWithValue("@RaisonSociale ", RaisonSociale);
                cmd.Parameters.AddWithValue("@NBonCommande", NBonCommande);
                cmd.Parameters.AddWithValue("@CEquipe", CSousTraitant);
                cmd.Parameters.AddWithValue("@NOrdredeTravail", id);
                cmd.Parameters.AddWithValue("@CEtablissement", Cetablissement);           
                cmd.Parameters.AddWithValue("@Observations",observation);
                cmd.Parameters.AddWithValue("@TypeRattachement", TypeRat);
                cmd.Parameters.AddWithValue("@CEtat", cetat);

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Gp_Rattement_update(SqlTransaction transaction, string NRattachement, string signature, string NBonCommande, string user, string ModifiePar, string reclamation, string cetat)
        {
            bool msg;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "MobileGP_Rattachement_Update";
                cmd.Parameters.AddWithValue("@Nrattachment", NRattachement);
                cmd.Parameters.AddWithValue("@SignatureClient", signature);
                cmd.Parameters.AddWithValue("@NBonCommande", NBonCommande);
                cmd.Parameters.AddWithValue("@Observation", reclamation);
                cmd.Parameters.AddWithValue("@CEquipe", user + '|' + ModifiePar);
                cmd.Parameters.AddWithValue("@CEtat", cetat);

                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;
                cmd.ExecuteNonQuery();
                msg = true;
            }
            catch (Exception)
            {
                msg = false;
                throw;
            }
            return (msg);
        }
        
        public string AvoirResponsbelTravail(SqlTransaction transaction, string Ordre)
        {
            string rsp = "";
            try
            {
                SqlDataReader reader = null;
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Mobile_responsableOrdreTravail_Charger";
                cmd.Parameters.AddWithValue("@CEquipe", Ordre);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    rsp = reader["Responsable"].ToString();
                }
                reader.Close();
                return rsp;

            }
            catch (Exception)
            {
                throw;
            }

        }

        public static List<MobileRattachement> GP_BonCommandelibre_avoir(string equipe,string dd ,string df)
        {
            MobileRattachement rattachement = new MobileRattachement();
            List<MobileRattachement> mobiList=new List<MobileRattachement>();
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    SqlDataReader reader = null;
                    SqlCommand cmd = new SqlCommand();
                    cmd.Transaction = transaction;
                    cmd.Connection = transaction.Connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Mobile_listebonlibre_charger";
                    cmd.Parameters.AddWithValue("@Cequipe", equipe);
                    cmd.Parameters.AddWithValue("dd", dd);
                    cmd.Parameters.AddWithValue("df", df);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        rattachement.NRattachement = reader["rattachement"].ToString();
                        rattachement.DateRattachement = reader["DateRattachement"].ToString();
                        rattachement.RaisonSociale = reader["raisonsociale"].ToString();
                        rattachement.CClient = reader["cclient"].ToString();
                          mobiList.Add(rattachement);
                    }
                    reader.Close();
                }
                catch (Exception)
                {
                    throw;
                }
                return mobiList;

            }
        }

        public void GP_OrdredeTravail_update(SqlTransaction transaction, string id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Mobile_Ordretravail_Modifier";
                cmd.Parameters.AddWithValue("@NOrdredeTravail", id);
                cmd.Parameters.AddWithValue("@EtatOT", "En cours");
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public void GP_OrdredeTravailCrm_update(SqlTransaction transaction, string id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "MobileCrm_Ordretravail_Modifier";
                cmd.Parameters.AddWithValue("@NOrdredeTravail", id);
                cmd.Parameters.AddWithValue("@EtatOT", "En cours");
                cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public void GP_OrdredeTravailDetail_avoir(SqlTransaction transaction, string id, string nouveau_bon_commande, string client)
        {
            string CEntrepot = "", CUnite = "", CArticle = "", LibArticle = "", QuantitePreparee = "", PrixHT = "", QuantiteOT = "";
            try
            {
                SqlDataReader reader = null;
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Mobile_OrdredeTravailDetail_Charger";
                cmd.Parameters.AddWithValue("@NOrdredeTravail", id);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CEntrepot = reader["CEntrepot"].ToString();
                    CUnite = reader["CUnite"].ToString();
                    CArticle = reader["CArticle"].ToString();
                    LibArticle = reader["LibArticle"].ToString();
                    QuantitePreparee = reader["QuantitePreparee"].ToString();
                    PrixHT = reader["PrixHT"].ToString();
                    QuantiteOT = reader["QuantiteOT"].ToString();

                }
                reader.Close();
                decimal resvient = Decimal.Parse(PrixHT) * Decimal.Parse(QuantitePreparee);

                GP_RattachementArticle_Inserer(transaction, id, nouveau_bon_commande, client, CEntrepot, CUnite, CArticle, LibArticle, QuantitePreparee, PrixHT, QuantiteOT, resvient);

            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public void GP_RattachementArticle_Inserer(SqlTransaction transaction, string id, string nouveau_bon_commande, string client, string CEntrepot, string CUnite, string CArticle, string LibArticle, string QuantitePreparee, string PrixHT, string QuantiteOT, decimal resvient)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Mobile_GP_RattachementArticle_inserer";
                cmd.Parameters.AddWithValue("@CArticle", CArticle);
                cmd.Parameters.AddWithValue("@NRattachement", nouveau_bon_commande);
                cmd.Parameters.AddWithValue("@CEntrepot", CEntrepot);
                cmd.Parameters.AddWithValue("@Libelle", LibArticle);
                cmd.Parameters.AddWithValue("@Quantite", Decimal.Parse(QuantitePreparee));
                cmd.Parameters.AddWithValue("@PrixRevient", Decimal.Parse(PrixHT));
                cmd.Parameters.AddWithValue("@CUnite", CUnite);
                cmd.Parameters.AddWithValue("@Revient", resvient);
                cmd.Parameters.AddWithValue("@QuantiteOTRattachement", Decimal.Parse(QuantiteOT));
                cmd.Parameters.AddWithValue("@NOrdredeTravail", id);
                cmd.Parameters.AddWithValue("@CClient", client);
                cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {
                throw;

            }
        }

        public void ChangerEtat(SqlTransaction transaction, string typeVisite, string cetat, string nordredeTravail) 
        { 
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Mobile_GP_Rattachement_UpdateEtat";
                cmd.Parameters.AddWithValue("@Type", typeVisite);
                cmd.Parameters.AddWithValue("@CEtat", cetat);
                cmd.Parameters.AddWithValue("@NOrdredeTravail", nordredeTravail);
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

        public void Modifier(SqlTransaction transaction, string n)
        {
            try
            {
                MobileRattachementOptions options = new MobileRattachementOptions();
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "MobileGP_Rattachement_Modifier";
                cmd.Parameters.AddWithValue("@NRattachement", n);
                cmd.Parameters.AddWithValue("@TypeRattachement", TypeRattachement);
                cmd.Parameters.AddWithValue("@DateRetour", DateRetour);
                cmd.Parameters.AddWithValue("@Remarque", Remarque);
                cmd.Parameters.AddWithValue("@SignatureClient", SignatureClient);
                cmd.Parameters.AddWithValue("@CEtat", this.CEtat);
                cmd.Parameters.AddWithValue("@JustificationVente", this.JustificationVente);
                cmd.Parameters.AddWithValue("@JustificationRecouvrement", this.JustificationRecouvrement);
                cmd.Parameters.AddWithValue("@StrategieConcurence", this.StrategieConcurence);

                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        this.NRattachement = dr["NRattachement"].ToString();
                        //this.Indice = int.Parse(dr["DernierIndice"].ToString());
                    }
                }

                foreach (MobileRattachementArticle rattachementArticle in RattachementArticles)
                {
                    //if (rattachementArticle.CArticle == "SCNC" || rattachementArticle.CArticle == "JUSTIF")
                    //    options.Sauvegarder(transaction, n, rattachementArticle.CArticle, rattachementArticle.CNoteRattachement, CreePar, PCInsertion);
                    //else {
                        rattachementArticle.NRattachement = n;
                        rattachementArticle.CRM_Sauvegarder(transaction);
                    //}
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// This method used for the new Etat, then we called when we need to finish the Visite Directly
        /// NOrdredeTravail, CClient, RaisonSociale, CEquipe, TypeRattachement, CEtat
        /// </summary>
        /// <remarks>
        /// Create a rattachement if not exist, i use NOrdretravail instead of NRattachement
        /// </remarks>
        /// <example>
        /// Etat = accompie/ non accomplie/ Non Rapporter
        /// </example>
        public void SauvegarderDirect(SqlTransaction transaction) 
        {
            try
            {
                SqlCommand cmd  = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection  = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Mobile_GP_Rattachement_SauvegarderDirect";
                cmd.Parameters.AddWithValue("@NOrdredeTravail", this.NOrdredeTravail);
                cmd.Parameters.AddWithValue("@CClient", this.CClient);
                cmd.Parameters.AddWithValue("@RaisonSociale ", this.RaisonSociale);
                cmd.Parameters.AddWithValue("@CEquipe", this.CEquipe);
                cmd.Parameters.AddWithValue("@TypeRattachement", this.TypeRattachement);
                cmd.Parameters.AddWithValue("@CEtat", this.CEtat);
                cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);

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