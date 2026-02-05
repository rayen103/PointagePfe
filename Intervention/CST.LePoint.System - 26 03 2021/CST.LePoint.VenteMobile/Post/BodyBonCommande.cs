using CST.LePoint.VenteMobile.Metier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace WebApp.Post
{
    public class BodyBonCommande
    {
        private string _nbcommande;

        public string nbcommande
        {
            get { return _nbcommande; }
            set { _nbcommande = value; }
        }

        private string _nordretravail;

        public string nordretravail
        {
            get { return _nordretravail; }
            set { _nordretravail = value; }
        }

        private decimal _remise;
        public decimal remise
        {
            get { return _remise; }
            set { _remise = value; }
        }

        private string _reclamation;
        public string reclamation
        {
            get { return _reclamation; }
            set { _reclamation = value; }
        }

        private string _montantHT;
        public string montantHT
        {
            get { return _montantHT; }
            set { _montantHT = value; }
        }

        private string _montantRemise;
        public string montantRemise
        {
            get { return _montantRemise; }
            set { _montantRemise = value; }
        }

        private DateTime? _datedelivraison;
        public DateTime? datedelivraison
        {
            get { return _datedelivraison; }
            set { _datedelivraison = value; }
        }

        private string _codeAchat;
        public string codeAchat
        {
            get { return _codeAchat; }
            set { _codeAchat = value; }
        }

        private string _libAchat;
        public string libAchat
        {
            get { return _libAchat; }
            set { _libAchat = value; }
        }

        private string _codeClient;
        public string codeClient
        {
            get { return _codeClient; }
            set { _codeClient = value; }
        }

        public string _Cequipe;
        public string Cequipe
        {
            get { return _Cequipe; }
            set { _Cequipe = value; }
        }

        public string _user;
        public string user
        {
            get { return _user; }
            set { _user = value; }
        }

        private string _cEntrepot;

        public string cEntrepot
        {
            get { return _cEntrepot; }
            set { _cEntrepot = value; }
        }


        string _file;
        public string file
        {
            get { return _file; }
            set { _file = value; }
        }

        private string _ordre;
        public string ordre
        {
            get { return _ordre; }
            set { _ordre = value; }
        }

        private bool _libre;
        public bool libre
        {
            get { return _libre; }
            set { _libre = value; }
        }

        private string _CTBAchat;
        public string CTBAchat
        {
            get { return _CTBAchat; }
            set { _CTBAchat = value; }
        }
        private string _LibTBAchat;

        public string LibTBAchat
        {
            get { return _LibTBAchat; }
            set { _LibTBAchat = value; }
        }

        public string _mPaiment;
        public string mPaiment
        {
            get { return _mPaiment; }
            set { _mPaiment = value; }
        }

        public string _TPC;
        public string TPC
        {
            get { return _TPC; }
            set { _TPC = value; }
        }

        public List<MobileTabArticle> _articles;
        public List<MobileTabArticle> articles
        {
            get { return _articles; }
            set { _articles = value; }
        }

        public string _NRattachement;
        public string NRattachement
        {
            get { return _NRattachement; }
            set { _NRattachement = value; }
        }
    }
}