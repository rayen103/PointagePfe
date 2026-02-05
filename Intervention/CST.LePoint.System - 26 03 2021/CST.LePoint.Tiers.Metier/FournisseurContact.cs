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
    public class FournisseurContactCollection : List<FournisseurContact>
    {
        public static DataSet ChargerVue(string cFournisseur)
        {
            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "RptFournisseurContact_Charger";
                cmd.Parameters.Add(new SqlParameter("@CFournisseur", cFournisseur));
                cmd.Parameters.Add(new SqlParameter("@CContact", DBNull.Value));

                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "RptFournisseurContact_Charger");
            }
            return (ds);
        }

        public static FournisseurContactCollection Charger(string cFournisseur)
        {
            FournisseurContactCollection collection = new FournisseurContactCollection();
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "FournisseurContact_Charger";
                    cmd.Parameters.Add(new SqlParameter("@CFournisseur", cFournisseur));
                    cmd.Parameters.Add(new SqlParameter("@CContact", DBNull.Value));
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            FournisseurContact FournisseurContact = new FournisseurContact();
                            FournisseurContact.CContact = int.Parse(dr["CContact"].ToString().Trim());
                            FournisseurContact.CFournisseur = dr["CFournisseur"].ToString().Trim();
                            if (dr["BPrincipal"] != DBNull.Value)
                                FournisseurContact.BPrincipal = bool.Parse(dr["BPrincipal"].ToString().Trim());
                            if (dr["CCivilite"] != DBNull.Value)
                                FournisseurContact.CCivilite = dr["CCivilite"].ToString().Trim();
                            if (dr["Fonction"] != DBNull.Value)
                                FournisseurContact.Fonction = dr["Fonction"].ToString().Trim();
                            if (dr["Interlocuteur"] != DBNull.Value)
                                FournisseurContact.Interlocuteur = dr["Interlocuteur"].ToString().Trim();
                            if (dr["Nom"] != DBNull.Value)
                                FournisseurContact.Nom = dr["Nom"].ToString().Trim();
                            if (dr["Prenom"] != DBNull.Value)
                                FournisseurContact.Prenom = dr["Prenom"].ToString().Trim();
                            if (dr["Email"] != DBNull.Value)
                                FournisseurContact.Email = dr["Email"].ToString().Trim();
                            if (dr["Portable"] != DBNull.Value)
                                FournisseurContact.Portable = dr["Portable"].ToString().Trim();
                            if (dr["Telephone"] != DBNull.Value)
                                FournisseurContact.Telephone = dr["Telephone"].ToString().Trim();

                            collection.Add(FournisseurContact);
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

        public FournisseurContact Obtenir(string cFournisseur, int cContact)
        {
            FournisseurContact FournisseurContact = this.Where(x => (x.CFournisseur.Equals(cFournisseur)) && (x.CContact.Equals(cContact))).FirstOrDefault();
            return FournisseurContact;
        }
    }

    [Serializable]
    public class FournisseurContact : ContactBase
    {
        #region Propriétés

        [XmlAttribute("CFournisseur")]
        [Bindable(true)]
        public string CFournisseur { get; set; }

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

        public FournisseurContact()
        {
            BPrincipal = false;
            CCivilite = string.Empty;
            CFournisseur = string.Empty;
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

        public static FournisseurContact Charger(string cFournisseur, int cContact)
        {
            FournisseurContact FournisseurContact = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "FournisseurContact_Charger";
                    cmd.Parameters.Add(new SqlParameter("@CFournisseur", cFournisseur));
                    cmd.Parameters.Add(new SqlParameter("@CContact", cContact));

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            FournisseurContact = new FournisseurContact();

                            FournisseurContact.CContact = int.Parse(dr["CContact"].ToString().Trim());
                            FournisseurContact.CFournisseur = dr["CFournisseur"].ToString().Trim();
                            if (dr["BPrincipal"] != DBNull.Value)
                                FournisseurContact.BPrincipal = bool.Parse(dr["BPrincipal"].ToString().Trim());
                            if (dr["CCivilite"] != DBNull.Value)
                                FournisseurContact.CCivilite = dr["CCivilite"].ToString().Trim();
                            if (dr["Fonction"] != DBNull.Value)
                                FournisseurContact.Fonction = dr["Fonction"].ToString().Trim();
                            if (dr["Interlocuteur"] != DBNull.Value)
                                FournisseurContact.Interlocuteur = dr["Interlocuteur"].ToString().Trim();
                            if (dr["Nom"] != DBNull.Value)
                                FournisseurContact.Nom = dr["Nom"].ToString().Trim();
                            if (dr["Prenom"] != DBNull.Value)
                                FournisseurContact.Prenom = dr["Prenom"].ToString().Trim();
                            if (dr["Email"] != DBNull.Value)
                                FournisseurContact.Email = dr["Email"].ToString().Trim();
                            if (dr["Portable"] != DBNull.Value)
                                FournisseurContact.Portable = dr["Portable"].ToString().Trim();
                            if (dr["Telephone"] != DBNull.Value)
                                FournisseurContact.Telephone = dr["Telephone"].ToString().Trim();
                        }
                        dr.Close();
                    }
                }
            }
            catch (Exception )
            {
                throw;
            }

            return FournisseurContact;
        }

        public void Sauvegarder()
        {
            SqlTransaction transaction = null;

            using (SqlConnection cnx = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
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
                cmd.CommandText = "FournisseurContact_Sauvegarder";

                cmd.Parameters.Add(new SqlParameter("@CContact", CContact));
                cmd.Parameters.Add(new SqlParameter("@CFournisseur", CFournisseur));
                cmd.Parameters.Add(new SqlParameter("@BPrincipal", BPrincipal));
                cmd.Parameters.Add(new SqlParameter("@Prenom", Prenom));
                cmd.Parameters.Add(new SqlParameter("@Nom", Nom));
                cmd.Parameters.Add(new SqlParameter("@Portable", Portable));
                cmd.Parameters.Add(new SqlParameter("@Email", Email));
                cmd.Parameters.Add(new SqlParameter("@Telephone", Telephone));

                cmd.Parameters.Add(new SqlParameter("@Interlocuteur", Interlocuteur));
                cmd.Parameters.Add(new SqlParameter("@Fonction", Fonction));

                cmd.Parameters.Add(new SqlParameter("@CCivilite", CCivilite));

                cmd.Parameters.Add(new SqlParameter("@DateInsertion", DateTime.Now));
                cmd.Parameters.Add(new SqlParameter("@DateModification", DateTime.Now));
                cmd.Parameters.Add(new SqlParameter("@CreePar", CreePar));
                cmd.Parameters.Add(new SqlParameter("@ModifiePar", ModifiePar));
                cmd.Parameters.Add(new SqlParameter("@PCInsertion", PCInsertion));
                cmd.Parameters.Add(new SqlParameter("@PCModification", PCModification));

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
                cmd.CommandText = "FournisseurContact_Supprimer";
                cmd.Parameters.Add(new SqlParameter("@CFournisseur", CFournisseur));
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