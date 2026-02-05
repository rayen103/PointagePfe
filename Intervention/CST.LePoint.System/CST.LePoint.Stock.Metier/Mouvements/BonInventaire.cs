using CST.Stock.Metier.Mouvements;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace CST.LePoint.Stock.Metier
{
    [Serializable]
    public class BonInventaire
    {
        [XmlAttribute("NBonInventaire")]
        [Bindable(true)]
        public string NBonInventaire { get; set; }

        [XmlAttribute("CEntrepot")]
        [Bindable(true)]
        public string CEntrepot { get; set; }

        [XmlAttribute("BValidation")]
        [Bindable(true)]
        public bool BValidation { get; set; }

        [XmlAttribute("Observation")]
        [Bindable(true)]
        public string Observation { get; set; }

        [XmlAttribute("Indice")]
        [Bindable(true)]
        public int Indice { get; set; }

        [XmlAttribute("BInventaireFinAnnee")]
        [Bindable(true)]
        public bool BInventaireFinAnnee { get; set; }

        [XmlAttribute("BCloture")]
        [Bindable(true)]
        public bool BCloture { get; set; }

        [XmlAttribute("Exercice ")]
        [Bindable(true)]
        public string Exercice { get; set; }

        [XmlAttribute("DateInventaire")]
        [Bindable(true)]
        public DateTime DateInventaire { get; set; }

        [XmlAttribute("DateInsertion")]
        [Bindable(true)]
        public DateTime DateInsertion { get; set; }

        [XmlAttribute("PCInsertion")]
        [Bindable(true)]
        public string PCInsertion { get; set; }

        [XmlAttribute("CreePar")]
        [Bindable(true)]
        public int CreePar { get; set; }

        [XmlAttribute("DateModification")]
        [Bindable(true)]
        public DateTime DateModification { get; set; }

        [XmlAttribute("PCModification")]
        [Bindable(true)]
        public string PCModification { get; set; }

        [XmlAttribute("ModifiePar")]
        [Bindable(true)]
        public int ModifiePar { get; set; }

        [XmlAttribute("CReleveur")]
        [Bindable(true)]
        public string CReleveur { get; set; }

        public BonInventaireDetailCollection BonInventaireDetailCollection;

        public BonInventaire()
        {
            this.CEntrepot = string.Empty;
            this.NBonInventaire = string.Empty;
            this.BonInventaireDetailCollection = new BonInventaireDetailCollection();
        }

        public void Inserer()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString2"].ConnectionString))
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
            catch (Exception ex)
            {
                throw ex;
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
                cmd.CommandText = "BonInventaire_Inserer";

                cmd.Parameters.AddWithValue("@CEntrepot", CEntrepot);
                cmd.Parameters.AddWithValue("@DateInventaire", DateInventaire);
                cmd.Parameters.AddWithValue("@BInventaireFinAnnee", BInventaireFinAnnee);
                cmd.Parameters.AddWithValue("@BValidation", BValidation);
                cmd.Parameters.AddWithValue("@Observation", Observation);
                cmd.Parameters.AddWithValue("@Exercice ", Exercice);
                cmd.Parameters.AddWithValue("@PCInsertion", PCInsertion);
                cmd.Parameters.AddWithValue("@CreePar", CreePar);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                cmd.Parameters.AddWithValue("@CReleveur", CReleveur);

                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        this.NBonInventaire = dr["NBonInventaire"].ToString();
                        this.Indice = int.Parse(dr["DernierIndice"].ToString());
                    }
                }
                int k = 0;
                foreach (BonInventaireDetail bonInventaireDetail in this.BonInventaireDetailCollection)
                {
                    k += 1;
                    bonInventaireDetail.NBonInventaire = this.NBonInventaire;
                    bonInventaireDetail.Ordre = k;
                    bonInventaireDetail.Sauvegarder(transaction);
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
                    this.Supprimer(transaction);
                    this.Modifier(transaction);
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
                cmd.CommandText = "BonInventaire_Modifier";

                cmd.Parameters.AddWithValue("@NBonInventaire", this.NBonInventaire);
                cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
                cmd.Parameters.AddWithValue("@DateInventaire", this.DateInventaire);
                cmd.Parameters.AddWithValue("@BInventaireFinAnnee", this.BInventaireFinAnnee);
                cmd.Parameters.AddWithValue("@BValidation", this.BValidation);
                cmd.Parameters.AddWithValue("@Observation", this.Observation);
                cmd.Parameters.AddWithValue("@PCModification", this.PCModification);
                cmd.Parameters.AddWithValue("@ModifiePar", this.ModifiePar);
                cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                cmd.Parameters.AddWithValue("@CReleveur", CReleveur);

                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                cmd.ExecuteNonQuery();
                int compteur = 0;
                foreach (BonInventaireDetail bonInventaireDetail in this.BonInventaireDetailCollection)
                {
                    compteur += 1;
                    bonInventaireDetail.NBonInventaire = this.NBonInventaire;
                    bonInventaireDetail.Ordre = compteur;
                    bonInventaireDetail.Sauvegarder(transaction);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Supprimer(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BonInventaireDetail_Supprimer";
                cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
                cmd.Parameters.AddWithValue("@NBonInventaire", this.NBonInventaire);

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

        public void Valider()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    BonEntree entree = new BonEntree();
                    BonSortie sortie = new BonSortie();
                    BonInventaire bonInventaire = BonInventaire.Charger(this.NBonInventaire, this.CEntrepot);
                    BonInventaireDetailCollection boninventaireDetails = BonInventaireDetailCollection.Charger(this.NBonInventaire, this.CEntrepot);
                    PriseInventaire prise = PriseInventaire.ChargerParInv(this.CEntrepot, this.NBonInventaire);

                    foreach (BonInventaireDetail bonInventaireDetail in boninventaireDetails)
                    {
                        ////decimal difference = (decimal)(bonInventaireDetail.Quantite - bonInventaireDetail.QuantiteHisto);
                        decimal difference = decimal.Parse(bonInventaireDetail.Quantite.ToString()) - decimal.Parse(bonInventaireDetail.QuantiteHisto.ToString());
                        if (difference > 0)
                        {
                            BonEntreeDetail entreeDetail = new BonEntreeDetail();
                            entreeDetail.CEntrepot = this.CEntrepot;
                            entreeDetail.CArticle = bonInventaireDetail.CArticle;
                            entreeDetail.LibArticle = bonInventaireDetail.LibArticle;
                            entreeDetail.CUnite = bonInventaireDetail.CUnite;
                            entreeDetail.PrixRevient = bonInventaireDetail.PrixRevient;
                            entreeDetail.Quantite = difference;
                            if (prise != null)
                            {
                                List<PriseInventaireDetail> details = prise.PriseInventaireDetailCollection.FindAll(x => !string.IsNullOrWhiteSpace(x.CLot) && x.CArticle == entreeDetail.CArticle);
                                foreach (PriseInventaireDetail detail in details)
                                {
                                   // decimal stockReelLot = 0;
                                   // ArticleLotEntrepot articleLotEntrepot = ArticleLotEntrepot.Charger(detail.CArticle, detail.CLot, detail.CEntrepot);
                                   // if (articleLotEntrepot != null)
                                     //   stockReelLot = articleLotEntrepot.StockReel;
                                    BonEntreeDetailLot detailLot = new BonEntreeDetailLot();
                                    detailLot.CArticle = detail.CArticle;
                                    detailLot.CEntrepot = detail.CEntrepot;
                                    detailLot.LibArticle = detail.LibArticle;
                                    detailLot.CLot = detail.CLot;
                                    detailLot.CUnite = detail.CUnite;
                                    detailLot.Quantite = detail.QuantitePriseInv - detail.StockReelLot;
                                    entreeDetail.BonEntreeDetailLotCollection.Add(detailLot);
                                }
                            }
                            entree.BonEntreeDetailCollection.Add(entreeDetail);
                        }
                        else
                        {
                            if (difference < 0)
                            {
                                BonSortieDetail sortieDetail = new BonSortieDetail();
                                sortieDetail.CEntrepot = CEntrepot;
                                sortieDetail.CArticle = bonInventaireDetail.CArticle;
                                sortieDetail.LibArticle = bonInventaireDetail.LibArticle;
                                sortieDetail.CUnite = bonInventaireDetail.CUnite;
                                sortieDetail.PrixHT = bonInventaireDetail.PrixHT;
                                sortieDetail.Quantite = difference * -1;
                                
                                if (prise != null) 
                                {
                                    List<PriseInventaireDetail> details = prise.PriseInventaireDetailCollection.FindAll(x => !string.IsNullOrWhiteSpace(x.CLot) && x.CArticle == sortieDetail.CArticle);
                                    foreach (PriseInventaireDetail detail in details)
                                    {
                                       // decimal stockReelLot = 0;
                                       // ArticleLotEntrepot articleLotEntrepot = ArticleLotEntrepot.Charger(detail.CArticle, detail.CLot, detail.CEntrepot);
                                       // if (articleLotEntrepot != null)
                                        //    stockReelLot = articleLotEntrepot.StockReel;
                                        BonSortieDetailLot detailLot = new BonSortieDetailLot();
                                        detailLot.CArticle = detail.CArticle;
                                        detailLot.CEntrepot = detail.CEntrepot;
                                        detailLot.LibArticle = detail.LibArticle;
                                        detailLot.CLot = detail.CLot;
                                        detailLot.CUnite = detail.CUnite;
                                        detailLot.Quantite = (detail.QuantitePriseInv - detail.StockReelLot)* -1 ;
                                        sortieDetail.BonSortieDetailLotCollection.Add(detailLot);
                                    }
                                }
                                sortie.BonSortieDetailCollection.Add(sortieDetail);
                                
                            }
                        }
                    }

                    if (entree.BonEntreeDetailCollection.Count != 0)
                    {
                        entree.CEntrepot = CEntrepot;
                        entree.NDocumentSource = bonInventaire.NBonInventaire;
                        entree.TypeMouvement = StockHelper.TypesMouvementStock.BE_INVENTAIRE.ToString();
                        entree.DateEntree = bonInventaire.DateInventaire;
                        entree.Exercice = DateTime.Now.Year.ToString(); ;
                        entree.Inserer(transaction);
                    }
                    if (sortie.BonSortieDetailCollection.Count != 0)
                    {
                        sortie.CEntrepot = bonInventaire.CEntrepot;
                        sortie.NDocumentSource = bonInventaire.NBonInventaire;
                        sortie.DateSortie = bonInventaire.DateInventaire;
                        sortie.Exercice = DateTime.Now.Year.ToString();
                        sortie.TypeMouvement = StockHelper.TypesMouvementStock.BS_INVENTAIRE.ToString();
                        sortie.Inserer(transaction);
                    }

                    if (sortie.BonSortieDetailCollection.Count != 0 || entree.BonEntreeDetailCollection.Count != 0)
                    {
                        SqlCommand cmd5 = new SqlCommand();
                        cmd5.Transaction = transaction;
                        cmd5.Connection = transaction.Connection;

                        cmd5.CommandType = CommandType.StoredProcedure;
                        cmd5.CommandText = "BonInventaire_Valider";
                        cmd5.Parameters.AddWithValue("@CEntrepot", bonInventaire.CEntrepot);
                        cmd5.Parameters.AddWithValue("@NBonInventaire", bonInventaire.NBonInventaire);
                        cmd5.ExecuteNonQuery();
                    }

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
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "BonInventaire_Supprimer";
                    cmd.Parameters.AddWithValue("@NBonInventaire ", NBonInventaire);
                    cmd.Parameters.AddWithValue("@CEntrepot", CEntrepot);
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

        public static string RecupererNumeroBonInventaire(string exercice, string cEntrepot, out int indice)
        {
            string nBonInventaire = string.Empty;
            indice = 0;

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = cn;
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.CommandText = "BonInventaire_RecupererNouveauNumero";
                cmd2.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                cmd2.Parameters.AddWithValue("@Exercice", exercice);
                SqlDataReader dr = cmd2.ExecuteReader();
                if (dr.Read())
                {
                    nBonInventaire = dr["NBonInventaire"].ToString();
                    indice = int.Parse(dr["DernierIndice"].ToString());
                }
                dr.Close();
            }

            return nBonInventaire;
        }

        public static string RecupererNumeroBonInventaire(string exercice, string cEntrepot)
        {
            int indice = 0;
            return BonInventaire.RecupererNumeroBonInventaire(exercice, cEntrepot, out indice);
        }

        public static BonInventaire Charger(string nBonInventaire, string cEntrepot, string cArticle, string cCategorie, string cFamille, string cType, string cNature, string cModele, string cSousModele1, string cSousModele2)
        {
            BonInventaire bonInventaire = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "BonInventaire_Charger";
                    cmd.Parameters.AddWithValue("@NBonInventaire", nBonInventaire);
                    cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            bonInventaire = new BonInventaire();

                            bonInventaire.NBonInventaire = dr["NBonInventaire"].ToString();
                            bonInventaire.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["CReleveur"] != DBNull.Value)
                                bonInventaire.CReleveur = dr["CReleveur"].ToString();
                            if (dr["Observation"] != DBNull.Value)
                                bonInventaire.Observation = dr["Observation"].ToString();
                            if (dr["DateInventaire"] != DBNull.Value)
                                bonInventaire.DateInventaire = DateTime.Parse(dr["DateInventaire"].ToString());
                            if (dr["BValidation"] != DBNull.Value)
                                bonInventaire.BValidation = bool.Parse(dr["BValidation"].ToString());
                            if (dr["BCloture"] != DBNull.Value)
                                bonInventaire.BCloture = bool.Parse(dr["BCloture"].ToString());
                            if (dr["BInventaireFinAnnee"] != DBNull.Value)
                                bonInventaire.BInventaireFinAnnee = bool.Parse(dr["BInventaireFinAnnee"].ToString());
                            if (dr["Indice"] != DBNull.Value)
                                bonInventaire.Indice = int.Parse(dr["Indice"].ToString());

                            bonInventaire.BonInventaireDetailCollection = BonInventaireDetailCollection.Charger(nBonInventaire, cEntrepot, cArticle, cCategorie, cFamille, cType, cNature, cModele, cSousModele1, cSousModele2);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bonInventaire;
        }

        public static BonInventaire Charger(string nBonInventaire, string cEntrepot)
        {
            BonInventaire bonInventaire = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "BonInventaire_Charger";
                    cmd.Parameters.AddWithValue("@NBonInventaire", nBonInventaire);
                    cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            bonInventaire = new BonInventaire();

                            bonInventaire.NBonInventaire = dr["NBonInventaire"].ToString();
                            bonInventaire.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["CReleveur"] != DBNull.Value)
                                bonInventaire.CReleveur = dr["CReleveur"].ToString();
                            if (dr["Observation"] != DBNull.Value)
                                bonInventaire.Observation = dr["Observation"].ToString();
                            if (dr["DateInventaire"] != DBNull.Value)
                                bonInventaire.DateInventaire = DateTime.Parse(dr["DateInventaire"].ToString());
                            if (dr["BValidation"] != DBNull.Value)
                                bonInventaire.BValidation = bool.Parse(dr["BValidation"].ToString());
                            if (dr["BCloture"] != DBNull.Value)
                                bonInventaire.BCloture = bool.Parse(dr["BCloture"].ToString());
                            if (dr["BInventaireFinAnnee"] != DBNull.Value)
                                bonInventaire.BInventaireFinAnnee = bool.Parse(dr["BInventaireFinAnnee"].ToString());
                            if (dr["Indice"] != DBNull.Value)
                                bonInventaire.Indice = int.Parse(dr["Indice"].ToString());

                            bonInventaire.BonInventaireDetailCollection = BonInventaireDetailCollection.Charger(bonInventaire.NBonInventaire, bonInventaire.CEntrepot);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bonInventaire;
        }

        public void InsererPrise(string nPrise1,string nPrise2, PriseInventaire prise3)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlTransaction transaction = cn.BeginTransaction();
                    try
                    {
                        prise3.Inserer(transaction);
                        if (this.BonInventaireDetailCollection.Count != 0)
                            InsererPrise(transaction);
                        prise3.InsererAnalysePrise(nPrise1, nPrise2, this.NBonInventaire, transaction);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsererPrise(string nPrise)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlTransaction transaction = cn.BeginTransaction();
                    try
                    {
                        InsererPrise(transaction);
                        PriseInsererNInventaire(nPrise, transaction);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void PriseInsererNInventaire(string nPrise,SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Prise_InsererNInventaire";

                cmd.Parameters.AddWithValue("@CEntrepot", CEntrepot);
                cmd.Parameters.AddWithValue("@NPrise ", nPrise);
                cmd.Parameters.AddWithValue("@NBonInventaire", NBonInventaire);

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

        public void InsererPrise(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BonInventaire_Inserer";

                cmd.Parameters.AddWithValue("@CEntrepot", CEntrepot);
                cmd.Parameters.AddWithValue("@DateInventaire", DateInventaire);
                cmd.Parameters.AddWithValue("@BInventaireFinAnnee", BInventaireFinAnnee);
                cmd.Parameters.AddWithValue("@BValidation", BValidation);
                cmd.Parameters.AddWithValue("@Observation", Observation);
                cmd.Parameters.AddWithValue("@Exercice ", Exercice);
                cmd.Parameters.AddWithValue("@PCInsertion", PCInsertion);
                cmd.Parameters.AddWithValue("@CreePar", CreePar);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                cmd.Parameters.AddWithValue("@CReleveur", CReleveur);

                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        this.NBonInventaire = dr["NBonInventaire"].ToString();
                        this.Indice = int.Parse(dr["DernierIndice"].ToString());
                    }
                }
                int k = 0;
                foreach (BonInventaireDetail bonInventaireDetail in this.BonInventaireDetailCollection)
                {
                    k += 1;
                    bonInventaireDetail.NBonInventaire = this.NBonInventaire;
                    bonInventaireDetail.Ordre = k;
                    bonInventaireDetail.Sauvegarder(transaction);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ModifieStockInitiale() 
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    foreach (BonInventaireDetail detail in this.BonInventaireDetailCollection)
                        detail.ModifierStockInitiale(transaction);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

    }

    [Serializable]
    public class BonInventaireCollection : List<BonInventaire>
    {
        public static DataSet ChargerVue(DateTime dateDebut, DateTime dateFin)
        {
            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BonInventaireListe_Rpt_Charger";
                cmd.Parameters.AddWithValue("@DateDeb", dateDebut);
                cmd.Parameters.AddWithValue("@DateFin", dateFin);
                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "BonInventaireListe_Rpt_Charger");
            }
            return (ds);
        }

        public static BonInventaireCollection Charger()
        {
            BonInventaireCollection bonInventaires = new BonInventaireCollection();
            BonInventaire bonInventaire = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "BonInventaire_Charger";
                    cmd.Parameters.AddWithValue("@NBonInventaire", DBNull.Value);
                    cmd.Parameters.AddWithValue("@CEntrepot", DBNull.Value);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            bonInventaire = new BonInventaire();

                            bonInventaire.NBonInventaire = dr["NBonInventaire"].ToString();
                            bonInventaire.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["CReleveur"] != DBNull.Value)
                                bonInventaire.CReleveur = dr["CReleveur"].ToString();
                            if (dr["Observation"] != DBNull.Value)
                                bonInventaire.Observation = dr["Observation"].ToString();
                            if (dr["DateInventaire"] != DBNull.Value)
                                bonInventaire.DateInventaire = DateTime.Parse(dr["DateInventaire"].ToString());
                            if (dr["BValidation"] != DBNull.Value)
                                bonInventaire.BValidation = bool.Parse(dr["BValidation"].ToString());
                            if (dr["BCloture"] != DBNull.Value)
                                bonInventaire.BCloture = bool.Parse(dr["BCloture"].ToString());
                            if (dr["BInventaireFinAnnee"] != DBNull.Value)
                                bonInventaire.BInventaireFinAnnee = bool.Parse(dr["BInventaireFinAnnee"].ToString());
                            if (dr["Indice"] != DBNull.Value)
                                bonInventaire.Indice = int.Parse(dr["Indice"].ToString());
                            bonInventaire.BonInventaireDetailCollection = BonInventaireDetailCollection.Charger(bonInventaire.NBonInventaire, bonInventaire.CEntrepot);
                            bonInventaires.Add(bonInventaire);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bonInventaires;
        }

        public static BonInventaireCollection ChargerInventaireDuJour(DateTime dateJour)
        {
            BonInventaireCollection bonInventaires = new BonInventaireCollection();
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "BonInventaire_ChargerInventaireDuJour";
                    cmd.Parameters.AddWithValue("@DateDuJour", dateJour);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
              

                             BonInventaire bonInventaire = new BonInventaire();

                            bonInventaire.NBonInventaire = dr["NBonInventaire"].ToString();
                            bonInventaire.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["CReleveur"] != DBNull.Value)
                                bonInventaire.CReleveur = dr["CReleveur"].ToString();
                            if (dr["Observation"] != DBNull.Value)
                                bonInventaire.Observation = dr["Observation"].ToString();
                            if (dr["DateInventaire"] != DBNull.Value)
                                bonInventaire.DateInventaire = DateTime.Parse(dr["DateInventaire"].ToString());
                            if (dr["BValidation"] != DBNull.Value)
                                bonInventaire.BValidation = bool.Parse(dr["BValidation"].ToString());
                            if (dr["BCloture"] != DBNull.Value)
                                bonInventaire.BCloture = bool.Parse(dr["BCloture"].ToString());
                            if (dr["BInventaireFinAnnee"] != DBNull.Value)
                                bonInventaire.BInventaireFinAnnee = bool.Parse(dr["BInventaireFinAnnee"].ToString());
                            if (dr["Indice"] != DBNull.Value)
                                bonInventaire.Indice = int.Parse(dr["Indice"].ToString());

                            bonInventaire.BonInventaireDetailCollection = BonInventaireDetailCollection.Charger(bonInventaire.NBonInventaire, bonInventaire.CEntrepot);
                            bonInventaires.Add(bonInventaire);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bonInventaires;
        }
    }
}