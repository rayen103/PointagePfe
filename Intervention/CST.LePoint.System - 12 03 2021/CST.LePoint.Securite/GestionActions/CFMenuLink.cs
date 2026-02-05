using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CST.LePoint.Securite.GestionActions
{
    [Serializable]
    public class CFMenuLink : CFMenu
    {
        public CFMenuLink()
        {
            SousMenus = new List<CFMenu>();
        }

        public CFMenuLink(string ressourceLib, string idEvenement)
            : this()
        {
            this.RessourceCaption = ressourceLib;
            this.IdEvenement = idEvenement;
        }

        [XmlAttribute]
        public string IdEvenement { get; set; }

        [XmlAttribute]
        public string RessourceCaption { get; set; }

        [XmlAttribute]
        public string RessourceIdIcone { get; set; }

        [XmlElement("Menu", typeof(CFMenuLink))]
        [XmlElement("Separateur", typeof(CFMenuSeparateur))]
        public List<CFMenu> SousMenus { get; set; }
    }
}