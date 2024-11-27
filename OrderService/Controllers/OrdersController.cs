using Confluent.Kafka;
using Ecommerce.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OrderService.Data;
using OrderService.Kafka;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderDbContext _context;
        private readonly IKafkaProducer _producer;

        public OrdersController(OrderDbContext context, IKafkaProducer producer)
        {
            this._context = context;
            this._producer = producer;
        }

        [HttpGet]
        public async Task<List<OrderModel>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }


        [HttpPost]
        public async Task<OrderModel> CreateOrder([FromBody] OrderModel order)
        {
            order.OrderDate = DateTime.Now;
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            //Enviar mensaje
            await _producer.ProduceAsync("order-topic", new Message<string, string>
            {
                Key = order.Id.ToString(),
                Value = JsonConvert.SerializeObject(order)
            });

            return order;
        }

    }
}
