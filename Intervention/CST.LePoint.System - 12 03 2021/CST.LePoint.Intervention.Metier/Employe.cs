using CST.LePoint.Referentiel;
using CST.LePoint.Securite;
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
    
    public class Employe : Item
    {
        #region Propriétés

    
        public string Matricule_Emp { get; set; }

        public string RFID_Emp { get; set; }
        public string Nom_Emp { get; set; }
        public string Prenom_Emp { get; set; }
        public string Code_Circuit_Emp { get; set; }
        public string Code_PC_Emp { get; set; }
        public string Code_Shift { get; set; }
        public string Adresse { get; set; }
        public string Code_Gouv_Emp { get; set; }
        public string Code_Region_Emp { get; set; }
        public string Code_Societe { get; set; }
        public string Code_Site { get; set; }

        public DateTime DateInsertion { get; set; }             
        public DateTime DateModification { get; set; }
        public int CreePar { get; set; }
        public int ModifiePar { get; set; }
        public string PCInsertion { get; set; }
        public string PCModification { get; set; }

        #endregion Propriétés

        public Employe()
        {
            //this.BDisponible = true;
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
                    cmd.CommandText = "Employe_Sauvegarder";
                    cmd.Parameters.AddWithValue("@RFID_Emp", this.RFID_Emp);
                    cmd.Parameters.AddWithValue("@Matricule_Emp", this.Matricule_Emp);
                    cmd.Parameters.AddWithValue("@Nom_Emp", this.Nom_Emp);
                    cmd.Parameters.AddWithValue("@Prenom_Emp", Prenom_Emp);
                    cmd.Parameters.AddWithValue("@Code_Circuit_Emp", Code_Circuit_Emp);
                    cmd.Parameters.AddWithValue("@Code_PC_Emp", Code_PC_Emp);
                    cmd.Parameters.AddWithValue("@Code_Shift", Code_Shift);
                    cmd.Parameters.AddWithValue("@Adresse", Adresse);
                    cmd.Parameters.AddWithValue("@Code_Gouv_Emp", Code_Gouv_Emp);
                    cmd.Parameters.AddWithValue("@Code_Region_Emp", Code_Region_Emp);

                    cmd.Parameters.AddWithValue("@CreePar", CreePar);
                    cmd.Parameters.AddWithValue("@ModifiePar", ModifiePar);
                    cmd.Parameters.AddWithValue("@PCInsertion", PCInsertion);
                    cmd.Parameters.AddWithValue("@PCModification", PCModification);
                    cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                    cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Code_Societe", GestionSession.SocieteCourante.CSociete);
                    cmd.Parameters.AddWithValue("@Code_Site", GestionSession.SocieteSite);
                   // cmd.Parameters.AddWithValue("@CEquipe", CEquipe);

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

        public void Sauvegarder(SqlTransaction transaction)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Transaction = transaction;
            cmd.Connection = transaction.Connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Employe_Sauvegarder";
            cmd.Parameters.AddWithValue("@RFID_Emp", this.RFID_Emp);
            cmd.Parameters.AddWithValue("@Matricule_Emp", this.Matricule_Emp);
            cmd.Parameters.AddWithValue("@Nom_Emp", this.Nom_Emp);
            cmd.Parameters.AddWithValue("@Prenom_Emp", Prenom_Emp);
            cmd.Parameters.AddWithValue("@Code_Circuit_Emp", Code_Circuit_Emp);
            cmd.Parameters.AddWithValue("@Code_PC_Emp", Code_PC_Emp);
            cmd.Parameters.AddWithValue("@Code_Shift", Code_Shift);
            cmd.Parameters.AddWithValue("@Adresse", Adresse);
            cmd.Parameters.AddWithValue("@Code_Gouv_Emp", Code_Gouv_Emp);
            cmd.Parameters.AddWithValue("@Code_Region_Emp", Code_Region_Emp);

            cmd.Parameters.AddWithValue("@CreePar", CreePar);
            cmd.Parameters.AddWithValue("@ModifiePar", ModifiePar);
            cmd.Parameters.AddWithValue("@PCInsertion", PCInsertion);
            cmd.Parameters.AddWithValue("@PCModification", PCModification);
            cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
            cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
            // cmd.Parameters.AddWithValue("@CEquipe", CEquipe);

            foreach (SqlParameter parametre in cmd.Parameters)
                if (parametre.Value == null)
                    parametre.Value = DBNull.Value;

            cmd.ExecuteNonQuery();

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
                    cmd.CommandText = "Employe_Supprimer";
                    cmd.Parameters.AddWithValue("@RFID_Emp", this.Code);

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

        public void Supprimer(SqlTransaction transaction)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Transaction = transaction;
            cmd.Connection = transaction.Connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Employe_Supprimer";
            cmd.Parameters.AddWithValue("@RFID_Emp", this.Code);

            foreach (SqlParameter parametre in cmd.Parameters)
                if (parametre.Value == null)
                    parametre.Value = DBNull.Value;

            cmd.ExecuteNonQuery();
        }

        public static Employe Charger(string Matricule)
        {
            Employe employe = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "Employe_Charger";
                    cmd.Parameters.AddWithValue("@RFID_Emp", Matricule);
                    cmd.Parameters.AddWithValue("@Code_Societe", GestionSession.SocieteCourante.CSociete);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            employe = new Employe();

                            employe.Code = dr["RFID_Emp"].ToString();
                            employe.Libelle = dr["Matricule_Emp"].ToString();
                            employe.RFID_Emp = dr["RFID_Emp"].ToString();
                            employe.Matricule_Emp = dr["Matricule_Emp"].ToString();

                            if (dr["Nom_Emp"] != DBNull.Value)
                                employe.Nom_Emp = (dr["Nom_Emp"].ToString());

                              if (dr["Prenom_Emp"] != DBNull.Value)
                                employe.Prenom_Emp = (dr["Prenom_Emp"].ToString());

                            if (dr["Code_Circuit_Emp"] != DBNull.Value)
                                employe.Code_Circuit_Emp = dr["Code_Circuit_Emp"].ToString();

                            if (dr["Code_PC_Emp"] != DBNull.Value)
                                employe.Code_PC_Emp = dr["Code_PC_Emp"].ToString();

                            if (dr["Code_Shift"] != DBNull.Value)
                                employe.Code_Shift = dr["Code_Shift"].ToString();

                            if (dr["Adresse"] != DBNull.Value)
                                employe.Adresse = dr["Adresse"].ToString();

                            if (dr["Code_Gouv_Emp"] != DBNull.Value)
                                employe.Code_Gouv_Emp = dr["Code_Gouv_Emp"].ToString();

                            if (dr["Code_Region_Emp"] != DBNull.Value)
                                employe.Code_Region_Emp = dr["Code_Region_Emp"].ToString();
                            
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return employe;
        }

        public static List<Employe> Charger()
        {
            List<Employe> collection = new List<Employe>();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Employe_Charger";
                    cmd.Parameters.AddWithValue("@RFID_Emp", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Code_Societe", GestionSession.SocieteCourante.CSociete);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Employe employe = new Employe();

                           employe.Code = dr["RFID_Emp"].ToString();
                            employe.Libelle = dr["Matricule_Emp"].ToString();
                            employe.RFID_Emp = dr["RFID_Emp"].ToString();
                            employe.Matricule_Emp = dr["Matricule_Emp"].ToString();

                            if (dr["Nom_Emp"] != DBNull.Value)
                                employe.Nom_Emp = (dr["Nom_Emp"].ToString());

                              if (dr["Prenom_Emp"] != DBNull.Value)
                                employe.Prenom_Emp = (dr["Prenom_Emp"].ToString());

                            if (dr["Code_Circuit_Emp"] != DBNull.Value)
                                employe.Code_Circuit_Emp = dr["Code_Circuit_Emp"].ToString();

                            if (dr["Code_PC_Emp"] != DBNull.Value)
                                employe.Code_PC_Emp = dr["Code_PC_Emp"].ToString();

                            if (dr["Code_Shift"] != DBNull.Value)
                                employe.Code_Shift = dr["Code_Shift"].ToString();

                            if (dr["Adresse"] != DBNull.Value)
                                employe.Adresse = dr["Adresse"].ToString();

                            if (dr["Code_Gouv_Emp"] != DBNull.Value)
                                employe.Code_Gouv_Emp = dr["Code_Gouv_Emp"].ToString();

                            if (dr["Code_Region_Emp"] != DBNull.Value)
                                employe.Code_Region_Emp = dr["Code_Region_Emp"].ToString();
                            
                        
                            collection.Add(employe);
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


    public class EmployeCollection : ItemCollection
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
        //        cmd.CommandText = "Vehicule_Rpt_Charger";
        //        cmd.Parameters.AddWithValue("@CVehicule", DBNull.Value);

        //        foreach (SqlParameter parametre in cmd.Parameters)
        //        {
        //            if (parametre.Value == null)
        //            {
        //                parametre.Value = DBNull.Value;
        //            }
        //        }
        //        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        //        sda.Fill(ds, "Vehicule_Rpt_Charger");
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
        //        cmd.CommandText = "Ref_Vehicule_ChargerTous";
        //        cmd.Parameters.AddWithValue("@CVehicule", DBNull.Value);

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

        public static EmployeCollection Charger()
        {
            EmployeCollection collection = new EmployeCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Employe_Charger";
                    cmd.Parameters.AddWithValue("@RFID_Emp", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Code_Societe", GestionSession.SocieteCourante.CSociete);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Employe employe = new Employe();

                            employe.Code = dr["RFID_Emp"].ToString();
                            employe.Libelle = dr["Nom_Emp"].ToString();

                            employe.RFID_Emp = dr["RFID_Emp"].ToString();
                            employe.Matricule_Emp = dr["Matricule_Emp"].ToString();

                            if (dr["Nom_Emp"] != DBNull.Value)
                                employe.Nom_Emp = (dr["Nom_Emp"].ToString());

                            if (dr["Prenom_Emp"] != DBNull.Value)
                                employe.Prenom_Emp = (dr["Prenom_Emp"].ToString());

                            if (dr["Code_Circuit_Emp"] != DBNull.Value)
                                employe.Code_Circuit_Emp = dr["Code_Circuit_Emp"].ToString();

                            if (dr["Code_PC_Emp"] != DBNull.Value)
                                employe.Code_PC_Emp = dr["Code_PC_Emp"].ToString();

                            if (dr["Code_Shift"] != DBNull.Value)
                                employe.Code_Shift = dr["Code_Shift"].ToString();

                            if (dr["Adresse"] != DBNull.Value)
                                employe.Adresse = dr["Adresse"].ToString();

                            if (dr["Code_Gouv_Emp"] != DBNull.Value)
                                employe.Code_Gouv_Emp = dr["Code_Gouv_Emp"].ToString();

                            if (dr["Code_Region_Emp"] != DBNull.Value)
                                employe.Code_Region_Emp = dr["Code_Region_Emp"].ToString();
                            collection.Add(employe);
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
        public static EmployeCollection ChargerResp()
        {
            EmployeCollection collection = new EmployeCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "GP_EmployeResp_Charger";
                    cmd.Parameters.AddWithValue("@Matricule", DBNull.Value);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Employe employe = new Employe();
                            employe.Code = dr["RFID_Emp"].ToString();
                            employe.Libelle = dr["Matricule_Emp"].ToString();
                            employe.RFID_Emp = dr["RFID_Emp"].ToString();
                            employe.Matricule_Emp = dr["Matricule_Emp"].ToString();

                            if (dr["Nom_Emp"] != DBNull.Value)
                                employe.Nom_Emp = (dr["Nom_Emp"].ToString());

                            if (dr["Prenom_Emp"] != DBNull.Value)
                                employe.Prenom_Emp = (dr["Prenom_Emp"].ToString());

                            if (dr["Code_Circuit_Emp"] != DBNull.Value)
                                employe.Code_Circuit_Emp = dr["Code_Circuit_Emp"].ToString();

                            if (dr["Code_PC_Emp"] != DBNull.Value)
                                employe.Code_PC_Emp = dr["Code_PC_Emp"].ToString();

                            if (dr["Code_Shift"] != DBNull.Value)
                                employe.Code_Shift = dr["Code_Shift"].ToString();

                            if (dr["Adresse"] != DBNull.Value)
                                employe.Adresse = dr["Adresse"].ToString();

                            if (dr["Code_Gouv_Emp"] != DBNull.Value)
                                employe.Code_Gouv_Emp = dr["Code_Gouv_Emp"].ToString();

                            if (dr["Code_Region_Emp"] != DBNull.Value)
                                employe.Code_Region_Emp = dr["Code_Region_Emp"].ToString();
                            collection.Add(employe);
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
