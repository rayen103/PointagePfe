using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CST.LePoint.VenteMobile.Metier
{
    public class PostCrm
    {
        private bool _Bvente;
        public bool Bvente
        {
            get { return _Bvente; }
            set { _Bvente = value; }
        }

        private bool _Bretour;
        public bool Bretour
        {
            get { return _Bretour; }
            set { _Bretour = value; }
        }

        /* private PosteNote _justif;
         public PosteNote justif
         {
             get { return _justif; }
             set { _justif = value; }
         }


         private PosteNote _concu;
         public PosteNote concu
         {
             get { return _concu; }
             set { _concu = value; }
         }  */

        private DateTime? _dateRetour;
        public DateTime? dateRetour
        {
            get { return _dateRetour; }
            set { _dateRetour = value; }
        }

        private string _observation;
        public string observation
        {
            get { return _observation; }
            set { _observation = value; }
        }

        private string _CEtat;
        public string CEtat
        {
            get { return _CEtat; }
            set { _CEtat = value; }
        }

        private string _Nrattachement;
        public string Nrattachement
        {
            get { return _Nrattachement; }
            set { _Nrattachement = value; }
        }

        private string _Nordre;
        public string Nordre
        {
            get { return _Nordre; }
            set { _Nordre = value; }
        }

        private string _Cclient;
        public string Cclient
        {
            get { return _Cclient; }
            set { _Cclient = value; }
        }

        private string _Cequipe;
        public string Cequipe
        {
            get { return _Cequipe; }
            set { _Cequipe = value; }
        }

        private string _Utilisateur;
        public string Utilisateur
        {
            get { return _Utilisateur; }
            set { _Utilisateur = value; }
        }

        private List<PosteNote> _spresentoires;
        public List<PosteNote> spresentoires
        {
            get { return _spresentoires; }
            set { _spresentoires = value; }
        }

        string _file;
        public string file
        {
            get { return _file; }
            set { _file = value; }
        }

        public string JustificationVente { get; set; }
        public string JustificationRecouvrement { get; set; }
        public string StrategieConcurence { get; set; }
        public bool BRecouvrement { get; set; }

        /*
        private List<PosteNote> _smarques;
        public List<PosteNote> smarques
        {
            get { return _smarques; }
            set { _smarques = value; }
        }
        private List<PosteNote> _sgrossistes;
        public List<PosteNote> sgrossistes
        {
            get { return _sgrossistes; }
            set { _sgrossistes = value; }
        }
        */
    }
}