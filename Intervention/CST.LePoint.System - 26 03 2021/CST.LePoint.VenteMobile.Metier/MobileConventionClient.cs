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
    public class MobileConventionClient
    {


        #region Proprietes
        [XmlAttribute("NConvention")]
        [Bindable(true)]
        public string NConvention { get; set; }

        [XmlAttribute("DatePlanif")]
        [Bindable(true)]
        public DateTime ? DatePlanif { get; set; }
          [XmlAttribute("DateInsertion")]
        [Bindable(true)]
        public DateTime ? DateInsertion { get; set; }
          [XmlAttribute("CreePar")]
          [Bindable(true)]
          public int CreePar { get; set; }
          [XmlAttribute("PCInsertion")]
          [Bindable(true)]
          public string PCInsertion { get; set; }
          [XmlAttribute("TIntervention ")]
          [Bindable(true)]
          public int TIntervention { get; set; }
          [XmlAttribute("CTypeVisite ")]
          [Bindable(true)]
          public string CTypeVisite { get; set; }
        #endregion Proprietes

        public MobileConventionClient()
        {

        }
        public bool SauvegarderConvention(SqlTransaction transaction)
        {
     
            bool msg;

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Mobile_ConventionClient_Inserer";
                cmd.Parameters.AddWithValue("@NConvention", NConvention);
                cmd.Parameters.AddWithValue("@DatePlanif", DatePlanif);
                cmd.Parameters.AddWithValue("@CreePar", CreePar);
                cmd.Parameters.AddWithValue("@DateInsertion", DateInsertion);
                cmd.Parameters.AddWithValue("@PCInsertion", PCInsertion);
                cmd.Parameters.AddWithValue("@TIntervention",TIntervention);
                cmd.Parameters.AddWithValue("@CTypeVisite",CTypeVisite);


                cmd.ExecuteNonQuery();
                msg = true;
            }
            catch (Exception)
            {
                msg = false;
                throw;
            }


            return (msg);
        

        }
    }


}