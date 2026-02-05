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
    public class BonSortieDetail
    {
        [XmlAttribute("NBonSortie")]
        [Bindable(true)]
        public string NBonSortie { get; set; }

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

        [XmlAttribute("PrixHT")]
        [Bindable(true)]
        public decimal PrixHT { get; set; }

        [XmlAttribute("NombreEmballage")]
        [Bindable(true)]
        public decimal NombreEmballage { get; set; }

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

        public BonSortieDetailLotCollection BonSortieDetailLotCollection;

        public BonSortieDetail()
        {
            NBonSortie = string.Empty;
            CEntrepot = string.Empty;
            CArticle = string.Empty;
            Ordre = 0;
            this.BonSortieDetailLotCollection = new BonSortieDetailLotCollection();
        }

        public BonSortieDetail(string cEntrepot, string nBonSortie)
        {
            NBonSortie = nBonSortie;
            CEntrepot = cEntrepot;
            this.BonSortieDetailLotCollection = new BonSortieDetailLotCollection();
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
                cmd.CommandText = "BonSortieDetail_Inserer";
                cmd.Parameters.AddWithValue("@NBonSortie", NBonSortie);
                cmd.Parameters.AddWithValue("@CEntrepot", CEntrepot);
                cmd.Parameters.AddWithValue("@CArticle", CArticle);
                cmd.Parameters.AddWithValue("@CUnite", CUnite);
                cmd.Parameters.AddWithValue("@LibArticle", LibArticle);
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

                StockHelper.AjusterStockReel(this.CArticle, this.CEntrepot, this.Quantite, SIGNE_AJOUT, transaction);

                if (this.BonSortieDetailLotCollection.Count != 0)
                {
                    int i = 0;
                    foreach (BonSortieDetailLot bonSortieDetailLot in this.BonSortieDetailLotCollection)
                    {
                        bonSortieDetailLot.NBonSortie = this.NBonSortie;
                        bonSortieDetailLot.OrdreDetail = this.Ordre;
                        bonSortieDetailLot.Ordre = i++;
                        bonSortieDetailLot.CreePar = this.CreePar;
                        bonSortieDetailLot.PCInsertion = this.PCInsertion;
                        bonSortieDetailLot.Sauvegarder(transaction);
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
                cmd.CommandText = "BonSortieDetail_Supprimer";
                cmd.Parameters.AddWithValue("@NBonSortie", NBonSortie);
                cmd.Parameters.AddWithValue("@CEntrepot", CEntrepot);
                cmd.Parameters.AddWithValue("@CArticle", CArticle);
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

    public class BonSortieDetailCollection : List<BonSortieDetail>
    {
        public static DataSet ChargerVue(string nBonSortie, string cEntrepot)
        {
            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BonSortieDetail_Rpt_Charger";
                cmd.Parameters.AddWithValue("@NBonSortie", nBonSortie);
                cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "BonSortieDetail_Rpt_Charger");
            }

            return (ds);
        }

        public static DataSet ChargerVueParLot(string nBonSortie, string cEntrepot)
        {
            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BonSortieDetail_Rpt_ChargerParLot";
                cmd.Parameters.AddWithValue("@NBonSortie", nBonSortie);
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

        public static BonSortieDetailCollection Charger(string nBonSortie, string cEntrepot)
        {
            BonSortieDetailCollection bonSortieDetailCollection = new BonSortieDetailCollection();
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
                    cmd.CommandText = "BonSortieDetail_Charger";
                    cmd.Parameters.AddWithValue("@NBonSortie", nBonSortie);
                    cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            BonSortieDetail bonSortiedetail = new BonSortieDetail();

                            bonSortiedetail.NBonSortie = dr["NBonSortie"].ToString();
                            bonSortiedetail.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["CArticle"] != DBNull.Value)
                                bonSortiedetail.CArticle = dr["CArticle"].ToString();
                            if (dr["LibArticle"] != DBNull.Value)
                                bonSortiedetail.LibArticle = dr["LibArticle"].ToString();
                            if (dr["CUnite"] != DBNull.Value)
                                bonSortiedetail.CUnite = dr["CUnite"].ToString();
                            if (dr["Quantite"] != DBNull.Value)
                                bonSortiedetail.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["PrixHT"] != DBNull.Value)
                                bonSortiedetail.PrixHT = decimal.Parse(dr["PrixHT"].ToString());
                            if (dr["NombreEmballage"] != DBNull.Value)
                                bonSortiedetail.NombreEmballage = decimal.Parse(dr["NombreEmballage"].ToString());
                            if (dr["Ordre"] != DBNull.Value)
                                bonSortiedetail.Ordre = int.Parse(dr["Ordre"].ToString());
                            if (dr["TauxTVA"] != DBNull.Value)
                                bonSortiedetail.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                bonSortiedetail.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["StockReel"] != DBNull.Value)
                                bonSortiedetail.StockReel = decimal.Parse(dr["StockReel"].ToString());

                            bonSortiedetail.BonSortieDetailLotCollection = BonSortieDetailLotCollection.Charger(bonSortiedetail.NBonSortie, bonSortiedetail.CEntrepot, bonSortiedetail.CArticle, bonSortiedetail.Ordre);
                            bonSortieDetailCollection.Add(bonSortiedetail);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return (bonSortieDetailCollection);
            }
        }

        public static DataTable Rechercher(string nBonSortie, string cEntrepot)
        {
            DataTable bonSortieDetailCollection = new DataTable();
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
                    cmd.CommandText = "BonSortieDetail_Rechercher";
                    cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                    cmd.Parameters.AddWithValue("@NBonSortie", nBonSortie);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(bonSortieDetailCollection);
                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return (bonSortieDetailCollection);
            }
        }

        public static DataTable ChargerDataTable(string nBonSortie, string cEntrepot)
        {
            DataTable bonSortieDetailCollection = new DataTable();
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
                    cmd.CommandText = "BonSortieDetail_Charger";
                    cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                    cmd.Parameters.AddWithValue("@NBonSortie", nBonSortie);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(bonSortieDetailCollection);
                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return (bonSortieDetailCollection);
            }
        }
    }
}