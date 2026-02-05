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
    public class BonPreparationDetail
    {
        #region Propriétés

        [XmlAttribute("NBonPreparation")]
        [Bindable(true)]
        public string NBonPreparation { get; set; }

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

        [XmlAttribute("Ordre")]
        [Bindable(true)]
        public int Ordre { get; set; }

        [XmlAttribute("Quantite")]
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

        #endregion Propriétés

        public BonPreparationDetail()
        {
        }

        public void Sauvegarder(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "BonPreparationDetail_Inserer";
                cmd.Parameters.AddWithValue("@NBonPreparation", this.NBonPreparation);
                cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
                cmd.Parameters.AddWithValue("@CArticle", this.CArticle);
                cmd.Parameters.AddWithValue("@LibArticle", this.LibArticle);
                cmd.Parameters.AddWithValue("@CUnite", this.CUnite);
                cmd.Parameters.AddWithValue("@Ordre", this.Ordre);
                cmd.Parameters.AddWithValue("@Quantite", this.Quantite);

                cmd.Parameters.AddWithValue("@CreePar", this.CreePar);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);

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

        public void Supprimer(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "BonPreparationDetail_Supprimer";
                cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
                cmd.Parameters.AddWithValue("@NBonPreparation", this.NBonPreparation);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class BonPreparationDetailCollection : List<BonPreparationDetail>
    {
        public static BonPreparationDetailCollection Charger(string nBonPreparation, string cEntrepot)
        {
            BonPreparationDetailCollection bonPreparationDetailCollection = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlTransaction transaction = cn.BeginTransaction();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Transaction = transaction;
                    cmd.Connection = transaction.Connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "BonPreparationDetail_Charger";
                    cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                    cmd.Parameters.AddWithValue("@NBonPreparation", nBonPreparation);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        bonPreparationDetailCollection = new BonPreparationDetailCollection();
                        while (dr.Read())
                        {
                            BonPreparationDetail bonPreparationDetail = new BonPreparationDetail();

                            bonPreparationDetail.NBonPreparation = dr["NBonPreparation"].ToString();
                            bonPreparationDetail.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["CArticle"] != DBNull.Value)
                                bonPreparationDetail.CArticle = dr["CArticle"].ToString();
                            if (dr["LibArticle"] != DBNull.Value)
                                bonPreparationDetail.LibArticle = dr["LibArticle"].ToString();
                            if (dr["CUnite"] != DBNull.Value)
                                bonPreparationDetail.CUnite = dr["CUnite"].ToString();
                            if (dr["Ordre"] != DBNull.Value)
                                bonPreparationDetail.Ordre = int.Parse(dr["Ordre"].ToString());
                            if (dr["Quantite"] != DBNull.Value)
                                bonPreparationDetail.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            bonPreparationDetailCollection.Add(bonPreparationDetail);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (bonPreparationDetailCollection);
        }

        public BonPreparationDetail Obtenir(string nBonPreparation)
        {
            BonPreparationDetail bonPreparationDetail = null;
            bonPreparationDetail = this.Where(p => p.NBonPreparation == nBonPreparation).FirstOrDefault();
            return bonPreparationDetail;
        }

        public static DataSet ChargerVue(string nBonPreparation, string cEntrepot)
        {
            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BonPreparationDetail_Rpt_Charger";
                cmd.Parameters.AddWithValue("@NBonPreparation", nBonPreparation);
                cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "BonPreparationDetail_Rpt_Charger");
            }
            return (ds);
        }
    }
}