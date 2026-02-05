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

namespace CST.LePoint.Intervention.Metier
{
    public class Rattachement
    {
         #region Propriétés

        [XmlAttribute("NRattachement")]
        [Bindable(true)]
        public string NRattachement { get; set; }

        [XmlAttribute("Exercice")]
        [Bindable(true)]
        public string Exercice { get; set; }
        
        [XmlAttribute("DateRattachement")]
        [Bindable(true)]
        public DateTime DateRattachement { get; set; }

        [XmlAttribute("NChantier")]
        [Bindable(true)]
        public string NChantier { get; set; }

        [XmlAttribute("CClient")]
        [Bindable(true)]
        public string CClient { get; set; }
        [XmlAttribute("BInterne")]
        [Bindable(true)]
        public bool BInterne { get; set; }


        [XmlAttribute("Cout")]
        [Bindable(true)]
        public decimal Cout { get; set; }

        [XmlAttribute("Indice")]
        [Bindable(true)]
        public int Indice { get; set; }

        [XmlAttribute("Type")]
        [Bindable(true)]
        public string Type { get; set; }

        [XmlAttribute("Nature")]
        [Bindable(true)]
        public string Nature { get; set; }

        [XmlAttribute("Responsable")]
        [Bindable(true)]
        public string Responsable { get; set; }

        [XmlAttribute("HeureDebut")]
        [Bindable(true)]
        public string HeureDebut { get; set; }

        [XmlAttribute("HeureFin")]
        [Bindable(true)]
        public string HeureFin { get; set; }

        [XmlAttribute("Emplacement")]
        [Bindable(true)]
        public string Emplacement { get; set; }

        [XmlAttribute("Reference")]
        [Bindable(true)]
        public string Reference { get; set; }

        [XmlAttribute("NBonLivraison")]
        [Bindable(true)]
        public string NBonLivraison { get; set; }

        [XmlAttribute("NFacture")]
        [Bindable(true)]
        public string NFacture { get; set; }

        [XmlAttribute("RaisonSociale")]
        [Bindable(true)]
        public string RaisonSociale { get; set; }

        [XmlAttribute("TypeRattachement")]
        [Bindable(true)]
        public string TypeRattachement { get; set; }

        [XmlAttribute("Cloture")]
        [Bindable(true)]
        public string Cloture { get; set; }

        [XmlAttribute("DateCloture")]
        [Bindable(true)]
        public DateTime? DateCloture { get; set; }

        [XmlAttribute("CoutRH")]
        [Bindable(true)]
        public decimal CoutRH { get; set; }
        [XmlAttribute("CoutAR")]
        [Bindable(true)]
        public decimal CoutAR { get; set; }
        [XmlAttribute("CoutCD")]
        [Bindable(true)]
        public decimal CoutCD { get; set; }
        [XmlAttribute("CoutDevis")]
        [Bindable(true)]
        public decimal CoutDevis { get; set; }
        [XmlAttribute("EmplacementBC")]
        [Bindable(true)]
        public string EmplacementBC { get; set; }
        [XmlAttribute("NBC")]
        [Bindable(true)]
        public string NBC { get; set; }
        [XmlAttribute("DateBC")]
        [Bindable(true)]
        public string DateBC { get; set; }
        [XmlAttribute("MtHtBC")]
        [Bindable(true)]
        public decimal MtHtBC { get; set; }
        [XmlAttribute("MtFact")]
        [Bindable(true)]
        public decimal MtFact { get; set; }
        [XmlAttribute("Remarque")]
        [Bindable(true)]
        public string Remarque { get; set; }
        [XmlAttribute("EstJours")]
        [Bindable(true)]
        public string EstJours { get; set; }
        [XmlAttribute("EstJoursNume")]
        [Bindable(true)]
        public decimal EstJoursNume { get; set; }

        [XmlAttribute("CEtablissement")]
        [Bindable(true)]
        public string CEtablissement { get; set; }

