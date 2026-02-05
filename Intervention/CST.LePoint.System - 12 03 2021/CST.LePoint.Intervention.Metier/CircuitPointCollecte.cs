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
    public class CircuitPointCollecte : Item
    {
        #region Propriétés

        public string Code_Circuit { get; set; }
        public string LibCircuit { get; set; }
        public string Code_PC { get; set; }
        public string Lib_PC { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }

        #endregion

        public CircuitPointCollecte() { }

        public void Sauvegarder(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Circuit_PointCollecte_Sauvegarder";
                cmd.Parameters.AddWithValue("@Code_Circuit", this.Code_Circuit);
                cmd.Parameters.AddWithValue("@LibCircuit", this.LibCircuit);
                cmd.Parameters.AddWithValue("@Code_PC", this.Code_PC);
                cmd.Parameters.AddWithValue("@Lib_PC", this.Lib_PC);

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
                    cmd.CommandText = "Circuit_PointCollecte_Supprimer";
                    cmd.Parameters.AddWithValue("@Code_Circuit", this.Code_Circuit);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Supprimer(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Circuit_PointCollecte_Supprimer";
                cmd.Parameters.AddWithValue("@Code_Circuit", this.Code_Circuit);

                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void Supprimer(SqlTransaction transaction, string Code_Circuit)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Circuit_PointCollecte_Supprimer";
                cmd.Parameters.AddWithValue("@Code_Circuit", Code_Circuit);

                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }

    public class CircuitPointCollecteCollection : ItemCollection
    {
        public static CircuitPointCollecteCollection Charger(string Code_Circuit)
        {
            CircuitPointCollecteCollection collection = new CircuitPointCollecteCollection();
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Circuit_PointCollecte_Charger";
                cmd.Parameters.AddWithValue("@Code_Circuit", Code_Circuit);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    CircuitPointCollecte circuit = new CircuitPointCollecte();
                    circuit.Code_Circuit = dr["Code_Circuit"].ToString();
                    circuit.Code = dr["Code_Circuit"].ToString();
                    if (dr["LibCircuit"] != DBNull.Value)
                    {
                        circuit.LibCircuit = (dr["LibCircuit"].ToString());
                        circuit.Libelle = (dr["LibCircuit"].ToString());
                    }

                    circuit.Code_PC = dr["Code_PC"].ToString();
                    if (dr["Lib_PC"] != DBNull.Value)
                        circuit.Lib_PC = (dr["Lib_PC"].ToString());

                    if (dr["Long_PC"] != DBNull.Value)
                        circuit.Longitude = decimal.Parse(dr["Long_PC"].ToString());
                    if (dr["Latt_PC"] != DBNull.Value)
                        circuit.Latitude = decimal.Parse(dr["Latt_PC"].ToString());

                    collection.Add(circuit);
                }
                dr.Close();
                return (collection);
            }
        }       
    }
}
