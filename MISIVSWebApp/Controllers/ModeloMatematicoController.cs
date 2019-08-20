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
    public class ModeloMatematicoController : Controller
    {
        private DBMathModelHelper db = new DBMathModelHelper();

        // GET: ModeloMatematico
        public ActionResult Index()
        {
            return View(db.ModeloMatematico.ToList());
        }

        // GET: ModeloMatematico/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModeloMatematico modeloMatematico = db.ModeloMatematico.Find(id);
            if (modeloMatematico == null)
            {
                return HttpNotFound();
            }
            return View(modeloMatematico);
        }

        // GET: ModeloMatematico/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ModeloMatematico/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,valor_min,valor_max")] ModeloMatematico modeloMatematico)
        {
            if (ModelState.IsValid)
            {
                db.ModeloMatematico.Add(modeloMatematico);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(modeloMatematico);
        }

        // GET: ModeloMatematico/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModeloMatematico modeloMatematico = db.ModeloMatematico.Find(id);
            if (modeloMatematico == null)
            {
                return HttpNotFound();
            }
            return View(modeloMatematico);
        }

        // POST: ModeloMatematico/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,valor_min,valor_max")] ModeloMatematico modeloMatematico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(modeloMatematico).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(modeloMatematico);
        }

        // GET: ModeloMatematico/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModeloMatematico modeloMatematico = db.ModeloMatematico.Find(id);
            if (modeloMatematico == null)
            {
                return HttpNotFound();
            }
            return View(modeloMatematico);
        }

        // POST: ModeloMatematico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ModeloMatematico modeloMatematico = db.ModeloMatematico.Find(id);
            db.ModeloMatematico.Remove(modeloMatematico);
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
