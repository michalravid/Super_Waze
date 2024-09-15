using MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTO;
namespace BL.Interface
{
    public interface ICustomerBL
    {
       void AddProductToCart(ProductDTO p);
       void  AddShop(ShopDTO s);
       List<ProductDTO> GetAllCart();
       List<ShopDTO> GetAllShops();
        void DeleteCart();
    }
}
