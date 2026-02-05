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
   public  class InterventionVehicule
    {
        
         #region Propriétés

       
        [XmlAttribute("NIntervention")]
        [Bindable(true)]
        public string NIntervention { get; set; }

        [XmlAttribute("CVehicule")]
        [Bindable(true)]
        public string CVehicule { get; set; }

        [XmlAttribute("Libelle")]
        [Bindable(true)]
        public string Libelle { get; set; }

        [XmlAttribute("NSerie")]
        [Bindable(true)]
        public String NSerie { get; set; }

        [XmlAttribute("Cout")]
        [Bindable(true)]
        public decimal Cout { get; set; }

        [XmlAttribute("NombreKM")]
        [Bindable(true)]
        public decimal NombreKM { get; set; }

        [XmlAttribute("CoutKM")]
        [Bindable(true)]
        public decimal CoutKM { get; set; }

        [XmlAttribute("TypeIntervention")]
        [Bindable(true)]
        public string TypeIntervention { get; set; }

        [XmlAttribute("CreePar")]
        [Bindable(true)]
        public int CreePar { get; set; }

        [XmlAttribute("ModifiePar")]
        [Bindable(true)]
        public int ModifiePar { get; set; }

        [XmlAttribute("DateInsertion")]
        [Bindable(true)]
        public DateTime DateInsertion { get; set; }

        [XmlAttribute("DateModification")]
        [Bindable(true)]
        public DateTime DateModification { get; set; }

        [XmlAttribute("PCInsertion")]
        [Bindable(true)]
        public string PCInsertion { get; set; }

        [XmlAttribute("PCModification")]
        [Bindable(true)]
        public string PCModification { get; set; }

        #endregion Propriétés

        public InterventionVehicule()
        {
          
        }

        public void Sauvegarder(SqlTransaction transaction)
        {
           

                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Transaction = transaction;
                    cmd.Connection = transaction.Connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "GI_InterventionVehicule_Sauvegarder";

                    cmd.Parameters.AddWithValue("@NIntervention", NIntervention);
                    cmd.Parameters.AddWithValue("@CVehicule", CVehicule);
                    cmd.Parameters.AddWithValue("@Libelle", Libelle);
                    cmd.Parameters.AddWithValue("@NSerie", NSerie);

                    cmd.Parameters.AddWithValue("@Cout", Cout);
                    cmd.Parameters.AddWithValue("@NombreKM", NombreKM);
                    cmd.Parameters.AddWithValue("@CoutKM", CoutKM);
                    cmd.Parameters.AddWithValue("@TypeIntervention", TypeIntervention);
                    
                    cmd.Parameters.AddWithValue("@CreePar", CreePar);
                    cmd.Parameters.AddWithValue("@ModifiePar", ModifiePar);
                    cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                    cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                    cmd.Parameters.AddWithValue("@PCInsertion", PCInsertion);
                    cmd.Parameters.AddWithValue("@PCModification", this.PCModification);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    cmd.ExecuteNonQuery();

                }
                catch (Exception)
                {

                    throw;
                }
            
        }

        public void Sauvegarder()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                Sauvegarder(transaction);
            }
        }

       
        public static InterventionVehicule Charger(string NIntervention, string CVehicule)
        {
            InterventionVehicule interventionVehicule = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "GI_InterventionVehicule_Charger";
                    
                    cmd.Parameters.AddWithValue("@NIntervention", NIntervention);
                    cmd.Parameters.AddWithValue("@CVehicule", CVehicule);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            interventionVehicule = new InterventionVehicule();


                            interventionVehicule.NIntervention = dr["NIntervention"].ToString();
                            interventionVehicule.CVehicule = dr["CVehicule"].ToString();

                            if (dr["Libelle"] != DBNull.Value)
                                interventionVehicule.Libelle = dr["Libelle"].ToString();
                            if (dr["NSerie"] != DBNull.Value)
                                interventionVehicule.NSerie = dr["NSerie"].ToString();
                            if (dr["Cout"] != DBNull.Value)
                                interventionVehicule.Cout = decimal.Parse(dr["Cout"].ToString());
                            if (dr["NombreKM"] != DBNull.Value)
                                interventionVehicule.NombreKM = decimal.Parse(dr["NombreKM"].ToString());
                            if (dr["CoutKM"] != DBNull.Value)
                                interventionVehicule.CoutKM = decimal.Parse(dr["CoutKM"].ToString());
                            if (dr["TypeIntervention"] != DBNull.Value)
                                interventionVehicule.TypeIntervention = dr["TypeIntervention"].ToString();
                           
                            
               
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return interventionVehicule;
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
                    cmd.CommandText = "GI_InterventionVehicule_Supprimer";
                   
                    cmd.Parameters.AddWithValue("@NIntervention", NIntervention);
                    cmd.Parameters.AddWithValue("@CVehicule", CVehicule);

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

    }

    public class InterventionVehiculeCollection : List<InterventionVehicule>
    {

        public static InterventionVehiculeCollection Charger()
        {
            InterventionVehiculeCollection collection = new InterventionVehiculeCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "GI_InterventionVehicule_Charger";

                    cmd.Parameters.AddWithValue("@CVehicule", DBNull.Value);
                    cmd.Parameters.AddWithValue("@NIntervention", DBNull.Value);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            InterventionVehicule interventionVehicule = new InterventionVehicule();

                            interventionVehicule.NIntervention = dr["NIntervention"].ToString();
                            interventionVehicule.CVehicule = dr["CVehicule"].ToString();

                            if (dr["Libelle"] != DBNull.Value)
                                interventionVehicule.Libelle = dr["Libelle"].ToString();
                            if (dr["NSerie"] != DBNull.Value)
                                interventionVehicule.NSerie = dr["NSerie"].ToString();
                            if (dr["Cout"] != DBNull.Value)
                                interventionVehicule.Cout = decimal.Parse(dr["Cout"].ToString());
                            if (dr["NombreKM"] != DBNull.Value)
                                interventionVehicule.NombreKM = decimal.Parse(dr["NombreKM"].ToString());
                            if (dr["CoutKM"] != DBNull.Value)
                                interventionVehicule.CoutKM = decimal.Parse(dr["CoutKM"].ToString());
                            if (dr["TypeIntervention"] != DBNull.Value)
                                interventionVehicule.TypeIntervention = dr["TypeIntervention"].ToString();


                            collection.Add(interventionVehicule);
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
}
