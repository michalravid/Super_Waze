using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS
{
    public class Shop
    {
        public int Id_Shop { get; set; }
        public string Name { get; set; }
        //public int[,] Matrix_Map { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<Customer> Customers { get; set; }



    }
}
