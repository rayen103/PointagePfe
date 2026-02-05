using CST.LePoint.Stock.Metier;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml.Serialization;

namespace CST.LePoint.Vente.Metier
{
    public class BonCommandeDetail
    {
        #region Proriétès

        [XmlAttribute("NBonCommande")]
        [Bindable(true)]
        public string NBonCommande { get; set; }

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

        [XmlAttribute("Poids")]
        [Bindable(true)]
        public decimal Poids { get; set; }

        [XmlAttribute("MontantTaxe")]
        [Bindable(true)]
        public decimal MontantTaxe { get; set; }

        [XmlAttribute("MontantNet")]
        [Bindable(true)]
        public decimal MontantNet { get; set; }

        [XmlAttribute("PourcentageFodec")]
        [Bindable(true)]
        public decimal PourcentageFodec { get; set; }

        [XmlAttribute("PourcentageRemise")]
        [Bindable(true)]
        public decimal PourcentageRemise { get; set; }

        [XmlAttribute("PrixHTArticle")]
        [Bindable(true)]
        public decimal PrixHTArticle { get; set; }

        [XmlAttribute("Quantite")]
        [Bindable(true)]
        public decimal Quantite { get; set; }

        [XmlAttribute("QuantiteHistorique")]
        [Bindable(true)]
        public decimal QuantiteHistorique { get; set; }

        [XmlAttribute("QuantitePurge")]
        [Bindable(true)]
        public decimal QuantitePurge { get; set; }

        [XmlAttribute("QuantitePreparee")]
        [Bindable(true)]
        public decimal QuantitePreparee { get; set; }

        [XmlAttribute("TauxTVA")]
        [Bindable(true)]
        public decimal TauxTVA { get; set; }

        [XmlAttribute("CTaxe")]
        [Bindable(true)]
        public string CTaxe { get; set; }

        [XmlAttribute("Remise1")]
        [Bindable(true)]
        public decimal Remise1 { get; set; }

        [XmlAttribute("Remise2")]
        [Bindable(true)]
        public decimal Remise2 { get; set; }

        [XmlAttribute("PrixVentePublic")]
        [Bindable(true)]
        public decimal PrixVentePublic { get; set; }
        [XmlAttribute("QuantiteOT")]
        [Bindable(true)]
        public decimal QuantiteOT { get; set; }
        [XmlAttribute("PourcentageTPE")]
        [Bindable(true)]
        public decimal PourcentageTPE { get; set; }

        [XmlAttribute("PourcentageTDC")]
        [Bindable(true)]
        public decimal PourcentageTDC { get; set; }
        [XmlAttribute("CGratuites")]
        [Bindable(true)]
        public string CGratuites { get; set; }

        [XmlAttribute("DateGratuitesDebut")]
        [Bindable(true)]
        public DateTime? DateGratuitesDebut { get; set; }
        [XmlAttribute("DateGratuitesFin")]
        [Bindable(true)]
        public DateTime? DateGratuitesFin { get; set; }
        public bool BGratuit { get; set; }


        #endregion Proriétès

        public BonCommandeDetail()
        {
        }

