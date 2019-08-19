using System;
using System.Collections.Generic;
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

        public ActionResult Index()
        {


            ViewBag.Sectors = 23;
            ViewBag.Homes = 100;
            ViewBag.Reports = 100;
            ViewBag.Blocks = 60;

            return View();
        }

        public ActionResult ConsultSectors()
        {

            List<int> list = new List<int>();
            list.Add(2);
            list.Add(3);
            list.Add(7);



            db.Configuration.ProxyCreationEnabled = false;
            var booksGrouped = from b in db.Vivienda
                               group b by b.sector into g
                               select new Group<string, Vivienda> { Key = g.Key, Values = g };

            ViewBag.sectores = booksGrouped.ToList();
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
            var fichaList = db.Ficha.Include(f => f.Vivienda).Where(i => i.activo==true);

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

    }
}


