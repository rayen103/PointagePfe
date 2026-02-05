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

namespace CST.LePoint.Achat.Metier
{
    public class Achat_FactureDetail
    {
        #region Proriétès
        [XmlAttribute("CArticle")]
        [Bindable(true)]
        public string CArticle { get; set; }
        [XmlAttribute("NFacture")]
        [Bindable(true)]
        public string NFacture { get; set; }
        [XmlAttribute("CUnite")]
        [Bindable(true)]
        public string CUnite { get; set; }
        [XmlAttribute("LibArticle")]
        [Bindable(true)]
        public string LibArticle { get; set; }
        [XmlAttribute("MontantTaxe")]
        [Bindable(true)]
        public decimal MontantTaxe { get; set; }
        [XmlAttribute("Ordre")]
        [Bindable(true)]
        public int Ordre { get; set; }
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
        [XmlAttribute("NBonReception")]
        [Bindable(true)]
        public string NBonReception { get; set; }
        [XmlAttribute("MontantNet")]
        [Bindable(true)]
        public decimal MontantNet { get; set; }
        [XmlAttribute("CEntrepotReception")]
        [Bindable(true)]
        public string CEntrepotReception { get; set; }
        #endregion

        public void Sauvegarder(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Achat_FactureDetail_Inserer";

                cmd.Parameters.AddWithValue("@NFacture", this.NFacture);
                cmd.Parameters.AddWithValue("@CArticle", this.CArticle);
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
                cmd.Parameters.AddWithValue("@NBonReception", this.NBonReception);
                cmd.Parameters.AddWithValue("@MontantNet", this.MontantNet);
                cmd.Parameters.AddWithValue("@CEntrepotReception", this.CEntrepotReception);
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

        public static Achat_FactureDetail Charger(string nFacture,string nBonReception, string cArticle, int ordre)
        {
            Achat_FactureDetail factureDetail = null;
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
                    cmd.CommandText = "Achat_FactureDetail_Charger";
                    cmd.Parameters.AddWithValue("@NBonReception", nBonReception);
                    cmd.Parameters.AddWithValue("@NFacture", nFacture);
                    cmd.Parameters.AddWithValue("@CArticle", cArticle);
                    cmd.Parameters.AddWithValue("@Ordre", ordre);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            factureDetail = new Achat_FactureDetail();
                            factureDetail.NFacture = dr["NFacture"].ToString();
                            factureDetail.CArticle = dr["CArticle"].ToString();
                            factureDetail.NBonReception = dr["NBonReception"].ToString();
                            factureDetail.Ordre = int.Parse(dr["Ordre"].ToString());
                            if (dr["CEntrepotReception"] != DBNull.Value)
                                factureDetail.CEntrepotReception = dr["CEntrepotReception"].ToString();
                            if (dr["CUnite"] != DBNull.Value)
                                factureDetail.CUnite = dr["CUnite"].ToString();
                            if (dr["LibArticle"] != DBNull.Value)
                                factureDetail.LibArticle = dr["LibArticle"].ToString();
                            if (dr["MontantTaxe"] != DBNull.Value)
                                factureDetail.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["PourcentageFodec"] != DBNull.Value)
                                factureDetail.PourcentageFodec = decimal.Parse(dr["PourcentageFodec"].ToString());
                            if (dr["PourcentageRemise"] != DBNull.Value)
                                factureDetail.PourcentageRemise = decimal.Parse(dr["PourcentageRemise"].ToString());
                            if (dr["PrixHT"] != DBNull.Value)
                                factureDetail.PrixHT = decimal.Parse(dr["PrixHT"].ToString());
                            if (dr["Quantite"] != DBNull.Value)
                                factureDetail.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["QuantiteHistorique"] != DBNull.Value)
                                factureDetail.QuantiteHistorique = decimal.Parse(dr["QuantiteHistorique"].ToString());
                            if (dr["TauxTVA"] != DBNull.Value)
                                factureDetail.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());
                            if (dr["MontantNet"] != DBNull.Value)
                                factureDetail.MontantNet = decimal.Parse(dr["MontantNet"].ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return (factureDetail);
            }
        }

    }

    public class Achat_FactureDetailCollection : List<Achat_FactureDetail>
    {
        public static Achat_FactureDetailCollection Charger(string nFacture)
        {
            Achat_FactureDetailCollection factureDetailCollection = new Achat_FactureDetailCollection();
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
                    cmd.CommandText = "Achat_FactureDetail_Charger";
                    cmd.Parameters.AddWithValue("@NBonReception", DBNull.Value);
                    cmd.Parameters.AddWithValue("@NFacture", nFacture);
                    cmd.Parameters.AddWithValue("@CArticle", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Ordre", DBNull.Value);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Achat_FactureDetail factureDetail = new Achat_FactureDetail();
                            factureDetail = new Achat_FactureDetail();
                            factureDetail.NFacture = dr["NFacture"].ToString();
                            factureDetail.CArticle = dr["CArticle"].ToString();
                            factureDetail.NBonReception = dr["NBonReception"].ToString();
                            factureDetail.Ordre = int.Parse(dr["Ordre"].ToString());
                            if (dr["CEntrepotReception"] != DBNull.Value)
                                factureDetail.CEntrepotReception = dr["CEntrepotReception"].ToString();
                            if (dr["CUnite"] != DBNull.Value)
                                factureDetail.CUnite = dr["CUnite"].ToString();
                            if (dr["LibArticle"] != DBNull.Value)
                                factureDetail.LibArticle = dr["LibArticle"].ToString();
                            if (dr["MontantTaxe"] != DBNull.Value)
                                factureDetail.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["PourcentageFodec"] != DBNull.Value)
                                factureDetail.PourcentageFodec = decimal.Parse(dr["PourcentageFodec"].ToString());
                            if (dr["PourcentageRemise"] != DBNull.Value)
                                factureDetail.PourcentageRemise = decimal.Parse(dr["PourcentageRemise"].ToString());
                            if (dr["PrixHT"] != DBNull.Value)
                                factureDetail.PrixHT = decimal.Parse(dr["PrixHT"].ToString());
                            if (dr["Quantite"] != DBNull.Value)
                                factureDetail.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["QuantiteHistorique"] != DBNull.Value)
                                factureDetail.QuantiteHistorique = decimal.Parse(dr["QuantiteHistorique"].ToString());
                            if (dr["TauxTVA"] != DBNull.Value)
                                factureDetail.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());
                            if (dr["MontantNet"] != DBNull.Value)
                                factureDetail.MontantNet = decimal.Parse(dr["MontantNet"].ToString());
                            factureDetailCollection.Add(factureDetail);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return (factureDetailCollection);
            }
        }

    }
}
