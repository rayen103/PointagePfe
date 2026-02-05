//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//using System.Data.SqlClient;
//using System.Configuration;
//using System.Data;
//using System.Xml.Serialization;
//using System.ComponentModel;
//using CST.LePoint.Tools;

//namespace CST.LePoint.Securite.Entites
//{
//    [Serializable]
//    public class Societe
//    {
//        #region Propriétés

//        [XmlAttribute("CSociete")]
//        [Bindable(true)]
//        public string CSociete { get; set; }

//        [XmlAttribute("Agence")]
//        [Bindable(true)]
//        public string Agence { get; set; }

//        [XmlAttribute("CBanque")]
//        [Bindable(true)]
//        public string CBanque { get; set; }

//        [XmlAttribute("Activites")]
//        [Bindable(true)]
//        public string Activites { get; set; }

//        [XmlAttribute("Adresse")]
//        [Bindable(true)]
//        public string Adresse { get; set; }

//        [XmlAttribute("Capital")]
//        [Bindable(true)]
//        public decimal Capital { get; set; }

//        [XmlAttribute("CNSS")]
//        [Bindable(true)]
//        public string CNSS { get; set; }

//        [XmlAttribute("CActivite")]
//        [Bindable(true)]
//        public string CActivite { get; set; }

//        [XmlAttribute("Nom")]
//        [Bindable(true)]
//        public string Nom { get; set; }

//        [XmlAttribute("ConventionCollective")]
//        [Bindable(true)]
//        public string ConventionCollective { get; set; }

//        [XmlAttribute("CDouane")]
//        [Bindable(true)]
//        public string CDouane { get; set; }

//        [XmlAttribute("CTVA")]
//        [Bindable(true)]
//        public string CTVA { get; set; }

//        [XmlAttribute("CodePostal")]
//        [Bindable(true)]
//        public string CodePostal { get; set; }

//        [XmlAttribute("DateOuverture")]
//        [Bindable(true)]
//        public DateTime DateOuverture { get; set; }

//        [XmlAttribute("Email")]
//        [Bindable(true)]
//        public string Email { get; set; }

//        [XmlAttribute("Fax")]
//        [Bindable(true)]
//        public string Fax { get; set; }

//        [XmlAttribute("Statut")]
//        [Bindable(true)]
//        public string Statut { get; set; }

//        [XmlAttribute("BLocalResidante")]
//        [Bindable(true)]
//        public bool BLocalResidante { get; set; }

//        [XmlAttribute("Pays")]
//        [Bindable(true)]
//        public string Pays { get; set; }

//        [XmlAttribute("PourcentageFodec")]
//        [Bindable(true)]
//        public decimal PourcentageFodec { get; set; }

//        [XmlAttribute("RaisonSociale")]
//        [Bindable(true)]
//        public string RaisonSociale { get; set; }

//        [XmlAttribute("RegistreCommerce")]
//        [Bindable(true)]
//        public string RegistreCommerce { get; set; }

//        [XmlAttribute("Telephone")]
//        [Bindable(true)]
//        public string Telephone { get; set; }

//        [XmlAttribute("Ville")]
//        [Bindable(true)]
//        public string Ville { get; set; }

//        [XmlAttribute("DateInsertion")]
//        [Bindable(true)]
//        public DateTime DateInsertion { get; set; }

//        [XmlAttribute("DateModification")]
//        [Bindable(true)]
//        public DateTime DateModification { get; set; }

//        [XmlAttribute("CreePar")]
//        [Bindable(true)]
//        public int CreePar { get; set; }

//        [XmlAttribute("ModifiePar")]
//        [Bindable(true)]
//        public int ModifiePar { get; set; }

//        [XmlAttribute("PCInsertion")]
//        [Bindable(true)]
//        public string PCInsertion { get; set; }

//        [XmlAttribute("PCModification")]
//        [Bindable(true)]
//        public string PCModification { get; set; }
//        #endregion

//        public Societe()
//        {
//            BLocalResidante = true;
//        }

