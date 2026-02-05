using CST.LePoint.Securite.DataAccess;
using CST.LePoint.Securite.DataAccess.Xml;

namespace CST.LePoint.Securite.Management
{
    public class GestionContexteSecurite
    {
        public static IContexteSecurite ContexteActive = new ContexteSecuriteXml();
    }
}