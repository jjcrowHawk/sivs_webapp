using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using MISIVSWebApp.Models;

namespace MISIVSWebApp.Controllers
{
    public class FichasController : ApiController
    {
        private DBHelper db = new DBHelper();

        // GET: api/Fichas
        public IQueryable<Ficha> GetFicha()
        {
            return db.Ficha;
        }

        // GET: api/Fichas/5
        [ResponseType(typeof(Ficha))]
        public async Task<IHttpActionResult> GetFicha(int id)
        {
            Ficha ficha = await db.Ficha.FindAsync(id);
            if (ficha == null)
            {
                return NotFound();
            }

            return Ok(ficha);
        }

        // PUT: api/Fichas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFicha(int id, Ficha ficha)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ficha.id)
            {
                return BadRequest();
            }

            db.Entry(ficha).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FichaExists(id))
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

        // POST: api/Fichas
        [ResponseType(typeof(Ficha))]
        public async Task<IHttpActionResult> PostFicha(Ficha ficha)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ficha.Add(ficha);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = ficha.id }, ficha);
        }

        // DELETE: api/Fichas/5
        [ResponseType(typeof(Ficha))]
        public async Task<IHttpActionResult> DeleteFicha(int id)
        {
            Ficha ficha = await db.Ficha.FindAsync(id);
            if (ficha == null)
            {
                return NotFound();
            }

            db.Ficha.Remove(ficha);
            await db.SaveChangesAsync();

            return Ok(ficha);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FichaExists(int id)
        {
            return db.Ficha.Count(e => e.id == id) > 0;
        }
    }
}