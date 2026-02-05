using CST.LePoint.Referentiel;
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
    public class SocieteSiteCollection : List<SocieteSite>
    {
        public static SocieteSiteCollection Charger(string cSociete, string csite, bool? bsiege)
        {
            SocieteSiteCollection collection = new SocieteSiteCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SocieteSite_Charger";
                    cmd.Parameters.AddWithValue("@CSociete", cSociete);
                    cmd.Parameters.AddWithValue("@CSite", csite);
                    cmd.Parameters.AddWithValue("@bsiege", bsiege);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            SocieteSite societeSite = new SocieteSite();

                            societeSite.CSociete = dr["CSociete"].ToString();
                            societeSite.CSite = dr["CSite"].ToString();
                            societeSite.bSiege = bool.Parse(dr["bSiege"].ToString());
                            if (dr["Site"] != DBNull.Value)
                                societeSite.Site = dr["Site"].ToString();
                            if (dr["Ip"] != DBNull.Value)
                                societeSite.Ip = dr["Ip"].ToString();
                            if (dr["Port"] != DBNull.Value)
                                societeSite.Port = int.Parse(dr["Port"].ToString());
                            if (dr["GMTPlus"] != DBNull.Value)
                                societeSite.GMTPlus = int.Parse(dr["GMTPlus"].ToString());
                            if (dr["Latitude"] != DBNull.Value)
                                societeSite.Latitude = decimal.Parse(dr["Latitude"].ToString());
                            if (dr["Longitude"] != DBNull.Value)
                                societeSite.Longitude = decimal.Parse(dr["Longitude"].ToString());
                            if (dr["Rayon"] != DBNull.Value)
                                societeSite.Rayon = int.Parse(dr["Rayon"].ToString());
                            if (dr["Time"] != DBNull.Value)
                                societeSite.Time = int.Parse(dr["Time"].ToString());

                            collection.Add(societeSite);
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
        
        public static SocieteSiteCollection Charger(string cSociete, string csite)
        {
            SocieteSiteCollection collection = new SocieteSiteCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SocieteSite_ChargerTous";
                    cmd.Parameters.AddWithValue("@CSociete", cSociete);
                    cmd.Parameters.AddWithValue("@CSite", csite);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            SocieteSite societeSite = new SocieteSite();

                            societeSite.CSociete = dr["CSociete"].ToString();
                            societeSite.CSite = dr["CSite"].ToString();
                            societeSite.bSiege = bool.Parse(dr["bSiege"].ToString());
                            if (dr["Site"] != DBNull.Value)
                                societeSite.Site = dr["Site"].ToString();
                            if (dr["Ip"] != DBNull.Value)
                                societeSite.Ip = dr["Ip"].ToString();
                            if (dr["Port"] != DBNull.Value)
                                societeSite.Port = int.Parse(dr["Port"].ToString());
                            if (dr["GMTPlus"] != DBNull.Value)
                                societeSite.GMTPlus = int.Parse(dr["GMTPlus"].ToString());
                            if (dr["Latitude"] != DBNull.Value)
                                societeSite.Latitude = decimal.Parse(dr["Latitude"].ToString());
                            if (dr["Longitude"] != DBNull.Value)
                                societeSite.Longitude = decimal.Parse(dr["Longitude"].ToString());
                            if (dr["Rayon"] != DBNull.Value)
                                societeSite.Rayon = int.Parse(dr["Rayon"].ToString());
                            if (dr["Time"] != DBNull.Value)
                                societeSite.Time = int.Parse(dr["Time"].ToString());

                            collection.Add(societeSite);
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

        public static ItemCollection ChargerSociete(string CSociete)
        {
            ItemCollection collection = new ItemCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SocieteSite_Charger_Societe";
                    cmd.Parameters.AddWithValue("@CSociete", CSociete);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Item societeSite = new Item();

                            societeSite.Code = dr["CSite"].ToString();
                            if (dr["Site"] != DBNull.Value)
                                societeSite.Libelle = dr["Site"].ToString();

                            collection.Add(societeSite);
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

        public static DataTable UtilisateurCharger(string CSociete, string CUtilisateur)
        {
            DataTable dtListe = new DataTable();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SocieteSite_Utilisateur_Charger";
                    cmd.Parameters.AddWithValue("@CSociete", CSociete);
                    cmd.Parameters.AddWithValue("@CUtilisateur", CUtilisateur);
                    if (ConfigurationManager.AppSettings["MODE_APPLICATION"].ToString() == "ADMINISTRATION")
                        cmd.Parameters.AddWithValue("@CSite", null);
                    else
                        cmd.Parameters.AddWithValue("@CSite", GestionSession.SocieteSite);


                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;
                    
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dtListe);
                    
                }
            }
            catch (Exception)
            {
                throw;
            }

            return dtListe;
        }
    }

    [Serializable]
    public class SocieteSite
    {
        #region Propriétés

        public string CSociete { get; set; }
        public string CSite { get; set; }
        public string Site { get; set; }
        public bool bSiege { get; set; }
        public string Ip { get; set; }
        public int Port { get; set; }
        public int GMTPlus { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int Rayon { get; set; }
        public int Time { get; set; }
        public DateTime DateInsertion { get; set; }
        public DateTime DateModification { get; set; }
        public int CreePar { get; set; }
        public int ModifiePar { get; set; }
        public string PCInsertion { get; set; }
        public string PCModification { get; set; }

        #endregion Propriétés

        public SocieteSite()
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
                    Sauvegarder(transaction);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void Sauvegarder(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SocieteSite_Sauvegarder";
                cmd.Parameters.AddWithValue("@CSite", CSite);
                cmd.Parameters.AddWithValue("@CSociete", CSociete);
                cmd.Parameters.AddWithValue("@Site", Site);
                cmd.Parameters.AddWithValue("@bSiege", bSiege);
                cmd.Parameters.AddWithValue("@IP", Ip);
                cmd.Parameters.AddWithValue("@Port", Port);
                cmd.Parameters.AddWithValue("@GMTPlus", GMTPlus);
                cmd.Parameters.AddWithValue("@Latitude", Latitude);
                cmd.Parameters.AddWithValue("@Longitude", Longitude);
                cmd.Parameters.AddWithValue("@Rayon", Rayon);
                cmd.Parameters.AddWithValue("@Time", Time);
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

        public void Supprimer()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {

                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    Supprimer(transaction);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
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
                cmd.CommandText = "SocieteSite_Supprimer";
                cmd.Parameters.AddWithValue("@CSociete", CSociete);
                cmd.Parameters.AddWithValue("@CSite", CSite);


                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }

        public static SocieteSite Charger(string cSociete, string csite, bool? bsiege)
        {
            SocieteSite societeSite = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "SocieteSite_Charger";
                    cmd.Parameters.AddWithValue("@CSociete", cSociete);
                    cmd.Parameters.AddWithValue("@CSite", csite);
                    cmd.Parameters.AddWithValue("@bsiege", bsiege);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            societeSite = new SocieteSite();

                            societeSite.CSociete = dr["CSociete"].ToString();
                            societeSite.CSite = dr["CSite"].ToString();
                            societeSite.bSiege = bool.Parse(dr["bSiege"].ToString());
                            if (dr["Site"] != DBNull.Value)
                                societeSite.Site = dr["Site"].ToString();
                            if (dr["Ip"] != DBNull.Value)
                                societeSite.Ip = dr["Ip"].ToString();
                            if (dr["Port"] != DBNull.Value)
                                societeSite.Port = int.Parse(dr["Port"].ToString());
                            if (dr["GMTPlus"] != DBNull.Value)
                                societeSite.GMTPlus = int.Parse(dr["GMTPlus"].ToString());
                            if (dr["Latitude"] != DBNull.Value)
                                societeSite.Latitude = decimal.Parse(dr["Latitude"].ToString());
                            if (dr["Longitude"] != DBNull.Value)
                                societeSite.Longitude = decimal.Parse(dr["Longitude"].ToString());
                            if (dr["Rayon"] != DBNull.Value)
                                societeSite.Rayon = int.Parse(dr["Rayon"].ToString());
                            if (dr["Time"] != DBNull.Value)
                                societeSite.Time = int.Parse(dr["Time"].ToString());
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return societeSite;
        }
    }
}