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
    public class Achat_BonReceptionDetail
    {
        #region Proriétès
        [XmlAttribute("CArticle")]
        [Bindable(true)]
        public string CArticle { get; set; }
        [XmlAttribute("CEntrepot")]
        [Bindable(true)]
        public string CEntrepot { get; set; }
        [XmlAttribute("NBonReception")]
        [Bindable(true)]
        public string NBonReception { get; set; }
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
        [XmlAttribute("CTaxe")]
        [Bindable(true)]
        public string CTaxe { get; set; }
        [XmlAttribute("OrdreBonCommande")]
        [Bindable(true)]
        public int OrdreBonCommande { get; set; }
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
                cmd.CommandText = "Achat_BonReceptionDetail_Inserer";

                cmd.Parameters.AddWithValue("@NBonReception", this.NBonReception);
                cmd.Parameters.AddWithValue("@CArticle", this.CArticle);
                cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
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
                cmd.Parameters.AddWithValue("@OrdreBonCommande", this.OrdreBonCommande);
                cmd.Parameters.AddWithValue("@MontantNet", this.MontantNet);

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

        public static Achat_BonReceptionDetail Charger(string nBonReception, string cArticle, int ordre, string cEntrepot)
        {
            Achat_BonReceptionDetail bonReceptionDetail = null;
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
                    cmd.CommandText = "Achat_BonReceptionDetail_Charger";
                    cmd.Parameters.AddWithValue("@NBonReception", nBonReception);
                    cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                    cmd.Parameters.AddWithValue("@CArticle", cArticle);
                    cmd.Parameters.AddWithValue("@Ordre", ordre);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            bonReceptionDetail = new Achat_BonReceptionDetail();
                            bonReceptionDetail.CArticle = dr["CArticle"].ToString();
                            bonReceptionDetail.NBonReception = dr["NBonReception"].ToString();
                            bonReceptionDetail.Ordre = int.Parse(dr["Ordre"].ToString());

                            if (dr["CEntrepot"] != DBNull.Value)
                                bonReceptionDetail.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["CUnite"] != DBNull.Value)
                                bonReceptionDetail.CUnite = dr["CUnite"].ToString();
                            if (dr["LibArticle"] != DBNull.Value)
                                bonReceptionDetail.LibArticle = dr["LibArticle"].ToString();
                            if (dr["MontantTaxe"] != DBNull.Value)
                                bonReceptionDetail.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["PourcentageFodec"] != DBNull.Value)
                                bonReceptionDetail.PourcentageFodec = decimal.Parse(dr["PourcentageFodec"].ToString());
                            if (dr["PourcentageRemise"] != DBNull.Value)
                                bonReceptionDetail.PourcentageRemise = decimal.Parse(dr["PourcentageRemise"].ToString());
                            if (dr["PrixHT"] != DBNull.Value)
                                bonReceptionDetail.PrixHT = decimal.Parse(dr["PrixHT"].ToString());
                            if (dr["Quantite"] != DBNull.Value)
                                bonReceptionDetail.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["QuantiteHistorique"] != DBNull.Value)
                                bonReceptionDetail.QuantiteHistorique = decimal.Parse(dr["QuantiteHistorique"].ToString());
                            if (dr["TauxTVA"] != DBNull.Value)
                                bonReceptionDetail.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());
                            if (dr["CTaxe"] != DBNull.Value)
                                bonReceptionDetail.CTaxe = dr["CTaxe"].ToString();
                            if (dr["OrdreBonCommande"] != DBNull.Value)
                                bonReceptionDetail.OrdreBonCommande = int.Parse(dr["OrdreBonCommande"].ToString());
                            if (dr["MontantNet"] != DBNull.Value)
                                bonReceptionDetail.MontantNet = decimal.Parse(dr["MontantNet"].ToString());
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return (bonReceptionDetail);
            }
        }


    }
    public class Achat_BonReceptionDetailCollection : List<Achat_BonReceptionDetail>
    {
        public Achat_BonReceptionDetailCollection()
        {
        }

        public static Achat_BonReceptionDetailCollection Charger(string nBonReception,string cEntrepot)
        {
            Achat_BonReceptionDetailCollection collection = new Achat_BonReceptionDetailCollection();

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
                    cmd.CommandText = "Achat_BonReceptionDetail_Charger";
                    cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                    cmd.Parameters.AddWithValue("@NBonReception", nBonReception);
                    cmd.Parameters.AddWithValue("@CArticle", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Ordre", DBNull.Value);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Achat_BonReceptionDetail bonReceptionDetail = new Achat_BonReceptionDetail();

                            bonReceptionDetail.CArticle = dr["CArticle"].ToString();
                            bonReceptionDetail.NBonReception = dr["NBonReception"].ToString();
                            bonReceptionDetail.Ordre = int.Parse(dr["Ordre"].ToString());

                            if (dr["CEntrepot"] != DBNull.Value)
                                bonReceptionDetail.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["CUnite"] != DBNull.Value)
                                bonReceptionDetail.CUnite = dr["CUnite"].ToString();
                            if (dr["LibArticle"] != DBNull.Value)
                                bonReceptionDetail.LibArticle = dr["LibArticle"].ToString();
                            if (dr["MontantTaxe"] != DBNull.Value)
                                bonReceptionDetail.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["PourcentageFodec"] != DBNull.Value)
                                bonReceptionDetail.PourcentageFodec = decimal.Parse(dr["PourcentageFodec"].ToString());
                            if (dr["PourcentageRemise"] != DBNull.Value)
                                bonReceptionDetail.PourcentageRemise = decimal.Parse(dr["PourcentageRemise"].ToString());
                            if (dr["PrixHT"] != DBNull.Value)
                                bonReceptionDetail.PrixHT = decimal.Parse(dr["PrixHT"].ToString());
                            if (dr["Quantite"] != DBNull.Value)
                                bonReceptionDetail.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["QuantiteHistorique"] != DBNull.Value)
                                bonReceptionDetail.QuantiteHistorique = decimal.Parse(dr["QuantiteHistorique"].ToString());
                            if (dr["TauxTVA"] != DBNull.Value)
                                bonReceptionDetail.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());
                            if (dr["CTaxe"] != DBNull.Value)
                                bonReceptionDetail.CTaxe = dr["CTaxe"].ToString();
                            if (dr["OrdreBonCommande"] != DBNull.Value)
                                bonReceptionDetail.OrdreBonCommande = int.Parse(dr["OrdreBonCommande"].ToString());
                            if (dr["MontantNet"] != DBNull.Value)
                                bonReceptionDetail.MontantNet = decimal.Parse(dr["MontantNet"].ToString());
                            collection.Add(bonReceptionDetail);
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
