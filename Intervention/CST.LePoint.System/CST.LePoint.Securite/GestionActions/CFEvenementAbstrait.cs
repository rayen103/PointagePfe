using System;
using System.Xml.Serialization;

namespace CST.LePoint.Securite.GestionActions
{
    [Serializable]
    public abstract class CFEvenementAbstrait
    {
        [XmlAttribute]
        public string IdEvenement { get; set; }
    }
}