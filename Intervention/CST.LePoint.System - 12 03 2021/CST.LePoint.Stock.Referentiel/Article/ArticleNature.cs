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
    public class ArticleNatureCollection : ItemCollection
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
                cmd.CommandText = "RptArticleNature_Charger";
                cmd.Parameters.AddWithValue("@CArticleNature", DBNull.Value);
                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "RptArticleNature_Charger");
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
                cmd.CommandText = "Ref_ArticleNature_Charger";
                cmd.Parameters.AddWithValue("@CNature", DBNull.Value);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            return (dt);
        }

        public static ArticleNatureCollection Charger()
        {
            ArticleNatureCollection ArticleNaturecollection = new ArticleNatureCollection();
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Ref_ArticleNature_Charger";
                cmd.Parameters.AddWithValue("@CNature", DBNull.Value);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    ArticleNature ArticleNature = new ArticleNature();
                    ArticleNature.Code = dr["CNature"].ToString();
                    if (dr["LibNature"] != DBNull.Value)
                        ArticleNature.Libelle = dr["LibNature"].ToString();
                    ArticleNaturecollection.Add(ArticleNature);
                }
                dr.Close();

                return (ArticleNaturecollection);
            }
        }

        public static ArticleNatureCollection ChargerCommercial()
        {
            ArticleNatureCollection ArticleNaturecollection = new ArticleNatureCollection();
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Ref_ArticleNature_ChargerCommercial";
                cmd.Parameters.AddWithValue("@CNature", DBNull.Value);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    ArticleNature ArticleNature = new ArticleNature();
                    ArticleNature.Code = dr["CNature"].ToString();
                    if (dr["LibNature"] != DBNull.Value)
                        ArticleNature.Libelle = dr["LibNature"].ToString();
                    ArticleNaturecollection.Add(ArticleNature);
                }
                dr.Close();

                return (ArticleNaturecollection);
            }
        }

    }

    [Serializable]
    public class ArticleNature : Item
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

        public ArticleNature()
        {
        }

        public ArticleNature(string cnature)
        {
            this.Code = cnature;
        }

        public ArticleNature(string cnature, string Lib)
        {
            this.Code = cnature;
            this.Libelle = Lib;
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

                    cmd.CommandText = "Ref_ArticleNature_Sauvegarder";
                    cmd.Parameters.AddWithValue("@CNature", Code);
                    cmd.Parameters.AddWithValue("@LibNature", Libelle);
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

        public static ArticleNature Charger(string cNature)
        {
            ArticleNature articleNature = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_ArticleNature_Charger";
                    cmd.Parameters.AddWithValue("@CNature", cNature);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            articleNature = new ArticleNature();
                            articleNature.Code = dr["CNature"].ToString();
                            articleNature.Libelle = dr["LibNature"].ToString();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return articleNature;
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

                    cmd.CommandText = "Ref_ArticleNature_Supprimer";
                    cmd.Parameters.AddWithValue("@CNature", Code);
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
    }
}