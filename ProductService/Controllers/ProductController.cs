using Ecommerce.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductService.Data;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductDbContext _dbContect;

        public ProductController(ProductDbContext dbContect)
        {
            this._dbContect = dbContect;
        }


        [HttpGet]
        public async Task<List<ProductModel>> GetProducts(){
            return await _dbContect.Products.ToListAsync();
        }


        [HttpGet("{Id:int}")]
        public async Task<ProductModel> GetProductById(int Id)
        {
            return await _dbContect.Products.FirstOrDefaultAsync(p => p.Id == Id);
        }

    }
}
