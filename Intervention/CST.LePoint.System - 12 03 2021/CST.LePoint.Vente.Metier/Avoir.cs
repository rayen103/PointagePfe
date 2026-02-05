using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace CST.LePoint.Vente.Metier
{
    public class Avoir
    {
        #region Proriétès

        [XmlAttribute("NAvoir")]
        [Bindable(true)]
        public string NAvoir { get; set; }

        [XmlAttribute("CUnite")]
        [Bindable(true)]
        public string CUnite { get; set; }

        [XmlAttribute("BFinancier")]
        [Bindable(true)]
        public bool BFinancier { get; set; }

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

        [XmlAttribute("DateAvoir")]
        [Bindable(true)]
        public DateTime? DateAvoir { get; set; }

        [XmlAttribute("DateRemboursement")]
        [Bindable(true)]
        public DateTime? DateRemboursement { get; set; }

        [XmlAttribute("Etat")]
        [Bindable(true)]
        public string Etat { get; set; }

        [XmlAttribute("BExonoreTVA")]
        [Bindable(true)]
        public bool BExonoreTVA { get; set; }

        [XmlAttribute("BExonoreFodec")]
        [Bindable(true)]
        public bool BExonoreFodec { get; set; }

        [XmlAttribute("BAvanceForfaitaire")]
        [Bindable(true)]
        public bool BAvanceForfaitaire { get; set; }

        [XmlAttribute("CNature")]
        [Bindable(true)]
        public int CNature { get; set; }

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

        [XmlAttribute("Observation")]
        [Bindable(true)]
        public string Observation { get; set; }

        [XmlAttribute("RaisonSociale")]
        [Bindable(true)]
        public string RaisonSociale { get; set; }

        [XmlAttribute("Indice")]
        [Bindable(true)]
        public int Indice { get; set; }

        [XmlAttribute("BRemboursement")]
        [Bindable(true)]
        public bool BRemboursement { get; set; }

        [XmlAttribute("BAncien")]
        [Bindable(true)]
        public bool BAncien { get; set; }

        [XmlAttribute("NPiece")]
        [Bindable(true)]
        public string NPiece { get; set; }

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

        [XmlAttribute("MessageAlerte")]
        [Bindable(true)]
        public string MessageAlerte { get; set; }

        [XmlAttribute("PoidsTotal")]
        [Bindable(true)]
        public decimal PoidsTotal { get; set; }

        public AvoirDetailCollection AvoirDetailCollection;
        public AvoirTaxeCollection AvoirTaxeCollection;

        #endregion Proriétès

        public Avoir()
        {
            this.AvoirDetailCollection = new AvoirDetailCollection();
            this.AvoirTaxeCollection = new AvoirTaxeCollection();
        }

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

        //public void Inserer(SqlTransaction transaction)
        //{
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Transaction = transaction;
        //        cmd.Connection = transaction.Connection;

        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = "Avoir_Inserer";
        //        cmd.Parameters.AddWithValue("@BFinancier ", this.BFinancier);
        //        cmd.Parameters.AddWithValue("@CUnite", this.CUnite);
        //        cmd.Parameters.AddWithValue("@CClient", this.CClient);
        //        cmd.Parameters.AddWithValue("@MatriculeFiscale", this.MatriculeFiscale);
        //        cmd.Parameters.AddWithValue("@CVendeur ", this.CVendeur);
        //        cmd.Parameters.AddWithValue("@BTransfereeComptabilite ", this.BTransfereeComptabilite);
        //        cmd.Parameters.AddWithValue("@DateAvoir ", this.DateAvoir);
        //        cmd.Parameters.AddWithValue("@DateRemboursement", this.DateRemboursement);
        //        cmd.Parameters.AddWithValue("@Etat", this.Etat);
        //        cmd.Parameters.AddWithValue("@BExonoreTVA", this.BExonoreTVA);
        //        cmd.Parameters.AddWithValue("@CNature", this.CNature);
        //        cmd.Parameters.AddWithValue("@MontantHT", this.MontantHT);
        //        cmd.Parameters.AddWithValue("@MontantRemise", this.MontantRemise);
        //        cmd.Parameters.AddWithValue("@MontantTaxe", this.MontantTaxe);
        //        cmd.Parameters.AddWithValue("@MontantTTC", this.MontantTTC);
        //        cmd.Parameters.AddWithValue("@NFacture", this.NFacture);
        //        cmd.Parameters.AddWithValue("@Observation", this.Observation);
        //        cmd.Parameters.AddWithValue("@RaisonSociale", this.RaisonSociale);
        //        cmd.Parameters.AddWithValue("@BRemboursement", this.BRemboursement);
        //        cmd.Parameters.AddWithValue("@BAncien", this.BAncien);
        //        cmd.Parameters.AddWithValue("@NPiece", this.NPiece);
        //        cmd.Parameters.AddWithValue("@MontantRetenuForfaitaire", this.MontantRetenuForfaitaire);
        //        cmd.Parameters.AddWithValue("@PoidsTotal", this.PoidsTotal);
        //        cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
        //        cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);
        //        cmd.Parameters.AddWithValue("@CreePar", this.CreePar);
        //        cmd.Parameters.AddWithValue("@Exercice", this.Exercice);

        //        foreach (SqlParameter parametre in cmd.Parameters)
        //            if (parametre.Value == null)
        //                parametre.Value = DBNull.Value;

        //        using (SqlDataReader dr = cmd.ExecuteReader())
        //        {
        //            if (dr.Read())
        //            {
        //                this.NAvoir = dr["NAvoir"].ToString();
        //                this.Indice = int.Parse(dr["DernierIndice"].ToString());
        //            }
        //        }

        //        int i = 1;
        //        Facture facture = Facture.Charger(NFacture);
        //        foreach (AvoirDetail avoirDetail in AvoirDetailCollection)
        //        {
        //            if (!(this.BFinancier))
        //            {
        //                Avoir.AjouterNAvoir(avoirDetail.NAvoir, avoirDetail.NBonRetour, transaction);
        //                //if (string.IsNullOrEmpty(avoirDetail.NBonRetour))
        //                //{
        //                //    this.CreerBonRetour(facture);
        //                //}
        //            }
        //            avoirDetail.NAvoir = this.NAvoir;
        //            avoirDetail.Ordre = i++;
        //            avoirDetail.Sauvegarder(transaction);
        //        }
        //        if (!(this.BRemboursement))
        //        {
        //            Reglement reglement = new Reglement();
        //            reglement.CClient = this.CClient;
        //            reglement.CEtatReglement = VenteHelper.EtatReglement.Regle.ToString();
        //            reglement.CreePar = this.CreePar;
        //            reglement.Observation = "Avoir Financier";
        //            reglement.CVendeur = this.CVendeur;
        //            reglement.DateInsertion = this.DateInsertion;
        //            reglement.Exercice = this.Exercice;
        //            reglement.Montant = this.MontantTTC;
        //            reglement.NAvoir = this.NAvoir;
        //            reglement.PCInsertion = this.PCInsertion;
        //            reglement.RaisonSociale = this.RaisonSociale;
        //            reglement.Sauvegarder();
        //            if (!(facture.CreditFacture == 0))
        //            {
        //                PaiementClient paiement = new PaiementClient(this.NFacture, reglement.CReglement);
        //                paiement.CClient = reglement.CClient;
        //                paiement.CreePar = reglement.CreePar;
        //                paiement.DateInsertion = (DateTime)reglement.DateInsertion;
        //                paiement.MontantReglement = reglement.Montant;
        //                paiement.PCInsertion = reglement.PCInsertion;
        //                paiement.Sauvegarder();
        //            }
        //        }
        //        else
        //        {
        //        }

        //        this.SupprimerTaxeAvoirAnterieurs(transaction);
        //        foreach (AvoirTaxe avoirTaxe in AvoirTaxeCollection)
        //        {
        //            avoirTaxe.NAvoir = this.NAvoir;
        //            avoirTaxe.Sauvegarder(transaction);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public void InsererAvoirFinancier(SqlTransaction transaction)
        {
            Facture facture = Facture.Charger(NFacture, transaction);
            if (facture.ResteAvoirFinancier < this.MontantTTC)
                this.MessageAlerte = "Le Cumul Des Avoirs Financiers Dépasse Le Montant De Cette Facture";
            else
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Transaction = transaction;
                    cmd.Connection = transaction.Connection;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Avoir_Inserer";
                    cmd.Parameters.AddWithValue("@BFinancier ", this.BFinancier);
                    cmd.Parameters.AddWithValue("@CUnite", this.CUnite);
                    cmd.Parameters.AddWithValue("@CClient", this.CClient);
                    cmd.Parameters.AddWithValue("@MatriculeFiscale", this.MatriculeFiscale);
                    cmd.Parameters.AddWithValue("@Adresse", this.Adresse);
                    cmd.Parameters.AddWithValue("@NTelephone", this.NTelephone);
                    cmd.Parameters.AddWithValue("@CVendeur ", this.CVendeur);
                    cmd.Parameters.AddWithValue("@BTransfereeComptabilite ", this.BTransfereeComptabilite);
                    cmd.Parameters.AddWithValue("@DateAvoir ", this.DateAvoir);
                    cmd.Parameters.AddWithValue("@DateRemboursement", this.DateRemboursement);
                    cmd.Parameters.AddWithValue("@Etat", this.Etat);
                    cmd.Parameters.AddWithValue("@BExonoreTVA", this.BExonoreTVA);
                    cmd.Parameters.AddWithValue("@BExonoreFodec", this.BExonoreFodec);
                    cmd.Parameters.AddWithValue("@BAvanceForfaitaire", this.BAvanceForfaitaire);
                    cmd.Parameters.AddWithValue("@CNature", this.CNature);
                    cmd.Parameters.AddWithValue("@MontantHT", this.MontantHT);
                    cmd.Parameters.AddWithValue("@MontantRemise", this.MontantRemise);
                    cmd.Parameters.AddWithValue("@MontantTaxe", this.MontantTaxe);
                    cmd.Parameters.AddWithValue("@MontantTTC", this.MontantTTC);
                    cmd.Parameters.AddWithValue("@NFacture", this.NFacture);
                    cmd.Parameters.AddWithValue("@Observation", this.Observation);
                    cmd.Parameters.AddWithValue("@RaisonSociale", this.RaisonSociale);
                    cmd.Parameters.AddWithValue("@BRemboursement", this.BRemboursement);
                    cmd.Parameters.AddWithValue("@BAncien", this.BAncien);
                    cmd.Parameters.AddWithValue("@NPiece", this.NPiece);
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
                            this.NAvoir = dr["NAvoir"].ToString();
                            this.Indice = int.Parse(dr["DernierIndice"].ToString());
                        }
                    }

                    int i = 1;

                    foreach (AvoirDetail avoirDetail in AvoirDetailCollection)
                    {
                        avoirDetail.NAvoir = this.NAvoir;
                        avoirDetail.Ordre = i++;
                        avoirDetail.Sauvegarder(transaction);
                    }

                    foreach (AvoirTaxe avoirTaxe in AvoirTaxeCollection)
                    {
                        avoirTaxe.NAvoir = this.NAvoir;
                        avoirTaxe.Sauvegarder(transaction);
                    }

                    facture.ResteAvoirFinancier = facture.ResteAvoirFinancier - this.MontantTTC;
                    facture.PCModification = this.PCInsertion;
                    facture.ModifiePar = this.CreePar;
                    facture.ModifierResteAvoir(transaction);
                    ReglerAvoir(transaction, facture);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public void InsererAvoirMarchandise(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Avoir_Inserer";
                cmd.Parameters.AddWithValue("@BFinancier ", this.BFinancier);
                cmd.Parameters.AddWithValue("@CUnite", this.CUnite);
                cmd.Parameters.AddWithValue("@CClient", this.CClient);
                cmd.Parameters.AddWithValue("@MatriculeFiscale", this.MatriculeFiscale);
                cmd.Parameters.AddWithValue("@Adresse", this.Adresse);
                cmd.Parameters.AddWithValue("@NTelephone", this.NTelephone);
                cmd.Parameters.AddWithValue("@CVendeur ", this.CVendeur);
                cmd.Parameters.AddWithValue("@BTransfereeComptabilite ", this.BTransfereeComptabilite);
                cmd.Parameters.AddWithValue("@DateAvoir ", this.DateAvoir);
                cmd.Parameters.AddWithValue("@DateRemboursement", this.DateRemboursement);
                cmd.Parameters.AddWithValue("@Etat", this.Etat);
                cmd.Parameters.AddWithValue("@BExonoreTVA", this.BExonoreTVA);
                cmd.Parameters.AddWithValue("@BExonoreFodec", this.BExonoreFodec);
                cmd.Parameters.AddWithValue("@BAvanceForfaitaire", this.BAvanceForfaitaire);
                cmd.Parameters.AddWithValue("@CNature", this.CNature);
                cmd.Parameters.AddWithValue("@MontantHT", this.MontantHT);
                cmd.Parameters.AddWithValue("@MontantRemise", this.MontantRemise);
                cmd.Parameters.AddWithValue("@MontantTaxe", this.MontantTaxe);
                cmd.Parameters.AddWithValue("@MontantTTC", this.MontantTTC);
                cmd.Parameters.AddWithValue("@NFacture", this.NFacture);
                cmd.Parameters.AddWithValue("@Observation", this.Observation);
                cmd.Parameters.AddWithValue("@RaisonSociale", this.RaisonSociale);
                cmd.Parameters.AddWithValue("@BRemboursement", this.BRemboursement);
                cmd.Parameters.AddWithValue("@BAncien", this.BAncien);
                cmd.Parameters.AddWithValue("@NPiece", this.NPiece);
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
                        this.NAvoir = dr["NAvoir"].ToString();
                        this.Indice = int.Parse(dr["DernierIndice"].ToString());
                    }
                }

                int i = 1;
                bool anterieur = false;
                foreach (AvoirDetail avoirDetail in AvoirDetailCollection)
                {
                    avoirDetail.NAvoir = this.NAvoir;
                    if (!this.BAncien)
                        Avoir.AjouterNAvoir(avoirDetail.NAvoir, avoirDetail.NBonRetour, transaction);
                    avoirDetail.Ordre = i++;
                    avoirDetail.Sauvegarder(transaction);
                    if (!anterieur)
                    {
                        BonRetour bonRetour = BonRetour.Charger(avoirDetail.NBonRetour);
                        if (bonRetour.BRetourAnterieur)
                            anterieur = true;
                    }
                }

                foreach (AvoirTaxe avoirTaxe in AvoirTaxeCollection)
                {
                    avoirTaxe.NAvoir = this.NAvoir;
                    avoirTaxe.Sauvegarder(transaction);
                }
                //if (!this.BAncien && !anterieur)
                //{
                //  Facture facture = Facture.Charger(NFacture, transaction);
                //    VenteHelper.ModifierSolde(null, null, this.CClient, 0, 0, -this.MontantTTC, 0, 0, 0, transaction);

                // ReglerAvoir(transaction, facture);

                //  }
                //else
                //{
                GenererReglement(transaction);
                //  }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void InsererTrac(SqlTransaction transaction)
        {
            try
            {
                int orderAvoir = 0;
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Avoir_InsererTrac";
                cmd.Parameters.AddWithValue("@NAvoir", this.NAvoir);
                cmd.Parameters.AddWithValue("@BFinancier", this.BFinancier);
                cmd.Parameters.AddWithValue("@CUnite", this.CUnite);
                cmd.Parameters.AddWithValue("@CClient", this.CClient);
                cmd.Parameters.AddWithValue("@MatriculeFiscale", this.MatriculeFiscale);
                cmd.Parameters.AddWithValue("@Adresse", this.Adresse);
                cmd.Parameters.AddWithValue("@NTelephone", this.NTelephone);
                cmd.Parameters.AddWithValue("@CVendeur ", this.CVendeur);
                cmd.Parameters.AddWithValue("@BTransfereeComptabilite", this.BTransfereeComptabilite);
                cmd.Parameters.AddWithValue("@DateAvoir", this.DateAvoir);
                cmd.Parameters.AddWithValue("@DateRemboursement", this.DateRemboursement);
                cmd.Parameters.AddWithValue("@Etat", this.Etat);
                cmd.Parameters.AddWithValue("@BExonoreTVA", this.BExonoreTVA);
                cmd.Parameters.AddWithValue("@BExonoreFodec", this.BExonoreFodec);
                cmd.Parameters.AddWithValue("@BAvanceForfaitaire", this.BAvanceForfaitaire);
                cmd.Parameters.AddWithValue("@CNature", this.CNature);
                cmd.Parameters.AddWithValue("@MontantHT", this.MontantHT);
                cmd.Parameters.AddWithValue("@MontantRemise", this.MontantRemise);
                cmd.Parameters.AddWithValue("@MontantTaxe", this.MontantTaxe);
                cmd.Parameters.AddWithValue("@MontantTTC", this.MontantTTC);
                cmd.Parameters.AddWithValue("@NFacture", this.NFacture);
                cmd.Parameters.AddWithValue("@Observation", this.Observation);
                cmd.Parameters.AddWithValue("@RaisonSociale", this.RaisonSociale);
                cmd.Parameters.AddWithValue("@BRemboursement", this.BRemboursement);
                cmd.Parameters.AddWithValue("@BAncien", this.BAncien);
                cmd.Parameters.AddWithValue("@NPiece", this.NPiece);
                cmd.Parameters.AddWithValue("@MontantRetenuForfaitaire", this.MontantRetenuForfaitaire);
                cmd.Parameters.AddWithValue("@PoidsTotal", this.PoidsTotal);
                cmd.Parameters.AddWithValue("@DateInsertion", this.DateInsertion);
                cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);
                cmd.Parameters.AddWithValue("@CreePar", this.CreePar);
                cmd.Parameters.AddWithValue("@DateModification", this.DateModification);
                cmd.Parameters.AddWithValue("@PCModification", this.PCModification);
                cmd.Parameters.AddWithValue("@ModifiePar", this.ModifiePar);
                cmd.Parameters.AddWithValue("@Indice", this.Indice);
                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        orderAvoir = int.Parse(dr["OrderAvoir"].ToString());
                    }
                }

               
                foreach (AvoirDetail avoirDetail in AvoirDetailCollection)
                {
                    avoirDetail.SauvegarderTrac(orderAvoir,transaction);
                }

                foreach (AvoirTaxe avoirTaxe in AvoirTaxeCollection)
                {
                    avoirTaxe.SauvegarderTrac(orderAvoir,transaction);
                }
               
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ReglerAvoir(SqlTransaction transaction, Facture facture)
        {
            if (!(this.BRemboursement))
            {
                Reglement reglement = new Reglement();
                reglement.CClient = this.CClient;
                reglement.CreePar = this.CreePar;
                reglement.CVendeur = this.CVendeur;
                reglement.DateInsertion = DateTime.Now;
                reglement.Exercice = this.Exercice;
                reglement.Montant = this.MontantTTC;
                reglement.ResteReglement = reglement.Montant;
                reglement.NAvoir = this.NAvoir;
                reglement.PCInsertion = this.PCInsertion;
                reglement.RaisonSociale = this.RaisonSociale;

                if (facture.BAncienneFacture)
                {
                    reglement.CEtatReglement = VenteHelper.EtatReglement.ENATTENTE.ToString();
                    reglement.CTypeReglement = VenteHelper.TypeReglement.AVRAVC.ToString();

                    this.Etat = VenteHelper.EtatAvoir.NREMB.ToString();
                    this.PCModification = this.PCModification;
                    this.ModifiePar = this.ModifiePar;
                    this.ModifierEtat(transaction);
                }

                PaiementClient paiement = null;
                if ((facture.CreditFacture > 0) && (facture.CreditFacture >= this.MontantTTC))
                {
                    paiement = new PaiementClient();
                    paiement.CClient = reglement.CClient;
                    paiement.CreePar = reglement.CreePar;
                    paiement.PCInsertion = reglement.PCInsertion;
                    this.Etat = VenteHelper.EtatAvoir.AVR_CON.ToString();
                    reglement.CEtatReglement = VenteHelper.EtatReglement.ASSOCIE.ToString();
                    reglement.CTypeReglement = VenteHelper.TypeReglement.AVR.ToString();
                }
                else
                {
                    if (facture.CreditFacture > 0)
                    {
                        paiement = new PaiementClient();
                        paiement.CClient = reglement.CClient;
                        paiement.CreePar = reglement.CreePar;
                        paiement.DateInsertion = DateTime.Now;
                        paiement.PCInsertion = reglement.PCInsertion;
                        this.Etat = VenteHelper.EtatAvoir.AVR_CON.ToString();
                        reglement.CEtatReglement = VenteHelper.EtatReglement.ASSOCIE.ToString();
                    }
                    else
                    {
                        this.Etat = VenteHelper.EtatAvoir.NREMB.ToString();
                        reglement.CEtatReglement = VenteHelper.EtatReglement.ENATTENTE.ToString();
                    }
                    this.Etat = VenteHelper.EtatAvoir.AVR_NCON.ToString();
                    reglement.CEtatReglement = VenteHelper.EtatReglement.ENATTENTE.ToString();
                    reglement.CTypeReglement = VenteHelper.TypeReglement.AVRAVC.ToString();
                }

                this.PCModification = this.PCModification;
                this.ModifiePar = this.ModifiePar;
                this.ModifierEtat(transaction);

                reglement.ObjetReglement = "RAV";
                if (this.BFinancier)
                    reglement.Observation = "REGLEMENT D'ORIGINE AVOIR FINANCIER NUMERO " + this.NAvoir;
                else
                    reglement.Observation = "REGLEMENT D'ORIGINE AVOIR MARCHANDISE NUMERO " + this.NAvoir;
                reglement.DateEmission = (DateTime)this.DateAvoir;

                reglement.Inserer(transaction);
                if (paiement != null)
                {
                    paiement.CReglement = reglement.CReglement;
                    paiement.NFacture = this.NFacture;
                    paiement.Sauvegarder(transaction);
                }
            }
        }

        public void GenererReglement(SqlTransaction transaction)
        {
            Reglement reglement = new Reglement();
            reglement.CClient = this.CClient;
            reglement.CreePar = this.CreePar;
            reglement.CVendeur = this.CVendeur;
            reglement.DateInsertion = DateTime.Now;
            reglement.Exercice = this.Exercice;
            reglement.Montant = this.MontantTTC;
            reglement.ResteReglement = reglement.Montant;
            reglement.NAvoir = this.NAvoir;
            reglement.PCInsertion = this.PCInsertion;
            reglement.RaisonSociale = this.RaisonSociale;

            reglement.CEtatReglement = VenteHelper.EtatReglement.ENATTENTE.ToString();
            reglement.CTypeReglement = VenteHelper.TypeReglement.AVRAVC.ToString();
            reglement.DateEcheance = DateTime.Now;
            this.Etat = VenteHelper.EtatAvoir.NREMB.ToString();
            this.PCModification = this.PCModification;
            this.ModifiePar = this.ModifiePar;
            this.ModifierEtat(transaction);
            //this.Etat = VenteHelper.EtatAvoir.NREMB.ToString();
            //reglement.CEtatReglement = VenteHelper.EtatReglement.ENATTENTE.ToString();
            //reglement.CTypeReglement = VenteHelper.TypeReglement.AVRAVC.ToString();
            //this.PCModification = this.PCModification;
            //this.ModifiePar = this.ModifiePar;
            //this.ModifierEtat(transaction);

            reglement.ObjetReglement = "RAV";
            if (this.BFinancier)
                reglement.Observation = "REGLEMENT D'ORIGINE AVOIR FINANCIER NUMERO " + this.NAvoir;
            else
                reglement.Observation = "REGLEMENT D'ORIGINE AVOIR MARCHANDISE NUMERO " + this.NAvoir;
            reglement.DateEmission = (DateTime)this.DateAvoir;
            reglement.Inserer(transaction);
        }

        public static void AjouterNAvoir(string nAvoir, string nBonRetour, SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "BonRetour_AjouterNAvoir";
                cmd.Parameters.AddWithValue("@NAvoir", nAvoir);
                cmd.Parameters.AddWithValue("@NBonRetour", nBonRetour);

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

        //private void CreerBonRetour(Facture facture)
        //{
        //    FactureDetailCollection collection = new FactureDetailCollection();
        //    foreach (FactureDetail factureDetail in facture.FactureDetailCollection)
        //        collection.Add(factureDetail);
        //    foreach (FactureDetail factureDetail in facture.FactureDetailCollection)
        //    {
        //        FactureDetail detail = collection.RecupererFactureDetail(factureDetail.NDevis);

        //        BonRetour bonRetour = new BonRetour();
        //        bonRetour.BExonoreFodec = facture.BExonoreFodec;
        //        bonRetour.BExonoreTVA = this.BExonoreTVA;
        //        bonRetour.CNature = this.CNature;
        //        bonRetour.BTransfertAvoir = true;
        //        bonRetour.CClient = this.CClient;
        //       bonRetour.CUnite = this.CUnite;
        //        bonRetour.CreePar = this.CreePar;
        //        bonRetour.CVendeur = this.CVendeur;
        //        bonRetour.DateInsertion = this.DateInsertion;
        //        bonRetour.DateRetour = this.DateAvoir;
        //        bonRetour.Exercice = this.Exercice;
        //        bonRetour.MatriculeFiscale = this.MatriculeFiscale;
        //        bonRetour.MontantHT = this.MontantHT;
        //        bonRetour.MontantRemise = this.MontantRemise;
        //        bonRetour.MontantRetenuForfaitaire = this.MontantRetenuForfaitaire;
        //        bonRetour.MontantTaxe = this.MontantTaxe;
        //        bonRetour.MontantTTC = this.MontantTTC;
        //        bonRetour.NAvoir = this.NAvoir;
        //        bonRetour.NDevis = factureDetail.NDevis;
        //        bonRetour.Observation = this.Observation;
        //        bonRetour.PCInsertion = this.PCInsertion;
        //        bonRetour.RaisonSociale = this.RaisonSociale;
        //         Devis livraison = Devis.Charger(factureDetail.NDevis);
        //         int i = 0;
        //        while (!(detail == null))
        //        {
        //            DevisDetail livraisonDetail = livraison.DevisDetailCollection.RecupererDevisDetail(detail.NDevis,detail.CArticle);
        //            BonRetourDetail bonRetourDetail = new BonRetourDetail();
        //            bonRetourDetail.CArticle = livraisonDetail.CArticle;
        //            bonRetourDetail.CUnite = livraisonDetail.CUnite;
        //            bonRetourDetail.CTaxe = livraisonDetail.CTaxe;
        //            bonRetourDetail.CUnite = livraisonDetail.CUnite;
        //            bonRetourDetail.Epaisseur = livraisonDetail.Epaisseur;
        //            bonRetourDetail.Largeur = livraisonDetail.Largeur;
        //            bonRetourDetail.LibArticle = livraisonDetail.LibArticle;
        //            bonRetourDetail.Longueur = livraisonDetail.Longueur;
        //            bonRetourDetail.MontantNet = livraisonDetail.MontantNet;
        //            bonRetourDetail.MontantTaxe = livraisonDetail.MontantTaxe;
        //            bonRetourDetail.Ordre = i++;
        //            bonRetourDetail.OrdreDevis = livraisonDetail.Ordre;
        //            bonRetourDetail.PourcentageFodec = livraisonDetail.PourcentageFodec;
        //            bonRetourDetail.PourcentageRemise = livraisonDetail.PourcentageRemise;
        //            bonRetourDetail.PrixHT = livraisonDetail.PrixHT;
        //            bonRetourDetail.PrixRevient = livraisonDetail.PrixRevient;
        //            bonRetourDetail.Quantite = livraisonDetail.Quantite;
        //            bonRetourDetail.Remise1 = livraisonDetail.Remise1;
        //            bonRetourDetail.Remise2 = livraisonDetail.Remise2;
        //            bonRetourDetail.TauxTVA = livraisonDetail.TauxTVA;
        //            bonRetour.BonRetourDetailCollection.Add(bonRetourDetail);

        //            collection.Remove(detail);
        //            detail = collection.RecupererFactureDetail(factureDetail.NDevis);

        //        }
        //        foreach (DevisTaxe taxeBL in livraison.DevisTaxeCollection)
        //        {
        //            BonRetourTaxe taxeBR = new BonRetourTaxe();
        //            taxeBR.Assiette = taxeBL.Assiette;
        //            taxeBR.BExonoreFodec = taxeBL.BExonoreFodec;
        //            taxeBR.BExonoreTVA = taxeBL.BExonoreTVA;
        //            taxeBR.CNature = taxeBL.CNature;
        //            taxeBR.CTaxe = taxeBL.CTaxe;
        //            taxeBR.MontantTaxe = taxeBL.MontantTaxe;
        //            taxeBR.TauxTVA = taxeBL.TauxTVA;
        //            bonRetour.BonRetourTaxeCollection.Add(taxeBR);
        //        }
        //        bonRetour.Inserer();
        //    }

        //}

        public void Modifier()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    Modifier(transaction);
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
                cmd.CommandText = "Avoir_Modifier";
                cmd.Parameters.AddWithValue("@NAvoir", this.NAvoir);
                cmd.Parameters.AddWithValue("@BFinancier ", this.BFinancier);
                cmd.Parameters.AddWithValue("@CUnite", this.CUnite);
                cmd.Parameters.AddWithValue("@CClient", this.CClient);
                cmd.Parameters.AddWithValue("@MatriculeFiscale", this.MatriculeFiscale);
                cmd.Parameters.AddWithValue("@Adresse", this.Adresse);
                cmd.Parameters.AddWithValue("@NTelephone", this.NTelephone);
                cmd.Parameters.AddWithValue("@CVendeur ", this.CVendeur);
                cmd.Parameters.AddWithValue("@BTransfereeComptabilite ", this.BTransfereeComptabilite);
                cmd.Parameters.AddWithValue("@DateAvoir ", this.DateAvoir);
                cmd.Parameters.AddWithValue("@DateRemboursement", this.DateRemboursement);
                cmd.Parameters.AddWithValue("@Etat", this.Etat);
                cmd.Parameters.AddWithValue("@BExonoreTVA", this.BExonoreTVA);
                cmd.Parameters.AddWithValue("@BExonoreFodec", this.BExonoreFodec);
                cmd.Parameters.AddWithValue("@BAvanceForfaitaire", this.BAvanceForfaitaire);
                cmd.Parameters.AddWithValue("@CNature", this.CNature);
                cmd.Parameters.AddWithValue("@MontantHT", this.MontantHT);
                cmd.Parameters.AddWithValue("@MontantRemise", this.MontantRemise);
                cmd.Parameters.AddWithValue("@MontantTaxe", this.MontantTaxe);
                cmd.Parameters.AddWithValue("@MontantTTC", this.MontantTTC);
                cmd.Parameters.AddWithValue("@NFacture", this.NFacture);
                cmd.Parameters.AddWithValue("@Observation", this.Observation);
                cmd.Parameters.AddWithValue("@RaisonSociale", this.RaisonSociale);
                cmd.Parameters.AddWithValue("@BRemboursement", this.BRemboursement);
                cmd.Parameters.AddWithValue("@BAncien", this.BAncien);
                cmd.Parameters.AddWithValue("@NPiece", this.NPiece);
                cmd.Parameters.AddWithValue("@MontantRetenuForfaitaire", this.MontantRetenuForfaitaire);
                cmd.Parameters.AddWithValue("@PoidsTotal", this.PoidsTotal);
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

                this.SupprimerDetailAvoirAnterieurs(transaction);
                this.SupprimerTaxeAvoirAnterieurs(transaction);
                int i = 0;
                foreach (AvoirDetail avoirDetail in AvoirDetailCollection)
                {
                    avoirDetail.NAvoir = this.NAvoir;
                    avoirDetail.Ordre = i++;
                    avoirDetail.Sauvegarder(transaction);
                }

                foreach (AvoirTaxe avoirTaxe in AvoirTaxeCollection)
                {
                    avoirTaxe.NAvoir = this.NAvoir;
                    avoirTaxe.Sauvegarder(transaction);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ModifierEtat(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Avoir_ModifierEtat";
                cmd.Parameters.AddWithValue("@NAvoir", this.NAvoir);
                cmd.Parameters.AddWithValue("@Etat", this.Etat);
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

        private void SupprimerDetailAvoirAnterieurs(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Avoir_SupprimerDetails";

                cmd.Parameters.AddWithValue("@NAvoir", this.NAvoir);

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

        private void SupprimerTaxeAvoirAnterieurs(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Avoir_SupprimerTaxes";

                cmd.Parameters.AddWithValue("@NAvoir", this.NAvoir);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModifierRemb(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Avoir_ModifierRemb";
                cmd.Parameters.AddWithValue("@NAvoir", this.NAvoir);
                cmd.Parameters.AddWithValue("@Etat", this.Etat);
                cmd.Parameters.AddWithValue("@DateRemboursement", DateRemboursement);
                cmd.Parameters.AddWithValue("@BRemboursement", this.BRemboursement);

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

        public static string RecupererNouveauNAvoir(string exercice, out int indice)
        {
            string nAvoir = string.Empty;
            indice = 0;

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                var cmd2 = new SqlCommand();
                cmd2.Connection = cn;
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.CommandText = "Avoir_RecupererNouveauNumero";
                cmd2.Parameters.AddWithValue("@Exercice", exercice);
                SqlDataReader dr = cmd2.ExecuteReader();
                if (dr.Read())
                {
                    nAvoir = dr["NAvoir"].ToString();
                    indice = int.Parse(dr["DernierIndice"].ToString());
                }
                dr.Close();
            }

            return nAvoir;
        }

        public static string RecupererNouveauNAvoir(string exercice)
        {
            int indice = 0;
            return Avoir.RecupererNouveauNAvoir(exercice, out indice);
        }

        public static Avoir Charger(string nAvoir)
        {
            Avoir avoir = null;
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
                    cmd.CommandText = "Avoir_Charger";
                    cmd.Parameters.AddWithValue("@NAvoir", nAvoir);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            avoir = new Avoir();
                            avoir.NAvoir = dr["NAvoir"].ToString();

                            if (dr["CUnite"] != DBNull.Value)
                                avoir.CUnite = dr["CUnite"].ToString();
                            if (dr["BFinancier"] != DBNull.Value)
                                avoir.BFinancier = bool.Parse(dr["BFinancier"].ToString());
                            if (dr["CClient"] != DBNull.Value)
                                avoir.CClient = dr["CClient"].ToString();
                            if (dr["MatriculeFiscale"] != DBNull.Value)
                                avoir.MatriculeFiscale = dr["MatriculeFiscale"].ToString();
                            if (dr["Adresse"] != DBNull.Value)
                                avoir.Adresse = dr["Adresse"].ToString();
                            if (dr["NTelephone"] != DBNull.Value)
                                avoir.NTelephone = dr["NTelephone"].ToString();
                            if (dr["CVendeur"] != DBNull.Value)
                                avoir.CVendeur = int.Parse(dr["CVendeur"].ToString());
                            if (dr["BTransfereeComptabilite"] != DBNull.Value)
                                avoir.BTransfereeComptabilite = bool.Parse(dr["BTransfereeComptabilite"].ToString());
                            if (dr["DateAvoir"] != DBNull.Value)
                                avoir.DateAvoir = DateTime.Parse(dr["DateAvoir"].ToString());
                            if (dr["DateRemboursement"] != DBNull.Value)
                                avoir.DateRemboursement = DateTime.Parse(dr["DateRemboursement"].ToString());
                            if (dr["Etat"] != DBNull.Value)
                                avoir.Etat = dr["Etat"].ToString();
                            if (dr["BExonoreTVA"] != DBNull.Value)
                                avoir.BExonoreTVA = bool.Parse(dr["BExonoreTVA"].ToString());
                            if (dr["BExonoreFodec"] != DBNull.Value)
                                avoir.BExonoreFodec = bool.Parse(dr["BExonoreFodec"].ToString());
                            if (dr["BAvanceForfaitaire"] != DBNull.Value)
                                avoir.BAvanceForfaitaire = bool.Parse(dr["BAvanceForfaitaire"].ToString());
                            if (dr["CNature"] != DBNull.Value)
                                avoir.CNature = int.Parse(dr["CNature"].ToString());
                            if (dr["MontantHT"] != DBNull.Value)
                                avoir.MontantHT = decimal.Parse(dr["MontantHT"].ToString());
                            if (dr["MontantRemise"] != DBNull.Value)
                                avoir.MontantRemise = decimal.Parse(dr["MontantRemise"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                avoir.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["MontantTTC"] != DBNull.Value)
                                avoir.MontantTTC = decimal.Parse(dr["MontantTTC"].ToString());
                            if (dr["NFacture"] != DBNull.Value)
                                avoir.NFacture = dr["NFacture"].ToString();
                            if (dr["Observation"] != DBNull.Value)
                                avoir.Observation = dr["Observation"].ToString();
                            if (dr["RaisonSociale"] != DBNull.Value)
                                avoir.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["Indice"] != DBNull.Value)
                                avoir.Indice = int.Parse(dr["Indice"].ToString());
                            if (dr["BRemboursement"] != DBNull.Value)
                                avoir.BRemboursement = bool.Parse(dr["BRemboursement"].ToString());
                            if (dr["BAncien"] != DBNull.Value)
                                avoir.BAncien = bool.Parse(dr["BAncien"].ToString());
                            if (dr["NPiece"] != DBNull.Value)
                                avoir.NPiece = dr["NPiece"].ToString();
                            if (dr["MontantRetenuForfaitaire"] != DBNull.Value)
                                avoir.MontantRetenuForfaitaire = decimal.Parse(dr["MontantRetenuForfaitaire"].ToString());
                            if (dr["DateInsertion"] != DBNull.Value)
                                avoir.DateInsertion = DateTime.Parse(dr["DateInsertion"].ToString());
                            if (dr["DateModification"] != DBNull.Value)
                                avoir.DateModification = DateTime.Parse(dr["DateModification"].ToString());
                            if (dr["CreePar"] != DBNull.Value)
                                avoir.CreePar = int.Parse(dr["CreePar"].ToString());
                            if (dr["ModifiePar"] != DBNull.Value)
                                avoir.ModifiePar = int.Parse(dr["ModifiePar"].ToString());
                            if (dr["PCInsertion"] != DBNull.Value)
                                avoir.PCInsertion = dr["PCInsertion"].ToString();
                            if (dr["PCModification"] != DBNull.Value)
                                avoir.PCModification = dr["PCModification"].ToString();
                            if (dr["PoidsTotal"] != DBNull.Value) //correction 05/08/2014
                                avoir.PoidsTotal = decimal.Parse(dr["PoidsTotal"].ToString());
                            avoir.AvoirDetailCollection = AvoirDetailCollection.Charger(avoir.NAvoir);
                            avoir.AvoirTaxeCollection = AvoirTaxeCollection.Charger(avoir.NAvoir);
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return (avoir);
            }
        }
    }

    public class AvoirCollection : List<Avoir>
    {
        public AvoirCollection()
        {
        }

        public static AvoirCollection Charger()
        {
            AvoirCollection collection = new AvoirCollection();
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
                    cmd.CommandText = "Avoir_Charger";
                    cmd.Parameters.AddWithValue("@NAvoir", DBNull.Value);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Avoir avoir = new Avoir();
                            avoir.NAvoir = dr["NAvoir"].ToString();

                            if (dr["CUnite"] != DBNull.Value)
                                avoir.CUnite = dr["CUnite"].ToString();
                            if (dr["BFinancier"] != DBNull.Value)
                                avoir.BFinancier = bool.Parse(dr["BFinancier"].ToString());
                            if (dr["CClient"] != DBNull.Value)
                                avoir.CClient = dr["CClient"].ToString();
                            if (dr["MatriculeFiscale"] != DBNull.Value)
                                avoir.MatriculeFiscale = dr["MatriculeFiscale"].ToString();
                            if (dr["Adresse"] != DBNull.Value)
                                avoir.Adresse = dr["Adresse"].ToString();
                            if (dr["NTelephone"] != DBNull.Value)
                                avoir.NTelephone = dr["NTelephone"].ToString();
                            if (dr["CVendeur"] != DBNull.Value)
                                avoir.CVendeur = int.Parse(dr["CVendeur"].ToString());
                            if (dr["BTransfereeComptabilite"] != DBNull.Value)
                                avoir.BTransfereeComptabilite = bool.Parse(dr["BTransfereeComptabilite"].ToString());
                            if (dr["DateAvoir"] != DBNull.Value)
                                avoir.DateAvoir = DateTime.Parse(dr["DateAvoir"].ToString());
                            if (dr["DateRemboursement"] != DBNull.Value)
                                avoir.DateRemboursement = DateTime.Parse(dr["DateRemboursement"].ToString());
                            if (dr["Etat"] != DBNull.Value)
                                avoir.Etat = dr["Etat"].ToString();
                            if (dr["BExonoreTVA"] != DBNull.Value)
                                avoir.BExonoreTVA = bool.Parse(dr["BExonoreTVA"].ToString());
                            if (dr["BExonoreFodec"] != DBNull.Value)
                                avoir.BExonoreFodec = bool.Parse(dr["BExonoreFodec"].ToString());
                            if (dr["BAvanceForfaitaire"] != DBNull.Value)
                                avoir.BAvanceForfaitaire = bool.Parse(dr["BAvanceForfaitaire"].ToString());
                            if (dr["CNature"] != DBNull.Value)
                                avoir.CNature = int.Parse(dr["CNature"].ToString());
                            if (dr["MontantHT"] != DBNull.Value)
                                avoir.MontantHT = decimal.Parse(dr["MontantHT"].ToString());
                            if (dr["MontantRemise"] != DBNull.Value)
                                avoir.MontantRemise = decimal.Parse(dr["MontantRemise"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                avoir.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["MontantTTC"] != DBNull.Value)
                                avoir.MontantTTC = decimal.Parse(dr["MontantTTC"].ToString());
                            if (dr["NFacture"] != DBNull.Value)
                                avoir.NFacture = dr["NFacture"].ToString();
                            if (dr["Observation"] != DBNull.Value)
                                avoir.Observation = dr["Observation"].ToString();
                            if (dr["RaisonSociale"] != DBNull.Value)
                                avoir.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["Indice"] != DBNull.Value)
                                avoir.Indice = int.Parse(dr["Indice"].ToString());
                            if (dr["BRemboursement"] != DBNull.Value)
                                avoir.BRemboursement = bool.Parse(dr["BRemboursement"].ToString());
                            if (dr["BAncien"] != DBNull.Value)
                                avoir.BAncien = bool.Parse(dr["BAncien"].ToString());
                            if (dr["NPiece"] != DBNull.Value)
                                avoir.NPiece = dr["NPiece"].ToString();
                            if (dr["MontantRetenuForfaitaire"] != DBNull.Value)
                                avoir.MontantRetenuForfaitaire = decimal.Parse(dr["MontantRetenuForfaitaire"].ToString());
                            if (dr["DateInsertion"] != DBNull.Value)
                                avoir.DateInsertion = DateTime.Parse(dr["DateInsertion"].ToString());
                            if (dr["DateModification"] != DBNull.Value)
                                avoir.DateModification = DateTime.Parse(dr["DateModification"].ToString());
                            if (dr["CreePar"] != DBNull.Value)
                                avoir.CreePar = int.Parse(dr["CreePar"].ToString());
                            if (dr["ModifiePar"] != DBNull.Value)
                                avoir.ModifiePar = int.Parse(dr["ModifiePar"].ToString());
                            if (dr["PCInsertion"] != DBNull.Value)
                                avoir.PCInsertion = dr["PCInsertion"].ToString();
                            if (dr["PCModification"] != DBNull.Value)
                                avoir.PCModification = dr["PCModification"].ToString();
                            if (dr["PoidsTotal"] != DBNull.Value)
                                avoir.PoidsTotal = decimal.Parse(dr["PoidsTotal"].ToString());
                            avoir.AvoirDetailCollection = AvoirDetailCollection.Charger(avoir.NAvoir);
                            avoir.AvoirTaxeCollection = AvoirTaxeCollection.Charger(avoir.NAvoir);

                            collection.Add(avoir);
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

        public static AvoirCollection ChargerAvoirFacture(string nFacture, SqlTransaction transaction)
        {
            AvoirCollection collection = new AvoirCollection();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Facture_ChargerAvoir";
                cmd.Parameters.AddWithValue("@NFacture", nFacture);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Avoir avoir = new Avoir();
                        avoir.NAvoir = dr["NAvoir"].ToString();

                        if (dr["CUnite"] != DBNull.Value)
                            avoir.CUnite = dr["CUnite"].ToString();
                        if (dr["BFinancier"] != DBNull.Value)
                            avoir.BFinancier = bool.Parse(dr["BFinancier"].ToString());
                        if (dr["CClient"] != DBNull.Value)
                            avoir.CClient = dr["CClient"].ToString();
                        if (dr["MatriculeFiscale"] != DBNull.Value)
                            avoir.MatriculeFiscale = dr["MatriculeFiscale"].ToString();
                        if (dr["Adresse"] != DBNull.Value)
                            avoir.Adresse = dr["Adresse"].ToString();
                        if (dr["NTelephone"] != DBNull.Value)
                            avoir.NTelephone = dr["NTelephone"].ToString();
                        if (dr["CVendeur"] != DBNull.Value)
                            avoir.CVendeur = int.Parse(dr["CVendeur"].ToString());
                        if (dr["BTransfereeComptabilite"] != DBNull.Value)
                            avoir.BTransfereeComptabilite = bool.Parse(dr["BTransfereeComptabilite"].ToString());
                        if (dr["DateAvoir"] != DBNull.Value)
                            avoir.DateAvoir = DateTime.Parse(dr["DateAvoir"].ToString());
                        if (dr["DateRemboursement"] != DBNull.Value)
                            avoir.DateRemboursement = DateTime.Parse(dr["DateRemboursement"].ToString());
                        if (dr["Etat"] != DBNull.Value)
                            avoir.Etat = dr["Etat"].ToString();
                        if (dr["BExonoreTVA"] != DBNull.Value)
                            avoir.BExonoreTVA = bool.Parse(dr["BExonoreTVA"].ToString());
                        if (dr["BExonoreFodec"] != DBNull.Value)
                            avoir.BExonoreFodec = bool.Parse(dr["BExonoreFodec"].ToString());
                        if (dr["BAvanceForfaitaire"] != DBNull.Value)
                            avoir.BAvanceForfaitaire = bool.Parse(dr["BAvanceForfaitaire"].ToString());
                        if (dr["CNature"] != DBNull.Value)
                            avoir.CNature = int.Parse(dr["CNature"].ToString());
                        if (dr["MontantHT"] != DBNull.Value)
                            avoir.MontantHT = decimal.Parse(dr["MontantHT"].ToString());
                        if (dr["MontantRemise"] != DBNull.Value)
                            avoir.MontantRemise = decimal.Parse(dr["MontantRemise"].ToString());
                        if (dr["MontantTaxe"] != DBNull.Value)
                            avoir.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                        if (dr["MontantTTC"] != DBNull.Value)
                            avoir.MontantTTC = decimal.Parse(dr["MontantTTC"].ToString());
                        if (dr["NFacture"] != DBNull.Value)
                            avoir.NFacture = dr["NFacture"].ToString();
                        if (dr["Observation"] != DBNull.Value)
                            avoir.Observation = dr["Observation"].ToString();
                        if (dr["RaisonSociale"] != DBNull.Value)
                            avoir.RaisonSociale = dr["RaisonSociale"].ToString();
                        if (dr["Indice"] != DBNull.Value)
                            avoir.Indice = int.Parse(dr["Indice"].ToString());
                        if (dr["BRemboursement"] != DBNull.Value)
                            avoir.BRemboursement = bool.Parse(dr["BRemboursement"].ToString());
                        if (dr["BAncien"] != DBNull.Value)
                            avoir.BAncien = bool.Parse(dr["BAncien"].ToString());
                        if (dr["NPiece"] != DBNull.Value)
                            avoir.NPiece = dr["NPiece"].ToString();
                        if (dr["MontantRetenuForfaitaire"] != DBNull.Value)
                            avoir.MontantRetenuForfaitaire = decimal.Parse(dr["MontantRetenuForfaitaire"].ToString());
                        if (dr["DateInsertion"] != DBNull.Value)
                            avoir.DateInsertion = DateTime.Parse(dr["DateInsertion"].ToString());
                        if (dr["DateModification"] != DBNull.Value)
                            avoir.DateModification = DateTime.Parse(dr["DateModification"].ToString());
                        if (dr["CreePar"] != DBNull.Value)
                            avoir.CreePar = int.Parse(dr["CreePar"].ToString());
                        if (dr["ModifiePar"] != DBNull.Value)
                            avoir.ModifiePar = int.Parse(dr["ModifiePar"].ToString());
                        if (dr["PCInsertion"] != DBNull.Value)
                            avoir.PCInsertion = dr["PCInsertion"].ToString();
                        if (dr["PCModification"] != DBNull.Value)
                            avoir.PCModification = dr["PCModification"].ToString();

                        avoir.AvoirDetailCollection = AvoirDetailCollection.Charger(avoir.NAvoir);
                        avoir.AvoirTaxeCollection = AvoirTaxeCollection.Charger(avoir.NAvoir);

                        collection.Add(avoir);
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