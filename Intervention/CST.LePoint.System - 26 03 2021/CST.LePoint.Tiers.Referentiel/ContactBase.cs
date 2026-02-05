using System.ComponentModel;
using System.Xml.Serialization;

namespace CST.LePoint.Tiers.Referentiel
{
    public class ContactBase
    {
        [XmlAttribute("CContact")]
        [Bindable(true)]
        public int CContact { get; set; }

        [XmlAttribute("BPrincipal")]
        [Bindable(true)]
        public bool BPrincipal { get; set; }

        [XmlAttribute("CCivilite")]
        [Bindable(true)]
        public string CCivilite { get; set; }

        [XmlAttribute("Nom")]
        [Bindable(true)]
        public string Nom { get; set; }

        [XmlAttribute("Prenom")]
        [Bindable(true)]
        public string Prenom { get; set; }

        [XmlAttribute("Fonction")]
        [Bindable(true)]
        public string Fonction { get; set; }

        [XmlAttribute("Telephone")]
        [Bindable(true)]
        public string Telephone { get; set; }

        [XmlAttribute("Portable")]
        [Bindable(true)]
        public string Portable { get; set; }

        [XmlAttribute("Email")]
        [Bindable(true)]
        public string Email { get; set; }

        [XmlAttribute("Interlocuteur")]
        [Bindable(true)]
        public string Interlocuteur { get; set; }
    }
}