using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml.Serialization;

namespace CST.LePoint.Stock.Metier
{
    [Serializable]
    public class BonLivraisonInterne
    {
        #region Propriétés

        [XmlAttribute("NBonLivraisonInterne")]
        [Bindable(true)]
        public string NBonLivraisonInterne { get; set; }

        [XmlAttribute("CChauffeur")]
        [Bindable(true)]
        public string CChauffeur { get; set; }

        [XmlAttribute("CEntrepotCible")]
        [Bindable(true)]
        public string CEntrepotCible { get; set; }

        [XmlAttribute("CEntrepotSource")]
        [Bindable(true)]
        public string CEntrepotSource { get; set; }

        [XmlAttribute("CVehicule")]
        [Bindable(true)]
        public string CVehicule { get; set; }

        [XmlAttribute("CMission")]
        [Bindable(true)]
        public string CMission { get; set; }

        [XmlAttribute("DateBonLivraisonInterne")]
        [Bindable(true)]
        public DateTime DateBonLivraisonInterne { get; set; }

        [XmlAttribute("CFournisseur")]
        [Bindable(true)]
        public string CFournisseur { get; set; }

        [XmlAttribute("Indice")]
        [Bindable(true)]
        public int Indice { get; set; }

        [XmlAttribute("Exercice")]
        [Bindable(true)]
        public string Exercice { get; set; }

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

        [XmlAttribute("NOrdredeTravail")]
        [Bindable(true)]
        public string NOrdredeTravail { get; set; }

        #endregion Propriétés

        public BonLivraisonInterneDetailCollection BonLivraisonInterneDetailCollection;

        public BonLivraisonInterne()
        {
            NBonLivraisonInterne = string.Empty;
            Exercice = DateTime.Now.Year.ToString();
            DateBonLivraisonInterne = DateTime.Now;
            BonLivraisonInterneDetailCollection = new BonLivraisonInterneDetailCollection();
        }

        public BonLivraisonInterne(string nBonLivraisonInterne)
        {
            NBonLivraisonInterne = nBonLivraisonInterne;
            DateBonLivraisonInterne = DateTime.Now;
            Exercice = DateTime.Now.Year.ToString();
            BonLivraisonInterneDetailCollection = new BonLivraisonInterneDetailCollection();
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
                cmd.CommandText = "BonLivraisonInterne_Inserer";
                cmd.Parameters.AddWithValue("@CChauffeur", CChauffeur);
                cmd.Parameters.AddWithValue("@CEntrepotCible", CEntrepotCible);
                cmd.Parameters.AddWithValue("@CEntrepotSource", CEntrepotSource);
                cmd.Parameters.AddWithValue("@CVehicule", CVehicule);
                cmd.Parameters.AddWithValue("@CMission", CMission);
                cmd.Parameters.AddWithValue("@DateBonLivraisonInterne", DateBonLivraisonInterne);
                cmd.Parameters.AddWithValue("@CFournisseur", CFournisseur);
                cmd.Parameters.AddWithValue("@Exercice", Exercice);
                cmd.Parameters.AddWithValue("@PCInsertion", PCInsertion);
                cmd.Parameters.AddWithValue("@CreePar", this.CreePar);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                cmd.Parameters.AddWithValue("@NOrdredeTravail", NOrdredeTravail);
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
                        NBonLivraisonInterne = dr["NBonLivraisonInterne"].ToString();
                        Indice = int.Parse(dr["DernierIndice"].ToString());
                    }
                }
                int i=0;
                foreach (BonLivraisonInterneDetail bonLivraisonInterneDetail in BonLivraisonInterneDetailCollection)
                {
                    bonLivraisonInterneDetail.NBonLivraisonInterne = NBonLivraisonInterne;
                    bonLivraisonInterneDetail.Ordre = i++;
                    bonLivraisonInterneDetail.Sauvegarder(transaction);
                }

