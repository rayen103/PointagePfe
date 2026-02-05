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

namespace CST.LePoint.Achat.Metier
{
    public class Achat_Avoir
    {
        #region Proriétès
        [XmlAttribute("NAvoir")]
        [Bindable(true)]
        public string NAvoir { get; set; }
        [XmlAttribute("CFournisseur")]
        [Bindable(true)]
        public string CFournisseur { get; set; }
        [XmlAttribute("BImport")]
        [Bindable(true)]
        public bool BImport { get; set; }
        [XmlAttribute("DateCreation")]
        [Bindable(true)]
        public DateTime? DateCreation { get; set; }
        [XmlAttribute("DateAvoir")]
        [Bindable(true)]
        public DateTime? DateAvoir { get; set; }
        [XmlAttribute("DateRemboursement")]
        [Bindable(true)]
        public DateTime? DateRemboursement { get; set; }
        [XmlAttribute("DateModification")]
        [Bindable(true)]
        public DateTime? DateModification { get; set; }
        [XmlAttribute("BTransfereeComptabilite")]
        [Bindable(true)]
        public bool BTransfereeComptabilite { get; set; }
        [XmlAttribute("Observation")]
        [Bindable(true)]
        public string Observation { get; set; }
        [XmlAttribute("MontantHT")]
        [Bindable(true)]
        public decimal MontantHT { get; set; }
        [XmlAttribute("MontantRemise")]
        [Bindable(true)]
        public decimal MontantRemise { get; set; }
        [XmlAttribute("MontantTaxe")]
        [Bindable(true)]
        public decimal MontantTaxe { get; set; }
        [XmlAttribute("MontantTTC")]
        [Bindable(true)]
        public decimal MontantTTC { get; set; }
        [XmlAttribute("NFacture")]
        [Bindable(true)]
        public string NFacture { get; set; }
        [XmlAttribute("RaisonSociale")]
        [Bindable(true)]
        public string RaisonSociale { get; set; }
        [XmlAttribute("Indice")]
        [Bindable(true)]
        public int Indice { get; set; }
        [XmlAttribute("Reference")]
        [Bindable(true)]
        public string Reference { get; set; }
        [XmlAttribute("BRemboursement")]
        [Bindable(true)]
        public bool BRemboursement { get; set; }
        [XmlAttribute("BFinancier")]
        [Bindable(true)]
        public bool BFinancier { get; set; }
        [XmlAttribute("CreePar")]
        [Bindable(true)]
        public int CreePar { get; set; }
        [XmlAttribute("ModifiePar")]
        [Bindable(true)]
        public int ModifiePar { get; set; }
        [XmlAttribute("PCInsertion")]
        [Bindable(true)]
        public string PCInsertion { get; set; }
        [XmlAttribute("PCModification")]
        [Bindable(true)]
        public string PCModification { get; set; }
        [XmlAttribute("NPiece")]
        [Bindable(true)]
        public string NPiece { get; set; }
        public Achat_AvoirDetailCollection AvoirDetails;
        public Achat_AvoirTaxeCollection AvoirTaxes;
        public string _NBonRetour { get; set; }
        public string _CEntrepot { get; set; }

        #endregion

        public void Inserer()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    if (this.BFinancier)
                        InsererAvoirFinancier(transaction);
                    else
                        InsererAvoirMarchandise(transaction);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        private void InsererAvoirMarchandise(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Achat_Avoir_Inserer";
                cmd.Parameters.AddWithValue("@BFinancier", this.BFinancier);
                cmd.Parameters.AddWithValue("@CFournisseur", this.CFournisseur);
                cmd.Parameters.AddWithValue("@Reference ", this.Reference);
                cmd.Parameters.AddWithValue("@DateAvoir ", this.DateAvoir);
                cmd.Parameters.AddWithValue("@BImport", this.BImport);
                cmd.Parameters.AddWithValue("@MontantHT", this.MontantHT);
                cmd.Parameters.AddWithValue("@MontantRemise", this.MontantRemise);
                cmd.Parameters.AddWithValue("@MontantTaxe", this.MontantTaxe);
                cmd.Parameters.AddWithValue("@MontantTTC", this.MontantTTC);
                cmd.Parameters.AddWithValue("@NFacture", this.NFacture);
                cmd.Parameters.AddWithValue("@Observation", this.Observation);
                cmd.Parameters.AddWithValue("@RaisonSociale", this.RaisonSociale);
                cmd.Parameters.AddWithValue("@BRemboursement", this.BRemboursement);
                cmd.Parameters.AddWithValue("@DateCreation", DateTime.Now);
                cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);
                cmd.Parameters.AddWithValue("@CreePar", this.CreePar);
                cmd.Parameters.AddWithValue("@Exercice", DateTime.Now.Year.ToString());
                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        this.NAvoir = dr["NAvoir"].ToString();
                    }
                }
                Achat_Avoir.AjouterNAvoir(this.NAvoir, this._NBonRetour,this._CEntrepot, transaction);
                int i = 1;
                foreach (Achat_AvoirDetail avoirDetail in AvoirDetails)
                {
                    avoirDetail.NAvoir = this.NAvoir;
                    avoirDetail.Ordre = i++;
                    avoirDetail.Sauvegarder(transaction);
                    Achat_Avoir.MiseAjourQteHist(this.NFacture, avoirDetail.CArticle,avoirDetail.Quantite, transaction);
                }

                foreach (Achat_AvoirTaxe avoirTaxe in AvoirTaxes)
                {
                    avoirTaxe.NAvoir = this.NAvoir;
                    avoirTaxe.Sauvegarder(transaction);
                }
                
                GenererReglement(transaction);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void MiseAjourQteHist(string nFacture, string cArticle, decimal quantite, SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Achat_Facture_MiseAjourQteHist";
                cmd.Parameters.AddWithValue("@NFacture", nFacture);
                cmd.Parameters.AddWithValue("@CArticle", cArticle);
                cmd.Parameters.AddWithValue("@Quantite", quantite);
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void GenererReglement(SqlTransaction transaction)
        {
            Achat_Reglement reglement = new Achat_Reglement();
            reglement.CEtatReglement = AchatHelper.EtatReglement.ENATTENTE.ToString();
            reglement.CFournisseur = this.CFournisseur;
            reglement.CreePar = this.CreePar;
            reglement.CTypeReglement = AchatHelper.TypeReglement.AVRAVC.ToString();
            reglement.DateCreation = this.DateCreation;
            reglement.DateEmission = this.DateAvoir;
            reglement.Montant = this.MontantTTC;
            reglement.NAvoir = this.NAvoir;
            reglement.PCCreation = this.PCInsertion;
            reglement.RaisonSociale = this.RaisonSociale;
            reglement.ResteReglement = this.MontantTTC;
            reglement.Inserer();
        }

        private static void AjouterNAvoir(string nAvoir, string nBonRetour,string cEntrepot, SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Achat_BonRetour_AjouterAvoir";
                cmd.Parameters.AddWithValue("@NAvoir", nAvoir);
                cmd.Parameters.AddWithValue("@NBonRetour", nBonRetour);
                cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void InsererAvoirFinancier(SqlTransaction transaction)
        {

        }
    }
}
