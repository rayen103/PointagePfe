using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace CST.LePoint.Vente.Metier
{
    public class DevisDetail
    {
        #region Propriétès

        [XmlAttribute("NDevis")]
        [Bindable(true)]
        public string NDevis { get; set; }

        [XmlAttribute("CArticle")]
        [Bindable(true)]
        public string CArticle { get; set; }

        [XmlAttribute("Ordre")]
        [Bindable(true)]
        public int Ordre { get; set; }

        [XmlAttribute("CUnite")]
        [Bindable(true)]
        public string CUnite { get; set; }

        [XmlAttribute("CEntrepot")]
        [Bindable(true)]
        public string CEntrepot { get; set; }

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

        [XmlAttribute("PrixHT")]
        [Bindable(true)]
        public decimal PrixHT { get; set; }

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

        #endregion Propriétès

        public void Sauvegarder(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DevisDetail_Inserer";

                cmd.Parameters.AddWithValue("@NDevis", this.NDevis);
                cmd.Parameters.AddWithValue("@CArticle", this.CArticle);
                cmd.Parameters.AddWithValue("@Ordre", this.Ordre);
                cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
                cmd.Parameters.AddWithValue("@CUnite", this.CUnite);
                cmd.Parameters.AddWithValue("@CTaxe", this.CTaxe);
                cmd.Parameters.AddWithValue("@TauxTVA", this.TauxTVA);
                cmd.Parameters.AddWithValue("@LibArticle", this.LibArticle);
                cmd.Parameters.AddWithValue("@Poids", this.Poids);
                cmd.Parameters.AddWithValue("@PourcentageFodec", this.PourcentageFodec);
                cmd.Parameters.AddWithValue("@PourcentageRemise", this.PourcentageRemise);
                cmd.Parameters.AddWithValue("@Quantite", this.Quantite);
                //cmd.Parameters.AddWithValue("@Largeur", this.Largeur);
                //cmd.Parameters.AddWithValue("@Longueur", this.Longueur);
                //cmd.Parameters.AddWithValue("@Epaisseur", this.Epaisseur);
                cmd.Parameters.AddWithValue("@MontantNet", this.MontantNet);
                cmd.Parameters.AddWithValue("@MontantTaxe", this.MontantTaxe);
                cmd.Parameters.AddWithValue("@PrixHT", this.PrixHT);
                cmd.Parameters.AddWithValue("@PrixVentePublic", this.PrixVentePublic);
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
                cmd.CommandText = "DevisDetail_Supprimer";

                cmd.Parameters.AddWithValue("@NDevis", this.NDevis);
                cmd.Parameters.AddWithValue("@CArticle", this.CArticle);
                cmd.Parameters.AddWithValue("@Ordre", this.Ordre);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DevisDetail Charger(string nDevis, string cArticle, int ordre)
        {
            DevisDetail devisDetail = null;
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
                    cmd.CommandText = "DevisDetail_Charger";
                    cmd.Parameters.AddWithValue("@NDevis", nDevis);
                    cmd.Parameters.AddWithValue("@CArticle", cArticle);
                    cmd.Parameters.AddWithValue("@Ordre", ordre);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            devisDetail = new DevisDetail();
                            devisDetail.NDevis = dr["NBonEntree"].ToString();
                            devisDetail.CArticle = dr["CArticle"].ToString();
                            devisDetail.Ordre = int.Parse(dr["Ordre"].ToString());

                            if (dr["CUnite"] != DBNull.Value)
                                devisDetail.CUnite = dr["CUnite"].ToString();
                            if (dr["CTaxe"] != DBNull.Value)
                                devisDetail.CTaxe = dr["CTaxe"].ToString();
                            if (dr["CEntrepot"] != DBNull.Value)
                                devisDetail.CUnite = dr["CEntrepot"].ToString();
                            //if (dr["Epaisseur"] != DBNull.Value)
                            //    DevisDetail.Epaisseur = decimal.Parse(dr["Epaisseur"].ToString());
                            //if (dr["Largeur"] != DBNull.Value)
                            //    DevisDetail.Largeur = decimal.Parse(dr["Largeur"].ToString());
                            if (dr["LibArticle"] != DBNull.Value)
                                devisDetail.LibArticle = dr["LibArticle"].ToString();

                            if (dr["Poids"] != DBNull.Value)
                                devisDetail.Poids = decimal.Parse(dr["Poids"].ToString());
                            //if (dr["Longueur"] != DBNull.Value)
                            //    DevisDetail.Longueur = decimal.Parse(dr["Longueur"].ToString());
                            if (dr["MontantNet"] != DBNull.Value)
                                devisDetail.MontantNet = decimal.Parse(dr["MontantNet"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                devisDetail.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["PourcentageFodec"] != DBNull.Value)
                                devisDetail.PourcentageFodec = decimal.Parse(dr["PourcentageFodec"].ToString());
                            if (dr["PourcentageRemise"] != DBNull.Value)
                                devisDetail.PourcentageRemise = decimal.Parse(dr["PourcentageRemise"].ToString());
                            if (dr["PrixHT"] != DBNull.Value)
                                devisDetail.PrixHT = decimal.Parse(dr["PrixHT"].ToString());
                            if (dr["PrixVentePublic"] != DBNull.Value)
                                devisDetail.PrixVentePublic = decimal.Parse(dr["PrixVentePublic"].ToString());
                            if (dr["Quantite"] != DBNull.Value)
                                devisDetail.Quantite = decimal.Parse(dr["Quantite"].ToString());

                            if (dr["Remise1"] != DBNull.Value)
                                devisDetail.Remise1 = decimal.Parse(dr["Remise1"].ToString());
                            if (dr["Remise2"] != DBNull.Value)
                                devisDetail.Remise2 = decimal.Parse(dr["Remise2"].ToString());
                            if (dr["TauxTVA"] != DBNull.Value)
                                devisDetail.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return (devisDetail);
            }
        }
    }

    public class DevisDetailCollection : List<DevisDetail>
    {
        public DevisDetailCollection()
        {
        }
        public static DataSet ChargerVue(string nDevis)
        {
            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DevisDetailRpt_Charger";
                cmd.Parameters.AddWithValue("@NDevis", nDevis);

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

                        DevisDetail devisDetail = new DevisDetail();

                        devisDetail.CArticle = dr["CArticle"].ToString();
                        devisDetail.Ordre = int.Parse(dr["Ordre"].ToString());
                    }
                }

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "DevisRptDataSet");
            }
            return (ds);
        }
        public static DevisDetailCollection Charger(string nDevis)
        {
            DevisDetailCollection collection = new DevisDetailCollection();

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
                    cmd.CommandText = "DevisDetail_Charger";
                    cmd.Parameters.AddWithValue("@NDevis", nDevis);
                    cmd.Parameters.AddWithValue("@CArticle", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Ordre", DBNull.Value);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            DevisDetail devisDetail = new DevisDetail();
                            devisDetail.NDevis = dr["NDevis"].ToString();
                            devisDetail.CArticle = dr["CArticle"].ToString();
                            devisDetail.Ordre = int.Parse(dr["Ordre"].ToString());

                            if (dr["CEntrepot"] != DBNull.Value)
                                devisDetail.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["CTaxe"] != DBNull.Value)
                                devisDetail.CTaxe = dr["CTaxe"].ToString();
                            if (dr["CUnite"] != DBNull.Value)
                                devisDetail.CUnite = dr["CUnite"].ToString();
                            //if (dr["Epaisseur"] != DBNull.Value)
                            //    DevisDetail.Epaisseur = decimal.Parse(dr["Epaisseur"].ToString());
                            //if (dr["Largeur"] != DBNull.Value)
                            //    DevisDetail.Largeur = decimal.Parse(dr["Largeur"].ToString());
                            if (dr["LibArticle"] != DBNull.Value)
                                devisDetail.LibArticle = dr["LibArticle"].ToString();
                            if (dr["Poids"] != DBNull.Value)
                                devisDetail.Poids = decimal.Parse(dr["Poids"].ToString());
                            //if (dr["Longueur"] != DBNull.Value)
                            //    DevisDetail.Longueur = decimal.Parse(dr["Longueur"].ToString());
                            if (dr["MontantNet"] != DBNull.Value)
                                devisDetail.MontantNet = decimal.Parse(dr["MontantNet"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                devisDetail.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["PourcentageFodec"] != DBNull.Value)
                                devisDetail.PourcentageFodec = decimal.Parse(dr["PourcentageFodec"].ToString());
                            if (dr["PourcentageRemise"] != DBNull.Value)
                                devisDetail.PourcentageRemise = decimal.Parse(dr["PourcentageRemise"].ToString());
                            if (dr["PrixHT"] != DBNull.Value)
                                devisDetail.PrixHT = decimal.Parse(dr["PrixHT"].ToString());
                            if (dr["PrixVentePublic"] != DBNull.Value)
                                devisDetail.PrixVentePublic = decimal.Parse(dr["PrixVentePublic"].ToString());
                            if (dr["Quantite"] != DBNull.Value)
                                devisDetail.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["Remise1"] != DBNull.Value)
                                devisDetail.Remise1 = decimal.Parse(dr["Remise1"].ToString());
                            if (dr["Remise2"] != DBNull.Value)
                                devisDetail.Remise2 = decimal.Parse(dr["Remise2"].ToString());
                            if (dr["TauxTVA"] != DBNull.Value)
                                devisDetail.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());
                            collection.Add(devisDetail);
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