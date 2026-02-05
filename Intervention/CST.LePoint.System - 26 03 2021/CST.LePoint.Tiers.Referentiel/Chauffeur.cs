using CST.LePoint.Referentiel;
using CST.LePoint.Securite;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace CST.LePoint.Tiers.Referentiel
{
    public class Chauffeur : Item
    {
        #region Propriétés

        [XmlAttribute("Nom")]
        [Bindable(true)]
        public string Nom { get; set; }

        [XmlAttribute("CIN")]
        [Bindable(true)]
        public string CIN { get; set; }

        [XmlAttribute("RFID_Chauf")]
        [Bindable(true)]
        public String RFID_Chauf { get; set; }

        [XmlAttribute("BExterne")]
        [Bindable(true)]
        public bool BExterne { get; set; }

        [XmlAttribute("Prenom")]
        [Bindable(true)]
        public string Prenom { get; set; }

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
        public string Code_Societe { get; set; }
        public string Code_Site { get; set; }

        #endregion Propriétés

        public Chauffeur()
        {
            //this.BActif = true;
            this.BExterne = false;
        }

        public void Sauvegarder()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();

                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_Chauffeur_Sauvegarder";
                    cmd.Parameters.AddWithValue("@CChauffeur", Code);
                    cmd.Parameters.AddWithValue("@CIN", CIN);
                    cmd.Parameters.AddWithValue("@RFID_Chauf", RFID_Chauf);
                    cmd.Parameters.AddWithValue("@Prenom", Prenom);
                    cmd.Parameters.AddWithValue("@Nom", Nom);
                    cmd.Parameters.AddWithValue("@BExterne", BExterne);
                    cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                    cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                    cmd.Parameters.AddWithValue("@CreePar", CreePar);
                    cmd.Parameters.AddWithValue("@ModifiePar", ModifiePar);
                    cmd.Parameters.AddWithValue("@PCInsertion", PCInsertion);
                    cmd.Parameters.AddWithValue("@PCModification", PCModification);
                    cmd.Parameters.AddWithValue("@Code_Societe", GestionSession.SocieteCourante.CSociete);
                    cmd.Parameters.AddWithValue("@Code_Site", GestionSession.SocieteSite);
                    foreach (SqlParameter parametre in cmd.Parameters)
                    {
                        if (parametre.Value == null)
                        {
                            parametre.Value = DBNull.Value;
                        }
                    }

                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public static Chauffeur Charger(string cChauffeur)
        {
            Chauffeur chauffeur = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_Chauffeur_Charger";
                    cmd.Parameters.AddWithValue("@CChauffeur", cChauffeur);
                    cmd.Parameters.AddWithValue("@Code_Societe", GestionSession.SocieteCourante.CSociete);
                    cmd.Parameters.AddWithValue("@Code_Site", GestionSession.SocieteSite);


                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            chauffeur = new Chauffeur();

                            chauffeur.Code = dr["CChauffeur"].ToString();
                            if (dr["CIN"] != DBNull.Value)
                                chauffeur.CIN = dr["CIN"].ToString();
                            if (dr["RFID_Chauf"] != DBNull.Value)
                                chauffeur.RFID_Chauf = (dr["RFID_Chauf"].ToString());
                            if (dr["BExterne"] != DBNull.Value)
                                chauffeur.BExterne = bool.Parse(dr["BExterne"].ToString());
                            if (dr["Nom"] != DBNull.Value)
                                chauffeur.Nom = dr["Nom"].ToString();
                            if (dr["Prenom"] != DBNull.Value)
                                chauffeur.Prenom = dr["Prenom"].ToString();

                            chauffeur.Libelle = chauffeur.Prenom + " " + chauffeur.Nom;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return chauffeur;
        }

        public void Supprimer()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_Chauffeur_Supprimer";
                    cmd.Parameters.AddWithValue("@CChauffeur", Code);
                    cmd.Parameters.AddWithValue("@Code_Societe", GestionSession.SocieteCourante.CSociete);
                    cmd.Parameters.AddWithValue("@Code_Site", GestionSession.SocieteSite);

                    foreach (SqlParameter parametre in cmd.Parameters)
                    {
                        if (parametre.Value == null)
                        {
                            parametre.Value = DBNull.Value;
                        }
                    }

                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }

    [Serializable]
    public class ChauffeurCollection : ItemCollection
    {
        public ChauffeurCollection()
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
                cmd.CommandText = "Chauffeur_Rpt_Charger";
                cmd.Parameters.AddWithValue("@CChauffeur", DBNull.Value);
                cmd.Parameters.AddWithValue("@Code_Societe", GestionSession.SocieteCourante.CSociete);

                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "Chauffeur_Rpt_Charger");
            }
            return (ds);
        }

        public static DataTable RemplirGrid()
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Ref_Chauffeur_ChargerTous";
                cmd.Parameters.AddWithValue("@CChauffeur", DBNull.Value);
                cmd.Parameters.AddWithValue("@Code_Societe", GestionSession.SocieteCourante.CSociete);
                cmd.Parameters.AddWithValue("@Code_Site", GestionSession.SocieteSite);

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

        public static ChauffeurCollection Charger()
        {
            ChauffeurCollection chauffeurCollection = new ChauffeurCollection();

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
                    cmd.CommandText = "Ref_Chauffeur_Charger";
                    cmd.Parameters.AddWithValue("@CChauffeur", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Code_Societe", GestionSession.SocieteCourante.CSociete);
                    cmd.Parameters.AddWithValue("@Code_Site", GestionSession.SocieteSite);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Chauffeur chauffeur = new Chauffeur();

                            chauffeur.Code = dr["CChauffeur"].ToString();
                            if (dr["Nom"] != DBNull.Value)
                                chauffeur.Nom = dr["Nom"].ToString();
                            if (dr["Prenom"] != DBNull.Value)
                                chauffeur.Prenom = dr["Prenom"].ToString();
                            if (dr["CIN"] != DBNull.Value)
                                chauffeur.CIN = dr["CIN"].ToString();
                            if (dr["BExterne"] != DBNull.Value)
                                chauffeur.BExterne = bool.Parse(dr["BExterne"].ToString());
                            if (dr["RFID_Chauf"] != DBNull.Value)
                                chauffeur.RFID_Chauf = (dr["RFID_Chauf"].ToString());

                            chauffeur.Libelle = chauffeur.Prenom + " " + chauffeur.Nom;

                            chauffeurCollection.Add(chauffeur);
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return (chauffeurCollection);
        }
    }
}