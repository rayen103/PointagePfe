using CST.LePoint.Referentiel;
using CST.LePoint.Tools;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using System.Xml.Schema;

namespace CST.LePoint.Securite.Entites
{
    [DataContract(Namespace = "")]
    public class Societe
    {
        #region Propriétés

        [DataMember]
        public string CSociete { get; set; }

        [DataMember]
        public string Agence { get; set; }

        [DataMember]
        public string CBanque { get; set; }

        [DataMember]
        public string Activites { get; set; }

        [DataMember]
        public string Adresse { get; set; }

        [DataMember]
        public decimal Capital { get; set; }

        [DataMember]
        public string CNSS { get; set; }

        [DataMember]
        public string CActivite { get; set; }

        [DataMember]
        public string Nom { get; set; }

        [DataMember]
        public string ConventionCollective { get; set; }

        [DataMember]
        public string CDouane { get; set; }

        [DataMember]
        public string CTVA { get; set; }

        [DataMember]
        public string CodePostal { get; set; }

        [DataMember]
        public DateTime DateOuverture { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Fax { get; set; }

        [DataMember]
        public string Statut { get; set; }

        [DataMember]
        public bool BLocalResidante { get; set; }

        [DataMember]
        public string Pays { get; set; }

        [DataMember]
        public decimal PourcentageFodec { get; set; }

        [DataMember]
        public string RaisonSociale { get; set; }

        [DataMember]
        public string RegistreCommerce { get; set; }

        [DataMember]
        public string Telephone { get; set; }

        [DataMember]
        public string Ville { get; set; }

        [DataMember]
        public DateTime DateInsertion { get; set; }

        [DataMember]
        public DateTime DateModification { get; set; }

        [DataMember]
        public int CreePar { get; set; }

        [DataMember]
        public int ModifiePar { get; set; }

        [DataMember]
        public string PCInsertion { get; set; }

        [DataMember]
        public string PCModification { get; set; }
        public string Ip { get; set; }
        public int Port { get; set; }
        public int GMTPlus { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int Rayon { get; set; }
        public int Time { get; set; }

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public bool BAssujetti { get; set; }

        public byte[] Logo { get; set; }

        public SocieteBanqueCollection SocieteBanques { get; set; }

        public SocieteSiteCollection SocieteSites { get; set; }

        #endregion Propriétés

        public Societe()
        {
            Id = Guid.NewGuid();
            BLocalResidante = true;
            SocieteBanques = new SocieteBanqueCollection();
            SocieteSites = new SocieteSiteCollection();
        }

        public bool Equals(Societe other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Id == Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Societe)) return false;
            return Equals((Societe)obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return Nom;
        }

        public XmlSchema GetSchema()
        {
            return null;
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
                    cmd.CommandText = "Societe_Sauvegarder";
                    cmd.Parameters.AddWithValue("@CSociete", CSociete);
                    cmd.Parameters.AddWithValue("@Agence", Agence);
                    cmd.Parameters.AddWithValue("@CBanque", CBanque);
                    cmd.Parameters.AddWithValue("@Activites", Activites);
                    cmd.Parameters.AddWithValue("@Adresse", Adresse);
                    cmd.Parameters.AddWithValue("@Capital", Capital);
                    cmd.Parameters.AddWithValue("@CNSS", CNSS);
                    cmd.Parameters.AddWithValue("@CActivite", CActivite);
                    cmd.Parameters.AddWithValue("@Nom", Nom);
                    cmd.Parameters.AddWithValue("@ConventionCollective", ConventionCollective);
                    cmd.Parameters.AddWithValue("@CDouane", CDouane);
                    cmd.Parameters.AddWithValue("@CTVA", CTVA);
                    cmd.Parameters.AddWithValue("@CodePostal", CodePostal);
                    cmd.Parameters.AddWithValue("@DateOuverture", DateOuverture);
                    cmd.Parameters.AddWithValue("@Email", Email);
                    cmd.Parameters.AddWithValue("@Fax", Fax);
                    cmd.Parameters.AddWithValue("@Statut", Statut);
                    cmd.Parameters.AddWithValue("@BLocalResidante", BLocalResidante);
                    cmd.Parameters.AddWithValue("@Pays", Pays);
                    cmd.Parameters.AddWithValue("@PourcentageFodec", PourcentageFodec);
                    cmd.Parameters.AddWithValue("@RaisonSociale", RaisonSociale);
                    cmd.Parameters.AddWithValue("@RegistreCommerce", RegistreCommerce);
                    cmd.Parameters.AddWithValue("@Telephone", Telephone);
                    cmd.Parameters.AddWithValue("@Ville", Ville);
                    cmd.Parameters.AddWithValue("@BAssujetti", BAssujetti);
                    cmd.Parameters.AddWithValue("@CreePar", CreePar);
                    cmd.Parameters.AddWithValue("@ModifiePar", ModifiePar);
                    cmd.Parameters.AddWithValue("@PCInsertion", PCInsertion);
                    cmd.Parameters.AddWithValue("@PCModification", PCModification);
                    cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                    cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Logo", Logo);
                    cmd.Parameters.AddWithValue("@IP", Ip);
                    cmd.Parameters.AddWithValue("@Port", Port);
                    cmd.Parameters.AddWithValue("@GMTPlus", GMTPlus);
                    cmd.Parameters.AddWithValue("@Latitude", Latitude);
                    cmd.Parameters.AddWithValue("@Longitude", Longitude);
                    cmd.Parameters.AddWithValue("@Rayon", Rayon);
                    cmd.Parameters.AddWithValue("@Time", Time);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    cmd.ExecuteNonQuery();

                    this.SupprimerSocieteBanques(transaction);
                    foreach (SocieteBanque banque in this.SocieteBanques)
                        banque.Sauvegarder(transaction);

                    foreach (SocieteSite site in this.SocieteSites)
                        site.Sauvegarder(transaction);

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        private void SupprimerSocieteBanques(SqlTransaction transaction)
        {
            SocieteBanqueCollection anciensBanque = SocieteBanqueCollection.Charger(CSociete);
            foreach (SocieteBanque item in anciensBanque)
            {
                if (!this.SocieteBanques.Exists(p => p.CSociete.Equals(item.CSociete) && p.CBanque.Equals(item.CBanque) && p.RIB.Equals(item.RIB)))
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
                    cmd.Connection = transaction.Connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Societe_Supprimer";
                    cmd.Parameters.AddWithValue("@CSociete", CSociete);
                    cmd.Parameters.AddWithValue("@Agence", Agence);
                    cmd.Parameters.AddWithValue("@CBanque", CBanque);

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

        public static Societe Charger(string cSociete)
        {
            Societe societe = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "Ref_Societe_Charger";
                    cmd.Parameters.AddWithValue("@CSociete", cSociete);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            societe = new Societe();

                            societe.CSociete = dr["CSociete"].ToString();
                            societe.Agence = dr["Agence"].ToString();
                            societe.CBanque = dr["CBanque"].ToString();
                            if (dr["Activites"] != DBNull.Value)
                                societe.Activites = dr["Activites"].ToString();
                            if (dr["Adresse"] != DBNull.Value)
                                societe.Adresse = dr["Adresse"].ToString();
                            if (dr["Capital"] != DBNull.Value)
                                societe.Capital = decimal.Parse(dr["Capital"].ToString());
                            if (dr["CNSS"] != DBNull.Value)
                                societe.CNSS = dr["CNSS"].ToString();
                            if (dr["CActivite"] != DBNull.Value)
                                societe.CActivite = dr["CActivite"].ToString();
                            if (dr["ConventionCollective"] != DBNull.Value)
                                societe.ConventionCollective = dr["ConventionCollective"].ToString();
                            if (dr["CDouane"] != DBNull.Value)
                                societe.CDouane = dr["CDouane"].ToString();
                            if (dr["CTVA"] != DBNull.Value)
                                societe.CTVA = dr["CTVA"].ToString();
                            if (dr["Nom"] != DBNull.Value)
                                societe.Nom = dr["Nom"].ToString();
                            if (dr["CodePostal"] != DBNull.Value)
                                societe.CodePostal = dr["CodePostal"].ToString();
                            if (dr["DateOuverture"] != DBNull.Value)
                                societe.DateOuverture = DateTime.Parse(dr["DateOuverture"].ToString());
                            if (dr["Email"] != DBNull.Value)
                                societe.Email = dr["Email"].ToString();
                            if (dr["Fax"] != DBNull.Value)
                                societe.Fax = dr["Fax"].ToString();
                            if (dr["Statut"] != DBNull.Value)
                                societe.Statut = dr["Statut"].ToString();
                            if (dr["BLocalResidante"] != DBNull.Value)
                                societe.BLocalResidante = bool.Parse(dr["BLocalResidante"].ToString());
                            if (dr["Pays"] != DBNull.Value)
                                societe.Pays = dr["Pays"].ToString();
                            if (dr["PourcentageFodec"] != DBNull.Value)
                                societe.PourcentageFodec = decimal.Parse(dr["PourcentageFodec"].ToString());
                            if (dr["RaisonSociale"] != DBNull.Value)
                                societe.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["RegistreCommerce"] != DBNull.Value)
                                societe.RegistreCommerce = dr["RegistreCommerce"].ToString();
                            if (dr["Telephone"] != DBNull.Value)
                                societe.Telephone = dr["Telephone"].ToString();
                            if (dr["Ville"] != DBNull.Value)
                                societe.Ville = dr["Ville"].ToString();
                            if (dr["BAssujetti"] != DBNull.Value)
                                societe.BAssujetti = bool.Parse(dr["BAssujetti"].ToString());
                            if (dr["Ip"] != DBNull.Value)
                                societe.Ip = dr["Ip"].ToString();
                            if (dr["Port"] != DBNull.Value)
                                societe.Port = int.Parse(dr["Port"].ToString());
                            if (dr["GMTPlus"] != DBNull.Value)
                                societe.GMTPlus = int.Parse(dr["GMTPlus"].ToString());
                            if (dr["Latitude"] != DBNull.Value)
                                societe.Latitude = decimal.Parse(dr["Latitude"].ToString());
                            if (dr["Longitude"] != DBNull.Value)
                                societe.Longitude = decimal.Parse(dr["Longitude"].ToString());
                            if (dr["Rayon"] != DBNull.Value)
                                societe.Rayon = int.Parse(dr["Rayon"].ToString());
                            if (dr["Time"] != DBNull.Value)
                                societe.Time = int.Parse(dr["Time"].ToString());
                            societe.SocieteBanques = SocieteBanqueCollection.Charger(societe.CSociete);
                            societe.SocieteSites = SocieteSiteCollection.Charger(societe.CSociete, null);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return societe;
        }

        public static Societe Charger()
        {
            Societe societe = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "Ref_Societe_Charger";
                    cmd.Parameters.AddWithValue("@CSociete", null);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            societe = new Societe();

                            societe.CSociete = dr["CSociete"].ToString();
                            societe.Agence = dr["Agence"].ToString();
                            societe.CBanque = dr["CBanque"].ToString();
                            if (dr["Activites"] != DBNull.Value)
                                societe.Activites = dr["Activites"].ToString();
                            if (dr["Adresse"] != DBNull.Value)
                                societe.Adresse = dr["Adresse"].ToString();
                            if (dr["Capital"] != DBNull.Value)
                                societe.Capital = decimal.Parse(dr["Capital"].ToString());
                            if (dr["CNSS"] != DBNull.Value)
                                societe.CNSS = dr["CNSS"].ToString();
                            if (dr["CActivite"] != DBNull.Value)
                                societe.CActivite = dr["CActivite"].ToString();
                            if (dr["ConventionCollective"] != DBNull.Value)
                                societe.ConventionCollective = dr["ConventionCollective"].ToString();
                            if (dr["CDouane"] != DBNull.Value)
                                societe.CDouane = dr["CDouane"].ToString();
                            if (dr["CTVA"] != DBNull.Value)
                                societe.CTVA = dr["CTVA"].ToString();
                            if (dr["Nom"] != DBNull.Value)
                                societe.Nom = dr["Nom"].ToString();
                            if (dr["CodePostal"] != DBNull.Value)
                                societe.CodePostal = dr["CodePostal"].ToString();
                            if (dr["DateOuverture"] != DBNull.Value)
                                societe.DateOuverture = DateTime.Parse(dr["DateOuverture"].ToString());
                            if (dr["Email"] != DBNull.Value)
                                societe.Email = dr["Email"].ToString();
                            if (dr["Fax"] != DBNull.Value)
                                societe.Fax = dr["Fax"].ToString();
                            if (dr["Statut"] != DBNull.Value)
                                societe.Statut = dr["Statut"].ToString();
                            if (dr["BLocalResidante"] != DBNull.Value)
                                societe.BLocalResidante = bool.Parse(dr["BLocalResidante"].ToString());
                            if (dr["Pays"] != DBNull.Value)
                                societe.Pays = dr["Pays"].ToString();
                            if (dr["PourcentageFodec"] != DBNull.Value)
                                societe.PourcentageFodec = decimal.Parse(dr["PourcentageFodec"].ToString());
                            if (dr["RaisonSociale"] != DBNull.Value)
                                societe.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["RegistreCommerce"] != DBNull.Value)
                                societe.RegistreCommerce = dr["RegistreCommerce"].ToString();
                            if (dr["Telephone"] != DBNull.Value)
                                societe.Telephone = dr["Telephone"].ToString();
                            if (dr["Ville"] != DBNull.Value)
                                societe.Ville = dr["Ville"].ToString();
                            if (dr["BAssujetti"] != DBNull.Value)
                                societe.BAssujetti = bool.Parse(dr["BAssujetti"].ToString());
                            if (dr["Ip"] != DBNull.Value)
                                societe.Ip = dr["Ip"].ToString();
                            if (dr["Port"] != DBNull.Value)
                                societe.Port = int.Parse(dr["Port"].ToString());
                            if (dr["GMTPlus"] != DBNull.Value)
                                societe.GMTPlus = int.Parse(dr["GMTPlus"].ToString());
                            if (dr["Latitude"] != DBNull.Value)
                                societe.Latitude = decimal.Parse(dr["Latitude"].ToString());
                            if (dr["Longitude"] != DBNull.Value)
                                societe.Longitude = decimal.Parse(dr["Longitude"].ToString());
                            if (dr["Rayon"] != DBNull.Value)
                                societe.Rayon = int.Parse(dr["Rayon"].ToString());
                            if (dr["Time"] != DBNull.Value)
                                societe.Time = int.Parse(dr["Time"].ToString());
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return societe;
        }

        public static List<Societe> Charger_collection()
        {
            Societe societe = null;
            List<Societe> collection = new List<Societe>();
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "Ref_Societe_Charger";
                    cmd.Parameters.AddWithValue("@CSociete", null);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    //societe = new Societe();
                    //societe.Nom = "";
                    //societe.CSociete = "0";
                    //collection.Add(societe);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            societe = new Societe();

                            societe.CSociete = dr["CSociete"].ToString();
                            societe.Agence = dr["Agence"].ToString();
                            societe.CBanque = dr["CBanque"].ToString();
                            if (dr["Activites"] != DBNull.Value)
                                societe.Activites = dr["Activites"].ToString();
                            if (dr["Adresse"] != DBNull.Value)
                                societe.Adresse = dr["Adresse"].ToString();
                            if (dr["Capital"] != DBNull.Value)
                                societe.Capital = decimal.Parse(dr["Capital"].ToString());
                            if (dr["CNSS"] != DBNull.Value)
                                societe.CNSS = dr["CNSS"].ToString();
                            if (dr["CActivite"] != DBNull.Value)
                                societe.CActivite = dr["CActivite"].ToString();
                            if (dr["ConventionCollective"] != DBNull.Value)
                                societe.ConventionCollective = dr["ConventionCollective"].ToString();
                            if (dr["CDouane"] != DBNull.Value)
                                societe.CDouane = dr["CDouane"].ToString();
                            if (dr["CTVA"] != DBNull.Value)
                                societe.CTVA = dr["CTVA"].ToString();
                            if (dr["Nom"] != DBNull.Value)
                                societe.Nom = dr["Nom"].ToString();
                            if (dr["CodePostal"] != DBNull.Value)
                                societe.CodePostal = dr["CodePostal"].ToString();
                            if (dr["DateOuverture"] != DBNull.Value)
                                societe.DateOuverture = DateTime.Parse(dr["DateOuverture"].ToString());
                            if (dr["Email"] != DBNull.Value)
                                societe.Email = dr["Email"].ToString();
                            if (dr["Fax"] != DBNull.Value)
                                societe.Fax = dr["Fax"].ToString();
                            if (dr["Statut"] != DBNull.Value)
                                societe.Statut = dr["Statut"].ToString();
                            if (dr["BLocalResidante"] != DBNull.Value)
                                societe.BLocalResidante = bool.Parse(dr["BLocalResidante"].ToString());
                            if (dr["Pays"] != DBNull.Value)
                                societe.Pays = dr["Pays"].ToString();
                            if (dr["PourcentageFodec"] != DBNull.Value)
                                societe.PourcentageFodec = decimal.Parse(dr["PourcentageFodec"].ToString());
                            if (dr["RaisonSociale"] != DBNull.Value)
                                societe.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["RegistreCommerce"] != DBNull.Value)
                                societe.RegistreCommerce = dr["RegistreCommerce"].ToString();
                            if (dr["Telephone"] != DBNull.Value)
                                societe.Telephone = dr["Telephone"].ToString();
                            if (dr["Ville"] != DBNull.Value)
                                societe.Ville = dr["Ville"].ToString();
                            if (dr["BAssujetti"] != DBNull.Value)
                                societe.BAssujetti = bool.Parse(dr["BAssujetti"].ToString());
                            if (dr["Logo"] != DBNull.Value)
                                societe.Logo = (byte[])dr["Logo"];
                            // societe.SocieteBanques = SocieteBanqueCollection.Charger(societe.CSociete);
                            collection.Add(societe);
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

        //ItemCollection
        public static ItemCollection ChargerItemCollection()
        {
            Item societe = null;
            ItemCollection collection = new ItemCollection();
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "Ref_Societe_Charger";
                    cmd.Parameters.AddWithValue("@CSociete", null);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    //societe = new Societe();
                    //societe.Nom = "";
                    //societe.CSociete = "0";
                    //collection.Add(societe);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            societe = new Item();

                            societe.Code = dr["CSociete"].ToString();
                            if (dr["Nom"] != DBNull.Value)
                                societe.Libelle = dr["Nom"].ToString();
                            collection.Add(societe);
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

    [CollectionDataContract(Namespace = "")]
    public class Societes : HashSetSerializable<Societe>
    {
    }
}