//        public void Sauvegarder()
//        {
//            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
//            {
//                cn.Open();
//                SqlTransaction transaction = cn.BeginTransaction();

//                try
//                {
//                    SqlCommand cmd = new SqlCommand();
//                    cmd.Transaction = transaction;
//                    cmd.Connection = transaction.Connection;
//                    cmd.CommandType = CommandType.StoredProcedure;
//                    cmd.CommandText = "Societe_Sauvegarder";
//                    cmd.Parameters.AddWithValue("@CSociete", CSociete);
//                    cmd.Parameters.AddWithValue("@Agence", Agence);
//                    cmd.Parameters.AddWithValue("@CBanque", CBanque);
//                    cmd.Parameters.AddWithValue("@Activites", Activites);
//                    cmd.Parameters.AddWithValue("@Adresse", Adresse);
//                    cmd.Parameters.AddWithValue("@Capital", Capital);
//                    cmd.Parameters.AddWithValue("@CNSS", CNSS);
//                    cmd.Parameters.AddWithValue("@CActivite", CActivite);
//                    cmd.Parameters.AddWithValue("@Nom", Nom);
//                    cmd.Parameters.AddWithValue("@ConventionCollective", ConventionCollective);
//                    cmd.Parameters.AddWithValue("@CDouane", CDouane);
//                    cmd.Parameters.AddWithValue("@CTVA", CTVA);
//                    cmd.Parameters.AddWithValue("@CodePostal", CodePostal);
//                    cmd.Parameters.AddWithValue("@DateOuverture", DateOuverture);
//                    cmd.Parameters.AddWithValue("@Email", Email);
//                    cmd.Parameters.AddWithValue("@Fax", Fax);
//                    cmd.Parameters.AddWithValue("@Statut", Statut);
//                    cmd.Parameters.AddWithValue("@BLocalResidante", BLocalResidante);
//                    cmd.Parameters.AddWithValue("@Pays", Pays);
//                    cmd.Parameters.AddWithValue("@PourcentageFodec", PourcentageFodec);
//                    cmd.Parameters.AddWithValue("@RaisonSociale", RaisonSociale);
//                    cmd.Parameters.AddWithValue("@RegistreCommerce", RegistreCommerce);
//                    cmd.Parameters.AddWithValue("@Telephone", Telephone);
//                    cmd.Parameters.AddWithValue("@Ville", Ville);

//                    cmd.Parameters.AddWithValue("@CreePar", CreePar);
//                    cmd.Parameters.AddWithValue("@ModifiePar", ModifiePar);
//                    cmd.Parameters.AddWithValue("@PCInsertion", PCInsertion);
//                    cmd.Parameters.AddWithValue("@PCModification", PCModification);
//                    cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
//                    cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);

//                    foreach (SqlParameter parametre in cmd.Parameters)
//                        if (parametre.Value == null)
//                            parametre.Value = DBNull.Value;

//                    cmd.ExecuteNonQuery();

//                    transaction.Commit();
//                }
//                catch (Exception)
//                {
//                    transaction.Rollback();
//                    throw;
//                }
//            }
//        }

//        public void Supprimer()
//        {
//            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
//            {
//                cn.Open();
//                SqlTransaction transaction = cn.BeginTransaction();
//                try
//                {
//                    SqlCommand cmd = new SqlCommand();
//                    cmd.Transaction = transaction;
//                    cmd.Connection = transaction.Connection;
//                    cmd.CommandType = CommandType.StoredProcedure;
//                    cmd.CommandText = "Societe_Supprimer";
//                    cmd.Parameters.AddWithValue("@CSociete", CSociete);
//                    cmd.Parameters.AddWithValue("@Agence", Agence);
//                    cmd.Parameters.AddWithValue("@CBanque", CBanque);

//                    foreach (SqlParameter parametre in cmd.Parameters)
//                        if (parametre.Value == null)
//                            parametre.Value = DBNull.Value;

