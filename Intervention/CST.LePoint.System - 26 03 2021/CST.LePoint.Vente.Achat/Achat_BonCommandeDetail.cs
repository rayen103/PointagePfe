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
    public class Achat_BonCommandeDetail
    {
        #region Proriétès
        [XmlAttribute("CArticle")]
        [Bindable(true)]
        public string CArticle { get; set; }

        [XmlAttribute("NBonCommande")]
        [Bindable(true)]
        public string NBonCommande { get; set; }

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

        [XmlAttribute("QuantitePurge")]
        [Bindable(true)]
        public decimal QuantitePurge { get; set; }

        [XmlAttribute("MontantNet")]
        [Bindable(true)]
        public decimal MontantNet { get; set; }
        #endregion

        public Achat_BonCommandeDetail() { }

        public void Sauvegarder(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Achat_BonCommandeDetail_Inserer";
               
                cmd.Parameters.AddWithValue("@NBonCommande", this.NBonCommande);
                cmd.Parameters.AddWithValue("@CArticle", this.CArticle);
                cmd.Parameters.AddWithValue("@Ordre", this.Ordre);
                cmd.Parameters.AddWithValue("@PrixHT", this.PrixHT);
                cmd.Parameters.AddWithValue("@CTaxe", this.CTaxe);
                cmd.Parameters.AddWithValue("@TauxTVA", this.TauxTVA);
                cmd.Parameters.AddWithValue("@CUnite", this.CUnite);
                cmd.Parameters.AddWithValue("@LibArticle", this.LibArticle);
                cmd.Parameters.AddWithValue("@PourcentageFodec", this.PourcentageFodec);
                cmd.Parameters.AddWithValue("@PourcentageRemise", this.PourcentageRemise);
                cmd.Parameters.AddWithValue("@Quantite", this.Quantite);
                cmd.Parameters.AddWithValue("@QuantiteHistorique", this.QuantiteHistorique);
                cmd.Parameters.AddWithValue("@QuantitePurge", this.QuantitePurge);
                cmd.Parameters.AddWithValue("@MontantNet", this.MontantNet);
                cmd.Parameters.AddWithValue("@MontantTaxe", this.MontantTaxe);

                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                cmd.ExecuteNonQuery();
                AchatHelper.MiseAJourStockEnCommandeFnr(this.CArticle, this.Quantite, transaction);
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
                cmd.CommandText = "Achat_BonCommandeDetail_Supprimer";

                cmd.Parameters.AddWithValue("@NBonCommande", this.NBonCommande);
                cmd.Parameters.AddWithValue("@CArticle", this.CArticle);
                cmd.Parameters.AddWithValue("@Ordre", this.Ordre);

                cmd.ExecuteNonQuery();
                AchatHelper.MiseAJourStockEnCommandeFnr(this.CArticle, -this.Quantite, transaction);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Achat_BonCommandeDetail Charger(string nBonCommande, string cArticle, int ordre)
        {
            Achat_BonCommandeDetail bonCommandeDetail = null;
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
                    cmd.CommandText = "BonCommandeDetail_Charger";
                    cmd.Parameters.AddWithValue("@NBonCommande", nBonCommande);
                    cmd.Parameters.AddWithValue("@CArticle", cArticle);
                    cmd.Parameters.AddWithValue("@Ordre", ordre);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            bonCommandeDetail = new Achat_BonCommandeDetail();
                            bonCommandeDetail.NBonCommande = dr["NBonCommande"].ToString();
                            bonCommandeDetail.CArticle = dr["CArticle"].ToString();
                            bonCommandeDetail.Ordre = int.Parse(dr["Ordre"].ToString());

                            if (dr["CTaxe"] != DBNull.Value)
                                bonCommandeDetail.CTaxe = dr["CTaxe"].ToString();
                            if (dr["CUnite"] != DBNull.Value)
                                bonCommandeDetail.CUnite = dr["CUnite"].ToString();
                            if (dr["LibArticle"] != DBNull.Value)
                                bonCommandeDetail.LibArticle = dr["LibArticle"].ToString();
                            if (dr["MontantNet"] != DBNull.Value)
                                bonCommandeDetail.MontantNet = decimal.Parse(dr["MontantNet"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                bonCommandeDetail.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["PourcentageFodec"] != DBNull.Value)
                                bonCommandeDetail.PourcentageFodec = decimal.Parse(dr["PourcentageFodec"].ToString());
                            if (dr["PourcentageRemise"] != DBNull.Value)
                                bonCommandeDetail.PourcentageRemise = decimal.Parse(dr["PourcentageRemise"].ToString());
                            if (dr["PrixHT"] != DBNull.Value)
                                bonCommandeDetail.PrixHT = decimal.Parse(dr["PrixHT"].ToString());
                            if (dr["Quantite"] != DBNull.Value)
                                bonCommandeDetail.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["QuantiteHistorique"] != DBNull.Value)
                                bonCommandeDetail.QuantiteHistorique = decimal.Parse(dr["QuantiteHistorique"].ToString());
                            if (dr["QuantitePurge"] != DBNull.Value)
                                bonCommandeDetail.QuantitePurge = decimal.Parse(dr["QuantitePurge"].ToString());
                            if (dr["TauxTVA"] != DBNull.Value)
                                bonCommandeDetail.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return (bonCommandeDetail);
            }
        }
    }

    public class Achat_BonCommandeDetailCollection : List<Achat_BonCommandeDetail>
    {
        public static Achat_BonCommandeDetailCollection Charger(string nBonCommande)
        {
            Achat_BonCommandeDetailCollection collection = new Achat_BonCommandeDetailCollection();

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
                    cmd.CommandText = "Achat_BonCommandeDetail_Charger";
                    cmd.Parameters.AddWithValue("@NBonCommande", nBonCommande);
                    cmd.Parameters.AddWithValue("@CArticle", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Ordre", DBNull.Value);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Achat_BonCommandeDetail bonCommandeDetail = new Achat_BonCommandeDetail();
                            bonCommandeDetail.NBonCommande = dr["NBonCommande"].ToString();
                            bonCommandeDetail.CArticle = dr["CArticle"].ToString();
                            bonCommandeDetail.Ordre = int.Parse(dr["Ordre"].ToString());

                            if (dr["CTaxe"] != DBNull.Value)
                                bonCommandeDetail.CTaxe = dr["CTaxe"].ToString();
                            if (dr["CUnite"] != DBNull.Value)
                                bonCommandeDetail.CUnite = dr["CUnite"].ToString();
                            if (dr["LibArticle"] != DBNull.Value)
                                bonCommandeDetail.LibArticle = dr["LibArticle"].ToString();
                            if (dr["MontantNet"] != DBNull.Value)
                                bonCommandeDetail.MontantNet = decimal.Parse(dr["MontantNet"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                bonCommandeDetail.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["PourcentageFodec"] != DBNull.Value)
                                bonCommandeDetail.PourcentageFodec = decimal.Parse(dr["PourcentageFodec"].ToString());
                            if (dr["PourcentageRemise"] != DBNull.Value)
                                bonCommandeDetail.PourcentageRemise = decimal.Parse(dr["PourcentageRemise"].ToString());
                            if (dr["PrixHT"] != DBNull.Value)
                                bonCommandeDetail.PrixHT = decimal.Parse(dr["PrixHT"].ToString());
                            if (dr["Quantite"] != DBNull.Value)
                                bonCommandeDetail.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["QuantiteHistorique"] != DBNull.Value)
                                bonCommandeDetail.QuantiteHistorique = decimal.Parse(dr["QuantiteHistorique"].ToString());
                            if (dr["QuantitePurge"] != DBNull.Value)
                                bonCommandeDetail.QuantitePurge = decimal.Parse(dr["QuantitePurge"].ToString());
                            if (dr["TauxTVA"] != DBNull.Value)
                                bonCommandeDetail.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());
                            collection.Add(bonCommandeDetail);
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
