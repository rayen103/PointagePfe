using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

//using CST.Framework;

namespace CST.LePoint.Stock.Metier
{
    [Serializable]
    public class ArticlePrix
    {
        [XmlAttribute("CArticle")]
        [Bindable(true)]
        public string CArticle { get; set; }

        [XmlAttribute("CTarif")]
        [Bindable(true)]
        public string CTarif { get; set; }

        [XmlAttribute("Marge")]
        [Bindable(true)]
        public decimal Marge { get; set; }

        [XmlAttribute("MargeReel")]
        [Bindable(true)]
        public decimal MargeReel { get; set; }

        [XmlAttribute("PrixHT")]
        [Bindable(true)]
        public decimal PrixHT { get; set; }

        [XmlAttribute("PrixTTC")]
        [Bindable(true)]
        public decimal PrixTTC { get; set; }

        [XmlAttribute("Remise")]
        [Bindable(true)]
        public decimal Remise { get; set; }

        [XmlAttribute("RemiseMax")]
        [Bindable(true)]
        public decimal RemiseMax { get; set; }

        [XmlAttribute("TauxVente")]
        [Bindable(true)]
        public decimal TauxVente { get; set; }

        [XmlAttribute("MargeMin")]
        [Bindable(true)]
        public decimal MargeMin { get; set; }

        [XmlAttribute("MargeDetaillent")]
        [Bindable(true)]
        public decimal MargeDetaillent { get; set; }

        [XmlAttribute("PrixPublic")]
        [Bindable(true)]
        public decimal PrixPublic { get; set; }

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

        public ArticlePrix()
        {
        }

        public ArticlePrix(string cArticle, string cTarif, DateTime DateDebut)
        {
            this.CArticle = cArticle;
            this.CTarif = cTarif;
        }

        public void Sauvegarder(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "ArticlePrix_Sauvegarder";
                cmd.Parameters.AddWithValue("@CArticle", CArticle);
                cmd.Parameters.AddWithValue("@CTarif", CTarif);
                cmd.Parameters.AddWithValue("@Marge", Marge);
                cmd.Parameters.AddWithValue("@MargeReel", MargeReel);
                cmd.Parameters.AddWithValue("@PrixHT", PrixHT);
                cmd.Parameters.AddWithValue("@PrixTTC", PrixTTC);
                cmd.Parameters.AddWithValue("@Remise", Remise);
                cmd.Parameters.AddWithValue("@RemiseMax", RemiseMax);
                cmd.Parameters.AddWithValue("@TauxVente", TauxVente);
                cmd.Parameters.AddWithValue("@MargeMin", MargeMin);
                cmd.Parameters.AddWithValue("@MargeDetaillent", MargeDetaillent);
                cmd.Parameters.AddWithValue("@PrixPublic", PrixPublic);
                cmd.Parameters.AddWithValue("@PCInsertion", PCInsertion);
                cmd.Parameters.AddWithValue("@CreePar", CreePar);
                cmd.Parameters.AddWithValue("@PCModification", PCModification);
                cmd.Parameters.AddWithValue("@ModifiePar", ModifiePar);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null || parametre.Value == "")
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

        public void Sauvegarder()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                Sauvegarder(transaction);
                transaction.Commit();
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

