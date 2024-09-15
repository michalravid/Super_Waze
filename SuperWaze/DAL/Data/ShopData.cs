using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using MODELS;
using MODELS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace DAL.Data
{
    public class ShopData : IShop
    {
        private readonly ProjectContext _context;
        public ShopData(ProjectContext context)
        {
            _context = context;
        }
        public async Task<bool> AddShop(Shop s)
        {
            s.MapJson = JsonConvert.SerializeObject(s.MapMatrix);
            await _context.Shops.AddAsync(s);
            var isOk = _context.SaveChanges() >= 0;
            return isOk;
        }
        public async Task<bool> DeleteShop(int id)
        {
            var IdShop = _context.Shops.FirstOrDefault(x => x.Id_Shop == id);
            if (IdShop != null)
            {
                return false;
            }
            _context.Shops.Remove(IdShop);
            var isOk = _context.SaveChanges() >= 0;
            return isOk;
        }
        public async Task<Shop> GetShopById(int id)
        {
            Shop shop = await _context.Shops.SingleOrDefaultAsync(x => x.Id_Shop == id);
            shop.MapMatrix = JsonConvert.DeserializeObject<bool[,]>(shop.MapJson);
            return shop;
        }
        public async Task<List<Shop>> GetAllShops()
        {
            return _context.Shops.ToList();
        }

    }
}
