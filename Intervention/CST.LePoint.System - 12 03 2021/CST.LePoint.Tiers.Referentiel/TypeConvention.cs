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

namespace CST.LePoint.Tiers.Referentiel
{
    [Serializable]
    public class TypeConvention : Item
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

        /// <summary>
        /// Constructeur TypeConvention
        /// </summary>
        public TypeConvention()
        { }

        public TypeConvention(string cTypeConvention)
            : this()
        {
            Code = cTypeConvention;
        }

        public TypeConvention(string cTypeConvention, string libTypeConvention)
        {
            Code = cTypeConvention;
            Libelle = libTypeConvention;
        }

        /// <summary>
        /// Ajout ou modification d'une TypeConvention
        /// </summary>
        public void Sauvegarder()
        {
            try
            {
                using (SqlConnection connexion = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    connexion.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = connexion;

                    cmd.CommandText = "Ref_ConventionType_Sauvegarder";
                    cmd.Parameters.AddWithValue("CTypeConvention", Code);
                    cmd.Parameters.AddWithValue("LibTypeConvention", Libelle);

                    cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                    cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                    cmd.Parameters.AddWithValue("@CreePar", CreePar);
                    cmd.Parameters.AddWithValue("@ModifiePar", ModifiePar);
                    cmd.Parameters.AddWithValue("@PCInsertion", PCInsertion);
                    cmd.Parameters.AddWithValue("@PCModification", PCModification);

                    foreach (SqlParameter oleDbParametre in cmd.Parameters)
                    {
                        if (oleDbParametre.Value == null)
                        {
                            oleDbParametre.Value = DBNull.Value;
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

        /// <summary>
        /// Suppression d'une TypeConvention
        /// </summary>
        public static bool Supprimer(String cTypeConvention)
        {
            var carton = false;
            try
            {
                using (SqlConnection connexion = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    connexion.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = connexion;
                    cmd.CommandText = "Ref_ConventionType_Supprimer";
                    cmd.Parameters.AddWithValue("CTypeConvention", cTypeConvention);

                    foreach (SqlParameter oleDbParametre in cmd.Parameters)
                    {
                        if (oleDbParametre.Value == null)
                        {
                            oleDbParametre.Value = DBNull.Value;
                        }
                    }
                    cmd.ExecuteNonQuery();
                    carton = true;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return carton;
        }


        public static TypeConvention Charger(String cTypeConvention)
        {
            TypeConvention typeConvention = null;
            try
            {
                using (SqlConnection connexion = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    connexion.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = connexion;
                    cmd.CommandText = "Ref_ConventionType_Charger";
                    cmd.Parameters.AddWithValue("CTypeConvention", cTypeConvention);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            typeConvention = new TypeConvention();
                            if (reader["CTypeConvention"] != DBNull.Value)
                                typeConvention.Code = reader["CTypeConvention"].ToString();
                            if (reader["LibTypeConvention"] != DBNull.Value)
                                typeConvention.Libelle = reader["LibTypeConvention"].ToString();

                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return typeConvention;
        }
    }

    [Serializable]
    public class TypeConventionCollection : ItemCollection
    {
        public TypeConventionCollection()
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
                cmd.CommandText = "ConventionType_Rpt_Charger";
                cmd.Parameters.AddWithValue("@CTypeConvention", DBNull.Value);

                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "ConventionType_Rpt_Charger");
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
                cmd.CommandText = "Ref_ConventionType_Charger";
                cmd.Parameters.AddWithValue("@CTypeConvention", DBNull.Value);

                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            return (dt);
        }

        public static TypeConventionCollection Charger()
        {
            TypeConventionCollection collection = new TypeConventionCollection();
            TypeConvention typeConvention = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_ConventionType_Charger";
                    cmd.Parameters.Add(new SqlParameter("@CTypeConvention", DBNull.Value));

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            typeConvention = new TypeConvention();
                            if (dr["CTypeConvention"] != DBNull.Value)
                                typeConvention.Code = dr["CTypeConvention"].ToString();
                            if (dr["LibTypeConvention"] != DBNull.Value)
                                typeConvention.Libelle = dr["LibTypeConvention"].ToString();

                            collection.Add(typeConvention);
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
