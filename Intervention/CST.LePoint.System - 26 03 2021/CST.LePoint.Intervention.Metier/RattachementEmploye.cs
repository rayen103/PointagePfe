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
   public class RattachementEmploye
    {
          #region Propriétés


        [XmlAttribute("NRattachement")]
        [Bindable(true)]
       public string NRattachement { get; set; }

        [XmlAttribute("Matricule")]
        [Bindable(true)]
        public string Matricule { get; set; }

        [XmlAttribute("NomPrenom")]
        [Bindable(true)]
        public string NomPrenom { get; set; }

        [XmlAttribute("DateDebut")]
        [Bindable(true)]
        public DateTime? DateDebut { get; set; }

        [XmlAttribute("HeureDebut")]
        [Bindable(true)]
        public string HeureDebut { get; set; }

        [XmlAttribute("DateFin")]
        [Bindable(true)]
        public DateTime? DateFin { get; set; }

        [XmlAttribute("HeureFin")]
        [Bindable(true)]
        public string HeureFin { get; set; }

        [XmlAttribute("NombreHeure")]
        [Bindable(true)]
        public decimal NombreHeure { get; set; }

        [XmlAttribute("Cout")]
        [Bindable(true)]
        public decimal Cout { get; set; }

        [XmlAttribute("CoutG")]
        [Bindable(true)]
        public decimal CoutG { get; set; }

        [XmlAttribute("TypeRattachement")]
        [Bindable(true)]
        public string TypeRattachement { get; set; }
        
        

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

        public RattachementEmploye()
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
                    cmd.CommandText = "GP_RattachementEmploye_Sauvegarder";

                    cmd.Parameters.AddWithValue("@NRattachement", NRattachement);
                    cmd.Parameters.AddWithValue("@Matricule", Matricule);
                    cmd.Parameters.AddWithValue("@NomPrenom", NomPrenom);
                    cmd.Parameters.AddWithValue("@DateDebut", DateDebut);
                    cmd.Parameters.AddWithValue("@HeureDebut", HeureDebut);
                    cmd.Parameters.AddWithValue("@DateFin", DateFin);
                    cmd.Parameters.AddWithValue("@HeureFin", HeureFin);
                    cmd.Parameters.AddWithValue("@NombreHeure", NombreHeure);
                    cmd.Parameters.AddWithValue("@Cout", Cout);
                    cmd.Parameters.AddWithValue("@CoutG", CoutG);
                    cmd.Parameters.AddWithValue("@TypeRattachement", TypeRattachement);
                   

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
                catch (Exception ex) 
                {
                   
                    throw ex;
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

        public static RattachementEmploye Charger(string NRattachement, string Matricule)
        {
            RattachementEmploye rattachementEmploye = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "GI_InterventionEmploye_Charger";

                    cmd.Parameters.AddWithValue("@NRattachement", NRattachement);
                    cmd.Parameters.AddWithValue("@Matricule", Matricule);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            rattachementEmploye = new RattachementEmploye();


                            rattachementEmploye.NRattachement = dr["NRattachement"].ToString();
                            rattachementEmploye.Matricule = dr["Matricule"].ToString();

                            if (dr["NomPrenom"] != DBNull.Value)
                                rattachementEmploye.NomPrenom = dr["NomPrenom"].ToString();
                            if (dr["DateDebut"] != DBNull.Value)
                                rattachementEmploye.DateDebut = DateTime.Parse(dr["DateDebut"].ToString());
                            if (dr["HeureDebut"] != DBNull.Value)
                                rattachementEmploye.HeureDebut = dr["HeureDebut"].ToString();
                            if (dr["DateFin"] != DBNull.Value)
                                rattachementEmploye.DateFin = DateTime.Parse(dr["DateFin"].ToString());
                            if (dr["HeureFin"] != DBNull.Value)
                                rattachementEmploye.HeureFin = dr["HeureFin"].ToString();
                            if (dr["NombreHeure"] != DBNull.Value)
                                rattachementEmploye.NombreHeure = int.Parse(dr["NombreHeure"].ToString());
                            if (dr["Cout"] != DBNull.Value)
                                rattachementEmploye.Cout = decimal.Parse(dr["Cout"].ToString());
                            if (dr["CoutG"] != DBNull.Value)
                                rattachementEmploye.CoutG = decimal.Parse(dr["CoutG"].ToString());
                            if (dr["TypeRattachement"] != DBNull.Value)
                                rattachementEmploye.TypeRattachement = dr["TypeRattachement"].ToString();
                            
               
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return rattachementEmploye;
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
                    cmd.CommandText = "GI_InterventionEmploye_Supprimer";

                    cmd.Parameters.AddWithValue("@NRattachement", NRattachement);
                    cmd.Parameters.AddWithValue("@Matricule", Matricule);

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

    public class RattachementEmployeCollection : List<RattachementEmploye>
    {

        public static RattachementEmployeCollection Charger()
        {
            RattachementEmployeCollection collection = new RattachementEmployeCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "GI_InterventionEmploye_Charger";

                    cmd.Parameters.AddWithValue("@Matricule", DBNull.Value);
                    cmd.Parameters.AddWithValue("@NRattachement", DBNull.Value);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            RattachementEmploye rattachementEmploye = new RattachementEmploye();


                            rattachementEmploye.NRattachement = dr["NRattachement"].ToString();
                            rattachementEmploye.Matricule = dr["Matricule"].ToString();

                            if (dr["NomPrenom"] != DBNull.Value)
                                rattachementEmploye.NomPrenom = dr["NomPrenom"].ToString();
                            if (dr["DateDebut"] != DBNull.Value)
                                rattachementEmploye.DateDebut = DateTime.Parse(dr["DateDebut"].ToString());
                            if (dr["HeureDebut"] != DBNull.Value)
                                rattachementEmploye.HeureDebut = dr["HeureDebut"].ToString();
                            if (dr["DateFin"] != DBNull.Value)
                                rattachementEmploye.DateFin = DateTime.Parse(dr["DateFin"].ToString());
                            if (dr["HeureFin"] != DBNull.Value)
                                rattachementEmploye.HeureFin = dr["HeureFin"].ToString();
                            if (dr["NombreHeure"] != DBNull.Value)
                                rattachementEmploye.NombreHeure = int.Parse(dr["NombreHeure"].ToString());
                            if (dr["Cout"] != DBNull.Value)
                                rattachementEmploye.Cout = decimal.Parse(dr["Cout"].ToString());
                            if (dr["CoutG"] != DBNull.Value)
                                rattachementEmploye.CoutG = decimal.Parse(dr["CoutG"].ToString());
                            if (dr["TypeRattachement"] != DBNull.Value)
                                rattachementEmploye.TypeRattachement = dr["TypeRattachement"].ToString();

                            collection.Add(rattachementEmploye);
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
