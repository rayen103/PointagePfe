using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace CST.LePoint.Stock.Metier
{
    [Serializable]
    public class BonReceptionProduction
    {
        #region Propriétés

        [XmlAttribute("NBonProduction")]
        [Bindable(true)]
        public string NBonProduction { get; set; }

        [XmlAttribute("DateProduction")]
        [Bindable(true)]
        public DateTime DateProduction { get; set; }

        [XmlAttribute("CConditionnement")]
        [Bindable(true)]
        public string CConditionnement { get; set; }

        [XmlAttribute("CLot")]
        [Bindable(true)]
        public string CLot { get; set; }

        public BonReceptionProductionDetailCollection BonReceptionDetailCollection;

        #endregion Propriétés

        public BonReceptionProduction(){}

        public static BonReceptionProduction Charger(string nBonProduction, string connectionString)
        {
            BonReceptionProduction bonReceptionProduction = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "BonReceptionProduction_Charger";
                    cmd.Parameters.AddWithValue("@NBonProduction", nBonProduction);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            bonReceptionProduction = new BonReceptionProduction();
                            bonReceptionProduction.NBonProduction = dr["NBonProduction"].ToString();

                            if (dr["CConditionnement"] != DBNull.Value)
                                bonReceptionProduction.CConditionnement = dr["CConditionnement"].ToString();
                            if (dr["CLot"] != DBNull.Value)
                                bonReceptionProduction.CLot = dr["CLot"].ToString();
                            if (dr["DateProduction"] != DBNull.Value)
                                bonReceptionProduction.DateProduction = DateTime.Parse(dr["DateProduction"].ToString());
                        }
                    }
                    bonReceptionProduction.BonReceptionDetailCollection = BonReceptionProductionDetailCollection.Charger(bonReceptionProduction.NBonProduction, connectionString);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bonReceptionProduction;
        }
    }
}