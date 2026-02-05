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
    public class FactureDetail
    {
        #region Proriétès

        [XmlAttribute("NFacture")]
        [Bindable(true)]
        public string NFacture { get; set; }

        [XmlAttribute("CArticle")]
        [Bindable(true)]
        public string CArticle { get; set; }

        [XmlAttribute("NBonLivraison")]
        [Bindable(true)]
        public string NBonLivraison { get; set; }

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

        [XmlAttribute("PourcentageFodec")]
        [Bindable(true)]
        public decimal PourcentageFodec { get; set; }

        [XmlAttribute("PourcentageRemise")]
        [Bindable(true)]
        public decimal PourcentageRemise { get; set; }

        [XmlAttribute("PrixHistorique")]
        [Bindable(true)]
        public decimal PrixHistorique { get; set; }

        [XmlAttribute("PrixHT")]
        [Bindable(true)]
        public decimal PrixHT { get; set; }

        [XmlAttribute("PrixRevient")]
        [Bindable(true)]
        public decimal PrixRevient { get; set; }

        [XmlAttribute("Quantite")]
        [Bindable(true)]
        public decimal Quantite { get; set; }

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

        //[XmlAttribute("Longueur")]
        //[Bindable(true)]
        //public decimal Longueur { get; set; }

        //[XmlAttribute("Largeur")]
        //[Bindable(true)]
        //public decimal Largeur { get; set; }

        //[XmlAttribute("Epaisseur")]
        //[Bindable(true)]
        //public decimal Epaisseur { get; set; }

        [XmlAttribute("MontantNet")]
        [Bindable(true)]
        public decimal MontantNet { get; set; }

        #endregion Proriétès

        public FactureDetail()
        {
        }

        public void Sauvegarder(SqlTransaction transaction)
        {
         
            try
            {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Transaction = transaction;
                    cmd.Connection = transaction.Connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "FactureDetail_Sauvegarder";

                    cmd.Parameters.AddWithValue("@NFacture", this.NFacture);
                    cmd.Parameters.AddWithValue("@CArticle", this.CArticle);
                    cmd.Parameters.AddWithValue("@Ordre", this.Ordre);
                    cmd.Parameters.AddWithValue("@NBonLivraison", this.NBonLivraison);
                    cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
                    cmd.Parameters.AddWithValue("@CUnite", this.CUnite);
                    cmd.Parameters.AddWithValue("@LibArticle", this.LibArticle);
                    cmd.Parameters.AddWithValue("@Poids", this.Poids);
                    cmd.Parameters.AddWithValue("@MontantTaxe", this.MontantTaxe);
                    cmd.Parameters.AddWithValue("@PourcentageFodec", this.PourcentageFodec);
                    cmd.Parameters.AddWithValue("@PourcentageRemise", this.PourcentageRemise);
                    cmd.Parameters.AddWithValue("@PrixHistorique", this.PrixHistorique);
                    cmd.Parameters.AddWithValue("@PrixHT", this.PrixHT);
                    cmd.Parameters.AddWithValue("@PrixRevient", this.PrixRevient);
                    cmd.Parameters.AddWithValue("@Quantite", this.Quantite);
                    cmd.Parameters.AddWithValue("@TauxTVA", this.TauxTVA);
                    cmd.Parameters.AddWithValue("@CTaxe", this.CTaxe);
                    cmd.Parameters.AddWithValue("@Remise1", this.Remise1);
                    cmd.Parameters.AddWithValue("@Remise2", this.Remise2);
                    cmd.Parameters.AddWithValue("@PrixVentePublic", this.PrixVentePublic);
                    //cmd.Parameters.AddWithValue("@Longueur", this.Longueur);
                    //cmd.Parameters.AddWithValue("@Largeur", this.Largeur);
                    //cmd.Parameters.AddWithValue("@Epaisseur", this.Epaisseur);
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
        public void SauvegarderFactureDetailSupprimer(SqlTransaction transaction, int ordre)
        {

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "FactureSupprimerDetail_Sauvegarder";

                cmd.Parameters.AddWithValue("@NFacture", this.NFacture);
                cmd.Parameters.AddWithValue("@CArticle", this.CArticle);
                cmd.Parameters.AddWithValue("@Ordre", this.Ordre);
                cmd.Parameters.AddWithValue("@NBonLivraison", this.NBonLivraison);
                cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
                cmd.Parameters.AddWithValue("@CUnite", this.CUnite);
                cmd.Parameters.AddWithValue("@LibArticle", this.LibArticle);
                cmd.Parameters.AddWithValue("@MontantTaxe", this.MontantTaxe);
                cmd.Parameters.AddWithValue("@PourcentageFodec", this.PourcentageFodec);
                cmd.Parameters.AddWithValue("@PourcentageRemise", this.PourcentageRemise);
                cmd.Parameters.AddWithValue("@PrixHistorique", this.PrixHistorique);
                cmd.Parameters.AddWithValue("@PrixHT", this.PrixHT);
                cmd.Parameters.AddWithValue("@PrixRevient", this.PrixRevient);
                cmd.Parameters.AddWithValue("@Quantite", this.Quantite);
                cmd.Parameters.AddWithValue("@TauxTVA", this.TauxTVA);
                cmd.Parameters.AddWithValue("@CTaxe", this.CTaxe);
                cmd.Parameters.AddWithValue("@Remise1", this.Remise1);
                cmd.Parameters.AddWithValue("@Remise2", this.Remise2);
                cmd.Parameters.AddWithValue("@PrixVentePublic", this.PrixVentePublic);
                //cmd.Parameters.AddWithValue("@Longueur", this.Longueur);
                //cmd.Parameters.AddWithValue("@Largeur", this.Largeur);
                //cmd.Parameters.AddWithValue("@Epaisseur", this.Epaisseur);
                cmd.Parameters.AddWithValue("@MontantNet", this.MontantNet);
                cmd.Parameters.AddWithValue("@OrdreFacture", ordre);
                cmd.Parameters.AddWithValue("@Poids", Poids);
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

        public void Supprimer(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "FactureDetail_Supprimer";

                cmd.Parameters.AddWithValue("@NFacture", this.NFacture);
                cmd.Parameters.AddWithValue("@CArticle", this.CArticle);
                cmd.Parameters.AddWithValue("@Ordre", this.Ordre);
                cmd.Parameters.AddWithValue("@NBonLivraison", this.NBonLivraison);
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

        public FactureDetail Charger(string nFacture, string cArticle, string nBonLivraison, int ordre)
        {
            FactureDetail factureDetail = null;
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
                    cmd.CommandText = "FactureDetail_Charger";
                    cmd.Parameters.AddWithValue("@NFacture", nFacture);
                    cmd.Parameters.AddWithValue("@CArticle", cArticle);
                    cmd.Parameters.AddWithValue("@NBonLivraison", nBonLivraison);
                    cmd.Parameters.AddWithValue("@Ordre", ordre);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            factureDetail = new FactureDetail();
                            factureDetail.NFacture = dr["NFacture"].ToString();
                            factureDetail.CArticle = dr["CArticle"].ToString();
                            factureDetail.NBonLivraison = dr["NBonLivraison"].ToString();
                            factureDetail.Ordre = int.Parse(dr["Ordre"].ToString());

                            if (dr["CEntrepot"] != DBNull.Value)
                                factureDetail.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["CUnite"] != DBNull.Value)
                                factureDetail.CUnite = dr["CUnite"].ToString();
                            if (dr["LibArticle"] != DBNull.Value)
                                factureDetail.LibArticle = dr["LibArticle"].ToString();
                            if (dr["Poids"] != DBNull.Value)
                                factureDetail.Poids = decimal.Parse(dr["Poids"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                factureDetail.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["PourcentageFodec"] != DBNull.Value)
                                factureDetail.PourcentageFodec = decimal.Parse(dr["PourcentageFodec"].ToString());
                            if (dr["PourcentageRemise"] != DBNull.Value)
                                factureDetail.PourcentageRemise = decimal.Parse(dr["PourcentageRemise"].ToString());
                            if (dr["PrixHistorique"] != DBNull.Value)
                                factureDetail.PrixHistorique = decimal.Parse(dr["PrixHistorique"].ToString());
                            if (dr["PrixHT"] != DBNull.Value)
                                factureDetail.PrixHT = decimal.Parse(dr["PrixHT"].ToString());
                            if (dr["PrixRevient"] != DBNull.Value)
                                factureDetail.PrixRevient = decimal.Parse(dr["PrixRevient"].ToString());
                            if (dr["Quantite"] != DBNull.Value)
                                factureDetail.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["TauxTVA"] != DBNull.Value)
                                factureDetail.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());
                            if (dr["CTaxe"] != DBNull.Value)
                                factureDetail.CTaxe = dr["CTaxe"].ToString();
                            if (dr["Remise1"] != DBNull.Value)
                                factureDetail.Remise1 = decimal.Parse(dr["Remise1"].ToString());
                            if (dr["Remise2"] != DBNull.Value)
                                factureDetail.Remise2 = decimal.Parse(dr["Remise2"].ToString());
                            if (dr["PrixVentePublic"] != DBNull.Value)
                                factureDetail.PrixVentePublic = decimal.Parse(dr["PrixVentePublic"].ToString());
                            //if (dr["Longueur"] != DBNull.Value)
                            //    factureDetail.Longueur = decimal.Parse(dr["Longueur"].ToString());
                            //if (dr["Largeur"] != DBNull.Value)
                            //    factureDetail.Largeur = decimal.Parse(dr["Largeur"].ToString());
                            //if (dr["Epaisseur"] != DBNull.Value)
                            //    factureDetail.Epaisseur = decimal.Parse(dr["Epaisseur"].ToString());
                            if (dr["MontantNet"] != DBNull.Value)
                                factureDetail.MontantNet = decimal.Parse(dr["MontantNet"].ToString());
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return (factureDetail);
            }
        }
    }

    public class FactureDetailCollection : List<FactureDetail>
    {
        public FactureDetailCollection()
        {
        }

        public FactureDetail RecupererFactureDetail(string nBonLivraison)
        {
            FactureDetail factureDetail = null;
            factureDetail = this.Where(p => p.NBonLivraison.Equals(nBonLivraison)).FirstOrDefault();
            return factureDetail;
        }
        public FactureDetail RecupererFactureDetail(string nBonLivraison, string cArticle, int ordre)
        {
            FactureDetail factureDetail = null;
            factureDetail = this.Where(p => p.NBonLivraison.Equals(nBonLivraison)&& p.CArticle.Equals(cArticle)&& p.Ordre.Equals(ordre)).FirstOrDefault();
            return factureDetail;
        }
        public static DataSet ChargerVue(string nFacture)
        {
            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BLFactureDetailRpt_Charger";
                cmd.Parameters.AddWithValue("@NFacture", nFacture);

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
                        FactureDetail FactureDetail = new FactureDetail();

                        FactureDetail.CArticle = dr["CArticle"].ToString();
                        FactureDetail.NFacture = dr["NFacture"].ToString();
                        FactureDetail.Ordre = int.Parse(dr["Ordre"].ToString());
                    }
                }

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "FactureRptDataSet");
            }
            return (ds);
        }

        public static FactureDetailCollection Charger(string nFacture, SqlTransaction transaction)
        {
            FactureDetailCollection collection = new FactureDetailCollection();

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
         
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Transaction = transaction;
                    cmd.Connection = transaction.Connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "FactureDetail_Charger";
                    cmd.Parameters.AddWithValue("@NFacture", nFacture);
                    cmd.Parameters.AddWithValue("@CArticle", DBNull.Value);
                    cmd.Parameters.AddWithValue("@NBonLivraison", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Ordre", DBNull.Value);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            FactureDetail factureDetail = new FactureDetail();
                            factureDetail.NFacture = dr["NFacture"].ToString();
                            factureDetail.CArticle = dr["CArticle"].ToString();
                            factureDetail.NBonLivraison = dr["NBonLivraison"].ToString();
                            factureDetail.Ordre = int.Parse(dr["Ordre"].ToString());

                            if (dr["CEntrepot"] != DBNull.Value)
                                factureDetail.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["CUnite"] != DBNull.Value)
                                factureDetail.CUnite = dr["CUnite"].ToString();
                            if (dr["LibArticle"] != DBNull.Value)
                                factureDetail.LibArticle = dr["LibArticle"].ToString();
                            if (dr["Poids"] != DBNull.Value)
                                factureDetail.Poids = decimal.Parse(dr["Poids"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                factureDetail.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["PourcentageFodec"] != DBNull.Value)
                                factureDetail.PourcentageFodec = decimal.Parse(dr["PourcentageFodec"].ToString());
                            if (dr["PourcentageRemise"] != DBNull.Value)
                                factureDetail.PourcentageRemise = decimal.Parse(dr["PourcentageRemise"].ToString());
                            if (dr["PrixHistorique"] != DBNull.Value)
                                factureDetail.PrixHistorique = decimal.Parse(dr["PrixHistorique"].ToString());
                            if (dr["PrixHT"] != DBNull.Value)
                                factureDetail.PrixHT = decimal.Parse(dr["PrixHT"].ToString());
                            if (dr["PrixRevient"] != DBNull.Value)
                                factureDetail.PrixRevient = decimal.Parse(dr["PrixRevient"].ToString());
                            if (dr["Quantite"] != DBNull.Value)
                                factureDetail.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["TauxTVA"] != DBNull.Value)
                                factureDetail.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());
                            if (dr["CTaxe"] != DBNull.Value)
                                factureDetail.CTaxe = dr["CTaxe"].ToString();
                            if (dr["Remise1"] != DBNull.Value)
                                factureDetail.Remise1 = decimal.Parse(dr["Remise1"].ToString());
                            if (dr["Remise2"] != DBNull.Value)
                                factureDetail.Remise2 = decimal.Parse(dr["Remise2"].ToString());
                            if (dr["PrixVentePublic"] != DBNull.Value)
                                factureDetail.PrixVentePublic = decimal.Parse(dr["PrixVentePublic"].ToString());
                            //if (dr["Longueur"] != DBNull.Value)
                            //    factureDetail.Longueur = decimal.Parse(dr["Longueur"].ToString());
                            //if (dr["Largeur"] != DBNull.Value)
                            //    factureDetail.Largeur = decimal.Parse(dr["Largeur"].ToString());
                            //if (dr["Epaisseur"] != DBNull.Value)
                            //    factureDetail.Epaisseur = decimal.Parse(dr["Epaisseur"].ToString());
                            if (dr["MontantNet"] != DBNull.Value)
                                factureDetail.MontantNet = decimal.Parse(dr["MontantNet"].ToString());
                            collection.Add(factureDetail);
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
        public static FactureDetailCollection Charger(string nFacture)
        {
            FactureDetailCollection collection = new FactureDetailCollection();

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
                    cmd.CommandText = "FactureDetail_Charger";
                    cmd.Parameters.AddWithValue("@NFacture", nFacture);
                    cmd.Parameters.AddWithValue("@CArticle", DBNull.Value);
                    cmd.Parameters.AddWithValue("@NBonLivraison", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Ordre", DBNull.Value);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            FactureDetail factureDetail = new FactureDetail();
                            factureDetail.NFacture = dr["NFacture"].ToString();
                            factureDetail.CArticle = dr["CArticle"].ToString();
                            factureDetail.NBonLivraison = dr["NBonLivraison"].ToString();
                            factureDetail.Ordre = int.Parse(dr["Ordre"].ToString());

                            if (dr["CEntrepot"] != DBNull.Value)
                                factureDetail.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["CUnite"] != DBNull.Value)
                                factureDetail.CUnite = dr["CUnite"].ToString();
                            if (dr["LibArticle"] != DBNull.Value)
                                factureDetail.LibArticle = dr["LibArticle"].ToString();
                            if (dr["Poids"] != DBNull.Value)
                                factureDetail.Poids = decimal.Parse(dr["Poids"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                factureDetail.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["PourcentageFodec"] != DBNull.Value)
                                factureDetail.PourcentageFodec = decimal.Parse(dr["PourcentageFodec"].ToString());
                            if (dr["PourcentageRemise"] != DBNull.Value)
                                factureDetail.PourcentageRemise = decimal.Parse(dr["PourcentageRemise"].ToString());
                            if (dr["PrixHistorique"] != DBNull.Value)
                                factureDetail.PrixHistorique = decimal.Parse(dr["PrixHistorique"].ToString());
                            if (dr["PrixHT"] != DBNull.Value)
                                factureDetail.PrixHT = decimal.Parse(dr["PrixHT"].ToString());
                            if (dr["PrixRevient"] != DBNull.Value)
                                factureDetail.PrixRevient = decimal.Parse(dr["PrixRevient"].ToString());
                            if (dr["Quantite"] != DBNull.Value)
                                factureDetail.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["TauxTVA"] != DBNull.Value)
                                factureDetail.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());
                            if (dr["CTaxe"] != DBNull.Value)
                                factureDetail.CTaxe = dr["CTaxe"].ToString();
                            if (dr["Remise1"] != DBNull.Value)
                                factureDetail.Remise1 = decimal.Parse(dr["Remise1"].ToString());
                            if (dr["Remise2"] != DBNull.Value)
                                factureDetail.Remise2 = decimal.Parse(dr["Remise2"].ToString());
                            if (dr["PrixVentePublic"] != DBNull.Value)
                                factureDetail.PrixVentePublic = decimal.Parse(dr["PrixVentePublic"].ToString());
                            //if (dr["Longueur"] != DBNull.Value)
                            //    factureDetail.Longueur = decimal.Parse(dr["Longueur"].ToString());
                            //if (dr["Largeur"] != DBNull.Value)
                            //    factureDetail.Largeur = decimal.Parse(dr["Largeur"].ToString());
                            //if (dr["Epaisseur"] != DBNull.Value)
                            //    factureDetail.Epaisseur = decimal.Parse(dr["Epaisseur"].ToString());
                            if (dr["MontantNet"] != DBNull.Value)
                                factureDetail.MontantNet = decimal.Parse(dr["MontantNet"].ToString());
                            collection.Add(factureDetail);
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