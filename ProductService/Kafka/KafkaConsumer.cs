
using Confluent.Kafka;
using Ecommerce.Model;
using Newtonsoft.Json;
using ProductService.Data;
using System.Text.Json.Serialization;

namespace ProductService.Kafka
{
    public class KafkaConsumer(IServiceScopeFactory scopeFactory) : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(() => {
            
                _= ConsumerAsync("order-topic",stoppingToken);

            }, stoppingToken);
        }

        public async Task ConsumerAsync(string Topic, CancellationToken stoppingToken)
        {
            var config = new ConsumerConfig
            {
                GroupId = "order-group",
                BootstrapServers = "192.168.1.15:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest,
            };
            using var consumer = new ConsumerBuilder<string, string>(config).Build();

            consumer.Subscribe(Topic);

            while (!stoppingToken.IsCancellationRequested)
            {
                var consumeResult = consumer.Consume(stoppingToken);

                var order = JsonConvert.DeserializeObject<OrderModel>(consumeResult.Message.Value);

                using var scope = scopeFactory.CreateScope();

                var dbContext = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
                var product = await dbContext.Products.FindAsync(order.ProductId);

                if(product != null)
                {
                    product.Quantity -= order.Quantity;
                    await dbContext.SaveChangesAsync();
                }
            }

            consumer.Close();
        }

    }
}
