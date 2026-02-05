using CST.LePoint.Stock.Metier;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml.Serialization;

namespace CST.LePoint.Vente.Metier
{
    public class BonRetour
    {
        #region Proriétès

        [XmlAttribute("NBonRetour")]
        [Bindable(true)]
        public string NBonRetour { get; set; }

        [XmlAttribute("CClient")]
        [Bindable(true)]
        public string CClient { get; set; }

        [XmlAttribute("CUnite")]
        [Bindable(true)]
        public string CUnite { get; set; }

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

        [XmlAttribute("DateRetour")]
        [Bindable(true)]
        public DateTime? DateRetour { get; set; }

        [XmlAttribute("BDefectueux")]
        [Bindable(true)]
        public bool BDefectueux { get; set; }

        [XmlAttribute("BExonoreFodec")]
        [Bindable(true)]
        public bool BExonoreFodec { get; set; }

        [XmlAttribute("BExonoreTVA")]
        [Bindable(true)]
        public bool BExonoreTVA { get; set; }

        [XmlAttribute("BAvanceForfaitaire")]
        [Bindable(true)]
        public bool BAvanceForfaitaire { get; set; }

        [XmlAttribute("BExport")]
        [Bindable(true)]
        public bool BExport { get; set; }

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

        [XmlAttribute("NAvoir")]
        [Bindable(true)]
        public string NAvoir { get; set; }

        [XmlAttribute("NBonLivraison")]
        [Bindable(true)]
        public string NBonLivraison { get; set; }

        [XmlAttribute("Observation")]
        [Bindable(true)]
        public string Observation { get; set; }

        [XmlAttribute("RaisonSociale")]
        [Bindable(true)]
        public string RaisonSociale { get; set; }

        [XmlAttribute("MontantRetenuForfaitaire")]
        [Bindable(true)]
        public decimal MontantRetenuForfaitaire { get; set; }

        [XmlAttribute("Indice")]
        [Bindable(true)]
        public int Indice { get; set; }

        [XmlAttribute("TypeRetour")]
        [Bindable(true)]
        public bool TypeRetour { get; set; }

        [XmlAttribute("BTransfertAvoir")]
        [Bindable(true)]
        public bool BTransfertAvoir { get; set; }

        [XmlAttribute("BRetourAnterieur")]
        [Bindable(true)]
        public bool BRetourAnterieur { get; set; }

        [XmlAttribute("NFactureAnterieur")]
        [Bindable(true)]
        public string NFactureAnterieur { get; set; }

        [XmlAttribute("CMission")]
        [Bindable(true)]
        public string CMission { get; set; }

        [XmlAttribute("BEchantillon")]
        [Bindable(true)]
        public bool BEchantillon { get; set; }

        [XmlAttribute("Reference")]
        [Bindable(true)]
        public string Reference { get; set; }

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

        public BonRetourDetailCollection BonRetourDetailCollection;
        public BonRetourTaxeCollection BonRetourTaxeCollection;

        #endregion Proriétès

        public BonRetour()
        {
            this.NBonRetour = string.Empty;
            this.BonRetourDetailCollection = new BonRetourDetailCollection();
            this.BonRetourTaxeCollection = new BonRetourTaxeCollection();
        }

        public void Inserer()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    if (!BRetourAnterieur)
                        Inserer(transaction);
                    else
                        InsererBonRetourAnterieur(transaction);
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
            decimal quantiteHistoriqueBL = 0;
            BonLivraison bonLivraison = BonLivraison.Charger(this.NBonLivraison);
            if (!(bonLivraison.BRetour))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Transaction = transaction;
                    cmd.Connection = transaction.Connection;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "BonRetour_Inserer";
                    cmd.Parameters.AddWithValue("@CUnite", this.CUnite);
                    cmd.Parameters.AddWithValue("@CClient", this.CClient);

                    cmd.Parameters.AddWithValue("@BExonoreTVA", this.BExonoreTVA);
                    cmd.Parameters.AddWithValue("@BAvanceForfaitaire", this.BAvanceForfaitaire);
                    cmd.Parameters.AddWithValue("@BEchantillon ", this.BEchantillon);
                    cmd.Parameters.AddWithValue("@BDefectueux", this.BDefectueux);
                    cmd.Parameters.AddWithValue("@BExport", this.BExport);
                    cmd.Parameters.AddWithValue("@BTransfertAvoir", this.BTransfertAvoir);
                    cmd.Parameters.AddWithValue("@BRetourAnterieur", this.BRetourAnterieur);
                    cmd.Parameters.AddWithValue("@BExonoreFodec", this.BExonoreFodec);

                    cmd.Parameters.AddWithValue("@CVendeur ", this.CVendeur);
                    cmd.Parameters.AddWithValue("@DateRetour ", this.DateRetour);
                    cmd.Parameters.AddWithValue("@NFactureAnterieur", this.NFactureAnterieur);
                    cmd.Parameters.AddWithValue("@MatriculeFiscale", this.MatriculeFiscale);
                    cmd.Parameters.AddWithValue("@Adresse", this.Adresse);
                    cmd.Parameters.AddWithValue("@NTelephone", this.NTelephone);
                    cmd.Parameters.AddWithValue("@CMission", this.CMission);
                    cmd.Parameters.AddWithValue("@NAvoir", this.NAvoir);
                    cmd.Parameters.AddWithValue("@NBonLivraison", this.NBonLivraison);
                    cmd.Parameters.AddWithValue("@Observation", this.Observation);
                    cmd.Parameters.AddWithValue("@RaisonSociale", this.RaisonSociale);
                    // cmd.Parameters.AddWithValue("@TypeRetour", this.TypeRetour);
                    cmd.Parameters.AddWithValue("@Exercice", this.Exercice);
                    cmd.Parameters.AddWithValue("@Reference", this.Reference);

