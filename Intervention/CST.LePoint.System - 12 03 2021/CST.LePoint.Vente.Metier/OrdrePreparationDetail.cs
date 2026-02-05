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
    public class OrdrePreparationDetail
    {
        #region Proriétès

        [XmlAttribute("NOrdrePreparation")]
        [Bindable(true)]
        public string NOrdrePreparation { get; set; }

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

        [XmlAttribute("Poids")]
        [Bindable(true)]
        public decimal Poids { get; set; }

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

        [XmlAttribute("BSpecial")]
        [Bindable(true)]
        public bool BSpecial { get; set; }


        #endregion Proriétès

        public OrdrePreparationDetail()
        {
        }

        public OrdrePreparationDetail(string NOrdrePreparation)
        {
            this.NOrdrePreparation = NOrdrePreparation;
        }

        public void Sauvegarder(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "OrdrePreparationDetail_Sauvegarder";

                cmd.Parameters.AddWithValue("@NOrdrePreparation", this.NOrdrePreparation);
                cmd.Parameters.AddWithValue("@CArticle", this.CArticle);
                cmd.Parameters.AddWithValue("@Ordre", this.Ordre);

                cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
                cmd.Parameters.AddWithValue("@CUnite", this.CUnite);
                cmd.Parameters.AddWithValue("@LibArticle", this.LibArticle);
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
                cmd.Parameters.AddWithValue("@OrdreBonCommande", this.OrdreBonCommande);
                cmd.Parameters.AddWithValue("@Poids", this.Poids);
                cmd.Parameters.AddWithValue("@BSpecial", this.BSpecial);
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

                cmd.CommandText = "OrdrePreparation_AjusterQuantiteHistorique";
                cmd.Parameters.AddWithValue("@CArticle", this.CArticle);
                cmd.Parameters.AddWithValue("NBonCommande", this.NOrdrePreparation);
                cmd.Parameters.AddWithValue("OrdreBonCommande", this.Ordre);
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
                cmd.CommandText = "OrdrePreparationDetail_Supprimer";

                cmd.Parameters.AddWithValue("@NOrdrePreparation", this.NOrdrePreparation);
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

        public static OrdrePreparationDetail Charger(string NOrdrePreparation, string cArticle, int ordre)
        {
            OrdrePreparationDetail ordrePreparationDetail = null;
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
                    cmd.CommandText = "OrdrePreparationDetail_Charger";
                    cmd.Parameters.AddWithValue("@NOrdrePreparation", NOrdrePreparation);
                    cmd.Parameters.AddWithValue("@CArticle", cArticle);
                    cmd.Parameters.AddWithValue("@Ordre", ordre);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            ordrePreparationDetail = new OrdrePreparationDetail();
                            ordrePreparationDetail.CArticle = dr["CArticle"].ToString();
                            ordrePreparationDetail.NOrdrePreparation = dr["NOrdrePreparation"].ToString();
                            ordrePreparationDetail.Ordre = int.Parse(dr["Ordre"].ToString());

                            if (dr["CEntrepot"] != DBNull.Value)
                                ordrePreparationDetail.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["CUnite"] != DBNull.Value)
                                ordrePreparationDetail.CUnite = dr["CUnite"].ToString();
                            if (dr["LibArticle"] != DBNull.Value)
                                ordrePreparationDetail.LibArticle = dr["LibArticle"].ToString();
                            if (dr["MontantTaxe"] != DBNull.Value)
                                ordrePreparationDetail.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["PourcentageFodec"] != DBNull.Value)
                                ordrePreparationDetail.PourcentageFodec = decimal.Parse(dr["PourcentageFodec"].ToString());
                            if (dr["PourcentageRemise"] != DBNull.Value)
                                ordrePreparationDetail.PourcentageRemise = decimal.Parse(dr["PourcentageRemise"].ToString());
                            if (dr["PrixHT"] != DBNull.Value)
                                ordrePreparationDetail.PrixHT = decimal.Parse(dr["PrixHT"].ToString());
                            if (dr["PrixRevient"] != DBNull.Value)
                                ordrePreparationDetail.PrixRevient = decimal.Parse(dr["PrixRevient"].ToString());
                            if (dr["Quantite"] != DBNull.Value)
                                ordrePreparationDetail.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["QuantiteHistorique"] != DBNull.Value)
                                ordrePreparationDetail.QuantiteHistorique = decimal.Parse(dr["QuantiteHistorique"].ToString());
                            if (dr["TauxTVA"] != DBNull.Value)
                                ordrePreparationDetail.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());
                            if (dr["CTaxe"] != DBNull.Value)
                                ordrePreparationDetail.CTaxe = dr["CTaxe"].ToString();
                            if (dr["Remise1"] != DBNull.Value)
                                ordrePreparationDetail.Remise1 = decimal.Parse(dr["Remise1"].ToString());
                            if (dr["Remise2"] != DBNull.Value)
                                ordrePreparationDetail.Remise2 = decimal.Parse(dr["Remise2"].ToString());
                            if (dr["PrixVentePublic"] != DBNull.Value)
                                ordrePreparationDetail.PrixVentePublic = decimal.Parse(dr["PrixVentePublic"].ToString());

                            if (dr["Poids"] != DBNull.Value)
                                ordrePreparationDetail.Poids = decimal.Parse(dr["Poids"].ToString());

                            if (dr["BSpecial"] != DBNull.Value)
                                ordrePreparationDetail.BSpecial = bool.Parse(dr["BSpecial"].ToString());

                            if (dr["OrdreBonCommande"] != DBNull.Value)
                                ordrePreparationDetail.OrdreBonCommande = int.Parse(dr["OrdreBonCommande"].ToString());
                            //if (dr["Longueur"] != DBNull.Value)
                            //    OrdrePreparationDetail.Longueur = decimal.Parse(dr["Longueur"].ToString());
                            //if (dr["Largeur"] != DBNull.Value)
                            //    OrdrePreparationDetail.Largeur = decimal.Parse(dr["Largeur"].ToString());
                            //if (dr["Epaisseur"] != DBNull.Value)
                            //    OrdrePreparationDetail.Epaisseur = decimal.Parse(dr["Epaisseur"].ToString());

                            if (dr["MontantNet"] != DBNull.Value)
                                ordrePreparationDetail.MontantNet = decimal.Parse(dr["MontantNet"].ToString());
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return (ordrePreparationDetail);
            }
        }
    }

    public class OrdrePreparationDetailCollection : List<OrdrePreparationDetail>
    {
        public OrdrePreparationDetailCollection()
        {
        }

        public static OrdrePreparationDetailCollection Charger(string nOrdrePreparation)
        {
            OrdrePreparationDetailCollection collection = new OrdrePreparationDetailCollection();

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
                    cmd.CommandText = "OrdrePreparationDetail_Charger";

                    cmd.Parameters.AddWithValue("@NOrdrePreparation", nOrdrePreparation);
                    cmd.Parameters.AddWithValue("@CArticle", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Ordre", DBNull.Value);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            OrdrePreparationDetail ordrePreparationDetail = new OrdrePreparationDetail();

                            ordrePreparationDetail.CArticle = dr["CArticle"].ToString();
                            ordrePreparationDetail.NOrdrePreparation = dr["NOrdrePreparation"].ToString();
                            ordrePreparationDetail.Ordre = int.Parse(dr["Ordre"].ToString());

                            if (dr["CEntrepot"] != DBNull.Value)
                                ordrePreparationDetail.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["CUnite"] != DBNull.Value)
                                ordrePreparationDetail.CUnite = dr["CUnite"].ToString();
                            if (dr["LibArticle"] != DBNull.Value)
                                ordrePreparationDetail.LibArticle = dr["LibArticle"].ToString();
                            if (dr["MontantTaxe"] != DBNull.Value)
                                ordrePreparationDetail.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["PourcentageFodec"] != DBNull.Value)
                                ordrePreparationDetail.PourcentageFodec = decimal.Parse(dr["PourcentageFodec"].ToString());
                            if (dr["PourcentageRemise"] != DBNull.Value)
                                ordrePreparationDetail.PourcentageRemise = decimal.Parse(dr["PourcentageRemise"].ToString());
                            if (dr["PrixHT"] != DBNull.Value)
                                ordrePreparationDetail.PrixHT = decimal.Parse(dr["PrixHT"].ToString());
                            if (dr["PrixRevient"] != DBNull.Value)
                                ordrePreparationDetail.PrixRevient = decimal.Parse(dr["PrixRevient"].ToString());
                            if (dr["Quantite"] != DBNull.Value)
                                ordrePreparationDetail.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["QuantiteHistorique"] != DBNull.Value)
                                ordrePreparationDetail.QuantiteHistorique = decimal.Parse(dr["QuantiteHistorique"].ToString());
                            if (dr["TauxTVA"] != DBNull.Value)
                                ordrePreparationDetail.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());
                            if (dr["CTaxe"] != DBNull.Value)
                                ordrePreparationDetail.CTaxe = dr["CTaxe"].ToString();
                            if (dr["Remise1"] != DBNull.Value)
                                ordrePreparationDetail.Remise1 = decimal.Parse(dr["Remise1"].ToString());
                            if (dr["Remise2"] != DBNull.Value)
                                ordrePreparationDetail.Remise2 = decimal.Parse(dr["Remise2"].ToString());
                            if (dr["PrixVentePublic"] != DBNull.Value)
                                ordrePreparationDetail.PrixVentePublic = decimal.Parse(dr["PrixVentePublic"].ToString());

                            if (dr["Poids"] != DBNull.Value)
                                ordrePreparationDetail.Poids = decimal.Parse(dr["Poids"].ToString());

                            if (dr["BSpecial"] != DBNull.Value)
                                ordrePreparationDetail.BSpecial = bool.Parse(dr["BSpecial"].ToString());
                            if (dr["OrdreBonCommande"] != DBNull.Value)
                                ordrePreparationDetail.OrdreBonCommande = int.Parse(dr["OrdreBonCommande"].ToString());

                            if (dr["MontantNet"] != DBNull.Value)
                                ordrePreparationDetail.MontantNet = decimal.Parse(dr["MontantNet"].ToString());
                            collection.Add(ordrePreparationDetail);
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

        public OrdrePreparationDetail RecupererOrdrePreparationDetail(string cEntrepot)
        {
            OrdrePreparationDetail ordrePreparationDetail = null;
            ordrePreparationDetail = this.Where(p => p.CEntrepot.Equals(cEntrepot)).FirstOrDefault();
            return ordrePreparationDetail;
        }

        public OrdrePreparationDetail RecupererOrdrePreparationDetail(string cEntrepot, string cArticle)
        {
            OrdrePreparationDetail ordrePreparationDetail = null;
            ordrePreparationDetail = this.Where(p => p.CEntrepot.Equals(cEntrepot) && p.CArticle.Equals(cArticle)).FirstOrDefault();
            return ordrePreparationDetail;
        }

        public OrdrePreparationDetail RecupererOrdrePreparationDetail(string NOrdrePreparation, string cArticle, int ordreBonCommande)
        {
            OrdrePreparationDetail ordrePreparationDetail = null;
            ordrePreparationDetail = this.Where(p => p.NOrdrePreparation.Equals(NOrdrePreparation) && p.CArticle.Equals(cArticle) && p.OrdreBonCommande == ordreBonCommande).FirstOrDefault();
            return ordrePreparationDetail;
        }
    }
}