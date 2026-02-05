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

namespace CST.LePoint.Intervention.Metier
{

    [Serializable]
    public class Equipe : Item
    {
        #region Propriétés

        [XmlAttribute("CEquipe")]
        [Bindable(true)]
        public string CEquipe { get; set; }

        //[XmlAttribute("Libelle")]
        //[Bindable(true)]
        //public string Libelle { get; set; }

        [XmlAttribute("CClient")]
        [Bindable(true)]
        public string CClient { get; set; }

        [XmlAttribute("CEntrepot")]
        [Bindable(true)]
        public string CEntrepot { get; set; }

        [XmlAttribute("CTarif")]
        [Bindable(true)]
        public string CTarif { get; set; }

        [XmlAttribute("CFournisseur")]
        [Bindable(true)]
        public string CFournisseur { get; set; }

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
        [XmlAttribute("Responsable")]
        [Bindable(true)]
        public string Responsable { get; set; }
       
        [XmlAttribute("BInterne")]
        [Bindable(true)]
        public bool BInterne { get; set; }
        [XmlAttribute("CVehicule")]
        [Bindable(true)]
        public string CVehicule { get; set; }

        #endregion Propriétés
        
        public Equipe()
        {
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
                    cmd.CommandText = "GP_Equipe_Sauvegarder";

                    cmd.Parameters.AddWithValue("@CEquipe", this.CEquipe);
                    cmd.Parameters.AddWithValue("@Libelle", this.Libelle);
                    cmd.Parameters.AddWithValue("@CClient", CClient);
                    cmd.Parameters.AddWithValue("@CEntrepot", CEntrepot);
                    cmd.Parameters.AddWithValue("@CTarif", CTarif);
                    cmd.Parameters.AddWithValue("@CFournisseur", CFournisseur);
                 
                    cmd.Parameters.AddWithValue("@CreePar", CreePar);
                    cmd.Parameters.AddWithValue("@ModifiePar", ModifiePar);
                    cmd.Parameters.AddWithValue("@PCInsertion", PCInsertion);
                    cmd.Parameters.AddWithValue("@PCModification", PCModification);
                    cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                    cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Responsable", Responsable);
                    cmd.Parameters.AddWithValue("@BInterne", BInterne);
                    cmd.Parameters.AddWithValue("@CVehicule", CVehicule);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    cmd.ExecuteNonQuery();



                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public static void ModifierResp(string equipe, string code)
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
                    cmd.CommandType = CommandType.Text;
                    // decimal resultat = qté - fait;
                    cmd.CommandText = "update GP_Equipe set Responsable = '" + code + "'   where CEquipe = '" + equipe + "' ";
                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public static void ModifierResp(SqlTransaction transaction, string equipe, string code)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Transaction = transaction;
            cmd.Connection = transaction.Connection;
            cmd.CommandType = CommandType.Text;
            // decimal resultat = qté - fait;
            cmd.CommandText = "update GP_Equipe set Responsable = '" + code + "'   where CEquipe = '" + equipe + "' ";
            cmd.ExecuteNonQuery();
        }

        public static void ModifierResp(SqlTransaction transaction, string code)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Transaction = transaction;
            cmd.Connection = transaction.Connection;
            cmd.CommandType = CommandType.Text;
            // decimal resultat = qté - fait;
            cmd.CommandText = "update GP_Equipe set Responsable = 'NULL'   where Responsable = '" + code + "' ";
            cmd.ExecuteNonQuery();
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
                    cmd.CommandText = "GP_Equipe_Supprimer";
                    cmd.Parameters.AddWithValue("@CEquipe", CEquipe);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static Equipe Charger(string CEquipe)
        {
            Equipe equipe = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "GP_Equipe_Charger";
                    cmd.Parameters.AddWithValue("@CEquipe", CEquipe);
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
                            equipe = new Equipe();
                            equipe.CEquipe = dr["CEquipe"].ToString();
                            equipe.Libelle = dr["Libelle"].ToString();
                            equipe.CClient = dr["CClient"].ToString();
                            equipe.CEntrepot = dr["CEntrepot"].ToString();
                            equipe.CTarif = dr["CTarif"].ToString();
                            equipe.CFournisseur = dr["CFournisseur"].ToString();
                            if (dr["Responsable"] != DBNull.Value)
                                equipe.Responsable = dr["Responsable"].ToString();
                            if (dr["BInterne"] != DBNull.Value)
                                equipe.BInterne = bool.Parse(dr["BInterne"].ToString());
                            if (dr["CVehicule"] != DBNull.Value)
                                equipe.CVehicule = dr["CVehicule"].ToString();
                           
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return equipe;
        }
        
