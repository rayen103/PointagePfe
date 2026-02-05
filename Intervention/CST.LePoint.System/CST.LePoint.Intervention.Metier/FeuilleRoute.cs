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
    public class FeuilleRoute
    {
        #region Proriétès

        [XmlAttribute("NFeuilleRoute")]
        [Bindable(true)]
        public string NFeuilleRoute { get; set; }

        [XmlAttribute("CEquipe")]
        [Bindable(true)]
        public string CEquipe { get; set; }

        [XmlAttribute("DateFeuilleRoute")]
        [Bindable(true)]
        public DateTime DateFeuilleRoute { get; set; }

        [XmlAttribute("BAnnuler")]
        [Bindable(true)]
        public bool BAnnuler { get; set; }

        [XmlAttribute("DateInsertion")]
        [Bindable(true)]
        public DateTime? DateInsertion { get; set; }

        [XmlAttribute("DateModification")]
        [Bindable(true)]
        public DateTime? DateModification { get; set; }

        [XmlAttribute("CreePar")]
        [Bindable(true)]
        public int CreePar { get; set; }

        [XmlAttribute("ModifiePar")]
        [Bindable(true)]
        public int ModifiePar { get; set; }

        [XmlAttribute("PCInsertion")]
        [Bindable(true)]
        public string PCInsertion { get; set; }

        [XmlAttribute("PCModification")]
        [Bindable(true)]
        public string PCModification { get; set; }

        #endregion

        public void Inserer(string CEquipe, DateTime DateFeuilleRoute, string Cequipeold)
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();

                try
                {
                    Inserer(transaction, CEquipe, DateFeuilleRoute, Cequipeold);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void Inserer(SqlTransaction transaction, string CEquipe, DateTime DateFeuilleRoute, string Cequipeold)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Feuille_Route_Inserer";

                cmd.Parameters.AddWithValue("@NFeuilleRoute", this.NFeuilleRoute);
                cmd.Parameters.AddWithValue("@CEquipe", CEquipe);
                cmd.Parameters.AddWithValue("@CEquipeOLD", Cequipeold);
                cmd.Parameters.AddWithValue("@DateFeuilleRoute", DateFeuilleRoute);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);
                cmd.Parameters.AddWithValue("@CreePar", this.CreePar);

                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        this.NFeuilleRoute = dr["NFeuilleRoute"].ToString();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Modifier()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();

                try
                {
                    Modifier(transaction);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void Modifier(SqlTransaction transaction)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Feuille_Route_Modifier";

                cmd.Parameters.AddWithValue("@CEquipe", this.NFeuilleRoute);

                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        this.NFeuilleRoute = dr["NumFeuilleRoute"].ToString();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
