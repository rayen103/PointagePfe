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

namespace CST.LePoint.VenteMobile.Metier
{
    public class MobileCircuit
    {
        #region Propriétés

        [XmlAttribute("CEquipe")]
        [Bindable(true)]
        public string CEquipe { get; set; }

        [XmlAttribute("CCircuit")]
        [Bindable(true)]
        public string CCircuit { get; set; }

        [XmlAttribute("Libelle")]
        [Bindable(true)]
        public string Libelle { get; set; }

        #endregion Propriétés
    }

    [Serializable]
    public class MobileCircuitCollection : List<MobileCircuit>
    {
        public static MobileCircuitCollection Charger() 
        {
            MobileCircuitCollection collection = new MobileCircuitCollection();
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Mobile_Circuit_Charger";
                    cmd.Parameters.Add(new SqlParameter("@CCircuit", DBNull.Value));

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            MobileCircuit circuit = new MobileCircuit();

                            circuit.CCircuit = dr["CCircuit"].ToString().Trim();
                            if (dr["LibCircuit"] != DBNull.Value)
                                circuit.Libelle = dr["LibCircuit"].ToString().Trim();
                            if (dr["CEquipe"] != DBNull.Value)
                                circuit.CEquipe = dr["CEquipe"].ToString();
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
}