        public static Equipe ChargerParCircuit(string CCircuit)
        {
            Equipe equipe = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "GP_Equipe_ChargerParCircuit";
                    cmd.Parameters.AddWithValue("@CCircuit", CCircuit);
                    foreach (SqlParameter parametre in cmd.Parameters)                    
                        if (parametre.Value == null)                        
                            parametre.Value = DBNull.Value;                      
                    
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            equipe = new Equipe();
                            equipe.CEquipe = dr["CEquipe"].ToString();
                            equipe.Libelle = dr["Libelle"].ToString();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return equipe;
        }
    }

    [Serializable]
    public class EquipeCollection : ItemCollection
    {
        //public static DataSet ChargerVue()
        //{
        //    DataSet ds = new DataSet();

        //    using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
        //    {
        //        cn.Open();
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Connection = cn;
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = "ListeEntrepot_Rpt_Charger";
        //        cmd.Parameters.AddWithValue("@CEntrepot", DBNull.Value);
        //        foreach (SqlParameter parametre in cmd.Parameters)
        //        {
        //            if (parametre.Value == null)
        //            {
        //                parametre.Value = DBNull.Value;
        //            }
        //        }
        //        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        //        sda.Fill(ds, "ListeEntrepot_Rpt_Charger");
        //    }
        //    return (ds);
        //}

        //public static DataTable RemplirGrid()
        //{
        //    DataTable dt = new DataTable();

        //    using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
        //    {
        //        cn.Open();
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Connection = cn;
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = "Ref_Entrepot_Charger";
        //        cmd.Parameters.AddWithValue("@CEntrepot", DBNull.Value);
        //        foreach (SqlParameter parametre in cmd.Parameters)
        //        {
        //            if (parametre.Value == null)
        //            {
        //                parametre.Value = DBNull.Value;
        //            }
        //        }
        //        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //        adapter.Fill(dt);
        //    }
        //    return (dt);
        //}

        public static EquipeCollection Charger()
        {
            EquipeCollection collection = new EquipeCollection();
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GP_Equipe_Charger";
                cmd.Parameters.AddWithValue("@CEquipe", DBNull.Value);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Equipe equipe = new Equipe();
                    equipe.Code = dr["CEquipe"].ToString();
                    equipe.Libelle = dr["Libelle"].ToString();

                    equipe.CClient = dr["CClient"].ToString();
                    equipe.CEntrepot = dr["CEntrepot"].ToString();
                    equipe.CTarif = dr["CTarif"].ToString();
                    equipe.CFournisseur = dr["CFournisseur"].ToString();
                    if (dr["Responsable"] != DBNull.Value)
                        equipe.Responsable = dr["Responsable"].ToString();
                    if (dr["BInterne"] != DBNull.Value)
                        equipe.BInterne = bool.Parse(dr["BInterne"].ToString());
                    if (dr["CVehicule"] != DBNull.Value)
                        equipe.CVehicule = dr["CVehicule"].ToString();
                    collection.Add(equipe);
                }
                dr.Close();

                return (collection);
            }
        }

        public static EquipeCollection Charger(string CEquipe)
        {
            EquipeCollection collection = new EquipeCollection();
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GP_Equipe_Charger";
                cmd.Parameters.AddWithValue("@CEquipe", CEquipe);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Equipe equipe = new Equipe();
                    equipe.CEquipe = dr["CEquipe"].ToString();
                    equipe.Libelle = dr["Libelle"].ToString();

                    equipe.CClient = dr["CClient"].ToString();
                    equipe.CEntrepot = dr["CEntrepot"].ToString();
                    equipe.CTarif = dr["CTarif"].ToString();
                    equipe.CFournisseur = dr["CFournisseur"].ToString();
                    if (dr["Responsable"] != DBNull.Value)
                        equipe.Responsable = dr["Responsable"].ToString();
                    if (dr["BInterne"] != DBNull.Value)
                        equipe.BInterne = bool.Parse(dr["BInterne"].ToString());
                    if (dr["CVehicule"] != DBNull.Value)
                        equipe.CVehicule = dr["CVehicule"].ToString();
                    collection.Add(equipe);
                }
                dr.Close();

                return (collection);
            }
        }
    }

}
