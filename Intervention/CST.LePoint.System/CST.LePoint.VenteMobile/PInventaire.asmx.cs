using CST.LePoint.Stock.Metier;
using CST.LePoint.VenteMobile.Metier;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using WebApp.Post;

namespace WebApp
{
    /// <summary>
    /// Description résumée de PInventaire
    /// </summary>
    [WebService(Namespace = "http://polyflex.cst/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class PInventaire : System.Web.Services.WebService
    {
        [WebMethod]
        public void ArticleSync()
        {
            ResponseJson<object, object> response = new ResponseJson<object, object>();
            response.message = "error";
            try
            {
                //response.results = MobilePolyflexArticleCollection.Charger() as MobilePolyflexArticleCollection;
                response.result1 = MobilePolyflexArticleContenantCollection.Charger() as MobilePolyflexArticleContenantCollection;
                response.message = "success";
            }
            catch (Exception)
            {
                response.message = "error";
            }
            finally
            {
                afficherJsonResult(response);                
            }
        }

        [WebMethod]
        public void PostInventaire() 
        {
            ResponseJson<object, object> response = new ResponseJson<object, object>();
            try
            {
                BodyInventaire inventaire = new BodyInventaire();
                BonInventaire bonInventaire = new BonInventaire();
                inventaire = readfromPostBody<BodyInventaire>(HttpContext.Current, inventaire);
                MobilePolyflexArticleContenantCollection Contenants = new MobilePolyflexArticleContenantCollection();
                MobilePolyflexArticleCollection Article = new MobilePolyflexArticleCollection();
                Contenants = inventaire.Contenants;
                foreach (MobilePolyflexArticleContenant c in Contenants) 
                {
                    if (Article.Exists(x => x.CArticle == c.CArticle))
                        Article.Find(x => x.CArticle == c.CArticle).Quantite += c.Quantite;
                    else
                    {
                        MobilePolyflexArticle art = new MobilePolyflexArticle();
                        art.CArticle = c.CArticle;
                        art.LibArticle = c.LibArticle;
                        art.Quantite = c.Quantite;
                        Article.Add(art);
                    }
                }

                bonInventaire.CReleveur = inventaire.CReleveur;
                bonInventaire.CEntrepot = inventaire.CEntrepot;
                bonInventaire.Exercice = DateTime.Now.Year.ToString();
                bonInventaire.DateInventaire = DateTime.Now.Date;
                bonInventaire.BInventaireFinAnnee = true;

                foreach (MobilePolyflexArticle ar in Article) 
                {
                    BonInventaireDetail detail = new BonInventaireDetail();
                    ArticleEntrepot articleEntrepot = ArticleEntrepot.Charger(ar.CArticle, bonInventaire.CEntrepot);
                    if (articleEntrepot != null)
                    {
                        detail.CArticle = ar.CArticle;
                        detail.CEntrepot = bonInventaire.CEntrepot;
                        detail.NBonInventaire = bonInventaire.NBonInventaire;
                        detail.LibArticle = ar.LibArticle;
                        detail.QuantiteHisto = articleEntrepot.StockReel;
                        detail.Quantite = ar.Quantite;
                        bonInventaire.BonInventaireDetailCollection.Add(detail);
                    }
                }

                bonInventaire.Inserer();

                response.message = "success";
            }
            catch (Exception)
            {
                response.message = "error";           
            }
            finally 
            {
                afficherJsonResult(response);                                
            }
        }

        [WebMethod]
        public void SatteliteEntrepot()
        {
            ResponseJson<object, object> response = new ResponseJson<object, object>();
            response.message = "error";
            try
            {
                response.results = MobilePolyflexEntrepotCollection.Charger() as MobilePolyflexEntrepotCollection;
                response.message = "success";
            }
            catch (Exception)
            {
                response.message = "error";
            }
            finally
            {
                afficherJsonResult(response);
            }
        }

        [WebMethod]
        public void SatteliteReleveur()
        {
            ResponseJson<object, object> response = new ResponseJson<object, object>();
            response.message = "error";
            try
            {
                response.results = MobilePolyflexReleveurCollection.Charger() as MobilePolyflexReleveurCollection;
                response.message = "success";
            }
            catch (Exception)
            {
                response.message = "error";
            }
            finally
            {
                afficherJsonResult(response);
            }
        }

        private static JavaScriptSerializer GetJss()
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            jss.MaxJsonLength = Int32.MaxValue;
            return jss;
        }

        T readfromPostBody<T>(HttpContext http, T value)
        {
            string body;
            http.Request.InputStream.Position = 0;
            body = new System.IO.StreamReader(http.Request.InputStream, System.Text.Encoding.UTF8).ReadToEnd();
            value = JsonConvert.DeserializeObject<T>(body);
            return value;
        }

        private void afficherJsonResult(object response)
        {
            var json = "";
            JavaScriptSerializer jss = GetJss();
            json = jss.Serialize(response);
            this.Context.Response.ContentType = "application/json; charset=utf-8";
            this.Context.Response.Write(json);
        }
    }
}
