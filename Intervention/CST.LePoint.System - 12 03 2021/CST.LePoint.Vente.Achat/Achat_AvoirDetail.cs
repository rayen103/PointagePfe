using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Configuration;

namespace CST.LePoint.Achat.Metier
{
    public class Achat_AvoirDetail
    {
        #region Proriétès
        [XmlAttribute("CArticle")]
        [Bindable(true)]
        public string CArticle { get; set; }
        [XmlAttribute("NAvoir")]
        [Bindable(true)]
        public string NAvoir { get; set; }
        [XmlAttribute("Ordre")]
        [Bindable(true)]
        public int Ordre { get; set; }
        [XmlAttribute("CUnite")]
        [Bindable(true)]
        public string CUnite { get; set; }
        [XmlAttribute("LibArticle")]
        [Bindable(true)]
        public string LibArticle { get; set; }
        [XmlAttribute("MontantTaxe")]
        [Bindable(true)]
        public decimal MontantTaxe { get; set; }
        [XmlAttribute("PourcentageFodec")]
        [Bindable(true)]
        public decimal PourcentageFodec { get; set; }
        [XmlAttribute("PourcentageRemise")]
        [Bindable(true)]
        public decimal PourcentageRemise { get; set; }
        [XmlAttribute("PrixHT")]
        [Bindable(true)]
        public decimal PrixHT { get; set; }
        [XmlAttribute("Quantite")]
        [Bindable(true)]
        public decimal Quantite { get; set; }
        [XmlAttribute("QuantiteHistorique")]
        [Bindable(true)]
        public decimal QuantiteHistorique { get; set; }
        [XmlAttribute("TauxTVA")]
        [Bindable(true)]
        public decimal TauxTVA { get; set; }
        [XmlAttribute("CTaxe")]
        [Bindable(true)]
        public string CTaxe { get; set; }
        [XmlAttribute("MontantNet")]
        [Bindable(true)]
        public decimal MontantNet { get; set; }
        #endregion

