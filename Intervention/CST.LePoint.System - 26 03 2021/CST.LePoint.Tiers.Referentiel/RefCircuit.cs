using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    public class RefCircuitCollection : ItemCollection
    {
        public RefCircuitCollection()
        {
        }





        public static RefCircuitCollection Charger()
        {
            RefCircuitCollection collection = new RefCircuitCollection();

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
                            RefCircuit circuit = new RefCircuit();

                            circuit.Code = dr["Code_Circuit"].ToString().Trim();
                            if (dr["Lib_Circuit"] != DBNull.Value)
                                circuit.Libelle = dr["Lib_Circuit"].ToString();
                            circuit.Code_Circuit = dr["Code_Circuit"].ToString().Trim();
                            if (dr["Lib_Circuit"] != DBNull.Value)
                                circuit.Lib_Circuit = dr["Lib_Circuit"].ToString();


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
    public class RefCircuit : Item
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

        public DateTime DateInsertion { get; set; }
        public DateTime DateModification { get; set; }
        public int CreePar { get; set; }
        public int ModifiePar { get; set; }
        public string PCInsertion { get; set; }
        public string PCModification { get; set; }



        #endregion Propriétés

        public RefCircuit()
        {
            //RefCircuit refCircuit = new RefCircuit();
        }

        public static RefCircuit Charger(string Code_Circuit)
        {
            RefCircuit circuit = null;

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
                            circuit = new RefCircuit();

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


    }


}
