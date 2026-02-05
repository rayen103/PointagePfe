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
    public class Consommation : Item
    {
        #region Propriétés

        [XmlAttribute("NConsommation")]
        [Bindable(true)]
        public string NConsommation { get; set; }
        [XmlAttribute("NRattachement")]
        [Bindable(true)]
        public string NRattachement { get; set; }
        [XmlAttribute("NChantier")]
        [Bindable(true)]
        public string NChantier { get; set; }
        [XmlAttribute("CClient")]
        [Bindable(true)]
        public string CClient { get; set; }
        [XmlAttribute("CArticle")]
        [Bindable(true)]
        public string CArticle { get; set; }
        [XmlAttribute("Qté")]
        [Bindable(true)]
        public int Qté { get; set; }

        [XmlAttribute("Libelle")]
        [Bindable(true)]
        public string Libelle { get; set; }

        
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
        public DateTime DateModification { get; set; }

        [XmlAttribute("PCInsertion")]
        [Bindable(true)]
        public string PCInsertion { get; set; }

        [XmlAttribute("PCModification")]
        [Bindable(true)]
        public string PCModification { get; set; }

        #endregion Propriétés

        public Consommation()
        {
          
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
                    cmd.CommandText = "GI_FamilleOperation_Sauvegarder";

                    cmd.Parameters.AddWithValue("@NConsommation", NConsommation);
                    cmd.Parameters.AddWithValue("@NRattachement", NRattachement);
                    cmd.Parameters.AddWithValue("@NChantier", NChantier);
                    cmd.Parameters.AddWithValue("@CClient", CClient);
                    cmd.Parameters.AddWithValue("@CArticle", CArticle);
                    cmd.Parameters.AddWithValue("@Qté", Qté);
                    cmd.Parameters.AddWithValue("@Libelle", Libelle);

                    cmd.Parameters.AddWithValue("@CreePar", CreePar);
                    cmd.Parameters.AddWithValue("@ModifiePar", ModifiePar);
                    cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                    cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                    cmd.Parameters.AddWithValue("@PCInsertion", PCInsertion);
                    cmd.Parameters.AddWithValue("@PCModification", this.PCModification);

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
        public static Consommation Charger(string NConsommation)
        {
            Consommation consommation = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "GI_FamilleOperation_Charger";
                    cmd.Parameters.AddWithValue("@NConsommation", NConsommation);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            consommation = new Consommation();

                            consommation.NConsommation = dr["NConsommation"].ToString();
                            consommation.NRattachement = dr["NRattachement"].ToString();
                            consommation.NChantier = dr["NChantier"].ToString();
                            consommation.CClient = dr["CClient"].ToString();
                            consommation.CArticle = dr["CArticle"].ToString();
                         
                            if (dr["Libelle"] != DBNull.Value)
                                consommation.Libelle = dr["Libelle"].ToString();
                            if (dr["Qté"] != DBNull.Value)
                                consommation.Qté = int.Parse(dr["Qté"].ToString());
                            
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return consommation;
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
                    cmd.CommandText = "GI_FamilleOperation_Supprimer";
                    cmd.Parameters.AddWithValue("@NConsommation", NConsommation);

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

    }

    public class ConsommationCollection : ItemCollection
    {

        public static ConsommationCollection Charger()
        {
            ConsommationCollection collection = new ConsommationCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "GI_FamilleOperation_Charger";
                    cmd.Parameters.AddWithValue("@NConsommation", DBNull.Value);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Consommation consommation = new Consommation();

                            consommation.NConsommation = dr["NConsommation"].ToString();
                            consommation.NRattachement = dr["NRattachement"].ToString();
                            consommation.NChantier = dr["NChantier"].ToString();
                            consommation.CClient = dr["CClient"].ToString();
                            consommation.CArticle = dr["CArticle"].ToString();

                            if (dr["Libelle"] != DBNull.Value)
                                consommation.Libelle = dr["Libelle"].ToString();
                            if (dr["Qté"] != DBNull.Value)
                                consommation.Qté = int.Parse(dr["Qté"].ToString());

                            collection.Add(consommation);
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
