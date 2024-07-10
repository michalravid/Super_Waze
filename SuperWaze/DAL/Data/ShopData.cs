using DAL.Interfaces;
using MODELS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public class ShopData: IShop
    {
        private readonly ProjectContext _context;
        public ShopData(ProjectContext context)
        {
            _context = context;
        }
    }
}
