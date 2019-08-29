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
using Newtonsoft.Json;

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
            db.Configuration.ProxyCreationEnabled = false;
            var sectorsGrouped = from b in db.Vivienda
                                 group b by b.sector into g
                                 select new Group<string, Vivienda> { Key = g.Key, Values = g };
            //ViewBag.sectores = sectorsGrouped.ToList();
            Dictionary<String, List<Vivienda>> viviendas_sector_dic = new Dictionary<string, List<Vivienda>>();
            foreach(var item in sectorsGrouped)
            {
                List<Vivienda> viviendas= db.Vivienda.Where(v => v.sector == item.Key).ToList();
                viviendas_sector_dic.Add(item.Key,viviendas);
            }

            ViewBag.sectores = viviendas_sector_dic;
            //ViewBag.Message = "Your contact page.";

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
            int puntaje_acumulado = 0;
            float puntaje_relativo = 0.00f;
            if (ficha != null) {
                var respuestas= ficha.Respuesta.Where(r => r.RespuestaOpcionMultiple.Count!= 0 || r.RespuestaOpcionSimple.Count !=0);
                var parametros = mathDB.Parametro.Include(p => p.Clasificacion) ;
                foreach (Parametro parametro in parametros) {
                    foreach(Clasificacion clasificacion in parametro.Clasificacion)
                    {
                        int puntaje_respuesta = 0;
                        float puntaje_respuesta_relativo = 0.00f;

                        IEnumerable<ItemVariable> itemsClasificacion = clasificacion.ItemClasificacion.Select(ic => ic.ItemVariable);
                        Trace.WriteLine("===> Items on this clasification " + clasificacion.nombre + " : " + itemsClasificacion.Count());
                        IEnumerable<Respuesta> respuestasClasificacion = respuestas.Where(r => itemsClasificacion.Contains(r.ItemVariable));
                        //Trace.WriteLine("---> Responses on this clasification: " + respuestasClasificacion.Count());

                        foreach (Respuesta r in respuestasClasificacion)
                        {
                            Trace.WriteLine("*** respuesta id: "+ r.id +" with item: " + r.ItemVariable.nombre + "for Class:  " + clasificacion.nombre+ 
                                " con respuestas simples: "+ r.RespuestaOpcionSimple.Count() + " y respuesta multiple: " +r.RespuestaOpcionMultiple.Count());
                        }
                        

                        //Comparamos cada puntaje con las respuestas para ver si tienen las mismas opciones
                        IEnumerable<PuntajeClasificacion> puntajesPosibles= clasificacion.PuntajeClasificacion;
                        foreach(PuntajeClasificacion puntaje in puntajesPosibles)
                        {

                            IEnumerable<Opcion> opcionesPuntaje= puntaje.OpcionesPuntaje.Select(op => op.Opcion);
                            List<Opcion> opcionesRespuesta = new List<Opcion>();
                            foreach (Respuesta r in respuestasClasificacion) {
                                if (r.RespuestaOpcionSimple.Count() != 0)
                                {
                                    Trace.WriteLine(" @@@ Respuesta simple con opcion: " + r.RespuestaOpcionSimple.First().Opcion.nombre);
                                    opcionesRespuesta.Add(r.RespuestaOpcionSimple.First().Opcion);
                                }
                                else if (r.RespuestaOpcionMultiple.Count() != 0) {
                                    opcionesRespuesta.AddRange(r.RespuestaOpcionMultiple.First().RespuestaOpcion.Select(ro => ro.Opcion));
                                }
                            }

                            Trace.WriteLine("--->Puntaje to test: "+ puntaje.nombre);
                            Trace.WriteLine("---> Puntaje options: " + opcionesPuntaje.Count());
                            Trace.WriteLine("---> Respuesta options: " + opcionesRespuesta.Count());

                            bool opcionesIguales = false;
                            
                            if (opcionesPuntaje.Count() < opcionesRespuesta.Count())
                            {
                                //Enumerable.Intersect(opcionesPuntaje.OrderBy(t => t.id), opcionesRespuesta.OrderBy(t => t.id));
                                opcionesIguales = Enumerable.Intersect(opcionesPuntaje.OrderBy(t => t.id), opcionesRespuesta.OrderBy(t => t.id)).Count() == opcionesPuntaje.Count();//opcionesRespuesta.Contains(opcionesPuntaje.First());
                            }
                            else {
                                opcionesIguales = Enumerable.SequenceEqual(opcionesPuntaje.OrderBy(t => t.id), opcionesRespuesta.OrderBy(t => t.id));
                            }

                            if (opcionesIguales) {
                                Trace.WriteLine(">>>>>>>>>>>> Respuesta coincide con Puntaje: " + puntaje.nombre);

                                if (puntaje.penalizacion.Equals("ninguna")) {
                                    puntaje_respuesta = puntaje.puntaje;
                                }
                                else if (puntaje.penalizacion.Equals("positiva"))
                                {
                                    puntaje_respuesta += puntaje.puntaje;
                                }
                                else if (puntaje.penalizacion.Equals("negativa"))
                                {
                                    puntaje_respuesta -= puntaje.puntaje;
                                }
                            }
                        }

                        float factor_multiplicador = (float)puntaje_respuesta / (float)clasificacion.valor_maximo;
                        puntaje_respuesta_relativo = (float)clasificacion.peso_relativo * factor_multiplicador;

                        puntaje_acumulado += puntaje_respuesta;
                        puntaje_relativo += puntaje_respuesta_relativo;
                        

                        Trace.WriteLine("Puntaje obtenido de esta clasificacion: "+ puntaje_respuesta + " , % "+puntaje_respuesta_relativo);
                        Trace.WriteLine("=======================================");
                    }
                }

            }
            return Json(new { puntaje= puntaje_acumulado, puntaje_porcentaje= Math.Round(puntaje_relativo,3) }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSectorVulnerabilityScore(String id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trace.WriteLine("===> Sector escogido: " + id);
            var viviendas = db.Vivienda.Where(v => v.sector.Equals(id));
            int puntaje_promedio = 0;
            float porcentaje_promedio = 0.00f;
            foreach (Vivienda vivienda in viviendas)
            {
                var ficha = vivienda.Ficha.First();
                int puntaje_acumulado = 0;
                float puntaje_relativo = 0.00f;
                if (ficha != null)
                {
                    var respuestas = ficha.Respuesta.Where(r => r.RespuestaOpcionMultiple.Count != 0 || r.RespuestaOpcionSimple.Count != 0);
                    var parametros = mathDB.Parametro.Include(p => p.Clasificacion);
                    foreach (Parametro parametro in parametros)
                    {
                        foreach (Clasificacion clasificacion in parametro.Clasificacion)
                        {
                            int puntaje_respuesta = 0;
                            float puntaje_respuesta_relativo = 0.00f;

                            IEnumerable<ItemVariable> itemsClasificacion = clasificacion.ItemClasificacion.Select(ic => ic.ItemVariable);
                            Trace.WriteLine("===> Items on this clasification " + clasificacion.nombre + " : " + itemsClasificacion.Count());
                            IEnumerable<Respuesta> respuestasClasificacion = respuestas.Where(r => itemsClasificacion.Contains(r.ItemVariable));
                            //Trace.WriteLine("---> Responses on this clasification: " + respuestasClasificacion.Count());

                            foreach (Respuesta r in respuestasClasificacion)
                            {
                                Trace.WriteLine("*** respuesta id: " + r.id + " with item: " + r.ItemVariable.nombre + "for Class:  " + clasificacion.nombre +
                                    " con respuestas simples: " + r.RespuestaOpcionSimple.Count() + " y respuesta multiple: " + r.RespuestaOpcionMultiple.Count());
                            }


                            //Comparamos cada puntaje con las respuestas para ver si tienen las mismas opciones
                            IEnumerable<PuntajeClasificacion> puntajesPosibles = clasificacion.PuntajeClasificacion;
                            foreach (PuntajeClasificacion puntaje in puntajesPosibles)
                            {

                                IEnumerable<Opcion> opcionesPuntaje = puntaje.OpcionesPuntaje.Select(op => op.Opcion);
                                List<Opcion> opcionesRespuesta = new List<Opcion>();
                                foreach (Respuesta r in respuestasClasificacion)
                                {
                                    if (r.RespuestaOpcionSimple.Count() != 0)
                                    {
                                        Trace.WriteLine(" @@@ Respuesta simple con opcion: " + r.RespuestaOpcionSimple.First().Opcion.nombre);
                                        opcionesRespuesta.Add(r.RespuestaOpcionSimple.First().Opcion);
                                    }
                                    else if (r.RespuestaOpcionMultiple.Count() != 0)
                                    {
                                        opcionesRespuesta.AddRange(r.RespuestaOpcionMultiple.First().RespuestaOpcion.Select(ro => ro.Opcion));
                                    }
                                }

                                Trace.WriteLine("--->Puntaje to test: " + puntaje.nombre);
                                Trace.WriteLine("---> Puntaje options: " + opcionesPuntaje.Count());
                                Trace.WriteLine("---> Respuesta options: " + opcionesRespuesta.Count());

                                bool opcionesIguales = false;

                                if (opcionesPuntaje.Count() < opcionesRespuesta.Count())
                                {
                                    //Enumerable.Intersect(opcionesPuntaje.OrderBy(t => t.id), opcionesRespuesta.OrderBy(t => t.id));
                                    opcionesIguales = Enumerable.Intersect(opcionesPuntaje.OrderBy(t => t.id), opcionesRespuesta.OrderBy(t => t.id)).Count() == opcionesPuntaje.Count();//opcionesRespuesta.Contains(opcionesPuntaje.First());
                                }
                                else
                                {
                                    opcionesIguales = Enumerable.SequenceEqual(opcionesPuntaje.OrderBy(t => t.id), opcionesRespuesta.OrderBy(t => t.id));
                                }

                                if (opcionesIguales)
                                {
                                    Trace.WriteLine(">>>>>>>>>>>> Respuesta coincide con Puntaje: " + puntaje.nombre);

                                    if (puntaje.penalizacion.Equals("ninguna"))
                                    {
                                        puntaje_respuesta = puntaje.puntaje;
                                    }
                                    else if (puntaje.penalizacion.Equals("positiva"))
                                    {
                                        puntaje_respuesta += puntaje.puntaje;
                                    }
                                    else if (puntaje.penalizacion.Equals("negativa"))
                                    {
                                        puntaje_respuesta -= puntaje.puntaje;
                                    }
                                }
                            }

                            float factor_multiplicador = (float)puntaje_respuesta / (float)clasificacion.valor_maximo;
                            puntaje_respuesta_relativo = (float)clasificacion.peso_relativo * factor_multiplicador;

                            puntaje_acumulado += puntaje_respuesta;
                            puntaje_relativo += puntaje_respuesta_relativo;


                            Trace.WriteLine("Puntaje obtenido de esta clasificacion: " + puntaje_respuesta + " , % " + puntaje_respuesta_relativo);
                            Trace.WriteLine("=======================================");
                        }
                    }

                }
                puntaje_promedio += puntaje_acumulado;
                porcentaje_promedio += puntaje_relativo;
            }
            if (viviendas.Count() != 0)
            {
                puntaje_promedio = puntaje_promedio / viviendas.Count();
                porcentaje_promedio = porcentaje_promedio / viviendas.Count();
            }
            return Json(new { puntaje = puntaje_promedio, puntaje_porcentaje = Math.Round(porcentaje_promedio, 3) }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetFichaInformation(int id)
        {
            Ficha ficha= db.Ficha.Single(f => f.vivienda_ficha == id);
            ficha.Vivienda = null;
            ficha.EvaluacionFicha = null;
            //ficha.Anexo = null;
            //ficha.Respuesta = null;
            ficha.SeccionFicha = null;
            foreach (Anexo a in ficha.Anexo)
                a.Ficha = null;

            /*var json = JsonConvert.SerializeObject(ficha,Formatting.None,
                new JsonSerializerSettings(){ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore});*/
            return Json(new {ficha.id,ficha.fecha_inspeccion,ficha.inspector,ficha.Anexo}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetRespuestasFicha(int id) {
            var respuestas= db.Respuesta.Where(r => r.ficha_respuesta == id);
            Dictionary<String,Dictionary<String,String>> json_dic = new Dictionary<String,Dictionary<String,String>>();
            IEnumerable<Seccion> secciones = respuestas.Select(r => r.ItemVariable.Variable.Seccion).Distinct();
            IEnumerable<ItemVariable> items = respuestas.Select(r => r.ItemVariable);


            foreach (Seccion sec in secciones.OrderBy(s => s.id)) {
                Dictionary<String, String> dict_items = new Dictionary<String, String>();
                IEnumerable<ItemVariable> items_seccion = items.Where(it => it.Variable.seccion_variable == sec.id);

                foreach(ItemVariable i in items_seccion)
                {
                    Trace.WriteLine("--------========================> " + i.nombre);
                }


                    foreach (ItemVariable it in items_seccion) {
                    Trace.WriteLine("========================> "+it.nombre);
                    Respuesta res = respuestas.Single(r => r.item_respuesta == it.id);
                    if (it.tipo == "Texto")
                    {
                        if(!dict_items.ContainsKey(it.nombre))
                            dict_items.Add(it.nombre, res.RespuestaTexto.First().texto);
                        else
                            dict_items.Add(it.nombre + " 2 ", res.RespuestaTexto.First().texto);
                    }
                    else if (it.tipo == "OpSimple")
                    {
                        if(!dict_items.ContainsKey(it.nombre))
                            dict_items.Add(it.nombre, res.RespuestaOpcionSimple.First().Opcion.nombre);
                        else
                            dict_items.Add(it.nombre + " 2 ", res.RespuestaOpcionSimple.First().Opcion.nombre);
                    }
                    else if (it.tipo == "OpMultiple")
                    {
                        var opcionesMulti= res.RespuestaOpcionMultiple.First().RespuestaOpcion.Select(ro => ro.Opcion.nombre);
                        if (opcionesMulti.Count() == 0)
                            continue;
                        String opciones = String.Join(", ", opcionesMulti);
                        if (!dict_items.ContainsKey(it.nombre))
                        {                         
                            dict_items.Add(it.nombre, opciones);
                        }
                        else
                        {
                            dict_items.Add(it.nombre + " 2 ", opciones);
                        }
                    }
                }
                if(!json_dic.ContainsKey(sec.nombre))
                    json_dic.Add(sec.nombre,dict_items);
            }
            /*var agrupacion= respuestas.OrderBy(r => r.ItemVariable.id).GroupBy(r => r.ItemVariable).GroupBy(g => g.Key.Variable).GroupBy(g => g.Key.Seccion);
            agrupacion*/
            
            //respuestas.OrderBy(r => r.ItemVariable.Variable.Seccion.id);
            //Dictionary<String,Dictionary<String,>>

            return Json(json_dic, JsonRequestBehavior.AllowGet);
        }
    }

}


