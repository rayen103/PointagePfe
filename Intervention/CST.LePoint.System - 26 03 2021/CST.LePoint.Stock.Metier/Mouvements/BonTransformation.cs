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
    public class BonTransformation
    {
        #region Propriétés

        [XmlAttribute("NBonTransformation")]
        [Bindable(true)]
        public string NBonTransformation { get; set; }

        [XmlAttribute("CEntrepot")]
        [Bindable(true)]
        public string CEntrepot { get; set; }

        [XmlAttribute("CArticle")]
        [Bindable(true)]
        public string CArticle { get; set; }

        [XmlAttribute("DateTransformation")]
        [Bindable(true)]
        public DateTime DateTransformation { get; set; }

        [XmlAttribute("CUnite")]
        [Bindable(true)]
        public string CUnite { get; set; }

        [XmlAttribute("LibArticle")]
        [Bindable(true)]
        public string LibArticle { get; set; }

        [XmlAttribute("PrixRevient")]
        [Bindable(true)]
        public decimal PrixRevient { get; set; }

        [XmlAttribute("Quantite")]
        [Bindable(true)]
        public decimal Quantite { get; set; }

        [XmlAttribute("Indice")]
        [Bindable(true)]
        public decimal Indice { get; set; }

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

        [XmlAttribute("ModifiePar")]
        [Bindable(true)]
        public int ModifiePar { get; set; }

        [XmlAttribute("PCModification")]
        [Bindable(true)]
        public string PCModification { get; set; }

        [XmlAttribute("DateModification")]
        [Bindable(true)]
        public DateTime DateModification { get; set; }

        public BonTransformationDetailCollection BonTransformationDetailCollection;

        #endregion Propriétés

        public BonTransformation()
        {
            this.NBonTransformation = string.Empty;

            this.BonTransformationDetailCollection = new BonTransformationDetailCollection();
        }

        public void Inserer()
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
                    cmd.CommandText = "BonTransformation_Inserer";
                    cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
                    cmd.Parameters.AddWithValue("@CArticle ", this.CArticle);
                    cmd.Parameters.AddWithValue("@CUnite", this.CUnite);
                    cmd.Parameters.AddWithValue("@LibArticle", this.LibArticle);
                    cmd.Parameters.AddWithValue("@DateTransformation", this.DateTransformation);
                    cmd.Parameters.AddWithValue("@PrixRevient ", this.PrixRevient);
                    cmd.Parameters.AddWithValue("@Quantite ", this.Quantite);
                    cmd.Parameters.AddWithValue("@Exercice", this.Exercice);

                    cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);
                    cmd.Parameters.AddWithValue("@CreePar", this.CreePar);
                    cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            this.NBonTransformation = dr["NBonTransformation"].ToString();
                            this.Indice = int.Parse(dr["DernierIndice"].ToString());
                        }
                    }

                    BonEntree bonEntree = new BonEntree();
                    bonEntree.CEntrepot = this.CEntrepot;
                    bonEntree.Exercice = this.Exercice;
                    bonEntree.DateEntree = this.DateTransformation;
                    bonEntree.NDocumentSource = this.NBonTransformation;
                    bonEntree.TypeMouvement = StockHelper.TypesMouvementStock.BE_BONTRANSFORMATION.ToString();
                    BonEntreeDetail detailEntree = new BonEntreeDetail();
                    detailEntree.CEntrepot = bonEntree.CEntrepot;
                    detailEntree.NBonEntree = bonEntree.NBonEntree;
                    detailEntree.CArticle = this.CArticle;
                    detailEntree.LibArticle = this.LibArticle;
                    detailEntree.CUnite = this.CUnite;
                    detailEntree.Ordre = 1;
                    detailEntree.Quantite = this.Quantite;
                    detailEntree.PrixRevient = this.PrixRevient;
                    bonEntree.BonEntreeDetailCollection.Add(detailEntree);
                    bonEntree.Inserer();

                    BonSortieCollection collecction = new BonSortieCollection();
                    int i = 0;
                    foreach (BonTransformationDetail bonTransformationDetail in BonTransformationDetailCollection)
                    {
                        bool trouver = false;
                        bonTransformationDetail.NBonTransformation = this.NBonTransformation;
                        bonTransformationDetail.Ordre = i++;
                        bonTransformationDetail.Sauvegarder(transaction);//, ref bonSortie, ref be, bonTransformationDetail.Quantite);
                        if (collecction.Count != 0)
                        {
                            foreach(BonSortie bonSortie in collecction)
                            {
                                if (bonTransformationDetail.CEntrepot == bonSortie.CEntrepot)
                                {
                                    BonSortieDetail detail = new BonSortieDetail();
                                    detail.CEntrepot = bonSortie.CEntrepot;
                                    detail.NBonSortie = bonSortie.NBonSortie;
                                    detail.CArticle = bonTransformationDetail.CArticle;
                                    detail.LibArticle = bonTransformationDetail.LibArticle;
                                    detail.CUnite = bonTransformationDetail.CUnite;
                                    detail.Quantite = bonTransformationDetail.Quantite;
                                    detail.PrixHT = bonTransformationDetail.PrixHTArticle;
                                    bonSortie.BonSortieDetailCollection.Add(detail);
                                    trouver = true;
                                    break;
                                }
                            }
                        }
                        if(!trouver)
                        {
                            BonSortie bonSortie = new BonSortie();
                            bonSortie.CEntrepot = bonTransformationDetail.CEntrepot;
                            bonSortie.Exercice = this.Exercice;
                            bonSortie.DateSortie = this.DateTransformation;
                            bonSortie.NDocumentSource = this.NBonTransformation;
                            bonSortie.TypeMouvement = StockHelper.TypesMouvementStock.BS_BONTRANSFORMATION.ToString();
                            
                            BonSortieDetail detail = new BonSortieDetail();
                            detail.CEntrepot = bonSortie.CEntrepot;
                            detail.NBonSortie = bonSortie.NBonSortie;
                            detail.CArticle = bonTransformationDetail.CArticle;
                            detail.LibArticle = bonTransformationDetail.LibArticle;
                            detail.CUnite = bonTransformationDetail.CUnite;
                            detail.Quantite = bonTransformationDetail.Quantite;
                            detail.PrixHT = bonTransformationDetail.PrixHTArticle;
                            bonSortie.BonSortieDetailCollection.Add(detail);
                            collecction.Add(bonSortie);
                        }
                            
                    }
                    foreach (BonSortie bonSortie in collecction)
                    {
                        bonSortie.Inserer(transaction);
           
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

        public void Modifier()
        {
            BonTransformation AncienBonTransformation = BonTransformation.Charger(NBonTransformation, CEntrepot);

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
                    cmd.CommandText = "BonTransformation_Modifier";
                    cmd.Parameters.AddWithValue("@NBonTransformation", this.NBonTransformation);
                    cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
                    cmd.Parameters.AddWithValue("@CArticle", this.CArticle);
                    cmd.Parameters.AddWithValue("@CUnite", this.CUnite);
                    cmd.Parameters.AddWithValue("@LibArticle", this.LibArticle);
                    cmd.Parameters.AddWithValue("@DateTransformation ", this.DateTransformation);
                    cmd.Parameters.AddWithValue("@PrixRevient ", this.PrixRevient);
                    cmd.Parameters.AddWithValue("@Quantite ", this.Quantite);
                    cmd.Parameters.AddWithValue("@PCModification", this.PCModification);
                    cmd.Parameters.AddWithValue("@ModifiePar", this.ModifiePar);
                    cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    cmd.ExecuteNonQuery();
                    BonTransformationDetail detail = new BonTransformationDetail();
                    detail.NBonTransformation = NBonTransformation;
                    detail.Supprimer(transaction);
                    if (AncienBonTransformation.Quantite > Quantite)
                    {
                        BonSortie bonSortie = new BonSortie();
                        bonSortie.CEntrepot = CEntrepot;
                        bonSortie.Exercice = Exercice;
                        bonSortie.DateSortie = DateTransformation;
                        bonSortie.TypeMouvement = StockHelper.TypesMouvementStock.BS_BONTRANSFORMATION.ToString();
                        BonSortieDetail detailSortie = new BonSortieDetail();
                        detailSortie.CEntrepot = bonSortie.CEntrepot;
                        detailSortie.NBonSortie = bonSortie.NBonSortie;
                        detailSortie.CArticle = this.CArticle;
                        detailSortie.LibArticle = this.LibArticle;
                        detailSortie.CUnite = this.CUnite;
                        detailSortie.Ordre = 1;
                        detailSortie.Quantite = AncienBonTransformation.Quantite - Quantite;
                        detailSortie.PrixHT = this.PrixRevient;
                        bonSortie.BonSortieDetailCollection.Add(detailSortie);
                        bonSortie.Inserer();

                        BonEntreeCollection collecction = new BonEntreeCollection();
                        foreach (BonTransformationDetail bonTransformationDetail in BonTransformationDetailCollection)
                        {
                            bool trouver = false;
                            decimal quantite;
                            bonTransformationDetail.NBonTransformation = this.NBonTransformation;
                            bonTransformationDetail.CEntrepot = this.CEntrepot;
                            BonTransformationDetail AncienDetail = AncienBonTransformation.BonTransformationDetailCollection.Obtenir(NBonTransformation, bonTransformationDetail.CArticle);
                            AncienBonTransformation.BonTransformationDetailCollection.Remove(AncienDetail);
                            quantite = AncienDetail.Quantite - bonTransformationDetail.Quantite;
                            bonTransformationDetail.Sauvegarder(transaction);
                            if (collecction.Count != 0)
                            {
                                foreach (BonEntree bonEntree in collecction)
                                {
                                    if (bonTransformationDetail.CEntrepot == bonEntree.CEntrepot)
                                    {
                                        BonEntreeDetail detailEntree = new BonEntreeDetail();
                                        detailEntree.CEntrepot = bonEntree.CEntrepot;
                                        detailEntree.NBonEntree = bonEntree.NBonEntree;
                                        detailEntree.CArticle = bonTransformationDetail.CArticle;
                                        detailEntree.LibArticle = bonTransformationDetail.LibArticle;
                                        detailEntree.CUnite = bonTransformationDetail.CUnite;
                                        detailEntree.Quantite = quantite;
                                        detailEntree.PrixRevient = bonTransformationDetail.PrixRevient;
                                        bonEntree.BonEntreeDetailCollection.Add(detailEntree);
                                        trouver = true;
                                        break;
                                    }
                                }
                            }
                            if (!trouver)
                            {
                                BonEntree bonEntree = new BonEntree();
                                bonEntree.CEntrepot = bonTransformationDetail.CEntrepot;
                                bonEntree.Exercice = this.Exercice;
                                bonEntree.DateEntree = this.DateTransformation;
                                bonEntree.NDocumentSource = this.NBonTransformation;
                                bonEntree.TypeMouvement = StockHelper.TypesMouvementStock.BS_BONTRANSFORMATION.ToString();

                                BonEntreeDetail detailEntree = new BonEntreeDetail();
                                detailEntree.CEntrepot = bonSortie.CEntrepot;
                                detailEntree.NBonEntree = bonSortie.NBonSortie;
                                detailEntree.CArticle = bonTransformationDetail.CArticle;
                                detailEntree.LibArticle = bonTransformationDetail.LibArticle;
                                detailEntree.CUnite = bonTransformationDetail.CUnite;
                                detailEntree.Quantite = quantite;
                                detailEntree.PrixRevient = bonTransformationDetail.PrixRevient;
                                bonEntree.BonEntreeDetailCollection.Add(detailEntree);
                                collecction.Add(bonEntree);
                            }


                        }

                        foreach (BonEntree bonEntree in collecction)
                        {
                            bonEntree.Inserer();
                            transaction.Commit();
                        }
                    }
                    else if (AncienBonTransformation.Quantite != Quantite)
                    {
                        BonEntree bonEntree = new BonEntree();
                        bonEntree.CEntrepot = CEntrepot;
                        bonEntree.Exercice = Exercice;
                        bonEntree.DateEntree = DateTransformation;
                        bonEntree.TypeMouvement = StockHelper.TypesMouvementStock.BE_BONTRANSFORMATION.ToString();
                        BonEntreeDetail detailEntree = new BonEntreeDetail();
                        detailEntree.CEntrepot = bonEntree.CEntrepot;
                        detailEntree.NBonEntree = bonEntree.NBonEntree;
                        detailEntree.CArticle = this.CArticle;
                        detailEntree.LibArticle = this.LibArticle;
                        detailEntree.CUnite = this.CUnite;
                        detailEntree.Ordre = 1;
                        detailEntree.Quantite = Quantite - AncienBonTransformation.Quantite;
                        detailEntree.PrixRevient = this.PrixRevient;
                        bonEntree.BonEntreeDetailCollection.Add(detailEntree);
                        bonEntree.Inserer();

                        BonSortieCollection collecction = new BonSortieCollection();
                        foreach (BonTransformationDetail bonTransformationDetail in BonTransformationDetailCollection)
                        {
                            bool trouver = false;
                            decimal quantite;
                            bonTransformationDetail.NBonTransformation = this.NBonTransformation;
                            //bonTransformationDetail.CEntrepot = this.CEntrepot;
                            BonTransformationDetail AncienDetail = AncienBonTransformation.BonTransformationDetailCollection.Obtenir(NBonTransformation, bonTransformationDetail.CArticle);
                            AncienBonTransformation.BonTransformationDetailCollection.Remove(AncienDetail);
                            quantite = bonTransformationDetail.Quantite - AncienDetail.Quantite;
                            bonTransformationDetail.Sauvegarder(transaction);
                            if (collecction.Count != 0)
                            {
                                foreach (BonSortie bonSortie in collecction)
                                {
                                    if (bonTransformationDetail.CEntrepot == bonSortie.CEntrepot)
                                    {
                                        BonSortieDetail detailSortie = new BonSortieDetail();
                                        detailSortie.CEntrepot = bonSortie.CEntrepot;
                                        detailSortie.NBonSortie = bonSortie.NBonSortie;
                                        detailSortie.CArticle = bonTransformationDetail.CArticle;
                                        detailSortie.LibArticle = bonTransformationDetail.LibArticle;
                                        detailSortie.CUnite = bonTransformationDetail.CUnite;
                                        detailSortie.Quantite = quantite;
                                        detailSortie.PrixHT = bonTransformationDetail.PrixHTArticle;
                                        bonSortie.BonSortieDetailCollection.Add(detailSortie);
                                        trouver = true;
                                        break;
                                    }
                                }
                            }
                            if (!trouver)
                            {
                                BonSortie bonSortie = new BonSortie();
                                bonSortie.CEntrepot = bonTransformationDetail.CEntrepot;
                                bonSortie.Exercice = this.Exercice;
                                bonSortie.DateSortie = this.DateTransformation;
                                bonSortie.NDocumentSource = this.NBonTransformation;
                                bonSortie.TypeMouvement = StockHelper.TypesMouvementStock.BS_BONTRANSFORMATION.ToString();

                                BonSortieDetail detailSortie = new BonSortieDetail();
                                detailSortie.CEntrepot = bonSortie.CEntrepot;
                                detailSortie.NBonSortie = bonSortie.NBonSortie;
                                detailSortie.CArticle = bonTransformationDetail.CArticle;
                                detailSortie.LibArticle = bonTransformationDetail.LibArticle;
                                detailSortie.CUnite = bonTransformationDetail.CUnite;
                                detailSortie.Quantite = quantite;
                                detailSortie.PrixHT = bonTransformationDetail.PrixHTArticle;
                                bonSortie.BonSortieDetailCollection.Add(detailSortie);
                                collecction.Add(bonSortie);
                            }

                        }
                        foreach (BonSortie bonSortie in collecction)
                        {
                            bonSortie.Inserer();
                            transaction.Commit();
                        }
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public void Supprimer()
        {
            BonTransformation AncienBonTransformation = BonTransformation.Charger(NBonTransformation, CEntrepot);

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Transaction = transaction;
                    BonSortie bonSortie = new BonSortie();
                    bonSortie.CEntrepot = CEntrepot;
                    bonSortie.Exercice = Exercice;
                    bonSortie.TypeMouvement = StockHelper.TypesMouvementStock.BS_BONTRANSFORMATION.ToString();
                    bonSortie.DateSortie = DateTransformation;
                    BonSortieDetail detailSortie = new BonSortieDetail();
                    detailSortie.CEntrepot = bonSortie.CEntrepot;
                    detailSortie.NBonSortie = bonSortie.NBonSortie;
                    detailSortie.CArticle = this.CArticle;
                    detailSortie.LibArticle = this.LibArticle;
                    detailSortie.CUnite = this.CUnite;
                    detailSortie.Ordre = 1;
                    detailSortie.Quantite = AncienBonTransformation.Quantite;
                    bonSortie.BonSortieDetailCollection.Add(detailSortie);
                    bonSortie.Inserer(transaction);

                    BonEntree bonEntree = new BonEntree();
                    bonEntree.CEntrepot = CEntrepot;
                    bonEntree.Exercice = Exercice;
                    bonEntree.DateEntree = DateTransformation;
                    bonEntree.TypeMouvement = StockHelper.TypesMouvementStock.BE_BONTRANSFORMATION.ToString();
                    foreach (BonTransformationDetail bonTransformationDetail in BonTransformationDetailCollection)
                    {
                        BonEntreeDetail detail = new BonEntreeDetail();
                        detail.CEntrepot = bonTransformationDetail.CEntrepot;
                        detail.CArticle = bonTransformationDetail.CArticle;
                        detail.LibArticle = bonTransformationDetail.LibArticle;
                        detail.CUnite = bonTransformationDetail.CUnite;
                        detail.Ordre = bonTransformationDetail.Ordre;
                        detail.PrixRevient = bonTransformationDetail.PrixRevient;
                        detail.Quantite = bonTransformationDetail.Quantite;
                        bonEntree.BonEntreeDetailCollection.Add(detail);
                    }
                    bonEntree.Inserer(transaction);
                    cmd.Connection = transaction.Connection;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "BonTransformation_Supprimer";
                    cmd.Parameters.Add(new SqlParameter("@NBonTransformation", this.NBonTransformation));

                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    transaction.Commit();
                }

                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public static BonTransformation Charger(string nBonTransformation, string cEntrepot)
        {
            BonTransformation bonTransformation = null;
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
                    cmd.CommandText = "BonTransformation_Charger";
                    cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                    cmd.Parameters.AddWithValue("@NBonTransformation", nBonTransformation);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            bonTransformation = new BonTransformation();
                            bonTransformation.NBonTransformation = dr["NBonTransformation"].ToString();
                            bonTransformation.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["CArticle"] != DBNull.Value)
                                bonTransformation.CArticle = dr["CArticle"].ToString();
                            if (dr["LibArticle"] != DBNull.Value)
                                bonTransformation.LibArticle = dr["LibArticle"].ToString();
                            if (dr["CUnite"] != DBNull.Value)
                                bonTransformation.CUnite = dr["CUnite"].ToString();
                            if (dr["DateTransformation"] != DBNull.Value)
                                bonTransformation.DateTransformation = DateTime.Parse(dr["DateTransformation"].ToString());
                            if (dr["PrixRevient"] != DBNull.Value)
                                bonTransformation.PrixRevient = decimal.Parse(dr["PrixRevient"].ToString());
                            if (dr["Quantite"] != DBNull.Value)
                                bonTransformation.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["Indice"] != DBNull.Value)
                                bonTransformation.Indice = int.Parse(dr["Indice"].ToString());
                        }
                    }
                    if (bonTransformation != null)
                        bonTransformation.BonTransformationDetailCollection = BonTransformationDetailCollection.Charger(nBonTransformation, null);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return (bonTransformation);
        }
    }

    public class BonTransformationCollection : List<BonTransformation>
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
                cmd.CommandText = "BonTransformationListe_Rpt_Charger";
                cmd.Parameters.AddWithValue("@CEntrepot", DBNull.Value);
                cmd.Parameters.AddWithValue("@NBonTransformation", DBNull.Value);
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
                sda.Fill(ds, "BonTransformationListe_Rpt_Charger");
            }
            return (ds);
        }
    }
}