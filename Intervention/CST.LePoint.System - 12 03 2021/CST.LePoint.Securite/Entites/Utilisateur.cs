using CST.LePoint.Securite.Entites;
using CST.LePoint.Tools;
//using CST.LePoint.LibShare;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization;

namespace CST.LePoint.Securite.Entites
{
    [DataContract(Namespace = "")]
    public class Utilisateur
    {
        public Utilisateur()
        {
            this.Roles = new HashSetSerializable<Role>();
        }

        [DataMember]
        public int IdUtilisateur { get; set; }

        [DataMember]
        public string CSociete { get; set; }

        [DataMember]
        public string Login { get; set; }

        [DataMember]
        public string Nom { get; set; }

        [DataMember]
        public string Prenom { get; set; }

        [IgnoreDataMember]
        public string MotDePasse
        {
            set
            {
                MotDePasseCry = SysHelper.CalculateSHA1(value);
            }
        }

        [DataMember]
        public string MotDePasseCry { get; set; }

        [DataMember]
        public string NTelephone { get; set; }

        [DataMember]
        public string Adresse { get; set; }

        [DataMember]
        public bool BAdministrateur { get; set; }

        [DataMember]
        public string CGroupeUtilisateur { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Fonction { get; set; }

        [DataMember]
        public string NMobile { get; set; }

        [DataMember]
        public string CRole { get; set; }

        [DataMember]
        public virtual HashSetSerializable<Role> Roles { get; set; }

        public bool Equals(Utilisateur other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Login == Login;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Utilisateur)) return false;
            return Equals((Utilisateur)obj);
        }

        public override int GetHashCode()
        {
            return Login.GetHashCode();
        }

        public override string ToString()
        {
            return Login;
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
                    cmd.CommandText = "Utilisateur_Sauvegarder";
                    cmd.Parameters.AddWithValue("@CSociete", this.CSociete);
                    cmd.Parameters.AddWithValue("@CUtilisateur", Login);
                    cmd.Parameters.AddWithValue("@BAdministrateur", BAdministrateur);
                    cmd.Parameters.AddWithValue("@CGroupe", CGroupeUtilisateur);
                    cmd.Parameters.AddWithValue("@Email", Email);
                    cmd.Parameters.AddWithValue("@Fonction", Fonction);
                    cmd.Parameters.AddWithValue("@GSM", NMobile);
                    cmd.Parameters.AddWithValue("@Nom", Nom);
                    cmd.Parameters.AddWithValue("@Prenom", Prenom);

                    if (!string.IsNullOrEmpty(this.MotDePasseCry))
                        cmd.Parameters.AddWithValue("@MotDePasse", this.MotDePasseCry);
                    else
                        cmd.Parameters.AddWithValue("@MotDePasse", DBNull.Value);

                    cmd.Parameters.AddWithValue("@NumeroTelephone", NTelephone);
                    cmd.Parameters.AddWithValue("@Adresse", Adresse);
                    cmd.Parameters.AddWithValue("@CRole", CRole);

                    //cmd.Parameters.AddWithValue("@IdUtilisateur", IdUtilisateur);



                    cmd.Parameters.AddWithValue("@CreePar", string.Empty);
                    cmd.Parameters.AddWithValue("@ModifiePar", string.Empty);
                    cmd.Parameters.AddWithValue("@PCInsertion", string.Empty);
                    cmd.Parameters.AddWithValue("@PCModification", string.Empty);
                    cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                    cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                   // cmd.ExecuteNonQuery();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            if (dr["IdUtilisateur"] != DBNull.Value)
                                IdUtilisateur = int.Parse(dr["IdUtilisateur"].ToString()) ;

                        }
                    }
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
                    cmd.Connection = transaction.Connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Utilisateur_Supprimer";
                    cmd.Parameters.AddWithValue("@CUtilisateur", Login);
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

        public static Utilisateur Charger(string CUtilisateur)
        {
            Utilisateur u = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Utilisateur_Charger";
                    cmd.Parameters.AddWithValue("@CUtilisateur", CUtilisateur);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            u = new Utilisateur();
                            u.Login = dr["CUtilisateur"].ToString();
                            if (dr["BAdministrateur"] != DBNull.Value)
                                u.BAdministrateur = bool.Parse(dr["BAdministrateur"].ToString());
                            if (dr["CGroupe"] != DBNull.Value)
                                u.CGroupeUtilisateur = dr["CGroupe"].ToString();
                            if (dr["Email"] != DBNull.Value)
                                u.Email = dr["Email"].ToString();
                            if (dr["Fonction"] != DBNull.Value)
                                u.Fonction = dr["Fonction"].ToString();
                            if (dr["GSM"] != DBNull.Value)
                                u.NMobile = dr["GSM"].ToString();
                            if (dr["Nom"] != DBNull.Value)
                                u.Nom = dr["Nom"].ToString();
                            if (dr["Prenom"] != DBNull.Value)
                                u.Prenom = dr["Prenom"].ToString();
                            if (dr["MotDePasse"] != DBNull.Value)
                                u.MotDePasseCry =dr["MotDePasse"].ToString();
                            if (dr["NumeroTelephone"] != DBNull.Value)
                                u.NTelephone = dr["NumeroTelephone"].ToString();
                            if (dr["Adresse"] != DBNull.Value)
                                u.Adresse = dr["Adresse"].ToString();

                            if (dr["CRole"] != DBNull.Value)
                                u.CRole = dr["CRole"].ToString();

                            if (dr["IdUtilisateur"] != DBNull.Value)
                                u.IdUtilisateur = int.Parse(dr["IdUtilisateur"].ToString());

                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return u;
        }

        public static Utilisateur Charger(string CUtilisateur, string CSociete)
        {
            Utilisateur u = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "Select TOP 1 * from Utilisateur (NOLOCK) where CSociete='" + CSociete + "' and CUtilisateur='" + CUtilisateur + "'";

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            u = new Utilisateur();
                            u.Login = dr["CUtilisateur"].ToString();
                            u.CSociete = dr["CSociete"].ToString();
                            u.MotDePasseCry = dr["MotDePasse"].ToString();
                            if (dr["BAdministrateur"] != DBNull.Value)
                                u.BAdministrateur = bool.Parse(dr["BAdministrateur"].ToString());
                            if (dr["CGroupe"] != DBNull.Value)
                                u.CGroupeUtilisateur = dr["CGroupe"].ToString();
                            if (dr["Email"] != DBNull.Value)
                                u.Email = dr["Email"].ToString();
                            if (dr["Fonction"] != DBNull.Value)
                                u.Fonction = dr["Fonction"].ToString();
                            if (dr["GSM"] != DBNull.Value)
                                u.NMobile = dr["GSM"].ToString();
                            if (dr["Nom"] != DBNull.Value)
                                u.Nom = dr["Nom"].ToString();
                            if (dr["Prenom"] != DBNull.Value)
                                u.Prenom = dr["Prenom"].ToString();
                            if (dr["NumeroTelephone"] != DBNull.Value)
                                u.NTelephone = dr["NumeroTelephone"].ToString();
                            if (dr["Adresse"] != DBNull.Value)
                                u.Adresse = dr["Adresse"].ToString();
                            if (dr["CRole"] != DBNull.Value)
                                u.CRole = dr["CRole"].ToString();
                            if (dr["IdUtilisateur"] != DBNull.Value)
                                u.IdUtilisateur = int.Parse(dr["IdUtilisateur"].ToString());
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return u;
        }
    }

    [CollectionDataContract(Namespace = "")]
    public class Utilisateurs : HashSetSerializable<Utilisateur>
    {
    }
}