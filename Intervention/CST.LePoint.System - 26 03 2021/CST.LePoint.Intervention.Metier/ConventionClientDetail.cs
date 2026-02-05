using CST.LePoint.Vente.Metier;
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


namespace CST.LePoint.Intervention.Metier
{
    public class ConventionClientDetail
    {
        #region Propriétès

        [XmlAttribute("NConvention")]
        [Bindable(true)]
        public string NConvention { get; set; }

        [XmlAttribute("CArticle")]
        [Bindable(true)]
        public string CArticle { get; set; }

        [XmlAttribute("LibArticle")]
        [Bindable(true)]
        public string LibArticle { get; set; }

        [XmlAttribute("Ordre")]
        [Bindable(true)]
        public int Ordre { get; set; }
        
        [XmlAttribute("CEntrepot")]
        [Bindable(true)]
        public string CEntrepot { get; set; }

        [XmlAttribute("Quantite")]
        [Bindable(true)]
        public decimal Quantite { get; set; }

        [XmlAttribute("PrixHT")]
        [Bindable(true)]
        public decimal PrixHT { get; set; }

        [XmlAttribute("PourcentageMaj")]
        [Bindable(true)]
        public decimal PourcentageMaj { get; set; }

        [XmlAttribute("MontantNet")]
        [Bindable(true)]
        public decimal MontantNet { get; set; }
        
        [XmlAttribute("PourcentageRemise")]
        [Bindable(true)]
        public decimal PourcentageRemise { get; set; }

        #endregion Propriétès
        public ConventionClientDetail()
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
                cmd.CommandText = "ConventionClientDetail_Inserer";

                cmd.Parameters.AddWithValue("@NConvention", this.NConvention);
                cmd.Parameters.AddWithValue("@CArticle", this.CArticle);
                cmd.Parameters.AddWithValue("@Ordre", this.Ordre);
                cmd.Parameters.AddWithValue("@LibArticle", this.LibArticle);
                cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
                cmd.Parameters.AddWithValue("@Quantite", this.Quantite);
                cmd.Parameters.AddWithValue("@PrixHT", this.PrixHT);
                cmd.Parameters.AddWithValue("@PourcentageRemise", this.PourcentageRemise);
                cmd.Parameters.AddWithValue("@Pourcentagemaj", this.PourcentageMaj);
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
        public ConventionClientDetail Charger(string nConvention, string cArticle, int ordre)
        {
            ConventionClientDetail conv = null;
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
                    cmd.CommandText = "ConventionClientDetail_Charger";
                    cmd.Parameters.AddWithValue("@NConvention", NConvention);
                    cmd.Parameters.AddWithValue("@CArticle", cArticle);
                    cmd.Parameters.AddWithValue("@Ordre", ordre);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            conv = new ConventionClientDetail();
                            conv.NConvention = dr["NConvention"].ToString();
                            conv.CArticle = dr["CArticle"].ToString();
                            conv.Ordre = int.Parse(dr["Ordre"].ToString());
                            if (dr["CEntrepot"] != DBNull.Value)
                                conv.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["LibArticle"] != DBNull.Value)
                                conv.LibArticle = dr["LibArticle"].ToString();
                            if (dr["PourcentageRemise"] != DBNull.Value)
                                conv.PourcentageRemise = decimal.Parse(dr["PourcentageRemise"].ToString());
                            if (dr["PrixHT"] != DBNull.Value)
                                conv.PrixHT = decimal.Parse(dr["PrixHT"].ToString());
                            if (dr["Quantite"] != DBNull.Value)
                                conv.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["PourcentageMaj"] != DBNull.Value)
                                conv.PourcentageMaj = decimal.Parse(dr["PourcentageMaj"].ToString());

                           
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return (conv);
            }
        }
    }
    public class ConventionClientDetailCollection : List<ConventionClientDetail>
    {
        public ConventionClientDetailCollection()
        {
        }
        public static DataSet ChargerVue(string nConvention)
        {
            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ConventionClientDetailRpt_Charger";
                cmd.Parameters.AddWithValue("@NConvention", nConvention);

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
        public static ConventionClientDetailCollection Charger(string nConvention)
        {
            ConventionClientDetailCollection collection = new ConventionClientDetailCollection();

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
                    cmd.CommandText = "ConventionClientDetail_Charger";
                    cmd.Parameters.AddWithValue("@NConvention", nConvention);
                    cmd.Parameters.AddWithValue("@CArticle", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Ordre", DBNull.Value);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ConventionClientDetail conv = new ConventionClientDetail();
                            conv.NConvention = dr["NConvention"].ToString();
                            conv.CArticle = dr["CArticle"].ToString();
                            conv.Ordre = int.Parse(dr["Ordre"].ToString());
                            if (dr["CEntrepot"] != DBNull.Value)
                                conv.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["LibArticle"] != DBNull.Value)
                                conv.LibArticle = dr["LibArticle"].ToString();
                            if (dr["PourcentageRemise"] != DBNull.Value)
                                conv.PourcentageRemise = decimal.Parse(dr["PourcentageRemise"].ToString());
                            if (dr["Pourcentagemaj"] != DBNull.Value)
                                conv.PourcentageMaj = decimal.Parse(dr["Pourcentagemaj"].ToString());
                            if (dr["PrixHT"] != DBNull.Value)
                                conv.PrixHT = decimal.Parse(dr["PrixHT"].ToString());
                            if (dr["Quantite"] != DBNull.Value)
                                conv.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["MtNet"] != DBNull.Value)
                                conv.MontantNet = decimal.Parse(dr["MtNet"].ToString());
                            collection.Add(conv);
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
