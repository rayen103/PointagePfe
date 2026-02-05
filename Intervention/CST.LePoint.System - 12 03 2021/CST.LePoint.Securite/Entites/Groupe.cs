using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace CST.LePoint.Securite.Entites
{
    [Serializable]
    public class GroupeCollection : List<Groupe>
    {
        public static GroupeCollection Charger()
        {
            GroupeCollection collection = new GroupeCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Groupe_Charger";
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Groupe groupe = new Groupe();

                            groupe.CSociete = dr["CSociete"].ToString();
                            groupe.CGroupe = dr["CGroupe"].ToString();
                            if (dr["CTypeGroupe"] != DBNull.Value)
                                groupe.CTypeGroupe = dr["Adresse"].ToString();
                            if (dr["LibGroupe"] != DBNull.Value)
                                groupe.LibGroupe = dr["Agence"].ToString();

                            collection.Add(groupe);
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
    public class Groupe
    {
        #region Propriétés

        [XmlAttribute("CSociete")]
        [Bindable(true)]
        public string CSociete { get; set; }

        [XmlAttribute("CGroupe")]
        [Bindable(true)]
        public string CGroupe { get; set; }

        [XmlAttribute("CTypeGroupe")]
        [Bindable(true)]
        public string CTypeGroupe { get; set; }

        [XmlAttribute("LibGroupe")]
        [Bindable(true)]
        public string LibGroupe { get; set; }

        #endregion Propriétés

        public Groupe()
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
                    cmd.CommandText = "Groupe_Sauvegarder";
                    cmd.Parameters.AddWithValue("@CSociete", CSociete);
                    cmd.Parameters.AddWithValue("@CGroupe", CGroupe);
                    cmd.Parameters.AddWithValue("@CTypeGroupe", CTypeGroupe);
                    cmd.Parameters.AddWithValue("@LibGroupe", LibGroupe);

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
                    cmd.CommandText = "Groupe_Supprimer";
                    cmd.Parameters.AddWithValue("@CSociete", CSociete);
                    cmd.Parameters.AddWithValue("@CGroupe", CGroupe);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public static Groupe Charger(string cSociete, string cGroupe)
        {
            Groupe groupe = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "Groupe_Charger";
                    cmd.Parameters.AddWithValue("@CSociete", cSociete);
                    cmd.Parameters.AddWithValue("@CGroupe", cGroupe);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            groupe = new Groupe();

                            groupe.CSociete = dr["CSociete"].ToString();
                            groupe.CGroupe = dr["CGroupe"].ToString();
                            if (dr["CTypeGroupe"] != DBNull.Value)
                                groupe.CTypeGroupe = dr["Adresse"].ToString();
                            if (dr["LibGroupe"] != DBNull.Value)
                                groupe.LibGroupe = dr["Agence"].ToString();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return groupe;
        }
    }
}