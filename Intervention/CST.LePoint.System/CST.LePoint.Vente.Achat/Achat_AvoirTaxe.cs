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
    public class Achat_AvoirTaxe
    {
        #region Proriétès
        [XmlAttribute("CTaxe")]
        [Bindable(true)]
        public string CTaxe { get; set; }
        [XmlAttribute("NAvoir")]
        [Bindable(true)]
        public string NAvoir { get; set; }
        [XmlAttribute("Assiette")]
        [Bindable(true)]
        public decimal Assiette { get; set; }
        [XmlAttribute("BExonoreFodec")]
        [Bindable(true)]
        public bool BExonoreFodec { get; set; }
        [XmlAttribute("BExonoreTVA")]
        [Bindable(true)]
        public bool BExonoreTVA { get; set; }
        [XmlAttribute("MontantTaxe")]
        [Bindable(true)]
        public decimal MontantTaxe { get; set; }
        [XmlAttribute("TauxTVA")]
        [Bindable(true)]
        public decimal TauxTVA { get; set; }
        [XmlAttribute("BImport")]
        [Bindable(true)]
        public bool BImport { get; set; }
        #endregion

        public void Sauvegarder(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Achat_AvoirTaxe_Inserer";

                cmd.Parameters.AddWithValue("@NAvoir", this.NAvoir);
                cmd.Parameters.AddWithValue("@CTaxe", this.CTaxe);
                cmd.Parameters.AddWithValue("@TauxTVA", this.TauxTVA);
                cmd.Parameters.AddWithValue("@Assiette", this.Assiette);
                cmd.Parameters.AddWithValue("@BExonoreFodec", this.BExonoreFodec);
                cmd.Parameters.AddWithValue("@BExonoreTVA", this.BExonoreTVA);
                cmd.Parameters.AddWithValue("@BImport", this.BImport);
                cmd.Parameters.AddWithValue("@MontantTaxe", this.MontantTaxe);

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
                cmd.CommandText = "Achat_AvoirTaxe_Supprimer";

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

        public static Achat_AvoirTaxe Charger(string nAvoir, string cTaxe)
        {
            Achat_AvoirTaxe avoirTaxe = null;

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
                    cmd.CommandText = "Achat_AvoirTaxe_Charger";
                    cmd.Parameters.AddWithValue("@NAvoir", nAvoir);
                    cmd.Parameters.AddWithValue("@CTaxe", cTaxe);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            avoirTaxe = new Achat_AvoirTaxe();
                            avoirTaxe.NAvoir = dr["NAvoir"].ToString();
                            avoirTaxe.CTaxe = dr["CTaxe"].ToString();
                            if(dr["Assiette"]!=DBNull.Value)
                                avoirTaxe.Assiette = decimal.Parse(dr["Assiette"].ToString());
                            if(dr["BExonoreFodec"]!= DBNull.Value)
                                avoirTaxe.BExonoreFodec = bool.Parse(dr["BExonoreFodec"].ToString());
                            if(dr["BExonoreTVA"]!=DBNull.Value)
                                avoirTaxe.BExonoreTVA = bool.Parse(dr["BExonoreTVA"].ToString());
                            if(dr["MontantTaxe"]!= DBNull.Value)
                                avoirTaxe.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if(dr["TauxTVA"]!= DBNull.Value)
                                avoirTaxe.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());
                            if(dr["BImport"]!= DBNull.Value)
                                avoirTaxe.BImport = bool.Parse(dr["BImport"].ToString()); 
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
    public class Achat_AvoirTaxeCollection : List<Achat_AvoirTaxe>
    {
        public static Achat_AvoirTaxeCollection Charger(string nAvoir, string cTaxe)
        {
            Achat_AvoirTaxeCollection collection = new Achat_AvoirTaxeCollection();

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
                    cmd.CommandText = "Achat_AvoirTaxe_Charger";
                    cmd.Parameters.AddWithValue("@NAvoir", nAvoir);
                    cmd.Parameters.AddWithValue("@CTaxe", cTaxe);
                    foreach (SqlParameter parameter in cmd.Parameters)
                        if (parameter.Value == null)
                            parameter.Value = DBNull.Value;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Achat_AvoirTaxe avoirTaxe = new Achat_AvoirTaxe();
                            avoirTaxe.NAvoir = dr["NAvoir"].ToString();
                            avoirTaxe.CTaxe = dr["CTaxe"].ToString();
                            if (dr["Assiette"] != DBNull.Value)
                                avoirTaxe.Assiette = decimal.Parse(dr["Assiette"].ToString());
                            if (dr["BExonoreFodec"] != DBNull.Value)
                                avoirTaxe.BExonoreFodec = bool.Parse(dr["BExonoreFodec"].ToString());
                            if (dr["BExonoreTVA"] != DBNull.Value)
                                avoirTaxe.BExonoreTVA = bool.Parse(dr["BExonoreTVA"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                avoirTaxe.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["TauxTVA"] != DBNull.Value)
                                avoirTaxe.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());
                            if (dr["BImport"] != DBNull.Value)
                                avoirTaxe.BImport = bool.Parse(dr["BImport"].ToString());
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
    }
}
