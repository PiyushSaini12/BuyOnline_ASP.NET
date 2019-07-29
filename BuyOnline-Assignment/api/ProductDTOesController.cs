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
using BuyOnline_Assignment.Models;
using BuyOnline_Assignment.Models.Data;

namespace BuyOnline_Assignment.api
{
    public class ProductDTOesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ProductDTOesController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        // GET: api/ProductDTOes
        public IQueryable<ProductDTO> GetProductDTOes()
        {
            return db.ProductDTOes;
        }

        // GET: api/ProductDTOes/5
        [ResponseType(typeof(ProductDTO))]
        public IHttpActionResult GetProductDTO(int id)
        {
            ProductDTO productDTO = db.ProductDTOes.Find(id);
            if (productDTO == null)
            {
                return NotFound();
            }

            return Ok(productDTO);
        }

        // PUT: api/ProductDTOes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProductDTO(int id, ProductDTO productDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productDTO.Id)
            {
                return BadRequest();
            }

            db.Entry(productDTO).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductDTOExists(id))
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

        // POST: api/ProductDTOes
        [ResponseType(typeof(ProductDTO))]
        public IHttpActionResult PostProductDTO(ProductDTO productDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductDTOes.Add(productDTO);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = productDTO.Id }, productDTO);
        }

        // DELETE: api/ProductDTOes/5
        [ResponseType(typeof(ProductDTO))]
        public IHttpActionResult DeleteProductDTO(int id)
        {
            ProductDTO productDTO = db.ProductDTOes.Find(id);
            if (productDTO == null)
            {
                return NotFound();
            }

            db.ProductDTOes.Remove(productDTO);
            db.SaveChanges();

            return Ok(productDTO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductDTOExists(int id)
        {
            return db.ProductDTOes.Count(e => e.Id == id) > 0;
        }
    }
}