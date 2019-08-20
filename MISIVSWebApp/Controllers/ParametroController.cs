using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MISIVSWebApp.Models;

namespace MISIVSWebApp.Controllers
{
    public class ParametroController : Controller
    {
        private DBMathModelHelper db = new DBMathModelHelper();

        // GET: Parametro
        public ActionResult Index()
        {
            return View(db.Parametro.ToList());
        }

        // GET: Parametro/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parametro parametro = db.Parametro.Find(id);
            if (parametro == null)
            {
                return HttpNotFound();
            }
            return View(parametro);
        }

        // GET: Parametro/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Parametro/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre")] Parametro parametro)
        {
            if (ModelState.IsValid)
            {
                db.Parametro.Add(parametro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(parametro);
        }

        // GET: Parametro/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parametro parametro = db.Parametro.Find(id);
            if (parametro == null)
            {
                return HttpNotFound();
            }
            return View(parametro);
        }

        // POST: Parametro/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre")] Parametro parametro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(parametro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(parametro);
        }

        // GET: Parametro/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parametro parametro = db.Parametro.Find(id);
            if (parametro == null)
            {
                return HttpNotFound();
            }
            return View(parametro);
        }

        // POST: Parametro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Parametro parametro = db.Parametro.Find(id);
            db.Parametro.Remove(parametro);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
