using CST.LePoint.Referentiel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CST.LePoint.Tiers.Metier
{
    [Serializable]
    public class ReleveurCollection : ItemCollection
    {
        public ReleveurCollection()
        {
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
                cmd.CommandText = "Ref_Releveur_Charger";
                cmd.Parameters.AddWithValue("@CReleveur", DBNull.Value);
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

        public static ReleveurCollection Charger()
        {
            ReleveurCollection collection = new ReleveurCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_Releveur_Charger";
                    cmd.Parameters.Add(new SqlParameter("@CReleveur", DBNull.Value));

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Releveur releveur = new Releveur();

                            releveur.CReleveur = dr["CReleveur"].ToString().Trim();
                            if (dr["Nom"] != DBNull.Value)
                                releveur.Nom = dr["Nom"].ToString().Trim();
                            if (dr["Prenom"] != DBNull.Value)
                                releveur.Prenom = dr["Prenom"].ToString().Trim();
                            releveur.Code = releveur.CReleveur.ToString();
                            releveur.Libelle = String.Format("{0} {1}", releveur.Nom, releveur.Prenom);
                            collection.Add(releveur);
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
    //********************
    [Serializable]
    public class Releveur : Item
    {
        #region Propriétés

        [XmlAttribute("CRevendeur")]
        [Bindable(true)]
        public string CReleveur { get; set; }

        [XmlAttribute("Nom")]
        [Bindable(true)]
        public string Nom { get; set; }

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

        #endregion

        public Releveur()
        {
            this.CReleveur = string.Empty;
            this.Nom = string.Empty;
            this.Prenom = string.Empty;
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
                    cmd.CommandText = "Ref_Releveur_Sauvegarder";

                    cmd.Parameters.Add(new SqlParameter("@CReleveur", CReleveur));
                    cmd.Parameters.Add(new SqlParameter("@Nom", Nom));
                    cmd.Parameters.Add(new SqlParameter("@Prenom", Prenom));
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
            catch (Exception)
            {
                throw;
            }
        }

        public static Releveur Charger(string cReleveur)
        {
            Releveur releveur = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_Releveur_Charger";
                    cmd.Parameters.Add(new SqlParameter("@CReleveur", cReleveur));

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            releveur = new Releveur();

                            releveur.CReleveur = dr["CReleveur"].ToString().Trim();
                            if (dr["Nom"] != DBNull.Value)
                                releveur.Nom = dr["Nom"].ToString().Trim();
                            if (dr["Prenom"] != DBNull.Value)
                                releveur.Prenom = dr["Prenom"].ToString().Trim();
                        }
                        dr.Close();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return releveur;
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
                    cmd.CommandText = "Ref_Releveur_Supprimer";
                    cmd.Parameters.Add(new SqlParameter("@CReleveur", CReleveur));
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
            catch (Exception)
            {
                throw;
            }
        }
    }
}
