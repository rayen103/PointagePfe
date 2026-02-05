using CST.LePoint.Referentiel;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace CST.LePoint.Tiers.Referentiel
{
    [Serializable]
    public class BanqueCollection : ItemCollection
    {
        public BanqueCollection()
        {
        }

        public static DataSet ChargerVue()
        {
            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Banque_Rpt_Charger";
                cmd.Parameters.AddWithValue("@CBanque", DBNull.Value);
                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "Banque_Rpt_Charger");
            }
            return (ds);
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
                cmd.CommandText = "Ref_Banque_Charger";
                cmd.Parameters.AddWithValue("@CBanque", DBNull.Value);
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

        public static BanqueCollection Charger()
        {
            Banque banque = null;
            BanqueCollection collection = new BanqueCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_Banque_Charger";
                    cmd.Parameters.Add(new SqlParameter("@CBanque", DBNull.Value));

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            banque = new Banque();

                            banque.Code = dr["CBanque"].ToString().Trim();
                            if (dr["LibBanque"] != DBNull.Value)
                                banque.Libelle = dr["LibBanque"].ToString().Trim();


                            collection.Add(banque);
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

        public static DataTable RecupererBanque()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connexion = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    connexion.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = connexion;
                    cmd.CommandText = "Ref_Banque_Charger";
                    cmd.Parameters.Add(new SqlParameter("@CBanque", DBNull.Value));
                    var Adapter = new SqlDataAdapter(cmd);
                    Adapter.Fill(dt);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }

        public static BanqueCollection ChargerTout()
        {
            Banque banque = null;
            BanqueCollection collection = new BanqueCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_BanqueScan_ChargerTout";
                    cmd.Parameters.Add(new SqlParameter("@CBanque", DBNull.Value));

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            banque = new Banque();

                            banque.Code = dr["CBanque"].ToString().Trim();
                            if (dr["LibBanque"] != DBNull.Value)
                                banque.Libelle = dr["LibBanque"].ToString().Trim();


                            collection.Add(banque);
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
    public class Banque : Item
    {
        #region Propriétés

        [XmlAttribute("LibAdresse")]
        [Bindable(true)]
        public string LibAdresse { get; set; }

        [XmlAttribute("Ville")]
        [Bindable(true)]
        public string Ville { get; set; }

        [XmlAttribute("CPostal")]
        [Bindable(true)]
        public string CPostal { get; set; }

        [XmlAttribute("CPays")]
        [Bindable(true)]
        public string CPays { get; set; }

        [XmlAttribute("IBAN")]
        [Bindable(true)]
        public string IBAN { get; set; }

        [XmlAttribute("CodeBic")]
        [Bindable(true)]
        public string CodeBic { get; set; }

        [XmlAttribute("Initiale")]
        [Bindable(true)]
        public string Initiale { get; set; }

        [XmlAttribute("Adresses")]
        [Bindable(true)]
        public AdresseCollection Adresses { get; set; }

        [XmlAttribute("Contacts")]
        [Bindable(true)]
        public BanqueContactCollection Contacts { get; set; }

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

        public Banque()
        {
            this.Code = string.Empty;
            this.Libelle = string.Empty;
            CPays = string.Empty;
            CPostal = string.Empty;
            LibAdresse = string.Empty;
            Ville = string.Empty;
            Adresses = new AdresseCollection();
            Contacts = new BanqueContactCollection();
        }

        public static Banque Charger(string cBanque)
        {
            Banque banque = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_Banque_Charger";
                    cmd.Parameters.Add(new SqlParameter("@CBanque", cBanque));

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            banque = new Banque();
                            banque.Code = dr["CBanque"].ToString().Trim();
                            banque.Libelle = dr["LibBanque"].ToString().Trim();

                            if (dr["CPays"] != DBNull.Value)
                                banque.CPays = dr["CPays"].ToString().Trim();
                            if (dr["CPostal"] != DBNull.Value)
                                banque.CPostal = dr["CPostal"].ToString().Trim();
                            if (dr["Ville"] != DBNull.Value)
                                banque.Ville = dr["Ville"].ToString().Trim();
                            if (dr["LibAdresse"] != DBNull.Value)
                                banque.LibAdresse = dr["LibAdresse"].ToString().Trim();
                            if (dr["IBAN"] != DBNull.Value)
                                banque.IBAN = dr["IBAN"].ToString().Trim();
                            if (dr["CodeBic"] != DBNull.Value)
                                banque.CodeBic = dr["CodeBic"].ToString().Trim();
                            if (dr["Initiale"] != DBNull.Value)
                                banque.Initiale = dr["Initiale"].ToString().Trim();

                            banque.Adresses = AdresseCollection.Charger(banque.Code);
                            banque.Contacts = BanqueContactCollection.Charger(banque.Libelle);
                        }
                        dr.Close();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return banque;
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
                    cmd.CommandText = "Ref_Banque_Sauvegarder";
                    cmd.Parameters.Add(new SqlParameter("@CBanque", this.Code));
                    cmd.Parameters.Add(new SqlParameter("@LibBanque", this.Libelle));
                    cmd.Parameters.Add(new SqlParameter("@LibAdresse", LibAdresse));
                    cmd.Parameters.Add(new SqlParameter("@Ville", Ville));
                    cmd.Parameters.Add(new SqlParameter("@CPostal", CPostal));
                    cmd.Parameters.Add(new SqlParameter("@CPays", CPays));
                    cmd.Parameters.Add(new SqlParameter("@IBAN", IBAN));
                    cmd.Parameters.Add(new SqlParameter("@CodeBic", CodeBic));
                    cmd.Parameters.Add(new SqlParameter("@Initiale", Initiale));
                    cmd.Parameters.Add(new SqlParameter("@DateInsertion", DateTime.Now));
                    cmd.Parameters.Add(new SqlParameter("@DateModification", DateTime.Now));
                    cmd.Parameters.Add(new SqlParameter("@CreePar", CreePar));
                    cmd.Parameters.Add(new SqlParameter("@ModifiePar", ModifiePar));
                    cmd.Parameters.Add(new SqlParameter("@PCInsertion", PCInsertion));
                    cmd.Parameters.Add(new SqlParameter("@PCModification", PCModification));

                    foreach (SqlParameter Parameter in cmd.Parameters)
                    {
                        if (Parameter.Value == null)
                        {
                            Parameter.Value = DBNull.Value;
                        }
                    }
                    cmd.ExecuteNonQuery();
                    //this.SupprimerAdressesAnterieurs(transaction);
                    //this.SupprimerContactsAnterieurs(transaction);
                    //foreach (Adresse adresse in Adresses)
                    //{
                    //    adresse.Sauvegarder(transaction);
                    //}
                    //foreach (BanqueContact contact in Contacts)
                    //{
                    //    contact.Sauvegarder(transaction);
                    //}
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        private void SupprimerAdressesAnterieurs(SqlTransaction transaction)
        {
            AdresseCollection collection = AdresseCollection.Charger(this.Code);
            foreach (Adresse item in collection)
            {
                if (!this.Adresses.Exists(p => p.NTiers == item.NTiers && p.IdAdresse == item.IdAdresse))
                    item.Supprimer(transaction);
            }
        }

        private void SupprimerContactsAnterieurs(SqlTransaction transaction)
        {
            BanqueContactCollection collection = BanqueContactCollection.Charger(this.Code);
            foreach (BanqueContact item in collection)
            {
                if (!this.Contacts.Exists(p => p.CContact == item.CContact && p.CBanque == item.CBanque))
                    item.Supprimer(transaction);
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
                    //foreach (Adresse adresse in Adresses)
                    //{
                    //    adresse.Supprimer(transaction);
                    //}
                    //foreach (BanqueContact contact in Contacts)
                    //{
                    //    contact.Supprimer(transaction);
                    //}
                    cmd.Connection = transaction.Connection;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "Ref_Banque_Supprimer";
                    cmd.Parameters.Add(new SqlParameter("@CBanque", this.Code));
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