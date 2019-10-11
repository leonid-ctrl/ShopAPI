using ShopDbAccess.DAL;
using ShopDbAccess.Models;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace ShopDbAccess.Controllers.API
{
    public class OrderAPIController : ApiController
    {
        private IOrderRepository orderRepository = null;

        public OrderAPIController()
        {
            this.orderRepository = new OrderRepository(new ShopContext());
        }
        public OrderAPIController(IOrderRepository repository)
        {
            this.orderRepository = repository;
        }

        /// <summary>
        /// Get all orders
        /// </summary>
        /// <returns>IEnumerable of all orders</returns>
        /// [ResponseType(typeof(IEnumerable<Order>))]
        public IHttpActionResult GetAllOrders()
        {
            return Ok(orderRepository.GetAllOrders());
        }

        /// <summary>
        /// Get single or none order by ID
        /// </summary>
        /// <param name="id">order id</param>
        /// <returns>type of order</returns>
        [ResponseType(typeof(Order))]
        public IHttpActionResult GetOrder(int id)
        {
            Order order = orderRepository.GetOrderByID(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        /// <summary>
        /// Update existing order
        /// </summary>
        /// <param name="id">order id to update</param>
        /// <param name="order">order body to update with</param>
        /// <returns>type of void</returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrder(int id, Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != order.ID)
            {
                return BadRequest();
            }
            try
            {
                orderRepository.UpdateOrder(order);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (orderRepository.GetOrderByID(id) != null)
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Create new order
        /// </summary>
        /// <param name="order">Order object to create</param>
        /// <returns>type of order</returns>
        [ResponseType(typeof(Order))]
        public IHttpActionResult PostOrder(Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            orderRepository.CreateOrder(order);
            return CreatedAtRoute("DefaultApi", new { id = order.ID }, order);
        }

        /// <summary>
        /// Delete order by ID
        /// </summary>
        /// <param name="id">Order ID to delete</param>
        /// <returns>type of void</returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteOrder(int id)
        {
            Order order = orderRepository.GetOrderByID(id);
            if (order == null)
            {
                return NotFound();
            }
            orderRepository.DeleteOrderByID(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}