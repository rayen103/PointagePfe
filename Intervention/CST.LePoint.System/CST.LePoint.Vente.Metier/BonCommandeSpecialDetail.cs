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

namespace CST.LePoint.Vente.Metier
{
    public class BonCommandeSpecialDetail
    {
         #region Proriétès

        [XmlAttribute("NBonCommande")]
        [Bindable(true)]
        public string NBonCommande { get; set; }

        [XmlAttribute("CArticle")]
        [Bindable(true)]
        public string CArticle { get; set; }

        [XmlAttribute("Ordre")]
        [Bindable(true)]
        public int Ordre { get; set; }

        [XmlAttribute("CEntrepot")]
        [Bindable(true)]
        public string CEntrepot { get; set; }

        [XmlAttribute("CUnite")]
        [Bindable(true)]
        public string CUnite { get; set; }

        [XmlAttribute("LibArticle")]
        [Bindable(true)]
        public string LibArticle { get; set; }

        [XmlAttribute("Poids")]
        [Bindable(true)]
        public decimal Poids { get; set; }

        
        [XmlAttribute("PrixHT")]
        [Bindable(true)]
        public decimal PrixHT { get; set; }

        [XmlAttribute("Quantite")]
        [Bindable(true)]
        public decimal Quantite { get; set; }

        

        #endregion Proriétès

        public BonCommandeSpecialDetail()
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
                cmd.CommandText = "BonCommandeSpecialDetail_Sauvegarder";
                cmd.Parameters.AddWithValue("@NBonCommande", this.NBonCommande);
                cmd.Parameters.AddWithValue("@CArticle", this.CArticle);
                cmd.Parameters.AddWithValue("@Ordre", this.Ordre);
                cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
                cmd.Parameters.AddWithValue("@CUnite", this.CUnite);
                cmd.Parameters.AddWithValue("@LibArticle", this.LibArticle);
                cmd.Parameters.AddWithValue("@Poids", this.Poids);
                cmd.Parameters.AddWithValue("@Quantite", this.Quantite);
                cmd.Parameters.AddWithValue("@PrixHT", this.PrixHT);

                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
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
                cmd.CommandText = "BonCommandeSpecialDetail_Supprimer";

                cmd.Parameters.AddWithValue("@NBonCommande", this.NBonCommande);
                cmd.Parameters.AddWithValue("@CArticle", this.CArticle);
                cmd.Parameters.AddWithValue("@Ordre", this.Ordre);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static BonCommandeSpecialDetail Charger(string nBonCommande, string cArticle, int ordre)
        {
            BonCommandeSpecialDetail bonCommandeSpecialDetail = null;
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
                    cmd.CommandText = "BonCommandeSpecialDetail_Charger";
                    cmd.Parameters.AddWithValue("@NBonCommande", nBonCommande);
                    cmd.Parameters.AddWithValue("@CArticle", cArticle);
                    cmd.Parameters.AddWithValue("@Ordre", ordre);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            bonCommandeSpecialDetail = new BonCommandeSpecialDetail();
                            bonCommandeSpecialDetail.NBonCommande = dr["NBonCommande"].ToString();
                            bonCommandeSpecialDetail.CArticle = dr["CArticle"].ToString();
                            bonCommandeSpecialDetail.Ordre = int.Parse(dr["Ordre"].ToString());

                            if (dr["CEntrepot"] != DBNull.Value)
                                bonCommandeSpecialDetail.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["CUnite"] != DBNull.Value)
                                bonCommandeSpecialDetail.CUnite = dr["CUnite"].ToString();
                            if (dr["LibArticle"] != DBNull.Value)
                                bonCommandeSpecialDetail.LibArticle = dr["LibArticle"].ToString();
                            if (dr["Poids"] != DBNull.Value)
                                bonCommandeSpecialDetail.Poids = decimal.Parse(dr["Poids"].ToString());
                            if (dr["PrixHT"] != DBNull.Value)
                                bonCommandeSpecialDetail.PrixHT = decimal.Parse(dr["PrixHTArticle"].ToString());
                            if (dr["Quantite"] != DBNull.Value)
                                bonCommandeSpecialDetail.Quantite = decimal.Parse(dr["Quantite"].ToString());
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return (bonCommandeSpecialDetail);
            }
        }
    }

    public class BonCommandeSpecialDetailCollection : List<BonCommandeSpecialDetail>
    {
        public BonCommandeSpecialDetailCollection()
        {
        }

        public static BonCommandeSpecialDetailCollection Charger(string nBonCommande)
        {
            BonCommandeSpecialDetailCollection collection = new BonCommandeSpecialDetailCollection();

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
                    cmd.CommandText = "BonCommandeSpecialDetail_Charger";
                    cmd.Parameters.AddWithValue("@NBonCommande", nBonCommande);
                    cmd.Parameters.AddWithValue("@CArticle", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Ordre", DBNull.Value);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            BonCommandeSpecialDetail bonCommandeSpecialDetail = new BonCommandeSpecialDetail();
                            bonCommandeSpecialDetail.NBonCommande = dr["NBonCommande"].ToString();
                            bonCommandeSpecialDetail.CArticle = dr["CArticle"].ToString();
                            bonCommandeSpecialDetail.Ordre = int.Parse(dr["Ordre"].ToString());

                            if (dr["CEntrepot"] != DBNull.Value)
                                bonCommandeSpecialDetail.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["CUnite"] != DBNull.Value)
                                bonCommandeSpecialDetail.CUnite = dr["CUnite"].ToString();
                            if (dr["LibArticle"] != DBNull.Value)
                                bonCommandeSpecialDetail.LibArticle = dr["LibArticle"].ToString();
                            if (dr["Poids"] != DBNull.Value)
                                bonCommandeSpecialDetail.Poids = decimal.Parse(dr["Poids"].ToString());
                            if (dr["PrixHT"] != DBNull.Value)
                                bonCommandeSpecialDetail.PrixHT = decimal.Parse(dr["PrixHT"].ToString());
                            if (dr["Quantite"] != DBNull.Value)
                                bonCommandeSpecialDetail.Quantite = decimal.Parse(dr["Quantite"].ToString());

                            collection.Add(bonCommandeSpecialDetail);
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return collection;
            }
        }

    }
}
