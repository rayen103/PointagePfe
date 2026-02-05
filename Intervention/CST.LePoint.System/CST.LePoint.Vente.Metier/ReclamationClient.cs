using CST.LePoint.Tiers.Referentiel;
using CST.LePoint.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace CST.LePoint.Vente.Metier
{
    public class ReclamationClient
    {
        #region Proprieté

        [XmlAttribute("IdReclamation")]
        [Bindable(true)]
        public int IdReclamation { get; set; }

        [XmlAttribute("CClient")]
        [Bindable(true)]
        public string CClient { get; set; }

        [XmlAttribute("Memo")]
        [Bindable(true)]
        public string Memo { get; set; }

        [XmlAttribute("DateReclamation")]
        [Bindable(true)]
        public DateTime DateReclamation { get; set; }

        [XmlAttribute("DateAnnulation")]
        [Bindable(true)]
        public DateTime DateAnnulation { get; set; }

        [XmlAttribute("BAnnulation")]
        [Bindable(true)]
        public bool BAnnulation { get; set; }

        [XmlAttribute("DateInsertion")]
        [Bindable(true)]
        public DateTime DateInsertion { get; set; }

        [XmlAttribute("DateModification")]
        [Bindable(true)]
        public DateTime DateModification { get; set; }

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

        #endregion Proprieté

        public ReclamationClient()
        {
        }

        public void Sauvegarder()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                Sauvegarder(transaction);
                transaction.Commit();
            }
        }

        public void Sauvegarder(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = transaction.Connection;
                cmd.Transaction = transaction;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ReclamationClient_Sauvegarder";
                cmd.Parameters.AddWithValue("@IdReclamation", IdReclamation);
                cmd.Parameters.AddWithValue("@CClient", CClient);
                cmd.Parameters.AddWithValue("@Memo", Memo);
                cmd.Parameters.AddWithValue("@DateReclamation", DateReclamation);
                cmd.Parameters.AddWithValue("@PCInsertion", PCInsertion);
                cmd.Parameters.AddWithValue("@CreePar", CreePar);
                cmd.Parameters.AddWithValue("@ModifiePar", ModifiePar);
                cmd.Parameters.AddWithValue("@PCModification", PCModification);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);

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

        public static ReclamationClient Charger(int idReclamation)
        {
            ReclamationClient reclamationClient = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "ReclamationClient_Charger";
                    cmd.Parameters.AddWithValue("@IdReclamation", idReclamation);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            reclamationClient = new ReclamationClient();
                            reclamationClient.IdReclamation = int.Parse(dr["IdReclamation"].ToString());
                            if (dr["CClient"] != DBNull.Value)
                                reclamationClient.CClient = dr["CClient"].ToString();
                            if (dr["DateReclamation"] != DBNull.Value)
                                reclamationClient.DateReclamation = DateTime.Parse(dr["DateReclamation"].ToString());
                            if (dr["BAnnulation"] != DBNull.Value)
                                reclamationClient.BAnnulation = bool.Parse(dr["BAnnulation"].ToString());
                            if (dr["Memo"] != DBNull.Value)
                                reclamationClient.Memo = dr["Memo"].ToString();
                            if (dr["DateAnnulation"] != DBNull.Value)
                                reclamationClient.DateAnnulation = DateTime.Parse(dr["DateAnnulation"].ToString());
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return reclamationClient;
        }

        public void Annuler()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    Annuler(transaction);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void Annuler(SqlTransaction transaction)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = transaction.Connection;
                cmd.Transaction = transaction;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ReclamationClient_Annuler";
                cmd.Parameters.AddWithValue("@IdReclamation", IdReclamation);

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
    }
    public class ReclamationClientCollection : List<ReclamationClient>
    {
        public static ReclamationClientCollection Charger(string cClient)
        {
            ReclamationClientCollection collection = new ReclamationClientCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "ReclamationClient_ChargerClient";
                    cmd.Parameters.AddWithValue("@CClient", cClient);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ReclamationClient reclamationClient = new ReclamationClient();
                            reclamationClient.IdReclamation = int.Parse(dr["IdReclamation"].ToString());
                            if (dr["CClient"] != DBNull.Value)
                                reclamationClient.CClient = dr["CClient"].ToString();
                            if (dr["DateReclamation"] != DBNull.Value)
                                reclamationClient.DateReclamation = DateTime.Parse(dr["DateReclamation"].ToString());
                            if (dr["BAnnulation"] != DBNull.Value)
                                reclamationClient.BAnnulation = bool.Parse(dr["BAnnulation"].ToString());
                            if (dr["Memo"] != DBNull.Value)
                                reclamationClient.Memo = dr["Memo"].ToString();
                            if (dr["DateAnnulation"] != DBNull.Value)
                                reclamationClient.DateAnnulation = DateTime.Parse(dr["DateAnnulation"].ToString());
                            collection.Add(reclamationClient);
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