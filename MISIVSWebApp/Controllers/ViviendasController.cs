using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MISIVSWebApp.Models;

namespace MISIVSWebApp.Content
{
    public class ViviendasController : Controller
    {
        private DBHelper db = new DBHelper();

        // GET: Viviendas
        public ActionResult Index()
        {
            return View(db.Vivienda.ToList());
        }

        // GET: Viviendas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vivienda vivienda = db.Vivienda.Find(id);
            if (vivienda == null)
            {
                return HttpNotFound();
            }
            return View(vivienda);
        }

        // GET: Viviendas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Viviendas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,inspeccion_id,edad_construcción,elevacion,sector,direccion,ubicacion")] Vivienda vivienda)
        {
            if (ModelState.IsValid)
            {
                db.Vivienda.Add(vivienda);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vivienda);
        }

        // GET: Viviendas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vivienda vivienda = db.Vivienda.Find(id);
            if (vivienda == null)
            {
                return HttpNotFound();
            }
            return View(vivienda);
        }

        // POST: Viviendas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,inspeccion_id,edad_construcción,elevacion,sector,direccion,ubicacion")] Vivienda vivienda)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vivienda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vivienda);
        }

        // GET: Viviendas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vivienda vivienda = db.Vivienda.Find(id);
            if (vivienda == null)
            {
                return HttpNotFound();
            }
            return View(vivienda);
        }

        // POST: Viviendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vivienda vivienda = db.Vivienda.Find(id);
            db.Vivienda.Remove(vivienda);
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