//                    cmd.ExecuteNonQuery();
//                    transaction.Commit();
//                }
//                catch
//                {
//                    transaction.Rollback();
//                    throw;
//                }
//            }
//        }

//        public static Societe Charger(string cSociete, string agence, string cBanque)
//        {
//            Societe societe = null;

//            try
//            {
//                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
//                {
//                    cn.Open();

//                    SqlCommand cmd = new SqlCommand();
//                    cmd.Connection = cn;
//                    cmd.CommandType = CommandType.StoredProcedure;

//                    cmd.CommandText = "Societe_Charger";
//                    cmd.Parameters.AddWithValue("@CSociete", cSociete);
//                    cmd.Parameters.AddWithValue("@Agence", agence);
//                    cmd.Parameters.AddWithValue("@CBanque", cBanque);

//                    foreach (SqlParameter parametre in cmd.Parameters)
//                        if (parametre.Value == null)
//                            parametre.Value = DBNull.Value;

//                    using (SqlDataReader dr = cmd.ExecuteReader())
//                    {
//                        if (dr.Read())
//                        {
//                            societe = new Societe();

//                            societe.CSociete = dr["CSociete"].ToString();
//                            societe.Agence = dr["Agence"].ToString();
//                            societe.CBanque = dr["CBanque"].ToString();
//                            if (dr["Activites"] != DBNull.Value)
//                                societe.Activites = dr["Activites"].ToString();
//                            if (dr["Adresse"] != DBNull.Value)
//                                societe.Adresse = dr["Adresse"].ToString();
//                            if (dr["Capital"] != DBNull.Value)
//                                societe.Capital = decimal.Parse(dr["Capital"].ToString());
//                            if (dr["CNSS"] != DBNull.Value)
//                                societe.CNSS = dr["CNSS"].ToString();
//                            if (dr["CActivite"] != DBNull.Value)
//                                societe.CActivite = dr["CActivite"].ToString();
//                            if (dr["ConventionCollective"] != DBNull.Value)
//                                societe.ConventionCollective = dr["ConventionCollective"].ToString();
//                            if (dr["CDouane"] != DBNull.Value)
//                                societe.CDouane = dr["CDouane"].ToString();
//                            if (dr["CTVA"] != DBNull.Value)
//                                societe.CTVA = dr["CTVA"].ToString();
//                            if (dr["Nom"] != DBNull.Value)
//                                societe.Nom = dr["Nom"].ToString();
//                            if (dr["CodePostal"] != DBNull.Value)
//                                societe.CodePostal = dr["CodePostal"].ToString();
//                            if (dr["DateOuverture"] != DBNull.Value)
//                                societe.DateOuverture = DateTime.Parse(dr["DateOuverture"].ToString());
//                            if (dr["Email"] != DBNull.Value)
//                                societe.Email = dr["Email"].ToString();
//                            if (dr["Fax"] != DBNull.Value)
//                                societe.Fax = dr["Fax"].ToString();
//                            if (dr["Statut"] != DBNull.Value)
//                                societe.Statut = dr["Statut"].ToString();
//                            if (dr["BLocalResidante"] != DBNull.Value)
//                                societe.BLocalResidante = bool.Parse(dr["BLocalResidante"].ToString());
//                            if (dr["Pays"] != DBNull.Value)
//                                societe.Pays = dr["Pays"].ToString();
//                            if (dr["PourcentageFodec"] != DBNull.Value)
//                                societe.PourcentageFodec = decimal.Parse(dr["PourcentageFodec"].ToString());
//                            if (dr["RaisonSociale"] != DBNull.Value)
//                                societe.RaisonSociale = dr["RaisonSociale"].ToString();
//                            if (dr["RegistreCommerce"] != DBNull.Value)
//                                societe.RegistreCommerce = dr["RegistreCommerce"].ToString();
//                            if (dr["Telephone"] != DBNull.Value)
//                                societe.Telephone = dr["Telephone"].ToString();
//                            if (dr["Ville"] != DBNull.Value)
//                                societe.Ville = dr["Ville"].ToString();
//                        }
//                    }
//                }
//            }
//            catch (Exception)
//            {
//                throw;
//            }

