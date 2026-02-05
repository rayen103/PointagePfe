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
    public class ArticleFournisseur
    {
        [XmlAttribute("CArticle")]
        [Bindable(true)]
        public string CArticle { get; set; }

        [XmlAttribute("CFournisseur")]
        [Bindable(true)]
        public string CFournisseur { get; set; }

        [XmlAttribute("CArticleFournisseur")]
        [Bindable(true)]
        public string CArticleFournisseur { get; set; }

        [XmlAttribute("CumuleAchat")]
        [Bindable(true)]
        public decimal CumuleAchat { get; set; }

        [XmlAttribute("DelaiLivraison")]
        [Bindable(true)]
        public decimal DelaiLivraison { get; set; }

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

        public ArticleFournisseur()
        {
        }

        public ArticleFournisseur(string cArticle, string cFournisseur)
        {
            this.CArticle = cArticle;
            this.CFournisseur = cFournisseur;
        }

        public ArticleFournisseur(string cArticle)
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

                cmd.CommandText = "ArticleFournisseur_Sauvegarder";
                cmd.Parameters.AddWithValue("@CArticle", CArticle);
                cmd.Parameters.AddWithValue("@CFournisseur", CFournisseur);
                cmd.Parameters.AddWithValue("@CArticleFournisseur", CArticleFournisseur);
                cmd.Parameters.AddWithValue("@DelaiLivraison", DelaiLivraison);
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

                cmd.CommandText = "ArticleFournisseur_Supprimer";
                cmd.Parameters.AddWithValue("@CArticle", CArticle);
                cmd.Parameters.AddWithValue("@CFournisseur", CFournisseur);
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

        public static ArticleFournisseur Charger(string cArticle, string cFournisseur)
        {
            ArticleFournisseur articlefournisseur = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "ArticleFournisseur_Charger";
                    cmd.Parameters.AddWithValue("@CArticle", cArticle);
                    cmd.Parameters.AddWithValue("@CFournisseur", cFournisseur);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            articlefournisseur = new ArticleFournisseur();
                            if (dr["CArticleFournisseur"] != DBNull.Value)
                                articlefournisseur.CArticleFournisseur = dr["CArticleFournisseur"].ToString();
                            if (dr["CArticle"] != DBNull.Value)
                                articlefournisseur.CArticle = dr["CArticle"].ToString();
                            if (dr["CFournisseur"] != DBNull.Value)
                                articlefournisseur.CFournisseur = dr["CFournisseur"].ToString();
                            if (dr["CumuleAchat"] != DBNull.Value)
                                articlefournisseur.CumuleAchat = decimal.Parse(dr["CumuleAchat"].ToString());
                            if (dr["DelaiLivraison"] != DBNull.Value)
                                articlefournisseur.DelaiLivraison = decimal.Parse(dr["DelaiLivraison"].ToString());
                            if (dr["Quantite"] != DBNull.Value)
                                articlefournisseur.Quantite = decimal.Parse(dr["Quantite"].ToString());
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return articlefournisseur;
        }
    }

    [Serializable]
    public class ArticleFournisseurCollection : List<ArticleFournisseur>
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
                cmd.CommandText = "RptArticleFournisseur_Charger";
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
                sda.Fill(ds, "RptArticleFournisseur_Charger");
            }
            return (ds);
        }

        public static ArticleFournisseurCollection Charger(string cArticle)
        {
            ArticleFournisseurCollection Collection = new ArticleFournisseurCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "ArticleFournisseur_Charger";
                    cmd.Parameters.AddWithValue("@CArticle", cArticle);
                    cmd.Parameters.AddWithValue("@CFournisseur", DBNull.Value);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ArticleFournisseur articlefournisseur = new ArticleFournisseur();
                            if (dr["CArticleFournisseur"] != DBNull.Value)
                                articlefournisseur.CArticleFournisseur = dr["CArticleFournisseur"].ToString();
                            articlefournisseur.CArticle = dr["CArticle"].ToString();
                            articlefournisseur.CFournisseur = dr["CFournisseur"].ToString();
                            if (dr["CumuleAchat"] != DBNull.Value)
                                articlefournisseur.CumuleAchat = decimal.Parse(dr["CumuleAchat"].ToString());
                            if (dr["DelaiLivraison"] != DBNull.Value)
                                articlefournisseur.DelaiLivraison = decimal.Parse(dr["DelaiLivraison"].ToString());
                            if (dr["Quantite"] != DBNull.Value)
                                articlefournisseur.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            Collection.Add(articlefournisseur);
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
}