                CreerBonEntree(transaction);
                CreerBonSortie(transaction);
            }

            catch (Exception)
            {
                throw;
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
                    cmd.CommandText = "update BonLivraisonInterne set NOrdredeTravail = '" + this.NOrdredeTravail + "' where NBonLivraisonInterne = '" + this.NBonLivraisonInterne + "'";
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
        public void Modifier()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    RestituerStock(transaction);
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
                cmd.CommandText = "BonLivraisonInterne_Modifier";
                cmd.Parameters.AddWithValue("@NBonLivraisonInterne", NBonLivraisonInterne);
                cmd.Parameters.AddWithValue("@CEntrepotCible", CEntrepotCible);
                cmd.Parameters.AddWithValue("@CEntrepotSource", CEntrepotSource);
                cmd.Parameters.AddWithValue("@CChauffeur", CChauffeur);
                cmd.Parameters.AddWithValue("@CVehicule", CVehicule);
                cmd.Parameters.AddWithValue("@CMission", CMission);
                cmd.Parameters.AddWithValue("@DateBonLivraisonInterne", DateBonLivraisonInterne);
                cmd.Parameters.AddWithValue("@CFournisseur", CFournisseur);
                cmd.Parameters.AddWithValue("@PCModification", PCModification);
                cmd.Parameters.AddWithValue("@ModifiePar", ModifiePar);
                cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                cmd.Parameters.AddWithValue("@NOrdredeTravail", NOrdredeTravail);
                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                SupprimerDetail(transaction);

                foreach (BonLivraisonInterneDetail bonLivraisonInterneDetail in BonLivraisonInterneDetailCollection)
                {
                    bonLivraisonInterneDetail.NBonLivraisonInterne = NBonLivraisonInterne;
                    bonLivraisonInterneDetail.Sauvegarder(transaction);
                }
                cmd.ExecuteNonQuery();
            }

            catch (Exception)
            {
                throw;
            }
        }

        private void SupprimerDetail(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BonLivraisonInterneDetail_Supprimer";
                cmd.Parameters.AddWithValue("@NBonLivraisonInterne", NBonLivraisonInterne);

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

        public void Supprimer()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    RestituerStock(transaction);
                    Supprimer(transaction);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void Supprimer(SqlTransaction transaction)
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();

                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Transaction = transaction;
                    cmd.Connection = transaction.Connection;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "BonLivraisonInterne_Supprimer";
                    cmd.Parameters.AddWithValue("@NBonLivraisonInterne", NBonLivraisonInterne);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public static string RecupererNumeroBonLivraisonInterne(string exercice, out int indice)
        {
            string nBonLivraisonInterne = string.Empty;
            indice = 0;

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = cn;

                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.CommandText = "BonLivraisonInterne_RecupererNouveauNumero";
                cmd2.Parameters.AddWithValue("@Exercice", exercice);
                SqlDataReader dr = cmd2.ExecuteReader();
                if (dr.Read())
                {
                    nBonLivraisonInterne = dr["NBonLivraisonInterne"].ToString();
                    indice = int.Parse(dr["DernierIndice"].ToString());
                }

                dr.Close();
            }

            return nBonLivraisonInterne;
        }

        public static string RecupererNumeroBonLivraisonInterne(string exercice)
        {
            int indice = 0;
            return BonLivraisonInterne.RecupererNumeroBonLivraisonInterne(exercice, out indice);
        }

        private void CreerBonEntree(SqlTransaction transaction)
        {
            try
            {
                BonEntreeDetail bonEntreeDetail = null;
                BonEntree bonEntree = new BonEntree();
                bonEntree.CEntrepot = CEntrepotCible;
                bonEntree.NDocumentSource = NBonLivraisonInterne;
                bonEntree.DateEntree = (DateTime)DateBonLivraisonInterne;
                bonEntree.TypeMouvement = StockHelper.TypesMouvementStock.BE_BONLIVRAISONINTERNE.ToString();
                bonEntree.Exercice = Exercice;

                foreach (BonLivraisonInterneDetail bonLivraisonInterneDetail in BonLivraisonInterneDetailCollection)
                {
                    bonEntreeDetail = new BonEntreeDetail();

                    bonEntreeDetail.CEntrepot = bonLivraisonInterneDetail.CEntrepotCible;
                    bonEntreeDetail.CArticle = bonLivraisonInterneDetail.CArticle;
                    bonEntreeDetail.Quantite = bonLivraisonInterneDetail.Quantite;
                    bonEntreeDetail.CUnite = bonLivraisonInterneDetail.CUnite;
                    bonEntreeDetail.LibArticle = bonLivraisonInterneDetail.LibArticle;
                    bonEntreeDetail.Ordre = bonLivraisonInterneDetail.Ordre;
                    bonEntreeDetail.TauxTVA = bonLivraisonInterneDetail.TauxTVA;

                    bonEntree.BonEntreeDetailCollection.Add(bonEntreeDetail);
                }

                bonEntree.Inserer(transaction);
            }

            catch (Exception)
            {
                throw;
            }
        }

        private void CreerBonSortie(SqlTransaction transaction)
        {
            try
            {
                BonSortieDetail bonSortieDetail = null;

                BonSortie bonSortie = new BonSortie();
                bonSortie.CEntrepot = CEntrepotSource;
                bonSortie.NDocumentSource = NBonLivraisonInterne;
                bonSortie.DateSortie = DateBonLivraisonInterne;
                bonSortie.Exercice = Exercice;
                bonSortie.TypeMouvement = StockHelper.TypesMouvementStock.BS_BONLIVRAISONINTERNE.ToString();

                foreach (BonLivraisonInterneDetail bonLivraisonInterneDetail in BonLivraisonInterneDetailCollection)
                {
                    bonSortieDetail = new BonSortieDetail();

                    bonSortieDetail.CEntrepot = CEntrepotSource;
                    bonSortieDetail.CArticle = bonLivraisonInterneDetail.CArticle;
                    bonSortieDetail.Quantite = bonLivraisonInterneDetail.Quantite;
                    bonSortieDetail.CUnite = bonLivraisonInterneDetail.CUnite;
                    bonSortieDetail.LibArticle = bonLivraisonInterneDetail.LibArticle;
                    bonSortieDetail.Ordre = bonLivraisonInterneDetail.Ordre;
                    bonSortieDetail.MontantTaxe = bonLivraisonInterneDetail.MontantTaxe;
                    bonSortieDetail.TauxTVA = bonLivraisonInterneDetail.TauxTVA;
                    bonSortie.BonSortieDetailCollection.Add(bonSortieDetail);
                }

                bonSortie.Inserer(transaction);
            }

            catch (Exception)
            {
                throw;
            }
        }

        public void RestituerStock(SqlTransaction transaction)
        {
            BonLivraisonInterneDetailCollection AncienneBLIDetailCollection = new BonLivraisonInterneDetailCollection();
            BonLivraisonInterneDetail bonLivraisonInterneDetail = null;

            try
            {
                SqlCommand cmdBonLivraisonInterne = new SqlCommand();
                cmdBonLivraisonInterne.Transaction = transaction;
                cmdBonLivraisonInterne.Connection = transaction.Connection;
                cmdBonLivraisonInterne.CommandType = CommandType.StoredProcedure;
                cmdBonLivraisonInterne.CommandText = "BonLivraisonInterneDetail_Charger";
                cmdBonLivraisonInterne.Parameters.AddWithValue("@NBonLivraisonInterne", NBonLivraisonInterne);
                foreach (SqlParameter parametre in cmdBonLivraisonInterne.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }

                using (SqlDataReader dr = cmdBonLivraisonInterne.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        bonLivraisonInterneDetail = new BonLivraisonInterneDetail(NBonLivraisonInterne);
                        bonLivraisonInterneDetail.NBonLivraisonInterne = NBonLivraisonInterne;
                        bonLivraisonInterneDetail.CArticle = dr["CArticle"].ToString();
                        bonLivraisonInterneDetail.CEntrepotCible = dr["CEntrepotCible"].ToString();
                        bonLivraisonInterneDetail.CEntrepotSource = dr["CEntrepotSource"].ToString();
                        bonLivraisonInterneDetail.CUnite = dr["CUnite"].ToString();
                        bonLivraisonInterneDetail.LibArticle = dr["LibArticle"].ToString();
                        bonLivraisonInterneDetail.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                        bonLivraisonInterneDetail.Ordre = int.Parse(dr["Ordre"].ToString());
                        bonLivraisonInterneDetail.PrixHTArticle = decimal.Parse(dr["PrixHTArticle"].ToString());
                        bonLivraisonInterneDetail.Quantite = decimal.Parse(dr["Quantite"].ToString());
                        bonLivraisonInterneDetail.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());
                        bonLivraisonInterneDetail.PourcentageRemise = decimal.Parse(dr["PourcentageRemise"].ToString());
                        AncienneBLIDetailCollection.Add(bonLivraisonInterneDetail);
                    }
                }

                BonEntree bonEntreeCible = new BonEntree();
                bonEntreeCible.CEntrepot = bonLivraisonInterneDetail.CEntrepotCible;
                bonEntreeCible.NDocumentSource = NBonLivraisonInterne;
                bonEntreeCible.TypeMouvement = StockHelper.TypesMouvementStock.BE_BONLIVRAISONINTERNE.ToString();
                bonEntreeCible.DateEntree = (DateTime)DateBonLivraisonInterne;
                bonEntreeCible.Exercice = Exercice;

                BonEntree bonEntreeSource = new BonEntree();
                bonEntreeSource.CEntrepot = bonLivraisonInterneDetail.CEntrepotSource;
                bonEntreeSource.NDocumentSource = NBonLivraisonInterne;
                bonEntreeSource.TypeMouvement = StockHelper.TypesMouvementStock.BE_BONLIVRAISONINTERNE.ToString();
                bonEntreeSource.DateEntree = (DateTime)DateBonLivraisonInterne;
                bonEntreeSource.Exercice = Exercice;

                BonSortie bonSortieSource = new BonSortie();
                bonSortieSource.CEntrepot = bonLivraisonInterneDetail.CEntrepotSource;
                bonSortieSource.NDocumentSource = NBonLivraisonInterne;
                bonSortieSource.TypeMouvement = StockHelper.TypesMouvementStock.BS_BONLIVRAISONINTERNE.ToString();
                bonSortieSource.CChauffeur = CChauffeur;
                bonSortieSource.CVehicule = CVehicule;
                bonSortieSource.CFournisseur = CFournisseur;
                bonSortieSource.DateSortie = DateBonLivraisonInterne;
                bonSortieSource.Exercice = Exercice;

                BonSortie bonSortieCible = new BonSortie();
                bonSortieCible.CEntrepot = bonLivraisonInterneDetail.CEntrepotCible;
                bonSortieCible.NDocumentSource = NBonLivraisonInterne;
                bonSortieCible.TypeMouvement = StockHelper.TypesMouvementStock.BS_BONLIVRAISONINTERNE.ToString();
                bonSortieCible.CChauffeur = CChauffeur;
                bonSortieCible.CVehicule = CVehicule;
                bonSortieCible.CFournisseur = CFournisseur;
                bonSortieCible.DateSortie = DateBonLivraisonInterne;
                bonSortieCible.Exercice = Exercice;

                foreach (BonLivraisonInterneDetail obj in AncienneBLIDetailCollection)
                {
                    var objModifie = this.BonLivraisonInterneDetailCollection.RecupererBonLivraisonInterneDetail(obj.NBonLivraisonInterne, obj.CArticle);
                    if (objModifie != null)
                    {
                        if (objModifie.Quantite > obj.Quantite)
                        {
                            BonEntreeDetail bonEntreeDetail = new BonEntreeDetail();
                            bonEntreeDetail.CEntrepot = objModifie.CEntrepotCible;
                            bonEntreeDetail.CArticle = objModifie.CArticle;
                            bonEntreeDetail.Quantite = objModifie.Quantite - obj.Quantite;
                            bonEntreeCible.BonEntreeDetailCollection.Add(bonEntreeDetail);

                            BonSortieDetail BonSortieDetail = new BonSortieDetail();
                            BonSortieDetail.CEntrepot = objModifie.CEntrepotSource;
                            BonSortieDetail.CArticle = objModifie.CArticle;
                            BonSortieDetail.Quantite = objModifie.Quantite - obj.Quantite;
                            bonSortieSource.BonSortieDetailCollection.Add(BonSortieDetail);
                        }
                        else
                        {
                            if (objModifie.Quantite < obj.Quantite)
                            {
                                BonSortieDetail BonSortieDetail = new BonSortieDetail();
                                BonSortieDetail.CEntrepot = objModifie.CEntrepotCible;
                                BonSortieDetail.CArticle = objModifie.CArticle;
                                BonSortieDetail.Quantite = obj.Quantite - objModifie.Quantite;
                                bonSortieCible.BonSortieDetailCollection.Add(BonSortieDetail);

                                BonEntreeDetail bonEntreeDetail = new BonEntreeDetail();
                                bonEntreeDetail.CEntrepot = objModifie.CEntrepotSource;
                                bonEntreeDetail.CArticle = objModifie.CArticle;
                                bonEntreeDetail.Quantite = obj.Quantite - objModifie.Quantite;
                                bonEntreeSource.BonEntreeDetailCollection.Add(bonEntreeDetail);
                            }
                        }
                    }
                    else
                    {
                        BonSortieDetail BonSortieDetail = new BonSortieDetail();
                        BonSortieDetail.CEntrepot = obj.CEntrepotCible;
                        BonSortieDetail.CArticle = obj.CArticle;
                        BonSortieDetail.Quantite = obj.Quantite;
                        bonSortieCible.BonSortieDetailCollection.Add(BonSortieDetail);

                        BonEntreeDetail bonEntreeDetail = new BonEntreeDetail();
                        bonEntreeDetail.CEntrepot = obj.CEntrepotSource;
                        bonEntreeDetail.CArticle = obj.CArticle;
                        bonEntreeDetail.Quantite = obj.Quantite;
                        bonEntreeSource.BonEntreeDetailCollection.Add(bonEntreeDetail);
                    }
                }

                foreach (BonLivraisonInterneDetail obj in AncienneBLIDetailCollection)
                {
                    var objAjoute = AncienneBLIDetailCollection.RecupererBonLivraisonInterneDetail(obj.NBonLivraisonInterne, obj.CArticle);
                    if (objAjoute == null)
                    {
                        BonEntreeDetail bonEntreeDetail = new BonEntreeDetail();
                        bonEntreeDetail.CEntrepot = obj.CEntrepotCible;
                        bonEntreeDetail.CArticle = obj.CArticle;
                        bonEntreeDetail.Quantite = obj.Quantite;
                        bonEntreeCible.BonEntreeDetailCollection.Add(bonEntreeDetail);

                        BonSortieDetail BonSortieDetail = new BonSortieDetail();
                        BonSortieDetail.CEntrepot = obj.CEntrepotSource;
                        BonSortieDetail.CArticle = obj.CArticle;
                        BonSortieDetail.Quantite = obj.Quantite;
                        bonSortieSource.BonSortieDetailCollection.Add(BonSortieDetail);
                    }
                }

                if (bonEntreeCible.BonEntreeDetailCollection.Count() > 0)
                {
                    bonEntreeCible.Inserer(transaction);
                }
                if (bonEntreeSource.BonEntreeDetailCollection.Count() > 0)
                {
                    bonEntreeSource.Inserer(transaction);
                }
                if (bonSortieSource.BonSortieDetailCollection.Count() > 0)
                {
                    bonSortieSource.Inserer(transaction);
                }
                if (bonSortieCible.BonSortieDetailCollection.Count() > 0)
                {
                    bonSortieCible.Inserer(transaction);
                }
            }

            catch (Exception)
            {
                throw;
            }
        }

        public static BonLivraisonInterne Charger(string nBonLivraisonInterne)
        {
            BonLivraisonInterne bonLivraisonInterne = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "BonLivraisonInterne_Charger";
                    cmd.Parameters.AddWithValue("@NBonLivraisonInterne", nBonLivraisonInterne);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            bonLivraisonInterne = new BonLivraisonInterne();
                            bonLivraisonInterne.NBonLivraisonInterne = dr["NBonLivraisonInterne"].ToString();
                            if (dr["CChauffeur"] != DBNull.Value)
                                bonLivraisonInterne.CChauffeur = dr["CChauffeur"].ToString();
                            if (dr["CFournisseur"] != DBNull.Value)
                                bonLivraisonInterne.CFournisseur = dr["CFournisseur"].ToString();
                            if (dr["CEntrepotCible"] != DBNull.Value)
                                bonLivraisonInterne.CEntrepotCible = dr["CEntrepotCible"].ToString();
                            if (dr["CEntrepotSource"] != DBNull.Value)
                                bonLivraisonInterne.CEntrepotSource = dr["CEntrepotSource"].ToString();
                            if (dr["CVehicule"] != DBNull.Value)
                                bonLivraisonInterne.CVehicule = dr["CVehicule"].ToString();
                            if (dr["CMission"] != DBNull.Value)
                                bonLivraisonInterne.CMission = dr["CMission"].ToString();
                            if (dr["DateBonLivraisonInterne"] != DBNull.Value)
                                bonLivraisonInterne.DateBonLivraisonInterne = DateTime.Parse(dr["DateBonLivraisonInterne"].ToString());
                            if (dr["Indice"] != DBNull.Value)
                                bonLivraisonInterne.Indice = int.Parse(dr["Indice"].ToString());
                            if (dr["NOrdredeTravail"] != DBNull.Value)
                                bonLivraisonInterne.NOrdredeTravail = dr["NOrdredeTravail"].ToString();
                            bonLivraisonInterne.BonLivraisonInterneDetailCollection = BonLivraisonInterneDetailCollection.Charger(nBonLivraisonInterne);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bonLivraisonInterne;
        }

        public static BonLivraisonInterne ChargerparOT(string p)
        {
            BonLivraisonInterne bonLivraisonInterne = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "BonLivraisonInterne_Charger";
                    cmd.Parameters.AddWithValue("@NOrdredeTravail", p);
                    cmd.Parameters.AddWithValue("@NBonLivraisonInterne", DBNull.Value);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            bonLivraisonInterne = new BonLivraisonInterne();
                            bonLivraisonInterne.NBonLivraisonInterne = dr["NBonLivraisonInterne"].ToString();
                            if (dr["CChauffeur"] != DBNull.Value)
                                bonLivraisonInterne.CChauffeur = dr["CChauffeur"].ToString();
                            if (dr["CFournisseur"] != DBNull.Value)
                                bonLivraisonInterne.CFournisseur = dr["CFournisseur"].ToString();
                            if (dr["CEntrepotCible"] != DBNull.Value)
                                bonLivraisonInterne.CEntrepotCible = dr["CEntrepotCible"].ToString();
                            if (dr["CEntrepotSource"] != DBNull.Value)
                                bonLivraisonInterne.CEntrepotSource = dr["CEntrepotSource"].ToString();
                            if (dr["CVehicule"] != DBNull.Value)
                                bonLivraisonInterne.CVehicule = dr["CVehicule"].ToString();
                            if (dr["CMission"] != DBNull.Value)
                                bonLivraisonInterne.CMission = dr["CMission"].ToString();
                            if (dr["DateBonLivraisonInterne"] != DBNull.Value)
                                bonLivraisonInterne.DateBonLivraisonInterne = DateTime.Parse(dr["DateBonLivraisonInterne"].ToString());
                            if (dr["Indice"] != DBNull.Value)
                                bonLivraisonInterne.Indice = int.Parse(dr["Indice"].ToString());
                            if (dr["NOrdredeTravail"] != DBNull.Value)
                                bonLivraisonInterne.NOrdredeTravail = dr["NOrdredeTravail"].ToString();
                            bonLivraisonInterne.BonLivraisonInterneDetailCollection = BonLivraisonInterneDetailCollection.Charger(p);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bonLivraisonInterne;
        }
    }

    public class BonLivraisonInterneCollection : List<BonLivraisonInterne>
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
                cmd.CommandText = "BonLivraisonInterneListe_Rpt_Charger";
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
                sda.Fill(ds, "BonLivraisonInterneListe_Rpt_Charger");
            }
            return (ds);
        }

        public static DataTable ChargerFiltre(DataTable collection, string cEntrepotSource, string cEntrepotCible, string nBonLivraisonInterne, DateTime? dt1, DateTime? dt2)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "BonLivraisonInterne_Filtre";
                    cmd.Parameters.AddWithValue("@CEntrepotSource", cEntrepotSource);
                    cmd.Parameters.AddWithValue("@CEntrepotCible", cEntrepotCible);
                    cmd.Parameters.AddWithValue("@NBonLivraisonInterne", nBonLivraisonInterne);
                    cmd.Parameters.AddWithValue("@DateBLIDu", dt1);
                    cmd.Parameters.AddWithValue("@DateBLIAu", dt2);
                    foreach (SqlParameter parametre in cmd.Parameters)

                        if ((parametre.Value == null) || (parametre.Value == ""))
                            parametre.Value = DBNull.Value;


                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(collection);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return collection;
        }



        public static BonLivraisonInterneCollection ChargerparOT(string NOrdredeTravail)
        {
           
            BonLivraisonInterne bonLivraisonInterne = null;
            BonLivraisonInterneCollection collection = new BonLivraisonInterneCollection();
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
                    cmd.CommandText = "BonlivraisonInterne_ChargerparOT";

                    cmd.Parameters.AddWithValue("@NOrdredeTravail", NOrdredeTravail);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                           
                                bonLivraisonInterne = new BonLivraisonInterne();
                                bonLivraisonInterne.NBonLivraisonInterne = dr["NBonLivraisonInterne"].ToString();
                                if (dr["CChauffeur"] != DBNull.Value)
                                    bonLivraisonInterne.CChauffeur = dr["CChauffeur"].ToString();
                                if (dr["CFournisseur"] != DBNull.Value)
                                    bonLivraisonInterne.CFournisseur = dr["CFournisseur"].ToString();
                                if (dr["CEntrepotCible"] != DBNull.Value)
                                    bonLivraisonInterne.CEntrepotCible = dr["CEntrepotCible"].ToString();
                                if (dr["CEntrepotSource"] != DBNull.Value)
                                    bonLivraisonInterne.CEntrepotSource = dr["CEntrepotSource"].ToString();
                                if (dr["CVehicule"] != DBNull.Value)
                                    bonLivraisonInterne.CVehicule = dr["CVehicule"].ToString();
                                if (dr["CMission"] != DBNull.Value)
                                    bonLivraisonInterne.CMission = dr["CMission"].ToString();
                                if (dr["DateBonLivraisonInterne"] != DBNull.Value)
                                    bonLivraisonInterne.DateBonLivraisonInterne = DateTime.Parse(dr["DateBonLivraisonInterne"].ToString());
                                if (dr["Indice"] != DBNull.Value)
                                    bonLivraisonInterne.Indice = int.Parse(dr["Indice"].ToString());
                                if (dr["NOrdredeTravail"] != DBNull.Value)
                                    bonLivraisonInterne.NOrdredeTravail = dr["NOrdredeTravail"].ToString();
                                //bonLivraisonInterne.BonLivraisonInterneDetailCollection = BonLivraisonInterneDetailCollection.Charger(p);




                                collection.Add(bonLivraisonInterne);
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