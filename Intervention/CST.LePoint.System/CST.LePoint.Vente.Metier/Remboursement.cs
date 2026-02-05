using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace CST.LePoint.Vente.Metier
{
    public class Remboursement
    {
        [XmlAttribute("CReglement")]
        [Bindable(true)]
        public string CReglement { get; set; }

        [XmlAttribute("NAvoir")]
        [Bindable(true)]
        public string NAvoir { get; set; }

        [XmlAttribute("MontantRemboursement")]
        [Bindable(true)]
        public decimal MontantRemboursement { get; set; }

        [XmlAttribute("DateRemboursement")]
        [Bindable(true)]
        public DateTime DateRemboursement { get; set; }

        [XmlAttribute("CreerPar")]
        [Bindable(true)]
        public int CreerPar { get; set; }

        [XmlAttribute("DateInsertion")]
        [Bindable(true)]
        public DateTime DateInsertion { get; set; }


        [XmlAttribute("PCInsertion")]
        [Bindable(true)]
        public string PCInsertion { get; set; }

        public Remboursement()
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
                cmd.CommandText = "Remboursement_Inserer";
                cmd.Parameters.AddWithValue("@CReglement", this.CReglement);
                cmd.Parameters.AddWithValue("@NAvoir", this.NAvoir);
                cmd.Parameters.AddWithValue("@CreerPar", this.CreerPar);
                cmd.Parameters.AddWithValue("@DateInsertion", this.DateInsertion);
                cmd.Parameters.AddWithValue("@DateRemboursement", this.DateRemboursement);
                cmd.Parameters.AddWithValue("@MontantRembourse", this.MontantRemboursement);
                cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);
                
                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                cmd.ExecuteNonQuery();
                RemboursementAvoir(transaction);
                
            }
            catch
            {
                transaction.Rollback();
            }
        }

        private void RemboursementAvoir(SqlTransaction transaction)
        {
            Avoir avoir = Avoir.Charger(this.NAvoir);
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
             
                try
                {
                    Reglement reglement = Reglement.ChargerReglementAvoir(this.NAvoir, transaction);
                    reglement.ResteReglement = reglement.ResteReglement - this.MontantRemboursement;
                    reglement.CEtatReglement = VenteHelper.EtatReglement.ASSOCIE.ToString();
                    reglement.Modifier(transaction);

                    avoir.BRemboursement = true;
                    avoir.DateRemboursement = (DateTime)this.DateRemboursement;
                    avoir.Etat = VenteHelper.EtatAvoir.REMB.ToString();
                    avoir.ModifierRemb(transaction);
                    if (avoir.BFinancier)
                    {
                        Facture facture = Facture.Charger(avoir.NFacture);
                        if (facture != null)
                        {
                            if (facture.ResteAvoirFinancier >= this.MontantRemboursement)
                                facture.ResteAvoirFinancier = facture.ResteAvoirFinancier - this.MontantRemboursement;
                            else
                                facture.ResteAvoirFinancier = 0;
                            facture.ModifierResteAvoir(transaction);
                        }
                    }
                   
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}