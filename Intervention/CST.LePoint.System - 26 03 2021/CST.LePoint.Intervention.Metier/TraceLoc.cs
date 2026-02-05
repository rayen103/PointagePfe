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
   public  class TraceLoc
    {
 
        #region Proprietes

        [XmlAttribute("Date")]
        [Bindable(true)]
        public DateTime Date { get; set; }

        [XmlAttribute("Heure")]
        [Bindable(true)]
        public string Heure { get; set; }

        [XmlAttribute("CRep")]
        [Bindable(true)]
        public string CRep { get; set; }

        [XmlAttribute("LongitudeRep")]
        [Bindable(true)]
        public decimal LongitudeRep { get; set; }

        [XmlAttribute("LatitudeRep")]
        [Bindable(true)]
        public decimal LatitudeRep { get; set; }

        [XmlAttribute("CClient")]
        [Bindable(true)]
        public string CClient { get; set; }

        [XmlAttribute("LongitudeClt")]
        [Bindable(true)]
        public decimal LongitudeClt { get; set; }

        [XmlAttribute("LatitudeClt")]
        [Bindable(true)]
        public decimal LatitudeClt { get; set; }

        [XmlAttribute("Distance")]
        [Bindable(true)]
        public decimal Distance { get; set; }

        [XmlAttribute("Durée")]
        [Bindable(true)]
        public decimal Durée { get; set; }

    


        #endregion Proprietes

        public TraceLoc()
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

                    cmd.CommandText = "TraceLoc_Sauvegarder";
                    cmd.Parameters.AddWithValue("@Date", Date);
                    cmd.Parameters.AddWithValue("@Heure", Heure);
                    cmd.Parameters.AddWithValue("@CRep", CRep);
                    cmd.Parameters.AddWithValue("@LongitudeRep", LongitudeRep);
                    cmd.Parameters.AddWithValue("@LatitudeRep", LatitudeRep);
                    cmd.Parameters.AddWithValue("@CClient", CClient);
                    cmd.Parameters.AddWithValue("@LongitudeClt", LongitudeClt);
                    cmd.Parameters.AddWithValue("@LatitudeClt", LatitudeClt);
                    cmd.Parameters.AddWithValue("@Distance", Distance);
                    cmd.Parameters.AddWithValue("@Durée", Durée);
                  

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


    const double PIx = Math.PI;
    const double RADIO = 6378.16;

    /// <summary>
    /// Convert degrees to Radians
    /// </summary>
    /// <param name="x">Degrees</param>
    /// <returns>The equivalent in radians</returns>
    public static double Radians(double x)
    {
        return x * PIx / 180;
    }

    /// <summary>
    /// Calculate the distance between two places.
    /// </summary>
    /// <param name="lon1"></param>
    /// <param name="lat1"></param>
    /// <param name="lon2"></param>
    /// <param name="lat2"></param>
    /// <returns></returns>
    public static double DistanceBetweenPlaces(double lon1, double lat1, double lon2, double lat2)
    {
        double R = 6371; // km
        double dLat = Radians(lat2 - lat1);
        double dLon = Radians(lon2 - lon1);
        lat1 = Radians(lat1);
        lat2 = Radians(lat2);

        double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);
        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        double d = R * c;

        return d;
    }


