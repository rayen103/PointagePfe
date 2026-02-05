using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace CST.LePoint.VenteMobile.Metier
{
    public class PosteNote
    {
             #region Proprietes
        [XmlAttribute("codeArt")]
        [Bindable(true)]
        public string codeArt { get; set; }

        [XmlAttribute("libArt")]
        [Bindable(true)]
        public string libArt { get; set; }

        [XmlAttribute("CnotePresentoire")]
        [Bindable(true)]
        public string CnotePresentoire { get; set; }
    
       #endregion Proprietes

        public PosteNote()
        {

        }

    }
}