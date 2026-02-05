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
    public class TypeGroupeUtilisateurCollection : List<TypeGroupeUtilisateur>
    {
        public static TypeGroupeUtilisateurCollection Charger()
        {
            TypeGroupeUtilisateurCollection collection = new TypeGroupeUtilisateurCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "TypeGroupe_Charger";
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            TypeGroupeUtilisateur typeGroupe = new TypeGroupeUtilisateur();

                            typeGroupe.CTypeGroupe = dr["CTypeGroupe"].ToString();
                            typeGroupe.LibTypeGroupe = dr["LibTypeGroupe"].ToString();

                            collection.Add(typeGroupe);
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
    public class TypeGroupeUtilisateur
    {
        #region Propriétés

        [XmlAttribute("CTypeGroupe")]
        [Bindable(true)]
        public string CTypeGroupe { get; set; }

        [XmlAttribute("LibTypeGroupe")]
        [Bindable(true)]
        public string LibTypeGroupe { get; set; }

        #endregion Propriétés

        public TypeGroupeUtilisateur()
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
                    cmd.CommandText = "TypeGroupe_Sauvegarder";
                    cmd.Parameters.AddWithValue("@CTypeGroupe", CTypeGroupe);
                    cmd.Parameters.AddWithValue("@LibTypeGroupe", LibTypeGroupe);

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
                    cmd.CommandText = "TypeGroupe_Supprimer";
                    cmd.Parameters.AddWithValue("@CTypeGroupe", CTypeGroupe);

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

        public static TypeGroupeUtilisateur Charger(string cTypeGroupe)
        {
            TypeGroupeUtilisateur typeGroupe = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "TypeGroupe_Charger";
                    cmd.Parameters.AddWithValue("@CTypeGroupe", cTypeGroupe);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            typeGroupe = new TypeGroupeUtilisateur();

                            typeGroupe.CTypeGroupe = dr["CTypeGroupe"].ToString();
                            typeGroupe.LibTypeGroupe = dr["LibTypeGroupe"].ToString();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return typeGroupe;
        }
    }
}