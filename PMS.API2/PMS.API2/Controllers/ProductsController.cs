using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PMS.API2.Data;
using PMS.API2.Models;

namespace PMS.API2.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]

    public class ProductsController : Controller
    {
        private readonly PMSDbContext _pMSDbContext;

        public ProductsController(PMSDbContext pMSDbContext)
        {
            this._pMSDbContext = pMSDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _pMSDbContext.Products.ToListAsync();

            return Ok(products);
        }


        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            product.Id = Guid.NewGuid();
            await _pMSDbContext.Products.AddAsync(product);
            await _pMSDbContext.SaveChangesAsync();
            return Ok(product);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task <IActionResult> GetProduct(Guid id)
        {
            var product = await _pMSDbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }


        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid  id , Product  updateproductRequest)
        {
            var product = await _pMSDbContext.Products.FindAsync(id);
            if(product == null)
            {
                return NotFound();
            }
       
            product.Name = updateproductRequest.Name;
            product.Type= updateproductRequest.Type;
            product.Color = updateproductRequest.Color;
            product.Price= updateproductRequest.Price;

            await _pMSDbContext.SaveChangesAsync();
            
            return Ok(product);
            Console.WriteLine("Updating product with ID: {id}");

        }
        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> DeletePoroduct(Guid id)
        {
            var product = await _pMSDbContext.Products.FindAsync(id);
            if (product== null)
                return NotFound();

             _pMSDbContext.Products.Remove(product);
            await _pMSDbContext.SaveChangesAsync();
            return Ok (product);
        }


    }
}
