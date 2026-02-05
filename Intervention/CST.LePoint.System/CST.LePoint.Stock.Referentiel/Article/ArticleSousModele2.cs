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
    public class ArticleSousModele2Collection : ItemCollection
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
                cmd.CommandText = "ArticleSousModele2_Rpt_Charger";
                cmd.Parameters.AddWithValue("@CSousModele2Article", DBNull.Value);
                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "ArticleSousModele2_Rpt_Charger");
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
                cmd.CommandText = "Ref_ArticleSousModele2_Charger";
                cmd.Parameters.AddWithValue("@CSousModele2Article", DBNull.Value);
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

        public static ArticleSousModele2Collection Charger(string cModeleArticle, string cSousModele1Article)
        {
            ArticleSousModele2Collection collection = new ArticleSousModele2Collection();

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
                    cmd.CommandText = "Ref_ArticleSousModele2_Charger";
                    cmd.Parameters.AddWithValue("@CSousModele2Article", DBNull.Value);
                    cmd.Parameters.AddWithValue("@CSousModele1Article", cSousModele1Article);
                    cmd.Parameters.AddWithValue("@CModeleArticle", cModeleArticle);
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
                            ArticleSousModele2 sousModele2 = new ArticleSousModele2();
                            sousModele2.Code = dr["CSousModele2Article"].ToString();
                            if (dr["CSousModele1Article"] != DBNull.Value)
                                sousModele2.CSousModele1Article = dr["CSousModele1Article"].ToString();
                            if (dr["CModeleArticle"] != DBNull.Value)
                                sousModele2.CModeleArticle = dr["CModeleArticle"].ToString();
                            if (dr["LibSousModele2Article"] != DBNull.Value)
                                sousModele2.Libelle = dr["LibSousModele2Article"].ToString();
                            collection.Add(sousModele2);
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

        public static ArticleSousModele2Collection Charger()
        {
            ArticleSousModele2Collection collection = new ArticleSousModele2Collection();

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
                    cmd.CommandText = "Ref_ArticleSousModele2_Charger";
                    cmd.Parameters.AddWithValue("@CSousModele2Article", DBNull.Value);
                    cmd.Parameters.AddWithValue("@CSousModele1Article", DBNull.Value);
                    cmd.Parameters.AddWithValue("@CModeleArticle", DBNull.Value);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ArticleSousModele2 sousModele2 = new ArticleSousModele2();
                            sousModele2.Code = dr["CSousModele2Article"].ToString();
                            if (dr["CSousModele1Article"] != DBNull.Value)
                                sousModele2.CSousModele1Article = dr["CSousModele1Article"].ToString();
                            if (dr["CModeleArticle"] != DBNull.Value)
                                sousModele2.CModeleArticle = dr["CModeleArticle"].ToString();
                            if (dr["LibSousModele2Article"] != DBNull.Value)
                                sousModele2.Libelle = dr["LibSousModele2Article"].ToString();
                            collection.Add(sousModele2);
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
        public static ArticleSousModele2Collection Charger1()
        {
            ArticleSousModele2Collection collection = new ArticleSousModele2Collection();

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
                    cmd.CommandText = "Ref_ArticleSousModele2_Charger1";
                    cmd.Parameters.AddWithValue("@CSousModele2Article", DBNull.Value);
                    cmd.Parameters.AddWithValue("@CSousModele1Article", DBNull.Value);
                    cmd.Parameters.AddWithValue("@CModeleArticle", DBNull.Value);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ArticleSousModele2 sousModele2 = new ArticleSousModele2();
                            sousModele2.Code = dr["CSousModele2Article"].ToString();
                            if (dr["CSousModele1Article"] != DBNull.Value)
                                sousModele2.CSousModele1Article = dr["CSousModele1Article"].ToString();
                            if (dr["CModeleArticle"] != DBNull.Value)
                                sousModele2.CModeleArticle = dr["CModeleArticle"].ToString();
                            if (dr["LibSousModele2Article"] != DBNull.Value)
                                sousModele2.Libelle = dr["LibSousModele2Article"].ToString();
                            collection.Add(sousModele2);
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
    public class ArticleSousModele2 : Item
    {
        #region Propriétés

        [XmlAttribute("CSousModele1Article")]
        [Bindable(true)]
        public string CSousModele1Article { get; set; }

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

        public ArticleSousModele2()
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

                    cmd.CommandText = "Ref_ArticleSousModele2_Sauvegarder";
                    cmd.Parameters.AddWithValue("@CSousModele2Article", this.Code);
                    cmd.Parameters.AddWithValue("@CSousModele1Article", this.CSousModele1Article);
                    cmd.Parameters.AddWithValue("@CModeleArticle", this.CModeleArticle);
                    cmd.Parameters.AddWithValue("@LibSousModele2Article", this.Libelle);
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

                    cmd.CommandText = "Ref_ArticleSousModele2_Supprimer";
                    cmd.Parameters.AddWithValue("@CSousModele1Article", this.CSousModele1Article);
                    cmd.Parameters.AddWithValue("@CSousModele2Article", this.Code);
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

        public static ArticleSousModele2 Charger(string CModeleArticle, string CSousModele1Article, string cSousModele2Article)
        {
            ArticleSousModele2 sousModele2 = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_ArticleSousModele2_Charger";

                    cmd.Parameters.AddWithValue("@CSousModele1Article", CSousModele1Article);
                    cmd.Parameters.AddWithValue("@CModeleArticle", CModeleArticle);
                    cmd.Parameters.AddWithValue("@CSousModele2Article", cSousModele2Article);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            sousModele2 = new ArticleSousModele2();
                            sousModele2.Code = dr["CSousModele2Article"].ToString();
                            if (dr["CSousModele1Article"] != DBNull.Value)
                                sousModele2.CSousModele1Article = dr["CSousModele1Article"].ToString();
                            if (dr["CModeleArticle"] != DBNull.Value)
                                sousModele2.CModeleArticle = dr["CModeleArticle"].ToString();
                            if (dr["LibSousModele2Article"] != DBNull.Value)
                                sousModele2.Libelle = dr["LibSousModele2Article"].ToString();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return sousModele2;
        }
    }
}