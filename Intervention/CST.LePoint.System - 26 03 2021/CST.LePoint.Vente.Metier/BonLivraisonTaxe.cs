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
    public class BonLivraisonTaxe
    {
        #region Propriètés

        [XmlAttribute("NBonLivraison")]
        [Bindable(true)]
        public string NBonLivraison { get; set; }

        [XmlAttribute("CTaxe")]
        [Bindable(true)]
        public string CTaxe { get; set; }

        [XmlAttribute("Assiette")]
        [Bindable(true)]
        public decimal Assiette { get; set; }

        [XmlAttribute("BExonoreFodec")]
        [Bindable(true)]
        public bool BExonoreFodec { get; set; }

        [XmlAttribute("BExonoreTVA")]
        [Bindable(true)]
        public bool BExonoreTVA { get; set; }

        [XmlAttribute("BExport")]
        [Bindable(true)]
        public bool BExport { get; set; }

        [XmlAttribute("MontantTaxe")]
        [Bindable(true)]
        public decimal MontantTaxe { get; set; }

        [XmlAttribute("TauxTVA")]
        [Bindable(true)]
        public decimal TauxTVA { get; set; }

        #endregion Propriètés

        public BonLivraisonTaxe()
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
                cmd.CommandText = "BonLivraisonTaxe_Sauvegarder";

                cmd.Parameters.AddWithValue("@NBonLivraison", this.NBonLivraison);
                cmd.Parameters.AddWithValue("@CTaxe", this.CTaxe);
                cmd.Parameters.AddWithValue("@TauxTVA", this.TauxTVA);
                cmd.Parameters.AddWithValue("@Assiette", this.Assiette);
                cmd.Parameters.AddWithValue("@BExonoreFodec", this.BExonoreFodec);
                cmd.Parameters.AddWithValue("@BExonoreTVA", this.BExonoreTVA);
                cmd.Parameters.AddWithValue("@BExport", this.BExport);
                cmd.Parameters.AddWithValue("@MontantTaxe", this.MontantTaxe);

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

        public void Supprimer(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BonLivraisonTaxe_Supprimer";

                cmd.Parameters.AddWithValue("@NBonLivraison", this.NBonLivraison);
                cmd.Parameters.AddWithValue("@CTaxe", this.CTaxe);

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

        public static BonLivraisonTaxe Charger(string nBonLivraison, string cTaxe)
        {
            BonLivraisonTaxe bonLivraison = null;

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
                    cmd.Parameters.AddWithValue("@CTaxe", cTaxe);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            bonLivraison = new BonLivraisonTaxe();
                            bonLivraison.NBonLivraison = dr["NBonLivraison"].ToString();
                            if (dr["CTaxe"] != DBNull.Value)
                                bonLivraison.CTaxe = dr["CTaxe"].ToString();
                            if (dr["Assiette"] != DBNull.Value)
                                bonLivraison.Assiette = decimal.Parse(dr["Assiette"].ToString());
                            if (dr["BExonoreFodec"] != DBNull.Value)
                                bonLivraison.BExonoreFodec = bool.Parse(dr["BExonoreFodec"].ToString());
                            if (dr["BExonoreTVA"] != DBNull.Value)
                                bonLivraison.BExonoreTVA = bool.Parse(dr["BExonoreTVA"].ToString());
                            if (dr["BExport"] != DBNull.Value)
                                bonLivraison.BExport = bool.Parse(dr["BExport"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                bonLivraison.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["TauxTVA"] != DBNull.Value)
                                bonLivraison.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return bonLivraison;
            }
        }
    }

    public class BonLivraisonTaxeCollection : List<BonLivraisonTaxe>
    {
        public static BonLivraisonTaxeCollection Charger(string nBonLivraison)
        {
            BonLivraisonTaxeCollection collection = new BonLivraisonTaxeCollection();

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
                    cmd.CommandText = "BonLivraisonTaxe_Charger";
                    cmd.Parameters.AddWithValue("@NBonLivraison", nBonLivraison);
                    cmd.Parameters.AddWithValue("@CTaxe", DBNull.Value);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            BonLivraisonTaxe bonLivraison = new BonLivraisonTaxe();
                            bonLivraison.NBonLivraison = dr["NBonLivraison"].ToString();
                            if (dr["CTaxe"] != DBNull.Value)
                                bonLivraison.CTaxe = dr["CTaxe"].ToString();
                            if (dr["Assiette"] != DBNull.Value)
                                bonLivraison.Assiette = decimal.Parse(dr["Assiette"].ToString());
                            if (dr["BExonoreFodec"] != DBNull.Value)
                                bonLivraison.BExonoreFodec = bool.Parse(dr["BExonoreFodec"].ToString());
                            if (dr["BExonoreTVA"] != DBNull.Value)
                                bonLivraison.BExonoreTVA = bool.Parse(dr["BExonoreTVA"].ToString());
                            if (dr["BExport"] != DBNull.Value)
                                bonLivraison.BExport = bool.Parse(dr["BExport"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                bonLivraison.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["TauxTVA"] != DBNull.Value)
                                bonLivraison.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());

                            collection.Add(bonLivraison);
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

        public BonLivraisonTaxe RecupererBonLivraisonTaxe(string cTaxe)
        {
            BonLivraisonTaxe bonLivraisonTaxe = null;
            bonLivraisonTaxe = this.Where(p => p.CTaxe.Equals(cTaxe)).FirstOrDefault();
            return bonLivraisonTaxe;
        }
    }
}