using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MISIVSWebApp.Models;

namespace MISIVSWebApp.Controllers
{
    public class SeccionController : ApiController
    {
        private DBHelper db = new DBHelper();

        // GET: api/Seccion
        public IQueryable<Seccion> GetSeccion()
        {
            return db.Seccion;
        }

        // GET: api/Seccion/5
        [ResponseType(typeof(Seccion))]
        public IHttpActionResult GetSeccion(int id)
        {
            Seccion seccion = db.Seccion.Find(id);
            if (seccion == null)
            {
                return NotFound();
            }

            return Ok(seccion);
        }

        // PUT: api/Seccion/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSeccion(int id, Seccion seccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != seccion.id)
            {
                return BadRequest();
            }

            db.Entry(seccion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeccionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Seccion
        [ResponseType(typeof(Seccion))]
        public IHttpActionResult PostSeccion(Seccion seccion)
        {
            System.Diagnostics.Debug.WriteLine("Este es enviado como :" + seccion.activo);
            

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Seccion.Add(seccion);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = seccion.id }, seccion);
        }

        // DELETE: api/Seccion/5
        [ResponseType(typeof(Seccion))]
        public IHttpActionResult DeleteSeccion(int id)
        {
            Seccion seccion = db.Seccion.Find(id);
            if (seccion == null)
            {
                return NotFound();
            }

            db.Seccion.Remove(seccion);
            db.SaveChanges();

            return Ok(seccion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SeccionExists(int id)
        {
            return db.Seccion.Count(e => e.id == id) > 0;
        }
    }
}