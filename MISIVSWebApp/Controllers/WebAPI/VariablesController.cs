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

namespace MISIVSWebApp.Controllers.WebAPI
{
    public class VariablesController : ApiController
    {
        private DBHelper db = new DBHelper();

        // GET: api/Variables
        public IQueryable<Variable> GetVariable()
        {
            return db.Variable;
        }

        // GET: api/Variables/5
        [ResponseType(typeof(Variable))]
        public IHttpActionResult GetVariable(int id)
        {
            Variable variable = db.Variable.Find(id);
            if (variable == null)
            {
                return NotFound();
            }

            return Ok(variable);
        }

        // PUT: api/Variables/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVariable(int id, Variable variable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != variable.id)
            {
                return BadRequest();
            }

            db.Entry(variable).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VariableExists(id))
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

        // POST: api/Variables
        [ResponseType(typeof(Variable))]
        public IHttpActionResult PostVariable(Variable variable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Variable.Add(variable);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = variable.id }, variable);
        }

        // DELETE: api/Variables/5
        [ResponseType(typeof(Variable))]
        public IHttpActionResult DeleteVariable(int id)
        {
            Variable variable = db.Variable.Find(id);
            if (variable == null)
            {
                return NotFound();
            }

            db.Variable.Remove(variable);
            db.SaveChanges();

            return Ok(variable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VariableExists(int id)
        {
            return db.Variable.Count(e => e.id == id) > 0;
        }
    }
}