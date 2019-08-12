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
    public class ItemVariableController : Controller
    {
        private DBHelper db = new DBHelper();

        // GET: ItemVariable
        public ActionResult Index()
        {
            var itemVariable = db.ItemVariable.Include(i => i.Variable);
            return View(itemVariable.ToList());
        }

        // GET: ItemVariable/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemVariable itemVariable = db.ItemVariable.Find(id);
            if (itemVariable == null)
            {
                return HttpNotFound();
            }
            return View(itemVariable);
        }

        // GET: ItemVariable/Create
        public ActionResult Create()
        {
            ViewBag.variable_item = new SelectList(db.Variable, "id", "nombre");
            return View();
        }

        // POST: ItemVariable/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,tipo,variable_item,activo")] ItemVariable itemVariable)
        {
            if (ModelState.IsValid)
            {
                db.ItemVariable.Add(itemVariable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.variable_item = new SelectList(db.Variable, "id", "nombre", itemVariable.variable_item);
            return View(itemVariable);
        }

        // GET: ItemVariable/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemVariable itemVariable = db.ItemVariable.Find(id);
            if (itemVariable == null)
            {
                return HttpNotFound();
            }
            ViewBag.variable_item = new SelectList(db.Variable, "id", "nombre", itemVariable.variable_item);
            return View(itemVariable);
        }

        // POST: ItemVariable/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,tipo,variable_item,activo")] ItemVariable itemVariable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itemVariable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.variable_item = new SelectList(db.Variable, "id", "nombre", itemVariable.variable_item);
            return View(itemVariable);
        }

        // GET: ItemVariable/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemVariable itemVariable = db.ItemVariable.Find(id);
            if (itemVariable == null)
            {
                return HttpNotFound();
            }
            return View(itemVariable);
        }

        // POST: ItemVariable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ItemVariable itemVariable = db.ItemVariable.Find(id);
            db.ItemVariable.Remove(itemVariable);
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
