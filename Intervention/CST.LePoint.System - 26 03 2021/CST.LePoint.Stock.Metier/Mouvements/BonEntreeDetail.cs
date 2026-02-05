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
    public class BonEntreeDetail
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

        public BonEntreeDetailLotCollection BonEntreeDetailLotCollection;
        public BonEntreeDetailCollection BonEntreeDetailCollection;

        #endregion Propriétés

        public BonEntreeDetail()
        {
            this.CArticle = string.Empty;
            this.NBonEntree = string.Empty;
            this.Ordre = 0;
            this.BonEntreeDetailLotCollection = new BonEntreeDetailLotCollection();
        }

        public BonEntreeDetail(string cEntrepot, string nBonEntree)
        {
            this.CEntrepot = cEntrepot;
            this.NBonEntree = nBonEntree;
            this.BonEntreeDetailLotCollection = new BonEntreeDetailLotCollection();
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
                cmd.CommandText = "BonEntreeDetail_Sauvegarder";

                cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
                cmd.Parameters.AddWithValue("@CArticle", this.CArticle);
                cmd.Parameters.AddWithValue("@NBonEntree", this.NBonEntree);
                cmd.Parameters.AddWithValue("@CUnite", this.CUnite);
                cmd.Parameters.AddWithValue("@LibArticle", this.LibArticle);
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

                StockHelper.AjusterStockReel(this.CArticle, this.CEntrepot, this.Quantite, SIGNE_AJOUT, transaction);

                if (this.BonEntreeDetailLotCollection.Count != 0)
                {
                    int i = 1;
                    foreach (BonEntreeDetailLot bonEntreeDetailLot in this.BonEntreeDetailLotCollection)
                    {
                        bonEntreeDetailLot.NBonEntree = this.NBonEntree;
                        bonEntreeDetailLot.OrdreDetail = this.Ordre;
                        bonEntreeDetailLot.Ordre = i++;
                        bonEntreeDetailLot.CreePar = this.CreePar;
                        bonEntreeDetailLot.PCInsertion = this.PCInsertion;
                        bonEntreeDetailLot.Sauvegarder(transaction);
                    }
                }
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
                cmd.CommandText = "BonEntreeDetail_Supprimer";

                cmd.Parameters.AddWithValue("@CArticle", this.CArticle);
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

    public class BonEntreeDetailCollection : List<BonEntreeDetail>
    {
        public static BonEntreeDetailCollection Charger(string nBonEntree, string cEntrepot)
        {
            BonEntreeDetailCollection bonEntreeDetailCollection = new BonEntreeDetailCollection();
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
                    cmd.CommandText = "BonEntreeDetail_Charger";
                    cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                    cmd.Parameters.AddWithValue("@NBonEntree", nBonEntree);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            BonEntreeDetail bonEntreedetail = new BonEntreeDetail();

                            bonEntreedetail.NBonEntree = dr["NBonEntree"].ToString();
                            bonEntreedetail.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["CArticle"] != DBNull.Value)
                                bonEntreedetail.CArticle = dr["CArticle"].ToString();
                            if (dr["CUnite"] != DBNull.Value)
                                bonEntreedetail.CUnite = dr["CUnite"].ToString();
                            if (dr["LibArticle"] != DBNull.Value)
                                bonEntreedetail.LibArticle = dr["LibArticle"].ToString();
                            if (dr["Ordre"] != DBNull.Value)
                                bonEntreedetail.Ordre = int.Parse(dr["Ordre"].ToString());
                            if (dr["PourcentageFodec"] != DBNull.Value)
                                bonEntreedetail.PourcentageFodec = decimal.Parse(dr["PourcentageFodec"].ToString());
                            if (dr["PourcentageRemise"] != DBNull.Value)
                                bonEntreedetail.PourcentageRemise = decimal.Parse(dr["PourcentageRemise"].ToString());
                            if (dr["PrixRevient"] != DBNull.Value)
                                bonEntreedetail.PrixRevient = decimal.Parse(dr["PrixRevient"].ToString());
                            if (dr["Quantite"] != DBNull.Value)
                                bonEntreedetail.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["Poids"] != DBNull.Value)
                                bonEntreedetail.Poids = decimal.Parse(dr["Poids"].ToString());
                            if (dr["CTaxe"] != DBNull.Value)
                                bonEntreedetail.CTaxe = dr["CTaxe"].ToString();
                            if (dr["TauxTVA"] != DBNull.Value)
                                bonEntreedetail.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());
                            if (dr["PrixRevientArticle"] != DBNull.Value)
                                bonEntreedetail.PrixRevientArticle = decimal.Parse(dr["PrixRevientArticle"].ToString());
                            if (dr["StockReel"] != DBNull.Value)
                                bonEntreedetail.StockReelArticle = decimal.Parse(dr["StockReel"].ToString());

                            bonEntreedetail.BonEntreeDetailLotCollection = BonEntreeDetailLotCollection.Charger(bonEntreedetail.NBonEntree, bonEntreedetail.CEntrepot, bonEntreedetail.CArticle, bonEntreedetail.Ordre);
                            
                            bonEntreeDetailCollection.Add(bonEntreedetail);
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return (bonEntreeDetailCollection);
            }
        }

        public static DataTable ChargerDataTable(string nBonEntree, string cEntrepot)
        {
            DataTable bonEntreeDetailCollection = new DataTable();
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
                    cmd.CommandText = "BonEntreeDetail_Charger";
                    cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                    cmd.Parameters.AddWithValue("@NBonEntree", nBonEntree);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(bonEntreeDetailCollection);
                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return (bonEntreeDetailCollection);
            }
        }

        public static DataSet ChargerVue(string nBonEntree, string cEntrepot)
        {
            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BonEntreeDetail_Rpt_Charger";
                cmd.Parameters.AddWithValue("@NBonEntree", nBonEntree);
                cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "BonEntreeDetail_Rpt_Charger");
            }

            return (ds);
        }

        public static DataSet ChargerVueParLot(string nBonEntree, string cEntrepot)
        {
            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BonEntreeDetail_Rpt_ChargerParLot";
                cmd.Parameters.AddWithValue("@NBonEntree", nBonEntree);
                cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);
            }

            return (ds);
        }

    }
}