using CST.LePoint.Tiers.Referentiel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml.Serialization;

namespace CST.LePoint.Tiers.Metier
{
    [Serializable]
    public class ClientContactCollection : List<ClientContact>
    {
        public ClientContactCollection()
        {
        }

        public static DataSet ChargerVue(string cClient)
        {
            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "RptClientContact_Charger";
                cmd.Parameters.Add(new SqlParameter("@CClient", cClient));
                cmd.Parameters.Add(new SqlParameter("@CContact", DBNull.Value));

                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "RptClientContact_Charger");
            }
            return (ds);
        }

        public static ClientContactCollection Charger(string cClient)
        {
            ClientContactCollection collection = new ClientContactCollection();
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "ClientContact_Charger";
                    cmd.Parameters.Add(new SqlParameter("@CClient", cClient));
                    cmd.Parameters.Add(new SqlParameter("@CContact", DBNull.Value));
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ClientContact clientContact = new ClientContact();

                            clientContact.CContact = int.Parse(dr["CContact"].ToString().Trim());
                            clientContact.CClient = dr["CClient"].ToString().Trim();
                            clientContact.BPrincipal = bool.Parse(dr["BPrincipal"].ToString().Trim());
                            clientContact.CCivilite = dr["CCivilite"].ToString().Trim();
                            clientContact.Fonction = dr["Fonction"].ToString().Trim();
                            clientContact.Interlocuteur = dr["Interlocuteur"].ToString().Trim();
                            clientContact.Nom = dr["Nom"].ToString().Trim();
                            clientContact.Prenom = dr["Prenom"].ToString().Trim();

                            clientContact.Email = dr["Email"].ToString().Trim();
                            clientContact.Portable = dr["Portable"].ToString().Trim();
                            clientContact.Telephone = dr["Telephone"].ToString().Trim();

                            collection.Add(clientContact);
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

        public ClientContact Obtenir(string cClient, int cContact)
        {
            ClientContact clientContact = this.Where(x => (x.CClient.Equals(cClient)) && (x.CContact.Equals(cContact))).FirstOrDefault();
            return clientContact;
        }
    }

    [Serializable]
    public class ClientContact : ContactBase
    {
        #region Propriétés

        [XmlAttribute("CClient")]
        [Bindable(true)]
        public string CClient { get; set; }

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

        public ClientContact()
        {
            BPrincipal = false;
            CCivilite = string.Empty;
            CClient = string.Empty;
            CContact = 0;

            Fonction = string.Empty;
            Interlocuteur = string.Empty;
            Nom = string.Empty;
            Prenom = string.Empty;

            Portable = string.Empty;
            Email = string.Empty;
            Telephone = string.Empty;
        }

        public static ClientContact Charger(string cClient, int cContact)
        {
            ClientContact clientContact = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "ClientContact_Charger";
                    cmd.Parameters.Add(new SqlParameter("@CClient", cClient));
                    cmd.Parameters.Add(new SqlParameter("@CContact", cContact));

                    foreach (SqlParameter sqlParametre in cmd.Parameters)
                        if (sqlParametre.Value == null)
                            sqlParametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            clientContact = new ClientContact();
                            clientContact.CContact = int.Parse(dr["CContact"].ToString().Trim());
                            clientContact.CClient = dr["CClient"].ToString().Trim();
                            if (dr["BPrincipal"] != DBNull.Value)
                                clientContact.BPrincipal = bool.Parse(dr["BPrincipal"].ToString().Trim());
                            if (dr["CCivilite"] != DBNull.Value)
                                clientContact.CCivilite = dr["CCivilite"].ToString().Trim();
                            if (dr["Email"] != DBNull.Value)
                                clientContact.Email = dr["Email"].ToString().Trim();
                            if (dr["Fonction"] != DBNull.Value)
                                clientContact.Fonction = dr["Fonction"].ToString().Trim();
                            if (dr["Interlocuteur"] != DBNull.Value)
                                clientContact.Interlocuteur = dr["Interlocuteur"].ToString().Trim();
                            if (dr["Nom"] != DBNull.Value)
                                clientContact.Nom = dr["Nom"].ToString().Trim();
                            if (dr["Portable"] != DBNull.Value)
                                clientContact.Portable = dr["Portable"].ToString().Trim();
                            if (dr["Prenom"] != DBNull.Value)
                                clientContact.Prenom = dr["Prenom"].ToString().Trim();
                            if (dr["Telephone"] != DBNull.Value)
                                clientContact.Telephone = dr["Telephone"].ToString().Trim();
                        }
                        dr.Close();
                    }
                }
            }
            catch (Exception )
            {
                throw;
            }

            return clientContact;
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
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = transaction.Connection;
                cmd.Transaction = transaction;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ClientContact_Sauvegarder";

                cmd.Parameters.Add(new SqlParameter("@CContact", CContact));
                cmd.Parameters.Add(new SqlParameter("@CClient", CClient));
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

            using (SqlConnection cnx = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
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
                cmd.CommandText = "ClientContact_Supprimer";
                cmd.Parameters.Add(new SqlParameter("@CClient", CClient));
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