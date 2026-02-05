using CST.LePoint.Stock.Metier;
using CST.LePoint.Tiers.Metier;
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
    public class BonLivraison
    {
        #region Proriétès

        [XmlAttribute("NBonLivraison")]
        [Bindable(true)]
        public string NBonLivraison { get; set; }

        [XmlAttribute("CClient")]
        [Bindable(true)]
        public string CClient { get; set; }

        [XmlAttribute("DateLivraison")]
        [Bindable(true)]
        public DateTime? DateLivraison { get; set; }

        [XmlAttribute("CUnite")]
        [Bindable(true)]
        public string CUnite { get; set; }

        [XmlAttribute("BEchantillon")]
        [Bindable(true)]
        public bool BEchantillon { get; set; }

        [XmlAttribute("BGratuit")]
        [Bindable(true)]
        public bool BGratuit { get; set; }

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

        [XmlAttribute("BExonereFodec")]
        [Bindable(true)]
        public bool BExonoreFodec { get; set; }

        [XmlAttribute("BExonoreTVA")]
        [Bindable(true)]
        public bool BExonoreTVA { get; set; }

        [XmlAttribute("BAvanceForfaitaire")]
        [Bindable(true)]
        public bool BAvanceForfaitaire { get; set; }

        //[XmlAttribute("BImprimer")]
        //[Bindable(true)]
        //public bool BImprimer { get; set; }

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

        [XmlAttribute("NBonCommande")]
        [Bindable(true)]
        public string NBonCommande { get; set; }

        [XmlAttribute("NOrdrePreparation")]
        [Bindable(true)]
        public string NOrdrePreparation { get; set; }

        [XmlAttribute("NBonCommandeMannuel")]
        [Bindable(true)]
        public string NBonCommandeMannuel { get; set; }

        [XmlAttribute("NBonLivraisonMannuel")]
        [Bindable(true)]
        public string NBonLivraisonMannuel { get; set; }

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

        [XmlAttribute("BRetour")]
        [Bindable(true)]
        public bool BRetour { get; set; }

        [XmlAttribute("Chauffeur")]
        [Bindable(true)]
        public String Chauffeur { get; set; }

        [XmlAttribute("CVehicule")]
        [Bindable(true)]
        public String CVehicule { get; set; }

        [XmlAttribute("CMission")]
        [Bindable(true)]
        public string CMission { get; set; }

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

        [XmlAttribute("PCModication")]
        [Bindable(true)]
        public string PCModification { get; set; }

        [XmlAttribute("Exercice")]
        [Bindable(true)]
        public string Exercice { get; set; }

        [XmlAttribute("poidsTotal")]
        [Bindable(true)]
        public decimal poidsTotal { get; set; }

        [XmlAttribute("NOrdredeTravail")]
        [Bindable(true)]
        public string NOrdredeTravail { get; set; }

        public BonLivraisonDetailCollection BonLivraisonDetailCollection;
        public BonLivraisonTaxeCollection BonLivraisonTaxeCollection;

        #endregion Proriétès

        public BonLivraison()
        {
            this.BonLivraisonDetailCollection = new BonLivraisonDetailCollection();
            this.BonLivraisonTaxeCollection = new BonLivraisonTaxeCollection();
        }

        public BonLivraison(string nBonLivraison)
        {
            this.NBonLivraison = nBonLivraison;
            this.BonLivraisonDetailCollection = new BonLivraisonDetailCollection();
            this.BonLivraisonTaxeCollection = new BonLivraisonTaxeCollection();
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
            string numeroAncienBL = string.Empty;
            try
            {
                BonCommande bonCommande = new BonCommande();
                if (!string.IsNullOrEmpty(this.NBonCommande))
                    bonCommande = BonCommande.Charger(this.NBonCommande, transaction);

                if (bonCommande.NBonCommande == null || !bonCommande.Etat.Equals(VenteHelper.EtatBonCommande.LIVRE.ToString()))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Transaction = transaction;
                    cmd.Connection = transaction.Connection;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "BonLivraison_Inserer";
                    cmd.Parameters.AddWithValue("@CClient", this.CClient);
                    cmd.Parameters.AddWithValue("@DateLivraison ", this.DateLivraison);
                    cmd.Parameters.AddWithValue("@CUnite", this.CUnite);
                    cmd.Parameters.AddWithValue("@BEchantillon ", this.BEchantillon);
                    cmd.Parameters.AddWithValue("@BGratuit", this.BGratuit);
                    cmd.Parameters.AddWithValue("@MatriculeFiscale", this.MatriculeFiscale);
                    cmd.Parameters.AddWithValue("@Adresse", this.Adresse);
                    cmd.Parameters.AddWithValue("@NTelephone", this.NTelephone);
                    cmd.Parameters.AddWithValue("@CVendeur ", this.CVendeur);
                    cmd.Parameters.AddWithValue("@BExonoreFodec", this.BExonoreFodec);
                    cmd.Parameters.AddWithValue("@BExonoreTVA", this.BExonoreTVA);
                    cmd.Parameters.AddWithValue("@BAvanceForfaitaire", this.BAvanceForfaitaire);
                    cmd.Parameters.AddWithValue("@MontantHT", this.MontantHT);
                    cmd.Parameters.AddWithValue("@MontantRemise", this.MontantRemise);
                    cmd.Parameters.AddWithValue("@MontantTaxe", this.MontantTaxe);
                    cmd.Parameters.AddWithValue("@MontantTTC", this.MontantTTC);
                    cmd.Parameters.AddWithValue("@NBonCommande", this.NBonCommande);
                    cmd.Parameters.AddWithValue("@NOrdrePreparation", this.NOrdrePreparation);
                    cmd.Parameters.AddWithValue("@NBonCommandeMannuel", this.NBonCommandeMannuel);
                    cmd.Parameters.AddWithValue("@NBonLivraisonMannuel", this.NBonLivraisonMannuel);
                    cmd.Parameters.AddWithValue("@NFacture", this.NFacture);
                    cmd.Parameters.AddWithValue("@Observation", this.Observation);
                    cmd.Parameters.AddWithValue("@RaisonSociale", this.RaisonSociale);
                    cmd.Parameters.AddWithValue("@BRetour", this.BRetour);
                    cmd.Parameters.AddWithValue("@Chauffeur", this.Chauffeur);
                    cmd.Parameters.AddWithValue("@CVehicule", this.CVehicule);
                    cmd.Parameters.AddWithValue("@CMission", this.CMission);
                    cmd.Parameters.AddWithValue("@MontantRetenuForfaitaire", this.MontantRetenuForfaitaire);
                    cmd.Parameters.AddWithValue("@poidsTotal", this.poidsTotal);
                    cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                    cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);
                    cmd.Parameters.AddWithValue("@CreePar", this.CreePar);
                    cmd.Parameters.AddWithValue("@Exercice", this.Exercice);
                    cmd.Parameters.AddWithValue("@NOrdredeTravail", this.NOrdredeTravail);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            this.NBonLivraison = dr["NBonLivraison"].ToString();
                            this.Indice = int.Parse(dr["DernierIndice"].ToString());
                        }
                    }
                    int i = 1;
                    decimal sommeQuantite = 0m;

                    foreach (BonLivraisonDetail bonLivraisonDetail in BonLivraisonDetailCollection)
                    {
                        if (!(string.IsNullOrEmpty(this.NBonCommande)))
                        {
                            BonCommandeDetail detailCommande = bonCommande.BonCommandeDetailCollection.RecupererBonCommandeDetail(this.NBonCommande, bonLivraisonDetail.CArticle, bonLivraisonDetail.OrdreBonCommande);
                            if (detailCommande != null)
                            {
                                if (bonLivraisonDetail.Quantite > detailCommande.QuantiteHistorique)

                                    bonLivraisonDetail.QuantiteRestore = detailCommande.QuantiteHistorique;

                                else
                                    bonLivraisonDetail.QuantiteRestore = bonLivraisonDetail.Quantite;

                                detailCommande.QuantiteHistorique = detailCommande.QuantiteHistorique - bonLivraisonDetail.QuantiteRestore;
                                sommeQuantite = sommeQuantite + detailCommande.QuantiteHistorique;
                                detailCommande.Modifier(transaction);
                                bonCommande.BonCommandeDetailCollection.Remove(detailCommande);
                            }
                            StockHelper.MiseAJourStockReserver(bonLivraisonDetail.CArticle, bonLivraisonDetail.CEntrepot, bonLivraisonDetail.QuantiteRestore, -1, transaction);
                            OrdrePreparationCollection collection = OrdrePreparationCollection.Charger(transaction, this.NBonCommande);
                            if (collection != null)
                            {
                                foreach (OrdrePreparation ordre in collection)
                                {
                                    //ordre.BLivre = true;
                                    ordre.AnnulerOrdre(transaction);
                                }
                            }
                        }

                        bonLivraisonDetail.NBonLivraison = this.NBonLivraison;
                        bonLivraisonDetail.Ordre = i++;
                        bonLivraisonDetail.Sauvegarder(transaction);
                    }
                    while (bonCommande.BonCommandeDetailCollection.Count != 0)
                    {
                        sommeQuantite = sommeQuantite + bonCommande.BonCommandeDetailCollection[0].QuantiteHistorique;
                        bonCommande.BonCommandeDetailCollection.Remove(bonCommande.BonCommandeDetailCollection[0]);
                    }
                    if (sommeQuantite == 0)
                        BonCommande.ModifierEtatBonCommande(this.NBonCommande, VenteHelper.EtatBonCommande.LIVRE.ToString(), transaction);
                    else
                        BonCommande.ModifierEtatBonCommande(this.NBonCommande, VenteHelper.EtatBonCommande.ENCOURS.ToString(), transaction);
                    this.SupprimerTaxeBonLivraisonAnterieurs(transaction);
                    foreach (BonLivraisonTaxe bonLivraisonTaxe in BonLivraisonTaxeCollection)
                    {
                        bonLivraisonTaxe.NBonLivraison = this.NBonLivraison;
                        bonLivraisonTaxe.Sauvegarder(transaction);
                    }

                    CreerBonSortie(transaction);

                    VenteHelper.ModifierSolde(null, null, this.CClient, this.MontantTTC, 0m, 0m, 0m, 0m, 0m, transaction);
                }
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
            BonLivraison ancienBonLivraison = BonLivraison.Charger(this.NBonLivraison);
            this.RestituerSoldeBonLivraison(transaction, ancienBonLivraison.CClient);
            int tailleAncien = ancienBonLivraison.BonLivraisonDetailCollection.Count;
            BonCommande bonCommande = new BonCommande();
            if (!string.IsNullOrEmpty(ancienBonLivraison.NBonCommande))
            {
                bonCommande = BonCommande.Charger(ancienBonLivraison.NBonCommande, transaction);
                if (!bonCommande.Etat.Equals(VenteHelper.EtatBonCommande.PURGER.ToString()))
                {
                    this.NBonCommande = ancienBonLivraison.NBonCommande;
                    this.RestituerQuantiteHistorique(transaction);
                    bonCommande = BonCommande.Charger(ancienBonLivraison.NBonCommande, transaction);
                }
            }
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BonLivraison_Modifier";
                cmd.Parameters.AddWithValue("@NBonLivraison", this.NBonLivraison);
                cmd.Parameters.AddWithValue("@CClient", this.CClient);
                cmd.Parameters.AddWithValue("@DateLivraison ", this.DateLivraison);
                cmd.Parameters.AddWithValue("@CUnite", this.CUnite);
                cmd.Parameters.AddWithValue("@BEchantillon ", this.BEchantillon);
                cmd.Parameters.AddWithValue("@BGratuit", this.BGratuit);
                cmd.Parameters.AddWithValue("@MatriculeFiscale", this.MatriculeFiscale);
                cmd.Parameters.AddWithValue("@Adresse", this.Adresse);
                cmd.Parameters.AddWithValue("@NTelephone", this.NTelephone);
                cmd.Parameters.AddWithValue("@CVendeur ", this.CVendeur);
                cmd.Parameters.AddWithValue("@BExonoreFodec", this.BExonoreFodec);
                cmd.Parameters.AddWithValue("@BExonoreTVA", this.BExonoreTVA);
                cmd.Parameters.AddWithValue("@BAvanceForfaitaire", this.BAvanceForfaitaire);
                cmd.Parameters.AddWithValue("@MontantHT", this.MontantHT);
                cmd.Parameters.AddWithValue("@MontantRemise", this.MontantRemise);
                cmd.Parameters.AddWithValue("@MontantTaxe", this.MontantTaxe);
                cmd.Parameters.AddWithValue("@MontantTTC", this.MontantTTC);
                cmd.Parameters.AddWithValue("@NBonCommande", ancienBonLivraison.NBonCommande);
                cmd.Parameters.AddWithValue("@NOrdrePreparation", ancienBonLivraison.NOrdrePreparation);
                cmd.Parameters.AddWithValue("@NBonCommandeMannuel", this.NBonCommandeMannuel);
                cmd.Parameters.AddWithValue("@NBonLivraisonMannuel", this.NBonLivraisonMannuel);
                cmd.Parameters.AddWithValue("@NFacture", this.NFacture);
                cmd.Parameters.AddWithValue("@Observation", this.Observation);
                cmd.Parameters.AddWithValue("@RaisonSociale", this.RaisonSociale);
                cmd.Parameters.AddWithValue("@BRetour", this.BRetour);
                cmd.Parameters.AddWithValue("@Chauffeur", this.Chauffeur);
                cmd.Parameters.AddWithValue("@CVehicule", this.CVehicule);
                cmd.Parameters.AddWithValue("@CMission", this.CMission);
                cmd.Parameters.AddWithValue("@MontantRetenuForfaitaire", this.MontantRetenuForfaitaire);
                cmd.Parameters.AddWithValue("@poidsTotal", this.poidsTotal);
                cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                cmd.Parameters.AddWithValue("@PCModification", this.PCModification);
                cmd.Parameters.AddWithValue("@ModifiePar", this.ModifiePar);
                cmd.Parameters.AddWithValue("@NOrdredeTravail", this.NOrdredeTravail);
                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                cmd.ExecuteNonQuery();
                if (ancienBonLivraison.DateLivraison != this.DateLivraison)
                {
                    BonEntree bonEntree = BonEntree.ChargerParDocumentSource(StockHelper.TypesMouvementStock.BE_BONLIVRAISONCLIENT.ToString(), this.NBonLivraison);

                    if (bonEntree != null)
                    {
                        bonEntree.DateEntree = (DateTime)this.DateLivraison;
                        bonEntree.PCModification = this.PCModification;
                        bonEntree.ModifiePar = this.ModifiePar;
                        bonEntree.Modifier(transaction);
                    }

                    BonSortie bonSortie = BonSortie.ChargerParDocumentSource(StockHelper.TypesMouvementStock.BS_BONLIVRAISONCLIENT.ToString(), this.NBonLivraison);
                    if (bonSortie != null)
                    {
                        bonSortie.DateSortie = (DateTime)this.DateLivraison;
                        bonSortie.PCModification = this.PCModification;
                        bonSortie.ModifiePar = this.ModifiePar;
                        bonSortie.Modifier(transaction);
                    }
                }
                this.SupprimerDetailBonLivraisonAnterieurs(transaction);
                this.SupprimerTaxeBonLivraisonAnterieurs(transaction);

                int i = 1;
                BonLivraisonDetailCollection bonLivraisonDetailCollection = new BonLivraisonDetailCollection();

                foreach (BonLivraisonDetail bonLivraisonDetail in BonLivraisonDetailCollection)
                    bonLivraisonDetailCollection.Add(bonLivraisonDetail);
                BonEntreeDetailCollection detailBECollection = new BonEntreeDetailCollection();
                BonSortieDetailCollection detailBSCollection = new BonSortieDetailCollection();

                BonEntreeCollection collectionBonEntree = new BonEntreeCollection();
                BonSortieCollection collectionBonSortie = new BonSortieCollection();
                int identique = 0;
                decimal sommeQuantite = 0;
                foreach (BonLivraisonDetail bonLivraisonDetail in BonLivraisonDetailCollection)
                {
                    BonLivraisonDetail ancienLivraison = ancienBonLivraison.BonLivraisonDetailCollection.RecupererBonLivraisonDetail(bonLivraisonDetail.CEntrepot, bonLivraisonDetail.CArticle);
                    BonLivraisonDetail nouveauLivraison = bonLivraisonDetailCollection.RecupererBonLivraisonDetail(bonLivraisonDetail.CEntrepot, bonLivraisonDetail.CArticle);

                    if (ancienLivraison != null)
                    {
                        if (ancienLivraison.Quantite > bonLivraisonDetail.Quantite)
                        {
                            BonEntreeDetail detailBE = new BonEntreeDetail();
                            detailBE.CArticle = bonLivraisonDetail.CArticle;
                            detailBE.CEntrepot = bonLivraisonDetail.CEntrepot;
                            detailBE.CreePar = this.CreePar;
                            detailBE.CTaxe = bonLivraisonDetail.CTaxe;
                            detailBE.CUnite = bonLivraisonDetail.CUnite;
                            detailBE.LibArticle = bonLivraisonDetail.LibArticle;
                            detailBE.PourcentageFodec = bonLivraisonDetail.PourcentageFodec;
                            detailBE.PourcentageRemise = bonLivraisonDetail.PourcentageRemise;
                            detailBE.PrixRevient = bonLivraisonDetail.PrixHT;
                            detailBE.Quantite = ancienLivraison.Quantite - bonLivraisonDetail.Quantite;
                            detailBE.TauxTVA = ancienLivraison.TauxTVA;
                            detailBE.CreePar = this.ModifiePar;
                            detailBE.PCInsertion = this.PCModification;
                            detailBECollection.Add(detailBE);

                            bonLivraisonDetailCollection.Remove(nouveauLivraison);
                            ancienBonLivraison.BonLivraisonDetailCollection.Remove(ancienLivraison);
                        }
                        else
                            if (ancienLivraison.Quantite < bonLivraisonDetail.Quantite)
                            {
                                BonSortieDetail detailBS = new BonSortieDetail();
                                detailBS.CArticle = bonLivraisonDetail.CArticle;
                                detailBS.CEntrepot = bonLivraisonDetail.CEntrepot;
                                detailBS.CUnite = bonLivraisonDetail.CUnite;
                                detailBS.LibArticle = bonLivraisonDetail.LibArticle;
                                detailBS.MontantTaxe = bonLivraisonDetail.MontantTaxe;
                                detailBS.PrixHT = bonLivraisonDetail.PrixHT;
                                detailBS.Quantite = bonLivraisonDetail.Quantite - ancienLivraison.Quantite;
                                detailBS.TauxTVA = bonLivraisonDetail.TauxTVA;
                                detailBS.CreePar = this.ModifiePar;
                                detailBS.PCInsertion = this.PCModification;
                                detailBSCollection.Add(detailBS);

                                bonLivraisonDetailCollection.Remove(nouveauLivraison);
                                ancienBonLivraison.BonLivraisonDetailCollection.Remove(ancienLivraison);
                            }
                            else
                            {
                                bonLivraisonDetailCollection.Remove(nouveauLivraison);
                                ancienBonLivraison.BonLivraisonDetailCollection.Remove(ancienLivraison);
                                identique++;
                            }

                        #region Mise A Jour Bon Commande


                        if (!(string.IsNullOrEmpty(ancienBonLivraison.NBonCommande)))
                        {
                            if (!bonCommande.Etat.Equals(VenteHelper.EtatBonCommande.PURGER.ToString()))
                            {
                                BonCommandeDetail detailCommande = bonCommande.BonCommandeDetailCollection.RecupererBonCommandeDetail(ancienBonLivraison.NBonCommande, bonLivraisonDetail.CArticle, ancienLivraison.OrdreBonCommande);
                                //BonCommandeDetail detailCommande = BonCommandeDetail.Charger(ancienBonLivraison.NBonCommande, bonLivraisonDetail.CArticle, bonLivraisonDetail.OrdreBonCommande);
                                if (!(detailCommande == null))
                                {
                                    if (bonLivraisonDetail.Quantite > detailCommande.QuantiteHistorique)
                                        bonLivraisonDetail.QuantiteRestore = detailCommande.QuantiteHistorique;

                                    else
                                        bonLivraisonDetail.QuantiteRestore = bonLivraisonDetail.Quantite;

                                    detailCommande.QuantiteHistorique = detailCommande.QuantiteHistorique - bonLivraisonDetail.QuantiteRestore;
                                    sommeQuantite = sommeQuantite + detailCommande.QuantiteHistorique;
                                    detailCommande.Modifier(transaction);
                                    bonCommande.BonCommandeDetailCollection.Remove(detailCommande);
                                }
                            }
                        }

                        #endregion Mise A Jour Bon Commande
                    }

                    bonLivraisonDetail.NBonLivraison = this.NBonLivraison;
                    bonLivraisonDetail.Ordre = i++;
                    bonLivraisonDetail.Sauvegarder(transaction);
                }
                if (!(this.BonLivraisonDetailCollection.Count == identique && identique == tailleAncien))
                {
                    foreach (BonLivraisonDetail nouveauDetail in bonLivraisonDetailCollection)
                    {
                        BonSortieDetail detailBS = new BonSortieDetail();
                        detailBS.CArticle = nouveauDetail.CArticle;
                        detailBS.CEntrepot = nouveauDetail.CEntrepot;
                        detailBS.CUnite = nouveauDetail.CUnite;
                        detailBS.LibArticle = nouveauDetail.LibArticle;
                        detailBS.MontantTaxe = nouveauDetail.MontantTaxe;
                        detailBS.PrixHT = nouveauDetail.PrixHT;
                        detailBS.Quantite = nouveauDetail.Quantite;
                        detailBS.TauxTVA = nouveauDetail.TauxTVA;
                        detailBS.CreePar = this.ModifiePar;
                        detailBS.PCInsertion = this.PCModification;
                        detailBSCollection.Add(detailBS);
                    }
                    foreach (BonLivraisonDetail ancienDetail in ancienBonLivraison.BonLivraisonDetailCollection)
                    {
                        BonEntreeDetail detailBE = new BonEntreeDetail();
                        detailBE.CArticle = ancienDetail.CArticle;
                        detailBE.CEntrepot = ancienDetail.CEntrepot;
                        detailBE.CreePar = this.CreePar;
                        detailBE.CTaxe = ancienDetail.CTaxe;
                        detailBE.CUnite = ancienDetail.CUnite;
                        detailBE.LibArticle = ancienDetail.LibArticle;
                        detailBE.PourcentageFodec = ancienDetail.PourcentageFodec;
                        detailBE.PourcentageRemise = ancienDetail.PourcentageRemise;
                        detailBE.PrixRevient = ancienDetail.PrixHT;
                        detailBE.Quantite = ancienDetail.Quantite;
                        detailBE.TauxTVA = ancienDetail.TauxTVA;
                        detailBE.CreePar = this.ModifiePar;
                        detailBE.PCInsertion = this.PCModification;
                        detailBECollection.Add(detailBE);
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
                        bonEntree.PCInsertion = this.PCModification;
                        bonEntree.DateEntree = (DateTime)this.DateLivraison;
                        bonEntree.Exercice = this.Exercice;
                        bonEntree.NDocumentSource = this.NBonLivraison;
                        bonEntree.RaisonSociale = this.RaisonSociale;
                        bonEntree.TypeMouvement = StockHelper.TypesMouvementStock.BE_BONLIVRAISONCLIENT.ToString();
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
                        bonSortie.CVehicule = this.CVehicule;
                        bonSortie.DateSortie = (DateTime)this.DateLivraison;
                        bonSortie.Exercice = this.Exercice;
                        bonSortie.NDocumentSource = this.NBonLivraison;
                        bonSortie.PCInsertion = this.PCModification;
                        bonSortie.RaisonSociale = this.RaisonSociale;
                        bonSortie.TypeMouvement = StockHelper.TypesMouvementStock.BS_BONLIVRAISONCLIENT.ToString();
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
                if (bonCommande.Etat != null && !bonCommande.Etat.Equals(VenteHelper.EtatBonCommande.PURGER.ToString()))
                {
                    while (bonCommande.BonCommandeDetailCollection.Count != 0)
                    {
                        sommeQuantite = sommeQuantite + bonCommande.BonCommandeDetailCollection[0].QuantiteHistorique;
                        bonCommande.BonCommandeDetailCollection.Remove(bonCommande.BonCommandeDetailCollection[0]);
                    }
                    if (sommeQuantite == 0)
                        BonCommande.ModifierEtatBonCommande(ancienBonLivraison.NBonCommande, VenteHelper.EtatBonCommande.LIVRE.ToString(), transaction);
                    else
                        BonCommande.ModifierEtatBonCommande(ancienBonLivraison.NBonCommande, VenteHelper.EtatBonCommande.ENCOURS.ToString(), transaction);
                }
                if (collectionBonSortie.Count != 0)
                {
                    foreach (BonSortie sortie in collectionBonSortie)
                        sortie.Inserer(transaction);
                }
                if (collectionBonEntree.Count != 0)
                    foreach (BonEntree entree in collectionBonEntree)
                        entree.Inserer(transaction);
                foreach (BonLivraisonTaxe bonLivraisonTaxe in BonLivraisonTaxeCollection)
                {
                    bonLivraisonTaxe.NBonLivraison = this.NBonLivraison;
                    bonLivraisonTaxe.Sauvegarder(transaction);
                }

                Client client = Client.Charger(this.CClient);

                VenteHelper.ModifierSolde(null, null, this.CClient, this.MontantTTC, 0m, 0m, 0m, 0m, 0m, transaction);
            }

            catch (Exception)
            {
                throw;
            }
        }

        //public void Modifier(SqlTransaction transaction)
        //{
        //    BonLivraison ancienBonLivraison = BonLivraison.Charger(this.NBonLivraison);
        //    this.RestituerSoldeBonLivraison(transaction, ancienBonLivraison.CClient);
        //    int tailleAncien = ancienBonLivraison.BonLivraisonDetailCollection.Count;
        //    BonCommande bonCommande = new BonCommande();
        //    if (!string.IsNullOrEmpty(ancienBonLivraison.NBonCommande))
        //    {

        //        bonCommande = BonCommande.Charger(ancienBonLivraison.NBonCommande, transaction);
        //        this.NBonCommande = ancienBonLivraison.NBonCommande;
        //        this.RestituerQuantiteHistorique(transaction);
        //    }
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Transaction = transaction;
        //        cmd.Connection = transaction.Connection;

        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = "BonLivraison_Modifier";
        //        cmd.Parameters.AddWithValue("@NBonLivraison", this.NBonLivraison);
        //        cmd.Parameters.AddWithValue("@CClient", this.CClient);
        //        cmd.Parameters.AddWithValue("@DateLivraison ", this.DateLivraison);
        //        cmd.Parameters.AddWithValue("@CUnite", this.CUnite);
        //        cmd.Parameters.AddWithValue("@BEchantillon ", this.BEchantillon);
        //        cmd.Parameters.AddWithValue("@BGratuit", this.BGratuit);
        //        cmd.Parameters.AddWithValue("@MatriculeFiscale", this.MatriculeFiscale);
        //        cmd.Parameters.AddWithValue("@Adresse", this.Adresse);
        //        cmd.Parameters.AddWithValue("@NTelephone", this.NTelephone);
        //        cmd.Parameters.AddWithValue("@CVendeur ", this.CVendeur);
        //        cmd.Parameters.AddWithValue("@BExonoreFodec", this.BExonoreFodec);
        //        cmd.Parameters.AddWithValue("@BExonoreTVA", this.BExonoreTVA);

        //        cmd.Parameters.AddWithValue("@MontantHT", this.MontantHT);
        //        cmd.Parameters.AddWithValue("@MontantRemise", this.MontantRemise);
        //        cmd.Parameters.AddWithValue("@MontantTaxe", this.MontantTaxe);
        //        cmd.Parameters.AddWithValue("@MontantTTC", this.MontantTTC);
        //        cmd.Parameters.AddWithValue("@NBonCommande", ancienBonLivraison.NBonCommande);
        //        cmd.Parameters.AddWithValue("@NOrdrePreparation", ancienBonLivraison.NOrdrePreparation);
        //        cmd.Parameters.AddWithValue("@NBonCommandeMannuel", this.NBonCommandeMannuel);
        //        cmd.Parameters.AddWithValue("@NBonLivraisonMannuel", this.NBonLivraisonMannuel);
        //        cmd.Parameters.AddWithValue("@NFacture", this.NFacture);
        //        cmd.Parameters.AddWithValue("@Observation", this.Observation);
        //        cmd.Parameters.AddWithValue("@RaisonSociale", this.RaisonSociale);
        //        cmd.Parameters.AddWithValue("@BRetour", this.BRetour);
        //        cmd.Parameters.AddWithValue("@Chauffeur", this.Chauffeur);
        //        cmd.Parameters.AddWithValue("@CVehicule", this.CVehicule);
        //        cmd.Parameters.AddWithValue("@CMission", this.CMission);
        //        cmd.Parameters.AddWithValue("@MontantRetenuForfaitaire", this.MontantRetenuForfaitaire);
        //        cmd.Parameters.AddWithValue("@poidsTotal", this.poidsTotal);
        //        cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
        //        cmd.Parameters.AddWithValue("@PCModification", this.PCModification);
        //        cmd.Parameters.AddWithValue("@ModifiePar", this.ModifiePar);
        //        foreach (SqlParameter parametre in cmd.Parameters)
        //            if (parametre.Value == null)
        //                parametre.Value = DBNull.Value;

        //        cmd.ExecuteNonQuery();
        //        if (ancienBonLivraison.DateLivraison != this.DateLivraison)
        //        {
        //            BonEntree bonEntree = BonEntree.ChargerParDocumentSource(StockHelper.TypesMouvementStock.BE_BONLIVRAISONCLIENT.ToString(), this.NBonLivraison);

        //            if (bonEntree != null)
        //            {
        //                bonEntree.DateEntree = (DateTime)this.DateLivraison;
        //                bonEntree.PCModification = this.PCModification;
        //                bonEntree.ModifiePar = this.ModifiePar;
        //                bonEntree.Modifier(transaction);
        //            }

        //            BonSortie bonSortie = BonSortie.ChargerParDocumentSource(StockHelper.TypesMouvementStock.BS_BONLIVRAISONCLIENT.ToString(), this.NBonLivraison);
        //            if (bonSortie != null)
        //            {
        //                bonSortie.DateSortie = (DateTime)this.DateLivraison;
        //                bonSortie.PCModification = this.PCModification;
        //                bonSortie.ModifiePar = this.ModifiePar;
        //                bonSortie.Modifier(transaction);
        //            }
        //        }
        //        this.SupprimerDetailBonLivraisonAnterieurs(transaction);
        //        this.SupprimerTaxeBonLivraisonAnterieurs(transaction);

        //        int i = 1;
        //        BonLivraisonDetailCollection bonLivraisonDetailCollection = new BonLivraisonDetailCollection();

        //        foreach (BonLivraisonDetail bonLivraisonDetail in BonLivraisonDetailCollection)
        //            bonLivraisonDetailCollection.Add(bonLivraisonDetail);
        //        BonEntreeDetailCollection detailBECollection = new BonEntreeDetailCollection();
        //        BonSortieDetailCollection detailBSCollection = new BonSortieDetailCollection();

        //        BonEntreeCollection collectionBonEntree = new BonEntreeCollection();
        //        BonSortieCollection collectionBonSortie = new BonSortieCollection();
        //        int identique = 0;
        //        decimal sommeQuantite = 0;
        //        foreach (BonLivraisonDetail bonLivraisonDetail in BonLivraisonDetailCollection)
        //        {
        //            BonLivraisonDetail ancienLivraison = ancienBonLivraison.BonLivraisonDetailCollection.RecupererBonLivraisonDetail(bonLivraisonDetail.CEntrepot, bonLivraisonDetail.CArticle);
        //            BonLivraisonDetail nouveauLivraison = bonLivraisonDetailCollection.RecupererBonLivraisonDetail(bonLivraisonDetail.CEntrepot, bonLivraisonDetail.CArticle);

        //            if (ancienLivraison != null)
        //            {
        //                if (ancienLivraison.Quantite > bonLivraisonDetail.Quantite)
        //                {
        //                    BonEntreeDetail detailBE = new BonEntreeDetail();
        //                    detailBE.CArticle = bonLivraisonDetail.CArticle;
        //                    detailBE.CEntrepot = bonLivraisonDetail.CEntrepot;
        //                    detailBE.CreePar = this.CreePar;
        //                    detailBE.CTaxe = bonLivraisonDetail.CTaxe;
        //                    detailBE.CUnite = bonLivraisonDetail.CUnite;
        //                    detailBE.LibArticle = bonLivraisonDetail.LibArticle;
        //                    detailBE.PourcentageFodec = bonLivraisonDetail.PourcentageFodec;
        //                    detailBE.PourcentageRemise = bonLivraisonDetail.PourcentageRemise;
        //                    detailBE.PrixRevient = bonLivraisonDetail.PrixHT;
        //                    detailBE.Quantite = ancienLivraison.Quantite - bonLivraisonDetail.Quantite;
        //                    detailBE.TauxTVA = ancienLivraison.TauxTVA;
        //                    detailBECollection.Add(detailBE);

        //                    bonLivraisonDetailCollection.Remove(nouveauLivraison);
        //                    ancienBonLivraison.BonLivraisonDetailCollection.Remove(ancienLivraison);
        //                }
        //                else
        //                    if (ancienLivraison.Quantite < bonLivraisonDetail.Quantite)
        //                    {
        //                        BonSortieDetail detailBS = new BonSortieDetail();
        //                        detailBS.CArticle = bonLivraisonDetail.CArticle;
        //                        detailBS.CEntrepot = bonLivraisonDetail.CEntrepot;
        //                        detailBS.CUnite = bonLivraisonDetail.CUnite;
        //                        detailBS.LibArticle = bonLivraisonDetail.LibArticle;
        //                        detailBS.MontantTaxe = bonLivraisonDetail.MontantTaxe;
        //                        detailBS.PrixHT = bonLivraisonDetail.PrixHT;
        //                        detailBS.Quantite = bonLivraisonDetail.Quantite - ancienLivraison.Quantite;
        //                        detailBS.TauxTVA = bonLivraisonDetail.TauxTVA;
        //                        detailBSCollection.Add(detailBS);

        //                        bonLivraisonDetailCollection.Remove(nouveauLivraison);
        //                        ancienBonLivraison.BonLivraisonDetailCollection.Remove(ancienLivraison);
        //                    }
        //                    else
        //                    {
        //                        bonLivraisonDetailCollection.Remove(nouveauLivraison);
        //                        ancienBonLivraison.BonLivraisonDetailCollection.Remove(ancienLivraison);
        //                        identique++;
        //                    }
        //            }

        //            #region Mise A Jour Bon Commande


        //            if (!(string.IsNullOrEmpty(ancienBonLivraison.NBonCommande)))
        //            {
        //                BonCommandeDetail detailCommande = bonCommande.BonCommandeDetailCollection.RecupererBonCommandeDetail(ancienBonLivraison.NBonCommande, bonLivraisonDetail.CArticle,  ancienLivraison.OrdreBonCommande);
        //                //BonCommandeDetail detailCommande = BonCommandeDetail.Charger(ancienBonLivraison.NBonCommande, bonLivraisonDetail.CArticle, bonLivraisonDetail.OrdreBonCommande);
        //                if (!(detailCommande == null))
        //                {
        //                    if (bonLivraisonDetail.Quantite > detailCommande.QuantiteHistorique)
        //                        bonLivraisonDetail.QuantiteRestore = detailCommande.QuantiteHistorique;

        //                    else
        //                        bonLivraisonDetail.QuantiteRestore = bonLivraisonDetail.Quantite;
                         
        //                    detailCommande.QuantiteHistorique = detailCommande.QuantiteHistorique - bonLivraisonDetail.QuantiteRestore;
        //                    sommeQuantite = sommeQuantite + detailCommande.QuantiteHistorique;
        //                    detailCommande.Modifier(transaction);
        //                    bonCommande.BonCommandeDetailCollection.Remove(detailCommande);
        //                }
        //            }

        //            #endregion Mise A Jour Bon Commande

        //            bonLivraisonDetail.NBonLivraison = this.NBonLivraison;
        //            bonLivraisonDetail.Ordre = i++;
        //            bonLivraisonDetail.Sauvegarder(transaction);
        //        }
        //        if (!(this.BonLivraisonDetailCollection.Count == identique && identique == tailleAncien))
        //        {
        //            foreach (BonLivraisonDetail nouveauDetail in bonLivraisonDetailCollection)
        //            {
        //                BonSortieDetail detailBS = new BonSortieDetail();
        //                detailBS.CArticle = nouveauDetail.CArticle;
        //                detailBS.CEntrepot = nouveauDetail.CEntrepot;
        //                detailBS.CUnite = nouveauDetail.CUnite;
        //                detailBS.LibArticle = nouveauDetail.LibArticle;
        //                detailBS.MontantTaxe = nouveauDetail.MontantTaxe;
        //                detailBS.PrixHT = nouveauDetail.PrixHT;
        //                detailBS.Quantite = nouveauDetail.Quantite;
        //                detailBS.TauxTVA = nouveauDetail.TauxTVA;
        //                detailBSCollection.Add(detailBS);
        //            }
        //            foreach (BonLivraisonDetail ancienDetail in ancienBonLivraison.BonLivraisonDetailCollection)
        //            {
        //                BonEntreeDetail detailBE = new BonEntreeDetail();
        //                detailBE.CArticle = ancienDetail.CArticle;
        //                detailBE.CEntrepot = ancienDetail.CEntrepot;
        //                detailBE.CreePar = this.CreePar;
        //                detailBE.CTaxe = ancienDetail.CTaxe;
        //                detailBE.CUnite = ancienDetail.CUnite;
        //                detailBE.LibArticle = ancienDetail.LibArticle;
        //                detailBE.PourcentageFodec = ancienDetail.PourcentageFodec;
        //                detailBE.PourcentageRemise = ancienDetail.PourcentageRemise;
        //                detailBE.PrixRevient = ancienDetail.PrixHT;
        //                detailBE.Quantite = ancienDetail.Quantite;
        //                detailBE.TauxTVA = ancienDetail.TauxTVA;
        //                detailBECollection.Add(detailBE);
        //            }
        //        }
        //        bool findBE = false;
        //        for (i = 0; i < detailBECollection.Count; )
        //        {
        //            if (!findBE)
        //            {
        //                BonEntree bonEntree = new BonEntree();
        //                bonEntree.BFodecExonore = this.BExonoreFodec;
        //                bonEntree.BTvaExonore = this.BExonoreTVA;
        //                bonEntree.CClient = this.CClient;
        //                bonEntree.CEntrepot = detailBECollection[i].CEntrepot;
        //                bonEntree.CreePar = this.ModifiePar;
        //                bonEntree.DateEntree = (DateTime)this.DateLivraison;
        //                bonEntree.Exercice = this.Exercice;
        //                bonEntree.NDocumentSource = this.NBonLivraison;
        //                bonEntree.RaisonSociale = this.RaisonSociale;
        //                bonEntree.TypeMouvement = StockHelper.TypesMouvementStock.BE_BONLIVRAISONCLIENT.ToString();
        //                collectionBonEntree.Add(bonEntree);
        //                findBE = true;
        //            }
        //            if (collectionBonEntree.Count != 0 || !findBE)
        //            {
        //                foreach (BonEntree entree in collectionBonEntree)
        //                {
        //                    if (detailBECollection[i].CEntrepot == entree.CEntrepot)
        //                    {
        //                        entree.BonEntreeDetailCollection.Add(detailBECollection[i]);
        //                        i++;
        //                    }
        //                    else
        //                    {
        //                        // i++;
        //                        findBE = false;
        //                    }
        //                }
        //            }
        //        }
        //        bool findBS = false;
        //        for (i = 0; i < detailBSCollection.Count; )
        //        {
        //            if (!findBS)
        //            {
        //                BonSortie bonSortie = new BonSortie();
        //                //bonSortie.CChauffeur = this.Chauffeur;
        //                bonSortie.CClient = this.CClient;
        //                bonSortie.CEntrepot = detailBSCollection[i].CEntrepot;
        //                bonSortie.CreePar = this.ModifiePar;
        //                bonSortie.CVehicule = this.CVehicule;
        //                bonSortie.DateSortie = (DateTime)this.DateLivraison;
        //                bonSortie.Exercice = this.Exercice;
        //                bonSortie.NDocumentSource = this.NBonLivraison;
        //                bonSortie.PCInsertion = this.PCModification;
        //                bonSortie.RaisonSociale = this.RaisonSociale;
        //                bonSortie.TypeMouvement = StockHelper.TypesMouvementStock.BS_BONLIVRAISONCLIENT.ToString();
        //                collectionBonSortie.Add(bonSortie);
        //                findBS = true;
        //            }

        //            if (collectionBonSortie.Count != 0 || !findBS)
        //            {
        //                foreach (BonSortie sortie in collectionBonSortie)
        //                {
        //                    if (detailBSCollection[i].CEntrepot == sortie.CEntrepot)
        //                    {
        //                        sortie.BonSortieDetailCollection.Add(detailBSCollection[i]);
        //                        i++;
        //                    }
        //                    else
        //                    {
        //                        findBS = false;
        //                    }
        //                }
        //            }
        //        }
        //        while (bonCommande.BonCommandeDetailCollection.Count != 0)
        //        {
        //            sommeQuantite = sommeQuantite + bonCommande.BonCommandeDetailCollection[0].Quantite;
        //            bonCommande.BonCommandeDetailCollection.Remove(bonCommande.BonCommandeDetailCollection[0]);
        //        }
        //        if (sommeQuantite == 0)
        //            BonCommande.ModifierEtatBonCommande(ancienBonLivraison.NBonCommande, VenteHelper.EtatBonCommande.LIVRE.ToString(), transaction);
        //        else
        //            BonCommande.ModifierEtatBonCommande(ancienBonLivraison.NBonCommande, VenteHelper.EtatBonCommande.ENCOURS.ToString(), transaction);
                   
        //        if (collectionBonSortie.Count != 0)
        //        {
        //            foreach (BonSortie sortie in collectionBonSortie)
        //                sortie.Inserer(transaction);
        //        }
        //        if (collectionBonEntree.Count != 0)
        //            foreach (BonEntree entree in collectionBonEntree)
        //                entree.Inserer(transaction);
        //        foreach (BonLivraisonTaxe bonLivraisonTaxe in BonLivraisonTaxeCollection)
        //        {
        //            bonLivraisonTaxe.NBonLivraison = this.NBonLivraison;
        //            bonLivraisonTaxe.Sauvegarder(transaction);
        //        }

        //        Client client = Client.Charger(this.CClient);

        //        VenteHelper.ModifierSolde(null, null, this.CClient, this.MontantTTC, 0m, 0m, 0m, 0m, 0m, transaction);
        //    }

        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        #region Bon Livraison Session

        public void InsererSession()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    InsererSession(transaction, VenteHelper.FACTURE_SESSION);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
        public void InsererLoyer()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    InsererSession(transaction, VenteHelper.FACTURE_LOYER);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
        public void InsererSession(SqlTransaction transaction, int type)
        {
            string numeroAncienBL = string.Empty;
            if(VenteHelper.FACTURE_SESSION==type)
            this.NBonLivraison = CodeBonLivraisonSession(transaction, this.Exercice);
            else
                if (VenteHelper.FACTURE_LOYER == type)
                    this.NBonLivraison = CodeBonLivraisonLoyer(transaction, this.Exercice);
            try
            {
                BonCommande bonCommande = new BonCommande();
                if (!string.IsNullOrEmpty(this.NBonCommande))
                    bonCommande = BonCommande.Charger(this.NBonCommande, transaction);

                if (bonCommande.NBonCommande == null || !bonCommande.Etat.Equals(VenteHelper.EtatBonCommande.LIVRE.ToString()))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Transaction = transaction;
                    cmd.Connection = transaction.Connection;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "BonLivraisonSession_Inserer";
                    cmd.Parameters.AddWithValue("@NBonLivraison", this.NBonLivraison);
                    cmd.Parameters.AddWithValue("@CClient", this.CClient);
                    cmd.Parameters.AddWithValue("@DateLivraison ", this.DateLivraison);
                    cmd.Parameters.AddWithValue("@CUnite", this.CUnite);
                    cmd.Parameters.AddWithValue("@BEchantillon ", this.BEchantillon);
                    cmd.Parameters.AddWithValue("@BGratuit", this.BGratuit);
                    cmd.Parameters.AddWithValue("@MatriculeFiscale", this.MatriculeFiscale);
                    cmd.Parameters.AddWithValue("@Adresse", this.Adresse);
                    cmd.Parameters.AddWithValue("@NTelephone", this.NTelephone);
                    cmd.Parameters.AddWithValue("@CVendeur ", this.CVendeur);
                    cmd.Parameters.AddWithValue("@BExonoreFodec", this.BExonoreFodec);
                    cmd.Parameters.AddWithValue("@BExonoreTVA", this.BExonoreTVA);
                    cmd.Parameters.AddWithValue("@BAvanceForfaitaire", this.BAvanceForfaitaire);
                    cmd.Parameters.AddWithValue("@MontantHT", this.MontantHT);
                    cmd.Parameters.AddWithValue("@MontantRemise", this.MontantRemise);
                    cmd.Parameters.AddWithValue("@MontantTaxe", this.MontantTaxe);
                    cmd.Parameters.AddWithValue("@MontantTTC", this.MontantTTC);
                    cmd.Parameters.AddWithValue("@NBonCommande", this.NBonCommande);
                    cmd.Parameters.AddWithValue("@NOrdrePreparation", this.NOrdrePreparation);
                    cmd.Parameters.AddWithValue("@NBonCommandeMannuel", this.NBonCommandeMannuel);
                    cmd.Parameters.AddWithValue("@NBonLivraisonMannuel", this.NBonLivraisonMannuel);
                    cmd.Parameters.AddWithValue("@NFacture", this.NFacture);
                    cmd.Parameters.AddWithValue("@Observation", this.Observation);
                    cmd.Parameters.AddWithValue("@RaisonSociale", this.RaisonSociale);
                    cmd.Parameters.AddWithValue("@BRetour", this.BRetour);
                    cmd.Parameters.AddWithValue("@Chauffeur", this.Chauffeur);
                    cmd.Parameters.AddWithValue("@CVehicule", this.CVehicule);
                    cmd.Parameters.AddWithValue("@CMission", this.CMission);
                    cmd.Parameters.AddWithValue("@MontantRetenuForfaitaire", this.MontantRetenuForfaitaire);
                    cmd.Parameters.AddWithValue("@poidsTotal", this.poidsTotal);
                    cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                    cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);
                    cmd.Parameters.AddWithValue("@CreePar", this.CreePar);
                    cmd.Parameters.AddWithValue("@Exercice", this.Exercice);
                    cmd.Parameters.AddWithValue("@BSession", type);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    cmd.ExecuteNonQuery();
     
                    int i = 1;
                    decimal sommeQuantite = 0m;

                    foreach (BonLivraisonDetail bonLivraisonDetail in BonLivraisonDetailCollection)
                    {
                        if (!(string.IsNullOrEmpty(this.NBonCommande)))
                        {
                            BonCommandeDetail detailCommande = BonCommandeDetail.Charger(this.NBonCommande, bonLivraisonDetail.CArticle, bonLivraisonDetail.OrdreBonCommande);
                            if (detailCommande != null)
                            {
                                if (bonLivraisonDetail.Quantite > detailCommande.QuantiteHistorique)
                                    bonLivraisonDetail.QuantiteRestore = detailCommande.QuantiteHistorique;

                                else
                                    bonLivraisonDetail.QuantiteRestore = bonLivraisonDetail.Quantite;

                                detailCommande.QuantiteHistorique = detailCommande.QuantiteHistorique - bonLivraisonDetail.QuantiteRestore;
                                sommeQuantite = sommeQuantite + detailCommande.QuantiteHistorique;
                                detailCommande.Modifier(transaction);
                            }
                            StockHelper.MiseAJourStockReserver(bonLivraisonDetail.CArticle, bonLivraisonDetail.CEntrepot, bonLivraisonDetail.QuantiteRestore, -1, transaction);
                        }

                        bonLivraisonDetail.NBonLivraison = this.NBonLivraison;
                        bonLivraisonDetail.Ordre = i++;
                        bonLivraisonDetail.Sauvegarder(transaction);
                    }
                    if (sommeQuantite == 0)
                        BonCommande.ModifierEtatBonCommande(this.NBonCommande, VenteHelper.EtatBonCommande.LIVRE.ToString(), transaction);
                    else
                        BonCommande.ModifierEtatBonCommande(this.NBonCommande, VenteHelper.EtatBonCommande.ENCOURS.ToString(), transaction);
                    this.SupprimerTaxeBonLivraisonAnterieurs(transaction);
                    foreach (BonLivraisonTaxe bonLivraisonTaxe in BonLivraisonTaxeCollection)
                    {
                        bonLivraisonTaxe.NBonLivraison = this.NBonLivraison;
                        bonLivraisonTaxe.Sauvegarder(transaction);
                    }
                    if(type!=VenteHelper.FACTURE_LOYER)
                       CreerBonSortie(transaction);

                    VenteHelper.ModifierSolde(null, null, this.CClient, this.MontantTTC, 0m, 0m, 0m, 0m, 0m, transaction);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string CodeBonLivraisonSession(SqlTransaction transaction, string exercice)
        {
            string codeBonLivraisonSession = string.Empty;
            string dernierNBonLivraisonSession = string.Empty;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT TOP 1 NBonLivraison FROM BonLivraison WHERE SUBSTRING(NBonLivraison,1,2)='" + exercice.Substring(2) + "' ORDER BY NBonLivraison DESC";
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                        dernierNBonLivraisonSession = dr["NBonLivraison"].ToString();
                }
                if (!string.IsNullOrEmpty(dernierNBonLivraisonSession))
                {
                    string dernierIndice = dernierNBonLivraisonSession.Substring(3);
                    int indice = int.Parse(dernierIndice) + 1;
                    codeBonLivraisonSession = exercice.Substring(2, 2) + "/" + indice.ToString().PadLeft(6, '0');

                }
                else
                    codeBonLivraisonSession = exercice.Substring(2, 2) + "/" + "000001";
            }

            catch (Exception)
            {
                throw;
            }
            return (codeBonLivraisonSession);
        }
        private string CodeBonLivraisonLoyer(SqlTransaction transaction, string exercice)
        {
            string codeBonLivraisonSession = string.Empty;
            string dernierNBonLivraisonSession = string.Empty;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT TOP 1 NBonLivraison FROM BonLivraison WHERE  BSession=2 AND SUBSTRING(NBonLivraison,2,2)='" + exercice.Substring(2) + "' ORDER BY NBonLivraison DESC";
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                        dernierNBonLivraisonSession = dr["NBonLivraison"].ToString();
                }
                if (!string.IsNullOrEmpty(dernierNBonLivraisonSession))
                {
                    string dernierIndice = dernierNBonLivraisonSession.Substring(8);
                    int indice = int.Parse(dernierIndice) + 1;
                    codeBonLivraisonSession = "L" + exercice.Substring(2, 2) + "/" + indice.ToString().PadLeft(6, '0');

                }
                else
                    codeBonLivraisonSession = "L" + exercice.Substring(2, 2) + "/" + "000001";
            }

            catch (Exception)
            {
                throw;
            }
            return (codeBonLivraisonSession);
        }

        #endregion Bon Livraison Session

        private void SupprimerDetailBonLivraisonAnterieurs(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BonLivraison_SupprimerDetails";

                cmd.Parameters.AddWithValue("@NBonLivraison", this.NBonLivraison);

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

        private void SupprimerTaxeBonLivraisonAnterieurs(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BonLivraison_SupprimerTaxes";

                cmd.Parameters.AddWithValue("@NBonLivraison", this.NBonLivraison);

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

        public static string RecupererNumeroBonLivraison(string exercice, out int indice)
        {
            string nBonLivraison = string.Empty;
            indice = 0;

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = cn;

                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.CommandText = "BonLivraison_RecupererNouveauNumero";
                cmd2.Parameters.AddWithValue("@Exercice", exercice);
                SqlDataReader dr = cmd2.ExecuteReader();
                if (dr.Read())
                {
                    nBonLivraison = dr["NBonLivraison"].ToString();
                    indice = int.Parse(dr["DernierIndice"].ToString());
                }

                dr.Close();
            }

            return nBonLivraison;
        }

        public static string RecupererNumeroBonLivraison(string exercice)
        {
            int indice = 0;
            return BonLivraison.RecupererNumeroBonLivraison(exercice, out indice);
        }

        private void CreerBonSortie(SqlTransaction transaction)
        {
            try
            {
                BonLivraisonDetailCollection col = new BonLivraisonDetailCollection();

                foreach (BonLivraisonDetail bonLivraisonDetail in BonLivraisonDetailCollection)
                    col.Add(bonLivraisonDetail);

                foreach (BonLivraisonDetail bonLivraisonDetail in BonLivraisonDetailCollection)
                {
                    BonLivraisonDetail BL = col.RecupererBonLivraisonDetail(bonLivraisonDetail.CEntrepot);
                    if (BL != null)
                    {
                        BonSortie bonSortie = new BonSortie();
                        bonSortie.CEntrepot = bonLivraisonDetail.CEntrepot;
                        bonSortie.NDocumentSource = NBonLivraison;
                        bonSortie.DateSortie = (DateTime)DateLivraison;
                        // bonSortie.CChauffeur = Chauffeur;
                        bonSortie.CClient = CClient;
                        bonSortie.CVehicule = CVehicule;
                        bonSortie.RaisonSociale = RaisonSociale;
                        bonSortie.Exercice = Exercice;
                        bonSortie.CreePar = this.CreePar;
                        bonSortie.PCInsertion = this.PCInsertion;
                        bonSortie.TypeMouvement = StockHelper.TypesMouvementStock.BS_BONLIVRAISONCLIENT.ToString();

                        while (BL != null)
                        {
                            BonSortieDetail bonSortieDetail = new BonSortieDetail();

                            bonSortieDetail.CEntrepot = BL.CEntrepot;
                            bonSortieDetail.CArticle = BL.CArticle;
                            bonSortieDetail.Quantite = BL.Quantite;
                            bonSortieDetail.CUnite = BL.CUnite;
                            bonSortieDetail.LibArticle = BL.LibArticle;
                            bonSortieDetail.MontantTaxe = BL.MontantTaxe;
                            bonSortieDetail.TauxTVA = BL.TauxTVA;
                            bonSortieDetail.PrixHT = BL.PrixHT;
                            bonSortieDetail.CreePar = this.CreePar;
                            bonSortieDetail.PCInsertion = this.PCInsertion;
                            bonSortie.BonSortieDetailCollection.Add(bonSortieDetail);

                            col.Remove(BL);

                            BL = col.RecupererBonLivraisonDetail(bonLivraisonDetail.CEntrepot);
                        }
                        bonSortie.Inserer(transaction);
                    }
                }
            }

            catch (Exception)
            {
                throw;
            }
        }

        //public void RestituerStock(SqlTransaction transaction)
        //{
        //    BonLivraisonDetailCollection AncienneBLDetailCollection = new BonLivraisonDetailCollection();
        //    BonLivraisonDetail bonLivraisonDetail = null;

        //    try
        //    {
        //        SqlCommand cmdBonLivraison = new SqlCommand();
        //        cmdBonLivraison.Transaction = transaction;
        //        cmdBonLivraison.Connection = transaction.Connection;
        //        cmdBonLivraison.CommandType = CommandType.StoredProcedure;
        //        cmdBonLivraison.CommandText = "BonLivraisonDetail_Charger";
        //        cmdBonLivraison.Parameters.AddWithValue("@NBonLivraison", NBonLivraison);
        //        cmdBonLivraison.Parameters.AddWithValue("@CArticle", DBNull.Value);
        //        cmdBonLivraison.Parameters.AddWithValue("@Ordre", DBNull.Value);
        //        foreach (SqlParameter parametre in cmdBonLivraison.Parameters)
        //        {
        //            if (parametre.Value == null)
        //            {
        //                parametre.Value = DBNull.Value;
        //            }
        //        }

        //        using (SqlDataReader dr = cmdBonLivraison.ExecuteReader())
        //        {
        //            while (dr.Read())
        //            {
        //                bonLivraisonDetail = new BonLivraisonDetail(NBonLivraison);
        //                bonLivraisonDetail.NBonLivraison = NBonLivraison;
        //                bonLivraisonDetail.CArticle = dr["CArticle"].ToString();
        //                bonLivraisonDetail.Ordre = int.Parse(dr["Ordre"].ToString());

        //                if (dr["CUnite"] != DBNull.Value)
        //                    bonLivraisonDetail.CUnite = dr["CUnite"].ToString();
        //                if (dr["CUnite"] != DBNull.Value)
        //                    bonLivraisonDetail.CUnite = dr["CUnite"].ToString();
        //                if (dr["LibArticle"] != DBNull.Value)
        //                    bonLivraisonDetail.LibArticle = dr["LibArticle"].ToString();
        //                if (dr["MontantTaxe"] != DBNull.Value)
        //                    bonLivraisonDetail.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
        //                if (dr["PourcentageFodec"] != DBNull.Value)
        //                    bonLivraisonDetail.PourcentageFodec = decimal.Parse(dr["PourcentageFodec"].ToString());
        //                if (dr["PourcentageRemise"] != DBNull.Value)
        //                    bonLivraisonDetail.PourcentageRemise = decimal.Parse(dr["PourcentageRemise"].ToString());
        //                if (dr["PrixHT"] != DBNull.Value)
        //                    bonLivraisonDetail.PrixHT = decimal.Parse(dr["PrixHT"].ToString());
        //                if (dr["PrixRevient"] != DBNull.Value)
        //                    bonLivraisonDetail.PrixRevient = decimal.Parse(dr["PrixRevient"].ToString());
        //                if (dr["Quantite"] != DBNull.Value)
        //                    bonLivraisonDetail.Quantite = decimal.Parse(dr["Quantite"].ToString());
        //                if (dr["QuantiteHistorique"] != DBNull.Value)
        //                    bonLivraisonDetail.QuantiteHistorique = decimal.Parse(dr["QuantiteHistorique"].ToString());
        //                if (dr["TauxTVA"] != DBNull.Value)
        //                    bonLivraisonDetail.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());
        //                if (dr["CTaxe"] != DBNull.Value)
        //                    bonLivraisonDetail.CTaxe = dr["CTaxe"].ToString();
        //                if (dr["Remise1"] != DBNull.Value)
        //                    bonLivraisonDetail.Remise1 = decimal.Parse(dr["Remise1"].ToString());
        //                if (dr["Remise2"] != DBNull.Value)
        //                    bonLivraisonDetail.Remise2 = decimal.Parse(dr["Remise2"].ToString());
        //                if (dr["PrixVentePublic"] != DBNull.Value)
        //                    bonLivraisonDetail.PrixVentePublic = decimal.Parse(dr["PrixVentePublic"].ToString());
        //                if (dr["QuantiteRestore"] != DBNull.Value)
        //                    bonLivraisonDetail.QuantiteRestore = decimal.Parse(dr["QuantiteRestore"].ToString());
        //                if (dr["OrdreBonCommande"] != DBNull.Value)
        //                    bonLivraisonDetail.OrdreBonCommande = int.Parse(dr["OrdreBonCommande"].ToString());
        //                if (dr["Longueur"] != DBNull.Value)
        //                    bonLivraisonDetail.Longueur = decimal.Parse(dr["Longueur"].ToString());
        //                if (dr["Largeur"] != DBNull.Value)
        //                    bonLivraisonDetail.Largeur = decimal.Parse(dr["Largeur"].ToString());
        //                if (dr["Epaisseur"] != DBNull.Value)
        //                    bonLivraisonDetail.Epaisseur = decimal.Parse(dr["Epaisseur"].ToString());
        //                if (dr["MontantNet"] != DBNull.Value)
        //                    bonLivraisonDetail.MontantNet = decimal.Parse(dr["MontantNet"].ToString());

        //                AncienneBLDetailCollection.Add(bonLivraisonDetail);
        //            }
        //        }

        //        BonEntree bonEntreeCible = new BonEntree();
        //        bonEntreeCible.CUnite = bonLivraisonDetail.CUnite;
        //        bonEntreeCible.NDocumentSource = NBonLivraison;
        //        bonEntreeCible.TypeMouvement = StockHelper.TypesMouvementStock.BE_BonLivraisonInterne.ToString();
        //        bonEntreeCible.DateEntree = (DateTime)DateLivraison;
        //        bonEntreeCible.Exercice = Exercice;

        //        BonEntree bonEntreeSource = new BonEntree();
        //        bonEntreeSource.CUnite = bonLivraisonDetail.CUnite;
        //        bonEntreeSource.NDocumentSource = NBonLivraison;
        //        bonEntreeSource.TypeMouvement = StockHelper.TypesMouvementStock.BE_BonLivraisonInterne.ToString();
        //        bonEntreeSource.DateEntree = (DateTime)DateLivraison;
        //        bonEntreeSource.Exercice = Exercice;

        //        BonSortie bonSortieSource = new BonSortie();
        //        bonSortieSource.CUnite = bonLivraisonDetail.CUnite;
        //        bonSortieSource.NDocumentSource = NBonLivraison;
        //        bonSortieSource.TypeMouvement = StockHelper.TypesMouvementStock.BS_BonLivraisonInterne.ToString();
        //        bonSortieSource.Chauffeur = Chauffeur;
        //        bonSortieSource.CVehicule = CVehicule;
        //        bonSortieSource.CClient = CClient;
        //        bonSortieSource.RaisonSociale = RaisonSociale;
        //        bonSortieSource.DateSortie = (DateTime)DateLivraison;
        //        bonSortieSource.Exercice = Exercice;

        //        BonSortie bonSortieCible = new BonSortie();
        //        bonSortieCible.CUnite = bonLivraisonDetail.CUnite;
        //        bonSortieCible.NDocumentSource = NBonLivraison;
        //        bonSortieCible.TypeMouvement = StockHelper.TypesMouvementStock.BS_BonLivraisonInterne.ToString();
        //        bonSortieCible.Chauffeur = Chauffeur;
        //        bonSortieCible.CVehicule = CVehicule;
        //        bonSortieCible.CClient = CClient;
        //        bonSortieCible.RaisonSociale = RaisonSociale;
        //        bonSortieCible.DateSortie = (DateTime)DateLivraison;
        //        bonSortieCible.Exercice = Exercice;

        //        foreach (BonLivraisonDetail obj in AncienneBLDetailCollection)
        //        {
        //            var objModifie = this.BonLivraisonDetailCollection.RecupererBonLivraisonDetail(obj.NBonLivraison, obj.CArticle);
        //            if (objModifie != null)
        //            {
        //                if (objModifie.Quantite > obj.Quantite)
        //                {
        //                    BonEntreeDetail bonEntreeDetail = new BonEntreeDetail();
        //                    bonEntreeDetail.CUnite = objModifie.CUnite;
        //                    bonEntreeDetail.CArticle = objModifie.CArticle;
        //                    bonEntreeDetail.Quantite = objModifie.Quantite - obj.Quantite;
        //                    bonEntreeCible.BonEntreeDetailCollection.Add(bonEntreeDetail);

        //                    BonSortieDetail BonSortieDetail = new BonSortieDetail();
        //                    BonSortieDetail.CUnite = objModifie.CUnite;
        //                    BonSortieDetail.CArticle = objModifie.CArticle;
        //                    BonSortieDetail.Quantite = objModifie.Quantite - obj.Quantite;
        //                    bonSortieSource.BonSortieDetailCollection.Add(BonSortieDetail);
        //                }
        //                else
        //                {
        //                    if (objModifie.Quantite < obj.Quantite)
        //                    {
        //                        BonSortieDetail BonSortieDetail = new BonSortieDetail();
        //                        BonSortieDetail.CUnite = objModifie.CUnite;
        //                        BonSortieDetail.CArticle = objModifie.CArticle;
        //                        BonSortieDetail.Quantite = obj.Quantite - objModifie.Quantite;
        //                        bonSortieCible.BonSortieDetailCollection.Add(BonSortieDetail);

        //                        BonEntreeDetail bonEntreeDetail = new BonEntreeDetail();
        //                        bonEntreeDetail.CUnite = objModifie.CUnite;
        //                        bonEntreeDetail.CArticle = objModifie.CArticle;
        //                        bonEntreeDetail.Quantite = obj.Quantite - objModifie.Quantite;
        //                        bonEntreeSource.BonEntreeDetailCollection.Add(bonEntreeDetail);
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                BonSortieDetail BonSortieDetail = new BonSortieDetail();
        //                BonSortieDetail.CUnite = obj.CUnite;
        //                BonSortieDetail.CArticle = obj.CArticle;
        //                BonSortieDetail.Quantite = obj.Quantite;
        //                bonSortieCible.BonSortieDetailCollection.Add(BonSortieDetail);

        //                BonEntreeDetail bonEntreeDetail = new BonEntreeDetail();
        //                bonEntreeDetail.CUnite = obj.CUnite;
        //                bonEntreeDetail.CArticle = obj.CArticle;
        //                bonEntreeDetail.Quantite = obj.Quantite;
        //                bonEntreeSource.BonEntreeDetailCollection.Add(bonEntreeDetail);
        //            }
        //        }

        //        foreach (BonLivraisonDetail obj in AncienneBLDetailCollection)
        //        {
        //            var objAjoute = AncienneBLDetailCollection.RecupererBonLivraisonDetail(obj.NBonLivraison, obj.CArticle);
        //            if (objAjoute == null)
        //            {
        //                BonEntreeDetail bonEntreeDetail = new BonEntreeDetail();
        //                bonEntreeDetail.CUnite = obj.CUnite;
        //                bonEntreeDetail.CArticle = obj.CArticle;
        //                bonEntreeDetail.Quantite = obj.Quantite;
        //                bonEntreeCible.BonEntreeDetailCollection.Add(bonEntreeDetail);

        //                BonSortieDetail BonSortieDetail = new BonSortieDetail();
        //                BonSortieDetail.CUnite = obj.CUnite;
        //                BonSortieDetail.CArticle = obj.CArticle;
        //                BonSortieDetail.Quantite = obj.Quantite;
        //                bonSortieSource.BonSortieDetailCollection.Add(BonSortieDetail);
        //            }
        //        }

        //        if (bonEntreeCible.BonEntreeDetailCollection.Count() > 0)
        //        {
        //            bonEntreeCible.Inserer(transaction);
        //        }
        //        if (bonEntreeSource.BonEntreeDetailCollection.Count() > 0)
        //        {
        //            bonEntreeSource.Inserer(transaction);
        //        }
        //        if (bonSortieSource.BonSortieDetailCollection.Count() > 0)
        //        {
        //            bonSortieSource.Inserer(transaction);
        //        }
        //        if (bonSortieCible.BonSortieDetailCollection.Count() > 0)
        //        {
        //            bonSortieCible.Inserer(transaction);
        //        }
        //    }

        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public void RestituerQuantiteHistorique(SqlTransaction transaction)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Transaction = transaction;
            cmd.Connection = transaction.Connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "BonCommande_RestituerQuantiteHistorique";

            cmd.Parameters.AddWithValue("@NBonLivraison", this.NBonLivraison);
            cmd.Parameters.AddWithValue("@NBonCommande", this.NBonCommande);
            foreach (SqlParameter parametre in cmd.Parameters)
                if (parametre.Value == null)
                    parametre.Value = DBNull.Value;

            cmd.ExecuteNonQuery();
        }
      
        private void RestituerSoldeBonLivraison(SqlTransaction transaction, string client)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Transaction = transaction;
            cmd.Connection = transaction.Connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Client_RestituerSoldeBonLivraison";

            cmd.Parameters.AddWithValue("@NBonLivraison", this.NBonLivraison);
            cmd.Parameters.AddWithValue("@CClient", client);
            foreach (SqlParameter parametre in cmd.Parameters)
                if (parametre.Value == null)
                    parametre.Value = DBNull.Value;

            cmd.ExecuteNonQuery();
        }

        public static BonLivraison Charger(string nBonLivraison)
        {
            BonLivraison bonLivraison = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "BonLivraison_Charger";
                    cmd.Parameters.AddWithValue("@NBonLivraison", nBonLivraison);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            bonLivraison = new BonLivraison();
                            bonLivraison.NBonLivraison = dr["NBonLivraison"].ToString();
                            if (dr["CClient"] != DBNull.Value)
                                bonLivraison.CClient = dr["CClient"].ToString();
                            if (dr["DateLivraison"] != DBNull.Value)
                                bonLivraison.DateLivraison = DateTime.Parse(dr["DateLivraison"].ToString());
                            if (dr["CUnite"] != DBNull.Value)
                                bonLivraison.CUnite = dr["CUnite"].ToString();
                            if (dr["BEchantillon"] != DBNull.Value)
                                bonLivraison.BEchantillon = bool.Parse(dr["BEchantillon"].ToString());
                            if (dr["BGratuit"] != DBNull.Value)
                                bonLivraison.BGratuit = bool.Parse(dr["BGratuit"].ToString());
                            if (dr["MatriculeFiscale"] != DBNull.Value)
                                bonLivraison.MatriculeFiscale = dr["MatriculeFiscale"].ToString();
                            if (dr["Adresse"] != DBNull.Value)
                                bonLivraison.Adresse = dr["Adresse"].ToString();
                            if (dr["NTelephone"] != DBNull.Value)
                                bonLivraison.NTelephone = dr["NTelephone"].ToString();
                            if (dr["CVendeur"] != DBNull.Value)
                                bonLivraison.CVendeur = int.Parse(dr["CVendeur"].ToString());
                            if (dr["BExonoreFodec"] != DBNull.Value)
                                bonLivraison.BExonoreFodec = bool.Parse(dr["BExonoreFodec"].ToString());
                            if (dr["BExonoreTVA"] != DBNull.Value)
                                bonLivraison.BExonoreTVA = bool.Parse(dr["BExonoreTVA"].ToString());
                            if (dr["BAvanceForfaitaire"] != DBNull.Value)
                                bonLivraison.BAvanceForfaitaire = bool.Parse(dr["BAvanceForfaitaire"].ToString());
                            if (dr["DateModification"] != DBNull.Value)
                                bonLivraison.DateModification = DateTime.Parse(dr["DateModification"].ToString());
                            if (dr["MontantHT"] != DBNull.Value)
                                bonLivraison.MontantHT = decimal.Parse(dr["MontantHT"].ToString());
                            if (dr["MontantRemise"] != DBNull.Value)
                                bonLivraison.MontantRemise = decimal.Parse(dr["MontantRemise"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                bonLivraison.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["MontantTTC"] != DBNull.Value)
                                bonLivraison.MontantTTC = decimal.Parse(dr["MontantTTC"].ToString());
                            if (dr["NBonCommande"] != DBNull.Value)
                                bonLivraison.NBonCommande = dr["NBonCommande"].ToString();
                            if (dr["NOrdrePreparation"] != DBNull.Value)
                                bonLivraison.NOrdrePreparation = dr["NOrdrePreparation"].ToString();
                            if (dr["NBonCommandeMannuel"] != DBNull.Value)
                                bonLivraison.NBonCommandeMannuel = dr["NBonCommandeMannuel"].ToString();
                            if (dr["NBonLivraisonMannuel"] != DBNull.Value)
                                bonLivraison.NBonLivraisonMannuel = dr["NBonLivraisonMannuel"].ToString();
                            if (dr["NFacture"] != DBNull.Value)
                                bonLivraison.NFacture = dr["NFacture"].ToString();
                            if (dr["Observation"] != DBNull.Value)
                                bonLivraison.Observation = dr["Observation"].ToString();
                            if (dr["RaisonSociale"] != DBNull.Value)
                                bonLivraison.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["Indice"] != DBNull.Value)
                                bonLivraison.Indice = int.Parse(dr["Indice"].ToString());
                            if (dr["BRetour"] != DBNull.Value)
                                bonLivraison.BRetour = bool.Parse(dr["BRetour"].ToString());
                            if (dr["Chauffeur"] != DBNull.Value)
                                bonLivraison.Chauffeur = dr["Chauffeur"].ToString();
                            if (dr["CVehicule"] != DBNull.Value)
                                bonLivraison.CVehicule = dr["CVehicule"].ToString();
                            if (dr["MontantRetenuForfaitaire"] != DBNull.Value)
                                bonLivraison.MontantRetenuForfaitaire = decimal.Parse(dr["MontantRetenuForfaitaire"].ToString());
                            if (dr["poidsTotal"] != DBNull.Value)
                                bonLivraison.poidsTotal = decimal.Parse(dr["poidsTotal"].ToString());
                            if (dr["NOrdredeTravail"] != DBNull.Value)
                                bonLivraison.NOrdredeTravail = dr["NOrdredeTravail"].ToString();
                            bonLivraison.BonLivraisonDetailCollection = BonLivraisonDetailCollection.Charger(nBonLivraison);
                            bonLivraison.BonLivraisonTaxeCollection = BonLivraisonTaxeCollection.Charger(nBonLivraison);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bonLivraison;
        }

        public static BonLivraison RecupererBL_ParFacture(string nFacture)
        {
            BonLivraison bonLivraison = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "BonLivraisonParFacure_Charger";
                    cmd.Parameters.AddWithValue("@NFacture", nFacture);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            bonLivraison = new BonLivraison();
                            bonLivraison.NBonLivraison = dr["NBonLivraison"].ToString();
                            if (dr["CClient"] != DBNull.Value)
                                bonLivraison.CClient = dr["CClient"].ToString();
                            if (dr["MontantHT"] != DBNull.Value)
                                bonLivraison.MontantHT = decimal.Parse(dr["MontantHT"].ToString());
                            if (dr["MontantRemise"] != DBNull.Value)
                                bonLivraison.MontantRemise = decimal.Parse(dr["MontantRemise"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                bonLivraison.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["MontantTTC"] != DBNull.Value)
                                bonLivraison.MontantTTC = decimal.Parse(dr["MontantTTC"].ToString());
                            if (dr["NBonCommande"] != DBNull.Value)
                                bonLivraison.NBonCommande = dr["NBonCommande"].ToString();
                            if (dr["NBonCommandeMannuel"] != DBNull.Value)
                                bonLivraison.NBonCommandeMannuel = dr["NBonCommandeMannuel"].ToString();
                            if (dr["NBonLivraisonMannuel"] != DBNull.Value)
                                bonLivraison.NBonLivraisonMannuel = dr["NBonLivraisonMannuel"].ToString();
                            if (dr["NFacture"] != DBNull.Value)
                                bonLivraison.NFacture = dr["NFacture"].ToString();
                            if (dr["Chauffeur"] != DBNull.Value)
                                bonLivraison.Chauffeur = dr["Chauffeur"].ToString();
                            if (dr["NOrdredeTravail"] != DBNull.Value)
                                bonLivraison.NOrdredeTravail = dr["NOrdredeTravail"].ToString();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bonLivraison;
        }

        public void ModifierNChantier()
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
                    cmd.CommandText = "update BonLivraison set NChantier = '" + this.NOrdredeTravail + "' where NBonLivraison = '" + this.NBonLivraison + "'";
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
        public void ModifierNOrdredeTravail()
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
                    cmd.CommandText = "update BonLivraison set NOrdredeTravail = '" + this.NOrdredeTravail + "' where NBonLivraison = '" + this.NBonLivraison + "'";
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
      
    }

    public class BonLivraisonCollection : List<BonLivraison>
    {
        public BonLivraisonCollection()
        {
        }

        public static BonLivraisonCollection BonLivraisonChargerControl(string cClient, DateTime limiteDate)
        {
            BonLivraisonCollection bonLivraisonCollection = new BonLivraisonCollection();
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "BonLivraison_ChargerControle";
                    cmd.Parameters.AddWithValue("@CClient", cClient);
                    cmd.Parameters.AddWithValue("@DateLimite", limiteDate);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            BonLivraison bonLivraison = new BonLivraison();
                            bonLivraison.NBonLivraison = dr["NBonLivraison"].ToString();
                            if (dr["CClient"] != DBNull.Value)
                                bonLivraison.CClient = dr["CClient"].ToString();
                            if (dr["DateLivraison"] != DBNull.Value)
                                bonLivraison.DateLivraison = DateTime.Parse(dr["DateLivraison"].ToString());
                            bonLivraisonCollection.Add(bonLivraison);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return bonLivraisonCollection;
        }

        public static BonLivraisonCollection ChargerparOrdredeTravail(string NOrdredeTravail)
        {
            BonLivraisonCollection collection = new BonLivraisonCollection();
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "BonLivraison_ChargerparOrdredeTravail";
                    cmd.Parameters.AddWithValue("@NOrdredeTravail", NOrdredeTravail);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            BonLivraison bonLivraison = new BonLivraison();
                            bonLivraison.NBonLivraison = dr["NBonLivraison"].ToString();
                            if (dr["CClient"] != DBNull.Value)
                                bonLivraison.CClient = dr["CClient"].ToString();
                            if (dr["DateLivraison"] != DBNull.Value)
                                bonLivraison.DateLivraison = DateTime.Parse(dr["DateLivraison"].ToString());
                            if (dr["CUnite"] != DBNull.Value)
                                bonLivraison.CUnite = dr["CUnite"].ToString();
                            if (dr["BEchantillon"] != DBNull.Value)
                                bonLivraison.BEchantillon = bool.Parse(dr["BEchantillon"].ToString());
                            if (dr["BGratuit"] != DBNull.Value)
                                bonLivraison.BGratuit = bool.Parse(dr["BGratuit"].ToString());
                            if (dr["MatriculeFiscale"] != DBNull.Value)
                                bonLivraison.MatriculeFiscale = dr["MatriculeFiscale"].ToString();
                            if (dr["Adresse"] != DBNull.Value)
                                bonLivraison.Adresse = dr["Adresse"].ToString();
                            if (dr["NTelephone"] != DBNull.Value)
                                bonLivraison.NTelephone = dr["NTelephone"].ToString();
                            if (dr["CVendeur"] != DBNull.Value)
                                bonLivraison.CVendeur = int.Parse(dr["CVendeur"].ToString());
                            if (dr["BExonoreFodec"] != DBNull.Value)
                                bonLivraison.BExonoreFodec = bool.Parse(dr["BExonoreFodec"].ToString());
                            if (dr["BExonoreTVA"] != DBNull.Value)
                                bonLivraison.BExonoreTVA = bool.Parse(dr["BExonoreTVA"].ToString());
                            if (dr["BAvanceForfaitaire"] != DBNull.Value)
                                bonLivraison.BAvanceForfaitaire = bool.Parse(dr["BAvanceForfaitaire"].ToString());
                            //if (dr["BImprimer"] != DBNull.Value)
                            //    bonLivraison.BImprimer = bool.Parse(dr["BImprimer"].ToString());
                            //if (dr["MontantHT"] != DBNull.Value)
                            bonLivraison.MontantHT = decimal.Parse(dr["MontantHT"].ToString());
                            if (dr["MontantRemise"] != DBNull.Value)
                                bonLivraison.MontantRemise = decimal.Parse(dr["MontantRemise"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                bonLivraison.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["MontantTTC"] != DBNull.Value)
                                bonLivraison.MontantTTC = decimal.Parse(dr["MontantTTC"].ToString());
                            if (dr["NBonCommande"] != DBNull.Value)
                                bonLivraison.NBonCommande = dr["NBonCommande"].ToString();
                            if (dr["NOrdrePreparation"] != DBNull.Value)
                                bonLivraison.NOrdrePreparation = dr["NOrdrePreparation"].ToString();
                            if (dr["NBonCommandeMannuel"] != DBNull.Value)
                                bonLivraison.NBonCommandeMannuel = dr["NBonCommandeMannuel"].ToString();
                            if (dr["NBonLivraisonMannuel"] != DBNull.Value)
                                bonLivraison.NBonLivraisonMannuel = dr["NBonLivraisonMannuel"].ToString();
                            if (dr["NFacture"] != DBNull.Value)
                                bonLivraison.NFacture = dr["NFacture"].ToString();
                            if (dr["Observation"] != DBNull.Value)
                                bonLivraison.Observation = dr["Observation"].ToString();
                            if (dr["RaisonSociale"] != DBNull.Value)
                                bonLivraison.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["Indice"] != DBNull.Value)
                                bonLivraison.Indice = int.Parse(dr["Indice"].ToString());
                            if (dr["BRetour"] != DBNull.Value)
                                bonLivraison.BRetour = bool.Parse(dr["BRetour"].ToString());
                            if (dr["Chauffeur"] != DBNull.Value)
                                bonLivraison.Chauffeur = dr["Chauffeur"].ToString();
                            if (dr["CVehicule"] != DBNull.Value)
                                bonLivraison.CVehicule = dr["CVehicule"].ToString();
                            if (dr["MontantRetenuForfaitaire"] != DBNull.Value)
                                bonLivraison.MontantRetenuForfaitaire = decimal.Parse(dr["MontantRetenuForfaitaire"].ToString());
                            if (dr["poidsTotal"] != DBNull.Value)
                                bonLivraison.poidsTotal = decimal.Parse(dr["poidsTotal"].ToString());
                            if (dr["NOrdredeTravail"] != DBNull.Value)
                                bonLivraison.NOrdredeTravail = dr["NOrdredeTravail"].ToString();
                            collection.Add(bonLivraison);
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

        public static BonLivraisonCollection Charger()
        {
            BonLivraisonCollection collection = new BonLivraisonCollection();
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "BonLivraison_Charger";
                    cmd.Parameters.AddWithValue("@NBonLivraison", DBNull.Value);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            BonLivraison bonLivraison = new BonLivraison();
                            bonLivraison.NBonLivraison = dr["NBonLivraison"].ToString();
                            if (dr["CClient"] != DBNull.Value)
                                bonLivraison.CClient = dr["CClient"].ToString();
                            if (dr["DateLivraison"] != DBNull.Value)
                                bonLivraison.DateLivraison = DateTime.Parse(dr["DateLivraison"].ToString());
                            if (dr["CUnite"] != DBNull.Value)
                                bonLivraison.CUnite = dr["CUnite"].ToString();
                            if (dr["BEchantillon"] != DBNull.Value)
                                bonLivraison.BEchantillon = bool.Parse(dr["BEchantillon"].ToString());
                            if (dr["BGratuit"] != DBNull.Value)
                                bonLivraison.BGratuit = bool.Parse(dr["BGratuit"].ToString());
                            if (dr["MatriculeFiscale"] != DBNull.Value)
                                bonLivraison.MatriculeFiscale = dr["MatriculeFiscale"].ToString();
                            if (dr["Adresse"] != DBNull.Value)
                                bonLivraison.Adresse = dr["Adresse"].ToString();
                            if (dr["NTelephone"] != DBNull.Value)
                                bonLivraison.NTelephone = dr["NTelephone"].ToString();
                            if (dr["CVendeur"] != DBNull.Value)
                                bonLivraison.CVendeur = int.Parse(dr["CVendeur"].ToString());
                            if (dr["BExonoreFodec"] != DBNull.Value)
                                bonLivraison.BExonoreFodec = bool.Parse(dr["BExonoreFodec"].ToString());
                            if (dr["BExonoreTVA"] != DBNull.Value)
                                bonLivraison.BExonoreTVA = bool.Parse(dr["BExonoreTVA"].ToString());
                            if (dr["BAvanceForfaitaire"] != DBNull.Value)
                                bonLivraison.BAvanceForfaitaire = bool.Parse(dr["BAvanceForfaitaire"].ToString());
                            //if (dr["BImprimer"] != DBNull.Value)
                            //    bonLivraison.BImprimer = bool.Parse(dr["BImprimer"].ToString());
                            //if (dr["MontantHT"] != DBNull.Value)
                            bonLivraison.MontantHT = decimal.Parse(dr["MontantHT"].ToString());
                            if (dr["MontantRemise"] != DBNull.Value)
                                bonLivraison.MontantRemise = decimal.Parse(dr["MontantRemise"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                bonLivraison.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["MontantTTC"] != DBNull.Value)
                                bonLivraison.MontantTTC = decimal.Parse(dr["MontantTTC"].ToString());
                            if (dr["NBonCommande"] != DBNull.Value)
                                bonLivraison.NBonCommande = dr["NBonCommande"].ToString();
                            if (dr["NOrdrePreparation"] != DBNull.Value)
                                bonLivraison.NOrdrePreparation = dr["NOrdrePreparation"].ToString();
                            if (dr["NBonCommandeMannuel"] != DBNull.Value)
                                bonLivraison.NBonCommandeMannuel = dr["NBonCommandeMannuel"].ToString();
                            if (dr["NBonLivraisonMannuel"] != DBNull.Value)
                                bonLivraison.NBonLivraisonMannuel = dr["NBonLivraisonMannuel"].ToString();
                            if (dr["NFacture"] != DBNull.Value)
                                bonLivraison.NFacture = dr["NFacture"].ToString();
                            if (dr["Observation"] != DBNull.Value)
                                bonLivraison.Observation = dr["Observation"].ToString();
                            if (dr["RaisonSociale"] != DBNull.Value)
                                bonLivraison.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["Indice"] != DBNull.Value)
                                bonLivraison.Indice = int.Parse(dr["Indice"].ToString());
                            if (dr["BRetour"] != DBNull.Value)
                                bonLivraison.BRetour = bool.Parse(dr["BRetour"].ToString());
                            if (dr["Chauffeur"] != DBNull.Value)
                                bonLivraison.Chauffeur = dr["Chauffeur"].ToString();
                            if (dr["CVehicule"] != DBNull.Value)
                                bonLivraison.CVehicule = dr["CVehicule"].ToString();
                            if (dr["MontantRetenuForfaitaire"] != DBNull.Value)
                                bonLivraison.MontantRetenuForfaitaire = decimal.Parse(dr["MontantRetenuForfaitaire"].ToString());
                            if (dr["poidsTotal"] != DBNull.Value)
                                bonLivraison.poidsTotal = decimal.Parse(dr["poidsTotal"].ToString());
                            collection.Add(bonLivraison);
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

        public static BonLivraisonCollection RecupererBLs_ParFacture(string nFacture, SqlTransaction transaction)
        {
            BonLivraison bonLivraison = null;
            BonLivraisonCollection collection = new BonLivraisonCollection();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BonLivraisonParFacure_Charger";
                cmd.Parameters.AddWithValue("@NFacture", nFacture);
                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        bonLivraison = new BonLivraison();
                        bonLivraison.NBonLivraison = dr["NBonLivraison"].ToString();
                        if (dr["CClient"] != DBNull.Value)
                            bonLivraison.CClient = dr["CClient"].ToString();
                        if (dr["MontantHT"] != DBNull.Value)
                            bonLivraison.MontantHT = decimal.Parse(dr["MontantHT"].ToString());
                        if (dr["MontantRemise"] != DBNull.Value)
                            bonLivraison.MontantRemise = decimal.Parse(dr["MontantRemise"].ToString());
                        if (dr["MontantTaxe"] != DBNull.Value)
                            bonLivraison.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                        if (dr["MontantTTC"] != DBNull.Value)
                            bonLivraison.MontantTTC = decimal.Parse(dr["MontantTTC"].ToString());
                        if (dr["NBonCommande"] != DBNull.Value)
                            bonLivraison.NBonCommande = dr["NBonCommande"].ToString();
                        if (dr["NBonCommandeMannuel"] != DBNull.Value)
                            bonLivraison.NBonCommandeMannuel = dr["NBonCommandeMannuel"].ToString();
                        if (dr["NBonLivraisonMannuel"] != DBNull.Value)
                            bonLivraison.NBonLivraisonMannuel = dr["NBonLivraisonMannuel"].ToString();
                        if (dr["NFacture"] != DBNull.Value)
                            bonLivraison.NFacture = dr["NFacture"].ToString();
                        if (dr["Chauffeur"] != DBNull.Value)
                            bonLivraison.Chauffeur = dr["Chauffeur"].ToString();
                        collection.Add(bonLivraison);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return collection;
        }

        public static BonLivraisonCollection RecupererBLs_ParFacture(string nFacture)
        {
            BonLivraison bonLivraison = null;
            BonLivraisonCollection collection = new BonLivraisonCollection();
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();

                    cmd.Connection = cn;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "BonLivraisonParFacure_Charger";
                    cmd.Parameters.AddWithValue("@NFacture", nFacture);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            bonLivraison = new BonLivraison();
                            bonLivraison.NBonLivraison = dr["NBonLivraison"].ToString();
                            if (dr["CClient"] != DBNull.Value)
                                bonLivraison.CClient = dr["CClient"].ToString();
                            if (dr["MontantHT"] != DBNull.Value)
                                bonLivraison.MontantHT = decimal.Parse(dr["MontantHT"].ToString());
                            if (dr["MontantRemise"] != DBNull.Value)
                                bonLivraison.MontantRemise = decimal.Parse(dr["MontantRemise"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                bonLivraison.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["MontantTTC"] != DBNull.Value)
                                bonLivraison.MontantTTC = decimal.Parse(dr["MontantTTC"].ToString());
                            if (dr["NBonCommande"] != DBNull.Value)
                                bonLivraison.NBonCommande = dr["NBonCommande"].ToString();
                            if (dr["NBonCommandeMannuel"] != DBNull.Value)
                                bonLivraison.NBonCommandeMannuel = dr["NBonCommandeMannuel"].ToString();
                            if (dr["NBonLivraisonMannuel"] != DBNull.Value)
                                bonLivraison.NBonLivraisonMannuel = dr["NBonLivraisonMannuel"].ToString();
                            if (dr["NFacture"] != DBNull.Value)
                                bonLivraison.NFacture = dr["NFacture"].ToString();
                            if (dr["Chauffeur"] != DBNull.Value)
                                bonLivraison.Chauffeur = dr["Chauffeur"].ToString();
                            collection.Add(bonLivraison);
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

        public static BonLivraisonCollection ChargerBCTransformer(string nBonCommande)
        {
            BonLivraisonCollection collection = new BonLivraisonCollection();
            if (string.IsNullOrEmpty(nBonCommande))
                nBonCommande = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "BonLivraison_ChargerBCTransformer";

                    cmd.Parameters.AddWithValue("@NBonCommande", nBonCommande);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            BonLivraison bonLivraison = new BonLivraison();
                            if (dr["NBonLivraison"] != DBNull.Value)
                                bonLivraison.NBonLivraison = dr["NBonLivraison"].ToString();
                            if (dr["CClient"] != DBNull.Value)
                                bonLivraison.CClient = dr["CClient"].ToString();
                            if (dr["DateLivraison"] != DBNull.Value)
                                bonLivraison.DateLivraison = DateTime.Parse(dr["DateLivraison"].ToString());
                            if (dr["CUnite"] != DBNull.Value)
                                bonLivraison.CUnite = dr["CUnite"].ToString();
                            if (dr["BEchantillon"] != DBNull.Value)
                                bonLivraison.BEchantillon = bool.Parse(dr["BEchantillon"].ToString());
                            if (dr["BGratuit"] != DBNull.Value)
                                bonLivraison.BGratuit = bool.Parse(dr["BGratuit"].ToString());
                            if (dr["MatriculeFiscale"] != DBNull.Value)
                                bonLivraison.MatriculeFiscale = dr["MatriculeFiscale"].ToString();
                            if (dr["Adresse"] != DBNull.Value)
                                bonLivraison.Adresse = dr["Adresse"].ToString();
                            if (dr["NTelephone"] != DBNull.Value)
                                bonLivraison.NTelephone = dr["NTelephone"].ToString();
                            if (dr["CVendeur"] != DBNull.Value)
                                bonLivraison.CVendeur = int.Parse(dr["CVendeur"].ToString());
                            if (dr["BExonoreFodec"] != DBNull.Value)
                                bonLivraison.BExonoreFodec = bool.Parse(dr["BExonoreFodec"].ToString());
                            if (dr["BExonoreTVA"] != DBNull.Value)
                                bonLivraison.BExonoreTVA = bool.Parse(dr["BExonoreTVA"].ToString());
                            if (dr["BAvanceForfaitaire"] != DBNull.Value)
                                bonLivraison.BAvanceForfaitaire = bool.Parse(dr["BAvanceForfaitaire"].ToString());
                            if (dr["MontantHT"] != DBNull.Value)
                                bonLivraison.MontantHT = decimal.Parse(dr["MontantHT"].ToString());
                            if (dr["MontantRemise"] != DBNull.Value)
                                bonLivraison.MontantRemise = decimal.Parse(dr["MontantRemise"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                bonLivraison.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["MontantTTC"] != DBNull.Value)
                                bonLivraison.MontantTTC = decimal.Parse(dr["MontantTTC"].ToString());
                            if (dr["NBonCommande"] != DBNull.Value)
                                bonLivraison.NBonCommande = dr["NBonCommande"].ToString();
                            if (dr["NBonCommandeMannuel"] != DBNull.Value)
                                bonLivraison.NBonCommandeMannuel = dr["NBonCommandeMannuel"].ToString();
                            if (dr["NBonLivraisonMannuel"] != DBNull.Value)
                                bonLivraison.NBonLivraisonMannuel = dr["NBonLivraisonMannuel"].ToString();
                            if (dr["NFacture"] != DBNull.Value)
                                bonLivraison.NFacture = dr["NFacture"].ToString();
                            if (dr["Observation"] != DBNull.Value)
                                bonLivraison.Observation = dr["Observation"].ToString();
                            if (dr["RaisonSociale"] != DBNull.Value)
                                bonLivraison.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["Indice"] != DBNull.Value)
                                bonLivraison.Indice = int.Parse(dr["Indice"].ToString());
                            if (dr["BRetour"] != DBNull.Value)
                                bonLivraison.BRetour = bool.Parse(dr["BRetour"].ToString());
                            if (dr["Chauffeur"] != DBNull.Value)
                                bonLivraison.Chauffeur = dr["Chauffeur"].ToString();
                            if (dr["CVehicule"] != DBNull.Value)
                                bonLivraison.CVehicule = dr["CVehicule"].ToString();
                            if (dr["MontantRetenuForfaitaire"] != DBNull.Value)
                                bonLivraison.MontantRetenuForfaitaire = decimal.Parse(dr["MontantRetenuForfaitaire"].ToString());

                            bonLivraison.BonLivraisonDetailCollection = BonLivraisonDetailCollection.Charger(bonLivraison.NBonLivraison);
                            bonLivraison.BonLivraisonTaxeCollection = BonLivraisonTaxeCollection.Charger(bonLivraison.NBonLivraison);
                            collection.Add(bonLivraison);
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

        public BonLivraison RecupererBonLivraison(string nBonLivraison)
        {
            BonLivraison bonLivraison = null;
            bonLivraison = this.Where(p => p.NBonLivraison.Equals(nBonLivraison)).FirstOrDefault();
            return bonLivraison;
        }

        public static BonLivraisonCollection ChargerAnomalie(string cClient, string nBonLivraison)
        {
            BonLivraisonCollection collection = new BonLivraisonCollection();
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
                    cmd.CommandText = "bonLivraison_ChargerAnomalie";
                    cmd.Parameters.AddWithValue("@CClient", cClient);
                    cmd.Parameters.AddWithValue("@NBonLivraison", nBonLivraison);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            BonLivraison bonLivraison = new BonLivraison();
                            bonLivraison.NBonLivraison = dr["NBonLivraison"].ToString();
                            if (dr["NFacture"] != DBNull.Value)
                                bonLivraison.NFacture = dr["NFacture"].ToString();
                            if (dr["CClient"] != DBNull.Value)
                                bonLivraison.CClient = dr["CClient"].ToString();
                            if (dr["RaisonSociale"] != DBNull.Value)
                                bonLivraison.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["NBonCommande"] != DBNull.Value)
                                bonLivraison.NBonCommande = dr["NBonCommande"].ToString();
                            collection.Add(bonLivraison);
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