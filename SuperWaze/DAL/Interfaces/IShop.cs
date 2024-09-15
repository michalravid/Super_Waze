using MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IShop
    {
        Task<bool> AddShop(Shop s);
        Task<bool> DeleteShop(int id);
        Task<Shop> GetShopById(int id);
        Task<List<Shop>> GetAllShops();
    }
}