                    cmd.Parameters.AddWithValue("@MontantHT", this.MontantHT);
                    cmd.Parameters.AddWithValue("@MontantRemise", this.MontantRemise);
                    cmd.Parameters.AddWithValue("@MontantRetenuForfaitaire", this.MontantRetenuForfaitaire);
                    cmd.Parameters.AddWithValue("@MontantTaxe", this.MontantTaxe);
                    cmd.Parameters.AddWithValue("@MontantTTC", this.MontantTTC);
                    cmd.Parameters.AddWithValue("@PoidsTotal", this.PoidsTotal);
                    cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                    cmd.Parameters.AddWithValue("@CreePar ", this.CreePar);
                    cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            this.NBonRetour = dr["NBonRetour"].ToString();
                            this.Indice = int.Parse(dr["DernierIndice"].ToString());
                        }
                    }

                    int i = 1;
                    //BonCommande bonCommande = null;
                    //if (!(bonLivraison == null))
                    //{
                    //    if (!(string.IsNullOrEmpty(bonLivraison.NBonCommande)))
                    //    {
                    //        bonCommande = BonCommande.Charger(bonLivraison.NBonCommande);
                    //    }
                    //}

                    BonRetourDetailCollection collection = new BonRetourDetailCollection();

                    foreach (BonRetourDetail bonRetourDetail in BonRetourDetailCollection)
                        collection.Add(bonRetourDetail);

                    foreach (BonRetourDetail bonRetourDetail in BonRetourDetailCollection)
                    {
                        if (!BDefectueux)
                        {
                            #region CreerBonEntree

                            BonRetourDetail BRDetail = collection.RecupererBonRetourDetail(bonRetourDetail.CEntrepot);
                            if (BRDetail != null)
                            {
                                BonEntree bonEntree = new BonEntree();
                                bonEntree.CEntrepot = bonRetourDetail.CEntrepot;
                                bonEntree.NDocumentSource = this.NBonRetour;
                                bonEntree.DateEntree = (DateTime)DateRetour;
                                bonEntree.CClient = this.CClient;
                                bonEntree.RaisonSociale = this.RaisonSociale;
                                bonEntree.Exercice = this.Exercice;
                                bonEntree.TypeMouvement = StockHelper.TypesMouvementStock.BE_BONRETOURCLIENT.ToString();
                                bonEntree.BFodecExonore = this.BExonoreFodec;
                                bonEntree.BTvaExonore = this.BExonoreTVA;
                                bonEntree.CreePar = this.CreePar;
                                bonEntree.DateInsertion = DateTime.Now;
                                bonEntree.NDocumentSource = this.NBonRetour;
                                while (BRDetail != null)
                                {
                                    BonEntreeDetail bonEntreeDetail = new BonEntreeDetail();

                                    bonEntreeDetail.CEntrepot = BRDetail.CEntrepot;
                                    bonEntreeDetail.CArticle = BRDetail.CArticle;
                                    bonEntreeDetail.Quantite = BRDetail.Quantite;
                                    bonEntreeDetail.CUnite = BRDetail.CUnite;
                                    bonEntreeDetail.LibArticle = BRDetail.LibArticle;
                                    bonEntreeDetail.TauxTVA = BRDetail.TauxTVA;
                                    bonEntreeDetail.CreePar = this.CreePar;
                                    bonEntreeDetail.PCInsertion = this.PCInsertion;
                                    bonEntree.BonEntreeDetailCollection.Add(bonEntreeDetail);
                                    collection.Remove(BRDetail);
                                    BRDetail = collection.RecupererBonRetourDetail(bonRetourDetail.CEntrepot);
                                }

                                bonEntree.Inserer(transaction);
                            }

                            #endregion CreerBonEntree
                        }
                        bonRetourDetail.NBonRetour = this.NBonRetour;
                        bonRetourDetail.Ordre = i++;
                        bonRetourDetail.Sauvegarder(transaction);

                        this.MiseAJourQuantiteHistorique(this.NBonLivraison, bonRetourDetail.CArticle, bonRetourDetail.OrdreBonLivraison, bonRetourDetail.Quantite, transaction);

                        //if (!(bonCommande == null))
                        //{
                        //    BonLivraisonDetail detailLivraison = bonLivraison.BonLivraisonDetailCollection.RecupererBonLivraisonDetail(this.NBonLivraison, bonRetourDetail.CArticle, bonRetourDetail.OrdreBonLivraison);
                        //    BonCommandeDetail detailCommande = bonCommande.BonCommandeDetailCollection.RecupererBonCommandeDetail(bonLivraison.NBonCommande, detailLivraison.CArticle, detailLivraison.OrdreBonCommande);
                        //    if (detailCommande != null)
                        //        detailCommande.QuantiteHistorique = detailCommande.QuantiteHistorique + bonRetourDetail.Quantite;

                        //    if (bonCommande.Etat == VenteHelper.EtatBonCommande.LIVRE.ToString())
                        //        BonCommande.ModifierEtatBonCommande(bonCommande.NBonCommande, VenteHelper.EtatBonCommande.ENCOURS.ToString(), transaction);

                        //    StockHelper.MiseAJourStockReserver(bonRetourDetail.CArticle, bonRetourDetail.CEntrepot, bonRetourDetail.Quantite, 1, transaction);
                        //    detailCommande.Modifier(transaction);
                        //}
                    }

                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.Transaction = transaction;
                    cmd1.Connection = transaction.Connection;
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "SELECT SUM(QuantiteHistorique) Somme FROM BonLivraisonDetail WHERE NBonLivraison = '" + this.NBonLivraison + "'";
                    using (SqlDataReader dr = cmd1.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            quantiteHistoriqueBL = quantiteHistoriqueBL + decimal.Parse(dr["Somme"].ToString());
                        }
                    }
                    if (quantiteHistoriqueBL == 0)
                        cmd1.CommandText = "UPDATE  BonLivraison SET BRetour=1 WHERE NBonLivraison ='" + this.NBonLivraison + "'";
                    cmd1.ExecuteNonQuery();
                    foreach (BonRetourTaxe bonRetourTaxe in BonRetourTaxeCollection)
                    {
                        bonRetourTaxe.NBonRetour = this.NBonRetour;
                        bonRetourTaxe.Sauvegarder(transaction);
                    }
                    if (!(this.BEchantillon))
                        VenteHelper.ModifierSolde(null, null, this.CClient, 0m, 0m, this.MontantTTC, 0m, 0m, 0m, transaction);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public void InsererBonRetourAnterieur(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BonRetour_Inserer";
                cmd.Parameters.AddWithValue("@CUnite", this.CUnite);
                cmd.Parameters.AddWithValue("@CClient", this.CClient);

                cmd.Parameters.AddWithValue("@BExonoreTVA", this.BExonoreTVA);
                cmd.Parameters.AddWithValue("@BAvanceForfaitaire", this.BAvanceForfaitaire);
                cmd.Parameters.AddWithValue("@BEchantillon ", this.BEchantillon);
                cmd.Parameters.AddWithValue("@BDefectueux", this.BDefectueux);
                cmd.Parameters.AddWithValue("@BExport", this.BExport);
                cmd.Parameters.AddWithValue("@BTransfertAvoir", this.BTransfertAvoir);
                cmd.Parameters.AddWithValue("@BRetourAnterieur", this.BRetourAnterieur);
                cmd.Parameters.AddWithValue("@BExonoreFodec", this.BExonoreFodec);

                cmd.Parameters.AddWithValue("@CVendeur ", this.CVendeur);
                cmd.Parameters.AddWithValue("@DateRetour ", this.DateRetour);
                cmd.Parameters.AddWithValue("@NFactureAnterieur", this.NFactureAnterieur);
                cmd.Parameters.AddWithValue("@MatriculeFiscale", this.MatriculeFiscale);
                cmd.Parameters.AddWithValue("@Adresse", this.Adresse);
                cmd.Parameters.AddWithValue("@NTelephone", this.NTelephone);
                cmd.Parameters.AddWithValue("@CMission", this.CMission);
                cmd.Parameters.AddWithValue("@NAvoir", this.NAvoir);
                cmd.Parameters.AddWithValue("@NBonLivraison", this.NBonLivraison);
                cmd.Parameters.AddWithValue("@Observation", this.Observation);
                cmd.Parameters.AddWithValue("@RaisonSociale", this.RaisonSociale);
                //cmd.Parameters.AddWithValue("@TypeRetour", this.TypeRetour);
                cmd.Parameters.AddWithValue("@Exercice", this.Exercice);
                cmd.Parameters.AddWithValue("@Reference", this.Reference);

                cmd.Parameters.AddWithValue("@MontantHT", this.MontantHT);
                cmd.Parameters.AddWithValue("@MontantRemise", this.MontantRemise);
                cmd.Parameters.AddWithValue("@MontantRetenuForfaitaire", this.MontantRetenuForfaitaire);
                cmd.Parameters.AddWithValue("@MontantTaxe", this.MontantTaxe);
                cmd.Parameters.AddWithValue("@MontantTTC", this.MontantTTC);
                cmd.Parameters.AddWithValue("@PoidsTotal", this.PoidsTotal);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                cmd.Parameters.AddWithValue("@CreePar ", this.CreePar);
                cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);

                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        this.NBonRetour = dr["NBonRetour"].ToString();
                        this.Indice = int.Parse(dr["DernierIndice"].ToString());
                    }
                }

                int i = 1;
                BonRetourDetailCollection collection = new BonRetourDetailCollection();

                foreach (BonRetourDetail bonRetourDetail in BonRetourDetailCollection)
                    collection.Add(bonRetourDetail);

                foreach (BonRetourDetail bonRetourDetail in BonRetourDetailCollection)
                {
                    if (!BDefectueux)
                    {
                        #region CreerBonEntree

                        BonRetourDetail BRDetail = collection.RecupererBonRetourDetail(bonRetourDetail.CEntrepot);
                        if (BRDetail != null)
                        {
                            BonEntree bonEntree = new BonEntree();
                            bonEntree.CEntrepot = bonRetourDetail.CEntrepot;
                            bonEntree.NDocumentSource = this.NBonRetour;
                            bonEntree.DateEntree = (DateTime)DateRetour;
                            bonEntree.CClient = this.CClient;
                            bonEntree.RaisonSociale = this.RaisonSociale;
                            bonEntree.Exercice = this.Exercice;
                            bonEntree.TypeMouvement = StockHelper.TypesMouvementStock.BE_BONRETOURCLIENT.ToString();
                            bonEntree.BFodecExonore = this.BExonoreFodec;
                            bonEntree.BTvaExonore = this.BExonoreTVA;
                            bonEntree.CreePar = this.CreePar;
                            bonEntree.DateInsertion = DateTime.Now;

                            while (BRDetail != null)
                            {
                                BonEntreeDetail bonEntreeDetail = new BonEntreeDetail();

                                bonEntreeDetail.CEntrepot = bonRetourDetail.CEntrepot;
                                bonEntreeDetail.CArticle = bonRetourDetail.CArticle;
                                bonEntreeDetail.Quantite = bonRetourDetail.Quantite;
                                bonEntreeDetail.CUnite = bonRetourDetail.CUnite;
                                bonEntreeDetail.LibArticle = bonRetourDetail.LibArticle;
                                bonEntreeDetail.TauxTVA = bonRetourDetail.TauxTVA;
                                bonEntreeDetail.CreePar = this.CreePar;
                                bonEntreeDetail.PCInsertion = this.PCInsertion;
                                bonEntree.BonEntreeDetailCollection.Add(bonEntreeDetail);
                                collection.Remove(BRDetail);
                                BRDetail = collection.RecupererBonRetourDetail(bonRetourDetail.CEntrepot);
                            }
                            bonEntree.Inserer(transaction);

                        #endregion CreerBonEntree
                        }
                    }
                    bonRetourDetail.NBonRetour = this.NBonRetour;
                    bonRetourDetail.Ordre = i++;
                    bonRetourDetail.Sauvegarder(transaction);
                }

                foreach (BonRetourTaxe bonRetourTaxe in BonRetourTaxeCollection)
                {
                    bonRetourTaxe.NBonRetour = this.NBonRetour;
                    bonRetourTaxe.Sauvegarder(transaction);
                }
                if (!(this.BEchantillon))
                    VenteHelper.ModifierSolde(null, null, this.CClient, 0m, 0m, this.MontantTTC, 0m, 0m, 0m, transaction);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void MiseAJourQuantiteHistorique(string nBonLivraison, string cArticle, int ordre, decimal quantite, SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "BonLivraison_AjusterQuantiteHistorique";
                cmd.Parameters.AddWithValue("@CArticle", cArticle);
                cmd.Parameters.AddWithValue("@NBonLivraison", nBonLivraison);
                cmd.Parameters.AddWithValue("@OrdreBonLivraison ", ordre);
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

        public void Modifier()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    Modifier(transaction);
                    if (!string.IsNullOrWhiteSpace(this.NAvoir))
                    {
                        Avoir avoirTrac = Avoir.Charger(this.NAvoir);
                        avoirTrac.InsererTrac(transaction);

                        RegenererAvoir(transaction);
                        ModifieReglement(transaction);
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

        private void ModifieReglement(SqlTransaction transaction)
        {
            decimal ancienMontant = 0;
            string ancienClient = string.Empty;
            Reglement reglement = Reglement.ChargerAvoir(this.NAvoir);
            ancienClient = reglement.CClient;
            if (this.BRetourAnterieur && this.CClient != reglement.CClient)
                reglement.CClient = this.CClient;
            reglement.RaisonSociale = this.RaisonSociale;
            ancienMontant = reglement.Montant;
            reglement.Montant = this.MontantTTC;
            reglement.ResteReglement = this.MontantTTC;
            reglement.DateModification = this.DateModification;
            reglement.ModifiePar = this.ModifiePar;
            reglement.PCModification = this.PCModification;
            reglement.ModifierAvoir(ancienClient,ancienMontant, transaction);
        }

        private void RegenererAvoir(SqlTransaction transaction)
        {
            Avoir avoir = Avoir.Charger(this.NAvoir);
            if (this.BRetourAnterieur && this.CClient != avoir.CClient)
                avoir.CClient = this.CClient;
            avoir.RaisonSociale = this.RaisonSociale;
            avoir.MatriculeFiscale = this.MatriculeFiscale;
            avoir.Adresse = this.Adresse;
            avoir.NTelephone = this.NTelephone;
            avoir.MontantHT = this.MontantHT;
            avoir.MontantRemise = this.MontantRemise;
            avoir.MontantRetenuForfaitaire = this.MontantRetenuForfaitaire;
            avoir.MontantTaxe = this.MontantTaxe;
            avoir.MontantTTC = this.MontantTTC;
            avoir.PoidsTotal = this.PoidsTotal;
            avoir.PCModification = this.PCModification;
            avoir.ModifiePar = this.ModifiePar;
            avoir.AvoirDetailCollection = new AvoirDetailCollection();
            avoir.AvoirTaxeCollection = new AvoirTaxeCollection();
            foreach (BonRetourDetail detailBR in this.BonRetourDetailCollection)
            {
                AvoirDetail detail = new AvoirDetail();
                detail.NAvoir = avoir.NAvoir;
                detail.CArticle = detailBR.CArticle;
                detail.CEntrepot = detailBR.CEntrepot;
                detail.CTaxe = detailBR.CTaxe;
                detail.CUnite = detailBR.CUnite;
                detail.LibArticle = detailBR.LibArticle;
                detail.NBonRetour = detailBR.NBonRetour;
                detail.PourcentageFodec = detailBR.PourcentageFodec;
                detail.Remise1 = detailBR.Remise1;
                detail.Remise2 = detailBR.Remise2;
                detail.PourcentageRemise = detailBR.PourcentageRemise;
                detail.PrixHT = detailBR.PrixHT;
                detail.PrixRevient = detailBR.PrixRevient;
                detail.Quantite = detailBR.Quantite;
                detail.QuantiteHistorique = detailBR.Quantite;
                detail.TauxTVA = detailBR.TauxTVA;
                detail.MontantNet = detailBR.MontantNet;
                detail.Poids = detailBR.Poids;
                detail.MontantTaxe = detail.MontantTaxe;

                avoir.AvoirDetailCollection.Add(detail);
            }
            foreach (BonRetourTaxe detailBRTaxe in this.BonRetourTaxeCollection)
            {
                AvoirTaxe avoirTaxe = new AvoirTaxe();
                avoirTaxe.Assiette = detailBRTaxe.Assiette;
                avoirTaxe.BExonoreFodec = detailBRTaxe.BExonoreFodec;
                avoirTaxe.BExonoreTVA = detailBRTaxe.BExonoreTVA;
                avoirTaxe.BExport = detailBRTaxe.BExport;
                avoirTaxe.CTaxe = detailBRTaxe.CTaxe;
                avoirTaxe.MontantTaxe = detailBRTaxe.MontantTaxe;
                avoirTaxe.NAvoir = avoir.NAvoir;
                avoirTaxe.TauxTVA = detailBRTaxe.TauxTVA;
                avoir.AvoirTaxeCollection.Add(avoirTaxe);
            }
            avoir.Modifier(transaction);
        }

        public void Modifier(SqlTransaction transaction)
        {
            decimal quantiteHistoriqueBL = 0;
            if (!(this.BEchantillon))
                this.RestituerSoldeBonRetour(transaction);
             BonLivraison bonLivraison=null;
             if (!this.BRetourAnterieur)
             {
                  bonLivraison = BonLivraison.Charger(this.NBonLivraison);
                 this.RestituerQuantiteHistorique(transaction);
                 bonLivraison.RestituerQuantiteHistorique(transaction);
             }
            BonRetour ancienBonRetour = BonRetour.Charger(this.NBonRetour);

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BonRetour_Modifier";
                cmd.Parameters.AddWithValue("@CUnite", this.CUnite);
                cmd.Parameters.AddWithValue("@CClient", this.CClient);
                cmd.Parameters.AddWithValue("@NBonRetour", this.NBonRetour);

                cmd.Parameters.AddWithValue("@BExonoreTVA ", this.BExonoreTVA);
                cmd.Parameters.AddWithValue("@BAvanceForfaitaire ", this.BAvanceForfaitaire);
                cmd.Parameters.AddWithValue("@BExonoreFodec", this.BExonoreFodec);
                cmd.Parameters.AddWithValue("@BEchantillon ", this.BEchantillon);
                cmd.Parameters.AddWithValue("@BDefectueux", this.BDefectueux);
                cmd.Parameters.AddWithValue("@BExport", this.BExport);
                cmd.Parameters.AddWithValue("@BTransfertAvoir", this.BTransfertAvoir);
                cmd.Parameters.AddWithValue("@BRetourAnterieur", this.BRetourAnterieur);

                cmd.Parameters.AddWithValue("@CVendeur ", this.CVendeur);
                cmd.Parameters.AddWithValue("@DateRetour ", this.DateRetour);
                cmd.Parameters.AddWithValue("@NFactureAnterieur", this.NFactureAnterieur);
                cmd.Parameters.AddWithValue("@MatriculeFiscale", this.MatriculeFiscale);
                cmd.Parameters.AddWithValue("@Adresse", this.Adresse);
                cmd.Parameters.AddWithValue("@NTelephone", this.NTelephone);
                cmd.Parameters.AddWithValue("@CMission", this.CMission);
                cmd.Parameters.AddWithValue("@NAvoir", this.NAvoir);
                cmd.Parameters.AddWithValue("@NBonLivraison", this.NBonLivraison);
                cmd.Parameters.AddWithValue("@Observation", this.Observation);
                cmd.Parameters.AddWithValue("@RaisonSociale", this.RaisonSociale);

                cmd.Parameters.AddWithValue("@Exercice", this.Exercice);
                cmd.Parameters.AddWithValue("@Reference", this.Reference);

                cmd.Parameters.AddWithValue("@MontantHT", this.MontantHT);
                cmd.Parameters.AddWithValue("@MontantRemise", this.MontantRemise);
                cmd.Parameters.AddWithValue("@MontantRetenuForfaitaire", this.MontantRetenuForfaitaire);
                cmd.Parameters.AddWithValue("@MontantTaxe", this.MontantTaxe);
                cmd.Parameters.AddWithValue("@MontantTTC", this.MontantTTC);
                cmd.Parameters.AddWithValue("@PoidsTotal", this.PoidsTotal);
                cmd.Parameters.AddWithValue("@PCModification", this.PCModification);
                cmd.Parameters.AddWithValue("@ModifiePar", this.ModifiePar);
                cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);

                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;
                cmd.ExecuteNonQuery();
                this.SupprimerDetailRetourAnterieurs(transaction);
                this.SupprimerTaxeRetourAnterieurs(transaction);
                int i = 1;

                if (ancienBonRetour.DateRetour != this.DateRetour)
                {
                    BonEntree bonEntree = BonEntree.ChargerParDocumentSource(StockHelper.TypesMouvementStock.BE_BONRETOURCLIENT.ToString(), this.NBonRetour);

                    if (bonEntree != null)
                    {
                        bonEntree.DateEntree = (DateTime)this.DateRetour;
                        bonEntree.PCModification = this.PCModification;
                        bonEntree.ModifiePar = this.ModifiePar;
                        bonEntree.Modifier(transaction);
                    }

                    BonSortie bonSortie = BonSortie.ChargerParDocumentSource(StockHelper.TypesMouvementStock.BS_BONRETOURCLIENT.ToString(), this.NBonRetour);

                    if (bonSortie != null)
                    {
                        bonSortie.DateSortie = (DateTime)this.DateRetour;
                        bonSortie.PCModification = this.PCModification;
                        bonSortie.ModifiePar = this.ModifiePar;
                        bonSortie.Modifier(transaction);
                    }
                }
                //BonCommande bonCommande = null;
                //if (!(bonLivraison == null))
                //{
                //    if (!(string.IsNullOrEmpty(bonLivraison.NBonCommande)))
                //    {
                //        bonCommande = BonCommande.Charger(bonLivraison.NBonCommande);
                //    }
                //}
                BonRetourDetailCollection bonRetourDetailCollection = new BonRetourDetailCollection();

                if (!BDefectueux)
                {
                    foreach (BonRetourDetail bonRetourDetail in BonRetourDetailCollection)
                        bonRetourDetailCollection.Add(bonRetourDetail);
                }
                BonEntreeDetailCollection detailBECollection = new BonEntreeDetailCollection();
                BonSortieDetailCollection detailBSCollection = new BonSortieDetailCollection();

                BonEntreeCollection collectionBonEntree = new BonEntreeCollection();
                BonSortieCollection collectionBonSortie = new BonSortieCollection();
                int identique = 0;
                int tailleAncien = ancienBonRetour.BonRetourDetailCollection.Count;
                foreach (BonRetourDetail bonRetourDetail in BonRetourDetailCollection)
                {
                    if (!BDefectueux)
                    {
                        BonRetourDetail ancienDetail = ancienBonRetour.BonRetourDetailCollection.RecupererBonRetourDetail(bonRetourDetail.CEntrepot, bonRetourDetail.CArticle, this.NBonRetour);
                        BonRetourDetail nouveauRetour = bonRetourDetailCollection.RecupererBonRetourDetail(bonRetourDetail.CEntrepot, bonRetourDetail.CArticle);
                        if (ancienDetail != null)
                        {
                            if (ancienDetail.Quantite > bonRetourDetail.Quantite)
                            {
                                BonSortieDetail sortieDetail = new BonSortieDetail();
                                sortieDetail.CArticle = bonRetourDetail.CArticle;
                                sortieDetail.CEntrepot = bonRetourDetail.CEntrepot;
                                sortieDetail.CreePar = CreePar;
                                sortieDetail.CUnite = bonRetourDetail.CUnite;
                                sortieDetail.DateInsertion = DateInsertion;
                                sortieDetail.LibArticle = bonRetourDetail.LibArticle;
                                sortieDetail.MontantTaxe = bonRetourDetail.MontantTaxe;
                                sortieDetail.PrixHT = bonRetourDetail.PrixHT;
                                sortieDetail.Quantite = ancienDetail.Quantite - bonRetourDetail.Quantite;
                                detailBSCollection.Add(sortieDetail);

                                bonRetourDetailCollection.Remove(nouveauRetour);
                                ancienBonRetour.BonRetourDetailCollection.Remove(ancienDetail);
                            }
                            else
                                if (ancienDetail.Quantite < bonRetourDetail.Quantite)
                                {
                                    BonEntreeDetail entreeDetail = new BonEntreeDetail();
                                    entreeDetail.CArticle = bonRetourDetail.CArticle;
                                    entreeDetail.CEntrepot = bonRetourDetail.CEntrepot;
                                    entreeDetail.CreePar = CreePar;
                                    entreeDetail.CUnite = bonRetourDetail.CUnite;
                                    entreeDetail.DateInsertion = DateTime.Now;
                                    entreeDetail.LibArticle = bonRetourDetail.LibArticle;

                                    entreeDetail.Quantite = bonRetourDetail.Quantite - ancienDetail.Quantite;
                                    detailBECollection.Add(entreeDetail);
                                    bonRetourDetailCollection.Remove(nouveauRetour);
                                    ancienBonRetour.BonRetourDetailCollection.Remove(ancienDetail);
                                }
                                else
                                {
                                    bonRetourDetailCollection.Remove(nouveauRetour);
                                    ancienBonRetour.BonRetourDetailCollection.Remove(ancienDetail);
                                    identique++;
                                }
                        }
                    }

                    bonRetourDetail.NBonRetour = this.NBonRetour;
                    bonRetourDetail.Ordre = i++;
                    bonRetourDetail.Sauvegarder(transaction);
                    if (!this.BRetourAnterieur)
                    {
                        this.MiseAJourQuantiteHistorique(this.NBonLivraison, bonRetourDetail.CArticle, bonRetourDetail.OrdreBonLivraison, bonRetourDetail.Quantite, transaction);

                        //if (!(bonCommande == null))
                        //{
                        //    BonLivraisonDetail detailLivraison = bonLivraison.BonLivraisonDetailCollection.RecupererBonLivraisonDetail(this.NBonLivraison, bonRetourDetail.CArticle, bonRetourDetail.OrdreBonLivraison);
                        //    BonCommandeDetail detailCommande = bonCommande.BonCommandeDetailCollection.RecupererBonCommandeDetail(bonLivraison.NBonCommande, detailLivraison.CArticle, detailLivraison.OrdreBonCommande);
                        //    detailCommande.QuantiteHistorique = detailCommande.QuantiteHistorique + bonRetourDetail.Quantite;

                        //    if (bonCommande.Etat == VenteHelper.EtatBonCommande.LIVRE.ToString())
                        //        BonCommande.ModifierEtatBonCommande(bonCommande.NBonCommande, VenteHelper.EtatBonCommande.ENCOURS.ToString(), transaction);

                        //    StockHelper.MiseAJourStockReserver(bonRetourDetail.CArticle, bonRetourDetail.CEntrepot, bonRetourDetail.Quantite, 1, transaction);
                        //    detailCommande.Modifier(transaction);
                      //  }
                    }
                }

                if (!(this.BonRetourDetailCollection.Count == identique && identique == tailleAncien))
                {
                    if (!BDefectueux)
                    {
                        foreach (BonRetourDetail nouveauDetail in bonRetourDetailCollection)
                        {
                            BonEntreeDetail detailBE = new BonEntreeDetail();
                            detailBE.CArticle = nouveauDetail.CArticle;
                            detailBE.CEntrepot = nouveauDetail.CEntrepot;
                            detailBE.CreePar = this.CreePar;
                            detailBE.CTaxe = nouveauDetail.CTaxe;
                            detailBE.CUnite = nouveauDetail.CUnite;
                            detailBE.LibArticle = nouveauDetail.LibArticle;
                            detailBE.PourcentageFodec = nouveauDetail.PourcentageFodec;
                            detailBE.PourcentageRemise = nouveauDetail.PourcentageRemise;
                            detailBE.PrixRevient = nouveauDetail.PrixHT;
                            detailBE.Quantite = nouveauDetail.Quantite;
                            detailBE.TauxTVA = nouveauDetail.TauxTVA;
                            detailBECollection.Add(detailBE);


                        }
                        foreach (BonRetourDetail ancienDetail in ancienBonRetour.BonRetourDetailCollection)
                        {
                            BonSortieDetail detailBS = new BonSortieDetail();
                            detailBS.CArticle = ancienDetail.CArticle;
                            detailBS.CEntrepot = ancienDetail.CEntrepot;
                            detailBS.CUnite = ancienDetail.CUnite;
                            detailBS.LibArticle = ancienDetail.LibArticle;
                            detailBS.MontantTaxe = ancienDetail.MontantTaxe;
                            detailBS.PrixHT = ancienDetail.PrixHT;
                            detailBS.Quantite = ancienDetail.Quantite;
                            detailBS.TauxTVA = ancienDetail.TauxTVA;
                            detailBSCollection.Add(detailBS);
                        }
                    }
                }
                bool findBE = false;
                for (i = 0; i < detailBECollection.Count; )
                {
                    if (!findBE)
                    {
                        BonEntree bonEntree = new BonEntree();
                        bonEntree.BFodecExonore = this.BExonoreFodec;
                        bonEntree.BTvaExonore = this.BExonoreTVA;
                        bonEntree.CClient = this.CClient;
                        bonEntree.CEntrepot = detailBECollection[i].CEntrepot;
                        bonEntree.CreePar = this.ModifiePar;
                        bonEntree.DateEntree = (DateTime)this.DateRetour;
                        bonEntree.Exercice = this.Exercice;
                        bonEntree.NDocumentSource = this.NBonLivraison;
                        bonEntree.RaisonSociale = this.RaisonSociale;
                        bonEntree.TypeMouvement = StockHelper.TypesMouvementStock.BE_BONRETOURCLIENT.ToString();
                        collectionBonEntree.Add(bonEntree);
                        findBE = true;
                    }
                    if (collectionBonEntree.Count != 0 || !findBE)
                    {
                        foreach (BonEntree entree in collectionBonEntree)
                        {
                            if (detailBECollection[i].CEntrepot == entree.CEntrepot)
                            {
                                entree.BonEntreeDetailCollection.Add(detailBECollection[i]);
                                i++;
                            }
                            else
                            {
                                // i++;
                                findBE = false;
                            }
                        }
                    }
                }
                bool findBS = false;
                for (i = 0; i < detailBSCollection.Count; )
                {
                    if (!findBS)
                    {
                        BonSortie bonSortie = new BonSortie();
                        //bonSortie.CChauffeur = this.Chauffeur;
                        bonSortie.CClient = this.CClient;
                        bonSortie.CEntrepot = detailBSCollection[i].CEntrepot;
                        bonSortie.CreePar = this.ModifiePar;
                       // bonSortie.CVehicule = this.CVehicule;
                        bonSortie.DateSortie = (DateTime)this.DateRetour;
                        bonSortie.Exercice = this.Exercice;
                        bonSortie.NDocumentSource = this.NBonLivraison;
                        bonSortie.PCInsertion = this.PCModification;
                        bonSortie.RaisonSociale = this.RaisonSociale;
                        bonSortie.TypeMouvement = StockHelper.TypesMouvementStock.BS_BONRETOURCLIENT.ToString();
                        collectionBonSortie.Add(bonSortie);
                        findBS = true;
                    }

                    if (collectionBonSortie.Count != 0 || !findBS)
                    {
                        foreach (BonSortie sortie in collectionBonSortie)
                        {
                            if (detailBSCollection[i].CEntrepot == sortie.CEntrepot)
                            {
                                sortie.BonSortieDetailCollection.Add(detailBSCollection[i]);
                                i++;
                            }
                            else
                            {
                                findBS = false;
                            }
                        }
                    }
                }
                if (collectionBonSortie.Count != 0)
                {
                    foreach (BonSortie sortie in collectionBonSortie)
                        sortie.Inserer(transaction);
                }
                if (collectionBonEntree.Count != 0)
                    foreach (BonEntree entree in collectionBonEntree)
                        entree.Inserer(transaction);
                if (!this.BRetourAnterieur)
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT ISNULL(SUM(ISNULL(QuantiteHistorique,0)),0) Somme FROM BonLivraisonDetail WHERE NBonLivraison = '" + this.NBonLivraison + "'";
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            quantiteHistoriqueBL = quantiteHistoriqueBL + decimal.Parse(dr["Somme"].ToString());
                        }
                    }
                    if (quantiteHistoriqueBL == 0)
                        cmd.CommandText = "UPDATE  BonLivraison SET BRetour =1 WHERE NBonLivraison = '" + this.NBonLivraison + "'";

                    else
                        cmd.CommandText = "UPDATE  BonLivraison SET BRetour =0  WHERE NBonLivraison = '" + this.NBonLivraison + "'";
                    cmd.ExecuteNonQuery();
                }
                foreach (BonRetourTaxe bonRetourTaxe in BonRetourTaxeCollection)
                {
                    bonRetourTaxe.NBonRetour = this.NBonRetour;
                    bonRetourTaxe.Sauvegarder(transaction);
                }
                if (!(this.BEchantillon))
                    VenteHelper.ModifierSolde(null, null, this.CClient, 0m, 0m, this.MontantTTC, 0m, 0m, 0m, transaction);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Modifier1(SqlTransaction transaction)
        {
            decimal quantiteHistoriqueBL = 0;
            if (!(this.BEchantillon))
                this.RestituerSoldeBonRetour(transaction);
            BonLivraison bonLivraison = BonLivraison.Charger(this.NBonLivraison);
            this.RestituerQuantiteHistorique(transaction);
            bonLivraison.RestituerQuantiteHistorique(transaction);
            BonRetour ancienBonRetour = BonRetour.Charger(this.NBonRetour);

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BonRetour_Modifier";
                cmd.Parameters.AddWithValue("@CUnite", this.CUnite);
                cmd.Parameters.AddWithValue("@CClient", this.CClient);
                cmd.Parameters.AddWithValue("@NBonRetour", this.NBonRetour);

                cmd.Parameters.AddWithValue("@BExonoreTVA ", this.BExonoreTVA);
                cmd.Parameters.AddWithValue("@BAvanceForfaitaire ", this.BAvanceForfaitaire);
                cmd.Parameters.AddWithValue("@BExonoreFodec", this.BExonoreFodec);
                cmd.Parameters.AddWithValue("@BEchantillon ", this.BEchantillon);
                cmd.Parameters.AddWithValue("@BDefectueux", this.BDefectueux);
                cmd.Parameters.AddWithValue("@BExport", this.BExport);
                cmd.Parameters.AddWithValue("@BTransfertAvoir", this.BTransfertAvoir);
                cmd.Parameters.AddWithValue("@BRetourAnterieur", this.BRetourAnterieur);

                cmd.Parameters.AddWithValue("@CVendeur ", this.CVendeur);
                cmd.Parameters.AddWithValue("@DateRetour ", this.DateRetour);
                cmd.Parameters.AddWithValue("@NFactureAnterieur", this.NFactureAnterieur);
                cmd.Parameters.AddWithValue("@MatriculeFiscale", this.MatriculeFiscale);
                cmd.Parameters.AddWithValue("@Adresse", this.Adresse);
                cmd.Parameters.AddWithValue("@NTelephone", this.NTelephone);
                cmd.Parameters.AddWithValue("@CMission", this.CMission);
                cmd.Parameters.AddWithValue("@NAvoir", this.NAvoir);
                cmd.Parameters.AddWithValue("@NBonLivraison", this.NBonLivraison);
                cmd.Parameters.AddWithValue("@Observation", this.Observation);
                cmd.Parameters.AddWithValue("@RaisonSociale", this.RaisonSociale);

                cmd.Parameters.AddWithValue("@Exercice", this.Exercice);
                cmd.Parameters.AddWithValue("@Reference", this.Reference);

                cmd.Parameters.AddWithValue("@MontantHT", this.MontantHT);
                cmd.Parameters.AddWithValue("@MontantRemise", this.MontantRemise);
                cmd.Parameters.AddWithValue("@MontantRetenuForfaitaire", this.MontantRetenuForfaitaire);
                cmd.Parameters.AddWithValue("@MontantTaxe", this.MontantTaxe);
                cmd.Parameters.AddWithValue("@MontantTTC", this.MontantTTC);
                cmd.Parameters.AddWithValue("@PoidsTotal", this.PoidsTotal);
                cmd.Parameters.AddWithValue("@PCModification", this.PCModification);
                cmd.Parameters.AddWithValue("@ModifiePar", this.ModifiePar);
                cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);

                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;
                cmd.ExecuteNonQuery();
                this.SupprimerDetailRetourAnterieurs(transaction);
                this.SupprimerTaxeRetourAnterieurs(transaction);
                int i = 1;

                if (ancienBonRetour.DateRetour != this.DateRetour)
                {
                    BonEntree bonEntree = BonEntree.ChargerParDocumentSource(StockHelper.TypesMouvementStock.BE_BONRETOURCLIENT.ToString(), this.NBonRetour);

                    if (bonEntree != null)
                    {
                        bonEntree.DateEntree = (DateTime)this.DateRetour;
                        bonEntree.PCModification = this.PCModification;
                        bonEntree.ModifiePar = this.ModifiePar;
                        bonEntree.Modifier(transaction);
                    }

                    BonSortie bonSortie = BonSortie.ChargerParDocumentSource(StockHelper.TypesMouvementStock.BS_BONRETOURCLIENT.ToString(), this.NBonRetour);

                    if (bonSortie != null)
                    {
                        bonSortie.DateSortie = (DateTime)this.DateRetour;
                        bonSortie.PCModification = this.PCModification;
                        bonSortie.ModifiePar = this.ModifiePar;
                        bonSortie.Modifier(transaction);
                    }
                }
                BonCommande bonCommande = null;
                if (!(bonLivraison == null))
                {
                    if (!(string.IsNullOrEmpty(bonLivraison.NBonCommande)))
                    {
                        bonCommande = BonCommande.Charger(bonLivraison.NBonCommande);
                    }
                }
                BonRetourDetailCollection bonRetourDetailCollection = new BonRetourDetailCollection();

                if (!BDefectueux)
                {
                    foreach (BonRetourDetail bonRetourDetail in BonRetourDetailCollection)
                        bonRetourDetailCollection.Add(bonRetourDetail);
                }
                foreach (BonRetourDetail bonRetourDetail in BonRetourDetailCollection)
                {
                    if (!BDefectueux)
                    {
                        #region Creer les mouvements

                        BonRetourDetail BRDetail = bonRetourDetailCollection.RecupererBonRetourDetail(bonRetourDetail.CEntrepot);
                        if (BRDetail != null)
                        {
                            BonEntree bonEntree = new BonEntree();
                            bonEntree.CEntrepot = BRDetail.CEntrepot;
                            bonEntree.DateEntree = DateTime.Now;
                            bonEntree.CClient = this.CClient;
                            bonEntree.BFodecExonore = this.BExonoreFodec;
                            bonEntree.BTvaExonore = this.BExonoreTVA;
                            bonEntree.CreePar = this.ModifiePar;
                            bonEntree.Exercice = this.Exercice;
                            bonEntree.NDocumentSource = this.NBonLivraison;
                            bonEntree.RaisonSociale = this.RaisonSociale;
                            bonEntree.TypeMouvement = StockHelper.TypesMouvementStock.BE_BONRETOURCLIENT.ToString();
                            bonEntree.PCInsertion = this.PCModification;

                            BonSortie bonSortie = new BonSortie();
                            bonSortie.CClient = this.CClient;
                            bonSortie.CEntrepot = BRDetail.CEntrepot;
                            bonSortie.CreePar = this.ModifiePar;
                            bonSortie.DateSortie = (DateTime)this.DateRetour;
                            bonSortie.Exercice = this.Exercice;
                            bonSortie.NDocumentSource = this.NBonLivraison;
                            bonSortie.RaisonSociale = this.RaisonSociale;
                            bonSortie.TypeMouvement = StockHelper.TypesMouvementStock.BS_BONRETOURCLIENT.ToString();
                            bonSortie.PCInsertion = this.PCModification;
                            int nombreAncien = ancienBonRetour.BonRetourDetailCollection.Count;
                            int nouveau = this.BonRetourDetailCollection.Count;
                            int identique = 0;
                            while (!(BRDetail == null))
                            {
                                BonRetourDetail ancienDetail = ancienBonRetour.BonRetourDetailCollection.RecupererBonRetourDetail(this.NBonRetour, bonRetourDetail.CArticle);

                                if ((ancienDetail == null) || (ancienDetail.Quantite < bonRetourDetail.Quantite))
                                {
                                    BonEntreeDetail detailBE = new BonEntreeDetail();
                                    detailBE.CArticle = bonRetourDetail.CArticle;
                                    detailBE.CEntrepot = bonRetourDetail.CEntrepot;
                                    detailBE.CreePar = this.ModifiePar;
                                    detailBE.CTaxe = bonRetourDetail.CTaxe;
                                    detailBE.CUnite = bonRetourDetail.CUnite;
                                    detailBE.LibArticle = bonRetourDetail.LibArticle;
                                    detailBE.PourcentageFodec = bonRetourDetail.PourcentageFodec;
                                    detailBE.PourcentageRemise = bonRetourDetail.PourcentageRemise;
                                    detailBE.PrixRevient = bonRetourDetail.PrixRevient;
                                    detailBE.Quantite = bonRetourDetail.Quantite;
                                    if (ancienDetail != null)
                                        detailBE.Quantite = bonRetourDetail.Quantite - ancienDetail.Quantite;
                                    detailBE.TauxTVA = bonRetourDetail.TauxTVA;
                                    bonEntree.BonEntreeDetailCollection.Add(detailBE);

                                }
                                else if ((ancienDetail != null) && (ancienDetail.Quantite > bonRetourDetail.Quantite))
                                {
                                    BonSortieDetail detailBS = new BonSortieDetail();
                                    detailBS.CArticle = bonRetourDetail.CArticle;
                                    detailBS.CEntrepot = bonRetourDetail.CEntrepot;
                                    detailBS.CreePar = this.ModifiePar;
                                    detailBS.CUnite = bonRetourDetail.CUnite;
                                    detailBS.LibArticle = bonRetourDetail.LibArticle;
                                    detailBS.MontantTaxe = bonRetourDetail.MontantTaxe;
                                    detailBS.PCInsertion = this.PCModification;
                                    detailBS.PrixHT = bonRetourDetail.PrixHT;
                                    detailBS.Quantite = Math.Abs(ancienDetail.Quantite - bonRetourDetail.Quantite);
                                    detailBS.TauxTVA = bonRetourDetail.TauxTVA;

                                    bonSortie.BonSortieDetailCollection.Add(detailBS);
                                }
                                else
                                    if (ancienDetail.Quantite == bonRetourDetail.Quantite)
                                        identique++;
                                string cEntrepot = BRDetail.CEntrepot;
                                bonRetourDetailCollection.Remove(BRDetail);

                                BRDetail = bonRetourDetailCollection.RecupererBonRetourDetail(bonRetourDetail.CEntrepot);
                                ancienBonRetour.BonRetourDetailCollection.Remove(ancienDetail);
                                if (BRDetail == null && !(identique == nombreAncien && identique == nouveau))
                                {
                                    BonRetourDetail ancien = ancienBonRetour.BonRetourDetailCollection.RecupererBonRetourDetail(cEntrepot);
                                    while (ancien != null)
                                    {
                                        BonSortieDetail detailBS = new BonSortieDetail();
                                        detailBS.CArticle = ancien.CArticle;
                                        detailBS.CEntrepot = ancien.CEntrepot;
                                        detailBS.CreePar = this.ModifiePar;
                                        detailBS.CUnite = ancien.CUnite;
                                        detailBS.LibArticle = ancien.LibArticle;
                                        detailBS.MontantTaxe = ancien.MontantTaxe;
                                        detailBS.PCInsertion = this.PCModification;
                                        detailBS.PrixHT = ancien.PrixHT;
                                        detailBS.Quantite = ancien.Quantite;
                                        detailBS.TauxTVA = ancien.TauxTVA;

                                        bonSortie.BonSortieDetailCollection.Add(detailBS);
                                        ancienBonRetour.BonRetourDetailCollection.Remove(ancien);
                                        ancien = ancienBonRetour.BonRetourDetailCollection.RecupererBonRetourDetail(cEntrepot);
                                    }
                                }
                                if (bonEntree.BonEntreeDetailCollection.Count() > 0)
                                    bonEntree.Inserer(transaction);
                                if (bonSortie.BonSortieDetailCollection.Count() > 0)
                                    bonSortie.Inserer(transaction);


                            }
                        }

                        #endregion Creer les mouvements
                    }

                    bonRetourDetail.NBonRetour = this.NBonRetour;
                    bonRetourDetail.Ordre = i++;
                    bonRetourDetail.Sauvegarder(transaction);

                    this.MiseAJourQuantiteHistorique(this.NBonLivraison, bonRetourDetail.CArticle, bonRetourDetail.OrdreBonLivraison, bonRetourDetail.Quantite, transaction);

                    if (!(bonCommande == null))
                    {
                        BonLivraisonDetail detailLivraison = bonLivraison.BonLivraisonDetailCollection.RecupererBonLivraisonDetail(this.NBonLivraison, bonRetourDetail.CArticle, bonRetourDetail.OrdreBonLivraison);
                        BonCommandeDetail detailCommande = bonCommande.BonCommandeDetailCollection.RecupererBonCommandeDetail(bonLivraison.NBonCommande, detailLivraison.CArticle, detailLivraison.OrdreBonCommande);
                        detailCommande.QuantiteHistorique = detailCommande.QuantiteHistorique + bonRetourDetail.Quantite;

                        if (bonCommande.Etat == VenteHelper.EtatBonCommande.LIVRE.ToString())
                            BonCommande.ModifierEtatBonCommande(bonCommande.NBonCommande, VenteHelper.EtatBonCommande.ENCOURS.ToString(), transaction);

                        StockHelper.MiseAJourStockReserver(bonRetourDetail.CArticle, bonRetourDetail.CEntrepot, bonRetourDetail.Quantite, 1, transaction);
                        detailCommande.Modifier(transaction);
                    }
                }
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT ISNULL(SUM(ISNULL(QuantiteHistorique,0)),0) Somme FROM BonLivraisonDetail WHERE NBonLivraison = '" + this.NBonLivraison + "'";
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        quantiteHistoriqueBL = quantiteHistoriqueBL + decimal.Parse(dr["Somme"].ToString());
                    }
                }
                if (quantiteHistoriqueBL == 0)
                    cmd.CommandText = "UPDATE  BonLivraison SET BRetour =1 WHERE NBonLivraison = '" + this.NBonLivraison + "'";

                else
                    cmd.CommandText = "UPDATE  BonLivraison SET BRetour =0  WHERE NBonLivraison = '" + this.NBonLivraison + "'";
                cmd.ExecuteNonQuery();
                foreach (BonRetourTaxe bonRetourTaxe in BonRetourTaxeCollection)
                {
                    bonRetourTaxe.NBonRetour = this.NBonRetour;
                    bonRetourTaxe.Sauvegarder(transaction);
                }
                if (!(this.BEchantillon))
                    VenteHelper.ModifierSolde(null, null, this.CClient, 0m, 0m, this.MontantTTC, 0m, 0m, 0m, transaction);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ModifierBonRetourAnterieur(SqlTransaction transaction)
        {
            BonRetour bonRetour = BonRetour.Charger(this.NBonRetour);
            if (!(this.BEchantillon))
                VenteHelper.ModifierSolde(null, null, this.CClient, 0m, 0m, -bonRetour.MontantTTC, 0m, 0m, 0m, transaction);
            BonRetourDetailCollection ancienBonRetourDetails = BonRetourDetailCollection.Charger(this.NBonRetour);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BonRetour_Modifier";
                cmd.Parameters.AddWithValue("@CUnite", this.CUnite);
                cmd.Parameters.AddWithValue("@CClient", this.CClient);
                cmd.Parameters.AddWithValue("@NBonRetour", this.NBonRetour);

                cmd.Parameters.AddWithValue("@BExonoreTVA ", this.BExonoreTVA);
                cmd.Parameters.AddWithValue("@BAvanceForfaitaire ", this.BAvanceForfaitaire);
                cmd.Parameters.AddWithValue("@BExonoreFodec", this.BExonoreFodec);
                cmd.Parameters.AddWithValue("@BEchantillon ", this.BEchantillon);
                cmd.Parameters.AddWithValue("@BDefectueux", this.BDefectueux);
                cmd.Parameters.AddWithValue("@BExport", this.BExport);
                cmd.Parameters.AddWithValue("@BTransfertAvoir", this.BTransfertAvoir);
                cmd.Parameters.AddWithValue("@BRetourAnterieur", this.BRetourAnterieur);

                cmd.Parameters.AddWithValue("@CVendeur ", this.CVendeur);
                cmd.Parameters.AddWithValue("@DateRetour ", this.DateRetour);
                cmd.Parameters.AddWithValue("@NFactureAnterieur", this.NFactureAnterieur);
                cmd.Parameters.AddWithValue("@MatriculeFiscale", this.MatriculeFiscale);
                cmd.Parameters.AddWithValue("@Adresse", this.Adresse);
                cmd.Parameters.AddWithValue("@NTelephone", this.NTelephone);
                cmd.Parameters.AddWithValue("@CMission", this.CMission);
                cmd.Parameters.AddWithValue("@NAvoir", this.NAvoir);
                cmd.Parameters.AddWithValue("@NBonLivraison", this.NBonLivraison);
                cmd.Parameters.AddWithValue("@Observation", this.Observation);
                cmd.Parameters.AddWithValue("@RaisonSociale", this.RaisonSociale);

                cmd.Parameters.AddWithValue("@Exercice", this.Exercice);
                cmd.Parameters.AddWithValue("@Reference", this.Reference);
                cmd.Parameters.AddWithValue("@MontantHT", this.MontantHT);
                cmd.Parameters.AddWithValue("@MontantRemise", this.MontantRemise);
                cmd.Parameters.AddWithValue("@MontantRetenuForfaitaire", this.MontantRetenuForfaitaire);
                cmd.Parameters.AddWithValue("@MontantTaxe", this.MontantTaxe);
                cmd.Parameters.AddWithValue("@MontantTTC", this.MontantTTC);
                cmd.Parameters.AddWithValue("@PoidsTotal", this.PoidsTotal);
                cmd.Parameters.AddWithValue("@PCModification", this.PCModification);
                cmd.Parameters.AddWithValue("@ModifiePar", this.ModifiePar);
                cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);

                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;
                cmd.ExecuteNonQuery();
                if (!(this.BEchantillon))
                    VenteHelper.ModifierSolde(null, null, this.CClient, 0m, 0m, this.MontantTTC, 0m, 0m, 0m, transaction);
                this.SupprimerDetailRetourAnterieurs(transaction);
                this.SupprimerTaxeRetourAnterieurs(transaction);
                int i = 1;

                BonRetourDetailCollection bonRetourDetailCollection = new BonRetourDetailCollection();

                if (!BDefectueux)
                {
                    foreach (BonRetourDetail bonRetourDetail in BonRetourDetailCollection)
                        bonRetourDetailCollection.Add(bonRetourDetail);
                }
                foreach (BonRetourDetail bonRetourDetail in BonRetourDetailCollection)
                {
                    if (!BDefectueux)
                    {
                        BonRetourDetail BRDetail = bonRetourDetailCollection.RecupererBonRetourDetail(bonRetourDetail.CEntrepot);
                        BonEntree bonEntree = new BonEntree();
                        bonEntree.CEntrepot = BRDetail.CEntrepot;
                        bonEntree.DateEntree = DateTime.Now;
                        bonEntree.CClient = this.CClient;
                        bonEntree.BFodecExonore = this.BExonoreFodec;
                        bonEntree.BTvaExonore = this.BExonoreTVA;
                        bonEntree.CreePar = this.ModifiePar;
                        bonEntree.Exercice = this.Exercice;
                        bonEntree.NDocumentSource = this.NBonLivraison;
                        bonEntree.RaisonSociale = this.RaisonSociale;
                        bonEntree.TypeMouvement = StockHelper.TypesMouvementStock.BE_BONRETOURCLIENT.ToString();
                        bonEntree.PCInsertion = this.PCModification;

                        BonSortie bonSortie = new BonSortie();
                        bonSortie.CClient = this.CClient;
                        bonSortie.CEntrepot = BRDetail.CEntrepot;
                        bonSortie.CreePar = this.ModifiePar;
                        bonSortie.DateSortie = (DateTime)this.DateRetour;
                        bonSortie.Exercice = this.Exercice;
                        bonSortie.NDocumentSource = this.NBonLivraison;
                        bonSortie.RaisonSociale = this.RaisonSociale;
                        bonSortie.TypeMouvement = StockHelper.TypesMouvementStock.BS_BONRETOURCLIENT.ToString();
                        bonSortie.PCInsertion = this.PCModification;

                        while (!(BRDetail == null))
                        {
                            BonRetourDetail ancienDetail = ancienBonRetourDetails.RecupererBonRetourDetail(this.NBonRetour, bonRetourDetail.CArticle);

                            ancienBonRetourDetails.Remove(ancienDetail);

                            if ((ancienDetail == null) & (ancienDetail.Quantite < bonRetourDetail.Quantite))
                            {
                                BonEntreeDetail detailBE = new BonEntreeDetail();
                                detailBE.CArticle = bonRetourDetail.CArticle;
                                detailBE.CEntrepot = bonRetourDetail.CEntrepot;
                                detailBE.CreePar = this.ModifiePar;
                                detailBE.CTaxe = bonRetourDetail.CTaxe;
                                detailBE.CUnite = bonRetourDetail.CUnite;
                                detailBE.LibArticle = bonRetourDetail.LibArticle;
                                detailBE.PourcentageFodec = bonRetourDetail.PourcentageFodec;
                                detailBE.PourcentageRemise = bonRetourDetail.PourcentageRemise;
                                detailBE.PrixRevient = bonRetourDetail.PrixRevient;
                                detailBE.Quantite = Math.Abs(ancienDetail.Quantite - bonRetourDetail.Quantite);
                                detailBE.TauxTVA = bonRetourDetail.TauxTVA;
                                bonEntree.BonEntreeDetailCollection.Add(detailBE);
                            }
                            else if (!(ancienDetail == null) || (ancienDetail.Quantite > bonRetourDetail.Quantite))
                            {
                                BonSortieDetail detailBS = new BonSortieDetail();
                                detailBS.CArticle = bonRetourDetail.CArticle;
                                detailBS.CEntrepot = bonRetourDetail.CEntrepot;
                                detailBS.CreePar = this.ModifiePar;
                                detailBS.CUnite = bonRetourDetail.CUnite;
                                detailBS.LibArticle = bonRetourDetail.LibArticle;
                                detailBS.MontantTaxe = bonRetourDetail.MontantTaxe;
                                detailBS.PCInsertion = this.PCModification;
                                detailBS.PrixHT = bonRetourDetail.PrixHT;
                                detailBS.Quantite = Math.Abs(bonRetourDetail.Quantite - ancienDetail.Quantite);
                                detailBS.TauxTVA = bonRetourDetail.TauxTVA;

                                bonSortie.BonSortieDetailCollection.Add(detailBS);
                            }
                            if (bonEntree.BonEntreeDetailCollection.Count() > 0)
                                bonEntree.Inserer(transaction);
                            if (bonSortie.BonSortieDetailCollection.Count() > 0)
                                bonSortie.Inserer(transaction);

                            bonRetourDetailCollection.Remove(BRDetail);
                            BRDetail = bonRetourDetailCollection.RecupererBonRetourDetail(bonRetourDetail.CEntrepot);
                        }
                    }

                    bonRetourDetail.NBonRetour = this.NBonRetour;
                    bonRetourDetail.Ordre = i++;
                    bonRetourDetail.Sauvegarder(transaction);
                }

                foreach (BonRetourTaxe bonRetourTaxe in BonRetourTaxeCollection)
                {
                    bonRetourTaxe.NBonRetour = this.NBonRetour;
                    bonRetourTaxe.Sauvegarder(transaction);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void RestituerSoldeBonRetour(SqlTransaction transaction)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Transaction = transaction;
            cmd.Connection = transaction.Connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Client_RestituerSoldeBonRetour";

            cmd.Parameters.AddWithValue("@NBonRetour", this.NBonRetour);
            cmd.Parameters.AddWithValue("@CClient", this.CClient);
            foreach (SqlParameter parametre in cmd.Parameters)
                if (parametre.Value == null)
                    parametre.Value = DBNull.Value;

            cmd.ExecuteNonQuery();
        }

        private void RestituerQuantiteHistorique(SqlTransaction transaction)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Transaction = transaction;
            cmd.Connection = transaction.Connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "BonLivraison_RestituerQuantiteHistorique";

            cmd.Parameters.AddWithValue("@NBonRetour", this.NBonRetour);
            foreach (SqlParameter parametre in cmd.Parameters)
                if (parametre.Value == null)
                    parametre.Value = DBNull.Value;

            cmd.ExecuteNonQuery();
        }

        public static string RecupererNumeroBonRetour(string exercice, out int indice)
        {
            string nBonRetour = string.Empty;
            indice = 0;

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                var cmd2 = new SqlCommand();
                cmd2.Connection = cn;
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.CommandText = "BonRetour_RecupererNouveauNumero";
                cmd2.Parameters.AddWithValue("@Exercice", exercice);
                SqlDataReader dr = cmd2.ExecuteReader();
                if (dr.Read())
                {
                    nBonRetour = dr["NBonRetour"].ToString();
                    indice = int.Parse(dr["DernierIndice"].ToString());
                }
                dr.Close();
            }

            return nBonRetour;
        }

        public static string RecupererNumeroBonRetour(string exercice, string CUnite)
        {
            int indice = 0;
            return BonRetour.RecupererNumeroBonRetour(exercice, out indice);
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
                    cmd.CommandText = "BonRetour_Supprimer";
                    cmd.Parameters.AddWithValue("@NBonRetour", this.NBonRetour); ;
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
        }

        private void SupprimerDetailRetourAnterieurs(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BonRetour_SupprimerDetails";

                cmd.Parameters.AddWithValue("@NBonRetour", this.NBonRetour);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SupprimerTaxeRetourAnterieurs(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BonRetour_SupprimerTaxes";

                cmd.Parameters.AddWithValue("@NBonRetour", this.NBonRetour);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static BonRetour Charger(string nBonRetour)
        {
            BonRetour bonRetour = null;
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
                    cmd.CommandText = "BonRetour_Charger";
                    cmd.Parameters.AddWithValue("@NBonRetour", nBonRetour);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            bonRetour = new BonRetour();
                            bonRetour.NBonRetour = dr["NBonRetour"].ToString();

                            if (dr["CUnite"] != DBNull.Value)
                                bonRetour.CUnite = dr["CUnite"].ToString();
                            if (dr["CClient"] != DBNull.Value)
                                bonRetour.CClient = dr["CClient"].ToString();

                            if (dr["CVendeur"] != DBNull.Value)
                                bonRetour.CVendeur = int.Parse(dr["CVendeur"].ToString());
                            if (dr["NAvoir"] != DBNull.Value)
                                bonRetour.NAvoir = dr["NAvoir"].ToString();
                            if (dr["Observation"] != DBNull.Value)
                                bonRetour.Observation = dr["Observation"].ToString();
                            if (dr["RaisonSociale"] != DBNull.Value)
                                bonRetour.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["DateInsertion"] != DBNull.Value)
                                bonRetour.DateInsertion = DateTime.Parse(dr["DateInsertion"].ToString());
                            if (dr["DateModification"] != DBNull.Value)
                                bonRetour.DateModification = DateTime.Parse(dr["DateModification"].ToString());
                            if (dr["DateRetour"] != DBNull.Value)
                                bonRetour.DateRetour = DateTime.Parse(dr["DateRetour"].ToString());
                            if (dr["NBonLivraison"] != DBNull.Value)
                                bonRetour.NBonLivraison = dr["NBonLivraison"].ToString();
                            if (dr["MatriculeFiscale"] != DBNull.Value)
                                bonRetour.MatriculeFiscale = dr["MatriculeFiscale"].ToString();
                            if (dr["Adresse"] != DBNull.Value)
                                bonRetour.Adresse = dr["Adresse"].ToString();
                            if (dr["NTelephone"] != DBNull.Value)
                                bonRetour.NTelephone = dr["NTelephone"].ToString();
                            if (dr["NFactureAnterieur"] != DBNull.Value)
                                bonRetour.NFactureAnterieur = dr["NFactureAnterieur"].ToString();
                            if (dr["CMission"] != DBNull.Value)
                                bonRetour.CMission = dr["CMission"].ToString();
                            if (dr["Indice"] != DBNull.Value)
                                bonRetour.Indice = int.Parse(dr["Indice"].ToString());

                            if (dr["MontantHT"] != DBNull.Value)
                                bonRetour.MontantHT = decimal.Parse(dr["MontantHT"].ToString());
                            if (dr["MontantRemise"] != DBNull.Value)
                                bonRetour.MontantRemise = decimal.Parse(dr["MontantRemise"].ToString());
                            if (dr["MontantRetenuForfaitaire"] != DBNull.Value)
                                bonRetour.MontantRetenuForfaitaire = decimal.Parse(dr["MontantRetenuForfaitaire"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                bonRetour.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["MontantTTC"] != DBNull.Value)
                                bonRetour.MontantTTC = decimal.Parse(dr["MontantTTC"].ToString());

                            if (dr["BExonoreTVA"] != DBNull.Value)
                                bonRetour.BExonoreTVA = bool.Parse(dr["BExonoreTVA"].ToString());
                            if (dr["BAvanceForfaitaire"] != DBNull.Value)
                                bonRetour.BAvanceForfaitaire = bool.Parse(dr["BAvanceForfaitaire"].ToString());
                            if (dr["BExonoreFodec"] != DBNull.Value)
                                bonRetour.BExonoreFodec = bool.Parse(dr["BExonoreFodec"].ToString());
                            if (dr["BDefectueux"] != DBNull.Value)
                                bonRetour.BDefectueux = bool.Parse(dr["BDefectueux"].ToString());
                            if (dr["BEchantillon"] != DBNull.Value)
                                bonRetour.BEchantillon = bool.Parse(dr["BEchantillon"].ToString());
                            if (dr["BExport"] != DBNull.Value)
                                bonRetour.BExport = bool.Parse(dr["BExport"].ToString());
                            if (dr["BRetourAnterieur"] != DBNull.Value)
                                bonRetour.BRetourAnterieur = bool.Parse(dr["BRetourAnterieur"].ToString());
                            if (dr["BTransfertAvoir"] != DBNull.Value)
                                bonRetour.BTransfertAvoir = bool.Parse(dr["BTransfertAvoir"].ToString());
                            if (dr["Reference"] != DBNull.Value)
                                bonRetour.Reference = dr["Reference"].ToString();
                            if (dr["PoidsTotal"] != DBNull.Value)
                                bonRetour.PoidsTotal = decimal.Parse(dr["PoidsTotal"].ToString());
                            bonRetour.BonRetourDetailCollection = BonRetourDetailCollection.Charger(bonRetour.NBonRetour);
                            bonRetour.BonRetourTaxeCollection = BonRetourTaxeCollection.Charger(bonRetour.NBonRetour);
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return (bonRetour);
            }
        }

        public static BonRetour Charger(string nBonRetour, string nBonLivraison)
        {
            BonRetour bonRetour = null;
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
                    cmd.CommandText = "BonRetour_Charger";
                    cmd.Parameters.AddWithValue("@NBonRetour", nBonRetour);
                    cmd.Parameters.AddWithValue("@NBonLivraison", nBonLivraison);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            bonRetour = new BonRetour();
                            bonRetour.NBonRetour = dr["NBonRetour"].ToString();

                            if (dr["CUnite"] != DBNull.Value)
                                bonRetour.CUnite = dr["CUnite"].ToString();
                            if (dr["CClient"] != DBNull.Value)
                                bonRetour.CClient = dr["CClient"].ToString();

                            if (dr["CVendeur"] != DBNull.Value)
                                bonRetour.CVendeur = int.Parse(dr["CVendeur"].ToString());
                            if (dr["NAvoir"] != DBNull.Value)
                                bonRetour.NAvoir = dr["NAvoir"].ToString();
                            if (dr["Observation"] != DBNull.Value)
                                bonRetour.Observation = dr["Observation"].ToString();
                            if (dr["RaisonSociale"] != DBNull.Value)
                                bonRetour.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["DateRetour"] != DBNull.Value)
                                bonRetour.DateRetour = DateTime.Parse(dr["DateRetour"].ToString());
                            if (dr["NBonLivraison"] != DBNull.Value)
                                bonRetour.NBonLivraison = dr["NBonLivraison"].ToString();
                            if (dr["MatriculeFiscale"] != DBNull.Value)
                                bonRetour.MatriculeFiscale = dr["MatriculeFiscale"].ToString();
                            if (dr["Adresse"] != DBNull.Value)
                                bonRetour.Adresse = dr["Adresse"].ToString();
                            if (dr["NTelephone"] != DBNull.Value)
                                bonRetour.NTelephone = dr["NTelephone"].ToString();
                            if (dr["NFactureAnterieur"] != DBNull.Value)
                                bonRetour.NFactureAnterieur = dr["NFactureAnterieur"].ToString();
                            if (dr["CMission"] != DBNull.Value)
                                bonRetour.CMission = dr["CMission"].ToString();
                            if (dr["TypeRetour"] != DBNull.Value)
                                bonRetour.TypeRetour = bool.Parse(dr["TypeRetour"].ToString());

                            if (dr["MontantHT"] != DBNull.Value)
                                bonRetour.MontantHT = decimal.Parse(dr["MontantHT"].ToString());
                            if (dr["MontantRemise"] != DBNull.Value)
                                bonRetour.MontantRemise = decimal.Parse(dr["MontantRemise"].ToString());
                            if (dr["MontantRetenuForfaitaire"] != DBNull.Value)
                                bonRetour.MontantRetenuForfaitaire = decimal.Parse(dr["MontantRetenuForfaitaire"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                bonRetour.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["MontantTTC"] != DBNull.Value)
                                bonRetour.MontantTTC = decimal.Parse(dr["MontantTTC"].ToString());
                            if (dr["BExonoreTVA"] != DBNull.Value)
                                bonRetour.BExonoreTVA = bool.Parse(dr["BExonoreTVA"].ToString());
                            if (dr["BAvanceForfaitaire"] != DBNull.Value)
                                bonRetour.BAvanceForfaitaire = bool.Parse(dr["BAvanceForfaitaire"].ToString());
                            if (dr["BExonoreFodec"] != DBNull.Value)
                                bonRetour.BExonoreFodec = bool.Parse(dr["BExonoreFodec"].ToString());
                            if (dr["BDefectueux"] != DBNull.Value)
                                bonRetour.BDefectueux = bool.Parse(dr["BDefectueux"].ToString());
                            if (dr["BEchantillon"] != DBNull.Value)
                                bonRetour.BEchantillon = bool.Parse(dr["BEchantillon"].ToString());
                            if (dr["BExport"] != DBNull.Value)
                                bonRetour.BExport = bool.Parse(dr["BExport"].ToString());
                            if (dr["BRetourAnterieur"] != DBNull.Value)
                                bonRetour.BRetourAnterieur = bool.Parse(dr["BRetourAnterieur"].ToString());
                            if (dr["BTransfertAvoir"] != DBNull.Value)
                                bonRetour.BTransfertAvoir = bool.Parse(dr["BTransfertAvoir"].ToString());
                            if (dr["Reference"] != DBNull.Value)
                                bonRetour.Reference = dr["Reference"].ToString();
                            if (dr["PoidsTotal"] != DBNull.Value)
                                bonRetour.PoidsTotal = decimal.Parse(dr["PoidsTotal"].ToString());
                            bonRetour.BonRetourDetailCollection = BonRetourDetailCollection.Charger(bonRetour.NBonRetour);
                            bonRetour.BonRetourTaxeCollection = BonRetourTaxeCollection.Charger(bonRetour.NBonRetour);
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return (bonRetour);
            }
        }
    }

    public class BonRetourCollection : List<BonRetour>
    {
        public BonRetourCollection()
        {
        }

        public BonRetourCollection Charger(string nBonRetour)
        {
            BonRetourCollection collection = new BonRetourCollection();
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
                    cmd.CommandText = "BonRetour_Charger";
                    cmd.Parameters.AddWithValue("@NBonRetour", nBonRetour);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            BonRetour bonRetour = new BonRetour();
                            bonRetour.NBonRetour = dr["NBonEntree"].ToString();

                            if (dr["CUnite"] != DBNull.Value)
                                bonRetour.CUnite = dr["CUnite"].ToString();
                            if (dr["CClient"] != DBNull.Value)
                                bonRetour.CClient = dr["CClient"].ToString();

                            if (dr["CVendeur"] != DBNull.Value)
                                bonRetour.CVendeur = int.Parse(dr["CVendeur"].ToString());
                            if (dr["NAvoir"] != DBNull.Value)
                                bonRetour.NAvoir = dr["NAvoir"].ToString();
                            if (dr["Observation"] != DBNull.Value)
                                bonRetour.Observation = dr["Observation"].ToString();
                            if (dr["RaisonSociale"] != DBNull.Value)
                                bonRetour.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["DateRetour"] != DBNull.Value)
                                bonRetour.DateRetour = DateTime.Parse(dr["DateRetour"].ToString());
                            if (dr["NBonLivraison"] != DBNull.Value)
                                bonRetour.NBonLivraison = dr["NBonLivraison"].ToString();
                            if (dr["Indice"] != DBNull.Value)
                                bonRetour.Indice = int.Parse(dr["Indice"].ToString());
                            if (dr["MatriculeFiscale"] != DBNull.Value)
                                bonRetour.MatriculeFiscale = dr["MatriculeFiscale"].ToString();
                            if (dr["Adresse"] != DBNull.Value)
                                bonRetour.Adresse = dr["Adresse"].ToString();
                            if (dr["NTelephone"] != DBNull.Value)
                                bonRetour.NTelephone = dr["NTelephone"].ToString();
                            if (dr["NFactureAnterieur"] != DBNull.Value)
                                bonRetour.NFactureAnterieur = dr["NFactureAnterieur"].ToString();
                            if (dr["CMission"] != DBNull.Value)
                                bonRetour.CMission = dr["CMission"].ToString();
                            if (dr["TypeRetour"] != DBNull.Value)
                                bonRetour.Indice = int.Parse(dr["TypeRetour"].ToString());
                            if (dr["Exercice"] != DBNull.Value)
                                bonRetour.Indice = int.Parse(dr["Exercice"].ToString());

                            if (dr["MontantHT"] != DBNull.Value)
                                bonRetour.MontantHT = decimal.Parse(dr["MontantHT"].ToString());
                            if (dr["MontantRemise"] != DBNull.Value)
                                bonRetour.MontantRemise = decimal.Parse(dr["MontantRemise"].ToString());
                            if (dr["MontantRetenuForfaitaire"] != DBNull.Value)
                                bonRetour.MontantRetenuForfaitaire = decimal.Parse(dr["MontantRetenuForfaitaire"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                bonRetour.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["MontantTTC"] != DBNull.Value)
                                bonRetour.MontantTTC = decimal.Parse(dr["MontantTTC"].ToString());

                            if (dr["BExonoreTVA"] != DBNull.Value)
                                bonRetour.BExonoreTVA = bool.Parse(dr["BExonoreTVA"].ToString());
                            if (dr["BAvanceForfaitaire"] != DBNull.Value)
                                bonRetour.BAvanceForfaitaire = bool.Parse(dr["BAvanceForfaitaire"].ToString());
                            if (dr["BExonereFodec"] != DBNull.Value)
                                bonRetour.BExonoreFodec = bool.Parse(dr["BExonereFodec"].ToString());
                            if (dr["BDefectueux"] != DBNull.Value)
                                bonRetour.BDefectueux = bool.Parse(dr["BDefectueux"].ToString());
                            if (dr["BEchantillon"] != DBNull.Value)
                                bonRetour.BEchantillon = bool.Parse(dr["BEchantillon"].ToString());
                            if (dr["BExport"] != DBNull.Value)
                                bonRetour.BExport = bool.Parse(dr["BExport"].ToString());
                            if (dr["BRetourAnterieur"] != DBNull.Value)
                                bonRetour.BRetourAnterieur = bool.Parse(dr["BRetourAnterieur"].ToString());
                            if (dr["BTransfertAvoir"] != DBNull.Value)
                                bonRetour.BTransfertAvoir = bool.Parse(dr["BTransfertAvoir"].ToString());
                            if (dr["Reference"] != DBNull.Value)
                                bonRetour.Reference = dr["Reference"].ToString();
                            if (dr["PoidsTotal"] != DBNull.Value)
                                bonRetour.PoidsTotal = decimal.Parse(dr["PoidsTotal"].ToString());
                            bonRetour.BonRetourDetailCollection = BonRetourDetailCollection.Charger(bonRetour.NBonRetour);
                            bonRetour.BonRetourTaxeCollection = BonRetourTaxeCollection.Charger(bonRetour.NBonRetour);
                            collection.Add(bonRetour);
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