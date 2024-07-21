using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using MODELS;
using MODELS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public class ProductData: IProduct
    {
        private readonly ProjectContext _context;
        public ProductData(ProjectContext context)
        {
            _context = context;
        }
        public async Task<List<Product>> GetAllProductsByIdShop(int id_shop)
        {
            List<Product> products =await _context.Products.Where(x => x.Id_Shop == id_shop).ToListAsync();
            return products;
        }
        public async Task<Product> GetProductById(int id)
        {
            Product product = await _context.Products.SingleOrDefaultAsync(x => x.Id_Product == id);
            return product;
        }
        public async Task<bool> AddProduct(Product product)
        {
            await _context.Products.AddAsync(product);
            var isOk = _context.SaveChanges() >= 0;
            return isOk;
        }
        public async Task<bool> DeleteProduct(int id)
        {
            var IdProduct = _context.Products.FirstOrDefault(x => x.Id_Product == id);
            if (IdProduct != null) 
            {
                return false;
            }
            _context.Products.Remove(IdProduct);
            var isOk = _context.SaveChanges() >= 0;
            return isOk;
        }
        public async Task<bool> UpdateCountProducts(int id,int cnt)
        {
            var IdProduct = _context.Products.FirstOrDefault(x => x.Id_Product == id);
            if (IdProduct != null)
            {
                IdProduct.Count_Products = cnt;
                var isOk =_context.SaveChanges() >= 0;
                return isOk;
            }
            return false;
        }
       
    }
}
