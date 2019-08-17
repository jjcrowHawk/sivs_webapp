using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MISIVSWebApp.Models;

namespace MISIVSWebApp.Controllers
{
    public class HomeController : Controller
    {
        private DBHelper db = new DBHelper();

        public ActionResult Index()
        {
          
            var num_sectores= db.Vivienda.GroupBy((v) => v.sector).Select((group) => new { name= group.Key, count= group.Count()}).ToList().Count();
            var num_fichas = db.Ficha.Where((f) => f.activo == true).ToList().Count();
            var num_casas = db.Vivienda.ToList().Count();
            Trace.WriteLine("query:"+num_sectores);
            ViewBag.Sectors = num_sectores;
            ViewBag.Homes = num_casas;
            ViewBag.Reports = num_fichas;
            ViewBag.Blocks ="No seas sapo, no preguntes";

            return View();
        }

        public ActionResult ConsultSectors()
        {
            ViewBag.sectores = db.Vivienda.GroupBy((v) => v.sector)
                .Select((group) => new { name = group.Key, count = group.Count() })
                .ToDictionary(reg => reg.name, reg => reg.count).Keys.ToList();
            //Trace.WriteLine("sectores: " + ViewBag.sectores[0] +"\n" + ViewBag.sectores[1]);
            ViewBag.Message = "ConsultSectors";

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

            db.Configuration.ProxyCreationEnabled = false;
            var fichaList = db.Ficha.ToList();
            return Json(new { data = fichaList }, JsonRequestBehavior.AllowGet);
        }


    }
}