//Console.WriteLine(DistanceAlgorithm.DistanceBetweenPlaces(36.578581, -118.291994, 36.23998, -116.83171));


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
                    cmd.CommandText = "TraceLoc_Supprimer";
                    cmd.Parameters.AddWithValue("@CClient", CClient);
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

        

       

        //public static TrameLoc Charger(string cClient)
        
        //{
            
        //    //TrameLoc trameLoc = null;

        //    //try
        //    //{
        //    //    using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
        //    //    {
        //    //        cn.Open();
        //    //        SqlCommand cmd = new SqlCommand();
        //    //        cmd.Connection = cn;
        //    //        cmd.CommandType = CommandType.StoredProcedure;
        //    //        cmd.CommandText = "TrameLoc_Charger";
        //    //        cmd.Parameters.AddWithValue("@CClient", cClient);
        //    //        foreach (SqlParameter parametre in cmd.Parameters)
        //    //            if (parametre.Value == null)
        //    //                parametre.Value = DBNull.Value;

        //    //        using (SqlDataReader dr = cmd.ExecuteReader())
        //    //        {
        //    //            if (dr.Read())
        //    //            {
        //    //                trameLoc = new TrameLoc();
        //    //                client.CClient = dr["CClient"].ToString();
        //    //                if (dr["CRegion"] != DBNull.Value)
        //    //                    client.CRegion = dr["CRegion"].ToString();
        //    //                if (dr["CGouvernorat"] != DBNull.Value)
        //    //                    client.CGouvernorat = dr["CGouvernorat"].ToString();
        //    //                if (dr["AbreviationClient"] != DBNull.Value)
        //    //                    client.Abreviation = dr["AbreviationClient"].ToString();
        //    //                if (dr["BActifClient"] != DBNull.Value)
        //    //                    client.BActif = bool.Parse(dr["BActifClient"].ToString());
        //    //                if (dr["CClientFamille"] != DBNull.Value)
        //    //                    client.CClientFamille = dr["CClientFamille"].ToString();
        //    //                if (dr["CGroupe"] != DBNull.Value)
        //    //                    client.CGroupe = dr["CGroupe"].ToString();
        //    //                if (dr["CPays"] != DBNull.Value)
        //    //                    client.CPays = dr["CPays"].ToString();
        //    //                if (dr["CRecouvreur"] != DBNull.Value)
        //    //                    client.CRecouvreur = int.Parse(dr["CRecouvreur"].ToString());
                  
                        
                         

        //    //                if (dr["Longitude"] != DBNull.Value)
        //    //                    client.Longitude = decimal.Parse(dr["Longitude"].ToString());
        //    //                if (dr["Latitude"] != DBNull.Value)
        //    //                    client.Latitude = decimal.Parse(dr["Latitude"].ToString());

        //    //            }
        //    //        }
        //    //    }
        //    //}
        //    //catch (Exception)
        //    //{
        //    //    throw;
        //    //}

        //    //return trameLoc;
        //}
        ////public static Client ChargerVue(string cClient)
        ////{

        ////    Client client = null;

        ////    try
        ////    {
        ////        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
        ////        {
        ////            cn.Open();
        ////            SqlCommand cmd = new SqlCommand();
        ////            cmd.Connection = cn;
        ////            cmd.CommandType = CommandType.StoredProcedure;
        ////            cmd.CommandText = "Client_ChargerVue";
        ////            cmd.Parameters.AddWithValue("@CClient", cClient);

        ////            using (SqlDataReader dr = cmd.ExecuteReader())
        ////            {
        ////                if (dr.Read())
        ////                {
        ////                    client = new Client();
        ////                    client.CClient = dr["CClient"].ToString();
        ////                    if (dr["CRegion"] != DBNull.Value)
        ////                        client.CRegion = dr["CRegion"].ToString();
        ////                    if (dr["CGouvernorat"] != DBNull.Value)
        ////                        client.CGouvernorat = dr["CGouvernorat"].ToString();
        ////                    if (dr["AbreviationClient"] != DBNull.Value)
        ////                        client.Abreviation = dr["AbreviationClient"].ToString();
        ////                    if (dr["BActifClient"] != DBNull.Value)
        ////                        client.BActif = bool.Parse(dr["BActifClient"].ToString());
        ////                    if (dr["CClientFamille"] != DBNull.Value)
        ////                        client.CClientFamille = dr["CClientFamille"].ToString();
        ////                    if (dr["CGroupe"] != DBNull.Value)
        ////                        client.CGroupe = dr["CGroupe"].ToString();
        ////                    if (dr["CPays"] != DBNull.Value)
        ////                        client.CPays = dr["CPays"].ToString();
        ////                    if (dr["CRecouvreur"] != DBNull.Value)
        ////                        client.CRecouvreur = int.Parse(dr["CRecouvreur"].ToString());
        ////                    if (dr["CSpeciale"] != DBNull.Value)
        ////                        client.CSpeciale = dr["CSpeciale"].ToString();
        ////                    if (dr["CTarif"] != DBNull.Value)
        ////                        client.CTarif = dr["CTarif"].ToString();
        ////                    if (dr["CTVA"] != DBNull.Value)
        ////                        client.CTVA = dr["CTVA"].ToString();
        ////                    if (dr["CModeReglement"] != DBNull.Value)
        ////                        client.CModeReglement = dr["CModeReglement"].ToString();
        ////                    if (dr["CVendeur"] != DBNull.Value)
        ////                        client.CVendeur = int.Parse(dr["CVendeur"].ToString());
        ////                    if (dr["TypeContrat"] != DBNull.Value)
        ////                        client.TypeContrat = dr["TypeContrat"].ToString();
        ////                    if (dr["DateFinExonoreFodec"] != DBNull.Value)
        ////                        client.DateFinExonoreFodec = DateTime.Parse(dr["DateFinExonoreFodec"].ToString());
        ////                    if (dr["DateFinExonoreTVA"] != DBNull.Value)
        ////                        client.DateFinExonoreTVA = DateTime.Parse(dr["DateFinExonoreTVA"].ToString());
        ////                    if (dr["Email"] != DBNull.Value)
        ////                        client.Email = dr["Email"].ToString();
        ////                    if (dr["BFodecExonore"] != DBNull.Value)
        ////                        client.BFodecExonore = bool.Parse(dr["BFodecExonore"].ToString());
        ////                    if (dr["BTimbreExonore"] != DBNull.Value)
        ////                        client.BTimbreExonore = bool.Parse(dr["BTimbreExonore"].ToString());
        ////                    if (dr["BTVAExonore"] != DBNull.Value)
        ////                        client.BTVAExonore = bool.Parse(dr["BTVAExonore"].ToString());
        ////                    if (dr["BContentieux"] != DBNull.Value)
        ////                        client.BContentieux = bool.Parse(dr["BContentieux"].ToString());
        ////                    if (dr["BRisque"] != DBNull.Value)
        ////                        client.BRisque = bool.Parse(dr["BRisque"].ToString());

        ////                    if (dr["Fax"] != DBNull.Value)
        ////                        client.Fax = dr["Fax"].ToString();
        ////                    if (dr["MontantCreditMax"] != DBNull.Value)
        ////                        client.MontantCreditMax = decimal.Parse(dr["MontantCreditMax"].ToString());
        ////                    if (dr["MontantCreditMin"] != DBNull.Value)
        ////                        client.MontantCreditMin = decimal.Parse(dr["MontantCreditMin"].ToString());
        ////                    if (dr["MontantExonoreTVA"] != DBNull.Value)
        ////                        client.MontantExonoreTVA = decimal.Parse(dr["MontantExonoreTVA"].ToString());
        ////                    if (dr["NbJourEcheancePaiment"] != DBNull.Value)
        ////                        client.NbJourEcheancePaiment = int.Parse(dr["NbJourEcheancePaiment"].ToString());
        ////                    if (dr["NbJourCreditFacture"] != DBNull.Value)
        ////                        client.NbJourCreditFacture = int.Parse(dr["NbJourCreditFacture"].ToString());
        ////                    if (dr["Nom"] != DBNull.Value)
        ////                        client.Nom = dr["Nom"].ToString();
        ////                    if (dr["NumCIN"] != DBNull.Value)
        ////                        client.NumCIN = dr["NumCIN"].ToString();
        ////                    if (dr["NumTimbre"] != DBNull.Value)
        ////                        client.NumTimbre = dr["NumTimbre"].ToString();
        ////                    if (dr["ObservationClient"] != DBNull.Value)
        ////                        client.ObservationClient = dr["ObservationClient"].ToString();
        ////                    if (dr["BPassager"] != DBNull.Value)
        ////                        client.BPassager = bool.Parse(dr["BPassager"].ToString());
        ////                    if (dr["RaisonSociale"] != DBNull.Value)
        ////                        client.RaisonSociale = dr["RaisonSociale"].ToString();
        ////                    if (dr["TauxRemise"] != DBNull.Value)
        ////                        client.TauxRemise = decimal.Parse(dr["TauxRemise"].ToString());
        ////                    if (dr["SoldeAvanceRestant"] != DBNull.Value)
        ////                        client.SoldeAvanceRestant = decimal.Parse(dr["SoldeAvanceRestant"].ToString());
        ////                    if (dr["SoldeAvoirRestant"] != DBNull.Value)
        ////                        client.SoldeAvoirRestant = decimal.Parse(dr["SoldeAvoirRestant"].ToString());
        ////                    if (dr["SoldeBonRetour"] != DBNull.Value)
        ////                        client.SoldeBonRetour = decimal.Parse(dr["SoldeBonRetour"].ToString());
        ////                    if (dr["SoldeAnterieur"] != DBNull.Value)
        ////                        client.SoldeAnterieur = decimal.Parse(dr["SoldeAnterieur"].ToString());
        ////                    if (dr["SoldeBonLivraison"] != DBNull.Value)
        ////                        client.SoldeBonLivraison = decimal.Parse(dr["SoldeBonLivraison"].ToString());
        ////                    if (dr["SoldeFacture"] != DBNull.Value)
        ////                        client.SoldeFacture = decimal.Parse(dr["SoldeFacture"].ToString());
        ////                    if (dr["SoldeImpaye"] != DBNull.Value)
        ////                        client.SoldeImpaye = decimal.Parse(dr["SoldeImpaye"].ToString());
        ////                    if (dr["TauxRetenuSource"] != DBNull.Value)
        ////                        client.TauxRetenuSource = decimal.Parse(dr["TauxRetenuSource"].ToString());
        ////                    if (dr["TauxRetenuTVA"] != DBNull.Value)
        ////                        client.TauxRetenuTVA = decimal.Parse(dr["TauxRetenuTVA"].ToString());
        ////                    if (dr["BMajoration"] != DBNull.Value)
        ////                        client.BMajoration = bool.Parse(dr["BMajoration"].ToString());
        ////                    if (dr["NumeroTelephone1"] != DBNull.Value)
        ////                        client.NumeroTelephone1 = dr["NumeroTelephone1"].ToString();
        ////                    if (dr["NumeroTelephone2"] != DBNull.Value)
        ////                        client.NumeroTelephone2 = dr["NumeroTelephone2"].ToString();
        ////                    if (dr["BTransfertCompta"] != DBNull.Value)
        ////                        client.BTransfertCompta = bool.Parse(dr["BTransfertCompta"].ToString());
        ////                    if (dr["BVIP"] != DBNull.Value)
        ////                        client.BVIP = bool.Parse(dr["BVIP"].ToString());
        ////                    if (dr["CNatureTiers"] != DBNull.Value)
        ////                        client.CNatureTiers = int.Parse(dr["CNatureTiers"].ToString());
        ////                    if (dr["BTransfert"] != DBNull.Value)
        ////                        client.BTransfert = bool.Parse(dr["BTransfert"].ToString());
        ////                    if (dr["NumAutorisation"] != DBNull.Value)
        ////                        client.NumAutorisation = dr["NumAutorisation"].ToString();
        ////                    if (dr["DateDebutAutorisation"] != DBNull.Value)
        ////                        client.DateDebutAutorisation = DateTime.Parse(dr["DateDebutAutorisation"].ToString());
        ////                    if (dr["BPaiementAvance"] != DBNull.Value)
        ////                        client.BPaiementAvance = bool.Parse(dr["BPaiementAvance"].ToString());
        ////                    if (dr["BAvanceForfaitaire"] != DBNull.Value)
        ////                        client.BAvanceForfaitaire = bool.Parse(dr["BAvanceForfaitaire"].ToString());
        ////                    if (dr["BInitialisationRemise"] != DBNull.Value)
        ////                        client.BInitialisationRemise = bool.Parse(dr["BInitialisationRemise"].ToString());
        ////                    if (dr["RemiseExeptionnel"] != DBNull.Value)
        ////                        client.RemiseExeptionnel = decimal.Parse(dr["RemiseExeptionnel"].ToString());
        ////                    if (dr["DateFinAutorisation"] != DBNull.Value)
        ////                        client.DateFinAutorisation = DateTime.Parse(dr["DateFinAutorisation"].ToString());
        ////                    if (dr["NFacture"] != DBNull.Value)
        ////                        client.NFacture = dr["NFacture"].ToString();
        ////                    if (dr["MontantTTCFacture"] != DBNull.Value)
        ////                        client.MontantTTCFacture = decimal.Parse(dr["MontantTTCFacture"].ToString());
        ////                    if (dr["DateFacture"] != DBNull.Value)
        ////                        client.DateFacture = DateTime.Parse(dr["DateFacture"].ToString());

        ////                    client.Banques = ClientBanqueCollection.Charger(client.CClient);
        ////                    client.Adresses = AdresseCollection.Charger(client.CClient);
        ////                    client.Contacts = ClientContactCollection.Charger(client.CClient);
        ////                }
        ////            }
        ////        }
        ////    }
        ////    catch (Exception)
        ////    {
        ////        throw;
        ////    }

        ////    return client;
        ////}
        
    }

    [Serializable]
    public class TrameLocCollection : List<TraceLoc>
    {
        public TrameLocCollection()
        {
        }

        //   public static DataSet ChargerVue(string cClient, string cArticle, string cCategorie, string cFamille, string cType, string cNature, string cModele, string cModele1, string cModele2, DateTime dateDeb, DateTime dateFin, string FamilleClient, string cVendeur, string cEntrepot, string cPays, int mouvement)
        //{
        //    DataSet ds = new DataSet();

        //    using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
        //    {
        //        cn.Open();
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Connection = cn;
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = "ClientMvt_Vue_Rechercher";
        //        cmd.Parameters.AddWithValue("@CClient", cClient);
        //        cmd.Parameters.AddWithValue("@CArticle", cArticle);
        //        cmd.Parameters.AddWithValue("@DateDeb", dateDeb);
        //        cmd.Parameters.AddWithValue("@DateFin", dateFin);
        //        cmd.Parameters.AddWithValue("@CCategorie", cCategorie);
        //        cmd.Parameters.AddWithValue("@CFamille", cFamille);
        //        cmd.Parameters.AddWithValue("@CType", cType);
        //        cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
        //        cmd.Parameters.AddWithValue("@CModele", cModele);
        //        cmd.Parameters.AddWithValue("@CModele1", cModele1);
        //        cmd.Parameters.AddWithValue("@CModele2", cModele2);
        //        cmd.Parameters.AddWithValue("@CNature", cNature);
        //        cmd.Parameters.AddWithValue("@CPays", cPays);
        //        cmd.Parameters.AddWithValue("@Vendeur", cVendeur);
        //        cmd.Parameters.AddWithValue("@FamilleClient", FamilleClient);
        //        cmd.Parameters.AddWithValue("@Mouvement", mouvement);
        //        foreach (SqlParameter parametre in cmd.Parameters)
        //        {
        //            if (parametre.Value == null)
        //            {
        //                parametre.Value = DBNull.Value;
        //            }
        //        }
        //        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        //        sda.Fill(ds, "Client_Mvt_Rpt_Charger");
        //    }
        //    return (ds);
        //}

        //public static TrameLocCollection Charger()
        //{
        //    //    TrameLocCollection trameLocCollection = new TrameLocCollection();

        //    //    using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
        //    //    {
        //    //        cn.Open();
        //    //        SqlTransaction transaction = cn.BeginTransaction();
        //    //        try
        //    //        {
        //    //            SqlCommand cmd = new SqlCommand();
        //    //            cmd.Transaction = transaction;
        //    //            cmd.Connection = transaction.Connection;
        //    //            cmd.CommandType = CommandType.StoredProcedure;
        //    //            cmd.CommandText = "TrameLoc_Charger";
        //    //            cmd.Parameters.AddWithValue("@CClient", DBNull.Value);

        //    //            using (SqlDataReader dr = cmd.ExecuteReader())
        //    //            {
        //    //                while (dr.Read())
        //    //                {
        //    //                    TrameLoc trameLoc = new TrameLoc();
        //    //                    if (dr["CClient"] != DBNull.Value)
        //    //                        client.CClient = dr["CClient"].ToString();
        //    //                    if (dr["CRegion"] != DBNull.Value)
        //    //                        client.CRegion = dr["CRegion"].ToString();
        //    //                    if (dr["AbreviationClient"] != DBNull.Value)
        //    //                        client.Abreviation = dr["AbreviationClient"].ToString();
        //    //                    if (dr["BActifClient"] != DBNull.Value)
        //    //                        client.BActif = bool.Parse(dr["BActifClient"].ToString());
        //    //                    if (dr["CClientFamille"] != DBNull.Value)
        //    //                        client.CClientFamille = dr["CClientFamille"].ToString();
        //    //                    if (dr["CGroupe"] != DBNull.Value)
        //    //                        client.CGroupe = dr["CGroupe"].ToString();
        //    //                    if (dr["CPays"] != DBNull.Value)
        //    //                        client.CPays = dr["CPays"].ToString();

        //    //                    trameLocCollection.Add(trameLoc);
        //    //                }
        //    //            }
        //    //        }
        //    //        catch (Exception)
        //    //        {
        //    //            throw;
        //    //        }
        //    //    }

        //    //    return (trameLocCollection);
        //}
    }
}

