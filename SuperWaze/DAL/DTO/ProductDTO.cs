using MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class ProductDTO
    {
        public string Name { get; set; }
        public int Id_Product { get; set; }
        public double Weight { get; set; }
        public double Price { get; set; }

        // public Location Location { get; set; }
        public int Count_Products { get; set; }
        public int Id_Shop { get; set; }
        public Shop Shop { get; set; }
        public int PositionX { get; set; }  // מיקום X בחנות
        public int PositionY { get; set; }  // מיקום Y בחנות
    }
}
