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
    public class ConventionClientSimulation
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

        [XmlAttribute("CUnite")]
        [Bindable(true)]
        public string CUnite { get; set; }

        [XmlAttribute("Ordre")]
        [Bindable(true)]
        public int Ordre { get; set; }
        
        [XmlAttribute("CEntrepot")]
        [Bindable(true)]
        public string CEntrepot { get; set; }

        [XmlAttribute("Quantite")]
        [Bindable(true)]
        public decimal Quantite { get; set; }
      
        [XmlAttribute("Image_Article")]
        [Bindable(true)]
        public byte[] Image_Article { get; set; }

        [XmlAttribute("BSupplementaire")]
        [Bindable(true)]
        public bool BSupplementaire { get; set; }        

        #endregion Propriétès
        
        public ConventionClientSimulation()
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
                cmd.CommandText = "ConventionClientSimulation_Inserer";

                cmd.Parameters.AddWithValue("@NConvention", this.NConvention);
                cmd.Parameters.AddWithValue("@CArticle", this.CArticle);
                cmd.Parameters.AddWithValue("@Ordre", this.Ordre);
                cmd.Parameters.AddWithValue("@LibArticle", this.LibArticle);
                cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
                cmd.Parameters.AddWithValue("@Quantite", this.Quantite);
                cmd.Parameters.AddWithValue("@CUnite", this.CUnite);
                cmd.Parameters.AddWithValue("@Image_Article", this.Image_Article);
                cmd.Parameters.AddWithValue("@BSupplementaire", this.BSupplementaire);
                
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
        
        public ConventionClientSimulation Charger(string nConvention, string cArticle, int ordre)
        {
            ConventionClientSimulation conv = null;
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
                    cmd.CommandText = "ConventionClientSimulation_Charger";
                    cmd.Parameters.AddWithValue("@NConvention", NConvention);
                    cmd.Parameters.AddWithValue("@CArticle", cArticle);
                    cmd.Parameters.AddWithValue("@Ordre", ordre);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            conv = new ConventionClientSimulation();
                            conv.NConvention = dr["NConvention"].ToString();
                            conv.CArticle = dr["CArticle"].ToString();
                            conv.Ordre = int.Parse(dr["Ordre"].ToString());
                            if (dr["CEntrepot"] != DBNull.Value)
                                conv.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["LibArticle"] != DBNull.Value)
                                conv.LibArticle = dr["LibArticle"].ToString();
                            if (dr["Quantite"] != DBNull.Value)
                                conv.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["Image_Article"] != DBNull.Value)
                                conv.Image_Article = (byte[])dr["Image_Article"];
                            if (dr["BSupplementaire"] != DBNull.Value)
                                conv.BSupplementaire = bool.Parse(dr["BSupplementaire"].ToString());
                           
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
        
        public static ConventionClientSimulation Charger(string nConvention)
        {
            ConventionClientSimulation conv = null;
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
                    cmd.CommandText = "ConventionClientSimulation_Charger";
                    cmd.Parameters.AddWithValue("@NConvention", nConvention);
                    cmd.Parameters.AddWithValue("@CArticle", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Ordre", DBNull.Value);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            conv = new ConventionClientSimulation();
                            conv.NConvention = dr["NConvention"].ToString();
                            conv.CArticle = dr["CArticle"].ToString();
                            conv.Ordre = int.Parse(dr["Ordre"].ToString());
                            if (dr["CEntrepot"] != DBNull.Value)
                                conv.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["LibArticle"] != DBNull.Value)
                                conv.LibArticle = dr["LibArticle"].ToString();
                            if (dr["Quantite"] != DBNull.Value)
                                conv.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["Image_Article"] != DBNull.Value)
                                conv.Image_Article = (byte[])dr["Image_Article"];
                            if (dr["BSupplementaire"] != DBNull.Value)
                                conv.BSupplementaire = bool.Parse(dr["BSupplementaire"].ToString());

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
    public class ConventionClientSimulationCollection : List<ConventionClientSimulation>
    {
        public ConventionClientSimulationCollection()
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
                cmd.CommandText = "ConventionClientSimulationRpt_Charger";
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

        public static ConventionClientSimulationCollection Charger(string nConvention)
        {
            ConventionClientSimulationCollection collection = new ConventionClientSimulationCollection();

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
                    cmd.CommandText = "ConventionClientSimulation_Charger";
                    cmd.Parameters.AddWithValue("@NConvention", nConvention);
                    cmd.Parameters.AddWithValue("@CArticle", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Ordre", DBNull.Value);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ConventionClientSimulation conv = new ConventionClientSimulation();
                            conv.NConvention = dr["NConvention"].ToString();
                            conv.CArticle = dr["CArticle"].ToString();
                            if (dr["Ordre"] != DBNull.Value)
                            conv.Ordre = int.Parse(dr["Ordre"].ToString());
                            if (dr["CEntrepot"] != DBNull.Value)
                                conv.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["LibArticle"] != DBNull.Value)
                                conv.LibArticle = dr["LibArticle"].ToString();
                            if (dr["Quantite"] != DBNull.Value)
                                conv.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["CUnite"] != DBNull.Value)
                                conv.CUnite = dr["CUnite"].ToString();
                            if (dr["Image_Article"] != DBNull.Value)
                                conv.Image_Article = (byte[])dr["Image_Article"];
                            if (dr["BSupplementaire"] != DBNull.Value)
                                conv.BSupplementaire = bool.Parse(dr["BSupplementaire"].ToString());
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

        public static ConventionClientSimulationCollection ChargerNonAffecter(string nConvention)
        {
            ConventionClientSimulationCollection collection = new ConventionClientSimulationCollection();

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
                    cmd.CommandText = "ConventionClientSimulation_ChargerNonAffecter";
                    cmd.Parameters.AddWithValue("@NConvention", nConvention);
                    cmd.Parameters.AddWithValue("@CArticle", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Ordre", DBNull.Value);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ConventionClientSimulation conv = new ConventionClientSimulation();
                            conv.NConvention = dr["NConvention"].ToString();
                            conv.CArticle = dr["CArticle"].ToString();
                            if (dr["Ordre"] != DBNull.Value)
                                conv.Ordre = int.Parse(dr["Ordre"].ToString());
                            if (dr["CEntrepot"] != DBNull.Value)
                                conv.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["LibArticle"] != DBNull.Value)
                                conv.LibArticle = dr["LibArticle"].ToString();
                            if (dr["Quantite"] != DBNull.Value)
                                conv.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["CUnite"] != DBNull.Value)
                                conv.CUnite = dr["CUnite"].ToString();
                            if (dr["Image_Article"] != DBNull.Value)
                                conv.Image_Article = (byte[])dr["Image_Article"];
                            if (dr["BSupplementaire"] != DBNull.Value)
                                conv.BSupplementaire = bool.Parse(dr["BSupplementaire"].ToString());
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
