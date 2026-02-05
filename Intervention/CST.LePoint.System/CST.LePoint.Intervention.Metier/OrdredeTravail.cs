using CST.LePoint.Referentiel;
using CST.LePoint.Vente.Metier;
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
    [Serializable]
    public class OrdredeTravailCollection : ItemCollection
    {
        public static OrdredeTravailCollection Charger(DateTime date)
        {
            OrdredeTravailCollection collection = new OrdredeTravailCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "GP_OrdredeTravail_Charger";
                    cmd.Parameters.AddWithValue("@NOrdredeTravail", DBNull.Value);
                    cmd.Parameters.AddWithValue("@date", date);
                   // cmd.Parameters.AddWithValue("@NOrdredeTravail", DBNull.Value);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            OrdredeTravail ordredeTravail = new OrdredeTravail();

                            ordredeTravail.NOrdredeTravail = dr["NOrdredeTravail"].ToString();
                            ordredeTravail.Libelle = dr["RaisonSociale"].ToString();

                            ordredeTravail.Nchantier = dr["Nchantier"].ToString();
                            ordredeTravail.CClient = dr["CClient"].ToString();
                            ordredeTravail.NBonCommande = dr["NBonCommande"].ToString();
                            ordredeTravail.CEquipe = dr["CSousTraitant"].ToString();
                            ordredeTravail.EtatOT = dr["EtatOT"].ToString();
                            ordredeTravail.Montant = decimal.Parse(dr["Montant"].ToString());
                            if (dr["DateCreation"] != DBNull.Value)
                                ordredeTravail.DateCreation = DateTime.Parse(dr["DateCreation"].ToString());                         
                            if (dr["NConvention"] != DBNull.Value)
                                ordredeTravail.NConvention = dr["NConvention"].ToString();
                            if (dr["CVehicule"] != DBNull.Value)
                                ordredeTravail.CVehicule = dr["CVehicule"].ToString();
                            collection.Add(ordredeTravail);
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
        
        public static OrdredeTravailCollection ChargerparEquipe(string cEquipe,string nBoncommande,string nchantier,string cclient)
        {
            OrdredeTravailCollection collection = new OrdredeTravailCollection();
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
                    cmd.CommandText = "GP_OrdredeTravail_ChargerparST";
                    cmd.Parameters.AddWithValue("@CEquipe", cEquipe);
                    cmd.Parameters.AddWithValue("@NBoncommande", nBoncommande);
                    cmd.Parameters.AddWithValue("@Nchantier", nchantier);
                    cmd.Parameters.AddWithValue("@CClient", cclient);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            OrdredeTravail ordredeTravail = new OrdredeTravail();

                            ordredeTravail.Code = dr["NOrdredeTravail"].ToString();
                            ordredeTravail.Nchantier = dr["Nchantier"].ToString();
                            ordredeTravail.CClient = dr["CClient"].ToString();
                            ordredeTravail.NBonCommande = dr["NBonCommande"].ToString();
                            ordredeTravail.CEquipe = dr["CSousTraitant"].ToString();
                            ordredeTravail.EtatOT = dr["EtatOT"].ToString();
                            if (dr["Montant"] != DBNull.Value)
                                ordredeTravail.Montant = decimal.Parse(dr["Montant"].ToString());
                            if (dr["DateCreation"] != DBNull.Value)
                                ordredeTravail.DateCreation = DateTime.Parse(dr["DateCreation"].ToString());
                            if (dr["RaisonSociale"] != DBNull.Value)
                                ordredeTravail.Libelle = dr["RaisonSociale"].ToString();
                            if (dr["MontantBL"] != DBNull.Value)
                             ordredeTravail.MontantBL = decimal.Parse(dr["MontantBL"].ToString());
                            if (dr["MontantGlobalArticle"] != DBNull.Value)
                                ordredeTravail.MontantGlobalArticle = decimal.Parse(dr["MontantGlobalArticle"].ToString());
                            if (dr["MontantGlobalChargesD"] != DBNull.Value)
                                ordredeTravail.MontantGlobalChargesD = decimal.Parse(dr["MontantGlobalChargesD"].ToString());
                            if (dr["Solde"] != DBNull.Value)
                                ordredeTravail.Solde = decimal.Parse(dr["Solde"].ToString());
                            if (dr["NConvention"] != DBNull.Value)
                                ordredeTravail.NConvention = dr["NConvention"].ToString();
                            if (dr["CVehicule"] != DBNull.Value)
                                ordredeTravail.CVehicule = dr["CVehicule"].ToString();
             
                            collection.Add(ordredeTravail);
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

        public static OrdredeTravailCollection ChargerparClient(string cclient,DateTime date)
        {
            OrdredeTravailCollection collection = new OrdredeTravailCollection();
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
                    cmd.CommandText = "GP_OrdredeTravail_ChargerparClient";
                    
                    cmd.Parameters.AddWithValue("@CClient", cclient);
                    cmd.Parameters.AddWithValue("@date", date);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            OrdredeTravail ordredeTravail = new OrdredeTravail();

                            ordredeTravail.NOrdredeTravail = dr["NOrdredeTravail"].ToString();
                            ordredeTravail.Nchantier = dr["Nchantier"].ToString();
                            ordredeTravail.CClient = dr["CClient"].ToString();
                            ordredeTravail.NBonCommande = dr["NBonCommande"].ToString();
                            ordredeTravail.CEquipe = dr["CSousTraitant"].ToString();
                            ordredeTravail.EtatOT = dr["EtatOT"].ToString();
                            if (dr["Montant"] != DBNull.Value)
                                ordredeTravail.Montant = decimal.Parse(dr["Montant"].ToString());
                            if (dr["DateCreation"] != DBNull.Value)
                                ordredeTravail.DateCreation = DateTime.Parse(dr["DateCreation"].ToString());
                            if (dr["RaisonSociale"] != DBNull.Value)
                                ordredeTravail.Libelle = dr["RaisonSociale"].ToString();
                            if (dr["MontantBL"] != DBNull.Value)
                                ordredeTravail.MontantBL = decimal.Parse(dr["MontantBL"].ToString());

                            if (dr["MontantGlobalArticle"] != DBNull.Value)
                                ordredeTravail.MontantGlobalArticle = decimal.Parse(dr["MontantGlobalArticle"].ToString());
                            if (dr["MontantGlobalChargesD"] != DBNull.Value)
                                ordredeTravail.MontantGlobalChargesD = decimal.Parse(dr["MontantGlobalChargesD"].ToString());
                            if (dr["Solde"] != DBNull.Value)
                                ordredeTravail.Solde = decimal.Parse(dr["Solde"].ToString());
                            if (dr["NConvention"] != DBNull.Value)
                                ordredeTravail.NConvention = dr["NConvention"].ToString();
                            if (dr["CVehicule"] != DBNull.Value)
                                ordredeTravail.CVehicule = dr["CVehicule"].ToString();


                            collection.Add(ordredeTravail);
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

        public static OrdredeTravailCollection ChargerparChantierClient(string cclient, DateTime date)
        {
            OrdredeTravailCollection collection = new OrdredeTravailCollection();
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
                    cmd.CommandText = "GP_OrdredeTravail_ChargerparChantierClient";

                    cmd.Parameters.AddWithValue("@Nchantier", DBNull.Value);
                    cmd.Parameters.AddWithValue("@CClient", cclient);
                   // cmd.Parameters.AddWithValue("@date", date);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            OrdredeTravail ordredeTravail = new OrdredeTravail();

                            ordredeTravail.NOrdredeTravail = dr["NOrdredeTravail"].ToString();
                            if (dr["Nchantier"] != DBNull.Value)
                            ordredeTravail.Nchantier = dr["Nchantier"].ToString();
                            ordredeTravail.CClient = dr["CClient"].ToString();
                            if (dr["NBonCommande"] != DBNull.Value)
                            ordredeTravail.NBonCommande = dr["NBonCommande"].ToString();
                            ordredeTravail.CEquipe = dr["CSousTraitant"].ToString();
                            ordredeTravail.EtatOT = dr["EtatOT"].ToString();
                            if (dr["Montant"] != DBNull.Value)
                                ordredeTravail.Montant = decimal.Parse(dr["Montant"].ToString());
                            if (dr["DateCreation"] != DBNull.Value)
                                ordredeTravail.DateCreation = DateTime.Parse(dr["DateCreation"].ToString());
                            if (dr["RaisonSociale"] != DBNull.Value)
                                ordredeTravail.Libelle = dr["RaisonSociale"].ToString();

                            if (dr["MontantBL"] != DBNull.Value)
                                ordredeTravail.MontantBL = decimal.Parse(dr["MontantBL"].ToString());

                            if (dr["MontantGlobalArticle"] != DBNull.Value)
                                ordredeTravail.MontantGlobalArticle = decimal.Parse(dr["MontantGlobalArticle"].ToString());
                            if (dr["MontantGlobalChargesD"] != DBNull.Value)
                                ordredeTravail.MontantGlobalChargesD = decimal.Parse(dr["MontantGlobalChargesD"].ToString());
                            if (dr["Solde"] != DBNull.Value)
                                ordredeTravail.Solde = decimal.Parse(dr["Solde"].ToString());
                            if (dr["NConvention"] != DBNull.Value)
                                ordredeTravail.NConvention = dr["NConvention"].ToString();
                            if (dr["CVehicule"] != DBNull.Value)
                                ordredeTravail.CVehicule = dr["CVehicule"].ToString();
                            collection.Add(ordredeTravail);
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

        public static OrdredeTravailCollection ChargerparOT(string NOrdredeTravail)
        {
            OrdredeTravailCollection collection = new OrdredeTravailCollection();
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GP_OrdredeTravail_ChargerparOT";
                cmd.Parameters.AddWithValue("@NOrdredeTravail", NOrdredeTravail);
               
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    OrdredeTravail ordredeTravail = new OrdredeTravail();

                    ordredeTravail.NOrdredeTravail = dr["NOrdredeTravail"].ToString();
                    ordredeTravail.Nchantier = dr["Nchantier"].ToString();
                    ordredeTravail.CClient = dr["CClient"].ToString();
                    ordredeTravail.NBonCommande = dr["NBonCommande"].ToString();
                    ordredeTravail.CEquipe = dr["CSousTraitant"].ToString();
                    ordredeTravail.EtatOT = dr["EtatOT"].ToString();
                    if (dr["Montant"] != DBNull.Value)
                        ordredeTravail.Montant = decimal.Parse(dr["Montant"].ToString());
                    if (dr["DateCreation"] != DBNull.Value)
                        ordredeTravail.DateCreation = DateTime.Parse(dr["DateCreation"].ToString());
                    if (dr["RaisonSociale"] != DBNull.Value)
                        ordredeTravail.Libelle = dr["RaisonSociale"].ToString();
                  
                    if (dr["MontantBL"] != DBNull.Value)
                        ordredeTravail.MontantBL = decimal.Parse(dr["MontantBL"].ToString());
                    if (dr["MontantGlobalArticle"] != DBNull.Value)
                        ordredeTravail.MontantGlobalArticle = decimal.Parse(dr["MontantGlobalArticle"].ToString());
                    if (dr["MontantGlobalChargesD"] != DBNull.Value)
                        ordredeTravail.MontantGlobalChargesD = decimal.Parse(dr["MontantGlobalChargesD"].ToString());
                    if (dr["Solde"] != DBNull.Value)
                        ordredeTravail.Solde = decimal.Parse(dr["Solde"].ToString());
                    if (dr["NConvention"] != DBNull.Value)
                        ordredeTravail.NConvention = dr["NConvention"].ToString();
                    if (dr["CVehicule"] != DBNull.Value)
                        ordredeTravail.CVehicule = dr["CVehicule"].ToString();
                    collection.Add(ordredeTravail);


                }
                dr.Close();

                return (collection);
            }
        }

        public static OrdredeTravailCollection ChargerpClient(string cclient)
        {
            OrdredeTravailCollection collection = new OrdredeTravailCollection();
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GP_OrdredeTravail_ChargerpClient";
                cmd.Parameters.AddWithValue("@CClient", cclient);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    OrdredeTravail ordredeTravail = new OrdredeTravail();

                    ordredeTravail.NOrdredeTravail = dr["NOrdredeTravail"].ToString();
                    ordredeTravail.Nchantier = dr["Nchantier"].ToString();
                    ordredeTravail.CClient = dr["CClient"].ToString();
                    ordredeTravail.NBonCommande = dr["NBonCommande"].ToString();
                    ordredeTravail.CEquipe = dr["CSousTraitant"].ToString();
                    ordredeTravail.EtatOT = dr["EtatOT"].ToString();
                    if (dr["Montant"] != DBNull.Value)
                        ordredeTravail.Montant = decimal.Parse(dr["Montant"].ToString());
                    if (dr["DateCreation"] != DBNull.Value)
                        ordredeTravail.DateCreation = DateTime.Parse(dr["DateCreation"].ToString());
                    if (dr["RaisonSociale"] != DBNull.Value)
                        ordredeTravail.Libelle = dr["RaisonSociale"].ToString();

                    if (dr["MontantBL"] != DBNull.Value)
                        ordredeTravail.MontantBL = decimal.Parse(dr["MontantBL"].ToString());
                    if (dr["MontantGlobalArticle"] != DBNull.Value)
                        ordredeTravail.MontantGlobalArticle = decimal.Parse(dr["MontantGlobalArticle"].ToString());
                    if (dr["MontantGlobalChargesD"] != DBNull.Value)
                        ordredeTravail.MontantGlobalChargesD = decimal.Parse(dr["MontantGlobalChargesD"].ToString());
                    if (dr["Solde"] != DBNull.Value)
                        ordredeTravail.Solde = decimal.Parse(dr["Solde"].ToString());
                    if (dr["NConvention"] != DBNull.Value)
                        ordredeTravail.NConvention = dr["NConvention"].ToString();
                    if (dr["CVehicule"] != DBNull.Value)
                        ordredeTravail.CVehicule = dr["CVehicule"].ToString();
                    collection.Add(ordredeTravail);


                }
                dr.Close();

                return (collection);
            }
        }
    }

    [Serializable]
    public class OrdredeTravail : Item
    {
        #region Propriétés

        public int Indice { get; set; }
        public string NOrdredeTravail { get; set; }
        public string Libelle { get; set; }
        public string Exercice { get; set; }
        public string Nchantier { get; set; }
        public string CClient { get; set; }
        public string NBonCommande { get; set; }
        public string CEquipe { get; set; }
        public string EtatOT { get; set; }
        public decimal Montant { get; set; }
        public DateTime? DateCreation { get; set; }
        public string CEtablissement { get; set; }
        public DateTime DateInsertion { get; set; }
        public DateTime DateModification { get; set; }
        public int CreePar { get; set; }
        public int ModifiePar { get; set; }
        public string PCInsertion { get; set; }
        public string PCModification { get; set; }
        public decimal MontantBL { get; set; }
        public decimal MontantGlobalChargesD { get; set; }
        public decimal MontantGlobalArticle { get; set; }
        public decimal Solde { get; set; }
        public string NConvention { get; set; }
        public DateTime DatePlanification { get; set; }
        public string CVehicule { get; set; }
        public bool BValid { get; set; }
        public bool BConfirmValid { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public int OrdreVisite { get; set; }
        public string Recommandation { get; set; }

        public OrdreTravailDetailCollection OrdreTravailDetails = new OrdreTravailDetailCollection();


        #endregion Propriétés

        public OrdredeTravail()
        {
            //this.BActif = true;
            //this.BDisponible = true;
        }
        
        public static void ModifierMontantGlobalChargesD(decimal p1, string p2)
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
                    cmd.CommandText = "update GP_OrdredeTravail set MontantGlobalChargesD = '" + p1 + "'   where NOrdredeTravail = '" + p2 + "' ";
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
        
        public static void ModifierMontantGlobalArticle(decimal p1, string p2)
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
                    cmd.CommandText = "update GP_OrdredeTravail set MontantGlobalArticle = '" + p1 + "'   where NOrdredeTravail = '" + p2 + "' ";
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
  
        //modifier date palnification et equipe
        public void ModifierEquipePlanification(string cequipe, string Nequipe, string convention, DateTime Dateplanold)
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
                    cmd.CommandText = "update ConventionClientPlanificationTechnicien set CTechnicien = '" + cequipe + "', NomTechnicien = '" + Nequipe + "'  where NConvention = '" + convention + "' AND DatePlanification =convert(datetime,'" + Dateplanold + "',103) ";
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

        public void ModifierDatePlanificationT(DateTime Dateplan, string convention, DateTime Dateplanold)
        {

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                //SqlTransaction transaction1 = cn.BeginTransaction();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Transaction = transaction;
                    cmd.Connection = transaction.Connection;
                    cmd.CommandType = CommandType.Text;
                    // decimal resultat = qté - fait;
                    cmd.CommandText = "update ConventionClientPlanificationTechnicien set DatePlanification =convert(datetime,'" + Dateplan + "',103) where NConvention = '" + convention + "' AND DatePlanification =convert(datetime,'" + Dateplanold + "',103) ";
                    cmd.ExecuteNonQuery();
                    transaction.Commit();

                    //SqlCommand cmd1 = new SqlCommand();
                    //cmd1.Transaction = transaction1;
                    //cmd1.Connection = transaction1.Connection;
                    //cmd1.CommandType = CommandType.Text;
                    //// decimal resultat = qté - fait;
                    //cmd1.CommandText = "update ConventionClientPlanification set DatePlanification = convert(datetime,'" + Dateplan + "',103) where NConvention = '" + convention + "' AND DatePlanification =convert(datetime,'" + Dateplanold + "',103) ";
                    //cmd1.ExecuteNonQuery();
                    //transaction1.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    //transaction1.Rollback();
                    throw ex;
                }
            }
        }

        public void ModifierDatePlanification(DateTime Dateplan, string convention, DateTime Dateplanold)
        {

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                //SqlTransaction transaction = cn.BeginTransaction();
                SqlTransaction transaction1 = cn.BeginTransaction();
                try
                {
                    //SqlCommand cmd = new SqlCommand();
                    //cmd.Transaction = transaction;
                    //cmd.Connection = transaction.Connection;
                    //cmd.CommandType = CommandType.Text;
                    //// decimal resultat = qté - fait;
                    //cmd.CommandText = "update ConventionClientPlanificationTechnicien set DatePlanification =convert(datetime,'" + Dateplan + "',103) where NConvention = '" + convention + "' AND DatePlanification =convert(datetime,'" + Dateplanold + "',103) ";
                    //cmd.ExecuteNonQuery();
                    //transaction.Commit();

                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.Transaction = transaction1;
                    cmd1.Connection = transaction1.Connection;
                    cmd1.CommandType = CommandType.Text;
                    // decimal resultat = qté - fait;
                    cmd1.CommandText = "update ConventionClientPlanification set DatePlanification = convert(datetime,'" + Dateplan + "',103) where NConvention = '" + convention + "' AND DatePlanification =convert(datetime,'" + Dateplanold + "',103) ";
                    cmd1.ExecuteNonQuery();
                    transaction1.Commit();
                }
                catch (Exception ex)
                {
                    //transaction.Rollback();
                    transaction1.Rollback();
                    throw ex;
                }
            }
        }
         
        public static void ModifierSolde(decimal p1, string p2)
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
                    cmd.CommandText = "update GP_OrdredeTravail set Solde = '" + p1 + "'   where NOrdredeTravail = '" + p2 + "' ";
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

        //valider l'ordrede travail en question
        public static void ModifierBValid(string ordredetravail)
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
                    cmd.CommandText = "update GP_OrdredeTravail set BValid = '" + "True" + "'   where NOrdredeTravail = '" + ordredetravail + "' ";
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

        public static void ModifierBConfirmValid(string ordredetravail)
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
                    cmd.CommandText = "update GP_OrdredeTravail set BConfirmValid = '" + "True" + "'   where NOrdredeTravail = '" + ordredetravail + "' ";
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

        public static void ModifierBConfirmValid(string ordredetravail, SqlTransaction transaction)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Transaction = transaction;
            cmd.Connection = transaction.Connection;
            cmd.CommandType = CommandType.Text;
            // decimal resultat = qté - fait;
            cmd.CommandText = "update GP_OrdredeTravail set BConfirmValid = '" + "True" + "'   where NOrdredeTravail = '" + ordredetravail + "' ";
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
                    cmd.CommandText = "GP_OrdredeTravail_Insererceg";
                    
                    cmd.Parameters.AddWithValue("@Libelle", this.Libelle);
                    cmd.Parameters.AddWithValue("@Exercice", this.Exercice);
                    cmd.Parameters.AddWithValue("@NChantier", this.Nchantier);
                    cmd.Parameters.AddWithValue("@CClient", this.CClient);
                    cmd.Parameters.AddWithValue("@NBonCommande", this.NBonCommande);
                    cmd.Parameters.AddWithValue("@CEquipe", this.CEquipe);
                    cmd.Parameters.AddWithValue("@EtatOT", this.EtatOT);
                    cmd.Parameters.AddWithValue("@Montant", this.Montant);
                    cmd.Parameters.AddWithValue("@DateCreation", this.DateCreation);
                    cmd.Parameters.AddWithValue("@CreePar", CreePar);
                    cmd.Parameters.AddWithValue("@ModifiePar", ModifiePar);
                    cmd.Parameters.AddWithValue("@PCInsertion", PCInsertion);
                    cmd.Parameters.AddWithValue("@PCModification", PCModification);
                    cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                    cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                   
                    cmd.Parameters.AddWithValue("@NConvention", this.NConvention);
                    cmd.Parameters.AddWithValue("@DatePlanification", this.DatePlanification);
                    cmd.Parameters.AddWithValue("@CVehicule", this.CVehicule);
                    cmd.Parameters.AddWithValue("@CEtablissement", this.CEtablissement);
                    if (Latitude == 0)
                        cmd.Parameters.AddWithValue("@Latitude", null);
                    else
                        cmd.Parameters.AddWithValue("@Latitude", Latitude);
                    if (Longitude == 0)
                        cmd.Parameters.AddWithValue("@Longitude", null);
                    else
                        cmd.Parameters.AddWithValue("@Longitude", Longitude);
                    cmd.Parameters.AddWithValue("@OrdreVisite", this.OrdreVisite);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        { this.NOrdredeTravail =dr["NOrdredeTravail"].ToString();
                           this.Indice = int.Parse(dr["DernierIndice"].ToString());
                        }
                    }
                    this.SupprimerOrdredeTravailDetailsAnterieurs(transaction, this.NOrdredeTravail);


                    foreach (OrdreTravailDetail ordreTravailDetail in OrdreTravailDetails)
                    {
                        ordreTravailDetail.NOrdredeTravail = this.NOrdredeTravail;
                        ordreTravailDetail.Sauvegarder(transaction);
                    }
                    
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
                cmd.CommandText = "GP_OrdredeTravail_Enregistrer";
                cmd.Parameters.AddWithValue("@NOrdredeTravail", this.NOrdredeTravail);
                cmd.Parameters.AddWithValue("@Libelle", this.Libelle);
                cmd.Parameters.AddWithValue("@Exercice", this.Exercice);
                cmd.Parameters.AddWithValue("@NChantier", this.Nchantier);
                cmd.Parameters.AddWithValue("@CClient", this.CClient);
                cmd.Parameters.AddWithValue("@NBonCommande", this.NBonCommande);
                cmd.Parameters.AddWithValue("@CEquipe", this.CEquipe);
                cmd.Parameters.AddWithValue("@EtatOT", this.EtatOT);
                cmd.Parameters.AddWithValue("@Montant", this.Montant);
                cmd.Parameters.AddWithValue("@DateCreation", this.DateCreation);
                cmd.Parameters.AddWithValue("@CreePar", CreePar);
                cmd.Parameters.AddWithValue("@ModifiePar", ModifiePar);
                cmd.Parameters.AddWithValue("@PCInsertion", PCInsertion);
                cmd.Parameters.AddWithValue("@PCModification", PCModification);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                cmd.Parameters.AddWithValue("@NConvention", this.NConvention);
                cmd.Parameters.AddWithValue("@DatePlanification", this.DatePlanification);
                cmd.Parameters.AddWithValue("@CVehicule", this.CVehicule);
                cmd.Parameters.AddWithValue("@CEtablissement", this.CEtablissement);
                if (Latitude == 0)
                    cmd.Parameters.AddWithValue("@Latitude", null);
                else
                    cmd.Parameters.AddWithValue("@Latitude", Latitude);
                if (Longitude == 0)
                    cmd.Parameters.AddWithValue("@Longitude", null);
                else
                    cmd.Parameters.AddWithValue("@Longitude", Longitude);
                cmd.Parameters.AddWithValue("@OrdreVisite", this.OrdreVisite);
                cmd.Parameters.AddWithValue("@Recommandation", this.Recommandation);

                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        this.NOrdredeTravail = dr["NOrdredeTravail"].ToString();
                        this.Indice = int.Parse(dr["DernierIndice"].ToString());
                    }
                }

                this.SupprimerOrdredeTravailDetailsAnterieurs(transaction, this.NOrdredeTravail);

                foreach (OrdreTravailDetail ordreTravailDetail in OrdreTravailDetails)
                {
                    ordreTravailDetail.NOrdredeTravail = this.NOrdredeTravail;
                    ordreTravailDetail.Sauvegarder(transaction);
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
                    cmd.CommandText = "GP_OrdredeTravail_Modifier";
                    cmd.Parameters.AddWithValue("@NOrdredeTravail", n);
                    cmd.Parameters.AddWithValue("@Libelle", this.Libelle);
                   
                    cmd.Parameters.AddWithValue("@NChantier", this.Nchantier);
                    cmd.Parameters.AddWithValue("@CClient", this.CClient);
                    cmd.Parameters.AddWithValue("@NBonCommande", this.NBonCommande);
                    cmd.Parameters.AddWithValue("@CEquipe", this.CEquipe);
                    cmd.Parameters.AddWithValue("@EtatOT", this.EtatOT);
                    cmd.Parameters.AddWithValue("@Montant", this.Montant);
                    cmd.Parameters.AddWithValue("@DatePlanification", this.DatePlanification);

                    cmd.Parameters.AddWithValue("@ModifiePar", ModifiePar);
                    cmd.Parameters.AddWithValue("@PCModification", PCModification);
                    cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                    cmd.Parameters.AddWithValue("@MontantBL", this.MontantBL);
                    cmd.Parameters.AddWithValue("@MontantGlobalChargesD", this.MontantGlobalChargesD);
                    cmd.Parameters.AddWithValue("@MontantGlobalArticle", this.MontantGlobalArticle);
                    cmd.Parameters.AddWithValue("@Solde", this.Solde);
                    cmd.Parameters.AddWithValue("@NConvention", this.NConvention);
                    cmd.Parameters.AddWithValue("@CVehicule", this.CVehicule);
                    cmd.Parameters.AddWithValue("@CEtablissement", this.CEtablissement);
                    if (Latitude == 0)
                        cmd.Parameters.AddWithValue("@Latitude", null);
                    else
                        cmd.Parameters.AddWithValue("@Latitude", Latitude);
                    if (Longitude == 0)
                        cmd.Parameters.AddWithValue("@Longitude", null);
                    else
                        cmd.Parameters.AddWithValue("@Longitude", Longitude);

                  //  cmd.Parameters.AddWithValue("@DatePlanification", this.DatePlanification);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;
                  
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            this.NOrdredeTravail = dr["NOrdredeTravail"].ToString();
                            this.Indice = int.Parse(dr["DernierIndice"].ToString());
                        }
                    }



                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        private void SupprimerOrdredeTravailDetailsAnterieurs(SqlTransaction transaction,string n)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GP_SupprimerOrdredeTravailDetails";

                cmd.Parameters.AddWithValue("@NOrdredeTravail", n);

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
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Transaction = transaction;
                    cmd.Connection = transaction.Connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "GP_OrdredeTravail_Supprimer";
                    cmd.Parameters.AddWithValue("@NOrdredeTravail", this.NOrdredeTravail);

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
                cmd.CommandText = "GP_OrdredeTravail_Supprimer";
                cmd.Parameters.AddWithValue("@NOrdredeTravail", this.NOrdredeTravail);

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

        public static OrdredeTravail Chargertt(string NOrdredeTravail)
        {
            OrdredeTravail ordredeTravail = new OrdredeTravail(); ;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "GP_OrdredeTravail_Chargertout";
                    cmd.Parameters.AddWithValue("@NOrdredeTravail", NOrdredeTravail);
                    cmd.Parameters.AddWithValue("@date", DBNull.Value);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            ordredeTravail.NOrdredeTravail = dr["NOrdredeTravail"].ToString();

                            if (dr["RaisonSociale"] != DBNull.Value)
                                ordredeTravail.Libelle = dr["RaisonSociale"].ToString();
                            if (dr["Nchantier"] != DBNull.Value)
                                ordredeTravail.Nchantier = dr["Nchantier"].ToString();
                            ordredeTravail.CClient = dr["CClient"].ToString();
                            if (dr["NBonCommande"] != DBNull.Value)
                                ordredeTravail.NBonCommande = dr["NBonCommande"].ToString();
                            ordredeTravail.CEquipe = dr["CSousTraitant"].ToString();
                            if (dr["EtatOT"] != DBNull.Value)
                                ordredeTravail.EtatOT = dr["EtatOT"].ToString();

                            if (dr["Montant"] != DBNull.Value)
                                ordredeTravail.Montant = decimal.Parse(dr["Montant"].ToString());
                            if (dr["DateCreation"] != DBNull.Value)
                                ordredeTravail.DateCreation = DateTime.Parse(dr["DateCreation"].ToString());
                            if (dr["DatePlanification"] != DBNull.Value)
                                ordredeTravail.DatePlanification = DateTime.Parse(dr["DatePlanification"].ToString());
                            if (dr["NConvention"] != DBNull.Value)
                                ordredeTravail.NConvention = dr["NConvention"].ToString();
                            if (dr["CVehicule"] != DBNull.Value)
                                ordredeTravail.CVehicule = dr["CVehicule"].ToString();
                            if (dr["CEtablissement"] != DBNull.Value)
                                ordredeTravail.CEtablissement = dr["CEtablissement"].ToString();

                           
                        }

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return ordredeTravail;
        }
        public static OrdredeTravail Charger(string NOrdredeTravail)
        {
            OrdredeTravail ordredeTravail = new OrdredeTravail(); ;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "GP_OrdredeTravail_Charger";
                    cmd.Parameters.AddWithValue("@NOrdredeTravail", NOrdredeTravail);
                    cmd.Parameters.AddWithValue("@date", DBNull.Value);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                           
                            ordredeTravail.NOrdredeTravail = dr["NOrdredeTravail"].ToString();

                            if (dr["RaisonSociale"] != DBNull.Value)
                                ordredeTravail.Libelle = dr["RaisonSociale"].ToString();
                            if (dr["Nchantier"] != DBNull.Value)
                            ordredeTravail.Nchantier = dr["Nchantier"].ToString();
                            ordredeTravail.CClient = dr["CClient"].ToString();
                            if (dr["NBonCommande"] != DBNull.Value)
                            ordredeTravail.NBonCommande = dr["NBonCommande"].ToString();
                            ordredeTravail.CEquipe = dr["CSousTraitant"].ToString();
                            if (dr["EtatOT"] != DBNull.Value)
                            ordredeTravail.EtatOT = dr["EtatOT"].ToString();

                            if (dr["Montant"] != DBNull.Value)
                                ordredeTravail.Montant = decimal.Parse(dr["Montant"].ToString());
                            if (dr["DateCreation"] != DBNull.Value)
                                ordredeTravail.DateCreation = DateTime.Parse(dr["DateCreation"].ToString());
                            if (dr["DatePlanification"] != DBNull.Value)
                                ordredeTravail.DatePlanification = DateTime.Parse(dr["DatePlanification"].ToString());
                            if (dr["NConvention"] != DBNull.Value)
                                ordredeTravail.NConvention =dr["NConvention"].ToString();
                            if (dr["CVehicule"] != DBNull.Value)
                                ordredeTravail.CVehicule = dr["CVehicule"].ToString();
                            if (dr["BValid"] != DBNull.Value)
                                ordredeTravail.BValid =bool.Parse( dr["BValid"].ToString());
                            //if (dr["BConfirmValid"] != DBNull.Value)
                               // ordredeTravail.BConfirmValid = bool.Parse(dr["BConfirmValid"].ToString());
                            if (dr["CEtablissement"] != DBNull.Value)
                                ordredeTravail.CEtablissement = dr["CEtablissement"].ToString();
                        }
                        
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return ordredeTravail;
        }

        // pour le controle changer date ou equipe d'un planning deja affecte  à un OT 
        public static OrdredeTravail Chargerpost(string conv,DateTime dat,string equip)
        {
            OrdredeTravail ordredeTravail = new OrdredeTravail(); ;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "GP_OrdredeTravail_Chargerparconvdatequi";
                    cmd.Parameters.AddWithValue("@conv", conv);
                    cmd.Parameters.AddWithValue("@dat", dat);
                    cmd.Parameters.AddWithValue("@equip", equip);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            ordredeTravail.NOrdredeTravail = dr["NOrdredeTravail"].ToString();

                            if (dr["RaisonSociale"] != DBNull.Value)
                                ordredeTravail.Libelle = dr["RaisonSociale"].ToString();
                           
                            ordredeTravail.CClient = dr["CClient"].ToString();
                            
                            ordredeTravail.CEquipe = dr["CSousTraitant"].ToString();
                           

                            if (dr["Montant"] != DBNull.Value)
                                ordredeTravail.Montant = decimal.Parse(dr["Montant"].ToString());
                            if (dr["DateCreation"] != DBNull.Value)
                                ordredeTravail.DateCreation = DateTime.Parse(dr["DateCreation"].ToString());
                            if (dr["DatePlanification"] != DBNull.Value)
                                ordredeTravail.DatePlanification = DateTime.Parse(dr["DatePlanification"].ToString());
                            if (dr["NConvention"] != DBNull.Value)
                                ordredeTravail.NConvention = dr["NConvention"].ToString();
                            if (dr["CVehicule"] != DBNull.Value)
                                ordredeTravail.CVehicule = dr["CVehicule"].ToString();
                            if (dr["BValid"] != DBNull.Value)
                                ordredeTravail.BValid = bool.Parse(dr["BValid"].ToString());
                           // if (dr["BConfirmValid"] != DBNull.Value)
                                //ordredeTravail.BConfirmValid = bool.Parse(dr["BConfirmValid"].ToString());
                            if (dr["CEtablissement"] != DBNull.Value)
                                ordredeTravail.CEtablissement = dr["CEtablissement"].ToString();
                            if (dr["EtatOT"] != DBNull.Value)
                                ordredeTravail.EtatOT = dr["EtatOT"].ToString();
                        }

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return ordredeTravail;
        }
     
    }
}
