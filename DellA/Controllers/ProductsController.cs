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
    public class ProductsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Products
        public IQueryable<Product> GetProducts()
        {
            return db.Products;
        }

        // GET: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.Id)
            {
                return BadRequest();
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/Products
        [ResponseType(typeof(Product))]
        public IHttpActionResult PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Products.Add(product);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult DeleteProduct(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            db.Products.Remove(product);
            db.SaveChanges();

            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return db.Products.Count(e => e.Id == id) > 0;
        }

        [HttpGet]
        public IHttpActionResult FilterProducts(int id)
        {
            Random rnd = new Random();
            string productType = null, category = null;
            List<Product> products = new List<Product>();
            var filter = db.Products
                .Where(m => m.Id == id).ToList();
            if (filter == null)
            {
                return NotFound();
            }
            foreach (var item in filter)
            {
                productType = item.Type;
                category = item.Category;
            }
            var newFilter = db.Products.Where(m => m.Type == productType && m.Category != category && m.Id != id).ToList();
            if (newFilter.Count < 1)
            {
                return Ok(products);
            }
            else 
            {
                while (products.Count < 1)
                {
                    Product p = newFilter[rnd.Next(newFilter.Count)];
                    if (!products.Contains(p))
                    {
                        products.Add(p);
                    }
                }
                foreach (var item in filter)
                {
                    products.Add(item);
                }
            }
            
            return Ok(products);
        }

        [HttpGet]
        public IHttpActionResult productRecommendation(int id)
        {
            string category = null;
            decimal price = 0;
            var allproducts = db.Products.Where(m => m.Id != id).ToList();
            var profilter = db.Products
                .Where(m => m.Id == id).ToList();
            if (profilter == null)
            {
                return NotFound();
            }
            foreach (var item in profilter)
            {
                category = item.Category;
                price = item.Price;
            }
            var filter = db.Products.Where(m => m.Category == category && m.Price >= price && m.Id != id).ToList();

            if (filter.Count < 1)
            {
                return Ok(allproducts);
            }
            else
            {
                return Ok(filter);
            }
            
        }

        [HttpGet]
        public IHttpActionResult searchFilter(string category, decimal price)
        {
            var allproducts = db.Products.ToList();
            if (category == null || price == 0)
            {
                return Ok(allproducts);
            }
            
            var filter = db.Products.Where(m => m.Category == category && m.Price >= price).ToList();

            return Ok(filter);

        }

    }
}