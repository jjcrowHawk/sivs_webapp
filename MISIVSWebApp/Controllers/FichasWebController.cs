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
    public class FichasWebController : Controller
    {
        private DBHelper db = new DBHelper();

        // GET: FichasWeb
        public ActionResult Index()
        {
            var ficha = db.Ficha.Include(f => f.Vivienda);
            return View(ficha.ToList());
        }

        // GET: FichasWeb/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ficha ficha = db.Ficha.Find(id);
            if (ficha == null)
            {
                return HttpNotFound();
            }
            return View(ficha);
        }

        // GET: FichasWeb/Create
        public ActionResult Create()
        {
            ViewBag.vivienda_ficha = new SelectList(db.Vivienda, "id", "inspeccion_id");
            return View();
        }

        // POST: FichasWeb/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,inspector,fecha_inspeccion,fecha_sincronizacion,vivienda_ficha,activo")] Ficha ficha)
        {
            if (ModelState.IsValid)
            {
                db.Ficha.Add(ficha);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.vivienda_ficha = new SelectList(db.Vivienda, "id", "inspeccion_id", ficha.vivienda_ficha);
            return View(ficha);
        }

        // GET: FichasWeb/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ficha ficha = db.Ficha.Find(id);
            if (ficha == null)
            {
                return HttpNotFound();
            }
            ViewBag.vivienda_ficha = new SelectList(db.Vivienda, "id", "inspeccion_id", ficha.vivienda_ficha);
            return View(ficha);
        }

        // POST: FichasWeb/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,inspector,fecha_inspeccion,fecha_sincronizacion,vivienda_ficha,activo")] Ficha ficha)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ficha).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.vivienda_ficha = new SelectList(db.Vivienda, "id", "inspeccion_id", ficha.vivienda_ficha);
            return View(ficha);
        }

        // GET: FichasWeb/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ficha ficha = db.Ficha.Find(id);
            if (ficha == null)
            {
                return HttpNotFound();
            }
            return View(ficha);
        }

        // POST: FichasWeb/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ficha ficha = db.Ficha.Find(id);
            db.Ficha.Remove(ficha);
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
