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
    public class AgenceCollection : ItemCollection
    {
        public AgenceCollection()
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
                cmd.CommandText = "Agence_Rpt_Charger";
                cmd.Parameters.AddWithValue("@CAgenceBanque", DBNull.Value);
                foreach (SqlParameter parametre in cmd.Parameters)                
                    if (parametre.Value == null)                    
                        parametre.Value = DBNull.Value;                    
                
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "Agence_Rpt_Charger");
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
                cmd.CommandText = "Ref_AgenceScan_Charger";
                cmd.Parameters.AddWithValue("@CAgence", DBNull.Value);
                cmd.Parameters.AddWithValue("@CBanque", DBNull.Value);
                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            return (dt);
        }

        public static AgenceCollection Charger()
        {
            Agence Agence = null;
            AgenceCollection collection = new AgenceCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_Agence_Charger";
                    cmd.Parameters.Add(new SqlParameter("@CAgenceBanque", DBNull.Value));

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Agence = new Agence();

                            Agence.Code = dr["CAgenceBanque"].ToString().Trim();
                            if (dr["Lib_Agc"] != DBNull.Value)
                                Agence.Libelle = dr["Lib_Agc"].ToString().Trim();


                            collection.Add(Agence);
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

        public static AgenceCollection Charger(string cBanque)
        {
            Agence Agence = null;
            AgenceCollection collection = new AgenceCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_Agence_ChargerParBanque";
                    cmd.Parameters.Add(new SqlParameter("@CBanque", cBanque));

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Agence = new Agence();

                            Agence.Code = dr["CAgenceBanque"].ToString().Trim();
                            if (dr["Lib_Agc"] != DBNull.Value)
                                Agence.Libelle = dr["Lib_Agc"].ToString().Trim();


                            collection.Add(Agence);
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
        
        public static AgenceCollection ChargerparBanque(string Cbanque)
        {
            Agence Agence = null;
            AgenceCollection collection = new AgenceCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_Agence_ChargerParBanque";
                    cmd.Parameters.Add(new SqlParameter("@CBanque", Cbanque));

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Agence = new Agence();

                            Agence.Code = dr["CAgenceBanque"].ToString().Trim();
                            if (dr["Lib_Agc"] != DBNull.Value)
                                Agence.Libelle = dr["Lib_Agc"].ToString().Trim();


                            collection.Add(Agence);
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
        
        public static DataTable RecupererAgence()
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
                    cmd.CommandText = "Ref_Agence_Charger";
                    cmd.Parameters.Add(new SqlParameter("@CAgenceBanque", DBNull.Value));
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
    }

    [Serializable]
    public class Agence : Item
    {
        #region Propriétés


        [XmlAttribute("CodeAgc")]
        [Bindable(true)]
        public string CodeAgc { get; set; }

        [XmlAttribute("CBanque")]
        [Bindable(true)]
        public string CBanque { get; set; }

        [XmlAttribute("Fax")]
        [Bindable(true)]
        public string Fax { get; set; }

        [XmlAttribute("Email")]
        [Bindable(true)]
        public string Email { get; set; }

        [XmlAttribute("CAgenceBanque")]
        [Bindable(true)]
        public string CAgenceBanque { get; set; }


        [XmlAttribute("LibAgc")]
        [Bindable(true)]
        public string LibAgc { get; set; }

        [XmlAttribute("SiteWeb")]
        [Bindable(true)]
        public string SiteWeb { get; set; }

        [XmlAttribute("Adresses")]
        [Bindable(true)]
        public string Adresses { get; set; }

        [XmlAttribute("Tel")]
        [Bindable(true)]
        public string Tel { get; set; }

        [XmlAttribute("Responsable")]
        [Bindable(true)]
        public string Responsable { get; set; }

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

        public Agence()
        {
            this.Code = string.Empty;
            this.Libelle = string.Empty;
            CAgenceBanque = string.Empty;
            CBanque = string.Empty;
            CodeAgc = string.Empty;
            Tel = string.Empty;

        }

        public static Agence Charger(string cAgenceBanque)
        {
            Agence Agence = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_Agence_Charger";
                    cmd.Parameters.Add(new SqlParameter("@CAgenceBanque", cAgenceBanque));

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Agence = new Agence();
                            Agence.Code = dr["CAgenceBanque"].ToString().Trim();
                            Agence.Libelle = dr["Lib_Agc"].ToString().Trim();

                            if (dr["CBanque"] != DBNull.Value)
                                Agence.CBanque = dr["CBanque"].ToString().Trim();
                            if (dr["CodeAgc"] != DBNull.Value)
                                Agence.CodeAgc = dr["CodeAgc"].ToString().Trim();
                            if (dr["Site_Web"] != DBNull.Value)
                                Agence.SiteWeb = dr["Site_Web"].ToString().Trim();
                            if (dr["Tel"] != DBNull.Value)
                                Agence.Tel = dr["Tel"].ToString().Trim();
                            if (dr["Adresse"] != DBNull.Value)
                                Agence.Adresses = dr["Adresse"].ToString().Trim();
                            if (dr["Responsable"] != DBNull.Value)
                                Agence.Responsable = dr["Responsable"].ToString().Trim();
                            if (dr["fax"] != DBNull.Value)
                                Agence.Fax = dr["fax"].ToString().Trim();
                            if (dr["Email"] != DBNull.Value)
                                Agence.Email = dr["Email"].ToString().Trim();


                        }
                        dr.Close();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return Agence;
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
                    cmd.CommandText = "Ref_Agence_Sauvegarder";
                    cmd.Parameters.Add(new SqlParameter("@Code_Agc", this.Code));
                    cmd.Parameters.Add(new SqlParameter("@Lib_Agc", this.Libelle));
                    cmd.Parameters.Add(new SqlParameter("@Site_Web", SiteWeb));
                    cmd.Parameters.Add(new SqlParameter("@Addresse", Adresses));
                    cmd.Parameters.Add(new SqlParameter("@Tel", Tel));
                    cmd.Parameters.Add(new SqlParameter("@Fax", Fax));
                    cmd.Parameters.Add(new SqlParameter("@Email", Email));
                    cmd.Parameters.Add(new SqlParameter("@CBanque", CBanque));
                    cmd.Parameters.Add(new SqlParameter("@CAgenceBanque", CAgenceBanque));
                    cmd.Parameters.Add(new SqlParameter("@Responsable", Responsable));
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
                    //foreach (Adresse adresse in Adresses)
                    //{
                    //    adresse.Supprimer(transaction);
                    //}
                    //foreach (AgenceContact contact in Contacts)
                    //{
                    //    contact.Supprimer(transaction);
                    //}
                    cmd.Connection = transaction.Connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_Agence_Supprimer";
                    cmd.Parameters.Add(new SqlParameter("@CAgenceBanque", this.CAgenceBanque));
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