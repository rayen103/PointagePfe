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
    public class BonLivraisonDetail
    {
        #region Proriétès

        [XmlAttribute("NBonLivraison")]
        [Bindable(true)]
        public string NBonLivraison { get; set; }

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

        [XmlAttribute("PourcentageFodec")]
        [Bindable(true)]
        public decimal PourcentageFodec { get; set; }

        [XmlAttribute("PourcentageRemise")]
        [Bindable(true)]
        public decimal PourcentageRemise { get; set; }

        [XmlAttribute("PrixHT")]
        [Bindable(true)]
        public decimal PrixHT { get; set; }

        [XmlAttribute("PrixRevient")]
        [Bindable(true)]
        public decimal PrixRevient { get; set; }

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

        [XmlAttribute("Remise1")]
        [Bindable(true)]
        public decimal Remise1 { get; set; }

        [XmlAttribute("Remise2")]
        [Bindable(true)]
        public decimal Remise2 { get; set; }

        [XmlAttribute("PrixVentePublic")]
        [Bindable(true)]
        public decimal PrixVentePublic { get; set; }

        [XmlAttribute("QuantiteRestore")]
        [Bindable(true)]
        public decimal QuantiteRestore { get; set; }

        [XmlAttribute("OrdreBonCommande")]
        [Bindable(true)]
        public int OrdreBonCommande { get; set; }

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

        public BonLivraisonDetail()
        {
        }

        public BonLivraisonDetail(string nBonLivraison)
        {
            this.NBonLivraison = nBonLivraison;
        }

        public void Sauvegarder(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BonLivraisonDetail_Sauvegarder";

                cmd.Parameters.AddWithValue("@NBonLivraison", this.NBonLivraison);
                cmd.Parameters.AddWithValue("@CArticle", this.CArticle);
                cmd.Parameters.AddWithValue("@Ordre", this.Ordre);

                cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
                cmd.Parameters.AddWithValue("@CUnite", this.CUnite);
                cmd.Parameters.AddWithValue("@LibArticle", this.LibArticle);
                cmd.Parameters.AddWithValue("@Poids", this.Poids);
                cmd.Parameters.AddWithValue("@MontantTaxe", this.MontantTaxe);
                cmd.Parameters.AddWithValue("@PourcentageFodec", this.PourcentageFodec);
                cmd.Parameters.AddWithValue("@PourcentageRemise", this.PourcentageRemise);
                cmd.Parameters.AddWithValue("@PrixHT", this.PrixHT);
                cmd.Parameters.AddWithValue("@PrixRevient", this.PrixRevient);
                cmd.Parameters.AddWithValue("@Quantite", this.Quantite);
                cmd.Parameters.AddWithValue("@QuantiteHistorique", this.QuantiteHistorique);
                cmd.Parameters.AddWithValue("@TauxTVA", this.TauxTVA);
                cmd.Parameters.AddWithValue("@CTaxe", this.CTaxe);
                cmd.Parameters.AddWithValue("@Remise1", this.Remise1);
                cmd.Parameters.AddWithValue("@Remise2", this.Remise2);
                cmd.Parameters.AddWithValue("@PrixVentePublic", this.PrixVentePublic);
                cmd.Parameters.AddWithValue("@QuantiteRestore", this.QuantiteRestore);
                cmd.Parameters.AddWithValue("@OrdreBonCommande", this.OrdreBonCommande);
                //cmd.Parameters.AddWithValue("@Longueur", this.Longueur);
                //cmd.Parameters.AddWithValue("@Largeur", this.Largeur);
                //cmd.Parameters.AddWithValue("@Epaisseur", this.Epaisseur);
                cmd.Parameters.AddWithValue("@MontantNet", this.MontantNet);

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

        public void Modifier(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "BonLivraison_AjusterQuantiteHistorique";
                cmd.Parameters.AddWithValue("@CArticle", this.CArticle);
                cmd.Parameters.AddWithValue("@NBonLivraison", this.NBonLivraison);
                cmd.Parameters.AddWithValue("@OrdreBonCommande", this.Ordre);
                cmd.Parameters.AddWithValue("@QuantiteHistorique", this.QuantiteHistorique);

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
                cmd.CommandText = "BonLivraisonDetail_Supprimer";

                cmd.Parameters.AddWithValue("@NBonLivraison", this.NBonLivraison);
                cmd.Parameters.AddWithValue("@CArticle", this.CArticle);
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

        public static BonLivraisonDetail Charger(string nBonLivraison, string cArticle, int ordre)
        {
            BonLivraisonDetail bonLivraisonDetail = null;
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
                    cmd.CommandText = "BonLivraisonDetail_Charger";
                    cmd.Parameters.AddWithValue("@NBonLivraison", nBonLivraison);
                    cmd.Parameters.AddWithValue("@CArticle", cArticle);
                    cmd.Parameters.AddWithValue("@Ordre", ordre);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            bonLivraisonDetail = new BonLivraisonDetail();
                            bonLivraisonDetail.CArticle = dr["CArticle"].ToString();
                            bonLivraisonDetail.NBonLivraison = dr["NBonLivraison"].ToString();
                            bonLivraisonDetail.Ordre = int.Parse(dr["Ordre"].ToString());

                            if (dr["CEntrepot"] != DBNull.Value)
                                bonLivraisonDetail.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["CUnite"] != DBNull.Value)
                                bonLivraisonDetail.CUnite = dr["CUnite"].ToString();
                            if (dr["LibArticle"] != DBNull.Value)
                                bonLivraisonDetail.LibArticle = dr["LibArticle"].ToString();
                            if (dr["Poids"] != DBNull.Value)
                                bonLivraisonDetail.Poids = decimal.Parse(dr["Poids"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                bonLivraisonDetail.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["PourcentageFodec"] != DBNull.Value)
                                bonLivraisonDetail.PourcentageFodec = decimal.Parse(dr["PourcentageFodec"].ToString());
                            if (dr["PourcentageRemise"] != DBNull.Value)
                                bonLivraisonDetail.PourcentageRemise = decimal.Parse(dr["PourcentageRemise"].ToString());
                            if (dr["PrixHT"] != DBNull.Value)
                                bonLivraisonDetail.PrixHT = decimal.Parse(dr["PrixHT"].ToString());
                            if (dr["PrixRevient"] != DBNull.Value)
                                bonLivraisonDetail.PrixRevient = decimal.Parse(dr["PrixRevient"].ToString());
                            if (dr["Quantite"] != DBNull.Value)
                                bonLivraisonDetail.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["QuantiteHistorique"] != DBNull.Value)
                                bonLivraisonDetail.QuantiteHistorique = decimal.Parse(dr["QuantiteHistorique"].ToString());
                            if (dr["TauxTVA"] != DBNull.Value)
                                bonLivraisonDetail.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());
                            if (dr["CTaxe"] != DBNull.Value)
                                bonLivraisonDetail.CTaxe = dr["CTaxe"].ToString();
                            if (dr["Remise1"] != DBNull.Value)
                                bonLivraisonDetail.Remise1 = decimal.Parse(dr["Remise1"].ToString());
                            if (dr["Remise2"] != DBNull.Value)
                                bonLivraisonDetail.Remise2 = decimal.Parse(dr["Remise2"].ToString());
                            if (dr["PrixVentePublic"] != DBNull.Value)
                                bonLivraisonDetail.PrixVentePublic = decimal.Parse(dr["PrixVentePublic"].ToString());
                            if (dr["QuantiteRestore"] != DBNull.Value)
                                bonLivraisonDetail.QuantiteRestore = decimal.Parse(dr["QuantiteRestore"].ToString());
                            if (dr["OrdreBonCommande"] != DBNull.Value)
                                bonLivraisonDetail.OrdreBonCommande = int.Parse(dr["OrdreBonCommande"].ToString());
                            //if (dr["Longueur"] != DBNull.Value)
                            //    bonLivraisonDetail.Longueur = decimal.Parse(dr["Longueur"].ToString());
                            //if (dr["Largeur"] != DBNull.Value)
                            //    bonLivraisonDetail.Largeur = decimal.Parse(dr["Largeur"].ToString());
                            //if (dr["Epaisseur"] != DBNull.Value)
                            //    bonLivraisonDetail.Epaisseur = decimal.Parse(dr["Epaisseur"].ToString());

                            if (dr["MontantNet"] != DBNull.Value)
                                bonLivraisonDetail.MontantNet = decimal.Parse(dr["MontantNet"].ToString());
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return (bonLivraisonDetail);
            }
        }
    }

    public class BonLivraisonDetailCollection : List<BonLivraisonDetail>
    {
        public BonLivraisonDetailCollection()
        {
        }

        public static BonLivraisonDetailCollection Charger(string nBonLivraison)
        {
            BonLivraisonDetailCollection collection = new BonLivraisonDetailCollection();

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
                    cmd.CommandText = "BonLivraisonDetail_Charger";

                    cmd.Parameters.AddWithValue("@NBonLivraison", nBonLivraison);
                    cmd.Parameters.AddWithValue("@CArticle", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Ordre", DBNull.Value);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            BonLivraisonDetail bonLivraisonDetail = new BonLivraisonDetail();

                            bonLivraisonDetail.CArticle = dr["CArticle"].ToString();
                            bonLivraisonDetail.NBonLivraison = dr["NBonLivraison"].ToString();
                            bonLivraisonDetail.Ordre = int.Parse(dr["Ordre"].ToString());

                            if (dr["CEntrepot"] != DBNull.Value)
                                bonLivraisonDetail.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["CUnite"] != DBNull.Value)
                                bonLivraisonDetail.CUnite = dr["CUnite"].ToString();
                            if (dr["LibArticle"] != DBNull.Value)
                                bonLivraisonDetail.LibArticle = dr["LibArticle"].ToString();
                            if (dr["Poids"] != DBNull.Value)
                                bonLivraisonDetail.Poids = decimal.Parse(dr["Poids"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                bonLivraisonDetail.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["PourcentageFodec"] != DBNull.Value)
                                bonLivraisonDetail.PourcentageFodec = decimal.Parse(dr["PourcentageFodec"].ToString());
                            if (dr["PourcentageRemise"] != DBNull.Value)
                                bonLivraisonDetail.PourcentageRemise = decimal.Parse(dr["PourcentageRemise"].ToString());
                            if (dr["PrixHT"] != DBNull.Value)
                                bonLivraisonDetail.PrixHT = decimal.Parse(dr["PrixHT"].ToString());
                            if (dr["PrixRevient"] != DBNull.Value)
                                bonLivraisonDetail.PrixRevient = decimal.Parse(dr["PrixRevient"].ToString());
                            if (dr["Quantite"] != DBNull.Value)
                                bonLivraisonDetail.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["QuantiteHistorique"] != DBNull.Value)
                                bonLivraisonDetail.QuantiteHistorique = decimal.Parse(dr["QuantiteHistorique"].ToString());
                            if (dr["TauxTVA"] != DBNull.Value)
                                bonLivraisonDetail.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());
                            if (dr["CTaxe"] != DBNull.Value)
                                bonLivraisonDetail.CTaxe = dr["CTaxe"].ToString();
                            if (dr["Remise1"] != DBNull.Value)
                                bonLivraisonDetail.Remise1 = decimal.Parse(dr["Remise1"].ToString());
                            if (dr["Remise2"] != DBNull.Value)
                                bonLivraisonDetail.Remise2 = decimal.Parse(dr["Remise2"].ToString());
                            if (dr["PrixVentePublic"] != DBNull.Value)
                                bonLivraisonDetail.PrixVentePublic = decimal.Parse(dr["PrixVentePublic"].ToString());
                            if (dr["QuantiteRestore"] != DBNull.Value)
                                bonLivraisonDetail.QuantiteRestore = decimal.Parse(dr["QuantiteRestore"].ToString());
                            if (dr["OrdreBonCommande"] != DBNull.Value)
                                bonLivraisonDetail.OrdreBonCommande = int.Parse(dr["OrdreBonCommande"].ToString());
                            //if (dr["Longueur"] != DBNull.Value)
                            //    bonLivraisonDetail.Longueur = decimal.Parse(dr["Longueur"].ToString());
                            //if (dr["Largeur"] != DBNull.Value)
                            //    bonLivraisonDetail.Largeur = decimal.Parse(dr["Largeur"].ToString());
                            //if (dr["Epaisseur"] != DBNull.Value)
                            //    bonLivraisonDetail.Epaisseur = decimal.Parse(dr["Epaisseur"].ToString());

                            if (dr["MontantNet"] != DBNull.Value)
                                bonLivraisonDetail.MontantNet = decimal.Parse(dr["MontantNet"].ToString());
                            collection.Add(bonLivraisonDetail);
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
        public static DataSet ChargerVue(string nBonLivraison)
        {
            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BonLivraisonDetailRpt_Charger";
                cmd.Parameters.AddWithValue("@NBonLivraison", nBonLivraison);
                
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

                BonLivraisonDetail bonLivraisonDetail = new BonLivraisonDetail();

                bonLivraisonDetail.CArticle = dr["CArticle"].ToString();
                bonLivraisonDetail.NBonLivraison = dr["NBonLivraison"].ToString();
                bonLivraisonDetail.Ordre = int.Parse(dr["Ordre"].ToString());
                        }
                    }

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "BonLivraisonRptDataSet");
            }
            return (ds);
        }

        //public BonLivraisonDetail RecupererBonLivraisonDetail(string nBonLivraison, string cArticle)
        //{
        //    BonLivraisonDetail bonLivraisonDetail = null;
        //    bonLivraisonDetail = this.Where(p => p.NBonLivraison.Equals(nBonLivraison) && p.CArticle.Equals(cArticle)).FirstOrDefault();
        //    return bonLivraisonDetail;
        //}

        public BonLivraisonDetail RecupererBonLivraisonDetail(string cEntrepot)
        {
            BonLivraisonDetail bonLivraisonDetail = null;
            bonLivraisonDetail = this.Where(p => p.CEntrepot.Equals(cEntrepot)).FirstOrDefault();
            return bonLivraisonDetail;
        }

        public BonLivraisonDetail RecupererBonLivraisonDetail(string cEntrepot, string cArticle)
        {
            BonLivraisonDetail bonLivraisonDetail = null;
            bonLivraisonDetail = this.Where(p => p.CEntrepot.Equals(cEntrepot) && p.CArticle.Equals(cArticle)).FirstOrDefault();
            return bonLivraisonDetail;
        }
        public BonLivraisonDetail RecupererBonLivraisonDetail(string nBonLivraison, string cArticle, int ordre)
        {
            BonLivraisonDetail bonLivraisonDetail = null;
            bonLivraisonDetail = this.Where(p => p.NBonLivraison.Equals(nBonLivraison) && p.CArticle.Equals(cArticle) && p.Ordre == ordre).FirstOrDefault();
            return bonLivraisonDetail;
        }
    }
}