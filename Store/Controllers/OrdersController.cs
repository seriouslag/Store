using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Store.Data;
using Store.Models;
using Store.Models.Dto;

namespace Store.Controllers
{
    public class OrdersController : ApiController
    {
        private readonly StoreDbContext _db = new StoreDbContext();

        // GET: api/Orders
        public IQueryable<OrderDto> GetOrders()
        {
            return _db.Orders.Select(o => new OrderDto
            {
                Id = o.Id,
                OrderItems = o.OrderItems.Select(oi => new OrderItemDto
                {
                    ProductOptionId = oi.ProductOption.Id,
                    Quantity = oi.Quantity
                }).ToList()
            });
        }

        // GET: api/Orders/5
        [ResponseType(typeof(OrderDto))]
        public async Task<IHttpActionResult> GetOrder(int id)
        {
            var order = await _db.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(new OrderDto
            {
                Id = order.Id,
                OrderItems = order.OrderItems.Select(oi => new OrderItemDto
                {
                    ProductOptionId = oi.ProductOption.Id,
                    Quantity = oi.Quantity
                }).ToList()
            });
        }

        // PUT: api/Orders/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutOrder(int id, OrderDto order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.Id)
            {
                return BadRequest();
            }

            var item = _db.Orders.Where(o => o.Id == id);

            _db.Entry(item).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        // POST: api/Orders
        public async Task<IHttpActionResult> PostOrder(OrderDto order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Orders.Add(
                new Order(
                    order.OrderItems.Select(oi => new OrderItem(GetProductOptionById(oi.ProductOptionId),
                        oi.Quantity)).ToList()
                )
            );

            await _db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = order.Id }, order);
        }

        // DELETE: api/Orders/5
        [ResponseType(typeof(Order))]
        public async Task<IHttpActionResult> DeleteOrder(int id)
        {
            var order = await _db.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _db.Orders.Remove(order);
            await _db.SaveChangesAsync();

            return Ok(order);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private ProductOption GetProductOptionById(int id)
        {
            return _db.ProductOptions.First(po => po.Id == id);
        }

        private bool OrderExists(int id)
        {
            return _db.Orders.Count(e => e.Id == id) > 0;
        }
    }
}