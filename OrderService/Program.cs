using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Kafka;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IKafkaProducer, KafkaProducer>();

builder.Services.AddDbContext<OrderDbContext>(option =>
option.UseSqlServer("Server=192.168.1.15,1433;Database=OrdersEcommerce;User Id=sa;Password=Admin123!;TrustServerCertificate=True;"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
