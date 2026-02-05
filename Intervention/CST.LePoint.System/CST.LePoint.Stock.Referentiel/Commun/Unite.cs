using CST.LePoint.Referentiel;
using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

//using CST.Framework;

namespace CST.LePoint.Stock.Referentiel.Commun
{
    [Serializable]
    public class Unite : Item
    {
        #region Propriétés

        [XmlAttribute("NombreDecimaleUnite")]
        [Bindable(true)]
        public decimal NombreDecimaleUnite { get; set; }

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

        public Unite()
        {
            this.Code = string.Empty;
            this.Libelle = string.Empty;
        }

        public void Sauvegarder()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Ref_Unite_Sauvegarder";
                cmd.Parameters.AddWithValue("@CUnite", Code);
                cmd.Parameters.AddWithValue("@LibUnite", Libelle);
                cmd.Parameters.AddWithValue("@NombreDecimaleUnite", NombreDecimaleUnite);
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

        public void Supprimer()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Ref_Unite_Supprimer";
                cmd.Parameters.AddWithValue("@CUnite", Code);

                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                cmd.ExecuteNonQuery();
            }
        }

        public static Unite Charger(string cUnite)
        {
            Unite unite = null;
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Ref_Unite_Charger";
                cmd.Parameters.AddWithValue("@CUnite", cUnite);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    unite = new Unite();
                    unite.Code = dr["CUnite"].ToString();
                    if (dr["LibUnite"] != DBNull.Value)
                        unite.Libelle = dr["LibUnite"].ToString();
                    if (dr["NombreDecimaleUnite"] != DBNull.Value)
                        unite.NombreDecimaleUnite = decimal.Parse(dr["NombreDecimaleUnite"].ToString());
                }
            }
            return (unite);
        }
    }

    [Serializable]
    public class UniteCollection : ItemCollection, IEnumerable
    {
        public static UniteCollection Charger()
        {
            UniteCollection uniteCollection = new UniteCollection();

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Ref_Unite_Charger";
                cmd.Parameters.AddWithValue("@CUnite", DBNull.Value);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Unite unite = new Unite();

                    unite.Code = dr["CUnite"].ToString();
                    if (dr["LibUnite"] != DBNull.Value)
                        unite.Libelle = dr["LibUnite"].ToString();
                    if (dr["NombreDecimaleUnite"] != DBNull.Value)
                        unite.NombreDecimaleUnite = int.Parse(dr["NombreDecimaleUnite"].ToString());

                    uniteCollection.Add(unite);
                }

                dr.Close();

                return (uniteCollection);
            }
        }

        public static DataSet ChargerVue(string cUnite)
        {
            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Unite_Rpt_Charger";
                cmd.Parameters.AddWithValue("@CUnite", cUnite);
                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);
            }

            ds.Tables[0].TableName = "Unite_Rpt_Charger";

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
                cmd.CommandText = "Ref_Unite_Charger";
                cmd.Parameters.AddWithValue("@CUnite", DBNull.Value);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }

            return (dt);
        }
    }
}