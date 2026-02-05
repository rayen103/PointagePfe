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
    public class BonReceptionProductionDetail
    {
        #region Propriétés

        [XmlAttribute("NBonProduction")]
        [Bindable(true)]
        public string NBonProduction { get; set; }

        [XmlAttribute("CArticle")]
        [Bindable(true)]
        public string CArticle { get; set; }

        [XmlAttribute("LibArticle")]
        [Bindable(true)]
        public string LibArticle { get; set; }


        [XmlAttribute("Quantite")]
        [Bindable(true)]
        public decimal Quantite { get; set; }

        #endregion Propriétés

        public BonReceptionProductionDetail(){}

       
    }

    public class BonReceptionProductionDetailCollection : List<BonReceptionProductionDetail>
    {
        public static BonReceptionProductionDetailCollection Charger(string nBonProduction, string connectionString)
        {
            BonReceptionProductionDetailCollection Collection = new BonReceptionProductionDetailCollection();
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Transaction = transaction;
                    cmd.Connection = transaction.Connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "BonReceptionProductionDetail_Reception";
                    cmd.Parameters.AddWithValue("@NBonProduction", nBonProduction);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            BonReceptionProductionDetail detail = new BonReceptionProductionDetail();

                            detail.NBonProduction = dr["NBonProduction"].ToString();
                            if (dr["CArticle"] != DBNull.Value)
                                detail.CArticle = dr["CArticle"].ToString();
                            if (dr["LibArticle"] != DBNull.Value)
                                detail.LibArticle = dr["LibArticle"].ToString();
                            if (dr["Quantite"] != DBNull.Value)
                                detail.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            Collection.Add(detail);
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return (Collection);
            }
        }

    }
}