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
    public class Achat_BonCommandeTaxe
    {
        #region Propriètés

        [XmlAttribute("NBonCommande")]
        [Bindable(true)]
        public string NBonCommande { get; set; }

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

        [XmlAttribute("BImport")]
        [Bindable(true)]
        public bool BImport { get; set; }

        [XmlAttribute("MontantTaxe")]
        [Bindable(true)]
        public decimal MontantTaxe { get; set; }

        [XmlAttribute("TauxTVA")]
        [Bindable(true)]
        public decimal TauxTVA { get; set; }

        #endregion Propriètés

        public Achat_BonCommandeTaxe() { }

        public void Sauvegarder(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Achat_BonCommandeTaxe_Inserer";

                cmd.Parameters.AddWithValue("@NBonCommande", this.NBonCommande);
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
                cmd.CommandText = "Achat_BonCommandeTaxe_Supprimer";

                cmd.Parameters.AddWithValue("@NBonCommande", this.NBonCommande);
                cmd.Parameters.AddWithValue("@CTaxe", this.CTaxe);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Achat_BonCommandeTaxe Charger(string nBonCommande, string cTaxe)
        {
            Achat_BonCommandeTaxe bonCommandeTaxe = null;

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
                    cmd.CommandText = "Achat_BonCommandeTaxe_Charger";
                    cmd.Parameters.AddWithValue("@NBonCommande", nBonCommande);
                    cmd.Parameters.AddWithValue("@CTaxe", cTaxe);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            bonCommandeTaxe = new Achat_BonCommandeTaxe();
                            bonCommandeTaxe.NBonCommande = dr["NBonCommande"].ToString();
                            if (dr["CTaxe"] != DBNull.Value)
                                bonCommandeTaxe.CTaxe = dr["CTaxe"].ToString();
                            if (dr["Assiette"] != DBNull.Value)
                                bonCommandeTaxe.Assiette = decimal.Parse(dr["Assiette"].ToString());
                            if (dr["BExonoreFodec"] != DBNull.Value)
                                bonCommandeTaxe.BExonoreFodec = bool.Parse(dr["BExonoreFodec"].ToString());
                            if (dr["BExonoreTVA"] != DBNull.Value)
                                bonCommandeTaxe.BExonoreTVA = bool.Parse(dr["BExonoreTVA"].ToString());
                            if (dr["BImport"] != DBNull.Value)
                                bonCommandeTaxe.BImport = bool.Parse(dr["BImport"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                bonCommandeTaxe.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["TauxTVA"] != DBNull.Value)
                                bonCommandeTaxe.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return bonCommandeTaxe;
            }
        }
    }

    public class Achat_BonCommandeTaxeCollection : List<Achat_BonCommandeTaxe>
    {
        public static Achat_BonCommandeTaxeCollection Charger(string nBonCommande)
        {
            Achat_BonCommandeTaxeCollection collection = new Achat_BonCommandeTaxeCollection();

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
                    cmd.CommandText = "Achat_BonCommandeTaxe_Charger";
                    cmd.Parameters.AddWithValue("@NBonCommande", nBonCommande);
                    cmd.Parameters.AddWithValue("@CTaxe", DBNull.Value);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Achat_BonCommandeTaxe bonCommandeTaxe = new Achat_BonCommandeTaxe();
                            bonCommandeTaxe.NBonCommande = dr["NBonCommande"].ToString();
                            if (dr["CTaxe"] != DBNull.Value)
                                bonCommandeTaxe.CTaxe = dr["CTaxe"].ToString();
                            if (dr["Assiette"] != DBNull.Value)
                                bonCommandeTaxe.Assiette = decimal.Parse(dr["Assiette"].ToString());
                            if (dr["BExonoreFodec"] != DBNull.Value)
                                bonCommandeTaxe.BExonoreFodec = bool.Parse(dr["BExonoreFodec"].ToString());
                            if (dr["BExonoreTVA"] != DBNull.Value)
                                bonCommandeTaxe.BExonoreTVA = bool.Parse(dr["BExonoreTVA"].ToString());
                            if (dr["BImport"] != DBNull.Value)
                                bonCommandeTaxe.BImport = bool.Parse(dr["BImport"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                bonCommandeTaxe.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["TauxTVA"] != DBNull.Value)
                                bonCommandeTaxe.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());

                            collection.Add(bonCommandeTaxe);
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
