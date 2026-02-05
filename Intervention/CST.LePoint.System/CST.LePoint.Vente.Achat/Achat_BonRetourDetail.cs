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
    public class Achat_BonRetourDetail
    {
        #region Propriétès

        [XmlAttribute("NBonRetour")]
        [Bindable(true)]
        public string NBonRetour { get; set; }
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
        [XmlAttribute("QuantiteHist")]
        [Bindable(true)]
        public decimal QuantiteHist { get; set; }
        [XmlAttribute("MontantNet")]
        [Bindable(true)]
        public decimal MontantNet { get; set; }
        [XmlAttribute("TauxTVA")]
        [Bindable(true)]
        public decimal TauxTVA { get; set; }
        [XmlAttribute("CTaxe")]
        [Bindable(true)]
        public string CTaxe { get; set; }
        [XmlAttribute("OrdreBonReception")]
        [Bindable(true)]
        public int OrdreBonReception { get; set; }

       

        #endregion Propriétès

        public Achat_BonRetourDetail(){}

        public void Sauvegarder(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Achat_BonRetourDetail_Inserer";

                cmd.Parameters.AddWithValue("@NBonRetour", this.NBonRetour);
                cmd.Parameters.AddWithValue("@CArticle", this.CArticle);
                cmd.Parameters.AddWithValue("@Ordre", this.Ordre);

                cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
                cmd.Parameters.AddWithValue("@CTaxe", this.CTaxe);
                cmd.Parameters.AddWithValue("@TauxTVA", this.TauxTVA);
                cmd.Parameters.AddWithValue("@CUnite", this.CUnite);
                cmd.Parameters.AddWithValue("@LibArticle", this.LibArticle);
                cmd.Parameters.AddWithValue("@PourcentageFodec", this.PourcentageFodec);
                cmd.Parameters.AddWithValue("@PourcentageRemise", this.PourcentageRemise);
                cmd.Parameters.AddWithValue("@Quantite", this.Quantite);
                cmd.Parameters.AddWithValue("@QuantiteHist", this.QuantiteHist);
                cmd.Parameters.AddWithValue("@OrdreBonReception", this.OrdreBonReception);
                cmd.Parameters.AddWithValue("@MontantNet", this.MontantNet);
                cmd.Parameters.AddWithValue("@MontantTaxe", this.MontantTaxe);
                cmd.Parameters.AddWithValue("@PrixHT", this.PrixHT);

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
                cmd.CommandText = "Achat_BonRetourDetail_Supprimer";

                cmd.Parameters.AddWithValue("@NBonRetour", this.NBonRetour);
                cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
                cmd.Parameters.AddWithValue("@CArticle", this.CArticle);
                cmd.Parameters.AddWithValue("@Ordre", this.Ordre);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Achat_BonRetourDetail Charger(string nBonRetour, string cArticle, int ordre,string cEntrepot)
        {
            Achat_BonRetourDetail bonRetourDetail = null;
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
                    cmd.CommandText = "Achat_BonRetourDetail_Charger";
                    cmd.Parameters.AddWithValue("@NBonRetour", nBonRetour);
                    cmd.Parameters.AddWithValue("@CArticle", cArticle);
                    cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                    cmd.Parameters.AddWithValue("@Ordre", ordre);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            bonRetourDetail = new Achat_BonRetourDetail();
                            bonRetourDetail.NBonRetour = dr["NBonRetour"].ToString();
                            bonRetourDetail.CArticle = dr["CArticle"].ToString();
                            bonRetourDetail.Ordre = int.Parse(dr["Ordre"].ToString());
                            bonRetourDetail.CEntrepot = dr["CEntrepot"].ToString();
                            
                            if (dr["CTaxe"] != DBNull.Value)
                                bonRetourDetail.CTaxe = dr["CTaxe"].ToString();
                            if (dr["CUnite"] != DBNull.Value)
                                bonRetourDetail.CUnite = dr["CUnite"].ToString();
                            if (dr["LibArticle"] != DBNull.Value)
                                bonRetourDetail.LibArticle = dr["LibArticle"].ToString();
                            if (dr["MontantNet"] != DBNull.Value)
                                bonRetourDetail.MontantNet = decimal.Parse(dr["MontantNet"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                bonRetourDetail.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["PourcentageFodec"] != DBNull.Value)
                                bonRetourDetail.PourcentageFodec = decimal.Parse(dr["PourcentageFodec"].ToString());
                            if (dr["PourcentageRemise"] != DBNull.Value)
                                bonRetourDetail.PourcentageRemise = decimal.Parse(dr["PourcentageRemise"].ToString());
                            if (dr["PrixHT"] != DBNull.Value)
                                bonRetourDetail.PrixHT = decimal.Parse(dr["PrixHT"].ToString());
                            if (dr["Quantite"] != DBNull.Value)
                                bonRetourDetail.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["QuantiteHist"] != DBNull.Value)
                                bonRetourDetail.QuantiteHist = decimal.Parse(dr["QuantiteHist"].ToString());
                            if (dr["OrdreBonReception"] != DBNull.Value)
                                bonRetourDetail.OrdreBonReception = int.Parse(dr["OrdreBonReception"].ToString());
                            if (dr["TauxTVA"] != DBNull.Value)
                                bonRetourDetail.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return (bonRetourDetail);
            }
        }
    }

    public class Achat_BonRetourDetailCollection : List<Achat_BonRetourDetail>
    {
        public Achat_BonRetourDetailCollection() { }

        public static Achat_BonRetourDetailCollection Charger(string nBonRetour, string cEntrepot)
        {
            Achat_BonRetourDetailCollection bonRetourDetailCollection = new Achat_BonRetourDetailCollection();
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
                    cmd.CommandText = "Achat_BonRetourDetail_Charger";
                    cmd.Parameters.AddWithValue("@NBonRetour", nBonRetour);
                    cmd.Parameters.AddWithValue("@CArticle", DBNull.Value);
                    cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                    cmd.Parameters.AddWithValue("@Ordre", DBNull.Value);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Achat_BonRetourDetail bonRetourDetail = new Achat_BonRetourDetail();
                            bonRetourDetail.NBonRetour = dr["NBonRetour"].ToString();
                            bonRetourDetail.CArticle = dr["CArticle"].ToString();
                            bonRetourDetail.Ordre = int.Parse(dr["Ordre"].ToString());
                            bonRetourDetail.CEntrepot = dr["CEntrepot"].ToString();

                            if (dr["CTaxe"] != DBNull.Value)
                                bonRetourDetail.CTaxe = dr["CTaxe"].ToString();
                            if (dr["CUnite"] != DBNull.Value)
                                bonRetourDetail.CUnite = dr["CUnite"].ToString();
                            if (dr["LibArticle"] != DBNull.Value)
                                bonRetourDetail.LibArticle = dr["LibArticle"].ToString();
                            if (dr["MontantNet"] != DBNull.Value)
                                bonRetourDetail.MontantNet = decimal.Parse(dr["MontantNet"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                bonRetourDetail.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["PourcentageFodec"] != DBNull.Value)
                                bonRetourDetail.PourcentageFodec = decimal.Parse(dr["PourcentageFodec"].ToString());
                            if (dr["PourcentageRemise"] != DBNull.Value)
                                bonRetourDetail.PourcentageRemise = decimal.Parse(dr["PourcentageRemise"].ToString());
                            if (dr["PrixHT"] != DBNull.Value)
                                bonRetourDetail.PrixHT = decimal.Parse(dr["PrixHT"].ToString());
                            if (dr["Quantite"] != DBNull.Value)
                                bonRetourDetail.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["QuantiteHist"] != DBNull.Value)
                                bonRetourDetail.QuantiteHist = decimal.Parse(dr["QuantiteHist"].ToString());
                            if (dr["OrdreBonReception"] != DBNull.Value)
                                bonRetourDetail.OrdreBonReception = int.Parse(dr["OrdreBonReception"].ToString());
                            if (dr["TauxTVA"] != DBNull.Value)
                                bonRetourDetail.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());
                            bonRetourDetailCollection.Add(bonRetourDetail);
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }
                return (bonRetourDetailCollection);
            }
        }

    }
}
