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
    public class ArticleSousModele1Collection : ItemCollection
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
                cmd.CommandText = "ArticleSousModele1_Rpt_Charger";
                cmd.Parameters.AddWithValue("@CSousModele1Article", DBNull.Value);
                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "ArticleSousModele1_Rpt_Charger");
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
                cmd.CommandText = "Ref_ArticleSousModele1_Charger";
                cmd.Parameters.AddWithValue("@CSousModele1Article", DBNull.Value);
                cmd.Parameters.AddWithValue("@CModeleArticle", DBNull.Value);
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

        public static ArticleSousModele1Collection Charger()
        {
            ArticleSousModele1Collection collection = new ArticleSousModele1Collection();

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
                    cmd.CommandText = "Ref_ArticleSousModele1_Charger";
                    cmd.Parameters.AddWithValue("@CSousModele1Article", DBNull.Value);
                    cmd.Parameters.AddWithValue("@CModeleArticle", DBNull.Value);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ArticleSousModele1 sousModele1 = new ArticleSousModele1();
                            if (dr["CSousModele1Article"] != DBNull.Value)
                                sousModele1.Code = dr["CSousModele1Article"].ToString();
                            if (dr["CModeleArticle"] != DBNull.Value)
                                sousModele1.CModeleArticle = dr["CModeleArticle"].ToString();
                            if (dr["LibSousModele1Article"] != DBNull.Value)
                                sousModele1.Libelle = dr["LibSousModele1Article"].ToString();
                            collection.Add(sousModele1);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return (collection);
        }
        public static ArticleSousModele1Collection Charger1()
        {
            ArticleSousModele1Collection collection = new ArticleSousModele1Collection();

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
                    cmd.CommandText = "Ref_ArticleSousModele1_Charger1";
                    cmd.Parameters.AddWithValue("@CSousModele1Article", DBNull.Value);
                    cmd.Parameters.AddWithValue("@CModeleArticle", DBNull.Value);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ArticleSousModele1 sousModele1 = new ArticleSousModele1();
                            if (dr["CSousModele1Article"] != DBNull.Value)
                                sousModele1.Code = dr["CSousModele1Article"].ToString();
                            if (dr["CModeleArticle"] != DBNull.Value)
                                sousModele1.CModeleArticle = dr["CModeleArticle"].ToString();
                            if (dr["LibSousModele1Article"] != DBNull.Value)
                                sousModele1.Libelle = dr["LibSousModele1Article"].ToString();
                            collection.Add(sousModele1);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return (collection);
        }

        public static ArticleSousModele1Collection Charger(string CModeleArticle)
        {
            ArticleSousModele1Collection collection = new ArticleSousModele1Collection();

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
                    cmd.CommandText = "Ref_ArticleSousModele1_Charger";
                    cmd.Parameters.AddWithValue("@CSousModele1Article", DBNull.Value);
                    cmd.Parameters.AddWithValue("@CModeleArticle", CModeleArticle);
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
                            ArticleSousModele1 sousModele1 = new ArticleSousModele1();
                            if (dr["CSousModele1Article"] != DBNull.Value)
                                sousModele1.Code = dr["CSousModele1Article"].ToString();
                            if (dr["CModeleArticle"] != DBNull.Value)
                                sousModele1.CModeleArticle = dr["CModeleArticle"].ToString();
                            if (dr["LibSousModele1Article"] != DBNull.Value)
                                sousModele1.Libelle = dr["LibSousModele1Article"].ToString();
                            collection.Add(sousModele1);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return (collection);
        }
    }

    [Serializable]
    public class ArticleSousModele1 : Item
    {
        #region Propriétés

        [XmlAttribute("CModeleArticle")]
        [Bindable(true)]
        public string CModeleArticle { get; set; }

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

        public ArticleSousModele1()
        {
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

                    cmd.CommandText = "Ref_ArticleSousModele1_Sauvegarder";
                    cmd.Parameters.AddWithValue("@CSousModele1Article", this.Code);
                    cmd.Parameters.AddWithValue("@CModeleArticle", this.CModeleArticle);
                    cmd.Parameters.AddWithValue("@LibSousModele1Article", this.Libelle);
                    cmd.Parameters.AddWithValue("@CreePar", this.CreePar);
                    cmd.Parameters.AddWithValue("@ModifiePar", this.ModifiePar);
                    cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);
                    cmd.Parameters.AddWithValue("@PCModification", this.PCModification);
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

                    cmd.CommandText = "Ref_ArticleSousModele1_Supprimer";
                    cmd.Parameters.AddWithValue("@CSousModele1Article", this.Code);
                    cmd.Parameters.AddWithValue("@CModeleArticle", this.CModeleArticle);

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

        public static ArticleSousModele1 Charger(string cArticleModele, string cSousModele1Article)
        {
            ArticleSousModele1 sousModele = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_ArticleSousModele1_Charger";
                    cmd.Parameters.AddWithValue("@CModeleArticle", cArticleModele);
                    cmd.Parameters.AddWithValue("@CSousModele1Article", cSousModele1Article);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            sousModele = new ArticleSousModele1();
                            sousModele.CModeleArticle = dr["CModeleArticle"].ToString();
                            sousModele.Code = dr["CSousModele1Article"].ToString();
                            if (dr["LibSousModele1Article"] != DBNull.Value)
                                sousModele.Libelle = dr["LibSousModele1Article"].ToString();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return sousModele;
        }
    }
}