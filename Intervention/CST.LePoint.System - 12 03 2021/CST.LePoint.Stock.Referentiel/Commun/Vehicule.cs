using CST.LePoint.Referentiel;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace CST.LePoint.Stock.Referentiel.Commun
{
    [Serializable]
    public class VehiculeCollection : ItemCollection
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
                cmd.CommandText = "Vehicule_Rpt_Charger";
                cmd.Parameters.AddWithValue("@CVehicule", DBNull.Value);

                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "Vehicule_Rpt_Charger");
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
                cmd.CommandText = "Ref_Vehicule_ChargerTous";
                cmd.Parameters.AddWithValue("@CVehicule", DBNull.Value);

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

        public static VehiculeCollection Charger()
        {
            VehiculeCollection collection = new VehiculeCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_Vehicule_Charger";
                    cmd.Parameters.AddWithValue("@CVehicule", DBNull.Value);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Vehicule vehicule = new Vehicule();
                            vehicule.Code = dr["CVehicule"].ToString();
                            if (dr["NumeroSerie"] != DBNull.Value)
                                vehicule.NumeroSerie = dr["NumeroSerie"].ToString();
                            vehicule.Libelle = dr["LibVehicule"].ToString();
                            if (dr["BActif"] != DBNull.Value)
                                vehicule.BActif = bool.Parse(dr["BActif"].ToString()); 
                            if (dr["ChargeMax"] != DBNull.Value)
                                vehicule.ChargeMax = decimal.Parse(dr["ChargeMax"].ToString());
                            if (dr["BDisponible"] != DBNull.Value)
                                vehicule.BDisponible = bool.Parse(dr["BDisponible"].ToString());
                            if (dr["ChargeLibre"] != DBNull.Value)
                                vehicule.ChargeLibre = decimal.Parse(dr["ChargeLibre"].ToString());
                            //if (dr["CoutparKM"] != DBNull.Value)
                            //    vehicule.CoutparKM = decimal.Parse(dr["CoutparKM"].ToString());
                            collection.Add(vehicule);
                        }
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

    [Serializable]
    public class Vehicule : Item
    {
        #region Propriétés

        [XmlAttribute("NumeroSerie")]
        [Bindable(true)]
        public string NumeroSerie { get; set; }

        [XmlAttribute("BActif")]
        [Bindable(true)]
        public bool BActif { get; set; }

        [XmlAttribute("BDisponible")]
        [Bindable(true)]
        public bool BDisponible { get; set; }

        [XmlAttribute("DateDebut")]
        [Bindable(true)]
        public DateTime? DateDebut { get; set; }

        [XmlAttribute("DateFin")]
        [Bindable(true)]
        public DateTime DateFin { get; set; }

        [XmlAttribute("ChargeMax")]
        [Bindable(true)]
        public decimal ChargeMax { get; set; }

        [XmlAttribute("ChargeLibre")]
        [Bindable(true)]
        public decimal ChargeLibre { get; set; }

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
        
        [XmlAttribute("CoutparKM")]
        [Bindable(true)]
        public decimal CoutparKM { get; set; }

        #endregion Propriétés

        public Vehicule()
        {
            this.BActif = true;
            this.BDisponible = true;
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
                    cmd.CommandText = "Ref_Vehicule_Sauvegarder";
                    cmd.Parameters.AddWithValue("@CVehicule", this.Code);
                    cmd.Parameters.AddWithValue("@LibVehicule", this.Libelle);
                    cmd.Parameters.AddWithValue("@NumeroSerie", NumeroSerie);
                    cmd.Parameters.AddWithValue("@BActif", BActif);
                    cmd.Parameters.AddWithValue("@BDisponible", BDisponible);
                    cmd.Parameters.AddWithValue("@ChargeMax", ChargeMax);
                    cmd.Parameters.AddWithValue("@ChargeLibre", ChargeLibre);
                    cmd.Parameters.AddWithValue("@CreePar", CreePar);
                    cmd.Parameters.AddWithValue("@ModifiePar", ModifiePar);
                    cmd.Parameters.AddWithValue("@PCInsertion", PCInsertion);
                    cmd.Parameters.AddWithValue("@PCModification", PCModification);
                    cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                    cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                    //cmd.Parameters.AddWithValue("@CoutparKM", CoutparKM);

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
                    cmd.CommandText = "Ref_Vehicule_Supprimer";
                    cmd.Parameters.AddWithValue("@CVehicule", this.Code);

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

        public static Vehicule Charger(string cVehicule)
        {
            Vehicule vehicule = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "Ref_Vehicule_Charger";
                    cmd.Parameters.AddWithValue("@CVehicule", cVehicule);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            vehicule = new Vehicule();

                            vehicule.Code = dr["CVehicule"].ToString();
                            vehicule.Libelle = dr["LibVehicule"].ToString();
                            if (dr["NumeroSerie"] != DBNull.Value)
                                vehicule.NumeroSerie = dr["NumeroSerie"].ToString();
                            if (dr["BActif"] != DBNull.Value)
                                vehicule.BActif = bool.Parse(dr["BActif"].ToString());
                            if (dr["ChargeMax"] != DBNull.Value)
                                vehicule.ChargeMax = decimal.Parse(dr["ChargeMax"].ToString());

                            if (dr["BDisponible"] != DBNull.Value)
                                vehicule.BDisponible = bool.Parse(dr["BDisponible"].ToString());
                            if (dr["ChargeLibre"] != DBNull.Value)
                                vehicule.ChargeLibre = decimal.Parse(dr["ChargeLibre"].ToString());
                            //if (dr["CoutparKM"] != DBNull.Value)
                            //    vehicule.CoutparKM = decimal.Parse(dr["CoutparKM"].ToString());
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return vehicule;
        }
    }
}