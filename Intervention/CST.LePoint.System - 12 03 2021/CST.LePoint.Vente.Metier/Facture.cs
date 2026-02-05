using CST.LePoint.Stock.Metier;
using CST.LePoint.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace CST.LePoint.Vente.Metier
{
    public class Facture
    {
        #region Propriétés

        [XmlAttribute("NFacture")]
        [Bindable(true)]
        public string NFacture { get; set; }

        [XmlAttribute("BAncienneFacture")]
        [Bindable(true)]
        public bool BAncienneFacture { get; set; }

        [XmlAttribute("CUnite")]
        [Bindable(true)]
        public string CUnite { get; set; }

        [XmlAttribute("CClient")]
        [Bindable(true)]
        public string CClient { get; set; }

        [XmlAttribute("MatriculeFiscale")]
        [Bindable(true)]
        public string MatriculeFiscale { get; set; }

        [XmlAttribute("Adresse")]
        [Bindable(true)]
        public string Adresse { get; set; }

        [XmlAttribute("NTelephone")]
        [Bindable(true)]
        public string NTelephone { get; set; }

        [XmlAttribute("CVendeur")]
        [Bindable(true)]
        public int CVendeur { get; set; }

        [XmlAttribute("BTransfereeComptabilite")]
        [Bindable(true)]
        public bool BTransfereeComptabilite { get; set; }

        [XmlAttribute("DateFacture")]
        [Bindable(true)]
        public DateTime? DateFacture { get; set; }

        [XmlAttribute("BExonereFodec")]
        [Bindable(true)]
        public bool BExonoreFodec { get; set; }

        [XmlAttribute("BExonoreTVA")]
        [Bindable(true)]
        public bool BExonoreTVA { get; set; }

        [XmlAttribute("BAvanceForfaitaire")]
        [Bindable(true)]
        public bool BAvanceForfaitaire { get; set; }

        [XmlAttribute("CNature")]
        [Bindable(true)]
        public int CNature { get; set; }

        [XmlAttribute("BGratuit")]
        [Bindable(true)]
        public bool BGratuit { get; set; }

        [XmlAttribute("MontantHT")]
        [Bindable(true)]
        public decimal MontantHT { get; set; }

        [XmlAttribute("MontantRemise")]
        [Bindable(true)]
        public decimal MontantRemise { get; set; }

        [XmlAttribute("MontantTaxe")]
        [Bindable(true)]
        public decimal MontantTaxe { get; set; }

        [XmlAttribute("MontantTimbre")]
        [Bindable(true)]
        public decimal MontantTimbre { get; set; }

        [XmlAttribute("MontantTTC")]
        [Bindable(true)]
        public decimal MontantTTC { get; set; }

        [XmlAttribute("CreditFacture")]
        [Bindable(true)]
        public decimal CreditFacture { get; set; }

        [XmlAttribute("NBonCommande")]
        [Bindable(true)]
        public string NBonCommande { get; set; }

        [XmlAttribute("Observation")]
        [Bindable(true)]
        public string Observation { get; set; }

        [XmlAttribute("OrigineFacture")]
        [Bindable(true)]
        public string OrigineFacture { get; set; }

        [XmlAttribute("RaisonSociale")]
        [Bindable(true)]
        public string RaisonSociale { get; set; }

        [XmlAttribute("Indice")]
        [Bindable(true)]
        public int Indice { get; set; }

        [XmlAttribute("ResteAvoirFinancier")]
        [Bindable(true)]
        public decimal ResteAvoirFinancier { get; set; }

        [XmlAttribute("NPiece")]
        [Bindable(true)]
        public string NPiece { get; set; }

        [XmlAttribute("BProFormat")]
        [Bindable(true)]
        public bool BProFormat { get; set; }

        [XmlAttribute("MontantArrondi")]
        [Bindable(true)]
        public decimal MontantArrondi { get; set; }

        [XmlAttribute("BValide")]
        [Bindable(true)]
        public bool BValide { get; set; }

        [XmlAttribute("MontantRetenuForfaitaire")]
        [Bindable(true)]
        public decimal MontantRetenuForfaitaire { get; set; }

        [XmlAttribute("DateInsertion")]
        [Bindable(true)]
        public DateTime? DateInsertion { get; set; }

        [XmlAttribute("DateModification")]
        [Bindable(true)]
        public DateTime? DateModification { get; set; }

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

        [XmlAttribute("Exercice")]
        [Bindable(true)]
        public string Exercice { get; set; }

        [XmlAttribute("PoidsTotal")]
        [Bindable(true)]
        public decimal PoidsTotal { get; set; }

        [XmlAttribute("CModeReglement")]
        [Bindable(true)]
        public string CModeReglement { get; set; }

        [XmlAttribute("BCommission")]
        [Bindable(true)]
        public bool BCommission { get; set; }

        [XmlAttribute("DateCommission")]
        [Bindable(true)]
        public DateTime? DateCommission { get; set; }

        public FactureDetailCollection FactureDetailCollection;
        public FactureTaxeCollection FactureTaxeCollection;

        #endregion Propriétés

        public Facture()
        {
            this.FactureDetailCollection = new FactureDetailCollection();
            this.FactureTaxeCollection = new FactureTaxeCollection();
        }

        #region FactureLoyer

        private void InsererFactureLoyer(SqlTransaction transaction, string chauffeur)
        {
            NFacture = CodeFactureLoyer(transaction, Exercice);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "FactureSession_Inserer";
                cmd.Parameters.AddWithValue("@NFacture ", this.NFacture);
                cmd.Parameters.AddWithValue("@BAncienneFacture ", this.BAncienneFacture);
                cmd.Parameters.AddWithValue("@CUnite", this.CUnite);
                cmd.Parameters.AddWithValue("@CClient", this.CClient);
                cmd.Parameters.AddWithValue("@MatriculeFiscale", this.MatriculeFiscale);
                cmd.Parameters.AddWithValue("@Adresse", this.Adresse);
                cmd.Parameters.AddWithValue("@NTelephone", this.NTelephone);
                cmd.Parameters.AddWithValue("@CVendeur ", this.CVendeur);
                cmd.Parameters.AddWithValue("@CModeReglement", this.CModeReglement);
                cmd.Parameters.AddWithValue("@BTransfereeComptabilite ", this.BTransfereeComptabilite);
                cmd.Parameters.AddWithValue("@DateFacture ", this.DateFacture);
                cmd.Parameters.AddWithValue("@BExonoreFodec", this.BExonoreFodec);
                cmd.Parameters.AddWithValue("@BExonoreTVA", this.BExonoreTVA);
                cmd.Parameters.AddWithValue("@BAvanceForfaitaire", this.BAvanceForfaitaire);
                cmd.Parameters.AddWithValue("@CNature", this.CNature);
                cmd.Parameters.AddWithValue("@BGratuit", this.BGratuit);
                cmd.Parameters.AddWithValue("@MontantHT", this.MontantHT);
                cmd.Parameters.AddWithValue("@MontantRemise", this.MontantRemise);
                cmd.Parameters.AddWithValue("@MontantTaxe", this.MontantTaxe);
                cmd.Parameters.AddWithValue("@MontantTimbre", this.MontantTimbre);
                cmd.Parameters.AddWithValue("@MontantTTC", this.MontantTTC);
                cmd.Parameters.AddWithValue("@CreditFacture", this.CreditFacture);
                cmd.Parameters.AddWithValue("@NBonCommande", this.NBonCommande);
                cmd.Parameters.AddWithValue("@Observation", this.Observation);
                cmd.Parameters.AddWithValue("@OrigineFacture", this.OrigineFacture);
                cmd.Parameters.AddWithValue("@RaisonSociale", this.RaisonSociale);
                cmd.Parameters.AddWithValue("@ResteAvoirFinancier", this.ResteAvoirFinancier);
                cmd.Parameters.AddWithValue("@NPiece", this.NPiece);
                cmd.Parameters.AddWithValue("@BProFormat", this.BProFormat);
                cmd.Parameters.AddWithValue("@MontantArrondi", this.MontantArrondi);
                cmd.Parameters.AddWithValue("@BValide", this.BValide);
                cmd.Parameters.AddWithValue("@MontantRetenuForfaitaire", this.MontantRetenuForfaitaire);
                cmd.Parameters.AddWithValue("@PoidsTotal", this.PoidsTotal);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);
                cmd.Parameters.AddWithValue("@CreePar", this.CreePar);
                cmd.Parameters.AddWithValue("@Exercice", this.Exercice);
                cmd.Parameters.AddWithValue("@BSession", VenteHelper.FACTURE_LOYER);

                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;
                cmd.ExecuteNonQuery();

                CreerBonLivraisonSession(transaction, chauffeur, VenteHelper.FACTURE_LOYER);
                int i = 1;
                foreach (FactureDetail factureDetail in FactureDetailCollection)
                {
                    factureDetail.NFacture = this.NFacture;
                    factureDetail.Ordre = i++;
                    factureDetail.Sauvegarder(transaction);
                }

                foreach (FactureTaxe factureTaxe in FactureTaxeCollection)
                {
                    factureTaxe.NFacture = this.NFacture;
                    factureTaxe.Sauvegarder(transaction);
                }
            }

            catch (Exception)
            {
                throw;
            }
        }

        private string CodeFactureLoyer(SqlTransaction transaction, string exercice)
        {
            string codeFacture = string.Empty;
            string dernierFacture = string.Empty;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT TOP 1 NFacture FROM Facture WHERE  BSession=2 AND SUBSTRING(NFacture,2,2)='" + exercice.Substring(2) + "' ORDER BY NFacture DESC";
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                        dernierFacture = dr["NFacture"].ToString();
                }
                if (!string.IsNullOrEmpty(dernierFacture))
                {
                    string dernierIndice = dernierFacture.Substring(8);
                    int indice = int.Parse(dernierIndice) + 1;
                    codeFacture = "L" + exercice.Substring(2, 2) + "/" + indice.ToString().PadLeft(6, '0');

                }
                else
                    codeFacture = "L" + exercice.Substring(2, 2) + "/" + "000001";
            }

            catch (Exception)
            {
                throw;
            }
            return (codeFacture);
        }

        public void InsererLoyer(string cChauffeur)
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    InsererFactureLoyer(transaction, cChauffeur);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        #endregion

        #region Facture Session

        public void InsererSession(string cChauffeur)
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    InsererFactureSession(transaction, cChauffeur);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        private string CodeFactureSession(SqlTransaction transaction, string exercice)
        {
            string codeFacture = string.Empty;
            string dernierFacture = string.Empty;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT TOP 1 NFacture FROM Facture WHERE SUBSTRING(NFacture,1,2)='" + exercice.Substring(2) + "' ORDER BY NFacture DESC";
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                        dernierFacture = dr["NFacture"].ToString();
                }
                if (!string.IsNullOrEmpty(dernierFacture))
                {
                    string dernierIndice = dernierFacture.Substring(3);
                    int indice = int.Parse(dernierIndice) + 1;
                    codeFacture = exercice.Substring(2, 2) + "/" + indice.ToString().PadLeft(6, '0');

                }
                else
                    codeFacture = exercice.Substring(2, 2) + "/" + "000001";
            }

            catch (Exception)
            {
                throw;
            }
            return (codeFacture);
        }

        private void InsererFactureSession(SqlTransaction transaction, string chauffeur)
        {
            NFacture = CodeFactureSession(transaction, Exercice);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "FactureSession_Inserer";
                cmd.Parameters.AddWithValue("@NFacture ", this.NFacture);
                cmd.Parameters.AddWithValue("@BAncienneFacture ", this.BAncienneFacture);
                cmd.Parameters.AddWithValue("@CUnite", this.CUnite);
                cmd.Parameters.AddWithValue("@CClient", this.CClient);
                cmd.Parameters.AddWithValue("@MatriculeFiscale", this.MatriculeFiscale);
                cmd.Parameters.AddWithValue("@Adresse", this.Adresse);
                cmd.Parameters.AddWithValue("@NTelephone", this.NTelephone);
                cmd.Parameters.AddWithValue("@CVendeur ", this.CVendeur);
                cmd.Parameters.AddWithValue("@CModeReglement", this.CModeReglement);
                cmd.Parameters.AddWithValue("@BTransfereeComptabilite ", this.BTransfereeComptabilite);
                cmd.Parameters.AddWithValue("@DateFacture ", this.DateFacture);
                cmd.Parameters.AddWithValue("@BExonoreFodec", this.BExonoreFodec);
                cmd.Parameters.AddWithValue("@BExonoreTVA", this.BExonoreTVA);
                cmd.Parameters.AddWithValue("@BAvanceForfaitaire", this.BAvanceForfaitaire);
                cmd.Parameters.AddWithValue("@CNature", this.CNature);
                cmd.Parameters.AddWithValue("@BGratuit", this.BGratuit);
                cmd.Parameters.AddWithValue("@MontantHT", this.MontantHT);
                cmd.Parameters.AddWithValue("@MontantRemise", this.MontantRemise);
                cmd.Parameters.AddWithValue("@MontantTaxe", this.MontantTaxe);
                cmd.Parameters.AddWithValue("@MontantTimbre", this.MontantTimbre);
                cmd.Parameters.AddWithValue("@MontantTTC", this.MontantTTC);
                cmd.Parameters.AddWithValue("@CreditFacture", this.CreditFacture);
                cmd.Parameters.AddWithValue("@NBonCommande", this.NBonCommande);
                cmd.Parameters.AddWithValue("@Observation", this.Observation);
                cmd.Parameters.AddWithValue("@OrigineFacture", this.OrigineFacture);
                cmd.Parameters.AddWithValue("@RaisonSociale", this.RaisonSociale);
                cmd.Parameters.AddWithValue("@ResteAvoirFinancier", this.ResteAvoirFinancier);
                cmd.Parameters.AddWithValue("@NPiece", this.NPiece);
                cmd.Parameters.AddWithValue("@BProFormat", this.BProFormat);
                cmd.Parameters.AddWithValue("@MontantArrondi", this.MontantArrondi);
                cmd.Parameters.AddWithValue("@BValide", this.BValide);
                cmd.Parameters.AddWithValue("@MontantRetenuForfaitaire", this.MontantRetenuForfaitaire);
                cmd.Parameters.AddWithValue("@PoidsTotal", this.PoidsTotal);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);
                cmd.Parameters.AddWithValue("@CreePar", this.CreePar);
                cmd.Parameters.AddWithValue("@Exercice", this.Exercice);
                cmd.Parameters.AddWithValue("@BSession", VenteHelper.FACTURE_SESSION);

                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;
                cmd.ExecuteNonQuery();

                CreerBonLivraisonSession(transaction, chauffeur, VenteHelper.FACTURE_SESSION);
                int i = 1;
                foreach (FactureDetail factureDetail in FactureDetailCollection)
                {
                    factureDetail.NFacture = this.NFacture;
                    factureDetail.Ordre = i++;
                    factureDetail.Sauvegarder(transaction);
                }

                foreach (FactureTaxe factureTaxe in FactureTaxeCollection)
                {
                    factureTaxe.NFacture = this.NFacture;
                    factureTaxe.Sauvegarder(transaction);
                }
            }

            catch (Exception)
            {
                throw;
            }
        }

        private void CreerBonLivraisonSession(SqlTransaction transaction, string chauffeur, int type)
        {
            bool premierBon = true;
            BonLivraison bonLivraison = null;
            decimal soldeBL = 0;
            foreach (FactureDetail factureDetail in FactureDetailCollection)
            {
                if (premierBon)
                {
                    bonLivraison = new BonLivraison();
                    bonLivraison.NBonCommande = this.NBonCommande;
                    bonLivraison.Chauffeur = chauffeur;
                    bonLivraison.BExonoreFodec = this.BExonoreFodec; // **correction 25-08-2014
                    bonLivraison.BExonoreTVA = this.BExonoreTVA;
                    bonLivraison.BAvanceForfaitaire = this.BAvanceForfaitaire;
                    bonLivraison.BGratuit = this.BGratuit;
                    bonLivraison.CClient = this.CClient;
                    bonLivraison.CUnite = this.CUnite;
                    bonLivraison.CreePar = this.CreePar;
                    bonLivraison.CVendeur = this.CVendeur;
                    bonLivraison.DateInsertion = this.DateInsertion;
                    bonLivraison.DateLivraison = this.DateFacture;
                    bonLivraison.Exercice = this.Exercice;
                    bonLivraison.MatriculeFiscale = this.MatriculeFiscale;
                    bonLivraison.MontantHT = this.MontantHT;
                    bonLivraison.MontantRemise = this.MontantRemise;
                    bonLivraison.MontantRetenuForfaitaire = this.MontantRetenuForfaitaire;
                    bonLivraison.MontantTaxe = this.MontantTaxe;
                    bonLivraison.MontantTTC = this.MontantTTC - this.MontantTimbre;
                    bonLivraison.NFacture = this.NFacture;
                    bonLivraison.Observation = this.Observation;
                    bonLivraison.PCInsertion = this.PCInsertion;
                    bonLivraison.RaisonSociale = this.RaisonSociale;

                    BonLivraisonDetail bonLivraisonDetail = new BonLivraisonDetail();
                    bonLivraisonDetail.CArticle = factureDetail.CArticle;
                    bonLivraisonDetail.CEntrepot = factureDetail.CEntrepot;
                    bonLivraisonDetail.CTaxe = factureDetail.CTaxe;
                    bonLivraisonDetail.CUnite = factureDetail.CUnite;
                    //bonLivraisonDetail.Epaisseur = factureDetail.Epaisseur;
                    //bonLivraisonDetail.Largeur = factureDetail.Largeur;
                    bonLivraisonDetail.LibArticle = factureDetail.LibArticle;
                    //bonLivraisonDetail.Longueur = factureDetail.Longueur;
                    bonLivraisonDetail.MontantNet = factureDetail.MontantNet;
                    bonLivraisonDetail.MontantTaxe = factureDetail.MontantTaxe;
                    bonLivraisonDetail.PourcentageFodec = factureDetail.PourcentageFodec;
                    bonLivraisonDetail.PourcentageRemise = factureDetail.PourcentageRemise;
                    bonLivraisonDetail.PrixHT = factureDetail.PrixHT;
                    bonLivraisonDetail.PrixRevient = factureDetail.PrixRevient;
                    bonLivraisonDetail.PrixVentePublic = factureDetail.PrixVentePublic;
                    bonLivraisonDetail.Quantite = factureDetail.Quantite;
                    bonLivraisonDetail.QuantiteHistorique = factureDetail.Quantite;
                    bonLivraisonDetail.Remise1 = factureDetail.Remise1;
                    bonLivraisonDetail.Remise2 = factureDetail.Remise2;
                    bonLivraisonDetail.TauxTVA = factureDetail.TauxTVA;
                    bonLivraison.BonLivraisonDetailCollection.Add(bonLivraisonDetail);
                    premierBon = false;
                }
                else
                {
                    BonLivraisonDetail bonLivraisonDetail = new BonLivraisonDetail();
                    bonLivraisonDetail.CArticle = factureDetail.CArticle;
                    bonLivraisonDetail.CEntrepot = factureDetail.CEntrepot;
                    bonLivraisonDetail.CTaxe = factureDetail.CTaxe;
                    bonLivraisonDetail.CUnite = factureDetail.CUnite;
                    //bonLivraisonDetail.Epaisseur = factureDetail.Epaisseur;
                    //bonLivraisonDetail.Largeur = factureDetail.Largeur;
                    bonLivraisonDetail.LibArticle = factureDetail.LibArticle;
                    //bonLivraisonDetail.Longueur = factureDetail.Longueur;
                    bonLivraisonDetail.MontantNet = factureDetail.MontantNet;
                    bonLivraisonDetail.MontantTaxe = factureDetail.MontantTaxe;
                    bonLivraisonDetail.PourcentageFodec = factureDetail.PourcentageFodec;
                    bonLivraisonDetail.PourcentageRemise = factureDetail.PourcentageRemise;
                    bonLivraisonDetail.PrixHT = factureDetail.PrixHT;
                    bonLivraisonDetail.PrixRevient = factureDetail.PrixRevient;
                    bonLivraisonDetail.PrixVentePublic = factureDetail.PrixVentePublic;
                    bonLivraisonDetail.Quantite = factureDetail.Quantite;
                    bonLivraisonDetail.QuantiteHistorique = factureDetail.Quantite;
                    bonLivraisonDetail.Remise1 = factureDetail.Remise1;
                    bonLivraisonDetail.Remise2 = factureDetail.Remise2;
                    bonLivraisonDetail.TauxTVA = factureDetail.TauxTVA;
                    bonLivraison.BonLivraisonDetailCollection.Add(bonLivraisonDetail);
                }
            }

            foreach (FactureTaxe factureTaxe in FactureTaxeCollection)
            {
                if (bonLivraison != null && !factureTaxe.CTaxe.Equals(VenteHelper.CODE_TAXE_TIMBRE_FISCAL.ToString()))
                {
                    BonLivraisonTaxe taxe = new BonLivraisonTaxe();
                    taxe.Assiette = factureTaxe.Assiette;
                    taxe.BExonoreFodec = factureTaxe.BExonoreFodec;
                    taxe.BExonoreTVA = factureTaxe.BExonoreTVA;
                    taxe.BExport = factureTaxe.BExport;
                    taxe.CTaxe = factureTaxe.CTaxe;
                    taxe.MontantTaxe = factureTaxe.MontantTaxe;
                    taxe.TauxTVA = factureTaxe.TauxTVA;
                    bonLivraison.BonLivraisonTaxeCollection.Add(taxe);
                }
            }
            if (bonLivraison != null)
            {
                bonLivraison.InsererSession(transaction, type);
                soldeBL = soldeBL + bonLivraison.MontantTTC;
                foreach (FactureDetail factureDetail1 in FactureDetailCollection)
                {
                    factureDetail1.NBonLivraison = bonLivraison.NBonLivraison;
                }
            }
            VenteHelper.ModifierSolde(this.NFacture, this.DateFacture, this.CClient, -soldeBL, this.MontantTTC, 0, 0, 0, 0, transaction);
        }

        #endregion

        private void InsererFacture(SqlTransaction transaction, string chauffeur)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Facture_Inserer";
                cmd.Parameters.AddWithValue("@BAncienneFacture ", this.BAncienneFacture);
                cmd.Parameters.AddWithValue("@CUnite", this.CUnite);
                cmd.Parameters.AddWithValue("@CClient", this.CClient);
                cmd.Parameters.AddWithValue("@MatriculeFiscale", this.MatriculeFiscale);
                cmd.Parameters.AddWithValue("@Adresse", this.Adresse);
                cmd.Parameters.AddWithValue("@NTelephone", this.NTelephone);
                cmd.Parameters.AddWithValue("@CVendeur ", this.CVendeur);
                cmd.Parameters.AddWithValue("@CModeReglement", this.CModeReglement);
                cmd.Parameters.AddWithValue("@BTransfereeComptabilite ", this.BTransfereeComptabilite);
                cmd.Parameters.AddWithValue("@DateFacture ", this.DateFacture);
                cmd.Parameters.AddWithValue("@BExonoreFodec", this.BExonoreFodec);
                cmd.Parameters.AddWithValue("@BExonoreTVA", this.BExonoreTVA);
                cmd.Parameters.AddWithValue("@BAvanceForfaitaire", this.BAvanceForfaitaire);
                cmd.Parameters.AddWithValue("@CNature", this.CNature);
                cmd.Parameters.AddWithValue("@BGratuit", this.BGratuit);
                cmd.Parameters.AddWithValue("@MontantHT", this.MontantHT);
                cmd.Parameters.AddWithValue("@MontantRemise", this.MontantRemise);
                cmd.Parameters.AddWithValue("@MontantTaxe", this.MontantTaxe);
                cmd.Parameters.AddWithValue("@MontantTimbre", this.MontantTimbre);
                cmd.Parameters.AddWithValue("@MontantTTC", this.MontantTTC);
                cmd.Parameters.AddWithValue("@CreditFacture", this.CreditFacture);
                cmd.Parameters.AddWithValue("@NBonCommande", this.NBonCommande);
                cmd.Parameters.AddWithValue("@Observation", this.Observation);
                cmd.Parameters.AddWithValue("@OrigineFacture", this.OrigineFacture);
                cmd.Parameters.AddWithValue("@RaisonSociale", this.RaisonSociale);
                cmd.Parameters.AddWithValue("@ResteAvoirFinancier", this.ResteAvoirFinancier);
                cmd.Parameters.AddWithValue("@NPiece", this.NPiece);
                cmd.Parameters.AddWithValue("@BProFormat", this.BProFormat);
                cmd.Parameters.AddWithValue("@MontantArrondi", this.MontantArrondi);
                cmd.Parameters.AddWithValue("@BValide", this.BValide);
                cmd.Parameters.AddWithValue("@MontantRetenuForfaitaire", this.MontantRetenuForfaitaire);
                cmd.Parameters.AddWithValue("@PoidsTotal", this.PoidsTotal);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);
                cmd.Parameters.AddWithValue("@CreePar", this.CreePar);
                cmd.Parameters.AddWithValue("@Exercice", this.Exercice);

                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        this.NFacture = dr["NFacture"].ToString();
                        this.Indice = int.Parse(dr["DernierIndice"].ToString());
                    }
                }

                CreerBonLivraison(transaction, chauffeur);
                int i = 1;
                foreach (FactureDetail factureDetail in FactureDetailCollection)
                {
                    factureDetail.NFacture = this.NFacture;
                    factureDetail.Ordre = i++;
                    factureDetail.Sauvegarder(transaction);
                }

                foreach (FactureTaxe factureTaxe in FactureTaxeCollection)
                {
                    factureTaxe.NFacture = this.NFacture;
                    factureTaxe.Sauvegarder(transaction);
                }
            }

            catch (Exception)
            {
                throw;
            }
        }

        public void InsererFactureBL(SqlTransaction transaction, bool toutBL)
        {
            string[] NBonLivraison = new string[this.FactureDetailCollection.Count];
            string NumeroFacture = string.Empty;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Facture_Inserer";
                cmd.Parameters.AddWithValue("@BAncienneFacture ", this.BAncienneFacture);
                cmd.Parameters.AddWithValue("@CUnite", this.CUnite);
                cmd.Parameters.AddWithValue("@CClient", this.CClient);
                cmd.Parameters.AddWithValue("@MatriculeFiscale", this.MatriculeFiscale);
                cmd.Parameters.AddWithValue("@Adresse", this.Adresse);
                cmd.Parameters.AddWithValue("@NTelephone", this.NTelephone);
                cmd.Parameters.AddWithValue("@CVendeur ", this.CVendeur);
                cmd.Parameters.AddWithValue("@CModeReglement", this.CModeReglement);
                cmd.Parameters.AddWithValue("@BTransfereeComptabilite ", this.BTransfereeComptabilite);
                cmd.Parameters.AddWithValue("@DateFacture ", this.DateFacture);
                cmd.Parameters.AddWithValue("@BExonoreFodec", this.BExonoreFodec);
                cmd.Parameters.AddWithValue("@BExonoreTVA", this.BExonoreTVA);
                cmd.Parameters.AddWithValue("@BAvanceForfaitaire", this.BAvanceForfaitaire);
                cmd.Parameters.AddWithValue("@CNature", this.CNature);
                cmd.Parameters.AddWithValue("@BGratuit", this.BGratuit);
                cmd.Parameters.AddWithValue("@MontantHT", this.MontantHT);
                cmd.Parameters.AddWithValue("@MontantRemise", this.MontantRemise);
                cmd.Parameters.AddWithValue("@MontantTaxe", this.MontantTaxe);
                cmd.Parameters.AddWithValue("@MontantTimbre", this.MontantTimbre);
                cmd.Parameters.AddWithValue("@MontantTTC", this.MontantTTC);
                cmd.Parameters.AddWithValue("@CreditFacture", this.CreditFacture);
                cmd.Parameters.AddWithValue("@NBonCommande", this.NBonCommande);
                cmd.Parameters.AddWithValue("@Observation", this.Observation);
                cmd.Parameters.AddWithValue("@OrigineFacture", this.OrigineFacture);
                cmd.Parameters.AddWithValue("@RaisonSociale", this.RaisonSociale);
                cmd.Parameters.AddWithValue("@ResteAvoirFinancier", this.ResteAvoirFinancier);
                cmd.Parameters.AddWithValue("@NPiece", this.NPiece);
                cmd.Parameters.AddWithValue("@BProFormat", this.BProFormat);
                cmd.Parameters.AddWithValue("@MontantArrondi", this.MontantArrondi);
                cmd.Parameters.AddWithValue("@BValide", this.BValide);
                cmd.Parameters.AddWithValue("@MontantRetenuForfaitaire", this.MontantRetenuForfaitaire);
                cmd.Parameters.AddWithValue("@PoidsTotal", this.PoidsTotal);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);
                cmd.Parameters.AddWithValue("@CreePar", this.CreePar);
                cmd.Parameters.AddWithValue("@Exercice", this.Exercice);


                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        this.NFacture = dr["NFacture"].ToString();
                        this.Indice = int.Parse(dr["DernierIndice"].ToString());
                    }
                }

                int i = 1;
                decimal soldeBR = 0;
                decimal soldeBL = 0;

                foreach (FactureDetail factureDetail in FactureDetailCollection)
                {
                    int j = 0;
                    bool exist = false;
                    while (!string.IsNullOrEmpty(NBonLivraison[j]))
                    {
                        if (NBonLivraison[j] == factureDetail.NBonLivraison)
                        { exist = true; }
                        if (j < FactureDetailCollection.Count)
                            j++;
                        else
                            break;
                    }
                    if (!exist)
                    { NBonLivraison[j] = factureDetail.NBonLivraison; }
                    exist = false;
                    if (NBonLivraison[0] == null)
                        transaction.Rollback();
                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.Transaction = transaction;
                    cmd1.Connection = transaction.Connection;

                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "SELECT NFacture AS NumeroFacture FROM BonLivraison WHERE NBonLivraison ='" + factureDetail.NBonLivraison + "'";

                    using (SqlDataReader dr = cmd1.ExecuteReader())
                    {
                        if (dr.Read())
                            NumeroFacture = dr["NumeroFacture"].ToString();
                    }
                    if (string.IsNullOrEmpty(NumeroFacture))
                    {
                        factureDetail.NFacture = this.NFacture;
                        factureDetail.Ordre = i++;
                        factureDetail.Sauvegarder(transaction);
                    }
                }

                foreach (FactureTaxe factureTaxe in FactureTaxeCollection)
                {
                    factureTaxe.NFacture = this.NFacture;
                    factureTaxe.Sauvegarder(transaction);
                }
                int k = 0;
                while (!string.IsNullOrEmpty(NBonLivraison[k]))
                {
                    Facture.AjouterNFacture(this.NFacture, NBonLivraison[k], transaction);
                    if (!toutBL)
                    {
                        SqlCommand cmd1 = new SqlCommand();
                        cmd1.Transaction = transaction;
                        cmd1.Connection = transaction.Connection;
                        cmd1.CommandType = CommandType.Text;
                        cmd1.CommandText = "SELECT ISNULL(SUM(ISNULL(MontantTTC,0)),0) AS Somme FROM BonRetour WHERE NBonLivraison = '" + NBonLivraison[k] + "'";
                        using (SqlDataReader dr = cmd1.ExecuteReader())
                            if (dr.Read())
                                soldeBR = soldeBR + decimal.Parse(dr["Somme"].ToString());
                        cmd1.ExecuteNonQuery();
                    }
                    if (k < FactureDetailCollection.Count - 1)
                        k++;
                    else
                        break;
                }
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = transaction.Connection;
                cmd2.Transaction = transaction;
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText = "SELECT ISNULL(SUM(ISNULL(MontantTTC,0)),0) AS Somme FROM BonLivraison WHERE NFacture ='" + this.NFacture + "'";
                using (SqlDataReader dr = cmd2.ExecuteReader())
                    if (dr.Read())
                        soldeBL = soldeBL + decimal.Parse(dr["Somme"].ToString());
                VenteHelper.ModifierSolde(this.NFacture, this.DateFacture, this.CClient, -soldeBL, this.MontantTTC, -soldeBR, 0m, 0m, 0m, transaction);
            }

            catch (Exception)
            {
                throw;
            }
        }

        private void CreerBonLivraison(SqlTransaction transaction, string chauffeur)
        {
            bool premierBon = true;
            BonLivraison bonLivraison = null;
            decimal soldeBL = 0;
            foreach (FactureDetail factureDetail in FactureDetailCollection)
            {
                if (premierBon)
                {
                    bonLivraison = new BonLivraison();
                    bonLivraison.NBonCommande = this.NBonCommande;
                    bonLivraison.Chauffeur = chauffeur;
                    bonLivraison.BExonoreFodec = this.BExonoreFodec; // **correction 25-08-2014
                    bonLivraison.BExonoreTVA = this.BExonoreTVA;
                    bonLivraison.BAvanceForfaitaire = this.BAvanceForfaitaire;
                    bonLivraison.BGratuit = this.BGratuit;
                    bonLivraison.CClient = this.CClient;
                    bonLivraison.CUnite = this.CUnite;
                    bonLivraison.CreePar = this.CreePar;
                    bonLivraison.CVendeur = this.CVendeur;
                    bonLivraison.DateInsertion = this.DateInsertion;
                    bonLivraison.DateLivraison = this.DateFacture;
                    bonLivraison.Exercice = this.Exercice;
                    bonLivraison.MatriculeFiscale = this.MatriculeFiscale;
                    bonLivraison.MontantHT = this.MontantHT;
                    bonLivraison.MontantRemise = this.MontantRemise;
                    bonLivraison.MontantRetenuForfaitaire = this.MontantRetenuForfaitaire;
                    bonLivraison.MontantTaxe = this.MontantTaxe;
                    bonLivraison.MontantTTC = this.MontantTTC - this.MontantTimbre;
                    bonLivraison.NFacture = this.NFacture;
                    bonLivraison.Observation = this.Observation;
                    bonLivraison.PCInsertion = this.PCInsertion;
                    bonLivraison.RaisonSociale = this.RaisonSociale;

                    BonLivraisonDetail bonLivraisonDetail = new BonLivraisonDetail();
                    bonLivraisonDetail.CArticle = factureDetail.CArticle;
                    bonLivraisonDetail.CEntrepot = factureDetail.CEntrepot;
                    bonLivraisonDetail.CTaxe = factureDetail.CTaxe;
                    bonLivraisonDetail.CUnite = factureDetail.CUnite;
                    bonLivraisonDetail.OrdreBonCommande = factureDetail.Ordre;
                    //bonLivraisonDetail.Epaisseur = factureDetail.Epaisseur;
                    //bonLivraisonDetail.Largeur = factureDetail.Largeur;
                    bonLivraisonDetail.LibArticle = factureDetail.LibArticle;
                    //bonLivraisonDetail.Longueur = factureDetail.Longueur;
                    bonLivraisonDetail.MontantNet = factureDetail.MontantNet;
                    bonLivraisonDetail.MontantTaxe = factureDetail.MontantTaxe;
                    bonLivraisonDetail.PourcentageFodec = factureDetail.PourcentageFodec;
                    bonLivraisonDetail.PourcentageRemise = factureDetail.PourcentageRemise;
                    bonLivraisonDetail.PrixHT = factureDetail.PrixHT;
                    bonLivraisonDetail.PrixRevient = factureDetail.PrixRevient;
                    bonLivraisonDetail.PrixVentePublic = factureDetail.PrixVentePublic;
                    bonLivraisonDetail.Quantite = factureDetail.Quantite;
                    bonLivraisonDetail.QuantiteHistorique = factureDetail.Quantite;
                    bonLivraisonDetail.Remise1 = factureDetail.Remise1;
                    bonLivraisonDetail.Remise2 = factureDetail.Remise2;
                    bonLivraisonDetail.TauxTVA = factureDetail.TauxTVA;
                    bonLivraisonDetail.Poids = factureDetail.Poids;
                    bonLivraison.BonLivraisonDetailCollection.Add(bonLivraisonDetail);
                    premierBon = false;
                }
                else
                {
                    BonLivraisonDetail bonLivraisonDetail = new BonLivraisonDetail();
                    bonLivraisonDetail.CArticle = factureDetail.CArticle;
                    bonLivraisonDetail.CEntrepot = factureDetail.CEntrepot;
                    bonLivraisonDetail.CTaxe = factureDetail.CTaxe;
                    bonLivraisonDetail.CUnite = factureDetail.CUnite;
                    bonLivraisonDetail.OrdreBonCommande = factureDetail.Ordre;
                    //bonLivraisonDetail.Epaisseur = factureDetail.Epaisseur;
                    //bonLivraisonDetail.Largeur = factureDetail.Largeur;
                    bonLivraisonDetail.LibArticle = factureDetail.LibArticle;
                    //bonLivraisonDetail.Longueur = factureDetail.Longueur;
                    bonLivraisonDetail.MontantNet = factureDetail.MontantNet;
                    bonLivraisonDetail.MontantTaxe = factureDetail.MontantTaxe;
                    bonLivraisonDetail.PourcentageFodec = factureDetail.PourcentageFodec;
                    bonLivraisonDetail.PourcentageRemise = factureDetail.PourcentageRemise;
                    bonLivraisonDetail.PrixHT = factureDetail.PrixHT;
                    bonLivraisonDetail.PrixRevient = factureDetail.PrixRevient;
                    bonLivraisonDetail.PrixVentePublic = factureDetail.PrixVentePublic;
                    bonLivraisonDetail.Quantite = factureDetail.Quantite;
                    bonLivraisonDetail.QuantiteHistorique = factureDetail.Quantite;
                    bonLivraisonDetail.Remise1 = factureDetail.Remise1;
                    bonLivraisonDetail.Remise2 = factureDetail.Remise2;
                    bonLivraisonDetail.TauxTVA = factureDetail.TauxTVA;
                    bonLivraisonDetail.Poids = factureDetail.Poids;
                    bonLivraison.BonLivraisonDetailCollection.Add(bonLivraisonDetail);
                }
            }

            foreach (FactureTaxe factureTaxe in FactureTaxeCollection)
            {
                if (bonLivraison != null && !factureTaxe.CTaxe.Equals(VenteHelper.CODE_TAXE_TIMBRE_FISCAL.ToString()))
                {
                    BonLivraisonTaxe taxe = new BonLivraisonTaxe();
                    taxe.Assiette = factureTaxe.Assiette;
                    taxe.BExonoreFodec = factureTaxe.BExonoreFodec;
                    taxe.BExonoreTVA = factureTaxe.BExonoreTVA;
                    taxe.BExport = factureTaxe.BExport;
                    taxe.CTaxe = factureTaxe.CTaxe;
                    taxe.MontantTaxe = factureTaxe.MontantTaxe;
                    taxe.TauxTVA = factureTaxe.TauxTVA;
                    bonLivraison.BonLivraisonTaxeCollection.Add(taxe);
                }
            }
            if (bonLivraison != null)
            {
                bonLivraison.Inserer(transaction);
                soldeBL = soldeBL + bonLivraison.MontantTTC;
                foreach (FactureDetail factureDetail1 in FactureDetailCollection)
                {
                    factureDetail1.NBonLivraison = bonLivraison.NBonLivraison;
                }
            }
            VenteHelper.ModifierSolde(this.NFacture, this.DateFacture, this.CClient, -soldeBL, this.MontantTTC, 0, 0, 0, 0, transaction);
        }

        public void FacturerBL(bool toutBL)
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    InsererFactureBL(transaction, toutBL);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        private void CreeBonSortie(SqlTransaction transaction)
        {
            try
            {
                BonCommandeSpecialDetailCollection details = BonCommandeSpecialDetailCollection.Charger(this.NBonCommande);
                if (details.Count == 0)
                    return;
                BonSortie bonSortie = new BonSortie();
                bonSortie.CEntrepot = details[0].CEntrepot;
                bonSortie.NDocumentSource = NBonCommande;
                bonSortie.DateSortie = DateTime.Now;
                bonSortie.CClient = CClient;
                bonSortie.RaisonSociale = RaisonSociale;
                bonSortie.Exercice = Exercice;
                bonSortie.TypeMouvement = StockHelper.TypesMouvementStock.BS_BONTRANSFORMATION.ToString();

                foreach (BonCommandeSpecialDetail detail in details)
                {
                    BonSortieDetail bonSortieDetail = new BonSortieDetail();
                    bonSortieDetail.CEntrepot = detail.CEntrepot;
                    bonSortieDetail.CArticle = detail.CArticle;
                    bonSortieDetail.LibArticle = detail.LibArticle;
                    bonSortieDetail.CUnite = detail.CUnite;
                    bonSortieDetail.PrixHT = detail.PrixHT;
                    bonSortieDetail.Quantite = detail.Quantite;
                    bonSortieDetail.DateInsertion = DateTime.Now;
                    bonSortie.BonSortieDetailCollection.Add(bonSortieDetail);
                }
                bonSortie.Inserer(transaction);
            }



            catch (Exception)
            {
                throw;
            }
        }

        public void InsererFactureDirecte(string chauffeur)
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    InsererFacture(transaction, chauffeur);
                    if (!string.IsNullOrWhiteSpace(this.NBonCommande))
                    {
                        BonCommande bc = BonCommande.Charger(this.NBonCommande);
                        if (bc != null && bc.BSpecial)
                            CreeBonSortie(transaction);
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

        public void InsererFactureAnt(string chauffeur)
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
                    cmd.CommandText = "FactureAnt_Inserer";
                    cmd.Parameters.AddWithValue("@NFacture ", this.NFacture);
                    cmd.Parameters.AddWithValue("@BAncienneFacture ", this.BAncienneFacture);
                    cmd.Parameters.AddWithValue("@CUnite", this.CUnite);
                    cmd.Parameters.AddWithValue("@CClient", this.CClient);
                    cmd.Parameters.AddWithValue("@MatriculeFiscale", this.MatriculeFiscale);
                    cmd.Parameters.AddWithValue("@Adresse", this.Adresse);
                    cmd.Parameters.AddWithValue("@NTelephone", this.NTelephone);
                    cmd.Parameters.AddWithValue("@CVendeur ", this.CVendeur);
                    cmd.Parameters.AddWithValue("@BTransfereeComptabilite ", this.BTransfereeComptabilite);
                    cmd.Parameters.AddWithValue("@DateFacture ", this.DateFacture);
                    cmd.Parameters.AddWithValue("@BExonoreFodec", this.BExonoreFodec);
                    cmd.Parameters.AddWithValue("@BExonoreTVA", this.BExonoreTVA);
                    cmd.Parameters.AddWithValue("@BAvanceForfaitaire", this.BAvanceForfaitaire);
                    cmd.Parameters.AddWithValue("@CNature", this.CNature);
                    cmd.Parameters.AddWithValue("@BGratuit", this.BGratuit);
                    cmd.Parameters.AddWithValue("@MontantHT", this.MontantHT);
                    cmd.Parameters.AddWithValue("@MontantRemise", this.MontantRemise);
                    cmd.Parameters.AddWithValue("@MontantTaxe", this.MontantTaxe);
                    cmd.Parameters.AddWithValue("@MontantTimbre", this.MontantTimbre);
                    cmd.Parameters.AddWithValue("@MontantTTC", this.MontantTTC);
                    cmd.Parameters.AddWithValue("@CreditFacture", this.CreditFacture);
                    cmd.Parameters.AddWithValue("@NBonCommande", this.NBonCommande);
                    cmd.Parameters.AddWithValue("@Observation", this.Observation);
                    cmd.Parameters.AddWithValue("@OrigineFacture", this.OrigineFacture);
                    cmd.Parameters.AddWithValue("@RaisonSociale", this.RaisonSociale);
                    cmd.Parameters.AddWithValue("@ResteAvoirFinancier", this.ResteAvoirFinancier);
                    cmd.Parameters.AddWithValue("@NPiece", this.NPiece);
                    cmd.Parameters.AddWithValue("@BProFormat", this.BProFormat);
                    cmd.Parameters.AddWithValue("@MontantArrondi", this.MontantArrondi);
                    cmd.Parameters.AddWithValue("@BValide", this.BValide);
                    cmd.Parameters.AddWithValue("@MontantRetenuForfaitaire", this.MontantRetenuForfaitaire);

                    cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                    cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);
                    cmd.Parameters.AddWithValue("@CreePar", this.CreePar);
                    cmd.Parameters.AddWithValue("@Exercice", this.Exercice);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    //using (SqlDataReader dr = cmd.ExecuteReader())
                    //{
                    //    if (dr.Read())
                    //    {
                    //        this.NFacture = dr["NFacture"].ToString();
                    //        this.Indice = int.Parse(dr["DernierIndice"].ToString());
                    //    }
                    //}

                    CreerBonLivraison(transaction, chauffeur);
                    int i = 1;
                    foreach (FactureDetail factureDetail in FactureDetailCollection)
                    {
                        factureDetail.NFacture = this.NFacture;
                        factureDetail.Ordre = i++;
                        factureDetail.Sauvegarder(transaction);
                    }

                    foreach (FactureTaxe factureTaxe in FactureTaxeCollection)
                    {
                        factureTaxe.NFacture = this.NFacture;
                        factureTaxe.Sauvegarder(transaction);
                    }
                }

                catch (Exception)
                {
                    throw;
                }
            }
        }

        public void ModifierFactureDirecte()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    ModifierFactureDirecte(transaction);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public void ModifierFactureDirecte(SqlTransaction transaction)
        {
            try
            {
                int ordre = 0;
                Facture ancienneFacture = Facture.Charger(this.NFacture, transaction);
                VenteHelper.ModifierSolde(null, null, ancienneFacture.CClient, 0, -ancienneFacture.MontantTTC, 0, 0, 0, 0, transaction);
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "FactureSupprimer_Inserer";
                cmd.Parameters.AddWithValue("@BAncienneFactureS ", ancienneFacture.BAncienneFacture);
                cmd.Parameters.AddWithValue("@CUniteS", ancienneFacture.CUnite);
                cmd.Parameters.AddWithValue("@CClientS", ancienneFacture.CClient);
                cmd.Parameters.AddWithValue("@MatriculeFiscaleS", ancienneFacture.MatriculeFiscale);
                cmd.Parameters.AddWithValue("@AdresseS", ancienneFacture.Adresse);
                cmd.Parameters.AddWithValue("@NTelephoneS", ancienneFacture.NTelephone);
                cmd.Parameters.AddWithValue("@CVendeurS", ancienneFacture.CVendeur);
              
                cmd.Parameters.AddWithValue("@BTransfereeComptabiliteS ", ancienneFacture.BTransfereeComptabilite);
                cmd.Parameters.AddWithValue("@DateFactureS ", ancienneFacture.DateFacture);
                cmd.Parameters.AddWithValue("@BExonoreFodecS", ancienneFacture.BExonoreFodec);
                cmd.Parameters.AddWithValue("@BExonoreTVAS", ancienneFacture.BExonoreTVA);
                cmd.Parameters.AddWithValue("@BAvanceForfaitaireS", ancienneFacture.BAvanceForfaitaire);
                cmd.Parameters.AddWithValue("@CNatureS", ancienneFacture.CNature);
                cmd.Parameters.AddWithValue("@BGratuitS", ancienneFacture.BGratuit);
                cmd.Parameters.AddWithValue("@MontantHTS", ancienneFacture.MontantHT);
                cmd.Parameters.AddWithValue("@MontantRemiseS", ancienneFacture.MontantRemise);
                cmd.Parameters.AddWithValue("@MontantTaxeS", ancienneFacture.MontantTaxe);
                cmd.Parameters.AddWithValue("@MontantTimbreS", ancienneFacture.MontantTimbre);
                cmd.Parameters.AddWithValue("@MontantTTCS", ancienneFacture.MontantTTC);
                cmd.Parameters.AddWithValue("@CreditFactureS", ancienneFacture.CreditFacture);
                cmd.Parameters.AddWithValue("@NBonCommandeS", ancienneFacture.NBonCommande);
                cmd.Parameters.AddWithValue("@ObservationS", ancienneFacture.Observation);
                cmd.Parameters.AddWithValue("@OrigineFactureS", ancienneFacture.OrigineFacture);
                cmd.Parameters.AddWithValue("@RaisonSocialeS", ancienneFacture.RaisonSociale);
                cmd.Parameters.AddWithValue("@ResteAvoirFinancierS", ancienneFacture.ResteAvoirFinancier);
                cmd.Parameters.AddWithValue("@NPieceS", ancienneFacture.NPiece);
                cmd.Parameters.AddWithValue("@IndiceS", ancienneFacture.Indice);
                cmd.Parameters.AddWithValue("@BProFormatS", ancienneFacture.BProFormat);
                cmd.Parameters.AddWithValue("@MontantArrondiS", ancienneFacture.MontantArrondi);
                cmd.Parameters.AddWithValue("@BValideS", ancienneFacture.BValide);
                cmd.Parameters.AddWithValue("@MontantRetenuForfaitaireS", ancienneFacture.MontantRetenuForfaitaire);
                cmd.Parameters.AddWithValue("@PoidsTotalS", ancienneFacture.PoidsTotal);
                cmd.Parameters.AddWithValue("@DateInsertionS", DateTime.Now);
                cmd.Parameters.AddWithValue("@DateModificationS", DateTime.Now);
                cmd.Parameters.AddWithValue("@PCInsertionS", ancienneFacture.PCInsertion);
                cmd.Parameters.AddWithValue("@CreeParS", ancienneFacture.CreePar);
                cmd.Parameters.AddWithValue("@PCModificationS", ancienneFacture.PCModification);
                cmd.Parameters.AddWithValue("@ModifieParS", ancienneFacture.ModifiePar);
                cmd.Parameters.AddWithValue("@CModeReglementS", ancienneFacture.CModeReglement);





                cmd.Parameters.AddWithValue("@NFacture", this.NFacture);
                cmd.Parameters.AddWithValue("@BAncienneFacture ", this.BAncienneFacture);
                cmd.Parameters.AddWithValue("@CUnite", this.CUnite);
                cmd.Parameters.AddWithValue("@CClient", this.CClient);
                cmd.Parameters.AddWithValue("@MatriculeFiscale", this.MatriculeFiscale);
                cmd.Parameters.AddWithValue("@Adresse", this.Adresse);
                cmd.Parameters.AddWithValue("@NTelephone", this.NTelephone);
                cmd.Parameters.AddWithValue("@CVendeur ", this.CVendeur);
      
                cmd.Parameters.AddWithValue("@BTransfereeComptabilite ", this.BTransfereeComptabilite);
                cmd.Parameters.AddWithValue("@DateFacture ", this.DateFacture);
                cmd.Parameters.AddWithValue("@BExonoreFodec", this.BExonoreFodec);
                cmd.Parameters.AddWithValue("@BExonoreTVA", this.BExonoreTVA);
                cmd.Parameters.AddWithValue("@BAvanceForfaitaire", this.BAvanceForfaitaire);
                cmd.Parameters.AddWithValue("@CNature", this.CNature);
                cmd.Parameters.AddWithValue("@BGratuit", this.BGratuit);
                cmd.Parameters.AddWithValue("@MontantHT", this.MontantHT);
                cmd.Parameters.AddWithValue("@MontantRemise", this.MontantRemise);
                cmd.Parameters.AddWithValue("@MontantTaxe", this.MontantTaxe);
                cmd.Parameters.AddWithValue("@MontantTimbre", this.MontantTimbre);
                cmd.Parameters.AddWithValue("@MontantTTC", this.MontantTTC);
                cmd.Parameters.AddWithValue("@CreditFacture", this.CreditFacture);
                cmd.Parameters.AddWithValue("@NBonCommande", this.NBonCommande);
                cmd.Parameters.AddWithValue("@Observation", this.Observation);
                cmd.Parameters.AddWithValue("@OrigineFacture", this.OrigineFacture);
                cmd.Parameters.AddWithValue("@RaisonSociale", this.RaisonSociale);
                cmd.Parameters.AddWithValue("@ResteAvoirFinancier", this.ResteAvoirFinancier);
                cmd.Parameters.AddWithValue("@NPiece", this.NPiece);
                cmd.Parameters.AddWithValue("@Indice", this.Indice);
                cmd.Parameters.AddWithValue("@BProFormat", this.BProFormat);
                cmd.Parameters.AddWithValue("@MontantArrondi", this.MontantArrondi);
                cmd.Parameters.AddWithValue("@BValide", this.BValide);
                cmd.Parameters.AddWithValue("@MontantRetenuForfaitaire", this.MontantRetenuForfaitaire);
                cmd.Parameters.AddWithValue("@PoidsTotal", this.PoidsTotal);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);
                cmd.Parameters.AddWithValue("@CreePar", this.CreePar);
                cmd.Parameters.AddWithValue("@PCModification", this.PCModification);
                cmd.Parameters.AddWithValue("@ModifiePar", this.ModifiePar);
                cmd.Parameters.AddWithValue("@Exercice", this.Exercice);
                cmd.Parameters.AddWithValue("@CModeReglement", this.CModeReglement);

                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        ordre = int.Parse(dr["DernierOrdre"].ToString());
                    }
                }
                //cmd.ExecuteNonQuery();
                int i = 1;
                string NBonLivraison = string.Empty;
                BonLivraisonDetailCollection bonLivraisonCollection = new BonLivraisonDetailCollection();
                SupprimerDetailFactureAnterieurs(transaction, ordre);
                SupprimerTaxeFactureAnterieurs(transaction);
                foreach (FactureDetail factureDetail in FactureDetailCollection)
                {
                    NBonLivraison = ancienneFacture.FactureDetailCollection[0].NBonLivraison;
                    BonLivraisonDetail ancienDetail = BonLivraisonDetail.Charger(NBonLivraison, factureDetail.CArticle, i);
                    if (ancienDetail == null)
                        ancienDetail = BonLivraisonDetail.Charger(NBonLivraison, factureDetail.CArticle, FactureDetailCollection.Count - i + 1);
                    factureDetail.NFacture = this.NFacture;
                    factureDetail.Ordre = i++;
                    factureDetail.NBonLivraison = ancienneFacture.FactureDetailCollection[0].NBonLivraison;
                    factureDetail.Sauvegarder(transaction);
                   // factureDetail.SauvegarderFactureDetailSupprimer(transaction, ordre);

                    BonLivraisonDetail detail = new BonLivraisonDetail();
                    detail.CArticle = factureDetail.CArticle;
                    detail.CEntrepot = factureDetail.CEntrepot;
                    detail.CTaxe = factureDetail.CTaxe;
                    detail.CUnite = factureDetail.CUnite;
                    detail.LibArticle = factureDetail.LibArticle;
                    detail.MontantNet = factureDetail.MontantNet;
                    detail.MontantTaxe = factureDetail.MontantTaxe;
                    detail.NBonLivraison = factureDetail.NBonLivraison;
                    if (ancienDetail != null)
                        detail.OrdreBonCommande = ancienDetail.OrdreBonCommande;
                    detail.PourcentageFodec = factureDetail.PourcentageFodec;
                    detail.PourcentageRemise = factureDetail.PourcentageRemise;
                    detail.PrixHT = factureDetail.PrixHT;
                    detail.PrixRevient = factureDetail.PrixRevient;
                    detail.Quantite = factureDetail.Quantite;
                    if (ancienDetail != null)
                        if (ancienDetail.QuantiteHistorique == ancienDetail.Quantite)
                            detail.QuantiteHistorique = detail.Quantite;
                        else
                            detail.QuantiteHistorique = ancienDetail.QuantiteHistorique - (ancienDetail.Quantite - detail.Quantite);
                    else
                        detail.QuantiteHistorique = detail.Quantite;
                    detail.Remise1 = factureDetail.Remise1;
                    detail.Remise2 = factureDetail.Remise2;
                    detail.TauxTVA = factureDetail.TauxTVA;
                    detail.Poids = factureDetail.Poids;
                    bonLivraisonCollection.Add(detail);
                }
                BonLivraisonTaxeCollection collectionTaxe = new BonLivraisonTaxeCollection();
                foreach (FactureTaxe factureTaxe in FactureTaxeCollection)
                {
                    factureTaxe.NFacture = this.NFacture;
                    factureTaxe.Sauvegarder(transaction);
                    factureTaxe.SauvegarderFactureTaxeSupprimer(transaction, ordre);
                    if (!factureTaxe.CTaxe.Equals(VenteHelper.CODE_TAXE_TIMBRE_FISCAL.ToString()))
                    {
                        BonLivraisonTaxe taxeBL = new BonLivraisonTaxe();
                        taxeBL.Assiette = factureTaxe.Assiette;
                        taxeBL.BExonoreFodec = factureTaxe.BExonoreFodec;
                        taxeBL.BExonoreTVA = factureTaxe.BExonoreTVA;
                        taxeBL.BExport = factureTaxe.BExport;
                        taxeBL.CTaxe = factureTaxe.CTaxe;
                        taxeBL.MontantTaxe = factureTaxe.MontantTaxe;
                        taxeBL.TauxTVA = factureTaxe.TauxTVA;
                        collectionTaxe.Add(taxeBL);
                    }
                }

                BonLivraison bonLivraison = new BonLivraison();
                bonLivraison.NBonLivraison = NBonLivraison;
                bonLivraison.Adresse = Adresse;
                bonLivraison.CClient = CClient;
                bonLivraison.RaisonSociale = RaisonSociale;
                bonLivraison.BExonoreFodec = BExonoreFodec;
                bonLivraison.BExonoreTVA = BExonoreTVA;
                bonLivraison.BAvanceForfaitaire = BAvanceForfaitaire;
                bonLivraison.BGratuit = BGratuit;
                bonLivraison.CVendeur = CVendeur;
                bonLivraison.DateLivraison = DateFacture;
                bonLivraison.Exercice = Exercice;
                bonLivraison.MatriculeFiscale = MatriculeFiscale;
                bonLivraison.MontantHT = MontantHT;
                bonLivraison.MontantRemise = MontantRemise;
                bonLivraison.MontantRetenuForfaitaire = MontantRetenuForfaitaire;
                bonLivraison.MontantTaxe = MontantTaxe;
                bonLivraison.MontantTTC = MontantTTC - MontantTimbre;
                bonLivraison.NBonCommandeMannuel = OrigineFacture;
                bonLivraison.NFacture = NFacture;
                bonLivraison.NTelephone = NTelephone;
                bonLivraison.Observation = Observation;
                bonLivraison.poidsTotal = PoidsTotal;

                bonLivraison.BonLivraisonDetailCollection = new BonLivraisonDetailCollection();
                bonLivraison.BonLivraisonDetailCollection = bonLivraisonCollection;
                bonLivraison.BonLivraisonTaxeCollection = new BonLivraisonTaxeCollection();
                bonLivraison.BonLivraisonTaxeCollection = collectionTaxe;
                bonLivraison.Exercice = this.Exercice;
                bonLivraison.Modifier(transaction);
                VenteHelper.ModifierClientDerniereFacture(ancienneFacture.CClient, transaction);
                VenteHelper.ModifierSolde(NFacture, DateFacture, this.CClient, 0, this.MontantTTC, 0, 0, 0, 0, transaction);
                VenteHelper.ModifierClientDerniereFacture(this.CClient, transaction);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public BonLivraisonCollection TrouverBonLivraisons(FactureDetailCollection detailsFacture)
        {
            string[] NBonLivraisons = new string[detailsFacture.Count];
            BonLivraisonCollection collection = new BonLivraisonCollection();

            foreach (FactureDetail detail in detailsFacture)
            {
                if (collection.Count == 0)
                {
                    BonLivraison bonLivraison = BonLivraison.Charger(detail.NBonLivraison);
                    BonLivraison ancienBL = collection.RecupererBonLivraison(detail.NBonLivraison);
                    if (ancienBL == null)
                    {
                        BonLivraison BL = new BonLivraison();
                        BL.Adresse = bonLivraison.Adresse;
                        BL.BEchantillon = bonLivraison.BEchantillon;
                        BL.BExonoreFodec = bonLivraison.BExonoreFodec;
                        BL.BExonoreTVA = bonLivraison.BExonoreTVA;
                        BL.BAvanceForfaitaire = bonLivraison.BAvanceForfaitaire;
                        BL.BGratuit = bonLivraison.BGratuit;
                        BL.BRetour = bonLivraison.BRetour;
                        BL.CClient = bonLivraison.CClient;
                        BL.Chauffeur = bonLivraison.Chauffeur;
                        BL.CMission = bonLivraison.CMission;
                        BL.CreePar = this.CreePar;
                        BL.CVehicule = bonLivraison.CVehicule;
                        BL.CVendeur = bonLivraison.CVendeur;
                        BL.DateInsertion = this.DateInsertion;
                        //BL.DateLivraison = bonLivraison.DateLivraison;
                        BL.DateModification = this.DateModification;
                        BL.MatriculeFiscale = bonLivraison.MatriculeFiscale;
                        BL.ModifiePar = this.ModifiePar;
                        collection.Add(bonLivraison);
                    }
                    else
                    {
                    }
                }
            }

            return collection;
        }

        public void ModifierResteAvoir(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Facture_ModifierResteAvoir";
                cmd.Parameters.AddWithValue("@NFacture", this.NFacture);
                cmd.Parameters.AddWithValue("@ResteAvoirFinancier", this.ResteAvoirFinancier);
                cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                cmd.Parameters.AddWithValue("@PCModification", this.PCModification);
                cmd.Parameters.AddWithValue("@ModifiePar", this.ModifiePar);

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

        public static void FactureDouteux(SqlTransaction transaction, string nFacture,string dateContentieux)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE Facture SET BContentieux = 1, DateContentieux = "+ SysHelper.ToSqlDatetime( dateContentieux) + " WHERE NFacture='" + nFacture + "'";
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SupprimerDetailFactureAnterieurs(SqlTransaction transaction, int ordre)
        {
            try
            {
                FactureDetailCollection collection = FactureDetailCollection.Charger(this.NFacture, transaction);
                foreach (FactureDetail detail in collection)
                {
                    detail.SauvegarderFactureDetailSupprimer(transaction, ordre);
                }
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Facture_SupprimerDetails";

                cmd.Parameters.AddWithValue("@NFacture", this.NFacture);

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

        private void SupprimerTaxeFactureAnterieurs(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Facture_SupprimerTaxes";

                cmd.Parameters.AddWithValue("@NFacture", this.NFacture);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void RestituerSoldeFacture(SqlTransaction transaction)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Transaction = transaction;
            cmd.Connection = transaction.Connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Client_RestituerSoldeFacture";

            cmd.Parameters.AddWithValue("@NFacture", this.NFacture);
            cmd.Parameters.AddWithValue("@CClient", this.CClient);
            foreach (SqlParameter parametre in cmd.Parameters)
                if (parametre.Value == null)
                    parametre.Value = DBNull.Value;

            cmd.ExecuteNonQuery();
        }

        public static Facture Charger(string nFacture, SqlTransaction transaction)
        {
            Facture facture = null;

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Facture_Charger";
                cmd.Parameters.AddWithValue("@NFacture", nFacture);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        facture = new Facture();
                        facture.NFacture = dr["NFacture"].ToString();

                        if (dr["CUnite"] != DBNull.Value)
                            facture.CUnite = dr["CUnite"].ToString();
                        if (dr["BAncienneFacture"] != DBNull.Value)
                            facture.BAncienneFacture = bool.Parse(dr["BAncienneFacture"].ToString());
                        if (dr["CClient"] != DBNull.Value)
                            facture.CClient = dr["CClient"].ToString();
                        if (dr["MatriculeFiscale"] != DBNull.Value)
                            facture.MatriculeFiscale = dr["MatriculeFiscale"].ToString();
                        if (dr["Adresse"] != DBNull.Value)
                            facture.Adresse = dr["Adresse"].ToString();
                        if (dr["NTelephone"] != DBNull.Value)
                            facture.NTelephone = dr["NTelephone"].ToString();
                        if (dr["CVendeur"] != DBNull.Value)
                            facture.CVendeur = int.Parse(dr["CVendeur"].ToString());
                        if (dr["CModeReglement"] != DBNull.Value)
                            facture.CModeReglement = dr["CModeReglement"].ToString();
                        if (dr["BTransfereeComptabilite"] != DBNull.Value)
                            facture.BTransfereeComptabilite = bool.Parse(dr["BTransfereeComptabilite"].ToString());
                        //if (dr["DateInsertion"] != DBNull.Value)
                        //    facture.DateInsertion = DateTime.Parse(dr["DateInsertion"].ToString());
                        if (dr["DateFacture"] != DBNull.Value)
                            facture.DateFacture = DateTime.Parse(dr["DateFacture"].ToString());
                        //if (dr["DateModification"] != DBNull.Value)
                        //    facture.DateModification = DateTime.Parse(dr["DateModification"].ToString());
                        if (dr["BExonoreFodec"] != DBNull.Value)
                            facture.BExonoreFodec = bool.Parse(dr["BExonoreFodec"].ToString());
                        if (dr["BExonoreTVA"] != DBNull.Value)
                            facture.BExonoreTVA = bool.Parse(dr["BExonoreTVA"].ToString());
                        if (dr["BAvanceForfaitaire"] != DBNull.Value)
                            facture.BAvanceForfaitaire = bool.Parse(dr["BAvanceForfaitaire"].ToString());
                        if (dr["CNature"] != DBNull.Value)
                            facture.CNature = int.Parse(dr["CNature"].ToString());
                        if (dr["BGratuit"] != DBNull.Value)
                            facture.BGratuit = bool.Parse(dr["BGratuit"].ToString());
                        if (dr["MontantHT"] != DBNull.Value)
                            facture.MontantHT = decimal.Parse(dr["MontantHT"].ToString());
                        if (dr["MontantRemise"] != DBNull.Value)
                            facture.MontantRemise = decimal.Parse(dr["MontantRemise"].ToString());
                        if (dr["MontantTaxe"] != DBNull.Value)
                            facture.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                        if (dr["MontantTimbre"] != DBNull.Value)
                            facture.MontantTimbre = decimal.Parse(dr["MontantTimbre"].ToString());
                        if (dr["MontantTTC"] != DBNull.Value)
                            facture.MontantTTC = decimal.Parse(dr["MontantTTC"].ToString());
                        if (dr["CreditFacture"] != DBNull.Value)
                            facture.CreditFacture = decimal.Parse(dr["CreditFacture"].ToString());
                        if (dr["NBonCommande"] != DBNull.Value)
                            facture.NBonCommande = dr["NBonCommande"].ToString();
                        if (dr["Observation"] != DBNull.Value)
                            facture.Observation = dr["Observation"].ToString();
                        if (dr["OrigineFacture"] != DBNull.Value)
                            facture.OrigineFacture = dr["OrigineFacture"].ToString();
                        if (dr["RaisonSociale"] != DBNull.Value)
                            facture.RaisonSociale = dr["RaisonSociale"].ToString();
                        if (dr["Indice"] != DBNull.Value)
                            facture.Indice = int.Parse(dr["Indice"].ToString());
                        if (dr["ResteAvoirFinancier"] != DBNull.Value)
                            facture.ResteAvoirFinancier = decimal.Parse(dr["ResteAvoirFinancier"].ToString());
                        //if (dr["CreePar"] != DBNull.Value)
                        //    facture.CreePar = int.Parse(dr["CreePar"].ToString());
                        //if (dr["ModifiePar"] != DBNull.Value)
                        //    facture.ModifiePar = int.Parse(dr["ModifiePar"].ToString());
                        //if (dr["PCInsertion"] != DBNull.Value)
                        //    facture.PCInsertion = dr["PCInsertion"].ToString();
                        //if (dr["PCModification"] != DBNull.Value)
                        //    facture.PCModification = dr["PCModification"].ToString();
                        if (dr["NPiece"] != DBNull.Value)
                            facture.NPiece = dr["NPiece"].ToString();
                        if (dr["BProFormat"] != DBNull.Value)
                            facture.BProFormat = bool.Parse(dr["BProFormat"].ToString());
                        if (dr["MontantArrondi"] != DBNull.Value)
                            facture.MontantArrondi = decimal.Parse(dr["MontantArrondi"].ToString());
                        if (dr["BValide"] != DBNull.Value)
                            facture.BValide = bool.Parse(dr["BValide"].ToString());
                        if (dr["MontantRetenuForfaitaire"] != DBNull.Value)
                            facture.MontantRetenuForfaitaire = decimal.Parse(dr["MontantRetenuForfaitaire"].ToString());
                        if (dr["PoidsTotal"] != DBNull.Value)
                            facture.PoidsTotal = decimal.Parse(dr["PoidsTotal"].ToString());
                        if (dr["BCommission"] != DBNull.Value)
                            facture.BCommission = bool.Parse(dr["BCommission"].ToString());
                        if (dr["DateCommission"] != DBNull.Value)
                            facture.DateCommission = DateTime.Parse(dr["DateCommission"].ToString());

                        facture.FactureDetailCollection = FactureDetailCollection.Charger(facture.NFacture);
                        facture.FactureTaxeCollection = FactureTaxeCollection.Charger(facture.NFacture);
                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return (facture);
        }

        public static Facture ChargerNumSeul(string nFacture)
        {
            Facture facture = null;

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
                    cmd.CommandText = "Facture_ChargerNumSeul";
                    cmd.Parameters.AddWithValue("@NFacture", nFacture);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            facture = new Facture();
                            facture.NFacture = dr["NFacture"].ToString();
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return (facture);
            }
        }

        public static Facture Charger(string nFacture)
        {
            Facture facture = null;
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    facture = Facture.Charger(nFacture, transaction);
                    transaction.Commit();
                }

                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }

                return (facture);
            }
        }

        public static void AjouterNFacture(string nFacture, string nBonLivraison, SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "BonLivraison_AjouterNFacture";
                cmd.Parameters.AddWithValue("@NFacture", nFacture);
                cmd.Parameters.AddWithValue("@NBonLivraison", nBonLivraison);

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

        public void MiseAJourCreditFacture(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "Facture_MiseAJourCredit";
                cmd.Parameters.AddWithValue("@NFacture", this.NFacture);
                cmd.Parameters.AddWithValue("@CreditFacture", this.CreditFacture);
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
    }


    public class FactureCollection : List<Facture>
    {
        public FactureCollection()
        {
        }

        public static FactureCollection FactureChargerControl(string cClient, DateTime limiteDate)
        {
            FactureCollection factureCollection = new FactureCollection();
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Facture_ChargerControle";
                    cmd.Parameters.AddWithValue("@CClient", cClient);
                    cmd.Parameters.AddWithValue("@DateLimite", limiteDate);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Facture facture = new Facture();
                            facture.NFacture = dr["NFacture"].ToString();
                            if (dr["CClient"] != DBNull.Value)
                                facture.CClient = dr["CClient"].ToString();
                            if (dr["DateFacture"] != DBNull.Value)
                                facture.DateFacture = DateTime.Parse(dr["DateFacture"].ToString());
                            factureCollection.Add(facture);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return factureCollection;
        }

        public static FactureCollection FactureChargerListeClient(string cClient)
        {
            FactureCollection factureCollection = new FactureCollection();
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Facture_ChargerListeClient";
                    cmd.Parameters.AddWithValue("@CClient", cClient);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Facture facture = new Facture();
                            facture.NFacture = dr["NFacture"].ToString();
                            factureCollection.Add(facture);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return factureCollection;
        }

        public static FactureCollection Charger(string nFacture)
        {
            FactureCollection collection = new FactureCollection();
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
                    cmd.CommandText = "Facture_Charger";
                    cmd.Parameters.AddWithValue("@NFacture", nFacture);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Facture facture = new Facture();
                            facture.NFacture = dr["NFacture"].ToString();

                            if (dr["CUnite"] != DBNull.Value)
                                facture.CUnite = dr["CUnite"].ToString();
                            if (dr["BAncienneFacture"] != DBNull.Value)
                                facture.BAncienneFacture = bool.Parse(dr["BAncienneFacture"].ToString());
                            if (dr["CClient"] != DBNull.Value)
                                facture.CClient = dr["CClient"].ToString();
                            if (dr["MatriculeFiscale"] != DBNull.Value)
                                facture.MatriculeFiscale = dr["MatriculeFiscale"].ToString();
                            if (dr["Adresse"] != DBNull.Value)
                                facture.Adresse = dr["Adresse"].ToString();
                            if (dr["NTelephone"] != DBNull.Value)
                                facture.NTelephone = dr["NTelephone"].ToString();
                            if (dr["CModeReglement"] != DBNull.Value)
                                facture.CModeReglement = dr["CModeReglement"].ToString();
                            if (dr["CVendeur"] != DBNull.Value)
                                facture.CVendeur = int.Parse(dr["CVendeur"].ToString());
                            if (dr["BTransfereeComptabilite"] != DBNull.Value)
                                facture.BTransfereeComptabilite = bool.Parse(dr["BTransfereeComptabilite"].ToString());
                            if (dr["DateInsertion"] != DBNull.Value)
                                facture.DateInsertion = DateTime.Parse(dr["DateInsertion"].ToString());
                            if (dr["DateFacture"] != DBNull.Value)
                                facture.DateFacture = DateTime.Parse(dr["DateFacture"].ToString());
                            if (dr["DateModification"] != DBNull.Value)
                                facture.DateModification = DateTime.Parse(dr["DateModification"].ToString());
                            if (dr["BExonoreFodec"] != DBNull.Value)
                                facture.BExonoreFodec = bool.Parse(dr["BExonoreFodec"].ToString());
                            if (dr["BExonoreTVA"] != DBNull.Value)
                                facture.BExonoreTVA = bool.Parse(dr["BExonoreTVA"].ToString());
                            if (dr["BAvanceForfaitaire"] != DBNull.Value)
                                facture.BAvanceForfaitaire = bool.Parse(dr["BAvanceForfaitaire"].ToString());
                            if (dr["CNature"] != DBNull.Value)
                                facture.CNature = int.Parse(dr["CNature"].ToString());
                            if (dr["BGratuit"] != DBNull.Value)
                                facture.BGratuit = bool.Parse(dr["BGratuit"].ToString());
                            if (dr["MontantHT"] != DBNull.Value)
                                facture.MontantHT = decimal.Parse(dr["MontantHT"].ToString());
                            if (dr["MontantRemise"] != DBNull.Value)
                                facture.MontantRemise = decimal.Parse(dr["MontantRemise"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                facture.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["MontantTimbre"] != DBNull.Value)
                                facture.MontantTimbre = decimal.Parse(dr["MontantTimbre"].ToString());
                            if (dr["MontantTTC"] != DBNull.Value)
                                facture.MontantTTC = decimal.Parse(dr["MontantTTC"].ToString());
                            if (dr["CreditFacture"] != DBNull.Value)
                                facture.CreditFacture = decimal.Parse(dr["CreditFacture"].ToString());
                            if (dr["NBonCommande"] != DBNull.Value)
                                facture.NBonCommande = dr["NBonCommande"].ToString();
                            if (dr["Observation"] != DBNull.Value)
                                facture.Observation = dr["Observation"].ToString();
                            if (dr["OrigineFacture"] != DBNull.Value)
                                facture.OrigineFacture = dr["OrigineFacture"].ToString();
                            if (dr["RaisonSociale"] != DBNull.Value)
                                facture.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["Indice"] != DBNull.Value)
                                facture.Indice = int.Parse(dr["Indice"].ToString());
                            if (dr["ResteAvoirFinancier"] != DBNull.Value)
                                facture.ResteAvoirFinancier = decimal.Parse(dr["ResteAvoirFinancier"].ToString());
                            if (dr["CreePar"] != DBNull.Value)
                                facture.CreePar = int.Parse(dr["CreePar"].ToString());
                            if (dr["ModifiePar"] != DBNull.Value)
                                facture.ModifiePar = int.Parse(dr["ModifiePar"].ToString());
                            if (dr["PCInsertion"] != DBNull.Value)
                                facture.PCInsertion = dr["PCInsertion"].ToString();
                            if (dr["PCModification"] != DBNull.Value)
                                facture.PCModification = dr["PCModification"].ToString();
                            if (dr["NPiece"] != DBNull.Value)
                                facture.NPiece = dr["NPiece"].ToString();
                            if (dr["BProFormat"] != DBNull.Value)
                                facture.BProFormat = bool.Parse(dr["BProFormat"].ToString());
                            if (dr["MontantArrondi"] != DBNull.Value)
                                facture.MontantArrondi = decimal.Parse(dr["MontantArrondi"].ToString());
                            if (dr["BValide"] != DBNull.Value)
                                facture.BValide = bool.Parse(dr["BValide"].ToString());
                            if (dr["MontantRetenuForfaitaire"] != DBNull.Value)
                                facture.MontantRetenuForfaitaire = decimal.Parse(dr["MontantRetenuForfaitaire"].ToString());
                            if (dr["PoidsTotal"] != DBNull.Value)
                                facture.PoidsTotal = decimal.Parse(dr["PoidsTotal"].ToString());
                            facture.FactureDetailCollection = FactureDetailCollection.Charger(facture.NFacture);
                            facture.FactureTaxeCollection = FactureTaxeCollection.Charger(facture.NFacture);

                            collection.Add(facture);
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

        public static FactureCollection ChargerAnomalie(string cClient,string nFacture)
        {
            FactureCollection collection = new FactureCollection();
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
                    cmd.CommandText = "Facture_ChargerAnomalie";
                    cmd.Parameters.AddWithValue("@CClient", cClient);
                    cmd.Parameters.AddWithValue("@NFacture", nFacture);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Facture facture = new Facture();
                            facture.NFacture = dr["NFacture"].ToString();
                            if (dr["CClient"] != DBNull.Value)
                                facture.CClient = dr["CClient"].ToString();
                            if (dr["RaisonSociale"] != DBNull.Value)
                                facture.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["NBonCommande"] != DBNull.Value)
                                facture.NBonCommande = dr["NBonCommande"].ToString();
                            collection.Add(facture);
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
    }
}