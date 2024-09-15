using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class ShopDTO
    {
        public int Id_Shop { get; set; }
        public string Name { get; set; }
        public bool[,] MapMatrix { get; set; } // מטריצה שלא תמופה ישירות ל-DB
    }
}
