using System;
using System.Collections.Generic;
using System.Resources;
using System.Xml.Serialization;

namespace CST.LePoint.Securite.GestionActions
{
    [Serializable]
    public class CFEvenementForm : CFEvenementAbstrait
    {
        public string NomCompletForm { get; set; }

        [XmlAttribute]
        public string ResTitreForm { get; set; }

        public static Dictionary<string, string> ChargerFormulaires(CFApplication2 cfApplication, ResourceManager rm)
        {
            Dictionary<string, string> formulaires = new Dictionary<string, string>();

            foreach (var ev in cfApplication.CFEvenements.CFEvenements)
            {
                var fev = ev as CFEvenementForm;
                if (fev != null)
                {
                    string screenName = rm.GetString(fev.ResTitreForm);
                    if(string.IsNullOrEmpty(screenName))
                        screenName = fev.IdEvenement;

                    formulaires.Add(fev.NomCompletForm, screenName);
                }
            }
            return formulaires;
        }
    }
}