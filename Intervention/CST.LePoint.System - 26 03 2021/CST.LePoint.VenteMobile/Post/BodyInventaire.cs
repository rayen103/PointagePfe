using CST.LePoint.VenteMobile.Metier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Post
{
    public class BodyInventaire
    {
        public string CEntrepot { get; set; }
        public string CReleveur { get; set; }
        public MobilePolyflexArticleCollection Articles { get; set; }
        public MobilePolyflexArticleContenantCollection Contenants { get; set; }
    }
}