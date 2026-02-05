using System.ComponentModel;
using System.Xml.Serialization;

namespace CST.LePoint.Tiers.Referentiel
{
    public class AdresseBase
    {
        [XmlAttribute("IdAdresse")]
        [Bindable(true)]
        public int IdAdresse { get; set; }

        [XmlAttribute("LibAdresse")]
        [Bindable(true)]
        public string LibAdresse { get; set; }

        [XmlAttribute("CPostal")]
        [Bindable(true)]
        public string CPostal { get; set; }

        [XmlAttribute("Ville")]
        [Bindable(true)]
        public string Ville { get; set; }

        [XmlAttribute("CPays")]
        [Bindable(true)]
        public string CPays { get; set; }

        [XmlAttribute("BNPAI")]
        [Bindable(true)]
        public bool BNPAI { get; set; }

        [XmlAttribute("SiteWeb")]
        [Bindable(true)]
        public string SiteWeb { get; set; }

        [XmlAttribute("CTypeAdresse")]
        [Bindable(true)]
        public string CTypeAdresse { get; set; }

        [XmlAttribute("AssigneA")]
        [Bindable(true)]
        public int AssigneA { get; set; }
    }
}