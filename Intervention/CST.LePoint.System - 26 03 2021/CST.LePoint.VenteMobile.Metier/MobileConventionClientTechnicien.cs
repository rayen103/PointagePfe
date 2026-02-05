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
    public class MobileConventionClientTechnicien
    {


        #region Proprietes
        [XmlAttribute("NConvention")]
        [Bindable(true)]
        public string NConvention { get; set; }
        [XmlAttribute("CreerPar")]
        [Bindable(true)]
        public int CreerPar { get; set; }
        [XmlAttribute("BValid ")]
        [Bindable(true)]
        public int BValid { get; set; }

        [XmlAttribute("DatePlanification ")]
        [Bindable(true)]
        public DateTime  DatePlanification { get; set; }
        #endregion Proprietes

        public MobileConventionClientTechnicien()
        {

        }
        public bool ModifierConventionTechnicien(SqlTransaction transaction)
        {

            bool msg;

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Mobile_ConventionClient_Technicien_Update";
                cmd.Parameters.AddWithValue("@NConvention", NConvention);
                cmd.Parameters.AddWithValue("@BValid", BValid);
                cmd.Parameters.AddWithValue("@CreerPar", CreerPar);
                cmd.Parameters.AddWithValue("@DatePlanification",DatePlanification.ToString("yyyy-MM-dd"));
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