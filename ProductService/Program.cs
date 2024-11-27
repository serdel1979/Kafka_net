using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using ProductService.Kafka;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHostedService<KafkaConsumer>();

//string connectionString = "Server=192.168.1.15,1433;Database=NombreBaseDatos;User Id=sa;Password=TuContraseñaSegura;";


builder.Services.AddDbContext<ProductDbContext>(option=>
option.UseSqlServer("Server=192.168.1.15,1433;Database=ProductsEcommerce;User Id=sa;Password=Admin123!;TrustServerCertificate=True;"));


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
