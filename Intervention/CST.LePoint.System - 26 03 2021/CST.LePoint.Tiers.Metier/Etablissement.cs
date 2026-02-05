using CST.LePoint.Referentiel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace CST.LePoint.Tiers.Metier
{
    [Serializable]
    public class EtablissementCollection : ItemCollection
    {

        public EtablissementCollection()
        {
        }

        public static DataTable RemplirGrid()
        {
            DataTable dt = new DataTable();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Ref_Etablissement_Charger";
                cmd.Parameters.AddWithValue("@Code", DBNull.Value);
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

        public static EtablissementCollection Charger()
        {
            EtablissementCollection collection = new EtablissementCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_Etablissement_Charger";
                    cmd.Parameters.Add(new SqlParameter("@Code", DBNull.Value));

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Etablissement etablissement = new Etablissement();

                            etablissement.Code = dr["Code"].ToString().Trim();
                            if (dr["Libelle"] != DBNull.Value)
                                etablissement.Libelle = dr["Libelle"].ToString().Trim();
                            if (dr["CClient"] != DBNull.Value)
                                etablissement.CClient = dr["CClient"].ToString().Trim();
                            if (dr["CRegion"] != DBNull.Value)
                                etablissement.CRegion = dr["CRegion"].ToString().Trim();
                            if (dr["Adresse"] != DBNull.Value)
                                etablissement.Adresse = dr["Adresse"].ToString().Trim();
                            if (dr["Ville"] != DBNull.Value)
                                etablissement.Ville = dr["Ville"].ToString().Trim();
                            if (dr["CodePostale"] != DBNull.Value)
                                etablissement.CodePostale = dr["CodePostale"].ToString().Trim();
                            if (dr["Latitude"] != DBNull.Value)
                                etablissement.Latitude = decimal.Parse(dr["Latitude"].ToString().Trim());
                            if (dr["Longitude"] != DBNull.Value)
                                etablissement.Longitude = decimal.Parse(dr["Longitude"].ToString().Trim());
                            collection.Add(etablissement);
                        }
                        dr.Close();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return collection;
        }

        public static EtablissementCollection Charger(string cclient)
        {
            EtablissementCollection collection = new EtablissementCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_Etablissement_Client_Charger";
                    cmd.Parameters.Add(new SqlParameter("@CClient", cclient));
                    foreach (SqlParameter sqlParametre in cmd.Parameters)
                        if (sqlParametre.Value == null)
                            sqlParametre.Value = DBNull.Value;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Etablissement etablissement = new Etablissement();

                            etablissement.Code = dr["Code"].ToString().Trim();
                            if (dr["Libelle"] != DBNull.Value)
                                etablissement.Libelle = dr["Libelle"].ToString().Trim();
                            if (dr["CClient"] != DBNull.Value)
                                etablissement.CClient = dr["CClient"].ToString().Trim();
                            if (dr["CRegion"] != DBNull.Value)
                                etablissement.CRegion = dr["CRegion"].ToString().Trim();
                            if (dr["Adresse"] != DBNull.Value)
                                etablissement.Adresse = dr["Adresse"].ToString().Trim();
                            if (dr["Ville"] != DBNull.Value)
                                etablissement.Ville = dr["Ville"].ToString().Trim();
                            if (dr["CodePostale"] != DBNull.Value)
                                etablissement.CodePostale = dr["CodePostale"].ToString().Trim();
                            if (dr["Latitude"] != DBNull.Value)
                                etablissement.Latitude = decimal.Parse(dr["Latitude"].ToString().Trim());
                            if (dr["Longitude"] != DBNull.Value)
                                etablissement.Longitude = decimal.Parse(dr["Longitude"].ToString().Trim());
                            collection.Add(etablissement);
                        }
                        dr.Close();
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
    public class Etablissement : Item
    {
        #region Propriétés


        [XmlAttribute("CClient")]
        [Bindable(true)]
        public string CClient { get; set; }

        [XmlAttribute("CRegion")]
        [Bindable(true)]
        public string CRegion { get; set; }

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

        [XmlAttribute("Adresse")]
        [Bindable(true)]
        public string Adresse { get; set; }

        [XmlAttribute("Ville")]
        [Bindable(true)]
        public string Ville { get; set; }

        [XmlAttribute("CodePostale")]
        [Bindable(true)]
        public string CodePostale { get; set; }

        [XmlAttribute("Latitude")]
        [Bindable(true)]
        public decimal Latitude { get; set; }

        [XmlAttribute("Longitude")]
        [Bindable(true)]
        public decimal Longitude { get; set; }

        #endregion Propriétés

        public Etablissement() { }

        public static Etablissement Charger(string code)
        {
            Etablissement etablissement = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_Etablissement_Charger";
                    cmd.Parameters.Add(new SqlParameter("@Code", code));
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            etablissement = new Etablissement();

                            etablissement.Code = dr["Code"].ToString().Trim();
                            if (dr["Libelle"] != DBNull.Value)
                                etablissement.Libelle = dr["Libelle"].ToString().Trim();
                            if (dr["CClient"] != DBNull.Value)
                                etablissement.CClient = dr["CClient"].ToString().Trim();
                            if (dr["CRegion"] != DBNull.Value)
                                etablissement.CRegion = dr["CRegion"].ToString().Trim();
                            if (dr["Adresse"] != DBNull.Value)
                                etablissement.Adresse = dr["Adresse"].ToString().Trim();
                            if (dr["Ville"] != DBNull.Value)
                                etablissement.Ville = dr["Ville"].ToString().Trim();
                            if (dr["CodePostale"] != DBNull.Value)
                                etablissement.CodePostale = dr["CodePostale"].ToString().Trim();
                            if (dr["Latitude"] != DBNull.Value)
                                etablissement.Latitude = decimal.Parse(dr["Latitude"].ToString().Trim());
                            if (dr["Longitude"] != DBNull.Value)
                                etablissement.Longitude = decimal.Parse(dr["Longitude"].ToString().Trim());
                        }
                        dr.Close();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return etablissement;
        }




        public void Sauvegarder()
        {
            SqlTransaction transaction = null;

            using (var cnx = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                try
                {
                    cnx.Open();
                    transaction = cnx.BeginTransaction();
                    Sauvegarder(transaction);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    if (transaction != null)
                    {
                        transaction.Rollback();
                    }
                }
            }
        }

        public void Sauvegarder(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = transaction.Connection;
                cmd.Transaction = transaction;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Ref_Etablissement_Sauvegarder";

                cmd.Parameters.Add(new SqlParameter("@Code", Code));
                cmd.Parameters.Add(new SqlParameter("@CClient", CClient));
                cmd.Parameters.Add(new SqlParameter("@Libelle", Libelle));
                cmd.Parameters.Add(new SqlParameter("@CRegion", CRegion));
                cmd.Parameters.Add(new SqlParameter("@DateInsertion", DateTime.Now));
                cmd.Parameters.Add(new SqlParameter("@DateModification", DateTime.Now));
                cmd.Parameters.Add(new SqlParameter("@CreePar", CreePar));
                cmd.Parameters.Add(new SqlParameter("@ModifiePar", ModifiePar));
                cmd.Parameters.Add(new SqlParameter("@PCInsertion", PCInsertion));
                cmd.Parameters.Add(new SqlParameter("@PCModification", PCModification));

                cmd.Parameters.Add(new SqlParameter("@Adresse", Adresse));
                cmd.Parameters.Add(new SqlParameter("@Ville", Ville));
                cmd.Parameters.Add(new SqlParameter("@CodePostale", CodePostale));
                cmd.Parameters.Add(new SqlParameter("@Latitude", Latitude));
                cmd.Parameters.Add(new SqlParameter("@Longitude", Longitude));

                foreach (SqlParameter sqlParametre in cmd.Parameters)
                {
                    if (sqlParametre.Value == null)
                    {
                        sqlParametre.Value = DBNull.Value;
                    }
                }
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }
        ////public void Sauvegarder()
        ////{
        ////    try
        ////    {
        ////        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
        ////        {
        ////            cn.Open();

        ////            SqlCommand cmd = cn.CreateCommand();
        ////            cmd.CommandType = CommandType.StoredProcedure;
        ////            cmd.CommandText = "Ref_Etablissement_Sauvegarder";

        ////            cmd.Parameters.Add(new SqlParameter("@Code", Code));
        ////            cmd.Parameters.Add(new SqlParameter("@CClient", CClient));
        ////            cmd.Parameters.Add(new SqlParameter("@Libelle", Libelle));
        ////            cmd.Parameters.Add(new SqlParameter("@CRegion", CRegion));
        ////            cmd.Parameters.Add(new SqlParameter("@DateInsertion", DateTime.Now));
        ////            cmd.Parameters.Add(new SqlParameter("@DateModification", DateTime.Now));
        ////            cmd.Parameters.Add(new SqlParameter("@CreePar", CreePar));
        ////            cmd.Parameters.Add(new SqlParameter("@ModifiePar", ModifiePar));
        ////            cmd.Parameters.Add(new SqlParameter("@PCInsertion", PCInsertion));
        ////            cmd.Parameters.Add(new SqlParameter("@PCModification", PCModification));

        ////            cmd.Parameters.Add(new SqlParameter("@Adresse", Adresse));
        ////            cmd.Parameters.Add(new SqlParameter("@Ville", Ville));
        ////            cmd.Parameters.Add(new SqlParameter("@CodePostale", CodePostale));
        ////            cmd.Parameters.Add(new SqlParameter("@Latitude", Latitude));
        ////            cmd.Parameters.Add(new SqlParameter("@Longitude", Longitude));

        ////            foreach (SqlParameter sqlParametre in cmd.Parameters)
        ////            {
        ////                if (sqlParametre.Value == null)
        ////                {
        ////                    sqlParametre.Value = DBNull.Value;
        ////                }
        ////            }
        ////            cmd.ExecuteNonQuery();
        ////        }
        ////    }
        ////    catch (Exception )
        ////    {
        ////        throw;
        ////    }
        ////}

        public void Supprimer()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_Etablissement_Supprimer";
                    cmd.Parameters.Add(new SqlParameter("@Code", Code));
                    foreach (SqlParameter sqlParametre in cmd.Parameters)
                    {
                        if (sqlParametre.Value == null)
                        {
                            sqlParametre.Value = DBNull.Value;
                        }
                    }
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }




    public class EtablissementColl : List<Etablissement>
    {


        public static EtablissementColl Charger(string cclient)
        {
            EtablissementColl collection = new EtablissementColl();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_Etablissement_Client_Charger";
                    cmd.Parameters.Add(new SqlParameter("@CClient", cclient));
                    foreach (SqlParameter sqlParametre in cmd.Parameters)
                        if (sqlParametre.Value == null)
                            sqlParametre.Value = DBNull.Value;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Etablissement etablissement = new Etablissement();

                            etablissement.Code = dr["Code"].ToString().Trim();
                            if (dr["Libelle"] != DBNull.Value)
                                etablissement.Libelle = dr["Libelle"].ToString().Trim();
                            if (dr["CClient"] != DBNull.Value)
                                etablissement.CClient = dr["CClient"].ToString().Trim();
                            if (dr["CRegion"] != DBNull.Value)
                                etablissement.CRegion = dr["CRegion"].ToString().Trim();
                            if (dr["Adresse"] != DBNull.Value)
                                etablissement.Adresse = dr["Adresse"].ToString().Trim();
                            if (dr["Ville"] != DBNull.Value)
                                etablissement.Ville = dr["Ville"].ToString().Trim();
                            if (dr["CodePostale"] != DBNull.Value)
                                etablissement.CodePostale = dr["CodePostale"].ToString().Trim();
                            if (dr["Latitude"] != DBNull.Value)
                                etablissement.Latitude = decimal.Parse(dr["Latitude"].ToString().Trim());
                            if (dr["Longitude"] != DBNull.Value)
                                etablissement.Longitude = decimal.Parse(dr["Longitude"].ToString().Trim());
                            collection.Add(etablissement);
                        }
                        dr.Close();
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