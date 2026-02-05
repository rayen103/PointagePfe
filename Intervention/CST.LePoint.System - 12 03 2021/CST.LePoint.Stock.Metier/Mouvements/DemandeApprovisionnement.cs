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
    public class DemandeApprovisionnement
    {
        [XmlAttribute("NDemande")]
        [Bindable(true)]
        public string NDemande { get; set; }

        [XmlAttribute("CEntrepot")]
        [Bindable(true)]
        public string CEntrepot { get; set; }

        [XmlAttribute("DateDemande")]
        [Bindable(true)]
        public DateTime DateDemande { get; set; }

        [XmlAttribute("DateLivraisonPlanifiee")]
        [Bindable(true)]
        public DateTime DateLivraisonPlanifiee { get; set; }

        [XmlAttribute("Indice")]
        [Bindable(true)]
        public int Indice { get; set; }

        [XmlAttribute("Demandeur")]
        [Bindable(true)]
        public string Demandeur { get; set; }

        [XmlAttribute("Exercice")]
        [Bindable(true)]
        public string Exercice { get; set; }

        [XmlAttribute("DateInsertion")]
        [Bindable(true)]
        public DateTime DateInsertion { get; set; }

        [XmlAttribute("PCInsertion")]
        [Bindable(true)]
        public string PCInsertion { get; set; }

        [XmlAttribute("CreePar")]
        [Bindable(true)]
        public int CreePar { get; set; }

        [XmlAttribute("DateModification")]
        [Bindable(true)]
        public DateTime DateModification { get; set; }

        [XmlAttribute("PCModification")]
        [Bindable(true)]
        public string PCModification { get; set; }

        [XmlAttribute("ModifiePar")]
        [Bindable(true)]
        public int ModifiePar { get; set; }

        public DemandeApprovisionnementDetailCollection DemandeApprovisionnementDetailCollection;

        public DemandeApprovisionnement()
        {
            this.CEntrepot = string.Empty;
            this.NDemande = string.Empty;

            this.DemandeApprovisionnementDetailCollection = new DemandeApprovisionnementDetailCollection();
        }

        public void Inserer()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    this.Inserer(transaction);

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public void Inserer(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DemandeApprovisionnement_Inserer";
                cmd.Parameters.AddWithValue("@DateDemande", this.DateDemande);
                cmd.Parameters.AddWithValue("@DateLivraisonPlanifiee", this.DateLivraisonPlanifiee);
                cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
                cmd.Parameters.AddWithValue("@Demandeur", this.Demandeur);
                cmd.Parameters.AddWithValue("@Exercice ", this.Exercice);
                cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);
                cmd.Parameters.AddWithValue("@CreePar", this.CreePar);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);

                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        this.NDemande = dr["NDemande"].ToString();
                        this.Indice = int.Parse(dr["DernierIndice"].ToString());
                    }
                }
                int i = 1;
                foreach (DemandeApprovisionnementDetail demandeApprovisionnementDetail in DemandeApprovisionnementDetailCollection)
                {
                    demandeApprovisionnementDetail.NDemande = this.NDemande;
                    //Restitution et Insertion detail
                    demandeApprovisionnementDetail.Ordre = i++;
                    demandeApprovisionnementDetail.Inserer(transaction);
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
                    this.Modifier(transaction);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public void Modifier(SqlTransaction transaction)
        {
            try
            {
                this.RestituerStockEnCommandeDAP(transaction);

                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DemandeApprovisionnement_Modifier";
                cmd.Parameters.AddWithValue("@NDemande", this.NDemande);
                cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
                cmd.Parameters.AddWithValue("@DateLivraisonPlanifiee", this.DateLivraisonPlanifiee);
                cmd.Parameters.AddWithValue("@DateDemande", this.DateDemande);
                cmd.Parameters.AddWithValue("@Demandeur", this.Demandeur);
                cmd.Parameters.AddWithValue("@PCModification", this.PCModification);
                cmd.Parameters.AddWithValue("@ModifiePar", this.ModifiePar);
                cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                cmd.ExecuteNonQuery();
                int i = 1;
                foreach (DemandeApprovisionnementDetail demandeApprovisionnementDetail in DemandeApprovisionnementDetailCollection)
                {
                    demandeApprovisionnementDetail.NDemande = this.NDemande;
                    demandeApprovisionnementDetail.Ordre = i++;
                    demandeApprovisionnementDetail.Inserer(transaction);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Supprimer()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    Supprimer(transaction);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
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
                cmd.CommandText = "DemandeApprovisionnement_Supprimer";
                cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
                cmd.Parameters.AddWithValue("@NDemande", this.NDemande);

                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DemandeApprovisionnement Charger(string nDemande, string cEntrepot)
        {
            DemandeApprovisionnement demandeApprovisionnement = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "DemandeApprovisionnement_Charger";
                    cmd.Parameters.AddWithValue("@NDemande", nDemande);
                    cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                    foreach (SqlParameter parametre in cmd.Parameters)
                    {
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;
                    }

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            demandeApprovisionnement = new DemandeApprovisionnement();

                            demandeApprovisionnement.NDemande = dr["NDemande"].ToString();
                            demandeApprovisionnement.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["Demandeur"] != DBNull.Value)
                                demandeApprovisionnement.Demandeur = dr["Demandeur"].ToString();
                            if (dr["DateLivraisonPlanifiee"] != DBNull.Value)
                                demandeApprovisionnement.DateLivraisonPlanifiee = DateTime.Parse(dr["DateLivraisonPlanifiee"].ToString());
                            if (dr["DateDemande"] != DBNull.Value)
                                demandeApprovisionnement.DateDemande = DateTime.Parse(dr["DateDemande"].ToString());
                            if (demandeApprovisionnement != null)
                                demandeApprovisionnement.DemandeApprovisionnementDetailCollection = DemandeApprovisionnementDetailCollection.Charger(demandeApprovisionnement.NDemande, demandeApprovisionnement.CEntrepot);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return demandeApprovisionnement;
        }

        public static string RecupererNumeroDemandeApprovisionnement(string exercice, string cEntrepot, out int indice)
        {
            string nDemande = string.Empty;
            indice = 0;

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = cn;
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.CommandText = "DemandeApprovisionnement_RecupererNouveauNumero";
                cmd2.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                cmd2.Parameters.AddWithValue("@Exercice", exercice);
                SqlDataReader dr = cmd2.ExecuteReader();
                if (dr.Read())
                {
                    nDemande = dr["NDemande"].ToString();
                    indice = int.Parse(dr["DernierIndice"].ToString());
                }
                dr.Close();
            }

            return nDemande;
        }

        private void RestituerStockEnCommandeDAP(SqlTransaction transaction)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Transaction = transaction;
            cmd.Connection = transaction.Connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DemandeApprovisionnementDetail_RestituerStockEnCommandeDAP";
            cmd.Parameters.AddWithValue("@CEntrepot", CEntrepot);
            cmd.Parameters.AddWithValue("@NDemande", NDemande);
            foreach (SqlParameter parametre in cmd.Parameters)
                if (parametre.Value == null)
                    parametre.Value = DBNull.Value;

            cmd.ExecuteNonQuery();
        }

        public static string RecupererNumeroDemandeApprovisionnement(string exercice, string cEntrepot)
        {
            int indice = 0;
            return DemandeApprovisionnement.RecupererNumeroDemandeApprovisionnement(exercice, cEntrepot, out indice);
        }
    }

    [Serializable]
    public class DemandeApprovisionnementCollection : List<DemandeApprovisionnement>
    {
        public static DataSet ChargerVue(DateTime dateDebut, DateTime dateFin)
        {
            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DemandeApprovisionnementListe_Rpt_Charger";
                cmd.Parameters.AddWithValue("@NDemande", DBNull.Value);
                cmd.Parameters.AddWithValue("@CEntrepot", DBNull.Value);
                cmd.Parameters.AddWithValue("@DateDeb", dateDebut);
                cmd.Parameters.AddWithValue("@DateFin", dateFin);
                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "DemandeApprovisionnementListe_Rpt_Charger");
            }
            return (ds);
        }

        public static DemandeApprovisionnementCollection Charger()
        {
            DemandeApprovisionnementCollection demandes = new DemandeApprovisionnementCollection();
            DemandeApprovisionnement demande = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "DemandeApprovisionnement_Charger";
                    cmd.Parameters.AddWithValue("@NDemande", DBNull.Value);
                    cmd.Parameters.AddWithValue("@CEntrepot", DBNull.Value);
                    foreach (SqlParameter parametre in cmd.Parameters)
                    {
                        if (parametre.Value == null)
                        {
                            parametre.Value = DBNull.Value;
                        }
                    }

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            demande = new DemandeApprovisionnement();

                            demande.NDemande = dr["NDemande"].ToString();
                            demande.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["DateDemande"] != DBNull.Value)
                                demande.DateDemande = DateTime.Parse(dr["DateDemande"].ToString());
                            if (dr["DateLivraisonPlanifiee"] != DBNull.Value)
                                demande.DateLivraisonPlanifiee = DateTime.Parse(dr["DateLivraisonPlanifiee"].ToString()); ;
                            if (dr["Demandeur"] != DBNull.Value)
                                demande.Demandeur = dr["Demandeur"].ToString();
                            demande.DemandeApprovisionnementDetailCollection = DemandeApprovisionnementDetailCollection.Charger(demande.NDemande, demande.CEntrepot);
                            demandes.Add(demande);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return demandes;
        }

        public static DemandeApprovisionnementCollection Charger(string cEntrepot)
        {
            DemandeApprovisionnementCollection demandes = new DemandeApprovisionnementCollection();
            DemandeApprovisionnement demande = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "DemandeApprovisionnement_VueLkp_Charger";
                    cmd.Parameters.AddWithValue("@NDemande", DBNull.Value);
                    cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                    foreach (SqlParameter parametre in cmd.Parameters)
                    {
                        if (parametre.Value == null)
                        {
                            parametre.Value = DBNull.Value;
                        }
                    }

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            demande = new DemandeApprovisionnement();

                            demande.NDemande = dr["NDemande"].ToString();
                            demande.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["DateDemande"] != DBNull.Value)
                                demande.DateDemande = DateTime.Parse(dr["DateDemande"].ToString());
                            if (dr["DateLivraisonPlanifiee"] != DBNull.Value)
                                demande.DateLivraisonPlanifiee = DateTime.Parse(dr["DateLivraisonPlanifiee"].ToString()); ;
                            if (dr["Demandeur"] != DBNull.Value)
                                demande.Demandeur = dr["Demandeur"].ToString();
                            demande.DemandeApprovisionnementDetailCollection = DemandeApprovisionnementDetailCollection.Charger(demande.NDemande, demande.CEntrepot);
                            demandes.Add(demande);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return demandes;
        }
    }
}