        public static void ModifierQuantiteOT(decimal qtéot, string nbc, string article)
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Transaction = transaction;
                    cmd.Connection = transaction.Connection;
                    cmd.CommandType = CommandType.Text;
                   // decimal resultat = qté - fait;
                    cmd.CommandText = "update BonCommandeDetail set QuantiteOT = '" + qtéot + "'   where NBonCommande = '" + nbc + "' and CArticle = '" + article + "'";
                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public void mobileSauvegarder(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Mobile_BonCommandeDetail_Sauvegarder";
                cmd.Parameters.AddWithValue("@NBonCommande", this.NBonCommande);
                cmd.Parameters.AddWithValue("@CArticle", this.CArticle);
                cmd.Parameters.AddWithValue("@Ordre", this.Ordre);
                cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
                cmd.Parameters.AddWithValue("@CTaxe", this.CTaxe);
                cmd.Parameters.AddWithValue("@TauxTVA", this.TauxTVA);
                cmd.Parameters.AddWithValue("@CUnite", this.CUnite);
                cmd.Parameters.AddWithValue("@LibArticle", this.LibArticle);
                cmd.Parameters.AddWithValue("@Poids", this.Poids);
                cmd.Parameters.AddWithValue("@PourcentageFodec", this.PourcentageFodec);
                //cmd.Parameters.AddWithValue("@PourcentageTPE", this.PourcentageTPE);
                //cmd.Parameters.AddWithValue("@PourcentageTDC", this.PourcentageTDC);
                cmd.Parameters.AddWithValue("@PourcentageRemise", this.PourcentageRemise);
                cmd.Parameters.AddWithValue("@Quantite", this.Quantite);
                cmd.Parameters.AddWithValue("@QuantiteHistorique", this.QuantiteHistorique);
                cmd.Parameters.AddWithValue("@QuantitePurge", this.QuantitePurge);
                cmd.Parameters.AddWithValue("@QuantitePreparee", this.QuantitePreparee);
                cmd.Parameters.AddWithValue("@MontantNet", this.MontantNet);
                cmd.Parameters.AddWithValue("@MontantTaxe", this.MontantTaxe);
                cmd.Parameters.AddWithValue("@PrixHTArticle", this.PrixHTArticle);
                cmd.Parameters.AddWithValue("@PrixVentePublic", this.PrixVentePublic);
                cmd.Parameters.AddWithValue("@Remise1", this.Remise1);
                cmd.Parameters.AddWithValue("@Remise2", this.Remise2);
                cmd.Parameters.AddWithValue("@CGratuites", this.CGratuites);
                cmd.Parameters.AddWithValue("@DateGratuitesDebut", this.DateGratuitesDebut);
                cmd.Parameters.AddWithValue("@DateGratuitesFin", this.DateGratuitesFin);
                cmd.Parameters.AddWithValue("@BGratuit", this.BGratuit);

                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                cmd.ExecuteNonQuery();
                StockHelper.MiseAJourStockReserver(this.CArticle, this.CEntrepot, this.Quantite, 1, transaction);
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
                cmd.CommandText = "BonCommandeDetail_Sauvegarder";

                cmd.Parameters.AddWithValue("@NBonCommande", this.NBonCommande);
                cmd.Parameters.AddWithValue("@CArticle", this.CArticle);
                cmd.Parameters.AddWithValue("@Ordre", this.Ordre);

                cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
                cmd.Parameters.AddWithValue("@CTaxe", this.CTaxe);
                cmd.Parameters.AddWithValue("@TauxTVA", this.TauxTVA);
                cmd.Parameters.AddWithValue("@CUnite", this.CUnite);
                cmd.Parameters.AddWithValue("@LibArticle", this.LibArticle);
                cmd.Parameters.AddWithValue("@Poids", this.Poids);
                cmd.Parameters.AddWithValue("@PourcentageFodec", this.PourcentageFodec);
                cmd.Parameters.AddWithValue("@PourcentageRemise", this.PourcentageRemise);
                cmd.Parameters.AddWithValue("@Quantite", this.Quantite);
                cmd.Parameters.AddWithValue("@QuantiteHistorique", this.QuantiteHistorique);
                cmd.Parameters.AddWithValue("@QuantitePurge", this.QuantitePurge);
                cmd.Parameters.AddWithValue("@QuantitePreparee", this.QuantitePreparee);
                cmd.Parameters.AddWithValue("@QuantitePurge", this.QuantitePurge);
                cmd.Parameters.AddWithValue("@QuantitePreparee", this.QuantitePreparee);
                //cmd.Parameters.AddWithValue("@Largeur", this.Largeur);
                //cmd.Parameters.AddWithValue("@Longueur", this.Longueur);
                //cmd.Parameters.AddWithValue("@Epaisseur", this.Epaisseur);
                cmd.Parameters.AddWithValue("@MontantNet", this.MontantNet);
                cmd.Parameters.AddWithValue("@MontantTaxe", this.MontantTaxe);
                cmd.Parameters.AddWithValue("@PrixHTArticle", this.PrixHTArticle);
                cmd.Parameters.AddWithValue("@PrixVentePublic", this.PrixVentePublic);
                cmd.Parameters.AddWithValue("@Remise1", this.Remise1);
                cmd.Parameters.AddWithValue("@Remise2", this.Remise2);
                cmd.Parameters.AddWithValue("@QuantiteOT", this.Quantite);


                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                cmd.ExecuteNonQuery();
                StockHelper.MiseAJourStockReserver(this.CArticle, this.CEntrepot, this.Quantite, 1, transaction);
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
                cmd.CommandText = "BonCommandeDetail_Supprimer";

                cmd.Parameters.AddWithValue("@NBonCommande", this.NBonCommande);
                cmd.Parameters.AddWithValue("@CArticle", this.CArticle);
                cmd.Parameters.AddWithValue("@Ordre", this.Ordre);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Modifier(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "BonCommande_AjusterQuantiteHistorique";
                cmd.Parameters.AddWithValue("@CArticle", this.CArticle);
                cmd.Parameters.AddWithValue("@NBonCommande", this.NBonCommande);
                cmd.Parameters.AddWithValue("@OrdreBonCommande", this.Ordre);
                cmd.Parameters.AddWithValue("@QuantiteHistorique", this.QuantiteHistorique);
                cmd.Parameters.AddWithValue("@QuantitePreparee", this.QuantitePreparee);

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

        public static BonCommandeDetail Charger(string nBonCommande, string cArticle, int ordre)
        {
            BonCommandeDetail bonCommandeDetail = null;
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
                            bonCommandeDetail = new BonCommandeDetail();
                            bonCommandeDetail.NBonCommande = dr["NBonCommande"].ToString();
                            bonCommandeDetail.CArticle = dr["CArticle"].ToString();
                            bonCommandeDetail.Ordre = int.Parse(dr["Ordre"].ToString());

                            if (dr["CEntrepot"] != DBNull.Value)
                                bonCommandeDetail.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["CTaxe"] != DBNull.Value)
                                bonCommandeDetail.CTaxe = dr["CTaxe"].ToString();
                            if (dr["CUnite"] != DBNull.Value)
                                bonCommandeDetail.CUnite = dr["CUnite"].ToString();
                            //if (dr["Epaisseur"] != DBNull.Value)
                            //    bonCommandeDetail.Epaisseur = decimal.Parse(dr["Epaisseur"].ToString());
                            //if (dr["Largeur"] != DBNull.Value)
                            //    bonCommandeDetail.Largeur = decimal.Parse(dr["Largeur"].ToString());
                            if (dr["LibArticle"] != DBNull.Value)
                                bonCommandeDetail.LibArticle = dr["LibArticle"].ToString();
                            if (dr["Poids"] != DBNull.Value)
                                bonCommandeDetail.Poids = decimal.Parse(dr["Poids"].ToString());
                            //if (dr["Longueur"] != DBNull.Value)
                            //    bonCommandeDetail.Longueur = decimal.Parse(dr["Longueur"].ToString());
                            if (dr["MontantNet"] != DBNull.Value)
                                bonCommandeDetail.MontantNet = decimal.Parse(dr["MontantNet"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                bonCommandeDetail.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["PourcentageFodec"] != DBNull.Value)
                                bonCommandeDetail.PourcentageFodec = decimal.Parse(dr["PourcentageFodec"].ToString());
                            if (dr["PourcentageRemise"] != DBNull.Value)
                                bonCommandeDetail.PourcentageRemise = decimal.Parse(dr["PourcentageRemise"].ToString());
                            if (dr["PrixHTArticle"] != DBNull.Value)
                                bonCommandeDetail.PrixHTArticle = decimal.Parse(dr["PrixHTArticle"].ToString());
                            if (dr["PrixVentePublic"] != DBNull.Value)
                                bonCommandeDetail.PrixVentePublic = decimal.Parse(dr["PrixVentePublic"].ToString());
                            if (dr["Quantite"] != DBNull.Value)
                                bonCommandeDetail.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["QuantiteHistorique"] != DBNull.Value)
                                bonCommandeDetail.QuantiteHistorique = decimal.Parse(dr["QuantiteHistorique"].ToString());
                            if (dr["QuantitePreparee"] != DBNull.Value)
                                bonCommandeDetail.QuantitePreparee = decimal.Parse(dr["QuantitePreparee"].ToString());
                            if (dr["QuantitePurge"] != DBNull.Value)
                                bonCommandeDetail.QuantitePurge = decimal.Parse(dr["QuantitePurge"].ToString());
                            if (dr["Remise1"] != DBNull.Value)
                                bonCommandeDetail.Remise1 = decimal.Parse(dr["Remise1"].ToString());
                            if (dr["Remise2"] != DBNull.Value)
                                bonCommandeDetail.Remise2 = decimal.Parse(dr["Remise2"].ToString());
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
        public static BonCommandeDetail Chargerr(string nBonCommande, string cArticle)
        {
            BonCommandeDetail bonCommandeDetail = null;
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
                    cmd.Parameters.AddWithValue("@Ordre", DBNull.Value);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            bonCommandeDetail = new BonCommandeDetail();
                            bonCommandeDetail.NBonCommande = dr["NBonCommande"].ToString();
                            bonCommandeDetail.CArticle = dr["CArticle"].ToString();
                            bonCommandeDetail.Ordre = int.Parse(dr["Ordre"].ToString());

                            if (dr["CEntrepot"] != DBNull.Value)
                                bonCommandeDetail.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["CTaxe"] != DBNull.Value)
                                bonCommandeDetail.CTaxe = dr["CTaxe"].ToString();
                            if (dr["CUnite"] != DBNull.Value)
                                bonCommandeDetail.CUnite = dr["CUnite"].ToString();
                            //if (dr["Epaisseur"] != DBNull.Value)
                            //    bonCommandeDetail.Epaisseur = decimal.Parse(dr["Epaisseur"].ToString());
                            //if (dr["Largeur"] != DBNull.Value)
                            //    bonCommandeDetail.Largeur = decimal.Parse(dr["Largeur"].ToString());
                            if (dr["LibArticle"] != DBNull.Value)
                                bonCommandeDetail.LibArticle = dr["LibArticle"].ToString();
                            if (dr["Poids"] != DBNull.Value)
                                bonCommandeDetail.Poids = decimal.Parse(dr["Poids"].ToString());
                            //if (dr["Longueur"] != DBNull.Value)
                            //    bonCommandeDetail.Longueur = decimal.Parse(dr["Longueur"].ToString());
                            if (dr["MontantNet"] != DBNull.Value)
                                bonCommandeDetail.MontantNet = decimal.Parse(dr["MontantNet"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                bonCommandeDetail.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["PourcentageFodec"] != DBNull.Value)
                                bonCommandeDetail.PourcentageFodec = decimal.Parse(dr["PourcentageFodec"].ToString());
                            if (dr["PourcentageRemise"] != DBNull.Value)
                                bonCommandeDetail.PourcentageRemise = decimal.Parse(dr["PourcentageRemise"].ToString());
                            if (dr["PrixHTArticle"] != DBNull.Value)
                                bonCommandeDetail.PrixHTArticle = decimal.Parse(dr["PrixHTArticle"].ToString());
                            if (dr["PrixVentePublic"] != DBNull.Value)
                                bonCommandeDetail.PrixVentePublic = decimal.Parse(dr["PrixVentePublic"].ToString());
                            if (dr["Quantite"] != DBNull.Value)
                                bonCommandeDetail.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["QuantiteHistorique"] != DBNull.Value)
                                bonCommandeDetail.QuantiteHistorique = decimal.Parse(dr["QuantiteHistorique"].ToString());
                            if (dr["QuantitePreparee"] != DBNull.Value)
                                bonCommandeDetail.QuantitePreparee = decimal.Parse(dr["QuantitePreparee"].ToString());
                            if (dr["QuantitePurge"] != DBNull.Value)
                                bonCommandeDetail.QuantitePurge = decimal.Parse(dr["QuantitePurge"].ToString());
                            if (dr["Remise1"] != DBNull.Value)
                                bonCommandeDetail.Remise1 = decimal.Parse(dr["Remise1"].ToString());
                            if (dr["Remise2"] != DBNull.Value)
                                bonCommandeDetail.Remise2 = decimal.Parse(dr["Remise2"].ToString());
                            if (dr["TauxTVA"] != DBNull.Value)
                                bonCommandeDetail.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());
                            if (dr["QuantiteOT"] != DBNull.Value)
                                bonCommandeDetail.QuantiteOT = decimal.Parse(dr["QuantiteOT"].ToString());
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

    public class BonCommandeDetailCollection : List<BonCommandeDetail>
    {
        public BonCommandeDetailCollection()
        {
        }

        public static BonCommandeDetailCollection Charger(string nBonCommande)
        {
            BonCommandeDetailCollection collection = new BonCommandeDetailCollection();

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
                    cmd.Parameters.AddWithValue("@CArticle", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Ordre", DBNull.Value);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            BonCommandeDetail bonCommandeDetail = new BonCommandeDetail();
                            bonCommandeDetail.NBonCommande = dr["NBonCommande"].ToString();
                            bonCommandeDetail.CArticle = dr["CArticle"].ToString();
                            bonCommandeDetail.Ordre = int.Parse(dr["Ordre"].ToString());

                            if (dr["CEntrepot"] != DBNull.Value)
                                bonCommandeDetail.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["CTaxe"] != DBNull.Value)
                                bonCommandeDetail.CTaxe = dr["CTaxe"].ToString();
                            if (dr["CUnite"] != DBNull.Value)
                                bonCommandeDetail.CUnite = dr["CUnite"].ToString();
                            if (dr["LibArticle"] != DBNull.Value)
                                bonCommandeDetail.LibArticle = dr["LibArticle"].ToString();
                            if (dr["Poids"] != DBNull.Value)
                                bonCommandeDetail.Poids = decimal.Parse(dr["Poids"].ToString());
                            //if (dr["Largeur"] != DBNull.Value)
                            //    bonCommandeDetail.Largeur = decimal.Parse(dr["Largeur"].ToString());
                            //if (dr["LibArticle"] != DBNull.Value)
                            //    bonCommandeDetail.LibArticle = dr["LibArticle"].ToString();
                            //if (dr["Longueur"] != DBNull.Value)
                            //    bonCommandeDetail.Longueur = decimal.Parse(dr["Longueur"].ToString());

                            if (dr["MontantNet"] != DBNull.Value)
                                bonCommandeDetail.MontantNet = decimal.Parse(dr["MontantNet"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                bonCommandeDetail.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["PourcentageFodec"] != DBNull.Value)
                                bonCommandeDetail.PourcentageFodec = decimal.Parse(dr["PourcentageFodec"].ToString());
                            if (dr["PourcentageRemise"] != DBNull.Value)
                                bonCommandeDetail.PourcentageRemise = decimal.Parse(dr["PourcentageRemise"].ToString());
                            if (dr["PrixHTArticle"] != DBNull.Value)
                                bonCommandeDetail.PrixHTArticle = decimal.Parse(dr["PrixHTArticle"].ToString());
                            if (dr["PrixVentePublic"] != DBNull.Value)
                                bonCommandeDetail.PrixVentePublic = decimal.Parse(dr["PrixVentePublic"].ToString());
                            if (dr["Quantite"] != DBNull.Value)
                                bonCommandeDetail.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["QuantiteHistorique"] != DBNull.Value)
                                bonCommandeDetail.QuantiteHistorique = decimal.Parse(dr["QuantiteHistorique"].ToString());
                            if (dr["QuantitePurge"] != DBNull.Value)
                                bonCommandeDetail.QuantitePurge = decimal.Parse(dr["QuantitePurge"].ToString());
                            if (dr["QuantitePreparee"] != DBNull.Value)
                                bonCommandeDetail.QuantitePreparee = decimal.Parse(dr["QuantitePreparee"].ToString());
                            if (dr["Remise1"] != DBNull.Value)
                                bonCommandeDetail.Remise1 = decimal.Parse(dr["Remise1"].ToString());
                            if (dr["Remise2"] != DBNull.Value)
                                bonCommandeDetail.Remise2 = decimal.Parse(dr["Remise2"].ToString());
                            if (dr["TauxTVA"] != DBNull.Value)
                                bonCommandeDetail.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());
                            if (dr["QuantiteOT"] != DBNull.Value)
                                bonCommandeDetail.QuantiteOT = decimal.Parse(dr["QuantiteOT"].ToString());
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

        public static DataSet ChargerVue(string nBonCommande)
        {
            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BonCommandeDetailRpt_Charger";
                cmd.Parameters.AddWithValue("@NBonCommande", nBonCommande);

                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        BonCommandeDetail bonCommandeDetail = new BonCommandeDetail();

                        bonCommandeDetail.CArticle = dr["CArticle"].ToString();
                        bonCommandeDetail.NBonCommande = dr["NBonCommande"].ToString();
                        bonCommandeDetail.Ordre = int.Parse(dr["Ordre"].ToString());
                    }
                }

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "BonCommandeRptDataSet");
            }
            return (ds);
        }

        public BonCommandeDetail RecupererBonCommandeDetail(string nBonCommande, string cArticle, int ordre)
        {
            BonCommandeDetail bonCommandeDetail = null;
            bonCommandeDetail = this.Where(p => p.NBonCommande.Equals(nBonCommande) && p.CArticle.Equals(cArticle) && p.Ordre == ordre).FirstOrDefault();
            return bonCommandeDetail;
        }

        public BonCommandeDetail RecupererBonCommandeDetail(string cArticle)
        {
            BonCommandeDetail bonCommandeDetail = null;
            bonCommandeDetail = this.Where(p => p.CArticle.Equals(cArticle)).FirstOrDefault();
            return bonCommandeDetail;
        }
    }
}