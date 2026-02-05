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
    class MobileRattachementOptions
    {

        #region Propriétés

        [XmlAttribute("NRattachement")]
        [Bindable(true)]
        public string NRattachement { get; set; }

        [XmlAttribute("COptions")]
        [Bindable(true)]
        public string COptions { get; set; }

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
        public DateTime  DateModification { get; set; }

        [XmlAttribute("PCInsertion")]
        [Bindable(true)]
        public string PCInsertion { get; set; }

        [XmlAttribute("PCModification")]
        [Bindable(true)]
        public string PCModification { get; set; }

        #endregion Propriétés

        public MobileRattachementOptions()
        {

        }

        public void Sauvegarder(SqlTransaction transaction, string n, string Carticle, string CNoteRattachement,int equipe, string user)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GP_RattachementOptions_Sauvegarder";
                cmd.Parameters.AddWithValue("@NRattachement", n);
                cmd.Parameters.AddWithValue("@COptions", CNoteRattachement);
                cmd.Parameters.AddWithValue("@CArticle", Carticle);
                //cmd.Parameters.AddWithValue("@CreePar",equipe);
                //cmd.Parameters.AddWithValue("@ModifiePar", ModifiePar);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                cmd.Parameters.AddWithValue("@PCInsertion", user);
                cmd.Parameters.AddWithValue("@PCModification", PCModification);

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
                Sauvegarder(transaction,"","","", 0,"");
            }
        }

    }
}
