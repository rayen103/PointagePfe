//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Data.SqlClient;
//using System.Configuration;
//using System.Data;
//using System.Xml.Serialization;
//using System.ComponentModel;

//namespace CST.LePoint.Securite.Entites
//{
//    [Serializable]
//    public class Utilisateur1Collection : List<Utilisateur1>
//    {
//        public static Utilisateur1Collection Charger()
//        {
//            Utilisateur1Collection collection = new Utilisateur1Collection();

//            try
//            {
//                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
//                {
//                    cn.Open();
//                    SqlCommand cmd = new SqlCommand();
//                    cmd.Connection = cn;
//                    cmd.CommandType = CommandType.StoredProcedure;
//                    cmd.CommandText = "Utilisateur_Charger";
//                    using (SqlDataReader dr = cmd.ExecuteReader())
//                    {
//                        while (dr.Read())
//                        {
//                            Utilisateur1 Utilisateur1 = new Utilisateur1();

//                            Utilisateur1.CSociete = dr["CSociete"].ToString();
//                            Utilisateur1.CUtilisateur = dr["CUtilisateur"].ToString();
//                            if (dr["BAdministrateur"] != DBNull.Value)
//                                Utilisateur1.BAdministrateur = bool.Parse(dr["BAdministrateur"].ToString());
//                            if (dr["CGroupe"] != DBNull.Value)
//                                Utilisateur1.CGroupe = dr["CGroupe"].ToString();
//                            if (dr["Email"] != DBNull.Value)
//                                Utilisateur1.Email = dr["Email"].ToString();
//                            if (dr["Fonction"] != DBNull.Value)
//                                Utilisateur1.Fonction = dr["Fonction"].ToString();
//                            if (dr["GSM"] != DBNull.Value)
//                                Utilisateur1.GSM = dr["GSM"].ToString();
//                            if (dr["Identifiant"] != DBNull.Value)
//                                Utilisateur1.Identifiant = int.Parse(dr["Identifiant"].ToString());
//                            if (dr["Nom"] != DBNull.Value)
//                                Utilisateur1.Nom = dr["Nom"].ToString();
//                            if (dr["Prenom"] != DBNull.Value)
//                                Utilisateur1.Prenom = dr["Prenom"].ToString();
//                            if (dr["MotDePasse"] != DBNull.Value)
//                                Utilisateur1.MotDePasse = dr["MotDePasse"].ToString();
//                            if (dr["NumeroTelephone"] != DBNull.Value)
//                                Utilisateur1.NumeroTelephone = dr["NumeroTelephone"].ToString();
//                            if (dr["Adresse"] != DBNull.Value)
//                                Utilisateur1.Adresse = dr["Adresse"].ToString();

//                            collection.Add(Utilisateur1);
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

//    [Serializable]
//    public class Utilisateur1
//    {
//        #region Propriétés

//        [XmlAttribute("CSociete")]
//        [Bindable(true)]
//        public string CSociete { get; set; }

//        [XmlAttribute("CUtilisateur")]
//        [Bindable(true)]
//        public string CUtilisateur { get; set; }

//        [XmlAttribute("BAdministrateur")]
//        [Bindable(true)]
//        public bool BAdministrateur { get; set; }

//        [XmlAttribute("CGroupe")]
//        [Bindable(true)]
//        public string CGroupe { get; set; }

//        [XmlAttribute("Email")]
//        [Bindable(true)]
//        public string Email { get; set; }

//        [XmlAttribute("Fonction")]
//        [Bindable(true)]
//        public string Fonction { get; set; }

//        [XmlAttribute("GSM")]
//        [Bindable(true)]
//        public string GSM { get; set; }

//        [XmlAttribute("Identifiant")]
//        [Bindable(true)]
//        public int Identifiant { get; set; }

//        [XmlAttribute("Nom")]
//        [Bindable(true)]
//        public string Nom { get; set; }

//        [XmlAttribute("Prenom")]
//        [Bindable(true)]
//        public string Prenom { get; set; }

//        [XmlAttribute("MotDePasse")]
//        [Bindable(true)]
//        public string MotDePasse { get; set; }

//        [XmlAttribute("NumeroTelephone")]
//        [Bindable(true)]
//        public string NumeroTelephone { get; set; }

//        [XmlAttribute("Adresse")]
//        [Bindable(true)]
//        public string Adresse { get; set; }

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

//        public Utilisateur1()
//        {
//            BAdministrateur = true;
//        }

//    }
//}