                cmd.CommandText = "ArticlePrix_Supprimer";
                cmd.Parameters.AddWithValue("@CArticle", CArticle);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ArticlePrix Charger(string cArticle, string cTarif)
        {
            ArticlePrix ArticlePrix = null;
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ArticlePrix_Charger";
                cmd.Parameters.AddWithValue("@CArticle", cArticle);
                cmd.Parameters.AddWithValue("@CTarif", cTarif);
                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    ArticlePrix = new ArticlePrix();
                    ArticlePrix.CArticle = dr["CArticle"].ToString();
                    ArticlePrix.CTarif = dr["CTarif"].ToString();

                    if (dr["Marge"] != DBNull.Value)
                        ArticlePrix.Marge = decimal.Parse(dr["Marge"].ToString());
                    if (dr["MargeReel"] != DBNull.Value)
                        ArticlePrix.MargeReel = decimal.Parse(dr["MargeReel"].ToString());
                    if (dr["PrixHT"] != DBNull.Value)
                        ArticlePrix.PrixHT = decimal.Parse(dr["PrixHT"].ToString());
                    if (dr["PrixTTC"] != DBNull.Value)
                        ArticlePrix.PrixTTC = decimal.Parse(dr["PrixTTC"].ToString());
                    if (dr["Remise"] != DBNull.Value)
                        ArticlePrix.Remise = decimal.Parse(dr["Remise"].ToString());
                    if (dr["RemiseMax"] != DBNull.Value)
                        ArticlePrix.RemiseMax = decimal.Parse(dr["RemiseMax"].ToString());
                    if (dr["TauxVente"] != DBNull.Value)
                        ArticlePrix.TauxVente = decimal.Parse(dr["TauxVente"].ToString());
                    if (dr["MargeMin"] != DBNull.Value)
                        ArticlePrix.MargeMin = decimal.Parse(dr["MargeMin"].ToString());
                    if (dr["MargeDetaillent"] != DBNull.Value)
                        ArticlePrix.MargeDetaillent = decimal.Parse(dr["MargeDetaillent"].ToString());
                    if (dr["PrixPublic"] != DBNull.Value)
                        ArticlePrix.PrixPublic = decimal.Parse(dr["PrixPublic"].ToString());
                }
                dr.Close();
            }
            return (ArticlePrix);
        }
    }

    [Serializable]
    public class ArticlePrixCollection : List<ArticlePrix>
    {
        public static DataSet ChargerVue(string cEntrepot, string cCategorie, string cFamille, string cType, string cNature, string cModele, string cModele1, string cModele2, string cTarif)
        {
            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ArticlePrix_Rpt_Charger";
                cmd.Parameters.AddWithValue("@CCategorie", cCategorie);
                cmd.Parameters.AddWithValue("@CFamille", cFamille);
                cmd.Parameters.AddWithValue("@CType", cType);
                cmd.Parameters.AddWithValue("@CTarif", cTarif);
                cmd.Parameters.AddWithValue("@CModele", cModele);
                cmd.Parameters.AddWithValue("@CModele1", cModele1);
                cmd.Parameters.AddWithValue("@CModele2", cModele2);
                cmd.Parameters.AddWithValue("@CNature", cNature);
                cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "ArticlePrix_Rpt_Charger");
            }

            return (ds);
        }

        public static ArticlePrixCollection Charger(string cArticle, bool bArticlePrixActif)
        {
            var ArticlePrixcollection = new ArticlePrixCollection();

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ArticlePrix_Charger";
                cmd.Parameters.AddWithValue("@CArticle", cArticle);
                cmd.Parameters.AddWithValue("@CTarif", DBNull.Value);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    ArticlePrix ArticlePrix = new ArticlePrix();

                    ArticlePrix.CArticle = dr["CArticle"].ToString();
                    ArticlePrix.CTarif = dr["CTarif"].ToString();

                    if (dr["Marge"] != DBNull.Value)
                        ArticlePrix.Marge = decimal.Parse(dr["Marge"].ToString());
                    if (dr["MargeReel"] != DBNull.Value)
                        ArticlePrix.MargeReel = decimal.Parse(dr["MargeReel"].ToString());
                    if (dr["PrixHT"] != DBNull.Value)
                        ArticlePrix.PrixHT = decimal.Parse(dr["PrixHT"].ToString());
                    if (dr["PrixTTC"] != DBNull.Value)
                        ArticlePrix.PrixTTC = decimal.Parse(dr["PrixTTC"].ToString());
                    if (dr["Remise"] != DBNull.Value)
                        ArticlePrix.Remise = decimal.Parse(dr["Remise"].ToString());
                    if (dr["RemiseMax"] != DBNull.Value)
                        ArticlePrix.RemiseMax = decimal.Parse(dr["RemiseMax"].ToString());
                    if (dr["TauxVente"] != DBNull.Value)
                        ArticlePrix.TauxVente = decimal.Parse(dr["TauxVente"].ToString());
                    if (dr["MargeMin"] != DBNull.Value)
                        ArticlePrix.MargeMin = decimal.Parse(dr["MargeMin"].ToString());
                    if (dr["MargeDetaillent"] != DBNull.Value)
                        ArticlePrix.MargeDetaillent = decimal.Parse(dr["MargeDetaillent"].ToString());
                    if (dr["PrixPublic"] != DBNull.Value)
                        ArticlePrix.PrixPublic = decimal.Parse(dr["PrixPublic"].ToString());

                    ArticlePrixcollection.Add(ArticlePrix);
                }
                dr.Close();
            }
            return (ArticlePrixcollection);
        }
    }
}