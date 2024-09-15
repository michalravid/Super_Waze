using BL.Services;
using DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODELS;
namespace SuperWaze.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController:ControllerBase
    {
        private readonly IShop _shop;
        private readonly JWT _jwt;

        public ShopController(IShop shop)
        {
            _shop = shop;
        }
        [AllowAnonymous]
        [HttpPost("register")]
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddShop([FromBody] Shop shop)
        {
            await _shop.AddShop(shop);
            return Ok();
        }
        [Authorize]
        [HttpDelete]
        public async Task<ActionResult> DeleteShop([FromQuery] int id)
        {
            await _shop.DeleteShop(id);
            return Ok();
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<Shop>> GetShopById([FromQuery]int id )
        {
            Shop res = await _shop.GetShopById(id);
            return Ok(res);
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<Shop>>> GetAllShops()
        {
            List<Shop> res= await _shop.GetAllShops();
            return Ok(res);
        }


    }
}
