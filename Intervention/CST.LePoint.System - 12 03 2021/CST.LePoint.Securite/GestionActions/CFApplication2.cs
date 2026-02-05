using System.IO;
using System.Xml.Serialization;

namespace CST.LePoint.Securite.GestionActions
{
    public class CFApplication2
    {
        public CFEvenementsListe CFEvenements { get; set; }

        public CFMenusListe CFMenus { get; set; }

        public static CFApplication2 Deserialiser(string cfMenus, string cfEvenements)
        {
            CFEvenementsListe CFEvenements;
            CFMenusListe CFMenus;

            //XmlSerializer evenSerializer = new XmlSerializer(typeof(CFEvenementsListe));

            XmlSerializer evenSerializer = XmlSerializer.FromTypes(new[] { typeof(CFEvenementsListe) })[0];

            //var menuSerializer = new XmlSerializer(typeof(CFMenusListe));
            XmlSerializer menuSerializer = XmlSerializer.FromTypes(new[] { typeof(CFMenusListe) })[0];

            using (var reader = new StringReader(cfEvenements))
                CFEvenements = (CFEvenementsListe)evenSerializer.Deserialize(reader);

            using (var reader = new StringReader(cfMenus))
                CFMenus = (CFMenusListe)menuSerializer.Deserialize(reader);

            return new CFApplication2 { CFEvenements = CFEvenements, CFMenus = CFMenus };
        }
    }
}