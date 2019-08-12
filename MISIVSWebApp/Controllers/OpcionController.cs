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
    public class OpcionController : Controller
    {
        private DBHelper db = new DBHelper();

        // GET: Opcion
        public ActionResult Index()
        {
            var opcion = db.Opcion.Include(o => o.ItemVariable);
            return View(opcion.ToList());
        }

        // GET: Opcion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Opcion opcion = db.Opcion.Find(id);
            if (opcion == null)
            {
                return HttpNotFound();
            }
            return View(opcion);
        }

        // GET: Opcion/Create
        public ActionResult Create()
        {
            ViewBag.item_opcion = new SelectList(db.ItemVariable, "id", "nombre");
            return View();
        }

        // POST: Opcion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,item_opcion,activo")] Opcion opcion)
        {
            if (ModelState.IsValid)
            {
                db.Opcion.Add(opcion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.item_opcion = new SelectList(db.ItemVariable, "id", "nombre", opcion.item_opcion);
            return View(opcion);
        }

        // GET: Opcion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Opcion opcion = db.Opcion.Find(id);
            if (opcion == null)
            {
                return HttpNotFound();
            }
            ViewBag.item_opcion = new SelectList(db.ItemVariable, "id", "nombre", opcion.item_opcion);
            return View(opcion);
        }

        // POST: Opcion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,item_opcion,activo")] Opcion opcion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(opcion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.item_opcion = new SelectList(db.ItemVariable, "id", "nombre", opcion.item_opcion);
            return View(opcion);
        }

        // GET: Opcion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Opcion opcion = db.Opcion.Find(id);
            if (opcion == null)
            {
                return HttpNotFound();
            }
            return View(opcion);
        }

        // POST: Opcion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Opcion opcion = db.Opcion.Find(id);
            db.Opcion.Remove(opcion);
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
