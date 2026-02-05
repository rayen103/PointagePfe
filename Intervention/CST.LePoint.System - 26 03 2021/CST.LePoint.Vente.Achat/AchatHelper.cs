using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST.LePoint.Achat.Metier
{
    public class AchatHelper
    {

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
            LT = 13,
        }

        public enum EtatAvoir
        {
            AVR_CON = 0,//Consomme
            AVR_NCON = 1,//NonConsomme
            REMB = 2,//Rembourse
            NREMB = 3,//NonRembourse
        }

        public static void MiseAJourStockEnCommandeFnr(string cArticle, decimal quantite, SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "Achat_Article_AjusterStockEnCommandeFnr";
                cmd.Parameters.AddWithValue("@CArticle", cArticle);
                cmd.Parameters.AddWithValue("@Quantite", quantite);

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

        public static void MiseAJourBonCommandeQteHist(string nBonCommande, string cArticle, int ordre, decimal qte, SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "Achat_BonCommande_AjusterQteHist";
                cmd.Parameters.AddWithValue("@NBonCommande", nBonCommande);
                cmd.Parameters.AddWithValue("@CArticle", cArticle);
                cmd.Parameters.AddWithValue("@Quantite", qte);
                cmd.Parameters.AddWithValue("@Ordre", ordre);
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

        public static void MiseAJourBonReceptionQteHist(string cEntrepot,string nBonReception, string cArticle, int ordre,decimal qte, SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "Achat_BonReception_AjusterQteHist";
                cmd.Parameters.AddWithValue("@NBonReception", nBonReception);
                cmd.Parameters.AddWithValue("@CArticle", cArticle);
                cmd.Parameters.AddWithValue("@Quantite", qte);
                cmd.Parameters.AddWithValue("@Ordre", ordre);
                cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
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

        public static void MiseAJourSoldeFnr(string cFournisseur, decimal montantTTC, decimal montantAvance, decimal montantIMP, decimal montantAvoir, SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "Achat_Fournisseur_AjusterSoldeFacture";
                cmd.Parameters.AddWithValue("@CFournisseur", cFournisseur);
                cmd.Parameters.AddWithValue("@MontantTTC", montantTTC);
                cmd.Parameters.AddWithValue("@MontantAvance", montantAvance);
                cmd.Parameters.AddWithValue("@MontantIMP", montantIMP);
                cmd.Parameters.AddWithValue("@MontantAvoir", montantAvoir);
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
    }
}
