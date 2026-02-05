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
    public class Achat_BonRetour
    {
        #region Propriètés

        [XmlAttribute("NBonRetour")]
        [Bindable(true)]
        public string NBonRetour { get; set; }
        [XmlAttribute("CEntrepot")]
        [Bindable(true)]
        public string CEntrepot { get; set; }
        [XmlAttribute("CFournisseur")]
        [Bindable(true)]
        public string CFournisseur { get; set; }
        [XmlAttribute("DateInsertion")]
        [Bindable(true)]
        public DateTime? DateInsertion { get; set; }
        [XmlAttribute("DateRetour")]
        [Bindable(true)]
        public DateTime? DateRetour { get; set; }
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
        [XmlAttribute("NBonReception")]
        [Bindable(true)]
        public string NBonReception { get; set; }
        [XmlAttribute("Observation")]
        [Bindable(true)]
        public string Observation { get; set; }
        [XmlAttribute("RaisonSociale")]
        [Bindable(true)]
        public string RaisonSociale { get; set; }
        [XmlAttribute("Indice")]
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
        [XmlAttribute("BTransfertAvoir")]
        [Bindable(true)]
        public bool BTransfertAvoir { get; set; }
        [XmlAttribute("NFactureAnt")]
        [Bindable(true)]
        public string NFactureAnt { get; set; }
        [XmlAttribute("BRetourAnterieur")]
        [Bindable(true)]
        public bool BRetourAnterieur { get; set; }

        public Achat_BonRetourDetailCollection BonRetourDetailCollection;
        public Achat_BonRetourTaxeCollection BonRetourTaxeCollection;
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

        private void Inserer(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Achat_BonRetour_Inserer";
                cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
                cmd.Parameters.AddWithValue("@CFournisseur", this.CFournisseur);
                cmd.Parameters.AddWithValue("@NBonReception", this.NBonReception);
                cmd.Parameters.AddWithValue("@DateInsertion", this.DateInsertion);
                cmd.Parameters.AddWithValue("@DateRetour", this.DateRetour);
                cmd.Parameters.AddWithValue("@BExonoreFodec", this.BExonoreFodec);
                cmd.Parameters.AddWithValue("@BExonoreTVA", this.BExonoreTVA);
                cmd.Parameters.AddWithValue("@MontantHT", this.MontantHT);
                cmd.Parameters.AddWithValue("@MontantRemise", this.MontantRemise);
                cmd.Parameters.AddWithValue("@MontantTaxe", this.MontantTaxe);
                cmd.Parameters.AddWithValue("@MontantTTC", this.MontantTTC);
                cmd.Parameters.AddWithValue("@NAvoir", this.NAvoir);
                cmd.Parameters.AddWithValue("@Observation", this.Observation);
                cmd.Parameters.AddWithValue("@RaisonSociale", this.RaisonSociale);
                cmd.Parameters.AddWithValue("@CreePar", this.CreePar);
                cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);
                cmd.Parameters.AddWithValue("@BTransfertAvoir", this.BTransfertAvoir);
                cmd.Parameters.AddWithValue("@BRetourAnterieur", this.BRetourAnterieur);
                cmd.Parameters.AddWithValue("@Exercice", DateTime.Now.Year.ToString());
                cmd.Parameters.AddWithValue("@NFactureAnt", this.NFactureAnt);

                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        this.NBonRetour = dr["NBonRetour"].ToString();
                    }
                }
                BonSortie bonSortie = new BonSortie();
                bonSortie.CClient = this.CFournisseur;
                bonSortie.CEntrepot = this.CEntrepot;
                bonSortie.CreePar = this.ModifiePar;
                bonSortie.DateSortie = (DateTime)this.DateRetour;
                bonSortie.Exercice = DateTime.Now.Year.ToString();
                bonSortie.NDocumentSource = this.NBonReception;
                bonSortie.PCInsertion = this.PCModification;
                bonSortie.RaisonSociale = this.RaisonSociale;
                bonSortie.TypeMouvement = StockHelper.TypesMouvementStock.BS_BONRETOURFOURNISSEUR.ToString();
                int i = 1;
                foreach (Achat_BonRetourDetail detail in this.BonRetourDetailCollection)
                {
                    detail.NBonRetour = this.NBonRetour;
                    detail.Ordre = i++;
                    detail.Sauvegarder(transaction);
                    if (!this.BRetourAnterieur)
                    {
                        AchatHelper.MiseAJourStockEnCommandeFnr(detail.CArticle, detail.Quantite, transaction);
                        AchatHelper.MiseAJourBonReceptionQteHist(this.CEntrepot, this.NBonReception, detail.CArticle, detail.OrdreBonReception, detail.Quantite, transaction);
                    }
                    BonSortieDetail detailBS = new BonSortieDetail();
                    detailBS.CArticle = detail.CArticle;
                    detailBS.CEntrepot = detail.CEntrepot;
                    detailBS.CUnite = detail.CUnite;
                    detailBS.LibArticle = detail.LibArticle;
                    detailBS.MontantTaxe = detail.MontantTaxe;
                    detailBS.PrixHT = detail.PrixHT;
                    detailBS.Quantite = detail.Quantite;
                    detailBS.TauxTVA = detail.TauxTVA;
                    detailBS.CreePar = this.CreePar;
                    detailBS.PCInsertion = this.PCInsertion;
                    bonSortie.BonSortieDetailCollection.Add(detailBS);
                }
                bonSortie.Inserer(transaction);

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
                cmd.CommandText = "Achat_BonRetour_Modifier";
                cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
                cmd.Parameters.AddWithValue("@CFournisseur", this.CFournisseur);
                cmd.Parameters.AddWithValue("@NBonRetour", this.NBonRetour);
                cmd.Parameters.AddWithValue("@BExonoreTVA ", this.BExonoreTVA);
                cmd.Parameters.AddWithValue("@BExonoreFodec", this.BExonoreFodec);
                cmd.Parameters.AddWithValue("@BTransfertAvoir", this.BTransfertAvoir);
                cmd.Parameters.AddWithValue("@BRetourAnterieur", this.BRetourAnterieur);
                cmd.Parameters.AddWithValue("@DateRetour ", this.DateRetour);
                cmd.Parameters.AddWithValue("@NFactureAnt", this.NFactureAnt);
                cmd.Parameters.AddWithValue("@NAvoir", this.NAvoir);
                cmd.Parameters.AddWithValue("@NBonReception", this.NBonReception);
                cmd.Parameters.AddWithValue("@Observation", this.Observation);
                cmd.Parameters.AddWithValue("@RaisonSociale", this.RaisonSociale);
                cmd.Parameters.AddWithValue("@MontantHT", this.MontantHT);
                cmd.Parameters.AddWithValue("@MontantRemise", this.MontantRemise);
                cmd.Parameters.AddWithValue("@MontantTaxe", this.MontantTaxe);
                cmd.Parameters.AddWithValue("@MontantTTC", this.MontantTTC);
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
                foreach (Achat_BonRetourDetail bonRetourDetail in BonRetourDetailCollection)
                {
                    bonRetourDetail.NBonRetour = this.NBonRetour;
                    bonRetourDetail.Ordre = i++;
                    bonRetourDetail.Sauvegarder(transaction);
                }
                foreach (Achat_BonRetourTaxe bonRetourTaxe in BonRetourTaxeCollection)
                {
                    bonRetourTaxe.NBonRetour = this.NBonRetour;
                    bonRetourTaxe.Sauvegarder(transaction);
                }

                this.CreeMouvement(transaction);
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
                cmd.CommandText = "Achat_BonRetour_SupprimerTaxes";
                cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
                cmd.Parameters.AddWithValue("@NBonRetour", this.NBonRetour);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
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
                cmd.CommandText = "Achat_BonRetour_SupprimerDetails";
                cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
                cmd.Parameters.AddWithValue("@NBonRetour", this.NBonRetour);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        private void CreeMouvement(SqlTransaction transaction)
        {
            Achat_BonRetour ancienBonRetour = Achat_BonRetour.Charger(this.NBonRetour, this.CEntrepot);
            Achat_BonRetourDetailCollection detailCollection = this.BonRetourDetailCollection;
            BonEntree bonEntree = new BonEntree();
            bonEntree.BFodecExonore = this.BExonoreFodec;
            bonEntree.BTvaExonore = this.BExonoreTVA;
            bonEntree.CFournisseur = this.CFournisseur;
            bonEntree.CEntrepot = this.CEntrepot;
            bonEntree.CreePar = this.ModifiePar;
            bonEntree.PCInsertion = this.PCModification;
            bonEntree.DateEntree = (DateTime)this.DateRetour;
            bonEntree.Exercice = DateTime.Now.Year.ToString();
            bonEntree.NDocumentSource = this.NBonRetour;
            bonEntree.RaisonSociale = this.RaisonSociale;
            bonEntree.TypeMouvement = StockHelper.TypesMouvementStock.BE_BONRETOURFOURNISSEUR.ToString();

            BonSortie bonSortie = new BonSortie();
            //bonSortie.CChauffeur = this.Chauffeur;
            bonSortie.CClient = this.CFournisseur;
            bonSortie.CEntrepot = this.CEntrepot;
            bonSortie.CreePar = this.ModifiePar;
            bonSortie.DateSortie = (DateTime)this.DateRetour;
            bonSortie.Exercice = DateTime.Now.Year.ToString();
            bonSortie.NDocumentSource = this.NBonRetour;
            bonSortie.PCInsertion = this.PCModification;
            bonSortie.RaisonSociale = this.RaisonSociale;
            bonSortie.TypeMouvement = StockHelper.TypesMouvementStock.BS_BONRETOURFOURNISSEUR.ToString();

            foreach (Achat_BonRetourDetail detail in this.BonRetourDetailCollection)
            {
                Achat_BonRetourDetail ancienDetail = ancienBonRetour.BonRetourDetailCollection.Find(x => x.CArticle == detail.CArticle && x.Ordre == detail.Ordre && x.CEntrepot == detail.CEntrepot);
                if (ancienDetail != null)
                {
                    if (detail.Quantite > ancienDetail.Quantite)
                    {
                        BonSortieDetail detailBS = new BonSortieDetail();
                        detailBS.CArticle = detail.CArticle;
                        detailBS.CEntrepot = detail.CEntrepot;
                        detailBS.CUnite = detail.CUnite;
                        detailBS.LibArticle = detail.LibArticle;
                        detailBS.MontantTaxe = detail.MontantTaxe;
                        detailBS.PrixHT = detail.PrixHT;
                        detailBS.Quantite = detail.Quantite - ancienDetail.Quantite;
                        detailBS.TauxTVA = detail.TauxTVA;
                        detailBS.CreePar = this.ModifiePar;
                        detailBS.PCInsertion = this.PCModification;
                        bonSortie.BonSortieDetailCollection.Add(detailBS);

                        if (!this.BRetourAnterieur)
                        {
                            AchatHelper.MiseAJourStockEnCommandeFnr(detail.CArticle, detailBS.Quantite, transaction);
                            AchatHelper.MiseAJourBonReceptionQteHist(this.CEntrepot, this.NBonReception, detail.CArticle, detail.OrdreBonReception, detailBS.Quantite, transaction);
                        }
                    }
                    if (detail.Quantite < ancienDetail.Quantite)
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
                        detailBE.Quantite = ancienDetail.Quantite - detail.Quantite;
                        detailBE.TauxTVA = detail.TauxTVA;
                        detailBE.CreePar = this.ModifiePar;
                        detailBE.PCInsertion = this.PCModification;
                        bonEntree.BonEntreeDetailCollection.Add(detailBE);
                        if (!this.BRetourAnterieur)
                        {
                            AchatHelper.MiseAJourStockEnCommandeFnr(detail.CArticle, -detailBE.Quantite, transaction);
                            AchatHelper.MiseAJourBonReceptionQteHist(this.CEntrepot, this.NBonReception, detail.CArticle, detail.OrdreBonReception, -detailBE.Quantite, transaction);
                        }
                    }
                    ancienBonRetour.BonRetourDetailCollection.Remove(ancienDetail);
                    detailCollection.Remove(detail);
                }
            }
            foreach (Achat_BonRetourDetail detail in detailCollection)
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
                if (!this.BRetourAnterieur)
                {
                    AchatHelper.MiseAJourStockEnCommandeFnr(detail.CArticle, detailBS.Quantite, transaction);
                    AchatHelper.MiseAJourBonReceptionQteHist(this.CEntrepot, this.NBonReception, detail.CArticle, detail.OrdreBonReception, detailBS.Quantite, transaction);
                }
            }

            foreach (Achat_BonRetourDetail detail in ancienBonRetour.BonRetourDetailCollection)
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
                if (!this.BRetourAnterieur)
                {
                    AchatHelper.MiseAJourStockEnCommandeFnr(detail.CArticle, -detailBE.Quantite, transaction);
                    AchatHelper.MiseAJourBonReceptionQteHist(this.CEntrepot, this.NBonReception, detail.CArticle, detail.OrdreBonReception, -detailBE.Quantite, transaction);
                }
            }
            if (bonSortie.BonSortieDetailCollection.Count > 0)
                bonSortie.Inserer();
            if (bonEntree.BonEntreeDetailCollection.Count > 0)
                bonEntree.Inserer();

        }

        public static Achat_BonRetour Charger(string nBonRetour, string cEntrepot)
        {
            Achat_BonRetour bonRetour = null;
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
                    cmd.CommandText = "Achat_BonRetour_Charger";
                    cmd.Parameters.AddWithValue("@NBonRetour", nBonRetour);
                    cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            bonRetour = new Achat_BonRetour();
                            bonRetour.NBonRetour = dr["NBonRetour"].ToString();
                            bonRetour.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["CFournisseur"] != DBNull.Value)
                                bonRetour.CFournisseur = dr["CFournisseur"].ToString();
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
                            if (dr["NBonReception"] != DBNull.Value)
                                bonRetour.NBonReception = dr["NBonReception"].ToString();
                            if (dr["NFactureAnt"] != DBNull.Value)
                                bonRetour.NFactureAnt = dr["NFactureAnt"].ToString();
                            if (dr["Indice"] != DBNull.Value)
                                bonRetour.Indice = int.Parse(dr["Indice"].ToString());
                            if (dr["MontantHT"] != DBNull.Value)
                                bonRetour.MontantHT = decimal.Parse(dr["MontantHT"].ToString());
                            if (dr["MontantRemise"] != DBNull.Value)
                                bonRetour.MontantRemise = decimal.Parse(dr["MontantRemise"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                bonRetour.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["MontantTTC"] != DBNull.Value)
                                bonRetour.MontantTTC = decimal.Parse(dr["MontantTTC"].ToString());
                            if (dr["BExonoreTVA"] != DBNull.Value)
                                bonRetour.BExonoreTVA = bool.Parse(dr["BExonoreTVA"].ToString());
                            if (dr["BExonoreFodec"] != DBNull.Value)
                                bonRetour.BExonoreFodec = bool.Parse(dr["BExonoreFodec"].ToString());
                            if (dr["BRetourAnterieur"] != DBNull.Value)
                                bonRetour.BRetourAnterieur = bool.Parse(dr["BRetourAnterieur"].ToString());
                            if (dr["BTransfertAvoir"] != DBNull.Value)
                                bonRetour.BTransfertAvoir = bool.Parse(dr["BTransfertAvoir"].ToString());
                            if (dr["CreePar"] != DBNull.Value)
                                bonRetour.CreePar = int.Parse(dr["CreePar"].ToString());
                            if (dr["ModifiePar"] != DBNull.Value)
                                bonRetour.ModifiePar = int.Parse(dr["ModifiePar"].ToString());
                            if (dr["PCModification"] != DBNull.Value)
                                bonRetour.PCModification = dr["PCModification"].ToString();
                            if (dr["PCInsertion"] != DBNull.Value)
                                bonRetour.PCInsertion = dr["PCInsertion"].ToString();
                            bonRetour.BonRetourDetailCollection = Achat_BonRetourDetailCollection.Charger(bonRetour.NBonRetour, bonRetour.CEntrepot);
                            bonRetour.BonRetourTaxeCollection = Achat_BonRetourTaxeCollection.Charger(bonRetour.NBonRetour, bonRetour.CEntrepot);
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

    public class Achat_BonRetourCollection : List<Achat_BonRetour>
    {
        public static Achat_BonRetourCollection Charger(string nBonRetour, string cEntrepot)
        {
            Achat_BonRetourCollection bonRetourCollection = new Achat_BonRetourCollection();
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
                    cmd.CommandText = "Achat_BonRetour_Charger";
                    cmd.Parameters.AddWithValue("@NBonRetour", nBonRetour);
                    cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Achat_BonRetour bonRetour = new Achat_BonRetour();
                            bonRetour.NBonRetour = dr["NBonRetour"].ToString();
                            bonRetour.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["CFournisseur"] != DBNull.Value)
                                bonRetour.CFournisseur = dr["CFournisseur"].ToString();
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
                            if (dr["NBonReception"] != DBNull.Value)
                                bonRetour.NBonReception = dr["NBonReception"].ToString();
                            if (dr["NFactureAnt"] != DBNull.Value)
                                bonRetour.NFactureAnt = dr["NFactureAnt"].ToString();
                            if (dr["Indice"] != DBNull.Value)
                                bonRetour.Indice = int.Parse(dr["Indice"].ToString());
                            if (dr["MontantHT"] != DBNull.Value)
                                bonRetour.MontantHT = decimal.Parse(dr["MontantHT"].ToString());
                            if (dr["MontantRemise"] != DBNull.Value)
                                bonRetour.MontantRemise = decimal.Parse(dr["MontantRemise"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                bonRetour.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["MontantTTC"] != DBNull.Value)
                                bonRetour.MontantTTC = decimal.Parse(dr["MontantTTC"].ToString());
                            if (dr["BExonoreTVA"] != DBNull.Value)
                                bonRetour.BExonoreTVA = bool.Parse(dr["BExonoreTVA"].ToString());
                            if (dr["BExonoreFodec"] != DBNull.Value)
                                bonRetour.BExonoreFodec = bool.Parse(dr["BExonoreFodec"].ToString());
                            if (dr["BRetourAnterieur"] != DBNull.Value)
                                bonRetour.BRetourAnterieur = bool.Parse(dr["BRetourAnterieur"].ToString());
                            if (dr["BTransfertAvoir"] != DBNull.Value)
                                bonRetour.BTransfertAvoir = bool.Parse(dr["BTransfertAvoir"].ToString());
                            if (dr["CreePar"] != DBNull.Value)
                                bonRetour.CreePar = int.Parse(dr["CreePar"].ToString());
                            if (dr["ModifiePar"] != DBNull.Value)
                                bonRetour.ModifiePar = int.Parse(dr["ModifiePar"].ToString());
                            if (dr["PCModification"] != DBNull.Value)
                                bonRetour.PCModification = dr["PCModification"].ToString();
                            if (dr["PCInsertion"] != DBNull.Value)
                                bonRetour.PCInsertion = dr["PCInsertion"].ToString();
                            bonRetour.BonRetourDetailCollection = Achat_BonRetourDetailCollection.Charger(bonRetour.NBonRetour, bonRetour.CEntrepot);
                            bonRetour.BonRetourTaxeCollection = Achat_BonRetourTaxeCollection.Charger(bonRetour.NBonRetour, bonRetour.CEntrepot);
                            bonRetourCollection.Add(bonRetour);
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }
                return (bonRetourCollection);
            }
        }

        public static Achat_BonRetourCollection ChargerParBonReception(string nBonReception, string cEntrepot)
        {
            Achat_BonRetourCollection bonRetourCollection = new Achat_BonRetourCollection();
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
                    cmd.CommandText = "Achat_BonRetour_ChargerParBonReception";
                    cmd.Parameters.AddWithValue("@NBonReception", nBonReception);
                    cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Achat_BonRetour bonRetour = new Achat_BonRetour();
                            bonRetour.NBonRetour = dr["NBonRetour"].ToString();
                            bonRetour.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["CFournisseur"] != DBNull.Value)
                                bonRetour.CFournisseur = dr["CFournisseur"].ToString();
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
                            if (dr["NBonReception"] != DBNull.Value)
                                bonRetour.NBonReception = dr["NBonReception"].ToString();
                            if (dr["NFactureAnt"] != DBNull.Value)
                                bonRetour.NFactureAnt = dr["NFactureAnt"].ToString();
                            if (dr["Indice"] != DBNull.Value)
                                bonRetour.Indice = int.Parse(dr["Indice"].ToString());
                            if (dr["MontantHT"] != DBNull.Value)
                                bonRetour.MontantHT = decimal.Parse(dr["MontantHT"].ToString());
                            if (dr["MontantRemise"] != DBNull.Value)
                                bonRetour.MontantRemise = decimal.Parse(dr["MontantRemise"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                bonRetour.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["MontantTTC"] != DBNull.Value)
                                bonRetour.MontantTTC = decimal.Parse(dr["MontantTTC"].ToString());
                            if (dr["BExonoreTVA"] != DBNull.Value)
                                bonRetour.BExonoreTVA = bool.Parse(dr["BExonoreTVA"].ToString());
                            if (dr["BExonoreFodec"] != DBNull.Value)
                                bonRetour.BExonoreFodec = bool.Parse(dr["BExonoreFodec"].ToString());
                            if (dr["BRetourAnterieur"] != DBNull.Value)
                                bonRetour.BRetourAnterieur = bool.Parse(dr["BRetourAnterieur"].ToString());
                            if (dr["BTransfertAvoir"] != DBNull.Value)
                                bonRetour.BTransfertAvoir = bool.Parse(dr["BTransfertAvoir"].ToString());
                            if (dr["CreePar"] != DBNull.Value)
                                bonRetour.CreePar = int.Parse(dr["CreePar"].ToString());
                            if (dr["ModifiePar"] != DBNull.Value)
                                bonRetour.ModifiePar = int.Parse(dr["ModifiePar"].ToString());
                            if (dr["PCModification"] != DBNull.Value)
                                bonRetour.PCModification = dr["PCModification"].ToString();
                            if (dr["PCInsertion"] != DBNull.Value)
                                bonRetour.PCInsertion = dr["PCInsertion"].ToString();
                            bonRetour.BonRetourDetailCollection = Achat_BonRetourDetailCollection.Charger(bonRetour.NBonRetour, bonRetour.CEntrepot);
                            bonRetour.BonRetourTaxeCollection = Achat_BonRetourTaxeCollection.Charger(bonRetour.NBonRetour, bonRetour.CEntrepot);
                            bonRetourCollection.Add(bonRetour);
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }
                return (bonRetourCollection);
            }
        }
    }
}