        public RattachementChargesDiversCollection RattachementChargesDiverss = new RattachementChargesDiversCollection();
        //public InterventionVehiculeCollection InterventionVehicules = new InterventionVehiculeCollection();
        public RattachementArticleCollection RattachementArticles = new RattachementArticleCollection();
      //  public RattachementEmployeCollection RattachementEmployes = new RattachementEmployeCollection();
      //  public RattachementTachesCollection RattachementTachess = new RattachementTachesCollection();

        
        [XmlAttribute("CreePar")]
        [Bindable(true)]
        public int CreePar { get; set; }

        [XmlAttribute("ModifiePar")]
        [Bindable(true)]
        public int ModifiePar { get; set; }

        [XmlAttribute("DateInsertion")]
        [Bindable(true)]
        public DateTime DateInsertion { get; set; }

        [XmlAttribute("DateModification")]
        [Bindable(true)]
        public DateTime DateModification { get; set; }

        [XmlAttribute("PCInsertion")]
        [Bindable(true)]
        public string PCInsertion { get; set; }

        [XmlAttribute("PCModification")]
        [Bindable(true)]
        public string PCModification { get; set; }

        [XmlAttribute("NBonCommande")]
        [Bindable(true)]
        public string NBonCommande { get; set; }
        [XmlAttribute("CEquipe")]
        [Bindable(true)]
        public string CEquipe { get; set; }
        [XmlAttribute("NOrdredeTravail")]
        [Bindable(true)]
        public string NOrdredeTravail { get; set; }

        [XmlAttribute("BValid")]
        [Bindable(true)]
        public bool BValid { get; set; }

        [XmlAttribute("BConfirmValid")]
        [Bindable(true)]
        public bool BConfirmValid { get; set; }

        [XmlAttribute("Evaluation")]
        [Bindable(true)]
        public string Evaluation { get; set; }

        [XmlAttribute("SignatureClient")]
        [Bindable(true)]
        public string SignatureClient { get; set; }

         #endregion Propriétés

        public Rattachement()
        {
            this.BInterne = true;

        }


