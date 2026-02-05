using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml.Serialization;

namespace CST.LePoint.Tiers.Referentiel
{
    [Serializable]
    public class BanqueContactCollection : List<BanqueContact>
    {
        public BanqueContactCollection()
        {
        }

        public static DataSet ChargerVue(string cBanque)
        {
            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "RptBanqueContact_Chargerr";
                cmd.Parameters.Add(new SqlParameter("@CBanque", cBanque));
                cmd.Parameters.Add(new SqlParameter("@CContact", DBNull.Value));

                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "RptBanqueContact_Charger");
            }
            return (ds);
        }

        public static BanqueContactCollection Charger(string cBanque)
        {
            BanqueContactCollection collection = new BanqueContactCollection();
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "BanqueContact_Charger";
                    cmd.Parameters.Add(new SqlParameter("@CBanque", cBanque));
                    cmd.Parameters.Add(new SqlParameter("@CContact", DBNull.Value));

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            BanqueContact banqueContact = new BanqueContact();

                            banqueContact = new BanqueContact();
                            banqueContact.CContact = int.Parse(dr["CContact"].ToString().Trim());
                            banqueContact.CBanque = dr["CBanque"].ToString().Trim();
                            if (dr["BPrincipal"] != DBNull.Value)
                                banqueContact.BPrincipal = bool.Parse(dr["BPrincipal"].ToString().Trim());
                            if (dr["CCivilite"] != DBNull.Value)
                                banqueContact.CCivilite = dr["CCivilite"].ToString().Trim();
                            if (dr["Email"] != DBNull.Value)
                                banqueContact.Email = dr["Email"].ToString().Trim();
                            if (dr["Fonction"] != DBNull.Value)
                                banqueContact.Fonction = dr["Fonction"].ToString().Trim();
                            if (dr["Interlocuteur"] != DBNull.Value)
                                banqueContact.Interlocuteur = dr["Interlocuteur"].ToString().Trim();
                            if (dr["Nom"] != DBNull.Value)
                                banqueContact.Nom = dr["Nom"].ToString().Trim();
                            if (dr["Portable"] != DBNull.Value)
                                banqueContact.Portable = dr["Portable"].ToString().Trim();
                            if (dr["Prenom"] != DBNull.Value)
                                banqueContact.Prenom = dr["Prenom"].ToString().Trim();
                            if (dr["Telephone"] != DBNull.Value)
                                banqueContact.Telephone = dr["Telephone"].ToString().Trim();

                            collection.Add(banqueContact);
                        }
                        dr.Close();
                    }
                }
            }
            catch (Exception )
            {
                throw;
            }

            return collection;
        }

        public BanqueContact Obtenir(string cBanque, int cContact)
        {
            BanqueContact BanqueContact = this.Where(x => (x.CBanque.Equals(cBanque)) && (x.CContact.Equals(cContact))).FirstOrDefault();
            return BanqueContact;
        }
    }

    [Serializable]
    public class BanqueContact : ContactBase
    {
        #region Propriétés

        [XmlAttribute("CBanque")]
        [Bindable(true)]
        public string CBanque { get; set; }

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

        public BanqueContact()
        {
            BPrincipal = false;
            CCivilite = string.Empty;
            CBanque = string.Empty;
            CContact = 0;

            Fonction = string.Empty;
            Interlocuteur = string.Empty;
            Nom = string.Empty;
            Prenom = string.Empty;

            Portable = string.Empty;
            Email = string.Empty;
            Telephone = string.Empty;
            //this.DateInsertion = DateTime.Now;
            //this.DateModification = DateTime.Now;
        }

        public static BanqueContact Charger(string cBanque, int cContact)
        {
            BanqueContact banqueContact = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "BanqueContact_Charger";
                    cmd.Parameters.Add(new SqlParameter("@CBanque", cBanque));
                    cmd.Parameters.Add(new SqlParameter("@CContact", cContact));

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            banqueContact = new BanqueContact();
                            banqueContact.CContact = int.Parse(dr["CContact"].ToString().Trim());
                            banqueContact.CBanque = dr["CBanque"].ToString().Trim();
                            if (dr["BPrincipal"] != DBNull.Value)
                                banqueContact.BPrincipal = bool.Parse(dr["BPrincipal"].ToString().Trim());
                            if (dr["CCivilite"] != DBNull.Value)
                                banqueContact.CCivilite = dr["CCivilite"].ToString().Trim();
                            if (dr["Email"] != DBNull.Value)
                                banqueContact.Email = dr["Email"].ToString().Trim();
                            if (dr["Fonction"] != DBNull.Value)
                                banqueContact.Fonction = dr["Fonction"].ToString().Trim();
                            if (dr["Interlocuteur"] != DBNull.Value)
                                banqueContact.Interlocuteur = dr["Interlocuteur"].ToString().Trim();
                            if (dr["Nom"] != DBNull.Value)
                                banqueContact.Nom = dr["Nom"].ToString().Trim();
                            if (dr["Portable"] != DBNull.Value)
                                banqueContact.Portable = dr["Portable"].ToString().Trim();
                            if (dr["Prenom"] != DBNull.Value)
                                banqueContact.Prenom = dr["Prenom"].ToString().Trim();
                            if (dr["Telephone"] != DBNull.Value)
                                banqueContact.Telephone = dr["Telephone"].ToString().Trim();
                        }
                        dr.Close();
                    }
                }
            }
            catch (Exception )
            {
                throw;
            }

            return banqueContact;
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
                catch (Exception )
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
            //Il faut créer la table BanqueContact et ses procédures stockées
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = transaction.Connection;
                cmd.Transaction = transaction;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BanqueContact_Sauvegarder";

                cmd.Parameters.Add(new SqlParameter("@CContact", CContact));
                cmd.Parameters.Add(new SqlParameter("@CBanque", CBanque));
                cmd.Parameters.Add(new SqlParameter("@BPrincipal", BPrincipal));
                cmd.Parameters.Add(new SqlParameter("@Prenom", Prenom));
                cmd.Parameters.Add(new SqlParameter("@Nom", Nom));
                cmd.Parameters.Add(new SqlParameter("@Portable", Portable));
                cmd.Parameters.Add(new SqlParameter("@Email", Email));
                cmd.Parameters.Add(new SqlParameter("@Telephone", Telephone));
                cmd.Parameters.Add(new SqlParameter("@Interlocuteur", Interlocuteur));
                cmd.Parameters.Add(new SqlParameter("@Fonction", Fonction));
                cmd.Parameters.Add(new SqlParameter("@CCivilite", CCivilite));
                cmd.Parameters.Add(new SqlParameter("@CreePar", CreePar));
                cmd.Parameters.Add(new SqlParameter("@ModifiePar", ModifiePar));
                cmd.Parameters.Add(new SqlParameter("@PCInsertion", PCInsertion));
                cmd.Parameters.Add(new SqlParameter("@PCModification", PCModification));
                cmd.Parameters.Add(new SqlParameter("@DateInsertion", DateTime.Now));
                cmd.Parameters.Add(new SqlParameter("@DateModification", DateTime.Now));

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
            catch (Exception )
            {
                throw;
            }
        }

        public void Supprimer()
        {
            SqlTransaction transaction = null;

            using (var cnx = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                try
                {
                    cnx.Open();
                    transaction = cnx.BeginTransaction();

                    Supprimer(transaction);

                    transaction.Commit();
                }
                catch (Exception )
                {
                    if (transaction != null)
                    {
                        transaction.Rollback();
                    }
                }
            }
        }

        public void Supprimer(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = transaction.Connection;
                cmd.Transaction = transaction;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BanqueContact_Supprimer";
                cmd.Parameters.Add(new SqlParameter("@CBanque", CBanque));
                cmd.Parameters.Add(new SqlParameter("@CContact", CContact));
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
            catch (Exception )
            {
                throw;
            }
        }
    }
}