using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace CST.Stock.Metier
{
[Serializable]
public class PreInventaireDetail
{
    #region Proprietés
    [XmlAttribute("CArticle")]
    [Bindable(true)]
    public string CArticle {get;set;}

    [XmlAttribute("CEntrepot")]
    [Bindable(true)]
    public string CEntrepot {get;set;}

    [XmlAttribute("NPreInventaire")]
    [Bindable(true)]  
    public string NPreInventaire {get;set;}

    [XmlAttribute("Ordre")]
    [Bindable(true)]
    public int Ordre {get;set;}

    [XmlAttribute("LibArticle")]
    [Bindable(true)]
    public string LibArticle {get;set;}

    [XmlAttribute("CUnite")]
    [Bindable(true)]
    public string CUnite {get;set;}

    [XmlAttribute("PrixRevient")]
    [Bindable(true)]
    public decimal PrixRevient {get;set;}

    [XmlAttribute("QuantitePriseInv")]
    [Bindable(true)]
    public int QuantitePriseInv {get;set;}

    [XmlAttribute("QuantiteDernierePrise")]
    [Bindable(true)]
    public int QuantiteDernierePrise {get;set;}

    [XmlAttribute("StockInitial")]
    [Bindable(true)]
    public int StockInitial {get;set;}

    [XmlAttribute("DateInsertion")]
    [Bindable(true)]
    public DateTime DateInsertion {get;set;}

    [XmlAttribute("CreePar")]
    [Bindable(true)]
    public int CreePar {get;set;}

    [XmlAttribute("PCInsertion")]
    [Bindable(true)]
    public string PCInsertion {get;set;}

    [XmlAttribute("PrixHT")]
    [Bindable(true)]
    public decimal PrixHT {get;set;}

    [XmlAttribute("StockReel")]
    [Bindable(true)]
    public int StockReel { get; set; }

    [XmlAttribute("CLot")]
    [Bindable(true)]
    public string CLot { get; set; }

    [XmlAttribute("StockReelLot")]
    [Bindable(true)]
    public int StockReelLot { get; set; }

    #endregion

    public PreInventaireDetail(){}

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
                cmd.CommandText = "PreInventaireDetail_Inserer";

                cmd.Parameters.AddWithValue("@CArticle", this.CArticle);
                cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
                cmd.Parameters.AddWithValue("@NPreInventaire", this.NPreInventaire);
                cmd.Parameters.AddWithValue("@LibArticle", this.LibArticle);
                cmd.Parameters.AddWithValue("@PrixRevient", this.PrixRevient);
                cmd.Parameters.AddWithValue("@PrixHT", this.PrixHT);
                cmd.Parameters.AddWithValue("@CUnite", this.CUnite);
                cmd.Parameters.AddWithValue("@QuantitePriseInv", this.QuantitePriseInv);
                cmd.Parameters.AddWithValue("@QuantiteDernierePrise", this.QuantiteDernierePrise);
                cmd.Parameters.AddWithValue("@StockInitial", this.StockInitial);
                cmd.Parameters.AddWithValue("@StockReel", this.StockReel);
                cmd.Parameters.AddWithValue("@CreePar", this.CreePar);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);
                cmd.Parameters.AddWithValue("@CLot", this.CLot);
                cmd.Parameters.AddWithValue("@StockReelLot", this.StockReelLot);
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

    }
    public class PreInventaireDetailCollection : List<PreInventaireDetail>
    {
        public static PreInventaireDetailCollection Charger(string nPreInventaire, string cEntrepot, string cArticle)
        {
            PreInventaireDetailCollection preInventaireDetails = new PreInventaireDetailCollection();

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
                    cmd.CommandText = "PreInventaireDetail_Charger";
                    cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                    cmd.Parameters.AddWithValue("@NPreInventaire", nPreInventaire);
                    cmd.Parameters.AddWithValue("@CArticle", cArticle);

                    foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;
                    
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            PreInventaireDetail preInventaireDetail = new PreInventaireDetail();
                            if (dr["CEntrepot"] != DBNull.Value)
                                preInventaireDetail.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["NPreInventaire"] != DBNull.Value)
                                preInventaireDetail.NPreInventaire = dr["NPreInventaire"].ToString();
                            if (dr["CArticle"] != DBNull.Value)
                                preInventaireDetail.CArticle = dr["CArticle"].ToString();
                            if (dr["LibArticle"] != DBNull.Value)
                                preInventaireDetail.LibArticle = dr["LibArticle"].ToString();
                            if (dr["CUnite"] != DBNull.Value)
                                preInventaireDetail.CUnite = dr["CUnite"].ToString();
                            if (dr["Ordre"] != DBNull.Value)
                                preInventaireDetail.Ordre = int.Parse(dr["Ordre"].ToString());
                            if (dr["PrixHT"] != DBNull.Value)
                                preInventaireDetail.PrixHT = decimal.Parse(dr["PrixHT"].ToString());
                            if (dr["StockInitial"] != DBNull.Value)
                                preInventaireDetail.StockInitial = int.Parse(dr["StockInitial"].ToString());
                            if (dr["StockReel"] != DBNull.Value)
                                preInventaireDetail.StockReel = int.Parse(dr["StockReel"].ToString());
                            if (dr["PrixRevient"] != DBNull.Value)
                                preInventaireDetail.PrixRevient = decimal.Parse(dr["PrixRevient"].ToString());
                            if (dr["QuantitePriseInv"] != DBNull.Value)
                                preInventaireDetail.QuantitePriseInv = int.Parse(dr["QuantitePriseInv"].ToString());
                            if (dr["QuantiteDernierePrise"] != DBNull.Value)
                                preInventaireDetail.QuantiteDernierePrise = int.Parse(dr["QuantiteDernierePrise"].ToString());
                            if (dr["CLot"] != DBNull.Value)
                                preInventaireDetail.CLot = dr["CLot"].ToString();
                            if (dr["StockReelLot"] != DBNull.Value)
                                preInventaireDetail.StockReelLot = int.Parse(dr["StockReelLot"].ToString());
                            preInventaireDetails.Add(preInventaireDetail);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return (preInventaireDetails);
            }
        }
    }
}