        public static void ModifierBValid(string rattach)
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
                    cmd.CommandType = CommandType.Text;
                    // decimal resultat = qté - fait;
                    cmd.CommandText = "update GP_Rattachement set BValid = '" + "True" + "'   where NRattachement = '" + rattach + "' ";
                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public static void ModifierBConfirmValid(string rattach)
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
                    cmd.CommandType = CommandType.Text;
                    // decimal resultat = qté - fait;
                    cmd.CommandText = "update GP_Rattachement set BConfirmValid = '" + "True" + "'   where NRattachement = '" + rattach + "' ";
                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public static void ModifierBConfirmValid(string rattach, SqlTransaction transaction)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Transaction = transaction;
            cmd.Connection = transaction.Connection;
            cmd.CommandType = CommandType.Text;
            // decimal resultat = qté - fait;
            cmd.CommandText = "update GP_Rattachement set BConfirmValid = '" + "True" + "'   where NRattachement = '" + rattach + "' ";
            cmd.ExecuteNonQuery();
        }


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
                    cmd.CommandText = "GP_Rattachement_Inserer";

                    
                    cmd.Parameters.AddWithValue("@DateRattachement", DateRattachement);
                    cmd.Parameters.AddWithValue("@NChantier", NChantier);
                    cmd.Parameters.AddWithValue("@CClient", CClient);
                    cmd.Parameters.AddWithValue("@Cout", Cout);
                    cmd.Parameters.AddWithValue("@Exercice", Exercice);                 
                    cmd.Parameters.AddWithValue("@Nature", Nature);
                    cmd.Parameters.AddWithValue("@Responsable", Responsable);
                    cmd.Parameters.AddWithValue("@HeureDebut", HeureDebut);
                    cmd.Parameters.AddWithValue("@HeureFin", HeureFin);
                    cmd.Parameters.AddWithValue("@Emplacement", Emplacement);
                    cmd.Parameters.AddWithValue("@Reference", Reference);
                    cmd.Parameters.AddWithValue("@NBonLivraison", NBonLivraison);
                    cmd.Parameters.AddWithValue("@NFacture", NFacture);
                    cmd.Parameters.AddWithValue("@RaisonSociale", RaisonSociale);
                    cmd.Parameters.AddWithValue("@TypeRattachement", TypeRattachement);
                    cmd.Parameters.AddWithValue("@Cloture", Cloture);
                    cmd.Parameters.AddWithValue("@DateCloture", DateCloture);
                    cmd.Parameters.AddWithValue("@CoutRH", CoutRH);
                    cmd.Parameters.AddWithValue("@CoutAR", CoutAR);
                    cmd.Parameters.AddWithValue("@CoutCD", CoutCD);
                    cmd.Parameters.AddWithValue("@CoutDevis", CoutDevis);
                    cmd.Parameters.AddWithValue("@EmplacementBC", EmplacementBC);
                    cmd.Parameters.AddWithValue("@NBC", NBC);
                    cmd.Parameters.AddWithValue("@DateBC", DateBC);
                    cmd.Parameters.AddWithValue("@MtHtBC", MtHtBC);
                    cmd.Parameters.AddWithValue("@MtFact", MtFact);
                    cmd.Parameters.AddWithValue("@Remarque", Remarque);
                    cmd.Parameters.AddWithValue("@EstJours", EstJours);
                    cmd.Parameters.AddWithValue("@EstJoursNume", EstJoursNume);
                    cmd.Parameters.AddWithValue("@CreePar", CreePar);
                    cmd.Parameters.AddWithValue("@ModifiePar", ModifiePar);
                    cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                    cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                    cmd.Parameters.AddWithValue("@PCInsertion", PCInsertion);
                    cmd.Parameters.AddWithValue("@PCModification", this.PCModification);
                    cmd.Parameters.AddWithValue("@NBonCommande", this.NBonCommande);
                    cmd.Parameters.AddWithValue("@CEquipe", this.CEquipe);
                    cmd.Parameters.AddWithValue("@NOrdredeTravail", this.NOrdredeTravail);
                    cmd.Parameters.AddWithValue("@BValid", this.BValid);
                    //cmd.Parameters.AddWithValue("@SignatureClient", this.SignatureClient);
                    cmd.Parameters.AddWithValue("@CEtablissement", this.CEtablissement);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            this.NRattachement = dr["NRattachement"].ToString();
                            this.Indice = int.Parse(dr["DernierIndice"].ToString());
                        }
                    }
                    this.SupprimerRattachementChargesDiverssAnterieurs(transaction);
                    this.SupprimerRattachementArticlesAnterieurs(transaction);
                   // this.SupprimerRattachementEmployesAnterieurs(transaction);
                   // this.SupprimerRattachementTachessAnterieurs(transaction);


                    foreach (RattachementChargesDivers rattachementChargesDivers in RattachementChargesDiverss)
                    {
                        rattachementChargesDivers.NRattachement = this.NRattachement;
                        rattachementChargesDivers.Sauvegarder(transaction);
                    }
                    foreach (RattachementArticle rattachementArticle in RattachementArticles)
                    {
                        rattachementArticle.NRattachement = this.NRattachement;
                        rattachementArticle.Sauvegarder(transaction);
                    }
                    //foreach (RattachementEmploye rattachementEmploye in RattachementEmployes)
                    //{
                    //    rattachementEmploye.Sauvegarder(transaction);
                    //}
                    //foreach (RattachementTaches rattachementTaches in RattachementTachess)
                    //{
                    //    rattachementTaches.NRattachement = this.NRattachement;

                    //    rattachementTaches.Sauvegarder(transaction);
                    //}
                    //foreach (InterventionVehicule interventionVehicule in InterventionVehicules)
                    //{
                    //    interventionVehicule.Sauvegarder(transaction);
                    //}
                  
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public void Modifier(string n)
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
                    cmd.CommandText = "GP_Rattachement_Modifier";

                    cmd.Parameters.AddWithValue("@NRattachement", n);
                    cmd.Parameters.AddWithValue("@DateRattachement", DateRattachement);
                    cmd.Parameters.AddWithValue("@NChantier", NChantier);
                    cmd.Parameters.AddWithValue("@CClient", CClient);
                    cmd.Parameters.AddWithValue("@Cout", Cout);
                  
                    cmd.Parameters.AddWithValue("@Nature", Nature);
                    cmd.Parameters.AddWithValue("@Responsable", Responsable);
                    cmd.Parameters.AddWithValue("@HeureDebut", HeureDebut);
                    cmd.Parameters.AddWithValue("@HeureFin", HeureFin);
                    cmd.Parameters.AddWithValue("@Emplacement", Emplacement);
                    cmd.Parameters.AddWithValue("@Reference", Reference);
                    cmd.Parameters.AddWithValue("@NBonLivraison", NBonLivraison);
                    cmd.Parameters.AddWithValue("@NFacture", NFacture);
                    cmd.Parameters.AddWithValue("@RaisonSociale", RaisonSociale);
                    cmd.Parameters.AddWithValue("@TypeRattachement", TypeRattachement);
                    cmd.Parameters.AddWithValue("@Cloture", Cloture);
                    cmd.Parameters.AddWithValue("@DateCloture", DateCloture);
                    cmd.Parameters.AddWithValue("@CoutRH", CoutRH);
                    cmd.Parameters.AddWithValue("@CoutAR", CoutAR);
                    cmd.Parameters.AddWithValue("@CoutCD", CoutCD);
                    cmd.Parameters.AddWithValue("@CoutDevis", CoutDevis);
                    cmd.Parameters.AddWithValue("@EmplacementBC", EmplacementBC);
                    cmd.Parameters.AddWithValue("@NBC", NBC);
                    cmd.Parameters.AddWithValue("@DateBC", DateBC);
                    cmd.Parameters.AddWithValue("@MtHtBC", MtHtBC);
                    cmd.Parameters.AddWithValue("@MtFact", MtFact);
                    cmd.Parameters.AddWithValue("@Remarque", Remarque);
                    cmd.Parameters.AddWithValue("@EstJours", EstJours);
                    cmd.Parameters.AddWithValue("@EstJoursNume", EstJoursNume);
                 
                    cmd.Parameters.AddWithValue("@ModifiePar", ModifiePar);
                   
                    cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                    
                    cmd.Parameters.AddWithValue("@PCModification", this.PCModification);
                    cmd.Parameters.AddWithValue("@NBonCommande", this.NBonCommande);
                    cmd.Parameters.AddWithValue("@CEquipe", this.CEquipe);
                    cmd.Parameters.AddWithValue("@NOrdredeTravail", this.NOrdredeTravail);
                    cmd.Parameters.AddWithValue("@BValid", this.BValid);
                    //cmd.Parameters.AddWithValue("@SignatureClient", this.SignatureClient);
                    cmd.Parameters.AddWithValue("@CEtablissement", this.CEtablissement);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            this.NRattachement = dr["NRattachement"].ToString();
                            this.Indice = int.Parse(dr["DernierIndice"].ToString());
                        }
                    }
                    this.SupprimerRattachementChargesDiverssAnterieurs(transaction);
                    this.SupprimerRattachementArticlesAnterieurs(transaction);
                    // this.SupprimerRattachementEmployesAnterieurs(transaction);
                    this.SupprimerRattachementTachessAnterieurs(transaction);


                    foreach (RattachementChargesDivers rattachementChargesDivers in RattachementChargesDiverss)
                    {
                        rattachementChargesDivers.NRattachement = n;
                        rattachementChargesDivers.Sauvegarder(transaction);
                    }
                    foreach (RattachementArticle rattachementArticle in RattachementArticles)
                    {
                        rattachementArticle.NRattachement = n;
                        rattachementArticle.Sauvegarder(transaction);
                    }
                    //foreach (RattachementEmploye rattachementEmploye in RattachementEmployes)
                    //{
                    //    rattachementEmploye.Sauvegarder(transaction);
                    //}
                    //foreach (RattachementTaches rattachementTaches in RattachementTachess)
                    //{
                    //    rattachementTaches.NRattachement = this.NRattachement;

                    //    rattachementTaches.Sauvegarder(transaction);
                    //}
                    //foreach (InterventionVehicule interventionVehicule in InterventionVehicules)
                    //{
                    //    interventionVehicule.Sauvegarder(transaction);
                    //}

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

     

        //private void SupprimerRattachementEmployesAnterieurs(SqlTransaction transaction)
        //{
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Transaction = transaction;
        //        cmd.Connection = transaction.Connection;
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = "GI_Intervention_SupprimerEmployesDetails";

        //        cmd.Parameters.AddWithValue("@NRattachement", this.NRattachement);

        //        foreach (SqlParameter parametre in cmd.Parameters)
        //            if (parametre.Value == null)
        //                parametre.Value = DBNull.Value;

        //        cmd.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        private void SupprimerRattachementChargesDiverssAnterieurs(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GP_Rattachement_SupprimerchargesDiversDetails";

                cmd.Parameters.AddWithValue("@NRattachement", this.NRattachement);

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
        private void SupprimerRattachementArticlesAnterieurs(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GP_Rattachement_SupprimerArticlesDetails";

                cmd.Parameters.AddWithValue("@NRattachement", this.NRattachement);

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
        private void SupprimerRattachementTachessAnterieurs(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GP_Rattachement_SupprimerTachesDetails";

                cmd.Parameters.AddWithValue("@NRattachement", this.NRattachement);

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

       

        public static Rattachement Charger(string NRattachement)
        {
            Rattachement rattachement = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "GP_Rattachement_Charger";
                    cmd.Parameters.AddWithValue("@NRattachement", NRattachement);
                  
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            rattachement = new Rattachement();

                            rattachement.NRattachement = dr["NRattachement"].ToString();

                            if (dr["DateRattachement"] != DBNull.Value)
                                rattachement.DateRattachement = DateTime.Parse(dr["DateRattachement"].ToString());
                            if (dr["NChantier"] != DBNull.Value)
                                rattachement.NChantier = dr["NChantier"].ToString();
                            if (dr["CClient"] != DBNull.Value)
                                rattachement.CClient = dr["CClient"].ToString();
                            if (dr["Cout"] != DBNull.Value)
                                rattachement.Cout = decimal.Parse(dr["Cout"].ToString());
                         
                          
                            if (dr["Nature"] != DBNull.Value)
                                rattachement.Nature = dr["Nature"].ToString();
                            if (dr["Responsable"] != DBNull.Value)
                                rattachement.Responsable = dr["Responsable"].ToString();
                            if (dr["HeureDebut"] != DBNull.Value)
                                rattachement.HeureDebut = dr["HeureDebut"].ToString();
                            if (dr["HeureFin"] != DBNull.Value)
                                rattachement.HeureFin = dr["HeureFin"].ToString();
                            if (dr["Emplacement"] != DBNull.Value)
                                rattachement.Emplacement = dr["Emplacement"].ToString();
                            if (dr["Reference"] != DBNull.Value)
                                rattachement.Reference = dr["Reference"].ToString();
                            if (dr["NBonLivraison"] != DBNull.Value)
                                rattachement.NBonLivraison = dr["NBonLivraison"].ToString();
                            if (dr["NFacture"] != DBNull.Value)
                                rattachement.NFacture = dr["NFacture"].ToString();
                            if (dr["RaisonSociale"] != DBNull.Value)
                                rattachement.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["TypeRattachement"] != DBNull.Value)
                                rattachement.TypeRattachement = dr["TypeRattachement"].ToString();
                            if (dr["Cloture"] != DBNull.Value)
                                rattachement.Cloture = dr["Cloture"].ToString();

                            if (dr["DateCloture"] != DBNull.Value)
                                rattachement.DateCloture = DateTime.Parse(dr["DateCloture"].ToString());
                            if (dr["CoutRH"] != DBNull.Value)
                                rattachement.CoutRH = decimal.Parse(dr["CoutRH"].ToString());
                            if (dr["CoutAR"] != DBNull.Value)
                                rattachement.CoutAR = decimal.Parse(dr["CoutAR"].ToString());
                            if (dr["CoutCD"] != DBNull.Value)
                                rattachement.CoutCD = decimal.Parse(dr["CoutCD"].ToString());
                            if (dr["CoutDevis"] != DBNull.Value)
                                rattachement.CoutDevis = decimal.Parse(dr["CoutDevis"].ToString());

                            if (dr["EmplacementBC"] != DBNull.Value)
                                rattachement.EmplacementBC = dr["EmplacementBC"].ToString();
                            if (dr["NBC"] != DBNull.Value)
                                rattachement.NBC = dr["NBC"].ToString();
                            if (dr["DateBC"] != DBNull.Value)
                                rattachement.DateBC = dr["DateBC"].ToString();
                            if (dr["MtHtBC"] != DBNull.Value)
                                rattachement.MtHtBC = decimal.Parse(dr["MtHtBC"].ToString());
                            if (dr["MtFact"] != DBNull.Value)
                                rattachement.MtFact = decimal.Parse(dr["MtFact"].ToString());
                            if (dr["Remarque"] != DBNull.Value)
                                rattachement.Remarque = dr["Remarque"].ToString();
                            if (dr["EstJours"] != DBNull.Value)
                                rattachement.EstJours = dr["EstJours"].ToString();
                            if (dr["EstJoursNume"] != DBNull.Value)
                                rattachement.EstJoursNume = decimal.Parse(dr["EstJoursNume"].ToString());
                            if (dr["NBonCommande"] != DBNull.Value)
                                rattachement.NBonCommande = dr["NBonCommande"].ToString();
                            if (dr["CEquipe"] != DBNull.Value)
                                rattachement.CEquipe = dr["CEquipe"].ToString();

                            if (dr["NOrdredeTravail"] != DBNull.Value)
                                rattachement.NOrdredeTravail = dr["NOrdredeTravail"].ToString();


                            if (dr["BValid"] != DBNull.Value)
                                rattachement.BValid =bool.Parse(dr["BValid"].ToString());
                            if (dr["SignatureClient"] != DBNull.Value)
                                rattachement.SignatureClient = dr["SignatureClient"].ToString();
                            if (dr["CEtablissement"] != DBNull.Value)
                                rattachement.CEtablissement = dr["CEtablissement"].ToString();


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
                    cmd.CommandText = "GP_Rattachement_Supprimer";
                    cmd.Parameters.AddWithValue("@NRattachement", NRattachement);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    cmd.ExecuteNonQuery();
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
                cmd.CommandText = "GP_Rattachement_Supprimer";
                cmd.Parameters.AddWithValue("@NRattachement", NRattachement);
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

        public static byte[] Base64ToImage(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            return imageBytes;
            //MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            //ms.Write(imageBytes, 0, imageBytes.Length);
            //System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            //return image;
        }

    }



    public class RattachementCollection : List<Rattachement>
    {

        public static RattachementCollection Charger()
        {
            RattachementCollection collection = new RattachementCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "GP_Rattachement_Charger";
                    cmd.Parameters.AddWithValue("@NRattachement", DBNull.Value);
                   
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Rattachement rattachement = new Rattachement();

                            rattachement.NRattachement = dr["NRattachement"].ToString();

                            if (dr["DateRattachement"] != DBNull.Value)
                                rattachement.DateRattachement = DateTime.Parse(dr["DateRattachement"].ToString());
                            if (dr["NChantier"] != DBNull.Value)
                                rattachement.NChantier = dr["NChantier"].ToString();
                            if (dr["CClient"] != DBNull.Value)
                                rattachement.CClient = dr["CClient"].ToString();
                            if (dr["Cout"] != DBNull.Value)
                                rattachement.Cout = decimal.Parse(dr["Cout"].ToString());
                            
                            if (dr["Type"] != DBNull.Value)
                                rattachement.Type = dr["Type"].ToString();
                            if (dr["Nature"] != DBNull.Value)
                                rattachement.Nature = dr["Nature"].ToString();
                            if (dr["Responsable"] != DBNull.Value)
                                rattachement.Responsable = dr["Responsable"].ToString();
                            if (dr["HeureDebut"] != DBNull.Value)
                                rattachement.HeureDebut = dr["HeureDebut"].ToString();
                            if (dr["HeureFin"] != DBNull.Value)
                                rattachement.HeureFin = dr["HeureFin"].ToString();
                            if (dr["Emplacement"] != DBNull.Value)
                                rattachement.Emplacement = dr["Emplacement"].ToString();
                            if (dr["Reference"] != DBNull.Value)
                                rattachement.Reference = dr["Reference"].ToString();
                            if (dr["NBonLivraison"] != DBNull.Value)
                                rattachement.NBonLivraison = dr["NBonLivraison"].ToString();
                            if (dr["NFacture"] != DBNull.Value)
                                rattachement.NFacture = dr["NFacture"].ToString();
                            if (dr["RaisonSociale"] != DBNull.Value)
                                rattachement.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["TypeRattachement"] != DBNull.Value)
                                rattachement.TypeRattachement = dr["TypeRattachement"].ToString();
                            if (dr["Cloture"] != DBNull.Value)
                                rattachement.Cloture = dr["Cloture"].ToString();

                            if (dr["DateCloture"] != DBNull.Value)
                                rattachement.DateCloture = DateTime.Parse(dr["DateCloture"].ToString());
                            if (dr["CoutRH"] != DBNull.Value)
                                rattachement.CoutRH = decimal.Parse(dr["CoutRH"].ToString());
                            if (dr["CoutAR"] != DBNull.Value)
                                rattachement.CoutAR = decimal.Parse(dr["CoutAR"].ToString());
                            if (dr["CoutCD"] != DBNull.Value)
                                rattachement.CoutCD = decimal.Parse(dr["CoutCD"].ToString());
                            if (dr["CoutDevis"] != DBNull.Value)
                                rattachement.CoutDevis = decimal.Parse(dr["CoutDevis"].ToString());

                            if (dr["EmplacementBC"] != DBNull.Value)
                                rattachement.EmplacementBC = dr["EmplacementBC"].ToString();
                            if (dr["NBC"] != DBNull.Value)
                                rattachement.NBC = dr["NBC"].ToString();
                            if (dr["DateBC"] != DBNull.Value)
                                rattachement.DateBC = dr["DateBC"].ToString();
                            if (dr["MtHtBC"] != DBNull.Value)
                                rattachement.MtHtBC = decimal.Parse(dr["MtHtBC"].ToString());
                            if (dr["MtFact"] != DBNull.Value)
                                rattachement.MtFact = decimal.Parse(dr["MtFact"].ToString());
                            if (dr["Remarque"] != DBNull.Value)
                                rattachement.Remarque = dr["Remarque"].ToString();
                            if (dr["EstJours"] != DBNull.Value)
                                rattachement.EstJours = dr["EstJours"].ToString();
                            if (dr["EstJoursNume"] != DBNull.Value)
                                rattachement.EstJoursNume = decimal.Parse(dr["EstJoursNume"].ToString());
                            if (dr["NBonCommande"] != DBNull.Value)
                                rattachement.NBonCommande = dr["NBonCommande"].ToString();
                            if (dr["CEquipe"] != DBNull.Value)
                                rattachement.CEquipe = dr["CEquipe"].ToString();

                            if (dr["NOrdredeTravail"] != DBNull.Value)
                                rattachement.NOrdredeTravail = dr["NOrdredeTravail"].ToString();
                            if (dr["BValid"] != DBNull.Value)
                                rattachement.BValid = bool.Parse(dr["BValid"].ToString());
                            if (dr["SignatureClient"] != DBNull.Value)
                                rattachement.SignatureClient = dr["SignatureClient"].ToString();
                            if (dr["CEtablissement"] != DBNull.Value)
                                rattachement.CEtablissement = dr["CEtablissement"].ToString();
                            collection.Add(rattachement);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return collection;
        }
        public static RattachementCollection ChargerparOT(string NOrdredeTravail)
        {
            RattachementCollection collection = new RattachementCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "GP_Rattachement_ChargerparOT";
                    cmd.Parameters.AddWithValue("@NOrdredeTravail", NOrdredeTravail);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Rattachement rattachement = new Rattachement();

                            rattachement.NRattachement = dr["NRattachement"].ToString();

                            if (dr["DateRattachement"] != DBNull.Value)
                                rattachement.DateRattachement = DateTime.Parse(dr["DateRattachement"].ToString());
                            if (dr["NChantier"] != DBNull.Value)
                                rattachement.NChantier = dr["NChantier"].ToString();
                            if (dr["CClient"] != DBNull.Value)
                                rattachement.CClient = dr["CClient"].ToString();
                            if (dr["Cout"] != DBNull.Value)
                                rattachement.Cout = decimal.Parse(dr["Cout"].ToString());

                           
                            if (dr["Nature"] != DBNull.Value)
                                rattachement.Nature = dr["Nature"].ToString();
                            if (dr["Responsable"] != DBNull.Value)
                                rattachement.Responsable = dr["Responsable"].ToString();
                            if (dr["HeureDebut"] != DBNull.Value)
                                rattachement.HeureDebut = dr["HeureDebut"].ToString();
                            if (dr["HeureFin"] != DBNull.Value)
                                rattachement.HeureFin = dr["HeureFin"].ToString();
                            if (dr["Emplacement"] != DBNull.Value)
                                rattachement.Emplacement = dr["Emplacement"].ToString();
                            if (dr["Reference"] != DBNull.Value)
                                rattachement.Reference = dr["Reference"].ToString();
                            if (dr["NBonLivraison"] != DBNull.Value)
                                rattachement.NBonLivraison = dr["NBonLivraison"].ToString();
                            if (dr["NFacture"] != DBNull.Value)
                                rattachement.NFacture = dr["NFacture"].ToString();
                            if (dr["RaisonSociale"] != DBNull.Value)
                                rattachement.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["TypeRattachement"] != DBNull.Value)
                                rattachement.TypeRattachement = dr["TypeRattachement"].ToString();
                            if (dr["Cloture"] != DBNull.Value)
                                rattachement.Cloture = dr["Cloture"].ToString();

                            if (dr["DateCloture"] != DBNull.Value)
                                rattachement.DateCloture = DateTime.Parse(dr["DateCloture"].ToString());
                            if (dr["CoutRH"] != DBNull.Value)
                                rattachement.CoutRH = decimal.Parse(dr["CoutRH"].ToString());
                            if (dr["CoutAR"] != DBNull.Value)
                                rattachement.CoutAR = decimal.Parse(dr["CoutAR"].ToString());
                            if (dr["CoutCD"] != DBNull.Value)
                                rattachement.CoutCD = decimal.Parse(dr["CoutCD"].ToString());
                            if (dr["CoutDevis"] != DBNull.Value)
                                rattachement.CoutDevis = decimal.Parse(dr["CoutDevis"].ToString());

                            if (dr["EmplacementBC"] != DBNull.Value)
                                rattachement.EmplacementBC = dr["EmplacementBC"].ToString();
                            if (dr["NBC"] != DBNull.Value)
                                rattachement.NBC = dr["NBC"].ToString();
                            if (dr["DateBC"] != DBNull.Value)
                                rattachement.DateBC = dr["DateBC"].ToString();
                            if (dr["MtHtBC"] != DBNull.Value)
                                rattachement.MtHtBC = decimal.Parse(dr["MtHtBC"].ToString());
                            if (dr["MtFact"] != DBNull.Value)
                                rattachement.MtFact = decimal.Parse(dr["MtFact"].ToString());
                            if (dr["Remarque"] != DBNull.Value)
                                rattachement.Remarque = dr["Remarque"].ToString();
                            if (dr["EstJours"] != DBNull.Value)
                                rattachement.EstJours = dr["EstJours"].ToString();
                            if (dr["EstJoursNume"] != DBNull.Value)
                                rattachement.EstJoursNume = decimal.Parse(dr["EstJoursNume"].ToString());
                            if (dr["NBonCommande"] != DBNull.Value)
                                rattachement.NBonCommande = dr["NBonCommande"].ToString();
                            if (dr["CEquipe"] != DBNull.Value)
                                rattachement.CEquipe = dr["CEquipe"].ToString();

                            if (dr["NOrdredeTravail"] != DBNull.Value)
                                rattachement.NOrdredeTravail = dr["NOrdredeTravail"].ToString();
                            if (dr["BValid"] != DBNull.Value)
                                rattachement.BValid = bool.Parse(dr["BValid"].ToString());
                            //if (dr["SignatureClient"] != DBNull.Value)
                            //    rattachement.SignatureClient = (byte[])dr["SignatureClient"];
                            if (dr["CEtablissement"] != DBNull.Value)
                                rattachement.CEtablissement = dr["CEtablissement"].ToString();
                            collection.Add(rattachement);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return collection;
        }
    }


}
