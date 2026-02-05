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

namespace CST.Stock.Metier.Mouvements
{
    [Serializable]
    public class PriseInventaireDetail
    {
        #region Proprietés
        [XmlAttribute("NPrise")]
        [Bindable(true)]
        public string NPrise { get; set; }

        [XmlAttribute("CArticle")]
        [Bindable(true)]
        public string CArticle { get; set; }

        [XmlAttribute("CEntrepot")]
        [Bindable(true)]
        public string CEntrepot { get; set; }

        [XmlAttribute("Ordre")]
        [Bindable(true)]
        public int Ordre { get; set; }

        [XmlAttribute("LibArticle")]
        [Bindable(true)]
        public string LibArticle { get; set; }

        [XmlAttribute("CUnite")]
        [Bindable(true)]
        public string CUnite { get; set; }

        [XmlAttribute("PrixRevient")]
        [Bindable(true)]
        public decimal PrixRevient { get; set; }

        [XmlAttribute("QuantitePriseInv")]
        [Bindable(true)]
        public int QuantitePriseInv { get; set; }

        [XmlAttribute("StockReel")]
        [Bindable(true)]
        public int StockReel { get; set; }

        [XmlAttribute("StockInitial")]
        [Bindable(true)]
        public int StockInitial { get; set; }

        [XmlAttribute("DateInsertion")]
        [Bindable(true)]
        public DateTime DateInsertion { get; set; }

        [XmlAttribute("CreePar")]
        [Bindable(true)]
        public int CreePar { get; set; }

        [XmlAttribute("PCInsertion")]
        [Bindable(true)]
        public string PCInsertion { get; set; }

        [XmlAttribute("PrixHT")]
        [Bindable(true)]
        public decimal PrixHT { get; set; }

        [XmlAttribute("ModifiePar")]
        [Bindable(true)]
        public int ModifiePar { get; set; }

        [XmlAttribute("DateModification")]
        [Bindable(true)]
        public DateTime DateModification { get; set; }

        [XmlAttribute("PCModification")]
        [Bindable(true)]
        public string PCModification { get; set; }

        [XmlAttribute("CLot")]
        [Bindable(true)]
        public string CLot { get; set; }

        [XmlAttribute("StockReelLot")]
        [Bindable(true)]
        public int StockReelLot { get; set; }

        [XmlAttribute("BValide")]
        [Bindable(true)]
        public bool BValide { get; set; }

        #endregion

        public PriseInventaireDetail(){}

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
                cmd.CommandText = "PriseInventaireDetail_Inserer";

                cmd.Parameters.AddWithValue("@CArticle", this.CArticle);
                cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
                cmd.Parameters.AddWithValue("@NPrise", this.NPrise);
                cmd.Parameters.AddWithValue("@LibArticle", this.LibArticle);
                cmd.Parameters.AddWithValue("@PrixRevient", this.PrixRevient);
                cmd.Parameters.AddWithValue("@PrixHT", this.PrixHT);
                cmd.Parameters.AddWithValue("@CUnite", this.CUnite);
                cmd.Parameters.AddWithValue("@QuantitePriseInv", this.QuantitePriseInv);
                cmd.Parameters.AddWithValue("@StockInitial", this.StockInitial);
                cmd.Parameters.AddWithValue("@StockReel", this.StockReel);
                cmd.Parameters.AddWithValue("@CreePar", this.CreePar);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);
                cmd.Parameters.AddWithValue("@CLot", this.CLot);
                cmd.Parameters.AddWithValue("@StockReelLot", this.StockReelLot);
                cmd.Parameters.AddWithValue("@BValide", this.BValide);
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
                cmd.CommandText = "PriseInventaireDetail_Modifier";

                cmd.Parameters.AddWithValue("@CArticle", this.CArticle);
                cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
                cmd.Parameters.AddWithValue("@NPrise", this.NPrise);
                cmd.Parameters.AddWithValue("@CLot", this.CLot);
                cmd.Parameters.AddWithValue("@QuantitePriseInv", this.QuantitePriseInv);
                cmd.Parameters.AddWithValue("@ModifiePar", this.ModifiePar);
                cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                cmd.Parameters.AddWithValue("@PCModification", this.PCModification);
                
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

    public class PriseInventaireDetailCollection : List<PriseInventaireDetail>
    {
        public static PriseInventaireDetailCollection Charger(string nPrise, string cEntrepot, string cArticle)
        {
            PriseInventaireDetailCollection priseInventaireDetails = new PriseInventaireDetailCollection();

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
                    cmd.CommandText = "PriseInventaireDetail_Charger";
                    cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                    cmd.Parameters.AddWithValue("@NPrise", nPrise);
                    cmd.Parameters.AddWithValue("@CArticle", cArticle);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            PriseInventaireDetail priseInventaireDetail = new PriseInventaireDetail();
                            if (dr["CEntrepot"] != DBNull.Value)
                                priseInventaireDetail.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["NPrise"] != DBNull.Value)
                                priseInventaireDetail.NPrise = dr["NPrise"].ToString();
                            if (dr["CArticle"] != DBNull.Value)
                                priseInventaireDetail.CArticle = dr["CArticle"].ToString();
                            if (dr["LibArticle"] != DBNull.Value)
                                priseInventaireDetail.LibArticle = dr["LibArticle"].ToString();
                            if (dr["CUnite"] != DBNull.Value)
                                priseInventaireDetail.CUnite = dr["CUnite"].ToString();
                            if (dr["Ordre"] != DBNull.Value)
                                priseInventaireDetail.Ordre = int.Parse(dr["Ordre"].ToString());
                            if (dr["PrixHT"] != DBNull.Value)
                                priseInventaireDetail.PrixHT = decimal.Parse(dr["PrixHT"].ToString());
                            if (dr["StockInitial"] != DBNull.Value)
                                priseInventaireDetail.StockInitial = int.Parse(dr["StockInitial"].ToString());
                            if (dr["StockReel"] != DBNull.Value)
                                priseInventaireDetail.StockReel = int.Parse(dr["StockReel"].ToString());
                            if (dr["PrixRevient"] != DBNull.Value)
                                priseInventaireDetail.PrixRevient = decimal.Parse(dr["PrixRevient"].ToString());
                            if (dr["QuantitePriseInv"] != DBNull.Value)
                                priseInventaireDetail.QuantitePriseInv = int.Parse(dr["QuantitePriseInv"].ToString());
                            if (dr["CLot"] != DBNull.Value)
                                priseInventaireDetail.CLot = dr["CLot"].ToString();
                            if (dr["StockReelLot"] != DBNull.Value)
                                priseInventaireDetail.StockReelLot = int.Parse(dr["StockReelLot"].ToString());
                            if (dr["BValide"] != DBNull.Value)
                                priseInventaireDetail.BValide = bool.Parse(dr["BValide"].ToString());
                            priseInventaireDetails.Add(priseInventaireDetail);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return (priseInventaireDetails);
            }
        }
    }
}
