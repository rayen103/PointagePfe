using CST.LePoint.Referentiel;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

//using CST.Framework;
using System.Xml.Serialization;

namespace CST.LePoint.Stock.Referentiel.Commun
{
    [Serializable]
    public class Entrepot : Item
    {
        #region Propriétés

        [XmlAttribute("AdresseEntrepot")]
        [Bindable(true)]
        public string AdresseEntrepot { get; set; }

        [XmlAttribute("BParDefault")]
        [Bindable(true)]
        public bool BParDefault { get; set; }

        [XmlAttribute("TypeEntrepot")]
        [Bindable(true)]
        public string TypeEntrepot { get; set; }

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

        [XmlAttribute("BLivrable")]
        [Bindable(true)]
        public bool BLivrable { get; set; }

        #endregion Propriétés

        public Entrepot()
        {
        }

        public void Sauvegarder()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_Entrepot_Sauvegarder";
                    cmd.Parameters.AddWithValue("@CEntrepot", this.Code);
                    cmd.Parameters.AddWithValue("@LibEntrepot", this.Libelle);
                    cmd.Parameters.AddWithValue("@AdresseEntrepot", AdresseEntrepot);
                    cmd.Parameters.AddWithValue("@BParDefault", BParDefault);
                    cmd.Parameters.AddWithValue("@BLivrable", BLivrable);
                    cmd.Parameters.AddWithValue("@TypeEntrepot", TypeEntrepot);
                    cmd.Parameters.AddWithValue("@CreePar", CreePar);
                    cmd.Parameters.AddWithValue("@ModifiePar", ModifiePar);
                    cmd.Parameters.AddWithValue("@PCInsertion", PCInsertion);
                    cmd.Parameters.AddWithValue("@PCModification", PCModification);
                    cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                    cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
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
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_Entrepot_Supprimer";
                    cmd.Parameters.AddWithValue("@CEntrepot", this.Code);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static Entrepot Charger(string cEntrepot)
        {
            Entrepot entrepot = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_Entrepot_Charger";
                    cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
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
                            entrepot = new Entrepot();
                            entrepot.Code = cEntrepot;
                            if (dr["AdresseEntrepot"] != DBNull.Value)
                                entrepot.AdresseEntrepot = dr["AdresseEntrepot"].ToString();
                            if (dr["BParDefault"] != DBNull.Value)
                                entrepot.BParDefault = bool.Parse(dr["BParDefault"].ToString());
                            if (dr["BLivrable"] != DBNull.Value)
                                entrepot.BLivrable = bool.Parse(dr["BLivrable"].ToString());
                            if (dr["LibEntrepot"] != DBNull.Value)
                                entrepot.Libelle = dr["LibEntrepot"].ToString();
                            if (dr["TypeEntrepot"] != DBNull.Value)
                                entrepot.TypeEntrepot = dr["TypeEntrepot"].ToString();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return entrepot;
        }
    }

    [Serializable]
    public class EntrepotCollection : ItemCollection
    {
        public static DataSet ChargerVue()
        {
            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ListeEntrepot_Rpt_Charger";
                cmd.Parameters.AddWithValue("@CEntrepot", DBNull.Value);
                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "ListeEntrepot_Rpt_Charger");
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
                cmd.CommandText = "Ref_Entrepot_Charger";
                cmd.Parameters.AddWithValue("@CEntrepot", DBNull.Value);
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

        public static EntrepotCollection Charger()
        {
            EntrepotCollection Entrepotcollection = new EntrepotCollection();
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Ref_Entrepot_Charger";
                cmd.Parameters.AddWithValue("@CEntrepot", DBNull.Value);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Entrepot Entrepot = new Entrepot();
                    Entrepot.Code = dr["CEntrepot"].ToString();
                    if (dr["LibEntrepot"] != DBNull.Value)
                        Entrepot.Libelle = dr["LibEntrepot"].ToString();
                    if (dr["AdresseEntrepot"] != DBNull.Value)
                        Entrepot.AdresseEntrepot = dr["AdresseEntrepot"].ToString();
                    if (dr["TypeEntrepot"] != DBNull.Value)
                        Entrepot.TypeEntrepot = dr["TypeEntrepot"].ToString();
                    if (dr["BParDefault"] != DBNull.Value)
                        Entrepot.BParDefault = bool.Parse(dr["BParDefault"].ToString());
                    if (dr["BLivrable"] != DBNull.Value)
                        Entrepot.BLivrable = bool.Parse(dr["BLivrable"].ToString());
                    Entrepotcollection.Add(Entrepot);
                }
                dr.Close();

                return (Entrepotcollection);
            }
        }
    }
}