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
    public class BonRetourDetail
    {
        #region Propriétès

        [XmlAttribute("NBonRetour")]
        [Bindable(true)]
        public string NBonRetour { get; set; }

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

        [XmlAttribute("MontantNet")]
        [Bindable(true)]
        public decimal MontantNet { get; set; }

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

        [XmlAttribute("OrdreBonLivraison")]
        [Bindable(true)]
        public int OrdreBonLivraison { get; set; }

        //[XmlAttribute("Longueur")]
        //[Bindable(true)]
        //public decimal Longueur { get; set; }

        //[XmlAttribute("Largeur")]
        //[Bindable(true)]
        //public decimal Largeur { get; set; }

        //[XmlAttribute("Epaisseur")]
        //[Bindable(true)]
        //public decimal Epaisseur { get; set; }

        #endregion Propriétès

        public BonRetourDetail()
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
                cmd.CommandText = "BonRetourDetail_Inserer";

                cmd.Parameters.AddWithValue("@NBonRetour", this.NBonRetour);
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
                cmd.Parameters.AddWithValue("@OrdreBonLivraison", this.OrdreBonLivraison);
                //cmd.Parameters.AddWithValue("@Largeur", this.Largeur);
                //cmd.Parameters.AddWithValue("@Longeur", this.Longueur);
                //cmd.Parameters.AddWithValue("@Epaisseur", this.Epaisseur);
                cmd.Parameters.AddWithValue("@MontantNet", this.MontantNet);
                cmd.Parameters.AddWithValue("@MontantTaxe", this.MontantTaxe);
                cmd.Parameters.AddWithValue("@PrixHT", this.PrixHT);
                cmd.Parameters.AddWithValue("@PrixRevient", this.PrixRevient);
                cmd.Parameters.AddWithValue("@Remise1", this.Remise1);
                cmd.Parameters.AddWithValue("@Remise2", this.Remise2);

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
                cmd.CommandText = "BonRetourDetail_Supprimer";

                cmd.Parameters.AddWithValue("@NBonRetour", this.NBonRetour);
                cmd.Parameters.AddWithValue("@CArticle", this.CArticle);
                cmd.Parameters.AddWithValue("@Ordre", this.Ordre);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static BonRetourDetail Charger(string nBonRetour, string cArticle, int ordre)
        {
            BonRetourDetail bonRetourDetail = null;
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
                    cmd.CommandText = "BonRetourDetail_Charger";
                    cmd.Parameters.AddWithValue("@NBonRetour", nBonRetour);
                    cmd.Parameters.AddWithValue("@CArticle", cArticle);
                    cmd.Parameters.AddWithValue("@Ordre", ordre);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            bonRetourDetail = new BonRetourDetail();
                            bonRetourDetail.NBonRetour = dr["NBonRetour"].ToString();
                            bonRetourDetail.CArticle = dr["CArticle"].ToString();
                            bonRetourDetail.Ordre = int.Parse(dr["Ordre"].ToString());

                            if (dr["CEntrepot"] != DBNull.Value)
                                bonRetourDetail.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["CTaxe"] != DBNull.Value)
                                bonRetourDetail.CTaxe = dr["CTaxe"].ToString();
                            if (dr["CUnite"] != DBNull.Value)
                                bonRetourDetail.CUnite = dr["CUnite"].ToString();
                            if (dr["LibArticle"] != DBNull.Value)
                                bonRetourDetail.LibArticle = dr["LibArticle"].ToString();
                            if (dr["Poids"] != DBNull.Value)
                                bonRetourDetail.Poids = decimal.Parse(dr["Poids"].ToString());
                            if (dr["MontantNet"] != DBNull.Value)
                                bonRetourDetail.MontantNet = decimal.Parse(dr["MontantNet"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                bonRetourDetail.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["PourcentageFodec"] != DBNull.Value)
                                bonRetourDetail.PourcentageFodec = decimal.Parse(dr["PourcentageFodec"].ToString());
                            if (dr["PourcentageRemise"] != DBNull.Value)
                                bonRetourDetail.PourcentageRemise = decimal.Parse(dr["PourcentageRemise"].ToString());
                            if (dr["PrixHT"] != DBNull.Value)
                                bonRetourDetail.PrixHT = decimal.Parse(dr["PrixHT"].ToString());
                            if (dr["PrixRevient"] != DBNull.Value)
                                bonRetourDetail.PrixRevient = decimal.Parse(dr["PrixRevient"].ToString());
                            if (dr["Quantite"] != DBNull.Value)
                                bonRetourDetail.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["OrdreBonLivraison"] != DBNull.Value)
                                bonRetourDetail.OrdreBonLivraison = int.Parse(dr["OrdreBonLivraison"].ToString());

                            if (dr["Remise1"] != DBNull.Value)
                                bonRetourDetail.Remise1 = decimal.Parse(dr["Remise1"].ToString());
                            if (dr["Remise2"] != DBNull.Value)
                                bonRetourDetail.Remise2 = decimal.Parse(dr["Remise2"].ToString());
                            if (dr["TauxTVA"] != DBNull.Value)
                                bonRetourDetail.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return (bonRetourDetail);
            }
        }
    }

    public class BonRetourDetailCollection : List<BonRetourDetail>
    {
        public BonRetourDetailCollection()
        {
        }
        public static DataSet ChargerVue(string nBonRetour)
        {
            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn; 
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BonRetourDetailRpt_Charger";
                cmd.Parameters.AddWithValue("@NBonRetour", nBonRetour);

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
                        bonLivraisonDetail.NBonLivraison = dr["NBonRetour"].ToString();
                        bonLivraisonDetail.Ordre = int.Parse(dr["Ordre"].ToString());
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "BonRetourDataSet");
            }
            return (ds);
        }
        public static BonRetourDetailCollection Charger(string nBonRetour)
        {
            BonRetourDetailCollection collection = new BonRetourDetailCollection();

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
                    cmd.CommandText = "BonRetourDetail_Charger";
                    cmd.Parameters.AddWithValue("@NBonRetour", nBonRetour);
                    cmd.Parameters.AddWithValue("@CArticle", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Ordre", DBNull.Value);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            BonRetourDetail bonRetourDetail = new BonRetourDetail();
                            bonRetourDetail.NBonRetour = dr["NBonRetour"].ToString();
                            bonRetourDetail.CArticle = dr["CArticle"].ToString();
                            bonRetourDetail.Ordre = int.Parse(dr["Ordre"].ToString());

                            if (dr["CEntrepot"] != DBNull.Value)
                                bonRetourDetail.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["CTaxe"] != DBNull.Value)
                                bonRetourDetail.CTaxe = dr["CTaxe"].ToString();
                            if (dr["CUnite"] != DBNull.Value)
                                bonRetourDetail.CUnite = dr["CUnite"].ToString();
                            //if (dr["Epaisseur"] != DBNull.Value)
                            //    bonRetourDetail.Epaisseur = decimal.Parse(dr["Epaisseur"].ToString());
                            //if (dr["Largeur"] != DBNull.Value)
                            //    bonRetourDetail.Largeur = decimal.Parse(dr["Largeur"].ToString());
                            if (dr["LibArticle"] != DBNull.Value)
                                bonRetourDetail.LibArticle = dr["LibArticle"].ToString();
                            if (dr["Poids"] != DBNull.Value)
                                bonRetourDetail.Poids = decimal.Parse(dr["Poids"].ToString());
                            //if (dr["Longeur"] != DBNull.Value)
                            //    bonRetourDetail.Longueur = decimal.Parse(dr["Longeur"].ToString());
                            if (dr["MontantNet"] != DBNull.Value)
                                bonRetourDetail.MontantNet = decimal.Parse(dr["MontantNet"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                bonRetourDetail.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["PourcentageFodec"] != DBNull.Value)
                                bonRetourDetail.PourcentageFodec = decimal.Parse(dr["PourcentageFodec"].ToString());
                            if (dr["PourcentageRemise"] != DBNull.Value)
                                bonRetourDetail.PourcentageRemise = decimal.Parse(dr["PourcentageRemise"].ToString());
                            if (dr["PrixHT"] != DBNull.Value)
                                bonRetourDetail.PrixHT = decimal.Parse(dr["PrixHT"].ToString());
                            if (dr["PrixRevient"] != DBNull.Value)
                                bonRetourDetail.PrixRevient = decimal.Parse(dr["PrixRevient"].ToString());
                            if (dr["Quantite"] != DBNull.Value)
                                bonRetourDetail.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["OrdreBonLivraison"] != DBNull.Value)
                                bonRetourDetail.OrdreBonLivraison = int.Parse(dr["OrdreBonLivraison"].ToString());

                            if (dr["Remise1"] != DBNull.Value)
                                bonRetourDetail.Remise1 = decimal.Parse(dr["Remise1"].ToString());
                            if (dr["Remise2"] != DBNull.Value)
                                bonRetourDetail.Remise2 = decimal.Parse(dr["Remise2"].ToString());
                            if (dr["TauxTVA"] != DBNull.Value)
                                bonRetourDetail.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());
                            collection.Add(bonRetourDetail);
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

        public BonRetourDetail RecupererBonRetourDetail(string cEntrepot, string cArticle)
        {
            BonRetourDetail bonRetourInterneDetail = null;
            bonRetourInterneDetail = this.Where(p => p.CEntrepot.Equals(cEntrepot) && p.CArticle.Equals(cArticle)).FirstOrDefault();
            return bonRetourInterneDetail;
        }

        public BonRetourDetail RecupererBonRetourDetail(string cEntrepot)
        {
            BonRetourDetail bonRetourDetail = null;
            bonRetourDetail = this.Where(p => p.CEntrepot.Equals(cEntrepot)).FirstOrDefault();
            return bonRetourDetail;
        }
        public BonRetourDetail RecupererBonRetourDetail(string cEntrepot, string cArticle, string nBonRetour)
        {
            BonRetourDetail bonRetourInterneDetail = null;
            bonRetourInterneDetail = this.Where(p => p.NBonRetour.Equals(nBonRetour) && p.CArticle.Equals(cArticle)&& p.CEntrepot.Equals(cEntrepot)).FirstOrDefault();
            return bonRetourInterneDetail;
        }
    }
}