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
    public class FournisseurFamille : Item
    {
        #region Propriétés

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

        public FournisseurFamille()
        {
            this.DateInsertion = DateTime.Now;
            this.DateModification = DateTime.Now;
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
                    cmd.CommandText = "FournisseurFamille_Sauvegarder";
                    cmd.Parameters.AddWithValue("@CFournisseurFamille ", Code);
                    cmd.Parameters.AddWithValue("@LibFournisseurFamille", Libelle);
                    cmd.Parameters.AddWithValue("@CreePar", CreePar);
                    cmd.Parameters.AddWithValue("@ModifiePar", ModifiePar);
                    cmd.Parameters.AddWithValue("@PCInsertion", PCInsertion);
                    cmd.Parameters.AddWithValue("@PCModification", PCModification);
                    cmd.Parameters.AddWithValue("@DateInsertion", DateInsertion);
                    cmd.Parameters.AddWithValue("@DateModification", DateModification);

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

        public static FournisseurFamille Charger(string cFournisseurFamille)
        {
            FournisseurFamille FournisseurFamille = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "FournisseurFamille_Charger";
                    cmd.Parameters.AddWithValue("@CFournisseurFamille", cFournisseurFamille);
                    foreach (SqlParameter parametre in cmd.Parameters)
                    {
                        if (parametre.Value == null)
                        {
                            parametre.Value = DBNull.Value;
                        }
                    }

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            FournisseurFamille = new FournisseurFamille();

                            FournisseurFamille.Code = dr["CFournisseurFamille"].ToString();
                            if (dr["LibFournisseurFamille"] != DBNull.Value)
                                FournisseurFamille.Libelle = dr["LibFournisseurFamille"].ToString();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return FournisseurFamille;
        }

        public static FournisseurFamille Charger()
        {
            FournisseurFamille FournisseurFamille = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "FournisseurFamille_ChargerLibille";
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            FournisseurFamille = new FournisseurFamille();

                            FournisseurFamille.Code = dr["CFournisseurFamille"].ToString();
                            if (dr["LibFournisseurFamille"] != DBNull.Value)
                                FournisseurFamille.Libelle = dr["LibFournisseurFamille"].ToString();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return FournisseurFamille;
        }

        public void Supprimer()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = cn;
                    cmd.CommandText = "FournisseurFamille_Supprimer";
                    cmd.Parameters.AddWithValue("@CFournisseurFamille ", Code);

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
    public class FournisseurFamilleCollection : ItemCollection
    {
        public FournisseurFamilleCollection()
        {
        }

        public static DataSet ChargerVue(string cFournisseurFamille)
        {
            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "RptFournisseurFamille_Charger";
                cmd.Parameters.AddWithValue("@CFournisseurFamille", cFournisseurFamille);

                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "RptFournisseurFamille_Charger");
            }
            return (ds);
        }

        public static DataTable RemplirGrid(string cFournisseurFamille)
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "FournisseurFamille_Charger";
                cmd.Parameters.AddWithValue("@CFournisseurFamille", cFournisseurFamille);

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

        public static FournisseurFamilleCollection Charger()
        {
            FournisseurFamilleCollection collection = new FournisseurFamilleCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "FournisseurFamille_Charger";
                    cmd.Parameters.AddWithValue("@CFournisseurFamille", DBNull.Value);
                    foreach (SqlParameter parametre in cmd.Parameters)
                    {
                        if (parametre.Value == null)
                        {
                            parametre.Value = DBNull.Value;
                        }
                    }

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            FournisseurFamille fnr = new FournisseurFamille();

                            fnr.Code = dr["CFournisseurFamille"].ToString();
                            if (dr["LibFournisseurFamille"] != DBNull.Value)
                                fnr.Libelle = dr["LibFournisseurFamille"].ToString();
                            collection.Add(fnr);
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
}