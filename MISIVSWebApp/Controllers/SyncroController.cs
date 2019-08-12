using MISIVSWebApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace MISIVSWebApp.Controllers
{
    public class SyncroController : ApiController
    {
        private DBHelper db = new DBHelper();

        /*public HttpResponseMessage GetActualizacionFicha() {
            IDictionary jsonData = new Dictionary<String,List<Seccion1>>();
            List<Seccion1> secciones= db.Seccion1.Where(s => s.activo == true).ToList();
            jsonData.Add("secciones",secciones);                                                            
            var formatter = new JsonMediaTypeFormatter();
            formatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            return Request.CreateResponse(HttpStatusCode.OK,jsonData,formatter);
        }*/

        [Route("api/actualizarFichaTelefono")]
        [HttpGet]
        public IQueryable<Seccion> GetActualizacionFicha()
        {
            /* List<Seccion> secciones = db.Seccion.Where(s => s.activo == true).ToList();
             foreach(Seccion s in secciones)
                 Trace.WriteLine("seccion1: " +s.ToString());*/
            return db.Seccion.Where(s => s.activo == true);
        }


        [Route("api/sincronizarFichas")]
        [HttpPost]
        public async Task<HttpResponseMessage> PostSincronizarFichas() {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/App_Data/");
            var provider = new MultipartMemoryStreamProvider();

            try
            {
                Ficha ficha = null;
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);

                //Extraccion de JSON de fichas
                foreach(var content in provider.Contents.Where(x=> !isImage(x))){
                    Trace.WriteLine("==> NOMBRE ARCHIVO: " + content.Headers.ContentDisposition.FileName);
                    Trace.WriteLine("==> MEDIA TYPE: " + content.Headers.ContentType);
                    var data = await content.ReadAsStringAsync();
                    //RespuestaOpcionSimple r = JsonConvert.DeserializeObject<RespuestaOpcionSimple>(data);
                    //db.RespuestaOpcionSimple.Add(r);
                    ficha = JsonConvert.DeserializeObject<Ficha>(data);
                    
                    db.Ficha.Add(ficha);
                    
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (DbEntityValidationException e) {
                        string message = "";

                        foreach (var valEntityError in db.GetValidationErrors())
                        {
                            message += string.Format("{0} ----\n", valEntityError.Entry.Entity.ToString());
                            foreach (var valError in valEntityError.ValidationErrors)
                            {
                                message += string.Format("{0}\n", valError.ErrorMessage);
                            }
                        }

                        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                            new DbEntityValidationException(message));

                    }
                    catch (System.Data.Entity.Infrastructure.DbUpdateException e){
                        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                            e.InnerException);
                    }
                }

                //int i= 0;
                //Extraccion de imagenes de Anexo
                foreach (var file in provider.Contents.Where(x => isImage(x)))
                {
                    Trace.WriteLine("==> NOMBRE ARCHIVO: " + file.Headers.ContentDisposition.FileName);
                    Trace.WriteLine("==> MEDIA TYPE: " + file.Headers.ContentType);
                    Trace.WriteLine("==> NAME: " + file.Headers.ContentDisposition.Name);

                    Anexo anex = ficha.Anexo.Where(a => a.url_anexo.Equals(file.Headers.ContentDisposition.FileName.Trim('\"'))).First();
                    var imageData = await file.ReadAsByteArrayAsync();

                    var ext = Path.GetExtension(file.Headers.ContentDisposition.FileName.Trim('\"'));
                    var fileName= string.Format("{0}_{1}{2}", ficha.Vivienda.inspeccion_id, anex.tipo, ext);

                    if (anex.tipo.Contains("specific") || anex.tipo.Contains("appendix"))
                    {
                        for(int i=1; ; i++)
                        {
                            fileName = string.Format("{0}_{1}{2}{3}", ficha.Vivienda.inspeccion_id, anex.tipo,i, ext);
                            if (!File.Exists(fileName))
                            {
                                break;
                            }
                        }
                    }
                    
                    File.WriteAllBytes(root + fileName, imageData);
                    anex.url_anexo = root + fileName;
                    //i++;
                }

                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }

            //Trace.WriteLine("This:\n"+json);
            //Trace.WriteLine("");

            //return Ok();
        }

        private Boolean isImage(HttpContent content) {
            return !string.IsNullOrEmpty(content.Headers.ContentDisposition.FileName) && content.Headers.ContentType.ToString().Contains("image");
        }
    }
}
