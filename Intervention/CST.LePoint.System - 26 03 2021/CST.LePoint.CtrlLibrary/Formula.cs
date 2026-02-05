using System.ComponentModel;
using System.Xml.Serialization;

namespace CST.LePoint.CtrlLibrary
{
    public partial class Formula
    {
        #region Proprietes

        [XmlAttribute("Title")]
        [Bindable(true)]
        public string Title { get; set; }

        [XmlAttribute("User")]
        [Bindable(true)]
        public string User { get; set; }

        [XmlAttribute("Societe")]
        [Bindable(true)]
        public string Societe { get; set; }

        [XmlAttribute("Article")]
        [Bindable(true)]
        public string CArticle { get; set; }

        [XmlAttribute("Entrepot")]
        [Bindable(true)]
        public string Entrepot { get; set; }

        [XmlAttribute("CCategorie")]
        [Bindable(true)]
        public string CCategorie { get; set; }

        [XmlAttribute("CFamille")]
        [Bindable(true)]
        public string CFamille { get; set; }

        [XmlAttribute("CType")]
        [Bindable(true)]
        public string CType { get; set; }

        [XmlAttribute("CNature")]
        [Bindable(true)]
        public string CNature { get; set; }

        [XmlAttribute("CTarif")]
        [Bindable(true)]
        public string CTarif { get; set; }

        [XmlAttribute("CModele")]
        [Bindable(true)]
        public string CModele { get; set; }

        [XmlAttribute("CModele1")]
        [Bindable(true)]
        public string CModele1 { get; set; }

        [XmlAttribute("CSousModele1")]
        [Bindable(true)]
        public string CSousModele1 { get; set; }

        [XmlAttribute("CSousModele2")]
        [Bindable(true)]
        public string CSousModele2 { get; set; }

        [XmlAttribute("CRegion")]
        [Bindable(true)]
        public int CRegion { get; set; }

        [XmlAttribute("DateDeb")]
        [Bindable(true)]
        public string DateDeb { get; set; }

        [XmlAttribute("DateFin")]
        [Bindable(true)]
        public string DateFin { get; set; }

        #endregion Proprietes
    }
}