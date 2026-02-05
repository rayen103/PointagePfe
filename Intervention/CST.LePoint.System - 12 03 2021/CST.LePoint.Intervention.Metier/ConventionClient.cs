using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CST.LePoint.Intervention.Metier
{
    public class ConventionClient
    {
        #region Proriétès

        public string NConvention { get; set; }
        public int Indice { get; set; }
        public string CClient { get; set; }
        public string CEtablissement { get; set; }
        public string RaisonSociale { get; set; }
        public DateTime? DateConvention { get; set; }
        public string NContrat { get; set; }
        public DateTime? DateDebutCtr { get; set; }
        public DateTime? DateFinCtr { get; set; }
        public bool BMajoration { get; set; }
        public DateTime? DateDebutMaj { get; set; }
        public decimal PourcentageMaj { get; set; }
        public string PeriodecitePaiement { get; set; }
        public bool BArret { get; set; }
        public string DebutFinPeriode { get; set; }
        public DateTime DaterepriseFacturation { get; set; }
        public string Observation { get; set; }
        public DateTime? DateInsertion { get; set; }
        public DateTime? DateModification { get; set; }
        public int CreePar { get; set; }
        public int ModifiePar { get; set; }
        public string PCInsertion { get; set; }
        public string PCModification { get; set; }
        public string Exercice { get; set; }
        public string CResponsable { get; set; }
        public bool BNFacturation { get; set; }
        public int TIntervention { get; set; }
        public int NbrePassage { get; set; }
        public string Archive { get; set; }
        public DateTime? DatePremierPssg { get; set; }
        public int PeriodicitePlanif { get; set; }
        public string TypeConvention { get; set; }
        public string Adresse { get; set; }
        public string Telephone { get; set; }
        public string CTVA { get; set; }
        public bool BFactureVisiteValid { get; set; }
        public string CTypeVisite { get; set; }
        public string NFeuilleRoute { get; set; }
        public string CEquipe { get; set; }
        public string CCircuit { get; set; }
        public bool BPlanificationAuto { get; set; }
        public List<GeneratedDate> Gdate { get; set; }
        public List<DateTime> RemoveDate { get; set; }

        public ConventionClientDetailCollection conventionClientDetailCollection = new ConventionClientDetailCollection();
        public ConventionClientSimulationCollection conventionClientSimulationCollection = new ConventionClientSimulationCollection();
        public ConventionClientPlanificationMotifCollection conventionClientPlanificationMotifCollection = new ConventionClientPlanificationMotifCollection();

        public List<DateTime> DatesPlanif = new List<DateTime>();
        public List<DateTime> AncDatesPlanif = new List<DateTime>();

        #endregion Proriétès
        
        public ConventionClient()
        {
        }

        public void Inserer()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    Inserer(transaction);

                    //Insertion Planification
                    //supprimer les affectations
                    //for (int j = 0; j < this.AncDatesPlanif.Count(); j++)
                    //{
                    //    SupprimerPlanif(transaction, this.AncDatesPlanif[j]);
                    //    SupprimerPlanifTech(transaction, this.NConvention, this.AncDatesPlanif[j]);
                    //}

                    foreach (GeneratedDate gd in this.Gdate)
                    {
                        InsererDatePlanif(transaction, gd.Dates, gd.Duree);
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// inserer une date dans le tableau convention client planification s'elle n'existe pas ou supprimer la
        /// </summary>
        public void PA_Modif()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    //suppression
                    foreach (DateTime dt in RemoveDate)
                        PA_MOdif_CCP_Supprimer(transaction, dt);
    
                    //Insertion
                    foreach (GeneratedDate gd in this.Gdate)
                        PA_MOdif_CCP_Sauvegarder(transaction, gd.Dates, gd.Duree);
                    
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public void PA_MOdif_CCP_Sauvegarder(SqlTransaction transaction, DateTime date, int duree)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ConventionClientPlanification_PA_Modif_Inserer";

                cmd.Parameters.AddWithValue("@NConvention", this.NConvention);
                cmd.Parameters.AddWithValue("@DatePlanification", date);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);
                cmd.Parameters.AddWithValue("@CreePar", this.CreePar);
                cmd.Parameters.AddWithValue("@Duree", duree);
                cmd.Parameters.AddWithValue("@BPlanificationAuto", this.BPlanificationAuto);

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

        public void PA_MOdif_CCP_Supprimer(SqlTransaction transaction, DateTime date)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ConventionClientPlanification_PA_Modif_Supprimer";

                cmd.Parameters.AddWithValue("@CClient", this.CClient);
                cmd.Parameters.AddWithValue("@DatePlanification", date);

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

        public void InsererPlanifTech(string conv, DateTime date, string cTech, string nom, int ordrevisite, string cclient, string raisSoc, string Cequipe, string responsable, string Cresponsable)
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    InsererDatePlanifTech(transaction, conv, date, cTech, nom, ordrevisite, cclient, raisSoc, Cequipe, responsable, Cresponsable, "");


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
                cmd.CommandText = "ConventionClient_Inserer";

                cmd.Parameters.AddWithValue("@CClient", this.CClient);
                cmd.Parameters.AddWithValue("@CEtablissement", this.CEtablissement);
                cmd.Parameters.AddWithValue("@RaisonSociale", this.RaisonSociale);
                cmd.Parameters.AddWithValue("@DateConvention", this.DateConvention);
                cmd.Parameters.AddWithValue("@NContrat", this.NContrat);
                cmd.Parameters.AddWithValue("@DateDebutCtr", this.DateDebutCtr);
                cmd.Parameters.AddWithValue("@DateFinCtr ", this.DateFinCtr);
                cmd.Parameters.AddWithValue("@BMajoration ", this.BMajoration);
                cmd.Parameters.AddWithValue("@DateDebutMaj ", this.DateDebutMaj);
                cmd.Parameters.AddWithValue("@BArret ", this.BArret);
                cmd.Parameters.AddWithValue("@BNFacturation ", this.BNFacturation);
                cmd.Parameters.AddWithValue("@DebutFinPeriode ", this.DebutFinPeriode);
                cmd.Parameters.AddWithValue("@DaterepriseFacturation ", this.DaterepriseFacturation);
                cmd.Parameters.AddWithValue("@PeriodecitePaiement", this.PeriodecitePaiement);
                cmd.Parameters.AddWithValue("@Observation", this.Observation);
                cmd.Parameters.AddWithValue("@Exercice", this.Exercice);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);
                cmd.Parameters.AddWithValue("@CreePar", this.CreePar);
                cmd.Parameters.AddWithValue("@Cresponsable", this.CResponsable);
                cmd.Parameters.AddWithValue("@TIntervention", this.TIntervention);
                cmd.Parameters.AddWithValue("@NbrePassage", this.NbrePassage);
                cmd.Parameters.AddWithValue("@Archive", this.Archive);
                cmd.Parameters.AddWithValue("@DatePremierPssg", this.DatePremierPssg);
                cmd.Parameters.AddWithValue("@PeriodicitePlanif", this.PeriodicitePlanif);
                cmd.Parameters.AddWithValue("@TypeConvention", this.TypeConvention);
                cmd.Parameters.AddWithValue("@Adresse", this.Adresse);
                cmd.Parameters.AddWithValue("@Telephone", this.Telephone);
                cmd.Parameters.AddWithValue("@CTVA", this.CTVA);
                cmd.Parameters.AddWithValue("@BFactureVisiteValid", this.BFactureVisiteValid);
                cmd.Parameters.AddWithValue("@CTypeVisite", this.CTypeVisite);
                cmd.Parameters.AddWithValue("@CEquipe", this.CEquipe);
                cmd.Parameters.AddWithValue("@CCircuit", this.CCircuit);


                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        this.NConvention = dr["NConvention"].ToString();
                        this.Indice = int.Parse(dr["DernierIndice"].ToString());
                    }
                }
                int i = 1;
                foreach (ConventionClientDetail DevisDetail in conventionClientDetailCollection)
                {
                    DevisDetail.NConvention = this.NConvention;
                    DevisDetail.Ordre = i++;
                    DevisDetail.Sauvegarder(transaction);
                }

                int j = 1;
                foreach (ConventionClientSimulation DevisDetailSim in conventionClientSimulationCollection)
                {
                    DevisDetailSim.NConvention = this.NConvention;
                    DevisDetailSim.Ordre = j++;
                    DevisDetailSim.Sauvegarder(transaction);
                }


            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public void InsererDatePlanif(SqlTransaction transaction, DateTime date, int duree)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ConventionClientPlanif_Inserer";

                cmd.Parameters.AddWithValue("@NConvention", this.NConvention);
                cmd.Parameters.AddWithValue("@Annee", this.Exercice);
                cmd.Parameters.AddWithValue("@DatePlanif", date);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);
                cmd.Parameters.AddWithValue("@CreePar", this.CreePar);
                cmd.Parameters.AddWithValue("@TIntervention", this.TIntervention);
                cmd.Parameters.AddWithValue("@CTypeVisite", this.CTypeVisite);
                cmd.Parameters.AddWithValue("@Duree", duree);
                cmd.Parameters.AddWithValue("@BPlanificationAuto", this.BPlanificationAuto);
                
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

        public void ModifierConventionclientplanifTypeVisite(string NConvention,string CTypeVisite, DateTime date)
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    ModifierConventionclientplanifTypeVisite(transaction, NConvention, CTypeVisite, date);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public void ModifierConventionclientplanifTypeVisite(SqlTransaction transaction, string NConvention,string CTypeVisite, DateTime date)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ConventionclientplanifTypeVisite_Modifier";

                cmd.Parameters.AddWithValue("@NConvention", NConvention);
                cmd.Parameters.AddWithValue("@DatePlanif", date);
                cmd.Parameters.AddWithValue("@CTypeVisite", CTypeVisite);
                cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);

                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                cmd.ExecuteNonQuery();


            }
            catch (Exception)
            {
                throw;
            }
        }

        public void RealisationDatePlanif(DateTime date,Boolean realise)
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    RealisationDatePlanif(transaction, date, realise);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void RealisationDatePlanif(SqlTransaction transaction, DateTime date, Boolean realise)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ConventionClientPlanif_Realise";

                cmd.Parameters.AddWithValue("@NConvention", this.NConvention);
                cmd.Parameters.AddWithValue("@DatePlanif", date);
                cmd.Parameters.AddWithValue("@BRealise", realise);
                cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                cmd.Parameters.AddWithValue("@PCModification", this.PCModification);
                cmd.Parameters.AddWithValue("@ModifiePar", this.ModifiePar);

                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                cmd.ExecuteNonQuery();
            }
            catch (Exception) {
                throw;
            }
        }

        public string TransfertPlanification(SqlTransaction transaction, string NConvention, DateTime DatePlanificationOLD, DateTime DatePlanificationNew)
        {
            string Status = "FOUND";
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "TransfertPlanification";
                cmd.Parameters.AddWithValue("@NConvention", NConvention);
                cmd.Parameters.AddWithValue("@DatePlanificationOLD", DatePlanificationOLD.Date);
                cmd.Parameters.AddWithValue("@DatePlanificationNew", DatePlanificationNew.Date);

                foreach (SqlParameter parametre in cmd.Parameters)                
                    if (parametre.Value == null)                    
                        parametre.Value = DBNull.Value;

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        Status = dr["Status"].ToString();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return Status;
        }

        public void InsererDatePlanifTech(SqlTransaction transaction, string conv, DateTime date, string cTech, string nom, int ordreVisite, string cclient, string raisSoc, string Cequipe, string responsable, string Cresponsable, string Recommandation)
        {
            try
            {                
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ConventionClientPlanifTech_Inserer";

                cmd.Parameters.AddWithValue("@NConvention", conv);
                cmd.Parameters.AddWithValue("@DatePlanif", date);
                cmd.Parameters.AddWithValue("@CTechnicien", cTech);
                cmd.Parameters.AddWithValue("@CTechnicienOLD", Cequipe);
                cmd.Parameters.AddWithValue("@NomTechnicien", nom);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);
                cmd.Parameters.AddWithValue("@CreePar", this.CreePar);
                cmd.Parameters.AddWithValue("@TIntervention", this.TIntervention);
                cmd.Parameters.AddWithValue("@ordreVisite", ordreVisite);
                cmd.Parameters.AddWithValue("@BValid", false);
                cmd.Parameters.AddWithValue("@CClient", cclient);
                cmd.Parameters.AddWithValue("@RaisSoc", raisSoc);
                cmd.Parameters.AddWithValue("@NFeuilleRoute", this.NFeuilleRoute);
                cmd.Parameters.AddWithValue("@Responsable", string.IsNullOrEmpty(responsable) ? null : responsable);
                cmd.Parameters.AddWithValue("@CResponsable", string.IsNullOrEmpty(Cresponsable) ? null : Cresponsable);
                cmd.Parameters.AddWithValue("@Recommandation", Recommandation);
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

        public void Modifier()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    Modifier(transaction);
                    //Insertion Planification
                    

                    //supprimer les affectations
                    foreach (DateTime dt in this.AncDatesPlanif)
                    {
                        SupprimerPlanif(transaction, dt);
                        SupprimerPlanifTech(transaction, this.NConvention, dt);
                        OrdredeTravail ordretravail = OrdredeTravail.Chargerpost(this.NConvention, dt, null);
                        if (ordretravail.NOrdredeTravail != null)
                        {
                            RattachementCollection rt = RattachementCollection.ChargerparOT(ordretravail.NOrdredeTravail);
                            foreach (Rattachement ra in rt)
                            {
                                Rattachement r = new Rattachement();
                                r.NRattachement = r.NRattachement;
                                r.Supprimer(transaction);
                            }
                            OrdredeTravail ov = new OrdredeTravail();
                            ov.NOrdredeTravail = ordretravail.NOrdredeTravail;
                            ov.Supprimer(transaction);
                        }
                    }

                    foreach (GeneratedDate gd in this.Gdate)
                    {
                        InsererDatePlanif(transaction, gd.Dates, gd.Duree);
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public void Modifier(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ConventionClient_Modifier";
                cmd.Parameters.AddWithValue("@NConvention", this.NConvention);
                cmd.Parameters.AddWithValue("@CClient", this.CClient);
                cmd.Parameters.AddWithValue("@CEtablissement", this.CEtablissement);
                cmd.Parameters.AddWithValue("@RaisonSociale", this.RaisonSociale);
                cmd.Parameters.AddWithValue("@DateConvention", this.DateConvention);
                cmd.Parameters.AddWithValue("@NContrat", this.NContrat);
                cmd.Parameters.AddWithValue("@DateDebutCtr", this.DateDebutCtr);
                cmd.Parameters.AddWithValue("@DateFinCtr ", this.DateFinCtr);
                cmd.Parameters.AddWithValue("@BMajoration ", this.BMajoration);
                cmd.Parameters.AddWithValue("@DateDebutMaj ", this.DateDebutMaj);
                cmd.Parameters.AddWithValue("@BArret ", this.BArret);
                cmd.Parameters.AddWithValue("@BNFacturation ", this.BNFacturation);
                cmd.Parameters.AddWithValue("@DebutFinPeriode ", this.DebutFinPeriode);
                cmd.Parameters.AddWithValue("@DaterepriseFacturation ", this.DaterepriseFacturation);
                cmd.Parameters.AddWithValue("@PeriodecitePaiement", this.PeriodecitePaiement);
                cmd.Parameters.AddWithValue("@Observation", this.Observation);
                cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                cmd.Parameters.AddWithValue("@PCModification", this.PCModification);
                cmd.Parameters.AddWithValue("@ModifiePar", this.ModifiePar);
                cmd.Parameters.AddWithValue("@Cresponsable", this.CResponsable);
                cmd.Parameters.AddWithValue("@NbrePassage", this.NbrePassage);
                cmd.Parameters.AddWithValue("@Archive", this.Archive);
                cmd.Parameters.AddWithValue("@DatePremierPssg", this.DatePremierPssg);
                cmd.Parameters.AddWithValue("@PeriodicitePlanif", this.PeriodicitePlanif);
                cmd.Parameters.AddWithValue("@TIntervention", this.TIntervention);
                cmd.Parameters.AddWithValue("@TypeConvention", this.TypeConvention);
                cmd.Parameters.AddWithValue("@Adresse", this.Adresse);
                cmd.Parameters.AddWithValue("@Telephone", this.Telephone);
                cmd.Parameters.AddWithValue("@CTVA", this.CTVA);
                cmd.Parameters.AddWithValue("@BFactureVisiteValid", this.BFactureVisiteValid);
                cmd.Parameters.AddWithValue("@CEquipe", this.CEquipe);

                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                cmd.ExecuteNonQuery();
                this.SupprimerDetailConventionAnterieurs(transaction);
                this.SupprimerDetailSimulationConventionAnterieurs(transaction);

                int i = 0;
                foreach (ConventionClientDetail DevisDetail in conventionClientDetailCollection)
                {
                    DevisDetail.NConvention = this.NConvention;
                    DevisDetail.Ordre = i++;
                    DevisDetail.Sauvegarder(transaction);
                }
                int j = 0;
                foreach (ConventionClientSimulation DevisDetailSim in conventionClientSimulationCollection)
                {
                    DevisDetailSim.NConvention = this.NConvention;
                    DevisDetailSim.Ordre = j++;
                    DevisDetailSim.Sauvegarder(transaction);
                }
                foreach (ConventionClientPlanificationMotif motif in conventionClientPlanificationMotifCollection)
                {
                    motif.NConvention = this.NConvention;
                    motif.Sauvegarder(transaction);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        private void SupprimerDetailConventionAnterieurs(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ConventionClient_SupprimerDetails";

                cmd.Parameters.AddWithValue("@NConvention", this.NConvention);

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
        
        private void SupprimerDetailSimulationConventionAnterieurs(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ConventionClient_SupprimerDetailsSimulation";

                cmd.Parameters.AddWithValue("@NConvention", this.NConvention);

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
        
        public void Supprimer()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();

                try
                {
                    SqlCommand cmd = new SqlCommand();

                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "ConventionClient_Supprimer";
                    cmd.Parameters.AddWithValue("@NConvention", this.NConvention);

                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        
        public void SupprimerPlanif(SqlTransaction transaction, DateTime date)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "ConventionClientPlanif_Supprimer";
                cmd.Parameters.AddWithValue("@NConvention", this.NConvention);
                cmd.Parameters.AddWithValue("@DatePlanif", date);
                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void SupprimerPlanifTech(string conv, DateTime date)
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    SupprimerPlanifTech(transaction, conv, date);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
        
        public void SupprimerPlanifTech(SqlTransaction transaction, string conv, DateTime date)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "ConventionClientPlanifTech_Supprimer";
                cmd.Parameters.AddWithValue("@NConvention", conv);
                cmd.Parameters.AddWithValue("@DatePlanif", date);

                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public static ConventionClient Charger(string nConv)
        {
            ConventionClient conv = new ConventionClient();
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
                    cmd.CommandText = "ConventionClient_Charger1";
                    cmd.Parameters.AddWithValue("@NConvention", nConv);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            conv.NConvention = dr["NConvention"].ToString();
                            if (dr["DateConvention"] != DBNull.Value)
                                conv.DateConvention = DateTime.Parse(dr["DateConvention"].ToString());
                            if (dr["CClient"] != DBNull.Value)
                                conv.CClient = dr["CClient"].ToString();
                            if (dr["CEtablissement"] != DBNull.Value)
                                conv.CEtablissement = dr["CEtablissement"].ToString();
                            if (dr["RaisonSociale"] != DBNull.Value)
                                conv.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["PeriodecitePaiement"] != DBNull.Value)
                                conv.PeriodecitePaiement = dr["PeriodecitePaiement"].ToString();
                            if (dr["NContrat"] != DBNull.Value)
                                conv.NContrat = dr["NContrat"].ToString();
                            if (dr["DateDebutCtr"] != DBNull.Value)
                                conv.DateDebutCtr = DateTime.Parse(dr["DateDebutCtr"].ToString());
                            if (dr["DateFinCtr"] != DBNull.Value)
                                conv.DateFinCtr = DateTime.Parse(dr["DateFinCtr"].ToString());
                            if (dr["BMajoration"] != DBNull.Value)
                                conv.BMajoration = bool.Parse(dr["BMajoration"].ToString());
                            if (dr["DateDebutMaj"] != DBNull.Value)
                                conv.DateDebutMaj = DateTime.Parse(dr["DateDebutMaj"].ToString());
                            if (dr["BArret"] != DBNull.Value)
                                conv.BArret = bool.Parse(dr["BArret"].ToString());
                            if (dr["DebutFinPeriode"] != DBNull.Value)
                                conv.DebutFinPeriode = dr["DebutFinPeriode"].ToString();
                            if (dr["DaterepriseFacturation"] != DBNull.Value)
                                conv.DaterepriseFacturation = DateTime.Parse(dr["DaterepriseFacturation"].ToString());
                            if (dr["Observation"] != DBNull.Value)
                                conv.Observation = dr["Observation"].ToString();
                            if (dr["CResponsable"] != DBNull.Value)
                                conv.CResponsable = dr["CResponsable"].ToString();
                            if (dr["BNFacturation"] != DBNull.Value)
                                conv.BNFacturation = bool.Parse(dr["BNFacturation"].ToString());
                            if (dr["TIntervention"] != DBNull.Value)
                                conv.TIntervention = int.Parse(dr["TIntervention"].ToString());
                            if (dr["NbrePassage"] != DBNull.Value)
                                conv.NbrePassage = int.Parse(dr["NbrePassage"].ToString());
                            if (dr["Archive"] != DBNull.Value)
                                conv.Archive = dr["Archive"].ToString();
                            if (dr["DatePremierPssg"] != DBNull.Value)
                                conv.DatePremierPssg = DateTime.Parse(dr["DatePremierPssg"].ToString());
                            if (dr["PeriodicitePlanif"] != DBNull.Value)
                                conv.PeriodicitePlanif = int.Parse(dr["PeriodicitePlanif"].ToString());
                            if (dr["TypeConvention"] != DBNull.Value)
                                conv.TypeConvention = dr["TypeConvention"].ToString();
                            if (dr["Adresse"] != DBNull.Value)
                                conv.Adresse = dr["Adresse"].ToString();
                            if (dr["Telephone"] != DBNull.Value)
                                conv.Telephone = dr["Telephone"].ToString();
                            if (dr["CTVA"] != DBNull.Value)
                                conv.CTVA = dr["CTVA"].ToString();
                            if (dr["BFactureVisiteValid"] != DBNull.Value)
                                conv.BFactureVisiteValid = bool.Parse(dr["BFactureVisiteValid"].ToString());
                            if (dr["CTypeVisite"] != DBNull.Value)
                                conv.CTypeVisite = dr["CTypeVisite"].ToString();
                            if (dr["CEquipe"] != DBNull.Value)
                                conv.CEquipe = dr["CEquipe"].ToString();
                            if (dr["CCircuit"] != DBNull.Value)
                                conv.CCircuit = dr["CCircuit"].ToString();
                            conv.conventionClientDetailCollection = ConventionClientDetailCollection.Charger(conv.NConvention);
                            conv.conventionClientSimulationCollection = ConventionClientSimulationCollection.Charger(conv.NConvention);
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return (conv);
            }
        }
        
        public void MiseAJourDateRepriseFact(SqlTransaction transaction, int sens)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "Convention_MiseAJourDateR";
                cmd.Parameters.AddWithValue("@NConvention", this.NConvention);
                cmd.Parameters.AddWithValue("@Sens", sens);
                cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                cmd.Parameters.AddWithValue("@PCModification", this.PCModification);
                cmd.Parameters.AddWithValue("@ModifiePar", this.ModifiePar);

                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void MiseAJourMaj(SqlTransaction transaction, int sens)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "Convention_MiseAjourMaj";
                cmd.Parameters.AddWithValue("@NConvention", this.NConvention);
                cmd.Parameters.AddWithValue("@Sens", sens);
                cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                cmd.Parameters.AddWithValue("@PCModification", this.PCModification);
                cmd.Parameters.AddWithValue("@ModifiePar", this.ModifiePar);

                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void MiseAJourMajSupp(SqlTransaction transaction, int sens)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "Convention_MiseAjourMajSupp";
                cmd.Parameters.AddWithValue("@NConvention", this.NConvention);
                cmd.Parameters.AddWithValue("@Sens", sens);
                cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                cmd.Parameters.AddWithValue("@PCModification", this.PCModification);
                cmd.Parameters.AddWithValue("@ModifiePar", this.ModifiePar);

                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<GeneratedDate> ChargerPlan_Equipe(string Cequipe)
        {
            List<GeneratedDate> collection = new List<GeneratedDate>();
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
                    cmd.CommandText = "ConventionClientPlanifParEquipe_Charger";
                    cmd.Parameters.AddWithValue("@CEquipe", Cequipe);
                    foreach (SqlParameter parameter in cmd.Parameters)
                        if (parameter.Value == null)
                            parameter.Value = DBNull.Value;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            GeneratedDate gd = new GeneratedDate();
                            DateTime d = DateTime.Parse(dr["DatePlanification"].ToString());                               
                            gd.Dates = d;
                            int duree = (int)dr["Duree"];
                            gd.Duree = duree;
                            gd.Circuit = dr["LibCircuit"].ToString();
                            collection.Add(gd);
                            DateTime rangedate = d;
                            for (int i = 1; i < duree; i++)
                            {
                                GeneratedDate gdr = new GeneratedDate();
                                rangedate = d.AddDays(i);
                                gdr.Dates = rangedate;
                                gdr.Duree = gd.Duree;
                                gdr.Circuit = gd.Circuit;
                                collection.Add(gdr);
                            }
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

        public static List<GeneratedDate> ChargerPlan_Equipe_PA_Modif(string Cequipe)
        {
            List<GeneratedDate> collection = new List<GeneratedDate>();
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
                    cmd.CommandText = "ConventionClientPlanifParEquipe_Charger";
                    cmd.Parameters.AddWithValue("@CEquipe", Cequipe);
                    foreach (SqlParameter parameter in cmd.Parameters)
                        if (parameter.Value == null)
                            parameter.Value = DBNull.Value;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            GeneratedDate gd = new GeneratedDate();
                            DateTime d = DateTime.Parse(dr["DatePlanification"].ToString());
                            gd.Dates = d;
                            int duree = (int)dr["Duree"];
                            gd.Duree = duree;
                            gd.Circuit = dr["LibCircuit"].ToString();
                            collection.Add(gd);
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

        public static DateTime? GetMaxDate(string CEquipe)
        {
            DateTime? maxdate= null;
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "ConventionClientPlanification_MaxDate";
                    cmd.Parameters.AddWithValue("@CEquipe", CEquipe);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            if (dr["MaxDate"] != DBNull.Value)
                                maxdate = Convert.ToDateTime(dr["MaxDate"].ToString());
                        }
                    }

                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return maxdate;
        }

        public static List<GeneratedDate> ChargerPlanification_Circuit(string CCircuit)
        {
            List<GeneratedDate> collection = new List<GeneratedDate>();
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
                    cmd.CommandText = "ConventionClientPlanifParCircuit_Charger";
                    cmd.Parameters.AddWithValue("@CCircuit", CCircuit);
                    foreach (SqlParameter parameter in cmd.Parameters)
                        if (parameter.Value == null)
                            parameter.Value = DBNull.Value;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            GeneratedDate gd = new GeneratedDate();
                            DateTime d = DateTime.Parse(dr["DatePlanification"].ToString());
                            gd.Dates = d;
                            int duree = (int)dr["Duree"];
                            gd.Duree = duree;
                            collection.Add(gd);
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

        public static List<GeneratedDate> ChargerPlanification_Circuit_D(string CCircuit)
        {
            List<GeneratedDate> collection = new List<GeneratedDate>();
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
                    cmd.CommandText = "ConventionClientPlanificationCircuit_Charger";
                    cmd.Parameters.AddWithValue("@CCircuit", CCircuit);
                    foreach (SqlParameter parameter in cmd.Parameters)
                        if (parameter.Value == null)
                            parameter.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            GeneratedDate gd = new GeneratedDate();
                            DateTime d = DateTime.Parse(dr["DatePlanification"].ToString());
                            gd.Dates = d;
                            int duree = (int)dr["Duree"];
                            gd.Duree = duree;
                            collection.Add(gd);
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
        
        public static List<CircuitDates> ChargerPlanificationCircuit(string CCircuit)
        {
            List<CircuitDates> collection = new List<CircuitDates>();
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
                    cmd.CommandText = "ConventionClientPlanificationCircuit_Charger";
                    cmd.Parameters.AddWithValue("@CCircuit", CCircuit);
                    foreach (SqlParameter parameter in cmd.Parameters)
                        if (parameter.Value == null)
                            parameter.Value = DBNull.Value;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            CircuitDates cd = new CircuitDates();
                            DateTime d = DateTime.Parse(dr["DatePlanification"].ToString());
                            cd.Dates = d;
                            int duree = (int)dr["Duree"];
                            cd.CCircuit = dr["CCircuit"].ToString();
                            collection.Add(cd);
                            DateTime rangedate = d;
                            for (int i = 1; i < duree; i++)
                            {
                                CircuitDates cdr = new CircuitDates();
                                rangedate = d.AddDays(i);
                                cdr.Dates = rangedate;
                                cdr.CCircuit = cd.CCircuit;
                                collection.Add(cdr);
                            }
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

        public static void AnnulerplannificationClient_Circuit(SqlTransaction transaction, string CCircuit, string CClient)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AnnulerPlanificationClient_Circuit";
                cmd.Parameters.AddWithValue("@CCircuit", CCircuit);
                cmd.Parameters.AddWithValue("@CClient", CClient);

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

        /// <summary>
        /// procedure not created yet
        /// </summary>
        /// <param name="CEquipe"></param>
        /// <returns></returns>
        public static List<PlanificationType> PlanificationType_Rechercher_Equipe(string CEquipe)
        {
            List<PlanificationType> collection = new List<PlanificationType>();
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "PlanificationType_Rechercher_Equipe";
                    cmd.Parameters.AddWithValue("@CEquipe", CEquipe);
                    foreach (SqlParameter parameter in cmd.Parameters)
                        if (parameter.Value == null)
                            parameter.Value = DBNull.Value;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            PlanificationType pt = new PlanificationType();
                            DateTime d = DateTime.Parse(dr["DatePlanification"].ToString());
                            pt.Date = d;
                            pt.Type = dr["Type"].ToString();
                            collection.Add(pt);
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return collection;
            }
        }

    }

    public class ConventionClientCollection : List<ConventionClient>
    {
        public ConventionClientCollection()
        {
        }

        public static ConventionClientCollection Charger(string client, DateTime dated, DateTime datef)
        {
            ConventionClientCollection collection = new ConventionClientCollection();
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
                    cmd.CommandText = "ConventionClient_Charger";
                    cmd.Parameters.AddWithValue("@CClient", client);
                    cmd.Parameters.AddWithValue("@DateD", dated);
                    cmd.Parameters.AddWithValue("@DateF", datef);
                    cmd.Parameters.AddWithValue("@NConvention", DBNull.Value);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ConventionClient conv = new ConventionClient();

                            conv.NConvention = dr["NConvention"].ToString();
                            if (dr["DateConvention"] != DBNull.Value)
                                conv.DateConvention = DateTime.Parse(dr["DateConvention"].ToString());
                            if (dr["CClient"] != DBNull.Value)
                                conv.CClient = dr["CClient"].ToString();
                            if (dr["CEtablissement"] != DBNull.Value)
                                conv.CEtablissement = dr["CEtablissement"].ToString();
                            if (dr["RaisonSociale"] != DBNull.Value)
                                conv.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["PeriodecitePaiement"] != DBNull.Value)
                                conv.PeriodecitePaiement = dr["PeriodecitePaiement"].ToString();
                            if (dr["BArret"] != DBNull.Value)
                                conv.BArret = bool.Parse(dr["BArret"].ToString());
                            if (dr["DebutFinPeriode"] != DBNull.Value)
                                conv.DebutFinPeriode = dr["DebutFinPeriode"].ToString();
                            if (dr["DaterepriseFacturation"] != DBNull.Value)
                                conv.DaterepriseFacturation = DateTime.Parse(dr["DaterepriseFacturation"].ToString());
                            if (dr["NContrat"] != DBNull.Value)
                                conv.NContrat = dr["NContrat"].ToString();
                            if (dr["DateDebutCtr"] != DBNull.Value)
                                conv.DateDebutCtr = DateTime.Parse(dr["DateDebutCtr"].ToString());
                            if (dr["DateFinCtr"] != DBNull.Value)
                                conv.DateFinCtr = DateTime.Parse(dr["DateFinCtr"].ToString());
                            if (dr["BMajoration"] != DBNull.Value)
                                conv.BMajoration = bool.Parse(dr["BMajoration"].ToString());
                            if (dr["PourcentageMaj"] != DBNull.Value)
                                conv.PourcentageMaj = decimal.Parse(dr["PourcentageMaj"].ToString());
                            if (dr["DateDebutMaj"] != DBNull.Value)
                                conv.DateDebutMaj = DateTime.Parse(dr["DateDebutMaj"].ToString());
                            if (dr["CResponsable"] != DBNull.Value)
                                conv.CResponsable = dr["CResponsable"].ToString();
                            if (dr["Observation"] != DBNull.Value)
                                conv.Observation = dr["Observation"].ToString();
                            if (dr["BNFacturation"] != DBNull.Value)
                                conv.BNFacturation = bool.Parse(dr["BNFacturation"].ToString());
                            if (dr["TIntervention"] != DBNull.Value)
                                conv.TIntervention = int.Parse(dr["TIntervention"].ToString());
                            if (dr["NbrePassage"] != DBNull.Value)
                                conv.NbrePassage = int.Parse(dr["NbrePassage"].ToString());
                            if (dr["Archive"] != DBNull.Value)
                                conv.Archive = dr["Archive"].ToString();
                            if (dr["DatePremierPssg"] != DBNull.Value)
                                conv.DatePremierPssg = DateTime.Parse(dr["DatePremierPssg"].ToString());
                            if (dr["PeriodicitePlanif"] != DBNull.Value)
                                conv.PeriodicitePlanif = int.Parse(dr["PeriodicitePlanif"].ToString());
                            if (dr["TypeConvention"] != DBNull.Value)
                                conv.TypeConvention = dr["TypeConvention"].ToString();
                            if (dr["Adresse"] != DBNull.Value)
                                conv.Adresse = dr["Adresse"].ToString();
                            if (dr["Telephone"] != DBNull.Value)
                                conv.Telephone = dr["Telephone"].ToString();
                            if (dr["CTVA"] != DBNull.Value)
                                conv.CTVA = dr["CTVA"].ToString();
                            if (dr["BFactureVisiteValid"] != DBNull.Value)
                                conv.BFactureVisiteValid = bool.Parse(dr["BFactureVisiteValid"].ToString());
                            conv.conventionClientDetailCollection = ConventionClientDetailCollection.Charger(conv.NConvention);
                            conv.conventionClientSimulationCollection = ConventionClientSimulationCollection.Charger(conv.NConvention);

                            collection.Add(conv);
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
    
        public static DataTable ChargerGrid(string client, DateTime dated, DateTime datef)
        {
            DataTable dtListe = new DataTable();
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
                    cmd.CommandText = "ConventionClient_Charger";
                    cmd.Parameters.AddWithValue("@CClient", client);
                    cmd.Parameters.AddWithValue("@DateD", dated);
                    cmd.Parameters.AddWithValue("@DateF", datef);
                    cmd.Parameters.AddWithValue("@NConvention", DBNull.Value);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dtListe);

                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return (dtListe);
            }
        }
        
        public static DataTable ChargerGrid(string client, DateTime dated, DateTime datef, int Filtre,string type)
        {
            DataTable dtListe = new DataTable();
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
                    if (Filtre == 0)
                        cmd.CommandText = "ConventionClient_Charger";
                    else
                        cmd.CommandText = "ConventionClientRep_Charger";

                    cmd.Parameters.AddWithValue("@CClient", client);
                    cmd.Parameters.AddWithValue("@DateD", dated);
                    cmd.Parameters.AddWithValue("@DateF", datef);
                    cmd.Parameters.AddWithValue("@NConvention", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Type", type);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dtListe);

                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return (dtListe);
            }
        }

        public static DataTable ChargerGrid(string client, DateTime dated, DateTime datef, string type, bool BArret)
        {
            DataTable dtListe = new DataTable();
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
                    cmd.CommandText = "ConventionClient_Charger_Latest";
                    cmd.Parameters.AddWithValue("@CClient", client);
                    cmd.Parameters.AddWithValue("@DateD", dated);
                    cmd.Parameters.AddWithValue("@DateF", datef);
                    cmd.Parameters.AddWithValue("@NConvention", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Type", type);
                    cmd.Parameters.AddWithValue("@BArret", BArret);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dtListe);

                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return (dtListe);
            }
        }

        public static DataTable ChargerGrid(string client, DateTime dateR)
        {
            DataTable dtListe = new DataTable();
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
                    cmd.CommandText = "ConventionClientMois_Charger";
                    cmd.Parameters.AddWithValue("@CClient", client);
                    cmd.Parameters.AddWithValue("@DateR", dateR);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dtListe);

                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return (dtListe);
            }
        }

        public static List<DateTime> Charger_DatePlanif(string numeroconv)
        {
            List<DateTime> list = new List<DateTime>();

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
                    cmd.CommandText = "ConventionClientPlanif_Charger";
                    cmd.Parameters.AddWithValue("@NConvention", numeroconv);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(DateTime.Parse(dr["DatePlanification"].ToString()));
                        }
                    }
                }
                catch (Exception)
                {
                    return null;
                    throw;
                }
            }
            return list;
        }

        public static List<GeneratedDate> Charger_DatePlanifDuree(string numeroconv)
        {
            List<GeneratedDate> list = new List<GeneratedDate>();

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
                    cmd.CommandText = "ConventionClientPlanifDuree_Charger";
                    cmd.Parameters.AddWithValue("@NConvention", numeroconv);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            GeneratedDate gd = new GeneratedDate();
                            gd.Dates = DateTime.Parse(dr["DatePlanification"].ToString());
                            gd.Duree = (int)dr["Duree"];
                            list.Add(gd);
                        }
                    }
                }
                catch (Exception)
                {
                    return null;
                    throw;
                }
            }
            return list;
        }

    }

    public class GeneratedDate
    {
        public DateTime Dates { get; set; }
        public int Duree { get; set; }
        public string Circuit { get; set; }
    }

    public class CircuitDates
    {
        public DateTime Dates { get; set; }
        public string CCircuit { get; set; }
    }

    public class PlanificationType
    {
        public DateTime Date { get; set; }
        public string Type { get; set; }
    }
}
