using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CST.LePoint.Securite.GestionActions
{
    [Serializable]
    [XmlRoot("Menus")]
    public class CFMenusListe
    {
        private List<CFMenu> cfMenus = new List<CFMenu>();

        [XmlElement("Menu", typeof(CFMenuLink))]
        [XmlElement("Separateur", typeof(CFMenuSeparateur))]
        public List<CFMenu> CFMenus
        {
            get { return cfMenus; }
            set { cfMenus = value; }
        }
    }
}