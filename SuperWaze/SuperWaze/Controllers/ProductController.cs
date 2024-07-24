using DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODELS;

namespace SuperWaze.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProduct _product;
        public ProductController(IProduct product)
        {
            _product = product;
        }
        [Authorize]
        [HttpGet] 
        [Route("/GetAllProduct")]
        public async Task<ActionResult<List<Product>>> GetAllProduct([FromQuery] int id_shop)
        {
            List<Product> res = await _product.GetAllProductsByIdShop(id_shop);
            return Ok(res);
        }
        [Authorize]
        [HttpGet]
        [Route("/GetProductById")]
        public async Task<ActionResult<Product>> GetProduct([FromQuery] int id)
        {
            Product res = await _product.GetProductById(id);
            return Ok(res);
        }
        [Authorize]
        [HttpPost]
        [Route("/Add_Product")]
        public async Task<ActionResult> Add_Product([FromBody] Product p)
        {
            await _product.AddProduct(p);
            return Ok();
        }
        [Authorize]
        [HttpDelete]
        [Route("/Delete_Product")]
        public async Task<ActionResult> Delete_Product([FromQuery] int id)
        {
            await _product.DeleteProduct(id);
            return Ok();
        }
        [Authorize]
        [HttpPut]
        [Route("/Update_Count_Products")]
        public async Task<ActionResult> Update_Count_Products([FromQuery] int id, [FromQuery] int cnt)
        {
            await _product.UpdateCountProducts(id, cnt);
            return Ok();
        }
    }
}


