using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Configuration;
using System.Data;
using System.Web.Script.Services;
using Newtonsoft.Json;
using CST.Stock.Metier.MobileArticleFamille;
using CST.LePoint.Stock.Metier;
using CST.LePoint.Intervention.Metier;
using CST.LePoint.Tiers.Metier;
//using CST.Stock.Metier.Article;
using CST.LePoint.Vente.Metier;
using CST.LePoint.Stock.Referentiel.Commun;
using CST.LePoint.Tiers.Referentiel;
using WebApp.Post;
using CST.LePoint.VenteMobile.Metier;

namespace WebApp
{
    [WebService(Namespace = "http://cst.tn/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class WebService : System.Web.Services.WebService
    {
        [WebMethod]
        public void Equipes()
        {
            ResponseJson<List<MobileEquipe>, String> response = new ResponseJson<List<MobileEquipe>, String>();
            try
            {
                if (!AuthorizationHeaders(HttpContext.Current.Request.Headers.Get("Authorization")))
                {
                    response.Status = "unauthorized".ToUpper();
                }
                else
                {
                    response.results = EquipeMobileCollection.Charger();
                    response.message = "success";
                    response.Status = "OK";
                }
            }
            catch (Exception ex)
            {
                response.Status = "ERROR".ToUpper();
                response.message = ex.Message;
            }
            afficherJsonResult(response);
        }

        [WebMethod]
        public void Circuits()
        {
            ResponseJson<List<MobileCircuit>, String> response = new ResponseJson<List<MobileCircuit>, String>();
            if (!AuthorizationHeaders(HttpContext.Current.Request.Headers.Get("Authorization")))
            {
                response.message = "error";
            }
            else
            {
                response.message = "success";
                response.results = MobileCircuitCollection.Charger();
            }
            afficherJsonResult(response);
        }

        [WebMethod]
        public void connectionUser()
        {
            ResponseJson<String, String> response = new ResponseJson<String, String>();
            MobileAuthUser user = new MobileAuthUser();
            user = readfromPostBody<MobileAuthUser>(HttpContext.Current, user);
            if (!AuthorizationHeaders(HttpContext.Current.Request.Headers.Get("Authorization")))
            {
                response.message = "Error Authorization";
            }
            else
            {

                if (user.CommercialConnexion(user.commercial, sha1Hash(user.password)))
                {
                    response.message = "success";
                    response.result1 = "login" + user.commercial + "Password" + user.password;
                }
                else
                {
                    response.message = "error";
                    response.result1 = "login" + user.commercial + "Password" + user.password;
                }
            }
            afficherJsonResult(response);
        }

        [WebMethod]
        public void planifiee(string id, string dd, string df, string cr, string cg)
        {
            ResponseJson<List<MobileOrdre>, int> response = new ResponseJson<List<MobileOrdre>, int>();
            try
            {
                if (!AuthorizationHeaders(HttpContext.Current.Request.Headers.Get("Authorization")) || id.Equals(""))
                {
                    response.Status = "unauthorized".ToUpper();
                }
                else
                {
                    response.result1 = MobileOrdreCollection.NombreManquer(id);
                    response.results = MobileOrdreCollection.planifieCharger(id, dd, df, cr, cg);
                    response.message = "success";
                    response.Status = "OK";
                }
            }
            catch (Exception ex)
            {
                response.Status = "ERROR".ToUpper();
                response.message = ex.Message;
            }
            afficherJsonResult(response);
        }

        [WebMethod]
        public void encours(string id, string dd, string df, string cr, string cg)
        {
            ResponseJson<List<MobileOrdre>, string> response = new ResponseJson<List<MobileOrdre>, string>();
            try
            {
                if (!AuthorizationHeaders(HttpContext.Current.Request.Headers.Get("Authorization")) || id.Equals(""))
                {
                    response.Status = "unauthorized".ToUpper();
                }
                else
                {
                    response.results = MobileOrdreCollection.encoursCharger(id, dd, df, cr, cg);
                    response.message = "success";
                    response.Status = "OK";
                }
            }
            catch (Exception ex)
            {
                response.Status = "ERROR".ToUpper();
                response.message = ex.Message;
            }
            afficherJsonResult(response);
        }

        [WebMethod]
        public void valides(string id, string dd, string df, string cr, string cg)
        {
            ResponseJson<List<MobileOrdre>, string> response = new ResponseJson<List<MobileOrdre>, string>();
            try
            {
                if (!AuthorizationHeaders(HttpContext.Current.Request.Headers.Get("Authorization")) || id.Equals(""))
                {
                    response.Status = "unauthorized".ToUpper();
                }
                else
                {
                    response.results = MobileOrdreCollection.validesCharger(id, dd, df, cr, cg);
                    response.message = "success";
                    response.Status = "OK";
                }
            }
            catch (Exception ex)
            {
                response.Status = "ERROR".ToUpper();
                response.message = ex.Message;
            }
            afficherJsonResult(response);
        }

        [WebMethod]
        public void reclamation(string id, string reclamation)
        {
            string rattachment;
            ResponseJson<bool, String> response = new ResponseJson<bool, String>();
            try
            {
                MobileRattachement mrattachement = new MobileRattachement();
                rattachment = mrattachement.Sauvegarder(id, true, null, "");
                MobileOrdre ordre = new MobileOrdre();
                if (!AuthorizationHeaders(HttpContext.Current.Request.Headers.Get("Authorization")) || id.Equals(""))
                {
                    response.Status = "unauthorized".ToUpper();
                }
                else
                {
                    response.message = rattachment;
                    response.results = ordre.ReclamationOrdre(id, reclamation);
                    response.Status = "OK";
                }
            }
            catch (Exception ex)
            {
                response.Status = "ERROR".ToUpper();
                response.message = ex.Message;
            }
            afficherJsonResult(response);
        }

        [WebMethod]
        public void validermission(string id)
        {
            MobileOrdre ordre = new MobileOrdre();
            ResponseJson<bool, string> response = new ResponseJson<bool, string>();
            if (!AuthorizationHeaders(HttpContext.Current.Request.Headers.Get("Authorization")) || id.Equals(""))
            {
                response.message = "error";
            }
            else
            {
                response.message = "success";
                response.results = ordre.validOrdre(id);
            }
            afficherJsonResult(response);
        }

        [WebMethod]
        public void ClientList(string motCle, int page)
        {
            ResponseJson<List<MobileClient>, string> response = new ResponseJson<List<MobileClient>, string>();
            try
            {
                if (!AuthorizationHeaders(HttpContext.Current.Request.Headers.Get("Authorization")))
                {
                    response.Status = "unauthorized".ToUpper();
                }
                else
                {
                    response.results = ClientMobileCollection.Charger(motCle, page);
                    response.message = "success";
                    response.Status = "OK";
                }
            }
            catch (Exception ex)
            {
                response.Status = "ERROR".ToUpper();
                response.message = ex.Message;
            }
            afficherJsonResult(response);
        }

        [WebMethod]
        public void bcdetails(string nbcmd)
        {
            ResponseJson<List<BonCommandeDetail>, string> response = new ResponseJson<List<BonCommandeDetail>, string>();
            if (!AuthorizationHeaders(HttpContext.Current.Request.Headers.Get("Authorization")))
            {
                response.message = "error";
            }
            else
            {
                response.message = nbcmd;
                response.results = BonCommandeDetailCollection.Charger(nbcmd);
            }
            afficherJsonResult(response);
        }

        [WebMethod]
        public void RegionClientsList(string region)
        {
            ResponseJson<List<MobileClient>, string> response = new ResponseJson<List<MobileClient>, string>();
            if (!AuthorizationHeaders(HttpContext.Current.Request.Headers.Get("Authorization")))
            {
                response.message = "error";
            }
            else
            {
                response.message = "success";
                response.results = ClientMobileCollection.ChargerRegionClients(region);
            }
            afficherJsonResult(response);
        }

        [WebMethod]
        public void getmodaliteList(string reg, string CClient)
        {
            ResponseJson<List<MobilePaimentModalite>, string> response = new ResponseJson<List<MobilePaimentModalite>, string>();
            if (!AuthorizationHeaders(HttpContext.Current.Request.Headers.Get("Authorization")))
            {
                response.message = "error";
            }
            else
            {
                response.message = "success";
                response.results = MobilePaimentModalite.Charger(reg, CClient);
            }
            afficherJsonResult(response);
        }

        [WebMethod]
        public void gettypebcList()
        {
            ResponseJson<List<MobileCTypeCommande>, string> response = new ResponseJson<List<MobileCTypeCommande>, string>();
            if (!AuthorizationHeaders(HttpContext.Current.Request.Headers.Get("Authorization")))
            {
                response.message = "error";
            }
            else
            {
                response.message = "success";
                response.results = MobileCTBCommandeCollection.Charger();
            }
            afficherJsonResult(response);
        }

        [WebMethod]
        public void BachatList()
        {
            ResponseJson<List<MobilebonAchat>, string> response = new ResponseJson<List<MobilebonAchat>, string>();
            if (!AuthorizationHeaders(HttpContext.Current.Request.Headers.Get("Authorization")))
            {
                response.message = "error";
            }
            else
            {
                response.message = "success";
                response.results = bonAchatMobileCollection.Charger();
            }
            afficherJsonResult(response);
        }

        [WebMethod]
        public void ProspecterClient()
        {
            ResponseJson<String, string> response = new ResponseJson<String, string>();
            MobileClientAprospecter clientprospecter = new MobileClientAprospecter();
            if (!AuthorizationHeaders(HttpContext.Current.Request.Headers.Get("Authorization")))
            {
                response.message = "error ";
            }
            else
            {
                clientprospecter = readfromPostBody<MobileClientAprospecter>(HttpContext.Current, clientprospecter);
                response.message = clientprospecter.prospecterClient(clientprospecter);
            }
            afficherJsonResult(response);
        }

        [WebMethod]
        public void PositionClient()
        {
            ResponseJson<String, string> response = new ResponseJson<String, string>();
            PositionClientMobile clientp = new PositionClientMobile();
            if (!AuthorizationHeaders(HttpContext.Current.Request.Headers.Get("Authorization")))
            {
                response.message = "error";
            }
            else
            {
                clientp = readfromPostBody<PositionClientMobile>(HttpContext.Current, clientp);
                response.message = clientp.verifierPositionClient(clientp);
            }
            afficherJsonResult(response);
        }

        //[WebMethod]
        //public void Articles(string code)
        //{
        //    ResponseJson<List<CST.LePoint.Intervention.Metier.MobileArticlee>,string> response = new ResponseJson<List<CST.LePoint.Intervention.Metier.MobileArticlee>,string>();
        //    if (!AuthorizationHeaders(HttpContext.Current.Request.Headers.Get("Authorization")))
        //    {
        //        response.message = "error";
        //    }
        //    else
        //    {
        //        response.results = CST.LePoint.Intervention.Metier.MobileArticleeCollection.articlesCharger(code);
        //        response.message = "success";
        //    }
        //    afficherJsonResult(response);
        //}

        [WebMethod]
        public void ajourattachement(string id, string TypeRat, bool isJustif, string CJustif)
        {
            ResponseJson<String, String> response = new ResponseJson<String, String>();
            try
            {
                if (!AuthorizationHeaders(HttpContext.Current.Request.Headers.Get("Authorization")))
                {
                    response.Status = "unauthorized".ToUpper();
                }
                else
                {
                    MobileRattachement mrattachement = new MobileRattachement();
                    response.results = mrattachement.Sauvegarder(id, isJustif, TypeRat, CJustif);
                    response.message = "success";
                    response.Status = "OK";
                }

            }
            catch (Exception ex)
            {
                response.Status = "ERROR".ToUpper();
                response.message = ex.Message;
            }
            afficherJsonResult(response);

        }

        [WebMethod]
        public void nvr(string not)
        {
            ResponseJson<string, string> response = new ResponseJson<string, string>();
            try
            {
                if (!AuthorizationHeaders(HttpContext.Current.Request.Headers.Get("Authorization")))
                {
                    response.Status = "unauthorized".ToUpper();
                }
                else
                {
                    MobileGPRattachement mrattachement = new MobileGPRattachement();
                    mrattachement = MobileGPRattachement.GP_OT_Charger(not);
                    if (mrattachement != null)
                    {
                        mrattachement.TypeRattachement = "MVENTE";
                        mrattachement.CEtat = "EC";
                        response.Status = mrattachement.Sauvgarder().ToUpper();
                        response.results = mrattachement.NRattachement;
                    }
                    else
                    {
                        response.Status = "NOT_FOUND".ToUpper();
                        response.message = "";
                    }
                }
            }
            catch(Exception ex){
                response.Status = "ERROR".ToUpper();
                response.message = ex.Message;
            }
            afficherJsonResult(response);
        }

        [WebMethod]
        public void ncr(string not)
        {
            ResponseJson<string, string> response = new ResponseJson<string, string>();
            try
            {
                if (!AuthorizationHeaders(HttpContext.Current.Request.Headers.Get("Authorization")))
                {
                    response.Status = "unauthorized".ToUpper();
                }
                else
                {
                    MobileGPRattachement mrattachement = new MobileGPRattachement();
                    mrattachement = MobileGPRattachement.GP_OT_Charger(not);
                    if (mrattachement != null)
                    {
                        mrattachement.TypeRattachement = "CRM";
                        mrattachement.CEtat = "EC";
                        response.Status = mrattachement.Sauvgarder().ToUpper();
                        response.results = mrattachement.NRattachement;
                    }
                    else
                    {
                        response.Status = "NOT_FOUND".ToUpper();
                        response.message = "";
                    }
                }
            }
            catch (Exception ex)
            {
                response.Status = "ERROR".ToUpper();
                response.message = ex.Message;
            }
            afficherJsonResult(response);
        }

        [WebMethod]
        public void jcr(string not, string jstf)
        {
            ResponseJson<string, string> response = new ResponseJson<string, string>();
            try
            {
                if (!AuthorizationHeaders(HttpContext.Current.Request.Headers.Get("Authorization")))
                {
                    response.Status = "unauthorized".ToUpper();
                }
                else
                {
                    MobileGPRattachement mrattachement = new MobileGPRattachement();
                    mrattachement = MobileGPRattachement.GP_OT_Charger(not);
                    if (mrattachement != null)
                    {
                        mrattachement.TypeRattachement = "CRM";
                        mrattachement.CEtat = "AN";
                        mrattachement.Observation = jstf;
                        response.Status = mrattachement.Sauvgarder().ToUpper();
                        mrattachement.TypeRattachement = "MVENTE";
                        response.Status = mrattachement.Sauvgarder().ToUpper();
                        response.results = mrattachement.NRattachement;
                    }
                    else
                    {
                        response.Status = "NOT_FOUND".ToUpper();
                        response.message = "";
                    }
                }
            }
            catch (Exception ex)
            {
                response.Status = "ERROR".ToUpper();
                response.message = ex.Message;
            }
            afficherJsonResult(response);
        }
        
        [WebMethod]
        public void jvr(string not, string jstf)
        {
            ResponseJson<string, string> response = new ResponseJson<string, string>();
            try
            {
                if (!AuthorizationHeaders(HttpContext.Current.Request.Headers.Get("Authorization")))
                {
                    response.Status = "unauthorized".ToUpper();
                }
                else
                {
                    MobileGPRattachement mrattachement = new MobileGPRattachement();
                    mrattachement = MobileGPRattachement.GP_OT_Charger(not);
                    if (mrattachement != null)
                    {
                        mrattachement.TypeRattachement = "MVENTE";
                        mrattachement.CEtat = "AN";
                        mrattachement.Observation = jstf;
                        response.Status = mrattachement.Sauvgarder().ToUpper();
                        response.results = mrattachement.NRattachement;
                    }
                    else
                    {
                        response.Status = "NOT_FOUND".ToUpper();
                        response.message = "";
                    }
                }
            }
            catch (Exception ex)
            {
                response.Status = "ERROR".ToUpper();
                response.message = ex.Message;
            }
            afficherJsonResult(response);
        }

        [WebMethod]
        public void justification()
        {
            ResponseJson<String, String> response = new ResponseJson<String, String>();
            if (!AuthorizationHeaders(HttpContext.Current.Request.Headers.Get("Authorization")))
            {
                response.message = "error";
            }
            response.message = "success";
            afficherJsonResult(response);
        }

        [WebMethod]
        public void ajourattachementBCL(string responsable, string CClient, string RaisonSociale)
        {
            ResponseJson<String, String> response = new ResponseJson<string, String>();
            MobileRattachement mrattachement = new MobileRattachement();
            response.message = "success";
            response.results = mrattachement.SauvegarderBCL(responsable, CClient, RaisonSociale);
            afficherJsonResult(response);
        }

        //[WebMethod]
        //public void postTracking()
        //{
        //    MobileTracking tracking = new MobileTracking();
        //    tracking = readfromPostBody<MobileTracking>(HttpContext.Current, tracking);
        //    tracking.sauvegarder(tracking);
        //}

        [WebMethod]
        public void getheure(string id)
        {
            ResponseJson<MobileHeur, String> response = new ResponseJson<MobileHeur, String>();
            MobileHeur heur = new MobileHeur();
            heur = heur.Charger(id);
            response.message = "success";
            response.results = heur;
            afficherJsonResult(response);
        }

        [WebMethod]
        public void getlistboncommandes(string equipe, string dd, string df)
        {
            BonCommande nBonCommande = new BonCommande();
            ResponseJson<List<BonCommande>, String> response = new ResponseJson<List<BonCommande>, String>();
            if (!AuthorizationHeaders(HttpContext.Current.Request.Headers.Get("Authorization")))
            {
                response.message = "error";
            }
            else
            {
                response.message = "success";
                response.results = BonCommandeCollection.MobileCharger(equipe, dd, df);
            }
            afficherJsonResult(response);
        }

        [WebMethod]
        public void getlist_article_boncommandes(string nbcommande, string cclient)
        {
            if (string.IsNullOrEmpty(nbcommande) || nbcommande.Equals("null"))
            {
                ResponseJson<List<string>, MobileClientVisite> response = new ResponseJson<List<string>, MobileClientVisite>();
                response.message = "success";
                response.result1 = MobileClientVisite.Charger(cclient);
                afficherJsonResult(response);
            }
            else
            {
                MobileBonCommandeDetail nBonCommande = new MobileBonCommandeDetail();
                ResponseJson<List<MobileBonCommandeDetail>, BonCommandeClient> response = new ResponseJson<List<MobileBonCommandeDetail>, BonCommandeClient>();
                if (!AuthorizationHeaders(HttpContext.Current.Request.Headers.Get("Authorization")))
                {
                    response.message = "error";
                }
                else
                {
                    response.message = "success";
                    response.result1 = BonCommandeClient.Charger(nbcommande);
                    response.results = MobileBonCommandeDetailCollection.Mobile_Charger(nbcommande);
                }
                afficherJsonResult(response);
            }
        }

        [WebMethod]
        public void getAllCategories()
        {
            ResponseJson<List<MobileArticleFamille>, String> response = new ResponseJson<List<MobileArticleFamille>, String>();
            if (!AuthorizationHeaders(HttpContext.Current.Request.Headers.Get("Authorization")))
            {
                response.message = "error";
            }
            else
            {
                response.message = "success";
                response.results = CST.Stock.Metier.MobileArticleFamille.MobileArticleFamille.Charger();
            }
            afficherJsonResult(response);
        }

        [WebMethod]
        public void GetCategorieArticles(string famille, string CClient, int page)
        {
            ResponseJson<List<CST.Stock.Metier.Article.MobileArticle>, String> response = new ResponseJson<List<CST.Stock.Metier.Article.MobileArticle>, String>();
            MobileArticle articles = new MobileArticle();
            if (!AuthorizationHeaders(HttpContext.Current.Request.Headers.Get("Authorization")))
            {
                response.message = "error";
            }
            else
            {
                Client cl = new Client();
                cl = Client.Charger(CClient);
                if (cl == null)
                {
                    response.message = "error";
                }
                else
                {
                    response.message = "success";
                    response.results = CST.Stock.Metier.Article.MobileArticle.MobileArticleCollection.Charger(famille, CClient, cl.CTarif, page);
                }
            }
            afficherJsonResult(response);
        }

        [WebMethod]
        public void fetchArticles(string famille, string type, string CClient, string PanierCArticles, int page)
        {
            ResponseJson<List<MobileArticle>, String> response = new ResponseJson<List<MobileArticle>, String>();
            if (!AuthorizationHeaders(HttpContext.Current.Request.Headers.Get("Authorization")))
            {
                response.message = "error";
            }
            else
            {
                response.message = "success";
                response.results = MobileArticle.MobileArticleCollection.ChargerParFamilleType(famille, type, CClient, PanierCArticles, page);
            }
            afficherJsonResult(response);
        }

        [WebMethod]
        public void getClientMaps(String Ordre)
        {
            ResponseJson<MobileClient_Position, String> response = new ResponseJson<MobileClient_Position, String>();
            MobileClient_Position Clientmaps = new MobileClient_Position();
            if (!AuthorizationHeaders(HttpContext.Current.Request.Headers.Get("Authorization")))
            {
                response.message = "error";
            }
            else
            {
                response.message = "success";
                response.results = Clientmaps.PositionCharger(Ordre);
            }
            afficherJsonResult(response);
        }

        [WebMethod]
        public void postBoncommande()
        {
            BonCommande bonCommande = new BonCommande();
            Client cl = new Client();
            decimal prixHT = 0;
            BodyBonCommande bn = new BodyBonCommande();
            bn = readfromPostBody<BodyBonCommande>(HttpContext.Current, bn);
            ResponseJson<String, String> response = new ResponseJson<String, String>();

            if (!AuthorizationHeaders(HttpContext.Current.Request.Headers.Get("Authorization")))
            {
                response.message = "error";
            }
            else
            {
                cl = Client.Charger(bn.codeClient);
                int k = 0;
                decimal PoidsTotal = 0;
                decimal SommePoids = 0;
                decimal TotalemontantFodec = 0;
                decimal montantTPETot = 0;
                decimal montantTDCTot = 0;
                decimal assietteTPE = 0;
                decimal assietteTDC = 0;
                decimal totalAssietteRetFor = 0;
                decimal montantFodecTot = 0;
                decimal assietteFodec = 0;
                decimal montanttvatotale = 0;
                decimal montantFodectotale = 0;
                decimal montantTdctotale = 0;
                decimal montanttpetotale = 0;
                decimal assietteForfaitaire = 0;
                decimal montantForfaitaire = 0;
                decimal montantTVA = 0;
                decimal tva = 0;
                decimal TotalemontantForfaitaire = 0;
                decimal montantTTC = 0;
                decimal TotalemontantTTC = 0;
                decimal TotalemontantRemise = 0;
                decimal montanttotaleNet = 0;
                decimal montantTotalHT = 0;

                List<MobileTabArticle> listArticle= new List<MobileTabArticle>();
                bn._articles.ForEach(mobileart =>
                {
                    listArticle.Add(mobileart);
                    if (mobileart.gratuite > 0)
                    {
                        MobileTabArticle art = new MobileTabArticle();
                        //art = mobileart;
                        art.remis = 100;
                        art.BGratuit = true;
                        art.codeArt = mobileart.codeArt;
                        art.libArt = mobileart.libArt;
                        art.gratuite = mobileart.gratuite;
                        art.b = mobileart.b;
                        art.CUnite = mobileart.CUnite;
                        art.ImageArt = mobileart.ImageArt;
                        art.qteOt = mobileart.qteOt;
                        art.qtePrep = mobileart.qtePrep;
                        art.qteRes = mobileart.qteRes;
                        art.remise = mobileart.remise;
                        art.i = mobileart.gratuite;
                        listArticle.Add(art);
                    }
                });

                foreach (MobileTabArticle mobileart in listArticle)
                {
                    decimal Quantite = mobileart.i;

                    decimal montantNetHT = 0;
                    decimal montantHT = 0;
                    decimal montantRemise = 0;
                    BonCommandeDetail detail = new BonCommandeDetail();
                    decimal pourcentagefodec, pourcentageTPE, pourcentageTDC, tauxTVA;
                    BonCommandeTaxe taxeBonCommande = new BonCommandeTaxe();
                    Article articleSaisi = Article.Charger(mobileart.codeArt);
                    //throw new Exception("{message: error}");
                    MobileArticle marticle = MobileArticle.Charger(mobileart.codeArt);
                    Equipe equipe = Equipe.Charger(bn.Cequipe);
                    if (equipe.CEntrepot != null)
                        detail.CEntrepot = equipe.CEntrepot;
                    else
                        detail.CEntrepot = "";
                    detail.CArticle = mobileart.codeArt;
                    detail.LibArticle = mobileart.libArt;
                    detail.CUnite = mobileart.CUnite;
                    detail.Poids = articleSaisi.Poids;

                    //calcul remise
                    ArticlePrix articlePrix = ArticlePrix.Charger(detail.CArticle, cl.CTarif);
                    // _Client.RemiseApplique(article.PrioriteRemise, 0, articlePrix.Remise, articlePrix.RemiseMax, false);

                    //if ( articlePrix.RemiseMax > 0 && articlePrix.RemiseMax < mobileart.remis)
                    //    mobileart.remis = articlePrix.RemiseMax;
                    //mobileart.remis = cl.RemiseApplique(articleSaisi.PrioriteRemise, 0, articlePrix.Remise, articlePrix.RemiseMax, false);


                    if (!string.IsNullOrEmpty(marticle.CGratuites))
                    {
                        detail.CGratuites = marticle.CGratuites;
                        detail.DateGratuitesDebut = marticle.DateGratuitesDebut;
                        detail.DateGratuitesFin = marticle.DateGratuitesFin;
                    }
                    detail.BGratuit = mobileart.BGratuit;
                    detail.Quantite = Quantite;
                    detail.Remise2 = 0;
                    detail.Remise1 = bn.remise + mobileart.remis - ((bn.remise * mobileart.remis) / 100);

                    //detail.PourcentageRemise = detail.Remise1 + detail.Remise2 - ((detail.Remise1 * detail.Remise2) / 100);
                    detail.PourcentageRemise = detail.Remise1;
                    detail.QuantiteHistorique = detail.Quantite;
                    if (articlePrix != null)
                    {
                        detail.PrixHTArticle = articlePrix.PrixHT;
                        prixHT = articlePrix.PrixHT;
                    }
                    else
                    {
                        detail.PrixHTArticle = 0;
                        prixHT = 0;
                    }
                    detail.Ordre = k;
                    k++;
                    PoidsTotal = Quantite * detail.Poids;
                    SommePoids = SommePoids + PoidsTotal;
                    //pourcentagefodec = cl.PrcFodecApplique(articleSaisi.Fodec);
                    pourcentagefodec = 0;
                    //pourcentageTPE = cl.PrcTPEApplique(articleSaisi.TPE);
                    //pourcentageTDC = cl.PrcTDCApplique(articleSaisi.TaxeDroitConsommation);
                    pourcentageTPE = 0;
                    pourcentageTDC = 0;
                    detail.PourcentageFodec = pourcentagefodec;
                    detail.PourcentageTPE = pourcentageTPE;
                    detail.PourcentageTDC = pourcentageTDC;
                    tauxTVA = cl.TauxTVAApplique(articleSaisi.CTaxeVente);
                    montantHT = prixHT * Quantite;
                    montantTotalHT += montantHT;
                    montantRemise = (montantHT * detail.PourcentageRemise) / 100;
                    TotalemontantRemise += montantRemise;
                    montantNetHT = montantHT - montantRemise;
                    detail.MontantNet = montantNetHT;
                    montanttotaleNet += montantNetHT;
                    decimal montantFodec = 0;
                    montantFodec = montantNetHT * pourcentagefodec / 100;
                    TotalemontantFodec = TotalemontantFodec + montantFodec;
                    decimal montantTPE = montantNetHT * pourcentageTPE / 100;
                    montantTPETot = montantTPE * pourcentageTPE / 100;
                    decimal montantTDC = montantNetHT * pourcentageTDC / 100;
                    montantTDCTot = montantTDC * pourcentageTDC / 100;
                    decimal assiette = montantFodec + montantTPE + montantTDC + montantNetHT;
                    if (montantFodec != 0)
                        assietteFodec = assietteFodec + montantNetHT;
                    if (montantTPE != 0)
                        assietteTPE = assietteTPE + montantNetHT;
                    if (montantTDC != 0)
                        assietteTDC = assietteTDC + montantNetHT;
                    montantTVA = assiette * tauxTVA / 100;
                    detail.MontantTaxe = montantTVA;
                    assietteForfaitaire = assiette + montantTVA;
                    if (cl.BAvanceForfaitaire)
                    {
                        totalAssietteRetFor = totalAssietteRetFor + assiette + montantTVA;
                    }
                    if (cl.BAvanceForfaitaire)
                    {
                        montantForfaitaire = assietteForfaitaire * VenteHelper.POURCENTAGE_TAUX_FORFAITAIRE / 100;
                        TotalemontantForfaitaire = TotalemontantForfaitaire + montantForfaitaire;
                    }
                    montantTTC = assietteForfaitaire + montantForfaitaire;
                    TotalemontantTTC += montantTTC;
                    montanttvatotale += montantTVA;
                    montantFodectotale = montantFodectotale + montantFodec;
                    montantTdctotale = montantTdctotale + montantTDC;
                    montanttpetotale = montanttpetotale + montantTDC;
                    if (string.IsNullOrEmpty(detail.CTaxe))
                        detail.CTaxe = articleSaisi.CTaxeVente;
                    detail.TauxTVA = cl.TauxTVAApplique(articleSaisi.CTaxeVente);
                    bonCommande.BonCommandeDetailCollection.Add(detail);
                    if (detail.CTaxe != null)
                    {
                        BonCommandeTaxe bonCommandeTaxe = bonCommande.BonCommandeTaxeCollection.RecupererBonCommandeTaxe(detail.CTaxe);
                        //Taxe taxe = Taxe.Charger(detail.CTaxe);
                        if (bonCommandeTaxe != null)
                        {
                            if (string.IsNullOrEmpty(bonCommandeTaxe.CTaxe))
                            {
                                bonCommandeTaxe.CTaxe = detail.CTaxe;
                            }
                            bonCommandeTaxe.MontantTaxe = bonCommandeTaxe.MontantTaxe + (assiette * detail.TauxTVA) / 100;
                            bonCommandeTaxe.TauxTVA = detail.TauxTVA;
                            bonCommandeTaxe.Assiette = bonCommandeTaxe.Assiette + assiette;
                            bonCommandeTaxe.BExonoreFodec = bonCommande.BExonoreFodec;
                            //bonCommandeTaxe.BExonoreTPE = bonCommande.BExonoreTPE;
                            //bonCommandeTaxe.BExonoreTDC = bonCommande.BExonoreTDC;
                            bonCommandeTaxe.BExonoreTVA = bonCommande.BExonoreTVA;
                            bonCommande.BonCommandeTaxeCollection.Remove(bonCommandeTaxe);
                        }
                        else
                        {
                            bonCommandeTaxe = new BonCommandeTaxe();
                            bonCommandeTaxe.CTaxe = detail.CTaxe;
                            bonCommandeTaxe.MontantTaxe = bonCommandeTaxe.MontantTaxe + (assiette * detail.TauxTVA) / 100;
                            bonCommandeTaxe.TauxTVA = detail.TauxTVA;
                            bonCommandeTaxe.Assiette = bonCommandeTaxe.Assiette + assiette;
                            bonCommandeTaxe.BExonoreFodec = bonCommande.BExonoreFodec;
                            //bonCommandeTaxe.BExonoreTPE = bonCommande.BExonoreTPE;
                            //bonCommandeTaxe.BExonoreTDC = bonCommande.BExonoreTDC;
                            bonCommandeTaxe.BExonoreTVA = bonCommande.BExonoreTVA;
                        }
                        bonCommande.BonCommandeTaxeCollection.Add(bonCommandeTaxe);
                    }

                }
                bonCommande.CClient = cl.CClient;
                bonCommande.NTelephone = cl.NumeroTelephone1;
                foreach (Adresse adresse in cl.Adresses)
                {
                    if (adresse.BAdresseFacturation)
                    {
                        bonCommande.Adresse = adresse.LibAdresse + " " + adresse.Ville;
                        break;
                    }
                }
                bonCommande.MatriculeFiscale = cl.CTVA;
                bonCommande.CVendeur = Convert.ToInt32(bn.Cequipe);
                //bonCommande.CreePar = Convert.ToInt32(bn.Cequipe);
                bonCommande.RaisonSociale = cl.RaisonSociale;
                bonCommande.DateLivraison = bn.datedelivraison;
                bonCommande.DateCommande = DateTime.Now;
                bonCommande.BValide = 0;
                bonCommande.CTBAchat = bn.CTBAchat;
                bonCommande.LibTBAchat = bn.LibTBAchat;
                bonCommande.PCInsertion = bn.Cequipe + "|" + bn.user;
                bonCommande.PCModification = bn.Cequipe + "|" + bn.user;
                bonCommande.CEquipe = bn.Cequipe;
                bonCommande.Observation = bn.reclamation;
                bonCommande.Etat = VenteHelper.EtatBonCommande.ENATTENTE.ToString();
                bonCommande.Exercice = DateTime.Now.Year.ToString();
                bonCommande.Reference = bn.libre == true ? bn.Cequipe : bn.nordretravail;
                bonCommande.BExonoreTVA = cl.ExonerationTVA();
                //bonCommande.BExonoreTPE = cl.ExonerationTPE();
                //bonCommande.BExonoreTDC = cl.ExonerationTDC();
                bonCommande.BAvanceForfaitaire = cl.BAvanceForfaitaire;
                bonCommande.MontantHT = montantTotalHT;
                bonCommande.MontantRemise = TotalemontantRemise;
                //throw new Exception("error");
                bonCommande.MontantTaxe = montanttvatotale + montantFodectotale + TotalemontantForfaitaire + montanttpetotale + montantTdctotale;
                bonCommande.MontantTTC = montanttotaleNet + bonCommande.MontantTaxe;
                bonCommande.MontantRetenuForfaitaire = TotalemontantForfaitaire;
                bonCommande.BValide = 0;
                bonCommande.ordre = bn.ordre;
                //bonCommande.CModeReglement = bn.mPaiment;
                bonCommande.ModalitesPaiement = bn.mPaiment;
                bonCommande.CTypeBonCommande = bn.TPC;
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlTransaction transaction = cn.BeginTransaction();
                    try
                    {
                        if (string.IsNullOrEmpty(bn.nbcommande))
                        {
                            bonCommande.mobileInserer(transaction);
                            MobileRattachement rattachment = new MobileRattachement();
                            MobileOrdre mobileOrdre = new MobileOrdre();
                            if (!string.IsNullOrEmpty(bn.nordretravail))
                            {
                                rattachment.Gp_Rattement_update(transaction, bn.NRattachement, bn.file, bonCommande.NBonCommande, bn.user, bn.Cequipe, bn.reclamation, "VD");
                                mobileOrdre.UpdateOrdre(transaction, bn.nordretravail, bonCommande.NBonCommande, bn.user, bn.Cequipe);
                                //throw new Exception("error");
                            }
                        }
                        else
                        {
                            bonCommande.NBonCommande = bn.nbcommande;
                            bonCommande.mobileModifier(transaction);
                        }
                        
                        transaction.Commit();
                        response.message = "success";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
            response.results = bonCommande.NBonCommande;
            afficherJsonResult(response);
        }

        /*
         * 
         * CRM Service
         * 
         * 
         */
        [WebMethod]
        public void CRM_ArticleList(string type)
        {
            ResponseJson<List<MobileArticlee>, string> response = new ResponseJson<List<MobileArticlee>, string>();

            if (!AuthorizationHeaders(HttpContext.Current.Request.Headers.Get("Authorization")))
            {
                response.message = "error";
            }
            else
            {
                response.message = "success";
                response.results = MobileArticleeCollection.CrmarticlesCharger(type);
            }
            afficherJsonResult(response);
        }

        [WebMethod]
        public void CRM_notePresentoireList()
        {
            string type = "PRS";
            ResponseJson<List<MobileNote>, string> response = new ResponseJson<List<MobileNote>, string>();

            if (!AuthorizationHeaders(HttpContext.Current.Request.Headers.Get("Authorization")))
            {
                response.message = "error";
            }
            else
            {
                response.message = "success";
                response.results = noteMobileCollection.Charger(type);
            }
            afficherJsonResult(response);
        }

        [WebMethod]
        public void CRM_MarqueList()
        {
            string type = "MRQ";
            ResponseJson<List<MobileNote>, string> response = new ResponseJson<List<MobileNote>, string>();

            if (!AuthorizationHeaders(HttpContext.Current.Request.Headers.Get("Authorization")))
            {
                response.message = "error";
            }
            else
            {
                response.message = "success";
                response.results = noteMobileCollection.Charger(type);
            }
            afficherJsonResult(response);
        }

        [WebMethod]
        public void CRM_grosistList()
        {

            string type = "GRO";
            ResponseJson<List<MobileNote>, string> response = new ResponseJson<List<MobileNote>, string>();

            if (!AuthorizationHeaders(HttpContext.Current.Request.Headers.Get("Authorization")))
            {
                response.message = "error";
            }
            else
            {

                response.message = "success";
                response.results = noteMobileCollection.Charger(type);
            }
            afficherJsonResult(response);
        }

        [WebMethod]
        public void CRM_motifList()
        {
            string type = "JUSTIF";
            ResponseJson<List<MobileNote>, string> response = new ResponseJson<List<MobileNote>, string>();

            if (!AuthorizationHeaders(HttpContext.Current.Request.Headers.Get("Authorization")))
            {
                response.message = "error";
            }
            else
            {
                response.message = "success";
                response.results = noteMobileCollection.Charger(type);
            }
            afficherJsonResult(response);
        }

        [WebMethod]
        public void CRM_concurrenceList()
        {
            string type = "SCNC";
            ResponseJson<List<MobileNote>, string> response = new ResponseJson<List<MobileNote>, string>();

            if (!AuthorizationHeaders(HttpContext.Current.Request.Headers.Get("Authorization")))
            {
                response.message = "error";
            }
            else
            {
                response.message = "success";
                response.results = noteMobileCollection.Charger(type);
            }
            afficherJsonResult(response);
        }

        [WebMethod]
        public void CRM_bachatList()
        {
            string type = "BA";
            ResponseJson<List<MobileNote>, string> response = new ResponseJson<List<MobileNote>, string>();
            response.message = "success";
            response.results = noteMobileCollection.Charger(type);
            /* if (!AuthorizationHeaders(HttpContext.Current.Request.Headers.Get("Authorization")))
             {
                 response.message = "error";
             }
             else
             {
                 response.message = "success";
                 response.results = noteMobileCollection.Charger(type);
             }       */
            afficherJsonResult(response);
        }

        [WebMethod]
        public void postCrm()
        {
            ResponseJson<String, PostCrm> response = new ResponseJson<String, PostCrm>();
            try
            {
                PostCrm bn = new PostCrm();
                bn = readfromPostBody<PostCrm>(HttpContext.Current, bn);
                MobileRattachement rattachement = new MobileRattachement();
                rattachement.NRattachement = bn.Nrattachement;
                rattachement.JustificationVente = bn.JustificationVente;
                rattachement.JustificationRecouvrement = bn.JustificationRecouvrement;
                rattachement.StrategieConcurence = bn.StrategieConcurence;

                int _CEquipe = 0;
                //rattachement.NOrdredeTravail = bn.Nordre;
                response.result1 = bn;
                int.TryParse(bn.Cequipe, out _CEquipe);
                response.result1 = bn;
                rattachement.NBonCommande = "";
                rattachement.TypeRattachement = "CRM";
                rattachement.CEtat = "VD"; // Valide
                rattachement.SignatureClient = bn.file;
                if (bn.Bretour == true)
                    rattachement.DateRetour = Convert.ToDateTime(bn.dateRetour);

                rattachement.Remarque = (bn.Bretour ? "Date de retour: " + rattachement.DateRetour.Value.ToShortDateString() + ", " : "") + bn.observation;
                rattachement.CreePar = _CEquipe;
                rattachement.PCInsertion = bn.Utilisateur;

                foreach (PosteNote note in bn.spresentoires)
                {
                    if (note != null)
                    {
                        MobileRattachementArticle art = new MobileRattachementArticle();
                        art.CArticle = note.codeArt;
                        art.Libelle = note.libArt;
                        art.CNoteRattachement = note.CnotePresentoire;
                        rattachement.RattachementArticles.Add(art);
                    }
                }
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlTransaction transaction = cn.BeginTransaction();
                    try
                    {
                        MobileRattachement MobileR = new MobileRattachement();
                        string id = ""; id = bn.Nordre;
                        MobileR = MobileRattachement.GP_OrdreTravail_avoir(transaction, id);
                        if (bn.Bretour == true)
                        {
                            MobileConventionClient convClient = new MobileConventionClient();
                            convClient.DatePlanif = bn.dateRetour;
                            convClient.NConvention = MobileR.NConvention;
                            convClient.CreePar = _CEquipe;
                            convClient.CTypeVisite = "RT";
                            convClient.TIntervention = 0;
                            convClient.PCInsertion = bn.Utilisateur + "|" + bn.Cequipe;
                            convClient.DateInsertion = DateTime.Now;
                            convClient.SauvegarderConvention(transaction);
                        }
                        MobileConventionClientTechnicien mConvention = new MobileConventionClientTechnicien();
                        mConvention.BValid = 1;
                        mConvention.CreerPar = _CEquipe;
                        mConvention.NConvention = MobileR.NConvention;
                        mConvention.DatePlanification = MobileR.Dateplanification;
                        mConvention.ModifierConventionTechnicien(transaction);
                        rattachement.Modifier(transaction, bn.Nrattachement);

                        //if there is no vente
                        if (!bn.Bvente)
                        {
                            rattachement.CEtat = bn.CEtat;
                            rattachement.TypeRattachement = "MVENTE";
                            rattachement.NOrdredeTravail = bn.Nordre;
                            rattachement.CEquipe = _CEquipe.ToString();
                            rattachement.CClient = bn.Cclient;
                            rattachement.SauvegarderDirect(transaction);
                        }

                        //MobileRattachement rattachement = new MobileRattachement();
                        MobileOrdre mobileOrdre = new MobileOrdre();
                        /// rattachment.Gp_Rattement_update(transaction, bn.ordre, bn.file, bonCommande.NBonCommande, bn.user, int.Parse(bn.Cequipe));
                        mobileOrdre.UpdateCrmOrdre(transaction, bn.Nordre, bn.Cequipe, bn.Utilisateur);
                        transaction.Commit();
                        response.Status = "OK".ToUpper();
                        response.message = "success";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                response.Status = "ERROR".ToUpper();
                response.message = ex.Message;
            }
            afficherJsonResult(response);
        }

        [WebMethod]
        public void Crmplanifiee(string id, string dd, string df, string cr, string cg)
        {
            ResponseJson<List<MobileOrdre>, int> response = new ResponseJson<List<MobileOrdre>, int>();
            if (!AuthorizationHeaders(HttpContext.Current.Request.Headers.Get("Authorization")) || id.Equals(""))
            {
                response.message = "error ";
            }
            else
            {
                response.message = "success";
                response.result1 = MobileOrdreCollection.CRMNombreManquer(id);
                response.results = MobileOrdreCollection.CrmplanifieCharger(id, dd, df, cr, cg);
            }
            afficherJsonResult(response);
        }

        [WebMethod]
        public void CrmOrdreDetail(string cclient)
        {
            ResponseJson<MobileOrdreDetail, string> response = new ResponseJson<MobileOrdreDetail, string>();
            if (!AuthorizationHeaders(HttpContext.Current.Request.Headers.Get("Authorization")) || cclient.Equals(""))
            {
                response.message = "error ";
            }
            else
            {
                response.message = "success";
                response.results = MobileOrdreDetail.OrdreDetailCharger(cclient);
            }
            afficherJsonResult(response);
        }

        [WebMethod]
        public void Crmencours(string id, string dd, string df, string cr, string cg)
        {
            ResponseJson<List<MobileOrdre>, string> response = new ResponseJson<List<MobileOrdre>, string>();
            if (!AuthorizationHeaders(HttpContext.Current.Request.Headers.Get("Authorization")) || id.Equals(""))
            {
                response.message = "error ";
            }
            else
            {
                response.message = "success";
                response.results = MobileOrdreCollection.CrmencoursCharger(id, dd, df, cr, cg);
            }
            afficherJsonResult(response);
        }

        [WebMethod]
        public void Crmvalides(string id, string dd, string df, string cr, string cg)
        {
            ResponseJson<List<MobileOrdre>, string> response = new ResponseJson<List<MobileOrdre>, string>();
            if (!AuthorizationHeaders(HttpContext.Current.Request.Headers.Get("Authorization")) || id.Equals(""))
            {
                response.message = "error";
            }
            else
            {
                response.message = "success";
                response.results = MobileOrdreCollection.CrmvalidesCharger(id, dd, df, cr, cg);
            }
            afficherJsonResult(response);
        }

        [WebMethod]
        public void CRM_rattachement(string rattachment)
        {
            ResponseJson<List<RattachementMobile>, Rattachment> response = new ResponseJson<List<RattachementMobile>, Rattachment>();
            response.message = "success";
            response.results = RattachementMobileCollection.Charger(rattachment);
            response.result1 = Rattachment.Charger(rattachment);

            afficherJsonResult(response);
        }

        [WebMethod]
        public void getSattelite(TypeSattelite type) 
        {
            ResponseJson<object, string> response = new ResponseJson<object, string>();
            try
            {

                if (!AuthorizationHeaders(HttpContext.Current.Request.Headers.Get("Authorization")))
                {
                    response.Status = "unauthorized".ToUpper();
                }
                else
                {
                    response.message = "success";
                    response.Status = "OK";
                    switch (type)
                    {
                        case TypeSattelite.Etat:
                            response.results = MobileEtatCollection.Charger();
                            break;
                        case TypeSattelite.OptionModalitePaiement:
                            response.results = MobileOptionCollection.Charger("MODPAI");
                            break;
                        case TypeSattelite.OptionJustificationNV:
                            MobileOptionCollection col = MobileOptionCollection.Charger("JUSTIF");
                            col.AddRange(MobileOptionCollection.Charger("JUSTIFREC"));
                            response.results = col;
                            break;
                        case TypeSattelite.OptionJustification:
                            response.results = MobileOptionCollection.Charger("JUSTIF");
                            break;
                        case TypeSattelite.OptionJustificationNVis:
                            response.results = MobileOptionCollection.Charger("JUSTIFVIS");
                            break;
                        case TypeSattelite.OptionJustificationR:
                            response.results = MobileOptionCollection.Charger("JUSTIFREC");
                            break;
                        case TypeSattelite.OptionStrategie:
                            response.results = MobileOptionCollection.Charger("SCNC");
                            break;
                        case TypeSattelite.FamilleArticle:
                            response.results = MobileFamilleArticleCollection.Charger();
                            break;
                        case TypeSattelite.TypeArticle:
                            response.results = MobileTypeArticleCollection.Charger();
                            break;
                        case TypeSattelite.BonAchat:
                            response.results = bonAchatMobileCollection.Charger();
                            break;
                        case TypeSattelite.TypeBonCommande:
                            response.results = MobileCTBCommandeCollection.Charger();
                            break;
                        case TypeSattelite.Region:
                            response.results = RegionMobileCollection.Charger();
                            break;
                        case TypeSattelite.Gouvernorat:
                            response.results = MobileGouvernoratCollection.Charger();
                            break;

                        default:
                            response.Status = "NOTFOUND".ToUpper();
                            response.message = "INCORRECT CHOICE";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                response.Status = "ERROR".ToUpper();
                response.message = ex.Message;
            }
            afficherJsonResult(response);
        }

        [WebMethod]
        public void Recouvrement(string CClient)
        {
            ResponseJson<object, object> response = new ResponseJson<object, object>();
            try
            {
                if (!AuthorizationHeaders(HttpContext.Current.Request.Headers.Get("Authorization")))
                {
                    response.Status = "unauthorized".ToUpper();
                }
                else
                {
                    response.result1 = MobileRecouvrementClientCollection.Charger(CClient);
                    response.results = MobileRecouvrementCollection.Charger(CClient);
                    response.message = "success";
                    response.Status = "OK";
                }
            }
            catch (Exception ex)
            {
                response.Status = "ERROR".ToUpper();
                response.message = ex.Message;
            }
            afficherJsonResult(response);
        }

        [WebMethod]
        public void Region(string CEquipe)
        {
            ResponseJson<List<MobileRegion>, string> response = new ResponseJson<List<MobileRegion>, string>();
            if (!AuthorizationHeaders(HttpContext.Current.Request.Headers.Get("Authorization")))
            {
                response.message = "error";
            }
            else
            {
                response.message = "success";
                response.results = RegionMobileCollection.Charger(CEquipe);
            }
            afficherJsonResult(response);
        }

        [WebMethod]
        public void Gouvernorat(string CEquipe)
        {
            ResponseJson<List<MobileGouvernorat>, string> response = new ResponseJson<List<MobileGouvernorat>, String>();
            if (!AuthorizationHeaders(HttpContext.Current.Request.Headers.Get("Authorization")))
            {
                response.message = "error";
            }
            else
            {
                response.message = "success";
                response.results = MobileGouvernoratCollection.Charger(CEquipe);
            }
            afficherJsonResult(response);
        }

        private static JavaScriptSerializer GetJss()
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            jss.MaxJsonLength = Int32.MaxValue;
            return jss;
        }

        string sha1Hash(string password)
        {
            return string.Join("", System.Security.Cryptography.SHA1CryptoServiceProvider.Create().ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)).Select(x => x.ToString("x2")));
        }

        string base64(string key)
        {
            byte[] decodedByteArray = Convert.FromBase64CharArray(key.ToCharArray(), 0, key.Length);
            string decodedString = System.Text.Encoding.UTF8.GetString(decodedByteArray);
            return decodedString;
        }

        T readfromPostBody<T>(HttpContext http, T value)
        {
            string body;
            http.Request.InputStream.Position = 0;
            body = new System.IO.StreamReader(http.Request.InputStream, System.Text.Encoding.UTF8).ReadToEnd();
            value = JsonConvert.DeserializeObject<T>(body);
            return value;
        }

        protected bool _useauthentification = false;
        
        private bool AuthorizationHeaders(string context)
        {
            if (!_useauthentification) return true;
            string authorizationDecoded, user, pass;
            bool result = true;
            if (string.IsNullOrEmpty(context))
            {
                result = false;
                goto back;
            }
            
            authorizationDecoded = base64(context.ToString().Substring(6, context.ToString().Length - 6));
            user = authorizationDecoded.Substring(0, authorizationDecoded.IndexOf(":"));
            pass = authorizationDecoded.Substring(authorizationDecoded.IndexOf(":") + 1, authorizationDecoded.Length - 1 - user.Length);
            if (!user.Equals("cst") || !pass.Equals("@dmin123"))
                result = false;
            back:
            return result;
        }

        public void afficherJsonResult(object response)
        {
            var json = "";
            JavaScriptSerializer jss = GetJss();
            json = jss.Serialize(response);
            this.Context.Response.ContentType = "application/json; charset=utf-8";
            this.Context.Response.Write(json);
        }
    }

    public enum TypeSattelite {
        Etat = 1,
        OptionModalitePaiement,
        OptionJustification,
        OptionJustificationNV,
        OptionJustificationNVis,
        OptionJustificationR,
        OptionStrategie,
        OptionPresentoire,
        OptionChoixPresentoire,
        OptionMarque,
        OptionChoixMarque,
        OptionGrossiste,
        FamilleArticle,
        TypeArticle,
        BonAchat,
        TypeBonCommande,
        Region,
        Gouvernorat
    }
}