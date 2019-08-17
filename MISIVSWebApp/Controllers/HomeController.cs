using System;
using System.Collections.Generic;
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


            ViewBag.Sectors = 23;
            ViewBag.Homes = 100;
            ViewBag.Reports = 100;
            ViewBag.Blocks = 60;

            return View();
        }

        public ActionResult ConsultSectors()
        {
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