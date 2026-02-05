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
    public class BonTransformationDetail
    {
        #region Propriètès

        [XmlAttribute("NBonTransformation")]
        [Bindable(true)]
        public string NBonTransformation { get; set; }

        [XmlAttribute("CEntrepot")]
        [Bindable(true)]
        public string CEntrepot { get; set; }

        [XmlAttribute("CArticle")]
        [Bindable(true)]
        public string CArticle { get; set; }

        [XmlAttribute("LibArticle")]
        [Bindable(true)]
        public string LibArticle { get; set; }

        [XmlAttribute("CUnite")]
        [Bindable(true)]
        public string CUnite { get; set; }

        [XmlAttribute("PrixHTArticle")]
        [Bindable(true)]
        public decimal PrixHTArticle { get; set; }

        [XmlAttribute("PrixRevient")]
        [Bindable(true)]
        public decimal PrixRevient { get; set; }

        [XmlAttribute("Ordre ")]
        [Bindable(true)]
        public int Ordre { get; set; }

        [XmlAttribute(" Quantite")]
        [Bindable(true)]
        public decimal Quantite { get; set; }

        [XmlAttribute("DateInsertion")]
        [Bindable(true)]
        public DateTime DateInsertion { get; set; }

        [XmlAttribute("PCInsertion")]
        [Bindable(true)]
        public string PCInsertion { get; set; }

        [XmlAttribute("CreePar")]
        [Bindable(true)]
        public int CreePar { get; set; }

        #endregion Propriètès

        public BonTransformationDetail()
        {
            this.NBonTransformation = string.Empty;
            this.Ordre = 0;
        }

        public BonTransformationDetail(string nBonTransformation, string cEntrepot)
        {
            this.NBonTransformation = nBonTransformation;
            this.CEntrepot = cEntrepot;
        }

        public void Sauvegarder(SqlTransaction transaction)//, ref BonSortie bonSortie, ref BonEntree bonEntree, decimal quantite)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "BonTransformationDetail_Sauvegarder";
                cmd.Parameters.AddWithValue("@NBonTransformation", this.NBonTransformation);
                cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
                cmd.Parameters.AddWithValue("@CArticle", this.CArticle);
                cmd.Parameters.AddWithValue("@CUnite", this.CUnite);
                cmd.Parameters.AddWithValue("@LibArticle", this.LibArticle);
                cmd.Parameters.AddWithValue("@Ordre", this.Ordre);
                cmd.Parameters.AddWithValue("@PrixHTArticle", this.PrixHTArticle);
                cmd.Parameters.AddWithValue("@PrixRevient", this.PrixRevient);
                cmd.Parameters.AddWithValue("@Quantite", this.Quantite);

                cmd.Parameters.AddWithValue("@CreePar", this.CreePar);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);

                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }

                cmd.ExecuteNonQuery();
                //if (bonSortie != null)
                //{
                //    BonSortieDetail detail = new BonSortieDetail();
                //    detail.CEntrepot = bonSortie.CEntrepot;
                //    detail.NBonSortie = bonSortie.NBonSortie;
                //    detail.CArticle = this.CArticle;
                //    detail.LibArticle = this.LibArticle;
                //    detail.CUnite = this.CUnite;
                //    detail.Ordre = this.Ordre;
                //    detail.Quantite = quantite;
                //    bonSortie.BonSortieDetailCollection.Add(detail);
                //}
                //else if (bonEntree != null)
                //{
                //    BonEntreeDetail detail = new BonEntreeDetail();
                //    detail.CEntrepot = bonEntree.CEntrepot;
                //    detail.NBonEntree = bonEntree.NBonEntree;
                //    detail.CArticle = this.CArticle;
                //    detail.LibArticle = this.LibArticle;
                //    detail.CUnite = this.CUnite;
                //    detail.PrixRevient = this.PrixRevient;
                //    detail.Ordre = this.Ordre;
                //    detail.Quantite = quantite;
                //    bonEntree.BonEntreeDetailCollection.Add(detail);
                //}
            }
            catch (Exception ex)
            {
                throw ex;
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

                cmd.CommandText = "BonTransformationDetail_Supprimer";
                cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
                cmd.Parameters.AddWithValue("@NBonTransformation", this.NBonTransformation);
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
    }

    public class BonTransformationDetailCollection : List<BonTransformationDetail>
    {
        public static BonTransformationDetailCollection Charger(string nBonTransformation, string cEntrepot)
        {
            BonTransformationDetailCollection bonTransformationDetailCollection = new BonTransformationDetailCollection();
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
                    cmd.CommandText = "BonTransformationDetail_Charger";
                    cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                    cmd.Parameters.AddWithValue("@NBonTransformation", nBonTransformation);
                    foreach (SqlParameter parametre in cmd.Parameters)
                    {
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;
                    }
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            BonTransformationDetail bonTransformationDetail = new BonTransformationDetail();

                            bonTransformationDetail.NBonTransformation = dr["NBonTransformation"].ToString();
                            bonTransformationDetail.CEntrepot = dr["CEntrepot"].ToString();

                            if (dr["CArticle"] != DBNull.Value)
                                bonTransformationDetail.CArticle = dr["CArticle"].ToString();
                            if (dr["LibArticle"] != DBNull.Value)
                                bonTransformationDetail.LibArticle = dr["LibArticle"].ToString();
                            if (dr["CUnite"] != DBNull.Value)
                                bonTransformationDetail.CUnite = dr["CUnite"].ToString();
                            if (dr["Ordre"] != DBNull.Value)
                                bonTransformationDetail.Ordre = int.Parse(dr["Ordre"].ToString());
                            if (dr["Quantite"] != DBNull.Value)
                                bonTransformationDetail.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["PrixRevient"] != DBNull.Value)
                                bonTransformationDetail.PrixRevient = decimal.Parse(dr["PrixRevient"].ToString());
                            if (dr["PrixHTArticle"] != DBNull.Value)
                                bonTransformationDetail.PrixHTArticle = decimal.Parse(dr["PrixHTArticle"].ToString());
                            bonTransformationDetailCollection.Add(bonTransformationDetail);
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return (bonTransformationDetailCollection);
        }

        public static DataSet ChargerVue(string nBonTransformation, string cEntrepot)
        {
            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BonTransformationDetail_Rpt_Charger";
                cmd.Parameters.AddWithValue("@NBonTransformation", nBonTransformation);
                cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "BonTransformationDetail_Rpt_Charger");
            }
            return (ds);
        }

        public BonTransformationDetail Obtenir(string nBonTransformation, string cArticle)
        {
            BonTransformationDetail bonTransformationDetail = null;
            bonTransformationDetail = this.Where(p => p.NBonTransformation == nBonTransformation && p.CArticle == cArticle).FirstOrDefault();
            return bonTransformationDetail;
        }
    }
}