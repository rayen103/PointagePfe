using CST.LePoint.Referentiel;
using CST.LePoint.Securite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CST.LePoint.Intervention.Metier
{
    [Serializable]
    public class CircuitCollection : ItemCollection
    {
        public CircuitCollection()
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
                cmd.CommandText = "Circuit_Rpt_Charger";
                cmd.Parameters.AddWithValue("@CCircuit", DBNull.Value);
                cmd.Parameters.AddWithValue("@Code_Societe", GestionSession.SocieteCourante.CSociete);
                cmd.Parameters.AddWithValue("@Code_Site", GestionSession.SocieteSite);

                foreach (SqlParameter parametre in cmd.Parameters)               
                    if (parametre.Value == null)                    
                        parametre.Value = DBNull.Value;                    
                
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "Circuit_Rpt_Charger");
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
                cmd.CommandText = "Ref_Circuit_Charger";
                cmd.Parameters.AddWithValue("@CCircuit", DBNull.Value);
                cmd.Parameters.AddWithValue("@Code_Societe", GestionSession.SocieteCourante.CSociete);
                cmd.Parameters.AddWithValue("@Code_Site", GestionSession.SocieteSite);

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

        public static CircuitCollection Charger()
        {
            CircuitCollection collection = new CircuitCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Circuit_Charger";
                    cmd.Parameters.AddWithValue("@Code_Circuit", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Code_Societe", GestionSession.SocieteCourante.CSociete);
                    cmd.Parameters.AddWithValue("@Code_Site", GestionSession.SocieteSite);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Circuit circuit = new Circuit();

                            circuit.Code = dr["Code_Circuit"].ToString().Trim();
                            if (dr["Lib_Circuit"] != DBNull.Value)
                                circuit.Libelle = dr["Lib_Circuit"].ToString();
                            circuit.Code_Circuit = dr["Code_Circuit"].ToString().Trim();
                            if (dr["Lib_Circuit"] != DBNull.Value)
                                circuit.Lib_Circuit = dr["Lib_Circuit"].ToString();
                            circuit.PC_Fin_Circuit = dr["PC_Fin_Circuit"].ToString().Trim();
                            circuit.PC_Depart_Circuit = dr["PC_Depart_Circuit"].ToString().Trim();
                            if (dr["Km_Circuit"] != DBNull.Value)
                                circuit.Km_Circuit = decimal.Parse(dr["Km_Circuit"].ToString());
                            if (dr["Duree_Circuit"] != DBNull.Value)
                                circuit.Duree_Circuit = int.Parse(dr["Duree_Circuit"].ToString());

                            circuit.circuitPointCollecteCollection = CircuitPointCollecteCollection.Charger(circuit.Code_Circuit);
                            collection.Add(circuit);
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

    [Serializable]
    public class Circuit : Item
    {
        #region Propriétés

        public string Code_Circuit { get; set; }
        public string Lib_Circuit { get; set; }
        public string PC_Fin_Circuit { get; set; }
        public string PC_Depart_Circuit { get; set; }
        public decimal Km_Circuit { get; set; }
        public int Duree_Circuit { get; set; }
        public string Code_Societe { get; set; }
        public string Code_Site { get; set; }
        public int Couleur { get; set; }

        public DateTime DateInsertion { get; set; }
        public DateTime DateModification { get; set; }
        public int CreePar { get; set; }
        public int ModifiePar { get; set; }
        public string PCInsertion { get; set; }
        public string PCModification { get; set; }

        public CircuitPointCollecteCollection circuitPointCollecteCollection;

        #endregion Propriétés

        public Circuit()
        {
            circuitPointCollecteCollection = new CircuitPointCollecteCollection();
        }

        public static Circuit Charger(string Code_Circuit)
        {
            Circuit circuit = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Circuit_Charger";
                    cmd.Parameters.AddWithValue("@Code_Circuit", Code_Circuit);
                    cmd.Parameters.AddWithValue("@Code_Societe", GestionSession.SocieteCourante.CSociete);
                    cmd.Parameters.AddWithValue("@Code_Site", GestionSession.SocieteSite);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            circuit = new Circuit();

                            circuit.Code = dr["Code_Circuit"].ToString().Trim();
                            if (dr["Lib_Circuit"] != DBNull.Value)
                                circuit.Libelle = dr["Lib_Circuit"].ToString();
                            circuit.Code_Circuit = dr["Code_Circuit"].ToString().Trim();
                            if (dr["Lib_Circuit"] != DBNull.Value)
                                circuit.Lib_Circuit = dr["Lib_Circuit"].ToString();
                            circuit.PC_Fin_Circuit = dr["PC_Fin_Circuit"].ToString().Trim();
                            circuit.PC_Depart_Circuit = dr["PC_Depart_Circuit"].ToString().Trim();
                            if (dr["Km_Circuit"] != DBNull.Value)
                                circuit.Km_Circuit = decimal.Parse(dr["Km_Circuit"].ToString());
                            if (dr["Duree_Circuit"] != DBNull.Value)
                                circuit.Duree_Circuit = int.Parse(dr["Duree_Circuit"].ToString());
                            if (dr["Couleur"] != DBNull.Value)
                                circuit.Couleur =  (int.Parse(dr["Couleur"].ToString()));

                            circuit.circuitPointCollecteCollection = CircuitPointCollecteCollection.Charger(circuit.Code_Circuit);
                        }
                        dr.Close();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return circuit;
        }

        public void Sauvegarder()
        {
            try
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
                        cmd.CommandText = "Circuit_Sauvegarder";
                        cmd.Parameters.AddWithValue("@Code_Circuit", this.Code_Circuit);
                        cmd.Parameters.AddWithValue("@Lib_Circuit", this.Lib_Circuit);
                        cmd.Parameters.AddWithValue("@PC_Depart_Circuit", this.PC_Depart_Circuit);
                        cmd.Parameters.AddWithValue("@PC_Fin_Circuit", this.PC_Fin_Circuit);
                        cmd.Parameters.AddWithValue("@Km_Circuit", this.Km_Circuit);
                        cmd.Parameters.AddWithValue("@Duree_Circuit", this.Duree_Circuit);
                        cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                        cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                        cmd.Parameters.AddWithValue("@CreePar", CreePar);
                        cmd.Parameters.AddWithValue("@ModifiePar", ModifiePar);
                        cmd.Parameters.AddWithValue("@PCInsertion", PCInsertion);
                        cmd.Parameters.AddWithValue("@PCModification", PCModification);
                        cmd.Parameters.AddWithValue("@Code_Societe", GestionSession.SocieteCourante.CSociete);
                        cmd.Parameters.AddWithValue("@Code_Site", GestionSession.SocieteSite);                        
                        cmd.Parameters.AddWithValue("@Couleur",(int)Couleur);
                        foreach (SqlParameter parametre in cmd.Parameters)
                            if (parametre.Value == null)
                                parametre.Value = DBNull.Value;

                        cmd.ExecuteNonQuery();
                        CircuitPointCollecte.Supprimer(transaction, Code_Circuit);
                        int i = 1;
                        foreach (CircuitPointCollecte circuitPointCollecte in circuitPointCollecteCollection)
                        {
                            circuitPointCollecte.Code_Circuit = this.Code_Circuit;
                            ////circuitDetail.Ordre = i++;
                            circuitPointCollecte.Sauvegarder(transaction);
                        }

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
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
                    SqlTransaction transaction = cn.BeginTransaction();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Transaction = transaction;
                    cmd.Connection = transaction.Connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Circuit_Supprimer";
                    cmd.Parameters.AddWithValue("@Code_Circuit", Code_Circuit);
                    cmd.Parameters.AddWithValue("@Code_Societe", GestionSession.SocieteCourante.CSociete);
                    cmd.Parameters.AddWithValue("@Code_Site", GestionSession.SocieteSite);
                    foreach (SqlParameter sqlParametre in cmd.Parameters)
                        if (sqlParametre.Value == null)
                            sqlParametre.Value = DBNull.Value;
                    CircuitPointCollecte.Supprimer(transaction, Code_Circuit);
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

    public class CircuitColor
    {
        public string name { get; set; }
        public Color color { get; set; }
    }
}