        public void Sauvegarder(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Achat_AvoirDetail_Inserer";
                cmd.Parameters.AddWithValue("@CArticle", this.CArticle);
                cmd.Parameters.AddWithValue("@NAvoir", this.NAvoir);
                cmd.Parameters.AddWithValue("@Ordre", this.Ordre);
                cmd.Parameters.AddWithValue("@CUnite", this.CUnite);
                cmd.Parameters.AddWithValue("@LibArticle", this.LibArticle);
                cmd.Parameters.AddWithValue("@MontantTaxe", this.MontantTaxe);
                cmd.Parameters.AddWithValue("@PourcentageFodec", this.PourcentageFodec);
                cmd.Parameters.AddWithValue("@PourcentageRemise", this.PourcentageRemise);
                cmd.Parameters.AddWithValue("@PrixHT", this.PrixHT);
                cmd.Parameters.AddWithValue("@Quantite", this.Quantite);
                cmd.Parameters.AddWithValue("@QuantiteHistorique", this.QuantiteHistorique);
                cmd.Parameters.AddWithValue("@TauxTVA", this.TauxTVA);
                cmd.Parameters.AddWithValue("@CTaxe", this.CTaxe);
                cmd.Parameters.AddWithValue("@MontantNet", this.MontantNet);
                foreach (SqlParameter parameter in cmd.Parameters)
                    if (parameter.Value == null)
                        parameter.Value = DBNull.Value;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Achat_AvoirDetail Charger(string nAvoir, string cArticle, int ordre)
        {
            Achat_AvoirDetail avoirDetail = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ToString()))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Achat_AvoirDetail_Charger";
                    cmd.Parameters.AddWithValue("@CArticle", cArticle);
                    cmd.Parameters.AddWithValue("@NAvoir", nAvoir);
                    cmd.Parameters.AddWithValue("@Ordre", ordre);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            avoirDetail = new Achat_AvoirDetail();
                            avoirDetail.CArticle = dr["CArticle"].ToString();
                            avoirDetail.NAvoir = dr["NAvoir"].ToString();
                            avoirDetail.Ordre = int.Parse(dr["Ordre"].ToString());
                            if (dr["CUnite"] != DBNull.Value)
                                avoirDetail.CUnite = dr["CUnite"].ToString();
                            if (dr["LibArticle"] != DBNull.Value)
                                avoirDetail.LibArticle = dr["LibArticle"].ToString();
                            if (dr["MontantTaxe"] != DBNull.Value)
                                avoirDetail.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["PourcentageFodec"] != DBNull.Value)
                                avoirDetail.PourcentageFodec = decimal.Parse(dr["PourcentageFodec"].ToString());
                            if (dr["PourcentageRemise"] != DBNull.Value)
                                avoirDetail.PourcentageRemise = decimal.Parse(dr["PourcentageRemise"].ToString());
                            if (dr["PrixHT"] != DBNull.Value)
                                avoirDetail.PrixHT = decimal.Parse(dr["PrixHT"].ToString());
                            if (dr["Quantite"] != DBNull.Value)
                                avoirDetail.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["QuantiteHistorique"] != DBNull.Value)
                                avoirDetail.QuantiteHistorique = decimal.Parse(dr["QuantiteHistorique"].ToString());
                            if (dr["TauxTVA"] != DBNull.Value)
                                avoirDetail.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());
                            if (dr["MontantNet"] != DBNull.Value)
                                avoirDetail.MontantNet = decimal.Parse(dr["MontantNet"].ToString());
                            if (dr["CTaxe"] != DBNull.Value)
                                avoirDetail.CTaxe = dr["CTaxe"].ToString();
                        }
                    }
                }
            }
            catch(Exception ex) 
            {
                throw ex;
            }
            return avoirDetail;
        }
    }

    public class Achat_AvoirDetailCollection : List<Achat_AvoirDetail>
    {
        public Achat_AvoirDetailCollection Charger(string nAvoir, string cArticle)
        {
            Achat_AvoirDetailCollection collection = new Achat_AvoirDetailCollection();
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ToString()))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Achat_AvoirDetail_Charger";
                    cmd.Parameters.AddWithValue("@CArticle", cArticle);
                    cmd.Parameters.AddWithValue("@NAvoir", nAvoir);
                    cmd.Parameters.AddWithValue("@Ordre", null);
                    foreach (SqlParameter parameter in cmd.Parameters)
                        if (parameter.Value == null)
                            parameter.Value = DBNull.Value;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Achat_AvoirDetail avoirDetail = new Achat_AvoirDetail();
                            avoirDetail.CArticle = dr["CArticle"].ToString();
                            avoirDetail.NAvoir = dr["NAvoir"].ToString();
                            avoirDetail.Ordre = int.Parse(dr["Ordre"].ToString());
                            if (dr["CUnite"] != DBNull.Value)
                                avoirDetail.CUnite = dr["CUnite"].ToString();
                            if (dr["LibArticle"] != DBNull.Value)
                                avoirDetail.LibArticle = dr["LibArticle"].ToString();
                            if (dr["MontantTaxe"] != DBNull.Value)
                                avoirDetail.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["PourcentageFodec"] != DBNull.Value)
                                avoirDetail.PourcentageFodec = decimal.Parse(dr["PourcentageFodec"].ToString());
                            if (dr["PourcentageRemise"] != DBNull.Value)
                                avoirDetail.PourcentageRemise = decimal.Parse(dr["PourcentageRemise"].ToString());
                            if (dr["PrixHT"] != DBNull.Value)
                                avoirDetail.PrixHT = decimal.Parse(dr["PrixHT"].ToString());
                            if (dr["Quantite"] != DBNull.Value)
                                avoirDetail.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["QuantiteHistorique"] != DBNull.Value)
                                avoirDetail.QuantiteHistorique = decimal.Parse(dr["QuantiteHistorique"].ToString());
                            if (dr["TauxTVA"] != DBNull.Value)
                                avoirDetail.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());
                            if (dr["MontantNet"] != DBNull.Value)
                                avoirDetail.MontantNet = decimal.Parse(dr["MontantNet"].ToString());
                            if (dr["CTaxe"] != DBNull.Value)
                                avoirDetail.CTaxe = dr["CTaxe"].ToString();
                            collection.Add(avoirDetail);
                        }
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
