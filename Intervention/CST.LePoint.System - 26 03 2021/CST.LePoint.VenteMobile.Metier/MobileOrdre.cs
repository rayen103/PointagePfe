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
    public class MobileOrdre
    {
        #region Proprietes

        public string NumOrdre { get; set; }
        public string date_plannification { get; set; }
        public string raisonsociale { get; set; }
        public string dernierVisite { get; set; }
        public string cclient { get; set; }
        public decimal lng { get; set; }
        public decimal lat { get; set; }
        public string tel1 { get; set; }
        public string tel2 { get; set; }
        public string ville { get; set; }
        public string Adresse { get; set; }
        public string Cetablissement { get; set; }
        public string Elib { get; set; }
        public string daycount { get; set; }
        public string DateRattachement { get; set; }
        public string DateCommande { get; set; }
        public string rattachement { get; set; }
        public string heured { get; set; }
        public string heuref { get; set; }
        public string duree { get; set; }
        public string DateLivraison { get; set; }
        public string NBonCommande { get; set; }
        public string Observations { get; set; }
        public string Etat { get; set; }
        public decimal Montant { get; set; }
        public string ObservationClient { get; set; }
        public string Objectif { get; set; }
        public string Recommandation { get; set; }

        #endregion Proprietes
        
        public MobileOrdre()
        {

        }
        
        public bool ReclamationOrdre(string ordre, string reclamation)
        {
            bool msg;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Mobile_OT_reclamation_Inserer";
                    cmd.Parameters.AddWithValue("@NOrdredeTravail", ordre);
                    cmd.Parameters.AddWithValue("@Reclamation", reclamation);
                    cmd.ExecuteNonQuery();
                    msg = true;
                }
                catch (Exception)
                {
                    msg = false;
                    throw;
                }
            }

            return (msg);
        }

        public bool UpdateOrdre(SqlTransaction transaction, string NOrdredeTravail, string NBonCommande, string CEquipe, string ModifierPar)
        {
            bool msg;

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Mobile_OT_Modifier";
                cmd.Parameters.AddWithValue("@NOrdredeTravail", NOrdredeTravail);
                cmd.Parameters.AddWithValue("@NBonCommande", NBonCommande);
                cmd.Parameters.AddWithValue("@CEquipe", CEquipe + '|' + ModifierPar);
                //cmd.Parameters.AddWithValue("@ModifierPar", ModifierPar);
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

        public bool UpdateCrmOrdre(SqlTransaction transaction, string NOrdredeTravail,string CEquipe, string ModifierPar)
        {
            bool msg;

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "MobileCrm_OT_Modifier";
                cmd.Parameters.AddWithValue("@NOrdredeTravail", NOrdredeTravail);
                cmd.Parameters.AddWithValue("@ModifierPar", CEquipe + '|' + ModifierPar);
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

        public bool validOrdre(string ordre)
        {
            bool msg;
            MobileOrdreCollection ordreMobile = new MobileOrdreCollection();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Mobile_OT_valider";
                    cmd.Parameters.AddWithValue("@NOrdredeTravail", ordre);
                    cmd.ExecuteNonQuery();
                    msg = true;
                }
                catch (Exception)
                {
                    msg = false;
                    throw;
                }
            }

            return (msg);
        }
    }

    [Serializable]
    public class MobileOrdreCollection : List<MobileOrdre>
    {
        public MobileOrdreCollection()
        {
        }

        public static int NombreManquer(string CEquipe)
        {
            int nombre = 0; 
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader reader;
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Mobile_Visite_OT_Manquer";
                    cmd.Parameters.AddWithValue("@CEquipe", CEquipe);
                    foreach (SqlParameter parameter in cmd.Parameters)
                        if (parameter.Value == null)
                            parameter.Value = DBNull.Value;
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        nombre = reader["Nombre"] is DBNull ? 0 : int.Parse(reader["Nombre"].ToString());
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return nombre;
        }

        public static MobileOrdreCollection planifieCharger(string code_commercial, string Datedebut, string Datefin, string CRegion, string CGouvernorat)
        {
            MobileOrdreCollection ordreMobile = new MobileOrdreCollection();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader reader;
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Mobile_OT_planifier_charger";
                    cmd.Parameters.AddWithValue("@code_commercial", code_commercial);
                    cmd.Parameters.AddWithValue("@Datedebut", Datedebut);
                    cmd.Parameters.AddWithValue("@Datefin", Datefin);
                    cmd.Parameters.AddWithValue("@CRegion", string.IsNullOrEmpty(CRegion) ? null : CRegion );
                    cmd.Parameters.AddWithValue("@CGouvernorat", string.IsNullOrEmpty(CGouvernorat) ? null : CGouvernorat);
                    foreach (SqlParameter parameter in cmd.Parameters)
                        if (parameter.Value == null)
                            parameter.Value = DBNull.Value;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        MobileOrdre ordre = new MobileOrdre();
                        ordre.NumOrdre = reader["NOrdredeTravail"] == DBNull.Value ? "" : reader["NOrdredeTravail"].ToString();
                        ordre.date_plannification = reader["DatePlanification"] == DBNull.Value ? "" : DateTime.Parse(reader["DatePlanification"].ToString()).ToString("dd/MM/yyyy");
                        ordre.raisonsociale = reader["RaisonSociale"] == DBNull.Value ? "" : reader["RaisonSociale"].ToString();
                        ordre.dernierVisite = reader["dernierVisite"] == DBNull.Value ? "" : DateTime.Parse(reader["dernierVisite"].ToString()).ToString("dd/MM/yyyy");
                        ordre.cclient = reader["CClient"] == DBNull.Value ? "" : reader["CClient"].ToString();
                        ordre.lng = reader["Longitude"] == DBNull.Value ? 0 : Decimal.Parse(reader["Longitude"].ToString());
                        ordre.lat = reader["Latitude"] == DBNull.Value ? 0 : Decimal.Parse(reader["Latitude"].ToString());
                        ordre.tel1 = reader["tel1"] == DBNull.Value ? "" : reader["tel1"].ToString(); 
                        ordre.tel2 = reader["tel2"] == DBNull.Value ? "" : reader["tel2"].ToString(); 
                        ordre.ville = reader["Ville"] == DBNull.Value  ? "" : reader["Ville"].ToString();
                        ordre.Adresse = reader["Adresse"] == DBNull.Value  ? "" : reader["Adresse"].ToString();
                        //ordre.Cetablissement = reader["Cetablissement"] == DBNull.Value  ? "" : reader["Cetablissement"].ToString(); ;
                        //ordre.Elib = reader["Libelle"] == DBNull.Value  ? "" : reader["Libelle"].ToString();
                        ordre.Montant = reader["Montant"] == DBNull.Value ? 0 : Decimal.Parse(reader["Montant"].ToString());
                        ordre.ObservationClient = reader["ObservationClient"] == DBNull.Value ? "" : reader["ObservationClient"].ToString();
                        ordre.Objectif = reader["Objectif"] == DBNull.Value ? "" : reader["Objectif"].ToString();
                        ordre.Etat = reader["Etat"] == DBNull.Value ? "" : reader["Etat"].ToString();
                        ordre.Recommandation = reader["Recommandation"] == DBNull.Value ? "" : reader["Recommandation"].ToString();
                        ordreMobile.Add(ordre);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return (ordreMobile);
        }

        public static MobileOrdreCollection encoursCharger(string code_commercial, string Datedebut, string Datefin, string CRegion, string CGouvernorat)
        {
            MobileOrdreCollection ordreMobile = new MobileOrdreCollection();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader reader;
                    cmd.CommandText = "Mobile_OT_encours_charger";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@commercial", code_commercial);
                    cmd.Parameters.AddWithValue("@Datedebut", Datedebut);
                    cmd.Parameters.AddWithValue("@Datefin", Datefin);
                    cmd.Parameters.AddWithValue("@CRegion", CRegion);
                    cmd.Parameters.AddWithValue("@CGouvernorat", CGouvernorat);
                    cmd.Connection = connection;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        MobileOrdre ordre = new MobileOrdre();
                        ordre.NumOrdre = reader["NOrdredeTravail"] == DBNull.Value ? "" : reader["NOrdredeTravail"].ToString();
                        ordre.rattachement = reader["Nrattachement"] == DBNull.Value ? "" : reader["Nrattachement"].ToString();
                        ordre.date_plannification = reader["DatePlanification"] == DBNull.Value ? "" : DateTime.Parse(reader["DatePlanification"].ToString()).ToString("dd/MM/yyyy");
                        ordre.DateRattachement = reader["DateRattachement"] == DBNull.Value ? "" :reader["DateRattachement"].ToString();
                        ordre.raisonsociale = reader["RaisonSociale"] == DBNull.Value ? "" : reader["RaisonSociale"].ToString();
                        ordre.cclient = reader["CClient"] == DBNull.Value ? "" : reader["CClient"].ToString();
                        //ordre.Cetablissement = reader["Cetablissement"] == DBNull.Value ? "" : reader["Cetablissement"].ToString(); ;
                        //ordre.Elib = reader["Libelle"] == DBNull.Value ? "" : reader["Libelle"].ToString();
                        ordre.daycount = reader["daycount"].ToString();
                        ordreMobile.Add(ordre);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return (ordreMobile);
        }

        public static MobileOrdreCollection validesCharger(string code_commercial, string Datedebut, string Datefin, string CRegion, string CGouvernorat)
        {
            MobileOrdreCollection ordreMobile = new MobileOrdreCollection();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader reader;
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Mobile_OT_valide_charger";
                    cmd.Parameters.AddWithValue("@code_commercial", code_commercial);
                    cmd.Parameters.AddWithValue("@Datedebut", Datedebut);
                    cmd.Parameters.AddWithValue("@Datefin", Datefin);
                    cmd.Parameters.AddWithValue("@CRegion", CRegion);
                    cmd.Parameters.AddWithValue("@CGouvernorat", CGouvernorat);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        MobileOrdre ordre = new MobileOrdre();
                        ordre.NumOrdre = reader["NOrdredeTravail"] == DBNull.Value ? "" : reader["NOrdredeTravail"].ToString();
                        ordre.date_plannification = reader["DatePlanification"] == DBNull.Value ? "" : reader["DatePlanification"].ToString();
                        ordre.DateCommande = reader["DateCommande"] == DBNull.Value ? "" : DateTime.Parse(reader["DateCommande"].ToString()).ToString("dd/MM/yyyy");
                        ordre.raisonsociale = reader["RaisonSociale"] == DBNull.Value ? "" : reader["RaisonSociale"].ToString();
                        ordre.cclient = reader["CClient"] == DBNull.Value ? "" : reader["CClient"].ToString();
                        ordre.DateLivraison = reader["DateLivraison"] == DBNull.Value ? "" : reader["DateLivraison"].ToString();
                        if (reader["NBonCommande"] != DBNull.Value) ordre.NBonCommande = reader["NBonCommande"].ToString();
                        //ordre.Observations = reader["Observations"] == DBNull.Value ? string.Empty : "mahich null";
                        if (reader["Observations"] == DBNull.Value || reader["Observations"].ToString().ToUpper().Equals("NULL"))                        
                            ordre.Observations = "";                        
                        else
                            ordre.Observations = reader["Observations"].ToString();
                        ordre.Etat = reader["Etat"] == DBNull.Value ? "" : reader["Etat"].ToString(); ;
                        ordre.duree = reader["duree"] == DBNull.Value ? "" : reader["duree"].ToString();
                        ordreMobile.Add(ordre);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return (ordreMobile);
        }

        public static int CRMNombreManquer(string CEquipe)
        {
            int nombre = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader reader;
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Mobile_Visite_CRM_Manquer";
                    cmd.Parameters.AddWithValue("@CEquipe", CEquipe);
                    foreach (SqlParameter parameter in cmd.Parameters)
                        if (parameter.Value == null)
                            parameter.Value = DBNull.Value;
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        nombre = reader["Nombre"] is DBNull ? 0 : int.Parse(reader["Nombre"].ToString());
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return nombre;
        }

        public static MobileOrdreCollection CrmencoursCharger(string code_commercial, string Datedebut, string Datefin, string CRegion, string CGouvernorat)
        {
            MobileOrdreCollection ordreMobile = new MobileOrdreCollection();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader reader;
                    cmd.CommandText = "Mobile_CRM_OT_encours_charger";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@commercial", code_commercial);
                    cmd.Parameters.AddWithValue("@Datedebut", Datedebut);
                    cmd.Parameters.AddWithValue("@Datefin", Datefin);
                    cmd.Parameters.AddWithValue("@CRegion", CRegion);
                    cmd.Parameters.AddWithValue("@CGouvernorat", CGouvernorat);
                    cmd.Connection = connection;
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value; 
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        MobileOrdre ordre = new MobileOrdre();
                        ordre.NumOrdre = reader["NOrdredeTravail"] == DBNull.Value ? "" : reader["NOrdredeTravail"].ToString();
                        ordre.rattachement = reader["Nrattachement"] == DBNull.Value ? "" : reader["Nrattachement"].ToString();
                        ordre.date_plannification = reader["DatePlanification"] == DBNull.Value ? "" : reader["DatePlanification"].ToString();
                        ordre.DateRattachement = reader["DateRattachement"] == DBNull.Value ? "" : reader["DateRattachement"].ToString();
                        ordre.raisonsociale = reader["RaisonSociale"] == DBNull.Value ? "" : reader["RaisonSociale"].ToString();
                        ordre.cclient = reader["CClient"] == DBNull.Value ? "" : reader["CClient"].ToString();
                        //ordre.Cetablissement = reader["Cetablissement"] == DBNull.Value ? "" : reader["Cetablissement"].ToString(); ;
                        //ordre.Elib = reader["Libelle"] == DBNull.Value ? "" : reader["Libelle"].ToString();
                        ordre.daycount = reader["daycount"].ToString();
                        ordreMobile.Add(ordre);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return (ordreMobile);
        }

        public static MobileOrdreCollection CrmplanifieCharger(string code_commercial, string Datedebut, string Datefin, string CRegion, string CGouvernorat)
        {
            MobileOrdreCollection ordreMobile = new MobileOrdreCollection();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader reader;
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Mobile_CRM_OT_planifier_charger";
                    cmd.Parameters.AddWithValue("@code_commercial", code_commercial);
                    cmd.Parameters.AddWithValue("@Datedebut", Datedebut);
                    cmd.Parameters.AddWithValue("@Datefin", Datefin);
                    cmd.Parameters.AddWithValue("@CRegion", CRegion);
                    cmd.Parameters.AddWithValue("@CGouvernorat", CGouvernorat);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value; 
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        MobileOrdre ordre = new MobileOrdre();
                        ordre.NumOrdre = reader["NOrdredeTravail"] == DBNull.Value ? "" : reader["NOrdredeTravail"].ToString();
                        ordre.date_plannification = reader["DatePlanification"] == DBNull.Value ? "" : DateTime.Parse(reader["DatePlanification"].ToString()).ToString("dd/MM/yyyy");
                        ordre.raisonsociale = reader["RaisonSociale"] == DBNull.Value ? "" : reader["RaisonSociale"].ToString();
                        ordre.dernierVisite = reader["dernierVisite"] == DBNull.Value ? "" : DateTime.Parse(reader["dernierVisite"].ToString()).ToString("dd/MM/yyyy");
                        ordre.cclient = reader["CClient"] == DBNull.Value ? "" : reader["CClient"].ToString();
                        ordre.lng = reader["Longitude"] == DBNull.Value ? 0 : Decimal.Parse(reader["Longitude"].ToString());
                        ordre.lat = reader["Latitude"] == DBNull.Value ? 0 : Decimal.Parse(reader["Latitude"].ToString());
                        ordre.tel1 = reader["tel1"] == DBNull.Value ? "" : reader["tel1"].ToString();
                        ordre.tel2 = reader["tel2"] == DBNull.Value ? "" : reader["tel2"].ToString();
                        ordre.ville = reader["Ville"] == DBNull.Value ? "" : reader["Ville"].ToString();
                        ordre.Adresse = reader["Adresse"] == DBNull.Value ? "" : reader["Adresse"].ToString();
                        //ordre.Cetablissement = reader["Cetablissement"] == DBNull.Value ? "" : reader["Cetablissement"].ToString(); ;
                        //ordre.Elib = reader["Libelle"] == DBNull.Value ? "" : reader["Libelle"].ToString();
                        ordre.Montant = reader["Montant"] == DBNull.Value ? 0 : Decimal.Parse(reader["Montant"].ToString());
                        ordre.ObservationClient = reader["ObservationClient"] == DBNull.Value ? "" : reader["ObservationClient"].ToString();
                        ordre.Objectif = reader["Objectif"] == DBNull.Value ? "" : reader["Objectif"].ToString();
                        ordre.Etat = reader["Etat"] == DBNull.Value ? "" : reader["Etat"].ToString();
                        ordre.Recommandation = reader["Recommandation"] == DBNull.Value ? "" : reader["Recommandation"].ToString();

                        ordreMobile.Add(ordre);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return (ordreMobile);
        }

        public static MobileOrdreCollection CrmvalidesCharger(string code_commercial, string Datedebut, string Datefin, string CRegion, string CGouvernorat)
        {
            MobileOrdreCollection ordreMobile = new MobileOrdreCollection();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader reader;
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Mobile_CRM_OT_valide_charger";
                    cmd.Parameters.AddWithValue("@code_commercial", code_commercial);
                    cmd.Parameters.AddWithValue("@Datedebut", Datedebut);
                    cmd.Parameters.AddWithValue("@Datefin", Datefin);
                    cmd.Parameters.AddWithValue("@CRegion", CRegion);
                    cmd.Parameters.AddWithValue("@CGouvernorat", CGouvernorat);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value; 
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        MobileOrdre ordre = new MobileOrdre();
                        ordre.NumOrdre = reader["NOrdredeTravail"] == DBNull.Value ? "" : reader["NOrdredeTravail"].ToString();
                        ordre.date_plannification = reader["DatePlanification"] == DBNull.Value ? "" : DateTime.Parse(reader["DatePlanification"].ToString()).ToString("dd/MM/yyyy");
                        ordre.DateRattachement = reader["DateRattachement"] == DBNull.Value ? "" : DateTime.Parse(reader["DateRattachement"].ToString()).ToString("dd/MM/yyyy");
                        ordre.raisonsociale = reader["RaisonSociale"] == DBNull.Value ? "" : reader["RaisonSociale"].ToString();
                        ordre.rattachement = reader["NRattachement"] == DBNull.Value ? "" : reader["NRattachement"].ToString();
                        ordre.cclient = reader["CClient"] == DBNull.Value ? "" : reader["CClient"].ToString();
                        //if (reader["NBonCommande"] != DBNull.Value) ordre.NBonCommande = reader["NBonCommande"].ToString();
                        if (reader["Observations"] == DBNull.Value || reader["Observations"].ToString().ToUpper().Equals("NULL"))
                        {
                            ordre.Observations = "";
                        }
                        else
                            ordre.Observations = reader["Observations"].ToString();
                        //ordre.Etat = reader["Etat"] == DBNull.Value ? "" : reader["Etat"].ToString(); ;




                        //reader["Observations"].ToString();
                        // ordre.lat = reader["Latitude"] == DBNull.Value ? "" : reader["Latitude"].ToString();
                        //  ordre.tel = reader["tel"] == DBNull.Value ? "" : reader["tel"].ToString(); ;
                        // ordre.ville = reader["Ville"]== DBNull.Value ? "" : reader["Ville"].ToString(); ;
                        /*  ordre.Adresse = reader["Adresse"] == DBNull.Value ? "" : reader["Adresse"].ToString(); ;
                          ordre.Cetablissement = reader["Cetablissement"]== DBNull.Value ? "" : reader["Cetablissement"].ToString(); ;
                          ordre.Elib = reader["Libelle"] == DBNull.Value ? "" : reader["Libelle"].ToString();
                          ordre.DateRattachement = reader["days"].ToString().Equals('0') ? "Aujourd'hui" : reader["days"].ToString().Equals('1') ? "Hier" : "dateratach";
                          ordre.rattachement = reader["rattachement"] == DBNull.Value ? "" : reader["rattachement"].ToString();
                          ordre.heuref = reader["heuref"] == DBNull.Value ? "" : reader["heuref"].ToString();
                          ordre.heured = reader["heured"] == DBNull.Value ? "" : reader["heured"].ToString();  */
                        ordre.duree = reader["duree"] == DBNull.Value ? "" : reader["duree"].ToString();
                        ordreMobile.Add(ordre);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return (ordreMobile);
        }
    }
}