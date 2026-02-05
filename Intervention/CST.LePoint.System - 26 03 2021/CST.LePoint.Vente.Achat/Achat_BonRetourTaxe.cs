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
    public class Achat_BonRetourTaxe
    {
        #region Propriètés

        [XmlAttribute("NBonRetour")]
        [Bindable(true)]
        public string NBonRetour { get; set; }
        [XmlAttribute("CEntrepot")]
        [Bindable(true)]
        public string CEntrepot { get; set; }
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

        public void Sauvegarder(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Achat_BonRetourTaxe_Inserer";

                cmd.Parameters.AddWithValue("@NBonRetour", this.NBonRetour);
                cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
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
                cmd.CommandText = "Achat_BonRetourTaxe_Supprimer";
                cmd.Parameters.AddWithValue("@NBonRetour", this.NBonRetour);
                cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
                cmd.Parameters.AddWithValue("@CTaxe", this.CTaxe);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Achat_BonRetourTaxe Charger(string nBonRetour, string cTaxe,string cEntrepot)
        {
            Achat_BonRetourTaxe bonRetourTaxe = null;

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
                    cmd.CommandText = "Achat_BonRetourTaxe_Charger";
                    cmd.Parameters.AddWithValue("@NBonRetour", nBonRetour);
                    cmd.Parameters.AddWithValue("@CTaxe", cTaxe);
                    cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            bonRetourTaxe = new Achat_BonRetourTaxe();
                            bonRetourTaxe.NBonRetour = dr["NBonRetour"].ToString();
                            bonRetourTaxe.CEntrepot = dr["CEntrepot"].ToString();
                            bonRetourTaxe.CTaxe = dr["CTaxe"].ToString();
                            if (dr["Assiette"] != DBNull.Value)
                                bonRetourTaxe.Assiette = decimal.Parse(dr["Assiette"].ToString());
                            if (dr["BExonoreFodec"] != DBNull.Value)
                                bonRetourTaxe.BExonoreFodec = bool.Parse(dr["BExonoreFodec"].ToString());
                            if (dr["BExonoreTVA"] != DBNull.Value)
                                bonRetourTaxe.BExonoreTVA = bool.Parse(dr["BExonoreTVA"].ToString());
                            if (dr["BImport"] != DBNull.Value)
                                bonRetourTaxe.BImport = bool.Parse(dr["BImport"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                bonRetourTaxe.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["TauxTVA"] != DBNull.Value)
                                bonRetourTaxe.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return bonRetourTaxe;
            }
        }
    }

    public class Achat_BonRetourTaxeCollection : List<Achat_BonRetourTaxe>
    {
        public static Achat_BonRetourTaxeCollection Charger(string nBonRetour, string cEntrepot)
        {
            Achat_BonRetourTaxeCollection bonRetourTaxeCollection = new Achat_BonRetourTaxeCollection();

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
                    cmd.CommandText = "Achat_BonRetourTaxe_Charger";
                    cmd.Parameters.AddWithValue("@NBonRetour", nBonRetour);
                    cmd.Parameters.AddWithValue("@CTaxe", DBNull.Value);
                    cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Achat_BonRetourTaxe bonRetourTaxe = new Achat_BonRetourTaxe();
                            bonRetourTaxe = new Achat_BonRetourTaxe();
                            bonRetourTaxe.NBonRetour = dr["NBonRetour"].ToString();
                            bonRetourTaxe.CEntrepot = dr["CEntrepot"].ToString();
                            bonRetourTaxe.CTaxe = dr["CTaxe"].ToString();
                            if (dr["Assiette"] != DBNull.Value)
                                bonRetourTaxe.Assiette = decimal.Parse(dr["Assiette"].ToString());
                            if (dr["BExonoreFodec"] != DBNull.Value)
                                bonRetourTaxe.BExonoreFodec = bool.Parse(dr["BExonoreFodec"].ToString());
                            if (dr["BExonoreTVA"] != DBNull.Value)
                                bonRetourTaxe.BExonoreTVA = bool.Parse(dr["BExonoreTVA"].ToString());
                            if (dr["BImport"] != DBNull.Value)
                                bonRetourTaxe.BImport = bool.Parse(dr["BImport"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                bonRetourTaxe.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["TauxTVA"] != DBNull.Value)
                                bonRetourTaxe.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());
                            bonRetourTaxeCollection.Add(bonRetourTaxe);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return bonRetourTaxeCollection;
            }
        }
    }
}
