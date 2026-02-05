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
    public class BonSortieDetailLot
    {
        #region Propriétés

        [XmlAttribute("NBonSortie")]
        [Bindable(true)]
        public string NBonSortie { get; set; }

        [XmlAttribute("CEntrepot")]
        [Bindable(true)]
        public string CEntrepot { get; set; }

        [XmlAttribute("CArticle")]
        [Bindable(true)]
        public string CArticle { get; set; }

        [XmlAttribute("CLot")]
        [Bindable(true)]
        public string CLot { get; set; }

        [XmlAttribute("LibArticle")]
        [Bindable(true)]
        public string LibArticle { get; set; }

        [XmlAttribute("CUnite")]
        [Bindable(true)]
        public string CUnite { get; set; }

        [XmlAttribute("Quantite")]
        [Bindable(true)]
        public decimal Quantite { get; set; }

        [XmlAttribute("PrixHT")]
        [Bindable(true)]
        public decimal PrixHT { get; set; }

        [XmlAttribute("NombreEmballage")]
        [Bindable(true)]
        public decimal NombreEmballage { get; set; }

        [XmlAttribute("OrdreDetail")]
        [Bindable(true)]
        public int OrdreDetail { get; set; }

        [XmlAttribute("Ordre")]
        [Bindable(true)]
        public int Ordre { get; set; }

        [XmlAttribute("TauxTVA")]
        [Bindable(true)]
        public decimal TauxTVA { get; set; }

        [XmlAttribute("MontantTaxe")]
        [Bindable(true)]
        public decimal MontantTaxe { get; set; }

        [XmlAttribute("StockReel")]
        [Bindable(true)]
        public decimal StockReel { get; set; }

        [XmlAttribute("DateInsertion")]
        [Bindable(true)]
        public DateTime? DateInsertion { get; set; }

        [XmlAttribute("PCInsertion")]
        [Bindable(true)]
        public string PCInsertion { get; set; }

        [XmlAttribute("CreePar")]
        [Bindable(true)]
        public int CreePar { get; set; }

        #endregion

        public BonSortieDetailLot()
        {
            NBonSortie = string.Empty;
            CEntrepot = string.Empty;
            CArticle = string.Empty;
            CLot = string.Empty;
            OrdreDetail = 0;
            Ordre = 0;
        }

        public BonSortieDetailLot(string cEntrepot, string nBonSortie)
        {
            NBonSortie = nBonSortie;
            CEntrepot = cEntrepot;
        }

        public void Sauvegarder(SqlTransaction transaction)
        {
            const int SIGNE_AJOUT = -1;

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BonSortieDetailLot_Inserer";
                cmd.Parameters.AddWithValue("@NBonSortie", NBonSortie);
                cmd.Parameters.AddWithValue("@CEntrepot", CEntrepot);
                cmd.Parameters.AddWithValue("@CArticle", CArticle);
                cmd.Parameters.AddWithValue("@CLot", CLot);
                cmd.Parameters.AddWithValue("@CUnite", CUnite);
                cmd.Parameters.AddWithValue("@LibArticle", LibArticle);
                cmd.Parameters.AddWithValue("@OrdreDetail", OrdreDetail);
                cmd.Parameters.AddWithValue("@Ordre", Ordre);
                cmd.Parameters.AddWithValue("@PrixHT", PrixHT);
                cmd.Parameters.AddWithValue("@Quantite", Quantite);
                cmd.Parameters.AddWithValue("@NombreEmballage", NombreEmballage);
                cmd.Parameters.AddWithValue("@MontantTaxe", MontantTaxe);
                cmd.Parameters.AddWithValue("@TauxTVA", TauxTVA);
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

                StockHelper.AjusterStockReelParLot(this.CArticle, this.CLot, this.CEntrepot, this.Quantite, SIGNE_AJOUT, transaction);
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
                cmd.CommandText = "BonSortieDetailLot_Supprimer";
                cmd.Parameters.AddWithValue("@NBonSortie", NBonSortie);
                cmd.Parameters.AddWithValue("@CEntrepot", CEntrepot);
                cmd.Parameters.AddWithValue("@CArticle", CArticle);
                cmd.Parameters.AddWithValue("@CLot", CLot);
                cmd.Parameters.AddWithValue("@Ordre", Ordre);

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

    public class BonSortieDetailLotCollection : List<BonSortieDetailLot>
    {
        public static BonSortieDetailLotCollection Charger(string nBonSortie, string cEntrepot)
        {
            BonSortieDetailLotCollection BonSortieDetailLotCollection = new BonSortieDetailLotCollection();
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
                    cmd.CommandText = "BonSortieDetailLot_Charger";
                    cmd.Parameters.AddWithValue("@NBonSortie", nBonSortie);
                    cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                    cmd.Parameters.AddWithValue("@CArticle", DBNull.Value);
                    cmd.Parameters.AddWithValue("@OrdreDetail", DBNull.Value);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            BonSortieDetailLot BonSortieDetailLot = new BonSortieDetailLot();

                            BonSortieDetailLot.NBonSortie = dr["NBonSortie"].ToString();
                            BonSortieDetailLot.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["CArticle"] != DBNull.Value)
                                BonSortieDetailLot.CArticle = dr["CArticle"].ToString();
                            if (dr["CLot"] != DBNull.Value)
                                BonSortieDetailLot.CLot = dr["CLot"].ToString();
                            if (dr["LibArticle"] != DBNull.Value)
                                BonSortieDetailLot.LibArticle = dr["LibArticle"].ToString();
                            if (dr["CUnite"] != DBNull.Value)
                                BonSortieDetailLot.CUnite = dr["CUnite"].ToString();
                            if (dr["Quantite"] != DBNull.Value)
                                BonSortieDetailLot.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["PrixHT"] != DBNull.Value)
                                BonSortieDetailLot.PrixHT = decimal.Parse(dr["PrixHT"].ToString());
                            if (dr["NombreEmballage"] != DBNull.Value)
                                BonSortieDetailLot.NombreEmballage = decimal.Parse(dr["NombreEmballage"].ToString());
                            if (dr["OrdreDetail"] != DBNull.Value)
                                BonSortieDetailLot.OrdreDetail = int.Parse(dr["OrdreDetail"].ToString());
                            if (dr["Ordre"] != DBNull.Value)
                                BonSortieDetailLot.Ordre = int.Parse(dr["Ordre"].ToString());
                            if (dr["TauxTVA"] != DBNull.Value)
                                BonSortieDetailLot.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                BonSortieDetailLot.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["StockReel"] != DBNull.Value)
                                BonSortieDetailLot.StockReel = decimal.Parse(dr["StockReel"].ToString());

                            BonSortieDetailLotCollection.Add(BonSortieDetailLot);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return (BonSortieDetailLotCollection);
            }
        }

        public static BonSortieDetailLotCollection Charger(string nBonSortie, string cEntrepot, string cArticle, int ordreDetail)
        {
            BonSortieDetailLotCollection BonSortieDetailLotCollection = new BonSortieDetailLotCollection();
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
                    cmd.CommandText = "BonSortieDetailLot_Charger";
                    cmd.Parameters.AddWithValue("@NBonSortie", nBonSortie);
                    cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                    cmd.Parameters.AddWithValue("@CArticle", cArticle);
                    cmd.Parameters.AddWithValue("@OrdreDetail", ordreDetail);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            BonSortieDetailLot BonSortieDetailLot = new BonSortieDetailLot();

                            BonSortieDetailLot.NBonSortie = dr["NBonSortie"].ToString();
                            BonSortieDetailLot.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["CArticle"] != DBNull.Value)
                                BonSortieDetailLot.CArticle = dr["CArticle"].ToString();
                            if (dr["CLot"] != DBNull.Value)
                                BonSortieDetailLot.CLot = dr["CLot"].ToString();
                            if (dr["LibArticle"] != DBNull.Value)
                                BonSortieDetailLot.LibArticle = dr["LibArticle"].ToString();
                            if (dr["CUnite"] != DBNull.Value)
                                BonSortieDetailLot.CUnite = dr["CUnite"].ToString();
                            if (dr["Quantite"] != DBNull.Value)
                                BonSortieDetailLot.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["PrixHT"] != DBNull.Value)
                                BonSortieDetailLot.PrixHT = decimal.Parse(dr["PrixHT"].ToString());
                            if (dr["NombreEmballage"] != DBNull.Value)
                                BonSortieDetailLot.NombreEmballage = decimal.Parse(dr["NombreEmballage"].ToString());
                            if (dr["OrdreDetail"] != DBNull.Value)
                                BonSortieDetailLot.OrdreDetail = int.Parse(dr["OrdreDetail"].ToString());
                            if (dr["Ordre"] != DBNull.Value)
                                BonSortieDetailLot.Ordre = int.Parse(dr["Ordre"].ToString());
                            if (dr["TauxTVA"] != DBNull.Value)
                                BonSortieDetailLot.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                BonSortieDetailLot.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["StockReel"] != DBNull.Value)
                                BonSortieDetailLot.StockReel = decimal.Parse(dr["StockReel"].ToString());

                            BonSortieDetailLotCollection.Add(BonSortieDetailLot);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return (BonSortieDetailLotCollection);
            }
        }

    }
}