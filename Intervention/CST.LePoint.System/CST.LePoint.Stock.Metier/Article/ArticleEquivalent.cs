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
    public class ArticleEquivalentCollection : List<ArticleEquivalent>
    {
        public ArticleEquivalentCollection()
        {
        }

        public static DataSet ChargerVue(string cArticle)
        {
            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "RptArticleEquivalent_Charger";
                cmd.Parameters.AddWithValue("@CArticle", cArticle);
                cmd.Parameters.AddWithValue("@CFournisseur", DBNull.Value);
                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "RptArticleEquivalent_Charger");
            }
            return (ds);
        }

        public static ArticleEquivalentCollection Charger(string cArticle)
        {
            ArticleEquivalentCollection Collection = new ArticleEquivalentCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "ArticleEquivalent_Charger";
                    cmd.Parameters.AddWithValue("@CArticle", cArticle);
                    cmd.Parameters.AddWithValue("@CArticleEquivalent", DBNull.Value);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ArticleEquivalent articleequivalent = new ArticleEquivalent();
                            articleequivalent.CArticle = dr["CArticle"].ToString();
                            articleequivalent.CArticleEquivalent = dr["CArticleEquivalent"].ToString();
                            Collection.Add(articleequivalent);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return Collection;
        }
    }

    [Serializable]
    public class ArticleEquivalent
    {
        public string CArticle { get; set; }

        public string CArticleEquivalent { get; set; }

        [XmlAttribute("DateInsertion")]
        [Bindable(true)]
        public DateTime? DateInsertion { get; set; }

        [XmlAttribute("CreePar")]
        [Bindable(true)]
        public int CreePar { get; set; }

        [XmlAttribute("PCInsertion")]
        [Bindable(true)]
        public string PCInsertion { get; set; }

        public ArticleEquivalent()
        {
        }

        public ArticleEquivalent(string cArticle, string cArticleEquivalent)
        {
            this.CArticle = cArticle;
            this.CArticleEquivalent = cArticleEquivalent;
        }

        public ArticleEquivalent(string cArticle)
        {
            this.CArticle = cArticle;
        }

        public void Sauvegarder(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "ArticleEquivalent_Inserer";
                cmd.Parameters.AddWithValue("@CArticle", this.CArticle);
                cmd.Parameters.AddWithValue("@CArticleEquivalent", this.CArticleEquivalent);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                cmd.Parameters.AddWithValue("@CreePar", this.CreePar);
                cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);

                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null || parametre.Value == "")

                        parametre.Value = DBNull.Value;

                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
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

                cmd.CommandText = "ArticleEquivalent_Supprimer";
                cmd.Parameters.AddWithValue("@CArticle", this.CArticle);
                cmd.Parameters.AddWithValue("@CArticleEquivalent", this.CArticleEquivalent);
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
                Supprimer(transaction);
                transaction.Commit();
            }
        }

        public static ArticleEquivalent Charger(string cArticle, string cArticleEquivalent)
        {
            ArticleEquivalent articleEquivalent = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "ArticleEquivalent_Charger";
                    cmd.Parameters.AddWithValue("@CArticle", cArticle);
                    cmd.Parameters.AddWithValue("@CArticleEquivalent", cArticleEquivalent);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            articleEquivalent = new ArticleEquivalent();

                            articleEquivalent.CArticle = dr["CArticle"].ToString();
                            articleEquivalent.CArticleEquivalent = dr["CArticleEquivalent"].ToString();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return articleEquivalent;
        }
    }
}