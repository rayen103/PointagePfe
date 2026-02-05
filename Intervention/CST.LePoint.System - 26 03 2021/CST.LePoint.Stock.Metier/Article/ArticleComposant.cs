using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace CST.LePoint.Stock.Metier
{
    [Serializable]
    public class ArticleComposant
    {
        [XmlAttribute("CArticle")]
        [Bindable(true)]
        public string CArticle { get; set; }

        [XmlAttribute("CComposant")]
        [Bindable(true)]
        public string CComposant { get; set; }

        [XmlAttribute("Quantite")]
        [Bindable(true)]
        public decimal Quantite { get; set; }

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

        public ArticleComposant()
        {
        }

        public ArticleComposant(string carticle, string cComposant)
        {
            this.CArticle = carticle;
            this.CComposant = cComposant;
        }

        public ArticleComposant(string carticle)
        {
            CArticle = carticle;
        }

        public void Sauvegarder()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    Sauvegarder(transaction);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public void Sauvegarder(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "ArticleComposant_Sauvegarder";
                cmd.Parameters.AddWithValue("@CArticle", CArticle);
                cmd.Parameters.AddWithValue("@CComposant", CComposant);
                cmd.Parameters.AddWithValue("@Quantite", Quantite);
                cmd.Parameters.AddWithValue("@PCInsertion", PCInsertion);
                cmd.Parameters.AddWithValue("@CreePar", CreePar);
                cmd.Parameters.AddWithValue("@PCModification", PCModification);
                cmd.Parameters.AddWithValue("@ModifiePar", ModifiePar);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                foreach (SqlParameter parameter in cmd.Parameters)
                {
                    if (parameter.Value == null || parameter.Value == "")
                    {
                        parameter.Value = DBNull.Value;
                    }
                }
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
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

                    cmd.CommandText = "ArticleComposant_Supprimer";
                    cmd.Parameters.AddWithValue("@CArticle", CArticle);
                    cmd.Parameters.AddWithValue("@CComposant", CComposant);
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

        public void Supprimer(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "ArticleComposant_Supprimer";
                cmd.Parameters.AddWithValue("@CArticle", CArticle);
                cmd.Parameters.AddWithValue("@CComposant", CComposant);
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static ArticleComposant Charger(string cArticle, string cComposant)
        {
            ArticleComposant articleComposant = null;
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ArticleComposant_Charger";
                cmd.Parameters.AddWithValue("@CArticle", cArticle);
                cmd.Parameters.AddWithValue("@CComposant", cComposant);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    articleComposant = new ArticleComposant();
                    articleComposant.CArticle = dr["CArticle"].ToString();
                    articleComposant.CComposant = dr["CComposant"].ToString();
                    if (dr["Quantite"] != DBNull.Value)
                        articleComposant.Quantite = decimal.Parse(dr["Quantite"].ToString());
                }
                dr.Close();
            }
            return articleComposant;
        }
    }

    [Serializable]
    public class ArticleComposantCollection : List<ArticleComposant>
    {
        public static DataSet ChargerVue(string cArticle)
        {
            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "RptArticleComposant_Charger";
                cmd.Parameters.AddWithValue("@CArticle", cArticle);
                cmd.Parameters.AddWithValue("@CComposant", DBNull.Value);
                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "RptArticleComposant_Charger");
            }
            return (ds);
        }

        public static ArticleComposantCollection Charger(string cArticle)
        {
            ArticleComposantCollection collection = new ArticleComposantCollection();
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ArticleComposant_Charger";
                cmd.Parameters.AddWithValue("@CArticle", cArticle);
                cmd.Parameters.AddWithValue("@CComposant", DBNull.Value);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    ArticleComposant articleComposant = new ArticleComposant();
                    articleComposant.CArticle = dr["CArticle"].ToString();
                    articleComposant.CComposant = dr["CComposant"].ToString();
                    if (dr["Quantite"] != DBNull.Value)
                        articleComposant.Quantite = decimal.Parse(dr["Quantite"].ToString());
                    collection.Add(articleComposant);
                }
                dr.Close();
            }
            return collection;
        }
    }
}