using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace CST.LePoint.Vente.Metier
{
    public class OrdrePreparation
    {
        #region Proriétès

        [XmlAttribute("NOrdrePreparation")]
        [Bindable(true)]
        public string NOrdrePreparation { get; set; }

        [XmlAttribute("CClient")]
        [Bindable(true)]
        public string CClient { get; set; }

        [XmlAttribute("DatePreparation")]
        [Bindable(true)]
        public DateTime? DatePreparation { get; set; }

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

        [XmlAttribute("CPreparateur")]
        [Bindable(true)]
        public int CPreparateur { get; set; }

        [XmlAttribute("BExonereFodec")]
        [Bindable(true)]
        public bool BExonoreFodec { get; set; }

        [XmlAttribute("BExonoreTVA")]
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

        [XmlAttribute("NBonCommande")]
        [Bindable(true)]
        public string NBonCommande { get; set; }

        [XmlAttribute("Observation")]
        [Bindable(true)]
        public string Observation { get; set; }

        [XmlAttribute("RaisonSociale")]
        [Bindable(true)]
        public string RaisonSociale { get; set; }

        [XmlAttribute("Indice")]
        [Bindable(true)]
        public int Indice { get; set; }

        [XmlAttribute("BLivre")]
        [Bindable(true)]
        public bool BLivre { get; set; }


        [XmlAttribute("BAnnuler")]
        [Bindable(true)]
        public bool BAnnuler { get; set; }

        [XmlAttribute("CChauffeur")]
        [Bindable(true)]
        public String CChauffeur { get; set; }

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

        public OrdrePreparationDetailCollection OrdrePreparationDetailCollection;
        public OrdrePreparationTaxeCollection OrdrePreparationTaxeCollection;

        #endregion Proriétès

        public OrdrePreparation()
        {
            this.OrdrePreparationDetailCollection = new OrdrePreparationDetailCollection();
            this.OrdrePreparationTaxeCollection = new OrdrePreparationTaxeCollection();
        }

        public OrdrePreparation(string NOrdrePreparation)
        {
            this.NOrdrePreparation = NOrdrePreparation;
            this.OrdrePreparationDetailCollection = new OrdrePreparationDetailCollection();
            this.OrdrePreparationTaxeCollection = new OrdrePreparationTaxeCollection();
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
                cmd.CommandText = "OrdrePreparation_Inserer";
                cmd.Parameters.AddWithValue("@CClient", this.CClient);
                cmd.Parameters.AddWithValue("@DatePreparation ", this.DatePreparation);
                cmd.Parameters.AddWithValue("@CUnite", this.CUnite);

                cmd.Parameters.AddWithValue("@MatriculeFiscale", this.MatriculeFiscale);
                cmd.Parameters.AddWithValue("@Adresse", this.Adresse);
                cmd.Parameters.AddWithValue("@NTelephone", this.NTelephone);
                cmd.Parameters.AddWithValue("@CPreparateur ", this.CPreparateur);
                cmd.Parameters.AddWithValue("@BExonoreFodec", this.BExonoreFodec);
                cmd.Parameters.AddWithValue("@BExonoreTVA", this.BExonoreTVA);
                cmd.Parameters.AddWithValue("@BAvanceForfaitaire", this.BAvanceForfaitaire);

                cmd.Parameters.AddWithValue("@MontantHT", this.MontantHT);
                cmd.Parameters.AddWithValue("@MontantRemise", this.MontantRemise);
                cmd.Parameters.AddWithValue("@MontantTaxe", this.MontantTaxe);
                cmd.Parameters.AddWithValue("@MontantTTC", this.MontantTTC);
                cmd.Parameters.AddWithValue("@PoidsTotal", this.PoidsTotal);
                cmd.Parameters.AddWithValue("@NBonCommande", this.NBonCommande);

                cmd.Parameters.AddWithValue("@Observation", this.Observation);
                cmd.Parameters.AddWithValue("@RaisonSociale", this.RaisonSociale);
                cmd.Parameters.AddWithValue("@CChauffeur", this.CChauffeur);
                cmd.Parameters.AddWithValue("@CVehicule", this.CVehicule);
                cmd.Parameters.AddWithValue("@CMission", this.CMission);
                cmd.Parameters.AddWithValue("@MontantRetenuForfaitaire", this.MontantRetenuForfaitaire);
                cmd.Parameters.AddWithValue("@BLivre", false);

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
                        this.NOrdrePreparation = dr["NOrdrePreparation"].ToString();
                        this.Indice = int.Parse(dr["DernierIndice"].ToString());
                    }
                }
                int i = 1;
                decimal sommeQuantitePreparee = 0;
                decimal sommeQuantiteCommandee = 0;
                BonCommande bonCommande = BonCommande.Charger(this.NBonCommande);
                foreach (OrdrePreparationDetail ordrePreparationDetail in OrdrePreparationDetailCollection)
                {
                    if (!string.IsNullOrEmpty(this.NBonCommande))
                    {
                        BonCommandeDetail detailCommande = bonCommande.BonCommandeDetailCollection.RecupererBonCommandeDetail(this.NBonCommande, ordrePreparationDetail.CArticle, ordrePreparationDetail.OrdreBonCommande);
                        if (detailCommande != null)
                        {
                            detailCommande.QuantitePreparee = detailCommande.QuantitePreparee + ordrePreparationDetail.Quantite;
                            sommeQuantitePreparee = sommeQuantitePreparee + detailCommande.QuantitePreparee;
                            sommeQuantiteCommandee = sommeQuantiteCommandee + detailCommande.QuantiteHistorique;
                            detailCommande.Modifier(transaction);
                            bonCommande.BonCommandeDetailCollection.Remove(detailCommande);
                        }
                    }
                    ordrePreparationDetail.NOrdrePreparation = this.NOrdrePreparation;
                    ordrePreparationDetail.Ordre = i++;
                    ordrePreparationDetail.Sauvegarder(transaction);
                }
                while (bonCommande.BonCommandeDetailCollection.Count != 0)
                {
                    BonCommandeDetail detail = bonCommande.BonCommandeDetailCollection[0];
                    sommeQuantiteCommandee = sommeQuantiteCommandee + detail.Quantite;
                    bonCommande.BonCommandeDetailCollection.Remove(detail);
                }
                if (bonCommande.Etat != VenteHelper.EtatBonCommande.ENCOURS.ToString())
                {
                    if (sommeQuantitePreparee == sommeQuantiteCommandee)
                        BonCommande.ModifierEtatBonCommande(this.NBonCommande, VenteHelper.EtatBonCommande.PREPARE.ToString(), transaction);
                    else
                        BonCommande.ModifierEtatBonCommande(this.NBonCommande, VenteHelper.EtatBonCommande.ENPREPARATION.ToString(), transaction);
                }
                foreach (OrdrePreparationTaxe ordrePreparationTaxe in OrdrePreparationTaxeCollection)
                {
                    ordrePreparationTaxe.NOrdrePreparation = this.NOrdrePreparation;
                    ordrePreparationTaxe.Sauvegarder(transaction);
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
        public void LivrerOrdre(SqlTransaction transaction)
        {

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE OrdrePreparation set BLivre=1 where NOrdrePreparation='" + this.NOrdrePreparation + "'";


                cmd.ExecuteNonQuery();
            }

            catch (Exception)
            {
                throw;
            }
        }
        public void AnnulerOrdre(SqlTransaction transaction)
        {

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE OrdrePreparation set BAnnuler=1 where NOrdrePreparation='" + this.NOrdrePreparation + "'";


                cmd.ExecuteNonQuery();
            }

            catch (Exception)
            {
                throw;
            }
        }
        public void Modifier(SqlTransaction transaction)
        {
            OrdrePreparation ancieNOrdrePreparation = OrdrePreparation.Charger(this.NOrdrePreparation);
            BonCommande bonCommande = new BonCommande();
            if (!string.IsNullOrEmpty(this.NBonCommande))
            {
                bonCommande = BonCommande.Charger(this.NBonCommande);
                if (bonCommande != null)

                    this.RestituerQuantitePreparee(transaction);
            }
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "OrdrePreparation_Modifier";
                cmd.Parameters.AddWithValue("@NOrdrePreparation", this.NOrdrePreparation);
                cmd.Parameters.AddWithValue("@CClient", this.CClient);
                cmd.Parameters.AddWithValue("@DatePreparation ", this.DatePreparation);
                cmd.Parameters.AddWithValue("@CUnite", this.CUnite);

                cmd.Parameters.AddWithValue("@MatriculeFiscale", this.MatriculeFiscale);
                cmd.Parameters.AddWithValue("@Adresse", this.Adresse);
                cmd.Parameters.AddWithValue("@NTelephone", this.NTelephone);
                cmd.Parameters.AddWithValue("@CPreparateur ", this.CPreparateur);
                cmd.Parameters.AddWithValue("@BExonoreFodec", this.BExonoreFodec);
                cmd.Parameters.AddWithValue("@BExonoreTVA", this.BExonoreTVA);
                cmd.Parameters.AddWithValue("@BAvanceForfaitaire", this.BAvanceForfaitaire);

                cmd.Parameters.AddWithValue("@MontantHT", this.MontantHT);
                cmd.Parameters.AddWithValue("@MontantRemise", this.MontantRemise);
                cmd.Parameters.AddWithValue("@MontantTaxe", this.MontantTaxe);
                cmd.Parameters.AddWithValue("@MontantTTC", this.MontantTTC);
                cmd.Parameters.AddWithValue("@PoidsTotal", this.PoidsTotal);
                cmd.Parameters.AddWithValue("@NBonCommande", this.NBonCommande);

                cmd.Parameters.AddWithValue("@Observation", this.Observation);
                cmd.Parameters.AddWithValue("@RaisonSociale", this.RaisonSociale);

                cmd.Parameters.AddWithValue("@CChauffeur", this.CChauffeur);
                cmd.Parameters.AddWithValue("@CVehicule", this.CVehicule);
                cmd.Parameters.AddWithValue("@CMission", this.CMission);
                cmd.Parameters.AddWithValue("@MontantRetenuForfaitaire", this.MontantRetenuForfaitaire);
                cmd.Parameters.AddWithValue("@BLivre", this.BLivre);

                cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                cmd.Parameters.AddWithValue("@PCModification", this.PCModification);
                cmd.Parameters.AddWithValue("@ModifiePar", this.ModifiePar);
                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                cmd.ExecuteNonQuery();
                int i = 1;
                decimal sommeQuantitePreparee = 0;
                decimal sommeQuantiteCommandee = 0;
                this.SupprimerDetailOrdrePreparationAnterieurs(transaction);
                bonCommande = BonCommande.Charger(this.NBonCommande);
                foreach (OrdrePreparationDetail ordrePreparationDetail in OrdrePreparationDetailCollection)
                {
                    sommeQuantitePreparee = sommeQuantitePreparee + ordrePreparationDetail.Quantite;
                    if (!string.IsNullOrEmpty(this.NBonCommande))
                    {
                        BonCommandeDetail detailCommande = bonCommande.BonCommandeDetailCollection.RecupererBonCommandeDetail(this.NBonCommande, ordrePreparationDetail.CArticle, ordrePreparationDetail.OrdreBonCommande);
                        if (detailCommande != null)
                        {
                            detailCommande.QuantitePreparee = detailCommande.QuantitePreparee + ordrePreparationDetail.Quantite;
                            sommeQuantiteCommandee = sommeQuantiteCommandee + detailCommande.Quantite;
                            detailCommande.Modifier(transaction);
                            bonCommande.BonCommandeDetailCollection.Remove(detailCommande);
                        }
                    }

                    ordrePreparationDetail.NOrdrePreparation = this.NOrdrePreparation;
                    ordrePreparationDetail.Ordre = i++;
                    ordrePreparationDetail.Sauvegarder(transaction);
                }
                while (bonCommande.BonCommandeDetailCollection.Count != 0)
                {
                    BonCommandeDetail detail = bonCommande.BonCommandeDetailCollection[0];
                    sommeQuantiteCommandee = sommeQuantiteCommandee + detail.Quantite;
                    bonCommande.BonCommandeDetailCollection.Remove(detail);
                }
                if (sommeQuantitePreparee == sommeQuantiteCommandee)
                    BonCommande.ModifierEtatBonCommande(this.NBonCommande, VenteHelper.EtatBonCommande.PREPARE.ToString(), transaction);
                else
                    BonCommande.ModifierEtatBonCommande(this.NBonCommande, VenteHelper.EtatBonCommande.ENPREPARATION.ToString(), transaction);

                this.SupprimerTaxeOrdrePreparationAnterieurs(transaction);
                foreach (OrdrePreparationTaxe ordrePreparationTaxe in OrdrePreparationTaxeCollection)
                {
                    ordrePreparationTaxe.NOrdrePreparation = this.NOrdrePreparation;
                    ordrePreparationTaxe.Sauvegarder(transaction);
                }
            }

            catch (Exception)
            {
                throw;
            }
        }

        private void SupprimerDetailOrdrePreparationAnterieurs(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "OrdrePreparation_SupprimerDetails";

                cmd.Parameters.AddWithValue("@NOrdrePreparation", this.NOrdrePreparation);

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

        private void SupprimerTaxeOrdrePreparationAnterieurs(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "OrdrePreparation_SupprimerTaxes";

                cmd.Parameters.AddWithValue("@NOrdrePreparation", this.NOrdrePreparation);

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

        public static string RecupererNumeroOrdrePreparation(string exercice, out int indice)
        {
            string NOrdrePreparation = string.Empty;
            indice = 0;

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = cn;

                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.CommandText = "OrdrePreparation_RecupererNouveauNumero";
                cmd2.Parameters.AddWithValue("@Exercice", exercice);
                SqlDataReader dr = cmd2.ExecuteReader();
                if (dr.Read())
                {
                    NOrdrePreparation = dr["NOrdrePreparation"].ToString();
                    indice = int.Parse(dr["DernierIndice"].ToString());
                }

                dr.Close();
            }

            return NOrdrePreparation;
        }

        public static string RecupererNumeroOrdrePreparation(string exercice)
        {
            int indice = 0;
            return OrdrePreparation.RecupererNumeroOrdrePreparation(exercice, out indice);
        }

        //public void RestituerStock(SqlTransaction transaction)
        //{
        //    OrdrePreparationDetailCollection AncienneBLDetailCollection = new OrdrePreparationDetailCollection();
        //    OrdrePreparationDetail OrdrePreparationDetail = null;

        //    try
        //    {
        //        SqlCommand cmdOrdrePreparation = new SqlCommand();
        //        cmdOrdrePreparation.Transaction = transaction;
        //        cmdOrdrePreparation.Connection = transaction.Connection;
        //        cmdOrdrePreparation.CommandType = CommandType.StoredProcedure;
        //        cmdOrdrePreparation.CommandText = "OrdrePreparationDetail_Charger";
        //        cmdOrdrePreparation.Parameters.AddWithValue("@NOrdrePreparation", NOrdrePreparation);
        //        cmdOrdrePreparation.Parameters.AddWithValue("@CArticle", DBNull.Value);
        //        cmdOrdrePreparation.Parameters.AddWithValue("@Ordre", DBNull.Value);
        //        foreach (SqlParameter parametre in cmdOrdrePreparation.Parameters)
        //        {
        //            if (parametre.Value == null)
        //            {
        //                parametre.Value = DBNull.Value;
        //            }
        //        }

        //        using (SqlDataReader dr = cmdOrdrePreparation.ExecuteReader())
        //        {
        //            while (dr.Read())
        //            {
        //                OrdrePreparationDetail = new OrdrePreparationDetail(NOrdrePreparation);
        //                OrdrePreparationDetail.NOrdrePreparation = NOrdrePreparation;
        //                OrdrePreparationDetail.CArticle = dr["CArticle"].ToString();
        //                OrdrePreparationDetail.Ordre = int.Parse(dr["Ordre"].ToString());

        //                if (dr["CUnite"] != DBNull.Value)
        //                    OrdrePreparationDetail.CUnite = dr["CUnite"].ToString();
        //                if (dr["CUnite"] != DBNull.Value)
        //                    OrdrePreparationDetail.CUnite = dr["CUnite"].ToString();
        //                if (dr["LibArticle"] != DBNull.Value)
        //                    OrdrePreparationDetail.LibArticle = dr["LibArticle"].ToString();
        //                if (dr["MontantTaxe"] != DBNull.Value)
        //                    OrdrePreparationDetail.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
        //                if (dr["PourcentageFodec"] != DBNull.Value)
        //                    OrdrePreparationDetail.PourcentageFodec = decimal.Parse(dr["PourcentageFodec"].ToString());
        //                if (dr["PourcentageRemise"] != DBNull.Value)
        //                    OrdrePreparationDetail.PourcentageRemise = decimal.Parse(dr["PourcentageRemise"].ToString());
        //                if (dr["PrixHT"] != DBNull.Value)
        //                    OrdrePreparationDetail.PrixHT = decimal.Parse(dr["PrixHT"].ToString());
        //                if (dr["PrixRevient"] != DBNull.Value)
        //                    OrdrePreparationDetail.PrixRevient = decimal.Parse(dr["PrixRevient"].ToString());
        //                if (dr["Quantite"] != DBNull.Value)
        //                    OrdrePreparationDetail.Quantite = decimal.Parse(dr["Quantite"].ToString());
        //                if (dr["QuantiteHistorique"] != DBNull.Value)
        //                    OrdrePreparationDetail.QuantiteHistorique = decimal.Parse(dr["QuantiteHistorique"].ToString());
        //                if (dr["TauxTVA"] != DBNull.Value)
        //                    OrdrePreparationDetail.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());
        //                if (dr["CTaxe"] != DBNull.Value)
        //                    OrdrePreparationDetail.CTaxe = dr["CTaxe"].ToString();
        //                if (dr["Remise1"] != DBNull.Value)
        //                    OrdrePreparationDetail.Remise1 = decimal.Parse(dr["Remise1"].ToString());
        //                if (dr["Remise2"] != DBNull.Value)
        //                    OrdrePreparationDetail.Remise2 = decimal.Parse(dr["Remise2"].ToString());
        //                if (dr["PrixVentePublic"] != DBNull.Value)
        //                    OrdrePreparationDetail.PrixVentePublic = decimal.Parse(dr["PrixVentePublic"].ToString());
        //                if (dr["QuantiteRestore"] != DBNull.Value)
        //                    OrdrePreparationDetail.QuantiteRestore = decimal.Parse(dr["QuantiteRestore"].ToString());
        //                if (dr["OrdreBonCommande"] != DBNull.Value)
        //                    OrdrePreparationDetail.OrdreBonCommande = int.Parse(dr["OrdreBonCommande"].ToString());
        //                if (dr["Longueur"] != DBNull.Value)
        //                    OrdrePreparationDetail.Longueur = decimal.Parse(dr["Longueur"].ToString());
        //                if (dr["Largeur"] != DBNull.Value)
        //                    OrdrePreparationDetail.Largeur = decimal.Parse(dr["Largeur"].ToString());
        //                if (dr["Epaisseur"] != DBNull.Value)
        //                    OrdrePreparationDetail.Epaisseur = decimal.Parse(dr["Epaisseur"].ToString());
        //                if (dr["MontantNet"] != DBNull.Value)
        //                    OrdrePreparationDetail.MontantNet = decimal.Parse(dr["MontantNet"].ToString());

        //                AncienneBLDetailCollection.Add(OrdrePreparationDetail);
        //            }
        //        }

        //        BonEntree bonEntreeCible = new BonEntree();
        //        bonEntreeCible.CUnite = OrdrePreparationDetail.CUnite;
        //        bonEntreeCible.NDocumentSource = NOrdrePreparation;
        //        bonEntreeCible.TypeMouvement = StockHelper.TypesMouvementStock.BE_OrdrePreparationInterne.ToString();
        //        bonEntreeCible.DateEntree = (DateTime)DatePreparation;
        //        bonEntreeCible.Exercice = Exercice;

        //        BonEntree bonEntreeSource = new BonEntree();
        //        bonEntreeSource.CUnite = OrdrePreparationDetail.CUnite;
        //        bonEntreeSource.NDocumentSource = NOrdrePreparation;
        //        bonEntreeSource.TypeMouvement = StockHelper.TypesMouvementStock.BE_OrdrePreparationInterne.ToString();
        //        bonEntreeSource.DateEntree = (DateTime)DatePreparation;
        //        bonEntreeSource.Exercice = Exercice;

        //        BonSortie bonSortieSource = new BonSortie();
        //        bonSortieSource.CUnite = OrdrePreparationDetail.CUnite;
        //        bonSortieSource.NDocumentSource = NOrdrePreparation;
        //        bonSortieSource.TypeMouvement = StockHelper.TypesMouvementStock.BS_OrdrePreparationInterne.ToString();
        //        bonSortieSource.CChauffeur = CChauffeur;
        //        bonSortieSource.CVehicule = CVehicule;
        //        bonSortieSource.CClient = CClient;
        //        bonSortieSource.RaisonSociale = RaisonSociale;
        //        bonSortieSource.DateSortie = (DateTime)DatePreparation;
        //        bonSortieSource.Exercice = Exercice;

        //        BonSortie bonSortieCible = new BonSortie();
        //        bonSortieCible.CUnite = OrdrePreparationDetail.CUnite;
        //        bonSortieCible.NDocumentSource = NOrdrePreparation;
        //        bonSortieCible.TypeMouvement = StockHelper.TypesMouvementStock.BS_OrdrePreparationInterne.ToString();
        //        bonSortieCible.CChauffeur = CChauffeur;
        //        bonSortieCible.CVehicule = CVehicule;
        //        bonSortieCible.CClient = CClient;
        //        bonSortieCible.RaisonSociale = RaisonSociale;
        //        bonSortieCible.DateSortie = (DateTime)DatePreparation;
        //        bonSortieCible.Exercice = Exercice;

        //        foreach (OrdrePreparationDetail obj in AncienneBLDetailCollection)
        //        {
        //            var objModifie = this.OrdrePreparationDetailCollection.RecupererOrdrePreparationDetail(obj.NOrdrePreparation, obj.CArticle);
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

        //        foreach (OrdrePreparationDetail obj in AncienneBLDetailCollection)
        //        {
        //            var objAjoute = AncienneBLDetailCollection.RecupererOrdrePreparationDetail(obj.NOrdrePreparation, obj.CArticle);
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

        public void RestituerQuantitePreparee(SqlTransaction transaction)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Transaction = transaction;
            cmd.Connection = transaction.Connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "BonCommande_RestituerQuantitePreparee";

            cmd.Parameters.AddWithValue("@NOrdrePreparation", this.NOrdrePreparation);
            cmd.Parameters.AddWithValue("@NBonCommande", this.NBonCommande);
            foreach (SqlParameter parametre in cmd.Parameters)
                if (parametre.Value == null)
                    parametre.Value = DBNull.Value;

            cmd.ExecuteNonQuery();
        }

        public static OrdrePreparation Charger(string nOrdrePreparation)
        {
            OrdrePreparation ordrePreparation = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "OrdrePreparation_Charger";
                    cmd.Parameters.AddWithValue("@NOrdrePreparation", nOrdrePreparation);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            ordrePreparation = new OrdrePreparation();
                            ordrePreparation.NOrdrePreparation = dr["NOrdrePreparation"].ToString();
                            if (dr["CClient"] != DBNull.Value)
                                ordrePreparation.CClient = dr["CClient"].ToString();
                            if (dr["DatePreparation"] != DBNull.Value)
                                ordrePreparation.DatePreparation = DateTime.Parse(dr["DatePreparation"].ToString());
                            if (dr["CUnite"] != DBNull.Value)
                                ordrePreparation.CUnite = dr["CUnite"].ToString();

                            if (dr["MatriculeFiscale"] != DBNull.Value)
                                ordrePreparation.MatriculeFiscale = dr["MatriculeFiscale"].ToString();
                            if (dr["Adresse"] != DBNull.Value)
                                ordrePreparation.Adresse = dr["Adresse"].ToString();
                            if (dr["NTelephone"] != DBNull.Value)
                                ordrePreparation.NTelephone = dr["NTelephone"].ToString();
                            if (dr["CPreparateur"] != DBNull.Value)
                                ordrePreparation.CPreparateur = int.Parse(dr["CPreparateur"].ToString());
                            if (dr["BExonoreFodec"] != DBNull.Value)
                                ordrePreparation.BExonoreFodec = bool.Parse(dr["BExonoreFodec"].ToString());
                            if (dr["BExonoreTVA"] != DBNull.Value)
                                ordrePreparation.BExonoreTVA = bool.Parse(dr["BExonoreTVA"].ToString());
                            if (dr["BAvanceForfaitaire"] != DBNull.Value)
                                ordrePreparation.BAvanceForfaitaire = bool.Parse(dr["BAvanceForfaitaire"].ToString());
                            if (dr["MontantHT"] != DBNull.Value)
                                ordrePreparation.MontantHT = decimal.Parse(dr["MontantHT"].ToString());
                            if (dr["MontantRemise"] != DBNull.Value)
                                ordrePreparation.MontantRemise = decimal.Parse(dr["MontantRemise"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                ordrePreparation.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["MontantTTC"] != DBNull.Value)
                                ordrePreparation.MontantTTC = decimal.Parse(dr["MontantTTC"].ToString());
                            if (dr["PoidsTotal"] != DBNull.Value)
                                ordrePreparation.PoidsTotal = decimal.Parse(dr["PoidsTotal"].ToString());
                            if (dr["BLivre"] != DBNull.Value)
                                ordrePreparation.BLivre = bool.Parse(dr["BLivre"].ToString());
                            if (dr["NBonCommande"] != DBNull.Value)
                                ordrePreparation.NBonCommande = dr["NBonCommande"].ToString();
                            if (dr["BAnnuler"] != DBNull.Value)
                                ordrePreparation.BAnnuler = bool.Parse(dr["BAnnuler"].ToString());
                            if (dr["Observation"] != DBNull.Value)
                                ordrePreparation.Observation = dr["Observation"].ToString();
                            if (dr["RaisonSociale"] != DBNull.Value)
                                ordrePreparation.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["Indice"] != DBNull.Value)
                                ordrePreparation.Indice = int.Parse(dr["Indice"].ToString());

                            if (dr["CChauffeur"] != DBNull.Value)
                                ordrePreparation.CChauffeur = dr["CChauffeur"].ToString();
                            if (dr["CVehicule"] != DBNull.Value)
                                ordrePreparation.CVehicule = dr["CVehicule"].ToString();
                            if (dr["MontantRetenuForfaitaire"] != DBNull.Value)
                                ordrePreparation.MontantRetenuForfaitaire = decimal.Parse(dr["MontantRetenuForfaitaire"].ToString());

                            ordrePreparation.OrdrePreparationDetailCollection = OrdrePreparationDetailCollection.Charger(nOrdrePreparation);
                            ordrePreparation.OrdrePreparationTaxeCollection = OrdrePreparationTaxeCollection.Charger(nOrdrePreparation);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return ordrePreparation;
        }

        public static OrdrePreparation ChargerDernierOrdre(string nBonCommande, SqlTransaction transaction)
        {
            OrdrePreparation ordrePreparation = null;

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "OrdrePreparation_ChargerCommande";
                cmd.Parameters.AddWithValue("@NBonCommande", nBonCommande);
                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        ordrePreparation = new OrdrePreparation();
                        ordrePreparation.NOrdrePreparation = dr["NOrdrePreparation"].ToString();
                        if (dr["CClient"] != DBNull.Value)
                            ordrePreparation.CClient = dr["CClient"].ToString();
                        if (dr["DatePreparation"] != DBNull.Value)
                            ordrePreparation.DatePreparation = DateTime.Parse(dr["DatePreparation"].ToString());
                        if (dr["CUnite"] != DBNull.Value)
                            ordrePreparation.CUnite = dr["CUnite"].ToString();

                        if (dr["MatriculeFiscale"] != DBNull.Value)
                            ordrePreparation.MatriculeFiscale = dr["MatriculeFiscale"].ToString();
                        if (dr["Adresse"] != DBNull.Value)
                            ordrePreparation.Adresse = dr["Adresse"].ToString();
                        if (dr["NTelephone"] != DBNull.Value)
                            ordrePreparation.NTelephone = dr["NTelephone"].ToString();
                        if (dr["CPreparateur"] != DBNull.Value)
                            ordrePreparation.CPreparateur = int.Parse(dr["CPreparateur"].ToString());
                        if (dr["BExonoreFodec"] != DBNull.Value)
                            ordrePreparation.BExonoreFodec = bool.Parse(dr["BExonoreFodec"].ToString());
                        if (dr["BExonoreTVA"] != DBNull.Value)
                            ordrePreparation.BExonoreTVA = bool.Parse(dr["BExonoreTVA"].ToString());
                        if (dr["BAvanceForfaitaire"] != DBNull.Value)
                            ordrePreparation.BAvanceForfaitaire = bool.Parse(dr["BAvanceForfaitaire"].ToString());

                        if (dr["MontantHT"] != DBNull.Value)
                            ordrePreparation.MontantHT = decimal.Parse(dr["MontantHT"].ToString());
                        if (dr["MontantRemise"] != DBNull.Value)
                            ordrePreparation.MontantRemise = decimal.Parse(dr["MontantRemise"].ToString());
                        if (dr["MontantTaxe"] != DBNull.Value)
                            ordrePreparation.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                        if (dr["MontantTTC"] != DBNull.Value)
                            ordrePreparation.MontantTTC = decimal.Parse(dr["MontantTTC"].ToString());
                        if (dr["PoidsTotal"] != DBNull.Value)
                            ordrePreparation.PoidsTotal = decimal.Parse(dr["PoidsTotal"].ToString());
                        if (dr["BLivre"] != DBNull.Value)
                            ordrePreparation.BLivre = bool.Parse(dr["BLivre"].ToString());


                        if (dr["NBonCommande"] != DBNull.Value)
                            ordrePreparation.NBonCommande = dr["NBonCommande"].ToString();

                        if (dr["Observation"] != DBNull.Value)
                            ordrePreparation.Observation = dr["Observation"].ToString();
                        if (dr["RaisonSociale"] != DBNull.Value)
                            ordrePreparation.RaisonSociale = dr["RaisonSociale"].ToString();
                        if (dr["Indice"] != DBNull.Value)
                            ordrePreparation.Indice = int.Parse(dr["Indice"].ToString());

                        if (dr["CChauffeur"] != DBNull.Value)
                            ordrePreparation.CChauffeur = dr["CChauffeur"].ToString();
                        if (dr["CVehicule"] != DBNull.Value)
                            ordrePreparation.CVehicule = dr["CVehicule"].ToString();
                        if (dr["MontantRetenuForfaitaire"] != DBNull.Value)
                            ordrePreparation.MontantRetenuForfaitaire = decimal.Parse(dr["MontantRetenuForfaitaire"].ToString());

                        ordrePreparation.OrdrePreparationDetailCollection = OrdrePreparationDetailCollection.Charger(ordrePreparation.NOrdrePreparation);
                        ordrePreparation.OrdrePreparationTaxeCollection = OrdrePreparationTaxeCollection.Charger(ordrePreparation.NOrdrePreparation);
                    }
                }
            }

            catch (Exception)
            {
                throw;
            }

            return ordrePreparation;
        }
    }

    public class OrdrePreparationCollection : List<OrdrePreparation>
    {
        public OrdrePreparationCollection()
        {
        }

        public static OrdrePreparationCollection Charger(SqlTransaction transaction, string nBonCommande)
        {
            OrdrePreparationCollection collection = new OrdrePreparationCollection();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BonCommande_ChargerOrdrePreparation";
                cmd.Parameters.AddWithValue("@NBonCommande", nBonCommande);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        OrdrePreparation ordrePreparation = new OrdrePreparation();
                        ordrePreparation.NOrdrePreparation = dr["NOrdrePreparation"].ToString();
                        if (dr["CClient"] != DBNull.Value)
                            ordrePreparation.CClient = dr["CClient"].ToString();
                        if (dr["DatePreparation"] != DBNull.Value)
                            ordrePreparation.DatePreparation = DateTime.Parse(dr["DatePreparation"].ToString());
                        if (dr["CUnite"] != DBNull.Value)
                            ordrePreparation.CUnite = dr["CUnite"].ToString();

                        if (dr["MatriculeFiscale"] != DBNull.Value)
                            ordrePreparation.MatriculeFiscale = dr["MatriculeFiscale"].ToString();
                        if (dr["Adresse"] != DBNull.Value)
                            ordrePreparation.Adresse = dr["Adresse"].ToString();
                        if (dr["NTelephone"] != DBNull.Value)
                            ordrePreparation.NTelephone = dr["NTelephone"].ToString();
                        if (dr["CPreparateur"] != DBNull.Value)
                            ordrePreparation.CPreparateur = int.Parse(dr["CPreparateur"].ToString());
                        if (dr["BExonoreFodec"] != DBNull.Value)
                            ordrePreparation.BExonoreFodec = bool.Parse(dr["BExonoreFodec"].ToString());
                        if (dr["BExonoreTVA"] != DBNull.Value)
                            ordrePreparation.BExonoreTVA = bool.Parse(dr["BExonoreTVA"].ToString());
                        if (dr["BAvanceForfaitaire"] != DBNull.Value)
                            ordrePreparation.BAvanceForfaitaire = bool.Parse(dr["BAvanceForfaitaire"].ToString());

                        if (dr["PoidsTotal"] != DBNull.Value)
                            ordrePreparation.PoidsTotal = decimal.Parse(dr["PoidsTotal"].ToString());
                        if (dr["BLivre"] != DBNull.Value)
                            ordrePreparation.BLivre = bool.Parse(dr["BLivre"].ToString());
                        ordrePreparation.MontantHT = decimal.Parse(dr["MontantHT"].ToString());
                        if (dr["MontantRemise"] != DBNull.Value)
                            ordrePreparation.MontantRemise = decimal.Parse(dr["MontantRemise"].ToString());
                        if (dr["MontantTaxe"] != DBNull.Value)
                            ordrePreparation.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                        if (dr["MontantTTC"] != DBNull.Value)
                            ordrePreparation.MontantTTC = decimal.Parse(dr["MontantTTC"].ToString());
                        if (dr["NBonCommande"] != DBNull.Value)
                            ordrePreparation.NBonCommande = dr["NBonCommande"].ToString();

                        if (dr["Observation"] != DBNull.Value)
                            ordrePreparation.Observation = dr["Observation"].ToString();
                        if (dr["RaisonSociale"] != DBNull.Value)
                            ordrePreparation.RaisonSociale = dr["RaisonSociale"].ToString();
                        if (dr["Indice"] != DBNull.Value)
                            ordrePreparation.Indice = int.Parse(dr["Indice"].ToString());

                        if (dr["CChauffeur"] != DBNull.Value)
                            ordrePreparation.CChauffeur = dr["CChauffeur"].ToString();
                        if (dr["CVehicule"] != DBNull.Value)
                            ordrePreparation.CVehicule = dr["CVehicule"].ToString();
                        if (dr["MontantRetenuForfaitaire"] != DBNull.Value)
                            ordrePreparation.MontantRetenuForfaitaire = decimal.Parse(dr["MontantRetenuForfaitaire"].ToString());
                        ordrePreparation.OrdrePreparationDetailCollection = OrdrePreparationDetailCollection.Charger(ordrePreparation.NOrdrePreparation);
                        ordrePreparation.OrdrePreparationTaxeCollection = OrdrePreparationTaxeCollection.Charger(ordrePreparation.NOrdrePreparation);
                        collection.Add(ordrePreparation);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return collection;
        }

        public static OrdrePreparationCollection Charger()
        {
            OrdrePreparationCollection collection = new OrdrePreparationCollection();
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "OrdrePreparation_Charger";
                    cmd.Parameters.AddWithValue("@NOrdrePreparation", DBNull.Value);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            OrdrePreparation ordrePreparation = new OrdrePreparation();
                            ordrePreparation.NOrdrePreparation = dr["NOrdrePreparation"].ToString();
                            if (dr["CClient"] != DBNull.Value)
                                ordrePreparation.CClient = dr["CClient"].ToString();
                            if (dr["DatePreparation"] != DBNull.Value)
                                ordrePreparation.DatePreparation = DateTime.Parse(dr["DatePreparation"].ToString());
                            if (dr["CUnite"] != DBNull.Value)
                                ordrePreparation.CUnite = dr["CUnite"].ToString();

                            if (dr["MatriculeFiscale"] != DBNull.Value)
                                ordrePreparation.MatriculeFiscale = dr["MatriculeFiscale"].ToString();
                            if (dr["Adresse"] != DBNull.Value)
                                ordrePreparation.Adresse = dr["Adresse"].ToString();
                            if (dr["NTelephone"] != DBNull.Value)
                                ordrePreparation.NTelephone = dr["NTelephone"].ToString();
                            if (dr["CPreparateur"] != DBNull.Value)
                                ordrePreparation.CPreparateur = int.Parse(dr["CPreparateur"].ToString());
                            if (dr["BExonoreFodec"] != DBNull.Value)
                                ordrePreparation.BExonoreFodec = bool.Parse(dr["BExonoreFodec"].ToString());
                            if (dr["BExonoreTVA"] != DBNull.Value)
                                ordrePreparation.BExonoreTVA = bool.Parse(dr["BExonoreTVA"].ToString());
                            if (dr["BAvanceForfaitaire"] != DBNull.Value)
                                ordrePreparation.BAvanceForfaitaire = bool.Parse(dr["BAvanceForfaitaire"].ToString());

                            if (dr["PoidsTotal"] != DBNull.Value)
                                ordrePreparation.PoidsTotal = decimal.Parse(dr["PoidsTotal"].ToString());
                            if (dr["BLivre"] != DBNull.Value)
                                ordrePreparation.BLivre = bool.Parse(dr["BLivre"].ToString());
                            if (dr["BAnnuler"] != DBNull.Value)
                                ordrePreparation.BAnnuler = bool.Parse(dr["BAnnuler"].ToString());

                            ordrePreparation.MontantHT = decimal.Parse(dr["MontantHT"].ToString());
                            if (dr["MontantRemise"] != DBNull.Value)
                                ordrePreparation.MontantRemise = decimal.Parse(dr["MontantRemise"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                ordrePreparation.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["MontantTTC"] != DBNull.Value)
                                ordrePreparation.MontantTTC = decimal.Parse(dr["MontantTTC"].ToString());
                            if (dr["NBonCommande"] != DBNull.Value)
                                ordrePreparation.NBonCommande = dr["NBonCommande"].ToString();

                            if (dr["Observation"] != DBNull.Value)
                                ordrePreparation.Observation = dr["Observation"].ToString();
                            if (dr["RaisonSociale"] != DBNull.Value)
                                ordrePreparation.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["Indice"] != DBNull.Value)
                                ordrePreparation.Indice = int.Parse(dr["Indice"].ToString());

                            if (dr["CChauffeur"] != DBNull.Value)
                                ordrePreparation.CChauffeur = dr["CChauffeur"].ToString();
                            if (dr["CVehicule"] != DBNull.Value)
                                ordrePreparation.CVehicule = dr["CVehicule"].ToString();
                            if (dr["MontantRetenuForfaitaire"] != DBNull.Value)
                                ordrePreparation.MontantRetenuForfaitaire = decimal.Parse(dr["MontantRetenuForfaitaire"].ToString());
                            ordrePreparation.OrdrePreparationDetailCollection = OrdrePreparationDetailCollection.Charger(ordrePreparation.NOrdrePreparation);
                            ordrePreparation.OrdrePreparationTaxeCollection = OrdrePreparationTaxeCollection.Charger(ordrePreparation.NOrdrePreparation);
                            collection.Add(ordrePreparation);
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
    }
}