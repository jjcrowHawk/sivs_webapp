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

        // GET: ItemVariables
        public ActionResult Index()
        {
            var itemVariable = db.ItemVariable.Include(i => i.Variable1);
            return View(itemVariable.ToList());
        }

        // GET: ItemVariables/Details/5
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

        // GET: ItemVariables/Create
        public ActionResult Create()
        {
            ViewBag.variable = new SelectList(db.Variable, "id", "nombre");

            List<SelectListItem> lst = new List<SelectListItem>();
            lst.Add(new SelectListItem() { Text = "Texto", Value = "Texto" });
            lst.Add(new SelectListItem() { Text = "Opcion Simple", Value = "OpSimple" });
            lst.Add(new SelectListItem() { Text = "Opcion Multiple", Value = "OpMultiple" });

            ViewBag.tipos = lst;

            return View();
        }

        // POST: ItemVariables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,tipo,variable,activo")] ItemVariable itemVariable)
        {
            if (ModelState.IsValid)
            {
                if (itemVariable.nombre == null || itemVariable.nombre.Equals("")){
                    itemVariable.nombre = db.Variable.Find(itemVariable.variable).nombre;
                }
                db.ItemVariable.Add(itemVariable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.variable = new SelectList(db.Variable, "id", "nombre", itemVariable.variable);
            return View(itemVariable);
        }

        // GET: ItemVariables/Edit/5
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
            ViewBag.variable = new SelectList(db.Variable, "id", "nombre", itemVariable.variable);
            return View(itemVariable);
        }

        // POST: ItemVariables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,tipo,variable,activo")] ItemVariable itemVariable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itemVariable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.variable = new SelectList(db.Variable, "id", "nombre", itemVariable.variable);
            return View(itemVariable);
        }

        // GET: ItemVariables/Delete/5
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

        // POST: ItemVariables/Delete/5
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
