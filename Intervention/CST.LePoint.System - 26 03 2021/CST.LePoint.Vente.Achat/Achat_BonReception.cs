using CST.LePoint.Stock.Metier;
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
    public class Achat_BonReception
    {
        #region Proriétès
        [XmlAttribute("NBonReception")]
        [Bindable(true)]
        public string NBonReception { get; set; }
        [XmlAttribute("CEntrepot")]
        [Bindable(true)]
        public string CEntrepot { get; set; }
        [XmlAttribute("CFournisseur")]
        [Bindable(true)]
        public string CFournisseur { get; set; }
        [XmlAttribute("TypeReception")]
        [Bindable(true)]
        public string TypeReception { get; set; }
        [XmlAttribute("DateInsertion")]
        [Bindable(true)]
        public DateTime? DateInsertion { get; set; }
        [XmlAttribute("DateReception")]
        [Bindable(true)]
        public DateTime? DateReception { get; set; }
        [XmlAttribute("DateModification")]
        [Bindable(true)]
        public DateTime? DateModification { get; set; }
        [XmlAttribute("BExonoreFodec")]
        [Bindable(true)]
        public bool BExonoreFodec { get; set; }
        [XmlAttribute("BExonoreTVA")]
        [Bindable(true)]
        public bool BExonoreTVA { get; set; }
        [XmlAttribute("MontantHT")]
        [Bindable(true)]
        public decimal MontantHT { get; set; }
        [XmlAttribute("CTaxe")]
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
        [XmlAttribute("NFacture")]
        [Bindable(true)]
        public string NFacture { get; set; }
        [XmlAttribute("Reference")]
        [Bindable(true)]
        public string Reference { get; set; }
        [XmlAttribute("RaisonSociale")]
        [Bindable(true)]
        public string RaisonSociale { get; set; }
        [XmlAttribute("CreePar")]
        [Bindable(true)]
        public int Indice { get; set; }
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

        public Achat_BonReceptionDetailCollection BonReceptionDetailCollection;
        public Achat_BonReceptionTaxeCollection BonReceptionTaxeCollection;
        #endregion

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
                cmd.CommandText = "Achat_BonReception_Inserer";
                cmd.Parameters.AddWithValue("@CFournisseur", this.CFournisseur);
                cmd.Parameters.AddWithValue("@DateReception ", this.DateReception);
                cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
                cmd.Parameters.AddWithValue("@BExonoreFodec", this.BExonoreFodec);
                cmd.Parameters.AddWithValue("@BExonoreTVA", this.BExonoreTVA);
                cmd.Parameters.AddWithValue("@MontantHT", this.MontantHT);
                cmd.Parameters.AddWithValue("@MontantRemise", this.MontantRemise);
                cmd.Parameters.AddWithValue("@MontantTaxe", this.MontantTaxe);
                cmd.Parameters.AddWithValue("@MontantTTC", this.MontantTTC);
                cmd.Parameters.AddWithValue("@NBonCommande", this.NBonCommande);
                cmd.Parameters.AddWithValue("@Reference", this.Reference);
                cmd.Parameters.AddWithValue("@NFacture", this.NFacture);
                cmd.Parameters.AddWithValue("@RaisonSociale", this.RaisonSociale);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);
                cmd.Parameters.AddWithValue("@CreePar", this.CreePar);
                cmd.Parameters.AddWithValue("@Exercice", DateTime.Now.Year.ToString());
                cmd.Parameters.AddWithValue("@TypeReception", this.TypeReception);
                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        this.NBonReception = dr["NBonReception"].ToString();
                    }
                }
                Achat_BonCommande bc = Achat_BonCommande.Charger(this.NBonCommande);
                int i = 1;
                foreach (Achat_BonReceptionDetail bonReceptionDetail in this.BonReceptionDetailCollection)
                {
                    bonReceptionDetail.NBonReception = this.NBonReception;
                    bonReceptionDetail.Ordre = i++;
                    bonReceptionDetail.Sauvegarder(transaction);

                    if (bc != null && bc.BonCommandeDetailCollection.Exists(x => x.CArticle == bonReceptionDetail.CArticle && x.Ordre == bonReceptionDetail.OrdreBonCommande))
                    {
                        decimal qteHist = bc.BonCommandeDetailCollection.Find(x => x.CArticle == bonReceptionDetail.CArticle && x.Ordre == bonReceptionDetail.OrdreBonCommande).QuantiteHistorique;
                        AchatHelper.MiseAJourStockEnCommandeFnr(bonReceptionDetail.CArticle, -bonReceptionDetail.Quantite, transaction);
                        AchatHelper.MiseAJourBonCommandeQteHist(this.NBonCommande, bonReceptionDetail.CArticle, bonReceptionDetail.OrdreBonCommande, -bonReceptionDetail.Quantite, transaction);
                        bc.BonCommandeDetailCollection.Find(x => x.CArticle == bonReceptionDetail.CArticle && x.Ordre == bonReceptionDetail.OrdreBonCommande).QuantiteHistorique = qteHist - bonReceptionDetail.Quantite;
                    }
                }
                foreach (Achat_BonReceptionTaxe bonReceptionTaxe in this.BonReceptionTaxeCollection)
                {
                    bonReceptionTaxe.NBonReception = this.NBonReception;
                    bonReceptionTaxe.Sauvegarder(transaction);
                }
                if (bc != null && (from detail in bc.BonCommandeDetailCollection select detail.QuantiteHistorique).Sum() == 0)
                    Achat_BonCommande.ModifierEtatBonCommande(bc.NBonCommande, AchatHelper.EtatBonCommande.LIVRE.ToString(), transaction);

                CreerBonEntree(transaction);


            }

            catch (Exception)
            {
                throw;
            }
        }

        private void CreerBonEntree(SqlTransaction transaction)
        {
            if (this.BonReceptionDetailCollection.Count > 0)
            {
                    BonEntree bonEntree = new BonEntree();
                    bonEntree.CEntrepot = this.CEntrepot;
                    bonEntree.NDocumentSource = this.NBonReception;
                    bonEntree.DateEntree = (DateTime)DateReception;
                    bonEntree.CFournisseur = this.CFournisseur;
                    bonEntree.RaisonSociale = this.RaisonSociale;
                    bonEntree.Exercice = DateTime.Now.Year.ToString();
                    bonEntree.TypeMouvement = StockHelper.TypesMouvementStock.BE_BONRECEPTION.ToString();
                    bonEntree.BFodecExonore = this.BExonoreFodec;
                    bonEntree.BTvaExonore = this.BExonoreTVA;
                    bonEntree.CreePar = this.CreePar;
                    bonEntree.DateInsertion = DateTime.Now;
                    foreach (Achat_BonReceptionDetail detail in this.BonReceptionDetailCollection)// while (BRDetail != null)
                    {
                        BonEntreeDetail bonEntreeDetail = new BonEntreeDetail();
                        bonEntreeDetail.CEntrepot = detail.CEntrepot;
                        bonEntreeDetail.CArticle = detail.CArticle;
                        bonEntreeDetail.Quantite = detail.Quantite;
                        bonEntreeDetail.CUnite = detail.CUnite;
                        bonEntreeDetail.LibArticle = detail.LibArticle;
                        bonEntreeDetail.TauxTVA = detail.TauxTVA;
                        bonEntreeDetail.CreePar = this.CreePar;
                        bonEntreeDetail.PCInsertion = this.PCInsertion;
                        bonEntree.BonEntreeDetailCollection.Add(bonEntreeDetail);
                    }
                    bonEntree.Inserer(transaction);

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
                    if (Achat_BonCommande.Charger(this.NBonCommande).BonCommandeDetailCollection.Sum(x => x.QuantiteHistorique) == 0)
                        Achat_BonCommande.ModifierEtatBonCommande(this.NBonCommande, AchatHelper.EtatBonCommande.LIVRE.ToString());
                    else
                        Achat_BonCommande.ModifierEtatBonCommande(this.NBonCommande, AchatHelper.EtatBonCommande.ENCOURS.ToString());
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
                cmd.CommandText = "Achat_BonReception_Inserer";
                cmd.Parameters.AddWithValue("@NBonReception", this.NBonReception);
                cmd.Parameters.AddWithValue("@CFournisseur", this.CFournisseur);
                cmd.Parameters.AddWithValue("@DateReception ", this.DateReception);
                cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
                cmd.Parameters.AddWithValue("@BExonoreFodec", this.BExonoreFodec);
                cmd.Parameters.AddWithValue("@BExonoreTVA", this.BExonoreTVA);
                cmd.Parameters.AddWithValue("@MontantHT", this.MontantHT);
                cmd.Parameters.AddWithValue("@MontantRemise", this.MontantRemise);
                cmd.Parameters.AddWithValue("@MontantTaxe", this.MontantTaxe);
                cmd.Parameters.AddWithValue("@MontantTTC", this.MontantTTC);
                cmd.Parameters.AddWithValue("@NBonCommande", this.NBonCommande);
                cmd.Parameters.AddWithValue("@Reference", this.Reference);
                cmd.Parameters.AddWithValue("@NFacture", this.NFacture);
                cmd.Parameters.AddWithValue("@RaisonSociale", this.RaisonSociale);
                cmd.Parameters.AddWithValue("@DateModification", DateModification);
                cmd.Parameters.AddWithValue("@PCModification", this.PCModification);
                cmd.Parameters.AddWithValue("@ModifiePar", this.ModifiePar);
                cmd.Parameters.AddWithValue("@TypeReception", this.TypeReception);
                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;
                cmd.ExecuteNonQuery();
              
                this.SupprimerDetailBonReceptionAnterieurs(transaction);
                this.SupprimerTaxeBonReceptionAnterieurs(transaction);
                int i = 1;
                foreach (Achat_BonReceptionDetail bonReceptionDetail in BonReceptionDetailCollection)
                {
                    bonReceptionDetail.NBonReception = this.NBonReception;
                    bonReceptionDetail.Ordre = i++;
                    bonReceptionDetail.Sauvegarder(transaction);
                }
                foreach (Achat_BonReceptionTaxe bonReceptionTaxe in BonReceptionTaxeCollection)
                {
                    bonReceptionTaxe.NBonReception = this.NBonReception;
                    bonReceptionTaxe.Sauvegarder(transaction);
                }

                this.CreeMouvement(transaction);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void CreeMouvement(SqlTransaction transaction)
        {
            Achat_BonReception ancienBonReception = Achat_BonReception.Charger(this.NBonReception,this.CEntrepot);
            Achat_BonReceptionDetailCollection detailCollection = this.BonReceptionDetailCollection;
            BonEntree bonEntree = new BonEntree();
            bonEntree.BFodecExonore = this.BExonoreFodec;
            bonEntree.BTvaExonore = this.BExonoreTVA;
            bonEntree.CFournisseur = this.CFournisseur;
            bonEntree.CEntrepot = this.CEntrepot;
            bonEntree.CreePar = this.ModifiePar;
            bonEntree.PCInsertion = this.PCModification;
            bonEntree.DateEntree = (DateTime)this.DateReception;
            bonEntree.Exercice = DateTime.Now.Year.ToString();
            bonEntree.NDocumentSource = this.NBonReception;
            bonEntree.RaisonSociale = this.RaisonSociale;
            bonEntree.TypeMouvement = StockHelper.TypesMouvementStock.BE_BONRECEPTION.ToString();

            BonSortie bonSortie = new BonSortie();
            //bonSortie.CChauffeur = this.Chauffeur;
            bonSortie.CClient = this.CFournisseur;
            bonSortie.CEntrepot = this.CEntrepot;
            bonSortie.CreePar = this.ModifiePar;
            bonSortie.DateSortie = (DateTime)this.DateReception;
            bonSortie.Exercice = DateTime.Now.Year.ToString();
            bonSortie.NDocumentSource = this.NBonReception;
            bonSortie.PCInsertion = this.PCModification;
            bonSortie.RaisonSociale = this.RaisonSociale;
            bonSortie.TypeMouvement = StockHelper.TypesMouvementStock.BS_BONRECEPTION.ToString();

            foreach (Achat_BonReceptionDetail detail in this.BonReceptionDetailCollection)
            {
                Achat_BonReceptionDetail ancienDetail = ancienBonReception.BonReceptionDetailCollection.Find(x => x.CArticle == detail.CArticle && x.Ordre == detail.Ordre && x.CEntrepot == detail.CEntrepot);
                if (ancienDetail!=null)
                {
                    if (detail.Quantite < ancienDetail.Quantite)
                    {
                        BonSortieDetail detailBS = new BonSortieDetail();
                        detailBS.CArticle = detail.CArticle;
                        detailBS.CEntrepot = detail.CEntrepot;
                        detailBS.CUnite = detail.CUnite;
                        detailBS.LibArticle = detail.LibArticle;
                        detailBS.MontantTaxe = detail.MontantTaxe;
                        detailBS.PrixHT = detail.PrixHT;
                        detailBS.Quantite = ancienDetail.Quantite - detail.Quantite;
                        detailBS.TauxTVA = detail.TauxTVA;
                        detailBS.CreePar = this.ModifiePar;
                        detailBS.PCInsertion = this.PCModification;
                        bonSortie.BonSortieDetailCollection.Add(detailBS);
                        AchatHelper.MiseAJourStockEnCommandeFnr(detail.CArticle, detailBS.Quantite, transaction);
                        AchatHelper.MiseAJourBonCommandeQteHist(this.NBonCommande, detail.CArticle, detail.OrdreBonCommande, detailBS.Quantite, transaction);
                    }
                    if (detail.Quantite > ancienDetail.Quantite)
                    {
                        BonEntreeDetail detailBE = new BonEntreeDetail();
                        detailBE.CArticle = detail.CArticle;
                        detailBE.CEntrepot = detail.CEntrepot;
                        detailBE.CUnite = detail.CUnite;
                        detailBE.CTaxe = detail.CTaxe;
                        detailBE.LibArticle = detail.LibArticle;
                        detailBE.PourcentageFodec = detail.PourcentageFodec;
                        detailBE.PourcentageRemise = detail.PourcentageRemise;
                        detailBE.PrixRevient = detail.PrixHT;
                        detailBE.Quantite = detail.Quantite - ancienDetail.Quantite;
                        detailBE.TauxTVA = detail.TauxTVA;
                        detailBE.CreePar = this.ModifiePar;
                        detailBE.PCInsertion = this.PCModification;
                        bonEntree.BonEntreeDetailCollection.Add(detailBE);
                        AchatHelper.MiseAJourStockEnCommandeFnr(detail.CArticle, -detailBE.Quantite, transaction);
                        AchatHelper.MiseAJourBonCommandeQteHist(this.NBonCommande, detail.CArticle, detail.OrdreBonCommande, -detailBE.Quantite, transaction);
                    }
                    ancienBonReception.BonReceptionDetailCollection.Remove(ancienDetail);
                    detailCollection.Remove(detail);
                }
            }
            foreach (Achat_BonReceptionDetail detail in ancienBonReception.BonReceptionDetailCollection)
            {
                BonSortieDetail detailBS = new BonSortieDetail();
                detailBS.CArticle = detail.CArticle;
                detailBS.CEntrepot = detail.CEntrepot;
                detailBS.CUnite = detail.CUnite;
                detailBS.LibArticle = detail.LibArticle;
                detailBS.MontantTaxe = detail.MontantTaxe;
                detailBS.PrixHT = detail.PrixHT;
                detailBS.Quantite = detail.Quantite;
                detailBS.TauxTVA = detail.TauxTVA;
                detailBS.CreePar = this.ModifiePar;
                detailBS.PCInsertion = this.PCModification;
                bonSortie.BonSortieDetailCollection.Add(detailBS);
                AchatHelper.MiseAJourStockEnCommandeFnr(detail.CArticle, detailBS.Quantite, transaction);
                AchatHelper.MiseAJourBonCommandeQteHist(this.NBonCommande, detail.CArticle, detail.OrdreBonCommande, detailBS.Quantite, transaction);
            }

            foreach (Achat_BonReceptionDetail detail in detailCollection)
            {
                BonEntreeDetail detailBE = new BonEntreeDetail();
                detailBE.CArticle = detail.CArticle;
                detailBE.CEntrepot = detail.CEntrepot;
                detailBE.CUnite = detail.CUnite;
                detailBE.CTaxe = detail.CTaxe;
                detailBE.LibArticle = detail.LibArticle;
                detailBE.PourcentageFodec = detail.PourcentageFodec;
                detailBE.PourcentageRemise = detail.PourcentageRemise;
                detailBE.PrixRevient = detail.PrixHT;
                detailBE.Quantite = detail.Quantite;
                detailBE.TauxTVA = detail.TauxTVA;
                detailBE.CreePar = this.ModifiePar;
                detailBE.PCInsertion = this.PCModification;
                bonEntree.BonEntreeDetailCollection.Add(detailBE);
                AchatHelper.MiseAJourStockEnCommandeFnr(detail.CArticle, -detailBE.Quantite, transaction);
                AchatHelper.MiseAJourBonCommandeQteHist(this.NBonCommande, detail.CArticle, -detail.OrdreBonCommande, detail.Quantite, transaction);
            }
            if (bonSortie.BonSortieDetailCollection.Count > 0)
                bonSortie.Inserer();
            if (bonEntree.BonEntreeDetailCollection.Count > 0)
                bonEntree.Inserer();

        }

        private void SupprimerTaxeBonReceptionAnterieurs(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Achat_BonReception_SupprimerTaxes";
                cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
                cmd.Parameters.AddWithValue("@NBonReception", this.NBonReception);

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

        private void SupprimerDetailBonReceptionAnterieurs(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Achat_BonReception_SupprimerDetails";
                cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
                cmd.Parameters.AddWithValue("@NBonReception", this.NBonReception);

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

        public static Achat_BonReception Charger(string nBonReception, string cEntrepot)
        {
            Achat_BonReception bonReception = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Achat_BonReception_Charger";
                    cmd.Parameters.AddWithValue("@NBonReception", bonReception);
                    cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            bonReception = new Achat_BonReception();
                            bonReception.NBonReception = dr["NBonReception"].ToString();
                            bonReception.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["CFournisseur"] != DBNull.Value)
                                bonReception.CFournisseur = dr["CFournisseur"].ToString();
                            if (dr["DateReception"] != DBNull.Value)
                                bonReception.DateReception = DateTime.Parse(dr["DateReception"].ToString());
                            if (dr["BExonoreFodec"] != DBNull.Value)
                                bonReception.BExonoreFodec = bool.Parse(dr["BExonoreFodec"].ToString());
                            if (dr["BExonoreTVA"] != DBNull.Value)
                                bonReception.BExonoreTVA = bool.Parse(dr["BExonoreTVA"].ToString());
                            if (dr["DateModification"] != DBNull.Value)
                                bonReception.DateModification = DateTime.Parse(dr["DateModification"].ToString());
                            if (dr["MontantHT"] != DBNull.Value)
                                bonReception.MontantHT = decimal.Parse(dr["MontantHT"].ToString());
                            if (dr["MontantRemise"] != DBNull.Value)
                                bonReception.MontantRemise = decimal.Parse(dr["MontantRemise"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                bonReception.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["MontantTTC"] != DBNull.Value)
                                bonReception.MontantTTC = decimal.Parse(dr["MontantTTC"].ToString());
                            if (dr["NBonCommande"] != DBNull.Value)
                                bonReception.NBonCommande = dr["NBonCommande"].ToString();
                            if (dr["NFacture"] != DBNull.Value)
                                bonReception.NFacture = dr["NFacture"].ToString();
                            if (dr["RaisonSociale"] != DBNull.Value)
                                bonReception.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["Indice"] != DBNull.Value)
                                bonReception.Indice = int.Parse(dr["Indice"].ToString());
                            if (dr["Reference"] != DBNull.Value)
                                bonReception.Reference = dr["Reference"].ToString();
                            if (dr["TypeReception"] != DBNull.Value)
                                bonReception.TypeReception = dr["TypeReception"].ToString();
                            if (dr["ModifiePar"] != DBNull.Value)
                                bonReception.ModifiePar = int.Parse(dr["ModifiePar"].ToString());
                            if (dr["CreePar"] != DBNull.Value)
                                bonReception.CreePar = int.Parse(dr["CreePar"].ToString());
                            if (dr["PCInsertion"] != DBNull.Value)
                                bonReception.PCInsertion = dr["PCInsertion"].ToString();
                            if (dr["PCModification"] != DBNull.Value)
                                bonReception.PCModification = dr["PCModification"].ToString();
                            if (dr["DateInsertion"] != DBNull.Value)
                                bonReception.DateInsertion = DateTime.Parse(dr["DateInsertion"].ToString());
                            bonReception.BonReceptionDetailCollection = Achat_BonReceptionDetailCollection.Charger(nBonReception,cEntrepot);
                            bonReception.BonReceptionTaxeCollection = Achat_BonReceptionTaxeCollection.Charger(nBonReception, cEntrepot);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bonReception;
        }

    }
    public class Achat_BonReceptionCollection : List<Achat_BonReception>
    {
        public static Achat_BonReceptionCollection Charger(string nBonReception, string cEntrepot)
        {
            Achat_BonReceptionCollection bonReceptionCollection = new Achat_BonReceptionCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Achat_BonReception_Charger";
                    cmd.Parameters.AddWithValue("@NBonReception", nBonReception);
                    cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Achat_BonReception bonReception = new Achat_BonReception();
                            bonReception = new Achat_BonReception();
                            bonReception.NBonReception = dr["NBonReception"].ToString();
                            bonReception.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["CFournisseur"] != DBNull.Value)
                                bonReception.CFournisseur = dr["CFournisseur"].ToString();
                            if (dr["DateReception"] != DBNull.Value)
                                bonReception.DateReception = DateTime.Parse(dr["DateReception"].ToString());
                            if (dr["BExonoreFodec"] != DBNull.Value)
                                bonReception.BExonoreFodec = bool.Parse(dr["BExonoreFodec"].ToString());
                            if (dr["BExonoreTVA"] != DBNull.Value)
                                bonReception.BExonoreTVA = bool.Parse(dr["BExonoreTVA"].ToString());
                            if (dr["DateModification"] != DBNull.Value)
                                bonReception.DateModification = DateTime.Parse(dr["DateModification"].ToString());
                            if (dr["MontantHT"] != DBNull.Value)
                                bonReception.MontantHT = decimal.Parse(dr["MontantHT"].ToString());
                            if (dr["MontantRemise"] != DBNull.Value)
                                bonReception.MontantRemise = decimal.Parse(dr["MontantRemise"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                bonReception.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["MontantTTC"] != DBNull.Value)
                                bonReception.MontantTTC = decimal.Parse(dr["MontantTTC"].ToString());
                            if (dr["NBonCommande"] != DBNull.Value)
                                bonReception.NBonCommande = dr["NBonCommande"].ToString();
                            if (dr["NFacture"] != DBNull.Value)
                                bonReception.NFacture = dr["NFacture"].ToString();
                            if (dr["RaisonSociale"] != DBNull.Value)
                                bonReception.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["Indice"] != DBNull.Value)
                                bonReception.Indice = int.Parse(dr["Indice"].ToString());
                            if (dr["Reference"] != DBNull.Value)
                                bonReception.Reference = dr["Reference"].ToString();
                            if (dr["TypeReception"] != DBNull.Value)
                                bonReception.TypeReception = dr["TypeReception"].ToString();
                            if (dr["ModifiePar"] != DBNull.Value)
                                bonReception.ModifiePar = int.Parse(dr["ModifiePar"].ToString());
                            if (dr["CreePar"] != DBNull.Value)
                                bonReception.CreePar = int.Parse(dr["CreePar"].ToString());
                            if (dr["PCInsertion"] != DBNull.Value)
                                bonReception.PCInsertion = dr["PCInsertion"].ToString();
                            if (dr["PCModification"] != DBNull.Value)
                                bonReception.PCModification = dr["PCModification"].ToString();
                            if (dr["DateInsertion"] != DBNull.Value)
                                bonReception.DateInsertion = DateTime.Parse(dr["DateInsertion"].ToString());
                            bonReception.BonReceptionDetailCollection = Achat_BonReceptionDetailCollection.Charger(bonReception.NBonReception, bonReception.CEntrepot);
                            bonReception.BonReceptionTaxeCollection = Achat_BonReceptionTaxeCollection.Charger(bonReception.NBonReception, bonReception.CEntrepot);
                            bonReceptionCollection.Add(bonReception);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bonReceptionCollection;
        }
    }
}