//            return societe;
//        }
//    }

//    [Serializable]
//    public class SocieteCollection : HashSetSerializable<Societe>
//    {
//        public static SocieteCollection Charger()
//        {
//            SocieteCollection collection = new SocieteCollection();

//            try
//            {
//                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
//                {
//                    cn.Open();
//                    SqlCommand cmd = new SqlCommand();
//                    cmd.Connection = cn;
//                    cmd.CommandType = CommandType.StoredProcedure;
//                    cmd.CommandText = "Societe_Charger";
//                    using (SqlDataReader dr = cmd.ExecuteReader())
//                    {
//                        while (dr.Read())
//                        {
//                            Societe societe = new Societe();
//                            societe.CSociete = dr["CSociete"].ToString();
//                            societe.Agence = dr["Agence"].ToString();
//                            societe.CBanque = dr["CBanque"].ToString();
//                            if (dr["Activites"] != DBNull.Value)
//                                societe.Activites = dr["Activites"].ToString();
//                            if (dr["Adresse"] != DBNull.Value)
//                                societe.Adresse = dr["Adresse"].ToString();
//                            if (dr["Capital"] != DBNull.Value)
//                                societe.Capital = decimal.Parse(dr["Capital"].ToString());
//                            if (dr["CNSS"] != DBNull.Value)
//                                societe.CNSS =dr["CNSS"].ToString();
//                            if (dr["CActivite"] != DBNull.Value)
//                                societe.CActivite = dr["CActivite"].ToString();
//                            if (dr["ConventionCollective"] != DBNull.Value)
//                                societe.ConventionCollective = dr["ConventionCollective"].ToString();
//                            if (dr["CDouane"] != DBNull.Value)
//                                societe.CDouane = dr["CDouane"].ToString();
//                            if (dr["CTVA"] != DBNull.Value)
//                                societe.CTVA = dr["CTVA"].ToString();
//                            if (dr["Nom"] != DBNull.Value)
//                                societe.Nom = dr["Nom"].ToString();
//                            if (dr["CodePostal"] != DBNull.Value)
//                                societe.CodePostal = dr["CodePostal"].ToString();
//                            if (dr["DateOuverture"] != DBNull.Value)
//                                societe.DateOuverture = DateTime.Parse(dr["DateOuverture"].ToString());
//                            if (dr["Email"] != DBNull.Value)
//                                societe.Email = dr["Email"].ToString();
//                            if (dr["Fax"] != DBNull.Value)
//                                societe.Fax = dr["Fax"].ToString();
//                            if (dr["Statut"] != DBNull.Value)
//                                societe.Statut = dr["Statut"].ToString();
//                            if (dr["BLocalResidante"] != DBNull.Value)
//                                societe.BLocalResidante = bool.Parse(dr["BLocalResidante"].ToString());
//                            if (dr["Pays"] != DBNull.Value)
//                                societe.Pays = dr["Pays"].ToString();
//                            if (dr["PourcentageFodec"] != DBNull.Value)
//                                societe.PourcentageFodec = decimal.Parse(dr["PourcentageFodec"].ToString());
//                            if (dr["RaisonSociale"] != DBNull.Value)
//                                societe.RaisonSociale = dr["RaisonSociale"].ToString();
//                            if (dr["RegistreCommerce"] != DBNull.Value)
//                                societe.RegistreCommerce = dr["RegistreCommerce"].ToString();
//                            if (dr["Telephone"] != DBNull.Value)
//                                societe.Telephone = dr["Telephone"].ToString();
//                            if (dr["Ville"] != DBNull.Value)
//                                societe.Ville = dr["Ville"].ToString();

//                            collection.Add(societe);
//                        }
//                    }
//                }
//            }
//            catch (Exception)
//            {
//                throw;
//            }

//            return collection;
//        }
//    }

//}