using CST.LePoint.Referentiel;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace CST.LePoint.Stock.Referentiel.Commun
{
    [Serializable]
    public class TaxeCollection : ItemCollection
    {
        public static DataSet ChargerVue()
        {
            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Taxe_Rpt_Charger";
                cmd.Parameters.AddWithValue("@CTaxe", DBNull.Value);
                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "Taxe_Rpt_Charger");
            }
            return (ds);
        }

        public static TaxeCollection Charger()
        {
            TaxeCollection collection = new TaxeCollection();
            Taxe taxe = null;
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Ref_Taxe_Charger";
                cmd.Parameters.AddWithValue("@CTaxe", DBNull.Value);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    taxe = new Taxe();
                    taxe.Code = dr["CTaxe"].ToString();
                    if (dr["LibTaxe"] != DBNull.Value)
                        taxe.Libelle = dr["LibTaxe"].ToString();
                    if (dr["BaseTaxe"] != DBNull.Value)
                        taxe.BaseTaxe = dr["BaseTaxe"].ToString();
                    if (dr["MontantTaxe"] != DBNull.Value)
                        taxe.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                    if (dr["Taux1"] != DBNull.Value)
                        taxe.Taux1 = decimal.Parse(dr["Taux1"].ToString());
                    if (dr["Taux2"] != DBNull.Value)
                        taxe.Taux2 = decimal.Parse(dr["Taux2"].ToString());
                    if (dr["BArticle"] != DBNull.Value)
                        taxe.BArticle = bool.Parse(dr["BArticle"].ToString());
                    collection.Add(taxe);
                }
            }
            return (collection);
        }

        public static TaxeCollection ChargerSansFodec()
        {
            TaxeCollection collection = new TaxeCollection();
            Taxe taxe = null;
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Ref_Taxe_Charger_SansFodec";
                cmd.Parameters.AddWithValue("@CTaxe", DBNull.Value);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    taxe = new Taxe();
                    taxe.Code = dr["CTaxe"].ToString();
                    if (dr["LibTaxe"] != DBNull.Value)
                        taxe.Libelle = dr["LibTaxe"].ToString();
                    if (dr["BaseTaxe"] != DBNull.Value)
                        taxe.BaseTaxe = dr["BaseTaxe"].ToString();
                    if (dr["MontantTaxe"] != DBNull.Value)
                        taxe.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                    if (dr["Taux1"] != DBNull.Value)
                        taxe.Taux1 = decimal.Parse(dr["Taux1"].ToString());
                    if (dr["Taux2"] != DBNull.Value)
                        taxe.Taux2 = decimal.Parse(dr["Taux2"].ToString());
                    if (dr["BArticle"] != DBNull.Value)
                        taxe.BArticle = bool.Parse(dr["BArticle"].ToString());
                    collection.Add(taxe);
                }
            }
            return (collection);
        }
    }

    [Serializable]
    public class Taxe : Item
    {
        #region Propriétés

        [XmlAttribute("BaseTaxe")]
        [Bindable(true)]
        public string BaseTaxe { get; set; }

        [XmlAttribute("MontantTaxe")]
        [Bindable(true)]
        public decimal MontantTaxe { get; set; }

        [XmlAttribute("Taux1")]
        [Bindable(true)]
        public decimal Taux1 { get; set; }

        [XmlAttribute("Taux2")]
        [Bindable(true)]
        public decimal Taux2 { get; set; }

        [XmlAttribute("BArticle")]
        [Bindable(true)]
        public bool BArticle { get; set; }

        [XmlAttribute("DateInsertion")]
        [Bindable(true)]
        public DateTime DateInsertion { get; set; }

        [XmlAttribute("DateModification")]
        [Bindable(true)]
        public DateTime DateModification { get; set; }

        [XmlAttribute("CreePar")]
        [Bindable(true)]
        public int CreePar { get; set; }

        [XmlAttribute("ModifiePar")]
        [Bindable(true)]
        public int ModifiePar { get; set; }

        [XmlAttribute("PCInsertion")]
        [Bindable(true)]
        public string PCInsertion { get; set; }

        [XmlAttribute("PCModification")]
        [Bindable(true)]
        public string PCModification { get; set; }

        #endregion Propriétés

        public Taxe()
        {
        }

        public Taxe(string ctaxe)
        {
            Code = ctaxe;
        }

        public void Sauvegarder()
        {
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

                    cmd.CommandText = "Ref_Taxe_Sauvegarder";
                    cmd.Parameters.AddWithValue("@CTaxe", Code);
                    cmd.Parameters.AddWithValue("@LibTaxe", Libelle);
                    cmd.Parameters.AddWithValue("@BaseTaxe", BaseTaxe);
                    cmd.Parameters.AddWithValue("@MontantTaxe", MontantTaxe);
                    cmd.Parameters.AddWithValue("@Taux1", Taux1);
                    cmd.Parameters.AddWithValue("@Taux2", Taux2);
                    cmd.Parameters.AddWithValue("@BArticle", BArticle);
                    cmd.Parameters.AddWithValue("@CreePar", CreePar);
                    cmd.Parameters.AddWithValue("@ModifiePar", ModifiePar);
                    cmd.Parameters.AddWithValue("@PCInsertion", PCInsertion);
                    cmd.Parameters.AddWithValue("@PCModification", PCModification);
                    cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                    cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void Supprimer()
        {
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

                    cmd.CommandText = "Ref_Taxe_Supprimer";
                    cmd.Parameters.AddWithValue("@CTaxe", Code);
                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public static Taxe Charger(string ctaxe)
        {
            Taxe taxe = null;
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Ref_Taxe_Charger";
                cmd.Parameters.AddWithValue("@CTaxe", ctaxe);
                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    taxe = new Taxe();
                    taxe.Code = dr["CTaxe"].ToString();
                    if (dr["LibTaxe"] != DBNull.Value)
                        taxe.Libelle = dr["LibTaxe"].ToString();
                    if (dr["BaseTaxe"] != DBNull.Value)
                        taxe.BaseTaxe = dr["BaseTaxe"].ToString();
                    if (dr["MontantTaxe"] != DBNull.Value)
                        taxe.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                    if (dr["Taux1"] != DBNull.Value)
                        taxe.Taux1 = decimal.Parse(dr["Taux1"].ToString());
                    if (dr["Taux2"] != DBNull.Value)
                        taxe.Taux2 = decimal.Parse(dr["Taux2"].ToString());
                    if (dr["BArticle"] != DBNull.Value)
                        taxe.BArticle = bool.Parse(dr["BArticle"].ToString());
                }
            }
            return (taxe);
        }
    }
}