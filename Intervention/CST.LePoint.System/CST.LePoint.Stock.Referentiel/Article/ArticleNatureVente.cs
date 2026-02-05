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
    public class ArticleNatureVenteCollection : ItemCollection
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
                cmd.CommandText = "RptArticleNatureVente_Charger";
                cmd.Parameters.AddWithValue("@CArticleNatureVente", DBNull.Value);
                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "RptArticleNatureVente_Charger");
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
                cmd.CommandText = "Ref_ArticleNatureVente_Charger";
                cmd.Parameters.AddWithValue("@CNatureVente", DBNull.Value);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            return (dt);
        }

        public static ArticleNatureVenteCollection Charger()
        {
            ArticleNatureVenteCollection ArticleNatureVentecollection = new ArticleNatureVenteCollection();
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Ref_ArticleNatureVente_Charger";
                cmd.Parameters.AddWithValue("@CNatureVente", DBNull.Value);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    ArticleNatureVente ArticleNatureVente = new ArticleNatureVente();
                    ArticleNatureVente.Code = dr["CNatureVente"].ToString();
                    if (dr["LibNatureVente"] != DBNull.Value)
                        ArticleNatureVente.Libelle = dr["LibNatureVente"].ToString();
                    ArticleNatureVentecollection.Add(ArticleNatureVente);
                }
                dr.Close();

                return (ArticleNatureVentecollection);
            }
        }
    }

    [Serializable]
    public class ArticleNatureVente : Item
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

        public ArticleNatureVente()
        {
        }

        public ArticleNatureVente(string cNatureVente)
        {
            this.Code = cNatureVente;
        }

        public ArticleNatureVente(string cNatureVente, string Lib)
        {
            this.Code = cNatureVente;
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

                    cmd.CommandText = "Ref_ArticleNatureVente_Sauvegarder";
                    cmd.Parameters.AddWithValue("@CNatureVente", Code);
                    cmd.Parameters.AddWithValue("@LibNatureVente", Libelle);
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

        public static ArticleNatureVente Charger(string cNatureVente)
        {
            ArticleNatureVente articleNatureVente = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_ArticleNatureVente_Charger";
                    cmd.Parameters.AddWithValue("@CNatureVente", cNatureVente);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            articleNatureVente = new ArticleNatureVente();
                            articleNatureVente.Code = dr["CNatureVente"].ToString();
                            articleNatureVente.Libelle = dr["LibNatureVente"].ToString();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return articleNatureVente;
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

                    cmd.CommandText = "Ref_ArticleNatureVente_Supprimer";
                    cmd.Parameters.AddWithValue("@CNatureVente", Code);
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