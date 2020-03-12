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
using DellA.Models;

namespace DellA.Controllers
{
    public class SpecificationsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Specifications
        public IQueryable<Specification> GetSpecifications()
        {
            return db.Specifications;
        }

        // GET: api/Specifications/5
        [ResponseType(typeof(Specification))]
        public IHttpActionResult GetSpecification(int id)
        {
            Specification specification = db.Specifications.Find(id);
            if (specification == null)
            {
                return NotFound();
            }

            return Ok(specification);
        }

        // PUT: api/Specifications/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSpecification(int id, Specification specification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != specification.Id)
            {
                return BadRequest();
            }

            db.Entry(specification).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpecificationExists(id))
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

        // POST: api/Specifications
        [ResponseType(typeof(Specification))]
        public IHttpActionResult PostSpecification(Specification specification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Specifications.Add(specification);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = specification.Id }, specification);
        }

        // DELETE: api/Specifications/5
        [ResponseType(typeof(Specification))]
        public IHttpActionResult DeleteSpecification(int id)
        {
            Specification specification = db.Specifications.Find(id);
            if (specification == null)
            {
                return NotFound();
            }

            db.Specifications.Remove(specification);
            db.SaveChanges();

            return Ok(specification);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SpecificationExists(int id)
        {
            return db.Specifications.Count(e => e.Id == id) > 0;
        }
    }
}