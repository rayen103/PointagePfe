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
    public class BonInventaireDetail
    {
        [XmlAttribute("CArticle")]
        [Bindable(true)]
        public string CArticle { get; set; }

        [XmlAttribute("CEntrepot")]
        [Bindable(true)]
        public string CEntrepot { get; set; }

        [XmlAttribute("NBonInventaire")]
        [Bindable(true)]
        public string NBonInventaire { get; set; }

        [XmlAttribute("LibArticle")]
        [Bindable(true)]
        public string LibArticle { get; set; }

        [XmlAttribute("CUnite")]
        [Bindable(true)]
        public string CUnite { get; set; }

        [XmlAttribute("Ordre")]
        [Bindable(true)]
        public int Ordre { get; set; }

        [XmlAttribute("PrixHT")]
        [Bindable(true)]
        public decimal PrixHT { get; set; }

        [XmlAttribute("Quantite")]
        [Bindable(true)]
        public decimal Quantite { get; set; }

        [XmlAttribute("PrixRevient")]
        [Bindable(true)]
        public decimal PrixRevient { get; set; }

        [XmlAttribute("QuantiteHisto")]
        [Bindable(true)]
        public decimal QuantiteHisto { get; set; }

        [XmlAttribute("StockInitial")]
        [Bindable(true)]
        public decimal StockInitial { get; set; }

        [XmlAttribute("DateInsertion")]
        [Bindable(true)]
        public DateTime DateInsertion { get; set; }

        [XmlAttribute("PCInsertion")]
        [Bindable(true)]
        public string PCInsertion { get; set; }

        [XmlAttribute("CreePar")]
        [Bindable(true)]
        public int CreePar { get; set; }

        public BonInventaireDetail()
        {
            this.CEntrepot = string.Empty;
            this.CArticle = string.Empty;
            this.NBonInventaire = string.Empty;
            //this.DateInsertion = DateTime.Now;
            this.Ordre = 0;
        }

        public BonInventaireDetail(string nBonInventaire, string cEntrepot)
        {
            NBonInventaire = nBonInventaire;
            CEntrepot = cEntrepot;
        }

        public void Sauvegarder(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BonInventaireDetail_Inserer";

                cmd.Parameters.AddWithValue("@CArticle", this.CArticle);
                cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
                cmd.Parameters.AddWithValue("@NBonInventaire", this.NBonInventaire);
                cmd.Parameters.AddWithValue("@Ordre", this.Ordre);
                cmd.Parameters.AddWithValue("@LibArticle", this.LibArticle);

                cmd.Parameters.AddWithValue("@PrixRevient", this.PrixRevient);
                cmd.Parameters.AddWithValue("@PrixHT", this.PrixHT);
                cmd.Parameters.AddWithValue("@CUnite", this.CUnite);
                cmd.Parameters.AddWithValue("@Quantite", this.Quantite);
                cmd.Parameters.AddWithValue("@QuantiteHisto", this.QuantiteHisto);
                cmd.Parameters.AddWithValue("@StockInitial", this.StockInitial);

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
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Sauvegarder()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    Sauvegarder(transaction);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
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
                cmd.CommandText = "BonInventaireDetail_Supprimer";
                cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
                cmd.Parameters.AddWithValue("@NBonInventaire", this.NBonInventaire);

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

        public void ModifierStockInitiale(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BonInventaireDetail_ModifierStockInitiale";
                cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
                cmd.Parameters.AddWithValue("@CArticle", this.CArticle);
                cmd.Parameters.AddWithValue("@Quantite", this.Quantite);
                cmd.Parameters.AddWithValue("@NBonInventaire", this.NBonInventaire);
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

    public class BonInventaireDetailCollection : List<BonInventaireDetail>
    {
        public static DataSet ChargerVue(string nBonInventaire, string cEntrepot)
        {
            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BonInventaireDetail_Rpt_Charger";
                cmd.Parameters.AddWithValue("@NBonInventaire", nBonInventaire);
                cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "BonInventaireDetail_Rpt_Charger");
            }
            return (ds);
        }

        public static BonInventaireDetailCollection Charger(string nBonInventaire, string cEntrepot)
        {
            BonInventaireDetailCollection bonInventaireDetails = new BonInventaireDetailCollection();

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
                    cmd.CommandText = "BonInventaireDetail_Charger";
                    cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                    cmd.Parameters.AddWithValue("@NBonInventaire", nBonInventaire);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            BonInventaireDetail bonInventaireDetail = new BonInventaireDetail(nBonInventaire, cEntrepot);
                            bonInventaireDetail.CArticle = dr["CArticle"].ToString();
                            bonInventaireDetail.LibArticle = dr["LibArticle"].ToString();
                            if (dr["CUnite"] != DBNull.Value)
                                bonInventaireDetail.CUnite = dr["CUnite"].ToString();
                            if (dr["Ordre"] != DBNull.Value)
                                bonInventaireDetail.Ordre = int.Parse(dr["Ordre"].ToString());
                            if (dr["PrixHT"] != DBNull.Value)
                                bonInventaireDetail.PrixHT = decimal.Parse(dr["PrixHT"].ToString());
                            if (dr["StockInitial"] != DBNull.Value)
                                bonInventaireDetail.StockInitial = decimal.Parse(dr["StockInitial"].ToString());
                            if (dr["PrixRevient"] != DBNull.Value)
                                bonInventaireDetail.PrixRevient = decimal.Parse(dr["PrixRevient"].ToString());
                            if (dr["Quantite"] != DBNull.Value)
                                bonInventaireDetail.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["QuantiteHisto"] != DBNull.Value)
                                bonInventaireDetail.QuantiteHisto = decimal.Parse(dr["QuantiteHisto"].ToString());
                            bonInventaireDetails.Add(bonInventaireDetail);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return (bonInventaireDetails);
            }
        }

        public static BonInventaireDetailCollection Charger(string nBonInventaire, string cEntrepot, string cArticle, string cCategorie, string cFamille, string cType, string cNature, string cModele, string cSousModele1, string cSousModele2)
        {
            BonInventaireDetailCollection bonInventaireDetails = new BonInventaireDetailCollection();

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

                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "BonInventaireDetail_Vue_Charger";
                    cmd.Parameters.AddWithValue("@CArticle", cArticle);
                    cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                    cmd.Parameters.AddWithValue("@NBonInventaire", nBonInventaire);
                    cmd.Parameters.AddWithValue("@CCategorie", cCategorie);
                    cmd.Parameters.AddWithValue("@CFamille", cFamille);
                    cmd.Parameters.AddWithValue("@CType", cType);
                    cmd.Parameters.AddWithValue("@CNature", cNature);
                    cmd.Parameters.AddWithValue("@CModele", cModele);
                    cmd.Parameters.AddWithValue("@CModele1", cSousModele1);
                    cmd.Parameters.AddWithValue("@CModele2", cSousModele2);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            BonInventaireDetail bonInventaireDetail = new BonInventaireDetail(nBonInventaire, cEntrepot);
                            bonInventaireDetail.CArticle = dr["CArticle"].ToString();
                            bonInventaireDetail.LibArticle = dr["LibArticle"].ToString();
                            if (dr["CUnite"] != DBNull.Value)
                                bonInventaireDetail.CUnite = dr["CUnite"].ToString();
                            if (dr["Ordre"] != DBNull.Value)
                                bonInventaireDetail.Ordre = int.Parse(dr["Ordre"].ToString());
                            if (dr["PrixHT"] != DBNull.Value)
                                bonInventaireDetail.PrixHT = decimal.Parse(dr["PrixHT"].ToString());
                            if (dr["StockInitial"] != DBNull.Value)
                                bonInventaireDetail.StockInitial = decimal.Parse(dr["StockInitial"].ToString());
                            if (dr["PrixRevient"] != DBNull.Value)
                                bonInventaireDetail.PrixRevient = decimal.Parse(dr["PrixRevient"].ToString());
                            if (dr["Quantite"] != DBNull.Value)
                                bonInventaireDetail.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["QuantiteHisto"] != DBNull.Value)
                                bonInventaireDetail.QuantiteHisto = decimal.Parse(dr["QuantiteHisto"].ToString());
                            bonInventaireDetails.Add(bonInventaireDetail);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return (bonInventaireDetails);
            }
        }
    }
}