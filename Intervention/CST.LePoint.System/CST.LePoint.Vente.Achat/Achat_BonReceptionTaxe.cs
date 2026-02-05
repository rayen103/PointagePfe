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
    public class Achat_BonReceptionTaxe
    {
        #region Proriétès
        [XmlAttribute("CTaxe")]
        [Bindable(true)]
        public string CTaxe { get; set; }
        [XmlAttribute("NBonReception")]
        [Bindable(true)]
        public string NBonReception { get; set; }
        [XmlAttribute("CEntrepot")]
        [Bindable(true)]
        public string CEntrepot { get; set; }
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
                cmd.CommandText = "Achat_BonReceptionTaxe_Inserer";

                cmd.Parameters.AddWithValue("@NBonReception", this.NBonReception);
                cmd.Parameters.AddWithValue("@CTaxe", this.CTaxe);
                cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
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
                cmd.CommandText = "Achat_BonReceptionTaxe_Supprimer";

                cmd.Parameters.AddWithValue("@NBonReception", this.NBonReception);
                cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
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

        public static Achat_BonReceptionTaxe Charger(string nBonReception, string cEntrepot, string cTaxe)
        {
            Achat_BonReceptionTaxe bonReceptionTaxe = null;

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
                    cmd.CommandText = "Achat_BonReceptionTaxe_Charger";
                    cmd.Parameters.AddWithValue("@NBonReception", nBonReception);
                    cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                    cmd.Parameters.AddWithValue("@CTaxe", cTaxe);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            bonReceptionTaxe = new Achat_BonReceptionTaxe();
                            bonReceptionTaxe.NBonReception = dr["NBonReception"].ToString();
                            bonReceptionTaxe.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["CTaxe"] != DBNull.Value)
                                bonReceptionTaxe.CTaxe = dr["CTaxe"].ToString();
                            if (dr["Assiette"] != DBNull.Value)
                                bonReceptionTaxe.Assiette = decimal.Parse(dr["Assiette"].ToString());
                            if (dr["BExonoreFodec"] != DBNull.Value)
                                bonReceptionTaxe.BExonoreFodec = bool.Parse(dr["BExonoreFodec"].ToString());
                            if (dr["BExonoreTVA"] != DBNull.Value)
                                bonReceptionTaxe.BExonoreTVA = bool.Parse(dr["BExonoreTVA"].ToString());
                            if (dr["BImport"] != DBNull.Value)
                                bonReceptionTaxe.BImport = bool.Parse(dr["BImport"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                bonReceptionTaxe.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["TauxTVA"] != DBNull.Value)
                                bonReceptionTaxe.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return bonReceptionTaxe;
            }
        }
    }

    public class Achat_BonReceptionTaxeCollection : List<Achat_BonReceptionTaxe>
    {
        public static Achat_BonReceptionTaxeCollection Charger(string nBonReception, string cEntrepot)
        {
            Achat_BonReceptionTaxeCollection collection = new Achat_BonReceptionTaxeCollection();

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
                    cmd.CommandText = "Achat_BonReceptionTaxe_Charger";
                    cmd.Parameters.AddWithValue("@NBonReception", nBonReception);
                    cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                    cmd.Parameters.AddWithValue("@CTaxe", DBNull.Value);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Achat_BonReceptionTaxe bonReceptionTaxe = new Achat_BonReceptionTaxe();
                            bonReceptionTaxe.NBonReception = dr["NBonReception"].ToString();
                            bonReceptionTaxe.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["CTaxe"] != DBNull.Value)
                                bonReceptionTaxe.CTaxe = dr["CTaxe"].ToString();
                            if (dr["Assiette"] != DBNull.Value)
                                bonReceptionTaxe.Assiette = decimal.Parse(dr["Assiette"].ToString());
                            if (dr["BExonoreFodec"] != DBNull.Value)
                                bonReceptionTaxe.BExonoreFodec = bool.Parse(dr["BExonoreFodec"].ToString());
                            if (dr["BExonoreTVA"] != DBNull.Value)
                                bonReceptionTaxe.BExonoreTVA = bool.Parse(dr["BExonoreTVA"].ToString());
                            if (dr["BImport"] != DBNull.Value)
                                bonReceptionTaxe.BImport = bool.Parse(dr["BImport"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                bonReceptionTaxe.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["TauxTVA"] != DBNull.Value)
                                bonReceptionTaxe.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());

                            collection.Add(bonReceptionTaxe);
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
