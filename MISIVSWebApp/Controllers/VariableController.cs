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
    public class VariableController : Controller
    {
        private DBHelper db = new DBHelper();

        // GET: Variable
        public ActionResult Index()
        {
            var variable = db.Variable.Include(v => v.Seccion);
            return View(variable.ToList());
        }

        // GET: Variable/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Variable variable = db.Variable.Find(id);
            if (variable == null)
            {
                return HttpNotFound();
            }
            return View(variable);
        }

        // GET: Variable/Create
        public ActionResult Create()
        {
            ViewBag.seccion = new SelectList(db.Seccion, "id", "nombre");
            return View();
        }

        // POST: Variable/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,obligatoria,seccion,activo")] Variable variable)
        {
            if (ModelState.IsValid)
            {
                db.Variable.Add(variable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.seccion = new SelectList(db.Seccion, "id", "nombre", variable.seccion);
            return View(variable);
        }

        // GET: Variable/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Variable variable = db.Variable.Find(id);
            if (variable == null)
            {
                return HttpNotFound();
            }
            ViewBag.seccion = new SelectList(db.Seccion, "id", "nombre", variable.seccion);
            return View(variable);
        }

        // POST: Variable/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,obligatoria,seccion,activo")] Variable variable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(variable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.seccion = new SelectList(db.Seccion, "id", "nombre", variable.seccion);
            return View(variable);
        }

        // GET: Variable/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Variable variable = db.Variable.Find(id);
            if (variable == null)
            {
                return HttpNotFound();
            }
            return View(variable);
        }

        // POST: Variable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Variable variable = db.Variable.Find(id);
            db.Variable.Remove(variable);
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
