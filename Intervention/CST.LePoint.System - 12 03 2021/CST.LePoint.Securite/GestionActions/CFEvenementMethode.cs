using System;
using System.Xml.Serialization;

namespace CST.LePoint.Securite.GestionActions
{
    [Serializable]
    public class CFEvenementMethode : CFEvenementAbstrait
    {
        [XmlElement("Nom")]
        public string NomCompletMethode { get; set; }
    }
}