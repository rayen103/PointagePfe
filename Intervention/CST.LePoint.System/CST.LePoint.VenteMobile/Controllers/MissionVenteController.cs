using CST.LePoint.VenteMobile.Metier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApp.Util;
namespace WebApp.Controllers
{
    public class MissionVenteController : ApiController
    {

        [Route("pea")]
        [HttpGet]
        public MyBody<Object, Object> planifie(string id, string dd, string df, string cr, string cg)
        {
            MyBody<Object, Object> response = new MyBody<object, object>();

            try
            {
                response.result1 = MobileOrdreCollection.NombreManquer(id);
                response.results = MobileOrdreCollection.planifieCharger(id, dd, df, cr, cg);
                response.message = "success";
                response.Status = "OK";

            }
            catch (Exception ex)
            {
                response.Status = "ERROR".ToUpper();
                response.message = ex.Message;
            }
            return response;
        }

    }
}