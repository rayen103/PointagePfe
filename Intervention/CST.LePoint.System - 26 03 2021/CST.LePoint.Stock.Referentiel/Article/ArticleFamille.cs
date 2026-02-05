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
    public class ArticleFamilleCollection : ItemCollection
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
                cmd.CommandText = "ArticleFamille_Rpt_Charger";
                cmd.Parameters.AddWithValue("@CArticleFamille", DBNull.Value);
                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "ArticleFamille_Rpt_Charger");
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
                cmd.CommandText = "Ref_ArticleFamille_Charger";
                cmd.Parameters.AddWithValue("@CArticleFamille", DBNull.Value);
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

        public static ArticleFamilleCollection Charger()
        {
            ArticleFamilleCollection articleFamilleCollection = new ArticleFamilleCollection();
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Ref_ArticleFamille_Charger";
                cmd.Parameters.AddWithValue("@CArticleFamille", DBNull.Value);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    ArticleFamille articleFamille = new ArticleFamille();
                    articleFamille.Code = dr["CArticleFamille"].ToString();
                    if (dr["LibArticleFamille"] != DBNull.Value)
                        articleFamille.Libelle = dr["LibArticleFamille"].ToString();
                    if (dr["BActive"] != DBNull.Value)
                        articleFamille.BActive = bool.Parse(dr["BActive"].ToString().Trim());
                    articleFamilleCollection.Add(articleFamille);
                }
                dr.Close();

                return (articleFamilleCollection);
            }
        }
    }

    [Serializable]
    public class ArticleFamille : Item
    {
        #region Propriétés

        public bool BActive { get; set; }
        public DateTime DateInsertion { get; set; }
        public DateTime DateModification { get; set; }
        public int CreePar { get; set; }
        public int ModifiePar { get; set; }
        public string PCInsertion { get; set; }
        public string PCModification { get; set; }
        public int Ordre { get; set; }

        #endregion Propriétés

        public ArticleFamille()
        {
            //    this.DateInsertion = DateTime.Now;
            //    this.DateModification = DateTime.Now;
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
                    cmd.CommandText = "Ref_ArticleFamille_Sauvegarder";
                    cmd.Parameters.AddWithValue("@CArticleFamille ", Code);
                    cmd.Parameters.AddWithValue("@LibArticleFamille", Libelle);
                    cmd.Parameters.AddWithValue("@BActive", this.BActive);
                    cmd.Parameters.AddWithValue("@Ordre", this.Ordre);
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
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public static ArticleFamille Charger(string cArticleFamille)
        {
            ArticleFamille articlefamille = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_ArticleFamille_Charger";
                    cmd.Parameters.AddWithValue("@CArticleFamille", cArticleFamille);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            articlefamille = new ArticleFamille();
                            articlefamille.Code = dr["CArticleFamille"].ToString();
                            if (dr["LibArticleFamille"] != DBNull.Value)
                                articlefamille.Libelle = dr["LibArticleFamille"].ToString();
                            if (dr["BActive"] != DBNull.Value)
                                articlefamille.BActive = bool.Parse(dr["BActive"].ToString().Trim());
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return articlefamille;
        }

        public void Supprimer()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = cn;
                    cmd.CommandText = "Ref_ArticleFamille_Supprimer";
                    cmd.Parameters.AddWithValue("@CArticleFamille ", Code);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null) parametre.Value = DBNull.Value;
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