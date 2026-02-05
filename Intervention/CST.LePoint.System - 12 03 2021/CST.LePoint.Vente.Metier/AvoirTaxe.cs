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
    public class AvoirTaxe
    {
        #region Propriètés

        [XmlAttribute("NAvoir")]
        [Bindable(true)]
        public string NAvoir { get; set; }

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

        public AvoirTaxe()
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
                cmd.CommandText = "AvoirTaxe_Sauvegarder";

                cmd.Parameters.AddWithValue("@NAvoir", this.NAvoir);
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

        public void SauvegarderTrac(int orderAvoir,SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AvoirTaxe_SauvegarderTrac";

                cmd.Parameters.AddWithValue("@NAvoir", this.NAvoir);
                cmd.Parameters.AddWithValue("@OrderAvoir", orderAvoir);
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
                cmd.CommandText = "AvoirTaxe_Supprimer";

                cmd.Parameters.AddWithValue("@NAvoir", this.NAvoir);
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

        public static AvoirTaxe Charger(string nAvoir, string cTaxe)
        {
            AvoirTaxe avoirTaxe = null;

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
                    cmd.CommandText = "AvoirTaxe_Charger";
                    cmd.Parameters.AddWithValue("@NAvoir", nAvoir);
                    cmd.Parameters.AddWithValue("@CTaxe", cTaxe);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            avoirTaxe = new AvoirTaxe();
                            avoirTaxe.NAvoir = dr["NAvoir"].ToString();
                            if (dr["CTaxe"] != DBNull.Value)
                                avoirTaxe.CTaxe = dr["CTaxe"].ToString();
                            if (dr["Assiette"] != DBNull.Value)
                                avoirTaxe.Assiette = decimal.Parse(dr["Assiette"].ToString());
                            if (dr["BExonoreFodec"] != DBNull.Value)
                                avoirTaxe.BExonoreFodec = bool.Parse(dr["BExonoreFodec"].ToString());
                            if (dr["BExonoreTVA"] != DBNull.Value)
                                avoirTaxe.BExonoreTVA = bool.Parse(dr["BExonoreTVA"].ToString());
                            if (dr["BExport"] != DBNull.Value)
                                avoirTaxe.BExport = bool.Parse(dr["BExport"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                avoirTaxe.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["TauxTVA"] != DBNull.Value)
                                avoirTaxe.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return avoirTaxe;
            }
        }
    }

    public class AvoirTaxeCollection : List<AvoirTaxe>
    {
        public static AvoirTaxeCollection Charger(string nAvoir)
        {
            AvoirTaxeCollection collection = new AvoirTaxeCollection();

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
                    cmd.CommandText = "AvoirTaxe_Charger";
                    cmd.Parameters.AddWithValue("@NAvoir", nAvoir);
                    cmd.Parameters.AddWithValue("@CTaxe", DBNull.Value);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            AvoirTaxe avoirTaxe = new AvoirTaxe();
                            avoirTaxe.NAvoir = dr["NAvoir"].ToString();
                            if (dr["CTaxe"] != DBNull.Value)
                                avoirTaxe.CTaxe = dr["CTaxe"].ToString();
                            if (dr["Assiette"] != DBNull.Value)
                                avoirTaxe.Assiette = decimal.Parse(dr["Assiette"].ToString());
                            if (dr["BExonoreFodec"] != DBNull.Value)
                                avoirTaxe.BExonoreFodec = bool.Parse(dr["BExonoreFodec"].ToString());
                            if (dr["BExonoreTVA"] != DBNull.Value)
                                avoirTaxe.BExonoreTVA = bool.Parse(dr["BExonoreTVA"].ToString());
                            if (dr["BExport"] != DBNull.Value)
                                avoirTaxe.BExport = bool.Parse(dr["BExport"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                avoirTaxe.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["TauxTVA"] != DBNull.Value)
                                avoirTaxe.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());

                            collection.Add(avoirTaxe);
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

        public AvoirTaxe RecupererAvoirTaxe(string cTaxe)
        {
            AvoirTaxe avoirTaxe = null;
            avoirTaxe = this.Where(p => p.CTaxe.Equals(cTaxe)).FirstOrDefault();
            return avoirTaxe;
        }
    }
}