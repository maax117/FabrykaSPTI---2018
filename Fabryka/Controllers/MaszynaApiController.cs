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
using Fabryka;

namespace Fabryka.Controllers
{
    public class MaszynaApiController : ApiController
    {
        private FabrykaContainer db = new FabrykaContainer();

        // GET: api/MaszynaApi
        public IQueryable<Maszyna> GetMaszynaSet()
        {
            return db.MaszynaSet;
        }

        // GET: api/MaszynaApi/5
        [ResponseType(typeof(Maszyna))]
        public IHttpActionResult GetMaszyna(int id)
        {
            Maszyna maszyna = db.MaszynaSet.Find(id);
            if (maszyna == null)
            {
                return NotFound();
            }

            return Ok(maszyna);
        }

        // PUT: api/MaszynaApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMaszyna(int id, Maszyna maszyna)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != maszyna.Id)
            {
                return BadRequest();
            }

            db.Entry(maszyna).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaszynaExists(id))
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

        // POST: api/MaszynaApi
        [ResponseType(typeof(Maszyna))]
        public IHttpActionResult PostMaszyna(Maszyna maszyna)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MaszynaSet.Add(maszyna);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = maszyna.Id }, maszyna);
        }

        // DELETE: api/MaszynaApi/5
        [ResponseType(typeof(Maszyna))]
        public IHttpActionResult DeleteMaszyna(int id)
        {
            Maszyna maszyna = db.MaszynaSet.Find(id);
            if (maszyna == null)
            {
                return NotFound();
            }

            db.MaszynaSet.Remove(maszyna);
            db.SaveChanges();

            return Ok(maszyna);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MaszynaExists(int id)
        {
            return db.MaszynaSet.Count(e => e.Id == id) > 0;
        }
    }
}