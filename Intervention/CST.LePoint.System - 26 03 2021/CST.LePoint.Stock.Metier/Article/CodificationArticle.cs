using CST.LePoint.Referentiel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CST.Stock.Metier.Article
{
    [Serializable]
    public class CodificationArticle : Item
    {
     
        #region Propriétés

        [XmlAttribute("Composition")]
        [Bindable(true)]
        public string Composition { get; set; }

        [XmlAttribute("Ordre")]
        [Bindable(true)]
        public int Ordre { get; set; }

        [XmlAttribute("BAutomatique")]
        [Bindable(true)]
        public bool BAutomatique { get; set; }

        [XmlAttribute("BIndexee")]
        [Bindable(true)]
        public bool BIndexee { get; set; }

        [XmlAttribute("Longueur")]
        [Bindable(true)]
        public int Longueur { get; set; }

        [XmlAttribute("BDebut")]
        [Bindable(true)]
        public bool BDebut { get; set; }

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

        public CodificationArticle(){}

        public void Sauvegarder()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {


                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = transaction.Connection;
                    cmd.Transaction = transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "CodificationArticle_Sauvegarder";
                    cmd.Parameters.AddWithValue("@Code", this.Code);
                    cmd.Parameters.AddWithValue("@Libelle", this.Libelle);
                    cmd.Parameters.AddWithValue("@Composition", this.Composition);
                    cmd.Parameters.AddWithValue("@BAutomatique", this.BAutomatique);
                    cmd.Parameters.AddWithValue("@BIndexee", this.BIndexee);
                    cmd.Parameters.AddWithValue("@BDebut", this.BDebut);
                    cmd.Parameters.AddWithValue("@Longueur", this.Longueur);
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
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "CodificationArticle_Supprimer";
                    cmd.Parameters.Add(new SqlParameter("@Code", Code));
                    cmd.Parameters.Add(new SqlParameter("@Composition", Composition));
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SupprimerTous()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "CodificationArticle_SupprimerTous";
                    cmd.Parameters.Add(new SqlParameter("@Code", Code));
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }

            catch (Exception)
            {
                throw;
            }
        }

        public static CodificationArticle Charger(string code, string composition)
        {
            CodificationArticle codificationArticle = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "CodificationArticle_Charger";
                    cmd.Parameters.AddWithValue("@Code", code);
                    cmd.Parameters.AddWithValue("@Composition", composition);
                    //foreach (SqlParameter parametre in cmd.Parameters)
                    //    if (parametre.Value == null)
                    //        parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            codificationArticle = new CodificationArticle();
                            codificationArticle.Code = dr["Code"].ToString();
                            codificationArticle.Composition = dr["Composition"].ToString();
                            if (dr["Libelle"] != DBNull.Value)
                                codificationArticle.Libelle = dr["Libelle"].ToString();
                            if (dr["BAutomatique"] != DBNull.Value)
                                codificationArticle.BAutomatique = bool.Parse(dr["BAutomatique"].ToString());
                            if (dr["BIndexee"] != DBNull.Value)
                                codificationArticle.BIndexee = bool.Parse(dr["BIndexee"].ToString());
                            if (dr["Ordre"] != DBNull.Value)
                                codificationArticle.Ordre = int.Parse(dr["Ordre"].ToString());
                            if (dr["BDebut"] != DBNull.Value)
                                codificationArticle.BDebut = bool.Parse(dr["BDebut"].ToString());
                            if (dr["Longueur"] != DBNull.Value)
                                codificationArticle.Longueur = int.Parse(dr["Longueur"].ToString());
                        }
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }

            return codificationArticle;
        }

        public static List<CodificationArticle> Charger(string code)    
        {
            List<CodificationArticle> collection = new List<CodificationArticle>();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "CodificationArticle_Charger";
                    cmd.Parameters.AddWithValue("@Code", code);
                    cmd.Parameters.AddWithValue("@Composition", DBNull.Value);
                    //foreach (SqlParameter parametre in cmd.Parameters)
                    //    if (parametre.Value == null)
                    //        parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            CodificationArticle codificationArticle = new CodificationArticle();
                            codificationArticle.Code = dr["Code"].ToString();
                            codificationArticle.Composition = dr["Composition"].ToString();
                            if (dr["Libelle"] != DBNull.Value)
                                codificationArticle.Libelle = dr["Libelle"].ToString();
                            if (dr["BAutomatique"] != DBNull.Value)
                                codificationArticle.BAutomatique = bool.Parse(dr["BAutomatique"].ToString());
                            if (dr["BIndexee"] != DBNull.Value)
                                codificationArticle.BIndexee = bool.Parse(dr["BIndexee"].ToString());
                            if (dr["Ordre"] != DBNull.Value)
                                codificationArticle.Ordre = int.Parse(dr["Ordre"].ToString());
                            if (dr["BDebut"] != DBNull.Value)
                                codificationArticle.BDebut = bool.Parse(dr["BDebut"].ToString());
                            if (dr["Longueur"] != DBNull.Value)
                                codificationArticle.Longueur = int.Parse(dr["Longueur"].ToString());
                            collection.Add(codificationArticle);
                        }
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }

            return collection;
        }

    }

    [Serializable]
    public class CodificationArticleCollection : ItemCollection
    {
        public static CodificationArticleCollection Charger()
        {
            CodificationArticleCollection collection = new CodificationArticleCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "CodificationArticle_ChargerDistinct";
                    

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            CodificationArticle codificationArticle = new CodificationArticle();
                            codificationArticle.Code = dr["Code"].ToString();
                            if (dr["Libelle"] != DBNull.Value)
                                codificationArticle.Libelle = dr["Libelle"].ToString();
                            collection.Add(codificationArticle);
                        }
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }

            return collection;
        }  


    }
}
