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
    public class DemandeApprovisionnementDetail
    {
        [XmlAttribute("CEntrepot")]
        [Bindable(true)]
        public string CEntrepot { get; set; }

        [XmlAttribute("NDemande")]
        [Bindable(true)]
        public string NDemande { get; set; }

        [XmlAttribute("CArticle")]
        [Bindable(true)]
        public string CArticle { get; set; }

        [XmlAttribute("CUnite ")]
        [Bindable(true)]
        public string CUnite { get; set; }

        [XmlAttribute("LibArticle")]
        [Bindable(true)]
        public string LibArticle { get; set; }

        [XmlAttribute("Ordre")]
        [Bindable(true)]
        public int Ordre { get; set; }

        [XmlAttribute("Quantite")]
        [Bindable(true)]
        public decimal Quantite { get; set; }

        [XmlAttribute("QuantiteHisto")]
        [Bindable(true)]
        public decimal QuantiteHisto { get; set; }

        [XmlAttribute("NCommande")]
        [Bindable(true)]
        public string NCommande { get; set; }

        [XmlAttribute("PrixUnitaire")]
        [Bindable(true)]
        public decimal PrixUnitaire { get; set; }

        [XmlAttribute("StockReel")]
        [Bindable(true)]
        public decimal StockReel { get; set; }

        [XmlAttribute("Seuille")]
        [Bindable(true)]
        public decimal Seuille { get; set; }

        [XmlAttribute("StockEnCommandeDAP")]
        [Bindable(true)]
        public decimal StockEnCommandeDAP { get; set; }

        [XmlAttribute("DateInsertion")]
        [Bindable(true)]
        public DateTime? DateInsertion { get; set; }

        [XmlAttribute("PCInsertion")]
        [Bindable(true)]
        public string PCInsertion { get; set; }

        [XmlAttribute("CreePar")]
        [Bindable(true)]
        public int CreePar { get; set; }

        public DemandeApprovisionnementDetail(string nDemande, string cEntrepot)
        {
            this.NDemande = nDemande;
            this.CEntrepot = cEntrepot;
        }

        public DemandeApprovisionnementDetail()
        {
            CEntrepot = string.Empty;
            CArticle = string.Empty;
            NDemande = string.Empty;
        }

        public void Inserer(SqlTransaction transaction)
        {
            const int SIGNE_AJOUT = 1;

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DemandeApprovisionnementDetail_Sauvegarder";

                cmd.Parameters.AddWithValue("@CArticle", this.CArticle);
                cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
                cmd.Parameters.AddWithValue("@NDemande", this.NDemande);
                cmd.Parameters.AddWithValue("@CUnite", this.CUnite);
                cmd.Parameters.AddWithValue("@LibArticle", this.LibArticle);
                cmd.Parameters.AddWithValue("@Ordre", this.Ordre);
                cmd.Parameters.AddWithValue("@PrixUnitaire", this.PrixUnitaire);
                cmd.Parameters.AddWithValue("@Quantite", this.Quantite);
                cmd.Parameters.AddWithValue("@QuantiteHisto", this.QuantiteHisto);
                cmd.Parameters.AddWithValue("@NCommande", this.NCommande);
                cmd.Parameters.AddWithValue("@StockReel", this.StockReel);
                cmd.Parameters.AddWithValue("@Seuille", this.Seuille);
                cmd.Parameters.AddWithValue("@StockEnCommandeDAP", this.StockEnCommandeDAP);

                cmd.Parameters.AddWithValue("@CreePar", this.CreePar);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);

                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }

                cmd.ExecuteNonQuery();

                StockHelper.AjusterStockEnCommandeDAP(this.CArticle, this.CEntrepot, this.Quantite, SIGNE_AJOUT, transaction);
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
                cmd.CommandText = "DemandeApprovisionnementDetail_Supprimer";
                cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
                cmd.Parameters.AddWithValue("@NDemande", this.NDemande);
                cmd.Parameters.AddWithValue("@CArticle", this.CArticle);
                cmd.Parameters.AddWithValue("@Ordre", this.Ordre);
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

    public class DemandeApprovisionnementDetailCollection : List<DemandeApprovisionnementDetail>
    {
        public static DataSet ChargerVue(string nDemandeApprovisionnement, string cEntrepot)
        {
            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DemandeApprovisionnementDetail_Rpt_Charger";
                cmd.Parameters.AddWithValue("@NDemande", nDemandeApprovisionnement);
                cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "DemandeApprovisionnementDetail_Rpt_Charger");
            }
            return (ds);
        }

        public static DemandeApprovisionnementDetailCollection Charger(string nDemande, string cEntrepot)
        {
            DemandeApprovisionnementDetailCollection demandeApprovisionnementDetailCollection = new DemandeApprovisionnementDetailCollection();
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
                    cmd.CommandText = "DemandeApprovisionnementDetail_Charger";
                    cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                    cmd.Parameters.AddWithValue("@NDemande", nDemande);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            DemandeApprovisionnementDetail demandeApprovisionnementDetail = new DemandeApprovisionnementDetail(nDemande, cEntrepot);
                            demandeApprovisionnementDetail.CArticle = dr["CArticle"].ToString();
                            if (dr["CUnite"] != DBNull.Value)
                                demandeApprovisionnementDetail.CUnite = dr["CUnite"].ToString();
                            demandeApprovisionnementDetail.LibArticle = dr["LibArticle"].ToString();
                            if (dr["Ordre"] != DBNull.Value)
                                demandeApprovisionnementDetail.Ordre = int.Parse(dr["Ordre"].ToString());
                            if (dr["Quantite"] != DBNull.Value)
                                demandeApprovisionnementDetail.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["QuantiteHisto"] != DBNull.Value)
                                demandeApprovisionnementDetail.QuantiteHisto = decimal.Parse(dr["QuantiteHisto"].ToString());
                            if (dr["NCommande"] != DBNull.Value)
                                demandeApprovisionnementDetail.NCommande = dr["NCommande"].ToString();
                            if (dr["StockReel"] != DBNull.Value)
                                demandeApprovisionnementDetail.StockReel = decimal.Parse(dr["StockReel"].ToString());
                            if (dr["Seuille"] != DBNull.Value)
                                demandeApprovisionnementDetail.Seuille = decimal.Parse(dr["Seuille"].ToString());
                            if (dr["StockEnCommandeDAP"] != DBNull.Value)
                                demandeApprovisionnementDetail.StockEnCommandeDAP = decimal.Parse(dr["StockEnCommandeDAP"].ToString());
                            demandeApprovisionnementDetailCollection.Add(demandeApprovisionnementDetail);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return (demandeApprovisionnementDetailCollection);
            }
        }
    }
}