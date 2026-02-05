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
    public class BonEntreeDetailLot
    {
        #region Propriétés

        [XmlAttribute("NBonEntree")]
        [Bindable(true)]
        public string NBonEntree { get; set; }

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

        [XmlAttribute("PrixRevient")]
        [Bindable(true)]
        public decimal PrixRevient { get; set; }

        [XmlAttribute("OrdreDetail")]
        [Bindable(true)]
        public int OrdreDetail { get; set; }

        [XmlAttribute("Ordre")]
        [Bindable(true)]
        public int Ordre { get; set; }

        [XmlAttribute("PourcentageFodec")]
        [Bindable(true)]
        public decimal PourcentageFodec { get; set; }

        [XmlAttribute("CTaxe")]
        [Bindable(true)]
        public string CTaxe { get; set; }

        [XmlAttribute("TauxTVA")]
        [Bindable(true)]
        public decimal TauxTVA { get; set; }

        [XmlAttribute("PourcentageRemise")]
        [Bindable(true)]
        public decimal PourcentageRemise { get; set; }

        [XmlAttribute("PrixRevientArticle")]
        [Bindable(true)]
        public decimal PrixRevientArticle { get; set; }

        [XmlAttribute("Poids")]
        [Bindable(true)]
        public decimal Poids { get; set; }

        [XmlAttribute("StockReelArticle")]
        [Bindable(true)]
        public decimal StockReelArticle { get; set; }

        [XmlAttribute("DateInsertion")]
        [Bindable(true)]
        public DateTime DateInsertion { get; set; }

        [XmlAttribute("PCInsertion")]
        [Bindable(true)]
        public string PCInsertion { get; set; }

        [XmlAttribute("CreePar")]
        [Bindable(true)]
        public int CreePar { get; set; }

        #endregion Propriétés

        public BonEntreeDetailLot()
        {
            this.NBonEntree = string.Empty;
            this.CArticle = string.Empty;
            this.CLot = string.Empty;
            this.NBonEntree = string.Empty;
            this.OrdreDetail = 0;
            this.Ordre = 0;
        }

        public BonEntreeDetailLot(string cEntrepot, string nBonEntree)
        {
            this.CEntrepot = cEntrepot;
            this.NBonEntree = nBonEntree;
        }

        public void Sauvegarder(SqlTransaction transaction)
        {
            const int SIGNE_AJOUT = 1;

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BonEntreeDetailLot_Sauvegarder";

                cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
                cmd.Parameters.AddWithValue("@CArticle", this.CArticle);
                cmd.Parameters.AddWithValue("@CLot", this.CLot);
                cmd.Parameters.AddWithValue("@NBonEntree", this.NBonEntree);
                cmd.Parameters.AddWithValue("@CUnite", this.CUnite);
                cmd.Parameters.AddWithValue("@LibArticle", this.LibArticle);
                cmd.Parameters.AddWithValue("@OrdreDetail", this.OrdreDetail);
                cmd.Parameters.AddWithValue("@Ordre", this.Ordre);
                cmd.Parameters.AddWithValue("@PourcentageFodec", this.PourcentageFodec);
                cmd.Parameters.AddWithValue("@PourcentageRemise", this.PourcentageRemise);
                cmd.Parameters.AddWithValue("@PrixRevient", this.PrixRevient);
                cmd.Parameters.AddWithValue("@Quantite", this.Quantite);
                cmd.Parameters.AddWithValue("@CTaxe", this.CTaxe);
                cmd.Parameters.AddWithValue("@TauxTVA", this.TauxTVA);
                cmd.Parameters.AddWithValue("@Poids", this.Poids);
                cmd.Parameters.AddWithValue("@PrixRevientArticle", this.PrixRevientArticle);

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
                cmd.CommandText = "BonEntreeDetailLot_Supprimer";

                cmd.Parameters.AddWithValue("@CArticle", this.CArticle);
                cmd.Parameters.AddWithValue("@CLot", this.CLot);
                cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
                cmd.Parameters.AddWithValue("@NBonEntree", this.NBonEntree);
                cmd.Parameters.AddWithValue("@Ordre", this.Ordre);

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

    public class BonEntreeDetailLotCollection : List<BonEntreeDetailLot>
    {
        public static BonEntreeDetailLotCollection Charger(string nBonEntree, string cEntrepot)
        {
            BonEntreeDetailLotCollection BonEntreeDetailLotCollection = new BonEntreeDetailLotCollection();
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
                    cmd.CommandText = "BonEntreeDetailLot_Charger";
                    cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                    cmd.Parameters.AddWithValue("@NBonEntree", nBonEntree);
                    cmd.Parameters.AddWithValue("@CArticle", DBNull.Value);
                    cmd.Parameters.AddWithValue("@OrdreDetail", DBNull.Value);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            BonEntreeDetailLot BonEntreeDetailLot = new BonEntreeDetailLot();

                            BonEntreeDetailLot.NBonEntree = dr["NBonEntree"].ToString();
                            BonEntreeDetailLot.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["CArticle"] != DBNull.Value)
                                BonEntreeDetailLot.CArticle = dr["CArticle"].ToString();
                            if (dr["CLot"] != DBNull.Value)
                                BonEntreeDetailLot.CLot = dr["CLot"].ToString();
                            if (dr["CUnite"] != DBNull.Value)
                                BonEntreeDetailLot.CUnite = dr["CUnite"].ToString();
                            if (dr["LibArticle"] != DBNull.Value)
                                BonEntreeDetailLot.LibArticle = dr["LibArticle"].ToString();
                            if (dr["OrdreDetail"] != DBNull.Value)
                                BonEntreeDetailLot.OrdreDetail = int.Parse(dr["OrdreDetail"].ToString());
                            if (dr["Ordre"] != DBNull.Value)
                                BonEntreeDetailLot.Ordre = int.Parse(dr["Ordre"].ToString());
                            if (dr["PourcentageFodec"] != DBNull.Value)
                                BonEntreeDetailLot.PourcentageFodec = decimal.Parse(dr["PourcentageFodec"].ToString());
                            if (dr["PourcentageRemise"] != DBNull.Value)
                                BonEntreeDetailLot.PourcentageRemise = decimal.Parse(dr["PourcentageRemise"].ToString());
                            if (dr["PrixRevient"] != DBNull.Value)
                                BonEntreeDetailLot.PrixRevient = decimal.Parse(dr["PrixRevient"].ToString());
                            if (dr["Quantite"] != DBNull.Value)
                                BonEntreeDetailLot.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["Poids"] != DBNull.Value)
                                BonEntreeDetailLot.Poids = decimal.Parse(dr["Poids"].ToString());
                            if (dr["CTaxe"] != DBNull.Value)
                                BonEntreeDetailLot.CTaxe = dr["CTaxe"].ToString();
                            if (dr["TauxTVA"] != DBNull.Value)
                                BonEntreeDetailLot.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());
                            if (dr["PrixRevientArticle"] != DBNull.Value)
                                BonEntreeDetailLot.PrixRevientArticle = decimal.Parse(dr["PrixRevientArticle"].ToString());
                            if (dr["StockReel"] != DBNull.Value)
                                BonEntreeDetailLot.StockReelArticle = decimal.Parse(dr["StockReel"].ToString());

                            BonEntreeDetailLotCollection.Add(BonEntreeDetailLot);
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return (BonEntreeDetailLotCollection);
            }
        }

        public static BonEntreeDetailLotCollection Charger(string nBonEntree, string cEntrepot, string cArticle, int ordreDetail)
        {
            BonEntreeDetailLotCollection BonEntreeDetailLotCollection = new BonEntreeDetailLotCollection();
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
                    cmd.CommandText = "BonEntreeDetailLot_Charger";
                    cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                    cmd.Parameters.AddWithValue("@NBonEntree", nBonEntree);
                    cmd.Parameters.AddWithValue("@CArticle", cArticle);
                    cmd.Parameters.AddWithValue("@OrdreDetail", ordreDetail);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            BonEntreeDetailLot BonEntreeDetailLot = new BonEntreeDetailLot();

                            BonEntreeDetailLot.NBonEntree = dr["NBonEntree"].ToString();
                            BonEntreeDetailLot.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["CArticle"] != DBNull.Value)
                                BonEntreeDetailLot.CArticle = dr["CArticle"].ToString();
                            if (dr["CLot"] != DBNull.Value)
                                BonEntreeDetailLot.CLot = dr["CLot"].ToString();
                            if (dr["CUnite"] != DBNull.Value)
                                BonEntreeDetailLot.CUnite = dr["CUnite"].ToString();
                            if (dr["LibArticle"] != DBNull.Value)
                                BonEntreeDetailLot.LibArticle = dr["LibArticle"].ToString();
                            if (dr["OrdreDetail"] != DBNull.Value)
                                BonEntreeDetailLot.OrdreDetail = int.Parse(dr["OrdreDetail"].ToString());
                            if (dr["Ordre"] != DBNull.Value)
                                BonEntreeDetailLot.Ordre = int.Parse(dr["Ordre"].ToString());
                            if (dr["PourcentageFodec"] != DBNull.Value)
                                BonEntreeDetailLot.PourcentageFodec = decimal.Parse(dr["PourcentageFodec"].ToString());
                            if (dr["PourcentageRemise"] != DBNull.Value)
                                BonEntreeDetailLot.PourcentageRemise = decimal.Parse(dr["PourcentageRemise"].ToString());
                            if (dr["PrixRevient"] != DBNull.Value)
                                BonEntreeDetailLot.PrixRevient = decimal.Parse(dr["PrixRevient"].ToString());
                            if (dr["Quantite"] != DBNull.Value)
                                BonEntreeDetailLot.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["Poids"] != DBNull.Value)
                                BonEntreeDetailLot.Poids = decimal.Parse(dr["Poids"].ToString());
                            if (dr["CTaxe"] != DBNull.Value)
                                BonEntreeDetailLot.CTaxe = dr["CTaxe"].ToString();
                            if (dr["TauxTVA"] != DBNull.Value)
                                BonEntreeDetailLot.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());
                            if (dr["PrixRevientArticle"] != DBNull.Value)
                                BonEntreeDetailLot.PrixRevientArticle = decimal.Parse(dr["PrixRevientArticle"].ToString());
                            if (dr["StockReel"] != DBNull.Value)
                                BonEntreeDetailLot.StockReelArticle = decimal.Parse(dr["StockReel"].ToString());

                            BonEntreeDetailLotCollection.Add(BonEntreeDetailLot);
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return (BonEntreeDetailLotCollection);
            }
        }
    }
}
