using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Data.Entity;

using System.Web;
using System.Web.Mvc;
using MISIVSWebApp.Models;
using LinqGrouping.Models;
using System.Net;

namespace MISIVSWebApp.Controllers
{
    public class HomeController : Controller
    {
        private DBHelper db = new DBHelper();
        private DBMathModelHelper mathDB = new DBMathModelHelper();

        public ActionResult Index()
        {

            var num_sectores = db.Vivienda.GroupBy((v) => v.sector).Select((group) => new { name = group.Key, count = group.Count() }).ToList().Count();
            var num_fichas = db.Ficha.Where((f) => f.activo == true).ToList().Count();
            var num_casas = db.Vivienda.ToList().Count();
            Trace.WriteLine("query:" + num_sectores);
            ViewBag.Sectors = num_sectores;
            ViewBag.Homes = num_casas;
            ViewBag.Reports = num_fichas;

            return View();
        }

        public ActionResult ConsultSectors()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var sectorsGrouped = from b in db.Vivienda
                               group b by b.sector into g
                               select new Group<string, Vivienda> { Key = g.Key, Values = g };

            ViewBag.sectores = sectorsGrouped.ToList();
            return View();
        }

        public ActionResult ConsultHomes()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ConsultReports()
        {
            ViewBag.Title = "Consult Reports";


            return View();
        }

        public ActionResult GetByInspector()
        {
            /*db.Configuration.ProxyCreationEnabled = false;
            var fichaList = db.Ficha.ToList();
            return Json(new { data = fichaList }, JsonRequestBehavior.AllowGet);*/
            db.Configuration.ProxyCreationEnabled = false;
            var fichaList = db.Ficha.Include(f => f.Vivienda).Where(i => i.activo == true);

            foreach (Ficha fic in fichaList)
            {
                fic.Vivienda.Ficha = null;
            }

            return Json(new { data = fichaList }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ViviendasBySector(String id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db.Configuration.ProxyCreationEnabled = false;
            var viviendasList = db.Vivienda.Where(x => x.sector == id);
            foreach (Vivienda viv in viviendasList)
            {
                viv.Ficha = null;
            }





            return Json(new { data = viviendasList }, JsonRequestBehavior.AllowGet);
        }



        public ActionResult ConsultFeatures()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var FeaturesGrouped = from b in db.ItemVariable
                                  group b by b.nombre into g
                                 select new Group<string, ItemVariable> { Key = g.Key, Values = g };



            return Json(new { data = FeaturesGrouped.ToList() }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ConsultFeatureStats(String sector,String feature)
        {
            if (sector == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (feature == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }



            db.Configuration.ProxyCreationEnabled = false;
            var fichaList = db.Ficha.Include(f => f.Vivienda).Where(i => i.activo == true && i.Vivienda.sector == sector);
            List<Respuesta> list = new List<Respuesta>();
            List<Opcion> listOpcion = new List<Opcion>();
            foreach (Ficha fic in fichaList)
            {
                System.Diagnostics.Debug.WriteLine(fic.Vivienda.inspeccion_id);
                var itemVar = db.ItemVariable.Where(t => t.nombre == feature).First();




                fic.Vivienda.Ficha = null;
                var respuestas = db.Respuesta.Include(i => i.RespuestaOpcionSimple)
                    .Include(f => f.RespuestaOpcionMultiple)
                    .Where(r => r.Ficha.id == fic.id );
                foreach (Respuesta r in respuestas)
                {
                    r.Ficha = null;
                    foreach (RespuestaOpcionSimple resSimple in r.RespuestaOpcionSimple)
                    {
                        resSimple.Respuesta1 = null;
                        var opciones = db.Opcion.Where(o => o.id == resSimple.opcion_respuestasimple && o.item_opcion ==itemVar.id);
                        foreach (Opcion op in opciones)
                        {
                            op.ItemVariable = null;
                            op.RespuestaOpcion = null;
                            op.RespuestaOpcionSimple = null;
                            op.OpcionPuntaje = null;
                            listOpcion.Add(op);
                        }
                        
                    }
                    foreach (RespuestaOpcionMultiple resMult in r.RespuestaOpcionMultiple)
                    {

                        resMult.Respuesta = null;
                        var opciones2 = db.Opcion.Where(o => o.id == resMult.respuesta_opcionmultiple && o.item_opcion == itemVar.id);
                        foreach (Opcion op in opciones2)
                        {
                            op.ItemVariable = null;
                            op.RespuestaOpcion = null;
                            op.RespuestaOpcionSimple = null;
                            op.OpcionPuntaje = null;
                            listOpcion.Add(op);
                        }


                    }

                    

                    list.Add(r);
                }
                
            }
            Dictionary<String, int> dicComparacion = new Dictionary <String,int>();
            
            foreach(Opcion op in listOpcion)
            {
                try
                {
                    dicComparacion.Add(op.nombre, 1);
                }
                catch (ArgumentException)
                {
                    dicComparacion[op.nombre]= dicComparacion[op.nombre] + 1;
                }
            }
            var claves = dicComparacion.Keys.ToList();
            foreach (String clave in claves)
            {
                dicComparacion[clave] = (dicComparacion[clave] * 100) / listOpcion.Count();
            }


                return Json(new { data = dicComparacion }, JsonRequestBehavior.AllowGet);
        }















        public ActionResult GetViviendaVulnerabilityScore(int id) {
            var vivienda= db.Vivienda.Single(v => v.id.Equals(id));
            var ficha = vivienda.Ficha.First();
            if (ficha != null) {
                var respuestas= ficha.Respuesta.Where(r => r.RespuestaOpcionMultiple.Count!= 0 || r.RespuestaOpcionSimple.Count !=0);
                var parametros = mathDB.Parametro;

                foreach (Respuesta r in respuestas) {
                    Trace.WriteLine("Respuesta: " +r.id + " of item: "+ r.ItemVariable.nombre);
                }
            }
            return Json(new { }, JsonRequestBehavior.AllowGet);
        }

    }
}


