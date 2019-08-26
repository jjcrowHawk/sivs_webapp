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
    public class PuntajeClasificacionController : Controller
    {
        private DBMathModelHelper db = new DBMathModelHelper();

        // GET: PuntajeClasificacion
        public ActionResult Index()
        {
            var puntajeClasificacion = db.PuntajeClasificacion.Include(p => p.Clasificacion);
            return View(puntajeClasificacion.ToList());
        }

        // GET: PuntajeClasificacion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PuntajeClasificacion puntajeClasificacion = db.PuntajeClasificacion.Find(id);
            if (puntajeClasificacion == null)
            {
                return HttpNotFound();
            }
            return View(puntajeClasificacion);
        }

        // GET: PuntajeClasificacion/Create
        public ActionResult Create()
        {
            ViewBag.clasificacion_puntaje = new SelectList(db.Clasificacion, "id", "nombre");
            return View();
        }

        // POST: PuntajeClasificacion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,puntaje,penalizacion,clasificacion_puntaje")] PuntajeClasificacion puntajeClasificacion)
        {
            if (ModelState.IsValid)
            {
                db.PuntajeClasificacion.Add(puntajeClasificacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.clasificacion_puntaje = new SelectList(db.Clasificacion, "id", "nombre", puntajeClasificacion.clasificacion_puntaje);
            return View(puntajeClasificacion);
        }

        // GET: PuntajeClasificacion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PuntajeClasificacion puntajeClasificacion = db.PuntajeClasificacion.Find(id);
            if (puntajeClasificacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.clasificacion_puntaje = new SelectList(db.Clasificacion, "id", "nombre", puntajeClasificacion.clasificacion_puntaje);
            return View(puntajeClasificacion);
        }

        // POST: PuntajeClasificacion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,puntaje,penalizacion,clasificacion_puntaje")] PuntajeClasificacion puntajeClasificacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(puntajeClasificacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.clasificacion_puntaje = new SelectList(db.Clasificacion, "id", "nombre", puntajeClasificacion.clasificacion_puntaje);
            return View(puntajeClasificacion);
        }

        // GET: PuntajeClasificacion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PuntajeClasificacion puntajeClasificacion = db.PuntajeClasificacion.Find(id);
            if (puntajeClasificacion == null)
            {
                return HttpNotFound();
            }
            return View(puntajeClasificacion);
        }

        // POST: PuntajeClasificacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PuntajeClasificacion puntajeClasificacion = db.PuntajeClasificacion.Find(id);
            db.PuntajeClasificacion.Remove(puntajeClasificacion);
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
