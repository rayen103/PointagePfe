using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace CST.LePoint.Vente.Metier
{
    public class BonCommande 
    {
        #region Proriétès

        [XmlAttribute("NBonCommande")]
        [Bindable(true)]
        public string NBonCommande { get; set; }

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

        [XmlAttribute("DateCommande")]
        [Bindable(true)]
        public DateTime? DateCommande { get; set; }

        [XmlAttribute("DateLivraison")]
        [Bindable(true)]
        public DateTime? DateLivraison { get; set; }

        [XmlAttribute("Etat")]
        [Bindable(true)]
        public string Etat { get; set; }

        [XmlAttribute("BExonereFodec")]
        [Bindable(true)]
        public bool BExonoreFodec { get; set; }
        /*
        [XmlAttribute("BExonereTPE")]
        [Bindable(true)]
        public bool BExonoreTPE { get; set; }

        [XmlAttribute("BExonereTDC")]
        [Bindable(true)]
        public bool BExonoreTDC { get; set; }
          */
        [XmlAttribute("BExonereTVA")]
        [Bindable(true)]
        public bool BExonoreTVA { get; set; }

        [XmlAttribute("BAvanceForfaitaire")]
        [Bindable(true)]
        public bool BAvanceForfaitaire { get; set; }

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

        [XmlAttribute("PoidsTotal")]
        [Bindable(true)]
        public decimal PoidsTotal { get; set; }

        [XmlAttribute("NDevis")]
        [Bindable(true)]
        public string NDevis { get; set; }

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

        [XmlAttribute("BSpecial")]
        [Bindable(true)]
        public bool BSpecial { get; set; }
        
        [XmlAttribute("NChantier")]
        [Bindable(true)]
        public string NChantier { get; set; }

        [XmlAttribute("BLinked")]
        [Bindable(true)]
        public bool BLinked { get; set; }
        [XmlAttribute("CTBAchat")]
        [Bindable(true)]
        public string CTBAchat { get; set; }
        [XmlAttribute("LibTBAchat")]
        [Bindable(true)]
        public string LibTBAchat { get; set; }
        [XmlAttribute("DateBC")]
        [Bindable(true)]
        public string DateBC { get; set; }
        [XmlAttribute("ordre")]
        [Bindable(true)]
        public string ordre { get; set; }
        [XmlAttribute("CEquipe")]
        [Bindable(true)]
        public string CEquipe { get; set; }

        [XmlAttribute("CModeReglement")]
        [Bindable(true)]
        public string CModeReglement { get; set; }

        [XmlAttribute("CTypeBonCommande")]
        [Bindable(true)]
        public string CTypeBonCommande { get; set; }

        [XmlAttribute("CEtablissement")]
        [Bindable(true)]
        public string CEtablissement { get; set; }

        [XmlAttribute("BValide")]
        [Bindable(true)]
        public int BValide { get; set; }

        [XmlAttribute("ModalitesPaiement")]
        [Bindable(true)]
        public string ModalitesPaiement { get; set; }

       // public BonCommandeCollection BonCommandCollection;
        public BonCommandeDetailCollection BonCommandeDetailCollection;
        public BonCommandeTaxeCollection BonCommandeTaxeCollection;

        #endregion Proriétès

        public BonCommande()

        {
            //this.BonCommandCollection = new BonCommandeCollection();
            this.BonCommandeDetailCollection = new BonCommandeDetailCollection();
            this.BonCommandeTaxeCollection = new BonCommandeTaxeCollection();
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

        public void Inserer(BonCommandeSpecialDetailCollection collectionBCS)
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    Inserer(transaction);
                    if (collectionBCS.Count > 0)
                    {
                        foreach (BonCommandeSpecialDetail bonCommandeSpecialDetail in collectionBCS)
                        {
                            bonCommandeSpecialDetail.NBonCommande = this.NBonCommande;
                            bonCommandeSpecialDetail.Sauvegarder(transaction);
                        }
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

        public void Inserer(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BonCommande_Inserer";
                cmd.Parameters.AddWithValue("@CUnite", this.CUnite);
                cmd.Parameters.AddWithValue("@CClient", this.CClient);
                cmd.Parameters.AddWithValue("@BExonoreTVA ", this.BExonoreTVA);
                cmd.Parameters.AddWithValue("@BAvanceForfaitaire ", this.BAvanceForfaitaire);
                cmd.Parameters.AddWithValue("@BExonoreFodec", this.BExonoreFodec);
                cmd.Parameters.AddWithValue("@CreePar ", this.CreePar);
                cmd.Parameters.AddWithValue("@CVendeur ", this.CVendeur);
                cmd.Parameters.AddWithValue("@DateCommande ", this.DateCommande);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                cmd.Parameters.AddWithValue("@DateLivraison ", this.DateLivraison);
                cmd.Parameters.AddWithValue("@Exercice", this.Exercice);
                cmd.Parameters.AddWithValue("@Etat", this.Etat);
                cmd.Parameters.AddWithValue("@MatriculeFiscale", this.MatriculeFiscale);
                cmd.Parameters.AddWithValue("@Adresse", this.Adresse);
                cmd.Parameters.AddWithValue("@NTelephone", this.NTelephone);
                cmd.Parameters.AddWithValue("@MontantHT", this.MontantHT);
                cmd.Parameters.AddWithValue("@PoidsTotal", this.PoidsTotal);
                cmd.Parameters.AddWithValue("@MontantRemise", this.MontantRemise);
                cmd.Parameters.AddWithValue("@MontantRetenuForfaitaire", this.MontantRetenuForfaitaire);
                cmd.Parameters.AddWithValue("@MontantTaxe", this.MontantTaxe);
                cmd.Parameters.AddWithValue("@MontantTTC", this.MontantTTC);
                cmd.Parameters.AddWithValue("@NDevis", this.NDevis);
                cmd.Parameters.AddWithValue("@Observation", this.Observation);
                cmd.Parameters.AddWithValue("@RaisonSociale", this.RaisonSociale);
                cmd.Parameters.AddWithValue("@Reference", this.Reference);
                cmd.Parameters.AddWithValue("@BSpecial", this.BSpecial);
                cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);
                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        this.NBonCommande = dr["NBonCommande"].ToString();
                        this.Indice = int.Parse(dr["DernierIndice"].ToString());
                    }
                }
                int i = 1;
                foreach (BonCommandeDetail bonCommandeDetail in BonCommandeDetailCollection)
                {
                    bonCommandeDetail.NBonCommande = this.NBonCommande;
                    bonCommandeDetail.Ordre = i++;
                    bonCommandeDetail.Sauvegarder(transaction);
                }

                foreach (BonCommandeTaxe bonCommandeTaxe in BonCommandeTaxeCollection)
                {
                    bonCommandeTaxe.NBonCommande = this.NBonCommande;
                    bonCommandeTaxe.Sauvegarder(transaction);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void mobileInserer(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Mobile_BonCommande_Inserer";
                cmd.Parameters.AddWithValue("@CUnite", this.CUnite);
                cmd.Parameters.AddWithValue("@CClient", this.CClient);
                cmd.Parameters.AddWithValue("@BExonoreTVA ", this.BExonoreTVA);
                cmd.Parameters.AddWithValue("@BAvanceForfaitaire ", this.BAvanceForfaitaire);
                cmd.Parameters.AddWithValue("@BExonoreFodec", this.BExonoreFodec);
                //cmd.Parameters.AddWithValue("@BExonoreTPE", this.BExonoreTPE);
                //cmd.Parameters.AddWithValue("@BExonoreTDC", this.BExonoreTDC);
                cmd.Parameters.AddWithValue("@CreePar ", this.CreePar);
                cmd.Parameters.AddWithValue("@CVendeur ", this.CVendeur);
                cmd.Parameters.AddWithValue("@DateCommande ", this.DateCommande);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                cmd.Parameters.AddWithValue("@DateLivraison ", this.DateLivraison);
                cmd.Parameters.AddWithValue("@Exercice", this.Exercice);
                cmd.Parameters.AddWithValue("@Etat", this.Etat);
                cmd.Parameters.AddWithValue("@MatriculeFiscale", this.MatriculeFiscale);
                cmd.Parameters.AddWithValue("@Adresse", this.Adresse);
                cmd.Parameters.AddWithValue("@NTelephone", this.NTelephone);
                cmd.Parameters.AddWithValue("@MontantHT", this.MontantHT);
                cmd.Parameters.AddWithValue("@PoidsTotal", this.PoidsTotal);
                cmd.Parameters.AddWithValue("@MontantRemise", this.MontantRemise);
                cmd.Parameters.AddWithValue("@MontantRetenuForfaitaire", this.MontantRetenuForfaitaire);
                cmd.Parameters.AddWithValue("@MontantTaxe", this.MontantTaxe);
                cmd.Parameters.AddWithValue("@MontantTTC", this.MontantTTC);
                cmd.Parameters.AddWithValue("@NDevis", this.NDevis);
                cmd.Parameters.AddWithValue("@Observation", this.Observation);
                cmd.Parameters.AddWithValue("@RaisonSociale", this.RaisonSociale);
                cmd.Parameters.AddWithValue("@Reference", this.Reference);
                cmd.Parameters.AddWithValue("@BSpecial", this.BSpecial);            
                cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);
                cmd.Parameters.AddWithValue("@NOrdredeTravail ", this.ordre);
                cmd.Parameters.AddWithValue("@BValide", this.BValide);
                cmd.Parameters.AddWithValue("@CEtablissement", this.CEtablissement);
                cmd.Parameters.AddWithValue("@CTBAchat", this.CTBAchat);
                cmd.Parameters.AddWithValue("@LibTBAchat", this.LibTBAchat);
                cmd.Parameters.AddWithValue("@CEquipe", this.CEquipe);
                cmd.Parameters.AddWithValue("@CModeReglement", this.CModeReglement);
                cmd.Parameters.AddWithValue("@CTypeBonCommande", this.CTypeBonCommande);
                cmd.Parameters.AddWithValue("@ModalitesPaiement", this.ModalitesPaiement);

                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        this.NBonCommande = dr["NBonCommande"].ToString();
                        this.Indice = int.Parse(dr["DernierIndice"].ToString());
                    }
                }
                int i = 1;
                foreach (BonCommandeDetail bonCommandeDetail in BonCommandeDetailCollection)
                {
                    bonCommandeDetail.NBonCommande = this.NBonCommande;
                    bonCommandeDetail.Ordre = i++;
                    bonCommandeDetail.mobileSauvegarder(transaction);
                }

                foreach (BonCommandeTaxe bonCommandeTaxe in BonCommandeTaxeCollection)
                {
                    bonCommandeTaxe.NBonCommande = this.NBonCommande;
                    bonCommandeTaxe.mobileSauvegarder(transaction);
                }
                //MobileRattachement mobile = new MobileRattachement();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void mobileModifier(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Mobile_BonCommande_Modifier";
                cmd.Parameters.AddWithValue("@NBonCommande", this.NBonCommande);
                cmd.Parameters.AddWithValue("@CUnite", this.CUnite);
                cmd.Parameters.AddWithValue("@CClient", this.CClient);
                cmd.Parameters.AddWithValue("@BExonoreTVA ", this.BExonoreTVA);
                cmd.Parameters.AddWithValue("@BAvanceForfaitaire ", this.BAvanceForfaitaire);
                cmd.Parameters.AddWithValue("@BExonoreFodec", this.BExonoreFodec);
                //cmd.Parameters.AddWithValue("@BExonoreTPE", this.BExonoreTPE);
                //cmd.Parameters.AddWithValue("@BExonoreTDC", this.BExonoreTDC);
                cmd.Parameters.AddWithValue("@CreePar ", this.CreePar);
                cmd.Parameters.AddWithValue("@CVendeur ", this.CVendeur);
                cmd.Parameters.AddWithValue("@DateCommande ", this.DateCommande);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                cmd.Parameters.AddWithValue("@DateLivraison ", this.DateLivraison);
                //cmd.Parameters.AddWithValue("@Exercice", this.Exercice);
                cmd.Parameters.AddWithValue("@Etat", this.Etat);
                cmd.Parameters.AddWithValue("@MatriculeFiscale", this.MatriculeFiscale);
                cmd.Parameters.AddWithValue("@Adresse", this.Adresse);
                cmd.Parameters.AddWithValue("@NTelephone", this.NTelephone);
                cmd.Parameters.AddWithValue("@MontantHT", this.MontantHT);
                cmd.Parameters.AddWithValue("@PoidsTotal", this.PoidsTotal);
                cmd.Parameters.AddWithValue("@MontantRemise", this.MontantRemise);
                cmd.Parameters.AddWithValue("@MontantRetenuForfaitaire", this.MontantRetenuForfaitaire);
                cmd.Parameters.AddWithValue("@MontantTaxe", this.MontantTaxe);
                cmd.Parameters.AddWithValue("@MontantTTC", this.MontantTTC);
                cmd.Parameters.AddWithValue("@NDevis", this.NDevis);
                cmd.Parameters.AddWithValue("@Observation", this.Observation);
                cmd.Parameters.AddWithValue("@RaisonSociale", this.RaisonSociale);
                //cmd.Parameters.AddWithValue("@Reference", this.Reference);
                cmd.Parameters.AddWithValue("@BSpecial", this.BSpecial);
                cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);
                cmd.Parameters.AddWithValue("@NOrdredeTravail ", this.ordre);
                cmd.Parameters.AddWithValue("@BValide", this.BValide);
                cmd.Parameters.AddWithValue("@CEtablissement", this.CEtablissement);
                cmd.Parameters.AddWithValue("@CTBAchat", this.CTBAchat);
                cmd.Parameters.AddWithValue("@LibTBAchat", this.LibTBAchat);
                cmd.Parameters.AddWithValue("@CEquipe", this.CEquipe);
                cmd.Parameters.AddWithValue("@CModeReglement", this.CModeReglement);
                cmd.Parameters.AddWithValue("@CTypeBonCommande", this.CTypeBonCommande);
                cmd.Parameters.AddWithValue("@ModalitesPaiement", this.ModalitesPaiement);

                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;
                cmd.ExecuteNonQuery();
                this.RestituerStockReserver(transaction);
                int i = 1;
                foreach (BonCommandeDetail bonCommandeDetail in BonCommandeDetailCollection)
                {
                    bonCommandeDetail.NBonCommande = this.NBonCommande;
                    bonCommandeDetail.Ordre = i++;
                    bonCommandeDetail.mobileSauvegarder(transaction);
                }
                this.SupprimerTaxes(transaction);
                foreach (BonCommandeTaxe bonCommandeTaxe in BonCommandeTaxeCollection)
                {
                    bonCommandeTaxe.NBonCommande = this.NBonCommande;
                    bonCommandeTaxe.mobileSauvegarder(transaction);
                }
                //MobileRattachement mobile = new MobileRattachement();
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

        public void Modifier(BonCommandeSpecialDetailCollection collectionBCS)
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    Modifier(transaction);
                    BonCommandeSpecialDetailCollection collection = BonCommandeSpecialDetailCollection.Charger(this.NBonCommande);
                    foreach (BonCommandeSpecialDetail specialDetail in collection)
                        specialDetail.Supprimer(transaction);

                    if (collectionBCS.Count > 0)
                    {
                        foreach (BonCommandeSpecialDetail bonCommandeSpecialDetail in collectionBCS)
                        {
                            bonCommandeSpecialDetail.NBonCommande = this.NBonCommande;
                            bonCommandeSpecialDetail.Sauvegarder(transaction);
                        }
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

        public void Modifier(SqlTransaction transaction)
        {
            BonCommande ancienCommande = BonCommande.Charger(NBonCommande);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BonCommande_Modifier";
                cmd.Parameters.AddWithValue("@NBonCommande", this.NBonCommande);
                cmd.Parameters.AddWithValue("@BExonoreTVA", this.BExonoreTVA);
                cmd.Parameters.AddWithValue("@BAvanceForfaitaire", this.BAvanceForfaitaire);
                cmd.Parameters.AddWithValue("@BExonoreFodec", this.BExonoreFodec);
                cmd.Parameters.AddWithValue("@CClient ", this.CClient);
                cmd.Parameters.AddWithValue("@CUnite", this.CUnite);
                cmd.Parameters.AddWithValue("@CVendeur ", this.CVendeur);
                cmd.Parameters.AddWithValue("@DateCommande ", this.DateCommande);
                cmd.Parameters.AddWithValue("@DateLivraison ", this.DateLivraison);
                cmd.Parameters.AddWithValue("@Etat ", this.Etat);
                cmd.Parameters.AddWithValue("@MatriculeFiscale ", this.MatriculeFiscale);
                cmd.Parameters.AddWithValue("@Adresse", this.Adresse);
                cmd.Parameters.AddWithValue("@NTelephone", this.NTelephone);
                cmd.Parameters.AddWithValue("@MontantHT ", this.MontantHT);
                cmd.Parameters.AddWithValue("@MontantRemise ", this.MontantRemise);
                cmd.Parameters.AddWithValue("@MontantRetenuForfaitaire ", this.MontantRetenuForfaitaire);
                cmd.Parameters.AddWithValue("@MontantTaxe ", this.MontantTaxe);
                cmd.Parameters.AddWithValue("@MontantTTC ", this.MontantTTC);
                cmd.Parameters.AddWithValue("@PoidsTotal", this.PoidsTotal);
                cmd.Parameters.AddWithValue("@NDevis ", this.NDevis);
                cmd.Parameters.AddWithValue("@Observation ", this.Observation);
                cmd.Parameters.AddWithValue("@RaisonSociale ", this.RaisonSociale);
                cmd.Parameters.AddWithValue("@Reference", this.Reference);
                cmd.Parameters.AddWithValue("@PCModification", this.PCModification);
                cmd.Parameters.AddWithValue("@ModifiePar", this.ModifiePar);
                cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                cmd.Parameters.AddWithValue("@NChantier", NChantier);
               // cmd.Parameters.AddWithValue("@QuantiteOT", QuantiteOT);
                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                cmd.ExecuteNonQuery();

                this.RestituerStockReserver(transaction);
                int i = 1;
                OrdrePreparation ordre = OrdrePreparation.ChargerDernierOrdre(NBonCommande, transaction);
                OrdrePreparationDetailCollection collection = new OrdrePreparationDetailCollection();
                foreach (BonCommandeDetail bonCommandeDetail in BonCommandeDetailCollection)
                {

                    bonCommandeDetail.NBonCommande = this.NBonCommande;
                    bonCommandeDetail.Ordre = i++;
                    bonCommandeDetail.Sauvegarder(transaction);
                    //if (ordre != null)
                    //{
                    //    OrdrePreparationDetail detail = ordre.OrdrePreparationDetailCollection.RecupererOrdrePreparationDetail(ordre.NOrdrePreparation, bonCommandeDetail.CArticle, bonCommandeDetail.Ordre);
                    //    if (detail == null)
                    //    {
                    //        OrdrePreparationDetail nouveauDetail = new OrdrePreparationDetail();
                    //        nouveauDetail.CArticle = bonCommandeDetail.CArticle;
                    //        nouveauDetail.CEntrepot = bonCommandeDetail.CEntrepot;
                    //        nouveauDetail.CTaxe = bonCommandeDetail.CTaxe;
                    //        nouveauDetail.CUnite = bonCommandeDetail.CUnite;
                    //        nouveauDetail.LibArticle = bonCommandeDetail.LibArticle;
                    //        nouveauDetail.MontantNet = bonCommandeDetail.MontantNet;
                    //        nouveauDetail.MontantTaxe = bonCommandeDetail.MontantTaxe;
                    //        nouveauDetail.NOrdrePreparation = ordre.NOrdrePreparation;
                    //        //nouveauDetail.Ordre= ordre
                    //        nouveauDetail.OrdreBonCommande = bonCommandeDetail.Ordre;
                    //        nouveauDetail.PourcentageFodec = bonCommandeDetail.PourcentageFodec;
                    //        nouveauDetail.PourcentageRemise = bonCommandeDetail.PourcentageRemise;
                    //        nouveauDetail.PrixHT = bonCommandeDetail.PrixHTArticle;
                    //        nouveauDetail.PrixRevient = bonCommandeDetail.PrixVentePublic;
                    //        nouveauDetail.PrixVentePublic = bonCommandeDetail.PrixVentePublic;
                    //        nouveauDetail.Quantite = bonCommandeDetail.Quantite;
                    //        nouveauDetail.QuantiteHistorique = nouveauDetail.Quantite;
                    //        nouveauDetail.Remise1 = bonCommandeDetail.Remise1;
                    //        nouveauDetail.Remise2 = bonCommandeDetail.Remise2;
                    //        nouveauDetail.TauxTVA = bonCommandeDetail.TauxTVA;
                    //        collection.Add(nouveauDetail);

                    //    }
                    //}
        
                }
                //if(collection.Count!=0)
                //    ModifierOrdre(transaction, ordre, collection);
                this.SupprimerTaxes(transaction);
                foreach (BonCommandeTaxe bonCommandeTaxe in BonCommandeTaxeCollection)
                {
                    bonCommandeTaxe.NBonCommande = this.NBonCommande;
                    bonCommandeTaxe.Sauvegarder(transaction);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ModifObservation()
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
                    cmd.CommandText = "BonCommande_ModifierObservation";
                    cmd.Parameters.AddWithValue("@NBonCommande", this.NBonCommande);
                    cmd.Parameters.AddWithValue("@DateLivraison ", this.DateLivraison);
                    cmd.Parameters.AddWithValue("@Observation ", this.Observation);
                    cmd.Parameters.AddWithValue("@Reference", this.Reference);
                    cmd.Parameters.AddWithValue("@PCModification", this.PCModification);
                    cmd.Parameters.AddWithValue("@ModifiePar", this.ModifiePar);
                    cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                    foreach (SqlParameter parametre in cmd.Parameters)
                    {
                        if (parametre.Value == null)
                        {
                            parametre.Value = DBNull.Value;
                        }
                    }
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

        public void ModifObservationBCS(BonCommandeSpecialDetailCollection collectionBCS)
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
                    cmd.CommandText = "BonCommande_ModifierObservation";
                    cmd.Parameters.AddWithValue("@NBonCommande", this.NBonCommande);
                    cmd.Parameters.AddWithValue("@DateLivraison ", this.DateLivraison);
                    cmd.Parameters.AddWithValue("@Observation ", this.Observation);
                    cmd.Parameters.AddWithValue("@Reference", this.Reference);
                    cmd.Parameters.AddWithValue("@PCModification", this.PCModification);
                    cmd.Parameters.AddWithValue("@ModifiePar", this.ModifiePar);
                    cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                    foreach (SqlParameter parametre in cmd.Parameters)
                    {
                        if (parametre.Value == null)
                        {
                            parametre.Value = DBNull.Value;
                        }
                    }
                    cmd.ExecuteNonQuery();

                    BonCommandeSpecialDetailCollection collection = BonCommandeSpecialDetailCollection.Charger(this.NBonCommande);
                    foreach (BonCommandeSpecialDetail specialDetail in collection)
                        specialDetail.Supprimer(transaction);

                    if (collectionBCS.Count > 0)
                    {
                        foreach (BonCommandeSpecialDetail bonCommandeSpecialDetail in collectionBCS)
                        {
                            bonCommandeSpecialDetail.NBonCommande = this.NBonCommande;
                            bonCommandeSpecialDetail.Sauvegarder(transaction);
                        }
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

        private void ModifierOrdre(SqlTransaction transaction, OrdrePreparation ordre, OrdrePreparationDetailCollection collection)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                decimal montantHT = collection[i].PrixHT * collection[i].Quantite;
                decimal montantRemise = (montantHT * collection[i].PourcentageRemise) / 100;
                decimal montantNetHT = montantHT - montantRemise;
                decimal montantFodec = (montantNetHT * collection[i].PourcentageFodec) / 100;
                decimal assietteTVA = montantNetHT + montantFodec;
                decimal montantTVA = (assietteTVA * collection[i].TauxTVA) / 100;
                decimal assietteForfaitaire = assietteTVA + montantTVA;
                decimal montantForfaitaire = (assietteForfaitaire * decimal.Parse(VenteHelper.POURCENTAGE_TAUX_FORFAITAIRE.ToString())) / 100;
                decimal ttc = assietteForfaitaire + montantForfaitaire;
                ordre.MontantHT = ordre.MontantHT + montantHT;
                ordre.MontantRemise = ordre.MontantRemise + montantRemise;
                ordre.MontantRetenuForfaitaire = ordre.MontantRetenuForfaitaire + MontantRetenuForfaitaire;
                ordre.MontantTaxe = ordre.MontantTaxe + montantTVA;
                ordre.MontantTTC = ordre.MontantTTC + ttc;
                ordre.OrdrePreparationDetailCollection.Add(collection[i]);
                if (collection[i].CTaxe != null)
                {
                    OrdrePreparationTaxe ordrePreparationTaxe = new OrdrePreparationTaxe();

                    ordrePreparationTaxe = ordre.OrdrePreparationTaxeCollection.RecupererOrdrePreparationTaxe(collection[i].CTaxe);
                    if (ordrePreparationTaxe != null)
                    {
                        ordrePreparationTaxe.CTaxe = collection[i].CTaxe;
                        ordrePreparationTaxe.MontantTaxe = ordrePreparationTaxe.MontantTaxe + collection[i].MontantTaxe;
                        ordrePreparationTaxe.TauxTVA = collection[i].TauxTVA;
                        ordrePreparationTaxe.Assiette = ordrePreparationTaxe.Assiette + assietteTVA;
                        ordre.OrdrePreparationTaxeCollection.Remove(ordrePreparationTaxe);
                        ordrePreparationTaxe.BExonoreFodec = ordre.BExonoreFodec;
                        ordrePreparationTaxe.BExonoreTVA = ordre.BExonoreTVA;
                        ordre.OrdrePreparationTaxeCollection.Remove(ordrePreparationTaxe);
                    }
                    else
                    {
                        ordrePreparationTaxe = new OrdrePreparationTaxe();
                        ordrePreparationTaxe.CTaxe = collection[i].CTaxe;
                        ordrePreparationTaxe.MontantTaxe = collection[i].MontantTaxe;
                        ordrePreparationTaxe.TauxTVA = collection[i].TauxTVA;
                        ordrePreparationTaxe.Assiette = assietteTVA;
                        ordrePreparationTaxe.BExonoreFodec = ordre.BExonoreFodec;
                        ordrePreparationTaxe.BExonoreTVA = ordre.BExonoreTVA;
                    }
                    ordre.OrdrePreparationTaxeCollection.Add(ordrePreparationTaxe);
                }
            }
            ordre.Modifier(transaction);

        }
        
        public static void ModifierEtatBonCommande(string nCommande, string etat)
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    ModifierEtatBonCommande(nCommande, etat, transaction);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public static void ModifierEtatBonCommande(string nCommande, string etat,string observation)
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    BonCommande bc = BonCommande.Charger(nCommande);
                    bc.Observation = observation;
                    bc.ModifObservation();
                    ModifierEtatBonCommande(nCommande, etat, transaction);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public static void ModifierEtatBonCommande(string nCommande, string etat, SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "BonCommande_ModifierEtat";
                cmd.Parameters.AddWithValue("@NBonCommande", nCommande);
                cmd.Parameters.AddWithValue("@Etat", etat);

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

        public void Annuler()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    // purger Bon Commande: annule la réservation du stock 
                    Purger(transaction);
                   // SupprimerBonCommande(transaction);
                    ModifierEtatBonCommande(this.NBonCommande, VenteHelper.EtatBonCommande.ANNULER.ToString(), transaction);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
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
                    Purger(transaction);
                    SupprimerBonCommande(transaction);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        
        private void SupprimerBonCommande(SqlTransaction transaction)
        {
            this.Etat = VenteHelper.EtatBonCommande.ANNULER.ToString();
            ModifierEtatBonCommande(this.NBonCommande, VenteHelper.EtatBonCommande.ANNULER.ToString(), transaction);
            try
            {
             
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "BonCommande_Supprimer";
                cmd.Parameters.AddWithValue("@NBonCommande", this.NBonCommande);

                cmd.ExecuteNonQuery();
  
            }
            catch (Exception)
            {
         
                throw;
            }
        }

        public void Purger()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    Purger(transaction);
                    ModifierEtatBonCommande(this.NBonCommande, VenteHelper.EtatBonCommande.PURGER.ToString(), transaction);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public void Purger(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BonCommande_Purger";
                cmd.Parameters.AddWithValue("@NBonCommande", this.NBonCommande);

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

        //private void SupprimerDetailCommandeAnterieurs(SqlTransaction transaction)
        //{
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Transaction = transaction;
        //        cmd.Connection = transaction.Connection;
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = "BonCommande_SupprimerDetails";

        //        cmd.Parameters.AddWithValue("@NBonCommande", this.NBonCommande);

        //        cmd.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        private void SupprimerTaxes(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BonCommande_SupprimerTaxes";

                cmd.Parameters.AddWithValue("@NBonCommande", this.NBonCommande);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string RecupererNouveauNBonCommande(string exercice, out int indice)
        {
            string nBonCommande = string.Empty;
            indice = 0;

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                var cmd2 = new SqlCommand();
                cmd2.Connection = cn;
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.CommandText = "BonCommande_RecupererNouveauNumero";
                cmd2.Parameters.AddWithValue("@Exercice", exercice);
                SqlDataReader dr = cmd2.ExecuteReader();
                if (dr.Read())
                {
                    nBonCommande = dr["NBonCommande"].ToString();
                    indice = int.Parse(dr["DernierIndice"].ToString());
                }
                dr.Close();
            }

            return nBonCommande;
        }

        public static string RecupererNouveauNBonCommande(string exercice)
        {
            int indice = 0;
            return BonCommande.RecupererNouveauNBonCommande(exercice, out indice);
        }
        
        public static BonCommande Charger(string nBonCommande, SqlTransaction transaction)
        {
            BonCommande bonCommande = null;
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
   
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Transaction = transaction;
                    cmd.Connection = transaction.Connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "BonCommande_Charger";
                    cmd.Parameters.AddWithValue("@NBonCommande", nBonCommande);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            bonCommande = new BonCommande();
                            bonCommande.NBonCommande = dr["NBonCommande"].ToString();

                            if (dr["CUnite"] != DBNull.Value)
                                bonCommande.CUnite = dr["CUnite"].ToString();
                            if (dr["BExonoreTVA"] != DBNull.Value)
                                bonCommande.BExonoreTVA = bool.Parse(dr["BExonoreTVA"].ToString());
                            if (dr["BAvanceForfaitaire"] != DBNull.Value)
                                bonCommande.BAvanceForfaitaire = bool.Parse(dr["BAvanceForfaitaire"].ToString());
                            if (dr["BExonoreFodec"] != DBNull.Value)
                                bonCommande.BExonoreFodec = bool.Parse(dr["BExonoreFodec"].ToString());
                            if (dr["CClient"] != DBNull.Value)
                                bonCommande.CClient = dr["CClient"].ToString();
                            if (dr["CreePar"] != DBNull.Value)
                                bonCommande.CreePar = int.Parse(dr["CreePar"].ToString());
                            if (dr["CVendeur"] != DBNull.Value)
                                bonCommande.CVendeur = int.Parse(dr["CVendeur"].ToString());
                            if (dr["DateCommande"] != DBNull.Value)
                                bonCommande.DateCommande = DateTime.Parse(dr["DateCommande"].ToString());
                            if (dr["DateInsertion"] != DBNull.Value)
                                bonCommande.DateInsertion = DateTime.Parse(dr["DateInsertion"].ToString());
                            if (dr["DateLivraison"] != DBNull.Value)
                                bonCommande.DateLivraison = DateTime.Parse(dr["DateLivraison"].ToString());
                            if (dr["DateModification"] != DBNull.Value)
                                bonCommande.DateModification = DateTime.Parse(dr["DateModification"].ToString());
                            if (dr["Etat"] != DBNull.Value)
                                bonCommande.Etat = dr["Etat"].ToString();
                            if (dr["Reference"] != DBNull.Value)
                                bonCommande.Reference = dr["Reference"].ToString();
                            if (dr["Indice"] != DBNull.Value)
                                bonCommande.Indice = int.Parse(dr["Indice"].ToString());
                            if (dr["MatriculeFiscale"] != DBNull.Value)
                                bonCommande.MatriculeFiscale = dr["MatriculeFiscale"].ToString();
                            if (dr["Adresse"] != DBNull.Value)
                                bonCommande.Adresse = dr["Adresse"].ToString();
                            if (dr["NTelephone"] != DBNull.Value)
                                bonCommande.NTelephone = dr["NTelephone"].ToString();
                            if (dr["ModifiePar"] != DBNull.Value)
                                bonCommande.ModifiePar = int.Parse(dr["ModifiePar"].ToString());
                            if (dr["MontantHT"] != DBNull.Value)
                                bonCommande.MontantHT = decimal.Parse(dr["MontantHT"].ToString());
                            if (dr["MontantRemise"] != DBNull.Value)
                                bonCommande.MontantRemise = decimal.Parse(dr["MontantRemise"].ToString());
                            if (dr["MontantRetenuForfaitaire"] != DBNull.Value)
                                bonCommande.MontantRetenuForfaitaire = decimal.Parse(dr["MontantRetenuForfaitaire"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                bonCommande.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["MontantTTC"] != DBNull.Value)
                                bonCommande.MontantTTC = decimal.Parse(dr["MontantTTC"].ToString());
                            if (dr["PoidsTotal"] != DBNull.Value)
                                bonCommande.PoidsTotal = decimal.Parse(dr["PoidsTotal"].ToString());
                            if (dr["NDevis"] != DBNull.Value)
                                bonCommande.NDevis = dr["NDevis"].ToString();
                            if (dr["Reference"] != DBNull.Value)
                                bonCommande.Reference = dr["Reference"].ToString();
                            if (dr["Observation"] != DBNull.Value)
                                bonCommande.Observation = dr["Observation"].ToString();
                            if (dr["PCInsertion"] != DBNull.Value)
                                bonCommande.PCInsertion = dr["PCInsertion"].ToString();
                            if (dr["PCModification"] != DBNull.Value)
                                bonCommande.PCModification = dr["PCModification"].ToString();
                            if (dr["RaisonSociale"] != DBNull.Value)
                                bonCommande.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["BSpecial"] != DBNull.Value)
                                bonCommande.BSpecial = bool.Parse(dr["BSpecial"].ToString());
                            bonCommande.BonCommandeDetailCollection = BonCommandeDetailCollection.Charger(bonCommande.NBonCommande);
                            bonCommande.BonCommandeTaxeCollection = BonCommandeTaxeCollection.Charger(bonCommande.NBonCommande);
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return (bonCommande);
            }
        }
        
        public static BonCommande Charger(string nBonCommande)
        {
            BonCommande bonCommande = null;
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
                    cmd.CommandText = "BonCommande_Charger";
                    cmd.Parameters.AddWithValue("@NBonCommande", nBonCommande);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            bonCommande = new BonCommande();
                            bonCommande.NBonCommande = dr["NBonCommande"].ToString();

                            if (dr["CUnite"] != DBNull.Value)
                                bonCommande.CUnite = dr["CUnite"].ToString();
                            if (dr["BExonoreTVA"] != DBNull.Value)
                                bonCommande.BExonoreTVA = bool.Parse(dr["BExonoreTVA"].ToString());
                            if (dr["BAvanceForfaitaire"] != DBNull.Value)
                                bonCommande.BAvanceForfaitaire = bool.Parse(dr["BAvanceForfaitaire"].ToString());
                            if (dr["BExonoreFodec"] != DBNull.Value)
                                bonCommande.BExonoreFodec = bool.Parse(dr["BExonoreFodec"].ToString());
                            if (dr["CClient"] != DBNull.Value)
                                bonCommande.CClient = dr["CClient"].ToString();
                            if (dr["CreePar"] != DBNull.Value)
                                bonCommande.CreePar = int.Parse(dr["CreePar"].ToString());
                            if (dr["CVendeur"] != DBNull.Value)
                                bonCommande.CVendeur = int.Parse(dr["CVendeur"].ToString());
                            if (dr["DateCommande"] != DBNull.Value)
                                bonCommande.DateCommande = DateTime.Parse(dr["DateCommande"].ToString());
                            if (dr["DateInsertion"] != DBNull.Value)
                                bonCommande.DateInsertion = DateTime.Parse(dr["DateInsertion"].ToString());
                            if (dr["DateLivraison"] != DBNull.Value)
                                bonCommande.DateLivraison = DateTime.Parse(dr["DateLivraison"].ToString());
                            if (dr["DateModification"] != DBNull.Value)
                                bonCommande.DateModification = DateTime.Parse(dr["DateModification"].ToString());
                            if (dr["Etat"] != DBNull.Value)
                                bonCommande.Etat = dr["Etat"].ToString();
                            if (dr["Reference"] != DBNull.Value)
                                bonCommande.Reference = dr["Reference"].ToString();
                            if (dr["Indice"] != DBNull.Value)
                                bonCommande.Indice = int.Parse(dr["Indice"].ToString());
                            if (dr["MatriculeFiscale"] != DBNull.Value)
                                bonCommande.MatriculeFiscale = dr["MatriculeFiscale"].ToString();
                            if (dr["Adresse"] != DBNull.Value)
                                bonCommande.Adresse = dr["Adresse"].ToString();
                            if (dr["NTelephone"] != DBNull.Value)
                                bonCommande.NTelephone = dr["NTelephone"].ToString();
                            if (dr["ModifiePar"] != DBNull.Value)
                                bonCommande.ModifiePar = int.Parse(dr["ModifiePar"].ToString());
                            if (dr["MontantHT"] != DBNull.Value)
                                bonCommande.MontantHT = decimal.Parse(dr["MontantHT"].ToString());
                            if (dr["MontantRemise"] != DBNull.Value)
                                bonCommande.MontantRemise = decimal.Parse(dr["MontantRemise"].ToString());
                            if (dr["MontantRetenuForfaitaire"] != DBNull.Value)
                                bonCommande.MontantRetenuForfaitaire = decimal.Parse(dr["MontantRetenuForfaitaire"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                bonCommande.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["MontantTTC"] != DBNull.Value)
                                bonCommande.MontantTTC = decimal.Parse(dr["MontantTTC"].ToString());
                            if (dr["PoidsTotal"] != DBNull.Value)
                                bonCommande.PoidsTotal = decimal.Parse(dr["PoidsTotal"].ToString());
                            if (dr["NDevis"] != DBNull.Value)
                                bonCommande.NDevis = dr["NDevis"].ToString();
                            if (dr["Reference"] != DBNull.Value)
                                bonCommande.Reference = dr["Reference"].ToString();
                            if (dr["Observation"] != DBNull.Value)
                                bonCommande.Observation = dr["Observation"].ToString();
                            if (dr["PCInsertion"] != DBNull.Value)
                                bonCommande.PCInsertion = dr["PCInsertion"].ToString();
                            if (dr["PCModification"] != DBNull.Value)
                                bonCommande.PCModification = dr["PCModification"].ToString();
                            if (dr["RaisonSociale"] != DBNull.Value)
                                bonCommande.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["BSpecial"] != DBNull.Value)
                                bonCommande.BSpecial = bool.Parse(dr["BSpecial"].ToString());
                            if (dr["NChantier"] != DBNull.Value)
                                bonCommande.NChantier = dr["NChantier"].ToString();
                            bonCommande.BonCommandeDetailCollection = BonCommandeDetailCollection.Charger(bonCommande.NBonCommande);
                            bonCommande.BonCommandeTaxeCollection = BonCommandeTaxeCollection.Charger(bonCommande.NBonCommande);
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return (bonCommande);
            }
        }

        public static BonCommande Chargerparchantier(string nChantier)
        {
            BonCommande bonCommande = null;
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
                    cmd.CommandText = "BonCommande_ChargerparChantier";
                    cmd.Parameters.AddWithValue("@Nchantier", nChantier);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            bonCommande = new BonCommande();
                            bonCommande.NBonCommande = dr["NBonCommande"].ToString();

                            if (dr["CUnite"] != DBNull.Value)
                                bonCommande.CUnite = dr["CUnite"].ToString();
                            if (dr["BExonoreTVA"] != DBNull.Value)
                                bonCommande.BExonoreTVA = bool.Parse(dr["BExonoreTVA"].ToString());
                            if (dr["BAvanceForfaitaire"] != DBNull.Value)
                                bonCommande.BAvanceForfaitaire = bool.Parse(dr["BAvanceForfaitaire"].ToString());
                            if (dr["BExonoreFodec"] != DBNull.Value)
                                bonCommande.BExonoreFodec = bool.Parse(dr["BExonoreFodec"].ToString());
                            if (dr["CClient"] != DBNull.Value)
                                bonCommande.CClient = dr["CClient"].ToString();
                            if (dr["CreePar"] != DBNull.Value)
                                bonCommande.CreePar = int.Parse(dr["CreePar"].ToString());
                            if (dr["CVendeur"] != DBNull.Value)
                                bonCommande.CVendeur = int.Parse(dr["CVendeur"].ToString());
                            if (dr["DateCommande"] != DBNull.Value)
                                bonCommande.DateCommande = DateTime.Parse(dr["DateCommande"].ToString());
                            if (dr["DateInsertion"] != DBNull.Value)
                                bonCommande.DateInsertion = DateTime.Parse(dr["DateInsertion"].ToString());
                            if (dr["DateLivraison"] != DBNull.Value)
                                bonCommande.DateLivraison = DateTime.Parse(dr["DateLivraison"].ToString());
                            if (dr["DateModification"] != DBNull.Value)
                                bonCommande.DateModification = DateTime.Parse(dr["DateModification"].ToString());
                            if (dr["Etat"] != DBNull.Value)
                                bonCommande.Etat = dr["Etat"].ToString();
                            if (dr["Reference"] != DBNull.Value)
                                bonCommande.Reference = dr["Reference"].ToString();
                            if (dr["Indice"] != DBNull.Value)
                                bonCommande.Indice = int.Parse(dr["Indice"].ToString());
                            if (dr["MatriculeFiscale"] != DBNull.Value)
                                bonCommande.MatriculeFiscale = dr["MatriculeFiscale"].ToString();
                            if (dr["Adresse"] != DBNull.Value)
                                bonCommande.Adresse = dr["Adresse"].ToString();
                            if (dr["NTelephone"] != DBNull.Value)
                                bonCommande.NTelephone = dr["NTelephone"].ToString();
                            if (dr["ModifiePar"] != DBNull.Value)
                                bonCommande.ModifiePar = int.Parse(dr["ModifiePar"].ToString());
                            if (dr["MontantHT"] != DBNull.Value)
                                bonCommande.MontantHT = decimal.Parse(dr["MontantHT"].ToString());
                            if (dr["MontantRemise"] != DBNull.Value)
                                bonCommande.MontantRemise = decimal.Parse(dr["MontantRemise"].ToString());
                            if (dr["MontantRetenuForfaitaire"] != DBNull.Value)
                                bonCommande.MontantRetenuForfaitaire = decimal.Parse(dr["MontantRetenuForfaitaire"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                bonCommande.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["MontantTTC"] != DBNull.Value)
                                bonCommande.MontantTTC = decimal.Parse(dr["MontantTTC"].ToString());
                            if (dr["PoidsTotal"] != DBNull.Value)
                                bonCommande.PoidsTotal = decimal.Parse(dr["PoidsTotal"].ToString());
                            if (dr["NDevis"] != DBNull.Value)
                                bonCommande.NDevis = dr["NDevis"].ToString();
                            if (dr["Reference"] != DBNull.Value)
                                bonCommande.Reference = dr["Reference"].ToString();
                            if (dr["Observation"] != DBNull.Value)
                                bonCommande.Observation = dr["Observation"].ToString();
                            if (dr["PCInsertion"] != DBNull.Value)
                                bonCommande.PCInsertion = dr["PCInsertion"].ToString();
                            if (dr["PCModification"] != DBNull.Value)
                                bonCommande.PCModification = dr["PCModification"].ToString();
                            if (dr["RaisonSociale"] != DBNull.Value)
                                bonCommande.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["BSpecial"] != DBNull.Value)
                                bonCommande.BSpecial = bool.Parse(dr["BSpecial"].ToString());
                            if (dr["NChantier"] != DBNull.Value)
                                bonCommande.NChantier = dr["NChantier"].ToString();
                            bonCommande.BonCommandeDetailCollection = BonCommandeDetailCollection.Charger(bonCommande.NBonCommande);
                            bonCommande.BonCommandeTaxeCollection = BonCommandeTaxeCollection.Charger(bonCommande.NBonCommande);
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return (bonCommande);
            }
        }

        private void RestituerStockReserver(SqlTransaction transaction)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Transaction = transaction;
            cmd.Connection = transaction.Connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "BonCommandeDetail_RestituerStockReserver";
            cmd.Parameters.AddWithValue("@NBonCommande", this.NBonCommande);

            foreach (SqlParameter parametre in cmd.Parameters)
                if (parametre.Value == null)
                    parametre.Value = DBNull.Value;

            cmd.ExecuteNonQuery();
        }

        //haithem

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
                    cmd.CommandText = "update BonCommande set NChantier = '" + this.NChantier + "' where NBonCommande = '" + this.NBonCommande + "'";
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

    public class BonCommandeCollection : List<BonCommande>
    {
        public BonCommandeCollection()
        {
        }

        public static BonCommandeCollection Charger(string nBonCommande)
        {
            BonCommandeCollection collection = new BonCommandeCollection();
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
                    cmd.CommandText = "BonCommande_Charger";
                    cmd.Parameters.AddWithValue("@NBonCommande", nBonCommande);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            BonCommande bonCommande = new BonCommande();
                            bonCommande.NBonCommande = dr["NBonCommande"].ToString();

                            if (dr["CUnite"] != DBNull.Value)
                                bonCommande.CUnite = dr["CUnite"].ToString();
                            if (dr["BExonoreTVA"] != DBNull.Value)
                                bonCommande.BExonoreTVA = bool.Parse(dr["BExonereTVA"].ToString());
                            if (dr["BAvanceForfaitaire"] != DBNull.Value)
                                bonCommande.BAvanceForfaitaire = bool.Parse(dr["BAvanceForfaitaire"].ToString());
                            if (dr["BExonoreFodec"] != DBNull.Value)
                                bonCommande.BExonoreFodec = bool.Parse(dr["BExonereFodec"].ToString());
                            if (dr["CClient"] != DBNull.Value)
                                bonCommande.CClient = dr["CClient"].ToString();
                            if (dr["CreePar"] != DBNull.Value)
                                bonCommande.CreePar = int.Parse(dr["CreePar"].ToString());
                            if (dr["CVendeur"] != DBNull.Value)
                                bonCommande.CVendeur = int.Parse(dr["CVendeur"].ToString());
                            if (dr["DateCommande"] != DBNull.Value)
                                bonCommande.DateCommande = DateTime.Parse(dr["DateCommande"].ToString());
                            if (dr["DateInsertion"] != DBNull.Value)
                                bonCommande.DateInsertion = DateTime.Parse(dr["DateInsertion"].ToString());
                            if (dr["DateLivraison"] != DBNull.Value)
                                bonCommande.DateLivraison = DateTime.Parse(dr["DateLivraison"].ToString());
                            if (dr["DateModification"] != DBNull.Value)
                                bonCommande.DateModification = DateTime.Parse(dr["DateModification"].ToString());
                            if (dr["Etat"] != DBNull.Value)
                                bonCommande.Etat = dr["Etat"].ToString();
                            if (dr["Indice"] != DBNull.Value)
                                bonCommande.Indice = int.Parse(dr["Indice"].ToString());
                            if (dr["MatriculeFiscale"] != DBNull.Value)
                                bonCommande.MatriculeFiscale = dr["MatriculeFiscale"].ToString();
                            if (dr["Adresse"] != DBNull.Value)
                                bonCommande.Adresse = dr["Adresse"].ToString();
                            if (dr["NTelephone"] != DBNull.Value)
                                bonCommande.NTelephone = dr["NTelephone"].ToString();
                            if (dr["ModifiePar"] != DBNull.Value)
                                bonCommande.ModifiePar = int.Parse(dr["ModifiePar"].ToString());
                            if (dr["MontantHT"] != DBNull.Value)
                                bonCommande.MontantHT = decimal.Parse(dr["MontantHT"].ToString());
                            if (dr["MontantRemise"] != DBNull.Value)
                                bonCommande.MontantRemise = decimal.Parse(dr["MontantRemise"].ToString());
                            if (dr["MontantRetenuForfaitaire"] != DBNull.Value)
                                bonCommande.MontantRetenuForfaitaire = decimal.Parse(dr["MontantRetenuForfaitaire"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                bonCommande.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["MontantTTC"] != DBNull.Value)
                                bonCommande.MontantTTC = decimal.Parse(dr["MontantTTC"].ToString());
                            if (dr["PoidsTotal"] != DBNull.Value)
                                bonCommande.PoidsTotal = decimal.Parse(dr["PoidsTotal"].ToString());
                            if (dr["NDevis"] != DBNull.Value)
                                bonCommande.NDevis = dr["NDevis"].ToString();
                            if (dr["Observation"] != DBNull.Value)
                                bonCommande.Observation = dr["Observation"].ToString();
                            if (dr["PCInsertion"] != DBNull.Value)
                                bonCommande.PCInsertion = dr["PCInsertion"].ToString();
                            if (dr["PCModification"] != DBNull.Value)
                                bonCommande.PCModification = dr["PCModification"].ToString();
                            if (dr["RaisonSociale"] != DBNull.Value)
                                bonCommande.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["Reference"] != DBNull.Value)
                                bonCommande.Reference = dr["Reference"].ToString();
                            if (dr["BSpecial"] != DBNull.Value)
                                bonCommande.BSpecial = bool.Parse(dr["BSpecial"].ToString());
                            bonCommande.BonCommandeDetailCollection = BonCommandeDetailCollection.Charger(bonCommande.NBonCommande);
                            bonCommande.BonCommandeTaxeCollection = BonCommandeTaxeCollection.Charger(bonCommande.NBonCommande);
                            collection.Add(bonCommande);
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
        
        public static BonCommandeCollection MobileCharger(string CEquipe, string dd, string df)
        {
            BonCommandeCollection collection = new BonCommandeCollection();
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
                    cmd.CommandText = "Mobile_listebonlibre_charger";
                    cmd.Parameters.AddWithValue("@dd", dd);
                    cmd.Parameters.AddWithValue("@df", df);
                    cmd.Parameters.AddWithValue("@CEquipe", CEquipe);


                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            BonCommande bonCommande = new BonCommande();
                            bonCommande.NBonCommande = dr["NBonCommande"].ToString();

                            if (dr["CUnite"] != DBNull.Value)
                                bonCommande.CUnite = dr["CUnite"].ToString();
                            if (dr["DateCommande"] != DBNull.Value)
                                bonCommande.DateCommande = DateTime.Parse(dr["DateCommande"].ToString());
                            if (dr["NTelephone"] != DBNull.Value)
                                bonCommande.NTelephone = dr["NTelephone"].ToString();
                            if (dr["Adresse"] != DBNull.Value)
                                bonCommande.Adresse = dr["Adresse"].ToString();
                            if (dr["RaisonSociale"] != DBNull.Value)
                                bonCommande.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["DateCommande"] != DBNull.Value)
                                bonCommande.DateBC = String.Format("{0:dd/MM/yyyy}", dr["DateCommande"]);
                            if (dr["CClient"] != DBNull.Value)
                                bonCommande.CClient = dr["CClient"].ToString();
                            if (dr["Etat"] != DBNull.Value)
                                bonCommande.Etat = dr["Etat"].ToString();
                            else
                                bonCommande.Etat = "";

                            bonCommande.CEquipe = dr["CEquipe"] == DBNull.Value ? "" : dr["CEquipe"].ToString();
                            bonCommande.LibTBAchat = dr["LibTBAchat"] == DBNull.Value ? "" : dr["LibTBAchat"].ToString();
                            bonCommande.CModeReglement = dr["CModeReglement"] == DBNull.Value ? "" : dr["CModeReglement"].ToString();

                            /*                
                            if (dr["BExonoreTVA"] != DBNull.Value)
                                bonCommande.BExonoreTVA = bool.Parse(dr["BExonereTVA"].ToString());
                            if (dr["BAvanceForfaitaire"] != DBNull.Value)
                                bonCommande.BAvanceForfaitaire = bool.Parse(dr["BAvanceForfaitaire"].ToString());
                            if (dr["BExonoreFodec"] != DBNull.Value)
                                bonCommande.BExonoreFodec = bool.Parse(dr["BExonereFodec"].ToString());
                            if (dr["CClient"] != DBNull.Value)
                                bonCommande.CClient = dr["CClient"].ToString();
                            if (dr["CreePar"] != DBNull.Value)
                                bonCommande.CreePar = int.Parse(dr["CreePar"].ToString());
                            if (dr["CVendeur"] != DBNull.Value)
                                bonCommande.CVendeur = int.Parse(dr["CVendeur"].ToString());
                            if (dr["DateCommande"] != DBNull.Value)
                                bonCommande.DateCommande = DateTime.Parse(dr["DateCommande"].ToString());
                            if (dr["DateInsertion"] != DBNull.Value)
                                bonCommande.DateInsertion = DateTime.Parse(dr["DateInsertion"].ToString());
                            if (dr["DateLivraison"] != DBNull.Value)
                                bonCommande.DateLivraison = DateTime.Parse(dr["DateLivraison"].ToString());
                            if (dr["DateModification"] != DBNull.Value)
                                bonCommande.DateModification = DateTime.Parse(dr["DateModification"].ToString());
                            if (dr["Etat"] != DBNull.Value)
                                bonCommande.Etat = dr["Etat"].ToString();
                            if (dr["Indice"] != DBNull.Value)
                                bonCommande.Indice = int.Parse(dr["Indice"].ToString());
                            if (dr["MatriculeFiscale"] != DBNull.Value)
                                bonCommande.MatriculeFiscale = dr["MatriculeFiscale"].ToString();
                            if (dr["Adresse"] != DBNull.Value)
                                bonCommande.Adresse = dr["Adresse"].ToString();
                            if (dr["NTelephone"] != DBNull.Value)
                                bonCommande.NTelephone = dr["NTelephone"].ToString();
                            if (dr["ModifiePar"] != DBNull.Value)
                                bonCommande.ModifiePar = int.Parse(dr["ModifiePar"].ToString());
                            if (dr["MontantHT"] != DBNull.Value)
                                bonCommande.MontantHT = decimal.Parse(dr["MontantHT"].ToString());
                            if (dr["MontantRemise"] != DBNull.Value)
                                bonCommande.MontantRemise = decimal.Parse(dr["MontantRemise"].ToString());
                            if (dr["MontantRetenuForfaitaire"] != DBNull.Value)
                                bonCommande.MontantRetenuForfaitaire = decimal.Parse(dr["MontantRetenuForfaitaire"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                bonCommande.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["MontantTTC"] != DBNull.Value)
                                bonCommande.MontantTTC = decimal.Parse(dr["MontantTTC"].ToString());
                            if (dr["PoidsTotal"] != DBNull.Value)
                                bonCommande.PoidsTotal = decimal.Parse(dr["PoidsTotal"].ToString());
                            if (dr["NDevis"] != DBNull.Value)
                                bonCommande.NDevis = dr["NDevis"].ToString();
                            if (dr["Observation"] != DBNull.Value)
                                bonCommande.Observation = dr["Observation"].ToString();
                            if (dr["PCInsertion"] != DBNull.Value)
                                bonCommande.PCInsertion = dr["PCInsertion"].ToString();
                            if (dr["PCModification"] != DBNull.Value)
                                bonCommande.PCModification = dr["PCModification"].ToString();
                            if (dr["RaisonSociale"] != DBNull.Value)
                                bonCommande.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["Reference"] != DBNull.Value)
                                bonCommande.Reference = dr["Reference"].ToString();
                            if (dr["BSpecial"] != DBNull.Value)
                                bonCommande.BSpecial = bool.Parse(dr["BSpecial"].ToString());
                            if (dr["BExonoreTPE"] != DBNull.Value)
                                bonCommande.BExonoreTPE = bool.Parse(dr["BExonoreTPE"].ToString());
                            if (dr["BExonoreTDC"] != DBNull.Value)
                                bonCommande.BExonoreTDC = bool.Parse(dr["BExonoreTDC"].ToString());
                            if (dr["CEtablissement"] != DBNull.Value)
                                bonCommande.CEtablissement = dr["CEtablissement"].ToString();
                            bonCommande.BonCommandeDetailCollection = BonCommandeDetailCollection.Charger(bonCommande.NBonCommande);
                            bonCommande.BonCommandeTaxeCollection = BonCommandeTaxeCollection.Charger(bonCommande.NBonCommande);    */
                            collection.Add(bonCommande);
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
        
        public static BonCommandeCollection ChargerparChantier(string nChantier)
        {
            BonCommandeCollection collection = new BonCommandeCollection();
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
                    cmd.CommandText = "BonCommande_ChargerparChantier";
                    cmd.Parameters.AddWithValue("@NChantier", nChantier);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            BonCommande bonCommande = new BonCommande();
                            bonCommande.NBonCommande = dr["NBonCommande"].ToString();

                            if (dr["CUnite"] != DBNull.Value)
                                bonCommande.CUnite = dr["CUnite"].ToString();
                            if (dr["BExonoreTVA"] != DBNull.Value)
                                bonCommande.BExonoreTVA = bool.Parse(dr["BExonoreTVA"].ToString());
                            if (dr["BAvanceForfaitaire"] != DBNull.Value)
                                bonCommande.BAvanceForfaitaire = bool.Parse(dr["BAvanceForfaitaire"].ToString());
                            if (dr["BExonoreFodec"] != DBNull.Value)
                                bonCommande.BExonoreFodec = bool.Parse(dr["BExonoreFodec"].ToString());
                            if (dr["CClient"] != DBNull.Value)
                                bonCommande.CClient = dr["CClient"].ToString();
                            if (dr["CreePar"] != DBNull.Value)
                                bonCommande.CreePar = int.Parse(dr["CreePar"].ToString());
                            if (dr["CVendeur"] != DBNull.Value)
                                bonCommande.CVendeur = int.Parse(dr["CVendeur"].ToString());
                            if (dr["DateCommande"] != DBNull.Value)
                                bonCommande.DateCommande = DateTime.Parse(dr["DateCommande"].ToString());
                            if (dr["DateInsertion"] != DBNull.Value)
                                bonCommande.DateInsertion = DateTime.Parse(dr["DateInsertion"].ToString());
                            if (dr["DateLivraison"] != DBNull.Value)
                                bonCommande.DateLivraison = DateTime.Parse(dr["DateLivraison"].ToString());
                            if (dr["DateModification"] != DBNull.Value)
                                bonCommande.DateModification = DateTime.Parse(dr["DateModification"].ToString());
                            if (dr["Etat"] != DBNull.Value)
                                bonCommande.Etat = dr["Etat"].ToString();
                            if (dr["Indice"] != DBNull.Value)
                                bonCommande.Indice = int.Parse(dr["Indice"].ToString());
                            if (dr["MatriculeFiscale"] != DBNull.Value)
                                bonCommande.MatriculeFiscale = dr["MatriculeFiscale"].ToString();
                            if (dr["Adresse"] != DBNull.Value)
                                bonCommande.Adresse = dr["Adresse"].ToString();
                            if (dr["NTelephone"] != DBNull.Value)
                                bonCommande.NTelephone = dr["NTelephone"].ToString();
                            if (dr["ModifiePar"] != DBNull.Value)
                                bonCommande.ModifiePar = int.Parse(dr["ModifiePar"].ToString());
                            if (dr["MontantHT"] != DBNull.Value)
                                bonCommande.MontantHT = decimal.Parse(dr["MontantHT"].ToString());
                            if (dr["MontantRemise"] != DBNull.Value)
                                bonCommande.MontantRemise = decimal.Parse(dr["MontantRemise"].ToString());
                            if (dr["MontantRetenuForfaitaire"] != DBNull.Value)
                                bonCommande.MontantRetenuForfaitaire = decimal.Parse(dr["MontantRetenuForfaitaire"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                bonCommande.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["MontantTTC"] != DBNull.Value)
                                bonCommande.MontantTTC = decimal.Parse(dr["MontantTTC"].ToString());
                            if (dr["PoidsTotal"] != DBNull.Value)
                                bonCommande.PoidsTotal = decimal.Parse(dr["PoidsTotal"].ToString());
                            if (dr["NDevis"] != DBNull.Value)
                                bonCommande.NDevis = dr["NDevis"].ToString();
                            if (dr["Observation"] != DBNull.Value)
                                bonCommande.Observation = dr["Observation"].ToString();
                            if (dr["PCInsertion"] != DBNull.Value)
                                bonCommande.PCInsertion = dr["PCInsertion"].ToString();
                            if (dr["PCModification"] != DBNull.Value)
                                bonCommande.PCModification = dr["PCModification"].ToString();
                            if (dr["RaisonSociale"] != DBNull.Value)
                                bonCommande.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["Reference"] != DBNull.Value)
                                bonCommande.Reference = dr["Reference"].ToString();
                            if (dr["BSpecial"] != DBNull.Value)
                                bonCommande.BSpecial = bool.Parse(dr["BSpecial"].ToString());
                            if (dr["NChantier"] != DBNull.Value)
                                bonCommande.NChantier = dr["NChantier"].ToString();
                            
                            bonCommande.BonCommandeDetailCollection = BonCommandeDetailCollection.Charger(bonCommande.NBonCommande);
                            bonCommande.BonCommandeTaxeCollection = BonCommandeTaxeCollection.Charger(bonCommande.NBonCommande);
                            collection.Add(bonCommande);
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