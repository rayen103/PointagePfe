using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace CST.LePoint.Stock.Referentiel.Commun
{
    [Serializable]
    public class ContenantCollection : List<Contenant>
    {
        public static ContenantCollection Charger()
        {
            ContenantCollection contenantCollection = new ContenantCollection();
            Contenant contenant = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_Contenant_Charger";
                    cmd.Parameters.Add(new SqlParameter("@CContenant", DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@CLot", DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@CArticle", DBNull.Value));
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            contenant = new Contenant();
                            if (dr["CContenant"] != DBNull.Value)
                                contenant.CContenant = dr["CContenant"].ToString().Trim();
                            if (dr["CLot"] != DBNull.Value)
                                contenant.CLot = dr["CLot"].ToString().Trim();
                            if (dr["CArticle"] != DBNull.Value)
                                contenant.CArticle = dr["CArticle"].ToString().Trim();
                            if (dr["LibArticle"] != DBNull.Value)
                                contenant.LibArticle = dr["LibArticle"].ToString().Trim();
                            if (dr["Quantite"] != DBNull.Value)
                                contenant.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["QuantiteHistorique"] != DBNull.Value)
                                contenant.QuantiteHistorique = decimal.Parse(dr["QuantiteHistorique"].ToString());
                            if (dr["Indice"] != DBNull.Value)
                                contenant.Indice = int.Parse(dr["Indice"].ToString());
                            if (dr["Statut"] != DBNull.Value)
                                contenant.Statut = dr["Statut"].ToString().Trim();
                            if (dr["CreePar"] != DBNull.Value)
                                contenant.CreePar = int.Parse(dr["CreePar"].ToString());
                            if (dr["ModifiePar"] != DBNull.Value)
                                contenant.ModifiePar = int.Parse(dr["ModifiePar"].ToString());
                            if (dr["PCInsertion"] != DBNull.Value)
                                contenant.PCInsertion = decimal.Parse(dr["PCInsertion"].ToString());
                            if (dr["PCModification"] != DBNull.Value)
                                contenant.PCModification = decimal.Parse(dr["PCModification"].ToString());
                            contenantCollection.Add(contenant);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return contenantCollection;
        }

        public static ContenantCollection Charger(string clot)
        {
            ContenantCollection contenantCollection = new ContenantCollection();
            Contenant contenant = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_Contenant_Charger";
                    cmd.Parameters.Add(new SqlParameter("@CContenant", DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@CLot", clot));
                    cmd.Parameters.Add(new SqlParameter("@CArticle", DBNull.Value));
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            contenant = new Contenant();
                            if (dr["CContenant"] != DBNull.Value)
                                contenant.CContenant = dr["CContenant"].ToString().Trim();
                            if (dr["CLot"] != DBNull.Value)
                                contenant.CLot = dr["CLot"].ToString().Trim();
                            if (dr["CArticle"] != DBNull.Value)
                                contenant.CArticle = dr["CArticle"].ToString().Trim();
                            if (dr["LibArticle"] != DBNull.Value)
                                contenant.LibArticle = dr["LibArticle"].ToString().Trim();
                            if (dr["Quantite"] != DBNull.Value)
                                contenant.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["QuantiteHistorique"] != DBNull.Value)
                                contenant.QuantiteHistorique = decimal.Parse(dr["QuantiteHistorique"].ToString());
                            if (dr["Indice"] != DBNull.Value)
                                contenant.Indice = int.Parse(dr["Indice"].ToString());
                            if (dr["Statut"] != DBNull.Value)
                                contenant.Statut = dr["Statut"].ToString().Trim();
                            if (dr["CreePar"] != DBNull.Value)
                                contenant.CreePar = int.Parse(dr["CreePar"].ToString());
                            if (dr["ModifiePar"] != DBNull.Value)
                                contenant.ModifiePar = int.Parse(dr["ModifiePar"].ToString());
                            if (dr["PCInsertion"] != DBNull.Value)
                                contenant.PCInsertion = decimal.Parse(dr["PCInsertion"].ToString());
                            if (dr["PCModification"] != DBNull.Value)
                                contenant.PCModification = decimal.Parse(dr["PCModification"].ToString());
                            contenantCollection.Add(contenant);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return contenantCollection;
        }

        public static ContenantCollection Charger(string clot, string carticle)
        {
            ContenantCollection contenantCollection = new ContenantCollection();
            Contenant contenant = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_Contenant_Charger";
                    cmd.Parameters.Add(new SqlParameter("@CContenant", DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@CLot", clot));
                    cmd.Parameters.Add(new SqlParameter("@CArticle", carticle));
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            contenant = new Contenant();
                            if (dr["CContenant"] != DBNull.Value)
                                contenant.CContenant = dr["CContenant"].ToString().Trim();
                            if (dr["CLot"] != DBNull.Value)
                                contenant.CLot = dr["CLot"].ToString().Trim();
                            if (dr["CArticle"] != DBNull.Value)
                                contenant.CArticle = dr["CArticle"].ToString().Trim();
                            if (dr["LibArticle"] != DBNull.Value)
                                contenant.LibArticle = dr["LibArticle"].ToString().Trim();
                            if (dr["Quantite"] != DBNull.Value)
                                contenant.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["QuantiteHistorique"] != DBNull.Value)
                                contenant.QuantiteHistorique = decimal.Parse(dr["QuantiteHistorique"].ToString());
                            if (dr["Indice"] != DBNull.Value)
                                contenant.Indice = int.Parse(dr["Indice"].ToString());
                            if (dr["Statut"] != DBNull.Value)
                                contenant.Statut = dr["Statut"].ToString().Trim();
                            if (dr["CreePar"] != DBNull.Value)
                                contenant.CreePar = int.Parse(dr["CreePar"].ToString());
                            if (dr["ModifiePar"] != DBNull.Value)
                                contenant.ModifiePar = int.Parse(dr["ModifiePar"].ToString());
                            if (dr["PCInsertion"] != DBNull.Value)
                                contenant.PCInsertion = decimal.Parse(dr["PCInsertion"].ToString());
                            if (dr["PCModification"] != DBNull.Value)
                                contenant.PCModification = decimal.Parse(dr["PCModification"].ToString());
                            contenantCollection.Add(contenant);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return contenantCollection;
        }
    }

    [Serializable]
    public class Contenant
    {
        #region Proprietés

        [XmlAttribute("CContenant")]
        [Bindable(true)]
        public string CContenant { get; set; }

        [XmlAttribute("CLot")]
        [Bindable(true)]
        public string CLot { get; set; }

        [XmlAttribute("CArticle")]
        [Bindable(true)]
        public string CArticle { get; set; }

        [XmlAttribute("LibArticle")]
        [Bindable(true)]
        public string LibArticle { get; set; }

        [XmlAttribute("Quantite")]
        [Bindable(true)]
        public decimal Quantite { get; set; }

        [XmlAttribute("QuantiteHistorique")]
        [Bindable(true)]
        public decimal QuantiteHistorique { get; set; }

        [XmlAttribute("Statut")]
        [Bindable(true)]
        public string Statut { get; set; }

        [XmlAttribute("Indice")]
        [Bindable(true)]
        public int Indice { get; set; }

        [XmlAttribute("CreePar")]
        [Bindable(true)]
        public int CreePar { get; set; }

        [XmlAttribute("ModifiePar")]
        [Bindable(true)]
        public int ModifiePar { get; set; }

        [XmlAttribute("PCInsertion")]
        [Bindable(true)]
        public decimal PCInsertion { get; set; }

        [XmlAttribute("PCModification")]
        [Bindable(true)]
        public decimal PCModification { get; set; }

        #endregion Proprietés

        public Contenant()
        {
        }

        public static Contenant Charger(string ccontenant, string clot, string carticle)
        {
            Contenant contenant = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_Contenant_Charger";
                    cmd.Parameters.Add(new SqlParameter("@CContenant", ccontenant));
                    cmd.Parameters.Add(new SqlParameter("@CLot", clot));
                    cmd.Parameters.Add(new SqlParameter("@CArticle", carticle));
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            contenant = new Contenant();
                            if (dr["CContenant"] != DBNull.Value)
                                contenant.CContenant = dr["CContenant"].ToString().Trim();
                            if (dr["CLot"] != DBNull.Value)
                                contenant.CLot = dr["CLot"].ToString().Trim();
                            if (dr["CArticle"] != DBNull.Value)
                                contenant.CArticle = dr["CArticle"].ToString().Trim();
                            if (dr["LibArticle"] != DBNull.Value)
                                contenant.LibArticle = dr["LibArticle"].ToString().Trim();
                            if (dr["Quantite"] != DBNull.Value)
                                contenant.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["QuantiteHistorique"] != DBNull.Value)
                                contenant.QuantiteHistorique = decimal.Parse(dr["QuantiteHistorique"].ToString());
                            if (dr["Indice"] != DBNull.Value)
                                contenant.Indice = int.Parse(dr["Indice"].ToString());
                            if (dr["Statut"] != DBNull.Value)
                                contenant.Statut = dr["Statut"].ToString().Trim();
                            if (dr["CreePar"] != DBNull.Value)
                                contenant.CreePar = int.Parse(dr["CreePar"].ToString());
                            if (dr["ModifiePar"] != DBNull.Value)
                                contenant.ModifiePar = int.Parse(dr["ModifiePar"].ToString());
                            if (dr["PCInsertion"] != DBNull.Value)
                                contenant.PCInsertion = decimal.Parse(dr["PCInsertion"].ToString());
                            if (dr["PCModification"] != DBNull.Value)
                                contenant.PCModification = decimal.Parse(dr["PCModification"].ToString());
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return contenant;
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

                    cmd.CommandText = "Ref_Contenant_Sauvegarder";
                    cmd.Parameters.AddWithValue("@CContenant", CContenant);
                    cmd.Parameters.AddWithValue("@CLot", CLot);
                    cmd.Parameters.AddWithValue("@CArticle", CArticle);
                    cmd.Parameters.AddWithValue("@LibArticle", LibArticle);
                    cmd.Parameters.AddWithValue("@Quantite", Quantite);
                    cmd.Parameters.AddWithValue("@QuantiteHistorique", QuantiteHistorique);
                    cmd.Parameters.AddWithValue("@Statut", Statut);
                    cmd.Parameters.AddWithValue("@Indice", Indice);
                    cmd.Parameters.AddWithValue("@CreePar", CreePar);
                    cmd.Parameters.AddWithValue("@ModifiePar", ModifiePar);
                    cmd.Parameters.AddWithValue("@PCInsertion", PCInsertion);
                    cmd.Parameters.AddWithValue("@PCModification", PCModification);
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

                    cmd.CommandText = "Ref_Contenant_Supprimer";
                    cmd.Parameters.AddWithValue("@CContenant", CContenant);
                    cmd.Parameters.AddWithValue("@CLot", CLot);
                    cmd.Parameters.AddWithValue("@CArticle", CArticle);
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