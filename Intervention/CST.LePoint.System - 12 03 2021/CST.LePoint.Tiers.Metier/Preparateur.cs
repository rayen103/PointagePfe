using CST.LePoint.Referentiel;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace CST.LePoint.Tiers.Metier
{
    [Serializable]
    public class PreparateurCollection : ItemCollection
    {
        public PreparateurCollection()
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
                cmd.CommandText = "Preparateur_Rpt_Charger";
                cmd.Parameters.AddWithValue("@CPreparateur", DBNull.Value);
                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "Preparateur_Rpt_Charger");
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
                cmd.CommandText = "Ref_Preparateur_Charger";
                cmd.Parameters.AddWithValue("@CPreparateur", DBNull.Value);
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

        public static PreparateurCollection Charger()
        {
            PreparateurCollection collection = new PreparateurCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_Preparateur_Charger";
                    cmd.Parameters.Add(new SqlParameter("@CPreparateur", DBNull.Value));

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Preparateur Preparateur = new Preparateur();

                            Preparateur.CPreparateur = int.Parse(dr["CPreparateur"].ToString().Trim());
                            if (dr["Email"] != DBNull.Value)
                                Preparateur.Email = dr["Email"].ToString().Trim();
                            if (dr["Nom"] != DBNull.Value)
                                Preparateur.Nom = dr["Nom"].ToString().Trim();
                            if (dr["Portable"] != DBNull.Value)
                                Preparateur.Portable = dr["Portable"].ToString().Trim();
                            if (dr["Prenom"] != DBNull.Value)
                                Preparateur.Prenom = dr["Prenom"].ToString().Trim();
                            if (dr["Telephone"] != DBNull.Value)
                                Preparateur.Telephone = dr["Telephone"].ToString().Trim();
                            Preparateur.Code = Preparateur.CPreparateur.ToString();
                            Preparateur.Libelle = String.Format("{0} {1}", Preparateur.Nom, Preparateur.Prenom);
                            collection.Add(Preparateur);
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

        //public Preparateur Obtenir(int cPreparateur)
        //{
        //    Preparateur Preparateur = this.Where(x => x.CPreparateur.Equals(cPreparateur)).FirstOrDefault();
        //    return Preparateur;
        //}
    }

    [Serializable]
    public class Preparateur : Item
    {
        #region Propriétés

        [XmlAttribute("CPreparateur")]
        [Bindable(true)]
        public int CPreparateur { get; set; }

        [XmlAttribute("Nom")]
        [Bindable(true)]
        public string Nom { get; set; }

        [XmlAttribute("Prenom")]
        [Bindable(true)]
        public string Prenom { get; set; }

        [XmlAttribute("Telephone")]
        [Bindable(true)]
        public string Telephone { get; set; }

        [XmlAttribute("Portable")]
        [Bindable(true)]
        public string Portable { get; set; }

        [XmlAttribute("Email")]
        [Bindable(true)]
        public string Email { get; set; }

        [XmlAttribute("CUtilisateur")]
        [Bindable(true)]
        public string CUtilisateur { get; set; }

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

        public Preparateur()
        {
            this.CPreparateur = 0;
            this.Nom = string.Empty;
            this.Prenom = string.Empty;
            this.Portable = string.Empty;
            this.Telephone = string.Empty;
            this.Email = string.Empty;
            this.CUtilisateur = string.Empty; ;
            //this.DateInsertion = DateTime.Now;
            //this.DateModification = DateTime.Now;
        }

        public static Preparateur Charger(int cPreparateur)
        {
            Preparateur Preparateur = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_Preparateur_Charger";
                    cmd.Parameters.Add(new SqlParameter("@CPreparateur", cPreparateur));

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Preparateur = new Preparateur();

                            Preparateur.CPreparateur = int.Parse(dr["CPreparateur"].ToString().Trim());
                            if (dr["Email"] != DBNull.Value)
                                Preparateur.Email = dr["Email"].ToString().Trim();
                            if (dr["Nom"] != DBNull.Value)
                                Preparateur.Nom = dr["Nom"].ToString().Trim();
                            if (dr["CUtilisateur"] != DBNull.Value)
                                Preparateur.CUtilisateur = dr["CUtilisateur"].ToString().Trim();
                            if (dr["Portable"] != DBNull.Value)
                                Preparateur.Portable = dr["Portable"].ToString().Trim();
                            if (dr["Prenom"] != DBNull.Value)
                                Preparateur.Prenom = dr["Prenom"].ToString().Trim();
                            if (dr["Telephone"] != DBNull.Value)
                                Preparateur.Telephone = dr["Telephone"].ToString().Trim();
                        }
                        dr.Close();
                    }
                }
            }
            catch (Exception )
            {
                throw;
            }

            return Preparateur;
        }

        public void Sauvegarder()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_Preparateur_Sauvegarder";

                    cmd.Parameters.Add(new SqlParameter("@CPreparateur", CPreparateur));
                    cmd.Parameters.Add(new SqlParameter("@Email", Email));
                    cmd.Parameters.Add(new SqlParameter("@Nom", Nom));
                    cmd.Parameters.Add(new SqlParameter("@CUtilisateur", CUtilisateur));
                    cmd.Parameters.Add(new SqlParameter("@Portable", Portable));
                    cmd.Parameters.Add(new SqlParameter("@Prenom", Prenom));
                    cmd.Parameters.Add(new SqlParameter("@Telephone", Telephone));
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
                }
            }
            catch (Exception )
            {
                throw;
            }
        }

        public void Supprimer()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_Preparateur_Supprimer";
                    cmd.Parameters.Add(new SqlParameter("@CPreparateur", CPreparateur));
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
            }
            catch (Exception )
            {
                throw;
            }
        }
    }
}