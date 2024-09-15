using BL.Interface;
using DAL.DTO;
using MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class CustomerBL : ICustomerBL
    {
        private readonly CustomerDTO _customer;
        public CustomerBL(CustomerDTO customer)
        {
            _customer = customer;
        }

        public void AddProductToCart(ProductDTO p)
        {
            _customer.Cart.Add(p);
        }
        public void AddShop(ShopDTO s)
        {
            _customer.Shops.Add(s);
        }
        public List<ProductDTO> GetAllCart()
        {
            return _customer.Cart.ToList();
        }
        public List<ShopDTO> GetAllShops()
        {
            return _customer.Shops.ToList();
        }
        public void DeleteCart()
        {
            _customer.Cart.Clear();
        }
    }
}
