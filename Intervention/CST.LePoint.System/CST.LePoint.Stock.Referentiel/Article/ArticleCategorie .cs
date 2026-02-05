using CST.LePoint.Referentiel;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace CST.LePoint.Stock.Referentiel.Article
{
    [Serializable]
    public class ArticleCategorieCollection : ItemCollection
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
                cmd.CommandText = "ArticleCategorie_Rpt_Charger";
                cmd.Parameters.AddWithValue("@CArticleCategorie", DBNull.Value);
                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "ListeBanque_Rpt_Charger");
            }
            return (ds);
        }

        public static DataTable RemplirGrid()
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Ref_ArticleCategorie_Charger";
                cmd.Parameters.AddWithValue("@CArticleCategorie", DBNull.Value);
                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            return (dt);
        }

        public static ArticleCategorieCollection Charger()
        {
            ArticleCategorieCollection articleCategoriecollection = new ArticleCategorieCollection();
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Ref_ArticleCategorie_Charger";
                cmd.Parameters.AddWithValue("@CArticleCategorie", DBNull.Value);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ArticleCategorie articleCategorie = new ArticleCategorie();

                    articleCategorie.Code = dr["CArticleCategorie"].ToString();
                    articleCategorie.Libelle = dr["LibArticleCategorie"].ToString();
                    articleCategoriecollection.Add(articleCategorie);
                }
                dr.Close();
                return (articleCategoriecollection);
            }
        }
    }

    [Serializable]
    public class ArticleCategorie : Item
    {
        #region Propriétés

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

        public ArticleCategorie()
        { }

        public ArticleCategorie(string cArticleCategorie)
        {
            Code = cArticleCategorie;
        }

        public ArticleCategorie(string cArticleCategorie, string libArticleCategorie)
        {
            Code = cArticleCategorie;
            Libelle = libArticleCategorie;
        }

        public void Sauvegarder()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_ArticleCategorie_Sauvegarder";
                    cmd.Parameters.AddWithValue("@CArticleCategorie ", Code);
                    cmd.Parameters.AddWithValue("@LibArticleCategorie", Libelle);
                    cmd.Parameters.AddWithValue("@CreePar", this.CreePar);
                    cmd.Parameters.AddWithValue("@ModifiePar", this.ModifiePar);
                    cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);
                    cmd.Parameters.AddWithValue("@PCModification", this.PCModification);
                    cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                    cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);

                    foreach (SqlParameter parametre in cmd.Parameters)
                    {
                        if (parametre.Value == null) parametre.Value = DBNull.Value;
                    }

                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public static ArticleCategorie Charger(string cArticleCategorie)
        {
            ArticleCategorie articleCategorie = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_ArticleCategorie_Charger";
                    cmd.Parameters.AddWithValue("@CArticleCategorie", cArticleCategorie);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            articleCategorie = new ArticleCategorie();
                            articleCategorie.Code = dr["CArticleCategorie"].ToString();
                            if (dr["LibArticleCategorie"] != DBNull.Value)
                                articleCategorie.Libelle = dr["LibArticleCategorie"].ToString();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return articleCategorie;
        }

        public void Supprimer()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_ArticleCategorie_Supprimer";
                    cmd.Parameters.AddWithValue("@CArticleCategorie ", Code);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}