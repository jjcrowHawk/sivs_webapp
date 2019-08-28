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
    public class RespuestaOpcionSimplesController : Controller
    {
        private DBHelper db = new DBHelper();

        // GET: RespuestaOpcionSimples
        public ActionResult Index()
        {
            var respuestaOpcionSimple = db.RespuestaOpcionSimple.Include(r => r.Opcion).Include(r => r.Respuesta1);
            return View(respuestaOpcionSimple.ToList());
        }

        // GET: RespuestaOpcionSimples/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RespuestaOpcionSimple respuestaOpcionSimple = db.RespuestaOpcionSimple.Find(id);
            if (respuestaOpcionSimple == null)
            {
                return HttpNotFound();
            }
            return View(respuestaOpcionSimple);
        }

        // GET: RespuestaOpcionSimples/Create
        public ActionResult Create()
        {
            ViewBag.opcion_respuestasimple = new SelectList(db.Opcion, "id", "nombre");
            ViewBag.respuesta = new SelectList(db.Respuesta, "id", "nota");
            return View();
        }

        // POST: RespuestaOpcionSimples/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,respuesta,opcion_respuestasimple")] RespuestaOpcionSimple respuestaOpcionSimple)
        {
            if (ModelState.IsValid)
            {
                db.RespuestaOpcionSimple.Add(respuestaOpcionSimple);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.opcion_respuestasimple = new SelectList(db.Opcion, "id", "nombre", respuestaOpcionSimple.opcion_respuestasimple);
            ViewBag.respuesta = new SelectList(db.Respuesta, "id", "nota", respuestaOpcionSimple.respuesta);
            return View(respuestaOpcionSimple);
        }

        // GET: RespuestaOpcionSimples/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RespuestaOpcionSimple respuestaOpcionSimple = db.RespuestaOpcionSimple.Find(id);
            if (respuestaOpcionSimple == null)
            {
                return HttpNotFound();
            }
            ViewBag.opcion_respuestasimple = new SelectList(db.Opcion, "id", "nombre", respuestaOpcionSimple.opcion_respuestasimple);
            ViewBag.respuesta = new SelectList(db.Respuesta, "id", "nota", respuestaOpcionSimple.respuesta);
            return View(respuestaOpcionSimple);
        }

        // POST: RespuestaOpcionSimples/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,respuesta,opcion_respuestasimple")] RespuestaOpcionSimple respuestaOpcionSimple)
        {
            if (ModelState.IsValid)
            {
                db.Entry(respuestaOpcionSimple).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.opcion_respuestasimple = new SelectList(db.Opcion, "id", "nombre", respuestaOpcionSimple.opcion_respuestasimple);
            ViewBag.respuesta = new SelectList(db.Respuesta, "id", "nota", respuestaOpcionSimple.respuesta);
            return View(respuestaOpcionSimple);
        }

        // GET: RespuestaOpcionSimples/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RespuestaOpcionSimple respuestaOpcionSimple = db.RespuestaOpcionSimple.Find(id);
            if (respuestaOpcionSimple == null)
            {
                return HttpNotFound();
            }
            return View(respuestaOpcionSimple);
        }

        // POST: RespuestaOpcionSimples/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RespuestaOpcionSimple respuestaOpcionSimple = db.RespuestaOpcionSimple.Find(id);
            db.RespuestaOpcionSimple.Remove(respuestaOpcionSimple);
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
