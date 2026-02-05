using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CST.LePoint.Securite.GestionActions
{
    [Serializable]
    [XmlRoot("Evenements")]
    public class CFEvenementsListe
    {
        private List<CFEvenementAbstrait> cfEvenements = new List<CFEvenementAbstrait>();

        //[XmlArrayItem(ElementName = "Methode", Type = typeof(CFEvenementDelegate))]
        //[XmlArrayItem(ElementName = "Form", Type = typeof(CFEvenementForm))]
        //[XmlArray("Evènement")]
        [XmlElement(Type = typeof(CFEvenementMethode), ElementName = "Methode")]
        [XmlElement(Type = typeof(CFEvenementForm), ElementName = "Form")]
        public List<CFEvenementAbstrait> CFEvenements
        {
            get { return cfEvenements; }
            set { cfEvenements = value; }
        }
    }
}