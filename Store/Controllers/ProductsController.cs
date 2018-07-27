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
using Store.Data;
using Store.Models;
using Store.Models.Dto;

namespace Store.Controllers
{
    public class ProductsController : ApiController
    {
        private readonly StoreDbContext _db = new StoreDbContext();

        // GET: api/Products
        public IHttpActionResult GetProducts()
        {
            if (!_db.Products.Any())
            {
                NotFound();
            }

            return Ok(_db.Products.Select(p => new ProductDto
                {
                    Id = p.Id,
                    CreatedDate = p.CreatedDate,
                    LastModified = p.LastModified,
                    Name = p.Name,
                    ProductOptions = p.ProductOptions.Select(po => new ProductOptionDto
                    {
                        Id = po.Id,
                        CreatedDate = po.CreatedDate,
                        LastModified = po.LastModified,
                        Name = po.Name,
                        Price = po.CurrentPrice,
                        QuantityInStock = po.QuantityInStock,
                        ProductOptionDescription = po.ProductOptionDescription,
                        Sku = po.Sku
                    })
                }
            ));
        }

        // GET: api/Products/5
        [ResponseType(typeof(ProductDto))]
        public async Task<IHttpActionResult> GetProduct(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(new ProductDto
            {
                Id = product.Id,
                CreatedDate = product.CreatedDate,
                LastModified = product.LastModified,
                Name = product.Name,
                ProductOptions = product.ProductOptions.Select(po => new ProductOptionDto
                {
                    Id = po.Id,
                    CreatedDate = po.CreatedDate,
                    LastModified = po.LastModified,
                    Name = po.Name,
                    Price = po.CurrentPrice,
                    QuantityInStock = po.QuantityInStock,
                    Sku = po.Sku
                })
            });
        }

        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProduct(int id, ProductDto product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.Id)
            {
                return BadRequest();
            }

            _db.Entry(new Product(product.Name, product.ProductOptions.Select(po => new ProductOption(po.Name, po.Price, po.QuantityInStock, po.ProductOptionDescription)).ToList())).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
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
        public async Task<IHttpActionResult> PostProduct(ProductDto product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Products.Add(new Product(product.Name, product.ProductOptions.Select(po => new ProductOption(po.Name, po.Price, po.QuantityInStock, po.ProductOptionDescription)).ToList()));
            await _db.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Products/5
        public async Task<IHttpActionResult> DeleteProduct(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return _db.Products.Count(e => e.Id == id) > 0;
        }
    }
}