using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CST.LePoint.Achat.Metier
{
    public class Achat_FactureTaxe
    {
        #region Proriétès
        [XmlAttribute("CTaxe")]
        [Bindable(true)]
        public string CTaxe { get; set; }
        [XmlAttribute("NFacture")]
        [Bindable(true)]
        public string NFacture { get; set; }
        [XmlAttribute("Assiette")]
        [Bindable(true)]
        public decimal Assiette { get; set; }
        [XmlAttribute("BExonoreFodec")]
        [Bindable(true)]
        public bool BExonoreFodec { get; set; }
        [XmlAttribute("BExonoreTVA")]
        [Bindable(true)]
        public bool BExonoreTVA { get; set; }
        [XmlAttribute("BImport")]
        [Bindable(true)]
        public bool BImport { get; set; }
        [XmlAttribute("MontantTaxe")]
        [Bindable(true)]
        public decimal MontantTaxe { get; set; }
        [XmlAttribute("TauxTVA")]
        [Bindable(true)]
        public decimal TauxTVA { get; set; }
        #endregion

        public void Sauvegarder(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Achat_FactureTaxe_Inserer";

                cmd.Parameters.AddWithValue("@NFacture", this.NFacture);
                cmd.Parameters.AddWithValue("@CTaxe", this.CTaxe);
                cmd.Parameters.AddWithValue("@TauxTVA", this.TauxTVA);
                cmd.Parameters.AddWithValue("@Assiette", this.Assiette);
                cmd.Parameters.AddWithValue("@BExonoreFodec", this.BExonoreFodec);
                cmd.Parameters.AddWithValue("@BExonoreTVA", this.BExonoreTVA);
                cmd.Parameters.AddWithValue("@BImport", this.BImport);
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
                cmd.CommandText = "Achat_FactureTaxe_Supprimer";

                cmd.Parameters.AddWithValue("@NFacture", this.NFacture);
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

        public static Achat_FactureTaxe Charger(string nFacture, string cTaxe)
        {
            Achat_FactureTaxe factureTaxe = null;

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
                    cmd.CommandText = "Achat_FactureTaxe_Charger";
                    cmd.Parameters.AddWithValue("@NFacture", nFacture);
                    cmd.Parameters.AddWithValue("@CTaxe", cTaxe);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            factureTaxe = new Achat_FactureTaxe();
                            factureTaxe.NFacture = dr["NFacture"].ToString();
                            if (dr["CTaxe"] != DBNull.Value)
                                factureTaxe.CTaxe = dr["CTaxe"].ToString();
                            if (dr["Assiette"] != DBNull.Value)
                                factureTaxe.Assiette = decimal.Parse(dr["Assiette"].ToString());
                            if (dr["BExonoreFodec"] != DBNull.Value)
                                factureTaxe.BExonoreFodec = bool.Parse(dr["BExonoreFodec"].ToString());
                            if (dr["BExonoreTVA"] != DBNull.Value)
                                factureTaxe.BExonoreTVA = bool.Parse(dr["BExonoreTVA"].ToString());
                            if (dr["BImport"] != DBNull.Value)
                                factureTaxe.BImport = bool.Parse(dr["BImport"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                factureTaxe.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["TauxTVA"] != DBNull.Value)
                                factureTaxe.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return factureTaxe;
            }
        }

    }

    public class Achat_FactureTaxeCollection : List<Achat_FactureTaxe>
    {
        public static Achat_FactureTaxeCollection Charger(string nFacture)
        {
            Achat_FactureTaxeCollection factureTaxeCollection = new Achat_FactureTaxeCollection();

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
                    cmd.CommandText = "Achat_FactureTaxe_Charger";
                    cmd.Parameters.AddWithValue("@NFacture", nFacture);
                    cmd.Parameters.AddWithValue("@CTaxe", DBNull.Value);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Achat_FactureTaxe factureTaxe = new Achat_FactureTaxe();
                            factureTaxe.NFacture = dr["NFacture"].ToString();
                            if (dr["CTaxe"] != DBNull.Value)
                                factureTaxe.CTaxe = dr["CTaxe"].ToString();
                            if (dr["Assiette"] != DBNull.Value)
                                factureTaxe.Assiette = decimal.Parse(dr["Assiette"].ToString());
                            if (dr["BExonoreFodec"] != DBNull.Value)
                                factureTaxe.BExonoreFodec = bool.Parse(dr["BExonoreFodec"].ToString());
                            if (dr["BExonoreTVA"] != DBNull.Value)
                                factureTaxe.BExonoreTVA = bool.Parse(dr["BExonoreTVA"].ToString());
                            if (dr["BImport"] != DBNull.Value)
                                factureTaxe.BImport = bool.Parse(dr["BImport"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                factureTaxe.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["TauxTVA"] != DBNull.Value)
                                factureTaxe.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());
                            factureTaxeCollection.Add(factureTaxe);
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return factureTaxeCollection;
            }
        }
    }
}
