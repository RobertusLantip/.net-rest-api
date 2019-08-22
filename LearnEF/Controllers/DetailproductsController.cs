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
using LearnEF.Models;
using LearnEF.Services;

namespace LearnEF.Controllers
{
    public class DetailproductsController : ApiController
    {
        private ProductsDBEntities db = new ProductsDBEntities();

        // GET: api/Detailproducts
        public IQueryable<Detailproduct> GetDetailproducts()
        {
            //return db.Detailproducts;
            DbSet<Detailproduct> DP;
            DP = db.Detailproducts;
            return DP;
        }

        [HttpGet]
        [Route("orderbyprice")]
        public IQueryable<Detailproduct> GetDetailproductsOrderByPrice()
        {
            DetailProductsServices DPS = new DetailProductsServices();
            return DPS.getDetail(db);
        }

        [HttpGet]
        [Route("check")]
        public List<string> check()
        {
            List<string> check = new List<string>();
            check.Add("check1");
            check.Add("check2");

            return check;
        }

        // GET: api/Detailproducts/5
        [ResponseType(typeof(Detailproduct))]
        public IHttpActionResult GetDetailproduct(int id)
        {
            Detailproduct detailproduct = db.Detailproducts.Find(id);
            if (detailproduct == null)
            {
                return NotFound();
            }

            return Ok(detailproduct);
        }

        // PUT: api/Detailproducts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDetailproduct(int id, Detailproduct detailproduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != detailproduct.DetailId)
            {
                return BadRequest();
            }

            db.Entry(detailproduct).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetailproductExists(id))
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

        // POST: api/Detailproducts
        [ResponseType(typeof(Detailproduct))]
        public IHttpActionResult PostDetailproduct(Detailproduct detailproduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Detailproducts.Add(detailproduct);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = detailproduct.DetailId }, detailproduct);
        }

        // DELETE: api/Detailproducts/5
        [ResponseType(typeof(Detailproduct))]
        public IHttpActionResult DeleteDetailproduct(int id)
        {
            Detailproduct detailproduct = db.Detailproducts.Find(id);
            if (detailproduct == null)
            {
                return NotFound();
            }

            db.Detailproducts.Remove(detailproduct);
            db.SaveChanges();

            return Ok(detailproduct);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DetailproductExists(int id)
        {
            return db.Detailproducts.Count(e => e.DetailId == id) > 0;
        }
    }
}