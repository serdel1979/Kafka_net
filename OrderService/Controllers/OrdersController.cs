using Ecommerce.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderService.Data;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderDbContext _context;

        public OrdersController(OrderDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<List<OrderModel>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

    }
}
