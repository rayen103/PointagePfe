using CST.LePoint.Referentiel;
using CST.LePoint.Tiers.Metier;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CST.LePoint.Vente.Metier
{
    public class VenteHelper
    {
        public static decimal POURCENTAGE_TAUX_FORFAITAIRE = decimal.Parse(ConfigurationManager.AppSettings["POURCENTAGE_TAUX_FORFAITAIRE"].ToString());
        public static decimal MONTANT_TIMBRE_FISCAL = decimal.Parse(ConfigurationManager.AppSettings["MONTANT_TIMBRE_FISCAL"].ToString());
        public static int DELAIE_ECHEANCE = int.Parse(ConfigurationManager.AppSettings["DELAIE_ECHEANCE"].ToString());
        public static string ARTICLE_DIVERS = ConfigurationManager.AppSettings["CODE_ARTICLE_DIVERS"].ToString();
        public static string ENTREPOT_PRODUCTION = ConfigurationManager.AppSettings["ENTREPOT_PRODUCTION"].ToString();
        public static string ENTREPOT_LIVRAISON = ConfigurationManager.AppSettings["ENTREPOT_LIVRAISON"].ToString();
        public static bool UTILISER_GAUTO_OV = bool.Parse(ConfigurationManager.AppSettings["UTILISER_GAUTO_OV"].ToString());

        public const string CODE_TAXE_FODEC = "FODEC";
        public const string CODE_TAXE_TIMBRE_FISCAL = "TIMFIS";
        public const string CODE_TAXE_FORFAITAIRE = "AV/VENTE";
        public const int FACTURE_SESSION = 1;
        public const int FACTURE_LOYER = 2;
        public static string NOMAPPLICATION = ConfigurationManager.AppSettings["NomApplication"].ToString()+".";
       
        public enum EtatBonCommande
        {
            ENATTENTE = 0,
            ENCOURS = 1,
            LIVRE = 2,
            PURGER = 3,
            ANNULER = 4,
            ENPREPARATION = 5,
            PREPARE = 6,
            VALIDE = 7,
        }

        public enum EtatReglement
        {
            REG = 0,// reglement réglé
            ECHU = 1, // Cas du chèque ou traite: a dépassé la date d'échéance
            NECHU = 2,// Cas du chèque ou traite: n'a pas encore dépassé la date d'échéance
            ANNULE = 3,// réglement annulé
            IMP = 4, // cas du chéque ou traite: s'il reste non réglé pendant 3 ou plus jours après la date d'échèance (date à fixer) il passe à l'état non payé
            ENATTENTE = 5,// cas d'un réglement non affecté
            ASSOCIE = 6,// associé à une facture
        }

        public enum TypeReglement
        {
            AVR = 0,//Avoir
            AVRAVC = 1,//Avoir_Avance
            ESP = 2,//Espece
            CHQ = 3,//Cheque
            TRT = 4,
            CB = 5,
            CHCR = 6,
            VRM = 7,
            AT = 8,
            NR = 9,
            RED = 10,//Redressement
            RS1_5 = 11,
            RS50 = 12,
            LT=13,
        }

        public enum EtatAvoir
        {
            AVR_CON = 0,//Consomme
            AVR_NCON = 1,//NonConsomme
            REMB = 2,//Rembourse
            NREMB = 3,//NonRembourse
        }

        public static ItemCollection ChargerEtatReglement()
        {
            ItemCollection collection = new ItemCollection();
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_EtatReglement_Charger";

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Item item = new Item();
                            item.Code = dr["CEtatReglement"].ToString().Trim();
                            item.Libelle = dr["LibEtatReglement"].ToString().Trim();
                            collection.Add(item);
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

        public static decimal ArrondiParDefaut(decimal montantTTC)
        {
            decimal arrondi = montantTTC;
            decimal difference = 0;
            string montant = montantTTC.ToString(".##0");
            int indice = montant.IndexOf('.');
            montant = montant.Substring(indice + 1);
            int apresVirgule = int.Parse(montant);
            int millime = apresVirgule % 10;
            if (millime > 0 && millime < 5)
                difference = 5 - millime;
            else
                if (millime > 5)
                    difference = 10 - millime;
            arrondi = montantTTC + difference / 1000;
            return arrondi;
        }

        public static decimal ArrondiParExces(decimal montantTTC)
        {
            decimal arrondi = montantTTC;
            return arrondi;
        }

        public static void ModifierSolde(string nFacture, DateTime? dateFact, string cClient, decimal soldeLivraison, decimal soldeFacture, decimal soldeRetour, decimal resteAvance, decimal resteAvoir, decimal soldeImpaye, SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Client_ModifierSolde";

                cmd.Parameters.AddWithValue("@CClient", cClient);
                cmd.Parameters.AddWithValue("@SoldeFacture", soldeFacture);
                cmd.Parameters.AddWithValue("@SoldeBonLivraison", soldeLivraison);
                cmd.Parameters.AddWithValue("@SoldeBonRetour", soldeRetour);
                cmd.Parameters.AddWithValue("@SoldeAvanceRestant", resteAvance);
                cmd.Parameters.AddWithValue("@SoldeAvoirRestant", resteAvoir);
                cmd.Parameters.AddWithValue("@SoldeImpaye", soldeImpaye);
                cmd.Parameters.AddWithValue("@NFacture", nFacture);
                cmd.Parameters.AddWithValue("@DateFact", dateFact);

                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;
                cmd.ExecuteNonQuery();
            }

            catch { throw; }
        }

        public static void ModifierClientDerniereFacture(string cClient, SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Client_ModifierDerniereFacture";

                cmd.Parameters.AddWithValue("@CClient", cClient);


                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;
                cmd.ExecuteNonQuery();
            }

            catch { throw; }
        }

        public static void PeriodeCredit(string cClient, int limiteObligation, int limiteInfo, ref string message)
        {
            DateTime errorDate = VenteHelper.DateLimite(limiteObligation);

            DateTime warningDate = VenteHelper.DateLimite(limiteInfo);

            bool depassementDelai = false;

            message = "Il est impossible de créer un BL ou une Facture pour ce client:";

            BonLivraisonCollection collectionBLNonFactureE = BonLivraisonCollection.BonLivraisonChargerControl(cClient, errorDate);
            if (collectionBLNonFactureE != null)
            {
                foreach (BonLivraison bonlivraisonCharge in collectionBLNonFactureE)
                    message = message + "- BL N°" + bonlivraisonCharge.NBonLivraison + "Le" + bonlivraisonCharge.DateLivraison + "non facturé à crédit a dépassé le délai d'obligation.";
                depassementDelai = true;
            }

            FactureCollection collectionFactureACreditE = FactureCollection.FactureChargerControl(cClient, errorDate);

            if (collectionFactureACreditE != null)
            {
                foreach (Facture factureCharge in collectionFactureACreditE)
                    message = message + "- Facture N°" + factureCharge.NFacture + "Le" + factureCharge.DateFacture + " à crédit a dépassé le délai d'obligation.";
                depassementDelai = true;
            }

            if (depassementDelai == true)
                return;
            message = string.Empty;

            BonLivraisonCollection collectionBLNonFactureW = BonLivraisonCollection.BonLivraisonChargerControl(cClient, warningDate);

            if (collectionBLNonFactureW != null)
            {
                foreach (BonLivraison bonlivraisonCharge in collectionBLNonFactureW)
                    message = "- BL N°" + bonlivraisonCharge.NBonLivraison + "Le" + bonlivraisonCharge.DateLivraison + "non facturé à crédit va dépasser le délai d'obligation.";
                depassementDelai = true;
            }

            FactureCollection factureCollection1 = FactureCollection.FactureChargerControl(cClient, warningDate);
            if (factureCollection1 != null)
            {
                foreach (Facture factureCharge in factureCollection1)
                    message = "- Facture N°" + factureCharge.NFacture + "Le" + factureCharge.DateFacture + " à crédit va dépasser le délai d'obligation.";
                depassementDelai = true;
            }

            if (depassementDelai == true)
                return;
        }

        public static DateTime DateLimite(int limite)
        {
            return (DateTime.Now.AddDays(-limite));
        }

        public static void ReglementImpaye(string cClient, ref string message)
        {
            ReglementCollection reglementCollection = new ReglementCollection();
            reglementCollection = ReglementCollection.reglementImpayer(cClient);

            if (reglementCollection != null)
            {
                foreach (Reglement reg in reglementCollection)
                    message = "- Paiement N° " + reg.CReglement + " Du : " + reg.DateEmission + " impayé.";
            }
        }

        public static void LimiteCreditMontants(string cClient, string message, decimal soldeClient, decimal mTTCFactureBL)
        {
            decimal soldeActuel;
            decimal PNE = 0;

            bool limiteCreditMontants = false;

            Client client = Client.Charger(cClient);

            if (client.MontantCreditMax == 0 & client.MontantCreditMin == 0)
            {
                limiteCreditMontants = false;
                return;
            }
            if (client.MontantCreditMax != 0)
            {
                try
                {
                    using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                    {
                        cn.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = cn;

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Reglement_Somme";
                        cmd.Parameters.AddWithValue("@CClient", cClient);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                PNE = decimal.Parse(dr["Montant"].ToString());
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }

                soldeActuel = PNE + mTTCFactureBL + soldeClient;

                if (soldeActuel > client.MontantCreditMax)
                {
                    limiteCreditMontants = true;
                    message = "- Dépassement du Crédit Maximal.";
                    return;
                }
            }
            if (client.MontantCreditMin != 0)
            {
                soldeActuel = soldeClient + mTTCFactureBL;
                if (soldeActuel > client.MontantCreditMin)
                {
                    limiteCreditMontants = true;
                    message = "- Dépassement de la limite du Crédit Minimal.";
                }
            }
        }
    }
}