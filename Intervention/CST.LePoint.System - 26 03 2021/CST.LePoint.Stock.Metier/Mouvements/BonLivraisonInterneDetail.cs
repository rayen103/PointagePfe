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
    public class BonLivraisonInterneDetail
    {
        #region Propriétés

        [XmlAttribute("NBonLivraisonInterne")]
        [Bindable(true)]
        public string NBonLivraisonInterne { get; set; }

        [XmlAttribute("CEntrepotCible")]
        [Bindable(true)]
        public string CEntrepotCible { get; set; }

        [XmlAttribute("CEntrepotSource")]
        [Bindable(true)]
        public string CEntrepotSource { get; set; }

        [XmlAttribute("CArticle")]
        [Bindable(true)]
        public string CArticle { get; set; }

        [XmlAttribute("CUnite")]
        [Bindable(true)]
        public string CUnite { get; set; }

        [XmlAttribute("LibArticle")]
        [Bindable(true)]
        public string LibArticle { get; set; }

        [XmlAttribute("MontantTaxe")]
        [Bindable(true)]
        public decimal MontantTaxe { get; set; }

        [XmlAttribute("PrixHTArticle")]
        [Bindable(true)]
        public decimal PrixHTArticle { get; set; }

        [XmlAttribute("Ordre")]
        [Bindable(true)]
        public int Ordre { get; set; }

        [XmlAttribute("Quantite")]
        [Bindable(true)]
        public decimal Quantite { get; set; }

        [XmlAttribute("TauxTVA")]
        [Bindable(true)]
        public decimal TauxTVA { get; set; }

        [XmlAttribute("PourcentageRemise")]
        [Bindable(true)]
        public decimal PourcentageRemise { get; set; }

        [XmlAttribute("DateInsertion")]
        [Bindable(true)]
        public DateTime? DateInsertion { get; set; }

        [XmlAttribute("PCInsertion")]
        [Bindable(true)]
        public string PCInsertion { get; set; }

        [XmlAttribute("CreePar")]
        [Bindable(true)]
        public int CreePar { get; set; }

        #endregion Propriétés

        public BonLivraisonInterneDetail()
        {
            NBonLivraisonInterne = string.Empty;
            CArticle = string.Empty;
            Ordre = 0;
        }

        public BonLivraisonInterneDetail(string nBonLivraisonInterne)
        {
            NBonLivraisonInterne = nBonLivraisonInterne;
        }

        public void Supprimer(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BonLivraisonInterneDetail_Supprimer";
                cmd.Parameters.AddWithValue("@NBonLivraisonInterne", NBonLivraisonInterne);

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

        public void Sauvegarder(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BonLivraisonInterneDetail_Sauvegarder";
                cmd.Parameters.AddWithValue("@NBonLivraisonInterne", NBonLivraisonInterne);
                cmd.Parameters.AddWithValue("@CArticle", CArticle);
                cmd.Parameters.AddWithValue("@CEntrepotCible", CEntrepotCible);
                cmd.Parameters.AddWithValue("@CEntrepotSource", CEntrepotSource);
                cmd.Parameters.AddWithValue("@CUnite", CUnite);
                cmd.Parameters.AddWithValue("@LibArticle", LibArticle);
                cmd.Parameters.AddWithValue("@MontantTaxe", MontantTaxe);
                cmd.Parameters.AddWithValue("@PrixHTArticle", PrixHTArticle);
                cmd.Parameters.AddWithValue("@Quantite", Quantite);
                cmd.Parameters.AddWithValue("@TauxTVA", TauxTVA);
                cmd.Parameters.AddWithValue("@Ordre", Ordre);
                cmd.Parameters.AddWithValue("@PourcentageRemise", PourcentageRemise);

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
    }

    public class BonLivraisonInterneDetailCollection : List<BonLivraisonInterneDetail>
    {
        public static BonLivraisonInterneDetailCollection Charger(string nBonLivraisonInterne)
        {
            BonLivraisonInterneDetailCollection bonLivraisonInterneDetailCollection = new BonLivraisonInterneDetailCollection();
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
                    cmd.CommandText = "BonLivraisonInterneDetail_Charger";
                    cmd.Parameters.AddWithValue("@NBonLivraisonInterne", nBonLivraisonInterne);
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        BonLivraisonInterneDetail bonLivraisonInterneDetail = new BonLivraisonInterneDetail();

                        bonLivraisonInterneDetail.NBonLivraisonInterne = dr["NBonLivraisonInterne"].ToString();

                        bonLivraisonInterneDetail.CArticle = dr["CArticle"].ToString();

                        if (dr["CEntrepotCible"] != DBNull.Value)
                            bonLivraisonInterneDetail.CEntrepotCible = dr["CEntrepotCible"].ToString();
                        if (dr["CEntrepotSource"] != DBNull.Value)
                            bonLivraisonInterneDetail.CEntrepotSource = dr["CEntrepotSource"].ToString();
                        if (dr["CUnite"] != DBNull.Value)
                            bonLivraisonInterneDetail.CUnite = dr["CUnite"].ToString();
                        if (dr["LibArticle"] != DBNull.Value)
                            bonLivraisonInterneDetail.LibArticle = dr["LibArticle"].ToString();
                        if (dr["MontantTaxe"] != DBNull.Value)
                            bonLivraisonInterneDetail.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                        if (dr["Ordre"] != DBNull.Value)
                            bonLivraisonInterneDetail.Ordre = int.Parse(dr["Ordre"].ToString());
                        if (dr["PrixHTArticle"] != DBNull.Value)
                            bonLivraisonInterneDetail.PrixHTArticle = decimal.Parse(dr["PrixHTArticle"].ToString());
                        if (dr["Quantite"] != DBNull.Value)
                            bonLivraisonInterneDetail.Quantite = decimal.Parse(dr["Quantite"].ToString());
                        if (dr["TauxTVA"] != DBNull.Value)
                            bonLivraisonInterneDetail.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());
                        if (dr["PourcentageRemise"] != DBNull.Value)
                            bonLivraisonInterneDetail.PourcentageRemise = decimal.Parse(dr["PourcentageRemise"].ToString());

                        bonLivraisonInterneDetailCollection.Add(bonLivraisonInterneDetail);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return (bonLivraisonInterneDetailCollection);
            }
        }

        public static DataSet ChargerVue(string nBonLivraisonInterne)
        {
            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BonLivraisonInterneDetail_Rpt_Charger";
                cmd.Parameters.AddWithValue("@NBonLivraisonInterne", nBonLivraisonInterne);

                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "BonLivraisonInterneDetail_Rpt_Charger");
            }
            return (ds);
        }

        public BonLivraisonInterneDetail RecupererBonLivraisonInterneDetail(string nBonLivraison, string cArticle)
        {
            BonLivraisonInterneDetail bonLivraisonInterneDetail = null;
            bonLivraisonInterneDetail = this.Where(p => p.NBonLivraisonInterne == nBonLivraison && p.CArticle == cArticle).FirstOrDefault();
            return bonLivraisonInterneDetail;
        }

        public BonLivraisonInterneDetail RecupererBonLivraisonInterneDetail(string cArticle)
        {
            BonLivraisonInterneDetail bonLivraisonInterneDetail = null;
            bonLivraisonInterneDetail = this.Where(p => p.CArticle == cArticle).FirstOrDefault();
            return bonLivraisonInterneDetail;
        }
    }
}