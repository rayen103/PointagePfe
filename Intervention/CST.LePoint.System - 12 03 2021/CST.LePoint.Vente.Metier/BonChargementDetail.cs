using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace CST.LePoint.Vente.Metier
{
    public class BonChargementDetail
    {
        [XmlAttribute("NBonChargement")]
        [Bindable(true)]
        public string NBonChargement { get; set; }

        [XmlAttribute("NOrdrePreparation")]
        [Bindable(true)]
        public string NOrdrePreparation { get; set; }

        [XmlAttribute("CRegion")]
        [Bindable(true)]
        public string CRegion { get; set; }

        [XmlAttribute("CGouvernorat")]
        [Bindable(true)]
        public string CGouvernorat { get; set; }

        public BonChargementDetail()
        {
        }

        public void Inserer(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BonChargementDetail_Inserer";

                cmd.Parameters.AddWithValue("@NBonChargement", this.NBonChargement);
                cmd.Parameters.AddWithValue("@NOrdrePreparation", this.NOrdrePreparation);
                cmd.Parameters.AddWithValue("@CGouvernorat ", this.CGouvernorat);
                cmd.Parameters.AddWithValue("@CRegion", this.CRegion);

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

        public static BonChargementDetail Charger(string nOrdrePreparation)
        {
            BonChargementDetail bonChargementDetail = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "BonChargementDetail_ChargerParOrdre";
                    cmd.Parameters.AddWithValue("@NOrdrePrepartion", nOrdrePreparation);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            bonChargementDetail = new BonChargementDetail();

                            bonChargementDetail.NBonChargement = dr["NBonChargement"].ToString();

                            bonChargementDetail.NOrdrePreparation = dr["NOrdrePrepartion"].ToString();
                            if (dr["CGouvernorat"] != DBNull.Value)
                                bonChargementDetail.CGouvernorat = dr["CGouvernorat"].ToString();
                            if (dr["CRegion"] != DBNull.Value)
                                bonChargementDetail.CRegion = dr["CRegion"].ToString();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bonChargementDetail;
        }
    }

    public class BonChargementDetailCollection : List<BonChargementDetail>
    {
        public static BonChargementDetailCollection Charger(string nBonChargement)
        {
            BonChargementDetailCollection collection = new BonChargementDetailCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "BonChargementDetail_Charger";
                    cmd.Parameters.AddWithValue("@NBonChargement", nBonChargement);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            BonChargementDetail bonChargementDetail = new BonChargementDetail();
                            bonChargementDetail.NBonChargement = dr["NBonChargement"].ToString();

                            bonChargementDetail.NOrdrePreparation = dr["NOrdrePrepartion"].ToString();
                            if (dr["CGouvernorat"] != DBNull.Value)
                                bonChargementDetail.CGouvernorat = dr["CGouvernorat"].ToString();
                            if (dr["CRegion"] != DBNull.Value)
                                bonChargementDetail.CRegion = dr["CRegion"].ToString();
                            collection.Add(bonChargementDetail);
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

    public class 
        
        BonChargementDetailArticle
    {
        public string CArticle { get; set; }

        public string LibArticle { get; set; }

        public decimal Quantite { get; set; }

        public decimal Poids { get; set; }

        public BonChargementDetailArticle()
        {
        }
    }

    public class BonChargementDetailArticleCollection : List<BonChargementDetailArticle>
    {
        public BonChargementDetailArticleCollection()
        {
        }

        public static BonChargementDetailArticleCollection Charger(BonChargementDetailCollection detailCollection)
        {
            BonChargementDetailArticleCollection collection = new BonChargementDetailArticleCollection();
            foreach (BonChargementDetail detail in detailCollection)
            {
                OrdrePreparation ordrePreparation = OrdrePreparation.Charger(detail.NOrdrePreparation);
                foreach (OrdrePreparationDetail detailOrdre in ordrePreparation.OrdrePreparationDetailCollection)
                {
                    BonChargementDetailArticle nouveauDetailArticle = null;
                    //BonChargementDetailArticle ancienDetailArticle = null;
                    int i = -1;
                    foreach (BonChargementDetailArticle detailArticle in collection)
                    {
                        //nouveauDetailArticle = new BonChargementDetailArticle();
                        //ancienDetailArticle.CArticle = detailArticle.CArticle;
                        //ancienDetailArticle.Quantite = detailArticle.Quantite;
                        i++;
                        if (!detailOrdre.BSpecial||!detailArticle.CArticle.Equals(VenteHelper.ARTICLE_DIVERS.ToString()))
                        {
                            if (detailOrdre.CArticle.Equals(detailArticle.CArticle))
                            {
                                nouveauDetailArticle = new BonChargementDetailArticle();
                                nouveauDetailArticle.CArticle = detailArticle.CArticle;
                                nouveauDetailArticle.LibArticle = detailArticle.LibArticle;
                                nouveauDetailArticle.Quantite = detailArticle.Quantite + detailOrdre.Quantite;
                                nouveauDetailArticle.Poids = detailArticle.Poids;
                                

                                break;
                            }
                        }
                        else
                        {
                            if (detailOrdre.CArticle.Equals(detailArticle.CArticle))
                            {
                                if (detailOrdre.CArticle.Equals(detailArticle.LibArticle))
                                {
                                nouveauDetailArticle = new BonChargementDetailArticle();
                                nouveauDetailArticle.CArticle = detailArticle.CArticle;
                                nouveauDetailArticle.LibArticle = detailArticle.LibArticle;
                                nouveauDetailArticle.Quantite = detailArticle.Quantite + detailOrdre.Quantite;
                                nouveauDetailArticle.Poids = detailArticle.Poids;
                                }
                                break;
                            }
                        }
                    }
                    if (nouveauDetailArticle != null)
                    {
                        if (i != -1)
                        {
                            collection.RemoveAt(i);
                           // ancienDetailArticle = null;
                        }
                    }
                    else
                    {
                        nouveauDetailArticle = new BonChargementDetailArticle();
              
                            nouveauDetailArticle.CArticle = detailOrdre.CArticle;
                            nouveauDetailArticle.LibArticle = detailOrdre.LibArticle;
                            nouveauDetailArticle.Quantite = detailOrdre.Quantite;
                            nouveauDetailArticle.Poids = detailOrdre.Poids;
                    }
                    collection.Add(nouveauDetailArticle);
                    nouveauDetailArticle = null;
                }
            }

            return collection;
        }
    }
}