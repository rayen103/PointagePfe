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
    public class OrdrePreparationTaxe
    {
        #region Propriètés

        [XmlAttribute("NOrdrePreparation")]
        [Bindable(true)]
        public string NOrdrePreparation { get; set; }

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

        public OrdrePreparationTaxe()
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
                cmd.CommandText = "OrdrePreparationTaxe_Sauvegarder";

                cmd.Parameters.AddWithValue("@NOrdrePreparation", this.NOrdrePreparation);
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
                cmd.CommandText = "OrdrePreparationTaxe_Supprimer";

                cmd.Parameters.AddWithValue("@NOrdrePreparation", this.NOrdrePreparation);
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

        public static OrdrePreparationTaxe Charger(string nOrdrePreparation, string cTaxe)
        {
            OrdrePreparationTaxe ordrePreparation = null;

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
                    cmd.Parameters.AddWithValue("@CTaxe", cTaxe);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            ordrePreparation = new OrdrePreparationTaxe();
                            ordrePreparation.NOrdrePreparation = dr["NOrdrePreparation"].ToString();
                            if (dr["CTaxe"] != DBNull.Value)
                                ordrePreparation.CTaxe = dr["CTaxe"].ToString();
                            if (dr["Assiette"] != DBNull.Value)
                                ordrePreparation.Assiette = decimal.Parse(dr["Assiette"].ToString());
                            if (dr["BExonoreFodec"] != DBNull.Value)
                                ordrePreparation.BExonoreFodec = bool.Parse(dr["BExonoreFodec"].ToString());
                            if (dr["BExonoreTVA"] != DBNull.Value)
                                ordrePreparation.BExonoreTVA = bool.Parse(dr["BExonoreTVA"].ToString());
                            if (dr["BExport"] != DBNull.Value)
                                ordrePreparation.BExport = bool.Parse(dr["BExport"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                ordrePreparation.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["TauxTVA"] != DBNull.Value)
                                ordrePreparation.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return ordrePreparation;
            }
        }
    }

    public class OrdrePreparationTaxeCollection : List<OrdrePreparationTaxe>
    {
        public static OrdrePreparationTaxeCollection Charger(string nOrdrePreparation)
        {
            OrdrePreparationTaxeCollection collection = new OrdrePreparationTaxeCollection();

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
                    cmd.CommandText = "OrdrePreparationTaxe_Charger";
                    cmd.Parameters.AddWithValue("@NOrdrePreparation", nOrdrePreparation);
                    cmd.Parameters.AddWithValue("@CTaxe", DBNull.Value);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            OrdrePreparationTaxe ordrePreparationTaxe = new OrdrePreparationTaxe();
                            ordrePreparationTaxe.NOrdrePreparation = dr["NOrdrePreparation"].ToString();
                            if (dr["CTaxe"] != DBNull.Value)
                                ordrePreparationTaxe.CTaxe = dr["CTaxe"].ToString();
                            if (dr["Assiette"] != DBNull.Value)
                                ordrePreparationTaxe.Assiette = decimal.Parse(dr["Assiette"].ToString());
                            if (dr["BExonoreFodec"] != DBNull.Value)
                                ordrePreparationTaxe.BExonoreFodec = bool.Parse(dr["BExonoreFodec"].ToString());
                            if (dr["BExonoreTVA"] != DBNull.Value)
                                ordrePreparationTaxe.BExonoreTVA = bool.Parse(dr["BExonoreTVA"].ToString());
                            if (dr["BExport"] != DBNull.Value)
                                ordrePreparationTaxe.BExport = bool.Parse(dr["BExport"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                ordrePreparationTaxe.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["TauxTVA"] != DBNull.Value)
                                ordrePreparationTaxe.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());

                            collection.Add(ordrePreparationTaxe);
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

        public  OrdrePreparationTaxe RecupererOrdrePreparationTaxe(string cTaxe)
        {
            OrdrePreparationTaxe ordrePreparationTaxe = null;
            ordrePreparationTaxe = this.Where(p => p.CTaxe.Equals(cTaxe)).FirstOrDefault();
            return ordrePreparationTaxe;
        }
    }
}