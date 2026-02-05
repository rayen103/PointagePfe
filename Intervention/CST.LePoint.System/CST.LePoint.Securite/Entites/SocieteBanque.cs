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
    public class SocieteBanqueCollection : List<SocieteBanque>
    {
        public static SocieteBanqueCollection Charger()
        {
            SocieteBanqueCollection collection = new SocieteBanqueCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SocieteBanque_Charger";
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            SocieteBanque SocieteBanque = new SocieteBanque();
                            SocieteBanque.CSociete = dr["CSociete"].ToString();
                            SocieteBanque.CBanque = dr["CBanque"].ToString();
                            SocieteBanque.CompteCourant = dr["CompteCourant"].ToString();
                            if (dr["Agence"] != DBNull.Value)
                                SocieteBanque.Agence = dr["Agence"].ToString();
                            if (dr["CompteComptable"] != DBNull.Value)
                                SocieteBanque.CompteComptable = dr["CompteComptable"].ToString();
                            if (dr["BParDefautRib"] != DBNull.Value)
                                SocieteBanque.BParDefautRib = bool.Parse(dr["BParDefautRib"].ToString());
                            if (dr["NumeroJournal"] != DBNull.Value)
                                SocieteBanque.NumeroJournal = dr["NumeroJournal"].ToString();

                            collection.Add(SocieteBanque);
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

        public static SocieteBanqueCollection Charger(string cSociete)
        {
            SocieteBanqueCollection collection = new SocieteBanqueCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SocieteBanque_Charger";
                    cmd.Parameters.AddWithValue("@CSociete", cSociete);
                    cmd.Parameters.AddWithValue("@CBanque", DBNull.Value);
                    cmd.Parameters.AddWithValue("@RIB", DBNull.Value);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            SocieteBanque SocieteBanque = new SocieteBanque();
                            SocieteBanque.CSociete = dr["CSociete"].ToString();
                            SocieteBanque.CBanque = dr["CBanque"].ToString();
                            SocieteBanque.RIB = dr["RIB"].ToString();
                            if (dr["Agence"] != DBNull.Value)
                                SocieteBanque.Agence = dr["Agence"].ToString();
                            if (dr["CompteComptable"] != DBNull.Value)
                                SocieteBanque.CompteComptable = dr["CompteComptable"].ToString();
                            if (dr["BParDefautRib"] != DBNull.Value)
                                SocieteBanque.BParDefautRib = bool.Parse(dr["BParDefautRib"].ToString());
                            if (dr["NumeroJournal"] != DBNull.Value)
                                SocieteBanque.NumeroJournal = dr["NumeroJournal"].ToString();

                            collection.Add(SocieteBanque);
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
    public class SocieteBanque
    {
        #region Propriétés

        [XmlAttribute("CSociete")]
        [Bindable(true)]
        public string CSociete { get; set; }

        [XmlAttribute("CBanque")]
        [Bindable(true)]
        public string CBanque { get; set; }

        public string RIB { get; set; }
        [XmlAttribute("CompteCourant")]
        [Bindable(true)]
        public string CompteCourant { get; set; }

        [XmlAttribute("Agence")]
        [Bindable(true)]
        public string Agence { get; set; }

        [XmlAttribute("CompteComptable")]
        [Bindable(true)]
        public string CompteComptable { get; set; }

        [XmlAttribute("BParDefautRib")]
        [Bindable(true)]
        public bool BParDefautRib { get; set; }

        [XmlAttribute("NumeroJournal")]
        [Bindable(true)]
        public string NumeroJournal { get; set; }

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

        public SocieteBanque()
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
                    cmd.CommandText = "SocieteBanque_Sauvegarder";
                    cmd.Parameters.AddWithValue("@CBanque", CBanque);
                    cmd.Parameters.AddWithValue("@CSociete", CSociete);
                    cmd.Parameters.AddWithValue("@CompteCourant", CompteCourant);
                    cmd.Parameters.AddWithValue("@Agence", Agence);
                    cmd.Parameters.AddWithValue("@CompteComptable", CompteComptable);
                    cmd.Parameters.AddWithValue("@BParDefautRib", BParDefautRib);
                    cmd.Parameters.AddWithValue("@NumeroJournal", NumeroJournal);

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

        public void Sauvegarder(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SocieteBanque_Sauvegarder";
                cmd.Parameters.AddWithValue("@CBanque", CBanque);
                cmd.Parameters.AddWithValue("@CSociete", CSociete);
                cmd.Parameters.AddWithValue("@RIB", RIB);
                cmd.Parameters.AddWithValue("@Agence", Agence);
                cmd.Parameters.AddWithValue("@CompteComptable", CompteComptable);
                cmd.Parameters.AddWithValue("@BParDefautRib", BParDefautRib);
                cmd.Parameters.AddWithValue("@NumeroJournal", NumeroJournal);
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
                    SqlCommand cmd = new SqlCommand();
                    cmd.Transaction = transaction;
                    cmd.Connection = transaction.Connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SocieteBanque_Supprimer";
                    cmd.Parameters.AddWithValue("@CSociete", CSociete);
                    cmd.Parameters.AddWithValue("@CBanque", CBanque);
                    cmd.Parameters.AddWithValue("@CompteCourant", CompteCourant);

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

        public void Supprimer(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SocieteBanque_Supprimer";
                cmd.Parameters.AddWithValue("@CSociete", CSociete);
                cmd.Parameters.AddWithValue("@CBanque", CBanque);
                cmd.Parameters.AddWithValue("@RIB", RIB);

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

        public static SocieteBanque Charger(string cSociete, string cBanque, string compteCourant)
        {
            SocieteBanque SocieteBanque = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "SocieteBanque_Charger";
                    cmd.Parameters.AddWithValue("@CSociete", cSociete);
                    cmd.Parameters.AddWithValue("@CBanque", cBanque);
                    cmd.Parameters.AddWithValue("@CompteCourant", compteCourant);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            SocieteBanque = new SocieteBanque();

                            SocieteBanque.CSociete = dr["CSociete"].ToString();
                            SocieteBanque.CBanque = dr["CBanque"].ToString();
                            SocieteBanque.CompteCourant = dr["CompteCourant"].ToString();
                            if (dr["Agence"] != DBNull.Value)
                                SocieteBanque.Agence = dr["Agence"].ToString();
                            if (dr["CompteComptable"] != DBNull.Value)
                                SocieteBanque.CompteComptable = dr["CompteComptable"].ToString();
                            if (dr["BParDefautRib"] != DBNull.Value)
                                SocieteBanque.BParDefautRib = bool.Parse(dr["BParDefautRib"].ToString());
                            if (dr["NumeroJournal"] != DBNull.Value)
                                SocieteBanque.NumeroJournal = dr["NumeroJournal"].ToString();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return SocieteBanque;
        }

        public static SocieteBanque Charger(string cSociete, string cBanque)
        {
            SocieteBanque SocieteBanque = null;
            if (string.IsNullOrWhiteSpace(cBanque))
                return SocieteBanque;
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "SocieteBanque_Rechercher";
                    cmd.Parameters.AddWithValue("@CSociete", cSociete);
                    cmd.Parameters.AddWithValue("@CBanque", cBanque);
             

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            SocieteBanque = new SocieteBanque();

                            SocieteBanque.CSociete = dr["CSociete"].ToString();
                            SocieteBanque.CBanque = dr["CBanque"].ToString();
                            SocieteBanque.CompteCourant = dr["CompteCourant"].ToString();
                            if (dr["Agence"] != DBNull.Value)
                                SocieteBanque.Agence = dr["Agence"].ToString();
                            if (dr["CompteComptable"] != DBNull.Value)
                                SocieteBanque.CompteComptable = dr["CompteComptable"].ToString();
                            if (dr["BParDefautRib"] != DBNull.Value)
                                SocieteBanque.BParDefautRib = bool.Parse(dr["BParDefautRib"].ToString());
                            if (dr["NumeroJournal"] != DBNull.Value)
                                SocieteBanque.NumeroJournal = dr["NumeroJournal"].ToString();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return SocieteBanque;
        }
    }
}