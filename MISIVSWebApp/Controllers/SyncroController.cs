using MISIVSWebApp.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace MISIVSWebApp.Controllers
{
    public class SyncroController : ApiController
    {
        private DBHelper db = new DBHelper();

        /*public HttpResponseMessage GetActualizacionFicha() {
            IDictionary jsonData = new Dictionary<String,List<Seccion>>();
            List<Seccion> secciones= db.Seccion.Where(s => s.activo == true).ToList();
            jsonData.Add("secciones",secciones);                                                            
            var formatter = new JsonMediaTypeFormatter();
            formatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            return Request.CreateResponse(HttpStatusCode.OK,jsonData,formatter);
        }*/

        public IQueryable<Seccion> GetActualizacionFicha()
        {
            return db.Seccion.Where(s => s.activo == true);
        }

    }
}
